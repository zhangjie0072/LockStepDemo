
UIHallOfFame =  {
	uiName	= "UIHallOfFame",
	----------------------UI
	uiBack,
	uiBtnBack,
	uiProcess,
	uiGroupScroll,
	uiGroupScrollPanel,
	uiGroupGrid,
	uiProgressBar,
	uiTips,
	uiScheduleText,
	uiLeftArrow,
	uiRightArrow,
	uiAnimator,
	------------------------
	isInit = false,
	isBack = false,
	startPosX = -250,
	startVPosX = 35,
	moveOffset = 1220,
	vMoveOffset = 1220,
	transPosX = 0,
	transVPosX = 0,
	isDrag = false,
	moveTime = 0.05,
	dotOffset = 40,
	curPage = 1,
	totalPages = 1,
	isPress = false,
	moveX = 0,
	steps = 0,
	waitLoadTime = 0.7,
}

local pointSprite = 
{
	show = "com_icon_Bump",
	notShow = 'com_icon_Concavepoint'
}

function UIHallOfFame:Awake()
	self.uiBtnBack = createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack'))
	self.uiProcess = self.transform:FindChild('BackBottom/Process/Back'):GetComponent('UIProgressBar')
	self.uiScheduleText = self.transform:FindChild('BackBottom/Process/Data'):GetComponent('UILabel')
	self.uiGroupScroll = self.transform:FindChild('Scroll')--:GetComponent('UIScrollView')
	self.uiGroupScrollPanel = self.transform:FindChild('Scroll'):GetComponent('UIPanel')
	self.uiGroupGrid = self.transform:FindChild('Scroll/GroupGrid'):GetComponent('UIGrid')
	self.uiProgressBar = self.transform:FindChild('Scroll/ProgressBar'):GetComponent('UIProgressBar')
	self.uiTips = self.transform:FindChild('Tips')
	self.uiLeftArrow = self.transform:FindChild('ButtonLeft'):GetComponent('UIButton')
	self.uiRightArrow = self.transform:FindChild('ButtonRight'):GetComponent('UIButton')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIHallOfFame:Start()
	-- self.uiGroupScroll.onDragFinished = self:OnTurnPage()
	addOnClick(self.uiBtnBack.gameObject, self:OnClickBack())
	addOnClick(self.uiLeftArrow.gameObject, self:OnArrowClick())
	addOnClick(self.uiRightArrow.gameObject, self:OnArrowClick())

	self.uiLeftArrow.transform:GetComponent('BoxCollider').enabled = false
	self.uiRightArrow.transform:GetComponent('BoxCollider').enabled = false

	local pos = self.uiGroupScroll.transform.localPosition
	local vPos = self.uiGroupScrollPanel.clipOffset
	self.startPosX = pos.x
	self.startVPosX = vPos.x
	self:ShowMapGroups()
end

function UIHallOfFame:FixedUpdate()
	if self.isDrag then
		local dTime = 0.18
		local smoothValue = 0.1
		if self.moveTime < dTime then
			self.moveTime = self.moveTime + UnityTime.fixedDeltaTime * smoothValue
		else
			self.moveTime = dTime
		end
		-- print('self.moveTime = ', self.moveTime)
		local pos = self.uiGroupScroll.transform.localPosition
		local vPos = self.uiGroupScrollPanel.clipOffset
		pos.x = pos.x + (self.transPosX - pos.x) * self.moveTime
		vPos.x = vPos.x + (self.transVPosX - vPos.x) * self.moveTime
		if self.transPosX - pos.x < 2 and self.transVPosX - vPos.x < 2 then
			pos.x = self.transPosX
			vPos.x = self.transVPosX
			self.isDrag = false
			self.moveTime = 0.05
		end 
		self.uiGroupScroll.transform.localPosition = pos
		self.uiGroupScrollPanel.clipOffset = vPos
	end

	if self.steps > 0 and self.waitLoadTime > 0 then
		self.waitLoadTime = self.waitLoadTime - UnityTime.fixedDeltaTime
	end
	if self.waitLoadTime <= 0 then
		self:DistributionLoad()
	end
end

function UIHallOfFame:OnClose()
	self:ResetMap()
	self.isBack = false
	TopPanelManager:HideTopPanel()
end

function UIHallOfFame:DoClose()
	self.isBack = true
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIHallOfFame:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIHallOfFame:Refresh()
	if IsRefreshMap > 0 then
		self:ShowMapGroups()
	end
	IsRefreshMap = 0
end
--------------------------------
function UIHallOfFame:OnClickBack( ... )
	return function (go)
		self:DoClose()
	end
end

function UIHallOfFame:ShowMapGroups( ... )
	if self.isInit then return end

	self.isInit = true
	self:SortByOwned()
	local groupList = GameSystem.Instance.MapConfig.mapConfigList
	if groupList.Count % 4 == 0 then
		self.totalPages = math.floor(groupList.Count / 4)
	else
		self.totalPages = math.floor(groupList.Count / 4) + 1
	end
	self.uiProcess.value = 0
	self.uiScheduleText.text =  '0/' .. self.totalPages
	CommonFunction.ClearGridChild(self.uiGroupGrid.transform)
	self:DistributionLoad()

	while self.uiTips.transform.childCount > 0 do
		NGUITools.Destroy(self.uiTips.transform:GetChild(0).gameObject)
	end
	local startTrans = -self.totalPages * self.dotOffset / 2
	for i = 1, self.totalPages do
		local pageDot = createUI('RolePageDot', self.uiTips.transform)
		local pos = pageDot.transform.localPosition
		pos.x = startTrans
		pageDot.transform.localPosition = pos
		startTrans = startTrans + self.dotOffset
	end
	self:UpdatePageDot(1)

	local pos = self.uiGroupScroll.transform.localPosition
	local vPos = self.uiGroupScrollPanel.clipOffset
	pos.x = self.startPosX
	vPos.x = self.startVPosX
	self.uiGroupScroll.transform.localPosition = pos
	self.uiGroupScrollPanel.clipOffset = vPos

	self:UpdateSchedule()
end

function UIHallOfFame:SortByOwned( ... )
	local ownMapCount = MainPlayer.Instance.MapIDInfo.Count
	if ownMapCount <= 0 then
		return
	end

	local newList = UintList.New()
	for i = 0, MainPlayer.Instance.MapIDInfo.Count - 1 do
		local mapID = MainPlayer.Instance.MapIDInfo:get_Item(i)
		newList:Add(mapID)
		GameSystem.Instance.MapConfig.mapConfigList:Remove(mapID)
	end

	for i = 0, GameSystem.Instance.MapConfig.mapConfigList.Count - 1 do
		newList:Add(GameSystem.Instance.MapConfig.mapConfigList:get_Item(i))
	end

	GameSystem.Instance.MapConfig:SortMapList(newList)
	GameSystem.Instance.MapConfig.mapConfigList = nil
	GameSystem.Instance.MapConfig.mapConfigList = newList
end

function UIHallOfFame:UpdatePageDot(page)
	if not self.isBack then
		if page <= 1 then
			NGUITools.SetActive(self.uiLeftArrow.gameObject, false)
			NGUITools.SetActive(self.uiRightArrow.gameObject, true)
		elseif page >= self.totalPages then
			NGUITools.SetActive(self.uiLeftArrow.gameObject, true)
			NGUITools.SetActive(self.uiRightArrow.gameObject, false)
		else
			NGUITools.SetActive(self.uiLeftArrow.gameObject, true)
			NGUITools.SetActive(self.uiRightArrow.gameObject, true)
		end
	end
	local parent = self.uiTips.transform
	for i = 1, parent.childCount do
		local child = parent:GetChild(i - 1).transform
		local sprite = child:GetComponent('UISprite')
		if page == i then
			sprite.spriteName = pointSprite.show
		else
			sprite.spriteName = pointSprite.notShow
		end
	end

	self:HideMaps(page)
end

function UIHallOfFame:HideMaps(page)
	local parent = self.uiGroupGrid.transform
	local startIndex = 1
	local endIndex = self.totalPages
	if page - 1 <= 0 then
		startIndex = 1
	else
		startIndex = page - 1
	end
	if page + 1 > self.totalPages then
		endIndex = self.totalPages
	else
		endIndex = page + 1
	end

	for i = 1, self.totalPages do
		local num = 4*i
		local e = num
		if e > parent.childCount then
			e = parent.childCount
		end
		if i < startIndex then
			for j = num - 3, e do
				local child = parent:GetChild(j -1)
				if child and child.gameObject.activeSelf then
					NGUITools.SetActive(child.gameObject, false)
				end
			end
		elseif i >= startIndex and i <= endIndex then
			for j = num - 3, e do
				local child = parent:GetChild(j -1)
				if child and not child.gameObject.activeSelf then
					NGUITools.SetActive(child.gameObject, true)
				end
			end
		elseif i > endIndex then
			for j = num - 3, e do
				local child = parent:GetChild(j -1)
				if child and child.gameObject.activeSelf then
					NGUITools.SetActive(child.gameObject, false)
				end
			end
		end
	end
end

function UIHallOfFame:DistributionLoad( ... )
	local parent = self.uiGroupGrid.transform
	local groupList = GameSystem.Instance.MapConfig.mapConfigList

	if parent.childCount >= groupList.Count then return end
	
	self.steps = self.steps + 1
	local LoadNum = 8	
	local startIndex = 1
	if parent.childCount <= 0 then
		startIndex = 1
	else
		startIndex = startIndex + parent.childCount
	end
	local endIndex = startIndex + LoadNum - 1
	if endIndex > groupList.Count then
		endIndex = groupList.Count
	end
	for i=startIndex, endIndex do
		local go = createUI('HallOfFame', self.uiGroupGrid.transform)
		go.transform.name = string.format('%03d',groupList:get_Item(i - 1))
		local fameLua = getLuaComponent(go)
		fameLua:SetData(groupList:get_Item(i - 1))
	end
	self.uiGroupGrid.repositionNow = true

	if endIndex >= groupList.Count then
		for i=1, groupList.Count do
			local go = parent:GetChild(i - 1).gameObject
			local fameLua = getLuaComponent(go)
			UIEventListener.Get(go).onDrag = LuaHelper.VectorDelegate(self:OnDrag())
			fameLua:SetDelegate(LuaHelper.VectorDelegate(self:OnDrag()))
		end

		self.uiLeftArrow.transform:GetComponent('BoxCollider').enabled = true
		self.uiRightArrow.transform:GetComponent('BoxCollider').enabled = true
	end
end

function UIHallOfFame:UpdateSchedule( ... )
	local groupList = GameSystem.Instance.MapConfig.mapConfigList
	local ownedCount = MainPlayer.Instance.MapIDInfo.Count
	self.uiProcess.value = ownedCount/groupList.Count
	self.uiScheduleText.text = math.floor(groupList.Count * self.uiProcess.value) .. '/' .. groupList.Count
end

function UIHallOfFame:OnArrowClick( ... )
	return function (go)
		if not self.isDrag then
			if go == self.uiLeftArrow.gameObject and self.curPage > 1 then
				self.curPage = self.curPage - 1
			elseif go == self.uiRightArrow.gameObject and self.curPage < self.totalPages then
				self.curPage = self.curPage + 1
			end
			self:CalcNextPos(self.curPage)
			self:UpdatePageDot(self.curPage)
			self.isDrag = true
		end
	end
end

function UIHallOfFame:CalcNextPos(page)
	self.transPosX = self.startPosX - (page - 1) * self.moveOffset
	self.transVPosX = self.startVPosX + (page - 1) * self.vMoveOffset
end

function UIHallOfFame:ResetMap( ... )
	local pos = self.uiGroupScroll.transform.localPosition
	local vPos = self.uiGroupScrollPanel.clipOffset
	pos.x = self.startPosX
	vPos.x = self.startVPosX
	self.uiGroupScroll.transform.localPosition = pos
	self.uiGroupScrollPanel.clipOffset = vPos

	self.curPage = 1
	self:UpdatePageDot(self.curPage)
	self.isDrag = false
	self.isInit = false
end

function UIHallOfFame:OnDrag( ... )
	return function (go, vec)
		if not self.isDrag then
			if vec.x > 0 and self.curPage <= 1 then
				return
			end
			if vec.x < 0 and self.curPage >= self.totalPages then
				return
			end
			local pos = self.uiGroupScroll.transform.localPosition
			local vPos = self.uiGroupScrollPanel.clipOffset
			pos.x = pos.x + vec.x
			vPos.x = vPos.x - vec.x
			self.uiGroupScroll.transform.localPosition = pos
			self.uiGroupScrollPanel.clipOffset = vPos

			self.moveX = self.moveX + vec.x
			if vec.x > 0 and math.abs(self.moveX) > 10 then
				self.curPage = self.curPage - 1
				self:UpdatePageDot(self.curPage)
				self:CalcNextPos(self.curPage)
				self.moveX = 0
				self.isDrag = true
			elseif vec.x < 0 and math.abs(self.moveX) > 10 then
				self.curPage = self.curPage + 1
				self:UpdatePageDot(self.curPage)
				self:CalcNextPos(self.curPage)
				self.moveX = 0
				self.isDrag = true
			-- else
			-- 	self.moveX = 0
			end
		end
	end
end

return UIHallOfFame

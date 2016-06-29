--encoding=utf-8

------------------------------------------------------------------------
-- class name	: UIRole
-- create time   : Sat Sep 12 15:39:09 2015
------------------------------------------------------------------------

UIRole =  {
	uiName	= "UIRole",

	ITEMS_PER_PAGE = 4,
	posMap = {0,3,1,2,5,4},

	-----------------------
	-- Parameters Module --
	-----------------------
	isRoleState = true,
	isResetState = false,
	forceBackToHall  = false,	-- force to make it back to Hall.
	posBtns,
	colliderBtns,
	pos,
	roleList,
	recruitList,
	roleDataList,
	roleMyIDList,
	recruitIDList,
	curRoleItem,
	detail,
	moving = false,				-- is moving.
	movey = 0 ,					-- moving position in Y.
	curPage = 0,
	maxPage = 0,
	pageTab,
	oRoleGridPos,
	oRecruitGridPos,
	roleLink = nil,
	goodsAcquire,
	-- canGetRoleIdList = {},
	--canEnhanceRoleIdList,

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	repositionRoleGrid = false,
	needResetTween = false,
	defaultEnhance = nil,
	defaultExercise = nil,
	switchState = 0,
	expStoreList,
	-- showLink,

	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiRoleGrid,
	uiRecruitGrid,
	uiBackBtn,
	uiPosAll,
	uiPosC ,
	uiPosPF,
	uiPosSF,
	uiPosSG,
	uiPosPG,
	uiRecruitBtn,
	uiTitle,
	uiSwitchBtnLabel,
	uiAnimator,
	uiRecruitBtnRedDot,
	uiBtnFameHall,
	uiBtnFameHallRedDot,
	uiResetMask,

	colliderPosAll,
	colliderPosC ,
	colliderPosPF,
	colliderPosSF,
	colliderPosSG,
	colliderPosPG,
	-- uiBtnMenu,
}


---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIRole:Awake()
	self:UIParse()				-- Foucs on UI Parse.

	self.oRoleGridPos    = self.uiRoleGrid.transform.localPosition
end

function UIRole:Start()
	local g = createUI("ButtonBack", self.uiBackBtn.transform)
	local t = getLuaComponent(g)
	t.onClick = self:ClickBack()

	self.posBtns = {
		self.uiPosAll,
		self.uiPosC ,
		self.uiPosPF,
		self.uiPosSF,
		self.uiPosSG,
		self.uiPosPG,
	}

	for i = 1,  #self.posBtns do
		addOnClick(self.posBtns[i].gameObject,self:ClickPos(i))
	end

	self.colliderBtns = {
		self.colliderPosAll,
		self.colliderPosC ,
		self.colliderPosPF,
		self.colliderPosSF,
		self.colliderPosSG,
		self.colliderPosPG,
	}

	addOnClick(self.gameObject,self:ClickBackground())
	addOnClick(self.uiResetMask,self:ClickBackground())

	print( string.format(" x:%d y:%d z:%d", self.oRoleGridPos.x, self.oRoleGridPos.y, self.oRoleGridPos.z) )

	self.uiRoleGrid.onCustomSort = function (x, y)
		local item1 = getLuaComponent(x)
		local item2 = getLuaComponent(y)

		if item1.isHas and not item2.isHas then
			return -1
		end

		if not item1.isHas and item2.isHas then
			return 1
		end

		if item1.id > item2.id then
			return -1
		elseif item1.id < item2.id then
			return 1
		else
			return 0
		end
	end
end

function UIRole:FixedUpdate()
	if not self.moving and self.repositionRoleGrid then
		self.uiRoleGrid:Reposition()
		self.repositionRoleGrid = false
	end
end

function UIRole:OnClose()
	if self.nextShowUI == "UIHallOfFame" then
		TopPanelManager:ShowPanel(self.nextShowUI)
	else
		TopPanelManager:HideTopPanel()
	end
end

function UIRole:DoClose()
	if not self.nextShowUI or not self.nextShowUI == "UIHallOfFame" then
		if not self.isRoleState then
			self:ClickStateChange()()
			return
		end
		self:ClearGrid(1)
	end

	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIRole:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIRole:Refresh(subID)
	print("TopPanelManager refresh:",self.uiName,"--subID:",subID)
	if MainPlayer.Instance.LinkEnable then
		subID = MainPlayer.Instance.LinkRoleId
		self.forceBackToHall = true
	end

	-- self:InitGetRoleRedDot()
	------------------------------------
	self.isRoleState = true
	self.pos = 1

	self:UpdateRoleIDList()
	self:ClearGrid(0)

	if self.nextShowUI == "UIHallOfFame" then
		self.isRoleState = false
		self.nextShowUI = nil
	end
	self:ShowPage(0)
	self:ShowPage(1)

	local visualizeRole
	if subID then
		if subID == 1 then
			self:ClickRoleItem()(self:GetRoleItem(1201))
		elseif subID == 2 then		--指引强化球员
			self.isRoleState = true
			self.isResetState = false
			visualizeRole = true
		elseif subID >= 1000 and subID <= 9999 then
			visualizeRole = true
		end
	end

	if self.defaultEnhance then
		self:ShowDefaultEnhance()
		if self:FindPanelList(self.uiName) then
			print(self.uiName,"-----set defaultEnhance false")
			self:FindPanelList(self.uiName).paramsRecord["defaultEnhance"] = false
		end
	end

	if self.defaultExercise	then
		self:ShowDefaultExercise()
		if self:FindPanelList(self.uiName) then
			print(self.uiName,"-----set defaultEnhance false")
			self:FindPanelList(self.uiName).paramsRecord["defaultExercise"] = false
		end
	end

	if not self.isRoleState then
		self:ClickStateChange()()
	else
		self:PageReset()
	end

	self:DataRefresh()
	self:RefreshGetRoleRedDot()
	-- self:RefreshRoleRedDot()
--	if IsRefreshMap ~= nil then
--		NGUITools.SetActive(self.uiBtnFameHallRedDot.gameObject, IsRefreshMap > 1)
--	end

	for k, v in pairs(self.roleList) do
		v:Refresh()
	end
end

--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
------------------------------UI
function UIRole:UIParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiTitle = find("TopLeft/Title"):GetComponent("MultiLabel")
	self.uiBackBtn = find("TopLeft/ButtonBack"):GetComponent("Transform")
	self.uiRoleGrid = find("RoleScroll/RoleGrid"):GetComponent("UIGrid")
--	self.uiRecruitGrid = find("RecuitScroll/RoleGrid"):GetComponent("UIGrid")
	self.uiPosAll = find("Left/Position/All"):GetComponent("UISprite")
	self.uiPosC = find("Left/Position/C"):GetComponent("UISprite")
	self.uiPosPF = find("Left/Position/PF"):GetComponent("UISprite")
	self.uiPosSF = find("Left/Position/SF"):GetComponent("UISprite")
	self.uiPosSG = find("Left/Position/SG"):GetComponent("UISprite")
	self.uiPosPG = find("Left/Position/PG"):GetComponent("UISprite")

	self.colliderPosAll = find("Left/Position/All"):GetComponent("BoxCollider")
	self.colliderPosC = find("Left/Position/C"):GetComponent("BoxCollider")
	self.colliderPosPF = find("Left/Position/PF"):GetComponent("BoxCollider")
	self.colliderPosSF = find("Left/Position/SF"):GetComponent("BoxCollider")
	self.colliderPosSG = find("Left/Position/SG"):GetComponent("BoxCollider")
	self.colliderPosPG = find("Left/Position/PG"):GetComponent("BoxCollider")

--	self.uiRecruitBtn = find("RecruitBtn/ButtonGray"):GetComponent("UIButton")
--	self.uiRecruitBtnRedDot = find("RecruitBtn/ButtonGray/RedDot"):GetComponent("UISprite")
--	self.uiSwitchBtnLabel = find("RecruitBtn/ButtonGray/Text"):GetComponent("MultiLabel")
	self.uiTipRoundGrid = find("TipRound"):GetComponent("UIGrid")
--	self.uiResetBtn = find("ResetBtn/ButtonReset"):GetComponent("UIButton")
--	self.uiBtnFameHall = find("FameHall/ButtonFameHall"):GetComponent("UIButton")
--	self.uiBtnFameHallRedDot = find("FameHall/ButtonFameHall/RedDot")

	self.uiResetMask = find("ResetMask").gameObject

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIRole:ClickBack()
	return function()
		self:DoClose()
	end
end

function UIRole:UpdateRoleIDList()
	self.roleDataList = {}
    self.roleMyIDList = {}

    local allRoleList = GameSystem.Instance.RoleBaseConfigData2:GetConfig()
	local myRoleList = MainPlayer.Instance:GetRoleIDList()

    --玩家拥有的球员先放入列表
	local enum = myRoleList:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current
		local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
		if data.position == UIRole.posMap[self.pos] or self.pos == 1 then
			table.insert(self.roleDataList, data)
            table.insert(self.roleMyIDList, data.id)
		end
	end

    --玩家没有的球员放入列表
    local enum2 = allRoleList:GetEnumerator()
    while enum2:MoveNext() do
        local data = enum2.Current
        if data.display == 1 and not myRoleList:Contains(data.id) then
            if data.position == UIRole.posMap[self.pos] or self.pos == 1 then
			    table.insert(self.roleDataList, data)
		    end
        end
    end
end

function UIRole:ClickPos(position)
	return function()
		if self.moving then
			return
		end

		local _, _, _, itemList = self:GetCurGrid()
		self.uiRoleGrid.transform.localPosition    = self.oRoleGridPos
		self.pos = position
		if self.isRoleState then
			for k,v in pairs(self.roleDataList) do
				if v.position == UIRole.posMap[position] or position == 1 then
					if not itemList[v.id] then
						self:CreateRoleItem(v)
					end
				end
			end

			for k,v in pairs(self.roleList) do
				self:SetRoleActive(v, v.position == UIRole.posMap[position] or position == 1)
			end
			--self:UpdateRoleIDList()
		end
		self:UpdateMaxPage()
		self:ShowPage(0)
		self:SetPage(0)
		for i=1,#self.posBtns do
			self.posBtns[i]:GetComponent("UIToggle").value = (i == position)
		end
		self.moving = false
	end
end

function UIRole:SetRoleActive(role, active)
	NGUITools.SetActive(role.gameObject, active)
    print("UIRole:SetRoleActive"..tostring(active))
end

function UIRole:ClickRoleItem(index)
	return function(item, exerciseId)
		local id = item.id
		self.curRoleItem = item
		if self.isRoleState then
			if not self.isResetState then
				self.detail = TopPanelManager:ShowPanel("NewRoleDetail")
				self.detail.id = id
				self.detail.pos = self.pos
				self.detail.roleIDList = {}
				for k,v in pairs(self.roleList) do
					--按照全部拥有球员的顺序进行左右切换
					--if v.gameObject.activeSelf then
						table.insert(self.detail.roleIDList, k)
					--end
				end

				if self.detail.roleData then
					self.detail:RefreshData()
				end
			end
		end
	end
end

function UIRole:ClickDetailClose()
	return function()

	end
end

function UIRole:ClickStateChange()
	return function()
		self.isRoleState = not self.isRoleState
		self:DataRefresh()
		if self.isRoleState then
			self.uiSwitchBtnLabel:SetText(getCommonStr("STR_RECRUIT"))
			NGUITools.SetActive(self.uiResetBtn.gameObject, true)
			-- NGUITools.SetActive(self.uiBtnFameHall.gameObject, true)
			self:ClearGrid(1)
			-- self:RefreshRoleRedDot()
		else
			if self.isResetState then
				self:ClickReset()()
			end
			self.uiSwitchBtnLabel:SetText(getCommonStr("STR_MEMBER1"))
--			NGUITools.SetActive(self.uiResetBtn.gameObject, false)
--			NGUITools.SetActive(self.uiRecruitBtnRedDot.gameObject, false)
			self:ClearGrid(0)
--			self:RefreshGetRoleRedDot()
			-- NGUITools.SetActive(self.uiBtnFameHall.gameObject, false)
		end
		self:PageReset()
	end
end

function UIRole:ClickFameHall( ... )
	return function (go)
		self.nextShowUI = "UIHallOfFame"
		self:DoClose()

		-- TopPanelManager:ShowPanel("UIHallOfFame")
	end
end

function UIRole:ClickBackground()
	return function()
		if self.isResetState then
			self:ClickReset()()
			EventDelegate.Execute(self.uiResetBtn.onClick)
		end
	end
end


function UIRole:PageReset()
	for i = 0, self.curPage - 1 do
		self:MoveUp(true)
	end
	self.uiRoleGrid.transform.localPosition    = self.oRoleGridPos
--	self.uiRecruitGrid.transform.localPosition = self.oRecruitGridPos

	self:ClickPos(1)()
	self:UpdateRoleIDList()
	self:UpdateMaxPage()
	self:ShowPage(0)
	self:SetPage(0)
end

function UIRole:UpdateMaxPage()
	local item_count = 0
	for k,v in pairs(self.roleDataList) do
		if v.position == UIRole.posMap[self.pos] or self.pos == 1 then
			item_count = item_count + 1
		end
	end

	print( "item_count:".. item_count)

	self.maxPage = math.floor((item_count - 1) / 4)
	self.pageTab ={}
	CommonFunction.ClearGridChild(self.uiTipRoundGrid.transform)
	for i = 1,  self.maxPage + 1 do
		table.insert(self.pageTab, createUI("RolePageDot", self.uiTipRoundGrid.transform))
	end
	print("self.maxPage=",self.maxPage)
	self.uiTipRoundGrid:Reposition()
end

function UIRole:DataRefresh()
	local isRoleState = self.isRoleState
	NGUITools.SetActive( self.uiRoleGrid.gameObject,  isRoleState)

	if isRoleState then
		self.uiTitle:SetText(getCommonStr("STR_MEMBER1"))
	end
	self:UpdateMaxPage()
	self:SetPage(self.curPage)
end


function UIRole:OnInviteRespFromCsharp(resp)
	-- self:cur_refresh()
	local result = resp.result
	if result == 0 then
		local id = resp.role.id
		print("id=",id)
		UpdateRedDotHandler.MessageHandler("Role")
		UpdateRedDotHandler.MessageHandler("Squad")
		UpdateRedDotHandler.MessageHandler("RoleDetail")

		local role = resp.role
		local exerList = ExerciseInfoList.New()
		local enum = role.exercise:GetEnumerator()
		while enum:MoveNext() do
			local ex = ExerciseInfo.New()
			ex = enum.Current
			exerList:Add(ex)
		end
		if not MainPlayer.Instance.ExerciseInfos:ContainsKey(role.id) then
			MainPlayer.Instance.ExerciseInfos:Add(role.id, exerList)
		end

		self:RefreshGetRoleRedDot()
	end
end

function UIRole:MoveDrag()
	return function(go, vec)
		--print("UIRole MoveDrag called vec.x=",vec.x)
		if self.moving then
			return
		end

		self.movey = self.movey + vec.y
		--print("UIRole move_drag called self.movey=",self.movey)
		local grid = self:GetCurGrid()

		local pos = grid.transform.localPosition
		pos.y = pos.y + vec.y
		grid.transform.localPosition = pos
	end
end


function UIRole:OnPress()
	return function(go, pressed)
		if not pressed then
			if self.movey > 0 then
				if self.movey > 50 then
					self:MoveUp()
				else
					self:MoveBack(false, false)
				end
			elseif self.movey < 0 then
				if self.movey < -50 then
					self:MoveDown()
				else
					self:MoveBack(true, false)
				end
			end
		else
			self.movey = 0
		end
	end
end

function UIRole:IsHasRole(id)
    if self.roleMyIDList == nil then
        return false
    end

    for k, v in pairs(self.roleMyIDList) do
		if v == id then
			return true
		end
	end

    return false
end

function UIRole:CreateRoleItem(data)
	local go = createUI("NewRoleBustItem1",self.uiRoleGrid.transform)
	local s = getLuaComponent(go)
	s.id = data.id
    s.isHas = self:IsHasRole(data.id)
	s.transform.name = tostring(data.id)
	s.onClickSelect = self:ClickRoleItem()
--	local roleState = (UpdateRedDotHandler.roleSkillList[id] and next(UpdateRedDotHandler.roleSkillList[id]) ~= nil)
--				or UpdateRedDotHandler.roleLevelUpList[id]
--				or UpdateRedDotHandler.roleEnhanceList[id]
--				or UpdateRedDotHandler.roleImproveList[id] ~= nil
--	s:SetState(roleState)
--    s:Refresh()

	UIEventListener.Get(s.gameObject).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
	UIEventListener.Get(s.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())
	self.roleList[data.id] = s
end

function UIRole:GetCurGrid()
	if self.isRoleState then
		return self.uiRoleGrid, self.roleDataList, 276, self.roleList
	end
end


function UIRole:MoveDown(immediately)
	for i = 1,  #self.colliderBtns do
		self.colliderBtns[i].enabled = false
	end

	if not self.moving then
		local grid, _, movePos = self:GetCurGrid()
		if self.curPage > 0 then
			local newPos = Vector3.New(grid.transform.localPosition.x, grid.transform.localPosition.y - movePos - self.movey, 0)
			if immediately then
				grid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(grid.gameObject, 0.5, newPos)
				self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
			self:SetPage(self.curPage - 1)
		else
			self:MoveBack(true, immediately)
		end
	end
	self.movey = 0
end


function UIRole:MoveUp(immediately)
	for i = 1,  #self.colliderBtns do
		self.colliderBtns[i].enabled = false
	end

	if not self.moving then
		local grid, _, movePos = self:GetCurGrid()
		if self.curPage < self.maxPage then
			local newPos = Vector3.New(grid.transform.localPosition.x ,grid.transform.localPosition.y +movePos - self.movey, 0)
			if immediately then
				grid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
				self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
			self:SetPage(self.curPage + 1)
		else
			self:MoveBack(false, immediately)
		end
	end
	self.movey = 0
end

function UIRole:MoveBack(isLeft, immediately)
	for i = 1,  #self.colliderBtns do
		self.colliderBtns[i].enabled = false
	end

	local grid, _, movePos = self:GetCurGrid()

	if isLeft then
		local newPos = Vector3.New(grid.transform.localPosition.x ,grid.transform.localPosition.y - self.movey, 0)
		if immediately then
			grid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	else
		local newPos = Vector3.New(grid.transform.localPosition.x ,grid.transform.localPosition.y - self.movey, 0)
		if immediately then
			grid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	end
end



function UIRole:SetPage(page)
	self.curPage = page
	print("curPage = ",self.curPage)
	print("curPage #self= ",#self.pageTab)
	for i = 1, #self.pageTab do
		if i == self.curPage + 1 then
			--self.pageTab[i].transform:GetComponent("UISprite").color = Color.New(1.0, 1.0, 1.0, 1.0)
			self.pageTab[i].transform:GetComponent("UISprite").spriteName = "com_icon_Bump"
		else
			--self.pageTab[i].transform:GetComponent("UISprite").color = Color.New(0, 0.25, 0.25, 1.0)
			self.pageTab[i].transform:GetComponent("UISprite").spriteName = "com_icon_Concavepoint"
		end
	end
	self:ShowPage(page + 1)
end

function UIRole:ShowPage(page)
	local _, list, _, itemList = self:GetCurGrid()
	local startIndex = math.max(page * UIRole.ITEMS_PER_PAGE + 1, 1)
	local endIndex = math.min((page + 1) * UIRole.ITEMS_PER_PAGE, table.getn(list))

	if self.isRoleState then
		for i = startIndex, endIndex do
			if not itemList[list[i].id] then
				self:CreateRoleItem(list[i])
			end
		end
		self.uiRoleGrid:Reposition()
	end
end

function UIRole:MoveFinish()
	return function()
		self.moving = false

		for i = 1,  #self.colliderBtns do
			self.colliderBtns[i].enabled = true
		end

	end
end


function UIRole:ClickCloseRoleLink()
	return function()
		if self.roleLink then
			NGUITools.Destroy(self.roleLink.gameObject)
			self.roleLink = nil
		end
	end
end


function UIRole:ClickResetCancle()
	return function()
		if self.roleReset then
			NGUITools.Destroy(self.roleReset.gameObject)
			self.roleReset = nil
		end
	end
end


function UIRole:OnResetFromC(resp)
	CommonFunction.HideWaitMask()
	self:ClickResetCancle()()
	-- local resp, err = protobuf.decode("fogs.proto.msg.ResetRoleResp", buf)
	if resp then
		print("resp.result=",resp.result)

		if resp.result == 0 then
			self.goodsAcquire = getLuaComponent(createUI("GoodsAcquirePopup", self.transform))
			local enum = resp.resource:GetEnumerator()
			while enum:MoveNext() do
				local id = enum.Current.id
				local value = enum.Current.value
				print("id=",id)
				print("value=",value)
				self.goodsAcquire:SetGoodsData(id, value)
			end
			-- self:DataRefresh()
			UpdateRedDotHandler.MessageHandler("Role")
			UpdateRedDotHandler.MessageHandler("Squad")
			UpdateRedDotHandler.MessageHandler("RoleDetail")

			for k, v in pairs(self.roleList) do
				v:Refresh()
			end
			self:Refresh()
			-- self:RefreshGetRoleRedDot()
			-- self:RefreshRoleRedDot()
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("RoleReset:OnReceiveRese(): " .. err)
	end
end

function UIRole:InitGetRoleRedDot( ... )
	UpdateRedDotHandler.roleRecruitList = {}
	local roleBase = GameSystem.Instance.RoleBaseConfigData2:GetConfig()
	local enum = roleBase:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current
		local baseData =  GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id)
		local costId = baseData.recruit_consume_id
		local costValue = baseData.recruit_consume_value
		local allListEnum = MainPlayer.Instance.AllGoodsList:GetEnumerator()
		--print('costId ----------- ' .. costId .. '   costValue ------------- ' .. costValue)
		while allListEnum:MoveNext() do
			local n = allListEnum.Current.Value
			if n:GetID() == costId and n:GetNum() >= costValue then
				UpdateRedDotHandler.roleRecruitList[v.id] = 1
			end
		end
	end
end

function UIRole:RefreshGetRoleRedDot( ... )
--	local count = 0
--	for k,v in pairs(UpdateRedDotHandler.roleRecruitList) do
--		count = count + 1
--	end
--	if self.isRoleState then
--		NGUITools.SetActive(self.uiRecruitBtnRedDot.gameObject, count > 0)
--	else
--		NGUITools.SetActive(self.uiRecruitBtnRedDot.gameObject, false)
--	end
end

function UIRole:RefreshRoleRedDot( ... )
	--self.trainRedDotRoleIdList = {}
	local roleEnum = MainPlayer.Instance.SquadInfo:GetEnumerator()
	local parent = self.uiRoleGrid.transform
	while roleEnum:MoveNext() do
		local roleId = roleEnum.Current.role_id
		-- print('roleId = ', roleId)
		local role = parent:FindChild(tostring(roleId))
		if role then
			local lua = getLuaComponent(role.gameObject)
			local roleState = (UpdateRedDotHandler.roleSkillList[roleId] and next(UpdateRedDotHandler.roleSkillList[roleId]) ~= nil)
						or UpdateRedDotHandler.roleLevelUpList[roleId]
						or UpdateRedDotHandler.roleEnhanceList[roleId]
						or UpdateRedDotHandler.roleImproveList[roleId] ~= nil
			lua:SetState(roleState)
		end
	end
end


function UIRole:GetRoleItem(id)
	return self.roleList[id]
end

function UIRole:ShowDefaultEnhance()
	self:ClickRoleItem(4)(self:GetRoleItem(MainPlayer.Instance.CaptainID))
	self.defaultEnhance = false
end

function UIRole:ShowDefaultExercise()
	self:ClickRoleItem()(self:GetRoleItem(MainPlayer.Instance.CaptainID))
	self.defaultExercise = false
end

function UIRole:ShowExerciseLink(roleId, exerciseIndex)
	self:ClickRoleItem()(self:GetRoleItem(roleId), exerciseIndex)
end


---------------------------------
-- tag is 0 is for Role.	   --
-- tag is 1 is for Recruit	   --
---------------------------------
function UIRole:ClearGrid(tag)
	if tag == 0 then
		self.roleList = {}
		CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	elseif tag == 1 then
--		self.recruitList  = {}
--		CommonFunction.ClearGridChild(self.uiRecruitGrid.transform)
	else
		error("UIRole:ClearGrid(tag) --> tag should set 0 or 1,  but you set",tag )
	end
end

function UIRole:FindPanelList(paneName)
local start = TopPanelManager.panelList:Begin()
  repeat
    if paneName == start:Value().name then
      return start:Value()
    end
  until start:Next() == false
  return nil
end

return UIRole

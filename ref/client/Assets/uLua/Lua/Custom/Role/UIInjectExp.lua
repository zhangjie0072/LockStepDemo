------------------------------------------------------------------------
-- class name    : UIInjectExp
-- create time   : Fri Nov  6 21:19:39 2015
------------------------------------------------------------------------

UIInjectExp =  {
	uiName     = "UIInjectExp",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiGoodsGrid  ,
	uiRoleGrid   ,
	uiLeft       ,
	uiRight      ,
	uiButtonBack ,
	uiAnimator,

	-----------------------
	-- Parameters Module --
	-----------------------
	expItemIds,
	EXP_ITEM1,
	EXP_ITEM2,
	EXP_ITEM3,
	roleIDList,
	roleList,
	curPage = 0,
	maxPage = 0,
	moving = false,
	movex = 0,
	ITEMS_PER_PAGE = 3,
	curExpItem,
	pressedTime,
	pressedRole = nil,
	aniDur,
	prevExp,
	prevLv,
	oGridx,
	goodsEnough = true,
	durBase,
	durRate,
	durMax,
	prePressedTime,
	firstItem,
	initExpId,
	initExpItem,
	respError,
	respErrorDur = 1,
	respErrorCounter = 0,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIInjectExp:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function UIInjectExp:Start()
	addOnClick(self.uiLeft.gameObject, self:ClickLeft(false))
	addOnClick(self.uiRight.gameObject, self:ClickLeft(true))
	self.testCounter = 1
	local s = getLuaComponent(self.uiButtonBack.gameObject)
	s.onClick = self:ClickBack()

	self.expItemIds = {}
	table.insert(self.expItemIds, self.EXP_ITEM1)
	table.insert(self.expItemIds, self.EXP_ITEM2)
	table.insert(self.expItemIds, self.EXP_ITEM3)


	print("self.initExpId=",self.initExpId)
	for k, v in ipairs( self.expItemIds ) do
		local s = getLuaComponent(createUI("GoodsIcon", self.uiGoodsGrid.transform))
		s.goodsID = v
		s.displayLeftNum = true
		s.needPlayAnimation = true
		s.onClick = self:ClickExpItem()
		if self.firstItem == nil then self.firstItem = s end
		if v == self.initExpId then self.initExpItem = s end
	end
	if self.initExpItem then
		self:ClickExpItem()(self.initExpItem)
	else
		self:ClickExpItem()(self.firstItem)
	end

	self.uiGoodsGrid:Reposition()

	self.roleList = {}
	self:UpdateRoleIDList()

	for i = 1, math.min(table.getn(self.roleIDList), UIInjectExp.ITEMS_PER_PAGE * 2) do
		self:CreateRoleItem(self.roleIDList[i])
	end

	self:UpdateMaxPage()
	LuaHelper.RegisterPlatMsgHandler(MsgID.UseGoodsRespID, self:OnRecevieUseGoods(), self.uiName)
	self:Refresh()
end

function UIInjectExp:Refresh()
	CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	self.roleList = {}

	self.aniDur = self.durBase
	self:PageReset()
	self:DataRefresh()
end

-- uncommoent if needed
function UIInjectExp:FixedUpdate()
	-- check sendable.
	if self.respError then
		self.respErrorCounter =	self.respErrorCounter + UnityTime.fixedDeltaTime
		if self.respErrorCounter >= self.respErrorDur then
			self.respError = nil
			self.respErrorCounter = 0
		else
			-- display error then send next time.
			return
		end
	end

	if self.pressedRole and UnityTime.time - self.prePressedTime > self.aniDur then
		self:ClickRoleItem()(self.pressedRole)
		self.prePressedTime = UnityTime.time

		if self.goodsEnough then
			local passed= math.max(math.modf(UnityTime.time - self.pressedTime)-1, 1)
			local powerRate = math.min(passed, self.durMax)
			self.aniDur = self.durBase * math.pow(self.durRate, powerRate)
		end
	end


end


function UIInjectExp:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
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
---------------------------------------------------------------------------------------------------
function UIInjectExp:UiParse()
	self.uiGoodsGrid  = self.transform:FindChild("Bottom/Goods/GridGoods"):GetComponent("UIGrid")
	self.uiRoleGrid   = self.transform:FindChild("Middle/RoleScroll/Grid"):GetComponent("UIGrid")
	self.uiLeft       = self.transform:FindChild("Middle/Left"):GetComponent("UISprite")
	self.uiRight      = self.transform:FindChild("Middle/Right"):GetComponent("UISprite")
	self.uiButtonBack = self.transform:FindChild("ButtonBack/ButtonBack"):GetComponent("UIButton")
	self.uiAnimator   = self.transform:GetComponent("Animator")
end


function UIInjectExp:SetData()

end

function UIInjectExp:RoleCompare(xid, yid)
	local datax = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(xid)
	local datay = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(yid)

	local xInSquad = MainPlayer.Instance:IsInSquad(xid)
	local yInSquad = MainPlayer.Instance:IsInSquad(yid)

	if xInSquad and not yInSquad then
		return -1
	elseif not xInSquad and yInSquad then
		return 1
	end

	local xInfo = MainPlayer.Instance:GetRole2(xid)
	local yInfo = MainPlayer.Instance:GetRole2(yid)

	if xInfo.level < yInfo.level then
		return 1
	elseif xInfo.level > yInfo.level then
		return -1
	end

	if xInfo.quality < yInfo.quality then
		return 1
	elseif xInfo.quality > yInfo.quality then
		return -1
	end

	if xInfo.star < yInfo.star then
		return 1
	elseif xInfo.star > yInfo.star then
		return -1
	end
	local xTalent = GameSystem.Instance.RoleBaseConfigData2:GetTalent(xid)
	local yTalent = GameSystem.Instance.RoleBaseConfigData2:GetTalent(yid)

	if xTalent < yTalent then
		return 1
	elseif xTalent > yTalent then
		return -1
	end

	if xid < yid then
		return 1
	elseif xid > yid then
		return -1
	else
		return 0
	end
end


function UIInjectExp:UpdateRoleIDList()
	self.roleIDList = {}

	local idList = MainPlayer.Instance:GetRoleIDList()
	local enum = idList:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current
		local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
		table.insert(self.roleIDList, id)
	end

	table.sort(self.roleIDList, function (x, y)
				   return self:RoleCompare(x, y) == -1
	end)
end


function UIInjectExp:MoveLeft(immediately)
	print("UIInjectExp:MoveLeft")
	if not self.moving then
		local grid, _, movePos = self:GetCurGrid()
		print("UIInjectExp:MoveLeft-1, self.curPage=", self.curPage, ", self.maxPage=", self.maxPage)
		if self.curPage < self.maxPage then
			local newPos = Vector3.New(grid.transform.localPosition.x - movePos - self.movex, grid.transform.localPosition.y , 0)
			print("UIInjectExp:MoveLeft-2")
			if immediately then
				grid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(grid.gameObject, 0.5, newPos)
				self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
				print("UIInjectExp:MoveLeft-3")
			end
			self:SetPage(self.curPage + 1)
			print("UIInjectExp:MoveLeft-4")
		else
			print("UIInjectExp:MoveLeft-5")
			self:MoveBack(true, immediately)
		end
	end
	print("UIInjectExp:MoveLeft-6")
	self.movex = 0
end

function UIInjectExp:MoveRight(immediately)
	print("UIInjectExp:MoveRight")
	if not self.moving then
		local grid, _, movePos = self:GetCurGrid()
		if self.curPage > 0 then
			local newPos = Vector3.New(grid.transform.localPosition.x +movePos - self.movex,grid.transform.localPosition.y , 0)
			if immediately then
				grid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
				self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
			self:SetPage(self.curPage - 1)
		else
			self:MoveBack(false, immediately)
		end
	end
	self.movex = 0
end


function UIInjectExp:GetCurGrid()
	return self.uiRoleGrid, self.roleIDList, 986, self.roleList
end

function UIInjectExp:MoveFinish()
	return function()
		self.moving = false
	end
end

function UIInjectExp:ClickLeft(isLeft)
	return function()
		if isLeft then
			self:MoveLeft()
		else
			self:MoveRight()
		end
	end
end

function UIInjectExp:ClickBack()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIInjectExp:ShowPage(page)
	print(self.uiName, "ShowPage:", page)
	local _, list, _, itemList = self:GetCurGrid()
	local startIndex = math.max(page * UIInjectExp.ITEMS_PER_PAGE + 1, 1)
	local endIndex = math.min((page + 1) * UIInjectExp.ITEMS_PER_PAGE, table.getn(list))
	for i = startIndex, endIndex do
		if not itemList[list[i]] then
			self:CreateRoleItem(list[i])
		end
	end
	self.uiRoleGrid:Reposition()
end

function UIInjectExp:SetPage(page)
	self.curPage = page
	self:ShowPage(page + 1)
	NGUITools.SetActive(self.uiLeft.gameObject, self.curPage ~= 0 )
	NGUITools.SetActive(self.uiRight.gameObject, self.curPage ~= self.maxPage )
end

function UIInjectExp:CreateRoleItem(id)
	local go = createUI("RoleBustItem1",self.uiRoleGrid.transform)
	local s = getLuaComponent(go)
	s.id = id
	s.transform.name = tostring(id)
	s.onClickSelect = self:ClickRoleItem()
	s:SetState(false)
	s.displayExpSchedule = true

	-- UIEventListener.Get(s.gameObject).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
	UIEventListener.Get(s.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())

	self.roleList[id] = s
end

function UIInjectExp:ClickRoleItem()
	return function(item)
		local category = fogs.proto.msg.GoodsCategory.GC_CONSUME
		local target = item.id
		local exp_card = 1
		local goods = self:FindAvailableGoods(self.curExpItem.goodsID)
		self.prevExp = MainPlayer.Instance:GetRole2(item.id).exp
		self.prevLv = MainPlayer.Instance:GetRole2(item.id).level
		-- for i = 1, self.prevLv -1 do
		--	self.prevExp = self.prevExp - GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		-- end
		if goods == nil then
			CommonFunction.ShowTip(getCommonStr("NO_CURRENT_ITEM"), nil)
			self.goodsEnough = false
			self.aniDur = 1.0
			return
		end
		self.goodsEnough = true
		local uuid = goods:GetUUID()
		print("category=",category)
		print("target=",target)
		print("exp_card=",exp_card)
		print("uuid=",uuid)
		local operation = {
			uuid = uuid,
			category = tostring(category),
			target = item.id,
			exp_card = exp_card
		}
		local req = protobuf.encode("fogs.proto.msg.UseGoods",operation)
        CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLuaNoWait(MsgID.UseGoodsID,req)
		-- Do not use ShowWaitMask for player continue clicking.
		-- CommonFunction.ShowWaitMask()

	end
end

function UIInjectExp:FindAvailableGoods(goodsId)
	local category = fogs.proto.msg.GoodsCategory.GC_CONSUME
	local goodsList = MainPlayer.Instance:GetGoodsList(category, goodsId)

	local enum = goodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current
		if goods then
			return goods
		end
	end
end


function UIInjectExp:MoveDrag()
	return function(go, vec)
		if self.moving then
			return
		end

		self.movex = self.movex + vec.x
		local grid = self:GetCurGrid()

		local pos = grid.transform.localPosition
		pos.x = pos.x + vec.x
		grid.transform.localPosition = pos
	end
end


function UIInjectExp:OnPress()
	return function(go, pressed)
		if pressed then
			self.pressedTime = UnityTime.time
			self.prePressedTime = UnityTime.time
			self.isPressed = pressed
			self.pressedRole = self:GetRoleItem(tonumber(go.transform.name))
		else
			self.isPressed = pressed
			self.pressedRole = nil
			self.aniDur = self.durBase
		end
	end
	--	if not pressed then
	--		if self.movex > 0 then
	--			if self.movex > 50 then
	--				self:MoveRight()
	--			else
	--				self:MoveBack(false, false)
	--			end
	--		elseif self.movex < 0 then
	--			if self.movex < -50 then
	--				self:MoveLeft()
	--			else
	--				self:MoveBack(true, false)
	--			end
	--		end
	--	else
	--		self.movex = 0
	--	end
	-- end
end

function UIInjectExp:MoveBack(isLeft, immediately)
	local grid, _, movePos = self:GetCurGrid()

	if isLeft then
		local newPos = Vector3.New(grid.transform.localPosition.x - self.movex,grid.transform.localPosition.y , 0)
		if immediately then
			grid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	else
		local newPos = Vector3.New(grid.transform.localPosition.x  - self.movex,grid.transform.localPosition.y , 0)
		if immediately then
			grid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(grid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	end
end

function UIInjectExp:OnClose()
	TopPanelManager:HideTopPanel()
end


function UIInjectExp:UpdateMaxPage()
	local _, list = self:GetCurGrid()
	self.maxPage = math.floor((table.getn(list) - 1) / 3)
	print("self.maxPage=",self.maxPage)
end

function UIInjectExp:ClickExpItem()
	return function(item)
		if self.curExpItem then
			self.curExpItem.transform:FindChild("Sele").gameObject:SetActive(false)
		end
		self.curExpItem = item
		self.curExpItem.transform:FindChild("Sele").gameObject:SetActive(true)
		self.goodsEnough = true
	end
end

function UIInjectExp:OnRecevieUseGoods()
	return function(buf)
		self.testCounter = self.testCounter + 1
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.UseGoodsResp", buf)
		if resp then
			print("resp.result=",resp.result)
			if resp.result == 0 then
				local target = resp.target
				local target_exp = resp.target_exp
				local target_level = resp.target_level
				MainPlayer.Instance:SetRoleLvAndExp(target,target_level,target_exp)
				local role = self:GetRoleItem(target)
				role:SetLevel(true)
				role:SetExp()

				role.aniDur = self.aniDur
				self.curExpItem:StartSparkle()
				role:StartExpAni(self.prevLv, self.prevExp, target_level, target_exp, self.aniDur)
				-- self:DataRefresh()
				if self.curExpItem then
					self.curExpItem:Refresh()
				end
			else
				if self.respError ~= resp.result then
					CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				end
				self.respError = resp.result
			end
		else
			error("OnRecevieUseGoodsd resp(): " .. err)
		end

	end
end


function UIInjectExp:DataRefresh()
	if self.curExpItem then
		self.curExpItem:Refresh()
	end

	for k, v in pairs(self.roleList) do
		v:Refresh()
	end
end

function UIInjectExp:GetRoleItem(id)
	return self.roleList[id]
end

function UIInjectExp:PageReset()
	for i = 0, self.curPage - 1 do
		self:MoveRight(true)
	end
	self:UpdateRoleIDList()
	self:UpdateMaxPage()
	self:ShowPage(0)
	self:SetPage(0)
end


return UIInjectExp

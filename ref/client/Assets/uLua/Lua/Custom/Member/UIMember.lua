--encoding=utf-8

UIMember =  {
	uiName = 'UIMember',

	ITEMS_PER_PAGE = 4,
	posMap = {0,3,1,2,5,4},
	positions ={'PF','SF','C','PG','SG'},

	----------------parameters
	isGoBack = false,
	--backClick,
	showlist = nil,
	showCache = nil,
	roleLuaTable = nil,
	positionTable = nil,
	RoleAddIndex = nil,
	moving = false,
	movex = 0,
	repositionRoleGrid = false,
	parent,
	selectedRole = nil,
	toReplaceRoleId,
	oriX,

	--------------UI
	uiPlayer = {},
	uiRoleSV,
	uiRoleGrid ,
	uiChooseBtn ,
	uiAnimator,

	uiAllToggle,
 }


-----------------------------------------------------------------
function UIMember:Awake()
	self.uiTitle = self.transform:FindChild("Top/Title"):GetComponent("MultiLabel")
	self.uiChooseTitle = self.transform:FindChild("Choose/Text"):GetComponent("MultiLabel")
	self.uiChooseTitle:SetText( getCommonStr("BUTTON_CONFIRM"))
	self.uiPlayer[1] = self.transform:FindChild("Player/Player1/CareerRoleIcon")
	self.uiPlayer[2] = self.transform:FindChild("Player/Player2/CareerRoleIcon")
	self.uiPlayer[3] = self.transform:FindChild("Player/Player3/CareerRoleIcon")
	self.uiTipRoundGrid = self.transform:FindChild("TipRound"):GetComponent("UIGrid")
	self.uiRoleSV = self.transform:FindChild("Scroll"):GetComponent("UIScrollView")
	self.uiRoleScrollBar = self.transform:FindChild("ScrollBar"):GetComponent("UIProgressBar")
	self.uiRoleGrid = self.transform:FindChild("Scroll/MemberGrid")
	self.uiBackGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiGrid = self.uiRoleGrid:GetComponent("UIGrid")
	----btn
	self.uiChooseBtn = self.transform:FindChild("Choose")
	addOnClick(self.uiChooseBtn.gameObject,self:MakeOnChoose())
	--------------btn choose
	self.uiPosAll = find("Position/All"):GetComponent("UISprite")
	self.uiPosC = find("Position/C"):GetComponent("UISprite")
	self.uiPosPF = find("Position/Pf"):GetComponent("UISprite")
	self.uiPosSF = find("Position/Sf"):GetComponent("UISprite")
	self.uiPosSG = find("Position/Sg"):GetComponent("UISprite")
	self.uiPosPG = find("Position/Pg"):GetComponent("UISprite")
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

	self.uiAllToggle = find("Position/All"):GetComponent("UIToggle")

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIMember:Start()
	self.positionTable = {}
	local backBtn = getLuaComponent(createUI("ButtonBack",self.uiBackGrid))
	backBtn.onClick = self:OnBackClick()
	--self:SetMode()

	--sort
	self.uiGrid.onCustomSort = function(x, y)
		return self:RoleCompare(getLuaComponent(x).id, getLuaComponent(y).id)
	end
	self.oriX = self.uiRoleGrid.localPosition
end

function UIMember:RoleCompare(xid, yid)
	local datax = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(xid)
	local datay = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(yid)

	local position = self.pos
	local ax = datax.position == UIMember.posMap[position] or position == 1
	local ay = datay.position == UIMember.posMap[position] or position == 1

	if not ax and ay then
		return 1
	elseif ax and not ay then
		return -1
	elseif not ax and not ay then
		return 0
	end

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

function UIMember:FixedUpdate()
	if not self.moving and self.repositionRoleGrid then
		self.uiRoleGrid:Reposition()
		self.repositionRoleGrid = false
	end
end

function UIMember:OnClose( ... )
	self.showCache = nil
	self.RoleAddIndex = nil
	self.selectedRole = nil

	TopPanelManager:HideTopPanel()
end

function UIMember:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIMember:Refresh(subID)
	self.uiAllToggle.value = true

	--refresh role list
	for k , v in pairs( self.positionTable) do
		NGUITools.SetActive( k,true)
	end
	self.pos = 1
	self.roleLuaTable = {}
	self.positionTable = {}
	self:UpdateRoleIDList()

	CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	self:ShowPage(0)
	self:ShowPage(1)
	self.uiGrid:Reposition()

	self.showCache ={}
	self.RoleAddIndex = {}
	local flag = 0
	for id,index in pairs(self.showlist) do
		self.showCache[id] = index
		flag = 1
	end
	if self.isGoBack == true and flag == 0 then
		self.RoleAddIndex[1] = 1
		self.RoleAddIndex[2] = 1
		self.RoleAddIndex[3] = 1
	end
	--self.showCache = self.showlist
	for id,index in pairs(self.showCache) do
		local child
		if self.uiPlayer[index].transform.childCount > 0 then
			child = self.uiPlayer[index].transform:GetChild(0)
		else
			child = createUI("CareerRoleIcon",self.uiPlayer[ index].transform)
		end
		if child then
			local player = getLuaComponent(child)
			player.id = id
			player.onClick = self:ClickIcon()
			player:Refresh()
			player.transform.parent.parent.name = "Player" .. id
		end
	end
	self:SetMode()
	self:PageReset()

	if subID == 2 then
		--直接翻到指定球员所在的页
		self.uiGrid.onReposition = function ()
			self.uiGrid.onReposition = nil
			local roles = GameSystem.Instance.roleGiftConfig:GetRoleGiftList(1)
			print(self.uiName, "role count", table.getn(self.roleIDList))
			for i = 1, table.getn(self.roleIDList) do
				local id = self.roleIDList[i]
				print(self.uiName, "onReposition", id)
				if roles:Contains(id) then
					local page = math.floor((i - 1)/4)
					print(self.uiName, "page", page)
					self:ShowPage(page)
					local item = self.roleLuaTable[id]
					item.gameObject.name = "RoleToAppear"
					for j = 1, page do
						self:MoveLeft(true)
					end
					break
				end
			end
		end
		self.uiGrid:Reposition()
	end
end

function UIMember:CreateRoleItem(id)
	local role = getLuaComponent(createUI("RoleBustItem1",self.uiRoleGrid))
	role.id = id
	role.gameObject.name = "RoleBustItem1(" .. id .. ")"
	role.onClickSelect = self:OnClickRole( role )
	role.useType = "Challenge"
	self.roleLuaTable[id] = role

	--add drag press
	UIEventListener.Get(role.gameObject).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
	UIEventListener.Get(role.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())

	local position = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id).position
	self.positionTable[role.gameObject] = self.positions[position]
end

-----------------------------------------------------------------
function UIMember:ClickPos( i )
	return function()
		for i = 0, self.curPage - 1 do
			self:MoveRight(true)
		end
		self.pos = i
		-- for k , v in pairs( self.positionTable) do
		--	NGUITools.SetActive( k,true)
		-- end
		-- self.uiGrid:Reposition()
		local position = {}
		position[1] = "All"
		position[2] = "C"
		position[3] = "PF"
		position[4] = "SF"
		position[5] = "SG"
		position[6] = "PG"
		print("positionTable:",self.positionTable)
		for k , v in pairs( self.positionTable) do
			print("positionTable:",v)
			if (v ~= position[ i]) and (i ~= 1) then
				print("set false:",k)
				NGUITools.SetActive( k,false)
			else
				print("set true:",k)
				NGUITools.SetActive( k,true)
			end
			local tween = k:GetComponent("TweenPosition")
			if tween then
				NGUITools.Destroy(tween)
			end
		end

		self:UpdateRoleIDList()
		self:UpdateMaxPage()
		self.uiGrid:Reposition()
		self.uiRoleSV:ResetPosition()
		self:ShowPage(0)
		self:SetPage(0)
		-- for i=1,#self.posBtns do
		--	self.posBtns[i]:GetComponent("UIToggle").value = (i == position)
		-- end
		self.moving = false
	end
end

function UIMember:UpdateRoleIDList()
	self.roleIDList = {}
	local list =  MainPlayer.Instance:GetRoleIDList()
	local enum = list:GetEnumerator()
	while enum:MoveNext() do
		if not MainPlayer.Instance:IsInSquad(enum.Current) then
			local position = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(enum.Current).position
			if self.pos == 1 or position == self.posMap[self.pos] then
				table.insert(self.roleIDList, enum.Current)
			end
		end
	end

	table.sort(self.roleIDList, function (x, y)
		return self:RoleCompare(x, y) == -1
	end)
end

function UIMember:SetMode()
	for id, item in pairs(self.roleLuaTable) do
		if self.showCache[ item.id] then --已选
			NGUITools.SetActive( item.uiMask.gameObject,true)
			if not self.uiPlayer[ self.showCache[ item.id]]:FindChild("CareerRoleIcon(Clone)") then
				local player = getLuaComponent(createUI("CareerRoleIcon", self.uiPlayer[ self.showCache[ item.id]]))
				player.id = item.id
				player.onClick = self:ClickIcon()
				player.transform.parent.parent.name = "Player" .. item.id
			end
			if self.RoleAddIndex[self.showCache[item.id]] == 1 then
				NGUITools.Destroy(self.uiPlayer[ self.showCache[item.id]]:FindChild("CareerRoleIcon(Clone)").gameObject)
				self.showCache[item.id] = nil
				NGUITools.SetActive( item.uiMask.gameObject,false)
			end
		else
			NGUITools.SetActive( item.uiMask.gameObject,false)
		end
	end
end

function UIMember:ClickIcon()
	return function(obj)
		NGUITools.Destroy(obj.gameObject)
		if not self.RoleAddIndex[ self.showCache[ obj.id]] then
			self.RoleAddIndex[ self.showCache[ obj.id]] = 1
			self.showCache[ obj.id] = nil
		end
		self:SetMode()
	end
end

function UIMember:OnBackClick()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMember:OnClickRole( role)
	return function()
		if self.selectedRole then
			self.selectedRole.uiMask.gameObject:SetActive(false)
		end
		if self.selectedRole and self.selectedRole.id == role.id then
			self.selectedRole = nil
			return
		end
		self.selectedRole = role
		self.selectedRole.uiMask.gameObject:SetActive(true)
		-- if NGUITools.GetActive(role.uiMask.gameObject) then
		--	self.RoleAddIndex[self.showCache[role.id]] = 1
		--	--return
		-- elseif self:GetChooseNum() >= 3 then
		--	--return
		-- elseif self:CountAddIndex() > 0 then
		--	self.showCache[ role.id] = self:GetMinIndex()
		--	self.RoleAddIndex[ self:GetMinIndex()] = nil
		-- end
		-- self:SetMode()
	end
end

function UIMember:CountAddIndex()
	local count = 0
	for k,v in pairs(self.RoleAddIndex) do
		count = count + 1
	end
	return count
end

function UIMember:GetMinIndex()
	local min = 5
	for k,v in pairs(self.RoleAddIndex) do
		if k < min then min = k end
	end
	return min
end

function UIMember:MakeOnChoose()
	return function()
		if not self.selectedRole then
			self:OnBackClick()()
			return
		end

		local info = {}
		local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
		while enum:MoveNext() do
			local id = enum.Current.role_id
			local st = enum.Current.status

			if id == self.toReplaceRoleId then
				id = self.selectedRole.id
			end
			table.insert(info, {role_id = id, status = st:ToString()})
		end
		-- for k, v in pairs(MainPlayer.Instance.SquadInfo or {}) do
		--	table.insert(info, {role_id = k, status = FightStatus.IntToEnum(v):ToString()})
		-- end
		local changeFightRole = {
			info = info,
		}
		local msg = protobuf.encode("fogs.proto.msg.ChangeFightRole", changeFightRole)
		LuaHelper.SendPlatMsgFromLua(MsgID.ChangeFightRoleID, msg)
		LuaHelper.RegisterPlatMsgHandler(MsgID.ChangeFightRoleRespID, self:OnChangeFightRoleRespHandle(), self.uiName)
		CommonFunction.ShowWait()


		-- if self:GetChooseNum() == 3 then
		--	local info = {}
		--	for k, v in pairs(self.showCache or {}) do
		--		table.insert(info, {role_id = k, status = FightStatus.IntToEnum(v):ToString()})
		--	end
		--	local changeFightRole = {
		--		info = info,
		--	}
		--	local msg = protobuf.encode("fogs.proto.msg.ChangeFightRole", changeFightRole)
		--	LuaHelper.SendPlatMsgFromLua(MsgID.ChangeFightRoleID, msg)
		--	LuaHelper.RegisterPlatMsgHandler(MsgID.ChangeFightRoleRespID, self:OnChangeFightRoleRespHandle(), self.uiName)
		-- else
		--	CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
		-- end
	end
end

function UIMember:GetChooseNum()
	local count = 0
	for _,v in pairs(self.showCache) do
		count = count + 1
	end
	return count
end

function UIMember:OnChangeFightRoleRespHandle( ... )
	return function (buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.ChangeFightRoleRespID, self.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.ChangeFightRoleResp", buf)
		if resp then
			if resp.result == 0 then
				print('----------------- resp.info: ', resp.info)
				for k, v in pairs(resp.info) do
					local squadInfo = MainPlayer.Instance.SquadInfo
					local enum = squadInfo:GetEnumerator()
					while enum:MoveNext() do
						if enum.Current.status:ToString() == v.status then
							enum.Current.role_id = v.role_id
							break
						end
					end
					if v.status == 'FS_MAIN' then
						MainPlayer.Instance.CaptainID = v.role_id
						IsInitPlayerModel = false
					end
				end

				UpdateRedDotHandler.MessageHandler("Role")
				UpdateRedDotHandler.MessageHandler("Squad")
				UpdateRedDotHandler.MessageHandler("RoleDetail")
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("OnChangeFightRoleRespHandle: " .. err)
		end
		self.parent:InitPlayerList()
		self:OnBackClick()()
	end
end

function UIMember:MoveDrag()
	return function(go, vec)
		print("UIRole MoveDrag called vec.x=",vec.x)
		if self.moving then
			return
		end

		self.movex = self.movex + vec.x

		local pos = self.uiGrid.transform.localPosition
		pos.x = pos.x + vec.x
		self.uiGrid.transform.localPosition = pos
	end
end


function UIMember:OnPress()
	return function(go, pressed)
		if not pressed then
			if self.movex > 0 then
				if self.movex > 50 then
					self:MoveRight()
				else
					self:MoveBack(false, false)
				end
			elseif self.movex < 0 then
				if self.movex < -50 then
					self:MoveLeft()
				else
					self:MoveBack(true, false)
				end
			end
		else
			self.movex = 0
		end
	end
end

function UIMember:MoveLeft(immediately)
	if not self.moving then
		if self.curPage < self.maxPage then
			local newPos = Vector3.New(self.uiGrid.transform.localPosition.x - self.uiGrid.cellWidth*4 - self.movex,self.uiGrid.transform.localPosition.y , 0)
			if immediately then
				self.uiGrid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(self.uiGrid.gameObject, 0.5, newPos)
				self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
			self:SetPage(self.curPage + 1)
		else
			self:MoveBack(true, immediately)
		end
	end
	self.movex = 0
end


function UIMember:MoveRight(immediately)
	if not self.moving then
		if self.curPage > 0 then
			local newPos = Vector3.New(self.uiGrid.transform.localPosition.x + self.uiGrid.cellWidth*4 - self.movex,self.uiGrid.transform.localPosition.y , 0)
			if immediately then
				self.uiGrid.transform.localPosition = newPos
			else
				self.tween = TweenPosition.Begin(self.uiGrid.gameObject,0.5,newPos)
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

function UIMember:MoveBack(isLeft, immediately)
	if isLeft then
		local newPos = Vector3.New(self.uiGrid.transform.localPosition.x - self.movex,self.uiGrid.transform.localPosition.y , 0)
		if immediately then
			self.uiGrid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(self.uiGrid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	else
		local newPos = Vector3.New(self.uiGrid.transform.localPosition.x  - self.movex,self.uiGrid.transform.localPosition.y , 0)
		if immediately then
			self.uiGrid.transform.localPosition = newPos
		else
			self.tween = TweenPosition.Begin(self.uiGrid.gameObject,0.5,newPos)
			self.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
			self.moving = true
		end
	end
end

function UIMember:MoveFinish()
	return function()
		self.moving = false
	end
end

function UIMember:PageReset()
	self:UpdateMaxPage()
	self:SetPage(0)
	self:ClickPos(1)()
	self.uiRoleGrid.localPosition = self.oriX
end

function UIMember:UpdateMaxPage()
	self.maxPage = math.floor((table.getn(self.roleIDList)-1)/4)

	self.pageTab ={}
	CommonFunction.ClearGridChild(self.uiTipRoundGrid.transform)
	for i = 1,  self.maxPage + 1 do
		table.insert(self.pageTab, createUI("RolePageDot", self.uiTipRoundGrid.transform))
	end
	print("self.maxPage=",self.maxPage)
	self.uiTipRoundGrid:Reposition()
end

function UIMember:SetPage(page)
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

function UIMember:ShowPage(page)
	local startIndex = math.max(page * self.ITEMS_PER_PAGE + 1, 1)
	local endIndex = math.min((page + 1) * self.ITEMS_PER_PAGE, table.getn(self.roleIDList))
	for i = startIndex, endIndex do
		local id = self.roleIDList[i]
		local item = self.roleLuaTable[id]
		if not item then
			self:CreateRoleItem(id)
		end
	end
	self.uiGrid:Reposition()
end

return UIMember

--encoding=utf-8

UIQualifySelect = {
	uiName = 'UIQualifySelect',

	---------------------
	onStart = nil,
	--selectedRoles = nil,

	-------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UIQualifySelect:Awake()
	local tm = self.transform:FindChild("Member")
	self.btnStart = getChildGameObject(tm, "Start")
	self.tmCaptain = tm:FindChild("Left/ModelShowItem")
	self.tmMember1 = tm:FindChild("Left/Member1")
	self.tmMember2 = tm:FindChild("Left/Member2")
	self.tmMember3 = tm:FindChild("Left/Member3")
	self.tmMember1card = getChildGameObject(self.tmMember1,"Card").transform
	self.tmMember2card = getChildGameObject(self.tmMember2,"Card").transform
	self.tmMember3card = getChildGameObject(self.tmMember3,"Card").transform
	self.iconAdd1 = getChildGameObject(tm, "Left/Member1/Add")
	self.iconAdd2 = getChildGameObject(tm, "Left/Member2/Add")
	self.background = self.transform:FindChild("BGBlue/BGground"):GetComponent("UISprite")
	self.background1 = self.transform:FindChild("BGBlue"):GetComponent("UISprite")

	self.uiAnimator = self.transform:GetComponent('Animator')

	addOnClick(self.tmMember1.gameObject, self:OnMemberClick())
	addOnClick(self.tmMember2.gameObject, self:OnMemberClick())
	addOnClick(self.tmMember3.gameObject, self:OnMemberClick())
	addOnClick(self.btnStart, self:MakeOnStart())
end

function UIQualifySelect:Start()
	
	--[[	
	local item1 = self.tmMember1card:FindChild("CaptainBustItem(Clone)").gameObject
	local item2 = self.tmMember2card:FindChild("CaptainBustItem(Clone)").gameObject
	local item3 = self.tmMember3card:FindChild("CaptainBustItem(Clone)").gameObject
	if item1 then
		NGUITools.Destroy(item1)
		NGUITools.Destroy(item2)
		NGUITools.Destroy(item3)
	end
	--]]
	self:SetBackground()
end

function UIQualifySelect:FixedUpdate()
	-- body
end

function UIQualifySelect:OnClose()
	TopPanelManager:HideTopPanel()
end

function UIQualifySelect:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIQualifySelect:Refresh()
	--print("hahahahahah:",self.selectedRoles)
	while self.tmMember1card.childCount > 0 do
		NGUITools.Destroy(self.tmMember1card:GetChild(0).gameObject)
		print("lyf------destory")
	end
	while self.tmMember2card.childCount > 0 do
		NGUITools.Destroy(self.tmMember2card:GetChild(0).gameObject)
	end
	while self.tmMember3card.childCount > 0 do
		NGUITools.Destroy(self.tmMember3card:GetChild(0).gameObject)
	end

	--print("0000000000000000000")
	for i,v in pairs(self.selectedRoles or {}) do
		print("--------------------------self.selectedRoles: ", i)
		local item = nil
		local go = nil
		if i == 1 then
			while self.tmMember1card.childCount > 0 do
				NGUITools.Destroy(self.tmMember1card:GetChild(0).gameObject)
			end
			go = createUI('RoleBustItem', self.tmMember1card)
			item = getLuaComponent(go)
		elseif i == 2 then
			while self.tmMember2card.childCount > 0 do
				NGUITools.Destroy(self.tmMember2card:GetChild(0).gameObject)
			end
			go = createUI('RoleBustItem', self.tmMember2card)
			item = getLuaComponent(go)
		elseif i == 3 then
			while self.tmMember3card.childCount > 0 do
				NGUITools.Destroy(self.tmMember3card:GetChild(0).gameObject)
			end
			go = createUI('RoleBustItem', self.tmMember3card)
			item = getLuaComponent(go)
		end
		item:set_data(v)
		item.id = v
		item:set_panel_depth(30)
		print("lyf--------refreshCreate")
		item.on_click = self:OnMemberClick2(i)
		go:AddComponent('UIManagedPanel') 
	end
	self.btnBack = getLuaComponent(getChildGameObject(self.transform, "ButtonBack"))
	self.btnBack.onClick = self:MakeOnBack()
end


-----------------------------------------------------------------
function UIQualifySelect:FillDefendRoleInfoList(list)
	local enum = list:GetEnumerator()
	local i = 1
	while enum:MoveNext() do
		enum.Current.id = self.selectedRoles[i]
		i = i + 1
		print("========:",i)
	end
end

function UIQualifySelect:GetDefendRoleInfoListTable()
	local list
	list = {unpack(self.selectedRoles)}
	printTable(list)
	print(table.getn(list))
	return list
end

function UIQualifySelect:FillFightRoleInfoList(list)
	list:Clear()
	local info = FightRole.New()
	info.role_id = self.selectedRoles[1]
	info.status = FightStatus.FS_MAIN
	list:Add(info)
	info = FightRole.New()
	info.role_id = self.selectedRoles[2]
	info.status = FightStatus.FS_ASSIST1
	list:Add(info)
	info = FightRole.New()
	info.role_id = self.selectedRoles[3]
	info.status = FightStatus.FS_ASSIST2
	list:Add(info)
end

function UIQualifySelect:GetFightRoleInfoListTable(gameMode)
	local list = {
		game_mode = gameMode:ToString(),
		fighters = {
			{role_id = self.selectedRoles[1], status = "FS_MAIN",},
			{role_id = self.selectedRoles[2], status = "FS_ASSIST1",},
			{role_id = self.selectedRoles[3], status = "FS_ASSIST2",},
		},
	}

	return list
end

function UIQualifySelect:MakeOnStart()
	return function ()
		if not self.selectedRoles[1] or not self.selectedRoles[2] or not self.selectedRoles[3] then
			CommonFunction.ShowPopupMsg(getCommonStr("CAREER_NOT_ENOUGH_PLAYER"),nil,nil,nil,nil,nil)
			return
		end
		if self.onStart then 
			self.onStart()

			--self.tmMember1.gameObject:SetActive(false)
			--self.tmMember2.gameObject:SetActive(false)
			--self.tmMember3.gameObject:SetActive(false)
		end
	end
end

function UIQualifySelect:MakeOnBack()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

--Ìí¼ÓÇòÔ±´¦Àí
function UIQualifySelect:OnMemberClick()
	return function (go)
		self.select = go
		if self.select == self.tmMember1.gameObject then
			self.memberid = 1
		elseif self.select == self.tmMember2.gameObject then
			self.memberid = 2
		elseif self.select == self.tmMember3.gameObject then
			self.memberid = 3
		end
		local list = {}
		if self.selectedRoles[1] == nil and self.selectedRoles[2] == nil and self.selectedRoles[3] ==nil then
			list = {}
		end
		if self.selectedRoles[1] ~= nil or self.selectedRoles[2] ~= nil or self.selectedRoles[3] ~= nil then
			list = self.selectedRoles
		end
		self.member = TopPanelManager:ShowPanel('UIMember', nil, {chooseplayer = true, memberid = self.memberid,selected_list = list})
		-- if self.selectedRoles[1] == nil and self.selectedRoles[2] == nil and self.selectedRoles[3] ==nil then
		-- 	self.member.selected_list = {}
		-- end
		-- if self.selectedRoles[1] ~= nil or self.selectedRoles[2] ~= nil or self.selectedRoles[3] ~= nil then
		-- 	self.member.selected_list = self.selectedRoles
		-- end
		self.member.on_choose = self:GetChoosePlayer()
	end
end

--»ñÈ¡Ñ¡ÔñÇòÔ±
function UIQualifySelect:GetChoosePlayer()
	return function (id)
	print("lyflyflyf..id:",id)
		if self.memberid == 1 then
			while self.tmMember1card.childCount > 0 do
				NGUITools.Destroy(self.tmMember1card:GetChild(0).gameObject)
			end
			local item1 = getLuaComponent(createUI('RoleBustItem', self.tmMember1card))
			item1:set_panel_depth(30)
			item1:set_data(id)
			item1.id = id
			print("lyf------create")
			item1.on_click = self:OnMemberClick2(1) 
			self.selectedRoles[1] = item1.id
		elseif self.memberid == 2 then
			while self.tmMember2card.childCount > 0 do
				NGUITools.Destroy(self.tmMember2card:GetChild(0).gameObject)
			end
			local item2 = getLuaComponent(createUI('RoleBustItem', self.tmMember2card))
			item2:set_panel_depth(30)
			item2:set_data(id)
			item2.id = id
			item2.on_click = self:OnMemberClick2(2) 
			self.selectedRoles[2] = item2.id
		elseif self.memberid == 3 then
			while self.tmMember3card.childCount > 0 do
				NGUITools.Destroy(self.tmMember3card:GetChild(0).gameObject)
			end
			local item3 = getLuaComponent(createUI('RoleBustItem', self.tmMember3card))
			item3:set_panel_depth(30)
			item3:set_data(id)
			item3.id = id
			item3.on_click = self:OnMemberClick2(3) 
			self.selectedRoles[3] = item3.id
		end
	end
end

function UIQualifySelect:OnMemberClick2( index )
	return function ()
		if index == 1 then
			self.memberid = 1
		elseif index == 2 then
			self.memberid = 2
		elseif index == 3 then
			self.memberid = 3
		end
		local  list = {}
		if self.selectedRoles[1] == nil and self.selectedRoles[2] == nil and self.selectedRoles[3] ==nil  then
			list = {}
		end
		if self.selectedRoles[1] ~= nil or self.selectedRoles[2] ~= nil or self.selectedRoles[3] ~= nil then
			list = self.selectedRoles
		end
		self.member = TopPanelManager:ShowPanel('UIMember', nil, {chooseplayer = true, memberid = self.memberid ,selected_list = list})
		-- if self.selectedRoles[1] == nil and self.selectedRoles[2] == nil and self.selectedRoles[3] ==nil  then
		-- 	self.member.selected_list = {}
		-- end
		-- if self.selectedRoles[1] ~= nil or self.selectedRoles[2] ~= nil or self.selectedRoles[3] ~= nil then
		-- 	self.member.selected_list = self.selectedRoles
		-- end
		self.member.on_choose = self:GetChoosePlayer()
	end
end

function UIQualifySelect:SetBackground( ... )
	local position_type = GameSystem.Instance.RoleBaseConfigData2:GetPosition(MainPlayer.Instance.CaptainID)
	local position = enumToInt(position_type)
	local bgw = {"com_bg_pure_purplelight" , "com_bg_pure_bluelight" , "com_bg_pure_brownlight" , "com_bg_pure_greenlight" , "com_bg_pure_goldenlight"}
	local bottombtnbg = {"com_bg_pure_purpledeep" , "com_bg_pure_bluedeep" , "com_bg_pure_browndeep" , "com_bg_pure_greendeep" , "com_bg_pure_goldendeep"}
	self.background.spriteName = bgw[position]
	self.background1.spriteName = bgw[position]
	--self.go.bgbottom.spriteName = bottombtnbg[position]
	--self.btnBack:set_back_icon(bottombtnbg[position],1.0)
	--print("6666666...",bottombtnbg[position])
end

return UIQualifySelect

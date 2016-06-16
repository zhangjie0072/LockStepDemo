UISelectSquad = {
	uiName = 'UISelectSquad',
	
	onStart = nil,
	section_id,
	tour_id,
	--accessible
	is_tour = false,
	gameModeID,
	selectedRoles = {},
	npc_table = {},
	is_bull_fight = false,

	roleNum = 1,
	enemylist = nil,
}

function UISelectSquad:FillFightRoleInfoList(list)
	list:Clear()
	local info = FightRole.New()
	info.role_id = self.selectedRoles[1]
	info.status = FightStatus.FS_MAIN
	list:Add(info)

	if self.selectedRoles[2] and self.selectedRoles[2] ~= 0 then
		info = FightRole.New()
		info.role_id = self.selectedRoles[2]
		info.status = FightStatus.FS_ASSIST1
		list:Add(info)
	end
	if self.selectedRoles[3] and self.selectedRoles[3] ~= 0 then
		info = FightRole.New()
		info.role_id = self.selectedRoles[3]
		info.status = FightStatus.FS_ASSIST2
		list:Add(info)
	end
end

function UISelectSquad:GetFightRoleInfoListTable(gameMode)
	local fightList = {
		game_mode = gameMode:ToString(),
		fighters = {
			{role_id = self.selectedRoles[1],	status = "FS_MAIN",}
		}
	}
	if self.selectedRoles[2] and self.selectedRoles[2] ~= 0 then
		table.insert(fightList.fighters, {role_id = self.selectedRoles[2], status = "FS_ASSIST1",})
	end
	if self.selectedRoles[3] and self.selectedRoles[3] ~= 0 then
		table.insert(fightList.fighters, {role_id = self.selectedRoles[3], status = "FS_ASSIST2",})
	end
	return fightList
end

function UISelectSquad:Awake()
	local tm = self.transform:FindChild("Member")
	self.btnStart = getChildGameObject(tm, "Start")
	self.tmCaptain = tm:FindChild("Left/ModelShowItem")

	self.tmMember1 = tm:FindChild("Left/Member0")
	self.tmMember1card = getChildGameObject(self.tmMember1,"Card").transform
	addOnClick(self.tmMember1.gameObject, self:OnMemberClick())

	self.tmMember2 = tm:FindChild("Left/Member1")
	self.tmMember2card = getChildGameObject(self.tmMember2,"Card").transform
	addOnClick(self.tmMember2.gameObject, self:OnMemberClick())

	self.tmMember3 = tm:FindChild("Left/Member2")
	self.tmMember3card = getChildGameObject(self.tmMember3,"Card").transform
	addOnClick(self.tmMember3.gameObject, self:OnMemberClick())

	self.scroll = getComponentInChild(tm, "Right/Scroll", "UIScrollView")
	self.grid = getComponentInChild(tm, "Right/Scroll/Grid", "UIGrid")
	self.background = self.transform:FindChild("BGBlue/BGground"):GetComponent("UISprite")
	self.background1 = self.transform:FindChild("BGBlue"):GetComponent("UISprite")
	addOnClick(self.btnStart, self:MakeOnStart())
end

function UISelectSquad:Start()
	self.btnBack = getLuaComponent(getChildGameObject(self.transform, "ButtonBack"))
	self.btnBack.onClick = self:MakeOnBack()

	-- if self.is_tour == false then
	-- 	local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.section_id)
	-- 	if sectionConfig == nil then
	-- 		return
	-- 	end

	-- 	local gameModeConfig = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)

	-- 	--print("--------------------------gameModeConfig.matchType: ", gameModeConfig.matchType)
	-- 	local matchType = gameModeConfig.matchType
	-- 	if matchType == GameMatch.Type.e3On3
	-- 		or matchType == GameMatch.Type.eCareer3On3
	-- 		or matchType == GameMatch.Type.eAsynPVP3On3 then
	-- 		self.roleNum = 3
	-- 	end
	-- else
	-- 	local tourConfig = GameSystem.Instance.TourConfig:GetTourData(self.tour_id, MainPlayer.Instance.Level)
	-- 	if tourConfig == nil then
	-- 		return
	-- 	end

	-- 	local gameModeConfig = GameSystem.Instance.GameModeConfig:GetGameMode(tourConfig.gameModeID)
	-- 	--print("--------------------------gameModeConfig.matchType: ", gameModeConfig.matchType)
	-- 	local matchType = gameModeConfig.matchType
	-- 	if matchType == GameMatch.Type.e3On3
	-- 		or matchType == GameMatch.Type.eCareer3On3
	-- 		or matchType == GameMatch.Type.eAsynPVP3On3 then
	-- 		self.roleNum = 3
	-- 	end
	-- end

	--self:SetBackground()
end

function UISelectSquad:Refresh()
	NGUITools.SetActive(self.btnStart, true)
	print("table------:")
	printTable(self.selectedRoles)
	self.roleNum = 1

	if self.is_bull_fight then
		self.roleNum = 1
	else

		if self.is_tour == false then
			local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.section_id)
			if sectionConfig == nil then
				return
			end

			local gameModeConfig = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
			print("-----------------------------------------section_id:",self.section_id)
			print("--------------------------gameModeConfig.matchType: ", gameModeConfig.matchType)
			local matchType = gameModeConfig.matchType
			if matchType == GameMatch.Type.e3On3
				or matchType == GameMatch.Type.eCareer3On3
			or matchType == GameMatch.Type.eAsynPVP3On3 then
				self.roleNum = 3
			end
		else
			local tourConfig = GameSystem.Instance.TourConfig:GetTourData(self.tour_id, MainPlayer.Instance.Level)
			if tourConfig == nil then
				return
			end

			local gameModeConfig = GameSystem.Instance.GameModeConfig:GetGameMode(tourConfig.gameModeID)
			--print("--------------------------gameModeConfig.matchType: ", gameModeConfig.matchType)
			local matchType = gameModeConfig.matchType
			if matchType == GameMatch.Type.e3On3
				or matchType == GameMatch.Type.eCareer3On3
			or matchType == GameMatch.Type.eAsynPVP3On3 then
				self.roleNum = 3
			end
		end

	end
	

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

	print("--------------------------self.roleNum: ", self.roleNum)
	self.tmMember1.gameObject:SetActive(true)
	if self.roleNum == 1 then
		self.tmMember2.gameObject:SetActive(false)
		self.tmMember3.gameObject:SetActive(false)
	else
		self.tmMember2.gameObject:SetActive(true)
		self.tmMember3.gameObject:SetActive(true)
	end
	print("selectedRoles:",table.getn(self.selectedRoles))
	for i,v in pairs(self.selectedRoles or {}) do
		print("--------------------------self.selectedRoles: ", i)
		local item = nil
		if i == 1 then
			while self.tmMember1card.childCount > 0 do
				NGUITools.Destroy(self.tmMember1card:GetChild(0).gameObject)
			end
			item = getLuaComponent(createUI('RoleBustItem', self.tmMember1card))
		elseif i == 2 then
			while self.tmMember2card.childCount > 0 do
				NGUITools.Destroy(self.tmMember2card:GetChild(0).gameObject)
			end
			item = getLuaComponent(createUI('RoleBustItem', self.tmMember2card))
		elseif i == 3 then
			while self.tmMember3card.childCount > 0 do
				NGUITools.Destroy(self.tmMember3card:GetChild(0).gameObject)
			end
			item = getLuaComponent(createUI('RoleBustItem', self.tmMember3card))
		end
		item:set_data(v)
		item.id = v
		item:set_panel_depth(30)
		print("lyf--------refreshCreate")
		item.on_click = self:OnMemberClick2(i) 
	end
	-- for i = 1 , 3 do
	-- 	if self.selectedRoles[i] then
	-- 		local item = nil
	-- 		if i == 1 then
	-- 			while self.tmMember1card.childCount > 0 do
	-- 				NGUITools.Destroy(self.tmMember1card:GetChild(0).gameObject)
	-- 			end
	-- 			item = getLuaComponent(createUI('RoleBustItem', self.tmMember1card))
	-- 		elseif i == 2 then
	-- 			while self.tmMember2card.childCount > 0 do
	-- 				NGUITools.Destroy(self.tmMember2card:GetChild(0).gameObject)
	-- 			end
	-- 			item = getLuaComponent(createUI('RoleBustItem', self.tmMember2card))
	-- 		elseif i == 3 then
	-- 			while self.tmMember3card.childCount > 0 do
	-- 				NGUITools.Destroy(self.tmMember3card:GetChild(0).gameObject)
	-- 			end
	-- 			item = getLuaComponent(createUI('RoleBustItem', self.tmMember3card))
	-- 		end
	-- 		item:set_data(self.selectedRoles[i])
	-- 		item.id = self.selectedRoles[i]
	-- 		item:set_panel_depth(30)
	-- 		print("lyf--------refreshCreate")
	-- 		item.on_click = self:OnMemberClick2(i)
	-- 	end 
	-- end

	CommonFunction.ClearGridChild(self.grid.transform)

	if self.is_bull_fight then
		local item = getLuaComponent(createUI("CareerIconItem", self.grid.transform))
		item.npc = true
		local id = self.enemy_id
		item:update_by_id(id)
	else
		if self.is_tour == false then
			local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.section_id)
			if sectionConfig == nil then
				return
			end

			local gameModeConfig = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
			local matchType = gameModeConfig.matchType

			local firstRoleID = self.selectedRoles[1]
			if firstRoleID == nil then
				firstRoleID = MainPlayer.Instance.CaptainID
			end
			local position = GameSystem.Instance.RoleBaseConfigData2:GetPosition(firstRoleID)

			local firstRoleID = gameModeConfig:GetMappedNPC(position)
			if firstRoleID and firstRoleID ~= 0 then
				local item = getLuaComponent(createUI("CareerIconItem", self.grid.transform))
				item.npc = true
				local id = firstRoleID
				item:update_by_id(id)
			else
				for i = 0, self.roleNum-1 do
					local enum = gameModeConfig.unmappedNPC[i]:GetEnumerator()
					while enum:MoveNext() do
						local item = getLuaComponent(createUI("CareerIconItem", self.grid.transform))
						item.npc = true
						local id = enum.Current
						item:update_by_id(id)
					end
				end
			end
		else
			for i = 1, self.roleNum do
				local item = getLuaComponent(createUI("CareerIconItem", self.grid.transform))
				item.npc = true
				local npc_id = self.npc_table[i]
				item:update_by_id(npc_id)
			end
		end
		
	end
	

	self.grid:Reposition()
	self.scroll:ResetPosition()
end

function UISelectSquad:MakeOnStart()
	return function ()
		if table.getn(self.selectedRoles) ~= self.roleNum then
			CommonFunction.ShowPopupMsg(getCommonStr("CAREER_NOT_ENOUGH_PLAYER"))
			return
		end
		if self.onStart then 

			-- self.tmMember1.gameObject:SetActive(false)
			-- self.tmMember2.gameObject:SetActive(false)
			-- self.tmMember3.gameObject:SetActive(false)

			NGUITools.SetActive(self.btnStart, false)
			self.onStart()
		end
	end
end

function UISelectSquad:MakeOnBack()
	return function ()
		TopPanelManager:HideTopPanel()
	end
end

--添加球员处理
function UISelectSquad:OnMemberClick()
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


function UISelectSquad:OnMemberClick2( index )
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

--获取选择球员
function UISelectSquad:GetChoosePlayer()
	return function (id)
	print("selectedRoles-id:",id)
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

function UISelectSquad:FixedUpdate()
	local base = UIManager.Instance.m_uiRootBasePanel
	local PopupMessage = base.transform:FindChild("PopupMessage(Clone)")
	if PopupMessage then 
		PopupMessage:GetComponent("UIPanel").depth = 50
	end
end

--[[
--随着队长设置背景颜色变化
function UISelectSquad:SetBackground( ... )
	-- 获取队长职位
	local position_type = GameSystem.Instance.RoleBaseConfigData2:GetPosition(MainPlayer.Instance.CaptainID)
	local position = enumToInt(position_type)
	local bgw = {
		"com_bg_pure_purplelight" ,
		"com_bg_pure_bluelight" ,
		"com_bg_pure_brownlight" ,
		"com_bg_pure_greenlight" ,
		"com_bg_pure_goldenlight"
	}
	local bottombtnbg = {
		"com_bg_pure_purpledeep" ,
		"com_bg_pure_bluedeep" ,
		"com_bg_pure_browndeep" ,
		"com_bg_pure_greendeep" ,
		"com_bg_pure_goldendeep"
	}
	self.background.spriteName = bgw[position]
	self.background1.spriteName = bgw[position]
	self.btnBack:set_back_icon(bottombtnbg[position],1.0)
end
--]]

return UISelectSquad

RoleOpen =  {
	uiName	 = "RoleOpen", 

}

function RoleOpen:Awake()
	self.go = {} 		-- create this for contained gameObject
	self:ui_parse()

	local g, t
	-- g = createUI("PopupFrame5",self.go.popup_frame5)
	g  = WidgetPlaceholder.Replace(self.go.popup_frame5.gameObject)
	self.popup_frame = getLuaComponent(g)
	self.popup_frame.onClose = self:click_close()
	self.popup_frame.title = getCommonStr("STR_MEMBER1")
	self.popup_frame:SetTitleColor(Color.New(0.317,0.317,0.317,1.0))

	
	self.role_bust_item = getLuaComponent(createUI("RoleBustItem",self.go.left_role_bust_item))
	self.role_bust_item:set_panel_depth(20)
	self:add_click()
end


function RoleOpen:add_click()
	addOnClick(self.go.right_sw_grid_train.gameObject,self:click_train_tab())
	addOnClick(self.go.right_sw_grid_skill.gameObject,self:click_skill_tab())
	addOnClick(self.go.right_sw_grid_attribute.gameObject,self:click_attribute_tab())	
	addOnClick(self.go.right_button_upgrade.gameObject,self:click_upgrade())
	addOnClick(self.go.right_button_enhance.gameObject,self:click_enhance())
	addOnClick(self.go.right_set_master.gameObject,self:click_set_master())
	addOnClick(self.go.right_level_exp_add.gameObject,self:click_exp_add())
end

function RoleOpen:click_exp_add()
	return function()
		if not MainPlayer.Instance:HasRole(self.id) then
			CommonFunction.ShowTip(getCommonStr("NOT_OWNED_ROLE"))
			return
		end
		
		
		local g = createUI("RoleExp",self.transform)
		self.ui_role_exp = getLuaComponent(g)
		self.ui_role_exp:set_data(self.id)
		self.ui_role_exp.on_click_close = self:click_role_exp_close()
		UIManager.Instance:BringPanelForward(g)
	end
end

function RoleOpen:click_role_exp_close()
	return function()
		print(" RoleOpen:click_role_exp_close")
		if self.ui_role_exp then
			NGUITools.Destroy(self.ui_role_exp.gameObject)
			self.ui_role_exp = nil
			self:Refresh()
		end
	end
end

function RoleOpen:click_set_master()
	return function()
		print("click_set_master called()")
		self:send_set_master(self.id)
	end
end

function RoleOpen:send_set_master(id)
	print("RoleOpen set_master id=",id)
	local operation = {
		id = id
	}
	
	local req = protobuf.encode("fogs.proto.msg.SwitchCaptain",operation)
	CommonFunction.ShowWait()
	LuaHelper.SendPlatMsgFromLua(MsgID.SwitchCaptainID,req)
end

function RoleOpen:s_handle_switch_captain()
	return function (buf)
		print("Receive s_handel_switch_captain")
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.SwitchCaptainResp", buf)
		if resp then
			print("s_handle_switch resp.result=",resp.result)
			if resp.result == 0 then
				MainPlayer.Instance:SwitchCaptain(resp.id)
				CommonFunction.ShowTip(getCommonStr("STR_SHOW_ROLE_IN_HALL"))
				-- self.current_item:refresh()
				-- self:refresh_all_item()
				-- self:update_action_button()
				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_switch_captain(): " .. err)
		end
	end
end






function RoleOpen:click_enhance()
	return function()
		if( not self:check_enhance(self.id)) then
			CommonFunction.ShowTip(getCommonStr("STR_MAX_STAR"))
			return
		end
		
		g = createUI("RoleUpgradePopup",self.transform)
		UIManager.Instance:BringPanelForward(g)			
		self.upgrade_pp = getLuaComponent(g)
		print("self.upgrade_pp:SetRoleData(self.id) = "..tostring(self.id))
		self.upgrade_pp:SetRoleStarData(self.id)
		self.upgrade_pp.on_click_close = self:click_upgrade_pp_close()
		UIManager.Instance:BringPanelForward(g)
	end
end

function RoleOpen:check_enhance(role_id)
	local cur_star = MainPlayer.Instance:GetRole2(role_id).star

	local star_data = GameSystem.Instance.starAttrConfig:GetStarAttr(role_id,cur_star + 1)
	return star_data ~= nil 

end

function RoleOpen:click_upgrade()
	return function()
		local quality = MainPlayer.Instance:GetRole2(self.id).quality
		if quality == 15 then
			CommonFunction.ShowTip(getCommonStr("ROLE_ENHANCE_MAX"))
			return
		end
		print("RoleOpen:click_upgrade() called")
		g = createUI("RoleUpgradePopup",self.transform)
		UIManager.Instance:BringPanelForward(g)			
		self.upgrade_pp = getLuaComponent(g)
		print("self.upgrade_pp:SetRoleData(self.id) = "..tostring(self.id))
		self.upgrade_pp:SetRoleData(self.id)
		self.upgrade_pp.on_click_close = self:click_upgrade_pp_close()
		print("self.upgrade_pp:SetRoleData(self.id) = after"..tostring(self.id))
		UIManager.Instance:BringPanelForward(g)
	end
end

-- parse the prefeb
function RoleOpen:ui_parse()

	-- right role
	self.go.right_level_level_label = self.transform:FindChild("right/level/levelLabel"):GetComponent("UILabel")


	self.go.right_level_exp_add = self.transform:FindChild("right/level/expAdd"):GetComponent("UIButton")

	self.go.popup_frame5 = self.transform:FindChild("popupFrame5"):GetComponent("Transform")

	-- tab
	self.go.right_sw_grid_train = self.transform:FindChild("right/sw/grid/train"):GetComponent("UISprite")	
	self.go.right_sw_grid_skill = self.transform:FindChild("right/sw/grid/skill"):GetComponent("UISprite")
	self.go.right_sw_grid_attribute = self.transform:FindChild("right/sw/grid/attribute"):GetComponent("UISprite")

	-- tab content
	self.go.right_train = self.transform:FindChild("right/train"):GetComponent("Transform")
	self.go.right_skill = self.transform:FindChild("right/skill"):GetComponent("Transform")
	self.go.right_attribute = self.transform:FindChild("right/attribute"):GetComponent("Transform")

	-- train grid
	self.go.right_train_goods = self.transform:FindChild("right/train/goods"):GetComponent("UIGrid")
	self.go.left_role_bust_item = self.transform:FindChild("left/roleBustItem"):GetComponent("Transform")


	-- skill
	self.go.right_skill_scroll = self.transform:FindChild("right/skill/scroll"):GetComponent("UIScrollView")
	self.go.right_skill_scroll_grid = self.transform:FindChild("right/skill/scroll/grid"):GetComponent("UIGrid")

	-- attribute
	self.go.right_attribute_attack_scroll = self.transform:FindChild("right/attribute/attackScroll"):GetComponent("UIScrollView")
	self.go.right_attribute_attack_scroll_grid = self.transform:FindChild("right/attribute/attackScroll/grid"):GetComponent("UIGrid")

	self.go.right_attribute_fight_scroll = self.transform:FindChild("right/attribute/fightScroll"):GetComponent("UIScrollView")
	self.go.right_attribute_fight_scroll_grid = self.transform:FindChild("right/attribute/fightScroll/grid"):GetComponent("UIGrid")


	-- action button
	self.go.right_button_upgrade = self.transform:FindChild("right/buttonUpgrade"):GetComponent("Transform")
	self.go.right_button_enhance = self.transform:FindChild("right/buttonEnhance"):GetComponent("UIButton")
	self.go.right_button_recruit = self.transform:FindChild("right/buttonRecruit"):GetComponent("UIButton")
	self.go.right_set_master = self.transform:FindChild("right/setMaster"):GetComponent("UIButton")	
	

	-- level
	self.go.right_level_progress = self.transform:FindChild("right/level/progress"):GetComponent("UIProgressBar")
	self.go.right_level_level_label = self.transform:FindChild("right/level/levelLabel"):GetComponent("UILabel")
	self.go.right_level_progress_num_exp = self.transform:FindChild("right/level/progress/numExp"):GetComponent("UILabel")
	
	
end


function RoleOpen:click_close()
	return function()
		if self.on_click_close then
			self.on_click_close()
		end

	end
end

function RoleOpen:click_train_tab()
	return function()
		self:to_tab(1)
	end
end


function RoleOpen:click_skill_tab()
	return function()
		self:to_tab(2)
	end
end

function RoleOpen:click_attribute_tab()
	return function()
		self:to_tab(3)
		self:refresh_attribute()	
	end
end


function RoleOpen:to_tab(tab)
	NGUITools.SetActive(self.go.right_train.gameObject, tab == 1)
	NGUITools.SetActive(self.go.right_skill.gameObject, tab == 2)
	NGUITools.SetActive(self.go.right_attribute.gameObject, tab == 3)
	self.tab = tab
end

function RoleOpen:click_train_item()
	return function(train)

		local exercise_level = MainPlayer.Instance:GetExerciseLevel(self.id,train.id)
		local e_quality = math.modf(exercise_level / 6) + 1
		local role_config = MainPlayer.Instance:GetRole2(self.id)

		
		local r_quality = 1
		if role_config then
			r_quality = MainPlayer.Instance:GetRole2(self.id).quality
		end

		print("e_quality=",e_quality)
		print("r_quality=",r_quality)
		print("e_level=",exercise_level)
		if e_quality > r_quality then
			CommonFunction.ShowTip(getCommonStr("EXERCISE_MAX_BY_ROLE"))
			return
		end
		-- max quality is 89 now. go back!!
		local max_quality = 89		
		if exercise_level >= 89 then
			CommonFunction.ShowTip(getCommonStr("EXERCISE_REACH_MAX"))
			return 
		end

		
		self.cur_train = train
		print("RoleOpen:click_train_item tran.id="..train.id)
		local g,t
		g = createUI("RoleUpgradePopup",self.transform)
		UIManager.Instance:BringPanelForward(g)			
		self.upgrade_pp = getLuaComponent(g)
		self.upgrade_pp:SetTrainData(self.id,train.id,train.level)
		self.upgrade_pp.on_click_close = self:click_upgrade_pp_close()
		UIManager.Instance:BringPanelForward(g)
	end
end

function RoleOpen:click_upgrade_pp_close()
	return function()
		if self.upgrade_pp ~= nil then
			self.upgrade_pp:destroy()
			self.upgrade_pp = nil
		end
		self:Refresh()
	end
end

function RoleOpen:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.SwitchCaptainRespID, self.uiName)	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function RoleOpen:set_data(id)
	print("ro id =",id)
	self.id = id

	self.role_bust_item:set_data(id)
	
	local captain_id = MainPlayer.Instance.CaptainID

	-- train
	self.train_list = {}
	-- for i = 1, 6 do
	-- 	table.insert(self.train_list, getLuaComponent(createUI("GoodsIcon2",self.go.right_train_goods.transform)))
	-- end
	print("set data id = self.id")
	local base_data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local trains = base_data.training_slots
	local enum = trains:GetEnumerator()
	while enum:MoveNext() do
		local train_id = enum.Current
		local train_item =  getLuaComponent(createUI("GoodsIcon2",self.go.right_train_goods.transform))
		local exercise_level = MainPlayer.Instance:GetExerciseLevel(self.id, train_id)
		train_item:SetData(self.id,train_id)
		train_item.onClick = self:click_train_item()
		table.insert(self.train_list,train_item)
	end
	
	self.go.right_train_goods:Reposition()

	local g,t
	-- skill
	local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local skills = data.training_skill_all
	--local skills = GameSystem.Instance.qualityAttrConfig:GetSkills(self.id)
	enum = skills:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current
		g = createUI("SkillCare",self.go.right_skill_scroll_grid.transform)
		t = getLuaComponent(g)
		t:SetData(self.id, id)
	end
	-- attribute
	self:refresh_attribute()
end


function RoleOpen:refresh_attribute()
	local owned = self:owned()
	local g,t

	CommonFunction.ClearGridChild(self.go.right_attribute_attack_scroll_grid.transform)
	CommonFunction.ClearGridChild(self.go.right_attribute_fight_scroll_grid.transform)
	
	if not owned then
		-- display the base data.
		local attrs = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id).attrs
		local enum = attrs:GetEnumerator()
		while enum:MoveNext() do
			local id = enum.Current.Key
			local value = enum.Current.Value

			local attr_name_config = GameSystem.Instance.AttrNameConfigData
			local symbol = attr_name_config:GetAttrSymbol(id)
			local attr_name_data = attr_name_config:GetAttrData(symbol)
			local display = attr_name_data.display
			local type = attr_name_data.type
			local name = attr_name_data.name
			if display==1 then
				if type == AttributeType.BASIC then
					-- base data
					g = createUI("PD_PropertyItem",self.go.right_attribute_attack_scroll_grid.transform)
					t = getLuaComponent(g)
					t:set_value( value/250,name,value,false)
				elseif type == AttributeType.HEDGING then
					-- fight data
					g = createUI("AttrListItem",self.go.right_attribute_fight_scroll_grid.transform)
					t=getLuaComponent(g)
					t.attrName = name
					t.value = value
				end
			end
		end
	else
		-- display the player info.
		local attr_data = MainPlayer.Instance:GetRoleAttrsByID(self.id).attrs
		local enum = attr_data:GetEnumerator()
		while enum:MoveNext() do
			local symbol = enum.Current.Key
			local value = enum.Current.Value
			
			local attr_name_config = GameSystem.Instance.AttrNameConfigData
			-- local symbol = attr_name_config:GetAttrSymbol(id)
			local attr_name_data = attr_name_config:GetAttrData(symbol)
			local display = attr_name_data.display
			local type = attr_name_data.type
			local name = attr_name_data.name
			if display==1 then
				if type == AttributeType.BASIC then
					-- base data
					g = createUI("PD_PropertyItem",self.go.right_attribute_attack_scroll_grid.transform)
					t = getLuaComponent(g)
					t:set_value( value/250,name,value,false)
				elseif type == AttributeType.HEDGING then
					-- fight data
					g = createUI("AttrListItem",self.go.right_attribute_fight_scroll_grid.transform)
					t=getLuaComponent(g)
					t.attrName = name
					t.value = value
				end
			end
			
		end
		
		
		
	end
	self.go.right_attribute_attack_scroll_grid:Reposition()	
	self.go.right_attribute_fight_scroll_grid:Reposition()
	
end



function RoleOpen:Start()
	LuaHelper.RegisterPlatMsgHandler(MsgID.SwitchCaptainRespID, self:s_handle_switch_captain(), self.uiName)
	self:Refresh()
end

function RoleOpen:owned()
	return MainPlayer.Instance:HasRole(self.id)
end


function RoleOpen:Refresh()
	local id = self.id
	
	self:to_tab(1)
	for k,v in pairs(self.train_list) do
		v:Refresh()
	end
	local owned = self:owned()
	NGUITools.SetActive(self.go.right_button_upgrade.gameObject,owned)
	NGUITools.SetActive(self.go.right_button_enhance.gameObject,owned)
	NGUITools.SetActive(self.go.right_button_recruit.gameObject, false)

	local captain_id = MainPlayer.Instance.CaptainID

	-- get level, exp datas info from role.
	local level = 1
	local exp = 1
	local owned = self:owned()
	if owned then
		local role_info = MainPlayer.Instance:GetRole2(id)
		level = role_info.level
		exp = role_info.exp
	else
	end


	
	NGUITools.SetActive(self.go.right_set_master.gameObject,owned and id ~= captain_id )
	self.level = level
	self.exp = exp
	
	local cost_exp = 0
	for i = 1, level - 1 do
		cost_exp = cost_exp + GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
	end
	exp = exp - cost_exp
	
	
	local max_exp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(level)
	self.go.right_level_level_label.text = "Lv."..tostring(level)
	self.go.right_level_progress.value = exp/max_exp
	self.go.right_level_progress_num_exp.text = "Exp. "..tostring(exp).."/"..tostring(max_exp)


	-- train
	self.go.right_train_goods:Reposition()
	--attribute
	self.go.right_attribute_attack_scroll_grid:Reposition()
	self.go.right_attribute_fight_scroll_grid:Reposition()
	-- skill
	self.go.right_skill_scroll_grid:Reposition()
	self.role_bust_item:Refresh()

end

-- uncommoent if needed
-- function RoleOpen:FixedUpdate()

-- end


-- uncommoent if needed
-- function RoleOpen:Update()
	

-- end




return RoleOpen

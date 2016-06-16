-- 20150617_144817




PlayerDisplay =  {
	uiName = 'PlayerDisplay',
	
	id =0,
	go={},
	model_show_item=nil,
	mode=0,
	is_show_mode = true,
	is_captain=false,
	is_show_lock = true,
	delay_id = false,

	cd = {},
	md = {},
	attr_list= {},
}




function PlayerDisplay:Awake()
	self.go ={}
	self.cd ={}
	self.md = {}

	self.go.model = self.transform:FindChild("Model").gameObject
	self.model_show_item = self.transform:FindChild('Model/ModelShowItem'):GetComponent('ModelShowItem')

	self.go.info			   = self.transform:FindChild("Info").gameObject
	
	self.go.switch_btn		 = self.transform:FindChild("Switch").gameObject
	self.go.character_btn	  = self.transform:FindChild("Info/CharacterBtn").gameObject
	
	self.go.basic_property_btn = self.transform:FindChild("Info/BasicPropertyBtn").gameObject
	
	self.go.character		  = self.transform:FindChild("Info/Character").gameObject
	self.go.basic_property	 = self.transform:FindChild("Info/BasicProperty").gameObject
	
	self.go.bp_data_grid	   = self.transform:FindChild('Info/BasicProperty/DataGrid'):GetComponent('UIGrid')
	self.go.bp_attack_grid	 = self.transform:FindChild('Info/BasicProperty/AttackGrid'):GetComponent('UIGrid')
	
	self.go.bp_defence_grid	= self.transform:FindChild('Info/BasicProperty/DefenceGrid'):GetComponent('UIGrid')
	
	

	-- self.go.role_data_graphics_node = self.transform:FindChild('Info/Character/RoleDataGraphicsNode')

	-- local g = createUI('RoleDataGraphics',self.go.role_data_graphics_node)
	-- self.role_data_graphics = getLuaComponent(g)

	addOnClick(self.go.character_btn,self:click_character())
	addOnClick(self.go.basic_property_btn,self:click_basic_property())
	addOnClick(self.go.switch_btn,self:click_switch())

	
	NGUITools.SetActive(self.go.model,true)
	NGUITools.SetActive(self.go.info,false)

	self.go.player_lock = self.transform:FindChild("Model/ModelShowItem/PlayerLock").gameObject
	local g = createUI("PlayerLock",self.go.player_lock.transform)
	self.player_lock = getLuaComponent(g)

	self.go.bias = self.transform:FindChild("Info/BasicProperty/Bias")
end



function PlayerDisplay:Start()
	local t_node = self.transform:FindChild("Info/Character/MemberDispCharNode")
	if not self.is_captain then
		local t_go = createUI('MemberDispChar',t_node)
		self.member_disp_char = getLuaComponent(t_go)
	else
		local t_go = createUI('CaptainDispChar',t_node)
		self.captain_disp_char = getLuaComponent(t_go)
		self.captain_disp_char.player_display = self
	end
	
	local t = createUI("Bias",self.transform:FindChild("Info/BasicProperty/Bias"))
	self.bias = getLuaComponent(t)
	self.bias.on_click_attack = self:click_bias()
	self.bias.on_click_balance = self:click_bias()
	self.bias.on_click_defence = self:click_bias()
	self.bias.player_display = self
end


function PlayerDisplay:click_bias()
	return function()
		self.cd.bias = self.bias.bias
		self:refresh_bias()
	end
end


function PlayerDisplay:set_member_data(id,quality)
	print("PlayerDisplay:set_member_data id=",id)
	print("PlayerDisplay:set_member_data quality=",quality)
	self.id = id
	self.md.quality = quality
	
	self:refresh()
	return
end


function PlayerDisplay:set_captain_data(id,bias)
	self.id = id
	self.is_captain = true
	self.cd.bias = bias
	self:refresh_default()
end

function PlayerDisplay:update_id(id)
	self.id = id
	print('PlayerDisplay :update_id='..tostring(self.id))
	if self.is_show_mode then
		self.model_show_item.ModelID = self.id
		self.delay_id = false
	else
		self.delay_id = true
	end
end


function PlayerDisplay:refresh_ui()
	self:refresh_data()
	self:update_mode(self.mode)
end

function PlayerDisplay:refresh_default()
	self.mode = 0
	self:refresh()
	if not self.is_show_mode then
		self:action_switch()
	end

	if self.mode~= 0 then
		self:update_mode(0)
	end
	if self.bias then
		self.bias:refresh()
	end
end

function PlayerDisplay:refresh()
	print("PlayerDisplay:refresh() is called()")
	local id = self.id
	self:update_id(id)
	self:refresh_data()

	-- if not self.is_show_mode then
	-- 	self:update_mode(self.mode)
	-- end
	
	local owned = self:owned()
	if not owned and self.is_show_lock then
		local cost = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id).buy_consume
		NGUITools.SetActive(self.go.player_lock,true)
		self.player_lock:set_data(1,cost)
	else
		NGUITools.SetActive(self.go.player_lock,false)
	end

	if self.is_captain then 
		self:refresh_captain()
	else
		self:refresh_member()
	end
end

function PlayerDisplay:refresh_member()
	self:action_switch()
	NGUITools.SetActive(self.go.switch_btn,false)
	
end

function PlayerDisplay:has_specail_skill()
	local role_base_data = self:get_role_base_data()
	return role_base_data.special_attrs.Count ~= 0
end


function PlayerDisplay:get_position()
	local role_base_data = self:get_role_base_data()
	return role_base_data.position
end


function PlayerDisplay:get_role_base_data()
	local base_data_config = GameSystem.Instance.RoleBaseConfigData2
	return base_data_config:GetConfigData(self.id)
end


function PlayerDisplay:refresh_captain()
	if self.started then
		if self.mode == 1 then
			self.captain_disp_char:set_data(self.id,self.cd)
			self.captain_disp_char:refresh_data()
		end
	end
	local owned = self:owned()

	self:visible_bias_btn(not owned)

end


--  click action 
function PlayerDisplay:click_character()
	return function()
		if self.is_captain then
			local has_specail_skill = self:has_specail_skill()
			if not has_specail_skill then
				CommonFunction.ShowTip(getCommonStr("STR_CAPTAIN_HAS_NO_CHARACTOR"))
				return
			end
		end
		self:update_mode(1)
	end
end


function PlayerDisplay:click_basic_property()
	return function()
		self:update_mode(0)
	end
end

function PlayerDisplay:click_switch()
	return function()
		self:action_switch()
	end
end


function PlayerDisplay:action_switch()
	local show_mode = not self.is_show_mode
	self.is_show_mode = show_mode
	NGUITools.SetActive(self.go.model,show_mode)
	NGUITools.SetActive(self.go.info,not show_mode)
	
	if show_mode then
		if self.delay_id then
			self.model_show_item.ModelID = self.id
			self.delay_id = false
		end
	else
		self:update_mode(self.mode)
	end

	if self.switch_dlg then
		self.switch_dlg()
	end
end


function PlayerDisplay:update_btn_by_mode()
	local mode = self.mode

	local character_btn = self.go.character_btn.transform:GetComponent('UISprite')
	local basic_property_btn = self.go.basic_property_btn.transform:GetComponent('UISprite')
	local character_label = character_btn.transform:FindChild("Name"):GetComponent("UILabel")
	local basic_property_label = basic_property_btn.transform:FindChild("Name"):GetComponent("UILabel")

	if mode == 0 then
		basic_property_btn.spriteName = "com_button_cutpage_property2" -- white
		character_btn.spriteName = "com_button_cutpage_property" -- black
		basic_property_label.color = Color.New(0,0,0)
		character_label.color = Color.New(1,1,1)
		basic_property_label.effectColor=Color.black
		character_label.effectColor=Color.white
	elseif mode == 1 then
		basic_property_btn.spriteName = "com_button_cutpage_property"
		character_btn.spriteName = "com_button_cutpage_property2"
		basic_property_label.color = Color.New(1,1,1)
		character_label.color = Color.New(0,0,0)
		character_label.effectColor=Color.black
		basic_property_label.effectColor=Color.white
	end
end

function PlayerDisplay:update_mode(mode)
	self.mode = mode
	NGUITools.SetActive(self.go.basic_property,mode==0)
	NGUITools.SetActive(self.go.character,mode==1)
	self:update_btn_by_mode()
	if mode == 0 then
		self:update_bp_grid()
	elseif mode == 1 then
		if not self.is_captain then
			self.member_disp_char:set_data(self.id,self.md)
		else
			self.captain_disp_char:set_data(self.id,self.cd)
		end
		self:refresh_bias()
	end
	
end

function PlayerDisplay:refresh_bias()
	self:refresh_data()
	self:update_bp_grid()
end


function PlayerDisplay:refresh_data()
	if self.is_captain then
		self:refresh_captain_data()
	else 
		self:refresh_member_data() 
	end
end

-- depends on the memebr/captain.
function PlayerDisplay:owned()
	if self.is_captain then
		return MainPlayer.Instance:HasCaptain(self.id)
	else 
		return self:check_member_owned()
	end
end


function PlayerDisplay:check_member_owned()
	return MainPlayer.Instance:HasRole(self.id)
end

function PlayerDisplay:refresh_member_data()
	local id = self.id
	local owned = self:owned()
	self.attr_list = {}
	if owned then
		-- local enum = MainPlayer.Instance:GetMemberAttr(id):GetEnumerator()
		-- while enum:MoveNext() do
		-- 	local k = enum.Current.Key
		-- 	local v = enum.Current.Value
			
		-- 	local attr = {}
		-- 	attr.id = k
		-- 	attr.value = v

		-- 	local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(attr.id)
		-- 	attr.type = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).type
		-- 	attr.name = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).name
		-- 	attr.is_hide= GameSystem.Instance.AttrNameConfigData:isHide(attr.id) 

		-- 	table.insert(self.attr_list,attr)
		-- end


		-- TOOD: do displaya first, data not right.
		local quality = 1	
		local attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(id,quality)
		local enum = attr_data.attrs:GetEnumerator()
		while enum:MoveNext() do
			local symbol = enum.Current.Key
			local value = enum.Current.Value
			
			local attr = {}
			attr.id = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).id
			attr.value = value
			attr.type = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).type
			attr.name = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).name
			attr.is_hide= GameSystem.Instance.AttrNameConfigData:isHide(attr.id) 

			table.insert(self.attr_list,attr)
		end
		
	else
		-- config
		local quality = 1	
		local attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(id,quality)
		local enum = attr_data.attrs:GetEnumerator()
		while enum:MoveNext() do
			local symbol = enum.Current.Key
			local value = enum.Current.Value
			
			local attr = {}
			attr.id = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).id
			attr.value = value
			attr.type = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).type
			attr.name = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).name
			attr.is_hide= GameSystem.Instance.AttrNameConfigData:isHide(attr.id) 

			table.insert(self.attr_list,attr)
		end
	end
end

function PlayerDisplay:refresh_captain_data()
	local id = self.id
	self.attr_list ={}
	local owned = self:owned()
	if owned then 
		local enum = MainPlayer.Instance:GetCaptainAttr(id):GetEnumerator()
		while enum:MoveNext() do
			local k = enum.Current.Key
			local v = enum.Current.Value
			
			local attr = {}
			attr.id = k
			attr.value = v

			local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(attr.id)
			attr.type = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).type
			attr.name = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).name
			attr.is_hide= GameSystem.Instance.AttrNameConfigData:isHide(attr.id) 

			table.insert(self.attr_list,attr)
		end
	else
		-- config
		local level = 1		
		local attr_data = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(id,self.cd.bias,level)
		local enum = attr_data.attrs:GetEnumerator()
		while enum:MoveNext() do
			local symbol = enum.Current.Key
			local value = enum.Current.Value
			
			local attr = {}
			attr.id = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).id
			attr.value = value
			attr.type = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).type
			attr.name = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).name
			attr.is_hide= GameSystem.Instance.AttrNameConfigData:isHide(attr.id) 

			table.insert(self.attr_list,attr)
		end
	end
end


function PlayerDisplay:update_bp_grid()
	for k,v in pairs(self.attr_list) do
		local name =v.name
	end

	self:update_part_pb_grid(self.go.bp_attack_grid,AttributeType.BASIC)
	self:update_part_pb_grid(self.go.bp_defence_grid,AttributeType.HEDGING)
	self:update_part_pb_grid(self.go.bp_data_grid,AttributeType.OTHER)
end

function PlayerDisplay:update_part_pb_grid(grid,config_type)
	if not NGUITools.GetActive(grid.gameObject) then 
		print("error failed to update_part_bp_grid for not active!!!!!!")
		return
	end
	CommonFunction.ClearGridChild(grid.transform)   
	for k,v in pairs(self.attr_list) do
		local type = v.type
		local is_hide = v.is_hide
		local value = v.value
		local name = v.name

		if not is_hide and type == config_type then
			local id = v.id
			local position = self:get_position()
			local recommend = GameSystem.Instance.AttrNameConfigData:IsRecommend(id,position)
			if type == AttributeType.BASIC then
				local t = createUI('PD_PropertyItem',grid.transform)
				local script = getLuaComponent(t)
				script:set_value(value/500,name,value,recommend)
			elseif type == AttributeType.HEDGING then
				local g = createUI("AttrListItem",grid.transform)
				local t=getLuaComponent(g)
				t.attrName = name
				t.value = value
				
			end
		end
	end
	
	grid:Reposition()
end




function PlayerDisplay:visible_bias_btn(visiable)
	NGUITools.SetActive(self.go.bias.gameObject,visiable)
end

function PlayerDisplay:visible(visible)
	NGUITools.SetActive(self.gameObject,visible)
end


return PlayerDisplay



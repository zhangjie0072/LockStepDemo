------------------------------------------------------------------------
-- class name    : CaptainBustItem
-- create time   : 20150714_134153
------------------------------------------------------------------------



CaptainBustItem =  {
	uiName = "CaptainBustItem", 
	selected = false,
	on_click,
	is_captain = true,
	cselect = false,
	model_show_item = nil,
}


function CaptainBustItem:Awake()
	self.go = {} 		-- create this for contained gameObject
	
	self.go.card = self.transform:FindChild("Card")
	self.go.name = self.transform:FindChild("Card/Name"):GetComponent("UILabel")
	self.go.position = self.transform:FindChild("Card/Position"):GetComponent("UISprite")
	self.go.bg = self.transform:FindChild("Card/Bg"):GetComponent("UISprite")
	self.go.select = self.transform:FindChild("Card/Select"):GetComponent("UISprite")
	self.go.current = self.transform:FindChild("Card/Current").gameObject
	self.go.role = self.transform:FindChild("Card/Role"):GetComponent("UISprite")

	self.go.action_btn = self.transform:FindChild('ActionBtn').gameObject
	self.go.action_label = self.transform:FindChild('ActionBtn/Label'):GetComponent("UILabel")
	self.go.bias = self.transform:FindChild("Card/Bias"):GetComponent("UISprite")
	
	self.go.model = self.transform:FindChild("Model").gameObject
	-- self.model_show_item = self.transform:FindChild("Model/ModelShowItem"):GetComponent("ModelShowItem")
	self.go.cselect = self.transform:FindChild("Card/CSelect")

	
	addOnClick(self.go.bg.gameObject,self:click())
	addOnClick(self.go.action_btn,self:click_action())
	addOnClick(self.go.cselect.gameObject,self:click_cselect())
end



function CaptainBustItem:click_action()
	return function()
		if self.on_click_action then
			self.on_click_action()
		end
	end
end

function CaptainBustItem:update_action_button()
	-- if  not self.player_display.is_show_mode then
	-- 	NGUITools.SetActive(self.go.action_btn,false)
	-- 	return
	-- end

	if not self.is_captain then
		NGUITools.SetActive(self.go.action_btn,false)   
		return
	end

	local visiual_action_btn = false
	local item = self
	local owned = item:owned()
	if owned then 
		if item.id ~= MainPlayer.Instance.CaptainID then
			self.go.action_label.text = getCommonStr('STR_ENTER_ON_THE_STAGE')
			visiual_action_btn = true
		else
			visiual_action_btn = false
		end
	else
		self.go.action_label.text = getCommonStr('STR_RECRUIT')
		visiual_action_btn = true
	end
	NGUITools.SetActive(self.go.action_btn,visiual_action_btn)
end


function CaptainBustItem:click()
	return function()
		if self.on_click then
			self.on_click(self)
		end
	end
end

function CaptainBustItem:click_cselect()
	return function()
		self.cselect = self.go.cselect:GetComponent("UIToggle").value
		if self.on_click_cselect then
			self.on_click_cselect(self)
		end
	end
end

function CaptainBustItem:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function CaptainBustItem:click_model()
	return function()
		self.on_click(self,NGUITools.GetActive(self.go.model.gameObject))
		-- self:set_selected(false)
	end
end


function CaptainBustItem:set_cselect_state(state)
	local prev_state = self.state
	self.state = state
	if state == 0 then
		NGUITools.SetActive(self.go.cselect.gameObject,false)
		local ct = self.go.bg.transform:GetComponent("BoxCollider")
		ct.enabled = true

		ct = self.go.cselect:GetComponent("UIToggle")
		ct.value = false
		self.cselect = ct.value
		-- self.go.bg:GetComponent("UIPlayTween").enabled = false
	elseif state == 1 then
		NGUITools.SetActive(self.go.cselect.gameObject,true)
		local img = self.go.cselect:GetComponent("UISprite")
		img.spriteName = "com_button_choice_yes"
		local ct = self.go.cselect:GetComponent("BoxCollider")
		ct.enabled = true
		ct = self.go.bg.transform:GetComponent("BoxCollider")
		-- ct.enabled = false
		if prev_state == 0 then
			self.state = 0 
			self:set_selected(false)
			self.go.bg:GetComponent("UIPlayTween").enabled = false
		end

		self.state = 1
	elseif state == 2 then
		NGUITools.SetActive(self.go.cselect.gameObject,true)
		local img = self.go.cselect:GetComponent("UISprite")
		img.spriteName = "com_button_choice_no"
		local ct = self.go.cselect:GetComponent("BoxCollider")
		ct.enabled = false
		ct = self.go.bg.transform:GetComponent("BoxCollider")
		self.go.bg:GetComponent("UIPlayTween").enabled = false
		-- ct.enabled = false
	end
end

function CaptainBustItem:enabled_bc(enabled)
	self.go.bg:GetComponent("BoxCollider").enabled = enabled
end

function CaptainBustItem:Start()
end

function CaptainBustItem:set_current(is_current)
	NGUITools.SetActive(self.go.current,is_current)
end


function CaptainBustItem:owned()
	local owned = false 
	if self.is_captain then
		owned = MainPlayer.Instance:HasCaptain(self.id)
	else
		owned = self:check_member_owned()
	end
	return owned
end

function CaptainBustItem:check_member_owned()
	return MainPlayer.Instance:HasRole(self.id)
end



function CaptainBustItem:set_selected(selected)
	self.selected = selected
	local is_color = false
	if self.is_captain then
		NGUITools.SetActive(self.go.select.gameObject,selected)
		NGUITools.SetActive(self.go.action_btn.gameObject,selected)
		is_color = true
	else
		if self.state ~= 1 and self.state ~= 2 then
			NGUITools.SetActive(self.go.card.gameObject, not selected)

			if selected then
				if self.model_show_item == nil then
					local t = createUI("ModelShowItem",self.transform:FindChild("Model"))
					t.transform.localPosition = Vector3.New(0, -25, 0)
					local shadow = t.transform:FindChild('Shadow').transform
					shadow.localPosition = Vector3.zero
					self.model_show_item = t:GetComponent("ModelShowItem")
					addOnClick(t.gameObject,self:click_model())
					-- self.model_show_item._layerName = "Player"

				end

				if self.model_show_item.ModelID ~= self.id then
					self.model_show_item.ModelID = self.id
				end
			else
				is_color = true
			end
			NGUITools.SetActive(self.go.model.gameObject, selected)
		end
	end
	
	if is_color then
		local owned = self:owned()
		if  not owned and not selected then
			self.go.role.color = Color.New(0,0,0)
		else
			self.go.role.color = Color.New(255,255,255)
		end
	end
end


function CaptainBustItem:set_data(id,is_captain)
	self.is_captain = is_captain
	self.id = id
	self:refresh()


end

function CaptainBustItem:refresh()
	local id = self.id
	local owned = self:owned()
	
	self.state = 0
	self.go.role.atlas = getBustAtlas(id) 
	self.go.role.spriteName = 'icon_bust_'..tostring(self.id)

	local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local init_state = config.init_state

	self.go.name.text = config.name
	local positions ={'PF','SF','C','PG','SG'}
	local position_c = {"purple","blue","brown","green","golden"}
	self.go.position.spriteName = 'PT_'..positions[config.position]
	local bg_str = "com_bg_pure_round_captain_"..position_c[config.position] 
	
	if init_state == 0 and self.is_captain then
		bg_str = bg_str..("_y")
	end

	self.go.bg.transform:GetComponent("UIButton").normalSprite = bg_str

	if self.is_captain then
		self:refresh_captain()
	else
		self:refresh_member()
	end
end

function CaptainBustItem:refresh_captain()
	local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local init_state = config.init_state

	local owned = self:owned()
	NGUITools.SetActive(self.go.bias.gameObject,owned)

	self.bias = 1
	local level = 1
	if owned then
		local role_info = MainPlayer.Instance:GetCaptainInfo(self.id)
		self.bias = role_info.bias
		level = MainPlayer.Instance.Level
		local bias_small_str = {"attack","defense","balance"}
		self.go.bias.spriteName = "com_icon_"..bias_small_str[self.bias].."_small"
	end

	if init_state==0 then
		self.go.bg.spriteName = "com_bg_pure_round_frame_yellow"
	end
	-- todo
	quality =1
	self.attr_data = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(self.id,self.bias,level)

	self:set_current(self.id == MainPlayer.Instance.CaptainID)
	NGUITools.SetActive(self.go.model.gameObject, false)
	NGUITools.SetActive(self.go.cselect.gameObject, false)

end


function CaptainBustItem:refresh_member()
	self:set_current(false)
	self.quality = 1
	local owned = self:owned()
	if owned then
		local player = MainPlayer.Instance:GetRole(self.id)
		self.quality = player:GetQuality()
	end
	self.attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.id,self.quality)
	NGUITools.SetActive(self.go.action_btn.gameObject,false)
	local bias = self.attr_data.bias
	local bias_small_str = {"attack","defense","balance"}
	self.go.bias.spriteName = "com_icon_"..bias_small_str[bias].."_small"


	-- self.model_show_item.ModelID = self.id
	self:set_cselect_state(0)
	
end

function CaptainBustItem:make_color_light(light)
	if light then
		self.go.role.color = Color.New(255,255,255)
	else
		self.go.role.color = Color.New(0,0,0)
	end
end

-- uncommoent if needed
-- function CaptainBustItem:FixedUpdate()

-- end


-- uncommoent if needed
-- function CaptainBustItem:Update()
	

-- end




return CaptainBustItem

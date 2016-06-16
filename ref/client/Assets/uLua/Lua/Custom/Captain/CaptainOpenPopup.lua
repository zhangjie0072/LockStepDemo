-- 20150623_152156






CaptainOpenPopup =  {
	uiName = 'CaptainOpenPopup',
	
	go ={},
	cost_diamond=0,
}




function CaptainOpenPopup:Awake()
	self.go = {}
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	local popup_frame_position = goFrame.transform.localPosition
	popup_frame_position.z = 0
	goFrame.transform.localPosition = popup_frame_position
	self.uiFrame = getLuaComponent(goFrame)
	self.uiFrame.onClose = self:click_close()
	local t = createUI("Bias",self.transform:FindChild("Bias"))
	self.bias_script = getLuaComponent(t)
	self.bias_script.on_click_attack = self:click_attack()
	self.bias_script.on_click_defence = self:click_defence()
	self.bias_script.on_click_balance = self:click_balance()
	-- self.go.attack_btn = self.transform:FindChild('Bias/Attack'):GetComponent('UIButton')
	-- self.go.attack_btn.pixelSnap = true
	-- addOnClick(self.go.attack_btn.gameObject,self:click_attack())

	-- self.go.defence_btn = self.transform:FindChild('Bias/Defence'):GetComponent('UIButton')
	-- self.go.defence_btn.pixelSnap = true
	-- addOnClick(self.go.defence_btn.gameObject,self:click_defence())

	-- self.go.balance_btn = self.transform:FindChild('Bias/Balance'):GetComponent('UIButton')
	-- self.go.balance_btn.pixelSnap = true
	-- addOnClick(self.go.balance_btn.gameObject,self:click_balance())

	self.go.captain_item_node = self.transform:FindChild('Info/CaptainItemNode')

	self.go.close_btn = self.transform:FindChild('Close').gameObject

	addOnClick(self.go.close_btn,self:click_close())

	self.go.cost_label = self.transform:FindChild('Info/Price/Num'):GetComponent('UILabel')

	self.go.open_btn = self.transform:FindChild('OpenBtn').gameObject
	

	addOnClick(self.go.open_btn,self:click_open())
	local g = createUI("RoleDataGraphics",self.transform:FindChild("RoleData"))
	self.role_data = getLuaComponent(g)
end


function CaptainOpenPopup:Start()

	local pos = self.transform.position
	pos.z = -500*0.002234637
	self.transform.position = Vector3.New(pos.x,pos.y,pos.z)
	local g = createUI('CaptainItem',self.go.captain_item_node)
	local id = self.captain_item.id

	local script = getLuaComponent(g)
	script:set_role_base_config(self.captain_item.role_base_config)

	script:update_by_id(id)

	local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
	-- self.go.cost_label.text = self.captain_item.role_base_config.buy_consume
	self.go.cost_label.text = config.buy_consume
	-- self.cost_diamond = self.captain_item.role_base_config.buy_consume
	self.cost_diamond = config.buy_consume
	self:bias_default()
	
end


function CaptainOpenPopup:bias_unselect()
	self.go.attack_btn.normalSprite = 'createCaptain_preference2'
	self.go.defence_btn.normalSprite = 'createCaptain_preference2'
	self.go.balance_btn.normalSprite = 'createCaptain_preference2'
end



function CaptainOpenPopup:bias_default()
	local acfun = self:click_attack()
	acfun()
end

function CaptainOpenPopup:click_attack()
	return function()
		-- self:bias_unselect()
		-- self.go.attack_btn.normalSprite = 'captain_choose'
		self.bias = 1
		self.role_data:refresh_captain_data(self.captain_item.id,self.bias)
	end
end


function CaptainOpenPopup:click_defence()
	return function()
		-- self:bias_unselect()
		-- self.go.defence_btn.normalSprite = 'captain_choose'
		self.bias = 2
		self.role_data:refresh_captain_data(self.captain_item.id,self.bias)
	end
end

function CaptainOpenPopup:click_balance()
	return function()
		-- self:bias_unselect()
		-- self.go.balance_btn.normalSprite = 'captain_choose'

		self.bias = 3
		self.role_data:refresh_captain_data(self.captain_item.id,self.bias)
	end
end

function CaptainOpenPopup:click_open()
	return function()
		local m_player = MainPlayer.Instance
		local owned_diamond = m_player.DiamondBuy + m_player.DiamondFree
		
		if owned_diamond < self.cost_diamond then
			CommonFunction.ShowTip(getCommonStr('NOT_ENOUGH_DIAMOND'))
			playSound("UI/UI-wrong")
			return
		end
		print('captian open id='..tostring(self.captain_item.id)..' bias='..tostring(self.bias))

		local operation = {
			id = self.captain_item.id,
			bias = self.bias
		}

		local req = protobuf.encode("fogs.proto.msg.BuyCaptain",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.BuyCaptainID,req)
		CommonFunction.ShowWait()
	end
end


function CaptainOpenPopup:close()
	NGUITools.Destroy(self.gameObject)
end

function CaptainOpenPopup:click_close()
	return function()
		self:close()
	end
end



return CaptainOpenPopup

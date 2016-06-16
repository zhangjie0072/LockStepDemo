------------------------------------------------------------------------
-- class name	: Bias
-- create time   : 20150714_134153
------------------------------------------------------------------------


Bias =  {
	uiName	 = "Bias", 

}




function Bias:Awake()
	self.go = {} 		-- create this for contained gameObject
	
	self.go.attack = self.transform:FindChild("Attack")
	self.go.defence = self.transform:FindChild("Defence")
	self.go.balance = self.transform:FindChild("Balance")

	self.go.attack_btn = self.transform:FindChild("Attack"):GetComponent("UIButton")
	self.go.defence_btn = self.transform:FindChild("Defence"):GetComponent("UIButton")
	self.go.balance_btn = self.transform:FindChild("Balance"):GetComponent("UIButton")
	
	addOnClick(self.go.attack.gameObject,self:click_attack())
	addOnClick(self.go.defence.gameObject,self:click_defence())
	addOnClick(self.go.balance.gameObject,self:click_balance())
end


function Bias:click_attack()
	return function()
		self:action_bias(1)
		if self.on_click_attack then
			self.on_click_attack()
		end
	end
end


function Bias:click_defence()
	return function()
		self:action_bias(2)	
		if self.on_click_defence then
			self.on_click_defence()
		end
	end
end

function Bias:click_balance()
	return function()
		self:action_bias(3)	
		if self.on_click_balance then
			self.on_click_balance()
		end
	end
end

function Bias:refresh()
	self:reset_bias_btns()
	self:action_bias(3)
end


function Bias:action_bias(bias)
	self:reset_bias_btns()
	self.bias = bias
	if bias == 1 then
	   self.go.attack_btn.normalSprite = 'com_icon_attack'
	elseif bias == 2 then
	   self.go.defence_btn.normalSprite = 'com_icon_defense'
	elseif bias == 3 then
	   self.go.balance_btn.normalSprite = 'com_icon_balance'
	end
	
	 self.player_display.cd.bias = bias
	 self.player_display:refresh_bias()
end

function Bias:reset_bias_btns()
	self.go.attack_btn.normalSprite = 'com_icon_attack_gray'
	self.go.defence_btn.normalSprite = 'com_icon_defense_gray'
	self.go.balance_btn.normalSprite = 'com_icon_balance_gray'
end


function Bias:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function Bias:Start()
	self:refresh()

end

-- uncommoent if needed
-- function Bias:FixedUpdate()

-- end


-- uncommoent if needed
-- function Bias:Update()
	

-- end




return Bias

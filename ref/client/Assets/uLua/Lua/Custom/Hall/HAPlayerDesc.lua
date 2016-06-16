

HAPlayerDesc={
	uiName = 'HAPlayerDesc',
	
	go = {}
}

function HAPlayerDesc:Awake()
	self.go= {}
	self.go.level = self.transform:FindChild('Level'):GetComponent('UILabel')
	self.go.name = self.transform:FindChild('Level/Name'):GetComponent('UILabel')   
	self.go.vip =self.transform:FindChild('Vip/Viplabel'):GetComponent('UILabel')
	self.go.experience = self.transform:FindChild('Level/Experence'):GetComponent('UILabel')
	
end

function HAPlayerDesc:get_vip()
	local level = 0
	local config= GameSystem.Instance.VipPrivilegeConfig
	for i=1,15 do
		local vip_data = config:GetVipData(i)
		if vip_data.consume <= MainPlayer.Instance.VipExp then
			level = i
		else
			break
		end
	end
	return level
end

function HAPlayerDesc:refresh()
	local level = MainPlayer.Instance.Level
	self.go.level.text =getCommonStr("UI_HALL_LEVEL_1")..tostring(level)
	self.go.name.text = MainPlayer.Instance.Name

	
	self.go.vip.text = tostring(self:get_vip())
	
	local offset_exp = MainPlayer.Instance.Exp
	local next_exp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level)
	for i=1,level - 1 do
	offset_exp = offset_exp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	end
	
	self.go.experience.text = getCommonStr("LABEL_HALL_EXPERIENCE")..tostring(offset_exp).."/"..tostring(next_exp)

end

function HAPlayerDesc:Start()
	-- add the button response
	local replace = self.transform:FindChild("Replace").gameObject
	local captain = self.transform:FindChild('Captain').gameObject
	local fashion = self.transform:FindChild('Fashion').gameObject
	
	UIEventListener.Get(replace).onClick = LuaHelper.VoidDelegate(self:OnReplaceClick())
	UIEventListener.Get(captain).onClick = LuaHelper.VoidDelegate(self.OnCaptianClick())
	addOnClick(fashion,self:click_fashion())

	self:refresh()

end

function HAPlayerDesc:click_fashion()
	return function()
	TopPanelManager:ShowPanel("UIFashion")
	end
end


function HAPlayerDesc:OnReplaceClick()
	return function()
	TopPanelManager:ShowPanel("UICaptain")
	end
end

function HAPlayerDesc.OnCaptianClick(go)
	return function()
	TopPanelManager:ShowPanel("UISkill")
	end
end

return HAPlayerDesc


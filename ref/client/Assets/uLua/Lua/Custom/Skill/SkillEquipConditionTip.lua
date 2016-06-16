SkillEquipConditionTip = {
	uiName = 'SkillEquipConditionTip',
	
	attrID = "",
	attrValue = 0,
}

function SkillEquipConditionTip:Awake()
	self.uiFrame = getLuaComponent(WidgetPlaceholder.Replace(self.transform:FindChild("Frame")))
	self.uiGrid = getComponentInChild(self.transform, "Icons/Grid", "UIGrid")
	self.uiTattoo = self.transform:FindChild("Icons/Grid/Tattoo").gameObject
	self.uiTraining = self.transform:FindChild("Icons/Grid/Training").gameObject
	self.uiMessage = getComponentInChild(self.transform, "Message", "UILabel")
	self.OnClose = function ()
		NGUITools.Destroy(self.gameObject)
	end
	addOnClick(self.uiTattoo, self:MakeOnTattoo())
	addOnClick(self.uiTraining, self:MakeOnTraining())
end

function SkillEquipConditionTip:Start()
	self.uiFrame.showCorner = false
	self.uiFrame.title = getCommonStr("SKILL_EQUIP_CONDITION")
	self.uiFrame.onClose = self.OnClose

	local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(self.attrID)
	local name = GameSystem.Instance.AttrNameConfigData:GetAttrName(symbol)
	self.uiMessage.text = self.uiMessage.text:format(name, self.attrValue)
end

function SkillEquipConditionTip:MakeOnTattoo()
	return function ()
		TopPanelManager:ShowPanel("UITattoo")
		self.OnClose()
	end
end

function SkillEquipConditionTip:MakeOnTraining()
	return function ()
		TopPanelManager:ShowPanel("UITraining")
		self.OnClose()
	end
end

return SkillEquipConditionTip

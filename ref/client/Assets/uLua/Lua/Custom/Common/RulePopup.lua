RulePopup = {
	uiName = "RulePopup",

	rule = "",
	title = getCommonStr("RULE_DETAIL")
}

function RulePopup:Awake()
	self.tmRule = self.transform:FindChild("Rule")
end

function RulePopup:Start()
	self.uiFrame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.uiFrame.title = self.title
	self.uiFrame.onClose = function () NGUITools.Destroy(self.gameObject) end

	self.uiRulePane = getLuaComponent(createUI("RulePane", self.tmRule))
	self.uiRulePane.rule = self.rule
end

return RulePopup

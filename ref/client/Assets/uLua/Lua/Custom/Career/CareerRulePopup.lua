CareerRulePopup = {
	uiName = 'CareerRulePopup',

	matchType = GameMatch.Type.eNone
}

function CareerRulePopup:Awake()
	self.uiName = getComponentInChild(self.transform, "Top/Name", "UILabel")
	self.uiRule = self.transform:FindChild("Rule")
	self.uiRules = {}
	self.uiRules[1] = self.uiRule:FindChild("1")
end

function CareerRulePopup:Start()
	self.uiFrame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.uiFrame.title = getCommonStr("RULE_DETAIL")
	self.uiFrame.onClose = function () NGUITools.Destroy(self.gameObject) end

	self.uiIcon = getLuaComponent(getChildGameObject(self.transform, "Top/Icon"))
	self.uiIcon.matchType = self.matchType
	self.uiName.text = getCommonStr("MATCH_TYPE_NAME_" .. tostring(self.matchType))

	local matchRule = getCommonStr("MATCH_RULE_" .. tostring(self.matchType))
	local rules = Split(matchRule, '\n')
	--[[
	local regex = "([^\n]*)"
	local rules = {}
	for r in matchRule:gmatch(regex) do
		table.insert(rules, r)
	end
	--]]

	local last
	local ruleNum = #rules
	for i = 1, ruleNum do
		if i > 1 then
			self.uiRules[i] = CommonFunction.InstantiateObject(self.uiRules[1].gameObject, self.uiRule).transform
			self.uiRules[i].name = tostring(i)
		end
		getComponentInChild(self.uiRules[i], "Round/Num", "UILabel").text = tostring(i)
		local uiRule = self.uiRules[i]:GetComponent("UILabel")
		if last then
			uiRule.topAnchor.target = last.transform
			uiRule.topAnchor:Set(0, -12)
			uiRule:ResetAnchors()
		end
		uiRule.text = rules[i]
		last = uiRule
	end
end

return CareerRulePopup

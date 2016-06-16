require "common/StringUtil"

RulePane = {
	uiName = "RulePane",

	rule = "",
	title,
}

function RulePane:Awake()
	self.uiRules = {}
	self.uiRules[1] = self.transform:FindChild("1")

	self.title = self.transform:FindChild('Title'):GetComponent('UILabel')
	self.title.text = ""

	self.pane = self.transform:GetComponent("UIWidget")
end

function RulePane:Start()
	print(self.uiName, "Rule:", self.rule)
	local rules = Split(self.rule, '\n')
	--[[
	local regex = "%d+%.([^\n]*)"
	local rules = {}
	for r in self.rule:gmatch(regex) do
		table.insert(rules, r)
	end
	--]]

	local last
	local ruleNum = #rules
	for i = 1, ruleNum do
		if i > 1 then
			self.uiRules[i] = CommonFunction.InstantiateObject(self.uiRules[1].gameObject, self.transform).transform
			self.uiRules[i].name = tostring(i)
		end
		if self.gameObject.name == 'RulePane1(Clone)' then
			getComponentInChild(self.uiRules[i], "Round/Num", "UILabel").text = tostring(i)
		end
		getComponentInChild(self.uiRules[i], "Label", "UILabel").text = rules[i]
		local uiRule = self.uiRules[i]:GetComponent("UIWidget")
		if last then
			uiRule.topAnchor.target = last.transform
			uiRule.topAnchor:Set(0, -12)
			uiRule:ResetAnchors()
			self.pane.bottomAnchor.target = uiRule.transform
			self.pane.bottomAnchor:Set(0, -12)
		end
		last = uiRule
	end

	self.pane:ResetAnchors()
	--local wid = self.transform:GetComponent("UIWidget")
	--wid.height = (self.uiRules[1]:GetComponent("UIWidget").height+12) * ruleNum
end

function RulePane:SetTitle( title )
	self.title.text = title
end

return RulePane

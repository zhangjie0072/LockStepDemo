MatchRulePopup = {
	uiName = "MatchRulePopup",
	-----------------UI
	uiButtonGrid,
	tmRule,
	-----------------parameters
	matchType = GameMatch.Type.eNone,
	leagueType = GameMatch.LeagueType.eNone,
	onClose,
}

function MatchRulePopup:Awake()
	--self.uiName = getComponentInChild(self.transform, "Top/Name", "UILabel")
	self.uiButtonGrid = self.transform:FindChild("Window/ButtonClose")
	self.tmRule = self.transform:FindChild("Window/Rule")
end

function MatchRulePopup:Start()
	local rule = getLuaComponent(createUI("ButtonClose" , self.uiButtonGrid))
	rule.onClick = self:MakeOnClose()

	local stringID = ""
	if self.matchType ~= GameMatch.Type.eNone then
		stringID = "MATCH_RULE_" .. tostring(self.matchType)
	elseif self.leagueType ~= GameMatch.LeagueType.eNone then
		stringID = "LEAGUE_RULE_" .. tostring(self.leagueType)
	end
	local matchRule = getCommonStr(stringID)
	if matchRule == stringID then
		for i = 1, 10000 do
			local stringIDEx = stringID .. i
			local str = getCommonStr(stringIDEx)
			if str == stringIDEx then break end
			if i == 1 then
				matchRule = str
			else
				matchRule = matchRule .. "\n" .. str
			end
		end
	end

	self.uiRulePane = getLuaComponent(createUI("RulePane", self.tmRule))
	self.uiRulePane.rule = matchRule
end

function MatchRulePopup:MakeOnClose()
	return function ()
		if self.onClose then
			self.onClose()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

return MatchRulePopup

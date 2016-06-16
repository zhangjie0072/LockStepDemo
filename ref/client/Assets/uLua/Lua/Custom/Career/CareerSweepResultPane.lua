SweepResultPane = {
	uiName = 'SweepResultPane',
	------------------UI
	uiTimesLabel,
	uiAwardsGrid,
	------------------parameters
	times = 1,
	awards = nil,
	icon_adjust = false,
}

function SweepResultPane:Awake()
	self.uiTimesLabel = getComponentInChild(self.transform, "TextLabel", "MultiLabel")
	self.uiAwardsGrid = self.transform:FindChild("Award")
end

function SweepResultPane:Start()
	self.uiTimesLabel:SetText( getCommonStr("CAREER_SWEEP_CUR_TIMES"):format(self.times))
	local awardPane = getLuaComponent(createUI("AwardPane", self.uiAwardsGrid))
	awardPane.awards = self.awards
	if self.icon_adjust == true then 
		awardPane.icon_adjust = true
	end
end

return SweepResultPane

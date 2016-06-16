SweepResultPane = {
	uiName = 'SweepResultPane',
	
	times = 1,
	awards = nil,
	icon_adjust = false,
}

function SweepResultPane:Awake()
	self.lblTimes = getComponentInChild(self.transform, "Times", "UILabel")
	self.tmAward = self.transform:FindChild("Award")
end

function SweepResultPane:Start()
	self.lblTimes.text = getCommonStr("CAREER_SWEEP_CUR_TIMES"):format(self.times)
	self.awardPane = getLuaComponent(createUI("AwardPane", self.tmAward))
	self.awardPane.awards = self.awards
	if self.icon_adjust == true then 
		self.awardPane.icon_adjust = true
	end
end

return SweepResultPane

CareerSweepResultPopup = {
	uiName = 'CareerSweepResultPopup',
	
	-----------------parameters
	chapterID = 0,
	sectionID = 0,
	times = 0,
}

function CareerSweepResultPopup:Awake()
	self.scroll = getComponentInChild(self.transform, "Scroll", "UIScrollView")
	self.grid = getComponentInChild(self.transform, "Scroll/Grid", "UIGrid")
end

function CareerSweepResultPopup:Start()
	self.frame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.frame.showCorner = false
	self.frame.title = getCommonStr("CAREER_SWEEP_COMPLETE")
	self.frame.onClose = self:MakeOnClose()

	self.sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.sectionID)
	self.section = MainPlayer.Instance:GetSection(self.chapterID, self.sectionID)

	for i = 1, self.times do
		local curTime = self.section.challenge_times - self.times + i
		local awards = CareerConfig.GetGoodsList(self.sectionConfig.award_id, curTime)
		local awardsTable = {}
		local enum = awards:GetEnumerator()
		while enum:MoveNext() do
			awardsTable[enum.Current.award_id] = enum.Current.award_value
		end
		local awardPane = getLuaComponent(createUI("SweepResultPane", self.grid.transform))
		awardPane.times = i
		awardPane.awards = awardsTable
	end

	self.grid:Reposition()
	self.scroll:ResetPosition()
end

function CareerSweepResultPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return CareerSweepResultPopup

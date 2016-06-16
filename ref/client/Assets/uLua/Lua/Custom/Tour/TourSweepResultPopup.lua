require "Custom/Career/CareerUtil"

TourSweepResultPopup = {
	uiName = "TourSweepResultPopup",

	awards = nil,
}

function TourSweepResultPopup:Start()
	self.frame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.frame.title = getCommonStr("TOUR_SWEEP")
	self.frame.onClose = function() NGUITools.Destroy(self.gameObject) end

	self.awardPane = getLuaComponent(createUI("AwardPane", self.transform:FindChild("Middle/Award")))
	self.awardPane.awards = self.awards
end

return TourSweepResultPopup

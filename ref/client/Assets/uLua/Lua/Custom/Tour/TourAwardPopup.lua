require "Custom/Career/CareerUtil"

TourAwardPopup = {
	uiName = "TourAwardPopup",

	tourID = 0,
}

function TourAwardPopup:Awake()
	self.label = getComponentInChild(self.transform, "Window/Label", "UILabel")
	self.grid = getComponentInChild(self.transform, "Window/Middle/Grid", "UIGrid")
end

function TourAwardPopup:Start()
	self.frame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.frame.showCorner = false
	self.frame.onClose = self:MakeOnClose()
	self.frame.title = getCommonStr("AWARD_DETAIL")

	self.label.text = getCommonStr("TOUR_AWARD_LABEL"):format(self.tourID)

	local tourData = GameSystem.Instance.TourConfig:GetTourData(self.tourID, MainPlayer.Instance.Level)
	CareerUtil.ListAwards(tourData.fixedAwards, self.grid.transform, true)
	self.grid:Reposition()
end

function TourAwardPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return TourAwardPopup

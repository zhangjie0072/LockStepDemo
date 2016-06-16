TourItem = {
	uiName = "TourItem",
	maxTourID = 5,
	--------------------
	tourID = 0,
	isCurrent = false,
	onClick = nil,
	------------------UI
	uiLabel,
	uiSprBall,
	uiArrow,
	------------------variable
	gameMode,
}

function TourItem:Awake()
	self.uiLabel = getComponentInChild(self.transform, "Label", "UILabel")
	--self.uiLabelBg = self.transform:FindChild("Label/BG"):GetComponent("UISprite")
	self.uiSprBall = getComponentInChild(self.transform, "Ball", "UISprite")
	self.uiArrow = self.transform:FindChild("Arrow").gameObject
	addOnClick(self.gameObject, self:ClickItem())
end

function TourItem:Start()
	self:Refresh()
end

function TourItem:Refresh()
	local PassedBallIcon = "tour_3on3_s"
	local UnpassedBallIcon = "tour_3on3_s_gray"

	local curTourID = MainPlayer.Instance.CurTourID
	local tourData = GameSystem.Instance.TourConfig:GetTourData(MainPlayer.Instance.Level, self.tourID)
	if not tourData then error(self.uiName, "Can't find tour data. Level:", MainPlayer.Instance.Level, "ID:", self.tourID) end
	self.gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(tourData.gameModeID)

	self.uiLabel.text = getCommonStr("TOUR_ROUND_NUM"):format(self.tourID)
	NGUITools.SetActive(self.uiArrow, self.isCurrent)
	self.uiSprBall.spriteName = self.tourID <= curTourID and PassedBallIcon or UnpassedBallIcon

	--self.uiLabelBg:MakePixelPerfect()
end

function TourItem:ClickItem()
	return function()
		if self.onClick then
			 self:onClick()
		end
	end
end

function TourItem:MakeOnRuleClick()
	return function ()
		local rule = getLuaComponent(createUI("MatchRulePopup"))
		rule.matchType = self.gameMode.matchType
		UIManager.Instance:BringPanelForward(rule.gameObject)
	end
end

function TourItem:MakeOnAwardClick()
	return function ()
		local popup = getLuaComponent(createUI("TourAwardPopup"))
		popup.tourID = self.tourID
		UIManager.Instance:BringPanelForward(popup.gameObject)
	end
end

return TourItem

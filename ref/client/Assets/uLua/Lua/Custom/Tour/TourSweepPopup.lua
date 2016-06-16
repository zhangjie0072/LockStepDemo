TourSweepPopup = {
	uiName = 'TourSweepPopup',
	
	tourID = 0,
	matchType = GameMatch.Type.eNone,
	times ,
	ruleClick = false,
	destroy = true,
	OnClick = nil,
}

function TourSweepPopup:Awake()
	self.title_right = getComponentInChild(self.transform,"Title","UILabel")
	self.iconNode = self.transform:FindChild("SectionInfo/Top/IconNode")
	self.leftTimes = getComponentInChild(self.transform,"SectionInfo/Middle/Award/Times/Label","UILabel")
	self.consume = getComponentInChild(self.transform,"SectionInfo/Middle/Award/Consume/Label","UILabel")
	self.rule = getChildGameObject(self.transform,"SectionInfo/Top/Rule")
	print("22222-----:",self.rule)
	addOnClick(self.rule,self:MakeOnRule())
end

function TourSweepPopup:Start()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiFrame.title = CommonFunction.GetConstString("TOUR_SWEEP")
	self.uiFrame.showCorner = true
	self.uiFrame.BUTTON_COUNT = 0
	self.uiFrame.onClose = self:MakeOnClose()
	UIManager.Instance:BringPanelForward(self.gameObject)
	--MatchTypetitle
	--self.title_right.text = self.sectionConfig.name
	self:Refresh()
end

function TourSweepPopup:Refresh()
	self.uiIcon = getLuaComponent(createUI("MatchTypeIcon", self.iconNode))
	print("66666___:",self.uiIcon)
	print("77777____:",self.matchType)
	self.uiIcon.matchType = self.matchType
	self.title_right.text = getCommonStr("TOUR_ROUND_NUM"):format(self.tourID)
	
	local maxResetTimes = GameSystem.Instance.TourConfig:GetResetTimes(MainPlayer.Instance.Vip)
	local RestTimes = maxResetTimes - MainPlayer.Instance.TourResetTimes 
	self.leftTimes.text = getCommonStr("STR_REMAIN_TIMES"):format(RestTimes, maxResetTimes)
	self.consume.text = getCommonStr("CONSUME")..":"
end

function TourSweepPopup:MakeOnRule()
	return function ()
		print("11111111111111")
		
	end
end

function TourSweepPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
		if self.OnClick then self.OnClick() end
	end
end

function TourSweepPopup:MakeOnRule()
	return function ()
		if self.ruleClick == false then
			self.rule = getLuaComponent(createUI("MatchRulePopup",self.transform))
			
			local pos = self.rule.gameObject.transform.localPosition
			pos.x = 98
			pos.y = 7
			self.rule.gameObject.transform.localPosition = pos
			
			self.rule.matchType = self.matchType
			--MatchRulePopup.rule = getCommonStr("TOUR_RULE_DESC")
			--MatchRulePopup.title = getCommonStr("TOUR_RULE")
			UIManager.Instance:BringPanelForward(self.rule.gameObject)
			self.ruleClick = true
		else 
			NGUITools.Destroy(self.rule.gameObject)
			self.ruleClick = false
		end	
	end
end

return TourSweepPopup
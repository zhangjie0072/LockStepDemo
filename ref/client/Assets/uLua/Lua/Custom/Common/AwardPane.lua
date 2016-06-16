require "Custom/Career/CareerUtil"

AwardPane = {
	uiName = 'AwardPane',
	---------------UI
	grid1,
	grid2,
	lblExpNum,
	lblGoldNum,
	lblPrestigeNum,
	---------------parameters
	awards = nil,	--table
	icon_adjust = false,
	isAlwaysShowExp = false,	-- display exp even it is zero.
	isAlwaysShowGold = false, -- display gold even it is zero.
	
}

function AwardPane:Awake()
	self.grid1 = getComponentInChild(self.transform, "Grid1", "UIGrid")
	self.grid2 = getComponentInChild(self.transform, "Grid2", "UIGrid")
	self.lblExpNum = getComponentInChild(self.grid1.transform, "ExpConsume/Num", "UILabel")
	self.lblGoldNum = getComponentInChild(self.grid1.transform, "GoldConsume/Num", "UILabel")
	self.lblPrestigeNum = getComponentInChild(self.grid1.transform, "PrestigeConsume/Num", "UILabel")
end

function AwardPane:Start()
	self.awards = self.awards or {}

	local exp, gold, prestige = 0, 0, 0
	for award_id, award_value in pairs(self.awards) do
		if award_id == GlobalConst.TEAM_EXP_ID or award_id == GlobalConst.ROLE_EXP_ID then
			exp = exp + award_value
		elseif award_id == GlobalConst.GOLD_ID then
			gold = gold + award_value
		elseif award_id == GlobalConst.PRESTIGE_ID then
			prestige = prestige + award_value
		end
	end
	self.lblExpNum.text = "+" .. exp
	if not self.isAlwaysShowExp then
		NGUITools.SetActive(self.lblExpNum.transform.parent.gameObject, exp > 0)
	end
	
	self.lblGoldNum.text = "+" .. gold
	if not self.isAlwaysShowGold then
		NGUITools.SetActive(self.lblGoldNum.transform.parent.gameObject, gold > 0)
	end
	
	self.lblPrestigeNum.text = "+" .. prestige
	NGUITools.SetActive(self.lblPrestigeNum.transform.parent.gameObject, prestige > 0)
	self.grid1:Reposition()
	if self.icon_adjust == true then
		self.grid1.arrangement = UIGrid.Arrangement.Horizontal
		local pos1 = self.grid1.gameObject.transform.localPosition
		pos1.x = -84
		pos1.y = 25
		self.grid1.gameObject.transform.localPosition = pos1 
		local pos = self.grid2.gameObject.transform.localScale
		pos.x = 0.7
		pos.y = 0.7
		self.grid2.gameObject.transform.localScale = pos
		local pos = self.grid2.gameObject.transform.localPosition
		pos.x = 0
		pos.y = -45
		self.grid2.gameObject.transform.localPosition = pos
		CareerUtil.ListAwards(self.awards, self.grid2.transform,nil,true)
	else
		CareerUtil.ListAwards(self.awards, self.grid2.transform)
	end
	self.grid2:Reposition()
end

return AwardPane

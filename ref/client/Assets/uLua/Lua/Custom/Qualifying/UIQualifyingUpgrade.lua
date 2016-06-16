module("UIQualifyingUpgrade", package.seeall)

uiName = "UIQualifyingUpgrade"

--parameters
success = true
score = 0
scoreDelta = 0
ranking = 0
detailPane = nil

--variables
fromQualifyingConfig = nil
toQualifyingConfig = nil

function Awake(self)
	local tmWindow = self.transform:FindChild("Window")
	self.spriteTitle = getComponentInChild(tmWindow, "Title", "UISprite")
	self.spriteFrom = getComponentInChild(tmWindow, "From", "UISprite")
	self.spriteTo = getComponentInChild(tmWindow, "To", "UISprite")
	self.gridStarFrom = getComponentInChild(tmWindow, "From/StarGrid", "UIGrid")
	self.gridStarTo = getComponentInChild(tmWindow, "To/StarGrid", "UIGrid")
	self.labelGrade = getComponentInChild(tmWindow, "Grade", "UILabel")
	self.labelRanking = getComponentInChild(tmWindow, "Grade/Ranking", "UILabel")
	self.labelScore = getComponentInChild(tmWindow, "Points/Num", "UILabel")
	self.btnOK = getChildGameObject(tmWindow, "OK")
	self.btnDetail = getChildGameObject(tmWindow, "Detail")

	addOnClick(self.btnOK, self:MakeOnOK())
	addOnClick(self.btnDetail, self:MakeOnDetail())

	NGUITools.SetActive(self.btnDetail, false)
end

function Start(self)
	print(self.uiName, "Score:", self.score)
	self.toQualifyingConfig = GameSystem.Instance.qualifyingNewConfig:GetGrade(self.score)
	if self.success then
		self.fromQualifyingConfig = GameSystem.Instance.qualifyingNewConfig:GetPrevGrade(self.score)
		if not self.fromQualifyingConfig then error(self.uiName, "Can not find prev grade") end
		print(self.uiName, "Prev grade score:", self.fromQualifyingConfig.score)
	else
		self.fromQualifyingConfig = GameSystem.Instance.qualifyingNewConfig:GetNextGrade(self.score)
		if not self.fromQualifyingConfig then error(self.uiName, "Can not find next grade") end
		print(self.uiName, "Next grade score:", self.fromQualifyingConfig.score)
	end

	self.spriteTitle.spriteName = self.success and self.SUCC_TITLE or self.FAIL_TITLE
	self.spriteFrom.spriteName = self.fromQualifyingConfig.icon
	self.spriteTo.spriteName = self.toQualifyingConfig.icon

	self.labelGrade.text = self.toQualifyingConfig.title
	if self.ranking == 0 then
		self.labelRanking.text = getCommonStr("NO_RANKING")
	else
		self.labelRanking.text = getCommonStr("RANK_SINGLESRCTION"):format(self.ranking)
	end
	self.labelScore.text = string.format("%d(%s%d)", self.score, self.scoreDelta > 0 and "+" or "", self.scoreDelta)

	CommonFunction.ClearChild(self.gridStarFrom.transform)
	for i = 1, self.fromQualifyingConfig.star do
		createUI("Star", self.gridStarFrom.transform)
	end
	self.gridStarFrom:Reposition()

	CommonFunction.ClearChild(self.gridStarTo.transform)
	for i = 1, self.toQualifyingConfig.star do
		createUI("Star", self.gridStarTo.transform)
	end
	self.gridStarTo:Reposition()

	if self.detailPane then
		self.detailPane.onClose = self:MakeOnDetailClose()
		self.detailPane:ShowDetails()
		NGUITools.SetActive(self.detailPane.gameObject, false)
	end
end

function MakeOnOK(self)
	return function ()
		jumpToUI("UIQualifyingNew", nil, {showIncStarAnim = self.success})
	end
end

function MakeOnDetail(self)
	return function ()
		if self.detailPane then
			NGUITools.SetActive(self.detailPane.gameObject, true)
			NGUITools.SetActive(self.gameObject, false)
		end
	end
end

function MakeOnDetailClose(self)
	return function ()
		if self.detailPane then
			NGUITools.SetActive(self.detailPane.gameObject, false)
			NGUITools.SetActive(self.gameObject, true)
		end
	end
end

return UIQualifyingUpgrade

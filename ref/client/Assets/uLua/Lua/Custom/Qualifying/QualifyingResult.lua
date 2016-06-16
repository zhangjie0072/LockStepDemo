--encoding=utf-8

QualifyResult = 
{
	uiName = 'QualifyResult',
	--------------------UI
	uiHomeName,
	uiHomeRank,
	uiHomeRoleGrid,
	uiAwayName,
	uiAwayRank,
	uiAwayRoleGrid,
	uiMask,
	--parameters
	upRank,
	homeTeam,
	homeName,
	awayTeam,
	awayName,
	awayRank,
	awards,
	isWin,
	leagueType,
}


-----------------------------------------------------------------
--Awake
function QualifyResult:Awake()
	self.uiHomeName = self.transform:FindChild("Window/Middle/Left/Name"):GetComponent("UILabel")
	self.uiHomeRank = self.transform:FindChild("Window/Middle/Left/Num"):GetComponent("UILabel")
	self.uiHomeRoleGrid = self.transform:FindChild("Window/Middle/Left/Grid")
	self.uiAwayName = self.transform:FindChild("Window/Middle/Right/Name"):GetComponent("UILabel")
	self.uiAwayRank = self.transform:FindChild("Window/Middle/Right/Num"):GetComponent("UILabel")
	self.uiAwayRoleGrid = self.transform:FindChild("Window/Middle/Right/Grid")
	self.uiArrowIcom = self.transform:FindChild("Window/ArrowIcom")
	self.uiNum = self.transform:FindChild("Window/Num"):GetComponent("UILabel")
	self.uiHistoryIcom = self.transform:FindChild("Window/HistoryIcom")
	self.uiHistoryIcomRank = self.transform:FindChild("Window/HistoryIcom/Num"):GetComponent("UILabel")
	self.uiMask = self.transform:FindChild("Window").gameObject
	addOnClick(self.uiMask, self:ClickMask())
end

function QualifyResult:Start()
	self.uiHomeName.text = self.homeName
	self.uiHomeRank.text = MainPlayer.Instance.QualifyingRanking
	self.uiAwayName.text = self.awayName
	self.uiAwayRank.text = self.awayRank

	--init home role
	local roles = self.homeTeam:GetEnumerator()
	while roles:MoveNext() do
		local homeRole = getLuaComponent(createUI("CareerRoleIcon", self.uiHomeRoleGrid))
		homeRole.id = roles.Current.id
	end

	--init away role
	local awayRoles = self.awayTeam:GetEnumerator()
	while awayRoles:MoveNext() do
		local awayRole = getLuaComponent(createUI("CareerRoleIcon", self.uiAwayRoleGrid))
		awayRole.id = awayRoles.Current.id
		awayRole.isQReal = true
		awayRole.qQuality = awayRoles.Current.quality
		awayRole.qLevel = awayRoles.Current.level
		awayRole.qStar =awayRoles.Current.star
	end

	--if upRank
	if self.upRank > 0 then
		NGUITools.SetActive(self.uiArrowIcom.gameObject, true)
		NGUITools.SetActive(self.uiNum.gameObject, true)
		self.uiNum.text = self.upRank
	end
	--if refresh maxRank
	print(self.uiName,"----histroy max rank:",MainPlayer.Instance.QualifyingInfo.max_ranking)
	if MainPlayer.Instance.QualifyingRanking < MainPlayer.Instance.QualifyingInfo.max_ranking 
		or MainPlayer.Instance.QualifyingInfo.max_ranking == 0 then
		NGUITools.SetActive(self.uiHistoryIcom.gameObject, true)
		self.uiHistoryIcomRank.text = MainPlayer.Instance.QualifyingRanking
		MainPlayer.Instance.QualifyingInfo.max_ranking = MainPlayer.Instance.QualifyingRanking
	end
end

function QualifyResult:ClickMask()
	return function()
		print(self.uiName, "ClickMask")
		local result = getLuaComponent(createUI("UIMatchResult"))
        UIManager.Instance:BringPanelForward(result.gameObject);
       	result.awards = self.awards
       	result.isWin = true
       	result.leagueType = self.leagueType
       	NGUITools.Destroy(self.gameObject)
	end
end

return QualifyResult
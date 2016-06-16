--encoding=utf-8

RankingBarItem = 
{
	uiName = 'RankingBarItem',
	
	-------------------------------------
	
	-------------------------------------UI
	uiBg,
	uiRanking,
	uiInfoLevel,
	uiInfoName,
	uiScore,

	uiDragSV,
};


-----------------------------------------------------------------
--Awake
function RankingBarItem:Awake()
	local transform = self.transform
	--±³¾°
	self.uiBg = transform:GetComponent('UISprite')
	self.uiRanking = transform:FindChild('Ranking'):GetComponent('UILabel')

	self.uiInfoName = transform:FindChild('Info/Name'):GetComponent('UILabel')
	self.uiScore = transform:FindChild('Score'):GetComponent('UILabel')

	self.uiDragSV = transform:GetComponent('UIDragScrollView')
end

--Start
function RankingBarItem:Start()

end

--Update
function RankingBarItem:Update( ... )
	-- body
end


-----------------------------------------------------------------
--
function RankingBarItem:SetRanking(ranking, barNum)
	self.uiRanking.text = ranking
	self.uiBg.spriteName = (barNum % 2) == 0 and 'com_bg_pure_white' or 'com_bg_pure_browngray'
end

--
function RankingBarItem:SetInfoLevel(level)
	self.uiInfoLevel.text = level
end

--
function RankingBarItem:SetInfoName(name)
	self.uiInfoName.text = name
end

--
function RankingBarItem:SetScore(score)
	self.uiScore.text = score
end

--
function RankingBarItem:SetDragSV(sv)
	self.uiDragSV.scrollView = sv
end

--
function RankingBarItem:SetBg(bgName)
	self.uiBg.spriteName = bgName
end

return RankingBarItem
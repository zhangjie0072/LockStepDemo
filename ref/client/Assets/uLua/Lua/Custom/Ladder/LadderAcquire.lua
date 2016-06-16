------------------------------------------------------------------------
-- class name    : LadderAcquire
-- create time   : Thu Mar  3 21:40:06 2016
------------------------------------------------------------------------

LadderAcquire =  {
	uiName     = "LadderAcquire",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------

	-----------------------
	-- Parameters Module --
	-----------------------
	-- |------------+-----------------------------------------------------|
	-- | Usage      | Comment                                             |
	-- |------------+-----------------------------------------------------|
	-- | LocalLevel | deault: finsh x games and determin the ladder level |
	-- |------------+-----------------------------------------------------|
	-- | Finish10   | finish 10 ladder match                              |
	-- |------------+-----------------------------------------------------|
	usage       = "Finish10",

	winNum      = 39,
	loseNum     = 43,
	initScore   = 349344,
	adjustScore = 45454333,
	curScore    = 1234586876,

	ladderRewardWin = nil,
	leagueInfoTbl  = nil,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function LadderAcquire:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function LadderAcquire:Start()
	self.uiLocalLevelNode.gameObject:SetActive(self.usage=="LocalLevel")
	self.uiFinish10Node.gameObject:SetActive(self.usage=="Finish10")

	addOnClick(self.uiConfirm.gameObject, self:ClickConfirm())
	addOnClick(self.uiShowOff.gameObject, self.ClickShowOff())

	self.leagueInfoTbl = {}
	for i = 1,  5 do
		table.insert(self.leagueInfoTbl, self.uiLeagueInfoGrid.transform:FindChild(i))
	end

	-- self.uiWinNumGrid.onCustomSort = function(x, y)
	--	return tonumber(x.name) > tonumber(y.name) and -1 or 1 end
	-- local s = self.curScore
	-- local i = 1
	-- while s >= 1 do
	--	local g = s % 10
	--	local t = self.uiNumU.gameObject
	--	if i > 1 then
	--		t = Object.Instantiate(t)
	--		t.transform.name = i
	--	end
	--	t.transform:GetComponent("UISprite").spriteName = "VIP_" .. g
	--	t.transform.parent = self.uiWinNumGrid.transform
	--	t.transform.localScale = Vector3.New(1, 1, 1)
	--	s = math.modf(s / 10)
	--	i = i + 1
	-- end

	-- self.uiWinNumGrid.repositionNow = true


	-- self.uiWinNum.text = self.ladderRewardWin
	-- self.uiLoseNum.text = 5 - self.ladderRewardWin

	local winNum = 0
	local enum = self.ladderRewardWin:GetEnumerator()
	local index = 1
	while enum:MoveNext() do
		local v = enum.Current
		local info = self.leagueInfoTbl[index]:GetComponent("UISprite")
		info.gameObject:SetActive(true)
		if v == 1 then
			info.spriteName = "tencent_fivewin"
			winNum = winNum + 1
		else
			info.spriteName = "tencent_fivelose"
		end
		index = index + 1
	end

	local reward = GameSystem.Instance.ladderConfig:GetReward(winNum)
	local rewards = reward.rewards
	local extraScore = reward.extra_score

	self.uiExtraScore.text = string.format(getCommonStr("STR_FIELD_PROMPT16"), extraScore)
	local enum = rewards:GetEnumerator()
	CommonFunction.ClearGridChild(self.uiGoodsIconGrid.transform)
	while enum:MoveNext() do
		local id = enum.Current.Key
		local num = enum.Current.Value

		if id == 1 or id == 2 then
			local p = id == 1 and self.uiDiamoandNode or self.uiGoldNode
			local t = getLuaComponent(createUI("GoodsIconConsume", p))
			t.rewardId = id
			t.rewardNum = num
		else
			local t = getLuaComponent(createUI("GoodsIcon", self.uiGoodsIconGrid.transform))
			t.goodsID = id
			t.num = num
			t.hideNum = false
			t.hideNeed = true
		end
	end
	self.uiGoodsIconGrid.repositionNow = true

	self:Refresh()
end

function LadderAcquire:Refresh()
	-- self.uiWinNum.text = self.winNum
	-- self.uiLoseNum.text = self.loseNum
	-- self.uiLadderScore.text = string.format(getCommonStr("STR_FIELD_PROMPT14"), self.initScore, self.adjustScore)


end

-- uncommoent if needed
-- function LadderAcquire:FixedUpdate()

-- end


function LadderAcquire:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function LadderAcquire:ClickConfirm()
	return function()
		self.nextShowUI = "UIHall"
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function LadderAcquire:OnClose()
	if self.nextShowUI then
		if self.nextShowUI=="UIHall" then
			jumpToUI("UIHall", nil, nil)
			return
		end
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end
end


function LadderAcquire:ClickShowOff()
	return function()
	end
end



---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function LadderAcquire:UiParse()
	self.uiLocalLevelNode = self.transform:FindChild("Window/WinMiddle")
	self.uiFinish10Node = self.transform:FindChild("Window/TenMiddle")

	-- top.
	self.uiLeagueInfoGrid = self.transform:FindChild("Window/LeagueInfoGrid"):GetComponent("UIGrid")

	self.uiConfirm = self.transform:FindChild("Window/Confirm"):GetComponent("UIButton")
	self.uiShowOff = self.transform:FindChild("Window/ShowOff"):GetComponent("UIButton")

	self.uiWinNum = self.transform:FindChild("Window/Win/WinNum"):GetComponent("UILabel")
	self.uiLoseNum = self.transform:FindChild("Window/LoseNum"):GetComponent("UILabel")

	self.uiLadderScore = self.transform:FindChild("Window/WinMiddle/LadderScore"):GetComponent("UILabel")
	self.uiWinNumGrid = self.transform:FindChild("Window/WinMiddle/Num"):GetComponent("UIGrid")
	self.uiNumU = self.transform:FindChild("Window/WinMiddle/Num/1"):GetComponent("UISprite")

	self.uiExtraScore = self.transform:FindChild("Window/TenMiddle/Text"):GetComponent("UILabel")
	self.uiDiamoandNode = self.transform:FindChild("Window/TenMiddle/GoodsIconConsume0")
	self.uiGoldNode = self.transform:FindChild("Window/TenMiddle/GoodsIconConsume1")
	self.uiGoodsIconGrid = self.transform:FindChild("Window/TenMiddle/GoodsIconGrid"):GetComponent("UIGrid")
end

return LadderAcquire

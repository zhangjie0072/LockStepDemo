--encoding=utf-8

QualifyingRulePopup = 
{
	uiName = 'QualifyingRulePopup',
	-----------------UI
	uiIconGrid,
	uiRankLabel,
	uiMaxRankLabel,
	uiExplain,
	uiRuleGrid,
	-----------------parameters
	cur_rank,
	max_rank,
	rank_min,
	rank_max,
	-- award_daimonds,
	-- award_golds,
	-- award_rankicons,
	rewardTable = {},
	include = false,
	onClose,
}


-----------------------------------------------------------------
--Awake
function QualifyingRulePopup:Awake()
	self.uiBtnClose = self.transform:FindChild("Window/ButtonClose")
	self.uiIconGrid = self.transform:FindChild("Window/Scroll/Rules/Upside/Grid")
	self.uiTitle = self.transform:FindChild("Window/Title1"):GetComponent("MultiLabel")
	self.uiTitle:SetText( getCommonStr("QUALIFYING_RULE"))
	self.uiRankLabel = getComponentInChild(self.transform,"Window/Scroll/Rules/Upside/CurRank","MultiLabel")
	self.uiMaxRankLabel = getComponentInChild(self.transform,"Window/Scroll/Rules/Upside/MaxRankHistory","MultiLabel")
	self.uiExplain = getComponentInChild(self.transform,"Window/Scroll/Rules/Upside/LabelExplain","UILabel")
	self.uiRuleGrid = self.transform:FindChild("Window/Scroll/Rules/Botton/Rule")
	self.uiAwardsGrid = self.transform:FindChild("Window/Scroll/Rules/Botton/Grid"):GetComponent("UIGrid")
	--self.rankicons = getChildGameObject(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon1")
	--self.rankiconsNum = getComponentInChild(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon1/Num","UILabel")
	--self.diamonds = getChildGameObject(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon2")
	--self.diamondsNum =  getComponentInChild(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon2/Num","UILabel")
	--self.golds = getChildGameObject(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon3")
	--self.goldsNum = getComponentInChild(self.transform,"Scroll/Rules/Upside/Grid/GoodsIcon3/Num","UILabel")
	--self.grid = getChildGameObject(self.transform,"Scroll/Rules/Botton/Grid")
end

function QualifyingRulePopup:Start()
	local close = getLuaComponent(createUI("ButtonClose",self.uiBtnClose))
	close.onClick = self:MakeOnClose()
	--
	print("-----++++:",self.include)
	if self.cur_rank == 0  then
		self.uiRankLabel:SetText(getCommonStr("CURRENT_RANK")..":"..GameSystem.Instance.CommonConfig:GetUInt("QualifyingMaxRank")..getCommonStr("OVER_MAXRANK"))
	else		
		self.uiRankLabel:SetText( getCommonStr("CURRENT_RANK")..":"..self.cur_rank)
		for id ,value in pairs(self.rewardTable) do
			local icon = getLuaComponent(createUI("GoodsIconConsume",self.uiIconGrid.transform))
			icon.rewardId = value.id
			icon.rewardNum = value.value
		end
	end
	if self.max_rank == 0 then
		self.uiMaxRankLabel:SetText( getCommonStr("HISTORY_MAX_RANK")..":"..GameSystem.Instance.CommonConfig:GetUInt("QualifyingMaxRank")..getCommonStr("OVER_MAXRANK"))
	else
		self.uiMaxRankLabel:SetText( getCommonStr("HISTORY_MAX_RANK")..":"..self.max_rank)
	end
	if self.cur_rank == 0 or self.include == false then
		--保持当前排名没有奖励
		self.uiExplain.text = getCommonStr("CURRENT_RANK_NOAWARDS")
		NGUITools.SetActive(self.rankicons,false)
		NGUITools.SetActive(self.diamonds,false)
		NGUITools.SetActive(self.golds,false)
	elseif self.rank_min == self.rank_max then
		self.uiExplain.text = getCommonStr("KEEP_SINGLERANK_AWARDS"):format(self.cur_rank)
	else
		self.uiExplain.text = getCommonStr("KEEP_RANK_AWARDS"):format(self.rank_min, self.rank_max)
	end
	--1
	-- if self.award_rankicons then
	-- 	--NGUITools.SetActive(self.rankicons,true)
	-- 	local icon = getLuaComponent(self.rankicons)
	-- 	icon.reward_id = 3
	-- 	icon.reward_num = self.award_rankicons  
	-- end
	-- --2
	-- if self.award_daimonds then
	-- 	--NGUITools.SetActive(self.diamonds,true)
	-- 	local icon = getLuaComponent(self.diamonds)
	-- 	icon.reward_id = 1
	-- 	icon.reward_num = self.award_daimonds  
	-- end
	-- --3
	-- if self.award_golds then
	-- 	--NGUITools.SetActive(self.golds,true)
	-- 	local icon = getLuaComponent(self.golds)
	-- 	icon.reward_id = 2
	-- 	icon.reward_num = self.award_golds  
	-- end
	
	--规则
	local matchRule = getCommonStr("RANK_RULE_DESC")
	self.uiRulePane = getLuaComponent(createUI("RulePane1", self.uiRuleGrid.transform))
	self.uiRulePane.rule = matchRule
	--奖励列表
	local data = GameSystem.Instance.qualifyingConfig.DayAwardsData
	local enum = data:GetEnumerator()
	local k = 1
	while enum:MoveNext() do
		if k > 10 then ---- include 10 lines
			break
		end	
		local award = getLuaComponent(createUI("QualifyAwardItem",self.uiAwardsGrid.transform))
		award.rank_min = enum.Current.rank_min
		award.rank_max = enum.Current.rank_max
		award.databyid = enum.Current.databyid
		k = k + 1
	end
	self.uiAwardsGrid:Reposition()
end

function QualifyingRulePopup:MakeOnClose()
	return function ()
		if self.onClose then
			self.onClose()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

return QualifyingRulePopup
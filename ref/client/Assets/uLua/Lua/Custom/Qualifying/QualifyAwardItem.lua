--encoding=utf-8

QualifyAwardItem = 
{
	uiName = 'QualifyAwardItem',
	--------------------UI
	uiIconGrid,
	uiRankLabel,
	--------------------parameters
	rank_min,
	rank_max,
	databyid,	
}


-----------------------------------------------------------------
--Awake
function QualifyAwardItem:Awake()
	self.uiIconGrid = self.transform:FindChild("Grid")
	self.uiRankLabel = getComponentInChild(self.transform,"Rank","UILabel")
	-- --ÅÅÎ»±Ò
	-- self.uiRankIcons = getChildGameObject(self.transform,"Grid/uiRankIcons")
	-- self.rankicons_num = self.uiRankIcons.transform:FindChild("Num"):GetComponent("UILabel")
	-- --×êÊ¯
	-- self.diamonds = getChildGameObject(self.transform,"Grid/Diamonds")
	-- self.diamonds_num = self.diamonds.transform:FindChild("Num"):GetComponent("UILabel")
	-- --½ð±Ò
	-- self.golds = getChildGameObject(self.transform,"Grid/Golds")
	-- self.golds_num = self.golds.transform:FindChild("Num"):GetComponent("UILabel")
end

function QualifyAwardItem:Start()
	if self.rank_min == self.rank_max then
		--print("111111111111111")
		self.uiRankLabel.text = getCommonStr("RANK_SINGLESRCTION"):format(self.rank_max)
	else
		--print("222222222222222")
		self.uiRankLabel.text = getCommonStr("RANK_SRCTION"):format(self.rank_min, self.rank_max)
	end
	local enum = self.databyid:GetEnumerator()
	while enum:MoveNext() do
		-- if enum.Current.id == 1 then
		-- 	--NGUITools.SetActive(self.uiRankIcons,true)
		-- 	local icon = getLuaComponent(self.diamonds)
		-- 	icon.reward_id = 1
		-- 	icon.reward_num = enum.Current.value
		-- 	--self.rankicons_num.text = enum.Current.value
		-- elseif enum.Current.id == 2 then
		-- 	--NGUITools.SetActive(self.diamonds,true)
		-- 	local icon = getLuaComponent(self.golds)
		-- 	icon.reward_id = 2
		-- 	icon.reward_num = enum.Current.value
		-- 	--self.diamonds_num.text = enum.Current.value
		-- elseif enum.Current.id == 3 then
		-- 	--NGUITools.SetActive(self.golds,true)
		-- 	local icon = getLuaComponent(self.uiRankIcons)
		-- 	icon.reward_id = 3
		-- 	icon.reward_num = enum.Current.value
		-- 	--self.golds_num.text = enum.Current.value
		-- end
		local icon = getLuaComponent(createUI("GoodsIconConsume",self.uiIconGrid))
		icon.rewardId = enum.Current.id
		icon.rewardNum = enum.Current.value
	end
end

return QualifyAwardItem
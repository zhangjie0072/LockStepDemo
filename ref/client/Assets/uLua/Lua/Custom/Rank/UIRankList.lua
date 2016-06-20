UIRankList = {
	uiName = "UIRankList",

	-- parameters
	defaultType = nil,
	onClose = nil,

	-- UI
	uiAnimator,
	myItem,
	items,

	-- variables
	rankTypes, -- {rankType -> {btn, toggle, config, onSwitch}, ...}
	curRankType,
	curRankList,

	-- callbacks
	onListRefreshed,
	onUpdateItem,
	onTipRefreshTimer,
}

-----------------------------------------------------------------
function UIRankList:Awake()
	self.tmSwitches = self.transform:FindChild("Switches")
	self.btnQualifying = getChildGameObject(self.tmSwitches, "Qualifying")
	self.btnLadder = getChildGameObject(self.tmSwitches, "Ladder")
	self.btnAchievement = getChildGameObject(self.tmSwitches, "Career")
	self.tmMyRank = self.transform:FindChild("Window/MyRank/My")
	self.scroll = getComponentInChild(self.transform, "Window/RankList/Scroll", "UIScrollView")
	self.wrap = getComponentInChild(self.scroll.transform, "Wrap", "UIWrapContent")
	self.btnBack = getLuaComponent(createUI("ButtonBack", self.transform:FindChild('Top/ButtonBack')))
	self.lblTipRefresh = getComponentInChild(self.transform, "Window/TipRefresh", "UILabel")
	self.lblPointsTitle = getComponentInChild(self.transform, "Window/Title/Points", "UILabel")

	self.uiAnimator = self.transform:GetComponent('Animator')

	self.rankTypes = {}
	self.rankTypes[RankType.RT_QUALIFYING_NEW] = {btn = self.btnQualifying}
	self.rankTypes[RankType.RT_LADDER] = {btn = self.btnLadder}
	self.rankTypes[RankType.RT_ACHIEVEMENT] = {btn = self.btnAchievement}

	for k, v in pairs(self.rankTypes) do
		v.toggle = v.btn:GetComponent("UIToggle")
		v.onSwitch = self:MakeOnSwitch(k)
		addOnClick(v.btn, v.onSwitch)
	end

	self.myItem = getLuaComponent(createUI("RankListItem", self.tmMyRank))
	self.myItem.hideBG = true
	self.items = {}
	for i = 0, 4 do
		local item = getLuaComponent(createUI("RankListItem", self.wrap.transform))
		item.gameObject.name = tostring(i)
		table.insert(self.items, item)
		NGUITools.SetActive(item.gameObject, false)
	end
	self.wrap:SortAlphabetically()

	self.onListRefreshed = self:MakeOnListRefreshed()
	self.onUpdateItem = self:MakeOnUpdateItem()
	--self.onTipRefreshTimer = LuaHelper.Action(self:TipRefresh())
end

function UIRankList:Start()
	for k, v in pairs(self.rankTypes) do
		v.config = GameSystem.Instance.RankConfig:GetConfig(k)
		if not v.config then
			error(self.uiName, "no rank config for type:", k)
		end
	end

	self.btnBack.onClick = self:OnBack()

	self.wrap.onInitializeItem = self.onUpdateItem
end

function UIRankList:OnEnable()
	RankList.onListRefreshed = self.onListRefreshed
	--Scheduler.Instance:AddTimer(1, true, self.onTipRefreshTimer)
end

function UIRankList:OnDisable()
	if RankList.onListRefreshed == self.onListRefreshed then
		RankList.onListRefreshed = nil
	end
	--Scheduler.Instance:RemoveTimer(self.onTipRefreshTimer)
end

function UIRankList:OnClose()
	if self.onClose then
		self.onClose()
		self.onClose = nil
		return
	end

	TopPanelManager:HideTopPanel()
end

function UIRankList:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIRankList:OnBack()
	return function (go)
		self:DoClose()
	end
end

function UIRankList:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIRankList:Refresh()
	for k, v in pairs(self.rankTypes) do
		--NGUITools.SetActive(v.btn, validateFunc(v.config.limit_condition, false))
		if self.defaultType == k or (not self.defaultType and v.config.default_display == 1) then
			v.toggle.value = true
			v.onSwitch()
		else
			v.toggle.value = false
		end
	end
end

--距离下次刷新倒计时
--[[
function UIRankList:TipRefresh()
	return function ()
	  local mTime = os.date('%M', GameSystem.mTime)
	  local sTime= os.date('%S', GameSystem.mTime)
	  mTime = 59 - mTime
	  sTime = 59 - sTime
	  self.lblTipRefresh.text = string.format(getCommonStr("TIP_REFRESH"), mTime, sTime)
	  if not self.lblTipRefresh.gameObject.activeSelf then
	    NGUITools.SetActive(self.lblTipRefresh.gameObject, true)
      end
	end
end
--]]

function UIRankList:MakeOnSwitch(rankType)
	return function ()

		-- ranktypes: RT_ACHIEVEMENT RT_LADDER RT_QUALIFYING_NEW
		if rankType == 'RT_ACHIEVEMENT' then
			if not FunctionSwitchData.CheckSwith(FSID.rank_achievement) then return end
		end

		if rankType == 'RT_QUALIFYING_NEW' then
			if not FunctionSwitchData.CheckSwith(FSID.rank_qualifying) then return end
		end

		self.curRankType = rankType
		print('UIRankList:MakeOnSwitch',rankType)
		local config = self.rankTypes[rankType].config
		self.lblPointsTitle.text = config.points_name
		-- if config.click_refresh == 1 or not RankList.GetList(rankType) then
		-- 	RankList.ReqList(rankType)
		-- else
		local myRank = RankList.GetMyRankInfo(rankType)
		if not myRank then 
			RankList.myRankInfos[rankType] = RankList.MakeMyRankInfo(rankType)
			myRank = RankList.myRankInfos[rankType]
		end

			self:RefreshList(rankType, RankList.GetList(rankType),myRank)
		-- end
	end
end

function UIRankList:MakeOnListRefreshed()
	return function (rankType, rankList, myRankInfo)
		self:RefreshList(rankType, rankList, myRankInfo)
	end
end

function UIRankList:RefreshList(rankType, rankList, myRankInfo)
	print(self.uiName, "RefreshList", rankType)
	if rankType == self.curRankType then
		self.curRankList = rankList
		self.wrap.minIndex = -table.getn(self.curRankList) + 1
		self.wrap.maxIndex = 0
		print(self.uiName, "minIndex", self.wrap.minIndex, "maxIndex", self.wrap.maxIndex)

		local count = self.wrap.transform.childCount
		for i = 0, count - 1 do
			local go = self.wrap.transform:GetChild(i).gameObject
			go.name = tostring(i)
			self.onUpdateItem(go, i, i)
		end
		self.wrap:SortAlphabetically()
		self.scroll:ResetPosition()

		self.myItem.rankType = self.curRankType
		self.myItem.rank = myRankInfo.ranking
		self.myItem.rankInfo = myRankInfo
		self.myItem:Refresh()
	end
end

function UIRankList:MakeOnUpdateItem()
	return function(obj, index, realIndex)
		if not self.curRankList then return end

		realIndex = math.abs(realIndex)
		print(self.uiName, "OnUpdateItem", index, realIndex)

		local rank = realIndex + 1
		if rank > table.getn(self.curRankList) then
			NGUITools.SetActive(obj, false)
			return
		end

		NGUITools.SetActive(obj, true)
		local item = getLuaComponent(obj)
		item.rank = rank
		item.rankInfo = self.curRankList[rank]
		item.rankType = self.curRankType
		item:Refresh()
	end
end

return UIRankList

------------------------------------------------------------------------
-- class name    : BadgeStorePanel
-- create time   : 19:41 3-8-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgeStorePanel = {
	uiName = "BadgeStorePanel",
	--------params----
	currentStoreViewCategory = nil,
	currentStoreViewLevel = nil,
	allGoods = nil,
	currentCheckGoods = nil,
	--------UI--------
	uiStoreTabAllBtn,
	uiStoreTabRedBtn,
	uiStoreTabBlueBtn,
	uiStoreTabGreenBtn,
	uiStoreTabGoldenBtn,
	uiStoreItemsContainer,
	uiBadgesVScorllView,
	uiWrap,
	uiBadgeToNumLabel,
	uiLevelOneTabBtn,
	uiLevelTwoTabBtn,
	uiLevelThreeTabBtn,
	uiLevelFourTabBtn,
	uiLevelFiveTabBtn,
	uiGotoDecomposeGroupBtn,
}

------store tab type----
local StoreTabType={
	TabAll 		= 1,
	TabRed 		= 2,
	TabBlue 	= 3,
	TabGreen 	= 4,
	TabGolden 	= 5,
}

local StoreGoodsLevel ={
	LevelOne 	= 1,
	LevelTwo	= 2,
	LevelThree	= 3,
	LevelFour	= 4,
	LevelFive	= 5,
}

------Awake---------
function BadgeStorePanel:Awake( ... )
	-- body
	self:UIParse()
end
------ Start------
function BadgeStorePanel:Start()
	self:AddEvent()
	self.uiWrap.onInitializeItem = self:UpdateStoreGroupItem()
end

-------Update----------
function BadgeStorePanel:Update()

end

------FixedUpdate------
function BadgeStorePanel:FixedUpdate()

end

---------OnDestroy-------
function BadgeStorePanel:OnDestroy()
	-- body
end

function BadgeStorePanel:UIParse( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiStoreTabAllBtn = find("TabGrid/TabAll"):GetComponent("UIToggle")
	self.uiStoreTabRedBtn = find("TabGrid/TabRed"):GetComponent("UIToggle")
	self.uiStoreTabBlueBtn = find("TabGrid/TabBlue"):GetComponent("UIToggle")
	self.uiStoreTabGreenBtn = find("TabGrid/TabGreen"):GetComponent("UIToggle")
	self.uiStoreTabGoldenBtn = find("TabGrid/TabGolden"):GetComponent("UIToggle")
	self.uiLevelOneTabBtn = find("Down/LevelGrid/TabI"):GetComponent("UIToggle")
	self.uiLevelTwoTabBtn = find("Down/LevelGrid/TabII"):GetComponent("UIToggle")
	self.uiLevelThreeTabBtn = find("Down/LevelGrid/TabIII"):GetComponent("UIToggle")
	self.uiLevelFourTabBtn = find("Down/LevelGrid/TabIV"):GetComponent("UIToggle")
	self.uiLevelFiveTabBtn = find("Down/LevelGrid/TabV"):GetComponent("UIToggle")
	self.uiGotoDecomposeGroupBtn = find("Down/ButtonOK")
	self.uiStoreItemsContainer = find("Wear/ScrollView/Grid"):GetComponent("UIGrid")
	self.uiBadgesVScorllView = find("Wear/ScrollView"):GetComponent("UIScrollView")
	self.uiBadgeToNumLabel = find("Top/Num"):GetComponent("UILabel")
	self.uiWrap = find("Wear/ScrollView/Grid"):GetComponent("UIWrapContent")
	for i = 0, 3 do
		local item = createUI("BadgeStoreItemGroup", self.uiWrap.transform)
		item.gameObject.name = tostring(i)
		local item1 = getLuaComponent(item.transform:FindChild("item1"))
		local item2 = getLuaComponent(item.transform:FindChild("item2"))
		local item3 = getLuaComponent(item.transform:FindChild("item3"))
		item1.itemSelectHanlder = self:ItemClick()
		item2.itemSelectHanlder = self:ItemClick()
		item3.itemSelectHanlder = self:ItemClick()
		NGUITools.SetActive(item.gameObject, false)
	end
	self.uiWrap:SortAlphabetically()
end

function BadgeStorePanel:AddEvent( ... )
	-- body
	addOnClick(self.uiStoreTabAllBtn.gameObject,self:OnShowAllBadge())
	addOnClick(self.uiStoreTabRedBtn.gameObject,self:OnShowRedBadge())
	addOnClick(self.uiStoreTabBlueBtn.gameObject,self:OnShowBlueBadge())
	addOnClick(self.uiStoreTabGreenBtn.gameObject,self:OnShowGreenBadge())
	addOnClick(self.uiStoreTabGoldenBtn.gameObject,self:OnShowGoldenBadge())

	addOnClick(self.uiLevelOneTabBtn.gameObject,self:OnShowLevelOneBadge())
	addOnClick(self.uiLevelTwoTabBtn.gameObject,self:OnShowLevelTwoBadge())
	addOnClick(self.uiLevelThreeTabBtn.gameObject,self:OnShowLevelThreeBadge())
	addOnClick(self.uiLevelFourTabBtn.gameObject,self:OnShowFourBadge())
	addOnClick(self.uiLevelFiveTabBtn.gameObject,self:OnShowFiveBadge())
	addOnClick(self.uiGotoDecomposeGroupBtn.gameObject,self:OnGotoDecomposeByGroup())
end

function BadgeStorePanel:OnGotoDecomposeByGroup( ... )
	return function()
		if not FunctionSwitchData.CheckSwith(FSID.scrawl_disintegrate) then return end

		local window = createUI("BadgeDecomposeByGroupWindow")
		local t = getLuaComponent(window)
		t.refreshCallBack = self:RefreshView()
		UIManager.Instance:BringPanelForward(window)
	end
end

function BadgeStorePanel:OnShowLevelOneBadge( ... )
	return function()
		if self.currentStoreViewLevel == StoreGoodsLevel.LevelOne then
			return
		end
		self.currentStoreViewLevel = StoreGoodsLevel.LevelOne
		self:ShowStoreItemByCategory()
	end
end

function BadgeStorePanel:OnShowLevelTwoBadge( ... )
	return function()
		if self.currentStoreViewLevel == StoreGoodsLevel.LevelTwo then
			return
		end
		self.currentStoreViewLevel = StoreGoodsLevel.LevelTwo
		self:ShowStoreItemByCategory()
	end
end

function BadgeStorePanel:OnShowLevelThreeBadge( ... )
	return function()
		if self.currentStoreViewLevel == StoreGoodsLevel.LevelThree then
			return
		end
		self.currentStoreViewLevel = StoreGoodsLevel.LevelThree
		self:ShowStoreItemByCategory()
	end
end

function BadgeStorePanel:OnShowFourBadge( ... )
	return function()
		if self.currentStoreViewLevel == StoreGoodsLevel.LevelFour then
			return
		end
		self.currentStoreViewLevel = StoreGoodsLevel.LevelFour
		self:ShowStoreItemByCategory()
	end
end

function BadgeStorePanel:OnShowFiveBadge( ... )
	return function()
		if self.currentStoreViewLevel == StoreGoodsLevel.LevelFive then
			return
		end
		self.currentStoreViewLevel = StoreGoodsLevel.LevelFive
		self:ShowStoreItemByCategory()
	end
end

function BadgeStorePanel:UpdateStoreGroupItem( ... )
	return function(obj,index,realIndex)
		-- print("Store WrapContent List Update!***********************"..index.."**"..realIndex.."obj.name:"..obj.name)
		if self.currentCheckGoods == nil then
			-- print("no CurrentSelect Goods")
			return
		end
		realIndex = math.abs(realIndex)
		local useIndex = realIndex
		local maxline = math.ceil(table.getn(self.currentCheckGoods)/3)
		if useIndex > maxline then
			NGUITools.SetActive(obj, false)
			return
		end
		NGUITools.SetActive(obj, true)
		-- local t = getLuaComponent(obj)
		-- t:SetBadgeId(badgeId)
		local item1 = getLuaComponent(obj.transform:FindChild("item1"))
		local item2 = getLuaComponent(obj.transform:FindChild("item2"))
		local item3 = getLuaComponent(obj.transform:FindChild("item3"))
		local item1Index = useIndex*3+1
		local item2Index = useIndex*3+2
		local item3Index = useIndex*3+3
		-- print("index1:"..item1Index.."index2:"..item2Index.."index3:"..item3Index)
		local totalNum = table.getn(self.currentCheckGoods)
		if item1Index<=totalNum then
			item1.storeConfigData = self.currentCheckGoods[item1Index]
			item1:RefreshView()
			NGUITools.SetActive(item1.gameObject, true)
		else
			NGUITools.SetActive(item1.gameObject, false)
		end

		if item2Index<=totalNum then
			item2.storeConfigData = self.currentCheckGoods[item2Index]
			item2:RefreshView()
			NGUITools.SetActive(item2.gameObject, true)
		else
			NGUITools.SetActive(item2.gameObject, false)
		end

		if item3Index<=totalNum then
			item3.storeConfigData = self.currentCheckGoods[item3Index]
			item3:RefreshView()
			NGUITools.SetActive(item3.gameObject, true)
		else
			NGUITools.SetActive(item3.gameObject, false)
		end
	end
end

-----show all badge------
function BadgeStorePanel:OnShowAllBadge()
	return function(go)
		if self.currentStoreViewCategory == StoreTabType.TabAll then
			return
		end
		self.currentStoreViewCategory = StoreTabType.TabAll
		self:ShowStoreItemByCategory()
		-- print("show All Badge")
	end
end
-----show red badge------
function BadgeStorePanel:OnShowRedBadge()
	return function(go)
		if self.currentStoreViewCategory == StoreTabType.TabRed then
			return
		end
		self.currentStoreViewCategory = StoreTabType.TabRed
		self:ShowStoreItemByCategory()
		-- print("show Red badge")
	end
end
-----show blue badge------
function BadgeStorePanel:OnShowBlueBadge()
	return function(go)
		if self.currentStoreViewCategory == StoreTabType.TabBlue then
			return
		end
		self.currentStoreViewCategory = StoreTabType.TabBlue
		self:ShowStoreItemByCategory()
		-- print("show blue badge")
	end
end
-----show green badge------
function BadgeStorePanel:OnShowGreenBadge()
	return function(go)
		if self.currentStoreViewCategory == StoreTabType.TabGreen then
			return
		end
		self.currentStoreViewCategory = StoreTabType.TabGreen
		self:ShowStoreItemByCategory()
		-- print("show green badge")
	end
end
-----show golden badge------
function BadgeStorePanel:OnShowGoldenBadge()
	return function(go)
		if self.currentStoreViewCategory == StoreTabType.TabGolden then
			return
		end
		self.currentStoreViewCategory = StoreTabType.TabGolden
		self:ShowStoreItemByCategory()
		-- print("show golden badge")
	end
end

-------show panel------
function BadgeStorePanel:ShowPanel( ... )
	if self.allGoods == nil then
		self.allGoods = GameSystem.Instance.GoodsConfigData:GetGoodsDicByCategory(10)
	end
	self.currentStoreViewCategory = StoreTabType.TabAll
	self.currentStoreViewLevel = StoreGoodsLevel.LevelOne
	self.uiStoreTabAllBtn.value = true
	self.uiLevelOneTabBtn.value = true
	self:RefreshView()()
end

function BadgeStorePanel:GetGoodsByCatetory(category,level)
	local group = {}
	local enum = self.allGoods:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current.Value
		if (tonumber(v.sub_category) == enumToInt(category) or category == BadgeCategory.CG_ALL) and (v.quality == level) then
			table.insert(group,v)
		end
	end
	return group
end

function BadgeStorePanel:RefreshView( ... )
	return function()
		self:ShowStoreItemByCategory()
		self:RefreshBadgeNum()()
	end
end

function BadgeStorePanel:RefreshBadgeNum( ... )
	return function()
		self.uiBadgeToNumLabel.text = MainPlayer.Instance:GetGoodsCount(4024)
	end
end

function BadgeStorePanel:ShowStoreItemByCategory()
	if self.allGoods==nil then
		-- print("has no anthing badge goods")
		return
	end
	-- print("*(*********(("..self.currentStoreViewCategory)
	local tabType = self.currentStoreViewCategory
	local goodsGroup = nil
	if tabType == StoreTabType.TabAll then
		goodsGroup = self:GetGoodsByCatetory(BadgeCategory.CG_ALL,self.currentStoreViewLevel)
	elseif tabType ==StoreTabType.TabRed then
		goodsGroup = self:GetGoodsByCatetory(BadgeCategory.CG_RED,self.currentStoreViewLevel)
	elseif tabType ==StoreTabType.TabBlue then
		goodsGroup = self:GetGoodsByCatetory(BadgeCategory.CG_BLUE,self.currentStoreViewLevel)
	elseif tabType == StoreTabType.TabGreen then
		goodsGroup = self:GetGoodsByCatetory(BadgeCategory.CG_GREEN,self.currentStoreViewLevel)
	elseif tabType == StoreTabType.TabGolden then
		goodsGroup = self:GetGoodsByCatetory(BadgeCategory.CG_GLODEN,self.currentStoreViewLevel)
	end
	self.currentCheckGoods = goodsGroup
	-- local lenght = #goodsGroup
	-- print("*************"..#lenght)
	-- CommonFunction.ClearGridChild(self.uiStoreItemsContainer.transform)
	-- if goodsGroup and #goodsGroup>0 then
	-- 	local count = #goodsGroup
	-- 	for i=1,count do
	-- 		local storeGoodItem = getLuaComponent(createUI("BadgeStoreItem",self.uiStoreItemsContainer.transform))
	-- 		storeGoodItem.storeConfigData = goodsGroup[i]
	-- 		storeGoodItem.itemSelectHanlder = self:ItemClick()
	-- 	end
	-- 	self.uiStoreItemsContainer:Reposition()
	-- 	self.uiBadgesVScorllView:ResetPosition()
	-- endrr
	local listCount = table.getn(self.currentCheckGoods)
	print("listcount"..listCount)
	self.uiWrap.minIndex = ((listCount%3 == 0) and(-listCount/3+1) or (-math.ceil(listCount/3)+1))
	print(self.uiWrap.minIndex)
	self.uiWrap.maxIndex = 0
	local count = self.uiWrap.transform.childCount
	-- print("BadgeStoreItemGroup:"..childCountt)
	for i = 0, count - 1 do
		local go = self.uiWrap.transform:GetChild(i).gameObject
		go.name = tostring(i)
		self:UpdateStoreGroupItem()(go, i, i)
	end
	self.uiWrap:SortAlphabetically()
	self.uiBadgesVScorllView:ResetPosition()
end

function BadgeStorePanel:ItemClick( ... )
	return function(data)
		local window = createUI("BadgeComposeWindow")
		local t = getLuaComponent(window.gameObject)
		t.badgeConfigData = data
		t.refreshHanlder = self:RefreshView()
		UIManager.Instance:BringPanelForward(window)
	end
end

return BadgeStorePanel

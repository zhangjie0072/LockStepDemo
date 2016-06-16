--encoding=utf-8

UIGoodsList = {
	uiName = 'UIGoodsList',

	----------------------------------	
	goodsCategory = GoodsCategory.GC_NONE,
	tattooType = 0,

	--function (selectedGoods)
	onOKClick = nil,

	----------------------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UIGoodsList:Awake()
	local window = self.transform:FindChild("Window")

	local backNode = window:FindChild("BackButtonNode")
	getLuaComponent(createUI("ButtonBack", backNode)).onClick = self:MakeOnBackClick()
	self.btnOk = window:FindChild("ButtonNode").gameObject
	self.goOK = createUI("ButtonOK", window:FindChild("ButtonNode"))
	LuaHelper.GetComponentInChildren(self.goOK, "UILabel").text = getCommonStr("EQUIP")
	addOnClick(self.goOK, self:MakeOnOKClick())

	self.uiTitle = getComponentInChild(window, "GoodsList/Title", "UILabel")
	self.uiScroll = getComponentInChild(window, "GoodsList/Scroll View", "UIScrollView")
	self.uiGrid = getComponentInChild(self.uiScroll.transform, "Grid", "UIGrid")
	NGUITools.SetActive(self.btnOk,false)

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIGoodsList:FixedUpdate( ... )
	-- body
end

function UIGoodsList:OnClose( ... )
	TopPanelManager:HideTopPanel()
end

function UIGoodsList:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIGoodsList:Refresh()
	local goDetail = self.transform:FindChild("Window/Detail").gameObject
	local prefab
	if self.goodsCategory == GoodsCategory.GC_SKILL then
		prefab = ResourceLoadManager.Instance:LoadPrefab("Prefab/GUI/SkillListDetail", false)
	elseif self.goodsCategory == GoodsCategory.GC_EQUIPMENT then
		--prefab = ResourceLoadManager.Instance:LoadPrefab("Prefab/GUI/TattooListDetail", false)
		--todo: add cod here.
	else
		error("UIGoodsList: error goods category: " .. tostring(self.goodsCategory))
	end
	goDetail:AddComponent("WidgetPlaceholder").prefab = prefab
	self.uiDetail = getLuaComponent(WidgetPlaceholder.Replace(goDetail))
	NGUITools.SetActive(self.uiDetail.gameObject, false)

	--valid goods list
	local goodsList = {}
	if self.goodsCategory == GoodsCategory.GC_SKILL then
		self.uiTitle.text = getCommonStr("SKILL_REPLACE")
		goodsList = self:GetSkillList()
	elseif self.goodsCategory == GoodsCategory.GC_EQUIPMENT then
		--self.uiTitle.text = getCommonStr("LABEL_PACKAGE_TATOO_PACKAGE")
		--goodsList = self:GetTattooList()
	else
		error("UIGoodsList: error goodsCategory: " .. tostring(self.goodsCategory))
	end

	-- show goods list
	self.curItem = nil
	CommonFunction.ClearGridChild(self.uiGrid.transform)
	for i = 1, math.max(8, #goodsList) do
		local goodsItem = getLuaComponent(createUI("GoodsItem", self.uiGrid.transform))
		local goodsInfo = goodsList[i]
		if goodsInfo then
			goodsItem.goods = goodsInfo.goods
			goodsItem.isRecommanded = goodsInfo.isRecommanded
			goodsItem.onClick = self:MakeOnItemClick()
		end
	end
	self.uiGrid:Reposition()
	self.uiScroll:ResetPosition()

	NGUITools.SetActive(self.goOK, #goodsList > 0)
	NGUITools.SetActive(self.btnOk,false)
end


-----------------------------------------------------------------
function UIGoodsList:GetSkillList()
	local skillList = {}
	local enum = MainPlayer.Instance.SkillGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if  goods:GetCategory() == GoodsCategory.GC_SKILL and
			not goods:IsEquip() then
			local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(goods:GetID())
			if not skillAttr then
				error("UIGoodsList: can not find skill attr. ID:", goods:GetID())
			end
			if skillAttr.type ~= SkillType.PASSIVE then
				table.insert(skillList, {goods = goods, isRecommanded = false})
			end
		end
	end
	-- sort skill list
	table.sort(skillList, function (goods1, goods2)
		if goods1.goods:GetLevel() > goods2.goods:GetLevel() then
			return true
		elseif goods1.goods:GetLevel() < goods2.goods:GetLevel() then
			return false
		else
			if goods1.goods:GetID() < goods2.goods:GetID() then
				return true
			else
				return false
			end
		end
	end)
	return skillList
end

--[[
function UIGoodsList:GetTattooList()
	local tattooList = {}
	local enum = MainPlayer.Instance.TattooGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		--print(goods:GetName() .. " " .. tostring(goods:GetSubCategory()) .. " " .. tostring(self.tattooType))
		if  goods:GetCategory() == GoodsCategory.GC_TATTOO and
			goods:GetSubCategory() == self.tattooType and
			not goods:IsEquip() then
			local tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(goods:GetID(), goods:GetLevel())
			table.insert(tattooList, {goods = goods, isRecommanded = tattooConfig.positions:Contains(MainPlayer.Instance.Captain.m_position)})
		end
	end
	-- sort tattoo list
	table.sort(tattooList, function (goods1, goods2)
		if goods1.isRecommanded and not goods2.isRecommanded then
			return true
		elseif not goods1.isRecommanded and goods2.isRecommanded then
			return false
		else
			if enumToInt(goods1.goods:GetQuality()) > enumToInt(goods2.goods:GetQuality()) then
				return true
			elseif enumToInt(goods1.goods:GetQuality()) < enumToInt(goods2.goods:GetQuality()) then
				return false
			else
				if goods1.goods:GetLevel() > goods2.goods:GetLevel() then
					return true
				elseif goods1.goods:GetLevel() < goods2.goods:GetLevel() then
					return false
				else
					if goods1.goods:GetID() < goods2.goods:GetID() then
						return true
					else
						return false
					end
				end
			end
		end
	end)
	return tattooList
end
--]]

function UIGoodsList:MakeOnBackClick()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIGoodsList:MakeOnOKClick()
	return function ()
		if self.curItem then
			if self.onOKClick then self.onOKClick(self.curItem.goods) end
		end
	end
end

function UIGoodsList:MakeOnItemClick()
	return function (item)
		if self.curItem then self.curItem:set_selected(false) end
		self.curItem = item
		self.curItem:set_selected(true)
		NGUITools.SetActive(self.uiDetail.gameObject, true)
		self.uiDetail.ID = item.goods:GetID()
		self.uiDetail.level = item.goods:GetLevel()
		self.uiDetail:Refresh()
		NGUITools.SetActive(self.btnOk,true)
	end
end

return UIGoodsList

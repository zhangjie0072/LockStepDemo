TattooUpgradePopup = {
	uiName = 'TattooUpgradePopup',
	
	uuid = 0,

	onUpgrade = nil,
	onClose = nil,

	--accessible
	--consumables = { uuid = num, ... }
	--goods
}

function TattooUpgradePopup:Awake()
	self.uiAttrGrid1 = getComponentInChild(self.transform, "Top/Attr/Grid1", "UIGrid")
	self.uiAttrGrid2 = getComponentInChild(self.transform, "Top/Attr/Grid2", "UIGrid")
	self.uiProgress = getComponentInChild(self.transform, "Middle/Progress", "UIProgressBar")
	self.uiProgressLabel = getComponentInChild(self.transform, "Middle/Progress/Label", "UILabel")
	self.uiConsumNum = getComponentInChild(self.transform, "Middle/Consumables/Num", "UILabel")
	self.uiScroll = getComponentInChild(self.transform, "Bottom/Scroll", "UIScrollView")
	self.uiIconGrid = getComponentInChild(self.transform, "Bottom/Scroll/Grid", "UIGrid")
end

function TattooUpgradePopup:Start()

	bringFrontZOrder(self.transform)
	self.uiFrame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.uiFrame.showCorner = true
	self.uiFrame.onClose = self:MakeOnClose()
	self.uiFrame.buttonLabels = {getCommonStr("UPGRADE1"), getCommonStr("ONE_KEY_FILL")}
	self.uiFrame.buttonHandlers = {self:MakeOnUpgrade(), self:MakeOnFill()}
	self.uiFrame.title = getCommonStr("TATTOO_UPGRADE")

	self.uiIcon = getLuaComponent(getChildGameObject(self.transform, "Top/Icon"))
	self.goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, self.uuid)

	self:Clear()
end

function TattooUpgradePopup:Clear()
	self.fromLevel = self.goods:GetLevel()
	self.uiIcon.tattooID = self.goods:GetID()
	self.uiIcon.level = self.fromLevel

	self.toLevel = self.fromLevel
	self.fromAttr = GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), self.fromLevel)
	if not self.fromAttr then error("TattooUpgradePopup: error tattoo ID: " .. self.goods:GetID() .. " Level: " .. self.fromLevel) end

	self.curExp = self.goods:GetExp()
	self.consumNum = 0
	self.consumables = {}

	self:ListValidTattoo()

	self:Refresh()
end

function TattooUpgradePopup:Refresh()
	self.toAttr = GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), self.toLevel)
	if not self.toAttr then error("TattooUpgradePopup: error tattoo ID: " .. self.goods:GetID() .. " Level: " .. self.toLevel) end
	self.totalExp = self.toAttr.require_exp

	self.uiProgress.value = self.curExp /self.totalExp
	NGUITools.SetActive(self.uiProgress.foregroundWidget.gameObject, self.curExp > 0)
	self.uiProgressLabel.text = self.curExp .. '/' .. self.totalExp
	self.uiConsumNum.text = tostring(self.consumNum)
	self.uiIcon.level = self.toLevel
	self.uiIcon:Refresh()

	--Attr grid
	CommonFunction.ClearGridChild(self.uiAttrGrid1.transform)
	CommonFunction.ClearGridChild(self.uiAttrGrid2.transform)
	if self.toAttr == self.fromAttr then
		local enum = self.fromAttr.addn_attr:GetEnumerator()
		while enum:MoveNext() do
			local goItem = createUI("AttrListItem", self.uiAttrGrid1.transform)
			local item = getLuaComponent(goItem)
			item.attrID = enum.Current.Key
			item.value = enum.Current.Value
			item.showPlus = true
			local widget = goItem:GetComponent("UIWidget")
			widget.width = self.uiAttrGrid1.cellWidth
			widget.height = self.uiAttrGrid1.cellHeight
		end
		self.uiAttrGrid1:Reposition()
	else
		local enum = self.toAttr.addn_attr:GetEnumerator()
		while enum:MoveNext() do
			local prevValue = self.fromAttr.addn_attr:get_Item(enum.Current.Key)
			if not prevValue then prevValue = 0 end
			local goItem = createUI("AttrUpgradeListItem", self.uiAttrGrid2.transform)
			local item = getLuaComponent(goItem)
			item.attrID = enum.Current.Key
			item.prevValue = prevValue
			item.curValue = enum.Current.Value
			item.showPlus = true
			local widget = goItem:GetComponent("UIWidget")
			widget.width = self.uiAttrGrid2.cellWidth
			widget.height = self.uiAttrGrid2.cellHeight
		end
		self.uiAttrGrid2:Reposition()
	end
end

function TattooUpgradePopup:ListValidTattoo()
	--unequiped goods
	local goodsList = {}
	local enum = MainPlayer.Instance.TattooGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if not goods:IsEquip() and goods:GetUUID() ~= self.uuid then
			table.insert(goodsList, goods)
		end
	end
	--sort
	table.sort(goodsList, function (goods1, goods2)
		if goods1:GetSubCategory() == goods2:GetSubCategory() then
			return goods1:GetLevel() < goods2:GetLevel()
		else
			return goods1:GetSubCategory() == TattooType.TT_PIECE
		end
	end)
	--display
	CommonFunction.ClearGridChild(self.uiIconGrid.transform)
	self.tattooIcons = {}
	for _, goods in ipairs(goodsList) do
		local icon = getLuaComponent(createUI("TattooIcon", self.uiIconGrid.transform))
		icon.uuid = goods:GetUUID()
		icon.onClick = self:MakeOnTattooClick()
		icon.onReduce = self:MakeOnTattooReduce()
		table.insert(self.tattooIcons, icon)
	end
	self.uiIconGrid:Reposition()
end

function TattooUpgradePopup:MakeOnTattooClick()
	return function (tattooIcon)
		if tattooIcon.selectedNum < tattooIcon.totalNum then
			if self:CheckConsume(tattooIcon.tattooConfig) then	--可以消耗
				self:ConsumeTattooIcon(tattooIcon)
				self:Refresh()
			else
			    self:PromptTopLevel()
			end
		end
	end
end

function TattooUpgradePopup:MakeOnTattooReduce()
	return function (tattooIcon)
		if tattooIcon.selectedNum > 0 then
			tattooIcon:Select(-1)
			local goods = tattooIcon.goods
			self.consumables[goods:GetUUID()] = tattooIcon.selectedNum
			local curExp = self.realCurExp or self.curExp
			curExp = curExp - tattooIcon.tattooConfig.sacrifice_exp
			while curExp < 0 do
				self.toLevel = self.toLevel - 1
				local tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), self.toLevel)
				curExp = curExp + tattooConfig.require_exp
			end
			if curExp > self.totalExp then
				self.curExp = self.totalExp
				self.realCurExp = curExp
			else
				self.curExp = curExp
				self.realCurExp = nil
			end
			self.consumNum = self.consumNum - tattooIcon.tattooConfig.sacrifice_consume
			self:Refresh()
		end
	end
end

function TattooUpgradePopup:ConsumeTattooIcon(tattooIcon)
	self.curExp = self.curExp + tattooIcon.tattooConfig.sacrifice_exp
	while self.curExp >= self.totalExp do	--经验满
		if self:CheckLevel(self.toLevel + 1) then	--可升级
			self.curExp = self.curExp - self.totalExp
			self.toLevel = self.toLevel + 1
			self:Refresh()
		else	--不可升级
			self.realCurExp = self.curExp
			self.curExp = self.totalExp
			break
		end
	end
	tattooIcon:Select(1)
	self.consumables[tattooIcon.goods:GetUUID()] = tattooIcon.selectedNum
	self.consumNum = self.consumNum + tattooIcon.tattooConfig.sacrifice_consume
end

--是否可以消耗
function TattooUpgradePopup:CheckConsume(tattooConfig)
	return self:CheckLevel(self.toLevel + 1)
end

--是否可以升级到该等级
function TattooUpgradePopup:CheckLevel(level, silent)
	local tattooLevel = GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), level)
	if not tattooLevel then return false end

	--check level
	if tattooLevel.require_level > MainPlayer.Instance.Level then
		return false
	end

	return true
end

function TattooUpgradePopup:MakeOnClose()
	return function ()
		if self.onClose then self:onClose() end
		NGUITools.Destroy(self.gameObject)
	end
end

function TattooUpgradePopup:MakeOnUpgrade()
	return function ()
		local hasConsumable
		for id, num in pairs(self.consumables) do
			if num > 0 then
				hasConsumable = true
			end
		end
		if hasConsumable then
			if self.onUpgrade then self:onUpgrade() end
		else
		    CommonFunction.ShowTip(getCommonStr("STR_NOT_SELECTED_ITEM_FOR_TATTOO_UPGRADE"))
		end
	end
end


function TattooUpgradePopup:PromptTopLevel()
	CommonFunction.ShowTip(getCommonStr("STR_LEVEL_ALREADY_TOP"))
end

function TattooUpgradePopup:MakeOnFill()
	return function ()
		local filled
		for _, icon in ipairs(self.tattooIcons) do	--遍历消耗品
			local fillOver = false
			local selectedNum = icon.selectedNum
			for i = selectedNum + 1, icon.goods:GetNum() do		--遍历数量
				if self:CheckConsume(icon.tattooConfig) then
				    self:ConsumeTattooIcon(icon)
					filled = true
				else
				    fillOver = true
					break
				end
			end
			if fillOver then
			    break 
			end
		end
		if not filled then
			self:PromptTopLevel()
		else
			self:Refresh()
		end
	end
end

return TattooUpgradePopup

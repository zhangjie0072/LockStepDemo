SkillUpgradePopup = {
	uiName = 'SkillUpgradePopup',
	
	skillID = 0,
	level = 0,

	--function (skillUpgradePopup)
	--onUpgrade
}

function SkillUpgradePopup:Awake()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiAttrGrid = getComponentInChild(self.transform, "Attrs/Grid", "UIGrid")
	self.uiConsumableGrid = getComponentInChild(self.transform, "Consumables/Grid", "UIGrid")
	self.uiConsumableFrame = getComponentInChild(self.transform, "Consumables", "UIWidget")
end

function SkillUpgradePopup:Start()
	self.uiFrame.showCorner = true
	self.uiFrame.title = getCommonStr("SKILL_UPGRADE_TO"):format(self.level)
	self.uiFrame.onClose = self:MakeOnClose()
	self.uiFrame.buttonLabels = {getCommonStr("UPGRADE")}
	self.uiFrame.buttonHandlers = {self:MakeOnUpgrade()}
	local goIcon = createUI("SkillIconDetail", self.uiIconNode)
	self.uiIcon = getLuaComponent(goIcon)
	self.uiIcon.skillID = self.skillID
	self.uiIcon.level = self.level - 1
	self.uiIcon.showCast = true

	self.skillAttr = GameSystem.Instance.SkillConfig:GetSkill(self.skillID)
	if not self.skillAttr then error("SkillDetailPopup: error skill ID.") end
	self.levelData = self.skillAttr:GetSkillLevel(self.level)
	self.prevLevelData = self.skillAttr:GetSkillLevel(self.level - 1)
	if not self.levelData or not self.prevLevelData then 
		error("SkillDetailPopup: error skill level.")
	end

	--Attr grid
	local enum = self.levelData.additional_attrs:GetEnumerator()
	while enum:MoveNext() do
		local goItem = createUI("AttrUpgradeListItem", self.uiAttrGrid.transform)
		goItem:GetComponent("UIWidget").width = self.uiAttrGrid.cellWidth
		local item = getLuaComponent(goItem)
		item.attrSymbol = enum.Current.Key
		local prevValue = self.prevLevelData.additional_attrs:get_Item(enum.Current.Key)
		item.prevValue = prevValue
		item.curValue = enum.Current.Value
		item.showPlus = true
	end

	--Consumable grid
	local itemWidth
	enum = self.prevLevelData.consumables:GetEnumerator()
	while enum:MoveNext() do
		local consumable = enum.Current
		local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(consumable.consumable_id)
		if not goodsConfig then 
			print("SkillUpgradePopup: can not find goods config of consumable id:", consumable.consumable_id, "SkillID:", self.skillID) 
		end
		local goConsumable = createUI("GoodsIconConsume", self.uiConsumableGrid.transform)
		getLuaComponent(goConsumable):SetData(goodsConfig.id, consumable.consumable_quantity, false)

		itemWidth = goConsumable:GetComponent("UIWidget").width
	end
	--self.uiConsumableFrame.width = self.prevLevelData.consumables.Count * itemWidth + 16
end

function SkillUpgradePopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

function SkillUpgradePopup:MakeOnUpgrade()
	return function ()
		local enum = self.prevLevelData.consumables:GetEnumerator()
		while enum:MoveNext() do
			if enum.Current.consumable_id == GlobalConst.GOLD_ID then
				if enum.Current.consumable_quantity > MainPlayer.Instance.Gold then
					CommonFunction.ShowPopupMsg(getCommonStr("SKILL_UPGRADE_LACK_GOLD"),nil,nil,nil,nil,nil)
					playSound("UI/UI-wrong")
					return
				end
			elseif enum.Current.consumable_id == GlobalConst.DIAMOND_ID then
				if enum.Current.consumable_quantity > MainPlayer.Instance.DiamondFree then
					CommonFunction.ShowPopupMsg(getCommonStr("SKILL_UPGRADE_LACK_DIAMOND"),nil,nil,nil,nil,nil)
					playSound("UI/UI-wrong")
					return
				end
			else
				if enum.Current.consumable_quantity > MainPlayer.Instance:GetGoodsCount(enum.Current.consumable_id) then
					CommonFunction.ShowPopupMsg(getCommonStr("SKILL_UPGRADE_LACK_GOODS"),nil,nil,nil,nil,nil)
					playSound("UI/UI-wrong")
					return
				end
			end
		end
		if self.onUpgrade then self:onUpgrade() end
	end
end

return SkillUpgradePopup

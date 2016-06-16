TrainingUpgradePopup = {
	uiName = 'TrainingUpgradePopup',
	
	trainingID = 0,
	fromLevel = 0,

	onUpgrade = nil
}

function TrainingUpgradePopup:Awake()
	self.uiIcon = getComponentInChild(self.transform, "Top/Icon", "UISprite")
	self.uiName = getComponentInChild(self.transform, "Top/Name", "UILabel")
	self.uiMaxLevel = getComponentInChild(self.transform, "Top/MaxLevel", "UILabel")
	self.uiAttrGrid = getComponentInChild(self.transform, "Middle/Attr/Grid", "UIGrid")
	self.uiConsumeGrid = getComponentInChild(self.transform, "Middle/Consume/Consume/Grid", "UIGrid")
end

function TrainingUpgradePopup:Start()
	if not self.toLevel then self.toLevel = self.fromLevel + 1 end

	self.trainingConfig = GameSystem.Instance.TrainingConfig:GetTrainingConfig(self.trainingID)
	self.fromLevelConfig = GameSystem.Instance.TrainingConfig:GetTrainingLevelConfig(self.trainingID, self.fromLevel)
	self.toLevelConfig = GameSystem.Instance.TrainingConfig:GetTrainingLevelConfig(self.trainingID, self.toLevel)

	--frame
	self.uiFrame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.uiFrame.title = getCommonStr("TRAINING_UPGRADE_TO"):format(self.toLevel)
	self.uiFrame.onClose = function () NGUITools.Destroy(self.gameObject) end
	self.uiFrame.buttonLabels = {getCommonStr("UPGRADE")}
	self.uiFrame.buttonHandlers = {self:MakeOnUpgrade()}

	--info
	self.uiName.text = self.trainingConfig.name
	local enum = self.trainingConfig.lv_data:GetEnumerator()
	while enum:MoveNext() do end
	self.uiMaxLevel.text = getCommonStr("LEVEL_MAXLEVEL"):format(self.fromLevel, enum.Current.Key)

	--attr grid
	enum = self.toLevelConfig.addn_attr:GetEnumerator()
	while enum:MoveNext() do
		local item = getLuaComponent(createUI("AttrUpgradeListItem", self.uiAttrGrid.transform))
		local prevValue = self.fromLevelConfig.addn_attr:get_Item(enum.Current.Key)
		if not prevValue then prevValue = 0 end
		item.attrID = enum.Current.Key
		item.prevValue = prevValue
		item.curValue = enum.Current.Value
		widget = item.transform:GetComponent("UIWidget")
		widget.width = self.uiAttrGrid.cellWidth
		widget.height = self.uiAttrGrid.cellHeight
		if self.trainingID == 1008 and 8 <= enum.Current.Key and enum.Current.Key <= 14 then	--气势训练特殊处理
			item.attrName = getCommonStr("ANTI_DISTURB")
			break
		end
	end
	self.uiAttrGrid:Reposition()

	--consume grid
	enum = self.fromLevelConfig.normal_consume:GetEnumerator()
	while enum:MoveNext() do
		local item = createUI("GoodsIconConsume", self.uiConsumeGrid.transform)
		local goodsAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(enum.Current.Key)
		getComponentInChild(item.transform, "Icon", "UISprite").spriteName = goodsAttr.icon
		getComponentInChild(item.transform, "Num", "UILabel").text = tostring(enum.Current.Value)
	end
	self.uiConsumeGrid:Reposition()
end

function TrainingUpgradePopup:MakeOnUpgrade()
	return function ()
		if self.onUpgrade then self:onUpgrade() end
	end
end

return TrainingUpgradePopup

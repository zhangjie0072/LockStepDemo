TrainingDetailPopup = {
	uiName = 'TrainingDetailPopup',
	
	trainingID = 0,
	level = 0,
}

function TrainingDetailPopup:Awake()
	self.uiIcon = getComponentInChild(self.transform, "Top/Icon", "UISprite")
	self.uiName = getComponentInChild(self.transform, "Top/Name", "UILabel")
	self.uiMaxLevel = getComponentInChild(self.transform, "Top/MaxLevel", "UILabel")
	self.uiLevel = getComponentInChild(self.transform, "Middle/Level/Label", "UILabel")
	self.uiGrid = getComponentInChild(self.transform, "Middle/Attr/Grid", "UIGrid")
end

function TrainingDetailPopup:Start()
	self.trainingConfig = GameSystem.Instance.TrainingConfig:GetTrainingConfig(self.trainingID)
	self.trainingLevelConfig = GameSystem.Instance.TrainingConfig:GetTrainingLevelConfig(self.trainingID, self.level)

	self.uiFrame = getLuaComponent(getChildGameObject(self.transform, "Frame"))
	self.uiFrame.title = getCommonStr("TRAINING_DETAIL")
	self.uiFrame.onClose = function () NGUITools.Destroy(self.gameObject) end

	self.uiName.text = self.trainingConfig.name
	local enum = self.trainingConfig.lv_data:GetEnumerator()
	while enum:MoveNext() do end
	self.uiMaxLevel.text = getCommonStr("TRAINING_MAX_LEVEL"):format(enum.Current.Key)
	self.uiLevel.text = "lv." .. self.level

	enum = self.trainingLevelConfig.addn_attr:GetEnumerator()
	while enum:MoveNext() do
		local item = getLuaComponent(createUI("AttrListItem", self.uiGrid.transform))
		item.attrID = enum.Current.Key
		item.value = enum.Current.Value
		if self.trainingID == 1008 and 8 <= enum.Current.Key and enum.Current.Key <= 14 then	--气势训练特殊处理
			item.attrName = getCommonStr("ANTI_DISTURB")
			break
		end
	end
	self.uiGrid:Reposition()
end

return TrainingDetailPopup

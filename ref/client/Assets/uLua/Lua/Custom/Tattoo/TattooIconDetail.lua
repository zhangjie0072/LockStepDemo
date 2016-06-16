require "Custom/Tattoo/TattooUtil"

TattooIconDetail = {
	uiName = 'TattooIconDetail',
	
	tattooID = 0,
	level = 1,
}

function TattooIconDetail:Awake()
	local go = createUI("GoodsIcon", self.transform)
	self.goodsIcon = getLuaComponent(go)
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiType = getComponentInChild(self.transform, "Type", "UILabel")
	self.uiLevel = getComponentInChild(self.transform, "Level", "UILabel")
end

function TattooIconDetail:Start()
	self:Refresh()
end

function TattooIconDetail:Refresh()
	self.goodsIcon.hideLevel = true
	self.goodsIcon.goodsID = self.tattooID
	local goodsInfo = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.tattooID)
	local tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(self.tattooID, self.level)
	if not tattooConfig then error("TattooIconDetail: error tattoo ID: " .. self.tattooID .. " Level: " .. self.level) end
	self.uiName.text = tattooConfig.name
	local tattooType = TattooUtil.GetTattooTypeFromSubCategory(goodsInfo.sub_category)
	self.uiType.text = getCommonStr("STR_POSITION") .. TattooUtil.GetTattooTypeName(tattooType)
	self.maxLevel = self.level
	while GameSystem.Instance.TattooConfig:GetTattooConfig(self.tattooID, self.maxLevel + 1) do
		self.maxLevel = self.maxLevel + 1
	end
	self.uiLevel.text = getCommonStr("LEVEL_MAXLEVEL"):format(self.level, self.maxLevel)
end

return TattooIconDetail

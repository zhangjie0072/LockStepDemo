require "Custom/Tattoo/TattooUtil"

TattooListDetail = {
	uiName = 'TattooListDetail',
	
	ID = 0,
	level = 1,
}

function TattooListDetail:Awake()
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiType = getComponentInChild(self.transform, "Type", "UILabel")
	self.uiAttrGrid = getComponentInChild(self.transform, "AttrGrid", "UIGrid")
end

function TattooListDetail:Refresh()
	if not self.uiIcon then
		self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.uiIconNode))
		self.uiIcon.hideLevel = true
	end

	local tattooAttr = GameSystem.Instance.TattooConfig:GetTattooConfig(self.ID, self.level)
	if not tattooAttr then error("TattooListDetail: error tattoo ID:" .. self.ID .. " Level:" .. self.level) end

	self.uiName.text = tattooAttr.name
	self.uiIcon.goodsID = self.ID
	self.uiIcon.level = self.level
	local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.ID)
	local tattooType = TattooUtil.GetTattooTypeFromSubCategory(goodsConfig.sub_category)
	self.uiType.text = getCommonStr("STR_POSITION") .. TattooUtil.GetTattooTypeName(tattooType)

	CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
	local enum = tattooAttr.addn_attr:GetEnumerator()
	while enum:MoveNext() do
		local attrItem = getLuaComponent(createUI("AttrListItem", self.uiAttrGrid.transform))
		attrItem.attrID = enum.Current.Key
		attrItem.value = enum.Current.Value
		attrItem.showPlus = true
		local widget = attrItem.gameObject:GetComponent("UIWidget")
		widget.width = self.uiAttrGrid.cellWidth
	end
	self.uiAttrGrid:Reposition()
end

return TattooListDetail

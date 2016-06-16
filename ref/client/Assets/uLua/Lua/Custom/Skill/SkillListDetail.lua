require "Custom/Skill/SkillUtil"

SkillListDetail = {
	uiName = 'SkillListDetail',
	
	ID = 0,
	level = 1,
}

function SkillListDetail:Awake()
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiType = getComponentInChild(self.transform, "Type", "UILabel")
	self.uiCast = getComponentInChild(self.transform, "Cast", "UILabel")
	self.uiAttrGrid = getComponentInChild(self.transform, "AttrGrid", "UIGrid")
	self.uiCondGrid = getComponentInChild(self.transform, "CondGrid", "UIGrid")
end

function SkillListDetail:Refresh()
	if not self.uiIcon then
		self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.uiIconNode))
		self.uiIcon.hideLevel = false
	end

	local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(self.ID)
	if not skillAttr then error("SkillListDetail: error skill ID:" .. self.ID) end
	local skillLevel = skillAttr:GetSkillLevel(self.level)
	if not skillLevel then error("SkillListDetail: error skill level:" .. self.level) end

	self.uiName.text = skillAttr.name
	self.uiIcon.goodsID = self.ID
	self.uiIcon.level = self.level
	self.uiType.text = getCommonStr("STR_TYPE") .. SkillUtil.GetActionTypeName(skillAttr.action_type)
	self.uiCast.text = skillAttr.cast

	CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
	local enum = skillLevel.additional_attrs:GetEnumerator()
	while enum:MoveNext() do
		local attrItem = getLuaComponent(createUI("AttrListItem", self.uiAttrGrid.transform))
		local widget = attrItem.gameObject:GetComponent("UIWidget")
		widget.width = self.uiAttrGrid.cellWidth
		attrItem.attrSymbol = enum.Current.Key
		attrItem.value = enum.Current.Value
		attrItem.showPlus = true
	end
	self.uiAttrGrid:Reposition()

	local captainAttrs = MainPlayer.Instance.Captain.m_attrData.attrs
	CommonFunction.ClearGridChild(self.uiCondGrid.transform)
	enum = skillAttr.equip_conditions:GetEnumerator()
	while enum:MoveNext() do
		local attrItem = getLuaComponent(createUI("AttrListItem", self.uiCondGrid.transform))
		local widget = attrItem.gameObject:GetComponent("UIWidget")
		widget.width = self.uiCondGrid.cellWidth
		attrItem.attrID = enum.Current.Key
		attrItem.value = enum.Current.Value
		attrItem.showPlus = false
		local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(enum.Current.Key)
		local v = captainAttrs:get_Item(symbol)
		if v and v >= enum.Current.Value then
			attrItem.textColor = Color.white
		else
			attrItem.textColor = Color.red
		end
	end
	self.uiCondGrid:Reposition()
end

return SkillListDetail

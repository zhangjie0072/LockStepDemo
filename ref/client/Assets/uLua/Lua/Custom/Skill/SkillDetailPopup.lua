SkillDetailPopup = {
	uiName = 'SkillDetailPopup',
	
	skillID = 0,
	level = 0,
}

function SkillDetailPopup:Awake()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.tmCast = self.transform:FindChild("Cast")
	self.tmAttr = self.transform:FindChild("Attr")
	self.uiCast = getComponentInChild(self.transform, "Cast/Cast", "UILabel")
	self.uiGrid = getComponentInChild(self.transform, "Attr/Attrs/Grid", "UIGrid")
end

function SkillDetailPopup:Start()
	self.uiFrame.showCorner = false
	self.uiFrame.title = getCommonStr("SKILL_DETAIL")
	self.uiFrame.onClose = self:MakeOnClose()
	local goIcon = createUI("SkillIconDetail", self.uiIconNode)
	self.uiIcon = getLuaComponent(goIcon)
	self.uiIcon.skillID = self.skillID
	self.uiIcon.level = self.level
	self.uiIcon.showCast = false

	local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(self.skillID)
	if not skillAttr then error("SkillDetailPopup: error skill ID.") end

	if skillAttr.type == SkillType.PASSIVE then
		self.tmAttr.localPosition = self.tmCast.localPosition
		NGUITools.SetActive(self.tmCast.gameObject, false)
	else
		self.uiCast.text = skillAttr.cast
	end

	local levelData = skillAttr:GetSkillLevel(self.level)
	if not levelData then error("SkillDetailPopup: error skill level.") end
	local enum = levelData.additional_attrs:GetEnumerator()
	while enum:MoveNext() do
		local goItem = createUI("AttrListItem", self.uiGrid.transform)
		local item = getLuaComponent(goItem)
		item.attrSymbol = enum.Current.Key
		item.value = enum.Current.Value
		item.showPlus = true
	end
end

function SkillDetailPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return SkillDetailPopup

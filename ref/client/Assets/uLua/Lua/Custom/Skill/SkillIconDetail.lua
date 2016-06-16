require "Custom/Skill/SkillUtil"

SkillIconDetail = {
	uiName = 'SkillIconDetail',
	
	skillID = 0,
	level = nil,
	showCast = true,
}

function SkillIconDetail:Awake()
	local go = createUI("GoodsIcon", self.transform)
	self.goodsIcon = getLuaComponent(go)
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiType = getComponentInChild(self.transform, "Type", "UILabel")
	self.uiCast = getComponentInChild(self.transform, "Cast", "UILabel")
end

function SkillIconDetail:Start()
	self.goodsIcon.hideLevel = false
	self.goodsIcon.goodsID = self.skillID
	self.goodsIcon.level = self.level
	local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(self.skillID)
	if not skillAttr then error("SkillIconDetail: error skill ID.") end
	self.uiName.text = skillAttr.name
	--self.uiCast.text = skillAttr.cast
	local enum = skillAttr.levels:GetEnumerator()
	while enum:MoveNext() do end
	self.uiCast.text = getCommonStr("LEVEL_MAXLEVEL"):format(self.level, enum.Current.Key)
	if skillAttr.type == SkillType.PASSIVE then
		self.uiType.text = getCommonStr("STR_TYPE") .. getCommonStr("PASSIVE")
	else
		self.uiType.text = getCommonStr("STR_TYPE") .. SkillUtil.GetActionTypeName(skillAttr.action_type)
	end
	--NGUITools.SetActive(self.uiCast.gameObject, self.showCast)
end

return SkillIconDetail

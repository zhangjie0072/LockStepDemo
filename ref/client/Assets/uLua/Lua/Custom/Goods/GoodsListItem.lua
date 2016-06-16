require "Custom/Skill/SkillUtil"
require "Custom/Tattoo/TattooUtil"

GoodsListItem = {
	uiName = 'GoodsListItem',
	
	goods = nil,
	ID = 0,
	level = 0,

	-- function (item)
	onClick = nil,

	--accessible attribute
	--goodsInfo
	--skillAttr or tattooConfig
}

function GoodsListItem:Awake()
	self.uiBackgrounds = {}
	for i = 1, 4 do
		self.uiBackgrounds[i] = getComponentInChild(self.transform, "Background" .. i, "UISprite")
	end
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiTitle = getComponentInChild(self.transform, "TitleBar/Title", "UILabel")
	self.uiType = getComponentInChild(self.transform, "Type", "UILabel")

	addOnClick(self.gameObject, self:MakeOnClick())
end

function GoodsListItem:Start()
	if not self.goods and self.ID == 0 then
		return
	end

	local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.uiIconNode))
	addOnClick(goodsIcon.gameObject, self:MakeOnClick())
	if self.goods then
		goodsIcon.goods = self.goods
		self.ID = self.goods:GetID()
		self.level = self.goods:GetLevel()
	else
		goodsIcon.goodsID = self.ID
		goodsIcon.level = self.level
	end
	goodsIcon.hideLevel = true
	
	self.goodsInfo = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.ID)
	if not self.goodsInfo then error("GoodsListItem: error goods ID: " .. self.ID) end
	if self.goodsInfo.category == enumToInt(GoodsCategory.GC_SKILL) then
		self.skillAttr = GameSystem.Instance.SkillConfig:GetSkill(self.ID)
		if not self.skillAttr then error("GoodsListItem: error skill ID:" .. self.ID) end
		self.uiType.text = getCommonStr("STR_TYPE") .. SkillUtil.GetActionTypeName(self.skillAttr.action_type)
	-- elseif self.goodsInfo.category == enumToInt(GoodsCategory.GC_TATTOO) then
	-- 	self.tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(self.ID, self.level)
	-- 	if not self.tattooConfig then error("GoodsListItem: error tattoo ID:" .. self.ID) end
	-- 	local tattooType = TattooUtil.GetTattooTypeFromSubCategory(self.goodsInfo.sub_category)
	-- 	self.uiType.text = getCommonStr("STR_POSITION") .. TattooUtil.GetTattooTypeName(tattooType)
	end
	self.uiTitle.text = self.goodsInfo.name
end

function GoodsListItem:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function GoodsListItem:Select(selected)
	for i = 1, 4 do
		self.uiBackgrounds[i].spriteName = selected and self.SelectedBackground or self.NormalBackground
	end
end

function GoodsListItem:MakeOnClick()
	return function ()
		if self.onClick and self.goodsInfo then self:onClick() end
	end
end

return GoodsListItem

--encoding=utf-8

SkillItemTip = {

	uiName = 'SkillItemTip',

	----------------------------------
	
	id,
	goods,
	skillConfig,
	goodsConfig,
	AttrNameConfig,

	----------------------------------UI

	uiTipName,
	uiTipType,
	uiDescribe,
	uiAddAttrGrid,
	uiUseContent,

}

-----------------------------------------------------------------
function SkillItemTip:Awake()	
	self.uiTipName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiTipType = self.transform:FindChild('TypeM'):GetComponent('UILabel')
	self.uiDescribe = self.transform:FindChild('Describe'):GetComponent('UILabel')
	self.uiAddAttrGrid = self.transform:FindChild('AddAttr/AttrInfo3Grid'):GetComponent('UIGrid')
	self.uiUseContent = self.transform:FindChild('Operation'):GetComponent('UILabel')
end

function SkillItemTip:Start()
	self.skillConfig = GameSystem.Instance.SkillConfig
	self.goodsConfig = GameSystem.Instance.GoodsConfigData
	self.AttrNameConfig = GameSystem.Instance.AttrNameConfigData

	self:Refresh()
end

function SkillItemTip:FixedUpdate( ... )
	-- body
end

function SkillItemTip:OnClose( ... )
end

function SkillItemTip:OnDestroy()
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function SkillItemTip:Refresh()
	print('id = ', self.id)
	if self.goods and not self.id then
		print('goodsID = ', self.goods:GetID())
		self.id = self.goods:GetID()
	end

	local goodsAttr = self.goodsConfig:GetgoodsAttrConfig(self.id)
	local skillAttr = self.skillConfig:GetSkill(self.id)
	if goodsAttr then
		self.uiTipName.text = goodsAttr.name
		-- self.uiTipType.text = goodsAttr.sub_category
		self.uiDescribe.text = goodsAttr.intro
		self.uiUseContent.text = goodsAttr.purpose

		if skillAttr then
			local level
			if self.goods then
				level = self.goods:GetLevel()
			else
				level = 1
			end
			local skillType = skillAttr.action_type
			if skillType then
				self.uiTipType.text = getCommonStr(string.format('ACTION_TYPE_%d', skillType))
			end
			local skill = skillAttr:GetSkillLevel(level)
			-- local nextLevelSkill = skillAttr:GetSkillLevel(level + 1)
			local skillAddAttrs = skill.additional_attrs
			local enum = skillAddAttrs:GetEnumerator()
			while enum:MoveNext() do
				local skillAttrInfo = createUI('SkillAttrInfo', self.uiAddAttrGrid.transform)
				local attrInfo = getLuaComponent(skillAttrInfo.gameObject)
				attrInfo.hideAfterValue = true
				local name = self.AttrNameConfig:GetAttrName(enum.Current.Key)
				attrInfo:SetData(name, enum.Current.Value)
			end
		else
			error('skillAttr is null !')
		end
	else
		error('goodsAttr is null !')
	end
end

-----------------------------------------------------------------



return SkillItemTip
 
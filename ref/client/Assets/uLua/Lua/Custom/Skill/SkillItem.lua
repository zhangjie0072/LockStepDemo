--encoding=utf-8

SkillItem = {

	uiName = 'SkillItem',

	----------------------------------

	onClick,
	goodsConfig,
	-- roleConfig,
	skillConfig,
	skillManager, --技能管理器UISkillManager

	id,
	goods,
	roleID,
	position,
	skillType,
	skillAttr,
	isInWareHouse,
	isWear = false,
	isOwn = false,
	isSell = true,
	isCanLearn = false,
	limitLearnLevel = 0,

	----------------------------------UI
	uiSkillIcon,
	uiSkillName,
	uiSkillType,
	uiSkillVocation,
	uiMask,
	uiFinishMark,
	uiCanLearn,
	uiLearnLimit,
	uiLearnLimitLabel,

	uiDragDropItemEvent,
}

local positions ={getCommonStr('STR_ALL'),'PF','SF','C','PG','SG'}

-----------------------------------------------------------------
function SkillItem:Awake()
	self.uiSkillIcon = self.transform:FindChild('Icon')
	self.uiSkillName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiSkillType = self.transform:FindChild('TypeM'):GetComponent('UILabel')
	self.uiSkillVocation = self.transform:FindChild('VocationM'):GetComponent('UILabel')
	self.uiMask = self.transform:FindChild('Mask'):GetComponent('UISprite')
	self.uiFinishMark = self.transform:FindChild('Red'):GetComponent('UISprite')
	self.uiCanLearn = self.transform:FindChild('Title')
	self.uiLearnLimit = self.transform:FindChild('Line')
	self.uiLearnLimitLabel = self.transform:FindChild('Line/Name'):GetComponent('UILabel')

	self.goodsConfig = GameSystem.Instance.GoodsConfigData
	self.skillConfig = GameSystem.Instance.SkillConfig
	-- self.roleConfig = GameSystem.Instance.RoleBaseConfigData2
	-- self:Refresh()
end

function SkillItem:Start()
	addOnClick(self.gameObject, self:OnSkillClick())
	if self.uiSkillIcon.transform.childCount > 0 then
		addOnClick(self.uiSkillIcon.transform:GetChild(0).gameObject, self:OnSkillClick())
	end
	-- self.goodsConfig = GameSystem.Instance.GoodsConfigData
	-- self.skillConfig = GameSystem.Instance.SkillConfig
	-- -- self.roleConfig = GameSystem.Instance.RoleBaseConfigData2
	-- self:Refresh()
end

function SkillItem:FixedUpdate( ... )
	-- body
end

function SkillItem:OnClose( ... )
end

function SkillItem:OnDestroy()
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function SkillItem:Refresh()
	self.position = {}
	local goodsLua
	if self.uiSkillIcon.transform.childCount <= 0 then
		local goGoodsIcon = createUI('GoodsIcon', self.uiSkillIcon.transform)
		--添加拖动事件
		self.dragDropItemEvent = goGoodsIcon:AddComponent('UIDragDropItemEvent')
		self.dragDropItemEvent.OnDragDrop = LuaHelper.VoidDelegate(self:OnDragDrop())
		self.dragDropItemEvent.cloneOnDrag = true

		goodsLua = getLuaComponent(goGoodsIcon)
	else
		goodsLua = getLuaComponent(self.uiSkillIcon.transform:GetChild(0).gameObject)
		goodsLua.goods = self.goods
		goodsLua:Refresh()
	end
	goodsLua.goodsID = self.id
	goodsLua.hideNeed = true
	goodsLua.hideNum = true
	goodsLua.hideLevel = false
	goodsLua.showTips = false
	if self.goods then
		goodsLua.hideNum = false
		goodsLua.goods = self.goods
		-- goodsLua.num = 'lv:' .. self.goods:GetLevel()
	end

	local goodsAttr = self.goodsConfig:GetgoodsAttrConfig(self.id)
	self.uiSkillName.text = goodsAttr.name

	-- local skillAttr = self.skillConfig:GetSkill(self.id)
	if self.skillAttr then
		local skillType = self.skillAttr.action_type
		if skillType then
			self.uiSkillType.text = getCommonStr(string.format('ACTION_TYPE_%d', skillType))
		end
		local positionList = self.skillAttr.positions
		local enum = positionList:GetEnumerator()
		self.uiSkillVocation.text = ''
		while enum:MoveNext() do
			-- print('pos = ', enum.Current)
			self.position[positions[enum.Current + 1]] = true
			if self.uiSkillVocation.text == nil or self.uiSkillVocation.text == '' then
				self.uiSkillVocation.text = positions[enum.Current + 1]
			else
				self.uiSkillVocation.text = self.uiSkillVocation.text .. ' ' .. positions[enum.Current + 1]
			end
		end
		-- print('self.position = nil ?', self.position == nil)
	else
		error('skillAttr is null !')
	end
	if self.limitLearnLevel > 0 then
		self.uiLearnLimitLabel.text = string.format(getCommonStr("STR_SKILL_STUDY_LEVEL"), self.limitLearnLevel)
	end
	NGUITools.SetActive(self.uiCanLearn.gameObject, (self.isInWareHouse and not self:CheckIsOwn() and self:CheckIsCanLearn()))
	NGUITools.SetActive(self.uiLearnLimit.gameObject, (self.isInWareHouse and not self:CheckIsOwn() and not self:CheckIsCanLearn()))
	--NGUITools.SetActive(self.uiMask.gameObject, not self:CheckIsOwn())
	if self:CheckIsOwn() then 
		goodsLua.uiIcon.color = Color.New(1,1,1,1)
	else
		goodsLua.uiIcon.color = Color.New(0,1,1,1)
	end

	NGUITools.SetActive(self.uiFinishMark.gameObject, self:CheckIsWear())

	if self:CheckIsWear() or not self:CheckIsOwn() or self.isInWareHouse then
		self.dragDropItemEvent.enabled = false
	else
		self.dragDropItemEvent.enabled = true
	end
end


-----------------------------------------------------------------

-- function SkillItem:SetData()
	-- self.id = id

	-- -- self.position = self.roleConfig:GetPosition(id)
	-- local goodsAttr = self.goodsConfig:GetgoodsAttrConfig(id)
	-- self.uiSkillName.text = goodsAttr.name

	-- local skillAttr = self.skillConfig:GetSkill(id)
	-- if skillAttr then
	-- 	local skillType = skillAttr.action_type
	-- 	if skillType then
	-- 		self.uiSkillType.text = getCommonStr(string.format('ACTION_TYPE_%d', skillType))
	-- 	end
	-- else
	-- 	error('skillAttr is null !')
	-- end
-- end

function SkillItem:CheckIsOwn( ... )
	-- 检测玩家是否拥有此技能
	return self.isOwn
end

function SkillItem:CheckIsWear( ... )
	-- body
	return self.isWear
end

function SkillItem:CheckIsCanLearn( ... )
	return self.isCanLearn
end

function SkillItem:OnSkillClick( ... )
	return function ( go )
		if self.onClick then
			self.onClick(self.goods)
		end
	end
end

function SkillItem:OnDragDrop()
	return function(go)
		if go ~= nil and go.name == "Frame" then
			if self.skillManager ~= nil then
				self.skillManager:OnDragIconEnd(self)
			end
		end
	end
end

return SkillItem

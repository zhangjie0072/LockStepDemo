--encoding=utf-8

SkillDetail = {

	uiName = 'SkillDetail',

	----------------------------------

	id,
	goods,
	hideBottom = false,
	hideCost = false,
	isWear = false,
	isOwn = false,
	isImproveSkill = false,
	isSell = true,
	isFull = false,
	isInWareHouse,
	isCanLearn = false,
	limitLearnLevel = 0,
	onBuyClick,
	onImproveClick,
	onWearClick,
	skillConfig,
	goodsConfig,
	AttrNameConfig,
	storeConfig,
	constString =
	{
		'STR_SKILL_STUDY',
		'STR_CARRY',
		'STR_UPGRADE',
		'STR_PRICE',
		'CONSUME2',
		'STR_SKLL_MAX',
		'STR_ENOUGH_SKILL',
		'STR_FIELD_PROMPT29',
	},
	MAXLEVEL = 30,
	consumeIDs,
	onClose,

	----------------------------------UI
	uiBtnClose,
	uiSkillIcon,
	uiSkillName,
	uiSkillType,
	uiSkillDescribe,

	uiAddAttrGrid,
	uiUseContent,

	uiBottom,
	uiBtnBottom,
	uiBtnBottomLabel,
	uiBottomCost,
	uiGoodsIconConsume,
	uiBottomDescribe,
	uiAnimator,
}


-----------------------------------------------------------------
function SkillDetail:Awake()
	self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
	self.uiSkillIcon = self.transform:FindChild('Window/GoodsIcon')
	self.uiSkillName = self.transform:FindChild('Window/Name'):GetComponent('UILabel')
	self.uiSkillType = self.transform:FindChild('Window/TypeM'):GetComponent('UILabel')
	self.uiSkillDescribe = self.transform:FindChild('Window/Describe'):GetComponent('UILabel')

	self.uiAddAttrGrid = self.transform:FindChild('Window/AddAttr/AttrInfo3Grid'):GetComponent('UIGrid')
	self.uiUseContent = self.transform:FindChild('Window/Operation'):GetComponent('UILabel')

	self.uiBottom = self.transform:FindChild('Window/Bottom')
	self.uiBtnBottom = self.transform:FindChild('Window/Bottom/SellBtn'):GetComponent('UIButton')
	self.uiBtnBottomLabel = self.transform:FindChild('Window/Bottom/SellBtn/Label'):GetComponent('MultiLabel')
	self.uiBottomCost = self.transform:FindChild('Window/Bottom/Price'):GetComponent('UILabel')
	self.uiGoodsIconConsume = self.transform:FindChild('Window/Bottom/GoodsIconConsume')
	self.uiBottomDescribe = self.transform:FindChild('Window/BottomDescribe'):GetComponent('UILabel')

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function SkillDetail:Start()
	self.skillConfig = GameSystem.Instance.SkillConfig
	self.goodsConfig = GameSystem.Instance.GoodsConfigData
	self.AttrNameConfig = GameSystem.Instance.AttrNameConfigData
	self.storeConfig = GameSystem.Instance.StoreGoodsConfigData

	addOnClick(self.uiBtnClose.gameObject, self:OnCloseClick())
	addOnClick(self.uiBtnBottom.gameObject, self:OnOperateClick())

	self:Refresh()
end

function SkillDetail:FixedUpdate( ... )
	-- body
end

function SkillDetail:OnClose( ... )
	if self.onClose then
		self.onClose()
	end
	NGUITools.Destroy(self.gameObject)
end

function SkillDetail:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function SkillDetail:Refresh()
	if self.isOwn then
		if not self.isWear and self.isInWareHouse then
			self.uiBtnBottomLabel:SetText(getCommonStr(self.constString[3]))
		elseif not self.isWear and not self.isInWareHouse then
			self.hideCost = true
			self.uiBtnBottomLabel:SetText(getCommonStr(self.constString[2]))
		end
	else
		if self.isCanLearn then
			self.uiBtnBottomLabel:SetText(getCommonStr(self.constString[1]))
		elseif self.limitLearnLevel > 0 then
			self.hideBottom = true
		end
	end

	if not self.isSell or
		(self.goods and self.goods:GetLevel() >= self.MAXLEVEL and self.isInWareHouse) or
	 	(self.isFull and not self.isInWareHouse) then
		self.hideBottom = true
	end

	print('id = ', self.id)
	if self.goods and not self.id then
		print('goodsID = ', self.goods:GetID())
		self.id = self.goods:GetID()
	end

	local goodsIcon
	if self.uiSkillIcon.transform.childCount <= 0 then
		goodsIcon = createUI('GoodsIcon', self.uiSkillIcon.transform)
		local goodsLua = getLuaComponent(goodsIcon.gameObject)
		goodsLua.goodsID = self.id
		goodsLua.hideNeed = true
		goodsLua.hideNum = true
		goodsLua.hideLevel = false
		goodsLua.showTips = false
		if self.goods then
			goodsLua.goods = self.goods
		end
	else
		goodsIcon = self.uiSkillIcon.transform:GetChild(0).gameObject
		local goodsLua = getLuaComponent(goodsIcon.gameObject)
		goodsLua.goods = self.goods
		goodsLua:Refresh()
	end
	local goodsAttr = self.goodsConfig:GetgoodsAttrConfig(self.id)
	local skillAttr = self.skillConfig:GetSkill(self.id)
	if goodsAttr then
		self.uiSkillName.text = goodsAttr.name
		-- self.uiSkillType.text = goodsAttr.sub_category
		self.uiSkillDescribe.text = goodsAttr.intro
		self.uiUseContent.text = goodsAttr.purpose
		self.uiBottomDescribe.text = goodsAttr.access_way
	end
	local skill, nextLevelSkill
	if skillAttr then
		local level
		if self.goods then
			level = self.goods:GetLevel()
		else
			level = 1
		end
		if level >= self.MAXLEVEL then
			self.uiBottomDescribe.text = getCommonStr(self.constString[6])
		end
		if level <= self.MAXLEVEL then
			skill = skillAttr:GetSkillLevel(level)
			if self.goods and self.isInWareHouse then
				local nextLevel = level + 1
				if nextLevel <= self.MAXLEVEL then
					nextLevelSkill = skillAttr:GetSkillLevel(nextLevel)
				else
					nextLevelSkill = nil
					self.hideBottom = true
				end
			end
			print('skillLevel = ', level)
			CommonFunction.ClearGridChild(self.uiAddAttrGrid.transform)
			local skillAddAttrs = skill.additional_attrs
			local enum = skillAddAttrs:GetEnumerator()
			while enum:MoveNext() do
				local skillAttrInfo = createUI('SkillAttrInfo', self.uiAddAttrGrid.transform)
				local attrInfo = getLuaComponent(skillAttrInfo.gameObject)
				attrInfo.hideAfterValue = (nextLevelSkill == nil)
				attrInfo.isImproveSkill = self.isImproveSkill
				local name = self.AttrNameConfig:GetAttrName(enum.Current.Key)
				attrInfo:SetData(name, enum.Current.Value, nextLevelSkill)
			end
			self.uiAddAttrGrid.repositionNow = true
			self.uiAddAttrGrid:Reposition()
		end
	else
		error('skillAttr is null !')
	end

	CommonFunction.ClearChild(self.uiGoodsIconConsume.transform)
	self.consumeIDs = {}
	local storeGoodsData = self.storeConfig:GetStoreGoodsData(enumToInt(StoreType.ST_SKILL), self.id)
	if storeGoodsData and not self.isOwn and self.isSell then
		local costID = storeGoodsData.store_good_consume_type
		local costNum = storeGoodsData.store_good_price
		local goodsIconConsume = getLuaComponent(createUI('GoodsIconConsume', self.uiGoodsIconConsume.transform))
		goodsIconConsume.isAdd = false
		goodsIconConsume:SetData(costID, costNum)
		self.uiBottomCost.text = getCommonStr(self.constString[4])
		self.consumeIDs[costID] = costNum
	elseif skill and self.goods and self.goods:GetLevel() < self.MAXLEVEL then
		local enum = skill.consumables:GetEnumerator() --:get_Item(0)
		while enum:MoveNext() do
			local consume = enum.Current
			print('id = ', consume.consumable_id, ' value = ', consume.consumable_quantity)
			local goodsIconConsume = getLuaComponent(createUI('GoodsIconConsume', self.uiGoodsIconConsume.transform))
			goodsIconConsume.isAdd = false
			goodsIconConsume:SetData(consume.consumable_id, consume.consumable_quantity)
			self.consumeIDs[consume.consumable_id] = consume.consumable_quantity
		end
		self.uiBottomCost.text = getCommonStr(self.constString[5])
	end
	if not self.isOwn and not self.isCanLearn and self.limitLearnLevel > 0 then
		self.uiBottomDescribe.text = string.format(getCommonStr(self.constString[8]), self.limitLearnLevel)
	end
	if self.isFull and not self.isInWareHouse then
		self.uiBottomDescribe.text = getCommonStr(self.constString[7])
	end

	NGUITools.SetActive(self.uiBottomCost.gameObject, not self.hideCost)
	NGUITools.SetActive(self.uiGoodsIconConsume.gameObject, not self.hideCost)
	NGUITools.SetActive(self.uiBottom.gameObject, not self.hideBottom)
	NGUITools.SetActive(self.uiBottomDescribe.gameObject, self.hideBottom)
end


-----------------------------------------------------------------

function SkillDetail:DoClose( ... )
	self.uiBtnBottom.transform:GetComponent('BoxCollider').enabled = false
	-- if self.uiAnimator then
	-- 	self:AnimClose()
	-- else
		self:OnClose()
	-- end
end

function SkillDetail:SetData()

end

function SkillDetail:OnOperateClick( ... )
	return function (go)
		print('bottomBtn click')
		if self.onBuyClick then
			if not FunctionSwitchData.CheckSwith(FSID.skills_pay) then return end
			self.onBuyClick(self.id, self.consumeIDs)
		end
		if self.onImproveClick then
			if not FunctionSwitchData.CheckSwith(FSID.skills_upgrade) then return end
			self.onImproveClick(self.goods, self.consumeIDs)
		end
		if self.onWearClick then
			if not FunctionSwitchData.CheckSwith(FSID.skills_wear) then return end
			self.onWearClick(self.goods)
		end
	end
end

function SkillDetail:OnCloseClick( ... )
	return function (go)
		if go == self.uiBtnClose.gameObject then
			self:DoClose()
		end
	end
end

return SkillDetail

TipPopup = {
	uiName = 'TipPopup',
	---------------------
	id,
	goods,
	-------------------UI
	uiAttr = {},
}

local ResourceSubCategory = 
{
	'DIAMOND',
	'GOLD',
}

local PlayerPieceSubCategory = 
{
	'STR_ROLE_PRORITE',
}

local PlayerSubCategory = 
{
	'STR_MEMBER1',
	'STR_FAMOUS_PLAYER',
}

local EquipSubCategory = 
{
	"STR_EQUIP_HEAD",
	"STR_EQUIP_CLOTHES",
	"STR_EQUIP_HAND",
	"STR_EQUIP_PANTS",
	"STR_EQUIP_SHOES",
	"STR_EQUIP_PIEACE",
}

local ConsumeSubCategory = 
{
	'STR_GIFT', 
	'STR_TREASUREBOX',
	'STR_EXP',
	'STR_PREMIUM',
	'STR_SKILL_IMPROVE',
	'UPGRADE3',
	'STR_CLUTTER',
	'STR_PAINT',
}

local BadgeSubCategory = 
{
	'STR_BADGE_ATTACK',
	'STR_BADGE_DEFENSE',
	'STR_BADGE_SKILL',
	'STR_BADGE_TACTICS',
}

function TipPopup:Awake()
	self.nameLabel = self.transform:FindChild("LabelName"):GetComponent("UILabel")
	self.numLabel = self.transform:FindChild("LabelNum"):GetComponent("UILabel")
	self.typeLabel = self.transform:FindChild("LabelType"):GetComponent("UILabel")
	self.detailLabel = self.transform:FindChild("LabelTip"):GetComponent("UILabel")
	self.labelPrice = self.transform:FindChild("LabelPrice"):GetComponent("UILabel")
end

function TipPopup:Start()
	self.category = {
	"STR_ROLE_LINK",
	"STR_ROLE_SKILL",
	"STR_ROLE_PRORITE",
	"STR_ROLE_CONSUME",
	"STR_EQUIPMENT",
	"STR_ROLE_FASHION",
	"STR_ROLE_EXERCISE",
	"STR_ROLE_MATERIAL",
	"STR_MEMBER1",
	"STR_BADGE",
	}
	self:Refresh()
end

function TipPopup:Refresh()
	local goodsdata = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig( self.id)
	self.nameLabel.text = goodsdata.name
	if goodsdata.stack_num ~= 1 then
		self.numLabel.text = getCommonStr("STR_OWN_NUM")..tostring(MainPlayer.Instance:GetGoodsCount( self.id))
	else
		self.numLabel.gameObject:SetActive(false)
	end
	local category_id  = goodsdata.category
	print("----lyf--type:",self.category[ tonumber(category_id)])
	-- self.typeLabel.text = CommonFunction.GetConstString(self.category[ tonumber(category_id)])
	local subCategory = tonumber(goodsdata.sub_category)
	if tostring(GoodsCategory.IntToEnum(category_id))=='GC_EQUIPMENT' then
		if EquipSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(EquipSubCategory[subCategory])
		end
	elseif tostring(GoodsCategory.IntToEnum(category_id))=='GC_RESOURCE' then
		if ResourceSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(ResourceSubCategory[subCategory])
		end
	elseif tostring(GoodsCategory.IntToEnum(category_id))=='GC_BADGE' then
		if BadgeSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(BadgeSubCategory[subCategory])
		end
	elseif tostring(GoodsCategory.IntToEnum(category_id))=='GC_ROLE' then
		if PlayerSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(PlayerSubCategory[subCategory])
		end
	elseif tostring(GoodsCategory.IntToEnum(category_id))=='GC_FAVORITE' then
		if PlayerPieceSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(PlayerPieceSubCategory[subCategory])
		end
	elseif tostring(GoodsCategory.IntToEnum(category_id))=='GC_CONSUME' then
		if ConsumeSubCategory[subCategory] then
			self.typeLabel.text = self.typeLabel.text .. getCommonStr(ConsumeSubCategory[subCategory])
		end
	end
	-- NGUITools.SetActive(self.typeLabel.gameObject, tonumber(category_id) ~= 1)

	self.detailLabel.text = goodsdata.purpose
	if goodsdata.can_sell > 0 then
		--能出售
		self.labelPrice.text = CommonFunction.GetConstString("STR_SELL")..CommonFunction.GetConstString("STR_PRICE")
		local goods = getLuaComponent(createUI('GoodsIconConsume', self.labelPrice.transform))
		local pos = goods.gameObject.transform.localPosition
		pos.x = pos.x + 50
		goods.gameObject.transform.localPosition = pos
		goods.rewardId = 2
		goods.rewardNum = goodsdata.sell_price
		goods.gameObject.transform:FindChild("Num"):GetComponent("UILabel").color = Color.New(255,255,255)
		goods.isAdd = false
	else
		--不显示出售
		--NGUITools.SetActive(self.labelPrice.gameObject, false)
		self.labelPrice.text = CommonFunction.GetConstString("STR_NOT_SELL")
	end

	self:ShowEquipDetail(goodsdata.category, goodsdata.sub_category)
	self:ShowBadgeDetail(goodsdata)
	addOnClick(self.gameObject,self:OnClick())
end

function TipPopup:ShowBadgeDetail(goodsdata)
	local num = 1
	local category = goodsdata.category
	if tostring(GoodsCategory.IntToEnum(category))=='GC_BADGE' then
		for i=1,4 do
			self.uiAttr[i] = self.transform:FindChild('Attr' .. tostring(i))
			NGUITools.SetActive(self.uiAttr[i].gameObject, false)
		end
		local badgeAttrConfigData = GameSystem.Instance.BadgeAttrConfigData:GetBaseConfig(goodsdata.id)
		if badgeAttrConfigData then
			local addAttrlist = badgeAttrConfigData.addAttr
			local enum = addAttrlist:GetEnumerator()
			while enum:MoveNext() do
				local attrId = enum.Current.Key
				local attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(attrId)
				local attrNum = enum.Current.Value
				local go = self.uiAttr[num].gameObject
				NGUITools.SetActive(go, true)
				local attr = getLuaComponent(go)
				attr:SetData(attrName, attrNum)
				attr.transform:FindChild("Name"):GetComponent("UILabel").color = Color.New(0,234/255,255/255)
				attr.transform:FindChild("Value"):GetComponent("UILabel").color = Color.New(16/255,226/255,0)
				num = num + 1
			end
		end
	end
end

function TipPopup:ShowEquipDetail(category, sub_category)
	local num = 1
	if not self.goods then
		if tostring(GoodsCategory.IntToEnum(category))=='GC_EQUIPMENT' and
			(EquipmentType.IntToEnum(tonumber(sub_category)) ~= EquipmentType.ET_EQUIPMENTPIECE) then
			for i=1,4 do
				self.uiAttr[i] = self.transform:FindChild('Attr' .. tostring(i))
				NGUITools.SetActive(self.uiAttr[i].gameObject, false)
			end
			local attrNameConfig = GameSystem.Instance.AttrNameConfigData
			local equipmentConfig = GameSystem.Instance.EquipmentConfigData
			local itemConfig = equipmentConfig:GetBaseConfig(self.id, 1)
			if itemConfig then
				local enum = itemConfig.addn_attr:GetEnumerator()
				while enum:MoveNext() do
					local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
					local name = attrNameConfig:GetAttrName(symbol)
					local go = self.uiAttr[num].gameObject
					NGUITools.SetActive(go, true)
					local attr = getLuaComponent(go)
					attr:SetData(name, enum.Current.Value)
					num = num + 1
				end
			end
		end
	else
		if tostring(self.goods:GetCategory()) =='GC_EQUIPMENT' and
			self.goods:GetSubCategory() ~= EquipmentType.ET_EQUIPMENTPIECE then
			for i=1,4 do
				self.uiAttr[i] = self.transform:FindChild('Attr' .. tostring(i))
				NGUITools.SetActive(self.uiAttr[i].gameObject, false)
			end
			local attrNameConfig = GameSystem.Instance.AttrNameConfigData
			local equipmentConfig = GameSystem.Instance.EquipmentConfigData
			local itemConfig = equipmentConfig:GetBaseConfig(self.id, self.goods:GetLevel())
			if itemConfig then
				local enum = itemConfig.addn_attr:GetEnumerator()
				while enum:MoveNext() do
					local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
					local name = attrNameConfig:GetAttrName(symbol)
					local go = self.uiAttr[num].gameObject
					NGUITools.SetActive(go, true)
					local attr = getLuaComponent(go)
					attr:SetData(name, enum.Current.Value)
					num = num + 1
				end
			end
		end
	end
end

function TipPopup:OnClick()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return TipPopup

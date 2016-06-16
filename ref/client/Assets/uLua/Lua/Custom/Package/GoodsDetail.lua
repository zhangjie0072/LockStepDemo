GoodsDetail =  {
	uiName = 'GoodsDetail',
	-----------------------
	inStore = false,
	---------------------UI
	uiGoodsName,
	uiGoodsCategory,
	uiGoodsIcon,
	uiGoodsDetail,
	uiAttrGrid,
	uiBackType,
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

function GoodsDetail:Awake()
	self.uiGoodsName = self.transform:FindChild('Name'):GetComponent('MultiLabel')
	self.uiGoodsCategory = self.transform:FindChild('Category'):GetComponent('UILabel')
	self.uiGoodsIcon = self.transform:FindChild('GoodsIcon')
	self.uiGoodsDetail = self.transform:FindChild('Detail'):GetComponent('UILabel')
	self.uiAttrGrid = self.transform:FindChild('Attr/Grid'):GetComponent('UIGrid')
	self.uiBackType = self.transform:FindChild('BackType'):GetComponent('UISprite')
end

function GoodsDetail:Start()
end

function GoodsDetail:Refresh()

end

function GoodsDetail:SetData(goodsArg, goodsid, goodsnum)
	if goodsArg then
		local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsArg:GetID())
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_CONSUME' then
			local subCategory = tonumber(goods.sub_category)
			if not ConsumeSubCategory[subCategory] or self.inStore then
				self.uiGoodsCategory.text = getCommonStr('STR_ROLE_CONSUME')
			else
				self.uiGoodsCategory.text = getCommonStr(ConsumeSubCategory[subCategory])
			end
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_FAVORITE' then 
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_PRORITE')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_MATERIAL' then
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_MATERIAL')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_SKILL' then
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_SKILL')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then
			self.uiGoodsCategory.text = getCommonStr('STR_EQUIPMENT')
		end

		local wColors = string.split(self.nameColorW, ",")
		local gColors = string.split(self.nameColorG, ",")
		local bColors = string.split(self.nameColorB, ",")
		local pColors = string.split(self.nameColorP, ",")
		local oColors = string.split(self.nameColorO, ",")
		local nameColors = {
			Color.New(wColors[1]/255, wColors[2]/255, wColors[3]/255, wColors[4]/1),
			Color.New(gColors[1]/255, gColors[2]/255, gColors[3]/255, gColors[4]/1),
			Color.New(bColors[1]/255, bColors[2]/255, bColors[3]/255, bColors[4]/1),
			Color.New(pColors[1]/255, pColors[2]/255, pColors[3]/255, pColors[4]/1),
			Color.New(oColors[1]/255, oColors[2]/255, oColors[3]/255, oColors[4]/1)
		}
		--self.uiGoodsName:SetColor(nameColors[goods.quality])
		self.uiGoodsName:SetText(goods.name)
		self.uiGoodsDetail.text = goods.intro

		while self.uiGoodsIcon.transform.childCount > 0 do
			NGUITools.Destroy(self.uiGoodsIcon.transform:GetChild(0).gameObject)
		end
		local goodsicon = getLuaComponent(createUI('GoodsIcon', self.uiGoodsIcon))
		goodsicon.goods = goodsArg
		goodsicon.goodsID = goodsArg:GetID()
		goodsicon.hideLevel = true
		goodsicon.hideNum = false
		goodsicon.hideNeed = true
		goodsicon.num = goodsArg:GetNum()
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then
			goodsicon.hideNum = true
		end

		if self.uiAttrGrid then
			CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
		end
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then 
			local attrNameConfig = GameSystem.Instance.AttrNameConfigData
			local equipmentConfig = GameSystem.Instance.EquipmentConfigData
			local itemConfig = equipmentConfig:GetBaseConfig(goodsArg:GetID(), goodsArg:GetLevel())
			if itemConfig then
				local enum = itemConfig.addn_attr:GetEnumerator()
				while enum:MoveNext() do
					local attrItemObj = createUI('RoleAttrItem2', self.uiAttrGrid.transform)
					local script = getLuaComponent(attrItemObj)
					local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
					local name = attrNameConfig:GetAttrName(symbol)
					script:SetData(name, enum.Current.Value)
				end
			else
				print('error -- can not get configuration by goodsID: ', goodsID, ' and level: ', goodsLevel)
			end
			self.uiAttrGrid.repositionNow = true
		end

		NGUITools.SetActive(self.uiBackType.gameObject, tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT')
	else
		local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsid)
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_CONSUME' then
			local subCategory = tonumber(goods.sub_category)
			if not ConsumeSubCategory[subCategory] or self.inStore then
				self.uiGoodsCategory.text = getCommonStr('STR_ROLE_CONSUME')
			else
				self.uiGoodsCategory.text = getCommonStr(ConsumeSubCategory[subCategory])
			end
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_FAVORITE' then 
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_PRORITE')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_MATERIAL' then
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_MATERIAL')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_SKILL' then
			self.uiGoodsCategory.text = getCommonStr('STR_ROLE_SKILL')
		elseif tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then
			self.uiGoodsCategory.text = getCommonStr('STR_EQUIPMENT')
		end

		local wColors = string.split(self.nameColorW, ",")
		local gColors = string.split(self.nameColorG, ",")
		local bColors = string.split(self.nameColorB, ",")
		local pColors = string.split(self.nameColorP, ",")
		local oColors = string.split(self.nameColorO, ",")
		local nameColors = {
			Color.New(wColors[1]/255, wColors[2]/255, wColors[3]/255, wColors[4]/1),
			Color.New(gColors[1]/255, gColors[2]/255, gColors[3]/255, gColors[4]/1),
			Color.New(bColors[1]/255, bColors[2]/255, bColors[3]/255, bColors[4]/1),
			Color.New(pColors[1]/255, pColors[2]/255, pColors[3]/255, pColors[4]/1),
			Color.New(oColors[1]/255, oColors[2]/255, oColors[3]/255, oColors[4]/1)
		}
		self.uiGoodsName:SetColor(nameColors[goods.quality])
		self.uiGoodsName:SetText(goods.name)
		self.uiGoodsDetail.text = goods.intro

		while self.uiGoodsIcon.transform.childCount > 0 do
			NGUITools.Destroy(self.uiGoodsIcon.transform:GetChild(0).gameObject)
		end
		local goodsicon = getLuaComponent(createUI('GoodsIcon', self.uiGoodsIcon))
		goodsicon.goodsID = goodsid
		goodsicon.hideLevel = true
		goodsicon.hideNum = false
		goodsicon.hideNeed = true
		goodsicon.num = goodsnum
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then
			goodsicon.hideNum = true
		end

		if self.uiAttrGrid then
			CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
		end
		if tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT' then
			local attrNameConfig = GameSystem.Instance.AttrNameConfigData
			local equipmentConfig = GameSystem.Instance.EquipmentConfigData
			local itemConfig = equipmentConfig:GetBaseConfig(goodsicon.goodsID, 1)
			if itemConfig then
				local enum = itemConfig.addn_attr:GetEnumerator()
				while enum:MoveNext() do
					local attrItemObj = createUI('RoleAttrItem2', self.uiAttrGrid.transform)
					local script = getLuaComponent(attrItemObj)
					local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
					local name = attrNameConfig:GetAttrName(symbol)
					script:SetData(name, enum.Current.Value)
				end
			else
				print('error -- can not get configuration by goodsID: ', goodsID, ' and level: ', goodsLevel)
			end
			self.uiAttrGrid.repositionNow = true
		end
		NGUITools.SetActive(self.uiBackType.gameObject, tostring(GoodsCategory.IntToEnum(goods.category))=='GC_EQUIPMENT')
	end
end

return GoodsDetail

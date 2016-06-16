--encoding=utf-8

FashionShopItem =  {
	uiName="FashionShopItem",

	----------------------------------
	onClick,
	uuid,
	id,
	isInStore,
	isTryDress = false,
	belongId = 0,
	cost = 0,
	gender,
	isReputation = false,
	goods,
	--钻石:0 声望:1
	realCostType,

	----------------------------------UI
	uiItemName,
	uiDiscount,
	uiDiscountLabel,
	uiGender,
	uiVip,
	uiNew,
	uiNowCost,
	uiCost,
	uiPreCost,
	uiPreLine,
	uiIcon,
	uiIconFrame,
	uiSele,
	uiOwner,
	uiFitting,
	uiAttrGrid,


}


-----------------------------------------------------------------
function FashionShopItem:Awake()
	self.uiItemName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiDiscount = self.transform:FindChild('Discount'):GetComponent('UISprite')
	self.uiDiscountLabel = self.transform:FindChild('Discount/Label'):GetComponent('UILabel')
	self.uiGender = self.transform:FindChild('Gender'):GetComponent('UISprite')
	self.uiVip = self.transform:FindChild('VIP')
	self.uiNew = self.transform:FindChild('New')
	self.uiCost = self.transform:FindChild('Cost'):GetComponent('UISprite')
	self.uiNowCost = self.transform:FindChild('Cost/NowCost'):GetComponent('UILabel')
	self.uiPreCost = self.transform:FindChild('Cost/Discount'):GetComponent('UILabel')
	self.uiPreLine = self.transform:FindChild('Cost/Discount/Line')
	self.uiIcon = self.transform:FindChild('Icon'):GetComponent('UISprite')
	self.uiIconFrame = self.transform:FindChild('Icon/Icon'):GetComponent('UISprite')
	self.uiSele = self.transform:FindChild('Sele'):GetComponent('UISprite')
	self.uiOwner = self.transform:FindChild('Owner'):GetComponent('UILabel')
	self.uiFitting = self.transform:FindChild('Fitting'):GetComponent('UISprite')
	self.uiAttrGrid = self.transform:FindChild('AttrGrid'):GetComponent('UIGrid')
end

function FashionShopItem:Start()
	NGUITools.SetActive(self.uiSele.gameObject, false)

	addOnClick(self.gameObject, self:ClickItem())
end

function  FashionShopItem:FixedUpdate()
end

function FashionShopItem:OnClose()
end

function FashionShopItem:OnDestroy()
	-- body

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function FashionShopItem:Refresh()
end


-----------------------------------------------------------------
function FashionShopItem:SetData(id)
	self.id = id

	local fashionShopConfig
	if not self.isReputation then
		fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(id)
	else
		fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(id)
	end
	self.uiItemName.text = fashionShopConfig:getName()
	local atlas = ResourceLoadManager.Instance:GetAtlas(fashionShopConfig:getAtlas())
	self.uiIcon.atlas = atlas
	self.uiIcon.spriteName = fashionShopConfig:getSpriteName()

	self:SetIconFrame(id)

	if fashionShopConfig._isDiscount == 0 then
		local pos = self.uiPreCost.transform.localPosition
		pos.y = 0
		self.uiPreCost.transform.localPosition = pos
	else
		local enumDis = fashionShopConfig._discountCost:GetEnumerator()
		while enumDis:MoveNext() do
			self.uiNowCost.text = enumDis.Current
			self.cost = enumDis.Current
			break
		end
		self.uiDiscountLabel.text = fashionShopConfig._isDiscount .. '折'
	end

	local enumType = fashionShopConfig._costType:GetEnumerator()
	while enumType:MoveNext() do
		local costType = enumType.Current
		if costType == 1 then
			self.uiCost.spriteName = 'com_property_diamond'
			self.realCostType = 0
		elseif costType == 2 then
			self.uiCost.spriteName = 'com_property_gold'
		elseif costType == 10 then
			self.uiCost.spriteName = 'com_property_reputation'
			self.realCostType = 1
		end
		break
	end
	local enumNum = fashionShopConfig._costNum:GetEnumerator()
	while enumNum:MoveNext() do
		self.uiPreCost.text = enumNum.Current
		if self.cost == 0 then self.cost = enumNum.Current end
		break
	end

	NGUITools.SetActive(self.uiVip.gameObject, fashionShopConfig._vip > 0)
	NGUITools.SetActive(self.uiDiscount.gameObject, fashionShopConfig._isDiscount ~= 0)
	NGUITools.SetActive(self.uiNowCost.gameObject, fashionShopConfig._isDiscount ~= 0)
	NGUITools.SetActive(self.uiPreLine.gameObject, fashionShopConfig._isDiscount ~= 0)
	NGUITools.SetActive(self.uiNew.gameObject, fashionShopConfig._isNew == 1)
end

-- 非腾讯版本的
-- function FashionShopItem:SetWardrobeData(id)
-- 	self.id = id
-- 	local ownGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, self.uuid)
-- 	self.uiItemName.text = ownGoods:GetName()
-- 	local atlas = ResourceLoadManager.Instance:GetAtlas(ownGoods:GetFashionAtlas(id))
-- 	self.uiIcon.atlas = atlas
-- 	self.uiIcon.spriteName = ownGoods:GetIcon()
-- 	self.gender = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).gender
-- 	if self.gender == 1 then
-- 		NGUITools.SetActive(self.uiDiscount.gameObject, true)
-- 		NGUITools.SetActive(self.uiDiscountLabel.gameObject, false)
-- 		self.uiDiscount.spriteName = 'fashion_boy'
-- 		-- self.uiOwner.text = self.uiOwner.text .. '（男）'
-- 	elseif self.gender == 2 then
-- 		NGUITools.SetActive(self.uiDiscount.gameObject, true)
-- 		NGUITools.SetActive(self.uiDiscountLabel.gameObject, false)
-- 		self.uiDiscount.spriteName = 'fashion_girl'
-- 		-- self.uiOwner.text = self.uiOwner.text .. '（女）'
-- 	else
-- 		NGUITools.SetActive(self.uiDiscount.gameObject, false)
-- 	end

-- 	local roles = MainPlayer.Instance.PlayerList
-- 	local enum = roles:GetEnumerator()
-- 	while enum:MoveNext() do
-- 		local role = enum.Current.m_roleInfo
-- 		local fashionInfo = role.fashion_slot_info
-- 		local fashion = fashionInfo:GetEnumerator()
-- 		while fashion:MoveNext() do
-- 			local fashionuuId = fashion.Current.fashion_uuid
-- 			if fashionuuId ~= 0 then
-- 				local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, fashionuuId)
-- 				if self.uuid == goods:GetUUID() then
-- 					self.uiOwner.text = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(role.id).name
-- 					self.uiOwner.color = Color.New(247/255,191/255,29/255,1)
-- 					self.belongId = role.id
-- 					break
-- 				end
-- 			end
-- 		end
-- 	end
-- 	NGUITools.SetActive(self.uiOwner.gameObject, true)
-- 	NGUITools.SetActive(self.uiVip.gameObject, false)
-- 	-- NGUITools.SetActive(self.uiDiscount.gameObject, false)
-- 	NGUITools.SetActive(self.uiCost.gameObject, false)
-- 	NGUITools.SetActive(self.uiNew.gameObject, false)
-- end

-- 腾讯版本的
function FashionShopItem:SetWardrobeDataNew(goods)
	NGUITools.SetActive(self.uiOwner.gameObject, true)
	NGUITools.SetActive(self.uiVip.gameObject, false)
	NGUITools.SetActive(self.uiDiscount.gameObject, false)
	NGUITools.SetActive(self.uiCost.gameObject, false)
	NGUITools.SetActive(self.uiNew.gameObject, false)

	self.id = goods:GetID()
	self.uuid = goods:GetUUID()
	-- local ownGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, self.uuid)
	self.uiItemName.text = goods:GetName()
	local atlas = ResourceLoadManager.Instance:GetAtlas(goods:GetFashionAtlas(self.id))
	self.uiIcon.atlas = atlas
	self.uiIcon.spriteName = goods:GetIcon()

	self:SetIconFrame(self.id)
	self.gender = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id).gender
	if self.gender ~= 0 then
		NGUITools.SetActive(self.uiGender.gameObject, true)
	end
	if self.gender == 1 then
		self.uiGender.spriteName = 'fashion_boy'
	elseif self.gender == 2 then
		self.uiGender.spriteName = 'fashion_girl'
	end

	self.goods = goods
	self:ResetFashionShopItem()

	local roles = MainPlayer.Instance.PlayerList
	local enum = roles:GetEnumerator()
	while enum:MoveNext() do
		local role = enum.Current.m_roleInfo
		local fashionInfo = role.fashion_slot_info
		local fashion = fashionInfo:GetEnumerator()
		while fashion:MoveNext() do
			local fashionuuId = fashion.Current.fashion_uuid
			if fashionuuId ~= 0 then
				local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, fashionuuId)
				if self.uuid == goods:GetUUID() then
					self.uiOwner.text = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(role.id).name
					self.uiOwner.color = Color.New(247/255,191/255,29/255,1)
					self.belongId = role.id
					return
				end
			end
		end
	end
end

function FashionShopItem:SetIconFrame(id)
	local frameColor = {"white","green","blue","purple","orange"}
	local GoodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	self.uiIconFrame.spriteName = "com_card_frame_"..frameColor[GoodsConfig.quality]
end

function FashionShopItem:ResetFashionShopItem()
	if not self.goods then
		error('FashionShopItem.lua: No Goods!!!')
		return
	end

	CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
	local attrIDList = self.goods:GetFashionAttrIDList()
	local enum = attrIDList:GetEnumerator()
	while enum:MoveNext() do
		local attr_id = enum.Current
		print('attr_id = ', attr_id)
		if attr_id ~= 0 then
			local fashionAttr = GameSystem.Instance.FashionConfig:GetFashionAttr(attr_id)
			-- print('fashionAttr.player_attr_id = ', fashionAttr.player_attr_id)
			local name = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(fashionAttr.player_attr_id)
			local num = fashionAttr.player_attr_num
			local attrInfo =  getLuaComponent(createUI("AttrInfo1", self.uiAttrGrid.transform))
			attrInfo:SetName(name)
			attrInfo:SetValue(num)
		end
	end

	self.uiAttrGrid.repositionNow = true
	self.uiAttrGrid:Reposition()
end

function FashionShopItem:ClickItem( ... )
	return function (go)
		if self.onClick then
			self.onClick(self)
		end
	end
end

function FashionShopItem:ChangeState(state)
	NGUITools.SetActive(self.uiSele.gameObject, state)
end

function FashionShopItem:IsTryDress(state)
	NGUITools.SetActive(self.uiFitting.gameObject, state)
	self.isTryDress = state
end

return FashionShopItem

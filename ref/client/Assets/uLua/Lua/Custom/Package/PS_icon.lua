-- 20150612_142006


PS_icon =  {
	uiName = 'PS_icon',
	------------------------------------
	-------  UI
	uiBuyType,
	uiSellType,



	------------------------------------
	-------  value
	good_item=nil,
	goods = nil,

	go = {},
	goodsId,
	isSell,
	goodsConsume,
	ps,
	isTreasureBox = false,
}




function PS_icon:Awake()
	-- self.go.icon = getComponentInChild(self.transform,"Icon","UISprite")
	-- self.go.bg = getComponentInChild(self.transform,"Bg","UISprite")
	-- self.go.side = getComponentInChild(self.transform,"Side","UISprite")
	self.go.name = getComponentInChild(self.transform,"Name","UILabel")
	self.go.type = getComponentInChild(self.transform,"Type","UILabel")
	self.go.action = getComponentInChild(self.transform,"Action","UILabel")
	self.go.iconGrid = self.transform:FindChild("GoodsIcon")
	self.uiBuyType = self.transform:FindChild("BuyType"):GetComponent("UILabel")
	self.uiSellType = self.transform:FindChild("SellType"):GetComponent("UILabel")
	-- self.go.level = getComponentInChild(self.transform,"LevelText","UILabel")

end



function PS_icon:Start()
	local attrConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.goodsId)
	self.goodsConsume = getLuaComponent(self.transform:FindChild("BuyType/GoodsIconConsume").gameObject)
	local item = getLuaComponent(createUI("GoodsIcon", self.go.iconGrid))
	if self.goods then
		item.goods = self.goods
	end
	item.goodsID = self.goodsId

	if self.isSell then
		self.uiBuyType.text = getCommonStr("SELL_PRICE")
		self.goodsConsume.rewardId = attrConfig.sell_id
		self.goodsConsume.rewardNum = attrConfig.sell_price
	else
		self.uiBuyType.text = getCommonStr("BUY_PRICE")
		self.goodsConsume.rewardId = self.ps.storeData.store_good_consume_type
		self.goodsConsume.rewardNum = self.ps.storeData.store_good_price
		self.goodsConsume.isAdd = false
		self.go.name.text = attrConfig.name
		self.uiSellType.text = "EXP"

	end
	NGUITools.SetActive(self.uiBuyType.gameObject, not self.isTreasureBox)


	item.hideNeed = true
	item.hideLevel = true
	if self.good_item then
		self.go.name.text = self.good_item.uiLabelName.text
		self.go.name.color = self.good_item.uiLabelName.color
	end

	local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(item.goodsID)
	-- local s = string.split(self.good_item.uiIcon.spriteName, '_')
	if GoodsCategory.IntToEnum(goodsConfig.category) == GoodsCategory.GC_EQUIPMENT and 
		EquipmentType.IntToEnum(goodsConfig.sub_category) ~= EquipmentType.ET_EQUIPMENTPIECE then
		self.go.action.text = '1'
	else
		if self.good_item then
			self.go.action.text = self.good_item.num
		elseif GoodsCategory.IntToEnum(attrConfig.category) == GoodsCategory.GC_CONSUME and attrConfig.sub_category=="1" then
			local enum = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(attrConfig.use_result_id).args:GetEnumerator()
			enum:MoveNext()
			local useId = enum.Current.id
			local t = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(useId)
			enum = t:GetEnumerator()
			enum:MoveNext()
			self.go.action.text = "+"..enum.Current.award_value
		else
			self.go.action.text = "+".."1"
		end
	end
end

return PS_icon

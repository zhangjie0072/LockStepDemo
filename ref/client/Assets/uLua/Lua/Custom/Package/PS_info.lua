-- 20150612_142006


PS_info =  {
	uiName = 'PS_info',

	------------------------------------
	-------  UI
	uiPriceInfo,
	uiPriceInfoName,



	------------------------------------
	-------  value
	good_item= nil,
	go={},
	price,
	isSell = true,
	goodsId,
	ps,
	goodsConsume,
	goodsAttr,
	isTreasureBox = false,
}




function PS_info:Awake()
	self.go.number = getComponentInChild(self.transform,"NumInfo/Number/Num",'UILabel')
	-- self.go.price = getComponentInChild(self.transform,"PriceInfo/Price/Num",'UILabel')

	self.go.mode0 = self.transform:FindChild('NumInfo/Num').gameObject
	self.go.mode1 = self.transform:FindChild('NumInfo/Number').gameObject


	self.go.add_btn = getComponentInChild(self.transform,'NumInfo/Number/+','UIButton').gameObject
	self.go.subtract_btn = getComponentInChild(self.transform,'NumInfo/Number/-','UIButton').gameObject
	self.go.max_btn = getComponentInChild(self.transform,'NumInfo/Number/MAX','UIButton').gameObject

	self.uiPriceInfo = self.transform:FindChild("PriceInfo")
	self.uiPriceInfoName = self.transform:FindChild("PriceInfo/Name"):GetComponent("UILabel")

	addOnClick(self.go.add_btn,self:click_add())
	addOnClick(self.go.subtract_btn,self:click_subtract())
	addOnClick(self.go.max_btn,self:click_max())
end


function PS_info:click_add()
	return function()
		if self.cur_num < self.max_num then
			self.cur_num = self.cur_num + 1
			self:update_num()

		end

	end
end

function PS_info:click_subtract()
	return function()
		if self.cur_num > 1 then
			self.cur_num = self.cur_num -1
			self:update_num()
		end

	end
end


function PS_info:click_max()
	return function()
		if self.cur_num ~= self.max then
			self.cur_num = self.max_num
			self:update_num()
		end
	end
end


function PS_info:update_num()
	self.go.number.text = tostring(self.cur_num)
	print('self.cur_num ========= ' .. self.cur_num)
	if not self.isTreasureBox then
		self.goodsConsume.rewardNum = (self:GetPrice() * self.cur_num)
		self.goodsConsume:Refresh()
	end
end


function PS_info:Start()
	if self.isTreasureBox then
		NGUITools.SetActive(self.uiPriceInfo.gameObject, false)
		local ownNum = self.good_item.goods:GetNum()
		if ownNum <= 99 then
			self.max_num = ownNum
		else
			self.max_num = 99
		end
		self.cur_num = self.max_num
		self:update_num()
		return
	end
	
	self.goodsConsume = getLuaComponent(self.transform:FindChild("PriceInfo/GoodsIconConsume"))
	self.goodsAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.goodsId)
	if self.isSell then
		self.max_num = self.good_item.goods:GetNum()
		self.uiPriceInfoName.text = getCommonStr("STR_SELL_TIP1")
		self.goodsConsume.rewardId = self.goodsAttr.sell_id
	else
		self.max_num = self.ps.limitBuyNum
		self.uiPriceInfoName.text = getCommonStr("STR_BUY_PRICE1")
		self.goodsConsume.rewardId = self.ps.storeData.store_good_consume_type
		self.goodsConsume.rewardNum = self.ps.storeData.store_good_price
		self.goodsConsume.isAdd = false
	end

	self:update_mode()

	if self.mode==0 then
		self.go.number.text = tostring( self.max_num)
	elseif self.mode==1 then
		local num = math.min(1, self.max_num)
		self.go.number.text= tostring(num)
	end
	self:update_num()
end



function PS_info:update_mode()
	self.mode = 1
	NGUITools.SetActive( self.go.mode0,self.mode==0)
	NGUITools.SetActive( self.go.mode1, self.mode == 1)
	if self.mode == 0 then
		self.cur_num = self.max_num
	elseif self.mode == 1 then
		self.go.number = getComponentInChild(self.transform,'NumInfo/Number/Num','UILabel')
		self.cur_num = math.min(1, self.max_num)
	end
end


function PS_info:GetPrice()
	if self.isSell then
		return self.goodsAttr.sell_price
	else
		return self.ps.storeData.store_good_price
	end
end
return PS_info

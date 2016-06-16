--encoding=utf-8

StoreBuyDetail =
{
	uiName = 'StoreBuyDetail',

	--------------------------
	pos,
	goodsItem,

	------------------------UI
	uiGoodsIcon,
	uiGoodsName,
	uiGoodsNum,
	uiPriceTypeItem,
	uiPriceValueItem,
}

function StoreBuyDetail:Awake()
	local transform = self.transform
	self.uiBuyBtn = self.transform:FindChild('Window/ButtonOK'):GetComponent('UIButton')
	self.uiCloseBtn = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))

	local go = createUI('GoodsIcon', transform:FindChild('Window/GoodsIcon').transform)
	self.uiGoodsIcon = getLuaComponent(go)

	self.uiGoodsName = transform:FindChild('Window/GoodsName'):GetComponent('UILabel')
	self.uiGoodsNum = transform:FindChild('Window/GoodsNum'):GetComponent('UILabel')

	self.uiPriceTypeItem = transform:FindChild('Window/PriceItem/CostLb/CostTypeIcon'):GetComponent('UISprite') --createUI('GoodsIcon', transform:FindChild('Window/PriceItem').transform)
	self.uiPriceValueItem = transform:FindChild('Window/PriceItem/CostLb/CostValue'):GetComponent('UILabel')
	--self.uiPriceItem = getLuaComponent(go)
end

--Start
function StoreBuyDetail:Start()
	addOnClick(self.uiBuyBtn.gameObject, self:OnBuyClick())
	local btnClose = getLuaComponent(self.uiCloseBtn)
	btnClose.onClick = self:OnCloseClick()
end

function StoreBuyDetail:Init(goodsItem, pos)
	self.goodsItem = goodsItem
	self.pos = pos

	self.uiGoodsIcon.goodsID = goodsItem.config.id
	self.uiGoodsIcon.hideLevel = true
	self.uiGoodsIcon.hideNeed = true

	local nameColors = {
		Color.New(255/255, 255/255, 255/255, 1),
		Color.New(62/255, 255/255, 42/255, 1),
		Color.New(33/255, 255/255, 255/255, 1),
		Color.New(213/255, 0/255, 255/255, 1),
		Color.New(255/255, 237/255, 3/255, 1)
	}
	self.uiGoodsName.color = nameColors[goodsItem.config.quality]
	self.uiGoodsName.text = goodsItem.uiName.text

	self.uiGoodsNum.text = goodsItem.num

	print('spriteName ------- ' .. tostring(goodsItem.uiCostType.spriteName))
	print('text ---------- ' .. tostring(goodsItem.uiCostValue.text))

	MainPlayer.Instance.BuyItemId = goodsItem.goodsID
	MainPlayer.Instance.BuyItemNum = goodsItem.num
	MainPlayer.Instance.BuyItemCost = tonumber(goodsItem.uiCostValue.text)
	
	self.uiPriceTypeItem.spriteName = goodsItem.uiCostType.spriteName
	self.uiPriceValueItem.text = tonumber(goodsItem.uiCostValue.text)
 -- * tonumber(goodsItem.uiNum.text)
end
--点击关闭事件
function StoreBuyDetail:OnCloseClick()
	return function (go)
		NGUITools.Destroy(self.gameObject)
	end
end

--点击购买事件
function StoreBuyDetail:OnBuyClick()
	return function (go)

		--发送打开商店的请求
		local buyStoreGoods = {  
			store_id = UIStore.type,
			info = {	},
		}

		table.insert(buyStoreGoods.info, {pos = self.pos,})
		local msg = protobuf.encode("fogs.proto.msg.BuyStoreGoods", buyStoreGoods)
		LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)
		CommonFunction.ShowWait()
		--注册购买商品的回复处理消息
		LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyStoreGoodsResp(), self.uiName)
	end
end

function StoreBuyDetail:BuyStoreGoodsResp()
	--解析pb
	return function(message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
		if resp == nil then
			Debugger.LogError('------BuyStoreGoodsResp error: ', err)
			return
		end

		Debugger.Log('---2---------resp: {0}', resp.store_id)
		if resp.result ~= 0 then
			Debugger.Log('-----------1: {0}', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			playSound("UI/UI-wrong")
			return
		end

		--
		for k, v in pairs(resp.info or {}) do
			self.uiStore:OnBuyResp(v.pos)
		end
		self.uiStore:RefreshButtonMenu()
		-- local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
		-- getGoods:SetGoodsData(self.uiGoodsIcon.goodsID, self.uiGoodsNum.text)

		NGUITools.Destroy(self.gameObject)
	end
end

return StoreBuyDetail

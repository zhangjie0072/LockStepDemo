--encoding=utf-8

StoreRefreshDetail =
{
	uiName = 'StoreRefreshDetail',
	
	--------------------------
	popupFrame,

	--------------------------
	uiPriceItem,
};


-----------------------------------------------------------------
--Awake
function StoreRefreshDetail:Awake()
	local transform = self.transform

	--背景
	local go = CommonFunction.InstantiateObject('Prefab/GUI/PopupFrame3', transform)
	if go == nil then
		Debugger.Log('-- InstantiateObject falied ')
		return
	end
	self.popupFrame = getLuaComponent(go)
	self.popupFrame:SetTitle(CommonFunction.GetConstString('STR_REFRESH'))
	self.popupFrame.showCorner = true
	--关闭
	self.popupFrame.onClose = self:OnCloseClick()
	--刷新
	self.popupFrame.buttonLabels = {getCommonStr('STR_REFRESH')}
	self.popupFrame.buttonHandlers = {self:OnRefreshClick()}

	go = createUI('PriceItem', transform:FindChild('PriceItem').transform)
	self.uiPriceItem = getLuaComponent(go)
end

--Start
function StoreRefreshDetail:Start()

end

--Update
function StoreRefreshDetail:Update( ... )
	-- body
end


-----------------------------------------------------------------
---
function StoreRefreshDetail:Init(config)
	if config.consume_type == 1 then
		self.uiPriceItem.uiValueIcon.spriteName = 'com_property_diamond2'
	elseif config.consume_type == 2 then
		self.uiPriceItem.uiValueIcon.spriteName = 'com_property_gold2'
	elseif config.consume_type == 3 then
		self.uiPriceItem.uiValueIcon.spriteName = 'com_property_honor2'
	elseif config.consume_type == 4 then
		self.uiPriceItem.uiValueIcon.spriteName = 'com_property_hp2'
	elseif config.consume_type == 7 then
		self.uiPriceItem.uiValueIcon.spriteName = 'com_property_prestige2'
	end
	self.uiPriceItem.uiValueNum.text = config.consume
end

--点击关闭事件
function StoreRefreshDetail:OnCloseClick()
	return function (go)
		NGUITools.Destroy(self.gameObject)
	end
end

--点击购买事件
function StoreRefreshDetail:OnRefreshClick()
	return function (go)
		--发送打开商店的请求
		local refreshStoreGoods = {  
			store_id = UIStore.type,
		}
		local msg = protobuf.encode("fogs.proto.msg.RefreshStoreGoods", refreshStoreGoods)
		LuaHelper.SendPlatMsgFromLua(MsgID.RefreshStoreGoodsID, msg)
		
		UIStore:RegisterRefresh()


		NGUITools.Destroy(self.gameObject)
	end
end


return StoreRefreshDetail

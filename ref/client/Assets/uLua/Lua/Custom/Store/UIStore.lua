--encoding=utf-8

UIStore = UIStore or
{
	uiName = 'UIStore',

	----------------------------------
	uiBack,

	----------------------------------
	--storeGoodsInfo ={},
	nextRefreshTime,
	refreshTimes,

	preSelectItem,

	goodsItemTable= {},
	goodsTable = {},

	type = 'ST_BLACK',
	id = 1,
	titleStr = 'STR_BLACK_STORE',

	curSwitch,
	itemNum,
	isShowSell = false,
	sellCost = 0,
	sellGoodsList,
	banTwice = false,	--禁止两次

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,

	selectedGoods,

	----------------------------------UI
	--返回
	uiButtonBack,
	--资产
	uiProperty,
	--商店标题
	uiTitle,
	uiBtnMenu,
	--商品列表ScrollView
	uiGoodsListSV,
	uiGoodsListGrid,
	--刷新信息
	uiRefresh,
	uiRefreshTips,
	uiRefreshTime,
	--刷新按钮
	uiButtonRefresh,
	uiButtonRefreshLabel,
	--技能物品切换
	uiSwitch,
	uiSwitchTotal,
	uiSwitchAttack,
	uiSwitchDefence,
	uiSwitchAssist,
	--左侧商品详细信息
	--技能
	--uiSkillDetail,
	uiSkillBtn,

	uiGoodsDetail,
	uiEquipmentDetail,

	--黑市
	uiBlackDetail,

	--购买详细界面
	uiStoreBuyDetail,
	--刷新详细界面
	uiStoreRefreshDetail,

	-- uiBackgroundLeft,
	-- uiBackgroundMiddle,
	-- uiBackgroundRight,
	-- uiBackgroundLine,

	-- uiDetailName,
	-- uiDetailIcon,
	-- uiDetailCategory,
	-- uiDetailLabel,
	uiAnimator,
	uiPopup,
}


-----------------------------------------------------------------
--Awake
function UIStore:Awake( ... )
	local transform = self.transform

	--返回
	local go = createUI('ButtonBack', transform:FindChild('Top/ButtonBack').transform)
	self.uiButtonBack = getLuaComponent(go)
	self.uiButtonBack.onClick = self:OnBackClick()

	--资产信息
	self.uiBtnMenu = createUI('ButtonMenu', transform:FindChild('Top/ButtonMenu'))

	--标题
	self.uiTitle = transform:FindChild('Top/Title'):GetComponent('MultiLabel')
	--
	self.uiGoodsListSV = transform:FindChild('SelectZone/SelectScrollView'):GetComponent('UIScrollView')
	self.uiGoodsListGrid = transform:FindChild('SelectZone/SelectScrollView/SelectGrid'):GetComponent('UIGrid')

	--
	self.uiGoodsDetail = transform:FindChild('GoodsDetailShow/DetailShow/GoodsDetail')
	self.uiEquipmentDetail = transform:FindChild('GoodsDetailShow/DetailShow/EquipmentDetail')

	--刷新
	self.uiRefresh = transform:FindChild('SelectZone/Refresh')
	self.uiRefreshTips = transform:FindChild('SelectZone/Refresh/RefreshTips'):GetComponent('UILabel')
	self.uiRefreshTips.text = CommonFunction.GetConstString('QUALIFYING_TIME_REFRESH') .. '：'
	self.uiRefreshTime = transform:FindChild('SelectZone/Refresh/RefreshTime'):GetComponent('MultiLabel')
	self.uiRefreshTime:SetText(self.nextRefreshTime .. ':00')
	self.uiButtonRefresh = transform:FindChild('SelectZone/Refresh/ButtonRefresh').gameObject
	self.uiButtonRefreshLabel = transform:FindChild('SelectZone/Refresh/ButtonRefresh/Text'):GetComponent('MultiLabel')
	self.uiButtonRefreshLabel:SetText(CommonFunction.GetConstString('STR_REFRESH'))

	-- self.uiDetailName = transform:FindChild('GoodsDetailShow/Name'):GetComponent('UILabel')
	-- self.uiDetailIcon = transform:FindChild('GoodsDetailShow/Icon')
	-- self.uiDetailCategory = transform:FindChild('GoodsDetailShow/CategoryLabel'):GetComponent('UILabel')
	-- self.uiDetailLabel = transform:FindChild('GoodsDetailShow/Detail'):GetComponent('UILabel')

	--购买详细界面
	self.uiStoreBuyDetail = transform:FindChild('StoreBuyDetail')
	self.uiStoreBuyDetail.gameObject:SetActive(false)
	--刷新详细界面
	self.uiStoreRefreshDetail = transform:FindChild('StoreRefreshDetail')
	self.uiStoreRefreshDetail.gameObject:SetActive(false)

	-- skill
	-- self.uiSkillBtn = transform:FindChild("ButtonSkill")
	-- self.uiSkillBtn.gameObject:SetActive(false) --hide skill button detault
	-- NGUITools.SetActive(self.uiSkillBtn.gameObject,false) -- hide skill button

	-- background in order to set color
	--self.uiBackgroundLeft = transform:FindChild("Background"):GetComponent("UISprite")
	--self.uiBackgroundMiddle = transform:FindChild("Background/BGYellow"):GetComponent("UISprite")
	--self.uiBackgroundRight = transform:FindChild("Background/BGRed_Strip1"):GetComponent("UISprite")
	--self.uiBackgroundLine = transform:FindChild("Background/BGRed_Strip"):GetComponent("UISprite")

	self.uiButtonBuy = transform:FindChild("GoodsDetailShow/DetailShow/ButtonBuy").gameObject
	NGUITools.SetActive(self.uiButtonBuy, false)
	addOnClick(self.uiButtonBuy,self:OnBuyClick())
	LuaHelper.RegisterPlatMsgHandler(MsgID.SellGoodsRespID, self:SellHandler(), self.uiName)

	--self.uiButtonUp = transform:FindChild("SelectZone/Up")
	--self.uiButtonDown = transform:FindChild("SelectZone/Down")
	--self.uiProcessBar = transform:FindChild("SelectZone/SelectScrollView/ProcessBar"):GetComponent("UIProgressBar")

	self.uiAnimator = self.transform:GetComponent('Animator')
end

--Start
function UIStore:Start( ... )
	self.uiProperty = self.transform:FindChild('Top/PlayerInfoGrids').gameObject
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	--点击刷新
	addOnClick(self.uiButtonRefresh, self:OnRefreshClick())
	self.uiGoodsListGrid.onCustomSort = function(x, y)
		return self:GoodsCompare(getLuaComponent(x).goodsID, getLuaComponent(y).goodsID)
	end
	self.uiGoodsIconConsume = getLuaComponent(self.uiRefresh:FindChild('GoodsIconConsume'))

	local num = 0
	local enum = MainPlayer.Instance.StoreRefreshTimes:GetEnumerator()
	while enum:MoveNext() do
		num = num + 1
		if num == self.id then
			self.refreshTimes = enum.Current
			break
		end
	end
	local consumeConfig = GameSystem.Instance.StoreGoodsConfigData:GetConsume(self.id, self.refreshTimes + 1)
	self.uiGoodsIconConsume:SetData(consumeConfig.consume_type, consumeConfig.consume,false)
	self.uiGoodsIconConsume.isAdd = false
	self:SellDetail()
	self:RegisterRefresh()
end

--Update
function UIStore:FixedUpdate( ... )
	-- body
end

function UIStore:OnClose( ... )
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, true)

	if self.onClose then
		--print("uiBack",self.uiName,"--:",self.onClose)
		self.onClose()
		self.onClose = nil
		return
	end

	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end
end

function UIStore:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIStore:OnDestroy( ... )
	-- body
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.RefreshStoreGoodsRespID, self.uiName)
	self.started = false
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--
function UIStore:Refresh()
	local localLua = getLuaComponent(self.uiProperty)
	self:RefreshButtonMenu()
	--设置巡回赛商店可见
	if self.type == 'ST_TOUR' then
		localLua.showPrestige = true
	else
		localLua.showPrestige = false
	end

	if self.type == 'ST_HONOR' then
		localLua.showHonor = true
	else
		localLua.showHonor = false
	end
	localLua:Refresh()

	self:ShowSellTip()
	self:InitGoodsList()
end


-----------------------------------------------------------------

function UIStore:SellDetail( ... )
	self.sellGoodsList = {}
	local goodsIDs = GameSystem.Instance.CommonConfig:GetString("gAutomaticSell")

	local sellGoodsID = string.split(goodsIDs, '&')	--{4017,4018,4019,4020,4021,4022,4023}
	printTable(sellGoodsID)
	local enum = MainPlayer.Instance.AllGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		for i,v in ipairs(sellGoodsID) do
			if tonumber(v) == goods:GetID() then
				local price = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goods:GetID()).sell_price
				self.sellCost = self.sellCost + goods:GetNum() * price
				table.insert(self.sellGoodsList, goods)
				self.isShowSell = true
			end
		end
	end
end

function UIStore:ShowSellTip( ... )
	print('self.isShowSell:' .. tostring(self.isShowSell) .. 'self.sellCost:' .. self.sellCost)
	if self.isShowSell == true and self.sellCost > 0 then
		self.uiPopup = createUI('PopupMessage2')
		local consume = getLuaComponent(self.uiPopup.transform:FindChild('Window/Acquire/GoodsIconConsume').gameObject)
		consume:SetData(2, self.sellCost)
		local btnOK = self.uiPopup.transform:FindChild('Window/ButtonOK')
		local btnCancel = self.uiPopup.transform:FindChild('Window/ButtonCancel')
		addOnClick(btnOK.gameObject, self:ConfirmSellClick())
		addOnClick(btnCancel.gameObject, self:CancelSellClick())
		local goods = self.uiPopup.transform:FindChild('Window/Scroll/Goods')
		for i,v in ipairs(self.sellGoodsList) do
			local eGoods = createUI('GoodsIcon', goods.transform)
			local icon = getLuaComponent(eGoods)
			icon.goods = v
			icon.num = v:GetNum()
			icon.hideLevel = true
			icon.hideNum = false
			icon.hideNeed = true
		end
		-- CommonFunction.ShowPopupMsg(string.format(getCommonStr('STR_STORE_SELL'), self.sellCost), nil, LuaHelper.VoidDelegate(self:ConfirmSellClick()),
		--	LuaHelper.VoidDelegate(self:CancelSellClick()), nil, nil)
	end
end

function UIStore:ConfirmSellClick( ... )
	return function (go)
		local info = {}
		for _,v in ipairs(self.sellGoodsList) do
			table.insert(info, {uuid = v:GetUUID(), num = v:GetNum(), category = tostring(v:GetCategory())})
		end
		local operation ={
			info = info
		}

		--print('handle_click_sell category='..info.category)
		local req = protobuf.encode("fogs.proto.msg.SellGoods",operation)

		LuaHelper.SendPlatMsgFromLua(MsgID.SellGoodsID,req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UIStore:SellHandler( ... )
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, error = protobuf.decode('fogs.proto.msg.SellGoodsResp',buf)
		if resp.result == 0 then
			if self.uiPopup ~= nil then
				NGUITools.Destroy(self.uiPopup.gameObject)
				self.uiPopup = nil
				local str = getCommonStr("ST_SELL_REWARDS_GOLDS"):format(getCommonStr("STR_PREMIUM"), self.sellCost, "金币")
				CommonFunction.ShowPopupMsg(str,nil,nil,nil,nil,nil)
			else
				CommonFunction.ShowPopupMsg(getCommonStr('SELL_SUCCESS'),nil,nil,nil,nil,nil)
			end
			self.isShowSell = false
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			playSound("UI/UI-wrong")
		end
	end
end

function UIStore:CancelSellClick( ... )
	return function (go)
		self.isShowSell = false
		NGUITools.Destroy(self.uiPopup.gameObject)
		self.uiPopup = nil
	end
end


-----------------------------------------------------------------
--设置返回界面
function UIStore:SetBackUI(uiBackName)
	self.uiBack = uiBackName
end

--返回点击处理
function UIStore:OnBackClick()
	return function (go)
		self:DoClose()
	end
end

function UIStore:SetModelActive( ... )
	-- body
end

--显示界面
function UIStore:ShowUI()
	--创建商店界面
	TopPanelManager:ShowPanel(self.uiName)
end

--设置商店类型
function UIStore:SetType(type)
	self.type = type
	if self.type == 'ST_BLACK' then
		self.id = 1
		self.titleStr = 'STR_BLACK_STORE'
	elseif self.type == 'ST_SKILL' then
		self.id = 2
		self.titleStr = 'STR_SKILL_STORE'
	elseif self.type == 'ST_FASHION' then
		self.id = 3
		self.titleStr = 'STR_FASHION_STORE'
	elseif self.type == 'ST_HONOR' then
		self.id = 4
		self.titleStr = 'STR_HONOR_STORE'
	--新增商店
	elseif self.type == 'ST_TOUR' then
		self.id = 5
		self.titleStr = 'STR_TOUR_STORE'
	elseif self.type == 'ST_OTHER' then
		self.id = 6
		self.titleStr = 'STR_OTHER_STORE'
	end
end

-- color is diffrent for different type.
function UIStore:set_background_color(left,middle,right,line)
	--self.uiBackgroundLeft.spriteName = left
	--self.uiBackgroundMiddle.spriteName = middle
	--self.uiBackgroundRight.spriteName = right
	--self.uiBackgroundLine.spriteName = line
end

function UIStore:RegisterRefresh()
	LuaHelper.RegisterPlatMsgHandler(MsgID.RefreshStoreGoodsRespID, self:RefreshStoreGoodsResp(), self.uiName)
end
--
function UIStore:OpenStore()
	--发送打开商店的请求
	local openStore = {
		store_id = self.type,
	}
	local msg = protobuf.encode("fogs.proto.msg.OpenStore", openStore)
	LuaHelper.SendPlatMsgFromLua(MsgID.OpenStoreID, msg)
	CommonFunction.ShowWait()

	--注册打开商店的回复处理消息
	LuaHelper.RegisterPlatMsgHandler(MsgID.RefreshStoreGoodsRespID, self:RefreshStoreGoodsResp(), self.uiName)
end

--打开商店回复消息的处理
function UIStore:RefreshStoreGoodsResp()
	--解析pb
	return function(message)
		CommonFunction.StopWait()
		if not self.started then
			LuaHelper.UnRegisterPlatMsgHandler(MsgID.RefreshStoreGoodsRespID, self.uiName)
		end
		local resp, err = protobuf.decode('fogs.proto.msg.RefreshStoreGoodsResp', message)
		-- Debugger.Log('------------resp: {0}', resp.store_id)
		if resp == nil then
			print('error -- RefreshStoreGoodsResp error: ', err)
			return
		end
		if resp.result ~= 0 then
			print('error --  RefreshStoreGoodsResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		if resp.store_id ~= self.type then
			print('error --  store id is not matched : ', resp.store_id)
			return
		end

		--初始化商品列表信息
		self:InitStoreGoodsInfo(resp.goods_list)
		if resp.next_refresh_time == 0 then
			self.nextRefreshTime = 24
		else
			self.nextRefreshTime = resp.next_refresh_time
		end

		if self.started and self.uiRefreshTime then
			self.uiRefreshTime:SetText(self.nextRefreshTime .. ':00')
			self:Refresh()
		end

		print(self.uiName,"------next_refresh_time:",resp.next_refresh_time)
		self.refreshTimes = resp.refresh_request
		if self.started and self.uiGoodsIconConsume then
			local consumeConfig = GameSystem.Instance.StoreGoodsConfigData:GetConsume(self.id, self.refreshTimes + 1)
			self.uiGoodsIconConsume:SetData(consumeConfig.consume_type, consumeConfig.consume,false)
		end
		if resp.oper_type ~= 'SRT_SYSTEM_REFRESH' then
			--创建商店界面
			self:ShowUI()
		end

		if self.refreshTimes then
			local num = 0
			local enum = MainPlayer.Instance.StoreRefreshTimes:GetEnumerator()
			while enum:MoveNext() do
				num = num + 1
				if num == self.id then
					MainPlayer.Instance.StoreRefreshTimes:set_Item(num - 1, self.refreshTimes)
					break
				end
			end
		end
		self.banTwice = false
	end
end

--初始化商品列表数据信息
function UIStore:InitStoreGoodsInfo(goodsList)
	StoreGoodsInfo = goodsList
end

--初始化商品列表区域
function UIStore:InitGoodsList()
	self.goodsTable = {}
	self.uiTitle:SetText(CommonFunction.GetConstString(self.titleStr))

	local firstSelect = false
	--初始化商品列表
	CommonFunction.ClearGridChild(self.uiGoodsListGrid.transform)
	self.itemNum  = 0
	for k, v in pairs(StoreGoodsInfo or {}) do
		local item = self:InitGoodsItem(v, self.uiGoodsListGrid.transform)
		if item then
			if self.goodsTable[k] == nil then
				local goodsProto = GoodsProto.New()
				goodsProto.uuid = 0
				goodsProto.id = item.goodsID
				goodsProto.category = GoodsCategory.IntToEnum(item.config.category)
				goodsProto.num = item.num
				goodsProto.level = 1
				goodsProto.quality = GoodsQuality.IntToEnum(item.config.quality)
				goodsProto.is_equip = 0
				goodsProto.time_left = 0
				goodsProto.is_used = 0
				goodsProto.exp = 0
				local goods = Goods.New()
				goods:Init(goodsProto)
				self.goodsTable[k] = goods
			end

			item.gameObject.name = string.format("StoreGoodsItem%05d", self.itemNum)
			item.pos = k
			addOnClick(item.gameObject, self:OnItemClick())
			if firstSelect == false then
				self:OnItemClick()(item.gameObject)
				firstSelect = true
			end
			self.goodsItemTable[k] = item
			item:SetParent(self)
			self.itemNum = self.itemNum + 1
		end
	end
	self.uiGoodsListGrid.repositionNow = true
	self.uiGoodsListSV:ResetPosition()

	--

	--NGUITools.SetActive(self.uiButtonUp.gameObject, self.itemNum > 6 )
	--NGUITools.SetActive(self.uiButtonDown.gameObject, self.itemNum > 6)


	-- if self.uiBlackDetail == nil then
	--	local go = createUI('GoodsDetail', self.transform:FindChild('GoodsDetailShow/DetailShow/GoodsDetail'))
	--	self.uiBlackDetail = getLuaComponent(go)
	-- end
	-- if self.uiSkillDetail == nil then
	--	local go = createUI('GoodsDetail', self.transform:FindChild('GoodsDetailShow/DetailShow/GoodsDetail'))
	--	self.uiSkillDetail = getLuaComponent(go)
	-- end

	if self.type == 'ST_BLACK' or self.type == 'ST_TOUR' then
		--self.uiBlackDetail.gameObject:SetActive(true)
		--self.uiSkillDetail.gameObject:SetActive(false)
		self.uiRefresh.gameObject:SetActive(true)
		self.curSwitch = 'TOTAL'
		--self.uiButtonBack:set_back_icon("com_bg_pure_goldendeep",1.0)
		-- NGUITools.SetActive(self.uiSkillBtn.gameObject,false)
		self:set_background_color("com_bg_pure_brownlight","com_bg_pure_browntop","com_bg_pure_round_brown","com_bg_pure_brownwhite")
	--elseif self.type == 'ST_SKILL' then
		--self.uiBlackDetail.gameObject:SetActive(false)
		--self.uiSkillDetail.gameObject:SetActive(true)
		--self.uiRefresh.gameObject:SetActive(false)
		----self.uiButtonBack:set_back_icon("com_bg_pure_goldendeep",1.0)
		---- NGUITools.SetActive(self.uiSkillBtn.gameObject,true)
		---- self:set_background_color("")
		-- self:set_background_color(
		--	"com_bg_pure_goldenlight",
		--	"com_bg_pure_goldentop",
		--	"com_bg_pure_round_golden",
		--	"com_bg_pure_goldenwhite")

	elseif self.type == 'ST_FASHION' then
		--TODO: add process here.

	elseif self.type == 'ST_HONOR' then
		--self.uiBlackDetail.gameObject:SetActive(true)
		--self.uiSkillDetail.gameObject:SetActive(false)
		self.uiRefresh.gameObject:SetActive(true)
	end

end

--初始化商品对象
function UIStore:InitGoodsItem(info, parent)
	local go = createUI('StoreGoodsItem', parent)
	if go == nil then
		Debugger.Log('-- InstantiateObject falied ')
		return
	end
	local item = getLuaComponent(go)
	--配置
	local config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(info.id)
	--item:SetGoodsData(info)
	item.goodsID = info.id
	item.num = info.num
	item.config = config
	item.uiName.text = config.name
	-- --商品名
	-- item.uiName.text = config.name
	-- --商品图标
	-- item.uiGoodsIcon.goodsID = info.id


	-- item.uiGoodsIcon.hideLevel = true

	-- if self.type == 'ST_SKILL' then
	--	item.uiGoodsIcon.hideLevel = false
	--	item.mode = item.m.skill
	-- elseif self.type == "ST_BLACK" or self.type == "ST_TOUR" then
	--	item.mode = item.m.black
--	end
	-- --商品数量
	-- item.uiNum.text = info.num
	-- item.num = info.num
	--购买价值
	if info.consume_type == 1 then
		item.uiCostType.spriteName = 'com_property_diamond'
	elseif info.consume_type == 2 then
		item.uiCostType.spriteName = 'com_property_gold'
	elseif info.consume_type == 3 then
		item.uiCostType.spriteName = 'com_property_honor'
	elseif info.consume_type == 4 then
		item.uiCostType.spriteName = 'com_property_hp'
	elseif info.consume_type == 7 then
		item.uiCostType.spriteName = 'com_property_prestige'
	elseif info.consume_type == 8 then
		item.uiCostType.spriteName = 'com_property_honor1'
	elseif info.consume_type == 9 then
		item.uiCostType.spriteName = 'com_property_prestige1'
	end
	item.uiCostValue.text = info.price * info.num

	--购买状态
	if info.sell_out == 1 then
		item:Sellout()
	else
		item:Normal()
		-- addOnClick(item.uiBuyInfo.gameObject, self:OnBuyClick())
	end

	--item.uiDragSV1.scrollView = self.uiGoodsListSV
	--item.uiDragSV2.scrollView = self.uiGoodsListSV

	return item
end

--
function UIStore:InitShowDetail(config, num)
	--左侧商品详细信息
	if self.selectedGoods and self.selectedGoods:GetCategory() == GoodsCategory.GC_EQUIPMENT then
		self.uiEquipmentDetail.gameObject:SetActive(true)
		self.uiGoodsDetail.gameObject:SetActive(false)

		local equipmentDetailObj
		if self.uiEquipmentDetail.transform.childCount > 0 then
			equipmentDetailObj = self.uiEquipmentDetail.transform:GetChild(0)
		end
		if equipmentDetailObj == nil then
			equipmentDetailObj = createUI('EquipmentDetail', self.uiEquipmentDetail.transform)
		end
		local script = getLuaComponent(equipmentDetailObj)
		script.inStore = true
		script.goods = self.selectedGoods
		script:Refresh()
	else
		self.uiEquipmentDetail.gameObject:SetActive(false)
		self.uiGoodsDetail.gameObject:SetActive(true)
		if self.uiBlackDetail == nil then
			local go = createUI('GoodsDetail', self.transform:FindChild('GoodsDetailShow/DetailShow/GoodsDetail'))
			self.uiBlackDetail = getLuaComponent(go)
			self.uiBlackDetail.inStore = true
		end

		if self.type == 'ST_BLACK' or self.type == 'ST_TOUR' then
			--self.uiBlackDetail:Init(config)
			self.uiBlackDetail:SetData(nil, config.id, num)
		--elseif self.type == 'ST_SKILL' then
			---- self.uiSkillDetail.ID = config.id
			---- self.uiSkillDetail.level = 1
			---- self.uiSkillDetail:Refresh()
			--self.uiSkillDetail:SetData(nil, config.id, num)
		elseif self.type == 'ST_FASHION' then
			--TODO: add process here.

		elseif self.type == 'ST_HONOR' then
			self.uiBlackDetail:SetData(nil, config.id, num)
		end
	end
end

--点击商品条目事件
function UIStore:OnItemClick()
	return function (go)
		if self.preSelectItem == go then
			return
		end

		playSound("UI/UI_button")
		if self.preSelectItem then
			getLuaComponent(self.preSelectItem):Normal()
		end
		self.preSelectItem = go

		local item = getLuaComponent(go)

		self.selectedGoods = self.goodsTable[item.pos]

		item:Select()
		self:InitShowDetail(item.config, item.num)
		self:UpdateBuyButton()
	end
end

function UIStore:UpdateBuyButton(item)
	print("item ="..tostring(item))
	local script = item or getLuaComponent(self.preSelectItem)
	print("UIUpdateBuyButton"..tostring(script))
	if script then
		local sellout = script.sellout
		local display = not sellout
		print("UIUpdateBuyButton display="..tostring(display))
		NGUITools.SetActive(self.uiButtonBuy, display)
	end
end

--点击商品购买事件
function UIStore:OnBuyClick()
	return function (go)
		-- local item = getLuaComponent(go.transform.parent)
		local item = getLuaComponent(self.preSelectItem)
		if item.sellout then
			return
		end

		--判断消耗
		local cost = getLuaComponent(self.preSelectItem)
		local costValue = tonumber(cost.uiCostValue.text)
		local sprite = cost.uiCostType.spriteName

		if sprite == 'com_property_diamond' then
			if MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree < costValue then
				if self.banTwice == true then
					return
				end
				self.banTwice = true
				self:ShowBuyTip("BUY_DIAMOND")
				return
			end
		elseif sprite == "com_property_gold" then
			if MainPlayer.Instance.Gold < costValue then
				if self.banTwice == true then
					return
				end
				self.banTwice = true
				self:ShowBuyTip("BUY_GOLD")
				return
			end

		end
		-- self:OnItemClick()(go.transform.parent)

		-- self.uiStoreBuyDetail.gameObject:SetActive(true)

		-- local goBuyDetail = createUI('StoreBuyDetail', self.uiStoreBuyDetail)
		-- local buyDetail = getLuaComponent(goBuyDetail)
		-- buyDetail.uiStore = self
		-- buyDetail:Init(item, item.pos)

		--发送打开商店的请求
		local buyStoreGoods = {
			store_id = UIStore.type,
			info = {	},
		}

		table.insert(buyStoreGoods.info, {pos = item.pos,})
		local msg = protobuf.encode("fogs.proto.msg.BuyStoreGoods", buyStoreGoods)
		LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)

		--注册购买商品的回复处理消息
		LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyStoreGoodsResp(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function UIStore:BuyStoreGoodsResp()
	--解析pb
	return function(message)
		CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self.uiName)

		local resp, err = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
		if resp == nil then
			Debugger.LogError('------BuyStoreGoodsResp error: ', err)
			return
		end
		if resp.result ~= 0 then
			Debugger.Log('-----------1: {0}', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			playSound("UI/UI-wrong")
			return
		end

		local item = getLuaComponent(self.preSelectItem)
		MainPlayer.Instance.BuyItemId = item.goodsID
		MainPlayer.Instance.BuyItemNum = item.num
		MainPlayer.Instance.BuyItemCost = tonumber(item.uiCostValue.text)
		--
		for k, v in pairs(resp.info or {}) do
			self:OnBuyResp(v.pos)
		end
		self:RefreshButtonMenu()
		local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
		getGoods:SetGoodsData(item.goodsID, item.num)
		getGoods.onClose = function ( ... )
			self.banTwice = false
		end
		self.banTwice = true
	end
end

--点击购买事件
function UIStore:OnConfirmRefresh()
	return function (go)
		--发送打开商店的请求
		local refreshStoreGoods = {
			store_id = UIStore.type,
		}
		local msg = protobuf.encode("fogs.proto.msg.RefreshStoreGoods", refreshStoreGoods)
		LuaHelper.SendPlatMsgFromLua(MsgID.RefreshStoreGoodsID, msg)

		self:RegisterRefresh()
	end
end

function UIStore:OnCloseClick( ... )
	return function (go)
		self.banTwice = false
	end
end

--
function UIStore:OnBuyResp(pos)
	local item = self.goodsItemTable[pos]
	if item == nil then
		print(' error -- cannot get goods item by position: ', pos)
		return
	end
	item:Sellout()
	StoreGoodsInfo[pos].sell_out = 1
	self.uiGoodsListGrid.repositionNow = true
	self.uiGoodsListSV:ResetPosition()
	self:UpdateBuyButton(item)
	MainPlayer.Instance:ConfirmBuy()
	--[[
	-- print('- -----------self.id : ', self.id)
	-- print('- -----------item.config.id : ', item.config.id)
	-- local storeData = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsData(self.id, item.config.id)
	-- local consumeGoods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(storeData.store_good_price)
	-- local tips = string.format(getCommonStr('STR_BUY_GOODS_TIPS'),
	--	storeData.store_good_price,
	--	consumeGoods.name,
	--	storeData.store_good_num,
	--	config.name)
	-- CommonFunction.ShowPopupMsg(tips, nil, nil, nil);
	--]]

end


--
function UIStore:OnRefreshClick()
	return function (go)
		if self.banTwice then
			return
		end
		local consumeConfig = GameSystem.Instance.StoreGoodsConfigData:GetConsume(self.id, self.refreshTimes + 1);
		--刷新次数检查
		if consumeConfig == nil then
			CommonFunction.ShowPopupMsg(
				CommonFunction.GetConstString("NOT_ENOUGH_TIMES"),
				self.transform,nil,nil,nil,nil)
			return
		end
		--金钱数量检查
		if MainPlayer.Instance:GetGoodsCount(consumeConfig.consume_type) < consumeConfig.consume then
			local tips
			if consumeConfig.consume_type == 1 then
				--tips = "NOT_ENOUGH_DIAMOND"
				if self.banTwice == true then
					return
				end
				self.banTwice = true
				self:ShowBuyTip("BUY_DIAMOND")
				return
			elseif consumeConfig.consume_type == 2 then
				--tips = "NOT_ENOUGH_MONEY"
				if self.banTwice == true then
					return
				end
				self.banTwice = true
				self:ShowBuyTip("BUY_GOLD")
				return
			elseif consumeConfig.consume_type == 3 then
				tips = "NOT_ENOUGH_HONOR"
			elseif consumeConfig.consume_type == 7 then
				tips = "NOT_ENOUGH_PRESTIGE"
			else
				tips = "NOT_ENOUGH_MONEY"
			end
			--提示金钱不足
			CommonFunction.ShowPopupMsg(
				CommonFunction.GetConstString(tips),
				self.transform,nil,nil,nil,nil)
			return
		end

		self.uiStoreRefreshDetail.gameObject:SetActive(true)

		-- local goBuyDetail = createUI('PopupMessage1', self.uiStoreRefreshDetail)
		-- local buyDetail = getLuaComponent(goBuyDetail)
		-- buyDetail.onOKClick = LuaHelper.VoidDelegate(self:OnConfirmRefresh())
		-- buyDetail:SetTitle(getCommonStr('STR_REFRESH_TITLE'))
		-- buyDetail:SetMessage(getCommonStr('STR_CONFIRM') .. getCommonStr('STR_REFRESH_TITLE') .. '?')
		local popup = CommonFunction.ShowPopupMsg(getCommonStr('STR_CONFIRM') .. getCommonStr('STR_REFRESH_TITLE') .. '?',nil,LuaHelper.VoidDelegate(self:OnConfirmRefresh()),LuaHelper.VoidDelegate(self:OnCloseClick()),nil,nil)
		popup.table:SetTitle(getCommonStr('STR_REFRESH_TITLE'))
		self.banTwice = true
		--NGUITools.BringForward(goBuyDetail)
	end
end

--
function UIStore:OnSkillTotalClick( ... )
	return function (go)
		self.curSwitch = 'TOTAL'
	end
end

--
function UIStore:OnSkillAttackClick( ... )
	return function (go)
		self.curSwitch = 'ATTACK'
	end
end

--
function UIStore:OnSkillDefenceClick( ... )
	return function (go)
		self.curSwitch = 'DEFENCE'
	end
end

--
function UIStore:OnSkillAssistClick( ... )
	return function (go)
		self.curSwitch = 'ASSIST'
	end
end

function  UIStore:FixedUpdate()
	-- if self.uiProcessBar and self.itemNum > 6 then
	--	local value = self.uiProcessBar.value
	--	if value >= 1.0 then
	--		NGUITools.SetActive(self.uiButtonDown.gameObject,false)
	--		NGUITools.SetActive(self.uiButtonUp.gameObject,true)
	--	elseif value <= 0 then
	--		NGUITools.SetActive(self.uiButtonDown.gameObject,true)
	--		NGUITools.SetActive(self.uiButtonUp.gameObject,false)
	--	else
	--		NGUITools.SetActive(self.uiButtonDown.gameObject,true)
	--		NGUITools.SetActive(self.uiButtonUp.gameObject,true)
	--	end
	-- end
end

function UIStore:RefreshButtonMenu( ... )
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
end

function UIStore:GoodsCompare(lID, rID)
	if lID == nil and rID ~= nil then
		return -1
	elseif lID ~= nil and rID == nil then
		return 1
	else
		if lID < rID then
			return -1
		elseif lID > rID then
			return 1
		else
			return 0
		end
	end
end

function UIStore:ShowBuyTip(type)
	local str
	if type == "BUY_GOLD" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
	elseif type == "BUY_DIAMOND" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
	elseif type == "BUY_HP" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
	end
	self.msg = CommonFunction.ShowPopupMsg(str, nil,
													LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
													LuaHelper.VoidDelegate(self:FramClickClose()),
													getCommonStr("BUTTON_CONFIRM"),
													getCommonStr("BUTTON_CANCEL"))
end

function UIStore:ShowBuyUI(type)
	return function()
		self.banTwice = false
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
	end
end

function UIStore:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.banTwice = false
	end
end

return UIStore

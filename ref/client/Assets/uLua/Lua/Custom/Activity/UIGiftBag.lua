UIGiftBag = 
{
	uiName = 'UIGiftBag',

	---------------------
	config,
	VIP_MAX_LEVEL = 15,
	currentVipLevel,
	currentItemID,
	getHp,
	HpItem,
	parent,
	onClose,

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,

	-------------------UI
	-- uiBackBtn,
	-- uiBtnMenu,
	uiBtnClose,
	uiLeftGrid,
	uiRightGrid,
	uiLeftScroll,
	uiRightScroll,
	uiRightPos,
	uiGetAwardBtn,
	uiAnimator,
}


-----------------------------------------------------------------
function UIGiftBag:Awake( ... )
	-- self.uiBackBtn = createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack'))
	-- self.uiBtnMenu = createUI('ButtonMenu', self.transform:FindChild('Top/ButtonMenu'))
	self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
	self.uiLeftGrid = self.transform:FindChild('Window/Left/Scroll/Grid'):GetComponent('UIGrid')
	self.uiRightGrid = self.transform:FindChild('Window/Right/Scroll/Grid'):GetComponent('UIGrid')
	self.uiLeftScroll = self.transform:FindChild('Window/Left/Scroll'):GetComponent('UIScrollView')
	self.uiRightScroll = self.transform:FindChild('Window/Right/Scroll'):GetComponent('UIScrollView')
	self.uiRightPos = self.transform:FindChild('Window/Right/Pos')

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIGiftBag:Start( ... )
	local closeBtn = getLuaComponent(self.uiBtnClose)
	closeBtn.onClick = self:OnCloseClick()
	-- local backBtn = getLuaComponent(self.uiBackBtn)
	-- backBtn.onClick = self:OnCloseClick()
	-- local menu = getLuaComponent(self.uiBtnMenu)
	-- menu:SetParent(self.gameObject, false)
	-- menu.parentScript = self

	self:InitGiftButton()

	-- self.config = GameSystem.Instance.VipPrivilegeConfig
	-- self:UpdateCost()
	-- self:InitVIPGiftItem()
	self:InitWelfareContent()

	--init vipgift button press
	-- local vipGiftBtn = self.uiLeftGrid.transform:GetChild(4):GetComponent('UIToggle')
	-- vipGiftBtn.startsActive = true
	local welfareBtn = self.uiLeftGrid.transform:GetChild(0):GetComponent('UIToggle')
	welfareBtn.startsActive = true

	--MainPlayer.Instance:AddDataChangedDelegate(self:OnVipChanged(), self.uiName)
	-- LuaHelper.RegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID, self:BuyVipGiftResp(), self.uiName)
end

--固定更新
function UIGiftBag:FixedUpdate( ... )
	self:GetAwardBtnRefresh(UpdateRedDotHandler.UpdateState["UIGiftBag"])
	self:RefreshGiftButtonState()
end

function UIGiftBag:OnClose( ... )
	NGUITools.Destroy(self.gameObject)
	if self.onClose then self.onClose() end
	-- if self.nextShowUI then
	-- 	TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
	-- 	self.nextShowUI = nil
	-- else
	-- 	TopPanelManager:HideTopPanel()
	-- end
end

function UIGiftBag:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIGiftBag:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIGiftBag:Refresh( ... )
	-- local menu = getLuaComponent(self.uiBtnMenu)
	-- menu:Refresh()
end


-----------------------------------------------------------------
function UIGiftBag:RefreshGiftButtonState( ... )
	--print('self.HpItem.name------' .. self.HpItem.uiTitle.transform:GetComponent('UILabel').text)
	if self.HpItem then
		self.HpItem:SetTipState(UpdateRedDotHandler.UpdateState["UIGiftBag"])
	end
end

function UIGiftBag:SetParent(parent)
	self.parent = parent
end

--关闭
function UIGiftBag:OnCloseClick( ... )
	return function(go)
		self:DoClose()
	end
end

function UIGiftBag:SetModelActive(active)
	-- body
end

function UIGiftBag:InitGiftButton()
	CommonFunction.ClearGridChild(self.uiLeftGrid.transform)
	--local giftType = {'DAILY_WELFARE', 'STR_INIT_GIFT', 'STR_ONLINE_GIFT', 'STR_LEVEL_GIFT', 'STR_VIP_GIFT', 'STR_RECHARGE_GIFT'}
	local giftType = {'DAILY_WELFARE'}--, 'COME_SOON', 'COME_SOON', 'COME_SOON', 'COME_SOON'}
	for i=1,table.getn(giftType) do
		local go = createUI('GiftButton', self.uiLeftGrid.transform)
		local giftBtn = getLuaComponent(go)
		giftBtn:SetTitle(getCommonStr(giftType[i]))
		giftBtn.uiSele:GetComponent("UISprite").color = Color.New(1,1,1,0)
		go.transform:GetComponent("UIToggle").enabled = false
		giftBtn.onClick = self:RefreshRightContent()
		if giftType[i] == 'DAILY_WELFARE' then
			self:RefreshRightContent()(giftType[i])
			go.transform:GetComponent("UIToggle").enabled = true
			go.transform:GetComponent("UIToggle").value = true
		end
	end
	self.uiLeftGrid.repositionNow = true
	self.uiLeftGrid:Reposition()
end

function UIGiftBag:RefreshRightContent()
	return function (name)
		if name == getCommonStr('COME_SOON') then
			return
		end

		--delete
		while self.uiRightPos.childCount > 0 do
			NGUITools.Destroy(self.uiRightPos:GetChild(0).gameObject)
		end
		self.uiGetAwardBtn = nil
		CommonFunction.ClearGridChild(self.uiRightGrid.transform)
		
		--if name == getCommonStr('STR_VIP_GIFT') then
			--self:InitVIPGiftItem()
		--else
		if name == getCommonStr('DAILY_WELFARE') then
			self:InitWelfareContent()
		end
	end
end

function UIGiftBag:InitVIPGiftItem( ... )
	for i = 1,self.VIP_MAX_LEVEL do
		local vipData = self.config:GetVipData(i)
		local vipGift = getLuaComponent(createUI('VIPGiftItem', self.uiRightGrid.transform))
		vipGift:set_data(vipData.gift,i,vipData.gift_price,self.vipLevel)
		vipGift.on_click = self:ClickVipGiftItem()
	end
	self.uiRightGrid.repositionNow = true
	self.uiRightGrid:Reposition()
end

function UIGiftBag:InitWelfareContent( ... )
	local welfare = createUI('DailyWelfare', self.uiRightPos.transform)
	UIManager.Instance:BringPanelForward(welfare.gameObject)
	local goods = getLuaComponent(welfare.transform:FindChild('GoodsIconConsume'))
	local hpID = 4
	local hpNum = GameSystem.Instance.presentHpConfigData:GetHP()
	goods:SetData(hpID, hpNum, false)
	local tips = welfare.transform:FindChild('Tip'):GetComponent('UILabel')
	tips.text = '每日' .. GameSystem.Instance.presentHpConfigData:GetTimeInterval() .. '可领取' .. GameSystem.Instance.presentHpConfigData:GetHP() .. '点体力'

	self.uiGetAwardBtn = welfare.transform:FindChild('ButtonOK'):GetComponent('UIButton')
	NGUITools.SetActive(self.uiGetAwardBtn.gameObject, false)
	addOnClick(self.uiGetAwardBtn.gameObject, self:OnGetAward())
end

function UIGiftBag:GetAwardBtnRefresh(state)
	if self.uiGetAwardBtn then
		NGUITools.SetActive(self.uiGetAwardBtn.gameObject, state)
	end
end

function UIGiftBag:OnVipChanged(...)
	return function(...)
		self:UpdateCost()
	end
end

function UIGiftBag:UpdateCost()
	self.cost = MainPlayer.Instance.VipExp
	local level = 0
	local cost = self.cost
	for i=1,15 do
		local vipData = self.config:GetVipData(i)
		if vipData.consume <= cost then
			level = i
		else
			break
		end
	end
	self.vipLevel = level
end

function UIGiftBag:ClickVipGiftItem( ... )
	return function(item)
		local operation = {
			type = "BUY_GIFT",
			vipLev = item.vip
		}
		self.currentVipLevel = item.vip
		self.currentItemID = item.id
		local req = protobuf.encode("fogs.proto.msg.PlayerVipOperation",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.PlayerVipOperationID,req)
	end
end

-- function UIGiftBag:BuyVipGiftResp( ... )
-- 	return function (buf)
-- 		local resp, err = protobuf.decode("fogs.proto.msg.PlayerVipOperationResp", buf)
-- 		if resp then
-- 			print("reslt = "..tostring(resp.result))
-- 			if resp.result == 0 then
-- 				local level = self.currentVipLevel
-- 				print("level="..tostring(level))
-- 				MainPlayer.Instance.VipGifts:Add(level)

-- 				local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
-- 				getGoods:SetGiftGoodsData(self.currentItemID)

-- 			else
-- 				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
-- 			end
-- 		else
-- 			error("s_handle_invite_role(): " .. err)
-- 		end
-- 	end
-- end

--点击领取按钮
function UIGiftBag:OnGetAward( ... )
	return function(go)
		local req = 
		{
			acc_id = MainPlayer.Instance.AccountID
		}
		local buf = protobuf.encode('fogs.proto.msg.GetFreeHpReq', req)
		if UpdateRedDotHandler.UpdateState["UIGiftBag"] == true then
			LuaHelper.SendPlatMsgFromLua(MsgID.GetFreeHpReqID, buf)
			LuaHelper.RegisterPlatMsgHandler(MsgID.GetFreeHpRespID, self:OnGetFreeHpRespHandler(), self.uiName)
			CommonFunction.ShowWait()
		end
	end
end

--处理回复消息
function UIGiftBag:OnGetFreeHpRespHandler( ... )
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetFreeHpRespID, self.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode('fogs.proto.msg.GetFreeHpResp', message)
		if resp ~= nil then
			print('resp.result =====>>>>>>' .. tostring(resp.result))
			if resp.result ~= 0 then
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
			MainPlayer.Instance.MoonHp = resp.noon_hp
			MainPlayer.Instance.EvenHp = resp.even_hp
			MainPlayer.Instance.ThirdHp = resp.three_times_hp
			CommonFunction.ShowPopupMsg(getCommonStr('RECEIVE_SUCCESS'),nil,nil,nil,nil,nil)
		else
			error("UIActivity:", err)
		end

	end
end

return UIGiftBag

BuyNumericWindow = {
	uiName = 'BuyNumericWindow',
	BuyType,
	
	CostText,
	GetText,
	Arrow,
	CostValue, 
	GetValue,
	GetTypeIcon,
	CostTypeIcon,
	Times,
	ResultLable,
}

--
function BuyNumericWindow:Awake()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiFrame.showCorner = true
	self.uiFrame.BUTTON_COUNT = 1
	self.uiFrame.buttonTypes = {Normal}
	self.uiFrame.buttonLabels = {CommonFunction.GetConstString("BUY_CONVERT")}
	self.uiFrame.onClose = self:click_close()
	self.uiFrame.buttonHandlers = {self:Buy_handler()}
	self.GetValue = getComponentInChild(self.transform,"BuyInfo/GetLb/GetValue","UILabel")
	self.GetText = getComponentInChild(self.transform,"BuyInfo/GetLb/GetText","UILabel")
	self.GetTypeIcon = getComponentInChild(self.transform,"BuyInfo/GetLb/GetTypeIcon","UISprite")
	self.CostValue = getComponentInChild(self.transform,"BuyInfo/CostLb/CostValue","UILabel")
	self.CostText = getComponentInChild(self.transform,"BuyInfo/CostLb/CostText","UILabel")
	self.CostTypeIcon = getComponentInChild(self.transform,"BuyInfo/CostLb/CostTypeIcon","UISprite")
	self.Arrow = getComponentInChild(self.transform,"BuyInfo/Arrow","UISprite")
	self.TimesInfo = getComponentInChild(self.transform,"BuyInfo/TimeLb/TimesInfo","UILabel")
	self.Times = getComponentInChild(self.transform,"BuyInfo/TimeLb/Times","UILabel")
	self.ResultLable = getComponentInChild(self.transform,"ResultLable","UILabel")
	self.costlabel = getComponentInChild(self.transform,"BuyInfo/Label","UILabel")
	NGUITools.SetActive(self.ResultLable.gameObject,false)
	self.GetText.text = CommonFunction.GetConstString("BUY_CONVERT")
	self.CostText.text = CommonFunction.GetConstString("BUY_COST")
	self.TimesInfo.text = CommonFunction.GetConstString("CURRENT_BUY_COUNT")		
	LuaHelper.RegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID,self:PlayerVipOperationResp(),self.uiName)
	
end

--
function BuyNumericWindow:Start()
	--print('---------------BuyType:', self.BuyType)
	if self.BuyType == 'BUY_HP' then
		self.uiFrame.title = CommonFunction.GetConstString("STORE_BUY_HP_USE_DIAMOND")
		self.GetTypeIcon.spriteName = "com_property_hp"
		self.costlabel.text = CommonFunction.GetConstString("USE_DIAMONDS_BUY")..CommonFunction.GetConstString("HP")
	elseif self.BuyType == 'BUY_GOLD' then
		self.uiFrame.title = CommonFunction.GetConstString("STORE_BUY_GOLD_USE_DIAMOND")
		self.GetTypeIcon.spriteName = "com_property_gold"
		self.costlabel.text = CommonFunction.GetConstString("USE_DIAMONDS_BUY")..CommonFunction.GetConstString("GOLD")
	else
		self.uiFrame.title = CommonFunction.GetConstString("STORE_BUY_GOLD_USE_DIAMOND")
		self.GetTypeIcon.spriteName = "com_property_gold"
	end	
	self:UI_Refresh()	
end

--消息发送	
function BuyNumericWindow:Buy_handler()
	return function()
		--print('1111111111111111111-------handler---BuyType:'..self.BuyType)
		local buyTimes = 0
		local maxTimes = 0
		local buyData
		if self.BuyType == 'BUY_GOLD' then 
			buyTimes = MainPlayer.Instance.GoldBuyTimes + 1
			buyData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyGoldDataByTimes(buyTimes)
		elseif self.BuyType == 'BUY_HP' then
			buyTimes = MainPlayer.Instance.Hp_Buy_Times + 1
			buyData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyHpDataByTimes(buyTimes)
		end
		--print('1111111111111111111111111:',buyData)	
		if buyData then
			if MainPlayer.Instance:GetGoodsCount(GlobalConst.DIAMOND_ID) >= buyData.diamond_need then
				local operation = {
					type = self.BuyType
				}
				local req = protobuf.encode("fogs.proto.msg.PlayerVipOperation",operation)
				LuaHelper.SendPlatMsgFromLua(MsgID.PlayerVipOperationID,req)
			else
				-- CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STORE_BUY_DIAMOND"),nil,nil,nil,nil,nil)
				self.msg = CommonFunction.ShowPopupMsg("XXXXXX", nil, LuaHelper.VoidDelegate(self:ShowBuyDiamond()), LuaHelper.VoidDelegate(self:FramClickClose()),getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL"))				
				NGUITools.Destroy(self.gameObject)
			end
		else
			print("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("ERROR_CONFIGURATION"),nil,nil,nil,nil,nil)
			NGUITools.Destroy(self.gameObject)
		end
	end	
end

function BuyNumericWindow:ShowBuyDiamond()
	return function()
		TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
	end
end

function BuyNumericWindow:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end
end

--消息接收
function BuyNumericWindow:PlayerVipOperationResp()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.PlayerVipOperationResp", buf)
		if resp then
			if resp.result == 0 then
				self.BuyType = resp.type
				if self.BuyType == 'BUY_GOLD' then
					MainPlayer.Instance.GoldBuyTimes = resp.times
				elseif self.BuyType == "BUY_HP" then
					MainPlayer.Instance.Hp_Buy_Times = resp.times
				end
				self:UI_Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_switch_captain():" .. err)
		end
	end
end

--界面刷新
function BuyNumericWindow:UI_Refresh()	
	UIManager.Instance:BringPanelForward(self.gameObject)
	local level = self:get_vip()
	local totalTimes = 0
	local buyTimes = 0
	if self.BuyType == 'BUY_GOLD' then
		totalTimes = GameSystem.Instance.VipPrivilegeConfig:GetBuygold_times(level)
		buyTimes = MainPlayer.Instance.GoldBuyTimes
	elseif self.BuyType == 'BUY_HP' then
		totalTimes = GameSystem.Instance.VipPrivilegeConfig:GetBuyhp_times(level)
		buyTimes = MainPlayer.Instance.Hp_Buy_Times
	else
		print('error!!!!!')
		return
	end
	--print(' =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  = '..leftTimes..'/'..totalTimes)
	print('buyTimes: ', buyTimes)
	print('totalTimes: ', totalTimes)
	self.Times.text = tostring(buyTimes)..'/'..tostring(totalTimes)
	if buyTimes >= totalTimes then
		NGUITools.SetActive(self.transform:FindChild('BuyInfo').gameObject,false)
		NGUITools.SetActive(self.ResultLable.gameObject,true)
		NGUITools.Destroy(self.uiFrame.transform:FindChild("ButtonNode1").gameObject)
		self.ResultLable.text = CommonFunction.GetConstString("STORE_BUY_TIMES_NOT_ENOUGH")
	else
		local buyGoldData = BuyData.New()
		if self.BuyType == 'BUY_GOLD' then
			buyGoldData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyGoldDataByTimes(buyTimes + 1)
			if buyGoldData then
				self.CostValue.text = tostring(buyGoldData.diamond_need)
				self.GetValue.text = tostring(buyGoldData.value)
			else
				self.CostValue.text = ''
				self.GetValue.text = ''
			end
		elseif self.BuyType == 'BUY_HP' then
			buyHpData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyHpDataByTimes(buyTimes + 1)
			if buyHpData then
				self.CostValue.text = tostring(buyHpData.diamond_need)
				self.GetValue.text = tostring(buyHpData.value)
			else
				self.CostValue.text = ''
				self.GetValue.text = ''
			end
		end
	end
end

--
function BuyNumericWindow:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID, self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--
function BuyNumericWindow:click_close()
	return function()
		self:OnDestroy()
	end
end


function BuyNumericWindow:get_vip()
	local level = 0
	local config= GameSystem.Instance.VipPrivilegeConfig
	for i=1,15 do
		local vip_data = config:GetVipData(i)
		if vip_data.consume <= MainPlayer.Instance.VipExp then
			level = i
		else
			break
		end
	end
	return level
end

return BuyNumericWindow


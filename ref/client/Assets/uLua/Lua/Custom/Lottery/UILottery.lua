--encoding=utf-8

UILottery = {
    uiName = "UILottery",

    ---------------------
    --parent,
    type,
    count,
    isFree1,
    isFree2,
    isFake = false,
    osTime,
    freeTimes1,
    totalFreeTimes1,
    lastElapsedTime1,
    restoreTime1,
    freeTimes2,
    totalFreeTimes2,
    lastElapsedTime2,
    restoreTime2,
    loopTimes1,
    loopTimes2,

    nextShowUI,
    nextShowUISubID,
    nextShowUIParams,

    newPlayerID = {},
    goldSingleCost,
    goldMultiCost,
    diamondSingleCost,
    diamondMultiCost,
    lastCost,
    lastFree,
    banTwice = false,
    isVipPopup = false,
    onClose,
    refreshCallBack = nil,
    -------------------UI

    uiBtnBack,
    uiLeftBtnOne,
    uiLeftBtnOneConsume,
    uiLeftBtnOneRedDot,
    uiLeftBtnTen,
    uiLeftBtnTenConsume,
    uiRightBtnOne,
    uiRightBtnOneConsume,
    uiRightBtnOneRedDot,
    uiRightBtnTen,
    uiRightBtnTenConsume,

    uiGoldFreeTimes,
    uiDiamondFreeTimes,
    uiGoldTip,
    uiDiamondTip,
    uiPaint1,
    uiPaint2,
    uiResultPane,
    uiBuyGoldNum,
    uiBuyDiamondNum,
    -- uiLeftRedDot,
    -- uiRightRedDot,
    uiPaintAnimator,
    uiAnimator,
}


-----------------------------------------------------------------
function UILottery:Awake()
    self.uiBtnBack = createUI('ButtonBack',self.transform:FindChild('Top/ButtonBack'))
    self.uiLeftBtnOne = self.transform:FindChild('Left/LotteryPane1turn/ButtonOne'):GetComponent('UIButton')
    self.uiLeftBtnTen = self.transform:FindChild('Left/LotteryPane1turn/ButtonTen'):GetComponent('UIButton')
    self.uiRightBtnOne = self.transform:FindChild('Right/LotteryPane2turn/ButtonOne'):GetComponent('UIButton')
    self.uiRightBtnTen = self.transform:FindChild('Right/LotteryPane2turn/ButtonTen'):GetComponent('UIButton')
    self.uiLeftBtnOneRedDot = self.transform:FindChild('Left/LotteryPane1turn/ButtonOne/RedDot'):GetComponent('UISprite')
    self.uiRightBtnOneRedDot = self.transform:FindChild('Right/LotteryPane2turn/ButtonOne/RedDot'):GetComponent('UISprite')
    self.uiLeftBtnOneConsume = createUI("GoodsIconConsume", self.transform:FindChild('Left/LotteryPane1turn/ButtonOne/GoodsIconConsume'))
    self.uiLeftBtnTenConsume = createUI("GoodsIconConsume", self.transform:FindChild('Left/LotteryPane1turn/ButtonTen/GoodsIconConsume'))
    self.uiRightBtnOneConsume = createUI("GoodsIconConsume", self.transform:FindChild('Right/LotteryPane2turn/ButtonOne/GoodsIconConsume'))
    self.uiRightBtnTenConsume = createUI("GoodsIconConsume", self.transform:FindChild('Right/LotteryPane2turn/ButtonTen/GoodsIconConsume'))

    self.uiGoldFreeTimes = self.transform:FindChild('Left/LotteryPane1turn/FreeTimes'):GetComponent('UILabel')
    self.uiDiamondFreeTimes = self.transform:FindChild('Right/LotteryPane2turn/FreeTimes'):GetComponent('UILabel')
    self.uiGoldTip = self.transform:FindChild('Left/LotteryPane1turn/TipOne'):GetComponent('UILabel')
    self.uiDiamondTip = self.transform:FindChild('Right/LotteryPane2turn/TipOne'):GetComponent('UILabel')
    self.uiPaint1 = self.transform:FindChild('Paint/Paint1')
    self.uiPaint2 = self.transform:FindChild('Paint/Paint2')
    self.uiBuyGoldNum = self.transform:FindChild('Left/LotteryPane1turn/Num'):GetComponent('UILabel')
    self.uiBuyDiamondNum = self.transform:FindChild('Right/LotteryPane2turn/Num'):GetComponent('UILabel')
    -- self.uiLeftRedDot = self.transform:FindChild('Left/LotteryPane1/TipIcon'):GetComponent('UISprite')
    -- self.uiRightRedDot = self.transform:FindChild('Right/LotteryPane2/TipIcon'):GetComponent('UISprite')

    self.uiPaintAnimator = self.transform:FindChild('Paint'):GetComponent('Animator')
    self.uiAnimator = self.transform:GetComponent('Animator')
end

function UILottery:Start()
    -- self.osTime = GameSystem.mTime
    local backLua = getLuaComponent(self.uiBtnBack)
    backLua.onClick = self:OnBack()
    addOnClick(self.uiLeftBtnOne.gameObject, self:OnBuyClick())
    addOnClick(self.uiLeftBtnTen.gameObject, self:OnBuyClick())
    addOnClick(self.uiRightBtnOne.gameObject, self:OnBuyClick())
    addOnClick(self.uiRightBtnTen.gameObject, self:OnBuyClick())

    for i=1,2 do
        local lottery = GameSystem.Instance.LotteryConfig:GetLottery(i, MainPlayer.Instance.Level)
        if i == 1 then
            self.goldSingleCost = lottery.consume_num_single
            self.goldMultiCost = lottery.consume_num_multi
        elseif i == 2 then
            self.diamondSingleCost = lottery.consume_num_single
            self.diamondMultiCost = lottery.consume_num_multi
        end
    end
    local consumeLua1 = getLuaComponent(self.uiLeftBtnOneConsume.gameObject)
    local consumeLua2 = getLuaComponent(self.uiLeftBtnTenConsume.gameObject)
    local consumeLua3 = getLuaComponent(self.uiRightBtnOneConsume.gameObject)
    local consumeLua4 = getLuaComponent(self.uiRightBtnTenConsume.gameObject)
    consumeLua1.isAdd = false
    consumeLua1:SetData(2, self.goldSingleCost)
    consumeLua2.isAdd = false
    consumeLua2:SetData(2, self.goldMultiCost)
    consumeLua3.isAdd = false
    consumeLua3:SetData(1, self.diamondSingleCost)
    consumeLua4.isAdd = false
    consumeLua4:SetData(1, self.diamondMultiCost)

    LuaHelper.RegisterPlatMsgHandler(MsgID.BuyLotteryRespID, self:MakeBuyHandler(), self.uiName)
end

function UILottery:OnDestroy( ... )
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyLotteryRespID, self.uiName)
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.uiPaintAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UILottery:FixedUpdate( ... )
    self.osTime = self.osTime + UnityTime.fixedDeltaTime
    if self.isFake then return end

    local hasFreeTimes1 = self.freeTimes1 > 0 or self.totalFreeTimes1 == 0
    self.lastElapsedTime1 = self.osTime - self.lastTime1
    if self.restoreTime1 > self.lastElapsedTime1 then
        local tip = getCommonStr("STR_FREE_COUNT_DOWN"):format(self.GetTimeStr(self.restoreTime1 - self.lastElapsedTime1))
        LotteryTime = self.restoreTime1 - self.lastElapsedTime1
        self.uiGoldFreeTimes.text = tip
        self.uiGoldTip.text = ''
        NGUITools.SetActive(self.uiLeftBtnOneRedDot.gameObject, false)
        NGUITools.SetActive(self.uiLeftBtnOneConsume.gameObject, true)
    else
        -- self.uiGoldFreeTimes.text = getCommonStr("STR_REMAIN_FREE_TIMES_TODAY"):format(self.freeTimes1, self.totalFreeTimes1)
        self.uiGoldTip.text = getCommonStr('FREE')
        NGUITools.SetActive(self.uiLeftBtnOneRedDot.gameObject, true)
        NGUITools.SetActive(self.uiLeftBtnOneConsume.gameObject, false)
        self:Refresh()
    end

    self.lastElapsedTime2 = self.osTime - self.lastTime2
    if self.restoreTime2 > self.lastElapsedTime2 then
        local tip = getCommonStr("STR_FREE_COUNT_DOWN"):format(self.GetTimeStr(self.restoreTime2 - self.lastElapsedTime2))
        -- LotteryTime = self.restoreTime2 - self.lastElapsedTime2
        self.uiDiamondFreeTimes.text = tip
        self.uiDiamondTip.text = ''
        NGUITools.SetActive(self.uiRightBtnOneRedDot.gameObject, false)
        NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, true)
    else
        -- self.uiGoldFreeTimes.text = getCommonStr("STR_REMAIN_FREE_TIMES_TODAY"):format(self.freeTimes1, self.totalFreeTimes1)
        self.uiDiamondTip.text = getCommonStr('FREE')
        NGUITools.SetActive(self.uiRightBtnOneRedDot.gameObject, true)
        NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, false)
        self:Refresh()
    end
    -- self.freeTimes2 = MainPlayer.Instance.LotteryInfo["free_times2"]
    -- local tab  = os.date("*t", self.osTime)
    -- local hour = tab.hour
    -- local min = tab.min
    -- local sec = tab.sec
    -- print(hour,":",min,":",sec)
    -- if hour < 4 then
    --  hour = hour + 24
    -- end
    -- print('hour = ', hour)
    -- if self.freeTimes2 < 1 then
    --  local tip = getCommonStr("STR_FREE_COUNT_DOWN"):format(self.GetTimeStr(28 * 3600 - hour * 3600 - min * 60 - sec))
    --  self.uiDiamondFreeTimes.text = tip
    --  self.uiDiamondTip.text = ''
    --  NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, true)
    -- else
    --  self.uiDiamondTip.text = getCommonStr('FREE')
    --  NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, false)
    --  self:Refresh()
    -- end
end

function UILottery:OnClose()
    self.osTime = GameSystem.mTime
    if self.uiResultPane ~= nil then
        NGUITools.Destroy(self.uiResultPane.gameObject)
        self.uiResultPane = nil
        self.uiPaintAnimator:SetTrigger("Paint_Back")
    end
    if self.onClose then
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

function UILottery:DoClose()
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
end

function UILottery:Refresh()
    self.osTime = GameSystem.mTime
    --self.isFake = (MainPlayer.Instance.CreateStep == 2 or MainPlayer.Instance.CreateStep == 3)

    self.restoreTime1 = GameSystem.Instance.CommonConfig:GetUInt('gNormalLotteryRestoreTime')
    self.totalFreeTimes1 = GameSystem.Instance.CommonConfig:GetUInt('gNormalLotteryFreeTimes')
    local lotteryInfo = MainPlayer.Instance.LotteryInfo
    self.freeTimes1 = lotteryInfo["free_times1"]
    self.lastTime1 = lotteryInfo["last_time1"]
    self.loopTimes1 = lotteryInfo["total_times1"]
    self.loopTimes2 = lotteryInfo["total_times2"]

    self.lastElapsedTime1 = self.osTime - self.lastTime1
    local hasFreeTimes1 = self.freeTimes1 > 0 or self.totalFreeTimes1 == 0
    if hasFreeTimes1 and self.lastElapsedTime1 >= self.restoreTime1 then
        self.isFree1 = true
        -- self.uiGoldFreeTimes.text = getCommonStr("STR_REMAIN_FREE_TIMES_TODAY"):format(self.freeTimes1, self.totalFreeTimes1)
        self.uiGoldTip.text = getCommonStr('FREE')
        NGUITools.SetActive(self.uiLeftBtnOneConsume.gameObject, false)
    else
        self.isFree1 = false
        self.uiGoldTip.text = ''
        NGUITools.SetActive(self.uiLeftBtnOneConsume.gameObject, true)
    end
    if self.freeTimes1 <= 0 then
        -- self.uiGoldFreeTimes.text = getCommonStr('STR_NO_FREE_TODAY')
    end

    self.restoreTime2 = GameSystem.Instance.CommonConfig:GetUInt('gSpecialLotteryRestoreTime')
    self.totalFreeTimes2 = GameSystem.Instance.CommonConfig:GetUInt('gSpecialLotteryFreeTimes')
    local goldLoopTimes = GameSystem.Instance.CommonConfig:GetUInt('gNormalLotteryPollingTimes')
    local diamondLoopTimes = GameSystem.Instance.CommonConfig:GetUInt('gSpecialLotteryPollingTimes')
    local lotteryInfo = MainPlayer.Instance.LotteryInfo
    self.freeTimes2 = lotteryInfo["free_times2"]
    self.lastTime2 = lotteryInfo["last_time2"]
    self.lastElapsedTime2 = self.osTime - self.lastTime2
    if self.freeTimes2 >= 1 then
        self.isFree2 = true
        self.uiDiamondTip.text = getCommonStr('FREE')
        NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, false)
    else
        self.isFree2 = false
        self.uiDiamondTip.text = ""
        NGUITools.SetActive(self.uiRightBtnOneConsume.gameObject, true)
    end
    -- print('goldLoopTimes = ', goldLoopTimes)
    -- print('diamondLoopTimes = ', diamondLoopTimes)
    self.uiBuyGoldNum.text = goldLoopTimes - (self.loopTimes1%goldLoopTimes)
    self.uiBuyDiamondNum.text = diamondLoopTimes - (self.loopTimes2%diamondLoopTimes)
end


-----------------------------------------------------------------
function UILottery:OnBuyClick( ... )
    return function (go)
        if go == self.uiLeftBtnOne.gameObject then
            if not FunctionSwitchData.CheckSwith(FSID.store_gold1) then return end

            self.type = 1
            self.count = 1
            self:MakeOnBuy(self.type,self.count,self.isFree1)()
        elseif go == self.uiLeftBtnTen.gameObject then
            if not FunctionSwitchData.CheckSwith(FSID.store_gold10) then return end

            self.type = 1
            self.count = 10
            self:MakeOnBuy(self.type,self.count,false)()
        elseif go == self.uiRightBtnOne.gameObject then
            if not FunctionSwitchData.CheckSwith(FSID.store_diamond1) then return end

            self.type = 2
            self.count = 1
            self:MakeOnBuy(self.type,self.count,self.isFree2)()
        elseif go == self.uiRightBtnTen.gameObject then
            if not FunctionSwitchData.CheckSwith(FSID.store_diamond10) then return end

            self.type = 2
            self.count = 10
            self:MakeOnBuy(self.type,self.count,false)()
        end
    end
end

function UILottery:MakeOnBuy(type, count, isFree)
    return function ()
        print(self.uiName,"-----:banTwice:",self.banTwice)
        --避免重复点击
        -- if self.banTwice == true then
        --  return
        -- end
        -- self.banTwice = true
        -- local info = self.uiPaintAnimator:GetCurrentAnimatorStateInfo(0)
        -- if info.normalizedTime < 1 then
        --  return
        -- end

        --判断消耗
        local cost = 0
        if type == 1 then
            if count == 1 then
                cost = self.goldSingleCost
                if MainPlayer.Instance.Gold < cost and self.uiGoldTip.text ~= getCommonStr('FREE') then
                    self:ShowBuyTip("BUY_GOLD")
                    return
                end
            elseif count == 10 then
                cost = self.goldMultiCost
                if MainPlayer.Instance.Gold < cost then
                    self:ShowBuyTip("BUY_GOLD")
                    return
                end
            end
        elseif type == 2 then
            local Diamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if count == 1 then
                cost = self.diamondSingleCost
                if Diamond < cost and self.uiDiamondTip.text ~= getCommonStr('FREE') then
                    self:ShowBuyTip("BUY_DIAMOND")
                    return
                end
            elseif count == 10 then
                cost = self.diamondMultiCost
                if Diamond < cost then
                    self:ShowBuyTip("BUY_DIAMOND")
                    return
                end
            end
        end

        if self.uiResultPane ~= nil and not self.isVipPopup then
            if self.uiPaintAnimator then
                self.uiPaintAnimator:SetTrigger("Paint_Back")
            end
        end

        local req = {
            type = type,
            count = count,
            is_free = isFree and 1 or 0,
        }
        print(type,count,isFree)
        self.lastFree = isFree
        if type == 1 then		-- gold
            if count == 1 then	-- single
                self.lastCost = self.goldSingleCost
            else				-- multi
                self.lastCost = self.goldMultiCost
            end
        elseif type == 2 then	-- diamond
            if count == 1 then	-- single
                self.lastCost = self.diamondSingleCost
            else				-- multi
                self.lastCost = self.diamondMultiCost
            end
        end

        local buf = protobuf.encode("fogs.proto.msg.BuyLotteryReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.BuyLotteryReqID, buf)
        CommonFunction.ShowWait()
        if type == 1 then
            if count == 1 and MainPlayer.Instance.Gold >= self.goldSingleCost then
                CommonFunction.ShowWaitMask()
            elseif count == 10 and MainPlayer.Instance.Gold >= self.goldMultiCost then
                CommonFunction.ShowWaitMask()
            end
        elseif type == 2 then
            local Diamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if count == 1 and Diamond >= self.diamondSingleCost then
                CommonFunction.ShowWaitMask()
            elseif count == 10 and Diamond>= self.diamondMultiCost then
                CommonFunction.ShowWaitMask()
            end
        end
    end
end

function UILottery:MakeOnRebuy( ... )
    return function (type, count, isFree)
        --判断消耗
        local cost = 0
        if type == 1 then
            if count == 1 then
                cost = self.goldSingleCost
            elseif count == 10 then
                cost = self.goldMultiCost
            end
            if MainPlayer.Instance.Gold < cost then
                self:ShowBuyTip("BUY_GOLD")
                return
            end
        elseif type == 2 then
            if count == 1 then
                cost = self.diamondSingleCost
            elseif count == 10 then
                cost = self.diamondMultiCost
            end
            local Diamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if Diamond < cost then
                self:ShowBuyTip("BUY_DIAMOND")
                return
            end
        end

        self.banTwice = false
        local req = {
            type = type,
            count = count,
            is_free = isFree and 1 or 0,
        }
        print(type,count,isFree)
        local buf = protobuf.encode("fogs.proto.msg.BuyLotteryReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.BuyLotteryReqID, buf)
        CommonFunction.ShowWait()
        if type == 1 then
            if count == 1 and MainPlayer.Instance.Gold >= self.goldSingleCost then
                CommonFunction.ShowWaitMask()
            elseif count == 10 and MainPlayer.Instance.Gold >= self.goldMultiCost then
                CommonFunction.ShowWaitMask()
            end
        elseif type == 2 then
            local Diamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if count == 1 and Diamond >= self.diamondSingleCost then
                CommonFunction.ShowWaitMask()
            elseif count == 10 and Diamond>= self.diamondMultiCost then
                CommonFunction.ShowWaitMask()
            end
        end
        --CommonFunction.ShowWaitMask()
    end
end

function UILottery:MakeBuyHandler()
    return function (buf)
        CommonFunction.HideWaitMask()
        CommonFunction.StopWait()
        -- if self.banTwice then
        --  return
        -- end
        local resp, err = protobuf.decode("fogs.proto.msg.BuyLotteryResp", buf)
        if resp then
            if resp.result == 0 then
                if self.uiPaintAnimator then
                    if self.uiResultPane ~= nil then
                        NGUITools.Destroy(self.uiResultPane.gameObject)
                        self.uiResultPane = nil
                    end
                end
                self:HandleBuy(resp)
                -- self.banTwice = true
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                self.banTwice = false
            end
        else
            error("decode error:", err)
            self.banTwice = false
        end
    end
end

function UILottery:HandleBuy(resp)
    MainPlayer.Instance.LotteryInfo["free_times" .. resp.type] = resp.free_times
    MainPlayer.Instance.LotteryInfo["last_time" .. resp.type] = resp.last_time
    MainPlayer.Instance.LotteryInfo["total_times" .. resp.type] = MainPlayer.Instance.LotteryInfo["total_times" .. resp.type] + resp.count

    if not self.lastFree then
        local str = self.type == 1 and "Gold" or "Diamond"
        MainPlayer.Instance:ConfirmCommonBuy("Lottery"..str, self.count, self.lastCost/self.count)
    end

    if resp.count == 1 then
        self.uiPaintAnimator:SetTrigger('Trigger_Paint')
    else
        self.uiPaintAnimator:SetTrigger('Paint2')
    end
    self.isVipPopup = false

    self.uiResultPane = getLuaComponent(createUI("LotteryResultPopup"))
    self.uiResultPane.type = resp.type
    self.uiResultPane.count = resp.count
    self.uiResultPane.awards = resp.prizes
    self.uiResultPane.onRebuy = self:MakeOnRebuy()
    -- self.uiResultPane.onClose = function ( ... )
    --  if self.uiPaintAnimator then
    --      self.uiPaintAnimator:SetTrigger("Paint_Back")
    --  end
    --  NGUITools.SetActive(self.uiPaint1.gameObject, false)
    --  NGUITools.SetActive(self.uiPaint2.gameObject, false)
    --  NGUITools.Destroy(self.uiResultPane.gameObject)
    --  self.banTwice = false
    -- end

    UIManager.Instance:BringPanelForward(self.uiResultPane.gameObject)
    for _,v in ipairs(resp.prizes) do
        if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id) then
            local popup = getLuaComponent(createUI("RoleAcquirePopup"))
            popup:SetData(v.id)
            local list = MainPlayer.Instance:GetRoleIDList()
            if list.Count <= 3 and self.isFake == true then
                popup.IsInClude = false
            else
                popup.IsInClude = self:CheckRoleInclude(v.id)
                if self.newPlayerID then
                    for i,m in ipairs(self.newPlayerID) do
                        if m == v.id then
                            popup.IsInClude = false
                            break
                        end
                    end
                end

                print('IsInClude == ' .. tostring(popup.IsInClude))
                if popup.IsInClude then
                    local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id).name
                    local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id).recruit_output_value
                    popup.contentStr = string.format(getCommonStr('STR_ROLE_LOTTERY_AWARDS'), roleName, roleName, Num)
                end
            end
            UIManager.Instance:BringPanelForward(popup.gameObject)
        end
    end
    self.newPlayerID = {}
    self:Refresh()
end

function UILottery:OnBack()
    return function ( ... )
        self:DoClose()
        if self.refreshCallBack ~= nil then
            self:refreshCallBack()
            self.refreshCallBack = nil
        end
    end
end

function UILottery.GetTimeStr(seconds)
    local h = math.floor(seconds / 3600)
    local m = math.floor(seconds % 3600 / 60)
    local s = math.floor(seconds % 60)
    return string.format("%02d:%02d:%02d", h, m, s)
end

function UILottery:SetModelActive(active)
    -- body
end

function UILottery:CheckRoleInclude(id)
    print("include id :",id)
    return MainPlayer.Instance:HasRole(id)
end

function UILottery:GetNewRole(id)
    table.insert(self.newPlayerID, id)
end

function UILottery:ShowBuyTip(type)
    local str
    if type == "BUY_GOLD" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
    elseif type == "BUY_DIAMOND" then
        str = getCommonStr("YOUR_DIMAND_LACK_AND_SWITCH_DISABLE")
    elseif type == "BUY_HP" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
    end

    if type == "BUY_DIAMOND" then
        self.msg = CommonFunction.ShowTip(str, nil)
    else
        self.msg = CommonFunction.ShowPopupMsg(str, nil,
                                            LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
                                            LuaHelper.VoidDelegate(self:FramClickClose()),
                                            getCommonStr("BUTTON_CONFIRM"),
                                            getCommonStr("BUTTON_CANCEL"))
    end
end

function UILottery:ShowBuyUI(type)
    return function()
        self.banTwice = false
        if type == "BUY_DIAMOND" then
            self.isVipPopup = true
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = type
    end
end

function UILottery:FramClickClose()
    return function()
        NGUITools.Destroy(self.msg.gameObject)
        self.banTwice = false
    end
end

return UILottery

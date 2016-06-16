------------------------------------------------------------------------
-- class name    : VIPPopup
-- create time   : Tue Nov 24 13:30:07 2015
------------------------------------------------------------------------

VIPPopup =  {
    uiName     = "VIPPopup",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    uiAnimator             ,
    uiButtonBack           ,
    uiStateChanteBtn       ,
    uiStateChangeBtnMLabel ,
    uiDiamondIcon          ,
    uiRecharge             ,
    uiNextLevelNode        ,
    uiNextLevel            ,
    uiLevel                ,
    uiRechargeNum          ,
    uiProcessNode          ,
    uiProcessBar           ,
    uiProcessNum           ,
    uiPrivilegeNode        ,
    uiRightArrow           ,
    uiLeftArrow            ,
    uiPrivilegeName        ,
    uiPrivilegeScroll      ,
    uiPrivilegeGrid        ,
    uiBuyBtn               ,
    uiBuyBtnLabel          ,
    uiGiftLabel            ,
    uiOriPrice             ,
    uiNowPrice             ,
    uiGiftGrid             ,
    uiRechargeNode         ,
    uiRechargeGrid         ,
    -----------------------
    -- Parameters Module --
    -----------------------
    vipExp          = nil,
    vip             = nil,
    state           = nil,
    config          = nil,
    vvip            = 0,
    ruleList        = nil,
    moving          = nil,
    MAX_LV          = nil,
    OFFSET_X        = 800,
    buyGiftLv       = nil,
    buyBtnState     = 1,
    moveX           = 0,
    privilegeGrid1x = 0,
    msg             = nil,
    vipIconNamePre  = nil,
    vipItems        = nil,
    isToCharge      = false,
    goodsList       = nil,
    tip             = nil,
}

local st = {
    privilege = 1,
    recharge = 2,
}



---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function VIPPopup:Awake()
    self:UiParse()				-- Foucs on UI Parse.
    self.goodsList = {}
    for i=1, 5 do
        local s = getLuaComponent(createUI("GoodsIcon", self.uiGiftGrid.transform))
        s.goodsID = 2
        self.goodsList[i] = s
    end
    self.uiGiftGrid.hideInactive = false
    self.uiGiftGrid.repositionNow = true
end


function VIPPopup:Start()
    self.vipExp = MainPlayer.Instance.VipExp
    self.vip    = MainPlayer.Instance.Vip
    self.config = GameSystem.Instance.VipPrivilegeConfig
    self.MAX_LV = self.config.maxVip

    local s = getLuaComponent(self.transform:FindChild("Top/PlayerInfoGrids"))
    s.isVip = true
    ------------------------------------
    -------  Click
    addOnClick(self.uiStateChanteBtn.gameObject, self:ClickStateChange())
    addOnClick(self.uiLeftArrow.gameObject,      self:ClickArrow(true))
    addOnClick(self.uiRightArrow.gameObject,     self:ClickArrow(false))
    addOnClick(self.uiBuyBtn.gameObject,         self:ClickBuy())
    getLuaComponent(createUI("ButtonBack", self.uiButtonBack)).onClick = self:ClickBack()

    self:SetVVip(self.vip, true)
    MainPlayer.Instance:AddDataChangedDelegate(self:OnVipChanged(), self.uiName)
end

function VIPPopup:Refresh()
    self:SetRechargeState(self.isToCharge)
    self.isToCharge = false
    self:PageReset()
    self:SetVVip(self.vip, true)
    self:DataRefresh()
end

function VIPPopup:OnDestroy()
    MainPlayer.Instance:RemoveDataChangedDelegate(self.uiName)
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.                                          --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.                                    --
-- NOTE:                                                                                         --
--	1. This function only used to parse the UI(GameObject).                                      --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.                   --
--	3. The name is according to the structure of prefab.                                         --
--	4. Please Do NOT MINDE the Comment Lines.                                                    --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.       --
---------------------------------------------------------------------------------------------------
function VIPPopup:UiParse()
    self.uiAnimator             = self.transform:GetComponent("Animator")
    self.uiButtonBack           = self.transform:FindChild("Top/ButtonBack"):GetComponent("Transform")
    self.uiStateChanteBtn       = self.transform:FindChild("Window/BackTitle/Button/Privilege/Sprite"):GetComponent("UISprite")
    self.uiStateChangeBtnMLabel = self.transform:FindChild("Window/BackTitle/Button/Privilege/Sprite/Text"):GetComponent("MultiLabel")
    self.uiDiamondIcon          = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/Icon")
    self.uiRecharge             = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/Recharge"):GetComponent("MultiLabel")
    self.uiNextLevelNode        = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/NextLevel")
    self.uiNextLevel            = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/NextLevel/LevelIcon")
    self.uiLevel                = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/LevelIcon"):GetComponent("UISprite")
    self.uiRechargeNum          = self.transform:FindChild("Window/BackTitle/VIPInfo/Tip/Num"):GetComponent("MultiLabel")
    self.uiProcessNode          = self.transform:FindChild("Window/BackTitle/VIPInfo/Process")
    self.uiProcessBar           = self.transform:FindChild("Window/BackTitle/VIPInfo/Process/Back"):GetComponent("UIProgressBar")
    self.uiProcessNum           = self.transform:FindChild("Window/BackTitle/VIPInfo/Process/Data"):GetComponent("UILabel")
    self.uiPrivilegeNode        = self.transform:FindChild("Window/Privilege"):GetComponent("Transform")
    self.uiRightArrow           = self.transform:FindChild("Window/Privilege/Right"):GetComponent("UIButton")
    self.uiLeftArrow            = self.transform:FindChild("Window/Privilege/Left"):GetComponent("UIButton")
    self.uiPrivilegeName        = self.transform:FindChild("Window/Privilege/Sprite/Up/VIPPrivilegeItem/Name"):GetComponent("UILabel")
    self.uiPrivilegeScroll      = self.transform:FindChild("Window/Privilege/Sprite/Up/VIPPrivilegeItem/Scroll"):GetComponent("UIScrollView")
    self.uiPrivilegeGrid        = self.transform:FindChild("Window/Privilege/Sprite/Up/VIPPrivilegeItem/Scroll/Grid") --:GetComponent("UIGrid")
    self.uiBuyBtn               = self.transform:FindChild("Window/Privilege/Sprite/Down/ButtonEnhance"):GetComponent("UIButton")
    self.uiBuyBtnLabel          = self.transform:FindChild("Window/Privilege/Sprite/Down/ButtonEnhance/Label"):GetComponent("MultiLabel")
    self.uiGiftLabel            = self.transform:FindChild("Window/Privilege/Sprite/Down/Label"):GetComponent("UILabel")
    self.uiOriPrice             = self.transform:FindChild("Window/Privilege/Sprite/Down/Label/OriLabel/Num"):GetComponent("UILabel")
    self.uiNowPrice             = self.transform:FindChild("Window/Privilege/Sprite/Down/Label/NowLabel/Num"):GetComponent("UILabel")
    self.uiGiftGrid             = self.transform:FindChild("Window/Privilege/Sprite/Down/Scroll2/Grid"):GetComponent("UIGrid")
    self.uiRechargeNode         = self.transform:FindChild("Window/Pay"):GetComponent("Transform")
    self.uiRechargeGrid         = self.transform:FindChild("Window/Pay/Scroll/Grid"):GetComponent("UIGrid")
end


------------------------------------
-------  Click
function VIPPopup:ClickStateChange()
    return function()
        print("stateChange self.state=",self.state)
        if self.state == st.privilege then
            self:SetRechargeState(true)
        elseif self.state == st.recharge then
            self:SetRechargeState(false)
        end
        self:DataRefresh()
    end
end

function VIPPopup:SetRechargeState(isRecharge)
    if isRecharge then
        self.state = st.recharge
        self.uiStateChangeBtnMLabel:SetText(getCommonStr("STR_PRIVILEGE"))
    else
        self.state = st.privilege
        self.uiStateChangeBtnMLabel:SetText(getCommonStr("STR_PAY"))
    end
end

function VIPPopup:ClickArrow(isLeft)
    return function()
        if isLeft then
            self:Move(false)
        else
            self:Move(true)
        end
        self:DataRefresh()
    end
end


function VIPPopup:ClickBuy()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.vip_btn) then return end

        if self.buyBtnState == 3 then
            CommonFunction.ShowTip(getCommonStr("VIP_NOT_ENOUGH_TO_BUY"), nil)
            return
        elseif self.buyBtnState == 2 then
            return
        end

        LuaHelper.RegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID, self:HandleBuyGift(), self.uiName)

        if self.config:GetVipData(self.vvip).gift_price > (MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy) then
            self.msg  = CommonFunction.ShowPopupMsg(getCommonStr("DIAMOND_NOT_ENOUGH_TO_BUY_GIFT"), nil, LuaHelper.VoidDelegate(self:OnMsgRecharge()), LuaHelper.VoidDelegate(self:OnMsgClose()),getCommonStr("STR_PAY"), getCommonStr("BUTTON_CANCEL"))
            return
        end
        local operation = {
            type = "BUY_GIFT",
            vipLev =  self.vvip
        }
        print("operation.vipLev=",operation.vipLev)
        self.buyGiftLv = self.vvip
        local req = protobuf.encode("fogs.proto.msg.PlayerVipOperation",operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.PlayerVipOperationID,req)
        CommonFunction.ShowWait()
    end
end

function VIPPopup:ClickBack()
    return function()
        self:AnimClose()
    end
end

function VIPPopup:ClickVIpItem()
    return function(item)
        print("Clickk vip Item item=",item)
        MainPlayer.Instance:Pay(item.data.id)
    end
end

function VIPPopup:DataRefresh()
    self.vipExp = MainPlayer.Instance.VipExp
    self.vip = MainPlayer.Instance.Vip

    local vipExp = self.vipExp
    local vip = self.vip
    self:SetLvIcon(self.uiLevel.transform, vip)

    if vip < self.config.maxVip then
        local nextVipData = self.config:GetVipData(vip + 1)
        local nextCostOff = nextVipData.consume - vipExp

        self.uiRechargeNum:SetText(tostring(nextCostOff))
        self:SetLvIcon(self.uiNextLevel.transform, vip + 1)
        self.uiProcessNum.text = tostring(vipExp).."/"..tostring(nextVipData.consume)
        self.uiProcessBar.value = vipExp/nextVipData.consume

    else
        self.uiRecharge:SetText(getCommonStr("REACH_VIP_MAX_LV"))
        self.uiRechargeNum.gameObject:SetActive(false)
        self.uiNextLevelNode.gameObject:SetActive(false)
        self.uiProcessNode.gameObject:SetActive(false)
        self.uiDiamondIcon.gameObject:SetActive(false)
    end

    self.uiPrivilegeNode.gameObject:SetActive(self.state == st.privilege)
    self.uiRechargeNode.gameObject:SetActive(self.state == st.recharge)

    if self.state == st.privilege then

    elseif self.state == st.recharge then
        self.vipItems = {}
        CommonFunction.ClearGridChild(self.uiRechargeGrid.transform)
        local enum = self.config.recharges:GetEnumerator()
        while enum:MoveNext() do
            local v = enum.Current.Value
            local s = getLuaComponent(createUI("VIPItem", self.uiRechargeGrid.transform))
            s:SetData(v)
            s.onClick = self:ClickVIpItem()
            table.insert(self.vipItems, s)
        end
        self.uiRechargeGrid.repositionNow = true
        for k, v in pairs(self.vipItems) do
            v:Refresh()
        end
    end


    ------------------------------------
    -------  vvip
    print("self.vvip=",self.vvip)
    NGUITools.SetActive(self.uiLeftArrow.gameObject,self.vvip ~= 0)
    NGUITools.SetActive(self.uiRightArrow.gameObject,self.vvip ~= self.MAX_LV)

    local vipData = self.config:GetVipData(self.vvip)
    self.uiGiftLabel.text = string.format(getCommonStr("VIP_GIFT_CONTAIN"), self.vvip)
    self.uiOriPrice.text =  vipData.ori_gift_price
    self.uiNowPrice.text = vipData.gift_price

    if vipData.gift ~= 0 then
        local goodsUseConfig = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(vipData.gift)
        if goodsUseConfig then
            local enum = goodsUseConfig.args:GetEnumerator()
            while enum:MoveNext() do
                local v = enum.Current
                local awardId = v.id
                local awardList = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(awardId)
                local awardEnum = awardList:GetEnumerator()
                local index = 1
                while awardEnum:MoveNext() do
                    local id = awardEnum.Current.award_id
                    local value = awardEnum.Current.award_value
                    local s = self.goodsList[index]
                    if s == nil then
                        error("s ==nil for index=", index)
                    end
                    s.gameObject:SetActive(true)
                    s.goodsID = id
                    s.num = value
                    s.hideNum = false
                    s:Refresh()
                    index = index + 1
                end
                for i=index, #self.goodsList do
                    self.goodsList[i].gameObject:SetActive(false)
                end
            end
        else
            error("cannot get use goods id =", vipData.gift, "please check the config")
        end
    end
    self.uiBuyBtn.gameObject:SetActive(vipData.gift ~= 0 )

    if self.vip >= self.vvip then
        if not MainPlayer.Instance.VipGifts:Contains(self.vvip) then
            self.uiBuyBtn.normalSprite = "com_button_yellow7up"
            self.uiBuyBtnLabel:SetText(getCommonStr("RECEIVE"))
            self.buyBtnState = 1
        else
            self.uiBuyBtn.normalSprite = "com_button_yellow_1"
            self.uiBuyBtnLabel:SetText(getCommonStr("ASSIST_ROLE_OWNED"))
            self.buyBtnState = 2
        end
    else
        self.uiBuyBtn.normalSprite = "com_button_yellow_1"
        self.uiBuyBtnLabel:SetText(getCommonStr("RECEIVE"))
        self.buyBtnState = 3
    end
end

function VIPPopup:OnVipChanged()
    return function()
        self:DataRefresh()
    end
end

function VIPPopup:OnClose()
    TopPanelManager:HideTopPanel()
end


function VIPPopup:HandleBuyGift()
    return function (buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID, self.uiName)
        CommonFunction.StopWait()
        local resp, err = protobuf.decode("fogs.proto.msg.PlayerVipOperationResp", buf)
        if resp then
            print("reslt = "..tostring(resp.result))
            if resp.result == 0 then
                local level = self.buyGiftLv
                print("level=",level)
                MainPlayer.Instance.VipGifts:Add(level)
                self.tip = nil
                self.tip = CommonFunction.ShowTip(getCommonStr("BUY_VIP_GIFT_SUCCESS"), nil)
                local root = UIManager.Instance.m_uiRootBasePanel
                local goodsAcquire = root.transform:FindChild("RoleAcquirePopupNew(Clone)")
                if goodsAcquire and goodsAcquire.gameObject.activeSelf then
                    NGUITools.SetActive(self.tip.gameObject, false)
                end
                self:DataRefresh()
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            end
        else
            error("HandleBuyGift: " .. err)
        end
        self.buyGiftLv = nil
    end
end

function VIPPopup:InvitePlayerResp(resp)
    NGUITools.SetActive(self.gameObject, false)
    local roleAcquireLua = TopPanelManager:ShowPanel("RoleAcquirePopupNew", nil, {id = resp.role.id})
    roleAcquireLua.onBack = function ( ... )
        NGUITools.SetActive(self.gameObject, true)
        if self.tip and not self.tip.gameObject.activeSelf then
            NGUITools.SetActive(self.tip.gameObject, true)
        end
    end
end

function VIPPopup:Move(left, immediately)
    if self.moving then
        return
    end
    self.uiPrivilegeScroll:ResetPosition()
    local sign = left and 1 or -1
    self:SetVVip(self.vvip + sign)
end

function VIPPopup:MoveFinish()
    return function()
        self.moving = false
        self.uiPrivilegeName.text = getCommonStr("STR_VIP_PRIVILEGE"):format(self.vvip)
    end
end

function VIPPopup:SetVVip(lv, immediately)
    assert( lv<= self.MAX_LV)
    local preLv = self.vvip
    self.vvip = lv

    self.moving = not immediately
    if immediately then
        if self.ruleList then
            for k, v in pairs(self.ruleList) do
                if v then
                    NGUITools.Destroy(v.gameObject)
                    self.ruleList[k] = nil
                end
            end
        end
        self.ruleList =  {}
        for llv = math.max(0, lv-1), math.min(self.MAX_LV, lv + 1) do
            self:CreateRuleList(lv, llv)
        end
        return
    end


    local sign = lv - preLv
    local offx = -self.moveX
    offx = offx + (self.OFFSET_X * (-sign))

    -- Move Pre
    for k, v in pairs(self.ruleList) do
        if v then
            local fps = UIManager.Instance.AvgFPS
            local d = fps ~= 0 and 0.5 *60 /fps or 0.5
            v.tween = TweenPosition.Begin(v.gameObject,d ,Vector3.New(v.transform.localPosition.x + offx, v.transform.localPosition.y , 0))
            v.tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
        end
    end

    -- Re-Manage List
    -- Destroy
    for k, v in pairs(self.ruleList) do
        if k < math.max(0, lv-1) or k > math.min(self.MAX_LV, lv + 1) then
            NGUITools.Destroy(v.gameObject)
            self.ruleList[k] = nil
        end
    end

    -- Create
    for llv = math.max(0, lv-1), math.min(self.MAX_LV, lv + 1) do
        if self.ruleList[llv] == nil then
            self:CreateRuleList(self.vvip, llv)
        end
    end
end

function VIPPopup:CreateRuleList(curLv, lv)
    local state = self.config:GetVipState(lv).states
    local s = getLuaComponent(createUI("RulePane", self.uiPrivilegeGrid.transform))
    local rule = ""
    local i = 1
    local enum = state:GetEnumerator()
    while enum:MoveNext() do
        if string.len(enum.Current) ~= 0 then
            if i ~= 1 then
                rule = rule.."\n"
            end
            rule = rule..enum.Current
            i = i + 1
        end
    end
    s.rule = rule
    if i <= 3 then
        Object.Destroy(s.transform:GetComponent("UIDragScrollView"))
    end
    s:SetTitle("")
    s.transform.name = lv
    local pos = s.transform.localPosition
    pos.x = self.privilegeGrid1x + (lv - curLv) * self.OFFSET_X
    s.transform.localPosition = pos
    self.ruleList[lv] = s
end

function VIPPopup:PageReset()
    self.moveX = 0
    self.uiPrivilegeName.text = getCommonStr("STR_VIP_PRIVILEGE"):format(self.vvip)
    local pos = self.uiPrivilegeGrid.transform.localPosition
    pos.x = self.privilegeGrid1x
    self.uiPrivilegeGrid.transform.localPosition = pos
end

function VIPPopup:OnMsgRecharge()
    return function()
        self:SetRechargeState(true)
        self:DataRefresh()
    end
end

function VIPPopup:OnMsgClose()
    return function()
        if self.msg then
            NGUITools.Destroy(self.msg)
            self.msg =nil
        end
    end
end

function VIPPopup:SetLvIcon(trans, vip)
    local num = trans:FindChild("Num"):GetComponent("UILabel")
    num.text = vip
end


return VIPPopup

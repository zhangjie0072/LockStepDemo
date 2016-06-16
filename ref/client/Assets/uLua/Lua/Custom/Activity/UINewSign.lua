--encoding=utf-8

UINewSign =
{
    uiName = 'UINewSign',
    ------------------UI
    uiGoodsGrid,
    uiTotalGrid1,
    uiTotalGrid2,
    uiBtnClose,
    uiLeftLabel,
    uiLeftGrid,
    uiLeftBtn,
    uiLeftBtnSprite,
    uiLeftBtnRedDot,
    uiLeftSprite,
    uiRoleGrid,
    uiRoleMask,
    uiRoleMaskUp,
    uiAnimator,
    uiSevenPlayerImg,
    uiGoodRoot,
    uiDayNumIcon,
    --------------------
    onClose,
    currentDay = 0,
    allDay = 0,
    newPlayerID,

    goodsIconList = {},

    isNewRole = false,
}

local signState =
{
    signed     = 1,
    signing    = 2,
    appendSign = 3,
    neverSign  = 4,
}

local buttonSprite =
{
    disable = 'com_button_yellow_1',
    normal = 'com_button_yellow7up'
}


-----------------------------------------------------------------
function UINewSign:Awake()
    self.uiGoodRoot = self.transform:FindChild('Window/Top/Scroll').transform
    self.uiTotalGrid1 = self.transform:FindChild('Window/Bottom/Left/Scroll/Grid'):GetComponent('UIGrid')
    self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
    self.uiLeftLabel = self.transform:FindChild('Window/Bottom/Left/Label'):GetComponent('UILabel')
    self.uiLeftGrid = self.transform:FindChild('Window/Bottom/Left/Scroll/Grid'):GetComponent('UIGrid')
    self.uiLeftBtn = self.transform:FindChild('Window/Bottom/Left/ButtonOK'):GetComponent('UIButton')
    self.uiLeftBtnSprite = self.transform:FindChild('Window/Bottom/Left/ButtonOK'):GetComponent('UISprite')
    self.uiLeftBtnRedDot = self.transform:FindChild('Window/Bottom/Left/ButtonOK/RedDot'):GetComponent('UISprite')
    self.uiLeftSprite = self.transform:FindChild('Window/Bottom/Left/Sprite'):GetComponent('UISprite')
    self.uiRoleGrid = self.transform:FindChild('Window/Top/Role')
    self.uiRoleMask = self.transform:FindChild('Window/Top/Background/Mask')
    self.uiDayNumIcon = self.transform:FindChild('Window/Top/Background/Day'):GetComponent('UISprite')
    self.uiSevenPlayerImg = self.transform:FindChild('Window/Top/Background/SevenPlayer')
    self.uiRoleMaskUp = self.transform:FindChild('Window/Top/Background/MaskUp')
    self.uiAnimator = self.transform:GetComponent('Animator')

    for i=1,self.uiGoodRoot.childCount do
        self.goodsIconList[i] = self.uiGoodRoot:GetChild(i-1)
    end
end

function UINewSign:Start()
    print ("start *************")
    addOnClick(self.uiBtnClose.gameObject, self:ClickClose())
    addOnClick(self.uiRoleMask.gameObject, self:ClickRoleAward())
    self:InitNewSign()
    LuaHelper.RegisterPlatMsgHandler(MsgID.NewComerSignRespID, self:GetAwardRespHandler(), self.uiName)
end

function UINewSign:FixedUpdate()
    -- body
end

function UINewSign:OnClose()
    if self.onClose then
        self.onClose()
    end
    print ("destroy *************")
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NewComerSignRespID, self.uiName)
    NGUITools.Destroy(self.gameObject)
end

function UINewSign:OnDestroy()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NewComerSignRespID, self.uiName)
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UINewSign:Refresh( ... )
end

function UINewSign:RefreshDayIcon(day_num)
    self.uiDayNumIcon.spriteName = 'sign_'.. day_num
end

-----------------------------------------------------------------
function UINewSign:InitNewSign( ... )
    local totalDays = GameSystem.Instance.NewComerSignConfig.configData.Count
    local list = self:RefreshTotalList()

    -- CommonFunction.ClearGridChild(self.uiGoodsGrid.transform)
    for i=1,totalDays do
        -- print("list" .. list[i])
        -- if i == 7 then
        --     local id = GameSystem.Instance.NewComerSignConfig:GetDayAwardType(i)
        --     local role = getLuaComponent(createUI("NewRoleBustItem1",self.uiRoleGrid.transform))
        --     role.id = tonumber(id)
        --     role.isHas = false
        --     role.gameObject.name = "RoleBustItem1(" .. id .. ")"

    -- role.showRecruit = false
    --      role.isResetDisplay = true
    --      if list[i] then
    --          if list[i] == 0 and i == self.currentDay then
    --              role:ShowEfShine(true)
    --          else
    --              role:ShowEfShine(false)
    --              NGUITools.SetActive(self.uiRoleMaskUp.gameObject, true)
    --              self.allDay = self.allDay + 1
    --          end
    --      end

            -- if list[i] then
            --     if not (list[i] == 0 and i == self.currentDay) then
            --         role.isHas = true
            --         role:Refresh()
            --         -- NGUITools.SetActive(self.uiRoleMaskUp.gameObject, true)
            --         self.allDay = self.allDay + 1
            --     end
            -- end
        -- end

        local daySign = createUI('NewSign', self.goodsIconList[i])
        daySign.transform.name = tostring(i)
        local signLua = getLuaComponent(daySign)

        -- list value : nil:未达天数,0:未签到,1:已签到,2:已领取累计签到奖励
        if list[i] == 1 or list[i] == 2 then
            self.allDay = self.allDay + 1
            signLua:SetData(i, signState.signed)
        elseif list[i] == 0 and i == self.currentDay then
            signLua.onClick = self:SendAwardReqHandler(signState.signing)
            signLua.isCurrentDay = true
            signLua:SetData(i, signState.signing)
        elseif list[i] == 0 and i ~= self.currentDay then
            local appendNum = GameSystem.Instance.NewComerSignConfig:GetConsumeNum(i)
            signLua.onClick = function ( ... )
                local table = CommonFunction.ShowPopupMsg(string.format(getCommonStr('CONFIRM_FILLSIGNIN'),appendNum),nil,
                    LuaHelper.VoidDelegate(function ( ... )
                        self:SendAwardReqHandler(signState.appendSign, i)()
                    end),
                    LuaHelper.VoidDelegate(function ( ... )
                        NGUITools.Destroy(table.gameObject)
                    end),
                    getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL")).table
            end
            signLua:SetData(i, signState.appendSign)
        elseif not list[i] then
            signLua:SetData(i, signState.neverSign)
        end
    end

    self:RefreshDayIcon(totalDays - self.allDay)

    -- self:RefreshTotalAwards()
end

function UINewSign:RefreshTotalList( ... )
    local list = {}
    local day = 0
    local signList = MainPlayer.Instance.NewComerSign.sign_list
    local enum = signList:GetEnumerator()
    while enum:MoveNext() do
        day = day + 1
        list[day] = enum.Current
        -- print('list[day] = ' .. list[day])
    end
    self.currentDay = day

    return list
end

function UINewSign:RefreshTotalAwards( ... )
    local list = self:RefreshTotalList()
    local enum = GameSystem.Instance.NewComerSignConfig.totalConfigData:GetEnumerator()
    while enum:MoveNext() do
        local totalDay = enum.Current.Key
        local parent = self.uiLeftGrid.transform
        CommonFunction.ClearGridChild(parent.transform)
        if not list[totalDay] or (list[totalDay] and list[totalDay] ~= 2 and list[totalDay] ~= 3) then
            local totalAwards = GameSystem.Instance.NewComerSignConfig:GetTotalAward(totalDay)
            local awardEnum = totalAwards:GetEnumerator()
            if self.allDay < totalDay then
                self.uiLeftLabel.text = "[FF0000]" .. self.allDay .. "[-]" .. '/' .. totalDay
                self.uiLeftBtn.normalSprite = buttonSprite.disable
                self.uiLeftBtn.transform:GetComponent('BoxCollider').enabled = false
                -- NGUITools.SetActive(self.uiLeftBtnRedDot.gameObject, false)
            else
                self.uiLeftLabel.text = self.allDay .. '/' .. totalDay
                self.uiLeftBtn.normalSprite = buttonSprite.normal
                self.uiLeftBtn.transform:GetComponent('BoxCollider').enabled = true
                -- NGUITools.SetActive(self.uiLeftBtnRedDot.gameObject, true)
                UIEventListener.Get(self.uiLeftBtn.gameObject).onClick = LuaHelper.VoidDelegate(self:SendAwardReqHandler(nil, totalDay))
            end
            while awardEnum:MoveNext() do
                local award = string.split(awardEnum.Current, ':')
                local goodsIcon = getLuaComponent(createUI('GoodsIcon', parent))
                goodsIcon.goodsID = tonumber(award[1])
                goodsIcon.num = tonumber(award[2])
                goodsIcon.hideNum = false
                goodsIcon.hideLevel = true
                goodsIcon.hideNeed = true
            end
            self.uiLeftGrid.repositionNow = true
            break
        elseif totalDay == 7 and list[totalDay] and list[totalDay] == 2 then
            local totalAwards = GameSystem.Instance.NewComerSignConfig:GetTotalAward(totalDay)
            local awardEnum = totalAwards:GetEnumerator()
            while awardEnum:MoveNext() do
                local award = string.split(awardEnum.Current, ':')
                local goodsIcon = getLuaComponent(createUI('GoodsIcon', parent))
                goodsIcon.goodsID = tonumber(award[1])
                goodsIcon.num = tonumber(award[2])
                goodsIcon.hideNum = false
                goodsIcon.hideLevel = true
                goodsIcon.hideNeed = true
            end
            self.uiLeftGrid.repositionNow = true

            self.uiLeftLabel.text = totalDay .. '/' .. totalDay
            self.uiLeftBtn.normalSprite = buttonSprite.disable
            self.uiLeftBtn.transform:GetComponent('BoxCollider').enabled = false
            -- NGUITools.SetActive(self.uiLeftBtnRedDot.gameObject, false)
            NGUITools.SetActive(self.uiLeftSprite.gameObject, true)
        end
    end
end

function UINewSign:ClickClose( ... )
    return function (go)
        self:DoClose()
    end
end

function UINewSign:DoClose( ... )
    -- if self.uiAnimator then
    --     self:AnimClose()
    -- else
        self:OnClose()
    -- end
end

function UINewSign:ClickRoleAward( ... )
    return function (go)
        local list = self:RefreshTotalList()
        if not list[7] then return end
        if list[7] and list[7] == 0 then
            self:SendAwardReqHandler(signState.signing, 7)()
        end
    end
end

function UINewSign:SendAwardReqHandler(state, day)
    return function (go)
        local signType = nil
        if state then
            if state == signState.appendSign then
                if not FunctionSwitchData.CheckSwith(FSID.give7_re) then return end

                local appendNum = GameSystem.Instance.NewComerSignConfig:GetConsumeNum(day)
                if MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy < appendNum then
                    self:ShowBuyTip("BUY_DIAMOND")
                    return
                end
                signType = 'NCST_APEND'
            elseif state == signState.signing then
                if not FunctionSwitchData.CheckSwith(FSID.give7_any) then return end
                signType = 'NCST_SIGN'
            end
        else
            if not FunctionSwitchData.CheckSwith(FSID.give7_any) then return end
            signType = 'NCST_CUMULATIVE'
        end
        local req =
        {
            ['type'] = signType,
            ['day'] = day
        }
        local buf = protobuf.encode('fogs.proto.msg.NewComerSignReq', req)
        LuaHelper.SendPlatMsgFromLua(MsgID.NewComerSignReqID, buf)
        CommonFunction.ShowWait()
    end
end

function UINewSign:GetAwardRespHandler( ... )
    return function (message)

        print (" 返回函数")
        -- Resp： 1 成功状态 2 领取类型 3 领取累计天数
        local resp, err = protobuf.decode('fogs.proto.msg.NewComerSignResp', message)
        CommonFunction.StopWait()
        if resp.result ~= 0 then
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            return
        end
        print('resp.type ========== ' .. resp.type)

        local root = UIManager.Instance.m_uiRootBasePanel

        local roleAcquire = root.transform:Find("RoleAcquirePopupNew(Clone)")
        if resp.type == 'NCST_SIGN' then
            print ("TYPE IS SIGN")
            MainPlayer.Instance.NewComerSign.sign_list:set_Item(self.currentDay - 1, 1)

            local goodsID = GameSystem.Instance.NewComerSignConfig:GetDayAwardType(self.currentDay)
            local goodsNum = GameSystem.Instance.NewComerSignConfig:GetDayAwardNum(self.currentDay)

            local currentSign = self.uiGoodRoot:FindChild(tostring(self.currentDay))
            local signLua = getLuaComponent(currentSign:GetChild(0).gameObject)
            signLua.onClick = nil
            signLua.changeLight = function (goods)
                local goodsIcon = getLuaComponent(goods.uiGoodsIcon.gameObject)
                goodsIcon:HideLight(true)
            end
            signLua:SetSignedState(resp.type)

            if not roleAcquire or not roleAcquire.gameObject.activeSelf then
                local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
                getGoods:SetGoodsData(goodsID, goodsNum)
                NGUITools.SetActive(getGoods.gameObject, true)
            end

        elseif resp.type == 'NCST_APEND' then
            print ("TYPE IS APEND")
            MainPlayer.Instance.NewComerSign.sign_list:set_Item(resp.day - 1, 1)
            local currentSign = self.uiGoodRoot:FindChild(tostring(resp.day))
            local signLua = getLuaComponent(currentSign:GetChild(0).gameObject)
            signLua.onClick = nil
            signLua:SetSignedState(resp.type)

            local goodsID = GameSystem.Instance.NewComerSignConfig:GetDayAwardType(resp.day)
            local goodsNum = GameSystem.Instance.NewComerSignConfig:GetDayAwardNum(resp.day)

            if not roleAcquire or not roleAcquire.gameObject.activeSelf then
                local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
                getGoods:SetGoodsData(goodsID, goodsNum)
                getGoods.gameObject:SetActive(true)
            end
        elseif resp.type == 'NCST_CUMULATIVE' then
            print ("TYPE IS CUMULATIVE")
            MainPlayer.Instance.NewComerSign.sign_list:set_Item(resp.day - 1, 2)
        end

        if resp.type ~= 'NCST_CUMULATIVE' then
            self.allDay = self.allDay + 1

            local totalDays = GameSystem.Instance.NewComerSignConfig.configData.Count
            self:RefreshDayIcon(totalDays - self.allDay)
        end

        -- 获得的是角色
        if  self.isNewRole then
            self.isNewRole = false
            if self.allDay == 7 then
                local activity = root.transform:FindChild("UIHall(Clone)/Activity")
                activity.gameObject:SetActive(false)
                self:OnClose()
            end
        end

        -- refresh top ui
        UpdateRedDotHandler.MessageHandler("NewComerSign")
    end
end

function UINewSign:GetNewRole(id)
    self.newPlayerID = id
end

function UINewSign:ShowBuyTip(type)
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

function UINewSign:ShowBuyUI(type)
    return function()
        if type == "BUY_DIAMOND" then
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = type
    end
end

function UINewSign:FramClickClose()
    return function()
        NGUITools.Destroy(self.msg.gameObject)
    end
end

function UINewSign:InvitePlayerResp(resp)
    print (" 获得新角色")

    self.isNewRole = true

    local root = UIManager.Instance.m_uiRootBasePanel
    -- local goodsAcquire = root.transform:FindChild("GoodsAcquirePopup(Clone)")
    -- if goodsAcquire then
    --     goodsAcquire.gameObject:SetActive(false)
    -- end

    TopPanelManager:ShowPanel("RoleAcquirePopupNew", nil, {id = resp.role.id})
    -- roleAcquireLua.onBack = function ( ... )
    --     NGUITools.SetActive(self.gameObject, true)
    --     if goodsAcquire and not goodsAcquire.gameObject.activeSelf then
    --         NGUITools.SetActive(goodsAcquire.gameObject, true)
    --     elseif not goodsAcquire then
    --         goodsAcquire = root.transform:FindChild("GoodsAcquirePopup(Clone)")
    --         if goodsAcquire and not goodsAcquire.gameObject.activeSelf then
    --             NGUITools.SetActive(goodsAcquire.gameObject, true)
    --         end
    --     end
    -- end
end

return UINewSign

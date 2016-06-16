UIPlayerBuyDiamondGoldHP = {
    uiName = 'UIPlayerBuyDiamondGoldHP',
    ------------------------------
    BuyType,
    isTaskLink = false,
    onClose,
    banTwice = false,
    isInHall = false,
    -----------------------------UI
    uiTitle,
    uiBtnClose,
    uiBtnOK,
    uiBtnOK1,
    uiBtnOKLabel,
    uiAnimator,
    uiCostLabel,
    --CostText,
    --GetText,
    --Arrow,
    uiCostValue,
    uiGetValue,
    uiGetTypeIcon,
    --uiCostTypeIcon,
    uiTimes,
    uiResultLable,
}


-----------------------------------------------------------------
function UIPlayerBuyDiamondGoldHP:Awake()
    self.uiTitle = self.transform:FindChild('Window/Title'):GetComponent('MultiLabel')
    self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
    self.uiBtnOK = self.transform:FindChild('Window/ButtonOK'):GetComponent('UIButton')
    self.uiBtnOK1 = self.transform:FindChild('Window/ButtonOK1'):GetComponent('UIButton')
    self.uiBtnOKLabel = self.transform:FindChild('Window/ButtonOK/Text'):GetComponent('MultiLabel')
    self.uiGetValue = getComponentInChild(self.transform,"Window/BuyInfo/GetLb/GetValue","UILabel")
    --self.GetText = getComponentInChild(self.transform,"Window/BuyInfo/GetLb/GetText","UILabel")
    self.uiGetTypeIcon = getComponentInChild(self.transform,"Window/BuyInfo/GetLb/GetTypeIcon","UISprite")
    self.uiCostValue = getComponentInChild(self.transform,"Window/BuyInfo/CostLb/CostValue","UILabel")
    --self.CostText = getComponentInChild(self.transform,"Window/BuyInfo/CostLb/CostText","UILabel")
    --self.CostTypeIcon = getComponentInChild(self.transform,"Window/BuyInfo/CostLb/CostTypeIcon","UISprite")
    --self.Arrow = getComponentInChild(self.transform,"Window/BuyInfo/Arrow","UISprite")
    --self.TimesInfo = getComponentInChild(self.transform,"Window/BuyInfo/TimeLb/TimesInfo","UILabel")
    self.uiTimes = getComponentInChild(self.transform,"Window/BuyInfo/TimeLb/Times","UILabel")
    self.uiResultLable = getComponentInChild(self.transform,"Window/ResultLable","UILabel")
    self.uiCostLabel = getComponentInChild(self.transform,"Window/BuyInfo/Text","UILabel")
    NGUITools.SetActive(self.uiResultLable.gameObject,false)
    --self.GetText.text = CommonFunction.GetConstString("BUY_CONVERT")
    --self.CostText.text = CommonFunction.GetConstString("BUY_COST")
    --self.TimesInfo.text = CommonFunction.GetConstString("CURRENT_BUY_COUNT")
    LuaHelper.RegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID,self:PlayerVipOperationResp(),self.uiName)

    self.uiAnimator = self.transform:GetComponent('Animator')
end

--
function UIPlayerBuyDiamondGoldHP:Start()
    local btnClose = getLuaComponent(self.uiBtnClose)
    btnClose.onClick = self:click_close()
    addOnClick(self.uiBtnOK.gameObject, self:Buy_handler())

    if self.BuyType == 'BUY_HP' then
        self.uiTitle:SetText(CommonFunction.GetConstString("STORE_BUY_HP_USE_DIAMOND"))
        self.uiGetTypeIcon.spriteName = "com_property_hp"
        self.uiCostLabel.text = CommonFunction.GetConstString("USE_DIAMONDS_BUY")..CommonFunction.GetConstString("HP")
    elseif self.BuyType == 'BUY_GOLD' then
        self.uiTitle:SetText(CommonFunction.GetConstString("STORE_BUY_GOLD_USE_DIAMOND"))
        self.uiGetTypeIcon.spriteName = "com_property_gold"
        self.uiCostLabel.text = CommonFunction.GetConstString("USE_DIAMONDS_BUY")..CommonFunction.GetConstString("GOLD")
    else
        self.uiTitle:SetText(CommonFunction.GetConstString("STORE_BUY_GOLD_USE_DIAMOND"))
        self.uiGetTypeIcon.spriteName = "com_property_gold"
    end

    self.transform.localPosition = Vector3.New(self.transform.localPosition.x,self.transform.localPosition.y,-500);

    self:UI_Refresh()
end

function UIPlayerBuyDiamondGoldHP:FixedUpdate( ... )
    -- body
end

function UIPlayerBuyDiamondGoldHP:OnClose( ... )
    print('OnClose-------------')
    if self.onClose then
        self.onClose()
    end
    -- self.banTwice = false
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.PlayerVipOperationRespID, self.uiName)
    NGUITools.Destroy(self.gameObject)
    local root = UIManager.Instance.m_uiRootBasePanel
    local hall = root.transform:FindChild('UIHall(Clone)')
    if hall and hall.gameObject.activeSelf then
        local hallLua = getLuaComponent(hall.gameObject)
        hallLua:SetModelActive(true)
    end
end

--
function UIPlayerBuyDiamondGoldHP:OnDestroy()
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

--消息发送
function UIPlayerBuyDiamondGoldHP:Buy_handler()
    return function()
        -- if self.banTwice then
        --	return
        -- end
        --print('1111111111111111111-------handler---BuyType:'..self.BuyType)
        if not FunctionSwitchData.CheckSwith(FSID.gold_btn) then return end

        local buyTimes = 0
        local maxTimes = 0
        local buyData
        if self.BuyType == 'BUY_GOLD' then
            buyTimes = MainPlayer.Instance.GoldBuyTimes + 1
            buyData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyGoldDataByTimes(buyTimes)
        elseif self.BuyType == 'BUY_HP' then
            buyTimes = MainPlayer.Instance.HpBuyTimes + 1
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
                CommonFunction.ShowWait()
            else
                -- local goBuyDetail = createUI('ConfirmFrame')
                -- goBuyDetail.transform.localPosition = Vector3.New(0,0,-700)
                -- local buyDetail = getLuaComponent(goBuyDetail)
                -- buyDetail.onConfirm = self:click_close()
                -- buyDetail:SetTitleAndMessage("", CommonFunction.GetConstString("STORE_BUY_DIAMOND"))
                -- CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("NOT_ENOUGH_DIAMOND"),nil,nil,nil,nil,nil)
                local str = getCommonStr("YOUR_DIMAND_LACK_AND_SWITCH_DISABLE")
                self.msg = CommonFunction.ShowTip(str, nil)
                self:click_close()()
            end
        else
            print("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
            CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("ERROR_CONFIGURATION"),nil,nil,nil,nil,nil)
            self:OnClose()
        end
    end
end

function UIPlayerBuyDiamondGoldHP:ShowBuyDiamond()
    return function()
        TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
    end
end

function UIPlayerBuyDiamondGoldHP:FramClickClose()
    return function()
        NGUITools.Destroy(self.msg.gameObject)
    end
end

--消息接收
function UIPlayerBuyDiamondGoldHP:PlayerVipOperationResp()
    return function (buf)
        local resp, err = protobuf.decode("fogs.proto.msg.PlayerVipOperationResp", buf)
        CommonFunction.StopWait()
        if resp then
            if resp.result == 0 then
                -- self.banTwice = true
                self.BuyType = resp.type
                local goodsID = 0
                if self.BuyType == 'BUY_GOLD' then
                    MainPlayer.Instance.GoldBuyTimes = resp.times
                    goodsID = 2
                elseif self.BuyType == "BUY_HP" then
                    MainPlayer.Instance.HpBuyTimes = resp.times
                    goodsID = 4
                end

                -- local goodsPopup = createUI('GoodsAcquirePopup', self.transform)
                -- local pos = goodsPopup.transform.localPosition
                -- pos.z = 500
                -- goodsPopup.transform.localPosition = pos
                -- local getGoods = getLuaComponent(goodsPopup)
                -- getGoods:SetGoodsData(goodsID, self.uiCostValue.text)

                self:UI_Refresh()

                local root = UIManager.Instance.m_uiRootBasePanel
                local careerSection = root.transform:FindChild('UICareer(Clone)/UICareerSection(Clone)')
                if careerSection and self.BuyType == "BUY_HP" then
                    local sectionLua = getLuaComponent(careerSection.gameObject)
                    sectionLua:RefreshHpState()
                end

                --动画调用
                local effectNode = self[self.BuyType]
                if self.isInHall == false then
                    effectNode = "E_Gold2"
                end
                local effectChild = self.transform:FindChild(effectNode)
                local eParent = effectChild.transform.parent

                if effectChild then
                    effectChild = Object.Instantiate(effectChild)
                    effectChild.transform.parent = eParent
                    effectChild.localPosition = Vector3.New(0, 0, 0)
                    effectChild.localScale = Vector3.New(1, 1, 1)
                    GameObject.Destroy(effectChild.gameObject, 3)

                    local effect = effectChild:GetComponent('Animator')
                    print("------            effect: ", effect)
                    if effect then
                        effect.gameObject:SetActive(true)
                        effect.enabled = true
                        effect:SetTrigger("go")
                    end
                end
                playSound("UI/buy")

                --
                if self.isTaskLink then
                    local parent = self.transform.parent
                    for i=0,parent.transform.childCount - 1 do
                        local child = parent.transform:GetChild(i).gameObject
                        if child.activeSelf then
                            local childCom = child:GetComponent('LuaComponent')
                            if childCom then
                                local childLua = getLuaComponent(child)
                                if childLua and childLua.uiBtnMenu then
                                    local btnMenu = getLuaComponent(childLua.uiBtnMenu)
                                    btnMenu:Refresh()
                                    self.isTaskLink = false
                                    break
                                end
                            end
                        end
                    end
                end
            else
                print('gou mai cuo wu')
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            end
        else
            error("s_handle_switch_captain():" .. err)
        end
    end
end

--界面刷新
function UIPlayerBuyDiamondGoldHP:UI_Refresh()
    UIManager.Instance:BringPanelForward(self.gameObject)
    local level = self:get_vip()
    local totalTimes = 0
    local buyTimes = 0
    if self.BuyType == 'BUY_GOLD' then
        totalTimes = GameSystem.Instance.VipPrivilegeConfig:GetBuygold_times(level)
        buyTimes = MainPlayer.Instance.GoldBuyTimes
    elseif self.BuyType == 'BUY_HP' then
        totalTimes = GameSystem.Instance.VipPrivilegeConfig:GetBuyhp_times(level)
        buyTimes = MainPlayer.Instance.HpBuyTimes
    else
        print('error!!!!!')
        return
    end
    --print(' =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  =  = '..leftTimes..'/'..totalTimes)
    print('buyTimes: ', buyTimes)
    print('totalTimes: ', totalTimes)
    self.uiTimes.text = tostring(totalTimes - buyTimes)..'/'..tostring(totalTimes)
    if buyTimes >= totalTimes then
        NGUITools.SetActive(self.transform:FindChild('Window/BuyInfo').gameObject,false)
        NGUITools.SetActive(self.uiResultLable.gameObject,true)
        -- self.uiBtnOKLabel:SetText(getCommonStr('BUTTON_CONFIRM'))
        -- NGUITools.Destroy(self.uiFrame.transform:FindChild("ButtonNode1").gameObject)
        self.uiResultLable.text = CommonFunction.GetConstString("STORE_BUY_TIMES_NOT_ENOUGH")-- .. "\n\n" .. getCommonStr('NOT_ENOUGH_VIP_LEVEL')
        self.uiResultLable.fontSize = 30
        NGUITools.SetActive(self.uiBtnClose.gameObject, false)
        NGUITools.SetActive(self.uiBtnOK.gameObject, false)
        NGUITools.SetActive(self.uiBtnOK1.gameObject, true)
        addOnClick(self.uiBtnOK1.gameObject, self:click_close())
        -- addOnClick(self.uiBtnOK.gameObject, self:click_close())
        -- local pos = self.uiBtnOK.transform.localPosition
        -- pos.x = 0
        -- self.uiBtnOK.transform.localPosition = pos
    else
        local buyGoldData = BuyData.New()
        if self.BuyType == 'BUY_GOLD' then
            buyGoldData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyGoldDataByTimes(buyTimes + 1)
            if buyGoldData then
                self.uiCostValue.text = tostring(buyGoldData.diamond_need)
                self.uiGetValue.text = tostring(buyGoldData.value)
            else
                self.uiCostValue.text = ''
                self.uiGetValue.text = ''
            end
        elseif self.BuyType == 'BUY_HP' then
            buyHpData = GameSystem.Instance.BaseDataBuyConfigData:GetBuyHpDataByTimes(buyTimes + 1)
            if buyHpData then
                self.uiCostValue.text = tostring(buyHpData.diamond_need)
                self.uiGetValue.text = tostring(buyHpData.value)
            else
                self.uiCostValue.text = ''
                self.uiGetValue.text = ''
            end
        end
    end
end

--
function UIPlayerBuyDiamondGoldHP:click_close()
    return function()
        -- if self.uiAnimator then
        --     self:AnimClose()
        -- else
            self:OnClose()
        -- end
    end
end


function UIPlayerBuyDiamondGoldHP:get_vip()
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

return UIPlayerBuyDiamondGoldHP

--encoding=utf-8

Fashion_OBB =  {
    uiName="Fashion_OBB",

    ----------------------------------
    diamondNum = 0,
    reputationNum = 0,

    goodsList = {},
    banTwice = false,
    onBuy,
    onClose,
    buyNumber = 0,
    isReputation = false,
    hasDiamond = false,
    hasReputation = false,
    ----------------------------------UI
    uiTitle,
    uiDiamondValue,
    uiReputationValue,
    uiBtnBuy,
    uiBtnClose,
    uiAnimator,
    uiGoodsGrid,
    uiDiamondIcon,
    uiReputationIcon,
}


-----------------------------------------------------------------
function Fashion_OBB:Awake()
    self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/Bg/ButtonClose'))
    self.uiTitle = self.transform:FindChild('Window/TipLabel'):GetComponent('UILabel')
    self.uiDiamondValue = self.transform:FindChild('Window/Total/Num'):GetComponent('UILabel')
    self.uiReputationValue = self.transform:FindChild('Window/Total/Num2'):GetComponent('UILabel')
    self.uiBtnBuy = self.transform:FindChild('Window/Buy'):GetComponent('UIButton')
    self.uiGoodsGrid = self.transform:FindChild('Window/Scroll/Grid'):GetComponent('UIGrid')
    self.uiDiamondIcon = self.transform:FindChild('Window/Total/DiamondIcon'):GetComponent('UISprite')
    self.uiReputationIcon = self.transform:FindChild('Window/Total/ReputationIcon'):GetComponent('UISprite')
    self.uiAnimator = self.transform:GetComponent('Animator')
end

function Fashion_OBB:Start()
    addOnClick(self.uiBtnClose.gameObject, self:CloseClick())
    addOnClick(self.uiBtnBuy.gameObject, self:OnBuyClick())
    if not self.hasDiamond then
        self.uiReputationIcon.transform.localPosition = self.uiDiamondIcon.transform.localPosition
        self.uiReputationValue.transform.localPosition = self.uiDiamondValue.transform.localPosition
        NGUITools.SetActive(self.uiDiamondIcon.transform.gameObject, false)
        NGUITools.SetActive(self.uiDiamondValue.transform.gameObject, false)
    elseif not self.hasReputation then
        NGUITools.SetActive(self.uiReputationIcon.transform.gameObject, false)
        NGUITools.SetActive(self.uiReputationValue.transform.gameObject, false)
    end
    --注册购买商品的回复处理消息
    LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyStoreGoodsResp(), self.uiName)
end

function  Fashion_OBB:FixedUpdate()
end

function Fashion_OBB:OnClose()
    if self.onClose then
        self.onClose()
    end
    NGUITools.Destroy(self.gameObject)
end

function Fashion_OBB:OnDestroy()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self.uiName)

    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function Fashion_OBB:Refresh()
end


-----------------------------------------------------------------
function Fashion_OBB:CloseClick( ... )
    return function (go)
        self:DoClose()
    end
end

function Fashion_OBB:DoClose( ... )
    -- if self.uiAnimator then
    --     self:AnimClose()
    -- else
        self:OnClose()
    -- end
end

function Fashion_OBB:OnBuyClick( ... )
    return function (go)
        --if not self.isReputation then
            local ownDiamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if self.diamondNum > ownDiamond then
                --CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_DIAMOND'),nil,nil,nil,nil,nil)
                --if self.banTwice == true then
                --    return
                --end
                --self.banTwice = true
                --self:ShowBuyTip("BUY_DIAMOND")
                CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_DIAMOND'),nil,nil,nil,nil,nil)
                return
            end
        --else
            local ownReputation = MainPlayer.Instance.Reputation
            if self.reputationNum > ownReputation then
                CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_REPUTATION'),nil,nil,nil,nil,nil)
                return
            end
        --end
        print('--------------------------------')
        for i,v in ipairs(self.goodsList) do
            print(i,v)
        end
        print('--------------------------------')
        local storeType = nil
        if self.isReputation then
            storeType = 'ST_REPUTATION'
        else
            storeType = 'ST_FASHION'
        end
        local buyStoreGoods = {
                store_id = storeType,	--fashion store
                info = {	},
            }
        local pos = nil
        local count = 0
        for i,v in ipairs(self.goodsList) do
            print(self.uiName,"----id in goodsList:",v,"i:",i)
            pos = self:FindFashionPos(v)
            table.insert(buyStoreGoods.info, {pos = pos,})
            count = count + 1
        end
        if count == 0 then
            CommonFunction.ShowPopupMsg(getCommonStr('UI_FASHION_YOU_ARE_NOT_SELECT_FASHION'),nil,nil,nil,nil,nil)
            return
        end
        local msg = protobuf.encode("fogs.proto.msg.BuyStoreGoods", buyStoreGoods)
        LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)
        CommonFunction.ShowWait()
    end
end

function Fashion_OBB:SetData(list)
    for i,v in ipairs(list) do
        local item = getLuaComponent(createUI('Fashion_OBB_item', self.uiGoodsGrid.transform))
        item.isReputation = self.isReputation
        item:SetData(v)
        -- item.onChoose = self:ChangeChoose()
        local num1 = self.diamondNum
        local num2 = self.reputationNum
        local fashionShopConfig = nil
        if not self.isReputation then
            fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(v)
        else
            fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(v)
        end

        local enumType = fashionShopConfig._costType:GetEnumerator()
        while enumType:MoveNext() do
            local costType = enumType.Current
            if costType == 1 then
                self.hasDiamond = true
                item.realCostType = 0
            elseif costType == 10 then
                self.hasReputation = true
                item.realCostType = 1
            end
            break
        end
        item.onChoose = self:ChangeChoose(not (self.diamondNum == num1 and self.reputationNum == num2))
    end

    --self.goodsList = list
    self.uiGoodsGrid.repositionNow = true
    self.uiDiamondValue.text = self.diamondNum
    self.uiReputationValue.text = self.reputationNum
    self.uiTitle.text = string.format(getCommonStr('UI_FASHION_NUM'), self.buyNumber)
end

function Fashion_OBB:ChangeChoose(isDiscount)
    return function (id, bool)
        local fashionShopConfig = nil
        if not self.isReputation then
            fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(id)
        else
            fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(id)
        end
        local enumNum = nil
        local realcostType = nil

        local enumType = fashionShopConfig._costType:GetEnumerator()
        while enumType:MoveNext() do
            local costType = enumType.Current
            if costType == 1 then
                realcostType = 0
            elseif costType == 10 then
                realcostType = 1
            end
            break
        end

        if not isDiscount then
            enumNum = fashionShopConfig._costNum:GetEnumerator()
        else
            enumNum = fashionShopConfig._discountCost:GetEnumerator()
        end
        while enumNum:MoveNext() do
            if bool then
                if realcostType == 0 then
                    self.diamondNum = self.diamondNum + tonumber(enumNum.Current)
                else
                    self.reputationNum = self.reputationNum + tonumber(enumNum.Current)
                end
                self:ChangeGoodsList(id, bool)
                self.buyNumber = self.buyNumber + 1
            else
                if realcostType == 0 then
                    self.diamondNum = self.diamondNum - tonumber(enumNum.Current)
                else
                    self.reputationNum = self.reputationNum - tonumber(enumNum.Current)
                end
                self:ChangeGoodsList(id, bool)
                self.buyNumber = self.buyNumber - 1
            end
            break
        end
        self.uiDiamondValue.text = self.diamondNum
        self.uiReputationValue.text = self.reputationNum
        self.uiTitle.text = string.format(getCommonStr('UI_FASHION_NUM'), self.buyNumber)
    end
end

function Fashion_OBB:ChangeGoodsList(id, bool)
    if bool then
        print(self.uiName,"----insert id:",id)
        local list = {}
        for i,v in ipairs(self.goodsList) do
            print(self.uiName,"----id in goodsList:",v)
            table.insert(list, tonumber(v))
        end
        table.insert(list, tonumber(id))
        self.goodsList = nil
        self.goodsList = list
        printTable(list)
        print("-----------------==")
        printTable(self.goodsList)
    else
        print(self.uiName,"----remove id:",id)
        local list = {}
        for i,v in ipairs(self.goodsList) do
            print(self.uiName,"----id in goodsList:",v)
            if tonumber(v) ~= id then
                table.insert(list, tonumber(v))
            end
        end
        self.goodsList = nil
        self.goodsList = list
        printTable(list)
        print("-----------------==")
        printTable(self.goodsList)
    end
end

function Fashion_OBB:FindFashionPos(id)
    if not self.isReputation then
        local fashionItem = GameSystem.Instance.FashionShopConfig.configs:get_Item(id)
        local index = GameSystem.Instance.FashionShopConfig.configsSort:IndexOf(fashionItem)
        return index + 1
    else
        local fashionItem = GameSystem.Instance.FashionShopConfig.reputationConfigs:get_Item(id)
        local index = GameSystem.Instance.FashionShopConfig.reputationConfigsSort:IndexOf(fashionItem)
        return index + 1
    end
end

function Fashion_OBB:BuyStoreGoodsResp( ... )
    --解析pb
    return function(message)
        local resp, err = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
        CommonFunction.StopWait()
        if resp == nil then
            Debugger.LogError('------BuyStoreGoodsResp error: ', err)
            return
        end
        Debugger.Log('---2---------resp: {0}', resp.store_id)
        print('resp.result:' .. resp.result)
        if resp.result ~= 0 then
            Debugger.Log('-----------1: {0}', resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
            playSound("UI/UI-wrong")
            return
        end
        if self.onBuy then
            self.onBuy()
            self:DoClose()
            CommonFunction.ShowPopupMsg(getCommonStr('BUY_FASHION_SUCCESS'),nil,nil,nil,nil,nil)
        end
    end
end

function Fashion_OBB:ShowBuyTip(type)
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

function Fashion_OBB:ShowBuyUI(type)
    return function()
        self.banTwice = false
        if type == "BUY_DIAMOND" then
            NGUITools.Destroy(self.gameObject)
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = type
    end
end

function Fashion_OBB:FramClickClose()
    return function()
        NGUITools.Destroy(self.msg.gameObject)
        self.banTwice = false
    end
end


return Fashion_OBB

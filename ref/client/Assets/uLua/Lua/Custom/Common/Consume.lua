print("<<<Init Consume>>>")

Consume = {}
Consume.buyMsg = nil

function Consume.CheckConsume(consumeID, consumeNum)
    local mainPlayer = MainPlayer.Instance
    if consumeID == 1 and mainPlayer.DiamondFree + mainPlayer.DiamondBuy < consumeNum then
        Consume.ShowBuyTip('BUY_DIAMOND')
        return false
    elseif consumeID == 2 and mainPlayer.Gold < consumeNum then
        Consume.ShowBuyTip('BUY_GOLD')
        return false
    elseif consumeID == 4 and mainPlayer.Hp < consumeNum then
        Consume.ShowBuyTip('BUY_HP')
        return false
    elseif consumeID ~= 1 and consumeID ~= 2 and consumeID ~= 4 then
        local ownGoods = mainPlayer:GetGoodsList(GoodsCategory.GC_CONSUME, consumeID)
        if ownGoods and ownGoods.Count > 0 and ownGoods.Count < consumeNum then
            local goodsItem = ownGoods:get_Item(0)
            Consume.ShowBuyTip(goodsItem:GetName())
            return false
        end
    end

    return true
end

function Consume.ShowBuyTip(consumeType)
    local str
    if consumeType == "BUY_GOLD" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
    elseif consumeType == "BUY_DIAMOND" then
        str = getCommonStr("YOUR_DIMAND_LACK_AND_SWITCH_DISABLE")
    elseif consumeType == 'BUY_HP' then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
    else
        str = string.format(getCommonStr("NOT_ENOUGH_GOODS"), consumeType)
        CommonFunction.ShowPopupMsg(str,nil,nil,nil,nil,nil)
        return
    end
    if consumeType == "BUY_DIAMOND" then
        Consume.msg = CommonFunction.ShowTip(str, nil)
    else
        Consume.buyMsg = CommonFunction.ShowPopupMsg(str, nil,
                                                     LuaHelper.VoidDelegate(Consume.ShowBuyUI(consumeType)),
                                                     LuaHelper.VoidDelegate(Consume.FramClickClose()),
                                                     getCommonStr("BUTTON_CONFIRM"),
                                                     getCommonStr("BUTTON_CANCEL"))
    end
end

function Consume.ShowBuyUI(consumeType)
    return function()
        if consumeType == "BUY_DIAMOND" then
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = consumeType
    end
end

function Consume.FramClickClose()
    return function()
        NGUITools.Destroy(Consume.buyMsg.gameObject)
        Consume.buyMsg = nil
    end
end

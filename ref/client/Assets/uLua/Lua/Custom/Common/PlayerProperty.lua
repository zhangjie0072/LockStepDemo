--encoding=utf-8

PlayerProperty =
{
    uiName = 'PlayerProperty',

    -------------------------------------
    data,
    showHonor = false,
    showPrestige = false,
    showReputation = false,
    isOpen = false,
    isVip = false,
    onCheckSetting = false,
    preGold = 0,
    forceHideHp = false,
    isInHall = false,
    isAchievement = false,
    -------------------------------------UI
    uiDiamondNum,
    uiDiamondPlus,
    uiGold,
    uiGoldNum,
    uiGoldPlus,
    -- uiHp,
    -- uiHpNum,
    -- uiHpPlus,
    uiHonor1,
    uiHonor1Num,
    uiHonor1Plus,
    uiHonor2,
    uiHonor2Num,
    uiHonor2Plus,
    uiPrestige1,
    uiPrestige1Num,
    uiPrestige2,
    uiPrestige2Num,
    uiReputation,
    uiReputationNum,
};


-----------------------------------------------------------------
--Awake
function PlayerProperty:Awake()
    local transform = self.transform

    --diamond
    self.uiDiamondNum = transform:FindChild('Diamond/Num'):GetComponent('UILabel')
    self.uiDiamondPlus = transform:FindChild('Diamond/ButtonPlus')
    addOnClick(self.uiDiamondPlus.gameObject, self:OnDiamondClick())
    -- gold
    self.uiGold = transform:FindChild('Gold')
    self.uiGoldNum = transform:FindChild('Gold/Num'):GetComponent('UILabel')
    self.uiGoldPlus = transform:FindChild('Gold/ButtonPlus')
    addOnClick(self.uiGoldPlus.gameObject, self:OnGoldClick())
    -- --hp
    -- self.uiHp = transform:FindChild('Hp')
    -- self.uiHpNum = transform:FindChild('Hp/Num'):GetComponent('UILabel')
    -- self.uiHpPlus = transform:FindChild('Hp/ButtonPlus')
    -- addOnClick(self.uiHpPlus.gameObject, self:OnHpClick())
    --honor
    self.uiHonor1 = transform:FindChild('Honor1')
    self.uiHonor1Num = transform:FindChild('Honor1/Num'):GetComponent('UILabel')
    self.uiHonor1Plus = transform:FindChild('Honor1/ButtonPlus')
    addOnClick(self.uiHonor1Plus .gameObject, self:OnHonorClick())

    self.uiHonor2 = transform:FindChild('Honor2')
    self.uiHonor2Num = transform:FindChild('Honor2/Num'):GetComponent('UILabel')
    self.uiHonor2Plus = transform:FindChild('Honor2/ButtonPlus')
    addOnClick(self.uiHonor2Plus .gameObject, self:OnHonorClick())

    --prestige
    self.uiPrestige1 = transform:FindChild('Prestige1')
    self.uiPrestige1Num = transform:FindChild('Prestige1/Num'):GetComponent('UILabel')

    self.uiPrestige2 = transform:FindChild('Prestige2')
    self.uiPrestige2Num = transform:FindChild('Prestige2/Num'):GetComponent('UILabel')

    --reputation
    self.uiReputation = transform:FindChild('Reputation')
    self.uiReputationNum = transform:FindChild('Reputation/Num'):GetComponent('UILabel')

    MainPlayer.Instance:AddDataChangedDelegate(self:OnPropertyChanged(), self.uiName..tostring(self))
    self.preGold = MainPlayer.Instance.Gold
    self:Refresh()
end

function PlayerProperty:OnDestroy()
    MainPlayer.Instance:RemoveDataChangedDelegate(self.uiName..tostring(self) )
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

--Start
function PlayerProperty:Start()
    -- body
    --判断是否为巡回赛
    if self.showPrestige then
        self.uiPrestige1.gameObject:SetActive(true)
        self.uiPrestige2.gameObject:SetActive(true)

        self.uiGold.gameObject:SetActive(false)
        --self.uiHp.gameObject:SetActive(false)

        self.uiHonor1.gameObject:SetActive(false)
        self.uiHonor2.gameObject:SetActive(false)
    end
    --判断是否为荣誉争霸赛
    if self.showHonor then
        self.uiHonor1.gameObject:SetActive(true)
        self.uiHonor2.gameObject:SetActive(true)

        self.uiGold.gameObject:SetActive(false)
        --self.uiHp.gameObject:SetActive(false)

        self.uiPrestige1.gameObject:SetActive(false)
        self.uiPrestige2.gameObject:SetActive(false)
    end
    if self.showPrestige == false and self.showHonor == false then
        self.uiHonor1.gameObject:SetActive(false)
        self.uiHonor2.gameObject:SetActive(false)

        self.uiPrestige1.gameObject:SetActive(false)
        self.uiPrestige2.gameObject:SetActive(false)

        self.uiGold.gameObject:SetActive(true)
        -- if not self.forceHideHp then
        --  self.uiHp.gameObject:SetActive(true)
        -- end
    end
    if self.isAchievement then
        self.uiHonor2.gameObject:SetActive(true)
    end
    self:RefreshReputationStore()
end

--Update
function PlayerProperty:Update( ... )
    -- body
end


-----------------------------------------------------------------
--
function PlayerProperty:Refresh( ... )
    self.uiDiamondNum.text = self:GenString(MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree)
    if self.preGold ~= MainPlayer.Instance.Gold then
        UpdateRedDotHandler.MessageHandler("Role")
        UpdateRedDotHandler.MessageHandler("Squad")
        UpdateRedDotHandler.MessageHandler("RoleDetail")
    end
    self.uiGoldNum.text = self:GenString(MainPlayer.Instance.Gold)
    self.preGold = MainPlayer.Instance.Gold
    -- local totalHp = GameSystem.Instance.TeamLevelConfigData:GetMaxHP(MainPlayer.Instance.Level)
    -- if MainPlayer.Instance.Hp > totalHp then --red
    --  self.uiHpNum.text = self:GenString("[E40000]" .. MainPlayer.Instance.Hp .. "[-]" .."[5CD14D]" .. '/' .. totalHp.."[-]")
    -- else
    --  self.uiHpNum.text = self:GenString("[5CD14D]"..MainPlayer.Instance.Hp .. '/' .. totalHp.."[-]")
    -- end

    --显示荣誉
    self.uiHonor1Num.text = self:GenString(MainPlayer.Instance.Honor)
    self.uiHonor2Num.text = self:GenString(MainPlayer.Instance.Honor2)

    --显示威望
    self.uiPrestige1Num.text = self:GenString(MainPlayer.Instance.Prestige)
    self.uiPrestige2Num.text = self:GenString(MainPlayer.Instance.Prestige2)

    --显示声望
    self.uiReputationNum.text = self:GenString(MainPlayer.Instance.Reputation)

    --self.uiHonor.gameObject:SetActive(self.showHonor)
    --self.uiPrestige.gameObject:SetActive(self.showPrestige)
end

--
function PlayerProperty:OnPropertyChanged( ... )
    return function ( ... )
        self:Refresh()
    end
end

--
function PlayerProperty:GenString(str)
    local len = string.len(str)
    return str
end

--
function PlayerProperty:OnDiamondClick( ... )
    return function (go)

        if not FunctionSwitchData.CheckSwith(FSID.recharge) then return end
        -- if not self.isVip then
        --  if self.onCheckSetting then self.onCheckSetting() end
        --  TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
        -- end
    end
end

--
function PlayerProperty:OnGoldClick()
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.gold) then return end

        if not self.isOpen then
            if self.onCheckSetting then self.onCheckSetting() end
            self:CreateBuyItem('BUY_GOLD')
        end
    end
end

--
function PlayerProperty:OnHpClick( ... )
    return function (go)
        if not self.isOpen then
            if self.onCheckSetting then self.onCheckSetting() end
            self:CreateBuyItem('BUY_HP')
        end
    end
end

--
function PlayerProperty:OnHonorClick( ... )
    return function (go)
        -- body
    end
end
--
function PlayerProperty:CreateBuyItem(str)
    self.isOpen = true
    --print('-------------12234567890')
    local UIPlayerBuyDiamondGoldHp = createUI('UIPlayerBuyDiamondGoldHP',self.transform)
    --print("************", UIPlayerBuyDiamondGoldHp)
    local pos = UIPlayerBuyDiamondGoldHp.transform.localPosition
    if self.isInHall then
        pos.x = -31
    else
        pos.x = -266
    end
    pos.y = -326
    pos.z = -700
    UIPlayerBuyDiamondGoldHp.transform.localPosition = pos
    local obj = getLuaComponent(UIPlayerBuyDiamondGoldHp)
    obj.BuyType = str
    obj.isInHall = self.isInHall
    obj.onClose = function ( ... )
        self.isOpen = false
    end
end

function PlayerProperty:RefreshReputationStore( ... )
    if self.showReputation then
        self.uiReputation.gameObject:SetActive(true)

        self.uiHonor1.gameObject:SetActive(false)
        self.uiHonor2.gameObject:SetActive(false)
        self.uiPrestige1.gameObject:SetActive(false)
        self.uiPrestige2.gameObject:SetActive(false)

        self.uiGold.gameObject:SetActive(false)
        -- self.uiHp.gameObject:SetActive(false)
    end
end

function PlayerProperty:HideHp()
    -- self.uiHp.gameObject:SetActive(false)
    self.forceHideHp = true
end


return PlayerProperty

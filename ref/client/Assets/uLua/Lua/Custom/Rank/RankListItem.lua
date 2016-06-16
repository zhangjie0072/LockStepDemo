RankListItem = {
    uiName = "RankListItem",

    rankType,
    rank = 0,
    rankInfo = nil,
    hideBG = false,
}

function RankListItem:Awake()
    self.uiBG = getChildGameObject(self.transform, "BG"):GetComponent('UISprite')
    self.lblRank = getComponentInChild(self.transform, "Rank/Text", "MultiLabel")
    self.sprRank = getComponentInChild(self.transform, "Rank/Icon", "UISprite")
    local tmIcon = self.transform:FindChild("Icon")
    self.icon = getLuaComponent(createUI("CareerRoleIcon", tmIcon))
    self.lblName = getComponentInChild(self.transform, "Name", "UILabel")
    self.lblLv = getComponentInChild(self.transform, "Lv", "UILabel")
    self.lblNum = getComponentInChild(self.transform, "Num", "UILabel")
    self.textLabel = getComponentInChild(self.transform, "Text", "UILabel")
    self.btnAdd = getChildGameObject(self.transform, "Button+")
    self.uiNum2 = getComponentInChild(self.transform, "Num2", "UILabel")
    self.lblStar = getComponentInChild(self.transform, "Num2/Num", "UILabel")

    addOnClick(self.btnAdd, self:MakeOnAdd())
    addOnClick(self.gameObject, self:MakeOnClick())
end

function RankListItem:Refresh()
    print(self.uiName,"----rank:", self.rank, "show_id:", self.rankInfo.show_id)
    if self.rank == 0 then
        self.lblRank:SetText("-")
        NGUITools.SetActive(self.sprRank.gameObject, false)
        NGUITools.SetActive(self.lblRank.gameObject, true)
    elseif self.rank <= 3 then
        self.sprRank.spriteName = self["RankIcon" .. self.rank]
        NGUITools.SetActive(self.sprRank.gameObject, true)
        NGUITools.SetActive(self.lblRank.gameObject, false)
    else
        self.lblRank:SetText(tostring(self.rank))
        NGUITools.SetActive(self.sprRank.gameObject, false)
        NGUITools.SetActive(self.lblRank.gameObject, true)
    end
    self.lblName.text = tostring(self.rankInfo.name)
    self.lblLv.text = "LV." ..self.rankInfo.level

    self.icon.id = tonumber(self.rankInfo.show_id)
    self.icon.showPosition = false
    self.icon:Refresh()
    self.icon.onClick = self:MakeOnClick()
    NGUITools.SetActive(self.lblNum.gameObject, true)
    NGUITools.SetActive(self.uiNum2.gameObject, false)
    local num
    if self.rankType == RankType.RT_LEVEL then
        num = tostring(self.rankInfo.level)
    elseif self.rankType == RankType.RT_WEALTH then
        num = tostring(self.rankInfo.wealth)
    elseif self.rankType == RankType.RT_QUALIFYING_NEW then
        NGUITools.SetActive(self.lblNum.gameObject, false)
        NGUITools.SetActive(self.uiNum2.gameObject, true)
        local grade = GameSystem.Instance.qualifyingNewerConfig:GetGrade(self.rankInfo.points)
        num = grade.title .. " " .. self.rankInfo.points
        self.uiNum2.text = grade.title
        self.lblStar.text = "×"..grade.star
    elseif self.rankType == RankType.RT_LADDER then
        local ladderLevel = GameSystem.Instance.ladderConfig:GetLevelByScore(self.rankInfo.points)
        if not ladderLevel then
            error(self.uiName, "Can not find ladder level, Score:", self.rankInfo.points)
        end
        num = ladderLevel.name .. " " .. self.rankInfo.points
    elseif self.rankType == RankType.RT_ACHIEVEMENT then
        num = tostring(self.rankInfo.points)
    end
    self.lblNum.text = num

    --self.uiBG.spriteName = (self.rank ~= 1) and self.NormalBG or self.SpecialBG
    NGUITools.SetActive(self.uiBG.gameObject, not self.hideBG)
    local isFriend = FriendData.Instance:IsFriend(self.rankInfo.acc_id)
    local isSelf = self.rankInfo.acc_id == MainPlayer.Instance.AccountID
    self.btnAdd:GetComponent("UIButton").isEnabled = not isFriend and not isSelf
end

function RankListItem:MakeOnAdd()
    return function ()
        local friendsInfo = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("FriendsInfo(Clone)")
        if friendsInfo then print(self.uiName, "Has friends info") return end

        local message = getCommonStr('STR_FRIENDS_SURE_ADD')
        self.uiSureTip = CommonFunction.ShowPopupMsg(message, nil,
            LuaHelper.VoidDelegate(self:OnBtnOK()),
            LuaHelper.VoidDelegate(self:OnBtnCancel()),
            getCommonStr("BUTTON_CONFIRM"),
            getCommonStr("BUTTON_CANCEL"))
    end
end

function RankListItem:OnBtnOK()
    return function()
        Friends.AddFriend(self.rankInfo.acc_id)
        self.btnAdd:GetComponent("UIButton").isEnabled = false
    end
end

--Cancel回调
function RankListItem:OnBtnCancel()
    return function()
        NGUITools.Destroy(self.uiSureTip.gameObject)
    end
end

function RankListItem:MakeOnClick()
    return function ()
        Friends.ShowAccountInfo(self.rankInfo.acc_id)
    end
end

return RankListItem

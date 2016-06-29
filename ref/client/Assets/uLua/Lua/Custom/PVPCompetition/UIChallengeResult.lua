--encoding=utf-8

UIChallengeResult = UIChallengeResult or
{
    uiName = 'UIChallengeResult',

    players,
    they_accid,

    --parameters
    isWin,
    leagueType,
    starNum,
    score,
    scoreDelta,
    homeScore,
    homeName,
    awayScore,
    awayName,
    --帮好友拿首胜
    assist_awards,
    assist_first_win_times,
    shiwakan_percent,
    assist_num,
    level,
    exp,
    totalExp,
    expDelta,
    awardText,
    awards,
    onConfirm,
    maxDailyIncomeData = nil,
    isHasgoodsAwards = false,
    lblGoldNum,
    lblFriestGold,
    lblFriestExp,
    lblName,
    uiRoleIcon,
    lblGoldNumMax,
    lblExpNumMax,
    ExpGoodsicon1,
    GoldGoodsicon1,

    --animation
    totalStars,
    getGoldvalue = 0,
    ExpChangeValue = 0,
    GoldChangeValue = 0,
    isPlay = false,
    isPlayOver = false,
    ExpBarChangeValue = 0,
    beginExpNum2 = 0,
    times = 0,
    finalExp,
    finalExp2,
    finalExp2Num,
    finalExpBar,
    finalGold,
    finalLevel,
    upLevel = 0,
    bfUpLevelExp = 0,
    NowLvTotalExp,
    upTimes = 0,

    tfWinIconBackShade,
    tfShowNext,
    tfLevelUp,

    tweenWinIcon,
    tweenTotalStars,
    tweenSettled1,
    tweenSettled2,

    ParticleLevelUp,
}

local resultColors =
{
    ["WinBG"] = Color.New(230/255, 185/255, 41/255, 1),
    ["WinLabel"] = Color.New(77/255, 111/255, 81/255, 1),
    ["LoseBG"] = Color.New(218/255, 221/255, 220/255, 1),
    ["LoseLabel"] = Color.New(0/255, 0/255, 0/255, 1),
}


-----------------------------------------------------------------
--Awake
function UIChallengeResult:Awake( ... )
    local transform = self.transform

    self.uiResult = transform:FindChild('Window/Result')
    self.uiDetail = transform:FindChild('Window/Detail')

    self.uiWinIcon = transform:FindChild("Window/Result/Top/Result/WinIcon")
    self.uiLoseIcon = transform:FindChild("Window/Result/Top/Result/LoseIcon")
    self.gridStars = getComponentInChild(transform, "Window/Result/Top/Result/WinIcon/Stars", "UIGrid")
    self.tmScore = transform:FindChild("Window/Result/Top/Score")
    self.labelScore = getComponentInChild(self.tmScore, "Num", "UILabel")
    self.labelScoreDelta = getComponentInChild(self.tmScore, "NumDelta", "UILabel")
    self.spriteArrow = getComponentInChild(self.tmScore, "Arrow", "UISprite")
    self.progExp = getComponentInChild(transform, "Window/Result/Settled1/Exp", "UIProgressBar")
    self.labelLevel = getComponentInChild(transform, "Window/Result/Settled1/Exp/Level", "UILabel")
    self.labelExp = getComponentInChild(transform, "Window/Result/Settled1/Exp/Num", "UILabel")
    self.lblExpNumMax = getComponentInChild(transform, "Window/Result/Settled1/Exp/NumMax", "UILabel")
    self.labelExp2 = getComponentInChild(transform, "Window/Result/Settled1/Exp/Num2", "UILabel")
    self.uiSettled1 = transform:FindChild('Window/Result/Settled1')
    self.uiSettled2 = transform:FindChild('Window/Result/Settled2')
    self.lblName = getComponentInChild(transform, "Window/Result/Settled1/Name", "UILabel")
    self.uiRoleIcon = transform:FindChild("Window/Result/Settled1/CareerRoleIcon")
    self.lblGoldNum = getComponentInChild(transform, "Window/Result/Settled1/GoldNum", "UILabel")
    self.lblGoldNumMax = getComponentInChild(transform, "Window/Result/Settled1/GoldMax", "UILabel")
    self.lblFriestExp = getComponentInChild(transform, "Window/Result/Settled1/FriestNum1", "UILabel")
    self.lblFriestGold = getComponentInChild(transform, "Window/Result/Settled1/FriestNum2", "UILabel")
    self.gridGoodsAward = getComponentInChild(self.uiSettled2.transform, "Award/GoodsList/Grid", "UIGrid")

    self.uiWinIconDetail = transform:FindChild("Window/Detail/Result/WinIcon")
    self.uiLoseIconDetail = transform:FindChild("Window/Detail/Result/LoseIcon")
    self.labelHomeNameDetail = getComponentInChild(transform, "Window/Detail/We/BGType3/Text", "UILabel")
    self.labelAwayNameDetail = getComponentInChild(transform, "Window/Detail/They/BGType3/Text", "UILabel")
    self.labelHomeScoreSprite = getComponentInChild(transform, "Window/Detail/We/WeScoreSprite", "UISprite")
    self.labelTheyScoreSprite = getComponentInChild(transform, "Window/Detail/They/TheyScoreSprite", "UISprite")
    self.labelHomeScore1 = getComponentInChild(transform, "Window/Detail/We/WeScoreSprite/WeScoreLabel", "UILabel")
    self.labelTheyScore1 = getComponentInChild(transform, "Window/Detail/They/TheyScoreSprite/TheyScoreLabel", "UILabel")

    --animation
    self.tweenWinIcon = getComponentInChild(transform, "Window/Result/Top/Result/WinIcon", "UITweener")
    self.tweenLoseIcon = getComponentInChild(transform, "Window/Result/Top/Result/LoseIcon", "UITweener")
    self.tweenSettled1 = getComponentInChild(transform, "Window/Result/Settled1", "UITweener")
    self.tweenSettled2 = getComponentInChild(transform, "Window/Result/Settled2", "UITweener")

    self.ParticleLevelUp = getComponentInChild(transform, "Window/Result/Settled1/Exp/E_Particle111", "ParticleSystem")

    self.ExpGoodsicon1 = transform:FindChild("Window/Result/Settled1/ExpGoodsicon1")
    self.GoldGoodsicon1 = transform:FindChild("Window/Result/Settled1/GoldGoodsicon1")
    self.LblExpGoodsicon1 = transform:FindChild("Window/Result/Settled1/ExpGoodsicon1/AssistFirstWin/percent"):GetComponent('UILabel')
    self.LblGoldGoodsicon1 = transform:FindChild("Window/Result/Settled1/GoldGoodsicon1/AssistFirstWin/percent"):GetComponent('UILabel')


    self.tfWinIconBackShade = transform:FindChild("Window/Result/Top/Result/BackShade")
    self.tfShowNext = transform:FindChild("Window/Result/ShowNext")
    self.tfLevelUp = transform:FindChild("Window/Result/Settled1/Exp/LevelUp")

    self.tmHomeItems = {}
    for i = 1, 3 do
        local tm = transform:FindChild("Window/Detail/We/ListGrid/ChallengeResultItem" .. i)
        table.insert(self.tmHomeItems, tm)
    end
    self.tmAwayItems = {}
    for i = 1, 3 do
        local tm = transform:FindChild("Window/Detail/They/ListGrid/ChallengeResultItem" .. i)
        table.insert(self.tmAwayItems, tm)
    end

    self.uiAniamtor = transform:GetComponent("Animator")

    self.weAddFriend = getChildGameObject(transform, "Window/Detail/We/AddFriend")
    self.theyAddFriend = getChildGameObject(transform, "Window/Detail/They/AddFriend")
end

--Start
function UIChallengeResult:Start( ... )
    addOnClick(self.uiResult.gameObject, self:OnConfirmClick())
    addOnClick(self.uiDetail.gameObject, self:OnDetailClick())
    addOnClick(self.weAddFriend, self:OnAddFriendButton())
    addOnClick(self.theyAddFriend, self:OnAddFriendButton())

    NGUITools.SetActive(self.uiWinIcon.gameObject, self.isWin)
    NGUITools.SetActive(self.uiLoseIcon.gameObject, not self.isWin)
    NGUITools.SetActive(self.uiWinIconDetail.gameObject, self.isWin)
    NGUITools.SetActive(self.uiLoseIconDetail.gameObject, not self.isWin)

    --ananimation
    self.tweenWinIcon:SetOnFinished(LuaHelper.Callback(self:WinFirstFinish()))
    self.tweenLoseIcon:SetOnFinished(LuaHelper.Callback(self:LoseFirstFinish()))

    self.totalStars = {}
    if self.starNum then
        for i = 1, self.starNum do
            local item = createUI("Star", self.gridStars.transform)
            table.insert(self.totalStars, item)
        end
    end
    self.gridStars.onReposition = function ()
        if self.starNum then
            for i = 1, #self.totalStars do
                NGUITools.SetActive(self.totalStars[i], false)
            end
        end
    end

    if self.score then
        self.labelScore.text = tostring(self.score)
        self.labelScoreDelta.text = tostring(self.scoreDelta)
        local color
        local rotation
        if self.scoreDelta > 0 then
            color = Color.New(0, 143/255, 5/255, 1)
            rotation = Quaternion.Euler(0, 0, 90)
        else
            color = Color.New(1, 0, 0, 1)
            rotation = Quaternion.Euler(0, 0, -90)
        end
        self.labelScoreDelta.color = color
        self.spriteArrow.color = color
        self.spriteArrow.transform.localRotation = rotation
        NGUITools.SetActive(self.tmScore.gameObject, true)
        if self.scoreDelta == 0 then
            self.labelScoreDelta.gameObject:SetActive(false)
            self.spriteArrow.gameObject:SetActive(false)
        end
    else
        NGUITools.SetActive(self.tmScore.gameObject, false)
    end
    self.labelHomeScore1.text = tostring(self.homeScore)
    self.labelTheyScore1.text = tostring(self.awayScore)
    self.lblName.text = MainPlayer.Instance.Name
    local t = getLuaComponent(createUI("CareerRoleIcon",self.uiRoleIcon))
    local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
    enum:MoveNext()
    t.id = enum.Current.role_id
    t.showPosition = false
    self.lblFriestGold.text = ''
    self.lblFriestExp.text = ''
    --获得物品
    if self.awards then
        local numAwards = {}
        local goodsAwards = {}
        self.getGoldvalue = 0
        local enum = self.awards:GetEnumerator()
        while enum:MoveNext() do
            local id = enum.Current.id
            local value = enum.Current.value
            print(self.uiName, "Award:", id, value)
            if id < 100 then
                if id ~= GlobalConst.TEAM_EXP_ID then
                    table.insert(numAwards, {id, value})
                elseif not self.expDelta then
                    self.expDelta = value
                end
            else
                table.insert(goodsAwards, {id, value})
                self.isHasgoodsAwards = true
            end
            if id == 2 then
                self.getGoldvalue = self.getGoldvalue + value
            end
        end

        for _, v in ipairs(numAwards) do

            -- 首胜的特殊处理
            if LuaPlayerData.awards then
                for _, val in ipairs(LuaPlayerData.awards) do
                    if val.id == v[1] then
                        local first_str = string.format(getCommonStr("STR_FIRST_WIN_AWARD1"), val.value)
                        if v[1] == 2 then
                            self.lblFriestGold.text = first_str
                            self.getGoldvalue = self.getGoldvalue + val.value
                            NGUITools.SetActive(self.lblFriestGold.gameObject, true)
                                --首胜友好度
                            if self.shiwakan_percent > 0 then 
                                NGUITools.SetActive(self.GoldGoodsicon1.gameObject,true)
                                self.LblGoldGoodsicon1.text = self.shiwakan_percent..'%'
                            end
                        end
                        break
                    end
                end
            end

            -- 物品获取上限处理
            if self.maxDailyIncomeData then
                local uptoStr = getCommonStr("STR_FIELD_PROMPT34")
                local enum = self.maxDailyIncomeData:GetEnumerator()
                while enum:MoveNext() do
                    local id = enum.Current.id
                    local value = enum.Current.value
                    warning("获取上限数据id:"..id.."value:"..value)
                    if id == v[1] and value == 1 then
                        if id == 2 then
                           self.lblGoldNumMax.text = uptoStr
                           self.getGoldvalue = v[2]
                           print('-------getGoldvalue2:' .. self.getGoldvalue)
                            NGUITools.SetActive(self.GoldGoodsicon1.gameObject,false)
                            self.LblGoldGoodsicon1.text = ''
                        end
                    end
                end
            end
        end
        for _, v in ipairs(goodsAwards) do
            local item = getLuaComponent(createUI("GoodsIcon", self.gridGoodsAward.transform))
            item.goodsID = v[1]
            item.num = v[2]
            item.hideNum = false
        end
        self.finalGold = self.getGoldvalue
    end
    --发的是0 不是协助好友，如果保存的和发过来的值一样说明上一次已经满了
    print(self.uiName,',prev assist win ',Friends.AssistFirstWin,',Current assist win ',self.assist_first_win_times,',shiwakan_percent ',self.shiwakan_percent)
    local firstwintimes = self.assist_first_win_times or 0
    if Friends.AssistFriendFirstWin < firstwintimes then 
        --协助好友拿首胜获得物品
        if self.assist_awards then
            local numAwards = {}
            local goodsAwards = {}
            self.getGoldvalue = 0
            local enum = self.assist_awards:GetEnumerator()
            while enum:MoveNext() do
                local id = enum.Current.id
                local value = enum.Current.value
                print(self.uiName, "Award:", id, value)
                if id < 100 then
                    table.insert(numAwards, {id, value})
                end
            end

            for _, v in ipairs(numAwards) do

                local first_str = ''
                if self.assist_num == 1 then 
                    first_str = string.format(getCommonStr("STR_FIRST_WIN_AWARD2"), v[2])
                elseif self.assist_num > 0 then 
                    first_str = string.format(getCommonStr("STR_FIRST_WIN_AWARD2"), v[2]/self.assist_num )..'*'..self.assist_num 
                end
                if self.assist_num > 0 then  
                    if v[1] == 2 then
                        self.lblFriestGold.text = self.lblFriestGold.text..' '..first_str
                        NGUITools.SetActive(self.lblFriestGold.gameObject, true)
                            --首胜友好度
                        if self.shiwakan_percent > 0 then 
                            NGUITools.SetActive(self.GoldGoodsicon1.gameObject,true)
                            self.LblGoldGoodsicon1.text = self.shiwakan_percent..'%'
                        else
                            NGUITools.SetActive(self.GoldGoodsicon1.gameObject,false)
                        end
                    elseif v[1] == 5 then
                        self.lblFriestExp.text =  ' '..first_str
                        NGUITools.SetActive(self.lblFriestExp.gameObject, true)
                            --首胜友好度
                        if self.shiwakan_percent > 0 then 
                            NGUITools.SetActive(self.ExpGoodsicon1.gameObject,true)
                            self.LblExpGoodsicon1.text = self.shiwakan_percent..'%'
                        else
                            NGUITools.SetActive(self.ExpGoodsicon1.gameObject, false)
                        end
                    end
                else
                    NGUITools.SetActive(self.ExpGoodsicon1.gameObject, false)
                    NGUITools.SetActive(self.GoldGoodsicon1.gameObject,false)
                end
               

            end
        end
        Friends.AssistFriendFirstWin = self.assist_first_win_times
    else
        NGUITools.SetActive(self.GoldGoodsicon1.gameObject,false)
        NGUITools.SetActive(self.ExpGoodsicon1.gameObject,false)
        if self.assist_first_win_times == 0 then 
            --不是协助首胜
        else
            --协助首胜次数已满
            self.lblFriestExp.text = ' '..CommonFunction.GetConstString('STR_ASSIST_TIMES_MAX')
            self.lblFriestGold.text = self.lblFriestGold.text..' '..CommonFunction.GetConstString('STR_ASSIST_TIMES_MAX')
        end
    end
    --获得经验
    local maxLevel = GameSystem.Instance.CommonConfig:GetUInt("gPlayerMaxLevel")
    print(self.uiName, maxLevel)
    if not self.level then
        self.level = MainPlayer.Instance.Level
    end
    if not self.exp then
        self.exp = MainPlayer.Instance.Exp
    end
    local config = GameSystem.Instance.TeamLevelConfigData
    if not self.totalExp then
        self.totalExp = config:GetMaxExp(self.level)
    end
    local prevExp = 0
    for i = 1, self.level - 1 do
        prevExp = prevExp + config:GetMaxExp(i)
    end
    print(self.uiName, "Level, Exp, PrevExp, TotalExp:", self.level, self.exp, prevExp, self.totalExp)
    self.finalExp = self.expDelta
    self.finalExp2Num = self.exp - prevExp
    self.finalExpBar = (self.exp - prevExp) / self.totalExp
    self.finalExp2 = (self.exp - prevExp) .. '/' .. self.totalExp

    self.finalLevel = "LV." .. self.level

    if self.expDelta and MainPlayer.Instance.Level < maxLevel then
        -- 首胜奖励比处理
        local first_str = ""
        if LuaPlayerData.awards then
            for _, val in ipairs(LuaPlayerData.awards) do
                if val.id == 5 then --EXP
                    first_str = string.format(getCommonStr("STR_FIRST_WIN_AWARD1"), val.value)
                    self.expDelta = self.expDelta + val.value
                    self.finalExp = self.expDelta
                    self.lblFriestExp.text = first_str..self.lblFriestExp.text 
                    NGUITools.SetActive(self.lblFriestExp.gameObject, true)
                    break
                end
            end
        end
        -- 经验获取上限处理
        if self.maxDailyIncomeData then
            local uptoStr = getCommonStr("STR_FIELD_PROMPT34")
            local enum = self.maxDailyIncomeData:GetEnumerator()
            while enum:MoveNext() do
                local id = enum.Current.id
                local value = enum.Current.value
                if id == 5 and value == 1 then
                    self.lblExpNumMax.text = uptoStr                    
                    NGUITools.SetActive(self.ExpGoodsicon1.gameObject,true)
                    self.LblExpGoodsicon1.text = ''
                end
            end
        end

        if self.exp - prevExp ==0 then
            self.beginExpNum2 = self.exp - prevExp
        else
             self.beginExpNum2 = self.exp - prevExp - self.expDelta
        end
        self.labelExp2.text = self.beginExpNum2 .. '/' .. self.totalExp
        self.progExp.value = self.beginExpNum2 / self.totalExp
    else
        self.expDelta = 0
        self.finalExp = 0
        self.beginExpNum2 = self.exp - prevExp - 0
        self.labelExp2.text = self.beginExpNum2 .. '/' .. self.totalExp
        self.progExp.value = self.beginExpNum2 / self.totalExp
        if not (MainPlayer.Instance.Level < maxLevel) then
            self.finalExpBar = 1
            self.labelExp2.text = "Max"
            self.finalExp2 = "Max"
            self.beginExpNum2 = -1
            self.progExp.value = 1
        end
    end
  
    self:FriendsBtnManager()

    LuaPlayerData.awards = nil
    local basePanel = UIManager.Instance.m_uiRootBasePanel
    if basePanel then
        local popupMsg = basePanel.transform:FindChild("PopupMessage1(Clone)")
        if popupMsg then
            popupMsg:GetComponent("UIPanel").depth = 50
        end
    end

    self.NowLvTotalExp = {}
    table.insert(self.NowLvTotalExp,0,self.totalExp)
    if (self.exp - prevExp - self.expDelta) <= 0 and MainPlayer.Instance.Level < maxLevel and self.expDelta ~= 0 then
        print('----------upLevel:' .. self.upLevel)
        self.upLevel = self.upLevel + 1
        print('----------upLevel:' .. self.upLevel)
        local leftexpDelta = self.expDelta - (self.exp - prevExp)  --获得经验剩余值
        if self.exp - prevExp == 0 then
            leftexpDelta = self.expDelta - config:GetMaxExp(1)
        end
        print('----------leftexpDelta:' .. leftexpDelta)

        local isloop = true
        local i = 1

        while isloop do
            local NowtotalExp = config:GetMaxExp(self.level - self.upLevel)  --当前等级升级总经验
            print("-----------------NowtotalExp:" .. NowtotalExp)
            print('----------leftexpDelta:' .. leftexpDelta)
            print("----------i:" .. i)
            table.insert(self.NowLvTotalExp,i,NowtotalExp)  --记录本次升级总经验值
            if NowtotalExp - leftexpDelta < 0 then  --是否继续升级
                leftexpDelta = leftexpDelta - NowtotalExp  --更新获得经验剩余值
                self.upLevel = self.upLevel + 1
                i = i + 1
            else
                self.bfUpLevelExp = NowtotalExp - leftexpDelta  --动画最初经验值
                print("----------bfUpLevelExp:" .. self.bfUpLevelExp)
                isloop = false
                if self.bfUpLevelExp == self.NowLvTotalExp[i] then
                    self.labelExp2.text = 0 .. '/' .. self.NowLvTotalExp[i]
                    self.progExp.value = 0 / self.NowLvTotalExp[i]
                else
                    self.labelExp2.text = self.bfUpLevelExp .. '/' .. self.NowLvTotalExp[i]
                    self.progExp.value = self.bfUpLevelExp / self.NowLvTotalExp[i]
                end
            end
        end
    end

    self.labelLevel.text = "LV." .. self.level - self.upLevel
    self.upTimes = self.upLevel

    ConditionallyActive.ValidateAll(self.uiName)
end

function UIChallengeResult:FixedUpdate( ... )
    --滚动数字经验条
    if self.times < 3 then
        self.times = self.times + 1
    end
    if self.isPlay and self.times == 3 then
        NGUITools.SetActive(self.lblGoldNumMax.transform.gameObject,true)
        NGUITools.SetActive(self.lblExpNumMax.transform.gameObject,true)
        self.times = 0

        self.ExpChangeValue = 1 + (self.expDelta - self.ExpChangeValue) * 0.3 + self.ExpChangeValue
        self.GoldChangeValue = 1 + (self.getGoldvalue - self.GoldChangeValue) * 0.3 + self.GoldChangeValue

        if self.upLevel > 0 then
            if self.upTimes >= 0 then
                self.ExpNum2ChangeValue = self.bfUpLevelExp + math.floor(self.ExpChangeValue)
                if self.upTimes == 0 and self.ExpNum2ChangeValue > self.NowLvTotalExp[self.upTimes] then
                    self.ExpNum2ChangeValue = self.finalExp2Num
                end
                if self.ExpNum2ChangeValue > self.NowLvTotalExp[self.upTimes] then
                    self.upTimes = self.upTimes - 1
                    self.bfUpLevelExp = 0
                    self.ExpNum2ChangeValue = 0
                    self.labelLevel.text = "LV." .. self.level - self.upTimes
                    NGUITools.SetActive(self.tfLevelUp.gameObject, true)
                    NGUITools.SetActive(self.ParticleLevelUp.gameObject, true)
                    self.ParticleLevelUp:Play()
                end
                self.labelExp2.text = self.ExpNum2ChangeValue .. '/' .. self.NowLvTotalExp[self.upTimes]
                self.progExp.value = self.ExpNum2ChangeValue / self.NowLvTotalExp[self.upTimes]
            end
        else
            if self.beginExpNum2 ~= -1 then
                self.ExpNum2ChangeValue = self.beginExpNum2 + math.floor(self.ExpChangeValue)
                self.labelExp2.text = self.ExpNum2ChangeValue .. '/' .. self.totalExp
                self.progExp.value = self.ExpNum2ChangeValue / self.totalExp
            else
                self.labelExp2.text = "Max"
                self.progExp.value = 1
            end
        end
        self.isPlay = false


        if self.ExpChangeValue < self.expDelta then
            self.labelExp.text = "+" .. math.floor(self.ExpChangeValue)
            self.isPlay = true
        else
            NGUITools.SetActive(self.labelExp.transform.gameObject,true)
            self.labelExp.text = "+" .. self.expDelta
            local prevExp = 0
            local config = GameSystem.Instance.TeamLevelConfigData
            for i = 1, self.level - 1 do
                prevExp = prevExp + config:GetMaxExp(i)
            end
            if self.exp - prevExp == 0 then
                self.labelExp2.text = '0/' .. self.totalExp
            else
                if self.beginExpNum2 ~= -1 then
                    self.labelExp2.text = self.beginExpNum2 + self.expDelta .. '/' .. self.totalExp
                else
                    self.labelExp2.text = "Max"
                end
            end
            self.progExp.value = self.finalExpBar
        end
        if self.GoldChangeValue < self.getGoldvalue then
            self.lblGoldNum.text = "+" .. math.floor(self.GoldChangeValue)
            self.isPlay = true
        else
            self.lblGoldNum.text = "+" .. self.finalGold
            NGUITools.SetActive(self.tfShowNext.gameObject,true)
            NGUITools.SetActive(self.lblGoldNum.transform.gameObject,true)
            self.isPlayOver = true
        end
    end
end

function UIChallengeResult:OnClose( ... )
    if self.onConfirm then
        self.onConfirm()
        return
    end

    if self.leagueType == GameMatch.LeagueType.eRegular1V1 then
        jumpToUI("UIHall", nil, nil)
    elseif self.leagueType == GameMatch.LeagueType.eQualifyingNew then
        jumpToUI("UIQualifyingNew", nil, {showIncStarAnim = self.isWin})
    elseif self.leagueType == GameMatch.LeagueType.ePractise1vs1 then
        print('@@aa1111111111111')
        jumpToUI("UIPracticeCourt1", nil, nil)
    elseif self.leagueType == GameMatch.LeagueType.eCareer then
        if CurSectionComplete then
            jumpToUI("UICareer", nil, {chapterID = CurChapterID})
        else
            jumpToUI("UICareer", nil, nil)
        end
    elseif self.leagueType == GameMatch.LeagueType.ePVP then
        print("1927 - <UIChallengeResult>  self.ladderRewardWin.Count, self.league_extra_score=",self.ladderRewardWin.Count, self.league_extra_score)
        local winNum = Ladder.GetWinNumByLeagueInfo(self.ladderRewardWin)
        MainPlayer.Instance.pvpLadderInfo.league_awards_flag = self.league_extra_score
        -- if self.ladderLevelState ~= 0 then
            -- self.gameObject:SetActive(false)
            -- TopPanelManager:ShowPanel("LadderResult", nil, {	isWin = self.ladderLevelState > 0, ladderRewardWin = self.ladderRewardWin, league_extra_score = self.league_extra_score})
            -- jumpToUI("UIHall", nil, nil)
        -- elseif self.ladderRewardWin.Count == 5 and self.league_extra_score == 1 then
        --     self.gameObject:SetActive(false)
        --     TopPanelManager:ShowPanel("LadderAcquire", nil, { ladderRewardWin = self.ladderRewardWin})
        -- else
        Ladder:PrepareBackToLadder()
            -- jumpToUI("UIHall", nil, nil)
        -- end
    elseif self.leagueType == GameMatch.LeagueType.eQualifyingNewer then
        QualifyingNewer.PrepareBackToQualifyingNewer(self.isWin)
    end
end

function UIChallengeResult:OnDestroy( ... )
    -- body
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UIChallengeResult:InitPlayerMatchData(
    roleInfo, playerID, score, shootTimes, onTarget, farShootTimes, farOnTarget, assist, rebound, steal, block, mvp, index, home, name)
    print("---------***---------")
    print("---------*** playerID:", playerID)
    print("---------*** transform:", self.transform.name)
    print('---------*** score: ', score)
    print('---------*** shootTimes: ', shootTimes)
    print('---------*** onTarget: ', onTarget)
    print('---------*** farShootTimes: ', farShootTimes)
    print('---------*** farOnTarget: ', farOnTarget)
    print('---------*** assist: ', assist)
    print('---------*** rebound: ', rebound)
    print('---------*** steal: ', steal)
    print('---------*** block: ', block)
    print('---------*** mvp: ', mvp)


    if not self.players then
        self.players = {}
    end
    local player = {}
    player.roleinfo = roleInfo
    player.home = home
    table.insert(self.players, player)

    local uiResultItem
    if home == true then
        uiResultItem = self.tmHomeItems[index]
    else
        uiResultItem = self.tmAwayItems[index]
    end

    local item = getLuaComponent( createUI("ChallengeResultItem", uiResultItem))
    item.id = playerID
    item.isMvp = mvp
    item.shootTimes = shootTimes
    item.onTarget = onTarget
    item.farShootTimes = farShootTimes
    item.farOnTarget = farOnTarget
    item.rebound = rebound
    item.assist = assist
    item.steal = steal
    item.block = block
    item.score = score
    item.roleInfo = roleInfo
    if name then
        item.name = name
    end
end

--返回确定处理
function UIChallengeResult:OnConfirmClick()
    return function (go)
        --动画是否播放完毕
        if self.isPlayOver then
            --是否有物品奖励
            if self.isHasgoodsAwards then
                self:ShowSettled2()
                self.isHasgoodsAwards = false
                return
            end
            if self.uiAnimator then
                self:AnimClose()
            else
                self:OnClose()
            end
        else
            --强制结束动画
            self.labelLevel.text = self.finalLevel
            self.isPlayOver = true
            self.tweenWinIcon.enabled = false
            self.tweenWinIcon.transform.localScale = Vector3.New(1,1,1)
            self.tweenLoseIcon.enabled = false
            self.tweenLoseIcon.transform.localScale = Vector3.New(1,1,1)
            if self.starNum and self.starNum ~= 0 then
                for k, v in pairs(self.totalStars) do
                    NGUITools.SetActive(v.transform.gameObject,true)
                    v.transform.localScale = Vector3.New(1,1,1)
                end
                if self.tweenTotalStars then
                    for k, v in pairs(self.tweenTotalStars) do
                        if v ~= nil and v.enabled ~= nil then
                            v.enabled = false
                        end
                    end
                end
            end
            self.isPlay = false
            self.labelExp.text = "+" .. self.finalExp
            self.labelExp2.text = self.finalExp2
            self.progExp.value = self.finalExpBar
            self.lblGoldNum.text = "+" .. self.finalGold
            NGUITools.SetActive(self.tfShowNext.gameObject,true)
            NGUITools.SetActive(self.labelExp.transform.gameObject,true)
            NGUITools.SetActive(self.lblGoldNum.transform.gameObject,true)
            if self.upLevel > 0 then
                NGUITools.SetActive(self.tfLevelUp.gameObject, true)
            end
        end
    end
end

function UIChallengeResult:OnDetailClick()
    return function ()
        self:ShowResult()
    end
end

function UIChallengeResult:ShowDetail()
    NGUITools.SetActive(self.uiResult.gameObject, false)
    NGUITools.SetActive(self.uiDetail.gameObject, true)
end

function UIChallengeResult:ShowResult()
    NGUITools.SetActive(self.uiResult.gameObject, true)
    NGUITools.SetActive(self.uiDetail.gameObject, false)
end

function UIChallengeResult:ShowSettled2()
    self.tweenSettled1.enabled = true
    self.tweenSettled2.enabled = true
end

function UIChallengeResult:FriendsBtnManager()
    NGUITools.SetActive(self.weAddFriend, false)
    NGUITools.SetActive(self.theyAddFriend, false)

    local match_1v1_tag = (self.leagueType == GameMatch.LeagueType.eRegular1V1 or self.leagueType == GameMatch.LeagueType.eQualifyingNew)
    if not match_1v1_tag then
        return
    end

    local self_id = MainPlayer.Instance.AccountID
    for k, v in pairs(self.players) do
        local roleInfo = v.roleinfo
        local home = v.home

        if self_id ~= v.roleinfo.acc_id and v.roleinfo.acc_id ~= 0 then
            local isFri = Friends.IsFriend(v.roleinfo.acc_id)--FriendData.Instance:IsFriend(v.roleinfo.acc_id)
            if isFri then
                return
            end

            self.they_accid = v.roleinfo.acc_id
            if home then
                NGUITools.SetActive(self.weAddFriend, true)
            else
                NGUITools.SetActive(self.theyAddFriend, true)
            end
            break;
        end
    end
end

function UIChallengeResult:OnAddFriendButton()
    return function(go)
        local friend_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY)
        local gFriendsMax = GameSystem.Instance.CommonConfig:GetUInt("gFriendsMax")
        if friend_count >= gFriendsMax then
            CommonFunction.ShowTip(getCommonStr("REACH_FRIENDS_MAX"), nil)
            return
        end

        print("UIChallengeResult:OnAddFriendButton")
        local req = {
            type = 'FOT_ADD',
            op_friend = {
                acc_id = self.they_accid,
                --plat_id = self.friendData.plat_id,
            },
        }

        local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)

       NGUITools.SetActive(self.weAddFriend, false)
       NGUITools.SetActive(self.theyAddFriend, false)
    end
end

--LoseIcon放大播放完毕
function UIChallengeResult:LoseFirstFinish()
    return function ()
        local gameObejct = self.tweenLoseIcon.gameObject
        Object.DestroyImmediate(self.tweenLoseIcon)
        self.tweenLoseIcon = TweenScale.Begin(gameObejct, 0.2, Vector3.New(1,1,1))
        self.tweenLoseIcon.delay = 0
        self.tweenLoseIcon:SetOnFinished(LuaHelper.Callback(self:LoseSecondFinish()))
    end
end

--LoseIcon缩小播放完毕
function UIChallengeResult:LoseSecondFinish()
    return function ()
        self.isPlay = true
        NGUITools.SetActive(self.labelExp.transform.gameObject,true)
        NGUITools.SetActive(self.lblGoldNum.transform.gameObject,true)
    end
end

--WinIcon放大播放完毕
function UIChallengeResult:WinFirstFinish()
    return function ()
        NGUITools.SetActive(self.tfWinIconBackShade.gameObject, true)
        local gameObejct = self.tweenWinIcon.gameObject
        Object.DestroyImmediate(self.tweenWinIcon)
        self.tweenWinIcon = TweenScale.Begin(gameObejct, 0.2, Vector3.New(1,1,1))
        self.tweenWinIcon.delay = 0
        self.tweenWinIcon:SetOnFinished(LuaHelper.Callback(self:WinSecondFinish()))
    end
end

--WinIcon缩小播放完毕
function UIChallengeResult:WinSecondFinish()
    return function ()
        --给所有星星添加放大动画
        if self.starNum then
            self.tweenTotalStars = {}
            for k, v in ipairs(self.totalStars) do
                self.totalStars[k].transform.localScale = Vector3.New(0.8,0.8,1)
                local tween = TweenScale.Begin(self.totalStars[k], 0.4, Vector3.New(1.5,1.5,1))
                tween.delay = 0
                table.insert(self.tweenTotalStars, tween)
            end
            self:SetTotalStarsFinish()
            NGUITools.SetActive(self.totalStars[1], true)
        else
            self.isPlay = true
            NGUITools.SetActive(self.labelExp.transform.gameObject,true)
            NGUITools.SetActive(self.lblGoldNum.transform.gameObject,true)
        end
    end
end
--设置所有星星放大动画Onfinished
function UIChallengeResult:SetTotalStarsFinish()
    for k, v in ipairs(self.tweenTotalStars) do
        --所有星星添加缩小动画
        v:SetOnFinished(LuaHelper.Callback(function ()
                Object.DestroyImmediate(v)
                v = TweenScale.Begin(self.totalStars[k], 0.25, Vector3.New(1,1,1))
                v.delay = 0
                v:SetOnFinished(LuaHelper.Callback(function ()
                --缩小后显示下一颗星
                if (k + 1) <= #self.tweenTotalStars then
                    NGUITools.SetActive(self.totalStars[k + 1], true)
                else
                    self.isPlay = true
                    NGUITools.SetActive(self.labelExp.transform.gameObject,true)
                    NGUITools.SetActive(self.lblGoldNum.transform.gameObject,true)
                end
            end))
        end))
    end
end

return UIChallengeResult

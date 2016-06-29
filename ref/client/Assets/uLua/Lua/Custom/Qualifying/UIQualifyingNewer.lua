require "Custom/Team/UICreateTeam"

module("UIQualifyingNewer", package.seeall)

uiName = "UIQualifyingNewer"

--parameters
showIncStarAnim = false

--variables
qualifying = nil
qualifyingConfig = nil
maxQualifyingConfig = nil
qualifyingSeason = nil

matchType = "MT_QUALIFYING_NEWER"
leagueType = GameMatch.LeagueType.eQualifyingNewer

uiSelectRole = nil
tip = nil
popupCountDown = nil
awardTip = nil
lastLightStar = nil
lastLightStar2 = nil
streakLights = nil
stars = nil

gameStartRespBuf = nil
gameStartResp = nil

actionOnTimer = nil
actionOnDisconnected = nil
actionOnReconnected = nil
onAnimResp = nil

confirmWaitTime = 0
isMatching = false
isWaitingConfirm = false
gameModeIDs = nil
gameModeIDOthers = nil
showWinningStreakStarAnim = false
showingPrevGrade = false
leagueReset = nil

function Awake(self)
    self.btnRule = getChildGameObject(self.transform, "Top/Rule")
    self.btnStore = getChildGameObject(self.transform, "Top/Store")
    self.labelTotalRound = getComponentInChild(self.transform, "Left/TotalRound/Num", "UILabel")
    self.labelWins = getComponentInChild(self.transform, "Left/Wins/Num", "UILabel")
    self.labelWinRate = getComponentInChild(self.transform, "Left/WinRate/Num", "UILabel")
    self.labelWinningStreak = getComponentInChild(self.transform, "Left/WinningStreak/Num", "UILabel")
    self.labelHighest = getComponentInChild(self.transform, "Left/Highest/Label", "UILabel")
    self.labelStartTime = getComponentInChild(self.transform, "Right/StartTime/Label", "UILabel")
    self.labelEndTime = getComponentInChild(self.transform, "Left/EndTime/Label", "UILabel")
    self.btnAward = getChildGameObject(self.transform, "Right/Award")
    self.awardText = getComponentInChild(self.transform, "Right/Award/Label", "UILabel")
    self.spriteAward = getComponentInChild(self.transform, "Right/Award", "UISprite")
    self.uiFightAwardBox = getComponentInChild(self.transform, "Right/Box", "UISprite")
    self.spriteIcon = getComponentInChild(self.transform, "Middle/Icon", "UISprite")
    self.labelGrade = getComponentInChild(self.transform, "Middle/StarLeague", "UILabel")
    self.labelRanking = getComponentInChild(self.transform, "Middle/StarLeague/Num", "UILabel")
    self.labelScore = getComponentInChild(self.transform, "Middle/Points/Num", "UILabel")
    self.gridStar = getComponentInChild(self.transform, "Middle/StarGrid", "UIGrid")
    self.btnSingleMatch = getChildGameObject(self.transform, "Middle/Start")
    self.btnTeamMatch = getChildGameObject(self.transform, "Middle/Start2")
    -- self.labelStart = getComponentInChild(self.transform, "Middle/Start/Label", "UILabel")
    self.tmWinningStreak = self.transform:FindChild("Middle/WinningStreak")
    --self.goEffectStar = getChildGameObject(self.transform, "Middle/E_Star")
    self.goEffectWinningStreak = getChildGameObject(self.transform, "Middle/E_Star3")
    self.uixN = getComponentInChild(self.transform, "Middle/xN", "UILabel")

    self.goIconShine = getChildGameObject(self.transform, "Middle/Icon")
    self.btnBack = getLuaComponent(createUI("ButtonBack", self.transform:FindChild("Top/ButtonBack")))
    --self.btnBack.delay = 0.47
    Object.DontDestroyOnLoad(self.btnBack.gameObject)

    self.uiComboGrid = getComponentInChild(self.transform, "Middle/WinningStreak/Grid", "UIGrid")
    -- self.streakLights = {}
    -- for i = 1, self.tmWinningStreak.transform.childCount do
    --     table.insert(self.streakLights, self.tmWinningStreak.transform:GetChild(i - 1))
    -- end

    self.fightAwardIcons = {}
    for i = 1, 3 do
        local s = getComponentInChild(self.transform, "Right/Grid/"..i.."/Icon", "UISprite")
        table.insert(self.fightAwardIcons, s)
    end
    self.uiFightAwardResetTime = getComponentInChild(self.transform, "Right/FightAwardResetTime", "UILabel")


    self.leagueReset =  {}
    local resets = GameSystem.Instance.CommonConfig:GetString("gQualifyingNewerLeagueResetTime")
    for w in string.gmatch(resets, "[^&]+") do
        table.insert(self.leagueReset, tonumber(w))
    end

    addOnClick(self.btnRule, self:MakeOnRule())
    addOnClick(self.btnStore, self:MakeOnStore())
    -- addOnClick(self.btnStart, self:MakeOnStart())
    addOnClick(self.btnSingleMatch, self:MakeOnSingleMatch())
    addOnClick(self.btnTeamMatch, self:MakeOnTeamMatch())
    self.btnBack.onClick = self:MakeOnBack()
    --UIEventListener.Get(self.btnAward).onPress = LuaHelper.BoolDelegate(self:MakeOnAwardPress())
    addOnClick(self.btnAward, self:MakeOnAwardClick())
    --UIEventListener.Get(self.uiFightAwardBox.gameObject).onPress = LuaHelper.BoolDelegate(self:MakeOnFightAwardPress())
    addOnClick(self.uiFightAwardBox.gameObject, self:MakeOnFightAwardClick())


    self.onAnimResp = self:MakeOnAnimResp()

    self.animator = self.transform:GetComponent("Animator")
    local animationResp = self.transform:GetComponent("AnimationResp")
    animationResp:AddResp(self.onAnimResp, self.gameObject)

    animationResp = self.goEffectWinningStreak:GetComponent("AnimationResp")
    animationResp:AddResp(self.onAnimResp, self.gameObject)

    animationResp = self.goIconShine:GetComponent("AnimationResp")
    animationResp:AddResp(self.onAnimResp, self.gameObject)
    NGUITools.SetActive(self.goIconShine, true)
end

function Start(self)
    if not UIQualifyingNewer.gameModeIDs then
        UIQualifyingNewer.gameModeIDs = {}
        local i = 1
        for i = 1, 10000000 do
            local gameModeID = GameSystem.Instance.CommonConfig:GetUInt("gRegular1V1GameModeID" .. i)
            if gameModeID ~= 0 then
                table.insert(UIQualifyingNewer.gameModeIDs, gameModeID)
            else
                break
            end
        end
    end

    if not UIQualifyingNewer.gameModeIDOthers then
        UIQualifyingNewer.gameModeIDOthers = {}
        local strGameModeIDOthers = GameSystem.Instance.CommonConfig:GetString("gRegular1V1GameModeIDOthers")
        local strIDs = string.split(strGameModeIDOthers, "&")
        for _, strID in ipairs(strIDs) do
            print("GameModeID:", strID)
            table.insert(UIQualifyingNewer.gameModeIDOthers, tonumber(strID))
        end
    end

    LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, self:HandleStartMatchResp(), self.uiName)
    -- LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyGameStartID, self:HandleNotifyGameStart(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.RefreshQualifyingNewerInfoID, self:HandleRefreshQualiyfingNewerInfo(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyMatchInfoID, self:MatchInfoHandler(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.GetQualifyingNewerLeagueAwardsRespID, self:GetFightAwardsHandler(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.GetQualifyingNewerGradesAwardsRespID, self:GetGradeAwardsHandler(), self.uiName)

    self.actionOnTimer = LuaHelper.Action(self:OnTimer())
    self.actionOnDisconnected = LuaHelper.Action(self:OnDisconnected())
    self.actionOnReconnected = LuaHelper.Action(self:OnReconnected())
    PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected + self.actionOnDisconnected
    PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionOnReconnected

end

function OnDestroy(self)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.StartMatchRespID, self.uiName)
    -- LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.RefreshQualifyingNewerInfoID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyMatchInfoID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetQualifyingNewerLeagueAwardsRespID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetQualifyingNewerGradesAwardsRespID, self.uiName)

    PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected - self.actionOnDisconnected
    PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionOnReconnected
end

function Refresh(self)
    print("1927 - <UIQualifyingNewer>  self.showIncStarAnim=",self.showIncStarAnim)

    if self.removeAccIdForBack then
        local inTeam = false
        for k, v in pairs(self.removeAccIdForBack) do
            print("1927 - <UIQualifyingNewer> removeAccIdForBack v=",v)
            print("1927 - <UIQualifyingNewer>  MainPlayer.Instance.AccountID=",MainPlayer.Instance.AccountID)
            if v == MainPlayer.Instance.AccountID then
                inTeam  = true
            end
        end
        if not inTeam then
            self.tip = getLuaComponent(createUI("PvpTipNew"))
            local pos = self.tip.transform.localPosition
            pos.z = -500
            self.tip.transform.localPosition = pos
            UIManager.Instance:BringPanelForward(self.tip.gameObject)
            self.tip.averTime = QualifyingNewer.lastAverTime
            self.tip.matchType = matchType
            self.tip.onCancel = self:OnCancelMatch()
            self.isMatching = true
        else
            if self.tip then
                self.tip:Close()
                self.tip = nil
            end
            self.isMatching = false
        end

        -- self:PartternMode(not inTeam)
        self.removeAccIdForBack = nil -- reset.
        return
    end

    self.qualifying = MainPlayer.Instance.QualifyingNewerInfo
    self.qualifyingScore = MainPlayer.Instance.QualifyingNewerScore

    -- TODO: Test Score
    -- self.qualifyingScore = 1790
    -- self.qualifying.max_score = self.qualifyingScore


    self.qualifyingConfig = GameSystem.Instance.qualifyingNewerConfig:GetGrade(self.qualifyingScore)

    local comboNum = self.qualifyingConfig.combo
    CommonFunction.ClearGridChild(self.uiComboGrid.transform)
    self.streakLights = {}
    for i = 1, comboNum do
        local t = getLuaComponent(createUI("Combo", self.uiComboGrid.transform))
        table.insert(self.streakLights, t)
    end
    self.uiComboGrid.repositionNow = true

    -- for i = 1, self.tmWinningStreak.transform.childCount do
    --     table.insert(self.streakLights, self.tmWinningStreak.transform:GetChild(i - 1))
    -- end


    print("1927 - <UIQualifyingNewer>  self.qualifying=",self.qualifying)
    print(self.uiName, "score:", self.qualifyingScore, "max score:", self.qualifying.max_score)
    print("1927 - <UIQualifyingNewer>  GameSystem.Instance.qualifyingNewerConfig=",GameSystem.Instance.qualifyingNewerConfig)

    if not self.qualifyingConfig then
        error(self.uiName, "Can not find qualifying config.")
    end
    local max_score = math.max(self.qualifying.max_score, self.qualifyingScore)
    self.maxQualifyingConfig = GameSystem.Instance.qualifyingNewerConfig:GetGrade(max_score)
    if not self.maxQualifyingConfig then
        error(self.uiName, "Can not find max qualifying config.")
    end
    self.qualifyingSeason = GameSystem.Instance.qualifyingNewerConfig:GetSeason(self.qualifying.season)
    if not self.qualifyingSeason then
        error(self.uiName, "Can not find season config. season:", self.qualifying.season)
    end

    print("1927 - <UIQualifyingNewer>  self=",self)
    print("1927 - <UIQualifyingNewer>  self.qualifying.race_times=",self.qualifying.race_times)
    self.labelTotalRound.text = tostring(self.qualifying.race_times)
    self.labelWins.text = tostring(self.qualifying.win_times)
    if self.qualifying.race_times == 0 then
        self.labelWinRate.text = "0%"
    else
        self.labelWinRate.text = math.floor(self.qualifying.win_times / self.qualifying.race_times * 100) .. "%"
    end
    self.labelWinningStreak.text = tostring(self.qualifying.max_winning_streak)
    self.labelHighest.text = self.maxQualifyingConfig.title
    self.spriteIcon.spriteName = self.qualifyingConfig.icon
    self.labelGrade.text = self.qualifyingConfig.title
    if self.qualifying.ranking == 0 then
        self.labelRanking.text = getCommonStr("NO_RANKING")
    else
        self.labelRanking.text = getCommonStr("RANK_SINGLESRCTION"):format(self.qualifying.ranking)
    end
    self.labelScore.text = tostring(self.qualifyingScore)
    local list = Split(self.qualifyingSeason.start_time," ")
    self.labelStartTime.text = list[1]
    list = Split(self.qualifyingSeason.end_time," ")
    list = Split(list[1], "-")
    self.labelEndTime.text = string.format(getCommonStr("STR_MONTH_DAY"),
                                        tonumber(list[2]),
                                        tonumber(list[3]))

    -- get award icon by first grade
    local gradeAwards = self.qualifying.grade_awards
    local displayGrade
    local qConfig = GameSystem.Instance.qualifyingNewerConfig
    print("1927 - <UIQualifyingNewer>  gradeAwards, self.qualifying.grade_awards_flag=",gradeAwards, self.qualifying.grade_awards_flag)

    if self.qualifying.grade_awards_flag == 1 then
        displayGrade = qConfig:GetFirstSubGrade(gradeAwards)
    else
        displayGrade = qConfig:GetNextSubSection(self.qualifyingScore)
        print("1927 - <UIQualifyingNewer>  displayGrade.title, displayGrade.score=",displayGrade.title, displayGrade.score)
        print("1927 - <UIQualifyingNewer>  self.qualifyingScore=",self.qualifyingScore)
        if not displayGrade then
            displayGrade = qConfig:GetGrade(self.qualifyingScore)
        else
            print("1927 - <UIQualifyingNewer>  displayGrade.title, displayGrade.score=",displayGrade.title, displayGrade.score)
            print("1927 - <UIQualifyingNewer>  displayGrade.award_id=",displayGrade.award_id)
        end
        while displayGrade.award_id == 0 do
            displayGrade = qConfig:GetNextSubSection(displayGrade.score)
        end
    end
    self.awardText.text = string.format(getCommonStr("STR_LELVEL_AWARD"), displayGrade.title)
    print("1927 - <UIQualifyingNewer> 333 displayGrade.title=",displayGrade.title)

    print("1927 - <UIQualifyingNewer>  displayGrade.section=",displayGrade.section)
    self.spriteAward.spriteName = displayGrade.award_icon


    -- fight award.
    print("1927 - <UIQualifyingNewer>  self.qualifying.league_awards_flag=",self.qualifying.league_awards_flag)

    if self.qualifying.league_awards_flag == 1 then
        print("1927 - <UIQualifyingNewer> Send GetQualifyingNewerLeagueAwardsReq")
        local operation = {}
        local req = protobuf.encode("fogs.proto.msg.GetQualifyingNewerLeagueAwardsReq", operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.GetQualifyingNewerLeagueAwardsReqID,req)
    else

    end


    if self.qualifyingConfig.upgrade_score ~= 0 and
        self.qualifyingScore >= self.qualifyingConfig.upgrade_score then
        -- self.labelStart.text = getCommonStr("STR_QUALIFYING_UPGRADE")
    else
        -- self.labelStart.text = getCommonStr("STR_JOIN_GAME")
    end

    print(self.uiName, "Winning streak:", self.qualifying.winning_streak)
    print(self.uiName, "combo win:", self.qualifying.combo_win)
    local lightNum = self.qualifying.combo_win % #self.streakLights
    local winningStreakFull = (lightNum == 0 and self.qualifying.winning_streak > 0)
    if self.animator and self.animator.enabled then
        if self.showIncStarAnim then
            if winningStreakFull then
                self.showWinningStreakStarAnim = true
            end
            if self.qualifyingConfig.star == 1 then		-- show prev grade
                self.prevGrade = GameSystem.Instance.qualifyingNewerConfig:GetPrevGrade(self.qualifyingScore)
                if self.prevGrade then
                    self.showingPrevGrade = true
                    self.spriteIcon.spriteName = self.prevGrade.icon
                    if self.showWinningStreakStarAnim then
                        self:DisplayStar(self.prevGrade.star - 1, self.prevGrade.section)
                        self:DisplayWinningStreak(#self.streakLights)
                    else
                        self:DisplayStar(self.prevGrade.star, self.prevGrade.section)
                        self:DisplayWinningStreak(lightNum)
                    end
                end
            end
        end
    else
        self.showIncStarAnim = false
        self.showWinningStreakStarAnim = false
    end


    local starNum = self.qualifyingConfig.star
    local xN
    local qMaxScore = GameSystem.Instance.qualifyingNewerConfig.MaxScore
    print("1927 - <UIQualifyingNewer>  qMaxScore=",qMaxScore)
    if  self.qualifyingScore > qMaxScore then
        starNum = 1
        xN = math.modf((self.qualifyingScore - qMaxScore)/10) - 1
    end

    if self.showWinningStreakStarAnim then
        self:DisplayStar(starNum - 2, self.qualifyingConfig.section, xN)
        self:DisplayWinningStreak(#self.streakLights)
    elseif self.showIncStarAnim then
        self:DisplayStar(starNum - 1, self.qualifyingConfig.section, xN)
        self:DisplayWinningStreak(lightNum)
    else
        self:DisplayStar(starNum, self.qualifyingConfig.section, xN)
        self:DisplayWinningStreak(lightNum)
    end

    -- Fight Awards
    local enum = self.qualifying.league_info:GetEnumerator()
    local kc = 0
    local winNum = 0
    while enum:MoveNext() do
        kc = kc + 1
        local v = enum.Current
        self.fightAwardIcons[kc].spriteName = v == 0 and "tencent_fivelose" or "tencent_fivewin"
        if v ~= 0 then
            winNum = winNum + 1
        end
    end
    print("1927 - <UIQualifyingNewer> fightAwardsIcon kc=",kc)

    for i = kc + 1,  3 do
        self.fightAwardIcons[i].gameObject:SetActive(false)
    end
end


function FixedUpdate(self)
    local qualifingNewerInfo = MainPlayer.Instance.QualifyingNewerInfo

    if qualifingNewerInfo.league_info.Count == 3 then
        local t = os.date("*t", GameSystem.Instance.mTime)
        local tHour = 0
        local tr = self.leagueReset
        for k, v in pairs(tr) do
            if v > t.hour then
                tHour = v
                break
            end
            tHour = 24 + tr[1]
        end

        local h = tHour - t.hour
        local m = 0

        if t.min ~= 0 then
            h = h - 1
            m = 60 - t.min
        end

        if h <= 0 then
            self.uiFightAwardResetTime.text = string.format(getCommonStr("RESET_REWARD_BY_MINUTE"), m)
        else
            self.uiFightAwardResetTime.text = string.format(getCommonStr("RESET_REWARD_BY_HOUR"), h)
        end
    else
        self.uiFightAwardResetTime.text = getCommonStr("STR_CAN_GET_AFTER_MATCH")
    end
end

function DisplayWinningStreak(self, winningStreak)
    for i = 1, #self.streakLights do
        local tm = self.streakLights[i]
        local light = (i <= winningStreak)
        tm:SetOn(light)
        -- NGUITools.SetActive(tm:FindChild("On").gameObject, light)
        -- NGUITools.SetActive(tm:FindChild("Off").gameObject, not light)
    end
end

function DisplayStar(self, lightStarNum, section, xN)
    local maxStarNum = GameSystem.Instance.qualifyingNewerConfig:GetMaxStarNum(section)

    self.uixN.gameObject:SetActive(xN ~= nil)
    if xN then
        self.uixN = "X"..xN
        maxStarNum = 1
    end

    print(self.uiName, "DisplayStar:", lightStarNum, maxStarNum, "Section:", section)

    self.stars = self.stars or {}
    for i = 1, math.max(maxStarNum, #self.stars) do
        if i > maxStarNum then
            if self.stars[i] then
                NGUITools.Destroy(self.stars[i])
            end
            self.stars[i] = nil
        else
            if not self.stars[i] then
                local go = createUI("Star", self.gridStar.transform)
                self.stars[i] = go
                local animationResp = getComponentInChild(go.transform, "E_Star", "AnimationResp")
                animationResp:AddResp(self.onAnimResp, self.gameObject)
            end
            local light = (i <= lightStarNum)
            LightenStar(self.stars[i], light)
        end
    end
    self.gridStar:Reposition()
end

function LightenStar(go, light)
    NGUITools.SetActive(go, light)
    NGUITools.SetActive(go, not light)
end

function ShowStarAnim(self, index)
    if index == nil or index == 0 then
        return
    end
    self.animatingStarIndex = index
    local tmEffect = self.stars[index].transform:FindChild("E_Star")
    local animator = tmEffect:GetComponent("Animator")
    animator:SetTrigger("Star")
end

function ShowWinningStreakAnim(self)
    print(self.uiName, "ShowWinningStreakAnim")
    self:DisplayWinningStreak(0)
    NGUITools.SetActive(self.goEffectWinningStreak, true)
    local animator = self.goEffectWinningStreak:GetComponent("Animator")
    animator:SetTrigger("Star3")
end

function MakeOnAnimResp(self)
    return function (value)
        if value == "BornFinish" then		-- UI animation finished
            print(self.uiName, "showIncStarAnim", self.showIncStarAnim)
            if self.showIncStarAnim then
                if self.showingPrevGrade then
                    if not self.showWinningStreakStarAnim then
                        self.goIconShine:GetComponent("Animator"):SetTrigger("Play")
                    else
                        self:ShowStarAnim(#self.stars)
                    end
                elseif self.showWinningStreakStarAnim then
                    self:ShowStarAnim(self.qualifyingConfig.star - 1)
                else
                    self:ShowStarAnim(self.qualifyingConfig.star)
                end
                self.showIncStarAnim = false
            end
        elseif value == "StarFinish" then	-- star animation finised
            print(self.uiName, "OnResp StarFinish", self.showWinningStreakStarAnim)
            if self.showingPrevGrade then
                -- 显示段位图标动画
                self:DisplayStar(self.prevGrade.star, self.prevGrade.section)
                NGUITools.SetActive(self.goIconShine, true)
                self.goIconShine:GetComponent("Animator"):SetTrigger("Play")
                self.showingPrevGrade = false
            elseif self.showWinningStreakStarAnim then
                self:DisplayStar(self.animatingStarIndex, self.qualifyingConfig.section)
                self:ShowWinningStreakAnim()
                self.showWinningStreakStarAnim = false
                print(self.uiName, "OnResp set showWinningStreakStarAnim to false")
                self.animatingStarIndex = self.animatingStarIndex + 1
            else
                self:DisplayStar(self.animatingStarIndex, self.qualifyingConfig.section)
            end
        elseif value == "Star3Finish" then	-- winning streak animation finished
            self:ShowStarAnim(self.animatingStarIndex)
        elseif value == "IconShineFinish" then
            -- 显示当前段位
            self.spriteIcon.spriteName = self.qualifyingConfig.icon
            self:DisplayStar(0, self.qualifyingConfig.section)
            self.showingPrevGrade = false
            -- 显示连胜动画
            if self.showWinningStreakStarAnim then
                Scheduler.Instance:AddTimer(0.5, false, function ()
                    self:ShowWinningStreakAnim()
                    self.animatingStarIndex = 1
                end)
                self.showWinningStreakStarAnim = false
            else    --直接显示一颗星
                self:ShowStarAnim(1)
            end
        end
    end
end

function LightenStar(go, light)
    NGUITools.SetActive(go.transform:FindChild("StarY").gameObject, light)
    NGUITools.SetActive(go.transform:FindChild("StarG").gameObject, not light)
end

function HandleRefreshQualiyfingNewerInfo(self)
    return function ()
        print(self.uiName, "HandleRefreshQualiyfingNewerInfo")
        self:Refresh()
    end
end

function MatchInfoHandler(self)
    return function(buf)
        if self.tip ~= nil then
            self.tip:Close()
            self.tip = nil
        end

        print("1927 - <UIQualifyingNewer> MatchInfoHandler")
        local resp, error = protobuf.decode('fogs.proto.msg.NotifyMatchInfo',buf)
        if resp then
            print("1927 - <UIQualifyingNewer>  resp.ai_flag=",resp.ai_flag)
            if resp.ai_flag == 0 then
                local nextShowUIParams = {
                    teamInfo = resp.team_info,
                    isLadder = true,
                    startLabel=getCommonStr("STR_READY_ALREAY"),
                    title = getCommonStr("STR_LABBER"),
                    masterId = self.masterId,
                    noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_LADDER"),
                    matchType = "MT_QUALIFYING_NEWER",
                    preUI = self.uiName
                }
                self.uiSelectRole = TopPanelManager:ShowPanel("UISelectRole", nil, nextShowUIParams)
            else
                local team_info = {}

                local grade = resp.grade
                print("1927 - <UIQualifyingNewer>  grade=",grade)

                QualifyingNewerAI.CreateNPC(self.matchType, grade)

                for i = 1, 3 do
                    local name = MainPlayer.Instance.Name
                    local acc_id = MainPlayer.Instance.AccountID
                    if i ~= 1 then
                        name = QualifyingNewerAI.nameList[i-1]
                        acc_id = i
                    end
                    table.insert(team_info, {name=name, acc_id=acc_id})
                end
                local session = resp.session_id
                print("1927 - <UIQualifyingNewer>  session=",session)


                local nextShowUIParams = {
                    teamInfo = team_info,
                    isLadder = true,
                    startLabel=getCommonStr("STR_READY_ALREAY"),
                    title = getCommonStr("STR_LABBER"),
                    masterId = self.masterId,
                    noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_LADDER"),
                    matchType = "MT_QUALIFYING_NEWER",
                    preUI = self.uiName
                }
                self.uiSelectRole = TopPanelManager:ShowPanel("UISelectRole", nil, nextShowUIParams)
                QualifyingNewerAI.StartAIMatch(self.uiSelectRole, session)
            end
        end
    end
end


function GetFightAwardsHandler(self)
    return function(buf)
        print("1927 - <UIQualifyingNewer> GetFightAwardsHandler called")
        local resp, error = protobuf.decode('fogs.proto.msg.GetQualifyingNewerLeagueAwardsResp',buf)
        local result = resp.result
        if result ~= 0 then
            return
        end

        MainPlayer.Instance.QualifyingNewerInfo.league_awards_flag = 0
        self:Refresh()

        local goodsAcquire = getLuaComponent(createUI("GoodsAcquirePopup"))
        for k, v in pairs(resp.awards) do
            print("1927 - <UIQualifyingNewer>  k, v=",k, v)
            print("1927 - <UIQualifyingNewer>  v.id, v.value=",v.id, v.value)
            goodsAcquire:SetGoodsData(v.id, v.value)
        end
    end
end


function OnEnable(self)
    print("1927 - <UIQualifyingNewer> OnEnable is called")
    QualifyingNewer.uiQualifyingNewer = self
end

function OnDisable(self)
    print("1927 - <UIQualifyingNewer> Disable is called")
    QualifyingNewer.uiQualifyingNewer = nil
end

function GetGradeAwardsHandler(self)
    return function(buf)
        print("1927 - <UIQualifyingNewer> GetGradeAwardsHandler called")
        local resp, error = protobuf.decode('fogs.proto.msg.GetQualifyingNewerGradesAwardsResp', buf)
        local result = resp.result
        if result ~= 0 then
            return
        end

        print("1927 - <UIQualifyingNewer>  resp.grade_awards=",resp.grade_awards)
        print("1927 - <UIQualifyingNewer>  resp.grade_awards_flag=",resp.grade_awards_flag)

        MainPlayer.Instance.QualifyingNewerInfo.grade_awards = resp.grade_awards
        MainPlayer.Instance.QualifyingNewerInfo.grade_awards_flag = resp.grade_awards_flag


        local goodsAcquire = getLuaComponent(createUI("GoodsAcquirePopup"))
        for k, v in pairs(resp.awards) do
            print("1927 - <UIQualifyingNewer>  k, v=",k, v)
            print("1927 - <UIQualifylingNewer>  v.id, v.value=",v.id, v.value)
            goodsAcquire:SetGoodsData(v.id, v.value)
        end

        self:Refresh()
    end
end


function MakeOnStart(self)
    return function ()
        if self.qualifyingConfig.upgrade_score ~= 0 and
            self.qualifyingScore >= self.qualifyingConfig.upgrade_score then
            local prompt = createUI("QualifyingUpgradePrompt")
            addOnClick(prompt.transform:FindChild("Window/OK").gameObject, function ()
                self:SelectRole()
                NGUITools.Destroy(prompt)
            end)
            addOnClick(prompt.transform:FindChild("Window/Cancel").gameObject, function ()
                NGUITools.Destroy(prompt)
            end)
            return
        end

        self:SelectRole()
    end
end


function MakeOnSingleMatch(self)
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.qualifying_matching) then return end

        local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = matchType,
        }

        -- Do not send data info.
        -- for _, ID in ipairs(selectedIDs) do
        --     local player = MainPlayer.Instance:GetRole(ID)
        --     local badgebook = MainPlayer.Instance.badgeSystemInfo:GetBadgeBookByBookId(player.m_roleInfo.badge_book_id)
        --     player.m_attrData = MainPlayer.Instance:GetRoleAttrs(player.m_roleInfo, nil, nil, nil, nil, badgebook)
        --     print(self.uiName, ID, player and player.m_name or "NULL")
        --     table.insert(req.fight_power, {id = ID, value = player.m_fightingCapacity})
        --     -- 添加涂鸦信息
        --     table.insert(req.data.roles,{id = ID,badge_book_id = player.m_roleInfo.badge_book_id})
        -- end

        -- local qualifyingReq = {
        --     level = MainPlayer.Instance.Level,
        --     name = MainPlayer.Instance.Name,
        --     score = self.qualifyingScore,
        -- }
        -- req.qualifying_newer = qualifyingReq

        print("1927 - <UIQualifyingNewer> Single StartMarchReq req.acc_id, req.type=",req.acc_id, req.type)

        local data = protobuf.encode("fogs.proto.msg.StartMatchReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, data)
        CommonFunction.ShowWait()
        print(self.uiName, "Send StartMatchReq, Qualifying score:", self.qualifyingScore)

        -- TODO: QAIT
        -- QAIT.UIQualifyingNewer_MatchInfoHandler(self)
    end
end


function MakeOnTeamMatch(self)
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.qualifying_matching) then return end

        QualifyingNewer.JoinLadder()
    end
end


function SelectRole(self)
    self.uiSelectRole = TopPanelManager:ShowPanel("UISelectRole", nil, {
        maxSelection = 3,
        onStart = self:MakeOnRoleSelected(),
        sendChangeRole = true,
        title = getCommonStr("STR_LABBER"),
        startLabel = getCommonStr("STR_START_MATCH"),
        noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_MATCH"),
    })
end

function MakeOnRoleSelected(self)
    return function (selectedIDs)
        TopPanelManager:HideTopPanel()
        self:SendStartMatchReq(selectedIDs)
    end
end

function SendStartMatchReq(self, selectedIDs)
    local req = {
        acc_id = MainPlayer.Instance.AccountID,
        type = matchType,
        game_mode = "GM_QUALIFYING",
        fight_power = {},
        data = {roles = {},},
    }

    for _, ID in ipairs(selectedIDs) do
        local player = MainPlayer.Instance:GetRole(ID)
        local badgebook = MainPlayer.Instance.badgeSystemInfo:GetBadgeBookByBookId(player.m_roleInfo.badge_book_id)
        player.m_attrData = MainPlayer.Instance:GetRoleAttrs(player.m_roleInfo, nil, nil, nil, nil, badgebook)
        print(self.uiName, ID, player and player.m_name or "NULL")
        table.insert(req.fight_power, {id = ID, value = player.m_fightingCapacity})
        -- 添加涂鸦信息
        table.insert(req.data.roles,{id = ID,badge_book_id = player.m_roleInfo.badge_book_id})
    end

    local qualifyingReq = {
        level = MainPlayer.Instance.Level,
        name = MainPlayer.Instance.Name,
        score = self.qualifyingScore,
    }
    req.qualifying_new = qualifyingReq

    local data = protobuf.encode("fogs.proto.msg.StartMatchReq", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, data)
    CommonFunction.ShowWait()
    print(self.uiName, "Send StartMatchReq, Qualifying score:", self.qualifyingScore)
end

function HandleStartMatchResp(self)
    return function (buf, force)

        -- if QAIT then
        --     return
        -- end

        print("1927 - <UIQualifyingNewer> HandlerStartMatchrResp")
        CommonFunction.StopWait()
        if not force and MainPlayer.Instance.inPvpJoining then
            return
        end

        local resp, err = protobuf.decode("fogs.proto.msg.StartMatchResp", buf)
        if resp then
            if resp.result == 0 then
                if force then
                    self:CreatePVPMatch(resp)
                end
                print("1927 - <UIQualifyingNewer>  resp.type=",resp.type)
                if resp.type == matchType then
                    self.tip = getLuaComponent(createUI("PvpTipNew"))
                    local pos = self.tip.transform.localPosition
                    pos.z = -500
                    self.tip.transform.localPosition = pos
                    UIManager.Instance:BringPanelForward(self.tip.gameObject)
                    self.tip.averTime = resp.aver_time
                    self.tip.matchType = matchType
                    self.tip.onCancel = self:OnCancelMatch()
                    self.isMatching = true
                    QualifyingNewer.lastAverTime = resp.aver_time
                end
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            end
        else
            error(self.uiName, "HandleStartMatchResp: " .. err)
        end
    end
end

function OnCancelMatch(self)
    return function ()
        if self.tip == nil then
            return
        end
        self.tip:Close()
        self.tip = nil
    end
end

function HandleNotifyGameStart(self)
    return function (buf)
        self.gameStartRespBuf = buf
        local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)
        if resp then
            if resp.type == matchType then
                if self.tip then
                    self.tip:Close()
                    self.tip = nil
                end
                self.gameStartResp = resp
                self.popupCountDown = CommonFunction.ShowPopupMsg(getCommonStr('MATCH_JOIN_GAME') .. 10, nil,
                    LuaHelper.VoidDelegate(self:OnConfirmStartMatch()), nil,
                    getCommonStr("BUTTON_CONFIRM"), "").table
                Scheduler.Instance:AddTimer(1, true, self.actionOnTimer)
                self.isMatching = false
                self.isWaitingConfirm = true
            end
        else
            error(self.uiName, "HandleNotifyGameStart: " .. err)
        end
    end
end

function OnTimer(self)
    return function ()
        self.confirmWaitTime = self.confirmWaitTime + 1
        self.popupCountDown:SetMessage(getCommonStr('MATCH_JOIN_GAME') .. tostring(10 - self.confirmWaitTime))
        if self.confirmWaitTime == 10 then
            if self.popupCountDown then
                NGUITools.Destroy(self.popupCountDown.gameObject)
                self.popupCountDown = nil
            end
            self:OnConfirmStartMatch()()
        end
    end
end

function OnDisconnected(self)
    return function ()
        if self.isMatching then
            self.isMatching = false
            if self.tip then
                self.tip:Close()
                self.tip = nil
            end
        elseif self.isWaitingConfirm then
            self.isWaitingConfirm = false
            self.confirmWaitTime = 0
            Scheduler.Instance:RemoveTimer(self.actionOnTimer)
            if self.popupCountDown then
                NGUITools.Destroy(self.popupCountDown.gameObject)
                self.popupCountDown = nil
            end
        end
    end
end

function OnReconnected(self)
    return function ()
        self:Refresh()
    end
end

function OnConfirmStartMatch(self)
    return function ()
        Scheduler.Instance:RemoveTimer(self.actionOnTimer)
        self.confirmWaitTime = 0
        self.popupCountDown = nil

        if self.uiSelectRole then
            self.uiSelectRole:VisibleModel(false)
        end

        local resp = self.gameStartResp
        print(self.uiName, "OnConfirmStartMatch", "race_times:", resp.qualifying_new.race_times, "ai_flag:", resp.qualifying_new.ai_flag)

        if resp.qualifying_new.ai_flag == 2 then -- gamemode
            self:CreateGameModeMatch(resp)
        elseif resp.qualifying_new.ai_flag == 1 then	-- AI
            self:CreateAIMatch(resp)
        else	-- PVP
            self:CreatePVPMatch(resp)
        end
    end
end

function CreateGameModeMatch(self, resp)
    local squadInfo = MainPlayer.Instance.SquadInfo
    local teammates = UintList.New()
    for i = 0, squadInfo.Count - 1 do
        teammates:Add(squadInfo:get_Item(i).role_id)
    end
    local gameModeID
    if resp.qualifying_new.race_times + 1 <= table.getn(self.gameModeIDs) then
        gameModeID = self.gameModeIDs[resp.qualifying_new.race_times + 1]
    else
        local index = math.random(1, table.getn(self.gameModeIDOthers))
        gameModeID = self.gameModeIDOthers[index]
    end
    print(self.uiName, "CreateGameModeMatch, GameMode ID:", gameModeID, resp.qualifying_new.session_id, matchType, leagueType)
    npcRivalName = UICreateTeam.GenerateName()
    GameSystem.Instance.mClient:CreateNewMatch(gameModeID, resp.qualifying_new.session_id, false, leagueType, teammates, nil)
end

function CreateAIMatch(self, resp)
    self:StartMatch(resp)
end

function CreatePVPMatch(self, resp)
    self:StartMatch(resp)
end

function StartMatch(self, notifyGameStart )
    -----TeamName
    local myName,rivalName
    local myList = RoleInfoList.New()
    local rivalList = RoleInfoList.New()
    for _,v in pairs(notifyGameStart.data) do
        myName = v.name
        for _,role in pairs(v.roles) do
            local gameRoleInfo = RoleInfo.New()
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
            print("pvp 1 + 2 plus myteam id :",role.id)
            myList:Add( gameRoleInfo)
        end
    end
    for _,v in pairs(notifyGameStart.rival_data) do
        rivalName = v.name
        for _,role in pairs(v.roles) do
            local gameRoleInfo = RoleInfo.New()
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
            print("pvp 1 + 2 plus rivalteam id :",role.id)
            rivalList:Add( gameRoleInfo)
        end
    end
    print("myname:",myName,"-----rivalName:",rivalName)

    local qualifying  = notifyGameStart.qualifying_new
    local session_id	= qualifying.session_id
    local matchType = qualifying.ai_flag == 1 and GameMatch.Type.eAsynPVP3On3 or GameMatch.Type.ePVP_1PLUS

    local matchConfig = GameMatch.Config.New()

    print(self.uiName, "LeagueType:", leagueType)
    matchConfig.leagueType	= leagueType
    matchConfig.type		= matchType
    matchConfig.sceneId		= qualifying.scene_id
    matchConfig.MatchTime	= GameSystem.Instance.CommonConfig:GetUInt("gChallengeGameTime")
    matchConfig.session_id	= session_id
    matchConfig.ip			= qualifying.game_ip
    matchConfig.port		= qualifying.game_port

    print("match session id "..session_id)
    print("match server ip "..qualifying.game_ip)
    print("match server port "..qualifying.game_port)

    GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
    if matchType == GameMatch.Type.eAsynPVP3On3 then
        GameSystem.Instance.mClient.mCurMatch:SetNotifyGameStart(self.gameStartRespBuf)
    end

    self.uiLoading = createUI("UIChallengeLoading_1"):GetComponent("UIChallengeLoading")
    if self.uiLoading then
        local pos = self.uiLoading.transform.localPosition
        pos.z = -400
        self.uiLoading.transform.localPosition = pos
        self.uiLoading.scene_name = qualifying.scene_id
        self.uiLoading.myName = myName
        self.uiLoading.rivalName = rivalName
        self.uiLoading.my_role_list = myList
        self.uiLoading.rival_list = rivalList
        self.uiLoading.pvp = true
        self.uiLoading:Refresh(false)
        NGUITools.BringForward(self.uiLoading.gameObject)
    end
end

function MakeOnRule(self)
    return function ()
        local rulePopup = getLuaComponent(createUI("MatchRulePopup"))
        rulePopup.leagueType = self.leagueType
        UIManager.Instance:BringPanelForward(rulePopup.gameObject)
    end
end

function MakeOnStore(self)
    return function ()
        TopPanelManager:ShowPanel("UIFashion", nil, {reputationStore = true, titleStr = "STR_REPUTATION_STORE"})
    end
end
--[[
function MakeOnAwardPress(self)
    return function (go, isPress)
        -- 未达到领取 grade_awards_flag = 0, 达到了为1
        if self.qualifying.grade_awards_flag == 1 then
            print("1927 - <UIQualifyingNewer> Press Award return due to flag is 1")
            return
        end

        if isPress then
            local awardId = self.qualifyingConfig.award_id
            print("1927 - <UIQualifyingNewer> 0000000000 awardId=",awardId)
            local grade = GameSystem.Instance.qualifyingNewerConfig:GetNextGrade(self.qualifyingConfig.score)

            if awardId == 0 then
                print("1927 - <UIQualifyingNewer> 1--- grade.award_id=",grade.award_id)
                while grade.award_id == 0 do
                    grade = GameSystem.Instance.qualifyingNewerConfig:GetNextGrade(grade.score)
                end
            end
            print("1927 - <UIQualifyingNewer> 2--- grade.award_id=",grade.award_id)
            self:ShowAwardTip(grade.award_id, go.transform)
        else
            self:HideAwardTip()
        end
    end
end
--]]
function MakeOnAwardClick(self)
    return function(go)
        if not FunctionSwitchData.CheckSwith(FSID.qualifying_grade) then return end

        if self.qualifying.grade_awards_flag ~= 1 then
            print("1927 - <UIQualifyingNewer> click Award return due to flag is not 1")
            local awardId = self.qualifyingConfig.award_id
            print("1927 - <UIQualifyingNewer> 0000000000 awardId=",awardId)
            local grade = GameSystem.Instance.qualifyingNewerConfig:GetNextGrade(self.qualifyingConfig.score)

            if awardId == 0 then
                print("1927 - <UIQualifyingNewer> 1--- grade.award_id=",grade.award_id)
                while grade.award_id == 0 do
                    grade = GameSystem.Instance.qualifyingNewerConfig:GetNextGrade(grade.score)
                end
            end
            print("1927 - <UIQualifyingNewer> 2--- grade.award_id=",grade.award_id)
            self:ShowAwardTip(grade.award_id, go.transform)
            return
        end
        print("1927 - <UIQualifyingNewer> MakeOnAwardClick is called")

        local operation = {}
        local req = protobuf.encode("fogs.proto.msg.GetQualifyingNewerLeagueAwardsReq", operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.GetQualifyingNewerGradesAwardsReqID,req)
    end
end

function MakeOnFightAwardClick(self)
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.qualifying_score) then return end

        self:ShowFightAwardTip(self.qualifyingConfig.award_id, go.transform)
    end
end

function MakeOnFightAwardPress(self)
    return function (go, isPress)
        if isPress then
            self:ShowFightAwardTip(self.qualifyingConfig.award_id, go.transform)
        else
            self:HideFightAwardTip()
        end
    end
end

function ShowAwardTip(self, ID, transform)
    --if self.awardTip then return end

    local popup = getLuaComponent(createUI("TipPopupMulti"))


    print("1927 - <UIQualifyingNewer> ShowAwardTip ID=",ID)
    local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(ID)

    local goods = {}
    local rewards = CommonFunction.GetListByType(3,AwarConfig.awards)
    local idx  = 1
    for k,v in pairs(rewards or {}) do
        print("1927 - <UIQualifyingNewer> paris k, v=",k, v)
        local cell = {id,num,}
        cell.id = v.award_id
        cell.num = v.award_value
        table.insert(goods,idx,cell)
        idx = idx +1
    end
    -- for i=0,#AwarConfig.awards-1 do
    --     local goodsID = AwarConfig.awards:get_Item(i).award_id
    --     local goodsNum = AwarConfig.awards:get_Item(i).award_value
    --     goods.goodsID = goodsNum
    -- end
    popup:SetData(goods)
    UIManager.Instance:BringPanelForward(popup.gameObject)


    --坐标设置
    local x = 0
    local y = 0
    local position = transform.position
    if position.x <= 0 and position.y >= 0 then
        x = 0.75
        y = -0.5
    elseif position.x >= 0 and position.y >= 0 then
        x = -0.75
        y = -0.5
    elseif position.x >= 0 and position.y <= 0 then
        x = -0.75
        y = 0.5
    elseif position.x <= 0 and position.y <= 0 then
        x = 0.75
        y = 0.5
    end
    local pos = popup.transform.position
    pos.x = position.x + x
    pos.y = position.y + y
    pos.z = 0
    popup.transform.position = pos

    --self.awardTip = popup
end
--[[
function HideAwardTip(self)
    if self.awardTip then
        NGUITools.Destroy(self.awardTip.gameObject)
        self.awardTip = nil
    end
end
--]]
function ShowFightAwardTip(self, ID, transform)
    --if self.fightAwardTip then
    --    return
    --end

    local popup = getLuaComponent(createUI("TipPopup2"))
    popup.title = getCommonStr("LADDER_AWARD_TIP_TITLE")
    popup.content = getCommonStr("LADDER_AWARD_TIP_CONTENT")

    UIManager.Instance:BringPanelForward(popup.gameObject)
    --坐标设置
    local x = 0
    local y = 0
    local position = transform.position
    if position.x <= 0 and position.y >= 0 then
        x = 0.75
        y = -0.5
    elseif position.x >= 0 and position.y >= 0 then
        x = -0.75
        y = -0.5
    elseif position.x >= 0 and position.y <= 0 then
        x = -0.75
        y = 0.5
    elseif position.x <= 0 and position.y <= 0 then
        x = 0.75
        y = 0.5
    end
    local pos = popup.transform.position
    pos.x = position.x + x
    pos.y = position.y + y
    pos.z = 0
    popup.transform.position = pos

    --self.fightAwardTip = popup
end
--[[
function HideFightAwardTip(self)
    if self.fightAwardTip then
        NGUITools.Destroy(self.fightAwardTip.gameObject)
        self.fightAwardTip = nil
    end
end
--]]


function MakeOnBack(self)
    return function ()
        TopPanelManager:ShowPanel("UIHall")
    end
end

return UIQualifyingNewer

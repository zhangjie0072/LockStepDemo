require "Custom/Team/UICreateTeam"

module("UIQualifyingNew", package.seeall)

uiName = "UIQualifyingNew"

--parameters
showIncStarAnim = false

--variables
qualifyingConfig = {}

matchType = "MT_QUALIFYING_NEW"
leagueType = GameMatch.LeagueType.eQualifyingNew

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
btnAward = {}
qualifying = nil
onPressGo = nil
isPressed = false
states = {}

lastGrade = nil
lastScore = nil

function Awake(self)
    self.btnRule = getChildGameObject(self.transform, "Top/Rule")
    -- self.btnStore = getChildGameObject(self.transform, "Top/Store")


    -- self.labelTotalRound = getComponentInChild(self.transform, "Left/TotalRound/Num", "UILabel")
    -- self.labelWins = getComponentInChild(self.transform, "Left/Wins/Num", "UILabel")
    -- self.labelWinRate = getComponentInChild(self.transform, "Left/WinRate/Num", "UILabel")
    -- self.labelWinningStreak = getComponentInChild(self.transform, "Left/WinningStreak/Num", "UILabel")
    -- self.labelHighest = getComponentInChild(self.transform, "Left/Highest/Label", "UILabel")
    -- self.labelStartTime = getComponentInChild(self.transform, "Right/StartTime/Label", "UILabel")
    self.labelEndTime = getComponentInChild(self.transform, "Text", "UILabel")
    --初始化箱子和篮球对象
    for i=1,5 do
        local btn = getChildGameObject(self.transform, "Middle/Box"..i.."/Box")
        if btn then
            btn.name = i
            -- btn:set_tag(0)    --使用tag来判断是否按下 按下为1 否则为0
            local name = getChildGameObject(self.transform, "Middle/Box"..i.."/Name"):GetComponent('UILabel')
            local anim = btn.transform:GetComponent('Animator')
            local btnCell = {btn,name,animator,effect,balls={}}
            --箱子
            btnCell.btn = btn
            --名称
            btnCell.name = name

            --可以领取效果
            btnCell.effect = getChildGameObject(self.transform, "Middle/Box"..i..'/UIEffect1')
            NGUITools.SetActive(btnCell.effect,false)

            -- 动画初始点
            btnCell.startPos = getChildGameObject(self.transform, "Middle/Box"..i.."/startPos")

            --动画
            btnCell.animator = anim
            btnCell.animator.enabled = false
            local balls = {}
            for j=1,10 do
                local ball = getChildGameObject(self.transform, "Middle/Box"..i..'/'..j)
                if ball then
                    local ballCell = {
                        On = nil,
                        Off = nil
                    }
                    --每个球的开关状态
                    ballCell.On =  getChildGameObject(self.transform, "Middle/Box"..i..'/'..j..'/On')
                    ballCell.Off =  getChildGameObject(self.transform, "Middle/Box"..i..'/'..j..'/Off')
                    -- NGUITools.SetActive(ballCell.On,false)
                    -- NGUITools.SetActive(ballCell.Off,true)
                    table.insert(balls,j,ballCell)
                else
                    break
                end
            end
            --下面的球
            btnCell.balls = balls
            table.insert(self.btnAward,i,btnCell)
            -- print('btn inx '..i)
        else
            break
        end
    end

    self.btnStart = getChildGameObject(self.transform, "Middle/ButtonOK")


    -- self.spriteAward = getComponentInChild(self.transform, "Right/Award", "UISprite")
    -- self.spriteIcon = getComponentInChild(self.transform, "Middle/Icon", "UISprite")
    -- self.labelGrade = getComponentInChild(self.transform, "Middle/StarLeague", "UILabel")
    -- self.labelRanking = getComponentInChild(self.transform, "Middle/StarLeague/Num", "UILabel")
    -- self.labelScore = getComponentInChild(self.transform, "Middle/Points/Num", "UILabel")
    -- self.gridStar = getComponentInChild(self.transform, "Middle/StarGrid", "UIGrid")
    -- self.labelStart = getComponentInChild(self.transform, "Middle/Start/Label", "UILabel")
    -- self.tmWinningStreak = self.transform:FindChild("Middle/WinningStreak")
    -- --self.goEffectStar = getChildGameObject(self.transform, "Middle/E_Star")
    -- self.goEffectWinningStreak = getChildGameObject(self.transform, "Middle/E_Star3")
    -- self.goIconShine = getChildGameObject(self.transform, "Middle/Icon")
    self.btnBack = getLuaComponent(createUI("ButtonBack", self.transform:FindChild("Top/ButtonBack")))
    self.btnBack.delay = 0.47
    -- Object.DontDestroyOnLoad(self.btnBack.gameObject)
    self.streakLights = {}
    -- for i = 1, self.tmWinningStreak.transform.childCount do
    --     table.insert(self.streakLights, self.tmWinningStreak.transform:GetChild(i - 1))
    -- end

    addOnClick(self.btnRule, self:MakeOnRule())
    -- addOnClick(self.btnStore, self:MakeOnStore())
    addOnClick(self.btnStart, self:MakeOnStart())
    self.btnBack.onClick = self:MakeOnBack()
    for i=1,5 do
        -- print('i = '..i)
        --UIEventListener.Get(self.btnAward[i].btn).onPress = LuaHelper.BoolDelegate(self:MakeOnAwardPress())
        UIEventListener.Get(self.btnAward[i].btn).onClick = LuaHelper.VoidDelegate(self:MakeOnAwardClick())
    end

    self.onAnimResp = self:MakeOnAnimResp()
    -- self.qualifying.win_times = math.random(0,5)
    self.qualifying = MainPlayer.Instance.qualifying_new
    -- self.animator = self.transform:GetComponent("Animator")
    -- local animationResp = self.transform:GetComponent("AnimationResp")
    -- animationResp:AddResp(self.onAnimResp, self.gameObject)

    -- animationResp = self.goEffectWinningStreak:GetComponent("AnimationResp")
    -- animationResp:AddResp(self.onAnimResp, self.gameObject)

    -- animationResp = self.goIconShine:GetComponent("AnimationResp")
    -- animationResp:AddResp(self.onAnimResp, self.gameObject)
    -- NGUITools.SetActive(self.goIconShine, true)
end

function Start(self)

    local season = GameSystem.Instance.qualifyingNewConfig:GetSeason(self.qualifying.season)
    local list = Split(season.start_time," ")
    local _start = list[1]
    list = Split(list[1], "-")
    _start = string.format(getCommonStr("STR_MONTH_DAY"),
                                        tonumber(list[2]),
                                        tonumber(list[3]))
    list = Split(season.end_time," ")
    list = Split(list[1], "-")
    local _end = string.format(getCommonStr("STR_MONTH_DAY"),
                                        tonumber(list[2]),
                                        tonumber(list[3]))
    self.labelEndTime.text = getCommonStr('STR_QUALIFYINGNEW_END_TIME').._start..'-'.._end

    -- self.labelEndTime.text =  getCommonStr('STR_QUALIFYING_END_TIME')..string.sub(season.start_time,6,10)..' 至 '..string.sub(season.end_time,6,10)
    -- if not UIQualifyingNew.gameModeIDs then
    --     UIQualifyingNew.gameModeIDs = {}
    --     local i = 1
    --     for i = 1, 10000000 do

    --         local gameModeID = GameSystem.Instance.CommonConfig:GetUInt("gRegular1V1GameModeID" .. i)
    --         if gameModeID ~= 0 then
    --             table.insert(UIQualifyingNew.gameModeIDs, gameModeID)
    --         else
    --             break
    --         end
    --     end
    -- end

    -- if not UIQualifyingNew.gameModeIDOthers then
    --     UIQualifyingNew.gameModeIDOthers = {}
    --     local strGameModeIDOthers = GameSystem.Instance.CommonConfig:GetString("gRegular1V1GameModeIDOthers")
    --     local strIDs = string.split(strGameModeIDOthers, "&")
    --     for _, strID in ipairs(strIDs) do
    --         print("GameModeID:", strID)
    --         table.insert(UIQualifyingNew.gameModeIDOthers, tonumber(strID))
    --     end
    -- end

    LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, self:HandleStartMatchResp(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyGameStartID, self:HandleNotifyGameStart(), self.uiName)
    LuaHelper.RegisterPlatMsgHandler(MsgID.RefreshQualifyingNewInfoID, self:HandleRefreshQualiyfingNewInfo(), self.uiName)
    self.actionOnTimer = LuaHelper.Action(self:OnTimer())
    self.actionOnDisconnected = LuaHelper.Action(self:OnDisconnected())
    self.actionOnReconnected = LuaHelper.Action(self:OnReconnected())
    PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected + self.actionOnDisconnected
    PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionOnReconnected


    --配置读取
    for i=1,5 do
        local config = GameSystem.Instance.qualifyingNewConfig:getGradeConfigByGrade(i)
        if config then
            self.qualifyingConfig[i] = config

            self.btnAward[i].name.text =    config.title
            -- print('qualifying config info section'..config.section..',award_id '..config.award_id..',icon '..config.icon..',icon small '..config.icon_small..',score '..config.score)
        else
            break
        end
    end

    if not (#self.qualifyingConfig>0) then
        error(self.uiName, "Can not read qualifying config.")
    end

    -- 初始
    if lastScore == nil then
        lastScore = self.qualifying.score
    end
end

function OnDestroy(self)
    for k,v in pairs(self.btnAward or {}) do
        Object.Destroy(v.animator )
    end
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.StartMatchRespID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.RefreshQualifyingNewInfoID, self.uiName)
    PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected - self.actionOnDisconnected
    PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionOnReconnected
end

function UIQualifyingNew:IsShowGradeEffect(last_score, cur_score, grade)
    local last_grade = UIQualifyingNew:GetGrade(last_score)
    local cur_grade = UIQualifyingNew:GetGrade(cur_score)

    -- print ("============")
    -- print (last_score)
    -- print (cur_score)
    -- print (last_grade)
    -- print (cur_grade)

    if grade < cur_grade then return false end

    -- 达到播放条件
    if last_score < cur_score and (self.qualifyingConfig[cur_grade + 1].score - 10 == cur_score) then
        lastScore = self.qualifying.score
        return true
    end

    return false
end

function UIQualifyingNew:GetGrade(score)
    if not score then return nil end

    local grade = nil

    for i=1,#self.qualifyingConfig do
        -- print ("------------")
        -- print (self.qualifyingConfig[i].score)
        -- print (score)

        if score >= self.qualifyingConfig[i].score then
            grade = i
            -- break
        end
    end

    return grade
end

function Refresh(self,Qstates)
    self.qualifying = MainPlayer.Instance.qualifying_new

    --刷新显示
    if not Qstates then
        self.states =  CommonFunction.GetListByType(2,self.qualifying.grade_awards)  -- math.random(0,1) --
    else
        self.states = Qstates
        local list = UintList.New()

        -- local idx = 1
        for k,v in pairs(self.states or {}) do
            list:Add(v)
        end
        MainPlayer.Instance:ResetQualifyingNewGrades(list)
    end
    -- print('states '..#self.states)
    -- local winning = {
    -- 0,1,3,6,10,
    -- }
    for i=1,5 do
         if self.states[i] and  self.btnAward[i] and self.qualifyingConfig[i] then
            --根据胜场显示篮球状态
            --显示规则为  当前段位点亮的篮球数量加上所有前面段位点亮的篮球数量为总的胜利场数
            local pre_win_times = 0
            for k=1,i do
                pre_win_times = pre_win_times+k-1
            end
            -- print('idx '..i..',total '..self.qualifying.win_times..',pre '..pre_win_times)
            if self.qualifying then
                for j=1,i+1 do
                    if self.btnAward[i].balls[j] then
                        local on = ( self.qualifying.win_times - pre_win_times >= j )
                        NGUITools.SetActive(self.btnAward[i].balls[j].On.gameObject,on)
                        NGUITools.SetActive(self.btnAward[i].balls[j].Off.gameObject,not on)
                    end

                end
            end
            --根据当前胜利场数来设置显示图标
            local btnUISp = self.btnAward[i].btn.transform:GetComponent('UISprite')
            self.btnAward[i].animator.enabled = false
            if not (self.states[i] == 2 )then
                btnUISp.spriteName = self.qualifyingConfig[i].icon--不可领取
                if self.states[i] == 0 then
                    self.btnAward[i].animator.enabled = false
                    self.btnAward[i].btn.transform.localRotation = Vector3.New(0,0,0)
                    NGUITools.SetActive(self.btnAward[i].effect,false)
                else
                    self.btnAward[i].animator.enabled = true
                    NGUITools.SetActive(self.btnAward[i].effect,true)

                    -- 段位升级特效设置
                    self:SetGradeEffect(self.btnAward[i], self:IsShowGradeEffect(lastScore, self.qualifying.score, i))
                end
            else
                btnUISp.spriteName = self.qualifyingConfig[i].award_icon--已经领取
                NGUITools.SetActive(self.btnAward[i].effect,false)
                self.btnAward[i].btn.transform.localRotation = Vector3.New(0,0,0)
            end
            -- print('qualifying info '..self.states[i]..',icon '..btnUISp.spriteName)

        end
    end

    -- self.labelTotalRound.text = tostring(self.qualifying.race_times)
    -- self.labelWins.text = tostring(self.qualifying.win_times)
    -- if self.qualifying.race_times == 0 then
    --     self.labelWinRate.text = "0%"
    -- else
    --     self.labelWinRate.text = math.floor(self.qualifying.win_times / self.qualifying.race_times * 100) .. "%"
    -- end
    -- self.labelWinningStreak.text = tostring(self.qualifying.max_winning_streak)
    -- self.labelHighest.text = self.maxQualifyingConfig.title
    -- self.spriteIcon.spriteName = self.qualifyingConfig.icon
    -- self.labelGrade.text = self.qualifyingConfig.title
    -- if self.qualifying.ranking == 0 then
    --     self.labelRanking.text = getCommonStr("NO_RANKING")
    -- else
    --     self.labelRanking.text = getCommonStr("RANK_SINGLESRCTION"):format(self.qualifying.ranking)
    -- end
    -- self.labelScore.text = tostring(self.qualifying.score)
    -- local list = Split(self.qualifyingSeason.start_time," ")
    -- self.labelStartTime.text = list[1]
    -- list = Split(self.qualifyingSeason.end_time," ")
    -- self.labelEndTime.text = list[1]
    -- self.spriteAward.spriteName = self.qualifyingConfig.award_icon
    -- if self.qualifyingConfig.upgrade_score ~= 0 and
    --     self.qualifying.score >= self.qualifyingConfig.upgrade_score then
    --     self.labelStart.text = getCommonStr("STR_QUALIFYING_UPGRADE")
    -- else
    --     self.labelStart.text = getCommonStr("STR_JOIN_GAME")
    -- end

    -- print(self.uiName, "Winning streak:", self.qualifying.winning_streak)
    -- local lightNum = self.qualifying.winning_streak % #self.streakLights
    -- local winningStreakFull = (lightNum == 0 and self.qualifying.winning_streak > 0)
    -- if self.animator.enabled then
    --     if self.showIncStarAnim then
    --         if winningStreakFull then
    --             self.showWinningStreakStarAnim = true
    --         end
    --         if self.qualifyingConfig.star == 1 then		-- show prev grade
    --             self.prevGrade = GameSystem.Instance.qualifyingNewConfig:GetPrevGrade(self.qualifying.score)
    --             if self.prevGrade then
    --                 self.showingPrevGrade = true
    --                 self.spriteIcon.spriteName = self.prevGrade.icon
    --                 if self.showWinningStreakStarAnim then
    --                     self:DisplayStar(self.prevGrade.star - 1, self.prevGrade.section)
    --                     self:DisplayWinningStreak(#self.streakLights)
    --                 else
    --                     self:DisplayStar(self.prevGrade.star, self.prevGrade.section)
    --                     self:DisplayWinningStreak(lightNum)
    --                 end
    --                 return
    --             end
    --         end
    --     end
    -- else
    --     self.showIncStarAnim = false
    --     self.showWinningStreakStarAnim = false
    -- end

    -- if self.showWinningStreakStarAnim then
    --     self:DisplayStar(self.qualifyingConfig.star - 2, self.qualifyingConfig.section)
    --     self:DisplayWinningStreak(#self.streakLights)
    -- elseif self.showIncStarAnim then
    --     self:DisplayStar(self.qualifyingConfig.star - 1, self.qualifyingConfig.section)
    --     self:DisplayWinningStreak(lightNum)
    -- else
    --     self:DisplayStar(self.qualifyingConfig.star, self.qualifyingConfig.section)
    --     self:DisplayWinningStreak(lightNum)
    -- end
end

function UIQualifyingNew:SetGradeEffect(box,tag)

    -- 不播放
    if not tag then
        box.effect.gameObject:GetComponent("TweenPosition").enabled = false
        box.effect.gameObject:GetComponents(TweenScale:GetClassType())[0].enabled = false
        box.effect.gameObject:GetComponents(TweenScale:GetClassType())[1].enabled = false
    else
        box.effect.transform.localPosition = Vector3.New(box.startPos.transform.localPosition.x, box.startPos.transform.localPosition.y, box.startPos.transform.localPosition.z)
    end
end

function DisplayWinningStreak(self, winningStreak)
    for i = 1, #self.streakLights do
        local tm = self.streakLights[i]
        local light = (i <= winningStreak)
        NGUITools.SetActive(tm:FindChild("On").gameObject, light)
        NGUITools.SetActive(tm:FindChild("Off").gameObject, not light)
    end
end

function DisplayStar(self, lightStarNum, section)
    local maxStarNum = GameSystem.Instance.qualifyingNewConfig:GetMaxStarNum(section)
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

function HandleRefreshQualiyfingNewInfo(self)
    return function ()
        print(self.uiName, "HandleRefreshQualiyfingNewInfo")
        self:Refresh()
    end
end

function MakeOnStart(self)
    return function ()
        -- if self.qualifyingConfig.upgrade_score ~= 0 and
        --     self.qualifying.score >= self.qualifyingConfig.upgrade_score then
        --     local prompt = createUI("QualifyingUpgradePrompt")
        --     addOnClick(prompt.transform:FindChild("Window/OK").gameObject, function ()
        --         self:SelectRole()
        --         NGUITools.Destroy(prompt)
        --     end)
        --     addOnClick(prompt.transform:FindChild("Window/Cancel").gameObject, function ()
        --         NGUITools.Destroy(prompt)
        --     end)
        --     return
        -- end
        -- local prompt = createUI("QualifyingUpgradePrompt")
        -- addOnClick(prompt.transform:FindChild("Window/OK").gameObject, function ()
        --     self:SelectRole()
        --     NGUITools.Destroy(prompt)
        -- end)
        -- addOnClick(prompt.transform:FindChild("Window/Cancel").gameObject, function ()
        --     NGUITools.Destroy(prompt)
        -- end)
        if not FunctionSwitchData.CheckSwith(FSID.tour_matching) then return end

        self:SelectRole()
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
        score = self.qualifying.score,
    }
    req.qualifying_new = qualifyingReq

    local data = protobuf.encode("fogs.proto.msg.StartMatchReq", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, data)
    CommonFunction.ShowWait()
    -- print(self.uiName, "Send StartMatchReq, Qualifying score:", self.qualifying.score)
end

function HandleStartMatchResp(self)
    return function (buf, force)
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

    local win_times = self.qualifying.win_times
    -- local sections = {1,3,6,10,15}

    local r = 0
    if win_times <= 1 then
            r = 1
        elseif win_times <= 3 then
            r = 2
        elseif win_times <= 6 then
            r = 3
        elseif win_times <= 10 then
            r = 4
        else
            r = 5
        end
    local config = GameSystem.Instance.qualifyingNewConfig:getGradeConfigByGrade(r)
    if config then
        gameModeID = config.opponentAI
        -- print('qualifying config info section'..config.section..',award_id '..config.award_id..',icon '..config.icon..',icon small '..config.icon_small..',score '..config.score)
    else
        gameModeID = 0
    end
    -- teamGameMode = GameSystem.Instance.GameModeConfig:GetGameMode(modes[r])


    -- if resp.qualifying_new.race_times + 1 <= table.getn(self.gameModeIDs) then
    --     gameModeID = self.gameModeIDs[resp.qualifying_new.race_times + 1]
    -- else
    --     local index = math.random(1, table.getn(self.gameModeIDOthers))
    --     gameModeID = self.gameModeIDOthers[index]
    -- end
    print(self.uiName, "CreateGameModeMatch, GameMode ID:", gameModeID, resp.qualifying_new.session_id, matchType, leagueType)
    npcRivalName = GameSystem.Instance.AIConfig:GetRandAIName()--UICreateTeam.GenerateName()
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

function MakeOnAwardClick(self)
    return function (go)
       --todo 这里添加领奖内容
       if not FunctionSwitchData.CheckSwith(FSID.tour_box) then return end

       print('click reward '..tonumber(go.name)..',state '..self.states[tonumber(go.name)])
       if self.states[tonumber(go.name)] and self.states[tonumber(go.name)] == 1 then
            local req = {
                grade = tonumber(go.name)
            }
            CommonFunction.ShowWait()
            local msg = protobuf.encode("fogs.proto.msg.GetTourNewGradesAwardsReq", req)
            LuaHelper.SendPlatMsgFromLua(MsgID.GetTourNewGradesAwardsReqID, msg)
            TourNewData.TourNewGradesAwardsCallBack = self:GetTourNewGradesAwardsResp()
         print('send reward '..tonumber(go.name)..',state '..self.states[tonumber(go.name)]..',id '..MsgID.GetTourNewGradesAwardsReqID)
       else
            if not ( self.qualifyingConfig[tonumber(go.name)].award_id >0 ) then
                print('no reward info for this cell')
                return
            end
            self.onPressGo = go
            self:ShowAwardTip()()
       end
    end
end
function GetTourNewGradesAwardsResp( self )
    -- body
    return function  (resp)
        -- body
        TourNewData.TourNewGradesAwardsCallBack  = nil
        if resp.result ~=0 then
            --error
            --no reward info
            print('resp result '..resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
            return
        end
        print('reward info '..#resp.grade_awards..',-'..resp.result..',c '..#resp.awards)
        local getGoods
        if #resp.awards >0 then
            getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
            getGoods.onClose = function ( ... )
                -- self.banTwice = false
            end
        end
        for k,v in pairs(resp.awards or {}) do
            getGoods:SetGoodsData(v.id,v.value)
        end

        -- self.states = resp.grade_awards
        self:Refresh(resp.grade_awards)
    end
end
function AwardCallBack(self)
    -- body
    return function (resp)
        -- body
        --reset awarded box state
        self:Refresh()
    end
end
--[[
function EnableClick( self )
    return function ()
        UIEventListener.Get(self.onPressGo).onClick = LuaHelper.VoidDelegate(self:MakeOnAwardClick())
        print('pressed '..self.onPressGo.name)
        self.onPressGo = nil
    end
end
function MakeOnAwardPress(self)
    return function (go, isPress)
        print('reward info MakeOnAwardPress'..tonumber(go.name))
        if isPress then

            if not ( self.qualifyingConfig[tonumber(go.name)].award_id >0 ) then
                print('no reward info for this cell')
                return
            end
            print('reward info for this cell '..go.name)
            -- go:set_tag(1)
            self.onPressGo = go
            self.isPressed = true
            Scheduler.Instance:AddTimer(0.5, false, self:ShowAwardTip())
        else
            -- go:set_tag(0)
            self.isPressed  = false
            if self.awardTip ~= nil then
                NGUITools.Destroy(self.awardTip.gameObject)
                self.awardTip = nil
                Scheduler.Instance:AddTimer(0.01, false, self:EnableClick())
            end
        end
    end
end
]]
--根据不同按钮的名字来查找配置的奖励内容
function ShowAwardTip(self)
    return function ( )
        -- body
        --if self.awardTip then return end
        --if not  self.isPressed then return end
        local transform = self.onPressGo.transform

        print('show award tips')
        --UIEventListener.Get(transform.gameObject).onClick = nil

        local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(self.qualifyingConfig[tonumber(transform.gameObject.name)].award_id)
        if not AwarConfig then return end
        local popup = getLuaComponent(createUI("TipPopupMulti"))
        local goods = {}
        local rewards = CommonFunction.GetListByType(3,AwarConfig.awards)
        local idx  = 1
        for k,v in pairs(rewards or {}) do
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
        UIManager.Instance:BringPanelForward(popup.gameObject)
    end

end

-- function HideAwardTip(self)
--     if self.awardTip then
--         NGUITools.Destroy(self.awardTip.gameObject)
--         self.awardTip = nil
--     end
-- end

function MakeOnBack(self)
    return function ()
        TopPanelManager:ShowPanel("UIHall")
        -- TopPanelManager:HideTopPanel()
    end
end

return UIQualifyingNew

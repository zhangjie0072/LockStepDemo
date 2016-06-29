------------------------------------------------------------------------
-- class name    : UIQLadder
-- create time   : Thu Mar  3 11:02:43 2016
------------------------------------------------------------------------

UIQLadder =  {
    uiName     = "UIQLadder",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------

    -----------------------
    -- Parameters Module --
    -----------------------
    joinType       = nil,

    isMaster       = false,
    -- roomInfo       = nil,
    joinRoomBuf    = nil,
    masterId       = nil,
    matchType      = "MT_QUALIFYING_NEWER",
    estimateTime   = 30,		-- Read from config.
    lvLock         = nil,		-- Read form config

    nextShowUI         = nil,
    nextShowUIParams   = nil,
    nextShowUISubID    = nil,
    teamList           = nil,
    friendsList        = nil,
    curFriendItem      = nil,
    winStateIconList   = nil,
    userInfo           = nil,
    myAccountId        = nil,
    removeAccIdForBack = nil,

    patterned     = false,
    timeToStartPattern = 0,
    inviteProtectedList = nil,
    reConnectAction = nil,
    disConnectionAction = nil,
    leagueReset = 0,
    maxInvite,
    inviteNum = 0,
    onRoomUserHandler = nil,
    isBackInMatch = false,
    awardTip,                   -- 奖励提示.
    resetFightNum = 0,          -- 战绩奖励重置数量.
    logic,
    -- roomUserHolder = nil,
    -----------------ui----------------------
    uiScrollViewAsyncLoadItem, -- Added by Conglin
}


---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIQLadder:Awake()
    self:UiParse()				-- Foucs on UI Parse.
    self.myAccountId = MainPlayer.Instance.AccountID

    -- 此模式已修改为常规赛，暂时不做开放等级限制
    -- local t = GameSystem.Instance.FunctionConditionConfig
    -- local enum = t:GetFuncCondition("UIQualifyingNewer").conditionParams:GetEnumerator()
    -- enum:MoveNext()
    self.lvLock = 0
    self.reConnectAction = LuaHelper.Action(self:OnReconnectedHandler())
    self.disConnectionAction = LuaHelper.Action(self:OnDisConnected())
    -- local resets = GameSystem.Instance.CommonConfig:GetString("gQaulifyingNewerResetTime")
    -- self.leagueReset =  {}
    -- for w in string.gmatch(resets, "[^&]+") do
    --     table.insert(self.leagueReset, w)
    -- end

    local resetFightNum = GameSystem.Instance.CommonConfig:GetString("gQaulifyingNewerResetFightNum")
    self.resetFightNum = tonumber(resetFightNum)

    self.maxInvite = tonumber(GameSystem.Instance.CommonConfig:GetString("gPVPInviteFriendMax"))
    -- self.roomUserHolder = {}
    self.onRoomUserHandler = self:MakeOnRoomUserHandler()
    self:Register(true)
end

function UIQLadder:Start()
    local mt = self.matchType
    if mt == "MT_QUALIFYING_NEWER" then
        self.uiTitle.spriteName = "com_tencent_t144"
        self.logic = QualifyingNewer
    elseif mt == "MT_PVP_3V3" then
        self.uiTitle.spriteName = "com_tencent_t150"
        self.logic = Ladder
    end




    self.inviteProtectedList = {}
    local t = getLuaComponent(createUI("ButtonBack",self.uiButtonBack))
    t.onClick = self:ClickBack()

    -- self.winStateIconList = {}
    -- for i = 1, 5 do
    --     t = self.transform:FindChild("Middle/Up/Grid/"..i.."/Icon"):GetComponent("UISprite")
    --     table.insert(self.winStateIconList, t)
    -- end

    addOnClick(self.uiStartPatternBtn.gameObject, self:ClickStartPattern())
    addOnClick(self.uiTabLeft.gameObject, self:ClickLeftTab(true))
    addOnClick(self.uiTabRight.gameObject, self:ClickLeftTab(false))
    -- addOnClick(self.uiRule.gameObject, self:ClickRule())
    -- UIEventListener.Get(self.uiAward.gameObject).onPress = LuaHelper.BoolDelegate(self:MakeOnAwardPress())

    -- self:Refresh()
end

function UIQLadder:Refresh()
    local mt = self.matchType
    local backFromMatch = QualifyingNewer.inBackToLadder
    local roomUserHolder
    local roomUserMasterIdHolder
    local inBackToLadder
    print("1927 - <UIQLadder>  mt=",mt)
    if mt == "MT_QUALIFYING_NEWER" then
        self.uiTitle.spriteName = "com_tencent_t144"
        self.logic = QualifyingNewer
    elseif mt == "MT_PVP_3V3" then
        self.uiTitle.spriteName = "com_tencent_t150"
        self.logic = Ladder
    end



    backFromMatch = self.logic.inBackToLadder
    roomUserHolder = self.logic.roomUserHolder
    roomUserMasterIdHolder = self.logic.roomUserMasterIdHolder
    inBackToLadder= self.logic.inBackToLadder


    print("1927 - <UIQLadder>  Ladder, self.logic, Ladder.inBackToLadder, self.logic.inBackToLadder=",Ladder, self.logic, Ladder.inBackToLadder, self.logic.inBackToLadder)
    print("1927 - <UIQLadder>  inBackToLadder=",inBackToLadder)

    self.logic.onRoomUserHandler = self.onRoomUserHandler
    self.logic.uiLadder = self



    print("1927 - <UIQLadder> Refresh Start, backFromMatch=", backFromMatch)
    self.inviteNum = 0



    if self.removeAccIdForBack then
        Scheduler.Instance:AddTimer(0.3, false, self:CheckRemoveAccIdForBack())
        return
    end


    -- friend
    self.friendsList = {}

    CommonFunction.ClearGridChild(self.uiFriendsGrid.transform)

    -- 异步加载好友 modify by Conglin
    local friendsTable = Friends.FriendList-- FriendData.Instance:GetList(FriendOperationType.FOT_QUERY)
    local friendsArray = {}
    local friendsCount = 0
    for k, v in pairs(friendsTable) do
        friendsCount = friendsCount + 1
        friendsArray[friendsCount] = v
        --self:AddFriendList(v)
    end

    self.uiScrollViewAsyncLoadItem.OnCreateItem = function(index, parent)

       index = index + 1
        local go = self:AddFriendList(friendsArray[index])

        if index == 1 then
            if self.friendsList and self.friendsList[1] then
                self:ClickFriendItem()(self.friendsList[1])
            end
        end

        return go;
    end

    self.uiScrollViewAsyncLoadItem:Refresh(friendsCount)

    -- team
    self.teamList    = {}
    CommonFunction.ClearGridChild(self.uiTeamGrid.transform)
    for i = 1, 3 do
        local t = getLuaComponent(createUI("LadderMemberIcon", self.uiTeamGrid.transform))
        t:SetData(nil)
        table.insert(self.teamList, t)
    end

    print("1927 - <UIQLadder>  roomUserHolder=",roomUserHolder)
    print("1927 - <UIQLadder>  roomUserMasterIdHolder=",roomUserMasterIdHolder)
    for k, v in pairs(roomUserHolder) do
        print("1927 - <UIQLadder> try to MakeOnRoomUSerHandler on Refreh v=",v)
        self:MakeOnRoomUserHandler()(v)
    end

    if roomUserMasterIdHolder ~= 0 then
        self.masterId = roomUserMasterIdHolder
    end

    self.uiTeamGrid.repositionNow = true

    if not backFromMatch then
        if not self.isMaster then
            local resp, error = protobuf.decode("fogs.proto.msg.JoinRoomResp",self.joinRoomBuf)
            self.userInfo = resp.info.user_info
            self.masterId = resp.info.master
        else
            self.masterId = self.myAccountId
        end

        for i = 1, 3 do
            local info = self.userInfo[i]
            if info.acc_id ~= 0 then
                self:UpdateTeam(info)
            end
        end
    else
        -- self.userInfo = QualifyingNewer.roomUserHolder
        -- self.masterId = QualifyingNewer.roomUserMasterIdHolder
    end

    roomUserHolder = {}

    self.uiStartPatternBtn.isEnabled = self.isMaster


    self:ApplyMasterChange(self.masterId)

    -- 领取奖励.
    local ladderInfo = MainPlayer.Instance.pvpLadderInfo
    print("1927 - <UIQLadder>  ladderInfo.league_awards_flag=",ladderInfo.league_awards_flag)

    if ladderInfo.league_awards_flag == 1 then
        print("1927 - <UIQLadder> Send GetLadderAwardsReq")
        local operation = {}
        local req = protobuf.encode("fogs.proto.msg.GetLadderLeagueAwardsReq", operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.GetLadderLeagueAwardsReqID,req)
    else
        -- self.uiAward.spriteName = "tencent_box3"
    end
    self:DataRefresh()

    if backFromMatch then
        print("1927 - <UIQLadder>  Send RoomActionReq Return Room")
        local userInfo = {}
        userInfo.acc_id = self.myAccountId
        local operation = {
            user_info = userInfo,
            type = "RAT_RETURN_ROOM"
        }
        local req = protobuf.encode("fogs.proto.msg.RoomActionReq", operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.RoomActionReqID,req)
        self.logic.inBackToLadder = false
    end
end

function UIQLadder:FixedUpdate()
    -- if self.patterned then
        -- local t = math.floor((os.time()-self.timeToStartPattern) )
        -- self.uiWaitCounter.text = string.format(getCommonStr("LADDER_WAIT_COUNTER"), t, self.estimateTime)
    -- end
    for k, v in pairs( self.inviteProtectedList) do
        local newValue = v - UnityTime.deltaTime
        if newValue < 0 then
            self.inviteProtectedList[k] = nil
        else
            self.inviteProtectedList[k] = newValue
        end
    end

    local ladderInfo = MainPlayer.Instance.pvpLadderInfo

    -- TODO: Comment condition to test.
    -- if self.leagueReset ~= 0 and ladderInfo.league_info.Count == self.resetFightNum then

    -- local t = os.date("*t", GameSystem.Instance.mTime)
    -- local tHour = 0
    -- local tr = self.leagueReset
    -- for k, v in pairs(tr) do
    --     if v > t.hour then
    --         tHour = v
    --         break
    --     end
    --     tHour = 24 + tr[1]
    -- end

    -- local h = tHour - t.hour
    -- local m = 0

    -- if t.min ~= 0 then
    --     h = hourOff - 1
    --     m = 60 - t.min
    -- end

    -- if h <= 0 then
    --     self.uiAwardDetail.text = string.format(getCommonStr("RESET_REWARD_BY_MINUTE"), m)
    -- else
    --     self.uiAwardDetail.text = string.format(getCommonStr("RESET_REWARD_BY_HOUR"), h)
    -- end

    -- if true then
    --     local t = os.date("*t", GameSystem.Instance.mTime)
    --     local r = self.leagueReset
    --     local index = 0
    --     while r * index - t.hour <= 0 do
    --         index = index + 1
    --     end

    --     local h = index  * r - t.hour
    --     local m = 0
    --     if t.min ~= 0 then
    --         h = h - 1
    --         m = 60 - t.min
    --     end
    --     if h <= 0 then
    --         self.uiAwardDetail.text = string.format(getCommonStr("RESET_REWARD_BY_MINUTE"), m)
    --     else
    --         self.uiAwardDetail.text = string.format(getCommonStr("RESET_REWARD_BY_HOUR"), h)
    --     end
    -- end

end

function UIQLadder:CheckRemoveAccIdForBack()
    return function()
        if self.removeAccIdForBack then
            local inTeam = false
            for k, v in pairs(self.removeAccIdForBack) do
                print("1927 - <UIQLadder>  v=",v)
                if self:RemoveTeam(v) then
                    inTeam  = true
                end
            end
            print("1927 - <UIQLadder>  inTeam=",inTeam)

            if not inTeam then
                self.tip = getLuaComponent(createUI("PvpTipNew"))
                local pos = self.tip.transform.localPosition
                pos.z = -500
                self.tip.transform.localPosition = pos
                UIManager.Instance:BringPanelForward(self.tip.gameObject)
                self.tip.averTime = self.logic.lastAverTime
                self.tip.matchType = self.matchType
                self.tip.onCancel = self:OnCancelTip()
                self.tip.onCancelMatchHandler = self:CancelMatchHandler()

                self.tip:SetCancelText(self.isMaster
                                        and getCommonStr("STR_CANCEL_PATTERN")
                                        or getCommonStr("STR_LEAVE_TEAM"))
                -- self:PartternMode(true)
            else
                if self.tip then
                    self.tip:Close()
                    self.tip = nil
                end
            end
            self:PartternMode(not inTeam)
            self.removeAccIdForBack = nil -- reset.
            for k, v in pairs(self.friendsList) do
                v.gameObject:SetActive(false)
                v.gameObject:SetActive(true)
            end
            return
        end
    end
end


function UIQLadder:OnEnable()
    -- print("1927 - <UIQLadder> OnEnable called self.matchType=", self.matchType)
    -- self.logic.onRoomUserHandler = self.onRoomUserHandler
    -- self.logic.uiLadder = self
end


function UIQLadder:OnDisable()
    print("1927 - <UIQLadder> OnDisable() called, self.matchType=", self.matchType)
    if self.logic.onRoomUserHandler == self.onRoomUserHandler then
        self.logic.onRoomUserHandler = nil
    end
    self.logic.roomUserHolder = {}
    self.logic.uiLadder = nil
end



function UIQLadder:OnDestroy()
    self:Register(false)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function UIQLadder:DataRefresh()
    -- Played Info
    -- local ladderInfo = MainPlayer.Instance.pvpLadderInfo
    -- local score = MainPlayer.Instance.pvpLadderScore

    -- self.uiLadderScore.text = string.format(getCommonStr("STR_LADDER_POINT"), score)
    -- self.uiWinNum.text = ladderInfo.win_times
    -- self.uiPlayedNum.text = ladderInfo.race_times

    -- self.uiWinRate.gameObject:SetActive(ladderInfo.race_times ~= 0 )
    -- if ladderInfo.race_times ~= 0 then
    --     local rate = math.ceil( (ladderInfo.win_times / ladderInfo.race_times) * 100)
    --     self.uiWinRate.text = rate .. "%"
    -- end

    -- self.uiContinueWinNum.text = ladderInfo.max_winning_streak


    -- local ladderConfig = GameSystem.Instance.ladderConfig
    -- local season = ladderConfig:GetSeason(ladderInfo.season)
    -- local ladderLevel = ladderConfig:GetLevelByScore(score)
    -- self.uiMyLadderIcon.spriteName = ladderLevel.icon
    -- self.uiHighestLv.text = ladderLevel.name

    -- self.uiDeadLine.text = string.format(getCommonStr("STR_FIELD_PROMPT12"), season.endYear, season.endMonth, season.endDay)

    -- self.uiEndTime.text = season.endYear.."/"..season.endMonth.."/"..season.endDay

    -- local enum = ladderInfo.league_info:GetEnumerator()
    -- local kc = 0
    -- local winNum = 0
    -- while enum:MoveNext() do
    --     kc = kc + 1
    --     local v = enum.Current
    --     -- self.winStateIconList[kc].spriteName = v == 0 and "tencent_fivelose" or "tencent_fivewin"
    --     if v ~= 0 then
    --         winNum = winNum + 1
    --     end
    -- end

    -- local ladderReward = ladderConfig:GetReward(winNum)
    -- local hasReward = false
    -- if ladderReward then
    --     hasReward = ladderReward.rewards.Count ~= 0
    -- end

    -- if ladderInfo.league_awards_flag == 1 then
    -- else
    -- end


    -- for i = kc + 1,  5 do
    --     self.winStateIconList[i].gameObject:SetActive(false)
    -- end


    -- if kc ~= 5 then
    --     self.uiAwardDetail.text = getCommonStr("FINISH_FIVE_MATCH_CAN_GET")
    --     self.uiAward.spriteName = "tencent_box3"
    -- else
    --     self.uiAward.spriteName = hasReward and "tencent_box2" or "tencent_box3"
    -- end

end

function UIQLadder:ClickBack()
    return function()
        if self.patterned then
            if self:GetRealTeamNum() == 0 then
                self:BackInMatching(true)()
                return
            end

            local  str = "IN_MATCHING_BACK_WILL_EXIT_QUEUE"
            CommonFunction.ShowPopupMsg(
                getCommonStr(str),
                nil,
                LuaHelper.VoidDelegate(self:BackInMatching(true)),
                LuaHelper.VoidDelegate(self:BackInMatching(false)),
                nil,
                nil)
            return
        end

        self:SendExitRoom(true)()
    end
end



--------------------------------------------------------------------------------
-- Function Name : BackInMatching
-- Create Time   : Wed May 11 10:35:09 2016
-- Input Value   : bool - 是否离开
-- Return Value  : nil
-- Description   : 在匹配时, 返回
--------------------------------------------------------------------------------
function UIQLadder:BackInMatching(isExit)
    return function()
        if not isExit then
            return
        end
        self.isBackInMatch = true
        self:ClickStartPattern()()
    end
end


function UIQLadder:SendExitRoom(toSend)
    return function()
        if toSend then
            print("1927 - <UIQLadder> SendExitRoomReq")
            local operation = {
                acc_id = self.myAccountId,
                type = self.matchType,
            }
            local req = protobuf.encode("fogs.proto.msg.ExitRoomReq",operation)
            LuaHelper.SendPlatMsgFromLua(MsgID.ExitRoomReqID,req)
        end
    end
end

function UIQLadder:DoClose()
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
end

function UIQLadder:OnClose()
    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        TopPanelManager:HideTopPanel()
    end
end


function UIQLadder:AddFriendList(info)
    local go = createUI("LadderFriendItem", self.uiFriendsGrid.transform)
    local t = getLuaComponent(go)
    t.onClick = self:ClickFriendItem()
    t.onClickInvite = self:ClickFriendInvite()
    t.isMaster = self.isMaster
    t:SetData(info)
    table.insert(self.friendsList, t)
    self.uiFriendsGrid.repositionNow = true
    return go;
end

function UIQLadder:GetFriendItem(accId)
    for k, v in pairs( self.friendsList ) do
        if v.friendInfo.acc_id == accId then
            return v
        end
    end
end

function UIQLadder:Register(isRegister)
    if isRegister then
        -- LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyRoomUserID, self:RoomUserHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyRoomUserExitID, self:RoomUserExitHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.ExitRoomRespID, self:RoomMasterExitHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyMatchInfoID, self:MatchInfoHandler(), self.uiName)
        -- LuaHelper.RegisterPlatMsgHandler(MsgID.CancelMatchRespID, self:CancelMatchHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, self:StartMatchHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.RefreshLadderInfoID,
                                         self:RefreshLadderInfo(),
                                         self.uiName)

        LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyUserReturnID, self:UserReturnHandler(), self.uiName)

        LuaHelper.RegisterPlatMsgHandler(MsgID.GetLadderLeagueAwardsRespID, self:LadderAwardsHandler(), self.uiName)
        PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.reConnectAction
        PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected + self.disConnectionAction
    else
        -- LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyRoomUserID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyRoomUserExitID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.ExitRoomRespID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyMatchInfoID, self.uiName)
        -- LuaHelper.UnRegisterPlatMsgHandler(MsgID.CancelMatchRespID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.StartMatchRespID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.RefreshLadderInfoID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetLadderLeagueAwardsRespID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyUserReturnID, self.uiName)


        PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.reConnectAction
        PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected - self.disConnectionAction
    end
end

function UIQLadder:ClickFriendItem()
    return function(item)
        if self.curFriendItem then
            self.curFriendItem:Selected(false)
        end
        self.curFriendItem = item
        self.curFriendItem:Selected(true)
        self:UpdateInviteBtn()
    end
end

function UIQLadder:ClickFriendInvite()
    return function(item)
        local t = item
        if t == nil then
            CommonFunction.ShowTip(getCommonStr("STR_PLEASE_SELECT_FRIEND_TO_INVITE"), nil)
            return
        end

        if t.friendInfo.level < self.lvLock then
            CommonFunction.ShowTip(getCommonStr("STR_FRIEND_LEVEL_NOT_OK_TO_INVITE"), nil)
            return
        end

        local inviteNum = 0
        for k, v in pairs(self.friendsList) do
            if v.isInviting then
                inviteNum = inviteNum + 1
            end
        end

        if inviteNum >= self.maxInvite then
            CommonFunction.ShowTip(string.format(getCommonStr("STR_MAX_INVITE_NUM"), self.maxInvite),
                                   nil)
            return
        end

        local inTeam = self:FindInTeam(t.friendInfo.acc_id)
        local userInfo = {}
        userInfo.acc_id = t.friendInfo.acc_id
        local operation
        if inTeam then
            operation = {
                user_info = userInfo,
                type = "RAT_KICK",
            }
            print("1927 - <UIQLadder> SendKick")
        else
            operation = {
                user_info = userInfo,
                type = "RAT_INVITE",
            }

            -- for k, v in pairs(self.inviteProtectedList) do
            --     if k == userInfo.acc_id then
            --         CommonFunction.ShowTip(getCommonStr("YOU_CAN_INVITE_SAME_PLAYER_IN_FIVE_SECONDS"), nil)
            --         return
            --     end
            -- end
            print("1927 - <UIQLadder> SendInvite")
            self.inviteProtectedList[userInfo.acc_id] = 5
            t:SetIsInviting(true)

        end
        local req = protobuf.encode("fogs.proto.msg.RoomActionReq",operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.RoomActionReqID,req)
        LuaHelper.RegisterPlatMsgHandler(MsgID.RoomActionRespID, self:RoomActionHandler(), self.uiName)
    end
end

function UIQLadder:ClickStartPattern()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.season_btn) then return end

        print("1927 - <UIQLadder> ClickStartPattern self.patterned=",self.patterned)
        if not self.patterned then
            local operation = {
                acc_id = self.myAccountId,
                type   = self.matchType,
            }
            print("1927 - <UIQLadder> Send StartMatchReq to server")

            print("1927 - <UIQLadder> StartMatchReq operation.type=",operation.type)
            local req = protobuf.encode("fogs.proto.msg.StartMatchReq",operation)
            LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID,req)
        else
            -- print("1927- <UIQLadder> Send CancleMatchReq to server")
            -- local operation = {
            --     acc_id = self.myAccountId,
            --     type   = self.matchType,
            -- }
            -- local req = protobuf.encode("fogs.proto.msg.CancelMatchReq",operation)
            -- LuaHelper.SendPlatMsgFromLua(MsgID.CancelMatchReqID,req)
        end
    end
end

function UIQLadder:ClickLeftTab(isLeft)
    return function()
        self.uiFriendsGrid.gameObject:SetActive(self.uiTabLeft.transform:GetComponent("UIToggle").value)
    end
end


function UIQLadder:ClickRule()
    return function()
        local rulePopup = getLuaComponent(createUI("MatchRulePopup"))
        rulePopup.matchType = "MT_PVP_3V3"
        UIManager.Instance:BringPanelForward(rulePopup.gameObject)
    end
end


function UIQLadder:ShowAwardTip()
    if self.awardTip then
        return
    end
    local transform = self.uiAward.transform

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
        y = -0.4
    elseif position.x >= 0 and position.y >= 0 then
        x = -0.75
        y = -0.4
    elseif position.x >= 0 and position.y <= 0 then
        x = -0.75
        y = 0.4
    elseif position.x <= 0 and position.y <= 0 then
        x = 0.75
        y = 0.4
    end
    local pos = popup.transform.position
    pos.x = position.x + x
    pos.y = position.y + y
    pos.z = -2
    popup.transform.position = pos

    self.awardTip = popup
end

function UIQLadder:HideAwardTip()
    if self.awardTip then
        NGUITools.Destroy(self.awardTip.gameObject)
        self.awardTip = nil
    end
end

function UIQLadder:MakeOnAwardPress()
    return function (go, isPress)
        if isPress then
            self:ShowAwardTip()
        else
            self:HideAwardTip()
        end
    end
end


-- TODO: print Debug
function UIQLadder:PrintUserInfo(u, msg)
    print("1927 - <UIQLadder> PrintUserInfo -------------Start ", msg)

    print("1927 - <UIQLadder>  u.acc_id=",u.acc_id)
    print("1927 - <UIQLadder>  u.name=",u.name)
    print("1927 - <UIQLadder>  u.icon=",u.icon)
    print("1927 - <UIQLadder>  u.state=",u.state)
    print("1927 - <UIQLadder>  u.level=",u.level)


    print("1927 - <UIQLadder> PrintUserInfo -------------End ", msg)

end




function UIQLadder:RoomActionHandler()
    return function(buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.RoomActionRespID, self.uiName)
        local resp, error = protobuf.decode('fogs.proto.msg.RoomActionResp',buf)
        if resp then
            local r = resp.result
            local userInfo = resp.user_info
            local accId, userState,type, friend
            if userInfo then
                accId     = userInfo.acc_id
                userState = userInfo.state
                if accId then
                    friend = self:GetFriendItem(accId)
                end
            end
            local type = resp.type

            if r == 0 then
                -- if type == "RAT_INVITE" then
                --  self:UpdateTeam(userInfo)
                --  self:UpdateInviteBtn()
                --  friend:SetIsInviting(true)
                -- end
                return
            end

            if r == enumToInt(ErrorID.FRIEND_OFFLINE) then
                local f = friend
                f.friendInfo.online = Ladder.PS.OFFLINE
                f:SetIsInviting(false)
                f:DataRefresh()
                self:RemoveTeam(accId)
                self:UpdateInviteBtn()
                CommonFunction.ShowTip(getCommonStr("FRIEND_OFFLINE"), nil)
            elseif r == enumToInt(ErrorID.FRIEND_IN_ROOM) or r == enumToInt(ErrorID.FRIEND_IN_MATCH) then
                local f = friend
                f.friendInfo.online = Ladder.PS.ROOM
                f:SetIsInviting(false)
                f:DataRefresh()
                self:UpdateInviteBtn()
                CommonFunction.ShowTip(getCommonStr("FRIEND_IN_ROOM"), nil)
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end


--------------------------------------------------------------------------------
-- Function Name : ApplyMasterChange
-- Create Time   : Thu Mar 31 11:51:50 2016
-- Input Value   : masterId
-- Return Value  : nil
-- Description   : 根据传入房主Id，设置是否是房主，如果是，做响应的改变。
--------------------------------------------------------------------------------
function UIQLadder:ApplyMasterChange(masterId)
    if not masterId or masterId == 0 then
        warning("masterId is ",  masterId, " not right so return in ApplyMasterChange" )
        return
    end
    self.masterId = masterId
    self.isMaster = self.myAccountId == masterId

    if self.isMaster then
        self.uiStartPatternBtn.isEnabled = self.isMaster
        for k, v in pairs( self.friendsList ) do
            v:ApplyMasterChange(true)
        end
    end

    for k, v in pairs(self.teamList) do
        if v.userInfo and v.userInfo.acc_id == masterId then
            v:SetIsMaster(true)
        end
    end

end

function UIQLadder:MakeOnRoomUserHandler()
    return function(users, masterId)
        print("1927 - <UIQLadder> MakeOnRoomUserHandler called")
        if self.teamList == nil then
            print("1927 - <UIQLadder> self.teamList == nil MakeOnRoomUserHandler return false")
            return false
        end

        for i = 1,3 do
            local u = users[i]
            if u.acc_id ~= 0 then
                self:UpdateTeam(u)
                if self.curFriendItem and self.curFriendItem.friendInfo.acc_id == u.acc_id then
                    self:UpdateInviteBtn(self.curFriendItem)
                end
            end
        end

        if masterId and masterId ~= 0 then
            self:ApplyMasterChange(masterId)
        end

        return true
    end
end



function UIQLadder:RoomUserExitHandler()
    return function(buf)
        local resp, error = protobuf.decode('fogs.proto.msg.NotifyRoomUserExit',buf)
        if resp then
            local exitUserInfo = resp.users
            self:RemoveTeam(exitUserInfo.acc_id)
            local f = self:GetFriendItem(exitUserInfo.acc_id)
            if f then
                f:SetFree()
            end
            local masterId = resp.master_id
            if masterId then
                self:ApplyMasterChange(resp.master_id)
            end
        end
    end
end

function UIQLadder:RoomMasterExitHandler()
    return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.ExitRoomResp",buf)
        if resp then
            local result = resp.result
            if self.nextShowUI == nil then
                self.nextShowUI = "UIHall"
            end

            if result == 0 then
                self:DoClose()
            elseif result == enumToInt(ErrorID.MASTER_EXIT) then
                if self.isMaster then
                    self:DoClose()
                else
                    -- self:InfoPlayerExitRoom(getCommonStr("INFO_ROOM_MASTER_EXIT_ROOM"))
                end
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end

function UIQLadder:InfoPlayerExitRoom(str)
    CommonFunction.ShowPopupMsg(str,
                                nil,
                                LuaHelper.VoidDelegate(self:OnConfirmToExit()),
                                nil,
                                getCommonStr("BUTTON_CONFIRM"),
                                nil)
end

function UIQLadder:OnConfirmToExit()
    return function()
        self.nextShowUI = "UIHall"
        self:DoClose()
    end
end

function UIQLadder:MatchInfoHandler()
    return function(buf)
        local resp, error = protobuf.decode('fogs.proto.msg.NotifyMatchInfo',buf)
        if resp then
            if self.tip ~= nil then
                self.tip:Close()
                self.tip = nil
            end
            print("1927 - <UIQLadder>  resp.ai_flag=",resp.ai_flag)

            self:PartternMode(false)
            if resp.ai_flag == 0 then
                self.nextShowUI = "UISelectRole"
                self.nextShowUIParams = {
                    teamInfo = resp.team_info,
                    isLadder = true,
                    startLabel=getCommonStr("STR_READY_ALREAY"),
                    title = getCommonStr("STR_LABBER"),
                    masterId = self.masterId,
                    matchType = self.matchType,
                    noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_LADDER"),
                    preUI = self.uiName
                }
                self:DoClose()
            else
                local team_info = {}
                local grade = resp.grade
                print("1927 - <UIQLadder>  grade=",grade)
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
                    matchType = self.matchType,
                    preUI = self.uiName
                }
                self.uiSelectRole = TopPanelManager:ShowPanel("UISelectRole", nil, nextShowUIParams)
                QualifyingNewerAI.StartAIMatch(self.uiSelectRole, session)
            end
        end
    end
end

function UIQLadder:StartMatchHandler()
    return function(buf)
        local resp, error = protobuf.decode('fogs.proto.msg.StartMatchResp',buf)
        print("1927 - <UIQLadder> StartMatchResp resp.result=",resp.result)
        print("1927 - <UIQLadder>  self=",self)

        if resp then
            if resp.result == 0 then
                self.tip = getLuaComponent(createUI("PvpTipNew"))
                local pos = self.tip.transform.localPosition
                pos.z = -500
                self.tip.transform.localPosition = pos
                UIManager.Instance:BringPanelForward(self.tip.gameObject)
                self.tip.averTime = resp.aver_time
                self.tip.matchType = self.matchType
                self.tip.onCancel = self:OnCancelTip()
                self.tip.onCancelMatchHandler = self:CancelMatchHandler()

                self.tip:SetCancelText(self.isMaster
                                        and getCommonStr("STR_CANCEL_PATTERN")
                                        or getCommonStr("STR_LEAVE_TEAM"))
                self:PartternMode(true)
                if self.matchType == "MT_PVP_3V3" then
                    Ladder.lastAverTime = resp.aver_time
                elseif self.matchType == "MT_QUALIFYING_NEWER" then
                    QualifyingNewer.lastAverTime = resp.aver_time
                end
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil, nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end


function UIQLadder:OnCancelTip()
    return function ()
        if self.tip == nil then
            return
        end
        self.tip:Close()
        self.tip = nil
    end
end



function UIQLadder:UserReturnHandler()
    return function(buf)
        local resp, error = protobuf.decode('fogs.proto.msg.NotifyUserReturn',buf)
        print("1927 - <UIQLadder> UserReturnHandler resp.acc_id=",resp.acc_id)
        local friendItem = self:GetFriendItem(resp.acc_id)
        if friendItem then
            friendItem.friendInfo.online = Ladder.PS.NORMAL
        end

        print("1927 - <UIQLadder>  friendItem=",friendItem)

        local member = self:FindInTeam(resp.acc_id)
        if member then
            member.userInfo.state = "RUS_NORMAL"
            self:UpdateTeam(member.userInfo)
        end

        print("1927 - <UIQLadder>  memeber=",memeber)
    end

end



--------------------------------------------------------------------------------
-- Function Name : LadderAwardsHandler
-- Create Time   : Thu May 12 13:29:09 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 战绩奖励领取消息返回处理.
--------------------------------------------------------------------------------
function UIQLadder:LadderAwardsHandler()
    return function(buf)
        print("1927 - <UIQLadder> LadderAwardsHandler called")
        local resp, error = protobuf.decode('fogs.proto.msg.GetLadderLeagueAwardsResp',buf)
        local result = resp.result
        print("1927 - <UIQLadder>  result=",result)
        if result ~= 0 then
            return
        end

        MainPlayer.Instance.pvpLadderInfo.league_awards_flag = 0
        self:DataRefresh()
        self:CreateGoodsAcquire(resp.awards)
    end
end



--------------------------------------------------------------------------------
-- Function Name : CreateGoodsAcquire
-- Create Time   : Thu May 12 13:42:26 2016
-- Input Value   : table -> key: id , value - num
-- Return Value  : nil
-- Description   : nil
--------------------------------------------------------------------------------
function UIQLadder:CreateGoodsAcquire(goodsTb)
    local goodsAcquire = getLuaComponent(createUI("GoodsAcquirePopup"))
    for k, v in pairs(goodsTb) do
        print("1927 - <UIQLadder>  k, v=",k, v)
        print("1927 - <UIQLadder>  v.id, v.value=",v.id, v.value)
        goodsAcquire:SetGoodsData(v.id, v.value)
    end
end


function UIQLadder:RefreshLadderInfo()
    return function(buf)
        self:Refresh()
    end
end


function UIQLadder:CancelMatchHandler()
    return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.CancelMatchResp",buf)
        print("1927 - <UIQLadder> CancelMatchHandler resp.acc_id=",resp.acc_id)

        if resp then
            self:PartternMode(false)
            if resp.result == enumToInt(ErrorID.PARTTEN_OFFLINE) then

                local fi = self:GetFriendItem(resp.acc_id)
                if fi then
                    fi.friendInfo.online = Ladder.PS.OFFLINE
                end
                self:UpdateInviteBtn()
                self:RemoveTeam(resp.acc_id)
            elseif resp.result ~= 0 then
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end

            local exitRoom = self.isBackInMatch
                or resp.acc_id == MainPlayer.Instance.AccountID
                and not self.isMaster

            print("1927 - <UIQLadder>  exitRoom=",exitRoom)

            if exitRoom then
                self:SendExitRoom(true)()
                self.isBackInMatch = false
            end
        end
    end
end

function UIQLadder:OnReconnectedHandler()
    return function()
        self:ClearRoomInfo()
        self.nextShowUI = "UIHall"
        self:DoClose()
    end
end

function UIQLadder:ClearRoomInfo()
    self:PartternMode(false)
end

function UIQLadder:PartternMode(isPattern)
    if isPattern then
        self.timeToStartPattern = os.time()
    else
        self.timeToStartPattern = 0
    end
    self.transform:FindChild("Middle/ButtonOK/Label"):GetComponent("MultiLabel"):SetText(isPattern and getCommonStr("STR_CANCEL_PATTERN") or getCommonStr("STR_LADDER_START"))
    -- self.uiWaitCounter.gameObject:SetActive(isPattern)
    self.patterned = isPattern
    print("1927 - <UIQLadder> PatternMode self.patterned=",self.patterned)
    for k, v in pairs(self.friendsList) do
        v:SetPattern(isPattern)
    end
    self:UpdateInviteBtn()
end


------------------------------------
-------  Team Action - Start
--------------------------------------------------------------------------------
-- Function Name : UpdateTeam
-- Create Time   : Sat Mar 19 16:27:52 2016
-- Input Value   : UserInfo
-- Return Value  : true/false
-- Description   : If in team, refresh, otherwise add.
--------------------------------------------------------------------------------
function UIQLadder:UpdateTeam(userInfo)
    self:PrintUserInfo(userInfo, "UpdateTeam")
    local t = self:FindInTeam(userInfo.acc_id)
    if t then
        t:SetData(userInfo)
        t:Refresh()
        return true
    else
        local f = self:GetFriendItem(userInfo.acc_id)
        for k, v in pairs(self.teamList) do
            if not v.userInfo then
                v:SetData(userInfo, f and f:OnUserInfoChanged() or nil )
                return false
            end
        end
    end
end


----------------------------------------------------
-- Function Name : RemoveTeam
-- Create Time   : Tue Mar  8 19:56:09 2016
-- Input Value   : id
-- Return Value  : t/f
-- Description   : nil
----------------------------------------------------
function UIQLadder:RemoveTeam(id)
    for i=1, #self.teamList do
        local v = self.teamList[i]
        if v and v.userInfo and v.userInfo.acc_id == id then
            local name = v.userInfo.name
            v:SetData(nil)
            self:UpdateInviteBtn(self.curFriendItem)

            if id ~= self.myAccountId then
                CommonFunction.ShowTip(string.format(getCommonStr("PLAYER_EXIT_TEAM"), name), nil)
            end

            return true
        end
    end
end


----------------------------------------------------
-- Function Name : GetTeamNum
-- Create Time   : Tue Mar  8 19:55:06 2016
-- Input Value   : nil
-- Return Value  : num of team member.
-- Description   : nil
----------------------------------------------------
function UIQLadder:GetTeamNum()
    local num = 0
    for i = 1, 3 do
        local t = self.teamList[i]
        if t and t.userInfo then
            num = num + 1
        end
    end
    return num
end


--------------------------------------------------------------------------------
-- Function Name : GetRealTeamNum
-- Create Time   : Sat Mar 26 15:35:08 2016
-- Input Value   : nil
-- Return Value  : num of team member agree already.
-- Description   : nil
--------------------------------------------------------------------------------
function UIQLadder:GetRealTeamNum()
    local teamNum = self:GetTeamNum()
    if teamNum >= 1 then
        teamNum = teamNum - 1
    end
    return teamNum
    -- local num = 0
    -- for k, v in pairs( self.friendsList ) do
    --     if v.isMember then
    --         num = num + 1
    --     end
    -- end
    -- return num
end

----------------------------------------------------
-- Function Name : FindInTeam
-- Create Time   : Tue Mar  8 20:57:53 2016
-- Input Value   : id
-- Return Value  : LadderMemberIcon or nil
-- Description   : nil
----------------------------------------------------
function UIQLadder:FindInTeam(id)
    for i = 1, 3 do
        t = self.teamList[i]
        if t and t.userInfo and t.userInfo.acc_id == id then
            return t
        end
    end
end

----------------------------------------------------
-- Function Name : UpdateInviteBtn
-- Create Time   : Tue Mar  8 21:10:28 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : nil
----------------------------------------------------
function UIQLadder:UpdateInviteBtn()
    -- if self.curFriendItem == nil then
    --	return
    -- end
    -- local item = self.curFriendItem
    -- local online = item.friendInfo.online
    -- self.uiInviteBtn.isEnabled = online == Ladder.PS.NORMAL and not item.isMember and not self.patterned

    -- local alreayMember = self:FindInTeam(item.friendInfo.acc_id) ~= nil
    -- local m = self.uiInviteBtn.transform:FindChild("Label"):GetComponent("MultiLabel")
    -- m:SetText( not alreayMember and getCommonStr("STR_LADDER_INVITE") or getCommonStr("STR_CANCEL_INVITE"))

    -- if not alreayMember and self:GetTeamNum() >= 3 then
    --	self.uiInviteBtn.isEnabled = false
    -- end
end


------------------------------------
-------  Team Action - End


function UIQLadder:OnDisConnected()
    return function()
        self:PartternMode(false)
    end
end



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
function UIQLadder:UiParse()
    -- Top
    self.uiButtonBack     = self.transform:FindChild("Top/ButtonBack"):GetComponent("Transform")
    -- self.uiRule = self.transform:FindChild("Top/Rule"):GetComponent("UISprite")
    -- self.uiTitle = self.transform:FindChild("Middle/Up/Title"):GetComponent("UILabel")


    -- self.uiMyLadderIcon   = self.transform:FindChild("Left/Icon"):GetComponent("UISprite")
    self.uiFriendsGrid    = self.transform:FindChild("Right/ScrollView/Grid"):GetComponent("UIGrid")
    self.uiScrollViewAsyncLoadItem = self.transform:FindChild("Right/ScrollView"):GetComponent("ScrollViewAsyncLoadItem")

    -- self.uiLadderScore    = self.transform:FindChild("Left/Num"):GetComponent("UILabel")
    -- self.uiWinNum         = self.transform:FindChild("Left/Win/Num"):GetComponent("UILabel")
    -- self.uiPlayedNum      = self.transform:FindChild("Left/Number/Num"):GetComponent("UILabel")
    -- self.uiWinRate        = self.transform:FindChild("Left/Winning/Num"):GetComponent("UILabel")
    -- self.uiContinueWinNum = self.transform:FindChild("Left/Victories/Num"):GetComponent("UILabel")
    -- self.uiEndTime = self.transform:FindChild("Left/EndTime/Num"):GetComponent("UILabel")
    -- self.uiHighestLv = self.transform:FindChild("Left/HighestLv/Level"):GetComponent("UILabel")

    self.uiStartPatternBtn = self.transform:FindChild("Middle/ButtonOK/Bg"):GetComponent("UIButton")
    -- self.uiWaitCounter     = self.transform:FindChild("Left/WaitCounter"):GetComponent("UILabel")
    -- self.uiAwardDetail        = self.transform:FindChild("Middle/Award/Detail"):GetComponent("UILabel")
    -- self.uiInviteBtn       = self.transform:FindChild("Right/ButtonOK"):GetComponent("UIButton")
    self.uiTeamGrid      = self.transform:FindChild("Middle/Down/BgIcon"):GetComponent("UIGrid")

    -- self.uiAward = self.transform:FindChild("Middle/Up/Bag3"):GetComponent("UISprite")

    self.uiTabLeft = self.transform:FindChild("Right/TabLeft"):GetComponent("UISprite")
    self.uiTabRight = self.transform:FindChild("Right/TabRight"):GetComponent("UISprite")
    self.uiTitle = self.transform:FindChild("Top/Heading"):GetComponent("UISprite")

end

return UIQLadder

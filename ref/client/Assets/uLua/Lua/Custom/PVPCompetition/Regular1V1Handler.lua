require "Custom/Team/UICreateTeam"

local modname = "Regular1V1Handler"
module(modname, package.seeall)

--variables
matchType = "MT_REGULAR_RACE"
leagueType = GameMatch.LeagueType.eRegular1V1
npcRivalName = ""
uiSelectRole = nil
tip = nil
popupCountDown = nil
gameStartRespBuf = nil
gameStartResp = nil
actionOnTimer = nil
confirmWaitTime = 0
isMatching = false
isWaitingConfirm = false
gameModeIDs = nil
gameModeIDOthers = nil
actionOnDisconnected = nil
actionOnReconnected = nil

function Initialize()
    LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, HandleStartMatchResp, modname)
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyGameStartID, HandleNotifyGameStart, modname)
    actionOnTimer = LuaHelper.Action(OnTimer)
    actionOnDisconnected = LuaHelper.Action(OnDisconnected)
    actionOnReconnected = LuaHelper.Action(OnReconnected)
    PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected + actionOnDisconnected
    PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + actionOnReconnected
end


function SelectRole()
    if not gameModeIDs then
        gameModeIDs = {}
        local i = 1
        for i = 1, 10000000 do
            local gameModeID = GameSystem.Instance.CommonConfig:GetUInt("gRegular1V1GameModeID" .. i)
            if gameModeID ~= 0 then
                table.insert(gameModeIDs, gameModeID)
            else
                break
            end
        end
    end

    if not gameModeIDOthers then
        gameModeIDOthers = {}
        local strGameModeIDOthers = GameSystem.Instance.CommonConfig:GetString("gRegular1V1GameModeIDOthers")
        local strIDs = string.split(strGameModeIDOthers, "&")
        for _, strID in ipairs(strIDs) do
            print("GameModeID:", strID)
            table.insert(gameModeIDOthers, tonumber(strID))
        end
    end

    isMatching = false
    isWaitingConfirm = false

    uiSelectRole = TopPanelManager:ShowPanel("UISelectRole", nil, {
        maxSelection = 3,
        title = getCommonStr("STR_REGULAR_MATCH"),
        startLabel = getCommonStr("STR_START_MATCH"),
        noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_MATCH"),
        onStart = OnStart,
        sendChangeRole = true,
    })
end

function OnStart(selectedIDs)
    if isWaitingConfirm then
        return
    end

    SendStartMatchReq(selectedIDs)
end

function SendStartMatchReq(selectedIDs)
    local req = {
        acc_id = MainPlayer.Instance.AccountID,
        type = matchType,
        game_mode = "GM_PVP_REGULAR",
        fight_power = {},
        data = {roles = {},},
    }

    for _, ID in ipairs(selectedIDs) do
        local player = MainPlayer.Instance:GetRole(ID)
        local badgebook = MainPlayer.Instance.badgeSystemInfo:GetBadgeBookByBookId(player.m_roleInfo.badge_book_id)
        player.m_attrData = MainPlayer.Instance:GetRoleAttrs(player.m_roleInfo, nil, nil, nil, nil, badgebook)
        print(modname, ID, player and player.m_name or "NULL", player.m_fightingCapacity)
        table.insert(req.fight_power, {id = ID, value = player.m_fightingCapacity})
        -- 添加涂鸦信息
        table.insert(req.data.roles,{id = ID,badge_book_id = player.m_roleInfo.badge_book_id})
    end

    local regularReq = {
        level = MainPlayer.Instance.Level,
        name = MainPlayer.Instance.Name,
        score = MainPlayer.Instance.pvp_regular.score,
    }
    req.regular = regularReq

    local data = protobuf.encode("fogs.proto.msg.StartMatchReq", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, data)
    print(modname, "Send StartMatchReq, Regular1V1 score:", MainPlayer.Instance.pvp_regular.score)
end

function HandleStartMatchResp(buf)
    local resp, err = protobuf.decode("fogs.proto.msg.StartMatchResp", buf)
    if resp then
        print("1927 - <Regular1V1Handler>  resp.result=",resp.result)
        if resp.type == matchType then
            if resp.result == 0 then
                tip = getLuaComponent(createUI("PvpTipNew"))
                local pos = tip.transform.localPosition
                pos.z = -500
                tip.transform.localPosition = pos
                UIManager.Instance:BringPanelForward(tip.gameObject)
                tip.averTime = resp.aver_time
                tip.matchType = matchType
                tip.onCancel = OnCancelMatch
                isMatching = true
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil, nil)
            end
        end
    else
        error(modname, "HandleStartMatchResp: " .. err)
    end
end

function OnCancelMatch()
    if tip == nil then
        return
    end
    tip:Close()
    tip = nil
end

function HandleNotifyGameStart(buf, force)
    if not force and MainPlayer.Instance.inPvpJoining then
        return
    end

    gameStartRespBuf = buf
    local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)

    if force then
        local resp = resp
        CreatePVPMatch(resp)
        return
    end



    if resp then
        if resp.type == matchType then
            if tip then
                tip:Close()
                tip = nil
            end
            gameStartResp = resp
            popupCountDown = CommonFunction.ShowPopupMsg(getCommonStr('MATCH_JOIN_GAME') .. 10, nil,
                LuaHelper.VoidDelegate(OnConfirmStartMatch), nil,
                getCommonStr("BUTTON_CONFIRM"), "").table
            Scheduler.Instance:AddTimer(1, true, actionOnTimer)
            isMatching = false
            isWaitingConfirm = true
        end
    else
        error(modname, "HandleNotifyGameStart: " .. err)
    end
end

function OnTimer()
    confirmWaitTime = confirmWaitTime + 1
    if popupCountDown ~= nil then
        popupCountDown:SetMessage(getCommonStr('MATCH_JOIN_GAME') .. tostring(10 - confirmWaitTime))
    end

    if confirmWaitTime == 10 then
        if popupCountDown then
            NGUITools.Destroy(popupCountDown.gameObject)
            popupCountDown = nil
        end
        OnConfirmStartMatch()
    end
end

function OnDisconnected()
    if isMatching then
        isMatching = false
        if tip then
            tip:Close()
            tip = nil
        end
    elseif isWaitingConfirm then
        isWaitingConfirm = false
        confirmWaitTime = 0
        Scheduler.Instance:RemoveTimer(actionOnTimer)
        if popupCountDown then
            NGUITools.Destroy(popupCountDown.gameObject)
            popupCountDown = nil
        end
    end
end

function OnReconnected()
end

function OnConfirmStartMatch()
    Scheduler.Instance:RemoveTimer(actionOnTimer)
    confirmWaitTime = 0
    popupCountDown = nil

    if uiSelectRole then
        uiSelectRole:VisibleModel(false)
    end

    local resp = gameStartResp
    print("OnConfirmStartMatch", "race_times:", resp.regular.race_times, "ai_flag:", resp.regular.ai_flag)

    if resp.regular.ai_flag == 2 then -- gamemode
        CreateGameModeMatch(gameStartResp)
    elseif resp.regular.ai_flag == 1 then	-- AI
        CreateAIMatch(gameStartResp)
    else	-- PVP
        CreatePVPMatch(gameStartResp)
    end
end

function CreateGameModeMatch(resp)
    local squadInfo = MainPlayer.Instance.SquadInfo
    local teammates = UintList.New()
    for i = 0, squadInfo.Count - 1 do
        teammates:Add(squadInfo:get_Item(i).role_id)
    end
    local gameModeID
    if resp.regular.race_times + 1 <= table.getn(gameModeIDs) then
        gameModeID = gameModeIDs[resp.regular.race_times + 1]
    else
        local index = math.random(1, table.getn(gameModeIDOthers))
        gameModeID = gameModeIDOthers[index]
    end
    print(modname, "CreateGameModeMatch, GameMode ID:", gameModeID, resp.regular.session_id, matchType, leagueType)
    npcRivalName = UICreateTeam.GenerateName()
    GameSystem.Instance.mClient:CreateNewMatch(gameModeID, resp.regular.session_id, false, leagueType, teammates, nil)
end

function CreateAIMatch(resp)
    StartMatch(resp)
end

function CreatePVPMatch(resp)
    StartMatch(resp)
end

function StartMatch( notifyGameStart )
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
            print("pvp 1 + 2 plus myteam id :", role.id, "fight_power:", role.fight_power)
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
            print("pvp 1 + 2 plus rivalteam id :", role.id, "fight_power:", role.fight_power)
            rivalList:Add( gameRoleInfo)
        end
    end
    print("myname:",myName,"-----rivalName:",rivalName)

    local regular  = notifyGameStart.regular
    local session_id	= regular.session_id
    local matchType = regular.ai_flag == 1 and GameMatch.Type.eAsynPVP3On3 or GameMatch.Type.ePVP_1PLUS

    local matchConfig = GameMatch.Config.New()

    print(modname, "LeagueType:", leagueType)
    matchConfig.leagueType	= leagueType
    matchConfig.type		= matchType
    matchConfig.sceneId		= regular.scene_id
    matchConfig.MatchTime	= GameSystem.Instance.CommonConfig:GetUInt("gChallengeGameTime")
    matchConfig.session_id	= session_id
    matchConfig.ip			= regular.game_ip
    matchConfig.port		= regular.game_port

    print("match session id "..session_id)
    print("match server ip "..regular.game_ip)
    print("match server port "..regular.game_port)

    GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
    if matchType == GameMatch.Type.eAsynPVP3On3 then
        GameSystem.Instance.mClient.mCurMatch:SetNotifyGameStart(gameStartRespBuf)
    end

    uiLoading = createUI("UIChallengeLoading_1"):GetComponent("UIChallengeLoading")
    if uiLoading then
        local pos = uiLoading.transform.localPosition
        pos.z = -400
        uiLoading.transform.localPosition = pos
        uiLoading.scene_name = regular.scene_id
        uiLoading.myName = myName
        uiLoading.rivalName = rivalName
        uiLoading.my_role_list = myList
        uiLoading.rival_list = rivalList
        uiLoading.pvp = true
        uiLoading:Refresh(false)
        NGUITools.BringForward(uiLoading.gameObject)
    end
end

Initialize()

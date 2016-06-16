local moduleName = "Qualifying"

module(moduleName, package.seeall)



function Init()
end


function StartMatch(buf)
    local notifyGameStart, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)

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
    print("1927 - <Qualifying> --1")
    local session_id	= qualifying.session_id
        print("1927 - <Qualifying> --2")
    local matchType = qualifying.ai_flag == 1 and GameMatch.Type.eAsynPVP3On3 or GameMatch.Type.ePVP_1PLUS
    print("1927 - <Qualifying> --3")
    local matchConfig = GameMatch.Config.New()
    print("1927 - <Qualifying> --4")


    matchConfig.leagueType	=  GameMatch.LeagueType.eQualifyingNew
    print("1927 - <Qualifying> --5")
    matchConfig.type		= matchType
    print("1927 - <Qualifying> --6")
    matchConfig.sceneId		= qualifying.scene_id
    print("1927 - <Qualifying> --7")
    matchConfig.MatchTime	= GameSystem.Instance.CommonConfig:GetUInt("gChallengeGameTime")
    print("1927 - <Qualifying> --8")
    matchConfig.session_id	= session_id
    print("1927 - <Qualifying> --9")
    matchConfig.ip			= qualifying.game_ip
    print("1927 - <Qualifying> --10")
    matchConfig.port		= qualifying.game_port
    print("1927 - <Qualifying> --11")

    print("match session id "..session_id)
    print("match server ip "..qualifying.game_ip)
    print("match server port "..qualifying.game_port)

    GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
    if matchType == GameMatch.Type.eAsynPVP3On3 then
        GameSystem.Instance.mClient.mCurMatch:SetNotifyGameStart(buf)
    end

    local uiLoading = createUI("UIChallengeLoading_1"):GetComponent("UIChallengeLoading")
    if uiLoading then
        local pos = uiLoading.transform.localPosition
        pos.z = -400
        uiLoading.transform.localPosition = pos
        uiLoading.scene_name = qualifying.scene_id
        uiLoading.myName = myName
        uiLoading.rivalName = rivalName
        uiLoading.my_role_list = myList
        uiLoading.rival_list = rivalList
        uiLoading.pvp = true
        uiLoading:Refresh(false)
        NGUITools.BringForward(uiLoading.gameObject)
    end
end

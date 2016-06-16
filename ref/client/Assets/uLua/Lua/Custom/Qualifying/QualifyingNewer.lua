local moduleName="QualifyingNewer"

module(moduleName, package.seeall)

gameStartBuf      = nil         -- 开始比赛所用的buffer.
onRoomUserHandler = nil         -- 用于UI回调
roomUserHolder    = nil
roomUserMasterIdHolder  = 0     -- 房主Id
inBackToLadder    = false       -- 返回天梯标识
-- backToLadderStep  = 0           -- 返回天梯步骤.
friendChangedFunc = nil         -- 好友列表刷新回调处理过程
uiQualifyingNewer = nil
isJoinLadder = false            -- 进入天梯标识

onGetGradesAwards = nil         -- Callback of get grade awards.
matchType = "MT_QUALIFYING_NEWER"
isWinShowIncStarAnim = false
lastAverTime = 0                -- 上次匹配时间

function Init()
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyQualifyingNewerLeagueResetID,
                                     NotifyQualifyingNewerLeagueResetHandler,
                                     moduleName)

    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyRoomUserID,
                                     RoomUserHandler,
                                     moduleName)

    -- LuaHelper.RegisterPlatMsgHandler(MsgID.GetQualifyingNewerGradesAwardsRespID,
    --                                  GetQuaNewerGradesAwardsHandler,
    --                                  moduleName)

    friendChangedFunc = FriendData.FriendListChangedDelegate(FriendListChanged())
    FriendData.Instance:RegisterOnListChanged(friendChangedFunc)

    roomUserHolder = {}
end

function SetGameStartBuf(buf)
    gameStartBuf = buf
end

function ContinueJoinGame()
    print("1927 - <QualifyingNewer> ContinueJoinGame called")
    local buf = gameStartBuf
    if buf == nil then
        return
    end

    local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)

    if resp.type == "MT_PVP_3V3" then
        HandleNotifyGameStart(buf)
    elseif resp.type == "MT_REGULAR_RACE" then
        Regular1V1Handler.HandleNotifyGameStart(buf, true)
    elseif resp.type == "MT_QUALIFYING_NEW" then
        Qualifying.StartMatch(buf)
    end

    gameStartBuf = nil
end

function HandleNotifyGameStart(buf)
    print("1927 - <QualifyingNewer> HandleNotifyGameStart called")
    local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)
    if resp then
        if QualifyingNewerAI.isAI then
            CreateAIMatch()
            return
            -- else
            -- CreatePVPMatch(resp)
            -- return
        end

        local notifyGameStart = resp
        local myList = RoleInfoList.New()
        local isDataSwitch = true
        local TeamMatesNameList = StringList.New()
        local TeamScoreList = UintList.New()
        for k,v in pairs(notifyGameStart.data) do
            local gameRoleInfo = RoleInfo.New()
            for _,role in pairs(v.roles) do
                gameRoleInfo.id = role.id
                gameRoleInfo.fight_power = role.fight_power
                gameRoleInfo.acc_id = v.acc_id
                gameRoleInfo.star = role.star
                gameRoleInfo.quality = role.quality
                gameRoleInfo.level = role.level
                if v.acc_id == MainPlayer.Instance.AccountID then
                    isDataSwitch = false
                end
            end
            TeamMatesNameList:Add(v.name)
            print("1927 - <QualifyingNewer> GameStartNotify Team v.score=",v.score)
            TeamScoreList:Add(v.score)
            myList:Add( gameRoleInfo)
        end

        local rivalList = RoleInfoList.New()
        local RivalsNameList = StringList.New()
        local RivalsScoreList = UintList.New()
        for k,v in pairs(notifyGameStart.rival_data) do
            local gameRoleInfo = RoleInfo.New()
            for _,role in pairs(v.roles) do
                gameRoleInfo.id = role.id
                gameRoleInfo.fight_power = role.fight_power
                gameRoleInfo.acc_id = v.acc_id
                gameRoleInfo.star = role.star
                gameRoleInfo.quality = role.quality
                gameRoleInfo.level = role.level
            end
            print("1927 - <QualifyingNewer> GameStartNotify Rival v.score=",v.score)
            RivalsNameList:Add(v.name)
            RivalsScoreList:Add(v.score)
            rivalList:Add( gameRoleInfo)
        end

        if isDataSwitch then
            local t = TeamMatesNameList
            TeamMatesNameList = RivalsNameList
            RivalsNameList = t
            t = myList
            myList = rivalList
            rivalList = t
            t = TeamScoreList
            TeamScoreList = RivalsScoreList
            RivalsScoreList = t
        end

        local myRoles = myList
        local rivalRoles = rivalList

        --print("myRoles:",table.getn(myRoles),"rivalRoles:",table.getn(rivalRoles))
        if UIChallenge then
            UIChallenge.isStartMatch = false
        elseif UI1V1Plus then
            UI1V1Plus.isStartMatch = false
        end

        local qualifying_new  = notifyGameStart.qualifying_newer
        local session_id	= notifyGameStart.session_id
        local matchConfig = GameMatch.Config.New()

        matchConfig.leagueType	= GameMatch.LeagueType.eQualifyingNewer
        matchConfig.type		= GameMatch.Type.ePVP_3On3
        matchConfig.sceneId		= qualifying_new.scene_id
        matchConfig.MatchTime	= 180
        matchConfig.session_id	= session_id
        matchConfig.ip			= qualifying_new.game_ip
        matchConfig.port		= qualifying_new.game_port

        print("match id "..session_id)
        print("match server ip "..qualifying_new.game_ip)
        print("match server port "..qualifying_new.game_port)

        GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
        local goUILoading	= createUI("UIChallengeLoading_1")
        local uiLoading = goUILoading:GetComponent("UIChallengeLoading")
        uiLoading.pvp = true
        if uiLoading ~= nil   then

            if rivalRoles.Count >= 2 then
                uiLoading.single = false
            else
                uiLoading.single = true
            end
            uiLoading.scene_name = qualifying_new.scene_id
            uiLoading.my_role_name_list = TeamMatesNameList
            uiLoading.rival_name_list = RivalsNameList
            uiLoading.my_role_list = myRoles
            uiLoading.rival_list = rivalRoles
            uiLoading.my_role_score_list = TeamScoreList
            uiLoading.rival_score_list = RivalsScoreList
            uiLoading:Refresh(false)

            NGUITools.BringForward(goUILoading)
        end
    end
end



function CreateAIMatch(self, resp)
    print("1927 - <QualifyingNewer> CreateAIMatch called")
    local notifyGameStart = resp
    local myList = RoleInfoList.New()
    local isDataSwitch = true
    local TeamMatesNameList = StringList.New()
    local TeamScoreList = UintList.New()
    for k,v in pairs(notifyGameStart.data) do
        local gameRoleInfo = RoleInfo.New()
        for _,role in pairs(v.roles) do
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
            if v.acc_id == MainPlayer.Instance.AccountID then
                isDataSwitch = false
            end
        end
        TeamMatesNameList:Add(v.name)
        print("1927 - <QualifyingNewer> GameStartNotify Team v.score=",v.score)
        TeamScoreList:Add(v.score)
        myList:Add( gameRoleInfo)
    end

    local rivalList = RoleInfoList.New()
    local RivalsNameList = StringList.New()
    local RivalsScoreList = UintList.New()
    for k,v in pairs(notifyGameStart.rival_data) do
        local gameRoleInfo = RoleInfo.New()
        for _,role in pairs(v.roles) do
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
        end
        print("1927 - <QualifyingNewer> GameStartNotify Rival v.score=",v.score)
        RivalsNameList:Add(v.name)
        RivalsScoreList:Add(v.score)
        rivalList:Add( gameRoleInfo)
    end

    if isDataSwitch then
        local t = TeamMatesNameList
        TeamMatesNameList = RivalsNameList
        RivalsNameList = t
        t = myList
        myList = rivalList
        rivalList = t
        t = TeamScoreList
        TeamScoreList = RivalsScoreList
        RivalsScoreList = t
    end

    local myRoles = myList
    local rivalRoles = rivalList

    --print("myRoles:",table.getn(myRoles),"rivalRoles:",table.getn(rivalRoles))
    if UIChallenge then
        UIChallenge.isStartMatch = false
    elseif UI1V1Plus then
        UI1V1Plus.isStartMatch = false
    end

    local qualifying_new  = notifyGameStart.qualifying_newer
    local session_id	= notifyGameStart.session_id
    local matchConfig = GameMatch.Config.New()

    matchConfig.leagueType	= GameMatch.LeagueType.eQualifyingNewer
    matchConfig.type		= GameMatch.Type.ePVP_3On3
    matchConfig.sceneId		= qualifying_new.scene_id
    matchConfig.MatchTime	= 180
    matchConfig.session_id	= session_id
    matchConfig.ip			= qualifying_new.game_ip
    matchConfig.port		= qualifying_new.game_port

    print("match id "..session_id)
    print("match server ip "..qualifying_new.game_ip)
    print("match server port "..qualifying_new.game_port)

    GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
    local goUILoading	= createUI("UIChallengeLoading_1")
    local uiLoading = goUILoading:GetComponent("UIChallengeLoading")
    uiLoading.pvp = true
    if uiLoading ~= nil   then

        if rivalRoles.Count >= 2 then
            uiLoading.single = false
        else
            uiLoading.single = true
        end
        uiLoading.scene_name = qualifying_new.scene_id
        uiLoading.my_role_name_list = TeamMatesNameList
        uiLoading.rival_name_list = RivalsNameList
        uiLoading.my_role_list = myRoles
        uiLoading.rival_list = rivalRoles
        uiLoading.my_role_score_list = TeamScoreList
        uiLoading.rival_score_list = RivalsScoreList
        uiLoading:Refresh(false)

        NGUITools.BringForward(goUILoading)
    end
end


function CreatePVPMatch(resp)
    local notifyGameStart = resp
    local myList = RoleInfoList.New()
    local isDataSwitch = true
    local TeamMatesNameList = StringList.New()
    local TeamScoreList = UintList.New()
    for k,v in pairs(notifyGameStart.data) do
        local gameRoleInfo = RoleInfo.New()
        for _,role in pairs(v.roles) do
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
            if v.acc_id == MainPlayer.Instance.AccountID then
                isDataSwitch = false
            end
        end
        TeamMatesNameList:Add(v.name)
        print("1927 - <QualifyingNewer> GameStartNotify Team v.score=",v.score)
        TeamScoreList:Add(v.score)
        myList:Add( gameRoleInfo)
    end

    local rivalList = RoleInfoList.New()
    local RivalsNameList = StringList.New()
    local RivalsScoreList = UintList.New()
    for k,v in pairs(notifyGameStart.rival_data) do
        local gameRoleInfo = RoleInfo.New()
        for _,role in pairs(v.roles) do
            gameRoleInfo.id = role.id
            gameRoleInfo.fight_power = role.fight_power
            gameRoleInfo.acc_id = v.acc_id
            gameRoleInfo.star = role.star
            gameRoleInfo.quality = role.quality
            gameRoleInfo.level = role.level
        end
        print("1927 - <QualifyingNewer> GameStartNotify Rival v.score=",v.score)
        RivalsNameList:Add(v.name)
        RivalsScoreList:Add(v.score)
        rivalList:Add( gameRoleInfo)
    end

    if isDataSwitch then
        local t = TeamMatesNameList
        TeamMatesNameList = RivalsNameList
        RivalsNameList = t
        t = myList
        myList = rivalList
        rivalList = t
        t = TeamScoreList
        TeamScoreList = RivalsScoreList
        RivalsScoreList = t
    end

    local myRoles = myList
    local rivalRoles = rivalList

    --print("myRoles:",table.getn(myRoles),"rivalRoles:",table.getn(rivalRoles))
    if UIChallenge then
        UIChallenge.isStartMatch = false
    elseif UI1V1Plus then
        UI1V1Plus.isStartMatch = false
    end

    local qualifying_new  = notifyGameStart.qualifying_newer
    local session_id	= notifyGameStart.session_id
    local matchConfig = GameMatch.Config.New()

    matchConfig.leagueType	= GameMatch.LeagueType.eQualifyingNewer
    matchConfig.type		= GameMatch.Type.ePVP_3On3
    matchConfig.sceneId		= qualifying_new.scene_id
    matchConfig.MatchTime	= 180
    matchConfig.session_id	= session_id
    matchConfig.ip			= qualifying_new.game_ip
    matchConfig.port		= qualifying_new.game_port

    print("match id "..session_id)
    print("match server ip "..qualifying_new.game_ip)
    print("match server port "..qualifying_new.game_port)

    GameSystem.Instance.mClient:CreateNewMatch(matchConfig)
    local goUILoading	= createUI("UIChallengeLoading_1")
    local uiLoading = goUILoading:GetComponent("UIChallengeLoading")
    uiLoading.pvp = true
    if uiLoading ~= nil   then

        if rivalRoles.Count >= 2 then
            uiLoading.single = false
        else
            uiLoading.single = true
        end
        uiLoading.scene_name = qualifying_new.scene_id
        uiLoading.my_role_name_list = TeamMatesNameList
        uiLoading.rival_name_list = RivalsNameList
        uiLoading.my_role_list = myRoles
        uiLoading.rival_list = rivalRoles
        uiLoading.my_role_score_list = TeamScoreList
        uiLoading.rival_score_list = RivalsScoreList
        uiLoading:Refresh(false)

        NGUITools.BringForward(goUILoading)
    end
end


function GetWinNumByLeagueInfo(leagueInfo)
    local winNum = 0
    local enum = leagueInfo:GetEnumerator()
    while enum:MoveNext() do
        local v = enum.Current
        if v == 1 then
            winNum = winNum + 1
        end
    end
    return winNum
end

function NotifyQualifyingNewerLeagueResetHandler(buf)
    print("1927 - <QualifyingNewer> QualifyingNewerLeagueResetHandler")
    MainPlayer.Instance.QualifyingNewerInfo.league_info:Clear()
    MainPlayer.Instance.QualifyingNewerInfo.league_awards_flag = 0

    print("1927 - <QualifyingNewer>  uiQualifyingNewer=",uiQualifyingNewer)

    if uiQualifyingNewer then
        uiQualifyingNewer:Refresh()
    end
end


function RoomUserHandler(buf)
    print("1927 - <QualifyingNewer> RoomUserHanlder called")
    local resp, error = protobuf.decode('fogs.proto.msg.NotifyRoomUser',buf)
    if not resp then
        warning("NotifyRoomUser decode erorr!")
        return
    end

    print("1927 - <QualifyingNewer>  resp.match_type=",resp.match_type)
    print("1927 - <QualifyingNewer>  matchType=",matchType)

    if resp.match_type ~= matchType then
        return
    end

    print("1927 - <QualifyingNewer>  resp.flag=",resp.flag)
    if resp.flag == 1 then
        print("1927 - <QualifyingNewer> Goto UIQualifyingNewer")
        print("1927 - <QualifyingNewer>  isWinShowIncStarAnim=",isWinShowIncStarAnim)
        inBackToLadder = false
        local params = {
            showIncStarAnim = isWinShowIncStarAnim
        }
        TopPanelManager:ShowPanel("UIQualifyingNewer", nil, params)
        isWinShowIncStarAnim = false
        return
    end

    if resp.master then
        print("1927 - <QualifyingNewer> RoomUserHandler resp.master=",resp.master)
        roomUserMasterIdHolder = resp.master
    end

    local handlerOk = false
    if onRoomUserHandler then
        handlerOk = onRoomUserHandler(resp.users, roomUserMasterIdHolder)
    end

    -- log
    print("1927 - <QualifyingNewer>  onRoomUserHandler=",onRoomUserHandler)
    for k, v in pairs(resp.users) do
        print("1927 - <QualifyingNewer> RoomUserHandler v.acc_id, v.name=",v.acc_id, v.name)
    end

    -- 排位赛界面还不能接受数据
    if not hanlderOk then
        print("1927 - <QualifyingNewer> insert into roomUserHolder")
        table.insert(roomUserHolder, resp.users)
        roomUserMasterIdHolder = resp.master
    end

    if inBackToLadder then
        JoinLadder()
        -- backToLadderStep = backToLadderStep + 1
        -- CheckBackToLadder()
    end
end


--------------------------------------------------------------------------------
-- Function Name : JoinLadder
-- Create Time   : Fri May 20 17:41:40 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 加入天梯界面
--------------------------------------------------------------------------------
function JoinLadder()
    print("1927 - <QualifyingNewer> JoingLadder is called")
    if isJoinLadder then
        print("1927 - <QualifyingNewer> Returan for isJoinLadder if true")
        return
    end

    isJoinLadder = true

    -- 发送好友列表更新.
    FriendData.Instance:SendUpdateFriendList()

end





--------------------------------------------------------------------------------
-- Function Name : PrepareBackToQualifyingNewer
-- Create Time   : Thu May 12 14:51:19 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 结算界面通知返回排位赛界面要做的准备.
--------------------------------------------------------------------------------
function PrepareBackToQualifyingNewer(isWin)
    print("1927 - <QualifyingNewer> PrepareBackToQualifyingNewer Called")
    if inBackToLadder then
        print("1927 - <QualifyingNewer> Return for inBackToLadder=",inBackToLadder)
        return
    end

    inBackToLadder = true
    isWinShowIncStarAnim = isWin
    -- backToLadderStep = 0
    roomUserHolder = {}
    roomUserMasterIdHolder = 0

    -- 发送好友列表更新.
    -- FriendData.Instance:SendUpdateFriendList()

    -- 请求房间信息.
    local operation = {
        match_type = matchType
    }
    local req = protobuf.encode("fogs.proto.msg.RoomInfoReq", operation)
    LuaHelper.SendPlatMsgFromLua(MsgID.RoomInfoReqID,req)
end

--------------------------------------------------------------------------------
-- Function Name : FriendListChanged
-- Create Time   : Thu May 12 15:04:25 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 好友列表更新回调.
--------------------------------------------------------------------------------
function FriendListChanged()
    return function()
        if inBackToLadder and isJoinLadder then
            -- backToLadderStep = backToLadderStep + 1
            -- CheckBackToLadder()
            local nextShowUI = "UIQLadder"
            local nextShowUIParams = {
                nextShowUI = "UIQualifyingNewer"
            }

            jumpToUI(nextShowUI, nil, nextShowUIParams)
            isJoinLadder = false
            return
        end

        if isJoinLadder then
            local operation = {
                acc_id = MainPlayer.Instance.AccountID,
                type = matchType
            }
            local req = protobuf.encode("fogs.proto.msg.CreateRoomReq",operation)
            LuaHelper.SendPlatMsgFromLua(MsgID.CreateRoomReqID,req)
            LuaHelper.RegisterPlatMsgHandler(MsgID.CreateRoomRespID, HandleCreateRoom(), moduleName)
            print("1927 - Send CreateRoomReq for ladder.")
            CommonFunction.ShowWait()

            isJoinLadder = false
        end
    end
end


--------------------------------------------------------------------------------
-- Function Name : HandleCreatRoom
-- Create Time   : Fri May 20 17:48:57 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 接受创建房间消息回复
--------------------------------------------------------------------------------
function HandleCreateRoom()
    return function(buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.CreateRoomRespID, moduleName)
        CommonFunction.StopWait()
        local resp, error = protobuf.decode('fogs.proto.msg.CreateRoomResp',buf)
        print("1927 - CreateRoomResp resp.result=",resp.result)

        if resp then
            local type = resp.type
            if type ~= matchType then
                print("1927 - <QualifyingNewer> Type Not Match return type=",type)
                return
            end

            if resp.result == 0 then
                local accId = resp.acc_id
                local roomInfo = resp.info
                local userInfos = roomInfo.user_info
                for i=1, 3 do
                    local v = userInfos[i]
                end

                local nextShowUI = "UIQLadder"
                local nextShowUIParams = {	joinType="active",
                                        userInfo = userInfos,
                                        isMaster = true,
                                        nextShowUI = "UIQualifyingNewer",
                                        matchType = matchType
                }

                TopPanelManager:ShowPanel(nextShowUI, nil, nextShowUIParams)

            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end


--------------------------------------------------------------------------------
-- Function Name : CheckBackToLadder
-- Create Time   : Thu May 12 15:12:28 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 检查是否满足返回天梯的条件, 如果满足则返回.
--------------------------------------------------------------------------------
function CheckBackToLadder()
    print("1927 - <QualifyingNewer> CheckBackToLadder backToLadderStep=",backToLadderStep)
    if backToLadderStep ~= 2 then
        return
    end
    jumpToUI("UILadder", nil, nil)
end



--------------------------------------------------------------------------------
-- Function Name : SendGetQuaNewerGradeAwards
-- Create Time   : Tue May 24 11:00:03 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : Send to Server to Get the GradesAwardsReq
--------------------------------------------------------------------------------
function QualifyingNewer:SendGetQuaNewerGradeAwards()
    local operation = {}
    local req = protobuf.encode("fogs.proto.msg.GetQualifyingNewerGradesAwardsReq", operation)
    LuaHelper.SendPlatMsgFromLua(MsgID.GetLadderLeagueAwardsReqID,req)
end


--------------------------------------------------------------------------------
-- Function Name : GetQuaNewerGradesAwardsHandler
-- Create Time   : Tue May 24 11:06:18 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : Get the Response from Server.
--------------------------------------------------------------------------------
function QualifyingNewer:GetQuaNewerGradesAwardsHandler(buf)
    local resp, error = protobuf.decode('fogs.proto.msg.GetQualifyingNewerGradesAwardsResp',buf)
    local result = resp.result
    print("1927 - <QualifyingNewer>  result=",result)

    if result ~= 0 then
        return
    end

    -- MainPlayer.Instance.QualifyingNewerInfo.grade_awards_flag = 0

    if onGetGradesAwards then
        onGetGradesAwards(resp.awards)
    end
end




Init()

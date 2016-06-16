local moduleName="Ladder"

module(moduleName, package.seeall)

gameStartBuf      = nil         -- 开始比赛所用的buffer.
onRoomUserHandler = nil         -- 用于UI回调
roomUserHolder    = nil
roomUserMasterIdHolder  = 0     -- 房主Id
inBackToLadder    = false       -- 返回天梯标识
backToLadderStep  = 0           -- 返回天梯步骤.
friendChangedFunc = nil         -- 好友列表刷新回调处理过程
uiLadder = nil


matchType = "MT_PVP_3V3"
isJoinLadder = false
lastAverTime = 0                -- 上次匹配时间


PS =
{
    NONE		= 0, --NONE
    NORMAL		= 1, --正常
    MATCH		= 2, --比赛匹配中
    GAME		= 3, --比赛中
    LOGOUT		= 4, --登出
    OFFLINE		= 5, --离线
    ROOM		= 8, --房间内
    CREATEGAME	= 9, --创建比赛中
    RECHARGE	= 10, --充值
    READYGAME	= 11, --准备进比赛中
    SELECTROLE	= 12, --选择球员进行比赛中
}

function Init()
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyLadderLeagueResetID,
                                     NotifyLadderLeagueResetHandler,
                                     moduleName)

    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyRoomUserID,
                                     RoomUserHandler,
                                     moduleName)

    friendChangedFunc = FriendData.FriendListChangedDelegate(FriendListChanged())
    FriendData.Instance:RegisterOnListChanged(friendChangedFunc)

    roomUserHolder = {}
end

function SetGameStartBuf(buf)
    gameStartBuf = buf
end

function ContinueJoinGame()
    print("1927 - <Ladder> ContinueJoinGame called")
    local buf = gameStartBuf
    if buf == nil then
        return
    end

    local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)

    print("1927 - <Ladder>  resp.type=",resp.type)
    if resp.type == "MT_PVP_3V3" then
        HandleNotifyGameStart(buf)
    elseif resp.type == "MT_REGULAR_RACE" then
        Regular1V1Handler.HandleNotifyGameStart(buf, true)
    elseif resp.type == "MT_QUALIFYING_NEW" then
        Qualifying.StartMatch(buf)
    elseif resp.type == "MT_QUALIFYING_NEWER" then
        QualifyingNewer.HandleNotifyGameStart(buf)
    end

    gameStartBuf = nil
end

function HandleNotifyGameStart(buf)
    print("1927 - <Ladder> HandleNotifyGameStart called")
    local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)
    if resp then
        local notifyGameStart = resp
        local myList = RoleInfoList.New()
        local isDataSwitch = true
        local TeamMatesNameList = StringList.New()
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
            myList:Add( gameRoleInfo)
        end

        local rivalList = RoleInfoList.New()
        local RivalsNameList = StringList.New()
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
            RivalsNameList:Add(v.name)
            rivalList:Add( gameRoleInfo)
        end

        if isDataSwitch then
            local t = TeamMatesNameList
            TeamMatesNameList = RivalsNameList
            RivalsNameList = t
            t = myList
            myList = rivalList
            rivalList = t
        end

        local myRoles = myList
        local rivalRoles = rivalList

        --print("myRoles:",table.getn(myRoles),"rivalRoles:",table.getn(rivalRoles))
        if UIChallenge then
            UIChallenge.isStartMatch = false
        elseif UI1V1Plus then
            UI1V1Plus.isStartMatch = false
        end

        local challenge  = notifyGameStart.challenge_ex
        local session_id	= notifyGameStart.session_id
        local matchConfig = GameMatch.Config.New()

        matchConfig.leagueType	= GameMatch.LeagueType.ePVP
        matchConfig.type		= GameMatch.Type.ePVP_3On3
        matchConfig.sceneId		= challenge.scene_id
        matchConfig.MatchTime	= 180
        matchConfig.session_id	= session_id
        matchConfig.ip			= challenge.game_ip
        matchConfig.port		= challenge.game_port

        print("match id "..session_id)
        print("match server ip "..challenge.game_ip)
        print("match server port "..challenge.game_port)

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
            uiLoading.scene_name = challenge.scene_id
            uiLoading.my_role_name_list = TeamMatesNameList
            uiLoading.rival_name_list = RivalsNameList
            uiLoading.my_role_list = myRoles
            uiLoading.rival_list = rivalRoles
            uiLoading:Refresh(false)

            NGUITools.BringForward(goUILoading)
        end
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

function NotifyLadderLeagueResetHandler(buf)
    print("1927 - <Ladder> NotifyLadderLeagueResetHandler")
    MainPlayer.Instance.pvpLadderInfo.league_info:Clear()
    MainPlayer.Instance.pvpLadderInfo.league_awards_flag = 0
    if uiLadder then
        uiLadder:DataRefresh()
    end
end


function RoomUserHandler(buf)
    print("1927 - <Ladder> RoomUserHanlder called")
    local resp, error = protobuf.decode('fogs.proto.msg.NotifyRoomUser',buf)
    if not resp then
        warning("NotifyRoomUser decode erorr!")
        return
    end

    print("1927 - <Ladder>  resp.match_type=",resp.match_type)
    if resp.match_type ~= "MT_PVP_3V3" then
        return
    end

    print("1927 - <Ladder>  resp.flag=",resp.flag)
    if resp.flag == 1 then
        print ('@@a22222222222')
        jumpToUI("UIPracticeCourt1", nil, nil)
        return
    end


    print("1927 - <Ladder>  #resp.users=",#resp.users)
    for k, v in pairs(resp.users) do
        print("1927 - <Ladder>  k, v, v.acc_id, v.name=",k, v, v.acc_id, v.name)
    end


    if resp.master then
        print("1927 - <Ladder> RoomUserHandler resp.master=",resp.master)
        roomUserMasterIdHolder = resp.master
    end


    local handlerOk = false
    if onRoomUserHandler then
        handlerOk = onRoomUserHandler(resp.users, roomUserMasterIdHolder)
    end

    -- log
    print("1927 - <Ladder>  onRoomUserHandler=",onRoomUserHandler)
    for k, v in pairs(resp.users) do
        print("1927 - <Ladder> RoomUserHandler v.acc_id, v.name=",v.acc_id, v.name)
    end

    -- 天梯界面还不能接受数据
    if not hanlderOk then
        print("1927 - <Ladder> insert into roomUserHolder")
        table.insert(roomUserHolder, resp.users)
        roomUserMasterIdHolder = resp.master
    end

    -- log
    -- for i=1, 3 do
    --     print("1927 - <Ladder> i, roomUserHolder[i]=",i, roomUserHolder[i])
    --     print("1927 - <Ladder>  roomUserHolder[i].acc_id=",roomUserHolder[i].acc_id)
    -- end

    if inBackToLadder then
        JoinLadder()
        -- backToLadderStep = backToLadderStep + 1
        -- CheckBackToLadder()
    end
end



function JoinLadder()
    print("1927 - <Ladder> JointLadder is called")
    if isJoinLadder then
        print("1927 - <Ladder> Return for isJoinLadder is true")
    end

    isJoinLadder = true
    FriendData.Instance:SendUpdateFriendList()
end


--------------------------------------------------------------------------------
-- Function Name : PrepareBackToLadder
-- Create Time   : Thu May 12 14:51:19 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : 结算界面通知返回天梯界面要做的准备.
--------------------------------------------------------------------------------
function Ladder:PrepareBackToLadder()
    print("1927 - <Ladder> PrepareBackToLadder Called")
    if inBackToLadder then
        print("1927 - <Ladder> Return for inBackToLadder=",inBackToLadder)
        return
    end

    inBackToLadder = true
    backToLadderStep = 0
    roomUserHolder = {}
    roomUserMasterIdHolder = 0

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
function Ladder:FriendListChanged()
    return function()
        print("1927 - <Ladder> FirendListChanged inBackToLadder, isJoinLadder=",inBackToLadder, isJoinLadder)
        if inBackToLadder and isJoinLadder then
            local nextShowUI = "UIQLadder"
            local nextShowUIParams = {
                nextShowUI = "UIPracticeCourt1",
                matchType = matchType
            }

            jumpToUI(nextShowUI, nil, nextShowUIParams)

            isJoinLadder = false
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



function HandleCreateRoom()
    return function(buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.CreateRoomRespID, moduleName)
        CommonFunction.StopWait()
        local resp, error = protobuf.decode('fogs.proto.msg.CreateRoomResp',buf)
        print("1927 - CreateRoomResp resp.result=",resp.result)

        if resp then
            local type = resp.type
            if type ~= matchType then
                print("1927 - <Ladder> Type Not match Return type=",type)
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
                                        nextShowUI = "UIPracticeCourt1",
                                        matchType = matchType,

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
function Ladder:CheckBackToLadder()
    print("1927 - <Ladder> CheckBackToLadder backToLadderStep=",backToLadderStep)
    if backToLadderStep ~= 2 then
        return
    end
    jumpToUI("UILadder", nil, nil)
end

function SendExitRoom()
    print("1927 - <Ladder> Send ExitRoom called")
    local operation = {
        acc_id = MainPlayer.Instance.AccountID,
        type = matchType,
    }
    if uiLadder then
        uiLadder:Register(false)
    else
        QualifyingNewer.uiLadder:Register(false)
    end
    local req = protobuf.encode("fogs.proto.msg.ExitRoomReq",operation)
    LuaHelper.SendPlatMsgFromLua(MsgID.ExitRoomReqID,req)
end


-- Check the network state,  if ok then return true, else return false.
-- Param okDel to hanlde if player choose ok.
-- Param cancelDel to handle if player choose cancel.
function CheckNetState(okDel, cancelDel)
    local latency = GameSystem.Instance.mNetworkManager.m_platConn.m_profiler.m_avgLatency * 1000
    local yellow = tonumber(GameSystem.Instance.CommonConfig:GetString("gNetStateYellow"))

    if latency < yellow then
        return true
    end

    CommonFunction.ShowPopupMsg(getCommonStr("STR_NETWARNING"),
                                nil,
                                okDel,
                                cancelDel,
                                getCommonStr("STR_CONTINUE_PLAY"),
                                getCommonStr("BUTTON_CANCEL")
    )
    return false
end

Init()

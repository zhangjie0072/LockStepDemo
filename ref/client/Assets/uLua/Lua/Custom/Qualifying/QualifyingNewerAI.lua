local moduleName="QualifyingNewerAI"
module(moduleName, package.seeall)

isAI = false
selectRoleNum = 0
readyRoleNum = 0
ui = nil
prepareStep = 0

prepareActionDel = nil
isSelectConfirm = false
teamGameMode = nil
enemyGameMode = nil
teamNPC = nil
enemyNPC = nil
myRoleId = 0
enemyGameModeId = 0
randomMin = 0                   -- for random AI select
randoMax = 0                    -- for random AI Select.
session = 0
nameList = nil
nameCount = 0
aName = nil
teamNames = nil
enemyNames = nil
matchType = nil


function Init()
    -- nameList =  {}
    -- randomMin =
end

function StartAIMatch(u, sess)
    print("1927 - <QualifyingNewerAI> StartAIMatch isAI=",isAI)
    isAI = true
    selectRoleNum = 0
    readyRoleNum = 0
    ui = u
    prepareStep = 0
    prepareActionDel=PrepareAction()
    isSelectConfirm = false
    myRoleId = 0
    -- enemyGameModeId = 0
    session = sess

    local preRoomInfo = ChatDataCenter.GetChatContentsByRoomId(0)
    print("1927 - <QualifyingNewerAI>  preRoomInfo=",preRoomInfo)
    if preRoomInfo == nil then
        preRoomInfo = {
            roomId = 0,
            matchType = matchType,
            chatContents = {},
        }
        table.insert(ChatDataCenter._chatRoomInfos,preRoomInfo)
    else
        preRoomInfo.chatContents = {}
        preRoomInfo.matchType = matchType
    end


    Scheduler.Instance:AddTimer(GenRandom(), false, prepareActionDel)
end

function CreateNPC(t, grade)
    matchType = t
    print("1927 - <QualifyingNewerAI> CreateNPC matchType=",matchType)
    if matchType == "MT_PVP_3V3" then
        local modes = Split(GameSystem.Instance.CommonConfig:GetString("g3on3AITeam"), '&')
        local r = math.random(1, #modes)
        teamGameMode = GameSystem.Instance.GameModeConfig:GetGameMode(modes[r])

        modes = Split(GameSystem.Instance.CommonConfig:GetString("g3on3AIEnemy"), '&')
        r = math.random(1, #modes)
        enemyGameMode = GameSystem.Instance.GameModeConfig:GetGameMode(modes[r])
        enemyGameModeId = modes[r]
    end

    if matchType == "MT_QUALIFYING_NEWER" then
        -- Get GameMode.
        local qScore = MainPlayer.Instance.QualifyingNewerScore
        local qGrade = GameSystem.Instance.qualifyingNewerConfig:GetFirstSubGrade(grade)

        print("1927 - <QualifyingNewerAI>  qScore, qGrade=",qScore, qGrade)
        print("1927 - <QualifyingNewerAI>  qGrade.team_ai, qGrade.enemy_ai=",qGrade.team_ai, qGrade.enemy_ai)
        teamGameMode = GameSystem.Instance.GameModeConfig:GetGameMode(qGrade.team_ai)
        enemyGameMode = GameSystem.Instance.GameModeConfig:GetGameMode(qGrade.enemy_ai)
        enemyGameModeId = qGrade.enemy_ai
    end

    -- Get NPC
    teamNPC = GenerateNPC(teamGameMode, 2)
    enemyNPC = GenerateNPC(enemyGameMode, 3)
    GenName()
    teamNames = {}
    enemyNames = {}
    for i = 1, 2 do
        teamNames[teamNPC[i].npc_id] = nameList[i]
    end

    for i = 1, 3 do
        enemyNames[enemyNPC[i].npc_id] = nameList[i+2]
    end
end

--------------------------------------------------------------------------------
-- Function Name : GenRandom
-- Create Time   : Mon Jun  6 10:49:17 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : Generate the random number for AI Choose
--------------------------------------------------------------------------------
function GenRandom()
    local wait = GameSystem.Instance.CommonConfig:GetString("gAIChooseWait")
    print("1927 - <QualifyingNewerAI>  wait=",wait)
    local waits = Split(wait, '&')
    randomMin = tonumber(waits[1])
    randomMax = tonumber(waits[2])

    print("1927 - <QualifyingNewerAI>  randomMin, randomMax=",randomMin, randomMax)

    local randValue = math.random(randomMin, randomMax)
    print("1927 - <QualifyingNewerAI>  randValue=",randValue)
    return randValue
end

function GenerateNPC(gameMode, num)
    print("1927 - <QualifyingNewerAI> GenerateNPC, gameMode=", gameMode)
    local t = {}
    local names = {}
    for i = 1, num do
        print("1927 - <QualifyingNewerAI>  i-1=",i-1)
        local  npcs = gameMode.unmappedNPC[i-1]
        if npcs then
            print("1927 - <QualifyingNewerAI>  npcs.Count=",npcs.Count)
            local index = math.random(0, npcs.Count - 1)
            print("1927 - <QualifyingNewerAI>  index=",index)
            local npcId = npcs:get_Item(index)
            print("1927 - <QualifyingNewerAI>  npcId=",npcId)
            local npcConfig = GameSystem.Instance.NPCConfigData:GetConfigData(npcId)
            print("1927 - <QualifyingNewerAI> isdddtest t=",t)
            table.insert(t, npcConfig)
        end
    end
    return t
end


function PrepareAction()
    return function()
        print("1927 - <QualifyingNewerAI>  prepareStep=",prepareStep)
        local acc_id={
            2, 3
        }
        local role_id = {
            teamNPC[1].shape,
            teamNPC[2].shape,
        }
        prepareStep = prepareStep + 1

        local flags= {
            1, 1, 2, 2
        }

        local pi =  (prepareStep + 1) % 2 + 1
        local accId  = acc_id[pi]
        local roleId = role_id[pi]
        local flag  = flags[prepareStep]

        print("1927 - <QualifyingNewerAI>  pi, accId, roleId, flag=",pi, accId, roleId, flag)

        SelectRole(accId, roleId, flag)

        if prepareStep < 4 then
            Scheduler.Instance:AddTimer(GenRandom(), false, prepareActionDel)
        else
            CheckNotifyGame()
        end
    end
end


function SelectRole(accId, roleId, flag, isMe)
    local index
    local name

    for i = 1, 3 do
        print("1927 - <QualifyingNewerAI>  ui.teamInfo[i].acc_id=",ui.teamInfo[i].acc_id)
        if ui.teamInfo[i].acc_id  == accId then
            index = i
            name = ui.teamInfo[i].name
            print("1927 - <QualifyingNewerAI>  name=",name)
            break

        end
    end

    if isMe and isSelectConfirm and flag ~= 2 then
        return
    end

    if roleId ~= 0 then
        ui:SelectOtherRoleIcon(index, roleId, flag, name,skillsInfo)
        if isMe then
            myRoleId = roleId
        end
    else
        ui:MakeOnOtherRoleIconClose(index, nil)
        if isMe then
            myRoleId = 0
        end
    end
end

function ClickSelectConfirm()
    print("1927 - <QualifyingNewerAI> ClickSelectConfirm called")
    isSelectConfirm = true
    SelectRole(MainPlayer.Instance.AccountID, myRoleId, 2, true)
    CheckNotifyGame()
end


--------------------------------------------------------------------------------
-- Function Name : CheckNotifyGame
-- Create Time   : Thu Jun  2 10:21:01 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : Check if can Handle Notify Game. If yes, do it.
--------------------------------------------------------------------------------
function CheckNotifyGame()
    if not isSelectConfirm or prepareStep < 4 then
        print("1927 - <QualifyingNewerAI> CheckNotifyGame failed")
        return
    end
    print("1927 - <QualifyingNewerAI> CheckNotifyGame Success and to handle")

    local squadInfo = MainPlayer.Instance.SquadInfo
    local teammates = UintList.New()
    local npcs = UintList.New()
    teammates:Add(myRoleId)
    print("1927 - <QualifyingNewerAI>  myRoleId=",myRoleId)

    for i = 1, 2 do
        teammates:Add(teamNPC[i].npc_id)
    end
    local gameModeId = enemyGameModeId
    print("1927 - <QualifyingNewerAI>  gameModeId=",gameModeId)

    local sessionId = session
    local leagueType = GameMatch.LeagueType.eQualifyingNewerAI
    if matchType == "MT_PVP_3V3" then
        leagueType = GameMatch.LeagueType.eLadderAI
    end

    for i = 1,3 do
        NGUITools.SetActive(ui.chatMsgPopItems[i].gameObject,false)
    end

    ui:VisibleModel(false)

    print("1927 - <QualifyingNewerAI>  sessionId=",sessionId)

    for i = 1, 3 do
        npcs:Add(enemyNPC[i].npc_id)
    end

    if ui.uiGameChatModule ~= nil then
        ui.uiGameChatModule:CloseChatModue()
    end

    ChatDataCenter.ClearChatRoomInfoDataByMatchType(matchType)

    -- npcRivalName = UICreateTeam.GenerateName()
    GameSystem.Instance.mClient:CreateNewMatch(gameModeId, sessionId, false, leagueType, teammates, npcs)
    isAI = false
end

--------------------------------------------------------------------------------
-- Function Name : GenName
-- Create Time   : Mon Jun  6 15:19:29 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : Generate Name for AI
--------------------------------------------------------------------------------
function GenName()
    print("1927 - <QualifyingNewerAI> GenName called")
    nameList =  {}



    if nameCount == 0 and  aName == nil then
        nameCount = GameSystem.Instance.AIConfig.names.Count

        aName = {}
        for i = 0 , nameCount - 1 do
            table.insert(aName, GameSystem.Instance.AIConfig.names:get_Item(i))
        end
    end
    print("1927 - <QualifyingNewerAI>  nameCount=",nameCount)
    local t = {}
    for i=1, nameCount do
        table.insert(t, i)
    end

    for i = 1, 5 do
        math.randomseed(os.time())
        local r = math.random(1, #t)
        local name = aName[t[r]]
        print("1927 - <QualifyingNewerAI> genName r, t[r], name=",r, t[r], name)

        table.insert(nameList, name)
        table.remove(t, r)
    end
end

function GetNPCName(index)
    index = tonumber(index)
    return nameList[index]
end

function ReceiveChatMsg(msg)
    local content = {
        content = msg,
        ogri_name = MainPlayer.Instance.Name,
        acc_id = MainPlayer.Instance.AccountID
    }

    local msgContent = MainPlayer.Instance.Name .. ":" .. msg
    ChatDataCenter.PushChatsRecord(0,content)
    if ChatDataCenter.ChatUpdateFunc ~= nil then
        ChatDataCenter.ChatUpdateFunc(0,msgContent)
    end

    if ChatDataCenter.ChatUpdateFuncForUISelectRole ~= nil then
        ChatDataCenter.ChatUpdateFuncForUISelectRole(MainPlayer.Instance.AccountID,msg)
    end
end



Init()

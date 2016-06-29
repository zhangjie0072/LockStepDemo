--项目公用函数
--公用静态变量
GlobalConstLua = GlobalConstLua or {
    MaxRoleLevel = 30,
}
function createUI(prefabName, transform)
    if not transform then
        if not UIManager.Instance.m_uiRootBasePanel then
            UIManager.Instance:CreateUIRoot(true)
        end
        transform = UIManager.Instance.m_uiRootBasePanel.transform
    end
    if UseAnimatorShade[prefabName] then
        CommonFunction.SetAnimatorShade(true)
    end

    return CommonFunction.InstantiateObject("Prefab/GUI/" .. prefabName, transform)
end

function getLuaComponent(gameObject)
    --print("prefabName:",gameObject.name)
    local luaCom = gameObject:GetComponent("LuaComponent")
    if not luaCom then
        error("getLuaComponent, GameObject: " .. gameObject.name .. " doesn't have LuaComponent")
        return
    end
    if not luaCom.table then
        error("getLuaComponent, GameObject: " .. gameObject.name .. " haven't awaked")
        return
    end
    return luaCom.table
end

function getComponentInChild(transform, childName, componentType)
    local child = transform:FindChild(childName)
    if child then
        return child:GetComponent(componentType)
    end
end

function getChildGameObject(transform, childName)
    local child = transform:FindChild(childName)
    if child then
        return child.gameObject
    end
end

function getQualityColor(quality)
    local colors = {
        Color.New(1.0,1.0,1.0,1.0),

        Color.New(0,0.65,0.26,1.0),
        Color.New(0,0.65,0.26,1.0),

        Color.New(0.05,0.61,0.85,1.0),
        Color.New(0.05,0.61,0.85,1.0),
        Color.New(0.05,0.61,0.85,1.0),


        Color.New(0.4,0.18,0.60,1.0),
        Color.New(0.4,0.18,0.60,1.0),
        Color.New(0.4,0.18,0.60,1.0),
        Color.New(0.4,0.18,0.60,1.0),

        Color.New(0.78,0.36,0.12,1.0),
        Color.New(0.78,0.36,0.12,1.0),
        Color.New(0.78,0.36,0.12,1.0),
        Color.New(0.78,0.36,0.12,1.0),
        Color.New(0.78,0.36,0.12,1.0),
    }
    return colors[quality]
end

function getQualityColorNew(quality)
    local colors = {
        Color.New(255/255, 255/255, 255/255, 1.0),
        Color.New(62/255, 255/255, 42/255, 1.0),
        Color.New(27/255, 110/255, 191/255, 1.0),
        Color.New(213/255, 0/255, 255/255, 1.0),
        Color.New(182/255, 99/255, 32/255, 1.0),
    }
    return colors[quality]
end

function playSound(sound)
    PlaySoundManager.PlaySoundOneShot("Audio/"..sound)
end

function addOnClick(gameObject, handler)
    if gameObject == nil then
        error("addOnClick, gameObject is nil, handler = "..tostring(handler))
        return
    end
    UIEventListener.Get(gameObject).onClick = UIEventListener.Get(gameObject).onClick + LuaHelper.VoidDelegate(handler)
end

function getCommonStr( str)
    return CommonFunction.GetConstString(str)
end

--倒计时
function getTimeStr(seconds)
    local h = math.floor(seconds / 3600)
    local m = math.floor(seconds % 3600 / 60)
    local s = math.floor(seconds % 60)
    return string.format("%02d:%02d:%02d", h, m, s)
end

function bringFrontZOrder(transform,z)
    local z_order = z or -500
    if transform then
        local position = transform.localPosition
        position.z = z_order
        transform.localPosition = position
    else
        error("transform is nil")
    end
end

function enumToInt(enumValue)
    -- TODO: 这个方法并不可靠
    return enumValue:GetHashCode()
end

function print(...)
    Util.Log(concatenate(...), tostring(debug.traceback("\n------ Lua Traceback ------", 2)))
end

function error(...)
    Util.LogError(concatenate(...), tostring(debug.traceback("\n------ Lua Traceback ------", 2)))
end

function warning(...)
    Util.LogWarning(concatenate(...), tostring(debug.traceback("\n------ Lua Traceback ------", 2)))
end

function concatenate(...)
    local str = ""
    for _, p in ipairs({...}) do
        str = str .. tostring(p) .. " "
    end
    if str == "" then str = "nil" end
    return str
end


function getPortraitAtlas(id)
    local at ='Atlas/icon/iconPortrait'
    if id < 1000 then
        at = at.."_3"
    elseif id >= 1000 and id < 1500 then

    elseif id <1800 then
        at=at..'_1'
    elseif id <2000 then
        at=at..'_2'
    else
        error('cannot getPortraitAtlas by id='..tostring(id))
    end
    return ResourceLoadManager.Instance:GetAtlas(at)

end

function getBustAtlas(id)
    local at ='Atlas/icon/iconBust'

    if id < 1000 then
        at = at .. "_3"
    elseif id >= 1000 and id < 1500 then

    elseif id <1800 then
        at=at..'_1'
    elseif id <2000 then
        at=at..'_2'
    else
        error('cannot getBustAtlas by id='..tostring(id))
    end
    return ResourceLoadManager.Instance:GetAtlas(at)

end

function printSimpleTable(t)
    -- body
    if type(t) ==  'table' then
        local length = 1
        for k,v in pairs(t) do
            if type(v) == 'userdata' then 
                    print('userdata-------------------k:',k,',v:',v)
            elseif type(v) == 'table' then
               printSimpleTable(v)
            elseif type(v) == 'number' or type(v) == 'string' then
                    print('ns-------------------k:'..k..',v:'..v)                
            else 
                print('-------------------unknown k ',k,',',v)
            end
            length = length +1
            if length > 100 then
                break
            end

        end
    else
        print('-------------------given data t is not a table but a '..type(t))
    end

end
function printTable(t, recursively, showMT, indent)
    local indent = indent or 0
    local indentStr = ""
    for i = 1, indent do indentStr = indentStr .. "	" end
    for k, v in pairs(t) do
        if type(v) == "table" and recursively then
            print(indentStr, tostring(k).."("..type(k):sub(1,1)..")", "=", "{")
            printTable(v, recursively, showMT, indent + 1)
            print(indentStr, "}")
        else
            print(indentStr, tostring(k).."("..type(k):sub(1,1)..")",
                "=", tostring(v).."("..type(v):sub(1,1)..")")
        end
    end
    if showMT and getmetatable(t) then
        print(indentStr, "METATABLE", "=", "{")
        printTable(getmetatable(t), recursively, showMT, indent + 1)
        print(indentStr, "}")
    end
end


function getQualitystr(index)
    local quality_strs = {'D-','D','D+','C-','C','C+','B-','B','B+','A-','A','A+','S-','S','S+'}
    return quality_strs[index]
end

function checkMatchUI(targetUI)
    local LTType = GameMatch.LeagueType
    if targetUI == "UICareer" or targetUI == LTType.eCareer then
        return true, "UICareer"
    elseif targetUI == "UITour" or targetUI == LTType.eTour then
        return true, "UITour"
    elseif targetUI == "UIShootGame" or targetUI == LTType.eShoot then
        return true, "UIShootGame"
    elseif targetUI == "UIPractice" or targetUI == LTType.ePractise then
        return true, "UIPractice"
    -- 下面elseif 解决截换练习模式时要截换场景卡顿的问题 modify by Conglin
    elseif targetUI == "UIPracticeCourt1" or targetUI == LTType.ePractise then
        return true, "UIPracticeCourt1"
    elseif targetUI == "UIPracticeCourt" or targetUI == LTType.ePractise or targetUI == LTType.ePractise1vs1 then
        return true, "UIPracticeCourt"
    elseif targetUI == "UI1V1Plus" or targetUI == LTType.ePVP then
        return true, "UI1V1Plus"
    elseif targetUI == "UIChallenge" or targetUI == LTType.ePVP then
        return true, "UIChallenge"
    elseif targetUI == "UIQualifying" or targetUI == LTType.eQualifying then
        return true, "UIQualifying"
    elseif targetUI == "UIQualifyingNew" or targetUI == LTType.eQualifyingNew then
        return true, "UIQualifyingNew"
    elseif targetUI == "UIQualifyingNewer" or targetUI == LTType.eQualifyingNewer then
        return true, "UIQualifyingNewer"
    elseif targetUI == "UIBullFight" or targetUI == LTType.eBullFight then
        return true, "UIBullFight"
    end
    return false, targetUI
end

function checkCommonUI(targetUI)
    local LTType = GameMatch.LeagueType
    if targetUI == "UIRole" then
        return true, "UIRole"
    elseif targetUI == "UISquad" then
        return true, "UISquad"
    elseif targetUI == "UIPackage" then
        return true, "UIPackage"
    elseif targetUI == "UIMember" then
        return true, "UIMember"
    elseif targetUI == "UIStore" then
        return true, "UIStore"
    elseif targetUI == "UISelectRole" then
        return true, "UISelectRole"
    elseif targetUI == "NewRoleDetail" then
        return true,"NewRoleDetail"
    elseif targetUI == "RoleAcquirePopupNew" then
        return true,"RoleAcquirePopupNew"
    end
    return false, targetUI
end

--这个函数的调用是在需要跳转场景的时候
function jumpToUI(targetUI, subID, params)

    -- print('@@jumpToUI targetUI='.. targetUI)

    local isMatchUI, targetUI = checkMatchUI(targetUI)
    Scene.targetUI = targetUI
    Scene.subID = subID
    Scene.params = params
    GameSystem.Instance.mClient:Reset()

    if isMatchUI then
        UIManager.Instance:JumpToUI(GlobalConst.SCENE_MATCH)
    else
        UIManager.Instance:JumpToUI(GlobalConst.SCENE_HALL)
    end
end

function bringNear(transform, deltaZ)
    deltaZ = deltaZ or -500
    local localPos = transform.localPosition
    localPos.z = localPos.z + deltaZ
    transform.localPosition = localPos
end

-- showErrMsg true
function validateFunc(funcName, showErrMsg)
    local showErrMsg = (showErrMsg == nil) or showErrMsg
    print('func name: ' .. funcName)

    if not GameSystem.Instance.FunctionConditionConfig:ValidateFunc(funcName) then
        if showErrMsg then
            CommonFunction.ShowPopupMsg(GameSystem.Instance.FunctionConditionConfig:GetFuncCondition(funcName).lockTip,nil,nil,nil,nil,nil)
        end
        return false
    end
    return true
end

function execGMCmd(cmd)
    local req = {
        commond_str = cmd,
    }
    local buf = protobuf.encode("fogs.proto.msg.GMCommondExcu", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.GMCommondExcuID, buf)
end

function instantiateBySquad(prefabName, parent)
    local squadInfo = MainPlayer.Instance.SquadInfo
    local enum = squadInfo:GetEnumerator()
    while enum:MoveNext() do
        if enum.Current.status == parent.status then --FS_MAIN, FS_ASSIST1, FS_ASSIST2
            if parent.transform then
                local child
                if parent.transform.childCount > 0 then
                    child = parent.transform:GetChild(0)
                end
                if child == nil then
                    child = createUI(prefabName, parent.transform)
                end
                local script = getLuaComponent(child)
                script.id = enum.Current.role_id
                if parent.rename then
                    local roles = GameSystem.Instance.roleGiftConfig:GetRoleGiftList(1)
                    if roles:Contains(script.id) then
                        parent.rename.transform.name = "RoleToAppear"
                    else
                        parent.rename.transform.name = enum.Current.role_id
                    end
                end
                script.status = enum.Current.status
                return script
            end
        end
    end
end


function GetTeamFight()
    local teamFight = 0
    local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
    while enum:MoveNext() do
        local roleId = enum.Current.role_id
        local attrData = MainPlayer.Instance:GetRoleAttrsByID(roleId)
        teamFight = teamFight + math.modf(attrData.fightingCapacity)
    end
    return teamFight
end

function HasExpGoods()
    if IsNil(StoreType.ST_EXP) == false then
        local storeList = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsDataList(enumToInt(StoreType.ST_EXP))
        enum = storeList:GetEnumerator()
        while enum:MoveNext() do
            if MainPlayer.Instance:GetGoodsCount(enum.Current.store_good_id) > 0 then
                return true
            end
        end
    end
    return false
end

function CanRoleLvUp(roleId)
    if HasExpGoods() then
        local roleInfo = MainPlayer.Instance:GetRole2(roleId)
        if roleInfo == nil then
            return false
        end
        local lv =  roleInfo.level
        local roleMaxLevel = GameSystem.Instance.CommonConfig:GetUInt("gPlayerMaxLevel")
        -- print('lv = ', lv)
        -- print('Level = ', MainPlayer.Instance.Level)
        if lv < MainPlayer.Instance.Level and lv < roleMaxLevel then
            return true
        end
    end
    return false
end

--called by EngineFramework (C#)
function OnLevelWasLoaded(level)
    -- TopPanelManager:Clear()
    AudioSettings.OnLevelWasLoaded(level)
end

function utf8strlen(str)
    local len = #str
    local left = len
    local cnt = 0
    local arr = {0,0xc0,0xe0,0xf0,0xf8,0xfc}
    while left ~= 0 do
        local tmp =string.byte(str,-left)
        local i = #arr
        while arr[i] do
            if tmp >= arr[i] then
                left = left - i
                break
            end
            i = i - 1
        end
        cnt = cnt + 1
    end
    return cnt
end

--注册proto
require "Custom/Common/RegisterProto"
--消息ID
require "Custom/Common/MsgID"
--顶层面板管理器
require "Custom/Common/TopPanelManager"
--声音设置
require "Custom/Common/AudioSettings"

-- require "Custom/Skill/SkillHandler"
require "Custom/Tattoo/TattooHandler"
require "Custom/Tour/TourHandler"
require "Custom/Qualifying/QualifyingHandler"
--任务回复消息处理
require "Custom/Common/TaskRespHandler"
TaskRespHandler.Regist()
--公告处理
require "Custom/Common/AnnounceHandler"
AnnounceHandler.Register()
--小红点刷新
require "Custom/Common/UpdateRedDotHandler"
UpdateRedDotHandler.Register()
--1V1常规赛
require "Custom/PVPCompetition/Regular1V1Handler"
--排行榜
require "Custom/Rank/RankList"

require "Custom/Common/Consume"
--好友
require "Custom/Friends/Friends"
require "Custom/Hall/LuaPlayerData"
require "Custom/Badge/BadgeCommon"

require "Custom/Common/ConditionallyActive"
require "Custom/Ladder/Ladder"

require "Custom/Qualifying/Qualifying"
require "Custom/Qualifying/QualifyingNewer"

-----------------------------------------
---        lua配置文件包含说明        ---
-----------------------------------------
require "Config/Res/ChatConstMsgConfigRes"
require "Config/IO/ChatConstMsgConfig"
--聊天数据
require "Custom/Chat/ChatDataCenter"
--成长引导

require "Custom/UITaskLevel/TaskLevelData"

--巡回赛
require "Custom/Qualifying/TourNewData"

--新秀试炼
require "Custom/Activity/NewComerTrialData"

require "Custom/Qualifying/QualifyingNewerAI"

-- 辅助功能开关
require "Custom/FunctionSwitch/FunctionSwitchData"
require "Custom/FunctionSwitch/FunctionSwitchConfig"



function assert(cond, ...)
    if not cond then
        print(...)
    end
end

--去掉命名空间
BadgeSlotStatus = fogs.proto.msg.BadgeSlotStatus
BadgeCategory = fogs.proto.msg.BadgeCG
GoodsCategory = fogs.proto.msg.GoodsCategory
GoodsQuality = fogs.proto.msg.GoodsQuality
GoodsProto = fogs.proto.msg.GoodsProto
PositionType = fogs.proto.msg.PositionType
TattooType = fogs.proto.msg.TattooType
TrainingState = fogs.proto.msg.TrainingState
TrainingInfo = fogs.proto.msg.TrainingInfo
ErrorID = fogs.proto.msg.ErrorID
FightRoleInfo = fogs.proto.msg.FightRoleInfo
FightRole = fogs.proto.msg.FightRole
FightStatus = fogs.proto.msg.FightStatus
MatchType = fogs.proto.msg.MatchType
StoreType = fogs.proto.msg.StoreType
vipOperType = fogs.proto.msg.vipOperType
RoleInfo = fogs.proto.msg.RoleInfo
RoleSimpleInfo = fogs.proto.msg.RoleSimpleInfo
TeamType = fogs.proto.msg.TeamType
GameMode = fogs.proto.msg.GameMode
GameModeInfo = fogs.proto.msg.GameModeInfo
PlayerData = fogs.proto.msg.PlayerData
EquipmentType = fogs.proto.msg.EquipmentType
EquipmentOperationType = fogs.proto.msg.EquipmentOperationType
EquipmentSlotID = fogs.proto.msg.EquipmentSlotID
--TaskType = fogs.proto.msg.TaskType
RankType = fogs.proto.msg.RankType
RankSubType = fogs.proto.msg.RankSubType
FashionOperationType = fogs.proto.msg.FashionOperationType
ChatChannelType = fogs.proto.msg.ChatChannelType
FriendOperationType = fogs.proto.msg.FriendOperationType

StoreGoodsInfo = {}

--动画调用遮罩界面
UseAnimatorShade = {
    ["UIPractice"]         = 1,
    ["UILottery"]          = 1,
    ["UICompetition"]      = 1,
    ["UISign"]             = 1,
    ["UIMail"]             = 1,
    -- ["UITask"]              = 1,
    ["UIHall"]             = 1,
    -- ["UICareer"]        = 1,
    ["LotteryResultPopup"] = 1,
    ["PopupMessage1"] = 1,
    ["UISystemSettings"] = 1,
    ["UINewSign"] = 1,
    ["UIPlayerBuyDiamondGoldHP"] = 1,
    ["FriendsInfo"] = 1,
    ["VipPopup"] = 1,
    ["NewRoleDetail"] = 1,
    ["SkillDetail"] = 1,
    ["FashionRole"] = 1,
    ["PackageSell"] = 1,
    ["UICareerSection"] = 1,
}

--玩家在线状态
PlayerStateOnLine =
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

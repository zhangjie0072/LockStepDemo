-- 辅助开关标记 获得、解析
local modleName = "FunctionSwitchData"

module(modleName, package.seeall)

fsTb ={}


print("FunctionSwitchData".."启动")
-- 检测当前功能是否可以可以打开
function CheckSwith(switch_strkey)
    -- print ("--------------")
    -- print (switch_strkey)
    -- print (fsTb[switch_strkey])
    -- print (type(fsTb[switch_strkey]))

    if fsTb[switch_strkey] == 0 then
        CommonFunction.ShowPopupMsg("功能正在维护！",nil,nil,nil,nil,nil)
        return false
    end

    return true
end

function Cancel(switch_strkey)
    if fsTb[switch_strkey] == 0 then return false end
    return true
end

-- 获得推送来的开关标记数据包
function HandlerFunctionSwitchResp()
    return function(buf)
        local resp, err = protobuf.decode("fogs.proto.msg.FunctionSwitchList", buf)
        CommonFunction.StopWait()
        if not resp then
            error(err)
            return
        end

        -- warning('function HandlerFunctionSwitchResp()')

        if resp then
            local t = resp.switches
            for i=1,#t do
                -- 临时开启所有开关
                -- t[i].opened = 1
                -------------------

                -- test
                -- if t[i].id == FSID.players_btn then
                --     t[i].opened = 0
                -- end

                fsTb[t[i].id] = t[i].opened
            end
        end
    end
end

-- 已经关闭了当前的功能点
-- function IsClosedFunctionMsg(buf)
--     local resp, err = protobuf.decode("fogs.proto.msg.FunctionClosedID", buf)
--     local buf = protobuf.encode("fogs.proto.msg.ChatRoomQueryLatestReq", req)
--     LuaHelper.SendPlatMsgFromLua(MsgID.ChatRoomLatestQueryReqID, buf)
--     LuaHelper.RegisterPlatMsgHandler(MsgID.ChatRoomQueryLatestRespID,ChatDataCenter.QueryLatestChatMsgForSeverHanlder,ChatDataCenter._modname)
--     if not resp then
--         error(err)
--         return
--     end

--     if resp and type(resp) == 'string' and resp.length > 0 then
--         return true
--     end
-- end

LuaHelper.RegisterPlatMsgHandler(MsgID.FunctionSwitchListID, HandlerFunctionSwitchResp(), modleName)
-- LuaHelper.RegisterPlatMsgHandler(MsgID.FunctionClosedID, HandlerFunctionSwitchResp(), modleName)
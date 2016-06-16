------------------------------------------------------------------------
-- module name    : ChatDataCenter
-- create time   : 2016-05-10 14:16:35
-- author        : Jackwu
-- description   : 聊天数据中心，目前只用于天梯赛
------------------------------------------------------------------------
print("ChatDataCenter".."启动")
ChatDataCenter = {
    _modname = "ChatDataCenter",
--=============================--
--==     公有变量分隔线      ==--
--=============================--
    TestRoomId = 11,
    ChatUpdateFunc = nil,--更新消息方法
    ChatUpdateFuncForUISelectRole = nil, -- 选人界面更新消息方法
    LadderChatModuleInfo = nil,--天梯赛聊天功能开启情况 结构={chatAble = true,roomId = 1123}
    ChatRoomIdUpdate = nil,--聊天室ID更新
--=============================--
--==     私有变量分隔线      ==--
--=============================--
    --聊天室信息
    ---结构如下
    -- {
    --	roomid ,所有的roomid不会重复
    --	matchtype,
    --	chatContents,
    -- }
    _chatRoomInfos = {},
}

--------------模块初始化------------
function ChatDataCenter.Initialize()

end

--------------接收消息--------------
function ChatDataCenter.RegisterMsgHanlder()
    -- print("聊天数据中心：侦听服务器的消息..doing!")
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyMatchChatRoomID, ChatDataCenter.NotifyMatchChatRoomHanlder, ChatDataCenter._modname)
    LuaHelper.RegisterPlatMsgHandler(MsgID.ChatRoomBroadcastID,ChatDataCenter.ReciveChatRoomBroadcastMessage,ChatDataCenter._modname)
end

--=============================--
--==     公有方法分隔线      ==--
--=============================--

------------------------------------
-- 方法作用：清除房间聊天数据
-- matchType: 比赛模式
-------------------------------------
function ChatDataCenter.ClearChatRoomInfoDataByMatchType(matchType)
    for k,v in pairs(ChatDataCenter._chatRoomInfos) do
        if v.matchType == matchType then
            table.remove(ChatDataCenter._chatRoomInfos,k)
        end
    end
    return nil
end

------------------------------------
-- 方法作用：保存聊天记录
-- roomid 聊天室roomid
-- chatmsg 聊天的消息
-------------------------------------
function ChatDataCenter.PushChatsRecord(roomid,chatmsg)
    -- local roomtable = ChatDataCenter._chatHistoryRecords['chatRoom'..roomid]
    local roomInfo = ChatDataCenter.GetChatContentsByRoomId(roomid)
    if roomInfo == nil then
        return
    end
    table.insert(roomInfo.chatContents,chatmsg)
end

------------------------------------
-- 方法作用：获取聊天记录
------------------------------------
function ChatDataCenter.GetLatestChatMsgData(roomid)
    local roomInfo = ChatDataCenter.GetChatContentsByRoomId(roomid)
    if roomInfo == nil then
        return nil
    end
    return roomInfo.chatContents
end

------------------------------------
-- 方法作用：log聊天记录(辅助功能)
-------------------------------------
function ChatDataCenter.PrintAllChatMessage(roomid)
    local roomInfo = ChatDataCenter.GetChatContentsByRoomId(roomid)
    if roomInfo == nil then
        print("roomid:"..roomid.."没有聊天消息")
        return
    end

    for _,v in pairs(roomInfo.chatContents) do
        print("【帐号id：】"..v.acc_id.."【帐号名称：】"..v.ogri_name.."【内容id：】"..v.content)
    end
end

------------------------------------
-- 方法作用：发送聊天信息
-- message 聊天的消息
-------------------------------------
function ChatDataCenter.SendChatMessage(roomid,message)
    --本地测试--
    -- print("发送聊天信息"..roomid.."message:"..message)
    -- if ChatDataCenter.ChatUpdateFuncForUISelectRole ~= nil then
    --  ChatDataCenter.ChatUpdateFuncForUISelectRole(1,message)
    -- end

    -- local preRoomInfo = ChatDataCenter.GetChatContentsByRoomId(ChatDataCenter.TestRoomId)
    -- if preRoomInfo == nil then
    --  preRoomInfo = {
    --      roomId = ChatDataCenter.TestRoomId,
    --      chatContents = {},
    --  }
    --  table.insert(ChatDataCenter._chatRoomInfos,preRoomInfo)
    -- end

    -- local cont = {content = message,acc_id = 12,ogri_name = "jackwu"}
    -- ChatDataCenter.PushChatsRecord(ChatDataCenter.TestRoomId,cont)

    -- if ChatDataCenter.ChatUpdateFunc ~= nil then
    --  ChatDataCenter.ChatUpdateFunc(ChatDataCenter.TestRoomId,"jackwu:"..message)
    --  ChatDataCenter.PrintAllChatMessage(ChatDataCenter.TestRoomId)
    --  return
    -- end
    --本地测试止线---
    local req = {
        room_id = roomid,
        content = message
    }

    local buf = protobuf.encode("fogs.proto.msg.ChatRoomDeliver" , req)
    LuaHelper.SendPlatMsgFromLua(MsgID.ChatRoomDeliverID, buf)
end

---------------------------------------
-- 方法作用: 从服务器去获取聊天历史记录
---------------------------------------
function ChatDataCenter.QueryLatestChatMsgForSever(roomid)
    print("ChatDataCenter.QueryLatestChatMsgForSever doing!")
    local req = {
        room_id  = roomid,
    }

    local buf = protobuf.encode("fogs.proto.msg.ChatRoomQueryLatestReq", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.ChatRoomLatestQueryReqID, buf)
    LuaHelper.RegisterPlatMsgHandler(MsgID.ChatRoomQueryLatestRespID,ChatDataCenter.QueryLatestChatMsgForSeverHanlder,ChatDataCenter._modname)
end


--=============================--
--==     私有方法分隔线      ==--
--=============================--

------------------------------------------
-- 方法作用: 匹配成功，聊天室开启,聊天室
-----------------------------------------
function ChatDataCenter.NotifyMatchChatRoomHanlder(buf)
    print("ChatDataCenter.NotifyMatchChatRoomHanlder")
    ChatDataCenter.LadderChatModuleInfo = nil
    local resp, err = protobuf.decode("fogs.proto.msg.NotifyMatchChatRoom", buf)
    if resp then
        -- print("匹配比赛后聊天功能开启:RoomID:"..resp.room_id.."MathTYpe:"..resp.match_type)
        local preRoomInfo = ChatDataCenter.GetChatContentsByRoomId(resp.room_id)
        if preRoomInfo == nil then
            preRoomInfo = {
                roomId = resp.room_id,
                matchType = resp.match_type,
                chatContents = {},
            }
            table.insert(ChatDataCenter._chatRoomInfos,preRoomInfo)
        else
            preRoomInfo.chatContents = {}
            preRoomInfo.matchType = resp.match_type
        end

        print("1927 - <ChatDataCenter>  resp.match_type=",resp.match_type)
        if resp.match_type == "MT_PVP_3V3" or resp.match_type == "MT_QUALIFYING_NEWER" then
            ChatDataCenter.LadderChatModuleInfo = {chatAble = true,roomId = resp.room_id}
        end
        ChatDataCenter.QueryLatestChatMsgForSever(resp.room_id)
        if ChatDataCenter.ChatRoomIdUpdate ~= nil then
            ChatDataCenter.ChatRoomIdUpdate(resp.room_id)
        end
    end
end

------------------------------------
-- 方法作用:查询最近聊天信息处理
-------------------------------------
function ChatDataCenter.QueryLatestChatMsgForSeverHanlder(buf)
    print("ChatDataCenter.QueryLatestChatMsgForSeverHanlder doing!")
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.ChatRoomQueryLatestRespID,ChatDataCenter._modname)
    local resp, err = protobuf.decode("fogs.proto.msg.ChatRoomQueryLatestResp", buf)
    if resp then
        if resp.result ~= 0 then
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            return
        end

        local rid = resp.room_id
        for k,v in pairs(resp.latest_contents) do
            ChatDataCenter.PushChatsRecord(rid,v)
        end
    end
end

------------------------------------
-- 方法作用:收到聊天信息（以广播的形式）
-------------------------------------
function ChatDataCenter.ReciveChatRoomBroadcastMessage(buf)
    print("ChatDataCenter.ReciveChatRoomBroadcastMessage doing!")
    local resp, err = protobuf.decode("fogs.proto.msg.ChatRoomBroadcast", buf)
    if resp then
        print("收到的聊天广播信息：RoomId:["..resp.room_id.."]..oric_name:"..resp.content.ogri_name.."msg:"..resp.content.content)
        local rid = resp.room_id

        -- --更新消息显示
        -- local matchType = ChatDataCenter.GetMatchTypeByRoomId(rid)
        -- --天梯赛
        -- if matchType == MatchType.MT_PVP_3V3 then
        --  if ChatDataCenter.ChatUpdateFunc ~= nil then
        --      ChatDataCenter.ChatUpdateFunc(resp.room_id,rsp.content)
        --  end
        -- end
        local msgContent = resp.content.ogri_name..":"..resp.content.content
        ChatDataCenter.PushChatsRecord(rid,resp.content)
        if ChatDataCenter.ChatUpdateFunc ~= nil then
            ChatDataCenter.ChatUpdateFunc(resp.room_id,msgContent)
        end

        if ChatDataCenter.ChatUpdateFuncForUISelectRole ~= nil then
            ChatDataCenter.ChatUpdateFuncForUISelectRole(resp.content.acc_id,resp.content.content)
        end
    end
end

------------------------------------
-- 方法作用:根据比赛类型获取
-------------------------------------
function ChatDataCenter.GetRoomIdByMatchType(matchType)
    for k,v in pairs(ChatDataCenter._chatRoomInfos) do
        if v.matchType == matchType then
            return v
        end
    end
    return nil
end

------------------------------------
-- 方法作用:根椐房间的id获取聊天信息table
-------------------------------------
function ChatDataCenter.GetChatContentsByRoomId(roomid)
    for k,v in pairs(ChatDataCenter._chatRoomInfos) do
        if v.roomId == roomid then
            return v
        end
    end
    return nil
end

----------------------------------------
-- 方法作用:根据roomid获取对应的比赛模式
----------------------------------------
function ChatDataCenter.GetMatchTypeByRoomId(roomid)
    for k,v in pairs(ChatDataCenter._chatRoomInfos) do
        if v.roomId == roomid then
            return v.matchType
        end
    end
    return nil
end


ChatDataCenter.Initialize()
ChatDataCenter.RegisterMsgHanlder()

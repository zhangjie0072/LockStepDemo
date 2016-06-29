local modname = "ChatCache"
module(modname, package.seeall)
FriendMsgList = {}
--当和一个玩家有了聊天信息后，消息变化直接通过消息回调来实现，但是没有创建聊天信息时，消息通知只能通过Notifyer来实现，创建消息后删除Notifyer
FriendMsgNotifyer = {}--好友消息通知回掉
--test
uichat = nil
local LogLevel = function () end--warning
function EmptyMsg( )
	-- body
	return #FriendMsgList <= 0
end
function testFriendMsg( ... )
	-- body
	local friends = Friends.FriendList
	for k,v in pairs(friends) do
		local num = math.random(0,10)
		if num >0 then 
			for i=1,num do				
				local msg = {
					type = ChatChannelType.CCT_PRIVATE,
					pos = 0,
					info = {
						content = 'test..'..math.random(0,1000),
						ogri_name = v.name,
						acc_id = v.acc_id,
					},
					time = UnityTime.realtimeSinceStartup*10
				}
				AddFriendMsg(msg)
			end

		end
	end
end
function testFriendMsg1( ... )
	-- body
	local friends = Friends.FriendList
	local count = math.random(0,2)
	for k,v in pairs(friends) do
		local num = math.random(0,1)
		if num >0 then 
			for i=1,num do				
				local msg = {
					type = ChatChannelType.CCT_PRIVATE,
					pos = 0,
					info = {
						content = 'test..'..math.random(0,1000),
						ogri_name = v.name,
						acc_id = v.acc_id,
					},
					time = UnityTime.realtimeSinceStartup*10
				}
				AddFriendMsg(msg)
				count = count -1
				if count<=0 then return end
			end

		end
	end
end
--notifyer 用于更新聊天好友界面的数据
function RemoveNotifyer( id  )
	-- body
	LogLevel('RemoveNotifyer '..id)
	for k,v in pairs(FriendMsgNotifyer) do
		if v.id == id then
			table.remove(FriendMsgNotifyer,k)
			return true
		end
	end
	--没有则删除消息列表中的
	for k,v in pairs(FriendMsgList) do
		if v.id == id and v.action ~= nil then 
			v.action = nil
		end
	end
end
function AddNotifyer( id,callBack )
	-- body
	--如果聊天信息有记录，直接加到好友信息内
	LogLevel('AddNotifyer ',id,',',callBack)
	for k,v in pairs(FriendMsgList) do
		if v.id == id then
			v.action = callBack
			return
		end
	end
	--如果聊天信息没有记录，加到通知表
	for k,v in pairs(FriendMsgNotifyer) do
		if v.id == id then 	--已存在更新
			local notifyer = {id,action,}
			notifyer.id = id
			notifyer.action = callBack
			v = notifyer
			return
		end
	end
	--都没有新添加
	local notifyer = {id,action,}
	notifyer.id = id
	notifyer.action = callBack
	table.insert(FriendMsgNotifyer,notifyer)
end
function AddFriendMsg(msg,myMsg)
	-- body
	-- local cell = {
	-- 	id,
	-- 	msgs = {}
	local addNew = true
	local firstK = nil	
	for k,v in pairs(FriendMsgList) do
		if firstK == nil then
			firstK = k
		end
		if v.id == msg.info.acc_id then
			--消息使用数组方式存储方便倒序读取
			v.msgs[#v.msgs+1] = msg
			if not myMsg then
				v.unread = v.unread + 1
				msg.pos = 0
			else
				msg.pos = 1
			end
			if v.unread>99 then v.unread = 99 end
			LogLevel('update Friend Msg for frd '..v.id..',unread '..v.unread,',name ',msg.info.ogri_name,',time',msg.time)
			--好友消息太多要删掉一部分保证内存不炸掉
			--保存最近50条聊天信息  (65消息长度*50+100)*100个好友 <0.4M好友聊天数据
			if #v.msgs > 99 then 
				table.remove(v.msgs,firstK)
			end
			if v.action ~= nil and not myMsg  then 
				v.action(v.unread)
			end
			addNew = false
			break
		end
	end
	if addNew then 
		local cell = {
			id,
			unread = 0,
			msgs = {},
			action = nil,--好友信息更新后的回掉
		}
		local id = msg.info.acc_id
		cell.id = id 
		if not myMsg then
			cell.unread = 1
			msg.pos = 0	--好友的消息
		else
			msg.pos = 1 --我的消息
		end
		local notifyer = nil
		for k,v in pairs(FriendMsgNotifyer) do
			if v.id == id then
				notifyer = v.action
				table.remove(FriendMsgNotifyer,k)
			end
		end
		cell.action = notifyer
		if cell.action ~= nil and not myMsg then 
			cell.action(1)
		end
		table.insert(cell.msgs,msg)
		table.insert(FriendMsgList,cell)
		LogLevel('addNew Friend Msg for frd '..id..',unread '..cell.unread,',name ',msg.info.ogri_name,',time',msg.time)
	end
	if uichat then 
		uichat:UpdateChatBar( ChatChannelType.CCT_PRIVATE )()
	end
	-- table.sort(FriendMsgList,SortFriendMsgOrder)

end
function ReadFriendMsg( id, all )
	-- body
	for k,v in pairs(FriendMsgList) do
		if v.id == id then
			if all then 
				v.unread = 0 
			else
				v.unread = v.unread - 1
			end
			if v.unread <0 then v.unread = 0 end
			return
		end		
	end

end
    --聊天好友列表显示规则 
    --[[
		有未查看密语信息的排列在前
		有过交流历史的排列在前
		没有密语信息时，友好度由高到低排列
    ]]
    
-- function SortFriendMsgOrder(M1,M2 )
-- 	-- body
-- 		if not M1 then return 1 end
-- 		if not M2 then return -1 end
-- 		if M1.unread ~= M2.unread then
-- 			return M1.unread > M2.unread
-- 		end
-- 		if #M1.msgs ~= #M2.msgs then 
-- 			return #M1.msgs > #M2.msgs
-- 		end
-- 		return 1
-- end
--根据好友ID来获取消息列表
function getFriendMsgById( id )
	-- body
	for k,v in pairs(FriendMsgList) do
		if v.id == id then 
			return v.msgs
		end
	end
end
function GetFriendMsgCountById( id )
	-- body
	for k,v in pairs(FriendMsgList) do
		if v.id == id then 
			return #v.msgs
		end
	end
	return 0
end
--获取最近的一条好友消息
function getLatestFriendMsg( ... )
	-- body
	local time = 0
	local latest = nil	
	for k,v in pairs(FriendMsgList) do
		
		local msg = v.msgs[#v.msgs]
		if msg.time > time then 
			latest = msg
			time = msg.time
		end

	end
	return latest
end
--获取未读消息长度 1好友 2联盟 3队伍
--如果传入了id则是取得某个好友的未读消息
function getUnreadCount( chatType,fid )
	-- body
	if chatType == 1 then 
		local unread = 0
		for k,v in pairs(FriendMsgList) do 
			if v.unread > #v.msgs then v.unread = #v.msgs end 	--可能会有缓存消息清掉但是未读状态未改变的情况
			if fid == v.id then 
				return v.unread
			end
			unread = unread + v.unread
		end
		if fid then return 0 end
		if unread > 99 then unread = 99 end
		return unread
	elseif chatType == 2 then
	elseif chatType == 3 then
	else
	end


end
function onFriendChatMessage( ... )
	-- body
	return function ( ... )
		-- body
		local msg = MainPlayer.Instance.FriendChatMessage
		if msg then 
			AddFriendMsg(msg)

		end

	end
end
-- function OnTeamChatMessage( ... )
-- 	-- body
-- end
-- function OnLeagueChatMessage( ... )
-- 	-- body
-- end
-- testFriendMsg()
-- Scheduler.Instance:AddTimer(15,true,testFriendMsg1)
MainPlayer.Instance.onFriendChatMessage = onFriendChatMessage()	--好友信息
-- MainPlayer.Instance.OnTeamChatMessage = OnTeamChatMessage()		--队伍信息
-- MainPlayer.Instance.OnLeagueChatMessage = OnLeagueChatMessage()	--联盟信息
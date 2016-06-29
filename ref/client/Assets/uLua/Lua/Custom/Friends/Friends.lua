local modname = "Friends"
FriendCache = require "Custom/Friends/FriendCache"
module(modname, package.seeall)

friendItemTip = nil  --好友选项菜单
FriendsInfoQueryLua = nil -- 好友请求界面
FriendsInfoCaches = {}
FriendList = {} -- 好友列表
AssistFriendFirstWin = 0	--协助好友拿首胜剩余次数
FriendInfoChangeNotifyer= {}	--好友信息变化的回掉列表
FriendInfoChangeEvent = nil		--好友信息变化更新好友界面
setmetatable(FriendsInfoCaches, {__mode = "kv"})--弱引用
function AddFriendNotifyer( id ,callback )
	-- body
	if IsFriend(id) then
		if callback then
			local addNew = true
			for k,v in pairs(FriendInfoChangeNotifyer) do
				if v.id == id then
					for _,notifyer in pairs(v.notifyers) do
						if notifyer == callback then
							warning('Try to readd same callback to id  ',id)
							return false
						else
							addNew = false
							v.notifyers[#v.notifyers+1] = callback
						end
					end
				else
				end
			end
			if addNew then
				local notifyer = {id,notifyers={},}
				notifyer.id = id
				notifyer.notifyers[1] = callback
				table.insert(FriendInfoChangeNotifyer,notifyer)
				return true
			end
		else
			warning('Try to AddFriendNotifyer with nil callback for ',id)
			return false
		end
	else
			warning('Try to AddFriendNotifyer with none friend for ',id)
			return false
	end
	return false
end
function RemoveFriendNotifyer( id,callback )
	-- body
	if IsFriend(id) then
		for _,v in pairs(FriendInfoChangeNotifyer) do
			if v.id == id then
				for k,n in pairs(v.notifyers) do
					if n == callback then
						table.remove(v.notifyers,k)
						return true
					end
				end
			end
		end
	end
	return false
end
function AddFriend(acc_id)
	local req = {
		type = "FOT_ADD",
		op_friend = { 
			acc_id = acc_id,
		},
	}

	local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
	print(modname, "AddFriendReq", acc_id)
end
function GetFriendById( acc_id )
	-- body
	for k,v in pairs(FriendList) do
		if acc_id == v.acc_id then 
			print('GetFriendById ',v.name,',online',v.online)
			return v
		end
	end
	return nil
end
function ShowAccountInfo(acc_id,flag,plat_id)
	if flag then
		FriendsInfoQueryLua = flag
	else
		FriendsInfoQueryLua = nil
	end
	for k,v in pairs(FriendsInfoCaches or {}) do 
		if v.resp.acc_id == acc_id then 
			print('exist player info '..v.resp.acc_id..',name '..v.resp.name)
        	Scheduler.Instance:AddFrame(1, false, NativeQueryFriendInfo(v.resp))			
			return
		end
	end
	local req = {}
	if plat_id then
		req = {
	            friend = {
	                acc_id = acc_id,
	                plat_id = plat_id,
	            },
	        }
	else
		req = {
			friend = {
				acc_id = acc_id,
			}
		}
	end
	local buf = protobuf.encode("fogs.proto.msg.QueryFriendInfoReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.QueryFriendInfoReqID, buf)
	
	LuaHelper.RegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, HandleQueryFriendInfo, modname)
	CommonFunction.ShowWaitMask()
	CommonFunction.ShowWait()
	
	-- print(modname, "QueryInfoReq", acc_id)
end
function NativeQueryFriendInfo( resp )
	-- body	
	return function ( )
		-- body
		if FriendsInfoQueryLua then
			FriendsInfoQueryLua:setData(resp) 
			FriendsInfoQueryLua = nil
		else--打开好友信息界面
			if not uiFriInfo then
				uiFriInfo = createUI("FriendsInfo")
				local friLua = getLuaComponent(uiFriInfo)
				friLua:setData(resp)
				friLua.onCloseEvent = function()
					uiFriInfo = nil
				end
				UIManager.Instance:BringPanelForward(uiFriInfo)
			end
		end
	end
	
end
function HandleQueryFriendInfo(buf)
	CommonFunction.StopWait()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, modname)
	
	local resp, err = protobuf.decode("fogs.proto.msg.QueryFriendInfoResp", buf)
	if not resp then
		error("", err)
		return
	end
	
	if resp.result ~= 0 then
		return
	end
	table.insert(FriendsInfoCaches,FriendCache:New(resp,FriendsInfoCaches))
	-- for k,v in pairs(FriendsInfoCaches) do
	-- 	print('friend cache id '..v.resp.acc_id..',name '..v.resp.name)
	-- end
	--当缓存太大，销毁时间较短的缓存
	if #FriendsInfoCaches > 30 then 
		local idx = 0
		for k,v in pairs(FriendsInfoCaches) do
			if idx == 0 then
				idx = k
			else
				if FriendsInfoCaches[idx].autoReleaseDuration > v.autoReleaseDuration then
					idx = k
				end
			end
		end
		if idx ~= 0 then
			print('there\'s too much friend cache,remove id '..FriendsInfoCaches[idx].resp.acc_id..',name '..FriendsInfoCaches[idx].resp.name)
			table.remove(FriendsInfoCaches,idx)
		end
		collectgarbage()
	end
	if FriendsInfoQueryLua then
		FriendsInfoQueryLua:setData(resp) 
	else--打开好友信息界面
		if not uiFriInfo then
			uiFriInfo = createUI("FriendsInfo")
			local friLua = getLuaComponent(uiFriInfo)
			friLua:setData(resp)
			friLua.onCloseEvent = function()
				uiFriInfo = nil
			end
			UIManager.Instance:BringPanelForward(uiFriInfo)
		end
	end
	FriendsInfoQueryLua = nil
	
end

--打开好友选项菜单
function OpenFriendItemTip(id, name, icon, plat_id)
	--在friendsItemTip.lua OnClose时设置为nil
	if friendItemTip == nil then
		friendItemTip = createUI("FriendsItemTip")
	end

    local tipsLua = getLuaComponent(friendItemTip)
    tipsLua:setData(id, name, icon, plat_id)

    UIManager.Instance:BringPanelForward(friendItemTip)
end
--获取好友友好度，如果返回-1说明不是好友
function GetShinWakanByFriendId( id )
	-- body
	for k,v in pairs(FriendList) do
		if id == v.acc_id then
			return v.shinwakan
		end
	end
	return -1

end
--好友列表
function HandlerFriendOperationResp(buf)
	-- body
	local resp, err = protobuf.decode("fogs.proto.msg.FriendOperationResp", buf)
	if not resp then
		error("", err)
		return
	end
	
	if resp.result ~= 0 then
		return
	end
	local sort = false
	-- warning('HandlerFriendOperationResp ',resp.type)
	local AddFriendToTable = function ( v )
		-- body
		local friend = {}
		friend.acc_id = v.acc_id
		friend.name = v.name
		friend.plat_id = v.plat_id
		friend.level = v.level
		friend.vip_level = v.vip_level
		friend.icon = v.icon
		friend.present_flag = v.present_flag
		friend.get_flag = v.get_flag
		friend.online = v.online
		friend.logout_time = v.logout_time
		friend.shinwakan = v.shinwakan
		friend.pvp_ladder_score = v.pvp_ladder_score
		friend.first_win_time = v.first_win_time
		print('Friends ',friend.name,',online ',v.online,',first_win_time ',v.first_win_time)
		table.insert(FriendList,friend)

	end
	if tostring(resp.type) == 'FOT_QUERY'  then 
		FriendList = {}
		sort = true
		for k,v in pairs(resp.friend or {}) do 
			AddFriendToTable(v)
		end
	elseif tostring(resp.type) == 'FOT_ADD'  then
		-- if not IsFriend(resp.op_friend.acc_id) then 
		-- 	-- sort = true		
		-- 	-- AddFriendToTable(v)
		-- end
	elseif tostring(resp.type) == 'FOT_AGREE_APPLY'  then 
		if not IsFriend(resp.op_friend.acc_id) then 
			sort = true		
			AddFriendToTable(resp.op_friend)
			CommonFunction.ShowTip(string.format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_AGREE"), resp.op_friend.name),nil);
		end
	elseif tostring(resp.type) == 'FOT_APPLY_AGREE'  then 
		if not IsFriend(resp.op_friend.acc_id) then 
			sort = true		
			AddFriendToTable(resp.op_friend)
			CommonFunction.ShowTip(string.format(CommonFunction.GetConstString("STR_FRIENDS_TIPS_ADD"), resp.op_friend.name),nil);
		end
	elseif tostring(resp.type) == 'FOT_DEL_FRIEND'  then 
		if IsFriend(resp.op_friend.acc_id) then 
			sort = true		
			local idx = 1
			for k,v in pairs(FriendList) do
				if v.acc_id == resp.op_friend.acc_id then
					table.remove(FriendList,idx)
				end
				idx = idx +1
			end
		end
	elseif tostring(resp.type) == 'FOT_PRESEND'  then
		if not resp.op_friend then	--自己赠送
			for k,v in pairs(resp.friend) do
				for k1,v1 in pairs(FriendList) do
					if v.acc_id == v1.acc_id then
						v1.present_flag = v.present_flag
						print('FOT_PRESEND state change ',v.name,',to ',v1.present_flag)
					end
				end
			end
		end
	end
	table.sort(FriendList,SortFriend)
	-- printSimpleTable(FriendList,modname)
end
function SortFriend( AF1,AF2 )
	-- body
		if not AF1 then return 1 end
		if not AF2 then return -1 end

	    local a1 = 0
        local a2 = 0
        if AF1.online ~= 5 then 
        	a1 = 1
        end
        if AF2.online ~= 5 then 
        	a2 = 1
        end
        if a1 ~= a2 then 
        	return a1 > a2
        end
        if AF1.shinwakan ~= AF2.shinwakan then 
        	return AF1.shinwakan > AF2.shinwakan
        end
        return AF1.acc_id < AF2.acc_id
end
function HandlerUpdateFriendInfo( buf )
	-- body	
	local resp, err = protobuf.decode("fogs.proto.msg.UpdateFriendInfo", buf)
	if not resp then
		error("", err)
		return
	end
	
	-- warning('HandlerUpdateFriendInfo',resp.friend.name,',online ',resp.friend.online)
	local get = false
	local update = false
	for k,v in pairs(FriendList or {}) do
		if v.acc_id == resp.friend.acc_id then 
			if( v.online ~= resp.friend.online )or (v.shinwakan ~= resp.friend.shinwakan) then 
				update = true
			end
			v.acc_id = resp.friend.acc_id
			v.name = resp.friend.name
			v.plat_id = resp.friend.plat_id
			v.level = resp.friend.level
			v.vip_level = resp.friend.vip_level
			v.icon = resp.friend.icon
			v.present_flag = resp.friend.present_flag
			v.get_flag = resp.friend.get_flag
			v.online = resp.friend.online
			v.logout_time = resp.friend.logout_time
			v.shinwakan = resp.friend.shinwakan
			v.pvp_ladder_score = resp.friend.pvp_ladder_score
			v.first_win_time = resp.friend.first_win_time
			get = true
			break
		end
	end
	if not get then 
		local friend = {}
		local v = resp.friend
		friend.acc_id = resp.friend.acc_id
		friend.name = resp.friend.name
		friend.plat_id = resp.friend.plat_id
		friend.level = resp.friend.level
		friend.vip_level = resp.friend.vip_level
		friend.icon = resp.friend.icon
		friend.present_flag = resp.friend.present_flag
		friend.get_flag = resp.friend.get_flag
		friend.online = resp.friend.online
		friend.logout_time = resp.friend.logout_time
		friend.shinwakan = resp.friend.shinwakan
		friend.pvp_ladder_score = resp.friend.pvp_ladder_score
		friend.first_win_time = resp.friend.first_win_time
		table.insert(FriendList,friend)
		update = true
	end
	if update then 
		table.sort(FriendList,SortFriend)
	end
	for _,v in pairs(FriendInfoChangeNotifyer) do
		if v.id == resp.friend.acc_id then
			for _,notifyer in pairs(v.notifyers) do
				if notifyer then 
					notifyer(resp.friend)
				end
			end
			break
		end
	end
	if FriendInfoChangeEvent ~= nil then 
		FriendInfoChangeEvent(FriendOperationType.FOT_QUERY)
	end
end
function IsFriend( acc_id )
	-- body
	for k,v in pairs(FriendList or {}) do
		if v.acc_id == acc_id then 
			return true
		end
	end
	return false
end
LuaHelper.RegisterPlatMsgHandler(MsgID.FriendOperationRespID, HandlerFriendOperationResp, modname)
LuaHelper.RegisterPlatMsgHandler(MsgID.UpdateFriendInfoID, HandlerUpdateFriendInfo, modname)



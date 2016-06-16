local modname = "Friends"
FriendCache = require "Custom/Friends/FriendCache"
module(modname, package.seeall)

friendItemTip = nil  --好友选项菜单
FriendsInfoQueryLua = nil -- 好友请求界面
FriendsInfoCaches = {}
setmetatable(FriendsInfoCaches, {__mode = "kv"})--弱引用
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
	
	print(modname, "QueryInfoReq", acc_id)
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
	print("Friends.HandleQueryFriendInfo")
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

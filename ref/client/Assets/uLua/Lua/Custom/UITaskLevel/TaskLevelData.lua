local modname = "TaskLevelData"
module(modname, package.seeall)
TaskLevelStates ={}
--奖励消息的回掉
AwardRespCallBack = nil
LevelStateRefreshCallBack = nil
function HandleLevelAwardStateResp()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.LevelAwardStateResp", buf)
		CommonFunction.StopWait()
		if not resp then
			error(err)
			return
		end
		-- warning('function HandleLevelAwardStateResp()')

 		for k, v in pairs(resp.state or {}) do
 			local _k = v.id
 			local _v = v.value
 			TaskLevelStates[_k]= _v;
 			-- print(' ,v '..v.id..',s '..v.value)
        end

        if LevelStateRefreshCallBack ~= nil then
        	LevelStateRefreshCallBack()
        else
        	-- print('LevelStateRefreshCallBack is nill')
        end


	end
end

function HandleGetLevelAwardResp()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.GetLevelAwardResp", buf)
		CommonFunction.StopWait()
		if not resp then
			error(err)
			return
		end
		-- print('function HandleGetLevelAwardResp()')

		if AwardRespCallBack ~=nil then
			AwardRespCallBack(resp)
		end
	end
end

LuaHelper.RegisterPlatMsgHandler(MsgID.LevelAwardStateRespID, HandleLevelAwardStateResp(), modname)
LuaHelper.RegisterPlatMsgHandler(MsgID.GetLevelAwardRespID, HandleGetLevelAwardResp(), modname)

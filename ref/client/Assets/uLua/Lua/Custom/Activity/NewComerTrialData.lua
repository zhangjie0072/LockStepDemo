local modname = "NewComerTrialData"
module(modname, package.seeall)
--新秀任务数据
NotifyCallBack = nil
TaskFinishNotifyCallBack = nil
function HandNewComerTrialNotify()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.NewComerTrialNotify", buf)
		CommonFunction.StopWait()
		if not resp then
			error(err)
			return
		end
		print('function HandNewComerTrialNotify()')


        if NotifyCallBack ~= nil then
        	NotifyCallBack(resp)
        else
        	-- print('LevelStateRefreshCallBack is nill')
        end


	end
end
function TaskFinishExitHandler( )
	-- body
	return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.NotifyTaskFinish",buf)
        if resp then
            local id = resp.task_info.id
            local state = resp.task_info.state
            if resp.task_info.type == 7 then --新秀任务
            	local taskInfo = MainPlayer.Instance.taskInfo
            	if taskInfo then 
            		local enum = taskInfo.task_list:GetEnumerator()
				    while enum:MoveNext() do
				        if enum.Current.id == id then			           
				            enum.Current.state = state 
				            break
				        end
				    end  
            	end               
        	end
        	if TaskFinishNotifyCallBack~=nil then 
        		TaskFinishNotifyCallBack(resp)
        	end
    	end
	end
end
LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyTaskFinishID, TaskFinishExitHandler(), modname)
LuaHelper.RegisterPlatMsgHandler(MsgID.NewComerTrialNotifyID, HandNewComerTrialNotify(), modname)
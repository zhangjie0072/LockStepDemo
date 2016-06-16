--项目公用函数

TaskRespHandler =
{
    parent = nil,
    hideModel = false,
    -- buttonMenu = nil,
    -- isOpen = false,
}

function TaskRespHandler.Regist()
    print("-------****************************- Handler Regist")
    LuaHelper.RegisterPlatMsgHandler(MsgID.TaskInfoRespID, TaskRespHandler.process(), 'TaskRespHandler')
end

function TaskRespHandler.process(message)
    return function (message)
        print("get task resp")
        local resp, err = protobuf.decode('fogs.proto.msg.TaskInfoResp', message)
        if resp == nil then
            Debugger.LogError('------TaskInfoRespHandler error: ', err)
            return
        end

        -- if TaskRespHandler.isOpen then
        --	return
        -- end

        local parent = TaskRespHandler.parent
        --隐藏模型
        if TaskRespHandler.hideModel then
            if parent and parent.SetModelActive then
                parent:SetModelActive(false)
            end
        end

        --创建任务界面
        CommonFunction.StopWait()
        local root = UIManager.Instance.m_uiRootBasePanel
        local task
        local willUpdate  = true
        if resp.type == TaskType.NEW_COMER then             
            task = TopPanelManager:ShowPanel('NewComerTrial')
            task.taskList = resp.task_main_list or {}
            task:SetParent(parent)
            task.unclaimedInfo = {}
            for k, v in pairs(resp.unclaimed_list or {}) do
                task.unclaimedInfo[v.type] = v.num
            end
            return
        elseif resp.type == 2 then -- 成就
            --parent.uiTask = parent.transform:FindChild('task')
            local go = root.transform:FindChild('UITask(Clone)')
            if go ~= nil then
                task = getLuaComponent(go)
            else
                task = getLuaComponent(createUI("UITask"))
            end            
            task.taskList[TaskType.MAIN] = resp.task_main_list
            print('TaskRespHandler refresh task main list')
            task:SetParent(parent)
            task:SetType(resp.type)
            task.hideModel = TaskRespHandler.hideModel
            task.mbTaskListRefreshed = false
        else
            task = TopPanelManager:ShowPanel('UITaskLevel')
            if resp.type == 3 then
                task.dailyList = resp.task_daily_list
                task.mUIState = 1
            else
                task.taskList = resp.task_main_list
                task.mUIState = 0
            end
            -- task.started = function ( ... )
            --            end

        end
        task.unclaimedInfo = {}
        for k, v in pairs(resp.unclaimed_list or {}) do
            task.unclaimedInfo[v.type] = v.num
        end
        task:SetType(resp.type)
        task.hideModel = TaskRespHandler.hideModel
        task:Refresh()
        -- task.onClose = function ( ... )
        --	TaskRespHandler.isOpen = false
        -- end

        task.onClose = function ()
            -- TaskRespHandler.buttonMenu.isClicking = false
            TaskUI = nil
        end

        -- if TaskRespHandler.buttonMenu then
        --	TaskUI = go
        --	print("---taskui:",TaskUI)
        -- end

        -- TaskRespHandler.isOpen = true
        -- if UITask.getAward == true then
        --	CommonFunction.ShowPopupMsg(getCommonStr("RECEIVE_SUCCESS"),nil,nil,nil,nil,nil)

        --	UITask.getAward = false
        -- end
    end
end

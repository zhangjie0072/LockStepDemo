--encoding=utf-8

TaskState =
{
    RUNNING = 1, --进行中
    FINISH = 2, --完成
    AWARDED = 3 --已领取奖励
}

TaskType =
{
    FESTIVAL = 1,--活动
    MAIN = 2,--主线任务
    DAILY = 3,--日常任务
    SIGN = 4,--签到任务
    OTHER = 5,--其他任务
    LEVEL = 6,--成长任务
    NEW_COMER = 7,--新秀任务
}

UITask = UITask or
{
    uiName = 'UITask',

    ----------------------------------
    parent,
    hideModel = false,
    onClose,
    banTwice = false,

    ----------------------------------
    taskType = 3,

    unclaimedTaskIDList = {},
    unclaimedInfo = {},

    taskList = {},

    is_rank = false,

    ----------------------------------UI
    uiBtnBack,
    uiTitle,

    uiMain,
    uiDaily,
    uiListSV,
    uiListGrid,

    uiActivity,
    uiPlayerProperty,

    lblAchieved,
    lblRanking,
    lblRankingNum,

    uiAnimator,
    uiScrollViewAsyncLoadItem, -- Added by Conglin

    sprBgIcon,
    btnClose1,
    btnClose2,
    mbTaskListRefreshed= false,
}


-----------------------------------------------------------------
--Awake
function UITask:Awake( ... )
    local transform = self.transform

    self.uiTitle = transform:FindChild('Top/Title'):GetComponent('MultiLabel')
    self.uiMain = transform:FindChild('Window1').gameObject
    self.uiDaily = transform:FindChild('Window2').gameObject
    self.uiScrollViewAsyncLoadItem = transform:FindChild('Window1/TaskList'):GetComponent("ScrollViewAsyncLoadItem")

    if self.uiDaily then
        NGUITools.SetActive(self.uiDaily.gameObject,false)
    end
    self.uiAnimator = self.transform:GetComponent('Animator')
    self.lblAchieved = self.transform:FindChild('Window1/Achieved'):GetComponent('UILabel')
    self.lblRanking = self.transform:FindChild('Window1/Ranking'):GetComponent('UILabel')
    self.lblRankingNum = self.transform:FindChild('Window1/Ranking/Num'):GetComponent('UILabel')
end

--Start
function UITask:Start( ... )
    --self.uiPlayerProperty = self.transform:FindChild('Top/PlayerInfoGrids')
    --local playerInfo = getLuaComponent(self.uiPlayerProperty.gameObject)
    --playerInfo.isAchievement = true

    self.btnClose1 = createUI('ButtonClose', self.transform:FindChild('Window1/ButtonClose'))
    self.btnClose2 = createUI('ButtonClose', self.transform:FindChild('Window2/ButtonClose'))
    addOnClick(self.btnClose1.gameObject, self:OnBackClick())
    addOnClick(self.btnClose2.gameObject, self:OnBackClick())

    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyTaskFinishID, self:TaskFinishExitHandler(), self.uiName)

    UIManager.Instance:BringPanelForward(self.gameObject)
end

--Update
function UITask:FixedUpdate( ... )
    -- body
end

function UITask:OnClose( ... )
    if self.onClose then
        self.onClose()
    end
    if self.is_rank == true then
        NGUITools.Destroy(self.gameObject)
    else
        NGUITools.Destroy(self.gameObject)
        if self.hideModel == true and self.parent and self.parent.SetModelActive then
            self.parent:SetModelActive(true)
        end
    end
end

function UITask:OnEnable()

end

function UITask:OnDisable()

end

function UITask:OnDestroy( ... )
    -- body
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyTaskFinishID, self.uiName)
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UITask:Refresh( ... )
    -- if self.taskType == 2 then
        self.uiTitle:SetText(CommonFunction.GetConstString('STR_ACHIEVEMENT'))

        self.uiListSV = self.uiMain.transform:FindChild('TaskList'):GetComponent('UIScrollView')
        self.uiListGrid = self.uiMain.transform:FindChild('TaskList/Grid'):GetComponent('UIGrid')

        -- self.uiDaily:SetActive(false)
        self.uiMain:SetActive(true)

        --self.uiPlayerProperty = self.transform:FindChild('Top/PlayerInfoGrids')
        --local playerInfo = getLuaComponent(self.uiPlayerProperty.gameObject)
        --playerInfo.isAchievement = true

        --RankList.ReqList("RT_ACHIEVEMENT")
        self.lblAchieved.text = string.format(getCommonStr("STR_FIELD_PROMPT49"), MainPlayer.Instance.Honor2)

    -- elseif self.taskType == 3 then
        -- self.uiTitle:SetText(CommonFunction.GetConstString('STR_TASK_DAILY'))

        -- self.uiListSV = self.uiDaily.transform:FindChild('TaskList'):GetComponent('UIScrollView')
        -- self.uiListGrid = self.uiDaily.transform:FindChild('TaskList/Grid'):GetComponent('UIGrid')
        -- self.uiActivity = self.uiDaily.transform:FindChild('Activity').gameObject
        -- self.sprBgIcon = self.uiActivity.transform:FindChild('BgIcon'):GetComponent('UISprite')
        -- getLuaComponent(self.uiActivity).parent = self.parent

        -- self.uiDaily:SetActive(true)
        -- self.uiMain:SetActive(false)

        --self.uiPlayerProperty = self.transform:FindChild('Top/PlayerInfoGrids')
        --local playerInfo = getLuaComponent(self.uiPlayerProperty.gameObject)
        --playerInfo.isAchievement = false
    -- end
    self:InitTaskList()

    self:MakeOnUITaskRefreshed()
end


-----------------------------------------------------------------
--
function UITask:SetParent(parent)
    self.parent = parent
end

--点击返回事件
function UITask:OnBackClick()
    return function (go)
        if self.uiAnimator then
            self:AnimClose()
        else
            self:OnClose()
        end
    end
end

--
function UITask:SetType(type)
    self.taskType = type
end

function UITask:SetModelActive(active)
    --can't delete!!!
end

--未领取奖励的任务的数量通知
function UITask:UnclaimedTaskNumNotify()
    return function(message)
        local resp, err = protobuf.decode('fogs.proto.msg.UnclaimedTaskNumNotify', message)
        if resp == nil then
            Debugger.LogError('------UnclaimedTaskNumNotify error: ', err)
            return
        end

        --设置大厅显示未领取奖励任务的提示
        if resp.num > 0 then
            UIHall.showTaskTips = true
        end
        self.unclaimedTaskIDList = resp.task_id
        --
        for k, v in pairs(resp.task_id or {}) do
            local type = GameSystem.instance.TaskConfigData:GetTypeById(v)
            self.unclaimedInfo[type] = (self.unclaimedInfo[type] or 0) + 1
        end
    end
end

--递减未处理任务数量信息
function UITask:DecreUnclaimedInfo(taskType)
    if self.unclaimedInfo.taskType then
        self.unclaimedInfo.taskType = self.unclaimedInfo.taskType - 1
        if self.unclaimedInfo.taskType <= 0 then
            self.unclaimedInfo.taskType = nil
        end
    end
end

--从任务列表中删除指定类型和ID的任务
function UITask:RemoveTaskData(taskType, taskID)
    for k, v in pairs(self.taskList[taskType] or {}) do
        if v.id == taskID then
            v = nil
            break
        end
    end
end

-- function UITask:doRefresh( ... )
--     -- body
--     return function ( go )
        -- body
        -- self:Refresh()
             -- TODO 临时解决方案，打开状态不能处理TaskInfoResp消息
        -- TaskRespHandler.isOpen = false
        -- local req = {
        --     acc_id = MainPlayer.Instance.AccountID,
        --     type = TaskType.MAIN,
        -- }
        -- local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        -- LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)
        -- CommonFunction.ShowWait()
        -- print(self.uiName, "Send task info req")
--     end
-- end

--奖励信息回复
function UITask:GetTaskAwardResp(award_id)
    return function (message)
        self.mbTaskListRefreshed = false
        if self.banTwice then
            return
        end
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetTaskAwardRespID, TaskItem.uiName)
        local resp, err = protobuf.decode('fogs.proto.msg.GetTaskAwardResp', message)
        if resp == nil then
            Debugger.LogError('------GetTaskAwardResp error: ', err)
            return
        end

        if resp.result ~= 0 then
            Debugger.Log('-----------1: {0}', resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
            return
        end
        self.banTwice = true
        --UITask.getAward = true
        --CommonFunction.ShowPopupMsg(getCommonStr("RECEIVE_SUCCESS"),nil,nil,nil,nil,nil)
        local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(award_id)
        local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
        for i = 0, awardConfig.awards.Count - 1 do
            getGoods:SetGoodsData(awardConfig.awards:get_Item(i).award_id,awardConfig.awards:get_Item(i).award_value)
        end
        getGoods.onClose = function ( ... )
            self.banTwice = false
        end

        local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
        while enum:MoveNext() do
            local itemid = enum.Current.id
            if itemid == resp.id then
                enum.Current.state = 3		--设置为已领取状态
                print('enum.Current.state change ========= ' .. enum.Current.state)
            end
        end

        --refresh buttonmenu
        -- if resp.type == 2 then
        -- elseif resp.type == 3 then
        --     UpdateRedDotHandler.MessageHandler("Daily")
        -- end
        --删除已经领取这个任务
        -- for k,v in pairs(self.taskList[TaskType.MAIN]) do
        --     if v.id == resp.id then
        --         self.taskList[TaskType.MAIN].k = nil
        --         break
        --     end
        -- end
        UpdateRedDotHandler.MessageHandler("Task")
        print(self.uiName,"----uiListGrid:",self.uiListGrid)
        self.uiListGrid.repositionNow = true
        self.uiListSV:ResetPosition()

        self:DecreUnclaimedInfo()
        --self:ShowUnclaimedTips()

        --NGUITools.Destroy(self.gameObject)

        -- TODO 临时解决方案，打开状态不能处理TaskInfoResp消息
        -- TaskRespHandler.isOpen = false
        -- local req = {
        --     acc_id = MainPlayer.Instance.AccountID,
        --     type = TaskType.MAIN,
        -- }
        -- local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        -- LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)
        -- print(self.uiName, "Send task info req")

        --self:Refresh();
    end
end

-- 切换日常任务
-- function UITask:OnDailyClick()
--     return function (go)
--         self.clickMainTimes = 0
--         self.clickDailyTimes = self.clickDailyTimes + 1
--         if self.clickDailyTimes <= 1 then
--             self.taskType = TaskType.DAILY
--             self:InitTaskList()
--             -- self.dailyTaskLabel.color = Color.New(255/255, 255/255, 255/255, 1)
--             -- self.mainTaskLabel.color = Color.New(82/255, 82/255, 82/255, 1)
--         end
--     end
-- end

-- 切换主线任务
-- function UITask:OnMainClick()
--     return function (go)
--         self.clickDailyTimes = 0
--         self.clickMainTimes = self.clickMainTimes + 1
--         if self.clickMainTimes <= 1 then
--             self.taskType = TaskType.MAIN
--             self:InitTaskList()
--             -- self.mainTaskLabel.color = Color.New(255/255, 255/255, 255/255, 1)
--             -- self.dailyTaskLabel.color = Color.New(82/255, 82/255, 82/255, 1)
--         end
--     end
-- end


-- 点击商品条目事件
-- function UITask:OnItemClick()
--     return function (go)
--         if self.preSelectItem == go then
--             return
--         end
--         if self.preSelectItem then
--             getLuaComponent(self.preSelectItem):Normal()
--         end
--         self.preSelectItem = go

--         local item = getLuaComponent(go)
--         item:Select()
--         self:InitShowDetail(item.config)
--     end
-- end

--
-- function UITask:ShowUnclaimedTips()
--     for k, v in pairs(self.unclaimedInfo) do
--         print('------------- k: ', k)
--         print('------------- v: ', v)
--     end
--     local showDailyTips = self.unclaimedInfo[TaskType.DAILY] and self.unclaimedInfo[TaskType.DAILY] > 0 and true or false
--     local showMainTips = self.unclaimedInfo[TaskType.MAIN] and self.unclaimedInfo[TaskType.MAIN] > 0 and true or false
--     self.uiDailyTips.gameObject:SetActive(showDailyTips)
--     self.uiMainTips.gameObject:SetActive(showMainTips)
-- end

-- 任务排序函数
function UITask.TaskSortFunc(task1, task2)
    if task1 == nil then
        return 1
    end
    if task2 == nil then
        return -1
    end
    if task1.state == TaskState.RUNNING and task2.state == TaskState.RUNNING then
        local schedule1 = task1.cond_info[1].condition_cur / task1.cond_info[1].condition_need
        local schedule2 = task2.cond_info[1].condition_cur / task2.cond_info[1].condition_need
        if schedule1 > 1 then
            schedule1 = task1.cond_info[1].condition_need / task1.cond_info[1].condition_cur
        end
        if schedule2 > 1 then
            schedule2 = task2.cond_info[1].condition_need / task2.cond_info[1].condition_cur
        end
        if schedule1 == schedule2 then
            return task1.id < task2.id
        else
            return schedule1 > schedule2
        end
    elseif task1.state == TaskState.FINISH and task2.state == TaskState.RUNNING then
        return true
    elseif task1.state == TaskState.RUNNING and task2.state == TaskState.FINISH then
        return false
    else
        return task1.id < task2.id
    end
end

-- 处理 NotifyTaskFinish 消息
function UITask:TaskFinishExitHandler()
    return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.NotifyTaskFinish",buf)

        if resp then
            if resp.task_info.type ~= 2 then return end
            local id = resp.task_info.id
            local state = resp.task_info.state
            print('UITask:TaskFinishExitHandler id '..id..',state '..state)
            -- for k, v in pairs(self.taskList[TaskType.DAILY]) do
            --     if v.id == id then
            --         v.state = state
            --         for kk, vv in pairs(v.cond_info) do
            --             vv.condition_need = 1
            --             vv.condition_cur = 1
            --         end

            --     end
            -- end
            for k, v in pairs(self.taskList[TaskType.MAIN]) do
                if v.id == id then
                    v.state = state
                     for kk, vv in pairs(v.cond_info) do
                        -- vv.condition_need = 1
                        vv.condition_cur = vv.condition_need
                        print('state id '..v.id..',state '..v.state)
                    end
                end
            end
        end
        self.mbTaskListRefreshed = false
    end
end

function UITask:CreateItem( index, k, v)
    local info = GameSystem.Instance.TaskConfigData:GetTaskMainInfoByID(v.id)
    if info then--or
    -- GameSystem.Instance.TaskConfigData:GetTaskDailyInfoByID(v.id) then
        local item_count = self.uiListGrid.transform.childCount;
        local go = nil
        if index < item_count then
            go = self.uiListGrid.transform:GetChild(index);
        else
            go = createUI('TaskItem', self.uiListGrid.transform)
        end
        --index = index + 1;
        go.transform.name = v.id
        if go == nil then
            Debugger.Log('-- InstantiateObject falied ')
            return
        end

        local taskItem = getLuaComponent(go)
        taskItem:SetData(v)
        taskItem:SetParent(self)
        taskItem:SetDragSV(self.uiListSV)
        print('task item info '..v.id..',title '..info.title..',log '..info.desc)
        if self.is_rank == true then
            taskItem.is_rank = true
        end

        return go;
    end

    return nil;
end



--初始化任务列表
function UITask:InitTaskList()
    if self.mbTaskListRefreshed then
        return
    else
         self.mbTaskListRefreshed = true
        -- table.sort(self.taskList[TaskType.DAILY], UITask.TaskSortFunc)
        table.sort(self.taskList[TaskType.MAIN], UITask.TaskSortFunc)

        --self:ShowUnclaimedTips()

        print(self.uiName,"-------------------:",self.uiListGrid)

        local item_count = self.uiListGrid.transform.childCount;
        local taskCount = 0;
        local taskArray = {}

        print('@aaa' .. taskCount)
        --CommonFunction.ClearGridChild(self.uiListGrid.transform)
        for k, v in pairs(self.taskList[self.taskType] or {}) do
                taskCount = taskCount + 1
                taskArray[taskCount] = { k = k, v = v }
        end

        print('@bbb' .. taskCount)
        if taskCount > 0 then
            self.uiScrollViewAsyncLoadItem.OnCreateItem = function ( i, parent )
                local index = i + 1

                print('@b[a]' .. i)
                local  go = self:CreateItem(i, taskArray[index].k, taskArray[index].v)
                print('@b[b]' .. go.name)
                return go;
            end

            self.uiScrollViewAsyncLoadItem:Refresh(taskCount)
        end
        print('@ccc' .. taskCount)

        -- for i = 1, index do
        --    self:CreateItem(item_count, i, kvList[i].k, kvList[i].v)
        -- end

        --多余的控件隐藏起来
        if taskCount < item_count then
            local go = self.uiListGrid.transform:GetChild(taskCount);
            NGUITools.SetActive(go.gameObject, false)
            taskCount = taskCount + 1
        end
         print('@ddd' .. taskCount)

        self.uiListGrid.repositionNow = true
        self.uiListSV:ResetPosition()
        if not (#self.taskList[self.taskType] > 0) then
            NGUITools.SetActive(self.sprBgIcon.gameObject, true)
        end
    end

end

--刷新成就信息
function UITask:MakeOnUITaskRefreshed()
    local rank = RankList.myRankInfos[RankType.RT_ACHIEVEMENT]
    local ranking = 0
    if rank ~= nil then
        ranking = rank.ranking
    end

    self.lblRanking.text = getCommonStr("STR_FIELD_PROMPT44")
    print('------------myRankInfo.Ranking:' .. ranking)
    if ranking == 0 then
        self.lblRankingNum.text = '-'
    else
        self.lblRankingNum.text = ranking
    end
end

return UITask

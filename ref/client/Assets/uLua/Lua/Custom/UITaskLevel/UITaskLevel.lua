
UITaskLevel =  UITaskLevel or  {
    uiName="UITaskLevel",

    ----------------------------------
    titleStr = 'STR_ROLE_FASHION',
    uiBtnBack= nil,
    uiPlayerPropertyLua= nil,
    uiGridAwardLevel= nil,
    uiGridTaskItem= nil,
    uiGridTeamUpItem= nil,
    uiBtnDaily= nil,
    uiBtnGrowth= nil,
    uiDailyItem = nil,
    uiGrowthItem = nil,
    uiGrowthItem1 = nil,
    uiGridDaily = nil,
    sprBgIcon = nil,
    uiActivity= nil,
    uiToggleGrowth = nil,
    uiToggleDaily = nil,
    --more to be added
    uiEmptyText= nil,
    uiAnimator= nil,
    mUIState = 1,--0 growth,1 daily else to be added
    taskList = {},
    levelRewardList = nil,
    dailyList = {},
    -- teamUpList = {},
    unclaimedTaskIDList = {},
    unclaimedInfo = {},
    mChoosedLevel = 1,
    posLevelScroll,
    posLevelGrid,
    uiActivityLua = nil,
    uiLblTaskDes = nil,
    uiRedDotDaily = nil,
    uiRedDotGrowth = nil,
    uiAwardLevelScv = nil,
    uiScrollViewAsyncLoadItem, -- Added by Conglin

}
-----------------------------------------------------------------
function UITaskLevel:Awake()

    self.uiTitle = self.transform:FindChild('Top/Title'):GetComponent('MultiLabel')

    --grid
    self.uiAwardLevelScv = self.transform:FindChild('Left/Scroll View'):GetComponent('UIScrollView')
    self.uiGridAwardLevel = self.transform:FindChild('Left/Scroll View/Grid'):GetComponent('UIGrid')
    self.uiGridTaskItem = self.transform:FindChild('Right/ScrollView/Grid'):GetComponent('UIGrid')
    self.uiGridTeamUpItem = self.transform:FindChild('Right/Grid'):GetComponent('UIGrid')

    self.uiGridDaily = self.transform:FindChild('Righ1/TaskList/Grid'):GetComponent('UIGrid')
    self.uiScrollViewAsyncLoadItem = self.transform:FindChild('Righ1/TaskList'):GetComponent('ScrollViewAsyncLoadItem')

    self.uiActivity = self.transform:FindChild('Righ1/Activity').gameObject
    self.sprBgIcon = self.uiActivity.transform:FindChild('BgIcon'):GetComponent('UISprite')

    --button
    self.uiBtnBack = createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack'))
    self.uiBtnDaily = self.transform:FindChild('Button/Daily')
    self.uiBtnGrowth = self.transform:FindChild('Button/Growth')
    self.uiRedDotDaily = self.transform:FindChild('Button/Daily/Tip')
    self.uiRedDotGrowth = self.transform:FindChild('Button/Growth/Tip')
    self.uiToggleDaily = self.transform:FindChild('Button/Daily'):GetComponent('UIToggle')
    self.uiToggleGrowth = self.transform:FindChild('Button/Growth'):GetComponent('UIToggle')

    self.uiDailyItem = self.transform:FindChild('Righ1')
    self.uiGrowthItem = self.transform:FindChild('Right')
    self.uiGrowthItem1 = self.transform:FindChild('Left')

    self.uiEmptyText = self.transform:FindChild('Right/EmptyText')
    self.uiAnimator = self.transform:GetComponent('Animator')

    uiPlayerProperty = self.transform:FindChild('Top/PlayerInfoGrids').gameObject

    -- self.levelRewardList = TaskLevelData.TaskLevelStates
    self.levelRewardList = MainPlayer.Instance.playerLevelAwardInfo
    -- self:ResetLevel()
end
function UITaskLevel:ResetLevel( )

       -- body
    local  level = MainPlayer.Instance.Level
    -- --如果不存在默认是当前等级比列表内最低等级还低
    -- if not self.levelRewardList.level then
    --     level = MainPlayer.Instance.Level+1
    -- end
    -- --如果不是上述情况，说明等级比列表最高等级要高
    -- if not self.levelRewardList.level then
    --     level = table.maxn(self.levelRewardList)
    -- end
    if level==1 then level = 2 end
    self.mChoosedLevel = level
end
function UITaskLevel:Start()
    print('UITaskLevelStart')
    -- self.parent.gameObject:SetActive(false)
    addOnClick(self.uiBtnBack.gameObject, self:ClickBack())
    -- addOnClick(self.uiWardrobe.gameObject, self:ChangeWardrobe())
    addOnClick(self.uiBtnDaily.gameObject, self:ClickDaily())
    addOnClick(self.uiBtnGrowth.gameObject, self:ClickGrowth())
    LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyTaskFinishID, self:TaskFinishExitHandler(), self.uiName)
    -- if not self.uiPlayerPropertyLua then
    --     self.uiPlayerPropertyLua = getLuaComponent(uiPlayerProperty)
    -- end
    -- if not self.uiActivityLua then
    --     self.uiActivityLua =  getLuaComponent(self.uiActivity)
    --     self.uiActivityLua.parent = self.transform
    -- end
    self:InitTaskLevelList()

    -- self:Refresh() -- 在TopPanelManager中的started有调用，这里重复了。commented by Conglin
end

function UITaskLevel:SetParent( parent )
    -- body
    self.parent = parent
end
function UITaskLevel:SetType(  )
    -- body
end

--已废弃
function UITaskLevel:StateRefreshCallBack( ... )
    -- body
    return function ( go )
        -- TaskLevelData.LevelStateRefreshCallBack = nil
        -- print('here we come')
        self:Refresh()
    end
end
function UITaskLevel:Refresh()
    print('UITaskLevel Refresh')
    -- NGUITools.SetActive(self.parent.gameObject, false)

    if self.mUIState == 0 then
        NGUITools.SetActive(self.uiDailyItem.gameObject, false)
        NGUITools.SetActive(self.uiGrowthItem.gameObject, true)
        NGUITools.SetActive(self.uiGrowthItem1.gameObject, true)
        self.uiToggleGrowth.value  = true
        self:RefreshGrowth()
        print('mChoosedLevel level:'..self.mChoosedLevel)
    elseif self.mUIState == 1 then
        NGUITools.SetActive(self.uiDailyItem.gameObject, true)
        NGUITools.SetActive(self.uiGrowthItem.gameObject, false)
        NGUITools.SetActive(self.uiGrowthItem1.gameObject, false)
        self.uiToggleDaily.value  = true
        self:RefreshDaily()
    end

    self:RefreshRedDot()
end
function UITaskLevel:RefreshRedDot( ... )
    -- body
    self.uiRedDotDaily.gameObject:SetActive(false)
    self.uiRedDotGrowth.gameObject:SetActive(false)

    local growthRedDot = false
    local dailyRedDot = false
    local level = MainPlayer.Instance.Level

    local item = self.levelRewardList:GetEnumerator()
    while item:MoveNext() do
        -- print(v .. "--k--"..k)
        if item.Current.Value == 0 and item.Current.Key <= level then
            self.uiRedDotGrowth.gameObject:SetActive(true)
            growthRedDot = true
            break
        end
    end

    for k,v in pairs(self.taskList or {}) do
        for k1, v1 in pairs(v.cond_info) do
            if v1.condition_cur>=v1.condition_need == 0 then
                elf.uiRedDotDaily.gameObject:SetActive(true)
                dailyRedDot = true
                break
            end
        end
        if dailyRedDot then break end
    end

    for k,v in pairs(self.dailyList or {}) do
        for k1, v1 in pairs(v.cond_info or {}) do
            if v1.condition_cur >= v1.condition_need  then
                self.uiRedDotDaily.gameObject:SetActive(true)
                dailyRedDot = true
                break
            end
        end
        if dailyRedDot then break end
    end

    if growthRedDot or dailyRedDot then
        UpdateRedDotHandler.UpdateState["UITaskLevel"] = true
    else
        UpdateRedDotHandler.UpdateState["UITaskLevel"] = false
    end
end

function UITaskLevel:RefreshDaily( ... )
    -- body
    self:InitDailyList()
end

function UITaskLevel:RefreshGrowth( ... )
    print("界面刷新")
    --grid移动有问题现在刷新显示第一个
    -- self.mChoosedLevel = 2;
    self:RefreshTaskLevel()
    self:RefreshTaskList()
    self:RefreshTeamUpItem()
end

function UITaskLevel:ClickRewardItem(go)
    -- print('ClickRewardItem level:'..go.level)
    self.mChoosedLevel = go.level
    --uitoggle 的valuechange事件回掉处理选中状态时机有问题
    --点击之后刷新其他等级箱子的状态
    local childCount = self.uiGridAwardLevel.transform.childCount
    for i=0,childCount-1 do
        local child = self.uiGridAwardLevel:GetChild(i)
        if child then
            local luaCom = getLuaComponent(child.gameObject)
            if luaCom then
                if luaCom.level ~= self.mChoosedLevel then
                    luaCom.onChoose = false
                end
            end
        end
    end
    self:RefreshTaskList()
    self:RefreshTeamUpItem()
end

function UITaskLevel:ShowUnclaimedTips()
    for k, v in pairs(self.unclaimedInfo) do
        -- print('------------- k: ', k)
        -- print('------------- v: ', v)
    end
    local showDailyTips = self.unclaimedInfo[TaskType.DAILY] and self.unclaimedInfo[TaskType.DAILY] > 0 and true or false
    --self.uiDailyTips.gameObject:SetActive(showDailyTips)
    --self.uiMainTips.gameObject:SetActive(showMainTips)
end

function UITaskLevel:InitTaskLevelList( ... )
    -- body
     CommonFunction.ClearGridChild(self.uiGridAwardLevel.transform)
    local scroll = self.uiGridAwardLevel.transform.parent:GetComponent('UIScrollView')
    local item  = self.levelRewardList:GetEnumerator()
    while item:MoveNext() do
        -- print(' info item k '..item.Current.Key...',v '..item.Current.Key)
        local config =  GameSystem.Instance.TeamLevelConfigData:GetTeamLevelDataWithReward(item.Current.Key)
        if config then
            local go = createUI('AwardLevel', self.uiGridAwardLevel.transform)
            go.transform.name = item.Current.Key
            if go == nil then
                Debugger.Log('-- InstantiateObject falied ')
                return
            end

            go.transform:Find("Left/AwardLevel"):GetComponent("UIDragScrollView").scrollView = self.uiAwardLevelScv

            local levelItem =getLuaComponent(go)
            --不要修改下面方法执行顺序
            levelItem:SetParent(self)
            levelItem:SetDragSV(scroll)
            levelItem.state = item.Current.Value
            levelItem.level = item.Current.Key
            levelItem:SetData(config)
        end

    end
end

function UITaskLevel:CreateDailyItem( v,  scroll)
    if  GameSystem.Instance.TaskConfigData:GetTaskDailyInfoByID(v.id) then
        local go = createUI('TaskItem', self.uiGridDaily.transform)
        go.transform.name = v.id
        if go == nil then
            Debugger.Log('-- InstantiateObject falied ')
            return nil;
        end
        local taskItem =getLuaComponent(go)
        taskItem:SetData(v)
        taskItem:SetParent(self)
        taskItem:SetDragSV(scroll)
        return go;
    end
    return nil;
end

--初始化日常表
function UITaskLevel:InitDailyList()
    self.mbTaskListRefreshed = true
    table.sort(self.dailyList, UITaskLevel.TaskSortFunc)

    self:ShowUnclaimedTips()

    -- print('dailyList size '..#self.dailyList)
    local scroll = self.uiGridDaily.transform.parent:GetComponent('UIScrollView')
    CommonFunction.ClearGridChild(self.uiGridDaily.transform)

    local taskList = {}
    local taskCount = 0

    for k, v in pairs(self.dailyList or {}) do

        taskList[taskCount] = v
        taskCount = taskCount + 1

        --self:CreateDailyItem(v, scroll)
    end

    print("!!taskCount=".. taskCount)

    self.uiScrollViewAsyncLoadItem.OnCreateItem = function ( index, parent )
        --print('@@@@ index='.. index)
        return self:CreateDailyItem(taskList[index], scroll)
    end
    self.uiScrollViewAsyncLoadItem:Refresh(taskCount)

    self.uiGridDaily.repositionNow = true
    scroll:ResetPosition()
    if not (#self.dailyList > 0) then
        NGUITools.SetActive(self.sprBgIcon.gameObject, true)
    end

end

function UITaskLevel:RefreshTaskLevel( ... )

     --直接翻到指定对象所在的位置
    local scroll = self.uiGridAwardLevel.transform.parent:GetComponent('UIScrollView')
    scroll:SetDragAmount(0,0,false)
    -- self.uiGridAwardLevel.onReposition = function ()
    --     self.uiGridAwardLevel.onReposition = nil
        if scroll and self.uiGridAwardLevel.transform.childCount>0 then
            local  idx = 0
            local item = self.levelRewardList:GetEnumerator()
            while item:MoveNext() do
                if item.Current.Key == self.mChoosedLevel then
                    break
                end
                idx = idx+1
            end
            print('idx '..idx..',level on choose '..self.mChoosedLevel)
            if idx > 1 then
                local total = self.levelRewardList.Count
                local newPos = Vector3.New(0,0,0)--self.uiGridAwardLevel.transform.localPosition
                local  height = self.uiGridAwardLevel.cellHeight
                -- local scrWidth = scroll.panel.width
                local  offset  = Vector3.New(0,height*(idx-0.5),0)
                newPos = newPos + offset
                local scrHeight = scroll.panel.height
                if (total-idx+1)*height<scrHeight then --如果末尾cell比scroll的最低坐标高了，重新计算位置
                    newPos = newPos - Vector3.New(0,scrHeight-((total-idx+0.5)*height),0)
                end
                -- self.uiGridAwardLevel.transform.localPosition = newPos
                -- self.uiGridAwardLevel:ConstrainWithinPanel()
                print('total '..total..',newPos.y '..newPos.y)
                scroll:MoveRelative(newPos)
                -- scroll:RestrictWithinBounds(true)
            end

        end
    -- end
    -- self.uiGridAwardLevel:Reposition()
    -- scroll:ResetPosition()
    -- scroll:RestrictWithinBounds(true)

end
function UITaskLevel:RefreshTeamUpItem()
    print('RefreshTeamUpItem')
    CommonFunction.ClearGridChild(self.uiGridTeamUpItem.transform)
    local unLockdata = GameSystem.Instance.TeamLevelConfigData:GetUnLockdata(self.mChoosedLevel)
    if unLockdata ~= nil then
        local icons = unLockdata.unlock_icon
        local des = unLockdata.unlock_describe
        local links = unLockdata.link
        local subId = unLockdata.subId
        local size = icons.Count
        if size ~= des.Count then
            error("icon and des size not support, please check the config.")
        end

        for i = 0, size - 1 do
            local t = getLuaComponent(createUI("TeamUpItem",self.uiGridTeamUpItem.transform))
            t:SetData(links:get_Item(i),subId:get_Item(i),  icons:get_Item(i), des:get_Item(i))
            -- t.onClick = self:ClickTeamUpItem()
        end
    end
    self.uiGridTeamUpItem.repositionNow = true
end

function UITaskLevel:ClickTeamUpItem()
    return function(item)
        self:ClickTeamUpOK()()
    end
end

function UITaskLevel:ClickTeamUpOK()
    return function ()
        if self.uiAnimator then
            self:AnimClose()
        else
            self:OnClose()
        end
    end
end

function UITaskLevel:RefreshTaskList( ... )
    -- print('RefreshTaskList')

    table.sort(self.taskList, UITaskLevel.TaskSortFunc)
    -- self:ShowUnclaimedTips()
    CommonFunction.ClearGridChild(self.uiGridTaskItem.transform)
    local taskCount = 0
    for k, v in pairs(self.taskList or {}) do
        if v.type == TaskType.LEVEL then
            -- print('need '..v.cond_info.condition_need..',now '..self.mChoosedLevel)
            local taskConfig = GameSystem.Instance.TaskConfigData:GetTaskLevelInfoByID(v.id)
            if taskConfig then
                local  value = GameSystem.Instance.TaskConfigData:GetTaskPreConditionValueById(v.id)
                -- print('precondition count '..#preconditions)
                if value == self.mChoosedLevel and value<=MainPlayer.Instance.Level then
                    local go = createUI('TaskLevelItem', self.uiGridTaskItem.transform)
                    go.transform.name = v.id
                    if go == nil then
                        Debugger.Log('-- InstantiateObject falied ')
                        return
                    end
                    local taskItem =getLuaComponent(go)
                    taskItem:SetData(v)
                    taskItem:SetParent(self)
                    taskItem:SetDragSV(self.uiGridTaskItem.transform.parent:GetComponent('UIScrollView'))
                    taskCount = taskCount + 1
                end
            end
        end
    end
    self.uiGridTaskItem.repositionNow = true
    self.uiGridTaskItem.transform.parent:GetComponent('UIScrollView'):ResetPosition()
    print('task length '..taskCount)
    if taskCount> 0 then
        NGUITools.SetActive(self.uiEmptyText.gameObject, false)
    else
        NGUITools.SetActive(self.uiEmptyText.gameObject, true)
        if self.mChoosedLevel <= MainPlayer.Instance.Level then--当前等级没有任务
            self.uiEmptyText.transform:GetComponent('UILabel').text = CommonFunction.GetConstString("STR_GROWTH_LINK010")
        else     --当前选中等级任务不可见
            self.uiEmptyText.transform:GetComponent('UILabel').text = CommonFunction.GetConstString("STR_GROWTH_LINK009")
        end
    end
end
function UITaskLevel.TaskSortFunc(task1, task2)
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

function UITaskLevel:SetRedDot(name)

end
--
function UITaskLevel:GetTaskAwardResp(award_id)
    return function (message)
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
        -- self.banTwice = true
        --UITask.getAward = true
        --CommonFunction.ShowPopupMsg(getCommonStr("RECEIVE_SUCCESS"),nil,nil,nil,nil,nil)
        local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(award_id)
        local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
        for i = 0, awardConfig.awards.Count - 1 do
            getGoods:SetGoodsData(awardConfig.awards:get_Item(i).award_id,awardConfig.awards:get_Item(i).award_value)
        end
        getGoods.onClose = function ( ... )
            -- self.banTwice = false
        end

        local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
        while enum:MoveNext() do
            local itemid = enum.Current.id
            if itemid == resp.id then
                enum.Current.state = 3      --设置为已领取状态
                print('task level enum.Current.state change ========= ' .. enum.Current.state)
            end
        end
        UpdateRedDotHandler.MessageHandler("UITaskLevel")
        if resp.type == 3 then
            self.uiGridTaskItem.repositionNow = true
            self.uiGridTaskItem.transform.parent:GetComponent('UIScrollView'):ResetPosition()
        else
            self.uiGridAwardLevel.repositionNow = true
            self.uiGridAwardLevel.transform.parent:GetComponent('UIScrollView'):ResetPosition()
        end

        self:DecreUnclaimedInfo()
        self:ShowUnclaimedTips()


        TaskRespHandler.isOpen = false
        local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = resp.type,--self.taskType,
        }
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)
        CommonFunction.ShowWait()
        print(self.uiName, "Send task info req")
    end
end
--递减未处理任务数量信息
function UITaskLevel:DecreUnclaimedInfo(taskType)
    if self.unclaimedInfo.taskType then
        self.unclaimedInfo.taskType = self.unclaimedInfo.taskType - 1
        if self.unclaimedInfo.taskType <= 0 then
            self.unclaimedInfo.taskType = nil
        end
    end
end
function UITaskLevel:DoClose( ... )
    print('UITaskLevel DoClose')
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        TopPanelManager:HideTopPanel()
    end

end
function UITaskLevel:ClickGrowth( ... )
    return function ()
        -- print('UITaskLevel ClickGrowth')

        if self.mUIState == 0 then
            return
        end

        self.mUIState = 0
          local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = 6, -- 成长任务 Task
        }
        CommonFunction.ShowWait()
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

        print('UITaskLevel TaskInfoReqID')
        -- TaskRespHandler.parent = self

    --等级奖励状态请求
        req = {
        }
        CommonFunction.ShowWait()
        msg = protobuf.encode("fogs.proto.msg.LevelAwardStateReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.LevelAwardStateReqID, msg)
        -- TaskLevelData.LevelStateRefreshCallBack = self:StateRefreshCallBack()
        print('UITaskLevel LevelAwardStateReqID')

    end
end
function UITaskLevel:ClickDaily( ... )
    return function ()
        if self.mUIState == 1 then return end
        self.mUIState = 1
        print('UITaskLevel ClickDaily')
         local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = 3, -- 日常任务 Task
        }
        CommonFunction.ShowWait()
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

        TaskRespHandler.parent = self

    end
end
function UITaskLevel:TaskFinishExitHandler()
    return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.NotifyTaskFinish",buf)
        if resp then
            local id = resp.task_info.id
            local state = resp.task_info.state
            if resp.task_info.type == 6 then --等级任务
                for k, v in pairs(self.taskList) do
                if v.id == id then
                    v.state = state
                    for kk, vv in pairs(v.cond_info) do
                        -- vv.condition_need = 1
                        vv.condition_cur =  vv.condition_need
                    end

                end
            end
            elseif resp.task_info.type == 3 then -- 日常任务
                for k, v in pairs(self.dailyList) do
                    if v.id == id then
                        v.state = state
                         for kk, vv in pairs(v.cond_info) do
                            -- vv.condition_need = 1
                            vv.condition_cur =  vv.condition_need
                        end
                    end
                end
            else
                return
            end


        end
    self:Refresh()
    end
end
function UITaskLevel:ClickBack()
    return function(go)

        print('UITaskLevel ClickBack')
        self.nextShowUI = "UIHall"
        self:DoClose()
    end
end
function UITaskLevel:OnClose()

    if self.onClose then
        self.onClose()
        self.onClose = nil
        -- NGUITools.Destroy(self.gameObject)
        return
    end

    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        TopPanelManager:HideTopPanel()
    end
    -- NGUITools.Destroy(self.gameObject)
end

function UITaskLevel:OnDestroy()

    -- Object.Destroy(self.uiAnimator)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyTaskFinishID, self.uiName)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end
--无用接口
function UITaskLevel:SetModelActive( ... )
    -- body
end
return UITaskLevel
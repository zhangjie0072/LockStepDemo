--encoding=utf-8

NewComerTrial =
{
	uiName = 'NewComerTrial',
	-----------------parameters
	day,
	onClose,
	parent,
	TotalScore,
	--------------------UI
	uiGrid,
	uiProcess,
	uiPercentLabel,
	uiCloseGrid,
	uiButtonGray,
	uiButtonGrayAnimator,
	uiTitle,
	uiAimator,
	uiRoleModel,
	uiCoka = nil,
	uiPrcLight,
	uiLightEffect,
	uiDayButton = {},
	uiRedDot = {},
	RealDay,
	taskList,
	isFromHallToTrial = true,
	Started = false,
	NextAnimationScore = 0,
	AnimExcecuting = false,
	AnimationTweenFinishFlag = 0,
	CurrentClickedChild = nil,
	DelayShowAwardId = nil,
	--延时更新
	m_bDirty = false,
}

--延时更新
function NewComerTrial:Dirty( )
	-- body
	if not self.m_bDirty then
		self.m_bDirty = true
   	 	Scheduler.Instance:AddFrame(1, false, self:LateRefresh())
	end
end
function NewComerTrial:LateRefresh(  )
	-- body
	return function ( )
		self.m_bDirty  = false
		-- body
		self:RefreshRedDot()
	end
end
--延时更新 end
-----------------------------------------------------------------
function NewComerTrial:Awake( ... )
	for i = 1 , 7 do
		table.insert(self.uiDayButton,i,self.transform:FindChild("Window/Right/Up/" .. i))
		addOnClick(self.uiDayButton[i].gameObject, self:ClickItem(i))
	end
	self.uiRedDot = {}
	for i = 1 , 7 do
		table.insert(self.uiRedDot,i,self.transform:FindChild("Window/Right/Up/" .. i .. "/RedDot"))
	end
	self.uiGrid = self.transform:FindChild("Window/Right/Scroll/Grid"):GetComponent("UIGrid")
	self.uiProcess = self.transform:FindChild("Window/Left/Process"):GetComponent("UIProgressBar")
	self.uiPrcLight = self.transform:FindChild("Window/Left/Process/Light"):GetComponent("UIProgressBar")
	self.uiLightEffect = self.transform:FindChild("Window/Left/Process/ThumbLight")
	self.uiPercentLabel = self.transform:FindChild("Window/Left/Num"):GetComponent("UILabel")
	self.uiCloseGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiButtonGray = self.transform:FindChild("Window/Left/ButtonGray")
	self.uiButtonGrayAnimator = self.transform:FindChild("Window/Left/Ef_Button1").gameObject
	-- self.uiTitle = self.transform:FindChild("Window/Left/Text"):GetComponent("UILabel")
	self.uiAimator = self.transform:GetComponent("Animator")
	self.uiRoleModel = self.transform:FindChild("Window/Left/Model/ModelShowItem"):GetComponent("ModelShowItem")
	self.uiCoka = self.transform:FindChild("Window/Left/Cola"):GetComponent('UITweener')

	if self.uiCoka then
		self.uiCoka:AddOnFinished(LuaHelper.Callback(self:TweenScaleFinished()))
	else
		warning('can\'t find TweenScale on Cola!')
	end
	addOnClick(self.uiButtonGray.gameObject, self:ClickReceive())
	--msg
end

function NewComerTrial:Start( ... )
	self.uiButtonClose = getLuaComponent(createUI("ButtonBack", self.uiCloseGrid.transform))
	self.uiButtonClose.onClick = self:OnCloseClick()
	--参数初始化
	local count = 0
	local signList = MainPlayer.Instance.NewComerSign.sign_list
	local enum = signList:GetEnumerator()
	while enum:MoveNext() do
		count = count + 1
	end
	local create_time = MainPlayer.Instance.CreateTime
    local now = os.date("*t", GameSystem.Instance.mTime).yday
    local create = os.date("*t", create_time).yday
    local createY =os.date("*t", create_time).year
    local curDay  = now-create
	print('current day 1 '..curDay..',create at '..create..',today is '..now)
    if curDay < 0 then
        local daysOfCreateYear = 365
        if createY %100 == 0 then
            if createY%400 == 0 then
                daysOfCreateYear = 366
            end
        elseif createY%4 == 0 then
            daysOfCreateYear = 366
        end
        curDay = daysOfCreateYear - create+now
    end
    curDay = curDay + 1
	print('current day '..curDay)
	if curDay >= 8 then
		count = 8
	end
	self.day = count
	self.RealDay = count
	for i = 1 , 7 do
		local sp = self.uiDayButton[i]:FindChild('icon'):GetComponent('UISprite')
		local sel = self.uiDayButton[i]:FindChild('Sele/icon'):GetComponent('UISprite')
		if sp then
			sp.spriteName = 'sign_'..i
		end
		if sel then
			sel.spriteName = 'sign_'..i
		end
		if i == self.day then
			sp.spriteName = 'sign_today'
			sp:MakePixelPerfect()
			sel.spriteName = 'sign_today'
			sel:MakePixelPerfect()
		end
	end
	local data = GameSystem.Instance.trialConfig:GetTrialDataByIndex(8, 1)
	self.TotalScore = data.score
	local ScoreTemp = MainPlayer.Instance.trialTotalScore
	local ScoreMax = self.TotalScore
	if count <= 7 then
		NGUITools.SetActive(self.uiButtonGray.gameObject,false)
		self.uiButtonGray.transform:GetComponent("UIButton").isEnabled = false
	elseif ScoreTemp>=ScoreMax or count==8 then
		NGUITools.SetActive(self.uiButtonGray.gameObject,true)
		self.uiButtonGray.transform:GetComponent("UIButton").isEnabled = true
		-- NGUITools.SetActive(self.uiPercentLabel.gameObject,false)
		local reddot = self.uiButtonGray.transform:FindChild('RedDot')
		if reddot then
			NGUITools.SetActive(reddot.gameObject,true)
		end
	end
	NGUITools.SetActive(self.uiLightEffect.gameObject,false)

	--更新球员信息
	local data = GameSystem.Instance.trialConfig:GetTrialDataByIndex(8, 1)
	local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(data.award_id)
  	if awardConfig then
	    --3D模型展示
		self.uiRoleModel.Rotation = true
	    self.uiRoleModel.ModelID = awardConfig.awards:get_Item(0).award_id
	end

	NewComerTrialData.NotifyCallBack = self:ScheduleChangeNotify()
	NewComerTrialData.TaskFinishNotifyCallBack = self:TaskFinishExitHandler()
	self:RefreshScore()
	self:Refresh()
    self.isFromHallToTrial = false
    self.Started = true

end

function NewComerTrial:Refresh(  )
	self:Dirty()
end
function NewComerTrial:TaskFinishExitHandler()
    return function(buf)
        local resp, error = protobuf.decode("fogs.proto.msg.NotifyTaskFinish",buf)

        if resp then
            if resp.task_info.type ~= 2 then return end
            local id = resp.task_info.id
            local state = resp.task_info.state
            -- print('UITask:TaskFinishExitHandler id '..id..',state '..state)
            for k, v in pairs(self.taskList or {}) do
                if v.id == id then
                    v.state = state
                     for kk, vv in pairs(v.cond_info) do
                        vv.condition_cur = vv.condition_need
                        -- print('state id '..v.id..',state '..v.state)
                    end
                end
            end
        end
        self:SetAcivity(self.day)
    end
end

function NewComerTrial:OnDisable()
	-- body
end

function NewComerTrial:OnEnable()
	-- body
	-- print('OnEnable NewComerTrial')
	Scheduler.Instance:AddFrame(2, false, self:RefreshOnActive())
	self.AnimationTweenFinishFlag = 0
	self.AnimExcecuting = false
end

function NewComerTrial:RefreshOnActive(  )
	-- body
	return function ( go )
		if self.gameObject.activeSelf and self.Started then

			if  self.isFromHallToTrial then
				--从大厅进入重新刷新数据
				local day = self.RealDay
				if day >= 8 then 
					day  = 7 
				end
				self:SetAcivity(day)
				self.isFromHallToTrial = false

			else
				--如果不是从大厅重新刷新选择天的数据
				self:SetAcivity(self.day)
			end
		else
			Scheduler.Instance:AddFrame(1, false, self:RefreshOnActive())
		end

	end
end


function NewComerTrial:SetParent( parent )
	-- body
	self.parent = parent
end

function NewComerTrial:RefreshRedDot(day)
	if day then
		NGUITools.SetActive(self.uiRedDot[day].gameObject, false)
		if self.RealDay < day then
			return
		end
		local dic = GameSystem.Instance.trialConfig:GetTrialListByDay(day)
		local enum = dic:GetEnumerator()
		while enum:MoveNext() do
			local task
			local reddot = false
			for k,v in pairs(self.taskList or {}) do
				if v.id == enum.Current.id then
					if v.state == 2 then
						NGUITools.SetActive(self.uiRedDot[day].gameObject, true)
						reddot = true
						break
					end
				end
			end
			if reddot then
				break
			end
		end
	else
		for i=1,7 do
			NGUITools.SetActive(self.uiRedDot[i].gameObject, false)
		end
		local endday = self.RealDay
		if endday > 7 then
			endday = 7
		end
		for i= 1,endday do
			local dic = GameSystem.Instance.trialConfig:GetTrialListByDay(i)
			local enum = dic:GetEnumerator()
			while enum:MoveNext() do
				local task
				local reddot = false
				for k,v in pairs(self.taskList or {}) do
					if v.id == enum.Current.id then
						if v.state == 2 then
							NGUITools.SetActive(self.uiRedDot[i].gameObject, true)
							reddot = true
							break
						end
					end
				end
				if reddot then
					break
				end
			end
		end
	end
end

function NewComerTrial:SetAcivity(day)
	--显示当前选中按钮状态
	if day >= 8 then
		day = 7
	end
	self.day = day
	--进入界面设置当前选中项
	--重新进入界面刷新选中项[重新进入界面后显示会错乱]
	self.uiDayButton[day].transform:GetComponent('UIToggle').value = true
	local tasksToday = {}
	local dic = GameSystem.Instance.trialConfig:GetTrialListByDay(day)
	local enum = dic:GetEnumerator()
	local idx = 1
	while enum:MoveNext() do

		local schedule  = {condition_cur,condition_need,}
		local taskCell = {
					index,
					day,
					showProgress,
					id,
					schedule,
					state,

				}
		-- print(self.uiName,"----index:",enum.Current.index,"----day:",day)
		taskCell.index = enum.Current.index
		taskCell.day = day
		taskCell.showProgress = enum.Current.showProgress
		taskCell.id = enum.Current.id
		local task
		for k,v in pairs(self.taskList or {}) do
			if v.id == enum.Current.id then
				task = v
				break
			end
		end
		if task then
			schedule.condition_need = task.cond_info[1].condition_need
			schedule.condition_cur = task.cond_info[1].condition_cur
			taskCell.schedule = schedule
			taskCell.state = task.state
			-- print('task id in taskList is '..task.id..',confid task id is '..enum.Current.id..',task state is '..task.state..',schedule cur '..schedule.condition_cur..',schedule need '..schedule.condition_need)
		else
			taskCell.state = 3 -- 服务器未下发数据的任务是已完成的任务
		end
		table.insert(tasksToday,idx,taskCell)
		idx = idx + 1
	end

    table.sort(tasksToday, NewComerTrial.TaskSortFunc)
	--刷新列表
	CommonFunction.ClearGridChild(self.uiGrid.transform)
	for k,v in pairs(tasksToday or {}) do
		local item = getLuaComponent(createUI("TrialItem", self.uiGrid.transform))
		item.index = v.index
		item.day = v.day
		item.showProgress = v.showProgress
		item.parent = self
		item.id = v.id
		item.schedule = v.schedule
		item.state = v.state
	end

	self.uiGrid.repositionNow = true
	self:RefreshRedDot(day)

	-- self.uiGrid:GetComponent("UIGrid"):Reposition()
end
--
function NewComerTrial.TaskSortFunc(task1, task2)
    if task1 == nil then
        return 1
    end
    if task2 == nil then
        return -1
    end
    if task1.state == TaskState.RUNNING and task2.state == TaskState.RUNNING then
        local schedule1 = task1.schedule.condition_cur / task1.schedule.condition_need
        local schedule2 = task2.schedule.condition_cur / task2.schedule.condition_need
        if schedule1 > 1 then
            schedule1 = task1.schedule.condition_need / task1.schedule.condition_cur
        end
        if schedule2 > 1 then
            schedule2 = task2.schedule.condition_need / task2.schedule.condition_cur
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
    elseif task1.state == TaskState.RUNNING and task2.state == TaskState.AWARDED then
        return true
    elseif task1.state == TaskState.FINISH and task2.state == TaskState.AWARDED then
        return true
    elseif task2.state == TaskState.RUNNING and task1.state == TaskState.AWARDED then
        return false
    elseif task2.state == TaskState.FINISH and task1.state == TaskState.AWARDED then
        return false
    else
        return task1.id < task2.id
    end
end

function NewComerTrial:GetTaskRewardHandler(award_id)
	return function(message)
	 	LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetTaskAwardRespID, self.uiName)
        local resp, err = protobuf.decode('fogs.proto.msg.GetTaskAwardResp', message)
        if resp == nil then
            Debugger.LogError('------GetTaskAwardResp error: ', err)
            return
        end

        if resp.result ~= 0 then
            Debugger.Log('-----------1: {0}', resp.result)
            -- CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
            return
        end
        self.banTwice = true
		local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
        while enum:MoveNext() do
            local itemid = enum.Current.id
            if itemid == resp.id then
                enum.Current.state = 3		--设置为已领取状态
                print('enum.Current.state change ========= ' .. enum.Current.state)
            end
        end

        if  self.AnimExcecuting then
        	self.DelayShowAwardId = {
	        	awardId = award_id,
	        	id = resp.id,
        	}
        	-- print('getAward and wait animation')
        else
        	self:ShowRewardPanel(award_id,resp.id)
        	self.DelayShowAwardId = nil
        	-- print('getAward and start panel')
        end

        -- print(self.uiName, "Send task info req")
	end
end
function NewComerTrial:ShowRewardPanel( award_id ,id)
	-- body
	 local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(award_id)
        local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
        for i = 0, awardConfig.awards.Count - 1 do
            getGoods:SetGoodsData(awardConfig.awards:get_Item(i).award_id,awardConfig.awards:get_Item(i).award_value)
        end
        getGoods.onClose = function ( ... )
            self.banTwice = false
            self:AwardGot()
        end
		for k,v in pairs(self.taskList or {}) do
			if v.id == id then
				v.state = TaskState.AWARDED
				table.remove(self.taskList,k)
				break
			end
		end

		self:SetAcivity(self.day)
end
function NewComerTrial:OnClose( ... )

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
end

function NewComerTrial:OnDestroy( ... )
	-- body
	NewComerTrialData.NotifyCallBack = nil
	NewComerTrialData.TaskFinishNotifyCallBack = nil
end

function NewComerTrial:ClickItem(i)
	return function()
		-- print(self.uiName,"----day:",self.day,"---i:",i)
        if not FunctionSwitchData.CheckSwith(FSID.active7_task) then return end

		if i == self.day then
			return
		end
		self:SetAcivity(i)
	end
end

function NewComerTrial:ClickAnimation(transform)
	-- body
	self.CurrentClickedChild = transform
end
function NewComerTrial:TweenScaleFinished( ... )
	-- body
	return function ( )
		-- body
		-- print('TweenScaleFinished '..self.AnimationTweenFinishFlag)
		if self.AnimationTweenFinishFlag == 2 then
			--todo 动画
			self.AnimationTweenFinishFlag = 0
			-- print('TweenScaleFinished time to play progress bar animation')
			self:RefreshScore(true)
		else
			self.AnimationTweenFinishFlag = self.AnimationTweenFinishFlag + 1
		end
	end
end
function NewComerTrial:ClickReceive()
	return function ()
        if not FunctionSwitchData.CheckSwith(FSID.active7_final) then return end

		if self.uiProcess.value == 1 then
			self:SendAwardsMsg("role")()
		else
			--如果有未领取任务进行提醒
			local unreward = false
			for k,v in pairs(self.taskList or {}) do
				print('task id '..v.id..',type '..v.type..',state '..v.state)
			end
			for k,v in pairs(self.taskList or {}) do
				if v.state == 2 and v.type == 7 and v.id ~=7000 then
					unreward  = true
					break
				end
			end
			if unreward then
				CommonFunction.ShowPopupMsg(getCommonStr("STR_TRIAL_WARNING"),nil,nil,nil,nil,nil)
				return
			end
			local award_id = GameSystem.Instance.trialConfig:GetTrialDataByIndex(8, 1).award_id
			local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(award_id)
	      	if awardConfig then
			else
				return
			end
			local lua = getLuaComponent(createUI("NewComerTrialPop",self.transform))
			lua.transform.localPosition = Vector3.New(self.transform.localPosition.x, self.transform.localPosition.y, -500)
			local TrialItemID = GameSystem.Instance.CommonConfig:GetUInt('gTrialItemID')
			local gTrialcostID = GameSystem.Instance.CommonConfig:GetUInt('gTrialcostID')
			local ScoreToPicRate = GameSystem.Instance.CommonConfig:GetFloat('gTrialItemcount')
			local DiaToScoreRate = GameSystem.Instance.CommonConfig:GetFloat('gTrialcostprice')
			lua.lbl1.text = tostring(math.floor((MainPlayer.Instance.trialTotalScore ) * ScoreToPicRate))
			local contentStr  =  getCommonStr('STR_BUY')
			local goodsAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(gTrialcostID)
			if goodsAttr then
				contentStr =  string.format(getCommonStr('STR_FIELD_Trailbutton02'),goodsAttr.name)
			end
			lua.uiLblCostStr.text = contentStr
			local iconRole =  getLuaComponent(createUI("CareerRoleIcon", lua.icon2.transform))
			--role icon
			local data = GameSystem.Instance.trialConfig:GetTrialDataByIndex(8, 1)
			local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(data.award_id)
		  	if awardConfig then
				iconRole.id = awardConfig.awards:get_Item(0).award_id
			end
		    iconRole.showPosition = false
		    iconRole.displayLevel = false
		    iconRole:SetIsMaster(false)

		    --goods icon
            local goods = getLuaComponent(createUI('GoodsIcon', lua.icon1.transform))
			goods.goodsID = TrialItemID
			goods.hideNeed = true
			goods.hideLevel = true
			goods.hideNum = true

			--diamond icon
			local gold = createUI("GoodsIconConsume", lua.iconGold.transform)
		    local BuyGold = getLuaComponent(gold)
			BuyGold.isAdd = false
			local Num = (self.TotalScore - MainPlayer.Instance.trialTotalScore ) * DiaToScoreRate
			BuyGold:SetData(gTrialcostID--[====[钻石ID]====],Num )
			BuyGold.isBG =false

			lua.onClickbtn = function (awardtype)
				if awardtype == "fragment" then
					self:SendAwardsMsg(awardtype)()
				elseif awardtype == "diamont" then
					self:SendAwardsMsg(awardtype)()
				end
			end
		end
	end
end

function NewComerTrial:SendAwardsMsg(AwardType)
	return function()
		print("send get tatol")
		local req = {
				force_get = 0,
		}
		if AwardType == "diamont" then
			req.force_get = 1
		end
		local msg = protobuf.encode("fogs.proto.msg.GetNewComerTrialAwardsReq", req)
		self.isTotal = true
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.GetNewComerTrialAwardsReqID, msg)
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetNewComerTrialAwardsRespID, self:GetAwardsHandler(), self.uiName)
	end
end

function NewComerTrial:GetAwardsHandler()
	return function(buf)
		print("getAwards----resp")
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.GetNewComerTrialAwardsResp", buf)
		if resp then
			if resp.result == 660 then
				local message = "STR_Trial_GET_FORCE"
				self.msg = CommonFunction.ShowPopupMsg(message, nil, LuaHelper.VoidDelegate(self:SendForceReq()), LuaHelper.VoidDelegate(self:FramClickClose()),getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL"))
			elseif resp.result == 0 then
				local role,popup
				for _,v in ipairs(resp.awards) do
					if v.id ~= 0 then
						local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id)
						if roleConfig then
							local roleAcquireLua = getLuaComponent(createUI('RoleAcquirePopupNew')) --TopPanelManager:ShowPanel("RoleAcquirePopupNew", nil, {id = awardConfig.awards:get_Item(0).award_id})
							NGUITools.SetActive(self.gameObject,false)
							roleAcquireLua.id = v.id
							roleAcquireLua:SetData(v.id)
							roleAcquireLua.onBack = function ( )
								-- NGUITools.Destroy(go)
								self:OnClose()
							end

						else
							if popup == nil then
								popup = getLuaComponent(createUI('GoodsAcquirePopup'))
							end
							popup:SetGoodsData(v.id,v.value)
						end
					end
				end
				if role then
					UIManager.Instance:BringPanelForward(role.gameObject)
				end
				if self.isTotal and self.isTotal == true then
					MainPlayer.Instance.trialFlag = 1
					if popup then
						popup.onClose = self:OnCloseClick()
					elseif role then
						role.onCloseClick = self:OnCloseClick()
					end
				end
				self:RefreshScore()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
			end
		else
			error("NewComerTrial:", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetNewComerTrialAwardsRespID, self.uiName)
	end
end

function NewComerTrial:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end
end

function NewComerTrial:SendForceReq()
	return function ()
		local req = {
			type = "NCTAT_TOTAL",
			force_get = 1,
		}
		local msg = protobuf.encode("fogs.proto.msg.GetNewComerTrialAwardsReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetNewComerTrialAwardsReqID, msg)
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetNewComerTrialAwardsRespID, self:GetAwardsHandler(), self.uiName)
		CommonFunction.ShowWait()
	end
end
--获得道具后的回掉
function NewComerTrial:AwardGot( )
	-- body
	print('got reward')
end
--任务完成后的通知消息，保存分数信息
function NewComerTrial:ScheduleChangeNotify()
	-- body
	return function ( resp )
		MainPlayer.Instance.trialFlag = resp.awards_flag
		--动画方式更新进度
		self.NextAnimationScore = resp.total_score
		if self.CurrentClickedChild then
			self.AnimExcecuting = true
			local startPos = self.CurrentClickedChild.position
			self.uiCoka:SendMessage('StartAtPosition',startPos)
			self.CurrentClickedChild = nil

		end
	end
end

function NewComerTrial:OnCloseClick( ... )
	return function (go)
		--关闭界面后当前天数重新设置
		self.isFromHallToTrial = true
		-- self:SetAcivity(self.RealDay)
		local day = self.RealDay	
		if day >= 8 then
			day = 7
		end
		--进入界面设置当前选中项
		--重新进入界面刷新选中项[重新进入界面后显示会错乱]
		if self.uiAimator then
			self:AnimClose()
		else
			self:OnClose()
		end
		self.uiDayButton[day].transform:GetComponent('UIToggle').value = true
	end
end
function NewComerTrial:RefreshScore(withAnim)
	--   添加进度条移动动画
	if withAnim then
		self:ProcessWithAnim()
		return
	end
	self.uiProcess.value = MainPlayer.Instance.trialTotalScore / self.TotalScore
	self.uiPercentLabel.text = string.format(getCommonStr('STR_NEED_COLA'), self.TotalScore- MainPlayer.Instance.trialTotalScore)
	if self.uiProcess.value == 1 or MainPlayer.Instance.NewComerSign.open_flag == 0 then
		self.uiButtonGray.transform:GetComponent("UIButton").isEnabled = true
		NGUITools.SetActive(self.uiButtonGrayAnimator, true)
		NGUITools.SetActive(self.uiButtonGray.gameObject,true)
	else
		NGUITools.SetActive(self.uiButtonGrayAnimator, false)
		NGUITools.SetActive(self.uiButtonGray.gameObject,false)
	end

end
--[[
	当扔瓶子动画结束后，执行进度条上光栅和进度条的动画
	当光栅动画完成后执行进度条动画
]]--
function NewComerTrial:ProcessWithAnim(  )
	-- body
	--正在执行动画时
	local score = self.NextAnimationScore

	local deltScore =	0.3 	--进度条执行动画的速度（分数）   单位 分/帧
	local lightDeltValue = 0.1 --光栅执行动画的速度   单位 %/帧
	local ScoreTemp = MainPlayer.Instance.trialTotalScore
	local ScoreMax = self.TotalScore
	local  function excecuteProgressBar()
		-- body
		ScoreTemp  = ScoreTemp +deltScore
		self.uiProcess.value = ScoreTemp / ScoreMax
		if self.uiProcess.value == 1 or MainPlayer.Instance.NewComerSign.open_flag == 0 then
			self.uiButtonGray.transform:GetComponent("UIButton").isEnabled = true
			NGUITools.SetActive(self.uiButtonGrayAnimator, true)
			NGUITools.SetActive(self.uiButtonGray.gameObject,true)
			local reddot = self.uiButtonGray.transform:FindChild('RedDot')
			if reddot then
				NGUITools.SetActive(reddot.gameObject,true)
			end
		else
			NGUITools.SetActive(self.uiButtonGrayAnimator, false)
			NGUITools.SetActive(self.uiButtonGray.gameObject,false)
		end
		-- print('current process '..self.uiProcess.value)
		if ScoreTemp >= score then
			MainPlayer.Instance.trialTotalScore = score
			-- print('process over')
			self.uiPercentLabel.text = string.format(getCommonStr('STR_NEED_COLA'), ScoreMax- score)
			self.AnimExcecuting = false
			--显示领奖信息
			if self.DelayShowAwardId~=nil then
				if self.DelayShowAwardId.awardId>0 then
					self:ShowRewardPanel(self.DelayShowAwardId.awardId,self.DelayShowAwardId.id)
					self.DelayShowAwardId = nil
				end
			end
			return
		else
			Scheduler.Instance:AddFrame(1,false,excecuteProgressBar)
		end

	end
	NGUITools.SetActive(self.uiLightEffect.gameObject,true)
	self.uiPrcLight.value = 0
	local function excecuteLight( )
		-- body
		self.uiPrcLight.value = self.uiPrcLight.value +lightDeltValue
		if self.uiPrcLight.value >= self.uiProcess.value then
			Scheduler.Instance:AddFrame(1,false,excecuteProgressBar)
			NGUITools.SetActive(self.uiLightEffect.gameObject,false)
			-- print('process light over')
		else
			Scheduler.Instance:AddFrame(1,false,excecuteLight)
		end


	end
	Scheduler.Instance:AddFrame(1,false,excecuteLight)
end
function NewComerTrial:CheckRoleInclude(id)
	print("include id :",id)
	return MainPlayer.Instance:HasRole(id)
end

function NewComerTrial:TaskFinishExitHandler()
    return function(resp)
    	print('TaskFinishExitHandler NewComerTrial')
        if resp then
            local id = resp.task_info.id
            local state = resp.task_info.state
            if resp.task_info.type == 7 then --等级任务
                for k, v in pairs(self.taskList) do
	                if v.id == id then
	                    v.state = state
	                    for kk, vv in pairs(v.cond_info) do
	                        vv.condition_need = 1
	                        vv.condition_cur = 1
	                    end

	                end
           		end
        	end
    		self:Refresh()
    	end
	end
end
return NewComerTrial
--encoding=utf-8

TaskItem =
{
    uiName = 'TaskItem',

    -------------------------------------
    data,
    parent,
    config,

    schedule = 0,
    is_rank,
    clickedLadder,

    -------------------------------------UI
    uiTaskSprite,
    uiIcon,
    uiNameLabel,
    uiProgressLabel,
    uiProgressBar,

    uiDesc,
    uiBackSele,
    --uiBgUp,
    --uiBgDown,
    uiAward,

    uiButton,
    uiButtonRedDot,
    uiButtonBg,
    uiButtonLabel,
    uiButtonShade,

    uiDragSV,

    lblProgressNum,
};


-----------------------------------------------------------------
--Awake
function TaskItem:Awake()
    local transform = self.transform
    self.uiTaskSprite = transform:GetComponent('UISprite')
    --self.BGSprite = transform:FindChild('BGGoldentop'):GetComponent('UISprite')
    --图标
    self.uiIcon = transform:FindChild('Icon'):GetComponent('UISprite')
    --标题
    self.uiNameLabel = transform:FindChild('Name'):GetComponent('UILabel')
    --进度
    self.uiProgressLabel = transform:FindChild('Progress'):GetComponent('UILabel')
    if not self.TaskType == TaskType.LEVEL then
        self.lblProgressNum = transform:FindChild('Progress/Num'):GetComponent('UILabel')
    end
    self.uiProgressBar = transform:FindChild('Progress/ProgressBack'):GetComponent('UIProgressBar')
    --描述
    self.uiDesc = transform:FindChild('Desc'):GetComponent('UILabel')
    --背景
    --self.uiBgUp = transform:GetComponent('UISprite')
    --self.uiBgDown = transform:FindChild('NameBG'):GetComponent('UISprite')
    self.uiBackSele = transform:FindChild('BackSele'):GetComponent('UISprite')
    --奖励
    self.uiAward = transform:FindChild('Award/Grid'):GetComponent('UIGrid')

    --操作
    self.uiButton = transform:FindChild('ButtonGo')
    self.uiButtonRedDot = transform:FindChild('ButtonGo/RedDot')
    self.uiButtonBg = self.uiButton:GetComponent('UIButton')
    self.uiButtonLabel = transform:FindChild('ButtonGo/Text'):GetComponent('MultiLabel')
    self.uiButtonShade = transform:FindChild('ButtonGo/Shade'):GetComponent('UISprite')
    --可领取红点
    --self.redDot = transform:FindChild('Reddot'):GetComponent('UISprite')
    --self.redDot.gameObject:SetActive(false)

    --
    self.uiDragSV = transform:GetComponent('UIDragScrollView')
end

--Start
function TaskItem:Start()
        addOnClick(self.uiButton.gameObject, self:OnOperClick())
end

--Update
function TaskItem:Update( ... )
    -- body
end


-----------------------------------------------------------------
--
function TaskItem:SetData(data)
    --任务数据
    self.data = data
    --配置
    if data.type == TaskType.DAILY then
        self.config = GameSystem.Instance.TaskConfigData:GetTaskDailyInfoByID(data.id)
    elseif data.type == TaskType.MAIN then
        self.config = GameSystem.Instance.TaskConfigData:GetTaskMainInfoByID(data.id)
    elseif data.type == TaskType.LEVEL then
        self.config = GameSystem.Instance.TaskConfigData:GetTaskLevelInfoByID(data.id)
    end
    -- print('----------------task id: ', data.id)
    --图标
    self.uiIcon.spriteName = self.config.icon
    --任务描述
    self.uiDesc.text = self.config.desc
    --富文本颜色
    local strFormatWhite = '[FFFFFF]'
    local strFormatWhiteEnd = '[-]'
    local strFormatGreen = '[B0FD04]'
    local strFormatGreenEnd = '[-]'
    --进度
    if #(data.cond_info) > 0 then
        for k, v in pairs(data.cond_info) do
            -- print('condition_cur' .. v.condition_cur .. 'condition_need' .. v.condition_need)
            if v.condition_cur < v.condition_need then
                self.uiProgressLabel.text = strFormatWhite..v.condition_cur..strFormatWhiteEnd..strFormatGreen.."/" .. v.condition_need..strFormatGreenEnd
            else
                self.uiProgressLabel.text = strFormatGreen..v.condition_cur.."/" .. v.condition_need..strFormatGreenEnd
            end
            --任务名
            self.uiNameLabel.text = self.config.title-- .. '  (' .. v.condition_cur .. "/" .. v.condition_need .. ')'
            if v.condition_need > 0 then
                self.schedule = v.condition_cur / v.condition_need
            end
            if v.condition_id == 5 then
                local z1, y1 = math.modf(v.condition_cur/10)
                local z2, y2 = math.modf(v.condition_need/10)
                if (y1*10 + 0.5) < math.ceil(y1*10) then
                    y1 = math.floor(y1*10)
                else
                    y1 = math.ceil(y1*10)
                end
                if (y2*10 + 0.5) < math.ceil(y2*10) then
                    y2 = math.floor(y2*10)
                else
                    y2 = math.ceil(y2*10)
                end
                if z1 == z2 then
                    self.schedule = y1 / y2
                    if y1 == y2 then
                        self.schedule = 1
                    end

                    if y1 < y2 then
                        self.uiProgressLabel.text = strFormatWhite..y1..strFormatWhiteEnd..strFormatGreen.."/" .. y2..strFormatGreenEnd
                    else
                        self.uiProgressLabel.text = strFormatGreen..y1.."/" .. y2..strFormatGreenEnd
                    end
                    --     if not self.TaskType == TaskType.LEVEL then
                    --         self.uiProgressLabel.text = y1
                    --         self.lblProgressNum.text = "/" .. y2
                    --     else
                    --         self.uiProgressLabel.text = y1.."/" .. y2
                    --     end
                    -- if y2 > y1 then
                    --     self.uiProgressLabel.color = Color.white
                    -- end
                else
                    self.schedule = 0.01
                    -- if not self.TaskType == TaskType.LEVEL then
                    --         self.uiProgressLabel.text = 0
                    --         self.lblProgressNum.text = "/" .. y2
                    -- else
                    --     self.uiProgressLabel.text = "0/" .. y2
                    -- end
                    if y2 > y1 then
                        self.uiProgressLabel.text = strFormatWhite..'0'..strFormatWhiteEnd..strFormatGreen.."/" .. y2..strFormatGreenEnd
                    else
                        self.uiProgressLabel.text = strFormatGreen..'0'.."/" .. y2..strFormatGreenEnd
                    end
                end
            elseif v.condition_id == 29 then
                if v.condition_cur <= v.condition_need then
                    if v.condition_cur ~= 0  then
                        self.schedule = 1
                    else
                        self.schedule = 0
                    end
                else
                    self.schedule = v.condition_need / v.condition_cur
                end
            end
            self.uiProgressBar.value = self.schedule
            break --只取第一个条件显示
        end
    else
        --任务名
        self.uiNameLabel.text = self.config.title

        self.uiProgressLabel.text = strFormatWhite.."0/0"..strFormatWhiteEnd
        -- if not self.TaskType == TaskType.LEVEL then
        --     self.uiProgressLabel.text = '0'
        --     self.lblProgressNum.text = "/0"
        -- else
        --     self.uiProgressLabel.text = "0/0"
        -- end
        self.uiProgressBar.value = 0
    end

    --if self.config.show_process == 1 then
        -- self.uiProgressLabel.text = ''
        -- self.uiNameLabel.text = self.config.title
    --end
    --根据配置决定是否显示任务进度
    if self.config.show_process == 0 then
        self.uiProgressLabel.gameObject:SetActive(false)
    end
    --奖励
    local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(self.config.award_id)
    -- print('get awardConfig by id '..self.config.award_id)

    local item_count = self.uiAward.transform.childCount;
    local index = 0;

    if awardConfig then
        for i = 0, awardConfig.awards.Count - 1 do
            local go = nil
            if index < item_count then
                go = self.uiAward.transform:GetChild(index);
                index = index + 1;
            else
                go = CommonFunction.InstantiateObject('Prefab/GUI/GoodsIconConsume', self.uiAward.transform)
            end

            if go == nil then
                Debugger.Log('-- InstantiateObject falied ')
                return
            end
            local taskAward = getLuaComponent(go)
            taskAward.isTask = true
            taskAward:SetData(awardConfig.awards:get_Item(i).award_id, awardConfig.awards:get_Item(i).award_value, false)

        end
    end

    if data.type == TaskType.DAILY then
        local go = nil
        if index < item_count then
            go = self.uiAward.transform:GetChild(index);
            index = index + 1;
        else
            go = CommonFunction.InstantiateObject('Prefab/GUI/GoodsIconConsume', self.uiAward.transform)
        end

        if go == nil then
            Debugger.Log('-- InstantiateObject falied ')
            return
        end
        local taskAward = getLuaComponent(go)
        taskAward.rewardId = false
        taskAward.uiIcon.spriteName = self["activeIcon"]
        taskAward.uiNum.text = "+" .. self.config.activity
        taskAward.uiNum.color = Color.New(255/255, 96/255, 0, 1)
    end
    self.uiAward.repositionNow = true

    -- print('--------------self.schedule: ', self.schedule)

    local r, g, b = 241, 217, 165		--任务完成rgb值
    if self.schedule and self.schedule > 0.9999999 then
        --self.uiTaskSprite.color = Color.New(r/255, g/255, b/255, 1)
        --self.uiTaskSprite.spriteName = ''
        self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
        --self.redDot.gameObject:SetActive(true)

        --self.uiBgUp.color = Color.New(255/255,230/255,90/255,1)
        --self.uiBgDown.color = Color.New(214/255,193/255,108/255,1)
        self.uiBackSele.gameObject:SetActive(true)
        -- NGUITools.SetActive(self.uiButtonRedDot.gameObject, true)
        self.uiButtonBg.normalSprite = 'com_button_newyellow02'
        self.uiButtonShade.gameObject:SetActive(false)
    else
        local ur, ug, ub = 241, 217, 165	--任务未完成rgb值
        --self.uiTaskSprite.color = Color.New(ur/255, ug/255, ub/255, 1)
        self.uiButtonLabel:SetText(CommonFunction.GetConstString('STR_LINK'))
        --self.redDot.gameObject:SetActive(false)
        self.uiBackSele.gameObject:SetActive(false)
        self.uiButtonShade.gameObject:SetActive(true)
        -- NGUITools.SetActive(self.uiButtonRedDot.gameObject, false)
        self.uiButtonBg.normalSprite = 'com_button_newgary02'
        self.uiButtonShade.spriteName = 'com_button_newgary02'
        --self.uiBgUp.color = Color.New(241/255,217/255,165/255,1)
        --self.uiBgDown.color = Color.New(210/255,170/255,83/255,1)
    end

    if #(data.cond_info) > 0 and data.type == TaskType.MAIN then
        --没达到指定等级不显示按钮
        for k, v in pairs(data.cond_info) do
            if v.condition_id == 6 then
                if v.condition_need > MainPlayer.Instance.Level then
                    --self.uiTaskSprite.color = Color.New(r/255, g/255, b/255, 1)

                    --self.uiBgUp.color = Color.New(241/255,217/255,165/255,1)
                    --self.uiBgDown.color = Color.New(210/255,170/255,83/255,1)
                    self.uiBackSele.gameObject:SetActive(false)
                    self.uiButtonLabel:SetText(CommonFunction.GetConstString('STR_FINISHED_NOTYET'))
                    self.uiButton:GetComponent('UIButton').isEnabled = false
                    self.uiButton.gameObject:SetActive(false)
                    --self.redDot.gameObject:SetActive(false)
                    break
                end
            end
        end
    end

    local uiName = GameSystem.Instance.TaskConfigData:GetTaskLinkUIName(self.data.id)
    if uiName == "" and self.schedule ~= 1 then
        self.uiButton.gameObject:SetActive(false)
    end
end

--
function TaskItem:SetParent(parent)
    self.parent = parent
end

--
function TaskItem:SetDragSV(scrollView)
    self.uiDragSV.scrollView = scrollView
end

--
function TaskItem:OnOperClick()
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.achievement_btn) or
            not FunctionSwitchData.CheckSwith(FSID.daily_btn)
        then
            return
        end

        if self.schedule and self.schedule > 0.99 then --任务完成，领取操作
            local req = {
                acc_id = MainPlayer.Instance.AccountID,
                id = self.data.id,
            }
            local msg = protobuf.encode("fogs.proto.msg.GetTaskAward", req)
            LuaHelper.SendPlatMsgFromLua(MsgID.GetTaskAwardReqID, msg)
            CommonFunction.ShowWait()

            --注册回复处理消息
            LuaHelper.RegisterPlatMsgHandler(MsgID.GetTaskAwardRespID, self.parent:GetTaskAwardResp(self.config.award_id), self.uiName)
            --注册回复处理消息
            print('on open click id is '..self.data.id)
            if self.is_rank == false then
                self.parent.parent:SetModelActive(false)
            end
        else --任务未完成，前往操作
            local uiName = GameSystem.Instance.TaskConfigData:GetTaskLinkUIName(self.data.id)
            -- print('--------link uiName: ', uiName)
            local subID = GameSystem.Instance.TaskConfigData:GetTaskLinkSubID(self.data.id)
            -- print("--------link dataid:" , self.data.id)
            -- print('--------link subID: ', subID)
            subID = subID > 0 and subID or nil

            if uiName ~= '' then
                --前往商店的方式要单独提出来
                if uiName == 'UIStore' then
                    if subID == 1 then
                        UIStore:SetType('ST_BLACK')
                    elseif subID == 2 then
                        UIStore:SetType('ST_SKILL')
                    elseif subID == 4 then
                        UIStore:SetType('ST_HONOR')
                    end
                    TaskRespHandler.isOpen = false
                    UIStore:OpenStore()
                elseif uiName == "UIPlayerBuyDiamondGoldHP" then
                    local buyProperty = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
                    if subID == 2 then
                        buyProperty.BuyType = "BUY_GOLD"
                    elseif subID == 4 then
                        buyProperty.BuyType = "BUY_HP"
                    end
                    buyProperty.isTaskLink = true
                    TaskRespHandler.isOpen = false
                    -- NGUITools.Destroy(self.parent.gameObject)
                    --TopPanelManager:ShowPanel("UIPlayerBuyDiamondGoldHP")
                elseif uiName == "UIChallenge" then
                    local open = tonumber(GlobalConst.CHALLENGE_OPEN)
                    local close = tonumber(GlobalConst.CHALLENGE_CLOSE)
                    local nowHTime = tonumber(os.date("%H", GameSystem.mTime))
                    if nowHTime < open or nowHTime > close then
                        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_CHALLENGE_TIP"):format(open,close), nil, nil,nil,nil,nil)
                        return
                    end
                    TopPanelManager:ShowPanel(uiName, subID)
                    TaskRespHandler.isOpen = false
                    self.parent:OnCloseClick()()
                elseif uiName == "UIBullFight" then
                    if not validateFunc("BullFight") then
                        return
                    end

                    local GetBullFightNpcReq = {
                        acc_id = MainPlayer.Instance.AccountID
                    }

                    local req = protobuf.encode("fogs.proto.msg.GetBullFightNpcReq",GetBullFightNpcReq)
                    LuaHelper.SendPlatMsgFromLua(MsgID.GetBullFightNpcReqID,req)
                    LuaHelper.RegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self:HandleGetNPC(), self.uiName)
                    CommonFunction.ShowWaitMask()
                elseif uiName == "UIShootGame" then
                    local ShootOpenReq = {
                        acc_id = MainPlayer.Instance.AccountID
                    }

                    local req = protobuf.encode("fogs.proto.msg.ShootOpenReq",ShootOpenReq)
                    print("Send Shoot Open ShootOpenReq.acc_id=", ShootOpenReq.acc_id)
                    LuaHelper.SendPlatMsgFromLua(MsgID.ShootOpenReqID,req)
                    LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpenRespID, self:HandleShootOpen(), self.uiName)
                    CommonFunction.ShowWaitMask()
                elseif uiName == "UILadder" then
                    self:ClickLadder()()
                else
                    -- TopPanelManager:ShowPanel(uiName, subID)
                    TaskRespHandler.isOpen = false
                    -- print(self.uiName,"------jump to uiName:",uiName,"-----current uiName:",self.parent.parent.uiName)
                    -- if self.parent.parent.uiName ~= uiName then
                    --  self.parent.parent.nextShowUI = uiName
                    --  self.parent.parent:DoClose()
                    -- end
                    -- self.parent:OnClose()

                    TopPanelManager:ShowPanel(uiName, subID)
                end
            end
        end
    end
end

function TaskItem.HandleShootOpenResp(buf)
    CommonFunction.HideWaitMask()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpenRespID, TaskItem.uiName)
    local resp, err = protobuf.decode("fogs.proto.msg.ShootOpenResp", buf)
    print("resp.result=",resp.result)
    if resp then
        if resp.result == 0 then
            MainPlayer.Instance:ClearShootGameModeInfo()
            for k, v in pairs(resp.game_mode_info ) do
                local value  = v
                local gameModeInfo = GameModeInfo.New()
                gameModeInfo.game_mode = GameMode[v.game_mode]
                gameModeInfo.times = v.times
                gameModeInfo.npc = v.npc
                MainPlayer.Instance:AddShootGameModeInfo(gameModeInfo)
            end
            MainPlayer.Instance.IsLastShootGame = true
            return true
        else
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
        end
    else
        error("UICompetition:HandleShootOpen -handler", err)
    end
    return false
end

function TaskItem:HandleShootOpen()
    return function(buf)
        if self.HandleShootOpenResp(buf) then
            TaskRespHandler.isOpen = false
            self.parent:OnCloseClick()()
            -- self.nextShowUI = "UIShootGame"
            -- self:DoClose()
            TopPanelManager:ShowPanel("UIShootGame")
        end
    end
end

function TaskItem:HandleGetNPC()
    return function(buf)
        CommonFunction.HideWaitMask()
        TaskRespHandler.isOpen = false
        self.parent:OnCloseClick()()
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self.uiName)
        local resp, err = protobuf.decode("fogs.proto.msg.GetBullFightNpcResp", buf)
        print("resp.result=",resp.result)
        if resp then
            if resp.result == 0 then
                MainPlayer.Instance.BullFightNpc:Clear()
                for k, v in ipairs(resp.npc) do
                    MainPlayer.Instance.BullFightNpc:Add(v)
                end
                MainPlayer.Instance.IsLastShootGame = false
                -- self.nextShowUI = "UIBullFight"
                -- self:DoClose()
                TopPanelManager:ShowPanel("UIBullFight")
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
            end
        else
            error("UICompetition:HandleGetNPC -handler", err)
        end
    end
end

function TaskItem:ClickLadder()
    return function()
        local t = GameSystem.Instance.FunctionConditionConfig
        enum = t:GetFuncCondition("UILadder").conditionParams:GetEnumerator()
        enum:MoveNext()
        local ladderFCLv = tonumber(enum.Current)
        local lv = MainPlayer.Instance.Level
        if lv < ladderFCLv then
            -- no response
            print('no!!!!!!!!!!!!')
            return
        end
        self.clickedLadder = true
        FriendData.Instance:SendUpdateFriendList()
        self:FriendListHandler()()
    end
end

function TaskItem:FriendListHandler()
    return function()
        if self.clickedLadder then
            local operation = {
                acc_id = MainPlayer.Instance.AccountID,
                type = "MT_PVP_3V3",
            }
            local req = protobuf.encode("fogs.proto.msg.CreateRoomReq",operation)
            LuaHelper.SendPlatMsgFromLua(MsgID.CreateRoomReqID,req)
            LuaHelper.RegisterPlatMsgHandler(MsgID.CreateRoomRespID, self:HandleCreateRoom(), self.uiName)
            print("1927 - Send CreateRoomReq for ladder.")
            self.clickedLadder =false
        end
    end
end

function TaskItem:HandleCreateRoom()
    return function(buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.CreateRoomRespID, self.uiName)
        local resp, error = protobuf.decode('fogs.proto.msg.CreateRoomResp',buf)
        print("1927 - CreateRoomResp resp.result=",resp.result)

        if resp then
            if resp.result == 0 then
                local accId = resp.acc_id
                local type = resp.type
                local roomInfo = resp.info
                local userInfos = roomInfo.user_info
                for i=1, 3 do
                    local v = userInfos[i]
                end

                local nextShowUIParams = {	joinType="active", userInfo = userInfos, isMaster = true }
                -- self:DoClose()
                TopPanelManager:ShowPanel("UILadder", nil, nextShowUIParams)
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end

return TaskItem

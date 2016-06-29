------------------------------------------------------------------------
-- class name    : UIHall
-- create time   : Sun Mar  6 12:30:15 2016
------------------------------------------------------------------------

require "Custom/Store/UIStore"
require "Custom/Task/UITask"
require "Custom/Mail/UIMail"
require "Custom/Activity/ActivityAnnounceData"

UIHall =  {
    uiName     = "UIHall",
    -- Constants
    RANK_TYPE = RankType.RT_QUALIFYING_NEW,
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    uiMyIcon                = nil,
    uiMyIconClick           = nil,
    uiRankIconGrid          = nil,
    uiTeamLevel             = nil,
    uiBgexp                 = nil,
    uiExp                   = nil,
    uiVipNum1               = nil,
    uiVipNum2               = nil,
    uiVip2                  = nil,
    uiVip2Num				= nil,
    uiPlayerInfoGrids       = nil,
    uiSocial                = nil,
    uiSocialRedDot			= nil,
    uiRank                  = nil,
    uiMail                  = nil,
    uiSetting               = nil,
    uiNewPlayerGift         = nil,
    uiPracticeCourt         = nil,
    uiFirstRecharge         = nil,
    uiShop                  = nil,
    uiActivity              = nil,
    uiActivityRedDot		= nil,
    uiSign                  = nil,
    uiCenter                = nil,
    uiRegular               = nil,
    uiQualifying            = nil,
    uiCareer                = nil,
    uiCareerRedDot			= nil,
    uiLadder                = nil,
    uiRole                  = nil,
    uiBadge                 = nil,
    uiBadgeRedDot			= nil,
    uiSkill                 = nil,
    uiFashion               = nil,
    uiFashionRedDot			= nil,
    uiSkillRedDot			= nil,
    uiPackage               = nil,
    uiPackageRedDot			= nil,
    uiAchievenment          = nil,
    uiDaily                 = nil,  --日常入口更新为成长引导入口
    uiBgVipBtn              = nil,
    uiChat                  = nil,
    uiWifi                  = nil,
    uiNewPlayerGiftAnimator = nil,
    uiNewComerTrial         = nil,
    uiAnimator              = nil,
    uiAnimatorResp          = nil,
    uiRedDot                = nil,
    uiMailRedDot            = nil,
    uiLotteryRedDot         = nil,
    uiSignRedDot            = nil,
    uiTaskRedDot			= nil,
    uiDailyRedDot			= nil,
    uiProgress              = nil,
    uiQualifyingLock        = nil,
    uiLadderLock            = nil,
    uiCareerLock            = nil,
    uiQualifyingLockLabel   = nil,
    uiLadderLockLabel       = nil,
    uiCareerLockLabel       = nil,
    uiLabelVictory			= nil,	--首胜奖励文字
    uiCode                  = nil,  --礼包兑换图标

    friendChangedFunc       = nil,  --好友数据变更回调
    onFirstWinRefreshTimer	= nil,  --首胜时间刷新
    onChatRefresh = nil,  --聊天栏刷新显示

    -----------------------
    -- Parameters Module --
    -----------------------
    myIcon         = nil,
    playerInfo     = nil ,
    playerProperty = nil,
    openSign       = false,
    openMail       = false,
    --openGift = false,
    subID    = nil,        --UIPlayerBuyDiamondGoldHP用于显示购买
    openSign = false,
    isFirstEnterHall   = true,

    lastPing           = 0,
    nextShowUI         = nil,
    nextShowUISubID    = nil,
    nextShowUIParams   = nil,
    qualifyingFCLv     = 0,
    ladderFCLv         = 0,
    careerFCLv         = 0,
    clickedLadder      = false,
    friendRespFun      = nil,
    MAXLEVEL           = 30,

    --data
    firstwinCD         = 0,

    --延时刷新
    m_bDirty = false,
}


---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIHall:Awake()
    self:UiParse()				-- Foucs on UI Parse.
    self.playerInfo = GameObjectPlaceholder.Replace(self.uiPlayerInfoGrids.gameObject)
    MainPlayer.Instance:AddDataChangedDelegate(self:OnPlayerDataChanged(), self.uiName)

    --获取解锁信息
    local t = GameSystem.Instance.FunctionConditionConfig

    -- 巡回赛
    local enum = t:GetFuncCondition("UIQualifyingNew").conditionParams:GetEnumerator()
    enum:MoveNext()
    self.qualifyingFCLv = tonumber(enum.Current)

    -- 排位赛
    enum = t:GetFuncCondition("UIQualifyingNewer").conditionParams:GetEnumerator()
    enum:MoveNext()
    self.ladderFCLv = tonumber(enum.Current)

    -- 生涯模式
    enum = t:GetFuncCondition("UICareer").conditionParams:GetEnumerator()
    enum:MoveNext()
    self.careerFCLv = tonumber(enum.Current)

    self.uiQualifyingLockLabel.text = string.format(getCommonStr("SKILL_UNLOCK_LEVEL"), self.qualifyingFCLv)
    self.uiLadderLockLabel.text = string.format(getCommonStr("SKILL_UNLOCK_LEVEL"), self.ladderFCLv)
    self.uiCareerLockLabel.text = string.format(getCommonStr("SKILL_UNLOCK_LEVEL"), self.careerFCLv)
    --END 解锁信息

    self.svRankList = getComponentInChild(self.transform, "Left/ForRanking/ScrollView", "UIScrollView")
    self.gridRankList = getComponentInChild(self.svRankList.transform, "Grid", "UIGrid")

    self.friendChangedFunc = FriendData.FriendListChangedDelegate(self:RefreshSocialRedDot())
    FriendData.Instance:RegisterOnListChanged(self.friendChangedFunc)

    --rank list
    self.RefreshRankList = self:MakeRefreshRankList()

    self.onFirstWinRefreshTimer = LuaHelper.Action(self:FirstWinRefresh())
    self.onChatRefresh = LuaHelper.Action(self:ChatRefresh())
    self.firstwinCD = GameSystem.Instance.CommonConfig:GetUInt("gFirstWinLastTime")
end

function UIHall:Start()
    -- Not use in tencent version.
    --newcomer trial
    if MainPlayer.Instance.trialFlag == 1 then
        NGUITools.SetActive(self.uiNewComerTrial.gameObject, false)
    end

    isFirstEnterHall = false

    local t = getLuaComponent(createUI("CareerRoleIcon",self.uiMyIcon))
    local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
    enum:MoveNext()
    t.id = enum.Current.role_id
    t.showPosition = false
    self.myIcon = t

    t = getLuaComponent(self.playerInfo)
    t.isInHall = true
    t:HideHp()
    t.onCheckSetting = self:CloseChatSetting()
    self.playerProperty = t

    self:SelectGiftRole()

    -- Red dot.
    self:RefreshRedDots()

    NGUITools.SetActive(self.uiChat.gameObject, GameSystem.Instance.CommonConfig:GetUInt('gEnableChat') == 1)


    -- Top
    addOnClick(self.uiMyIconClick.gameObject,   self:ClickMyIcon())
    --addOnClick(self.uiBgVipBtn.gameObject,      self:ClickVip())
    addOnClick(self.uiSocial.gameObject,        self:ClickSocial())
    addOnClick(self.uiMail.gameObject,          self:ClickMail())
    addOnClick(self.uiSetting.gameObject,       self:ClickSetting())
    addOnClick(self.uiVip2.gameObject,			self:ClickVip())

    -- Right
    addOnClick(self.uiFirstRecharge.gameObject, self:ClickFirstRecharge())
    addOnClick(self.uiShop.gameObject,          self:ClickShop())
    -- addOnClick(self.uiActivity.gameObject,      self:ClickActivity())
    addOnClick(self.uiSign.gameObject,          self:ClickSign())
    addOnClick(self.uiActivity.gameObject, self:ClickNewPlayerGift())
    addOnClick(self.uiCode.gameObject,          self:ClickCode())
    -- addOnClick(self.uiNewPlayerGift.gameObject, self:ClickNewPlayerGift())
    --addOnClick(self.uiPracticeCourt.gameObject, self:ClickPracticeCourt1())
    -- not use in tencent version
    addOnClick(self.uiNewComerTrial.transform:FindChild("Icon").gameObject, self:ClickTrial())

    -- left
    addOnClick(self.uiRank.gameObject,          self:ClickRank())

    -- float
    addOnClick(self.uiCenter.gameObject,        self:ClickCenter())

    -- middle
    addOnClick(self.uiRegular.gameObject,       self:ClickPracticeCourt1())
    addOnClick(self.uiQualifying.gameObject,    self:ClickQualifying())
    addOnClick(self.uiCareer.gameObject,        self:ClickCareer())
    addOnClick(self.uiLadder.gameObject,        self:ClickLadder())

    -- bottom
    addOnClick(self.uiRole.gameObject,          self:ClickRole())
    addOnClick(self.uiBadge.gameObject,         self:ClickBadge())
    addOnClick(self.uiSkill.gameObject,         self:ClickSkill())
    addOnClick(self.uiFashion.gameObject,       self:ClickFashion())
    addOnClick(self.uiPackage.gameObject,       self:ClickPackage())
    addOnClick(self.uiAchievenment.gameObject,  self:ClickAchievement())
    addOnClick(self.uiDaily.gameObject,         self:ClickDaily())

    if UpdateRedDotHandler.UpdateState["UINewComer"] == 0 then
        NGUITools.SetActive(self.uiActivity.transform.parent.gameObject, false)
    end
    print("first login:",LoginIDManager.GetAnnounceVersion())
    if GameSystem.Instance.AnnouncementConfigData:GetOpenItem() and
    LoginIDManager.GetAnnounceVersion() ~= GameSystem.Instance.AnnouncementConfigData:GetOpenItem().version then
        print("first login:",LoginIDManager.GetAnnounceVersion())
        if not LoginIDManager.GetAnnounceVersion() or LoginIDManager.GetAnnounceVersion() == 0 then
            LoginIDManager.SetAnnouceVersion(GameSystem.Instance.AnnouncementConfigData:GetOpenItem().version)
        else
            local obj = createUI("NoticePopup")
            local pos = obj.transform.localPosition
            pos.z = -500
            obj.transform.localPosition = pos
        end
    end

    local centerW = self.uiCenter.transform:GetComponent("UISprite").width
    local centerH = self.uiCenter.transform:GetComponent("UISprite").height
    self.uiCenter.transform:GetComponent("UIDragDropItem"):SetLimit(-640+centerW/2, 640-centerW/2, -360+centerH/2, 360-centerH/2)
    --签到自动弹出
    --TODO
    -- self.uiAnimatorResp:AddResp(self:ShowPopup(), self.gameObject)
    self:ShowPopup()()
    self:RefreshSocialRedDot()()
    self:FirstWinRefresh()()
    --等级奖励状态请求
    local req = {
    }
    CommonFunction.ShowWait()
    local msg = protobuf.encode("fogs.proto.msg.LevelAwardStateReq", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.LevelAwardStateReqID, msg)

    --更新新秀试炼入口状态
    local trialFlag = MainPlayer.Instance.trialFlag
    if trialFlag == 1 then
        NGUITools.SetActive(self.uiNewComerTrial.gameObject, false)
    else
        -- if trialReceiveFlag == 1 then
        --     self.uiNewComerTrial:SetBool('New Bool', true)
        -- else
        --     self.uiNewComerTrial:SetBool('New Bool', false)
        -- end
    end
    --设置协助首胜次数
    Friends.AssistFriendFirstWin = MainPlayer.Instance.assist_first_win_times
    print('assist_first_win_times '..Friends.AssistFriendFirstWin)

end

function UIHall:RefreshRedDots()
    UpdateRedDotHandler.MessageHandler("Sign")
    UpdateRedDotHandler.MessageHandler("Task")
    UpdateRedDotHandler.MessageHandler("Daily")
    UpdateRedDotHandler.MessageHandler("Career")
    UpdateRedDotHandler.MessageHandler("NewComerSign")
    UpdateRedDotHandler.MessageHandler("Package")
    UpdateRedDotHandler.MessageHandler("Badge")
    UpdateRedDotHandler.MessageHandler("Fashion")
    UpdateRedDotHandler.MessageHandler("UISkillManager")
    UpdateRedDotHandler.MessageHandler("Mail")
end

function UIHall:OnEnable()
    RankList.onListRefreshed = self.RefreshRankList

    Scheduler.Instance:AddTimer(1, true, self.onFirstWinRefreshTimer)
    Scheduler.Instance:AddFrame(1, false, self.onChatRefresh)

    if not isFirstEnterHall then self:RefreshRedDots() end
end

function UIHall:OnDisable()
    if RankList.onListRefreshed == self.RefreshRankList then
        RankList.onListRefreshed = nil
    end

    Scheduler.Instance:RemoveTimer(self.onFirstWinRefreshTimer)
end

function UIHall:Dirty(subID)
    -- body
    if not self.m_bDirty then
        Scheduler.Instance:AddFrame(1, false, self:LateRefresh(subID))
        self.m_bDirty = true
    end
end
function UIHall:LateRefresh(subID )
    -- body
    return function ( )
        -- body
        self.m_bDirty = false
       if MainPlayer.Instance.inPvpJoining then
            Ladder.ContinueJoinGame()
            return
        end
        if self.forceShow then  --显示好友聊天
            if self.forceShow == 'uiChat' then
                if self.acc_id and self.acc_id ~= 0 then
                    local friend = Friends.GetFriendById(self.acc_id)
                    if friend then
                        warning('open uichat to player ',friend.acc_id)
                        local data = {}
                        data.name = friend.name
                        data.id = friend.acc_id
                        local luaChat = getLuaComponent(self.uiChat)
                        luaChat:OpenChatFrame()(luaChat.uiBox.gameObject)
                        luaChat:HandlerClickFriend()(data)
                        return
                    end
                end
            end
        end
        -- self:RefreshDailyRedDot()
        self:RefreshSocialRedDot()

        self.playerProperty:Refresh()

        self:UpdatePlayerInfo()

        if subID and subID ~= 0 then
            local buyProperty = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
            if subID == 2 then
                buyProperty.BuyType = "BUY_GOLD"
            elseif subID == 4 then
                buyProperty.BuyType = "BUY_HP"
            end
            subID = nil
        end

        local chat = getLuaComponent(self.uiChat)
        chat:EnabledChatSwitch(true)

        local lv = MainPlayer.Instance.Level
        self.uiQualifyingLock.gameObject:SetActive(lv<self.qualifyingFCLv)
        self.uiLadderLock.gameObject:SetActive(lv<self.ladderFCLv)
        self.uiCareerLock.gameObject:SetActive(lv<self.careerFCLv)

        self:DestroyTaskUI()

        CommonFunction.ClearChild(self.gridRankList.transform)
        local rankList = RankList.GetList(self.RANK_TYPE)
        if rankList then
            self.RefreshRankList(self.RANK_TYPE, rankList)
        else
            RankList.ReqList(self.RANK_TYPE);
            RankList.ReqList(RankType.RT_ACHIEVEMENT);
        end

        --更新新秀试炼入口状态
        local trialFlag = MainPlayer.Instance.trialFlag
        if trialFlag == 1 then
            NGUITools.SetActive(self.uiNewComerTrial.gameObject, false)
        end
        --刷新新秀试炼小红点
        if self.uiNewComerTrial.gameObject.activeSelf then
            self:RefreshNewComerTrialRedDot()
        end
    end
end
function UIHall:Refresh(subID)
    self:Dirty(subID)
end
function UIHall:RefreshNewComerTrialRedDot( ... )
    -- body
    UpdateRedDotHandler.RefreshNewComerTrialTip( )

    local rd = self.uiNewComerTrial.transform:FindChild('RedDot')
    local reddot = UpdateRedDotHandler.UpdateState["NewComerTrial"]
    if rd then
        NGUITools.SetActive(rd.gameObject,reddot)
    end
end
function UIHall:RefreshDailyRedDot( ... )
    -- body
    local reddot = false
    local level = MainPlayer.Instance.Level
    --等级奖励
     for k,v in pairs(TaskLevelData.TaskLevelStates or {}) do
        if v == 0 and k <= level then
            -- print ( v .. "k" .. k )
            reddot = true
            break
        end
    end
    --等级任务
    -- if not reddot then
    --     for k,v in pairs(self.taskList or {}) do
    --         local reddot = false
    --         for k1, v1 in pairs(v.cond_info) do
    --             if v1.condition_cur>=v.condition_need == 0 then
    --                 reddot = true
    --                 break
    --             end
    --         end
    --         if reddot then break end
    --     end
    -- end
    --日常任务
    if not reddot then
       reddot = UpdateRedDotHandler.UpdateState["UITaskLevel"]
    end
    NGUITools.SetActive(self.uiDailyRedDot.gameObject,reddot)
end
function UIHall:RefreshSocialRedDot()
    return function(lst)
        local apply_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY_APPLY)
        local gift_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY_GIFT)
        local max_get_gift = GameSystem.Instance.CommonConfig:GetUInt("gFriendGetGiftTimes")
        -- print( string.format("apply:%d gift:%d", apply_count, gift_count) )
        local isRed = (apply_count > 0 or (gift_count > 0 and FriendData.Instance.get_gift_times < max_get_gift))

        if self.uiSocialRedDot.gameObject then
            NGUITools.SetActive(self.uiSocialRedDot.gameObject, isRed)
        end
        if self.clickedLadder then
            self:FriendListHandler()()
        end
    end
end

-- function UIHall:FixedUpdate()
--     -- if not self.lastPing or os.time() - self.lastPing > 2.0 then
--     --	self.lastPing = os.time()
--     --	local latency = GameSystem.Instance.mNetworkManager.m_platConn.m_profiler.m_avgLatency * 1000
--     --	self:UpdateLatency(latency )
--     -- end


--     --new player seven days awards
--     -- local flag = MainPlayer.Instance.NewComerSign.open_flag
--     -- -- print('flag :' .. flag)
--     -- if flag == 0 then
--     --  NGUITools.SetActive(self.uiNewPlayerGiftAnimator.gameObject, false)
--     -- else
--     --  local index = MainPlayer.Instance.NewComerSign.sign_list.Count - 1
--     --  local signState = MainPlayer.Instance.NewComerSign.sign_list:get_Item(index)
--     --  if signState == 0 or signState == 3 then
--     --      self.uiNewPlayerGiftAnimator:SetBool('New Bool', true)
--     --  else
--     --      self.uiNewPlayerGiftAnimator:SetBool('New Bool', false)
--     --  end
--     -- end

--     -- local trialFlag = MainPlayer.Instance.trialFlag
--     -- local trialReceiveFlag = MainPlayer.Instance.trialReceiveFlag
--     -- if trialFlag == 1 then
--     --   NGUITools.SetActive(self.uiNewComerTrial.gameObject, false)
--     -- else
--     --   if trialReceiveFlag == 1 then
--     --       self.uiNewComerTrial:SetBool('New Bool', true)
--     --   else
--     --       self.uiNewComerTrial:SetBool('New Bool', false)
--     --   end
--     -- end
-- end


function UIHall:OnDestroy()
    MainPlayer.Instance:RemoveDataChangedDelegate(self.uiName)
    FriendData.Instance:UnRegisterOnListChanged(self.friendChangedFunc)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function UIHall:GetVip()
    local level = 0
    local config= GameSystem.Instance.VipPrivilegeConfig
    for i=1,15 do
        local vipData = config:GetVipData(i)
        if vipData.consume <= MainPlayer.Instance.VipExp then
            level = i
        else
            break
        end
    end
    return level
end


function UIHall:SelectGiftRole()
    local enumChapter = MainPlayer.Instance.ChapterList:GetEnumerator()
    while enumChapter:MoveNext() do
        local chapter = enumChapter.Current.Value
        local enumSection = chapter.sections:GetEnumerator()
        while enumSection:MoveNext() do
            local section = enumSection.Current.Value
            if section.is_complete and section.get_role == 0 then
                local sectionData = GameSystem.Instance.CareerConfigData:GetSectionData(section.id);
                local roleGift = 0 
                if sectionData then
                    roleGift = GameSystem.Instance.CareerConfigData:GetSectionData(section.id).role_gift
                end
                if roleGift > 0 then
                    CurChapterID = chapter.id
                    CurSectionID = section.id
                    local list = GameSystem.Instance.roleGiftConfig:GetRoleGiftList(roleGift)
                    local rolePresent = getLuaComponent(createUI("UIRolePresented"))
                    rolePresent.roleList = list
                    UIManager.Instance:BringPanelForward(rolePresent.gameObject)
                    local pos = rolePresent.transform.localPosition
                    pos.z = -500
                    rolePresent.transform.localPosition = pos
                    return
                end
            end
        end
    end
end

function UIHall:SetRedDot(uiName)
    -- if self.uiGiftBagRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UIGiftBag"] then
    --	NGUITools.SetActive(self.uiGiftBagRedDot.gameObject, UpdateRedDotHandler.UpdateState["UIGiftBag"])
    -- end
    -- print( "set red dot $$$$ " .. uiName)

    if uiName == "UIMail" then
        if self.uiMailRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UIMail"] then
            NGUITools.SetActive(self.uiMailRedDot.gameObject, UpdateRedDotHandler.UpdateState["UIMail"])
        end
    elseif uiName == "UILottery" then
        if self.uiLotteryRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UILottery"] then
            NGUITools.SetActive(self.uiLotteryRedDot.gameObject, UpdateRedDotHandler.UpdateState["UILottery"])
        end
    elseif uiName == "UISign" then
        if self.uiSignRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UISign"] then
            NGUITools.SetActive(self.uiSignRedDot.gameObject, UpdateRedDotHandler.UpdateState["UISign"])
        end
    elseif uiName == "UITask" then
        if self.uiTaskRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UITask"] then
            NGUITools.SetActive(self.uiTaskRedDot.gameObject, UpdateRedDotHandler.UpdateState["UITask"])
        end
    elseif uiName == "UITaskLevel" then
        print (UpdateRedDotHandler.UpdateState["UITaskLevel"])
        if self.uiDailyRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UITaskLevel"] then
            NGUITools.SetActive(self.uiDailyRedDot.gameObject, UpdateRedDotHandler.UpdateState["UITaskLevel"])
        end
    elseif uiName == "UICareer" then
        if self.uiCareerRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UICareer"] then
            NGUITools.SetActive(self.uiCareerRedDot.gameObject, UpdateRedDotHandler.UpdateState["UICareer"])
        end
    elseif uiName == "UINewComer" then
        if self.uiActivityRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UINewComer"] then
            NGUITools.SetActive(self.uiActivityRedDot.gameObject, UpdateRedDotHandler.UpdateState["UINewComer"])
        end
    elseif uiName == "UIPackage" then
        if self.uiPackageRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UIPackage"] then
            NGUITools.SetActive(self.uiPackageRedDot.gameObject, UpdateRedDotHandler.UpdateState["UIPackage"])
        end
    elseif uiName == "UIBadge" then
        if self.uiBadgeRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["UIBadge"] then
            NGUITools.SetActive(self.uiBadgeRedDot.gameObject, UpdateRedDotHandler.UpdateState["UIBadge"])
        end
    elseif uiName == "UIFashion" then
        if self.uiFashionRedDot.gameObject.activeSelf ~= (UpdateRedDotHandler.UpdateState["UIFashion"].Count > 0) then
            NGUITools.SetActive(self.uiFashionRedDot.gameObject, UpdateRedDotHandler.UpdateState["UIFashion"].Count > 0)
        end
    elseif uiName == "UISkillManager" then
        if self.uiSkillRedDot.gameObject.activeSelf ~= (UpdateRedDotHandler.UpdateState["UISkillManager"].Count > 0) then
            NGUITools.SetActive(self.uiSkillRedDot.gameObject, UpdateRedDotHandler.UpdateState["UISkillManager"].Count > 0)
        end
    end
end



function UIHall:MailInfoNotify(message)
    return function (message)
        --解析pb
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.MailInfoNotifyID, UIMail.uiName)
        CommonFunction.StopWait()
        local resp, err = protobuf.decode('fogs.proto.msg.MailInfoNotify', message)
        --Debugger.Log('------------resp: {0}', resp.store_id)
        if resp == nil then
            print('error -- MailInfoNotify error: ', err)
            self.openMail = false
            return
        end
        if resp.result ~= 0 then
            print('error --  MailInfoNotify return failed: ', resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            self.openMail = false
            return
        end
        UIMail:SetMailList(resp.mail_list)

        --隐藏模型
        -- local root = UIManager.Instance.m_uiRootBasePanel
        -- local hall = root.transform:FindChild('UIHall(Clone)')
        -- if hall then
        --	self.uiModelShowItem = hall.transform:FindChild("ModelShowItem/Model"):GetComponent("ModelShowItem")
        -- end
        -- if self.auiModelShowItem.gameObject.activeSelf then
        --	self:SetModelActive(false)
        -- end

        --创建邮件界面
        local go = createUI('UIMail')
        local mail = getLuaComponent(go)
        mail:SetParent(self)
        mail:InitMailList()
        mail.onClose = function ( ... )
            self.openMail = false
        end
    end
end


function UIHall:ClickSocial()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.friends) then return end

        self.nextShowUI = "FriendsList"
        self:DoClose()
    end
end


function UIHall:ClickRank()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.rank) then return end

        self.nextShowUI = "UIRankList"
        self:DoClose()
    end
end

function UIHall:MakeRefreshRankList()
    return function (rankType, rankList)
        if rankType == self.RANK_TYPE then
            local count = math.min(10, table.getn(rankList))
            print(self.uiName, "RankList count:", count)
            for i = 1, count do
                local info = rankList[i]
                local roleIcon = getLuaComponent(createUI("CareerRoleIcon", self.gridRankList.transform))
                roleIcon.id = tonumber(info.show_id)
                roleIcon.showPosition = false
                roleIcon.onClick = self:ClickRank()
                if i <= 3 then
                    roleIcon.ranking = i
                end
            end
            self.gridRankList:Reposition()
            self.svRankList:ResetPosition()
        end
    end
end

function UIHall:ClickMail()
    return function()
        print (" click mail btn")
        if not FunctionSwitchData.CheckSwith(FSID.email) then return end

        if self.openMail then
            return
        end

        self:CloseChatSetting()()

        local msg = protobuf.encode("fogs.proto.msg.OpenMailSys", {})
        LuaHelper.SendPlatMsgFromLua(MsgID.OpenMailSysID, msg)

        --
        LuaHelper.RegisterPlatMsgHandler(MsgID.MailInfoNotifyID, self:MailInfoNotify(), UIMail.uiName)
        self.openMail = true
        CommonFunction.ShowWait()
    end
end

function UIHall:ClickNewPlayerGift()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.give7) then return end

        local newSign = getLuaComponent(createUI('UINewSign'))
        UIManager.Instance:BringPanelForward(newSign.gameObject)
        newSign.onClose = function ( ... )
            self:SetModelActive(true)
        end
        self:SetModelActive(false)
    end
end


function UIHall:ClickPracticeCourt1()
    return function()
        self.nextShowUI = "UIPracticeCourt1"
        self:DoClose()
    end
end

function UIHall:ClickSetting()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.setting) then return end

        local go = createUI("UISystemSettings")
        UIManager.Instance:BringPanelForward(go)
    end
end

function UIHall:ClickFirstRecharge()
    return function()
        --注释首充功能
        -- CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil, nil, nil);

        if not FunctionSwitchData.CheckSwith(FSID.first_recharg) then return end

        --作弊功能
        if GameSystem.Instance.CommonConfig:GetUInt("gEnableGMCommond") == 0 then
            CommonFunction.ShowPopupMsg(getCommonStr("IN_CONSTRUCTING"),nil,nil,nil,nil,nil)
            return
        end
        self:CloseChatSetting()()
        local goCheat = createUI("Cheat")
        UIManager.Instance:BringPanelForward(goCheat)
        local position = goCheat.transform.localPosition
        position.z = -500
        goCheat.transform.localPosition = position
    end
end


function UIHall:ClickShop()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.store) then return end
        if validateFunc('Shop') then
            self.nextShowUI = "UILottery"
            self:DoClose()
        end
    end
end

-- activity
function UIHall:ClickRecharge()
    return function()
        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil, nil, nil);
    end
end


function UIHall:ClickActivity()
    return function()
        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil, nil, nil);
    end
end

function UIHall:ClickSign()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.checkin) then return end

        self:OpenSignInRespHandler()
    end
end

function UIHall:ClickCode()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.gift) then return end

        self:OpenCodeInRespHandler()
    end
end


function UIHall:ClickCenter()
    return function()
        MainPlayer.Instance:OpenPlayerPlat()
    end
end



function UIHall:ClickReguluar()
    return function()
        Regular1V1Handler.SelectRole()
    end
end


function UIHall:ClickQualifying()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.tour) then return end

        local lv = MainPlayer.Instance.Level
        if lv < self.qualifyingFCLv then
            -- no response
            return
        end
        if Ladder.CheckNetState(LuaHelper.VoidDelegate(self:OnCheckQualifyingNewOK()), self:OnCheckQualifyingNewCancel()) then
            Scheduler.Instance:AddTimer(0.3, false, self:GoQualifyingNew())
        end

    end
end
function UIHall:GoQualifyingNew( )
    -- body
     return function()
        if validateFunc('UIQualifyingNew') then
            self.nextShowUI = "UIQualifyingNew"
            self:DoClose()
        end
    end
end
function UIHall:ClickCareer()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.career) then return end

        --小于开放等级，点击按钮不做响应
        if MainPlayer.Instance.Level < self.careerFCLv then
            return
        end

        self.nextShowUI = "UICareer"
        self:DoClose()
    end
end

function UIHall:ClickLadder()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.qualifying) then return end

        local lv = MainPlayer.Instance.Level
        print("1927 - <UIHall>  lv, self.ladderFCLv=",lv, self.ladderFCLv)
        if lv < self.ladderFCLv then
            -- no response
            return
        end
        if Ladder.CheckNetState(LuaHelper.VoidDelegate(self:OnCheckQualifyingNewerOK()), self:OnCheckQualifyingNewerCancel()) then
            Scheduler.Instance:AddTimer(0.3, false, self:GoQualifying())
        end
    end
end



function UIHall:FriendListHandler()
    return function()
        print("1927 - <UIHall> FriendListHander called")
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
            CommonFunction.ShowWait()
        end
    end
end

function UIHall:HandleCreateRoom()
    return function(buf)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.CreateRoomRespID, self.uiName)
        CommonFunction.StopWait()
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

                self.nextShowUI = "UILadder"
                self.nextShowUIParams = {	joinType="active", userInfo = userInfos, isMaster = true }
                self:DoClose()

            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                playSound("UI/UI-wrong")
            end
        end
    end
end


function UIHall:ClickRole()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.players) then return end

        self.nextShowUI = "UIRole"
        self:DoClose()
    end
end

function UIHall:ClickBadge()
    return function()
        -- CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil, nil, nil);
        if not FunctionSwitchData.CheckSwith(FSID.scrawl) then return end

        if validateFunc('Badge') then
            self.nextShowUI = "UIBadge"
            self:DoClose()
        end
    end
end

function UIHall:ClickSkill()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.skills) then return end

        if validateFunc('Skill') then
            self.nextShowUI = "UISkillManager"
            self:DoClose()
        end
    end
end

function UIHall:ClickFashion()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.clothes) then return end

        self.nextShowUI = "UIFashion"
        self.nextShowUIParams = {reputationStore = false, titleStr = 'STR_ROLE_FASHION'}
        self:DoClose()
    end
end

function UIHall:ClickPackage()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.bag) then return end

        self.nextShowUI = "UIPackage"
        self:DoClose()
    end
end

-- Task
function UIHall:ClickAchievement()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.achievement) then return end

        local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = 2, -- normal task.
        }
        CommonFunction.ShowWait()
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

        TaskRespHandler.parent = self
    end
end

function UIHall:ClickDaily()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.daily) then return end

        --等级任务请求
        local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = 3, -- level Task
        }
        CommonFunction.ShowWait()
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

        TaskRespHandler.parent = self
    end
end

function UIHall:ClickMyIcon()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.player_info) then return end

        local friendsInfo = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("FriendsInfo(Clone)")
        if friendsInfo then
            NGUITools.Destroy(friendsInfo.gameObject)
        end

        local friInfo = createUI("FriendsInfo")
        local friLua = getLuaComponent(friInfo)
        friLua:Query( MainPlayer.Instance.AccountID,self.platId)
        -- local req = {
        --     friend = {
        --         acc_id = MainPlayer.Instance.AccountID,
        --         plat_id = self.platId,
        --     },
        -- }

        -- local buf = protobuf.encode("fogs.proto.msg.QueryFriendInfoReq", req)
        -- LuaHelper.SendPlatMsgFromLua(MsgID.QueryFriendInfoReqID, buf)

        -- LuaHelper.RegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self:QueryMyInfoHandler(), self.uiName)
        -- CommonFunction.ShowWait()
    end
end

-- function UIHall:QueryMyInfoHandler()
--     return function(buf)
--         LuaHelper.UnRegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self.uiName)
--         CommonFunction.StopWait()
--         local resp, err = protobuf.decode("fogs.proto.msg.QueryFriendInfoResp", buf)
--         if not resp then
--             error("", err)
--             do return end
--         end

--         if resp.result ~= 0 then
--             do return end
--         end

--         --打开好友信息界面
--         local friInfo = createUI("FriendsInfo")
--         local friLua = getLuaComponent(friInfo)
--         friLua:setData(resp)

--         UIManager.Instance:BringPanelForward(friInfo)
--     end
-- end

function UIHall:ClickVip()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.vip) then return end

        self.nextShowUI = "VIPPopup"
        self:DoClose()
    end
end

function UIHall:ClickTrial()
    return function()
        -- if self.clickState then
        --     return
        -- end
        -- self.clickState = true
        if not FunctionSwitchData.CheckSwith(FSID.active7) then return end

          --等级任务请求
        local req = {
            acc_id = MainPlayer.Instance.AccountID,
            type = 7, -- NEW_COMER Task
        }
        CommonFunction.ShowWait()
        local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
        LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

        TaskRespHandler.parent = self

        -- local trial = getLuaComponent(createUI('NewComerTrial'))
        -- trial.parent = self
        -- UIManager.Instance:BringPanelForward(trial.gameObject)
        -- trial.onClose = function ( ... )
        --     self:SetModelActive(true)
        --     self.clickState = false
        -- end
        -- self:SetModelActive(false)
    end
end


function UIHall:OnClose( ... )
    if self.onClose then
        self.onClose()
        self.onClose = nil
        return
    end
    --print(self.uiName," panel test nextShowUI:",self.nextShowUI)
    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    elseif self.openStore == true then
        UIStore:SetType('ST_BLACK')
        UIStore:SetBackUI(self.uiName)
        UIStore:OpenStore()
        self.openStore = false
    end
end

function UIHall:DoClose()
    if self.uiAnimator then
        self:CloseChatSetting(true)()
        self:AnimClose()
    else
        self:OnClose()
    end
end


function UIHall:DestroyTaskUI()
    if TaskUI then
        -- print(self.uiName,"------clear taskui")
        NGUITools.Destroy(TaskUI.gameObject)
        TaskUI = nil
    end
end

function UIHall:OnPlayerDataChanged()
    return function()
        if not NGUITools.GetActive(self.gameObject) then return end
        self.playerProperty:Refresh()
        self:UpdatePlayerInfo()
    end
end


function UIHall:UpdatePlayerInfo()
    local level = MainPlayer.Instance.Level
    -- local slevel = nil
    -- local glevel = nil
    -- if level >= 10 then
    --	slevel = math.floor(level/10)
    --	glevel = level - slevel*10
    --	-- self.uiPlayerTeamUnitLevel.spriteName = 'gameInterface_figure_white' .. tostring(glevel)
    --	-- self.uiPlayerTeamDecadeLevel.gameObject:SetActive(true)
    --	-- self.uiPlayerTeamDecadeLevel.spriteName = 'gameInterface_figure_white' .. tostring(slevel)
    --	-- local pos = self.uiPlayerTeamUnitLevel.transform.localPosition
    --	-- if pos.x == 0 then pos.x = 14 end
    --	-- self.uiPlayerTeamUnitLevel.transform.localPosition = pos
    -- else
    --	-- self.uiPlayerTeamDecadeLevel.gameObject:SetActive(false)
    --	-- self.uiPlayerTeamUnitLevel.spriteName = 'gameInterface_figure_white' .. tostring(level)
    --	-- local pos = self.uiPlayerTeamUnitLevel.transform.localPosition
    --	-- pos.x = 0
    --	-- self.uiPlayerTeamUnitLevel.transform.localPosition = pos
    -- end
    self.uiTeamName.text = MainPlayer.Instance.Name
    self.uiTeamLevel.text = "LV."..level
    local vip = self:GetVip()
    self.uiVip2Num.text = vip
    self.uiVipNum2.gameObject:SetActive(vip>=10)
    local vipStrPre = "hall2_"
    local pos = self.uiVipNum1.transform.localPosition
    if vip < 10 then
        self.uiVipNum1.spriteName = vipStrPre..vip
        pos.x = -2
        self.uiVipNum1.transform.localPosition = pos
    else
        self.uiVipNum2.spriteName = vipStrPre..math.modf(vip/10)
        self.uiVipNum1.spriteName = vipStrPre..(vip%10)
        pos.x = 11
        self.uiVipNum1.transform.localPosition = pos
    end

    local currentMaxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level)
    local showExp = 0
    for i=1,level - 1 do
        showExp = showExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
    end
    if level == self.MAXLEVEL then
        local maxExp = showExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(self.MAXLEVEL)
        if MainPlayer.Instance.Exp > maxExp then
            MainPlayer.Instance.Exp = maxExp
        end

        self.uiExp.transform.parent.gameObject:SetActive(false)
    end
    showExp = MainPlayer.Instance.Exp - showExp
    local t = showExp / currentMaxExp
    self.uiBgexp.value = t
    self.uiProgress.gameObject:SetActive(t >= 0.01)
    self.uiExp.text = showExp .. "/" .. currentMaxExp
end


function UIHall:OpenSignInRespHandler( ... )
    if self.openSign then
        return
    end
    self:CloseChatSetting()()
    self:SetModelActive(false)
    local signList = nil
    local sign = createUI('UISign')
    local l = getLuaComponent(sign.gameObject)
    if MainPlayer.Instance.signInfo ~= nil then
        signList = MainPlayer.Instance.signInfo.sign_list
        l.listcount = signList.Count
    end
    l.signlists = {}
    local enum = signList:GetEnumerator()
    while enum:MoveNext() do
        table.insert(l.signlists, enum.Current)
    end
    l:SetParent(self)
    l.onClose = function ( ... )
        self.openSign = false
        self:ShowPopup()(0)
    end
    self.openSign = true
    UIManager.Instance:BringPanelForward(sign.gameObject)
end

function UIHall:OpenCodeInRespHandler( ... )
    self:CloseChatSetting()()
    local code = createUI('UIGiftBagExchange')
    UIManager.Instance:BringPanelForward(code.gameObject)
end

-- function UIHall:UpdateLatency(l)
--	if l < 100 then
--		self.uiWifi.spriteName = "hall2_03"
--	elseif l <= 199 then
--		self.uiWifi.spriteName = "hall2_02"
--	else
--		self.uiWifi.spriteName = "hall2_01"
--	end
-- end




function UIHall:CloseChatSetting(isClose)
    return function ( ... )
        local chat = getLuaComponent(self.uiChat)
        if isClose then
            chat:EnabledChatSwitch(false)
        end
        if chat.uiSetUp.gameObject.activeSelf then
            chat:OpenSetUp()()
        end
    end
end

function UIHall:ShowPopup( ... )
    -- body
    return function (callbackType)
        -- body
        if callbackType then 
            if callbackType == 0 and self.openSign then
                self.openSign = nil
            elseif callbackType == 1 and self.openActivityAnnounce then
                self.openActivityAnnounce = nil
            end
        end
        local signFlag = false
          --签到先弹出
          --第一次调用不会传入callbacktype
        if not callbackType then
            local signed = MainPlayer.Instance.signInfo.signed
            if signed == 0 then
                self:OpenSignInRespHandler()
                signed = 1
                signFlag = true
            elseif signed ==1 then
                local listCount = MainPlayer.Instance.signInfo.sign_list.Count
                if listCount > 0 then
                    local signState = MainPlayer.Instance.signInfo.sign_list:get_Item(listCount - 1)
                    local vipLevel = GameSystem.Instance.signConfig:GetDaySignData(listCount).vip_level
                    if signState == 1 and vipLevel and vipLevel > 0 and self:GetVip() >= vipLevel then
                        self:OpenSignInRespHandler()
                        signFlag = true
                        signed = 1
                    end
                end
            end
        end

        callbackType = callbackType or 0
        if not signFlag then
            --弹出活动公告
            if not ActivityAnnounceData.AnnounceAlreadyRead and callbackType == 0 then
                self:ShowUIActivityAnnounce()
            else -- 弹出新手引导
                GuideSystem.Instance:ReqBeginGuide(self.uiName)
            end
        end
    end
end

-- function UIHall:ShowUISign( ... )
--     -- body
--     return function ()
--         if not FunctionSwitchData.Cancel(FSID.checkin) then return end

--         local isGuide = GuideSystem.Instance.curModule
--         local signed = MainPlayer.Instance.signInfo.signed
--         -- print('isGuide -------- ' .. tostring(isGuide))
--         -- print('signed -------- ' .. tostring(signed))
--         -- print('have unfinished Guide -------- ' .. tostring(MainPlayer.Instance:GetUncompletedGuide() ~= 0))
--         local inPvpJoing = MainPlayer.Instance.inPvpJoining

--         if signed == 0
--             and not isGuide
--             and MainPlayer.Instance:GetUncompletedGuide() == 0
--             and not inPvpJoing
--         then
--             self:OpenSignInRespHandler()
--         end
--         if signed == 1
--             and not isGuide
--             and MainPlayer.Instance:GetUncompletedGuide() == 0
--             and not inPvpJoing
--         then
--             local listCount = MainPlayer.Instance.signInfo.sign_list.Count
--             if listCount > 0 then
--                 local signState = MainPlayer.Instance.signInfo.sign_list:get_Item(listCount - 1)
--                 local vipLevel = GameSystem.Instance.signConfig:GetDaySignData(listCount).vip_level
--                 if signState == 1 and vipLevel and vipLevel > 0 and self:GetVip() >= vipLevel then
--                     self:OpenSignInRespHandler()
--                 end
--             end
--         end
--     end
-- end

--打开活动公告界面
function UIHall:ShowUIActivityAnnounce( ... )
    -- body
    if self.openActivityAnnounce then 
        return
    end
    self:CloseChatSetting()()
    
    local l = require "Custom/Activity/UIActivityAnnounce"
    l:SetParent(self)
    l.onClose = function ( ... )
        self:ShowPopup()(1)
    end
    l:Start()
    self.openActivityAnnounce = true
end
-- Now Hall does not have model, nothing to do now.
-- Keep the interface to let outter call.
function UIHall:SetModelActive()

end

local STR_FIRST_WIN_OK = getCommonStr("STR_FIRST_WIN_OK")
local STR_FIRST_WIN_TIME = getCommonStr("STR_FIRST_WIN_TIME")
function UIHall:FirstWinRefresh()
    return function()
        local server_time = GameSystem.mTime
        local last_win_time = LuaPlayerData.last_win_time

        local ctr = self.transform:FindChild("BottomLeft/FristVictory")
        if last_win_time == 0 then
            NGUITools.SetActive(ctr.gameObject, false)
        else
            NGUITools.SetActive(ctr.gameObject, true)

            local diff = self.firstwinCD * 60 - (server_time - last_win_time)
            if diff <= 0 then
                self.uiLabelVictory.text = STR_FIRST_WIN_OK
            else
                local timeStr = getTimeStr( diff )
                --print(timeStr.."=>"..diff)
                self.uiLabelVictory.text = string.format(STR_FIRST_WIN_TIME, timeStr)
            end
        end

        --print(self.uiLabelVictory.text)

    end
end

function UIHall:ChatRefresh()
    return function ()
        print('-----------------ChatRefresh-------------------')
        self.transform:FindChild("BottomLeft/UIChat/UIChat/SmallTip/Scroll"):GetComponent("UIPanel"):Refresh()
    end
end

function UIHall:GoQualifying()
    return function()
        if validateFunc('UIQualifyingNewer') then
            self.nextShowUI = "UIQualifyingNewer"
            self:DoClose()
        end
    end
end

function UIHall:OnCheckQualifyingNewerOK()
    return function()
        Scheduler.Instance:AddTimer(0.3, false, self:GoQualifying())
    end
end

function UIHall:OnCheckQualifyingNewerCancel()
    return function()
    end
end

function UIHall:OnCheckQualifyingNewOK()
    return function()
        Scheduler.Instance:AddTimer(0.3, false, self:GoQualifyingNew())
    end
end

function UIHall:OnCheckQualifyingNewCancel()
    return function()
    end
end



---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.                                          --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.                                    --
-- NOTE:                                                                                         --
--	1. This function only used to parse the UI(GameObject).                                      --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.                   --
--	3. The name is according to the structure of prefab.                                         --
--	4. Please Do NOT MINDE the Comment Lines.                                                    --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.       --
---------------------------------------------------------------------------------------------------
function UIHall:UiParse()
    self.uiMyIcon          = self.transform:FindChild("TopLeft/PlayerDesc/Icon"):GetComponent("Transform")
    self.uiMyIconClick     = self.transform:FindChild("TopLeft/PlayerDesc/Icon/Click"):GetComponent("Transform")
    self.uiRankIconGrid    = self.transform:FindChild("Left/ForRanking/ScrollView/Grid"):GetComponent("UIGrid")
    self.uiTeamName        = self.transform:FindChild("TopLeft/PlayerDesc/TeamName"):GetComponent("UILabel")
    self.uiTeamLevel       = self.transform:FindChild("TopLeft/PlayerDesc/TeamName/LabelLevel"):GetComponent("UILabel")
    self.uiBgexp           = self.transform:FindChild("TopLeft/PlayerDesc/Exp/Bgexp"):GetComponent("UIProgressBar")
    self.uiExp             = self.transform:FindChild("TopLeft/PlayerDesc/Exp/LabelNum"):GetComponent("UILabel")
    self.uiVipNum1         = self.transform:FindChild("TopLeft/PlayerDesc/BgVip/VipNum1"):GetComponent("UISprite")
    self.uiVipNum2         = self.transform:FindChild("TopLeft/PlayerDesc/BgVip/VipNum2"):GetComponent("UISprite")
    self.uiVip2               = self.transform:FindChild("TopLeft/PlayerDesc/BgVip2"):GetComponent("UISprite")
    self.uiVip2Num        = self.transform:FindChild("TopLeft/PlayerDesc/BgVip2/Num"):GetComponent("UILabel")
    self.uiPlayerInfoGrids = self.transform:FindChild("TopRight/PlayerInfoGrids"):GetComponent("Transform")
    self.uiSocial          = self.transform:FindChild("TopRight/HallTopBtns/Social/SocialButton"):GetComponent("UIButton")
    self.uiSocialRedDot       = self.transform:FindChild("TopRight/HallTopBtns/Social/SocialButton/RedDot")

    self.uiRank          = self.transform:FindChild("Left/ForRanking/Bg"):GetComponent("UIButton")
    self.uiMail          = self.transform:FindChild("TopRight/HallTopBtns/Mail/MailButton"):GetComponent("UIButton")
    self.uiSetting       = self.transform:FindChild("TopRight/HallTopBtns/Setting/SettingButton"):GetComponent("UIButton")
    self.uiNewPlayerGift = self.transform:FindChild("Right/NewPlayerGift/Background"):GetComponent("UISprite")
    self.uiPracticeCourt = self.transform:FindChild("Right/PracticeCourt/Icon"):GetComponent("UISprite")
    self.uiFirstRecharge = self.transform:FindChild("Right/FristRecharge/Icon"):GetComponent("UISprite")
    self.uiShop          = self.transform:FindChild("Right/Shop/Icon"):GetComponent("UISprite")
    self.uiActivity      = self.transform:FindChild("Right/Activity/Icon"):GetComponent("UISprite")
    self.uiActivityRedDot= self.transform:FindChild("Right/Activity/RedDot"):GetComponent("UISprite")
    self.uiSign          = self.transform:FindChild("Right/Sign/Icon"):GetComponent("UISprite")
    self.uiCode          = self.transform:FindChild("Right/code/Icon"):GetComponent("UISprite")

    self.uiCenter     = self.transform:FindChild("Right/UserCenter/Background"):GetComponent("UIButton")
    self.uiRegular    = self.transform:FindChild("HallMatch/BoxMatch"):GetComponent("UIButton")
    self.uiQualifying = self.transform:FindChild("HallMatch/BoxQualifying"):GetComponent("UIButton")
    self.uiCareer     = self.transform:FindChild("HallMatch/BoxCareer"):GetComponent("UIButton")
    self.uiCareerRedDot = self.transform:FindChild("HallMatch/BoxCareer/RedDot"):GetComponent('UISprite')
    self.uiLadder     = self.transform:FindChild("HallMatch/BoxLadder"):GetComponent("UIButton")

    self.uiRole         = self.transform:FindChild("BottomLeft/HallBottomBtns/ForMember/Bg"):GetComponent("UIButton")
    self.uiBadge        = self.transform:FindChild("BottomLeft/HallBottomBtns/Badge/Bg"):GetComponent("UIButton")
    self.uiBadgeRedDot	= self.transform:FindChild("BottomLeft/HallBottomBtns/Badge/Bg/Tip"):GetComponent("UISprite")
    self.uiSkill        = self.transform:FindChild("BottomLeft/HallBottomBtns/Skill/Bg"):GetComponent("UIButton")
    self.uiFashion      = self.transform:FindChild("BottomLeft/HallBottomBtns/ForFashion/Bg"):GetComponent("UIButton")
    self.uiFashionRedDot= self.transform:FindChild("BottomLeft/HallBottomBtns/ForFashion/Bg/Tip"):GetComponent("UISprite")
    self.uiSkillRedDot = self.transform:FindChild("BottomLeft/HallBottomBtns/Skill/Bg/Tip"):GetComponent("UISprite")
    self.uiPackage      = self.transform:FindChild("BottomLeft/HallBottomBtns/ForPackage/Bg"):GetComponent("UIButton")
    self.uiPackageRedDot= self.transform:FindChild("BottomLeft/HallBottomBtns/ForPackage/Bg/Tip"):GetComponent("UISprite")
    self.uiAchievenment = self.transform:FindChild("BottomLeft/HallBottomBtns/ForAchievement/Bg"):GetComponent("UIButton")
    self.uiDaily        = self.transform:FindChild("BottomRight/ForDaily/Bg"):GetComponent("UIButton")

    self.uiBgVipBtn = self.transform:FindChild("TopLeft/PlayerDesc/BgVip"):GetComponent("UISprite")
    self.uiChat     = self.transform:FindChild("BottomLeft/UIChat/UIChat"):GetComponent("UIPanel")

    -- self.uiWifi                  = self.transform:FindChild("Top/HallTopBtns/Wifi/icon"):GetComponent("UISprite")
    self.uiNewPlayerGiftAnimator = self.transform:FindChild("Right/NewPlayerGift"):GetComponent("Animator")
    self.uiNewComerTrial         = self.transform:FindChild("Right/NewComerTrial")

    self.uiAnimator     = self.transform:GetComponent('Animator')
    self.uiAnimatorResp = self.transform:GetComponent("AnimationResp")

    -- red dot.
    --self.uiRedDot = self.transform:FindChild("ButtonMenu/ButtonMenu/Tip")
    -- NOT use in tencent.
    -- self.uiGiftBagRedDot = self.transform:FindChild("Top/HallTopBtns/Recharge/RedDot")
    self.uiMailRedDot          = self.transform:FindChild("TopRight/HallTopBtns/Mail/MailButton/RedDot")
    self.uiLotteryRedDot       = self.transform:FindChild("Right/Shop/RedDot")
    self.uiSignRedDot          = self.transform:FindChild("Right/Sign/RedDot")
    self.uiTaskRedDot          = self.transform:FindChild("BottomLeft/HallBottomBtns/ForAchievement/Bg/Tip")
    self.uiDailyRedDot         = self.transform:FindChild("BottomRight/ForDaily/Bg/Tip")
    self.uiProgress            = self.transform:FindChild("TopLeft/PlayerDesc/Exp/Progress"):GetComponent("UIProgressBar")
    self.uiQualifyingLock      = self.transform:FindChild("HallMatch/BoxQualifying/LockNode"):GetComponent("Transform")
    self.uiLadderLock          = self.transform:FindChild("HallMatch/BoxLadder/LockNode"):GetComponent("Transform")
    self.uiCareerLock          = self.transform:FindChild("HallMatch/BoxCareer/LockNode")
    self.uiQualifyingLockLabel = self.transform:FindChild("HallMatch/BoxQualifying/LockNode/LockLabel"):GetComponent("UILabel")
    self.uiLadderLockLabel     = self.transform:FindChild("HallMatch/BoxLadder/LockNode/LockLabel"):GetComponent("UILabel")
    self.uiCareerLockLabel     = self.transform:FindChild("HallMatch/BoxCareer/LockNode/LockLabel"):GetComponent("UILabel")

    self.uiLabelVictory        = getComponentInChild(self.transform, "BottomLeft/FristVictory/LabelVictory", "UILabel")
end


return UIHall

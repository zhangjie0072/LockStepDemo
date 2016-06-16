--common lua update redDot handler

UpdateRedDotHandler = {
    uiName = 'UpdateRedDotHandler',
    -------------------------------
    UpdateState =
    {
        ["UIMail"] = false,
        -- ["UIGiftBag"] = false,
        ["UINewComer"] = false,
        ["UILottery"] = false,
        ["UISign"] = false,
        ["UITask"] = false,
        ["UITaskLevel"] = false,
        ["UICareer"] = false,
        ["UIPackage"] = false,
        ["UIBadge"] = false,
        ["UIFashion"] = nil,
        ["NewComerTrial"] = {},
        ["ButtonMenu"] =
        {
            ["UIRole"] = false,
            ["UISquad"] = false,
        },
        ["UISkillManger"] = false,
        ["UISkillWarehouse"] = false,
    },

    ShowUI =
    {
        ["UIMail"] = {"UIHall(Clone)"},
        ["UINewComer"] = {"UIHall(Clone)"},
        -- ["UIGiftBag"] = {"UIHall(Clone)"},
        ["UILottery"] = {"UIHall(Clone)"},
        ["UISign"] = {"UIHall(Clone)"},
        ["UITask"] = {"UIHall(Clone)"},
        ["UITaskLevel"] = {"UIHall(Clone)"},
        ["UICareer"] = {"UIHall(Clone)"},
        ["UIPackage"] = {"UIHall(Clone)"},
        ["UIBadge"] = {"UIHall(Clone)"},
        ["UIFashion"] = {"UIHall(Clone)"},
        ["UISkillManager"] = {"UIHall(Clone)"},
        ["Others"] = {
            "UIHall(Clone)",
            "UICareer(Clone)",
            "UIChallenge(Clone)",
            "UICompetition(Clone)",
            "UIShootGame(Clone)",
            "UIBullFight(Clone)",
            "UITour(Clone)",
            "UIPractice(Clone)",
            "UIPVPEntrance(Clone)",
            "UI1V1Plus(Clone)",
            "UIQualifying(Clone)",
            "UIFashion(Clone)",
            "UIRankList(Clone)",
            "UILottery(Clone)",
            "UIStore(Clone)"
        }
    },
    --
    restoreTime1,
    restoreTime2,
    equipTipList = {},
    roleSkillList = {},
    roleLevelUpList = {},
    roleEnhanceList = {},
    roleRecruitList = {},
    roleImproveList = {},
    MainPlayerLotteryInfo,
    MainPlayerSignInfo,
    roleMaxQuality,
}

function UpdateRedDotHandler.Register( ... )
    MainPlayer.Instance.onCheckUpdate = UpdateRedDotHandler.HallCheckUpdate()
end

function UpdateRedDotHandler.HallCheckUpdate( ... )
    return function ( ... )
            -- UpdateRedDotHandler.RefreshGiftTip()
            UpdateRedDotHandler.RefreshLotteryTip()
        end
    end

function UpdateRedDotHandler.MessageHandler(mess)
    print('mess = ', mess)
    if mess == "Mail" then
        UpdateRedDotHandler.RefreshMailTip()
    elseif mess == "Sign" then
        UpdateRedDotHandler.RefreshSignTip()
    elseif mess == "NewComerTrial" then
     UpdateRedDotHandler.RefreshNewComerTrialTip()
    elseif mess == "Task" then
        UpdateRedDotHandler.RefreshTaskTip()
    elseif mess == "Daily" then
        UpdateRedDotHandler.RefreshDailyTip()
    elseif mess == "Activity" then
        UpdateRedDotHandler.RefreshActivityTip()
    -- elseif mess == "Role" then
    --  UpdateRedDotHandler.RefreshRoleTip()
    -- elseif mess == "Squad" then
    --  UpdateRedDotHandler.RefreshSquadTip()
    -- elseif mess == "RoleDetail" then
    --  UpdateRedDotHandler.RefreshRoleDetailTip()
    elseif mess == "Career" then
        UpdateRedDotHandler.RefreshCareerTip()
    elseif mess == "NewComerSign" then
        UpdateRedDotHandler.RefreshGiftTip()
    elseif mess == "Package" then
        UpdateRedDotHandler.RefreshPackageTip()
    elseif mess == "Badge" then
        UpdateRedDotHandler.RefreshBadgeTip()
    elseif mess == "Fashion" then
        UpdateRedDotHandler.RefreshFashionTip()
    elseif mess == "UISkillManager" then
        UpdateRedDotHandler.RefreshSkillTip()
    end
end

-- UNREAD			= 1, --未读取
-- READ_NOT_GET		= 2, --读取并且未领取附件
function UpdateRedDotHandler.RefreshMailTip( ... )
    local mailList = MainPlayer.Instance.MailList
    if not mailList then return end

    local enum = mailList:GetEnumerator()
    while enum:MoveNext() do
        local mail = enum.Current
        if mail and (mail.state == 1 or mail.state == 2) then
            UpdateRedDotHandler.UpdateState["UIMail"] = true
            UpdateRedDotHandler.CheckUI("UIMail")
            return
        end
    end

    UpdateRedDotHandler.UpdateState["UIMail"] = false
    UpdateRedDotHandler.CheckUI("UIMail")
    -- print('UpdateRedDotHandler.UpdateState["UIMail"] = ' , UpdateRedDotHandler.UpdateState["UIMail"])
end

function UpdateRedDotHandler.RefreshGiftTip( ... )
    -- old activity
    -- local inTime = GameSystem.Instance.presentHpConfigData:IsGetPresentHP()
    -- local getHp = false
    -- --print("inTime=====>>>>>>" .. tostring(inTime))
    -- if inTime == 1 and MainPlayer.Instance.MoonHp == 0 then
    --  getHp = true
    -- elseif inTime == 2 and MainPlayer.Instance.EvenHp == 0 then
    --  getHp = true
    -- elseif inTime == 3 and MainPlayer.Instance.ThirdHp == 0 then
    --  getHp = true
    -- end
    -- if UpdateRedDotHandler.UpdateState["UIGiftBag"] ~= getHp then
    --  UpdateRedDotHandler.UpdateState["UIGiftBag"] = getHp
    --  UpdateRedDotHandler.CheckUI("UIGiftBag")
    -- end

    --new activity
    local flag = MainPlayer.Instance.NewComerSign.open_flag
    if flag ~= 0 then
        local index = MainPlayer.Instance.NewComerSign.sign_list.Count - 1
        local signDay = 0, signState
        for i=0,index do
            signState = MainPlayer.Instance.NewComerSign.sign_list:get_Item(i)
            if signState == 1 or signState == 2 then
                signDay = signDay + 1
            end
        end
        signState = MainPlayer.Instance.NewComerSign.sign_list:get_Item(index)
        if signState == 0 or signState == 3 then
            UpdateRedDotHandler.UpdateState["UINewComer"] = true
        else
            UpdateRedDotHandler.UpdateState["UINewComer"] = false
        end

        if signDay >= 3 and signDay < 7 then
            local state = MainPlayer.Instance.NewComerSign.sign_list:get_Item(2)
            if state ~= 2 and state ~= 3 then
                UpdateRedDotHandler.UpdateState["UINewComer"] = true
            end
        elseif signDay >= 7 then
            local state = MainPlayer.Instance.NewComerSign.sign_list:get_Item(6)
            if state ~= 2 and state ~= 3 then
                UpdateRedDotHandler.UpdateState["UINewComer"] = true
            end
        end
    else
        UpdateRedDotHandler.UpdateState["UINewComer"] = 0
        return
    end
    UpdateRedDotHandler.CheckUI("UINewComer")
end

function UpdateRedDotHandler.RefreshLotteryTip( ... )
    if not UpdateRedDotHandler.MainPlayerLotteryInfo then
        UpdateRedDotHandler.MainPlayerLotteryInfo = MainPlayer.Instance.LotteryInfo
    end
    if not UpdateRedDotHandler.MainPlayerLotteryInfo then
        return
    end
    if MainPlayer.Instance.Level < 5 then return end

    local lastTime1 = UpdateRedDotHandler.MainPlayerLotteryInfo.last_time1
    local lastTime2 = UpdateRedDotHandler.MainPlayerLotteryInfo.last_time2
    -- local freeTimes1 = UpdateRedDotHandler.MainPlayerLotteryInfo.free_times1
    -- local freeTimes2 = UpdateRedDotHandler.MainPlayerLotteryInfo.free_times2
    local nowTime = GameSystem.mTime
    if not UpdateRedDotHandler.restoreTime1 then
        UpdateRedDotHandler.restoreTime1 = GameSystem.Instance.CommonConfig:GetUInt('gNormalLotteryRestoreTime')
    end
    if not UpdateRedDotHandler.restoreTime2 then
        UpdateRedDotHandler.restoreTime2 = GameSystem.Instance.CommonConfig:GetUInt('gSpecialLotteryRestoreTime')
    end
    -- if freeTimes1 <= 0 then
    --  lastTime1 = nowTime
    -- end
    -- if freeTimes2 <= 0 then
    --  lastTime2 = nowTime
    -- end
    -- print('freeTimes2 = ', freeTimes2)
    -- local state = (nowTime - lastTime1 <= UpdateRedDotHandler.restoreTime1 and freeTimes2 < 1)
    if lastTime1 == 0 or lastTime2 == 0 then
        UpdateRedDotHandler.UpdateState["UILottery"] = true
        UpdateRedDotHandler.CheckUI("UILottery")
    else
        local state = (nowTime - lastTime1 <= UpdateRedDotHandler.restoreTime1 and nowTime - lastTime2 <= UpdateRedDotHandler.restoreTime2)
        -- print('nowTime = ', nowTime, ' lastTime1 = ', lastTime1, ' restoreTime1 = ', UpdateRedDotHandler.restoreTime1)
        -- print('nowTime = ', nowTime, ' lastTime2 = ', lastTime2, ' restoreTime2 = ', UpdateRedDotHandler.restoreTime2)
        if UpdateRedDotHandler.UpdateState["UILottery"] == state then
            UpdateRedDotHandler.UpdateState["UILottery"] = not state
            UpdateRedDotHandler.CheckUI("UILottery")
        end
    end
end

function UpdateRedDotHandler.RefreshSignTip( ... )
    if not UpdateRedDotHandler.MainPlayerSignInfo then
        UpdateRedDotHandler.MainPlayerSignInfo = MainPlayer.Instance.signInfo
        UpdateRedDotHandler.roleMaxQuality = GameSystem.Instance.CommonConfig:GetUInt("gMaxQualityNum")
    end
    if not UpdateRedDotHandler.MainPlayerSignInfo then
        return
    end
    local v = UpdateRedDotHandler.MainPlayerSignInfo.sign_award
    local t = UpdateRedDotHandler.MainPlayerSignInfo.signed_times
    local totalaward = GameSystem.Instance.signConfig:GetMonthSignData(v)
    local state = UpdateRedDotHandler.MainPlayerSignInfo.signed == 0 or t >= totalaward.sign_times

    UpdateRedDotHandler.UpdateState["UISign"] = state
    UpdateRedDotHandler.CheckUI("UISign")
end

function UpdateRedDotHandler.RefreshNewComerTrialTip( ... )
    UpdateRedDotHandler.UpdateState["NewComerTrial"] = false
    -- create_time
    local create_time = MainPlayer.Instance.CreateTime
    local now = os.date("*t", GameSystem.Instance.mTime).yday 
    local create = os.date("*t", create_time).yday
    local createY =os.date("*t", create_time).year
    local curDay  = now-create 
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
        UpdateRedDotHandler.UpdateState["NewComerTrial"] = true
        return
    end
    --只有 当前天和之前天可以领取
    local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
    while enum:MoveNext() do
        if 7 == enum.Current.type then
            if enum.Current.state == 2 then
                for i=1,curDay do
                    local dic = GameSystem.Instance.trialConfig:GetTrialListByDay(i)
                    local enum1 = dic:GetEnumerator()
                    while enum1:MoveNext() do
                        if enum1.Current.id == enum.Current.id then 
                            UpdateRedDotHandler.UpdateState["NewComerTrial"] = true
                            return
                        end
                    end
                end
            end
        end
    end
end

-- function UpdateRedDotHandler.RefreshButtonMenuTip( ... )
--  -- body
-- end

function UpdateRedDotHandler.RefreshRoleTip( ... )
    UpdateRedDotHandler.roleRecruitList = {}
    --招募
    local roleBase = GameSystem.Instance.RoleBaseConfigData2:GetConfig()
    local enum = roleBase:GetEnumerator()
    while enum:MoveNext() do
        local v = enum.Current
        local baseData =  GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id)
        --添加球员时会出现错误，暂时屏蔽
        --local costId = baseData.recruit_consume_id
        --local costValue = baseData.recruit_consume_value
        local allListEnum = MainPlayer.Instance.FavoriteGoodsList:GetEnumerator()
        --print('costId ----------- ' .. costId .. '   costValue ------------- ' .. costValue)
        while allListEnum:MoveNext() do

-- local n = allListEnum.Current.Value
--          if n:GetID() == costId and n:GetNum() >= costValue then
--              UpdateRedDotHandler.roleRecruitList[v.id] = 1
--          end

        end
    end

    if next(UpdateRedDotHandler.roleRecruitList) then
        UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIRole"] = true
    else
        --名人堂
        UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIRole"] = IsRefreshMap > 1
    end
    UpdateRedDotHandler.CheckUI("Others")
end

function UpdateRedDotHandler.RefreshSquadTip( ... )
    --删选出品质最高的未装备的物品
    local equipmentList = {}
    local enum = MainPlayer.Instance.EquipmentGoodsList:GetEnumerator()
    while enum:MoveNext() do
        local goods = enum.Current.Value
        if goods:IsEquip() == false then
            local cate = goods:GetSubCategory()
            if equipmentList[cate] then
                local curQuality = enumToInt(equipmentList[cate]:GetQuality())
                local nextQuality = enumToInt(goods:GetQuality())
                if curQuality < nextQuality then
                    equipmentList[cate] = goods
                end
            else
                equipmentList[cate] = goods
            end
        end
    end

    --当前的装备信息
    local equipInfo = MainPlayer.Instance.EquipInfo
    local enum = equipInfo:GetEnumerator()
    while enum:MoveNext() do
        local info = {}
        local equip = enum.Current
        local status = equip.pos
        local enumSlot = equip.slot_info:GetEnumerator()
        while enumSlot:MoveNext() do
            local cate = EquipmentType.IntToEnum(enumToInt(enumSlot.Current.id))
            if equipmentList[cate] then
                local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_EQUIPMENT,enumSlot.Current.equipment_uuid)
                if goods then
                    local goodsQuality = enumToInt(goods:GetQuality())
                    local equipQuality = enumToInt(equipmentList[cate]:GetQuality())
                    if goodsQuality < equipQuality then
                        table.insert(info, enumToInt(cate))
                    elseif goodsQuality == equipQuality then
                        local goodsLevel = goods:GetLevel()
                        local equipLevel = equipmentList[cate]:GetLevel()
                        if goodsLevel < equipLevel then
                            table.insert(info, enumToInt(cate))
                        end
                    end
                else
                    table.insert(info, enumToInt(cate))
                end
            end
        end
        UpdateRedDotHandler.equipTipList[status] = info
    end

    for k,v in pairs(UpdateRedDotHandler.equipTipList) do
        if table.getn(v) > 0 then
            UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"] = true
            UpdateRedDotHandler.CheckUI("Others")
            return
        end
    end
    UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"] = false
    UpdateRedDotHandler.CheckUI("Others")
end

function UpdateRedDotHandler.RefreshRoleDetailTip()
    local showRedDot = false
    UpdateRedDotHandler.roleEnhanceList = {}
    UpdateRedDotHandler.roleImproveList = {}

    local config = GameSystem.Instance.skillUpConfig
    local roleEnum = MainPlayer.Instance.SquadInfo:GetEnumerator()
    while roleEnum:MoveNext() do
        local roleId = roleEnum.Current.role_id
        if not roleId or roleId <= 0 then
            -- print('error, have not this roleId')
            return
        end

        --level up
        if CanRoleLvUp(roleId) then
            -- print('can level up -------------->>>>>>>>')
            showRedDot = true
            UpdateRedDotHandler.roleLevelUpList[roleId] = true
        else
            UpdateRedDotHandler.roleLevelUpList[roleId] = false
        end
        ---------

        --enhance
        local curStar = MainPlayer.Instance:GetRole2(roleId).star
        if curStar < 10 then
            -- print('roleId = ', roleId, ' curStar = ', curStar)
            local starData = GameSystem.Instance.starAttrConfig:GetStarAttr(roleId, curStar + 1)
            local enum = starData.consume:GetEnumerator()
            -- CommonFunction.ClearGridChild(self.uiEnhanceStarConsumeGrid.transform)
            -- self.enhanceStarGoodsList = {}
            local totalNum = 0
            local num = 0
            while enum:MoveNext() do
                local consumeId = enum.Current.Key
                local consumeValue = enum.Current.Value
                totalNum = totalNum + 1

                local ownedNum = MainPlayer.Instance:GetGoodsCount(consumeId)
                if ownedNum >= consumeValue then
                    num = num + 1
                end
            end
            if totalNum == num then
                showRedDot = true
                -- print(roleId, 'can player enhance')
                UpdateRedDotHandler.roleEnhanceList[roleId] = true
            else
                UpdateRedDotHandler.roleEnhanceList[roleId] = false
            end
        else
            UpdateRedDotHandler.roleEnhanceList[roleId] = false
        end

        --skill
        local skillInfo = {}
        totalNum = 0
        local roleQuality = MainPlayer.Instance:GetRole2(roleId).quality
        local exeList = MainPlayer.Instance:GetExerciseInfoList(roleId)
        local enumList = exeList:GetEnumerator() --enum.Current.Value:GetEnumerator()
        while enumList:MoveNext() do
            local id = enumList.Current.id

            local level = MainPlayer.Instance:GetExerciseLevel(roleId,id)
            if level < 5 * roleQuality then
                -- Consume.
                local skillConsume = config:GetSkillConsume(id, level + 1)
                local skillEnum = skillConsume and skillConsume:GetEnumerator()
                local num = 0
                if skillEnum then
                    while skillEnum:MoveNext() do
                        local id = skillEnum.Current.Key
                        local value = skillEnum.Current.Value
                        if id == 2 then
                            --  gold
                            if value <= MainPlayer.Instance.Gold then
                                num = num + 1
                            end
                        else
                            --consume
                            local owned = MainPlayer.Instance:GetGoodsCount(id)
                            if owned >= value then
                                num = num + 1
                            end
                        end
                    end
                end
                if num == 2 then
                    -- print('can skill train --------------->>>>>>>')
                    skillInfo[id] = true
                    showRedDot = true
                end
                num = 0
            elseif level == 5 * roleQuality then
                totalNum = totalNum + 1
            end
        end
        if totalNum == exeList.Count and roleQuality < UpdateRedDotHandler.roleMaxQuality then
            showRedDot = true
            UpdateRedDotHandler.roleImproveList[roleId] = true
        end
        UpdateRedDotHandler.roleSkillList[roleId] = skillInfo
    end
    if showRedDot then
        UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"] = true
        UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIRole"] = true
        UpdateRedDotHandler.CheckUI("Others")
    end
end

function UpdateRedDotHandler.RefreshTaskTip( ... )
    local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
    while enum:MoveNext() do
        local type = enum.Current.type --FESTIVAL = 1;//活动 MAIN = 2;//主线任务 DAILY = 3;//日常任务 SIGN = 4;//签到任务 LEVEL = 6;//成长任务
        local state = enum.Current.state --2:finish 3:getaward
        local taskID = enum.Current.id
        if type == 2 and state == 2 and GameSystem.Instance.TaskConfigData:GetTaskMainInfoByID(taskID) ~= nil then
            -- print('have task award ---------------')
            UpdateRedDotHandler.UpdateState["UITask"] =  true
            UpdateRedDotHandler.CheckUI("UITask")
            return
        end
    end
    UpdateRedDotHandler.UpdateState["UITask"] =  false
    UpdateRedDotHandler.CheckUI("UITask")
end

function UpdateRedDotHandler.RefreshDailyTip( ... )
    -- print ("do daily check.....")
    local enum = MainPlayer.Instance.taskInfo.task_list:GetEnumerator()
    while enum:MoveNext() do
        local type = enum.Current.type --FESTIVAL = 1;//活动 MAIN = 2;//主线任务 DAILY = 3;//日常任务 SIGN = 4;//签到任务 LEVEL = 6;//成长任务
        local state = enum.Current.state --2:finish 3:getaward
        local taskID = enum.Current.id
        if type == 3 or type == 6 then
            if state == 2 and GameSystem.Instance.TaskConfigData:GetTaskDailyInfoByID(taskID) ~= nil then
                -- print('have daily award ---------------')
                UpdateRedDotHandler.UpdateState["UITaskLevel"] =  true
                UpdateRedDotHandler.CheckUI("UITaskLevel")
                return
            end
        end
    end
    UpdateRedDotHandler.UpdateState["UITaskLevel"] =  false
    UpdateRedDotHandler.RefreshActivityTip()
end

function UpdateRedDotHandler.RefreshActivityTip( ... )
    -- print ("do activity check........")
    local enum = MainPlayer.Instance.activityInfo.gift:GetEnumerator()
    while enum:MoveNext() do
        if enum.Current == 2 then
            -- print('have activity award ---------------')
            UpdateRedDotHandler.UpdateState["UITaskLevel"] =  true
            UpdateRedDotHandler.CheckUI("UITaskLevel")
            return
        end
    end
    UpdateRedDotHandler.UpdateState["UITaskLevel"] =  false
    UpdateRedDotHandler.RefreshLevelStep()
end

function UpdateRedDotHandler.RefreshLevelStep()
    -- print ("do levlestep check.........")
    local levelAwardDic = MainPlayer.Instance.playerLevelAwardInfo:GetEnumerator()
    local level = MainPlayer.Instance.Level

    while levelAwardDic:MoveNext() do
        -- print(levelAwardDic.Current.Value)
        -- print(levelAwardDic.Current.Key)
        -- print(level)

        if levelAwardDic.Current.Value == 0 and levelAwardDic.Current.Key <= level then
            -- print ('have level award------------------')
            UpdateRedDotHandler.UpdateState["UITaskLevel"] =  true
            UpdateRedDotHandler.CheckUI("UITaskLevel")
            return
        end
    end

    UpdateRedDotHandler.UpdateState["UITaskLevel"] =  false
    UpdateRedDotHandler.CheckUI("UITaskLevel")
end

function UpdateRedDotHandler.RefreshCareerTip( ... )
    local chapterID = 0
    local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    while enum:MoveNext() do
        local configID = enum.Current.Key
        if MainPlayer.Instance:CheckChapter(configID) then
            chapterID = configID
            local chapter = MainPlayer.Instance:GetChapter(chapterID)
            local chapterConfig = GameSystem.Instance.CareerConfigData:GetChapterData(chapterID)
            local num1 = chapterConfig.bronze_value
            local num2 = chapterConfig.silver_value
            local num3 = chapterConfig.gold_value
            local getNum = chapter.star_num

            if getNum >= num1 and not chapter.is_bronze_awarded then
                UpdateRedDotHandler.UpdateState["UICareer"] =  true
                UpdateRedDotHandler.CheckUI("UICareer")
                break
            elseif getNum >= num2 and not chapter.is_silver_awarded then
                UpdateRedDotHandler.UpdateState["UICareer"] =  true
                UpdateRedDotHandler.CheckUI("UICareer")
                break
            elseif getNum >= num3 and not chapter.is_gold_awarded then
                UpdateRedDotHandler.UpdateState["UICareer"] =  true
                UpdateRedDotHandler.CheckUI("UICareer")
                break
            else
                UpdateRedDotHandler.UpdateState["UICareer"] =  false
                UpdateRedDotHandler.CheckUI("UICareer")
                break
            end

            break
        end
    end
end

function UpdateRedDotHandler.RefreshPackageTip( ... )
    if NeedGetGift == nil then
        NeedGetGift = false
    end
    UpdateRedDotHandler.UpdateState["UIPackage"] = NeedGetGift
    UpdateRedDotHandler.CheckUI("UIPackage")
end

function UpdateRedDotHandler.RefreshBadgeTip( ... )
    if HaveNewBadge == nil then
        HaveNewBadge = false
    end
    UpdateRedDotHandler.UpdateState["UIBadge"] = HaveNewBadge
    UpdateRedDotHandler.CheckUI("UIBadge")
end

function UpdateRedDotHandler.RefreshFashionTip( ... )
    if ShowFashionTip == nil then
        ShowFashionTip = UintList.New()
    end
    UpdateRedDotHandler.UpdateState["UIFashion"] = ShowFashionTip
    UpdateRedDotHandler.CheckUI("UIFashion")
end

function UpdateRedDotHandler.OperateFashionTip(operateType ,type)
    if operateType == "Add" then
        if not ShowFashionTip:Contains(type) then
            ShowFashionTip:Add(type)
            UpdateRedDotHandler.RefreshFashionTip()
        end
    elseif operateType == "Delete" then
        if ShowFashionTip:Contains(type) then
            ShowFashionTip:Remove(type)
            UpdateRedDotHandler.RefreshFashionTip()
        end
    end
end


function UpdateRedDotHandler.RefreshSkillTip( ... )
    if ShowSkillTip == nil then
        ShowSkillTip = UintList.New()
    end
    UpdateRedDotHandler.UpdateState["UISkillManager"] = ShowSkillTip
    UpdateRedDotHandler.CheckUI("UISkillManager")
    -- 未开放
    -- if MainPlayer.Instance.Level < 3 then
    --     UpdateRedDotHandler.UpdateState["UISkillManager"] = false
    --     UpdateRedDotHandler.CheckUI("UISkillManager")

    --     return
    -- end

    -- 有技能可学习
    -- local storeConfig = GameSystem.Instance.StoreGoodsConfigData
    -- local skillList = storeConfig:GetStoreGoodsDataList(enumToInt(StoreType.ST_SKILL))

    -- local enum = skillList:GetEnumerator()
    -- while enum:MoveNext() do
    --     if enum.Current.apply_min_level <= MainPlayer.Instance.Level then
    --         UpdateRedDotHandler.UpdateState["UISkillManager"] = true
    --         UpdateRedDotHandler.CheckUI("UISkillManager")
    --         return
    --     end
    -- end

    -- UpdateRedDotHandler.UpdateState["UISkillManager"] = true
    -- UpdateRedDotHandler.CheckUI("UISkillManager")
end

function UpdateRedDotHandler.OperateSkillTip(operateType ,type)
    if operateType == "Add" then
        if not ShowSkillTip:Contains(type) then
            ShowSkillTip:Add(type)
            UpdateRedDotHandler.RefreshSkillTip()
        end
    elseif operateType == "Delete" then
        if ShowSkillTip:Contains(type) then
            ShowSkillTip:Remove(type)
            UpdateRedDotHandler.RefreshSkillTip()
        end
    end
end


function UpdateRedDotHandler.CheckUI(name)
    local root = UIManager.Instance.m_uiRootBasePanel.transform
    local nameTable = UpdateRedDotHandler.ShowUI[name]
    for i,v in ipairs(nameTable) do
        local trans = root
        if name ~= "Others" then
            trans = root:Find(v)
        else
            trans = root:Find(v .. '/Top/ButtonMenu/ButtonMenu(Clone)')
        end
        -- print (root.transform:Find(v).name)
        if trans then
            local lua = getLuaComponent(trans.gameObject)
            if lua then
                lua:SetRedDot(name)
            end
        end
    end
end

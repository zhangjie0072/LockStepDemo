--encoding=utf-8

UISkillManager = {
    uiName = 'UISkillManager',

    ----------------------------------

    banTwice1 = false,
    banTwice2 = false,
    currentLeftTab,
    currentTopTab,

    playerPos,
    skillConfig,
    storeConfig,
    mainPlayer,
    roleID,
    defaultRoleID,
    roleSkillInfo,
    roleConfig,
    skillList = {},
    skillDetail,

    currentSkillItem,
    changePlayerFlag = false,
    buyMsg,
    onCloseEvent,	--关闭回调

    ----------------------------------UI

    uiBtnBack,
    uiTitle,
    uiLeftTab = {},
    uiTopTab = {},
    uiWear,
    uiWearGrid,
    uiWareHouse,
    uiWareHouseGrid,
    uiWareHouseProgress,
    uiWearSlot = {},
    uiWearCancel = {},
    uiBtnChangePlayer,
    uiPlayerIcon,
    uiAnimator,
    uiTips,
    uiWearHouseRedDot,

    isFirstIn = true,
    isTween = false,
}

local positions ={getCommonStr('STR_ALL'),'PF','SF','C','PG','SG'}

-----------------------------------------------------------------
function UISkillManager:Awake()
    self.uiBtnBack = createUI("ButtonBack", self.transform:FindChild('Top/ButtonBack'))
    self.uiTitle = self.transform:FindChild('Top/Title'):GetComponent('MultiLabel')

    self.uiLeftTab[1] = self.transform:FindChild('Left/ButtonWarehouse'):GetComponent('UIToggle')
    self.uiLeftTab[2] = self.transform:FindChild('Left/ButtonWear'):GetComponent('UIToggle')

    self.uiTopTab[1] = self.transform:FindChild('Right/TabGrid/TabAll'):GetComponent('UIToggle')
    self.uiTopTab[2] = self.transform:FindChild('Right/TabGrid/TabShoot'):GetComponent('UIToggle')
    self.uiTopTab[3] = self.transform:FindChild('Right/TabGrid/TabDunk'):GetComponent('UIToggle')
    self.uiTopTab[4] = self.transform:FindChild('Right/TabGrid/TabLayup'):GetComponent('UIToggle')
    self.uiTopTab[5] = self.transform:FindChild('Right/TabGrid/TabBackboard'):GetComponent('UIToggle')
    self.uiTopTab[6] = self.transform:FindChild('Right/TabGrid/TabBlockShot'):GetComponent('UIToggle')
    self.uiTopTab[7] = self.transform:FindChild('Right/TabGrid/TabPass'):GetComponent('UIToggle')

    self.uiTips = {}
    self.uiTips[1] = self.transform:FindChild('Right/TabGrid/TabShoot/Tip'):GetComponent('UISprite')
    self.uiTips[2] = self.transform:FindChild('Right/TabGrid/TabDunk/Tip'):GetComponent('UISprite')
    self.uiTips[3] = self.transform:FindChild('Right/TabGrid/TabLayup/Tip'):GetComponent('UISprite')
    self.uiTips[4] = self.transform:FindChild('Right/TabGrid/TabBackboard/Tip'):GetComponent('UISprite')
    self.uiTips[5] = self.transform:FindChild('Right/TabGrid/TabBlockShot/Tip'):GetComponent('UISprite')
    self.uiTips[6] = self.transform:FindChild('Right/TabGrid/TabPass/Tip'):GetComponent('UISprite')



    --wear
    self.uiWear = self.transform:FindChild('Right/Wear')
    self.uiWearGrid = self.transform:FindChild('Right/Wear/ScrollView/Grid'):GetComponent('UIGrid')
    self.uiWearProgress = self.transform:FindChild('Right/Wear/ScrollView/Progress'):GetComponent('UIProgressBar')
    for i=1,4 do
        self.uiWearSlot[i] = self.transform:FindChild('Right/Wear/Down/Frame/Icon' .. tostring(i) .. '/Icon')
        self.uiWearCancel[i] = self.transform:FindChild('Right/Wear/Down/Frame/Icon' .. tostring(i) .. '/ButtonCancel'):GetComponent('UIButton')
    end
    self.uiBtnChangePlayer = self.transform:FindChild('Right/Wear/Down/ButtonChange'):GetComponent('UIButton')
    self.uiPlayerIcon = self.transform:FindChild('Right/Wear/Down/CareerIcon')
    --wearhouse
    self.uiWareHouse = self.transform:FindChild('Right/Warehouse')
    self.uiWareHouseGrid = self.transform:FindChild('Right/Warehouse/ScrollView/Grid'):GetComponent('UIGrid')
    self.uiWareHouseProgress = self.transform:FindChild('Right/Warehouse/Progress'):GetComponent('UIProgressBar')
    self.uiWearHouseRedDot = self.transform:FindChild('Left/ButtonWarehouse/RedDot')
    -- self.uiAnimator = self.transform:GetComponent('Animator')
end

function UISkillManager:Start()
    NGUITools.SetActive(self.uiWear.gameObject, true)
    NGUITools.SetActive(self.uiWareHouse.gameObject, false)

    LuaHelper.RegisterPlatMsgHandler(MsgID.SkillOperationRespID, self:SkillOperationResp(), self.uiName)
    addOnClick(self.uiBtnBack.gameObject, self:OnBackClick())
    addOnClick(self.uiBtnChangePlayer.gameObject, self:OnChangePlayerClick())
    addOnClick(self.uiLeftTab[1].gameObject, self:OnSwitchLeftTabClick())
    addOnClick(self.uiLeftTab[2].gameObject, self:OnSwitchLeftTabClick())
    for i=1,7 do
        addOnClick(self.uiTopTab[i].gameObject, self:OnSwitchSkillTabClick())
    end
    for i=1,4 do
        addOnClick(self.uiWearCancel[i].gameObject, self:OnCancelSkillClick(i))
    end
    -- self.currentLeftTab = self.uiLeftTab[1].gameObject
    -- self.currentTopTab = self.uiTopTab[1].gameObject

    --config
    self.skillConfig = GameSystem.Instance.SkillConfig
    self.storeConfig = GameSystem.Instance.StoreGoodsConfigData
    self.mainPlayer = MainPlayer.Instance
    self.roleConfig = GameSystem.Instance.RoleBaseConfigData2
    self.skillList = self.storeConfig:GetStoreGoodsDataList(enumToInt(StoreType.ST_SKILL))

    -- self:OnSwitchSkillTabClick()(self.uiTopTab[1].gameObject)
    -- self:ShowWearSkills()
    self.uiWearGrid.onCustomSort = self:WearGridCustomSort()
    -- self:OnSwitchLeftTabClick()(self.uiLeftTab[1].gameObject)
    -- self:OnSwitchLeftTabClick()(self.uiLeftTab[2].gameObject)

    -- refresh red dots
    self:RefreshWearHouseRedDot()
    self.isFirstIn = false
end

function UISkillManager:OnEnable()
    -- enable
    if not self.isFirstIn then
        self:RefreshWearHouseRedDot()
    end
    self.isTween = false
end

function UISkillManager:FixedUpdate( ... )
    -- body
end

function UISkillManager:OnClose( ... )
    self:OnSwitchLeftTabClick()(self.uiLeftTab[2].gameObject)
    self.uiLeftTab[2].value = true
    self.defaultRoleID = nil
    if self.onCloseEvent then
        self.onCloseEvent()
    end
    TopPanelManager:HideTopPanel()

    self:OnDestroy()
end

function UISkillManager:OnDestroy()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.SkillOperationRespID, self.uiName)

    --Object.Destroy(self.uiAnimator)
    --Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UISkillManager:Refresh(subID)
    print ( "整个刷新")
    print('self.defaultRoleID = ', self.defaultRoleID)
    print('self.roleID = ', self.roleID)
    print('subID = ', subID)
    if subID and subID == 1 then
        self.uiLeftTab[2].startsActive = true
        self.uiLeftTab[2].value = true
    elseif subID and subID == 2 then
        self.uiLeftTab[1].startsActive = true
        self.uiLeftTab[1].value = true
    elseif subID and MainPlayer.Instance:HasRole(subID) then
        self.defaultRoleID = subID
        self.uiLeftTab[2].startsActive = true
        self.uiLeftTab[2].value = true
    elseif not subID then
        self.uiLeftTab[2].startsActive = true
        self.uiLeftTab[2].value = true
    end
    if self.defaultRoleID and not self.roleID then
        self.roleID = self.defaultRoleID
        self:ShowWearSkills()
        -- self:OnSwitchLeftTabClick()(self.uiLeftTab[1].gameObject)
        self:OnSwitchLeftTabClick()(self.uiLeftTab[2].gameObject)
    elseif self.defaultRoleID and self.defaultRoleID ~= self.roleID then
        self:ChoosePlayer()(self.defaultRoleID)
    elseif not self.defaultRoleID and not self.roleID then
        self:ShowWearSkills()
        -- self:OnSwitchLeftTabClick()(self.uiLeftTab[1].gameObject)
        self:OnSwitchLeftTabClick()(self.uiLeftTab[2].gameObject)
    end
    if subID then
        if subID == 2 then
            self:OnSwitchLeftTabClick()(self.uiLeftTab[1].gameObject)
        end
        subID = nil
    end
end


-----------------------------------------------------------------
function UISkillManager:SetDefaultRoleID(roleID)
    if roleID then
        self.roleID = roleID
    end
    if not self.mainPlayer or not self.roleConfig then
        self.mainPlayer = MainPlayer.Instance
        self.roleConfig = GameSystem.Instance.RoleBaseConfigData2
    end
    local roleInfo = self.mainPlayer:GetRole2(self.roleID)
    if roleInfo then
        self.roleSkillInfo = roleInfo.skill_slot_info
        local roleConfig = self.roleConfig:GetConfigData(self.roleID)
        self.playerPos = roleConfig.position
    else
        error('do not have this player!')
    end
end

function UISkillManager:OnBackClick( ... )
    return function (go)
        self:DoClose()
    end
end

function UISkillManager:DoClose( ... )
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
end

function UISkillManager:OnSwitchLeftTabClick( ... )
    return function (go)
        print ("我看看进来几次")
        if go == self.currentLeftTab then
            return
        end
        self.currentLeftTab = go
        self.currentTopTab = nil
        -- print('self.currentLeftTab.name = ', self.currentLeftTab.gameObject.name)
        NGUITools.SetActive(self.uiWear.gameObject, self.currentLeftTab == self.uiLeftTab[2].gameObject)
        NGUITools.SetActive(self.uiWareHouse.gameObject, self.currentLeftTab == self.uiLeftTab[1].gameObject)

        self.uiTopTab[1].value = true
        if go == self.uiLeftTab[1].gameObject then
            -- self:OnSwitchLeftTabClick()(self.uiLeftTab[1].gameObject)
            self:OnSwitchSkillTabClick()(self.uiTopTab[1].gameObject)
            self.uiTitle:SetText(getCommonStr('STR_SKILL_WH'))
            self.uiWearHouseRedDot.gameObject:SetActive(false)
        elseif go == self.uiLeftTab[2].gameObject then
            self:OnSwitchSkillTabClick()(self.uiTopTab[1].gameObject)
            self.uiTitle:SetText(getCommonStr('STR_SKILL_WEAR'))
        end
    end
end

function UISkillManager:OnSwitchSkillTabClick()
    return function (go)
        if go == self.currentTopTab then
            return
        end
        self.currentTopTab = go
        self:RefreshTopTabSkills()
    end
end

function UISkillManager:RefreshTopTabSkills()
    print (" 刷新侧面技能按钮")

    local type = 0
    --原投篮页签下显示投篮和上篮类型技能。
    --上篮页签改为突破，显示突破类型技能。
    --仓库和佩戴页面显示规则一致。
    --0：无；1：投篮；2：上篮；3：扣篮；4：盖帽；5：篮板；6：突破；7：传球；
    local typeList = {0, 1, 3, 6, 5, 4, 7}
    for i=1,7 do
        if self.currentTopTab == self.uiTopTab[i].gameObject then
            self.uiTopTab[i].value = true
            type = typeList[i]
            if self.currentLeftTab == self.uiLeftTab[1].gameObject then
                UpdateRedDotHandler.OperateSkillTip("Delete", i - 1)
            end
            self:RefreshTip()
            break
        end
    end
    local isWarehouse = (self.currentLeftTab.gameObject == self.uiLeftTab[1].gameObject)
    self:RefreshSkills(isWarehouse, type)
end

function UISkillManager:OnChangePlayerClick( ... )
    return function (go)
        if self.banTwice1 then
            return
        end
        self.isTween = false
        self.banTwice1 = true
        local changeRole = getLuaComponent(createUI('FashionRole'))
        changeRole.onChoose = self:ChoosePlayer()
        changeRole.onBack = function ( ... )
            for i=1,7 do
                if self.currentTopTab == self.uiTopTab[i].gameObject then
                    self.uiTopTab[i].value = true
                    break
                end
            end
            self.banTwice1 = false
        end

        UIManager.Instance:BringPanelForward(changeRole.gameObject)
    end
end

function UISkillManager:ChoosePlayer( ... )
    return function (roleID)
        if self.roleID == roleID then
            return
        end
        print('choose roleID = ', roleID)
        self.changePlayerFlag = true
        self.roleID = roleID
        local roleInfo = self.mainPlayer:GetRole2(self.roleID)
        self.roleSkillInfo = roleInfo.skill_slot_info
        local roleConfig = self.roleConfig:GetConfigData(self.roleID)
        self.playerPos = roleConfig.position
        -- roleInfo skill info
        self:ShowWearSkills()
        self:RefreshTopTabSkills()
    end
end

function UISkillManager:RefreshWearHouseRedDot()
    local enum = self.skillList:GetEnumerator()
    while enum:MoveNext() do
        if UpdateRedDotHandler.UpdateState["UISkillManager"].Count > 0 then
            self.uiWearHouseRedDot.gameObject:SetActive(true)
            return
        end
    end

    self.uiWearHouseRedDot.gameObject:SetActive(false)
end

function UISkillManager:RefreshSkills(isWarehouse, tabIndex)
    -- tabIndex = 0 :ALL
    -- tabIndex <= 6
    print('isWarehouse = ', isWarehouse)
    if isWarehouse then
        if self.uiWareHouseGrid.transform.childCount <= 0 then
            local enum = self.skillList:GetEnumerator()
            while enum:MoveNext() do
                local skillAttr = self.skillConfig:GetSkill(enum.Current.store_good_id)
                local goodsList = self.mainPlayer:GetGoodsList(GoodsCategory.GC_SKILL, enum.Current.store_good_id)
                if skillAttr and skillAttr.action_type <=7 then
                    local skillItem = createUI('SkillItem', self.uiWareHouseGrid.transform)
                    skillItem.name = "SkillItem" .. skillAttr.id
                    local skillItemLua = getLuaComponent(skillItem.gameObject)
                    skillItemLua.skillAttr = skillAttr
                    skillItemLua.skillManager = self
                    skillItemLua.id = enum.Current.store_good_id
                    skillItemLua.isSell = enum.Current.is_sell == 0
                    skillItemLua.limitLearnLevel = enum.Current.apply_min_level
                    skillItemLua.isCanLearn = enum.Current.apply_min_level <= MainPlayer.Instance.Level
                    print('goodsList.Count = ', goodsList.Count)
                    skillItemLua.isOwn = goodsList.Count > 0
                    skillItemLua.isInWareHouse = true
                    if skillItemLua.isOwn then
                        skillItemLua.goods = goodsList:get_Item(0)
                        print('own id = ', skillItemLua.id)
                    end
                    skillItemLua.onClick = self:OnOpenSkillDetail(skillItemLua)
                    skillItemLua:Refresh()
                end
            end
        else
            self:HideBySkillType(tabIndex, true)
        end

        self.uiWareHouseProgress.value = 0.0
        self.uiWareHouseGrid.hideInactive = true
        self.uiWareHouseGrid.repositionNow = true
        self.uiWareHouseGrid:Reposition()
    else
        if self.uiWearGrid.transform.childCount <= 0 then
            local enum = self.skillList:GetEnumerator()
            while enum:MoveNext() do
                local skillAttr = self.skillConfig:GetSkill(enum.Current.store_good_id)
                local goodsList = self.mainPlayer:GetGoodsList(GoodsCategory.GC_SKILL, enum.Current.store_good_id)
                if skillAttr and skillAttr.action_type <=7
                    and enum.Current.apply_min_level <= MainPlayer.Instance.Level
                then --and
                    -- (skillAttr.positions:Contains(self.playerPos) or skillAttr.positions:get_Item(0) == 0) then
                    local skillItem = createUI('SkillItem', self.uiWearGrid.transform)
                    skillItem.name = "SkillItem" .. skillAttr.id
                    local skillItemLua = getLuaComponent(skillItem.gameObject)
                    skillItemLua.skillAttr = skillAttr
                    skillItemLua.skillManager = self
                    skillItemLua.id = enum.Current.store_good_id
                    skillItemLua.isSell = enum.Current.is_sell == 0
                    skillItemLua.limitLearnLevel = enum.Current.apply_min_level
                    skillItemLua.isCanLearn = enum.Current.apply_min_level <= MainPlayer.Instance.Level
                    print('goodsList.Count = ', goodsList.Count)
                    skillItemLua.isOwn = goodsList.Count > 0
                    skillItemLua.isInWareHouse = false
                    skillItemLua.isWear = false
                    if goodsList.Count > 0 then
                        skillItemLua.goods = goodsList:get_Item(0)
                        skillItemLua.isWear = self.mainPlayer:CheckSkillInRole(self.roleID, skillItemLua.goods:GetUUID())
                    end
                    if not skillItemLua.isWear then
                        skillItemLua.onClick = self:OnOpenSkillDetail(skillItemLua)
                    else
                        skillItemLua.onClick = self:OnOperateSkillClick("SOT_UNEQUIP")
                    end
                    skillItemLua:Refresh()

                    if not skillAttr.positions:Contains(self.playerPos) and skillAttr.positions:get_Item(0) ~= 0 then
                        NGUITools.SetActive(skillItem.gameObject, false)
                    end
                end
            end
        else
            if self.changePlayerFlag then
                for i=1,self.uiWearGrid.transform.childCount do
                    local child = self.uiWearGrid.transform:GetChild(i - 1)
                    local childLua = getLuaComponent(child.gameObject)
                    childLua.isWear = false
                    if childLua.goods then
                        childLua.isWear = self.mainPlayer:CheckSkillInRole(self.roleID, childLua.goods:GetUUID())
                    end
                    if not childLua.isWear then
                        childLua.onClick = self:OnOpenSkillDetail(childLua)
                    else
                        childLua.onClick = self:OnOperateSkillClick("SOT_UNEQUIP")
                    end
                    childLua:Refresh()
                end
                self.changePlayerFlag = false
            end
            self:HideBySkillType(tabIndex, false)
        end
        self.uiWearProgress.value = 0.0
        self.uiWearGrid.hideInactive = true
        self.uiWearGrid.repositionNow = true
        self.uiWearGrid:Reposition()
    end
end

function UISkillManager:ShowWearSkills( ... )
    if not self.roleID then
        self.roleID = self.mainPlayer:GetFightRole(FightStatus.FS_MAIN)
    end
    self:SetDefaultRoleID(self.roleID)
    if self.roleSkillInfo then
        if self.uiPlayerIcon.transform.childCount <= 0 then
            local roleIcon = createUI('CareerRoleIcon', self.uiPlayerIcon.transform)
            local roleIconLua = getLuaComponent(roleIcon.gameObject)
            roleIconLua.id = self.roleID
            addOnClick(roleIcon.gameObject, self:OnChangePlayerClick())
        else
            local roleIcon = self.uiPlayerIcon.transform:GetChild(0).gameObject
            local roleIconLua = getLuaComponent(roleIcon.gameObject)
            roleIconLua.id = self.roleID
            roleIconLua:Refresh()
        end

        for i=1,4 do
            if self.uiWearSlot[i].transform.childCount > 0 then
                NGUITools.Destroy(self.uiWearSlot[i].transform:GetChild(0).gameObject)
            end
        end
        local pos = 1
        local enum = self.roleSkillInfo:GetEnumerator()
        while enum:MoveNext() do
            local skillUUID = enum.Current.skill_uuid
            if skillUUID ~= 0 then
                local goods = self.mainPlayer:GetGoods(GoodsCategory.GC_SKILL, skillUUID)
                local goodsIcon = createUI('GoodsIcon', self.uiWearSlot[pos].transform)
                NGUITools.SetActive(self.uiWearCancel[pos].gameObject, true)
                local goodsIconLua = getLuaComponent(goodsIcon.gameObject)
                print('goodsID = ', goods:GetID())
                goodsIconLua.goods = goods
                goodsIconLua.hideNeed = true
                goodsIconLua.hideNum = false
                pos = pos + 1
            end
        end
        for i=1,4 do
            if self.uiWearSlot[i].transform.childCount <= 0 and self.uiWearCancel[i].gameObject.activeSelf then
                NGUITools.SetActive(self.uiWearCancel[i].gameObject, false)
            end
        end
        if self.isTween then
            pos = pos - 1
            print('===================ShowEffect' .. 'pos:' .. pos)
            local tween = self.transform:FindChild('Right/Wear/Down/Frame/Icon' .. tostring(pos)):GetComponent('UITweener')
            local gameObejct = tween.gameObject
            Object.DestroyImmediate(tween)
            tween = TweenScale.Begin(gameObejct, 0.2, Vector3.New(1.1,1.1,1.1))
            NGUITools.SetActive(tween.transform.gameObject,true)
            tween.enabled = true
            tween:Play(true)
            tween:SetOnFinished(LuaHelper.Callback(self:IconTweenFinish(pos)))
            local goodsicon = getLuaComponent(tween.transform:FindChild('Icon/GoodsIcon(Clone)'))
            goodsicon:SetEf_GoodsiconDN(true)
        end
        self.isTween = true
    end
end

function UISkillManager:HideBySkillType(skillType, isWarehouse)
    -- print('hide ----------')
    if isWarehouse then
        local allSkill = self.uiWareHouseGrid.transform.childCount
        for i=0, allSkill - 1 do
            local item = self.uiWareHouseGrid.transform:GetChild(i)
            local itemLua = getLuaComponent(item.gameObject)
            if skillType ~= 0 then
                --原投篮页签下显示投篮和上篮类型技能。
                if skillType == 1 and (itemLua.skillAttr.action_type == 1 or itemLua.skillAttr.action_type == 2 ) then
                    if not item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, true)
                    end
                elseif itemLua.skillAttr.action_type == skillType then
                    if not item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, true)
                    end
                else
                    if item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, false)
                    end
                end
            else
                if not item.gameObject.activeSelf then
                    NGUITools.SetActive(item.gameObject, true)
                end
            end
        end
    else
        local allSkill = self.uiWearGrid.transform.childCount
        for i=0, allSkill - 1 do
            local item = self.uiWearGrid.transform:GetChild(i)
            local itemLua = getLuaComponent(item.gameObject)
            if skillType ~= 0 then
                ----原投篮页签下显示投篮和上篮类型技能。
                if skillType == 1 and (itemLua.skillAttr.action_type == 1 or itemLua.skillAttr.action_type == 2 ) then
                    if not item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, true)
                    end
                elseif itemLua.skillAttr.action_type == skillType then
                    if not item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, true)
                    end
                else
                    if item.gameObject.activeSelf then
                        NGUITools.SetActive(item.gameObject, false)
                    end
                end
            else
                ---------------
                if not item.gameObject.activeSelf then
                    NGUITools.SetActive(item.gameObject, true)
                end
            end
            if not itemLua.position[positions[self.playerPos + 1]] and not itemLua.position[positions[1]] then
                NGUITools.SetActive(item.gameObject, false)
            end
        end
    end
end

function UISkillManager:OnOpenSkillDetail(skillItem)
    return function (go)
        if self.banTwice1 then
            return
        end
        self.banTwice1 = true
        -- print('click')
        self.currentSkillItem = skillItem

        local wearSkillNum = 0
        local isFull = false
        for i=1,4 do
            if self.uiWearSlot[i].transform.childCount > 0 then
                wearSkillNum = wearSkillNum + 1
            end
        end
        if wearSkillNum >= 4 then
            isFull = true
        end

        self.skillDetail = createUI('SkillDetail')
        local skillDetailLua = getLuaComponent(self.skillDetail.gameObject)
        skillDetailLua.isOwn = skillItem.isOwn
        skillDetailLua.isWear = skillItem.isWear
        skillDetailLua.isSell = skillItem.isSell
        skillDetailLua.isFull = isFull
        skillDetailLua.isCanLearn = skillItem.isCanLearn
        skillDetailLua.limitLearnLevel = skillItem.limitLearnLevel
        skillDetailLua.id = skillItem.id
        skillDetailLua.uiSkillType.text = skillItem.uiSkillType.text
        skillDetailLua.isInWareHouse = skillItem.isInWareHouse
        if skillItem.goods then
            skillDetailLua.goods = skillItem.goods
            if not self.isWear and skillItem.isInWareHouse then
                skillDetailLua.onImproveClick = self:OnOperateSkillClick('SOT_UPGRADE')
            elseif not self.isWear and not skillItem.isInWareHouse then
                skillDetailLua.onWearClick = self:OnOperateSkillClick('SOT_EQUIP')
            end
        else
            skillDetailLua.onBuyClick = self:OnBuySkillClick()
        end
        skillDetailLua.onClose = function ( ... )
            self.banTwice1 = false
            self.banTwice2 = false

            self.skillDetail = nil
        end

        UIManager.Instance:BringPanelForward(self.skillDetail.gameObject)
    end
end

--operate
function UISkillManager:OnBuySkillClick( ... )
    return function (skillID, consumeIDs)
        if self.banTwice2 then
            return
        end
        self.banTwice2 = true
        for k,v in pairs(consumeIDs) do
            local consumeID = tonumber(k)
            local consumeNum = tonumber(v)
            if consumeID == 1 and self.mainPlayer.DiamondFree + self.mainPlayer.DiamondBuy < consumeNum then
                self:ShowBuyTip('BUY_DIAMOND')
                return
            elseif consumeID == 2 and self.mainPlayer.Gold < consumeNum then
                self:ShowBuyTip('BUY_GOLD')
                return
            elseif consumeID == 4 and self.mainPlayer.Hp < consumeNum then
                self:ShowBuyTip('BUY_HP')
                return
            elseif consumeID ~= 1 and consumeID ~= 2 and consumeID ~= 4 then
                local ownGoods = self.mainPlayer:GetGoodsList(GoodsCategory.GC_CONSUME, consumeID)
                if ownGoods and ownGoods.Count > 0 and ownGoods.Count < consumeNum then
                    local goodsItem = ownGoods:get_Item(0)
                    self:ShowBuyTip(goodsItem:GetName())
                    return
                end
            end
        end
        local storeItem = self.storeConfig:GetStoreGoodsData(enumToInt(StoreType.ST_SKILL), skillID)
        local index = self.skillList:IndexOf(storeItem)
        local pos = index + 1

        local storeInfo =
        {
            pos = pos,
            -- type = enumToInt(StoreType.ST_SKILL),
        }

        local req =
        {
            store_id = 'ST_SKILL',
            info = {storeInfo,},
        }

        local msg = protobuf.encode('fogs.proto.msg.BuyStoreGoods', req)
        LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)
        LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyStoreGoodsResp(), self.uiName)
        CommonFunction.ShowWait()
    end
end

function UISkillManager:OnOperateSkillClick(operateType)
    return function (goods, consumeIDs)
        if operateType == 'SOT_UPGRADE' then
            local skillLevel = goods:GetLevel()
            if skillLevel >= self.mainPlayer.Level then
                CommonFunction.ShowPopupMsg(getCommonStr('LEVEL_LIMIT_SKILL_LEVEL'),nil,nil,nil,nil,nil)
                return
            end

            for k,v in pairs(consumeIDs) do
                local consumeID = tonumber(k)
                local consumeNum = tonumber(v)
                if consumeID == 1 and self.mainPlayer.DiamondFree + self.mainPlayer.DiamondBuy < consumeNum then
                    self:ShowBuyTip('BUY_DIAMOND')
                    return
                elseif consumeID == 2 and self.mainPlayer.Gold < consumeNum then
                    self:ShowBuyTip('BUY_GOLD')
                    return
                elseif consumeID == 4 and self.mainPlayer.Hp < consumeNum then
                    self:ShowBuyTip('BUY_HP')
                    return
                elseif consumeID ~= 1 and consumeID ~= 2 and consumeID ~= 4 then
                    local ownGoods = self.mainPlayer:GetGoodsList(GoodsCategory.GC_CONSUME, consumeID)
                    if ownGoods and ownGoods.Count > 0 and ownGoods.Count < consumeNum then
                        local goodsItem = ownGoods:get_Item(0)
                        self:ShowBuyTip(goodsItem:GetName())
                        return
                    end
                end
            end
        else
            if self.banTwice2 then
                return
            end
            self.banTwice2 = true
        end

        local operation =
        {
            type = operateType,
            skill_uuid = goods:GetUUID(),
        }

        if operateType == 'SOT_EQUIP' or operateType == 'SOT_UNEQUIP' then
            operation['role_id'] = self.roleID
        end
        if operateType == 'SOT_UNEQUIP' then
            self.isTween = false
            local enum = self.roleSkillInfo:GetEnumerator()
            while enum:MoveNext() do
                if enum.Current.skill_uuid ~= 0 and enum.Current.skill_uuid == goods:GetUUID() then
                    operation['slot_id'] = enum.Current.id
                    break
                end
            end
        end

        local req = protobuf.encode("fogs.proto.msg.SkillOperation", operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.SkillOperationID, req)
        CommonFunction.ShowWait()
    end
end

function UISkillManager:OnCancelSkillClick(index)
    return function (go)
        -- print('cancel index = ', index)
        local wearSkill = self.uiWearSlot[index].transform:GetChild(0).gameObject
        local wearSkillLua = getLuaComponent(wearSkill)
        self:OnOperateSkillClick('SOT_UNEQUIP')(wearSkillLua.goods)
    end
end
-------

function UISkillManager:BuyStoreGoodsResp( ... )
    return function (message)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self.uiName)
        CommonFunction.StopWait()
        local resp = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
        if resp.result ~= 0 then
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            return
        end
        self.banTwice2 = false
        local parentGrid, inWareHouse
        if self.currentLeftTab == self.uiLeftTab[1].gameObject then
            parentGrid = self.uiWearGrid.transform
            inWareHouse = false
        elseif self.currentLeftTab == self.uiLeftTab[2].gameObject then
            parentGrid = self.uiWareHouseGrid.transform
            inWareHouse = true
        end

        local goodsList = self.mainPlayer:GetGoodsList(GoodsCategory.GC_SKILL, self.currentSkillItem.id)
        self.currentSkillItem.isOwn = true
        self.currentSkillItem.isWear = false
        if goodsList.Count > 0 then
            print('have goods !')
            self.currentSkillItem.goods = goodsList:get_Item(0)
        end
        self.currentSkillItem.onClick = self:OnOpenSkillDetail(self.currentSkillItem)
        self.currentSkillItem:Refresh()
        -- wear
        for i=1,parentGrid.transform.childCount do
            local childLua = getLuaComponent(parentGrid.transform:GetChild(i - 1).gameObject)
            if childLua.id == self.currentSkillItem.id then
                local goodsList = self.mainPlayer:GetGoodsList(GoodsCategory.GC_SKILL, self.currentSkillItem.id)
                childLua.isOwn = true
                childLua.isWear = false
                childLua.isInWareHouse = inWareHouse
                if goodsList.Count > 0 then
                    childLua.goods = goodsList:get_Item(0)
                end
                childLua.onClick = self:OnOpenSkillDetail(childLua)
                childLua:Refresh()
                break
            end
        end
        self.uiWearGrid.repositionNow = true
        self.uiWearGrid:Reposition()
        -- self.currentSkillItem = nil
        local skillDetailLua = getLuaComponent(self.skillDetail.gameObject)
        skillDetailLua:DoClose()
        self.skillDetail = nil
    end
end

function UISkillManager:SkillOperationResp( ... )
    return function (message)
        local resp = protobuf.decode('fogs.proto.msg.SkillOperationResp', message)
        print('resp.result = ', resp.result)
        print('resp.type = ', resp.type)
        print('resp.slot_id = ', resp.slot_id)
        CommonFunction.StopWait()
        if resp.result ~= 0 then
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            return
        end
        self.banTwice2 = false
        if resp.type == 'SOT_UPGRADE' then
            local skillDetailLua = getLuaComponent(self.skillDetail.gameObject)
            skillDetailLua.isImproveSkill = true
            skillDetailLua:Refresh()
            skillDetailLua.isImproveSkill = false
            self.currentSkillItem:Refresh()

            for i=1,self.uiWearGrid.transform.childCount do
                local childLua = getLuaComponent(self.uiWearGrid.transform:GetChild(i - 1).gameObject)
                if childLua.id == self.currentSkillItem.id then
                    local goodsList = self.mainPlayer:GetGoodsList(GoodsCategory.GC_SKILL, self.currentSkillItem.id)
                    childLua.isOwn = true
                    -- childLua.isWear = false
                    if goodsList.Count > 0 then
                        childLua.goods = goodsList:get_Item(0)
                    end
                    childLua.onClick = self:OnOpenSkillDetail(childLua)
                    childLua:Refresh()
                    break
                end
            end

            local enum = self.roleSkillInfo:GetEnumerator()
            while enum:MoveNext() do
                if enum.Current.skill_id == self.currentSkillItem.id and resp.skill_uuid ~= 0 then
                    enum.Current.skill_uuid = resp.skill_uuid
                    local goods = self.mainPlayer:GetGoods(GoodsCategory.GC_SKILL, resp.skill_uuid)
                    enum.Current.skill_level = goods:GetLevel()
                    break
                end
            end
            self:ShowWearSkills()
        elseif resp.type == 'SOT_EQUIP' then
            if self.skillDetail ~= nil then
                local skillDetailLua = getLuaComponent(self.skillDetail.gameObject)
                skillDetailLua:DoClose()
            end

            if self.currentSkillItem ~= nil then
                self.currentSkillItem.isWear = true
                self.currentSkillItem.onClick = self:OnOperateSkillClick("SOT_UNEQUIP")
                self.currentSkillItem:Refresh()
                self.currentSkillItem = nil
            end

            local enum = self.roleSkillInfo:GetEnumerator()
            while enum:MoveNext() do
                if enum.Current.id == resp.slot_id and resp.skill_uuid ~= 0 then
                    enum.Current.skill_uuid = resp.skill_uuid
                    local goods = self.mainPlayer:GetGoods(GoodsCategory.GC_SKILL, resp.skill_uuid)
                    enum.Current.skill_id = goods:GetID()
                    enum.Current.skill_level = goods:GetLevel()
                    break
                end
            end
            self:ShowWearSkills()
        elseif resp.type == 'SOT_UNEQUIP' then
            local skillItem = self:FindWearSkill(resp.skill_uuid)
            skillItem.isWear = false
            skillItem.onClick = self:OnOpenSkillDetail(skillItem)
            skillItem:Refresh()

            local enum = self.roleSkillInfo:GetEnumerator()
            while enum:MoveNext() do
                if enum.Current.id == resp.slot_id then
                    enum.Current.skill_uuid = 0
                    enum.Current.skill_id = 0
                    enum.Current.skill_level = 0
                    break
                end
            end
            self:ShowWearSkills()
        end
    end
end

function UISkillManager:FindWearSkill(uuid)
    for i=1,self.uiWearGrid.transform.childCount do
        local skillItem = self.uiWearGrid.transform:GetChild(i - 1).gameObject
        local skillItemLua = getLuaComponent(skillItem)
        if skillItemLua.goods and skillItemLua.goods:GetUUID() == uuid then
            return skillItemLua
        end
    end
end

function UISkillManager:WearGridCustomSort( ... )
    return function (xItem, yItem)
        local xLua = getLuaComponent(xItem.gameObject)
        local yLua = getLuaComponent(yItem.gameObject)

        if xItem.gameObject.activeSelf and not yItem.gameObject.activeSelf then
            return 1
        elseif not xItem.gameObject.activeSelf and yItem.gameObject.activeSelf then
            return -1
        end

        if xLua.goods and not yLua.goods then
            return -1
        elseif not xLua.goods and yLua.goods then
            return 1
        elseif xLua.goods and yLua.goods then
            if xLua.goods:GetLevel() < yLua.goods:GetLevel() then
                return 1
            elseif xLua.goods:GetLevel() > yLua.goods:GetLevel() then
                return -1
            else
                if xLua.goods:GetID() < yLua.goods:GetID() then
                    return -1
                elseif xLua.goods:GetID() > yLua.goods:GetID() then
                    return 1
                else
                    return 0
                end
            end
        elseif not xLua.goods and not yLua.goods then
            if xLua.id < yLua.id then
                return -1
            elseif xLua.id > yLua.id then
                return 1
            else
                return 0
            end
        end
    end
end

function UISkillManager:ShowBuyTip(type)
    local str
    if type == "BUY_GOLD" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
    elseif type == "BUY_DIAMOND" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
    elseif type == 'BUY_HP' then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
    else
        str = string.format(getCommonStr("NOT_ENOUGH_GOODS"), type)
        CommonFunction.ShowPopupMsg(str,nil,nil,nil,nil,nil)
        return
    end
    if not self.buyMsg then
        self.buyMsg = CommonFunction.ShowPopupMsg(str, nil,
                                                        LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
                                                        LuaHelper.VoidDelegate(self:FramClickClose()),
                                                        getCommonStr("BUTTON_CONFIRM"),
                                                        getCommonStr("BUTTON_CANCEL"))
    end
end

function UISkillManager:ShowBuyUI(type)
    return function()
        if type == "BUY_DIAMOND" then
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = type
        self.banTwice2 = false
        self.buyMsg = nil
    end
end

function UISkillManager:FramClickClose()
    return function()
        self.banTwice2 = false
        NGUITools.Destroy(self.buyMsg.gameObject)
        self.buyMsg = nil
    end
end

function UISkillManager:RefreshAfterLevelUp( ... )
    local parent = self.uiWareHouseGrid.transform
    for i=0,parent.childCount - 1 do
        local child = parent.transform:GetChild(i).transform
        local childLua = getLuaComponent(child.gameObject)
        if childLua.limitLearnLevel <= MainPlayer.Instance.Level and childLua.isCanLearn == false then
            childLua.isCanLearn = true
            childLua:Refresh()
        end
    end
    parent = self.uiWearGrid.transform
    for i=0,parent.childCount - 1 do
        local child = parent.transform:GetChild(i).transform
        local childLua = getLuaComponent(child.gameObject)
        if childLua.limitLearnLevel <= MainPlayer.Instance.Level and childLua.isCanLearn == false then
            childLua.isCanLearn = true
            childLua:Refresh()
        end
    end
end

function UISkillManager:OnDragIconEnd(item)
    self.currentSkillItem = item
    self:OnOperateSkillClick('SOT_EQUIP')(item.goods)
    print("UISkillManager:OnDragIconEnd(goods)")
end



function UISkillManager:RefreshTip( ... )
    local tipList = UpdateRedDotHandler.UpdateState["UISkillManager"]
    for i=1,6 do
        -- NGUITools.SetActive(self.uiTips[i].gameObject, true)
        if self.currentLeftTab == self.uiLeftTab[2].gameObject then
            self.uiTips[i].gameObject:SetActive(false)
        else
            if tipList:Contains(i) then
                self.uiTips[i].gameObject:SetActive(true)
            else
                self.uiTips[i].gameObject:SetActive(false)
            end
        end
    end
end

function UISkillManager:IconTweenFinish(pos)
    return function ()
        local tween = self.transform:FindChild('Right/Wear/Down/Frame/Icon' .. tostring(pos)):GetComponent('UITweener')
        local gameObejct = tween.gameObject
        Object.DestroyImmediate(tween)
        tween = TweenScale.Begin(gameObejct, 0.2, Vector3.New(1,1,1))
        tween.delay = 0
    end
end

return UISkillManager

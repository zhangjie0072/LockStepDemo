UISelectRole = {
    uiName = "UISelectRole",

    -- uiPart.
    --labelNoPlayer,
    uiTattoo,
    uiBadgeList,
    uiBadgeBtnArrow,
    uiBadgeCheckPropBtn,
    uiBadgeBookSelectItem,
    uiPositionName,
    uiTalentIcon,
    uiTalentName,
    uiTalentDesc,
    uiMask,
    uiGridChatMsgPops,
    uiChatModuleNode,--聊天功能加载模块
    uiGameChatModule,--聊天功能界面
    uiRoleSkillsTipsNode,--角色技能详情加载节点
    uiLeftTimeLabel,--
    uiSpecialityInfo,--特长描述
    uiInfinityIcon,--无穷大icon
    -- uiMinTimeEScore,--
    -- uiSecTimeEScore,--


    -- parameters
    maxSelection = 3,
    onStart,
    sendChangeRole = false,
    title,						-- Use System Font. Not Graphics Font!
    noPlayerText,
    startLabel,
    pressTime = 0,
    isPress,
    badgeBookPropTip,
    currentSelectBadgeBookId = nil,
    currentPlaceSelectIndex = 1,--当前右侧角色选择的index(空位也记录)
    freshAll = true,--是否刷新整个界面
    -- Ladder
    teamInfo = nil,
    isLadder = false,
    isLadderRegister = false,
    is1vs1PracticeMatch=false;
    myPos = 0,
    myAccountId = 0,
    masterId = 0,
    startSelectTime = 0,
    ladderSelectRoleCountDown = 0,
    msgBox = nil,
    matchType = "MT_PVP_3V3",

    -- variables
    ownedRoleIcons,		-- = {},--玩家拥有的球员列表
    selectedRoleIcons,	-- = {},--己选择的角色的icon集合
    selectedRoleIDs,	-- = {},--选择的角色集合
    allRoleIcons,		-- save all role icons,
    roleIconNodes,      -- 右侧角色Icon的添加节点
    focusedRoleIcon,
    rightRolePlaceIcons,-- 右侧球员角色占位图标

    ---vars for chat--
    chatMsgPopItems,


}

function UISelectRole:Awake()
    self.scrollRoleList = getComponentInChild(self.transform, "Left/Left/Scroll", "UIScrollView")
    self.gridRoleList = getComponentInChild(self.transform, "Left/Left/Scroll/Grid", "UIGrid")
    self.gridSelectedRole = getComponentInChild(self.transform, "Right/Right/HeadGrid", "UIGrid")
    self.gridSkill = getComponentInChild(self.transform, "Right/Skill", "UIGrid")
    self.tmBGIcon = self.transform:FindChild("Right/Right/BgIcon")
    self.spritePosition = getComponentInChild(self.transform, "TopLeft/Player/Position", "UISprite")
    self.uiPositionName = self.transform:FindChild("TopLeft/Player/PositionName"):GetComponent("UILabel")
    self.labelName = getComponentInChild(self.transform, "TopLeft/Player/Name", "UILabel")
    self.btnStart = self.transform:FindChild("Right/Right/ButtonOK").gameObject
    --self.labelNoPlayer = getComponentInChild(self.transform, "Middle/NoPlayerLabel", "UILabel")
    self.uiTattoo = self.transform:FindChild("TopLeft/Player/Tattoo")
    self.uiBadgeList = self.transform:FindChild("TopLeft/Player/Tattoo/DropDown")
    self.uiBadgeBtnArrow = self.transform:FindChild("TopLeft/Player/Tattoo/Arrow")
    self.uiBadgeCheckPropBtn = self.transform:FindChild("TopLeft/Player/Tattoo/Eye")
    self.uiBadgeBookSelectItem = getLuaComponent(createUI("DropDownListItem",self.transform:FindChild("TopLeft/Player/Tattoo/SelectNode").transform))
    self.uiBadgeBookSelectItem.transform:GetComponent("BoxCollider").enabled = false
    self.uiTalent = self.transform:FindChild("Right/Talent")
    self.uiTalentIcon = getComponentInChild(self.transform, "Right/Talent/Icon/Sprite", "UISprite")
    self.uiTalentName = getComponentInChild(self.transform, "Right/Talent/Name", "UILabel")
    self.uiTalentDesc = getComponentInChild(self.transform, "Right/Talent/Describe", "UILabel")
    self.uiMask = self.transform:FindChild("TopLeft/Player/Tattoo/DropDown/Mask")
    self.uiGridChatMsgPops = self.transform:FindChild("Right/ChatMsgPopsGroup/Grid"):GetComponent("UIGrid")
    self.uiChatModuleNode = self.transform:FindChild("Bottom/ChatModuleNode")
    self.uiGameChatModule = getLuaComponent(createUI("UIGameChat",self.uiChatModuleNode.transform))
    self.uiRoleSkillsTipsNode = self.transform:FindChild("Right/SkillDetailTipsNode")
    self.uiLeftTimeLabel = self.transform:FindChild("TopRight/LeftTimeLabel"):GetComponent("UILabel")
    self.uiSpecialityInfo = self.transform:FindChild("TopLeft/Player/SpecialityInfo"):GetComponent("UILabel")
    self.uiInfinityIcon = self.transform:FindChild("TopRight/LeftTimeLabel/InfinityIcon")
    -- self.uiMinTimeEScore = self.transform:FindChild("Middle/LeftTimeLabel/E_Score1/min"):GetComponent("UScore")
    -- self.uiSecTimeEScore = self.transform:FindChild("Middle/LeftTimeLabel/E_Score2/sec"):GetComponent("UScore")

    addOnClick(self.uiBadgeBtnArrow.gameObject,self:DropDownBadgesList())
    addOnClick(self.uiMask.gameObject,self:DropDownBadgesList())
    addOnClick(self.btnStart, self:MakeOnStart())
    addOnClick(self.uiBadgeCheckPropBtn.gameObject, self:ShowBadgePropTips())
    --add press event----
    self.pressTime = 0
    --UIEventListener.Get(self.uiBadgeCheckPropBtn.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())
    ----
    self.model = self.transform:FindChild("Middle/Model"):GetComponent("ModelShowItem")
    self.uiLeftTime = self.transform:FindChild("TopRight/LeftTimeLabel/LeftTime"):GetComponent("UILabel")
    NGUITools.SetActive(self.model.gameObject, false)
    self.btnBack = getLuaComponent(createUI("ButtonBack", self.transform:FindChild("TopLeft/ButtonBack")))
    self.btnBack.onClick = self:MakeOnClose()
    NGUITools.SetActive(self.uiTalent.gameObject, false)
    NGUITools.SetActive(self.uiTattoo.gameObject, false)
    self:FocusRoleIcon(nil)

    self.roleIconNodes = {}
    self.rightRolePlaceIcons = {}
    for i = 1, 3 do
        local node =  self.transform:FindChild("Right/Right/BgIcon/Icon"..i)
        local placeIcon = getLuaComponent(createUI("RoleSelectedIcon",node))
        placeIcon.Index = i
        placeIcon.ClickCallback = self:OnPlaceRoleIconClickHanlder()
        table.insert(self.rightRolePlaceIcons,placeIcon)
        table.insert(self.roleIconNodes,node)
    end
    self.myAccountId = MainPlayer.Instance.AccountID
    --天梯赛准备倒计时
    self.ladderSelectRoleCountDown = tonumber(GameSystem.Instance.CommonConfig:GetString("gPVPLadderReadyTime"))
    print("1927 -  self.ladderSelectRoleCountDown=",self.ladderSelectRoleCountDown)

    --add chatMsgPopItems
    self.chatMsgPopItems = {}
    for k = 1, 3 do
        table.insert(self.chatMsgPopItems,getLuaComponent(createUI("ChatMsgPopItem",self.transform:FindChild("Right/ChatMsgPopsGroup/Grid/item"..k))))
    end
    self.uiGridChatMsgPops:Reposition()
    for m = 1,3 do
        NGUITools.SetActive(self.chatMsgPopItems[m].gameObject,false)
    end
    ChatDataCenter.ChatUpdateFuncForUISelectRole = self:UpdateChatMsg()
end

function UISelectRole:Start()
    print("UISelectRole:Start doing!")
    LuaHelper.RegisterPlatMsgHandler(MsgID.ChangeFightRoleRespID, self:HandleChangeFightRoleResp(), self.uiName)
end

function UISelectRole:OnDestroy()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.ChangeFightRoleRespID, self.uiName)
end

function UISelectRole:OnDisable( ... )
    if self.uiGameChatModule ~= nil then
        self.uiGameChatModule:CloseChatModue()
    end
end

function UISelectRole:FixedUpdate()
    if self.focusFirst then
        -- local roleIcon =
        -- self:FocusRoleIcon(self.selectedRoleIcons[self.selectedRoleIDs[1]])
        self:SetPlaceRoleIconSelectedIndex(self.currentPlaceSelectIndex)
        self.focusFirst = nil
    end

    if self.isLadder then
        local leftTime =  self.ladderSelectRoleCountDown - (os.time() - self.startSelectTime)
        if leftTime < 0 then
            leftTime = 0
            if QualifyingNewerAI.isAI then
                if QualifyingNewerAI.myRoleId ~= 0 then
                    QualifyingNewerAI.isSelectConfirm = true
                    QualifyingNewerAI.CheckNotifyGame()
                    self:ExitAsLadder()
                else
                    local operation = {
                        type = self.matchType
                    }
                    print("1927 - Send ExitSelectRole ")
                    local req = protobuf.encode("fogs.proto.msg.ExitSelectRoleUI",operation)
                    LuaHelper.SendPlatMsgFromLua(MsgID.ExitSelectRoleUIID,req)
                    self:TryDestroyMsgBox()
                    self.msgBox = CommonFunction.ShowPopupMsg(getCommonStr("STR_YOU_ARE_NOT_READY_GO_BACK_TO_HALL"),
                                                           nil,
                                                           LuaHelper.VoidDelegate(self:OnConfirmBackToHall()),
                                                           nil,
                                                           getCommonStr("BUTTON_CONFIRM"),
                                                           nil)
                    QualifyingNewerAI.isAI = false
                end
            end
        end
        self.uiLeftTime.text = math.modf(leftTime)
        -- local tempTime = math.modf(leftTime)
        -- local minPart = math.floor(tempTime / 10)
        -- local secPart = tempTime % 10
        -- self.uiMinTimeEScore:SetScore(minPart)
        -- self.uiSecTimeEScore:SetScore(secPart)
    end
--[[
    if (os.time() - self.pressTime) > 0.15 and self.isPress == true then
        --显示TIPS
        if not self.badgeBookPropTip then
            self:ShowBadgePropTips()
        end
    end
--]]
end

function UISelectRole:Refresh()
    print("UISelectRole:Refresh doing! needFreshAll"..tostring(self.freshAll))
    if not self.freshAll then
        self.freshAll = true
        self:RefreshRoleList()
        local ids = self:GetSelectRoleIDs()
        for _,v in pairs(ids) do
            self.ownedRoleIcons[v]:SetSelected(true)
        end
        return
    end
    for k=1,3 do
        self.rightRolePlaceIcons[k]:SetRoleId(nil)
        --self.rightRolePlaceIcons[k]:SetPressAnabled(false)
    end
    self.uiGameChatModule:CloseChatModue()
    self:FocusRoleIcon(nil)
    if self.isLadder then
        for i = 1, 3 do
            local info = self.teamInfo[i]
            if info.acc_id  == self.myAccountId then
                self.myPos = i
                self.currentPlaceSelectIndex = i
                self:SetPlaceRoleIconSelectedIndex(self.currentPlaceSelectIndex)
                print("1927 - Calc myPos --start self.myPos=",self.myPos)
                -- break
            else
                self.rightRolePlaceIcons[i].ClickAble = false
                --self.rightRolePlaceIcons[i]:SetPressAnabled(true,self:ShowRoleDetailSkillTip())
            end
            local nameGo = self.rightRolePlaceIcons[i].transform:FindChild("Name")
            nameGo.gameObject:SetActive(true)
            nameGo:GetComponent("UILabel").text = info.name
        end

        if not self.isLadderRegister and not QualifyingNewerAI.isAI then
            self:RegisterForLadder(true)
        end

        if ChatDataCenter.LadderChatModuleInfo ~= nil then
            self.uiGameChatModule:OpenChatModule(ChatDataCenter.LadderChatModuleInfo.roomId)
        end

        if QualifyingNewerAI.isAI then
            self.uiGameChatModule:OpenChatModule(0)
        end
    else
        self.currentPlaceSelectIndex = 1
    end
    self.btnBack.gameObject:SetActive(not self.isLadder)
    if not self.isLadder then
        for i = 1,3 do
            self.rightRolePlaceIcons[i].ClickAble = true
        end
    end

    if self.startLabel then
        self.btnStart.transform:FindChild("Label"):GetComponent("MultiLabel"):SetText(self.startLabel)
    end
    self.ownedRoleIcons = {}
    self.selectedRoleIcons = {}
    self.selectedRoleIDs = {}
    --if self.noPlayerText then
    --    self.labelNoPlayer.text = self.noPlayerText
    --end

    self:RefreshRoleList()
    --目前好像没有使用
    CommonFunction.ClearChild(self.gridSelectedRole.transform)
    for i = 1, 3 do
        self:DestroySelectIcon(i)
    end

    --显示右侧需要显示的头像个数
    for i = 0, self.tmBGIcon.childCount - 1 do
        NGUITools.SetActive(self.tmBGIcon:GetChild(i).gameObject, i < self.maxSelection)
    end

    --非天梯赛 ，需要选3个人的，默认选择3个人
    if self.maxSelection == 3 and not self.isLadder then
        self:SelectSquad()
    end
    NGUITools.SetActive(self.uiLeftTime.gameObject,self.isLadder)
    self.uiLeftTimeLabel.text = getCommonStr("STR_SELECT_ROLE_FIGHTER")
    -- if not self.isLadder then
    --     self.uiLeftTime.text = "∞"
    -- end
    NGUITools.SetActive(self.uiInfinityIcon.gameObject,not self.isLadder)
    -- NGUITools.SetActive(self.uiMinTimeEScore.gameObject,self.isLadder)
    -- NGUITools.SetActive(self.uiSecTimeEScore.gameObject,self.isLadder)
    if self.isLadder then
        self.startSelectTime = os.time()
    end
    self.btnStart.transform:GetComponent("UIButton").isEnabled = true
    self:TryDestroyMsgBox()
end
--[[
function UISelectRole:OnPress( ... )
    return function ( go , isPressed )
        print("OnPress...................")
        if isPressed  then
            self.pressTime = os.time()
            self.isPress = true
        else
            self.isPress = false
            if self.badgeBookPropTip ~= nil then
                NGUITools.Destroy(self.badgeBookPropTip)
                self.badgeBookPropTip = nil
            end
        end
    end
end
--]]
function UISelectRole:ShowBadgePropTips( ... )
    return function ()
        if self.currentSelectBadgeBookId == nil then
            return
        end
        --if not self.badgeBookPropTip then
            self.badgeBookPropTip = createUI("BadgeBookPropTip")
        --end
        local badgeBook = MainPlayer.Instance.badgeSystemInfo:GetBadgeBookByBookId(self.currentSelectBadgeBookId)
        if badgeBook then
            local t = getLuaComponent(self.badgeBookPropTip)
            t:SetData(badgeBook)
            UIManager.Instance:BringPanelForward(self.badgeBookPropTip)
            self.badgeBookPropTip.transform.localPosition = Vector3.New(-273,-8,0)
        end
    end
end

function UISelectRole:DropDownBadgesList( ... )
    return function()
        local listNode  = self.uiBadgeList.transform:FindChild("DropGrid")
        CommonFunction.ClearGridChild(listNode.transform)
        local booksList = MainPlayer.Instance.badgeSystemInfo:GetAllBadgeBooks()
        local count = booksList.Count
        local widget = self.uiBadgeList.transform:GetComponent("UIWidget")
        widget.height = listNode:GetComponent("UIGrid").cellHeight*count
        if booksList~=nil then
            local count = booksList.Count
            if count>1 then
                for i=1,count do
                    local bookData = booksList:get_Item(i-1)
                    if bookData then
                        local item = getLuaComponent(createUI("DropDownListItem",listNode))
                        item:SetLabelText(bookData.name)
                        item:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(bookData.id))
                        item:SetIndex(i)
                        item:SetId(bookData.id)
                        addOnClick(item.gameObject,self:OnSelectBookPage())
                    end
                end
            end
        end
        NGUITools.SetActive(self.uiBadgeList.gameObject,not self.uiBadgeList.gameObject.activeSelf)
        listNode.transform:GetComponent("UIGrid"):Reposition()
    end
end

-- function UISelectRole:RefreshBadgeLabelView( ... )
--  local isHasUsedBadge = false
--  local booksList = MainPlayer.Instance.badgeSystemInfo:GetAllBadgeBooks()
--  if booksList ~= nil then
--      local count = booksList.Count
--      if count<1 then
--          return
--      end
--      for i=1,count do
--          local bookData = booksList:get_Item(i-1)
--          if bookData then
--              if bookData.been_used then
--                  -- self.uiBadgeUsedNameLabel.text = bookData.name
--                  self.uiBadgeBookSelectItem:SetLabelText(bookData.name)
--                  self.uiBadgeBookSelectItem:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(bookData.id))
--                  isHasUsedBadge = true
--                  self.currentSelectBadgeBookId = bookData.id
--              end
--          end
--      end
--      if not isHasUsedBadge then
--          local data = booksList:get_Item(0)
--          if data then
--              -- self.uiBadgeUsedNameLabel.text = data.name
--              self.uiBadgeBookSelectItem:SetLabelText(data.name)
--              self.uiBadgeBookSelectItem:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(data.id))
--          end
--      end
--  end
-- end

function UISelectRole:OnSelectBookPage( ... )
    return function(go)
        local goT = getLuaComponent(go)
        -- self.uiBadgeBookSelectItem:SetLabelText(goT:GetLabelText())
        -- self.uiBadgeBookSelectItem:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(goT:GetId()))
        print("bookId",goT:GetId())
        local roleInfo = MainPlayer.Instance:GetRole2(self.focusedRoleIcon.id)
        roleInfo.badge_book_id = goT:GetId()
        self:RefreshBadgeBookItem(goT:GetId())
        NGUITools.SetActive(self.uiBadgeList.gameObject,not self.uiBadgeList.gameObject.activeSelf)
    end
end

-------------------------------------
-- 删除右侧角色icon
-------------------------------------
function UISelectRole:DestroySelectIcon(index)
    -- local t = self:GetSelectIconByIndex(index)
    -- if t then
    --     NGUITools.Destroy(t.gameObject)
    -- end
       self.rightRolePlaceIcons[index]:SetRoleId(nil)
end

function UISelectRole:GetSelectIconByIndex(index)
    -- TODO: index check.
    if not index or index < 1  or index > 3 then
        error("UISelectRole index check failed: index =", index or "nil")
        return
    end

    local t = self.roleIconNodes[index]:FindChild(tostring(index))
    local s = nil
    if t then
        s = getLuaComponent(t)
    end
    return t, s
end
-- left panel role list--
function UISelectRole:RefreshRoleList()
    CommonFunction.ClearChild(self.gridRoleList.transform)
    local rTb = {}
    local enum = GameSystem.Instance.RoleBaseConfigData2.roleBaseDatas:GetEnumerator()
    while enum:MoveNext() do
        if enum.Current.Value.display == 1 then
            print(self.uiName, "RefreshRoleList", "Role ID", enum.Current.Value.id)
            table.insert(rTb, enum.Current.Value)
        end
    end
    local mi = MainPlayer.Instance
    table.sort(rTb, function(a, b)
                   local hasA = mi:HasRole(a.id)
                   local hasB = mi:HasRole(b.id)
                   if hasA and not hasB then return true end
                   if not hasA and hasB then return false end
                   local starA, starB
                   if hasA then
                       starA = mi:GetRole2(a.id).star
                       starB = mi:GetRole2(b.id).star
                   else
                       starA = a.init_star
                       starB = b.init_star
                   end
                   if starA ~= starB then
                       return starA > starB
                   end
                   return a.id < b.id
    end )

    for k,v in pairs(rTb) do
        local id = v.id
        local has = mi:HasRole(id)
        local roleIcon = self:CreateRoleIcon(id, self.gridRoleList.transform)
        roleIcon.transform.name = id
        if has then
            self.ownedRoleIcons[id] = roleIcon
        else
            roleIcon.disabled = true
            --除天梯赛左侧角色选择rue
            --天梯赛中不显示未拥有的球员
            if self.isLadder then
                NGUITools.SetActive(roleIcon.gameObject,false)
            end
        end
    end
    self.gridRoleList:Reposition()
    self.scrollRoleList:ResetPosition()
end

function UISelectRole:SelectSquad()
    local squadInfo = MainPlayer.Instance.SquadInfo
    local count = squadInfo.Count
    for i = 0, count - 1 do
        local role_id = squadInfo:get_Item(i).role_id
        print(self.uiName, "SelectSquad", "Role ID:", role_id)
        self:SelectRoleIcon(self.ownedRoleIcons[role_id],i+1)
    end
    if count > 0 then
        --选中第一个
        self.focusFirst = true	-- do next frame for performance reason
        self:RefreshIconGrey()
    else
        self:FocusRoleIcon(nil)
    end
end

function UISelectRole:CreateRoleIcon(roleID, parent)
    local roleIcon = getLuaComponent(createUI("CareerRoleIcon", parent))
    roleIcon.id = roleID
    if self.isLadder then
        roleIcon.onClick = self:MakeOnLadderRoleIconClick()
    else
    --除天梯赛左侧角色选择
        roleIcon.onClick = self:MakeOnRoleIconClick()
    end
    return roleIcon
end

-----------------------------------------------------------
--- 把指定的角色Icon标记成高亮状态
--- 显示球员在团队中扮演的位置（如PF，SG, SF等)
--- 更新人物模型相关的信息，显示指定角色的人物模型
--- 显示对应人物的天赋,涂鸦,技能,相关信息
---
-----------------------------------------------------------
function UISelectRole:FocusRoleIcon(roleIcon)
    print(self.uiName, "FocusRoleIcon", roleIcon and roleIcon.id, self.focusedRoleIcon and self.focusedRoleIcon.id)
    if roleIcon then
        if roleIcon ~= self.focusedRoleIcon then
            --设置高亮状态
            -- for k, v in pairs(self.selectedRoleIcons) do
            --     v:SetSele(v == roleIcon)
            -- end
            --显示人物扮演的角色信息
            local positions ={'PF','SF','C','PG','SG'}
            local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleIcon.id)
            self.spritePosition.spriteName = tostring(PositionType.IntToEnum(data.position))
            local specialityInfoStr = data.specialityInfo
            if specialityInfoStr == nil then
                specialityInfoStr = ""
            end
            self.uiSpecialityInfo.text = specialityInfoStr
            self.uiPositionName.text = getCommonStr(positions[data.position])

            --显示人物基本信息,名字，天赋，涂鸦 技能---
            self.labelName.text = data.name
            self.model.Rotation = true
            self.model.ModelID = roleIcon.id
            NGUITools.SetActive(self.uiTalent.gameObject, true)
            NGUITools.SetActive(self.uiTattoo.gameObject, true)
            self:RefreshTalent(roleIcon.id)
            local roleInfo = MainPlayer.Instance:GetRole2(roleIcon.id)
            if roleInfo then
                local skills = roleInfo.skill_slot_info
                self:DisplaySkills(skills)
                self:RefreshBadgeBookItem(roleInfo.badge_book_id)
            else
                self:DisplaySkills(nil)
            end
            NGUITools.SetActive(self.spritePosition.gameObject, true)
            NGUITools.SetActive(self.labelName.gameObject, true)
            NGUITools.SetActive(self.model.gameObject, true)
            NGUITools.SetActive(self.uiPositionName.gameObject, true)
        end
    else
        self:DisplaySkills(nil)
        self.uiSpecialityInfo.text = ""
        NGUITools.SetActive(self.spritePosition.gameObject, false)
        NGUITools.SetActive(self.labelName.gameObject, false)
        NGUITools.SetActive(self.model.gameObject, false)
        NGUITools.SetActive(self.uiPositionName.gameObject, false)
        NGUITools.SetActive(self.uiTalent.gameObject, false)
        NGUITools.SetActive(self.uiTattoo.gameObject, false)
    end
    self.focusedRoleIcon = roleIcon
    -- self.labelNoPlayer.gameObject:SetActive(roleIcon==nil)
end

function UISelectRole:RefreshBadgeBookItem(bookId)
    if bookId == nil then
        return
    end

    self.currentSelectBadgeBookId = bookId
    local bookData = MainPlayer.Instance.badgeSystemInfo:GetBadgeBookByBookId(tonumber(bookId))
    -- 找不到信息.
    if bookData == nil then
        warning("cannot find BookData!")
        return
    end
    self.uiBadgeBookSelectItem:SetLabelText(bookData.name)
    self.uiBadgeBookSelectItem:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(bookId))
end

function UISelectRole:DisplaySkills(skills)
    CommonFunction.ClearChild(self.gridSkill.transform)
    if skills then
        local count = skills.Count
        for i = 0, count - 1 do
            local skill = skills:get_Item(i)
            if skill.skill_uuid ~= 0 then
                local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, skill.skill_uuid)
                local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.gridSkill.transform))
                goodsIcon.goods = goods
                goodsIcon.hideLevel = true
                goodsIcon.hideNeed = true
                goodsIcon.showTips = true
            end
        end
    end
    self.gridSkill.repositionNow = true
end

-------------------------------------------
-- 左侧角色选择,右侧角色图标显示
-- 更新选己选择角色的ID 集合(selectedRoleIDs)
-- 在更新对应的球员角色图标
------------------------------------------
function UISelectRole:SelectRoleIcon(roleIcon, index)
    -- roleIcon:SetSelected(true)
    -- table.insert(self.selectedRoleIDs, roleIcon.id)
    -- if not self.isLadder then
    --     -- self:DestroySelectIcon(#self.selectedRoleIDs)
    --     -- roleIcon = self:CreateRoleIcon(roleIcon.id, self.roleIconNodes[#self.selectedRoleIDs]:FindChild("RoleIconNode"))
    --     -- roleIcon.transform.name = #self.selectedRoleIDs
    -- else
    --     if not index then
    --         index = #self.selectedRoleIDs
    --     end
    --     self:DestroySelectIcon(index)
    --     roleIcon = self:CreateRoleIcon(roleIcon.id, self.roleIconNodes[index]:FindChild("RoleIconNode"))
    --     roleIcon.transform.name = index
    -- end
    -- roleIcon:EnableClose(true)
    -- roleIcon.onClose = self:MakeOnRoleIconClose()
    -- self.selectedRoleIcons[roleIcon.id] = roleIcon
    -- self.gridSelectedRole:Reposition()
    -- 重构后的代码---------------------
    roleIcon:SetSelected(true)
    -- table.insert(self.selectedRoleIDs, roleIcon.id)
    local placeIcon = self.rightRolePlaceIcons[index]
    local preRoleId = placeIcon:GetRoleId()
    if preRoleId ~= nil then
        for _,v in pairs(self.ownedRoleIcons) do
            if v.id == preRoleId then
                v:SetSelected(false)
            end
        end
    end
    placeIcon:SetRoleId(roleIcon.id)
    placeIcon:SetRoleIconName(#self.selectedRoleIDs)
    placeIcon:CloseButtonVisible(true)
    placeIcon:AddRoleIconCloseCallCack(self:MakeOnRoleIconClose())
    -- self.selectedRoleIcons[roleIcon.id] = roleIcon
    self.selectedRoleIDs = self:GetSelectRoleIDs()
end

-------------------------------------------
--- 注：下面说明只用于理解逻辑有用，可能有误(用于天梯赛选人)
----方法说明:在右侧显示当前选择的球员（每次会把节点上重创建一个RoleIcon)
--- index:球员icon所处右侧列表球员的index
--- id : roleId
--- flag :标记，1.未确定，2.确定
--- name :玩家名称
-----------------------------------------
function UISelectRole:SelectOtherRoleIcon(index, id, flag, name,skills)
    -- if flag == 1 then
    --     table.insert(self.selectedRoleIDs, id)
    -- end
    -- self:DestroySelectIcon(index)
    -- local roleIcon = self:CreateRoleIcon(id, self.roleIconNodes[index]:FindChild("RoleIconNode"))
    -- roleIcon.onClick = nil      -- 不能点击
    -- roleIcon.transform.name = index
    print("1927 - <UISelectRole>  index, id, flag, name=",index, id, flag, name)
    local placeIcon = self.rightRolePlaceIcons[index]
    local preRoleId = placeIcon:GetRoleId()
    if index == self.myPos then
        if preRoleId ~= nil then
            self.ownedRoleIcons[preRoleId]:SetSelected(false)
        end
        self.ownedRoleIcons[id]:SetSelected(true)
        if flag == 2 then
            self.uiLeftTimeLabel.text = getCommonStr("STR_FIELD_PROMPT50")
        end
    end
    placeIcon:SetSkillsInfo(skills)
    print("1927 - <UISelectRole>  id=",id)
    placeIcon:SetRoleId(id)
    placeIcon:SetRoleIconName(index)
    placeIcon:SetRealyVisible(flag == 2)
    placeIcon:SetRoleNameVisible(true)
      --收到自己选择的广播
    if index == self.myPos then
        --打开删除按钮
        if self.btnStart.transform:GetComponent("UIButton").isEnabled then
            placeIcon:CloseButtonVisible(true)
        end
        placeIcon:AddRoleIconCloseCallCack(self:MakeOnLadderRoleIconClose())
        self:FocusRoleIcon(placeIcon:GetRoleIcon())
        self:RefreshIconGrey()
    end
    self.selectedRoleIDs = self:GetSelectRoleIDs()
    return placeIcon
end

-----------------------------------------
--当人数选择满了，把拥有的球员全部置灰
----------------------------------------
function UISelectRole:RefreshIconGrey()
    for k, v in pairs(self.ownedRoleIcons) do
        -- v.disabled = self.maxSelection == #self.selectedRoleIDs
        v:Refresh()
    end
end
--------------------------
--此角色是否己经选择--
--------------------------
function UISelectRole:IsSelected(id)
    for _, v in ipairs(self.selectedRoleIDs) do
        if v == id then return true end
    end
    return false
end

function UISelectRole:IsSelectedRoleIcon(roleIcon)
    for k, v in pairs(self.selectedRoleIcons) do
        if v == roleIcon then return true end
    end
    return false
end

--天梯赛左侧角色选择（需要告之服务器)
function UISelectRole:MakeOnLadderRoleIconClick()
    return function(roleIcon)
        print("1927 - MakeOnLadderRoleIconClick called self.myPos=",self.myPos)
        if roleIcon.disabled then return end
        -- if self:GetSelectIconByIndex(self.myPos) then
        --     print("1927 - Ladder Click and return for selected already")
        --     return
        -- end

        if QualifyingNewerAI.isAI then
            QualifyingNewerAI.SelectRole(self.myAccountId, roleIcon.id, 1, true)
            return
        end

        local operation = {
            role_id = roleIcon.id,
            type = self.matchType,
            flag = 1,
        }
        print("1927 - Send SelectFightRole ")
        local req = protobuf.encode("fogs.proto.msg.SelectFightRole",operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.SelectFightRoleID,req)
    end
end

--除天梯赛左侧角色选择
function UISelectRole:MakeOnRoleIconClick()
    return function (roleIcon)
        print(self.uiName, "OnRoleIconClick", roleIcon.disabled, table.getn(self.selectedRoleIDs), self:IsSelected(roleIcon.id))
        --未拥有点击购买
        if roleIcon.disabled then
            local detailPanel = TopPanelManager:ShowPanel("NewRoleDetail",nil,{backCallBackFunc = self:CallbackFuncForBuyPlayer()})
            if detailPanel ~= nil then
                detailPanel.id = roleIcon.id
                detailPanel:SetLeftRightBtnsVisible(false)
                if detailPanel.roleData then
                   detailPanel:RefreshData()
                end
            end
            return
        end
        --左侧icon点击
        if not self:IsSelected(roleIcon.id) then
            -- if table.getn(self.selectedRoleIDs) == self.maxSelection then return end
            self:SelectRoleIcon(roleIcon,self.currentPlaceSelectIndex)
            local nextIndex = self:GetPlaceRoleIconNextIndex()
            self.currentPlaceSelectIndex = nextIndex
            self:SetPlaceRoleIconSelectedIndex(self.currentPlaceSelectIndex)
            local nextIndexRoleIcon = self.rightRolePlaceIcons[self.currentPlaceSelectIndex]:GetRoleIcon()
            self:FocusRoleIcon(nextIndexRoleIcon)
            self:RefreshIconGrey()
        --右侧icon点击
        end
        -- elseif self:IsSelectedRoleIcon(roleIcon) then
        --     self:FocusRoleIcon(roleIcon)
        -- end
    end
end

--天梯赛中球员重选，通过服务器同步
function UISelectRole:MakeOnLadderRoleIconClose()
    return function(roleIcon)
        -- print("1927 - MakeOnLadderRoleIconClose called self.myPos=",self.myPos)
        -- if not self:GetSelectIconByIndex(self.myPos) then
        --     error("1927 - Ladder Role Icon closet but no pos")
        --     return
        -- end

        if QualifyingNewerAI.isAI then
            QualifyingNewerAI.SelectRole(self.myAccountId, 0, 1, true)
            return
        end

        local operation = {
            role_id = 0,
            type = self.matchType,
            flag = 1,
        }
        print("1927 - Send SelectFightRole ")
        print("1927 -  operation.role_id, operation.type, operation.flag=",operation.role_id, operation.type, operation.flag)
        local req = protobuf.encode("fogs.proto.msg.SelectFightRole",operation)
        LuaHelper.SendPlatMsgFromLua(MsgID.SelectFightRoleID,req)
    end
end

-------------------------------------------------------------
-- 删除角色 以供重选---
-- 包括球员高亮选择规则实现,左侧角色（球员）列表状成会刷新
-------------------------------------------------------------
function UISelectRole:MakeOnRoleIconClose()
    return function (roleIcon)
        -- local selectedRoleIDs = {}
        -- for i = 1, table.getn(self.selectedRoleIDs) do
        --     if self.selectedRoleIDs[i] ~= roleIcon.id then
        --         table.insert(selectedRoleIDs, self.selectedRoleIDs[i])
        --     end
        -- end
        -- self.selectedRoleIcons[roleIcon.id] = nil
        -- if #self.selectedRoleIcons <= 0 then
        --	NGUITools.SetActive(self.uiTalent.gameObject, false)
        -- end

        -- ---高亮显示规则实现
        -- if roleIcon == self.focusedRoleIcon then
        --     if table.getn(self.selectedRoleIDs) > 0 and not self.isLadder then
        --         -- self:FocusRoleIcon(self.selectedRoleIcons[self.selectedRoleIDs[1]])
        --         self:FocusRoleIcon(nil)
        --     else
        --         self:FocusRoleIcon(nil)
        --     end
        -- end
        if self.ownedRoleIcons[roleIcon.id] then
            self.ownedRoleIcons[roleIcon.id]:SetSelected(false)
        end
        --先选中，再清出图标
        self:SetRightIconSelectedStatusByRoleId(roleIcon.id)
        local placeIcon = self:GetPlaceIconByRoldId(roleIcon.id)
        if placeIcon ~= nil then
            placeIcon:SetRoleId(nil)
        end
        self:FocusRoleIcon(nil)
        self.selectedRoleIDs = self:GetSelectRoleIDs()
        -- NGUITools.Destroy(roleIcon.gameObject)

        --非天梯赛，已球员上重排规则
        -- if not self.isLadder then
        --     local num = tonumber(roleIcon.transform.name)
        --     print("1927 -  #self.selectedRoleIcons=",#self.selectedRoleIDs)
        --     for i = num + 1, #self.selectedRoleIDs + 1 do
        --         print("1927 - Move from ", i, " to ", i-1)
        --         local t = self.roleIconNodes[i]:FindChild(tostring(i)) -- origin.
        --         if t then
        --             t.parent = self.roleIconNodes[i - 1]
        --             t.localPosition = Vector3.New(0, 0, 0)
        --             t.name = i-1
        --         end
        --     end
        -- end
        -- self.gridSelectedRole:Reposition()
        self:RefreshIconGrey()
    end
end

--------------------------------------
---删除之前位置选择好的球员
--- index :玩家角色对应的列表位置
--- id ：角色的id
--------------------------------------
function UISelectRole:MakeOnOtherRoleIconClose(index, id)
    print("1927 - MakeOnOtherRoleIconClose index =",index )
    -- local t, script = self:GetSelectIconByIndex(index)
    -- local oriId = script.id

    -- --刷新已选角色Id列表---
    -- local selectedRoleIDs = {}
    -- for i = 1, table.getn(self.selectedRoleIDs) do
    --     if self.selectedRoleIDs[i] ~= oriId then
    --         table.insert(selectedRoleIDs, self.selectedRoleIDs[i])
    --     end
    -- end
    -- self.selectedRoleIDs = selectedRoleIDs
    -- self.selectedRoleIcons[oriId] = nil

    -- NGUITools.Destroy(script.gameObject)
    local placeIcon = self.rightRolePlaceIcons[index]
    local preRoleId = placeIcon:GetRoleId()
    placeIcon:SetRoleId(id)

    if self.myPos == index then
        self:FocusRoleIcon(nil)
        if self.ownedRoleIcons[preRoleId] then
            self.ownedRoleIcons[preRoleId]:SetSelected(false)
        end
        self:RefreshIconGrey()
        return
    end
end


function UISelectRole:SendChangeFightRoleReq(selectedIDs)
    local req = {
        info = {
            {
                role_id = selectedIDs[1],
                status = "FS_MAIN",
            },
            {
                role_id = selectedIDs[2],
                status = "FS_ASSIST1",
            },
            {
                role_id = selectedIDs[3],
                status = "FS_ASSIST2",
            },
        }
    }
    for i =1, 3 do
        if selectedIDs[i] == nil or MainPlayer.Instance:GetRole(selectedIDs[i]) == nil then
            return
        end
    end

    local data = protobuf.encode("fogs.proto.msg.ChangeFightRole", req)
    LuaHelper.SendPlatMsgFromLua(MsgID.ChangeFightRoleID, data)
    print(self.uiName, "Send ChangeFightRole")
    self.sendChangeRole = false
end

function UISelectRole:HandleChangeFightRoleResp()
    return function (buf)
        local resp, err = protobuf.decode("fogs.proto.msg.ChangeFightRoleResp", buf)
        if resp then
            if resp.result == 0 then
                for k, v in pairs(resp.info) do
                    local squadInfo = MainPlayer.Instance.SquadInfo
                    local enum = squadInfo:GetEnumerator()
                    while enum:MoveNext() do
                        if enum.Current.status:ToString() == v.status then
                            enum.Current.role_id = v.role_id
                            break
                        end
                    end
                    if v.status == 'FS_MAIN' then
                        MainPlayer.Instance.CaptainID = v.role_id
                    end
                end
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            end
        else
            error(modname, "HandleChangeFightRoleResp: " .. err)
        end
    end
end

function UISelectRole:MakeOnStart()
    return function ()
        if self.isLadder then
            -- local t , roleIcon= self:GetSelectIconByIndex(self.myPos)
            --天梯至少要选一个人
            local placeIcon = self.rightRolePlaceIcons[self.myPos]
            if placeIcon == nil or placeIcon:GetRoleId() == nil then
                CommonFunction.ShowTip(getCommonStr("STR_PLEASE_SELECT_ONE_ROLE_TO_PLAY"), nil)
                return
            end

            local roleIcon = placeIcon:GetRoleIcon()
            self.btnStart.transform:GetComponent("UIButton").isEnabled = false
            placeIcon:CloseButtonVisible(false)

            if QualifyingNewerAI.isAI then
                QualifyingNewerAI.ClickSelectConfirm()
                return
            end



            local roleInfo = MainPlayer.Instance:GetRole2(roleIcon.id)
            local book_id = roleInfo.badge_book_id
            print("1927 - MakeOnStart to send confirm t.id=",roleIcon.id)
            local operation = {
                role_id = roleIcon.id,
                type = self.matchType,
                flag = 2,
                badge_book_id = book_id,
            }
            print("1927 - Send SelectFightRole Confirom ")
            local req = protobuf.encode("fogs.proto.msg.SelectFightRole",operation)
            LuaHelper.SendPlatMsgFromLua(MsgID.SelectFightRoleID,req)
            return
        end
        if self.is1vs1PracticeMatch then
            if self.maxSelection ~= #self.selectedRoleIDs then
                CommonFunction.ShowTip(getCommonStr("STR_PLEASE_SELECT_ONE_ROLE_TO_PLAY"), nil)
                return
            end
             local operation = {
                acc_id = MainPlayer.Instance.AccountID,--玩家唯一id
                type = 'MT_PRACTICE_1V1',--base.type 枚举
                game_mode = GameMode.GM_Practice1On1 :ToString(),--base.gamemode枚举
                practice_pve={
                    id=1,
                    fight_list={
                        game_mode=GameMode.GM_Practice1On1:ToString(),
                        fighters = {
                            {role_id = self.selectedRoleIDs[1], status = "FS_MAIN",},
                        }
                    }--球员列表
                }
            }
            local req = protobuf.encode("fogs.proto.msg.EnterGameReq",operation)
            LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:Make1vs1PracticeMatchHandler(), self.uiName)
            LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
            self.btnStart.transform:GetComponent("UIButton").isEnabled = false
            CommonFunction.ShowWaitMask()
            CommonFunction.ShowWait()
            return
        end
        print(self.uiName, "OnStart")
        printTable(self.selectedRoleIDs)
        if self.maxSelection ~= #self.selectedRoleIDs then
            CommonFunction.ShowTip(string.format(getCommonStr("PLEASE_SELECT_PLAYER_NUM"), self.maxSelection), nil)
            return
        end
        -------记录角色使用的涂鸦页信息-------
        if #self.selectedRoleIDs>0 then
            local bookInfo = {}
            for k,v in pairs(self.selectedRoleIDs) do
                local roleInfo = MainPlayer.Instance:GetRole2(v)
                table.insert(bookInfo,{['id'] = roleInfo.id,['value'] = roleInfo.badge_book_id})
            end
            local req = {
                book_use_info = bookInfo
            }
            local buf = protobuf.encode("fogs.proto.msg.BadgeBookUseReq",req)
            LuaHelper.SendPlatMsgFromLua(MsgID.BadgeBookUseReqID,buf)
        end

        if self.sendChangeRole and not self.isLadder then
            self:SendChangeFightRoleReq(self.selectedRoleIDs)
        end
        if self.onStart then self.onStart(self.selectedRoleIDs) end
    end
end

function UISelectRole:MakeOnClose()
    return function ()
        if self.isLadder then
            CommonFunction.ShowTip(getCommonStr("STR_CANNOT_GO_BACK_FOR_SELECT"), nil)
            return
        end
        self.is1vs1PracticeMatch=false;
        self:FocusRoleIcon(nil)
        TopPanelManager:HideTopPanel()
    end
end

function UISelectRole:VisibleModel(visible)
    self.model.gameObject:SetActive(visible)
end

function UISelectRole:RegisterForLadder(isRegister)
    if isRegister then
        LuaHelper.RegisterPlatMsgHandler(MsgID.NotifySelectFightRoleID, self:SelectFightRoleHandler(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyGameStartID, self:OnNotifyGameStart(), self.uiName)
        LuaHelper.RegisterPlatMsgHandler(MsgID.MatchOverID, self:MatchOverHandler(), self.uiName)
    else
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifySelectFightRoleID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.MatchOverID, self.uiName)
    end
    self.isLadderRegister = isRegister
end

function UISelectRole:UpdateChatMsg()
    return function(accId,msg)
        local index = nil
        for i = 1, 3 do
            if self.teamInfo ~= nil then
                if self.teamInfo[i].acc_id  == accId then
                    index = i
                    break
                end
            end
        end
        if index == nil then
            return
        end
        self.chatMsgPopItems[index]:SetContent(msg)
    end
end

----------------------------------------
--选择球员广播处理(用于天梯赛选人)
--右侧角色icon显示
--更新当前人物模型，及所选角色相关的信息
----------------------------------------
function UISelectRole:SelectFightRoleHandler()
    return function(buf)
        print("1927 - SelectFightRoleHandler is called")
        local resp, error = protobuf.decode("fogs.proto.msg.NotifySelectFightRole",buf)
        if resp then
            local accId  = resp.acc_id
            local roleId = resp.role_id
            local flag   = resp.flag
            local skillsInfo = nil
            if resp.role then
                skillsInfo = resp.role.skill_slot_info
                for k,v in pairs(skillsInfo) do
                    print("Skill信息HEAD ：skill_uuid:"..v.skill_uuid.."..skill_id:"..v.skill_id)
                end
            end

            print("1927 - Notify Select Role accId, roleId, flag=",accId, roleId, flag)
            local index
            local name
            for i = 1, 3 do
                if self.teamInfo[i].acc_id  == accId then
                    index = i
                    name = self.teamInfo[i].name
                    print("1927 - <UISelectRole> toGetName name=",name)
                    break
                end
            end

            -- roleId ~= 0 表示选择了球员
            if roleId ~= 0 then
                self:SelectOtherRoleIcon(index, roleId, flag, name,skillsInfo)
            -- roleid == 0（等同于重选球员）
            else
                self:MakeOnOtherRoleIconClose(index, nil)
            end
        end
    end
end
--进入1vs1练习赛消息返回
function UISelectRole:Make1vs1PracticeMatchHandler()
    return function(buf)
        CommonFunction.HideWaitMask()
        CommonFunction.StopWait()
        local resp, error = protobuf.decode("fogs.proto.msg.EnterGameResp",buf)
        if resp then
            local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
            print("lllllllllllllllaaaaaa:",resp.result)
            if resp then
                if resp.result == 0 then
                    self:SectionStart(resp.practice_pve.session_id)
                else
                    CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
                end
            else
                error("UISelectRole: ", err)
            end
            LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
        end
    end
end

function UISelectRole:SectionStart(session_id)
    self:VisibleModel(false)
    local teammates = UintList.New()
    for _,v in pairs(self.selectedRoleIDs) do
        teammates:Add(tonumber(v))
    end
    print("gamemode"..GameSystem.Instance.PracticePveConfig:GetConfig(1).gamemode)
    GameSystem.Instance.mClient:CreateNewMatch(GameSystem.Instance.PracticePveConfig:GetConfig(1).gamemode,session_id,false, GameMatch.LeagueType.ePractise1vs1, teammates,nil)
end


function UISelectRole:MatchOverHandler()
    return function(buf)
        print("1927 - <UISelectRole> MatchOverHandler()--Start")
        local resp, error = protobuf.decode("fogs.proto.msg.MatchOver",buf)
        if resp then
            local flag = resp.flag
            local accids = resp.acc_id
            local isInTeam = false
            local isMaster = false
            local isMe = false
            local homeExit = {}
            for k, v in pairs(accids) do
                print("1927 - <UISelectRole> MatchOverHander not prepare v(acc_id)=",v)
                for tk, tv in pairs(self.teamInfo) do
                    if v == tv.acc_id then
                        isInTeam = true
                        table.insert(homeExit, v)
                    end
                end
                if v == MainPlayer.Instance.AccountID then
                    isMe = true
                end

                if v == self.masterId then
                    isMaster = true
                end
            end

            if isMe then
                self:TryDestroyMsgBox()
                self.msgBox = CommonFunction.ShowPopupMsg(getCommonStr("STR_YOU_ARE_NOT_READY_GO_BACK_TO_HALL"),
                                            nil,
                                            LuaHelper.VoidDelegate(self:OnConfirmBackToHall()),
                                            nil,
                                            getCommonStr("BUTTON_CONFIRM"),
                                            nil)
            elseif isMaster then
                self:TryDestroyMsgBox()
                self.msgBox = CommonFunction.ShowPopupMsg(getCommonStr("STR_BACK_TO_HALL_DUE_TO_MASTER_NOT_READY"),
                                            nil,
                                            LuaHelper.VoidDelegate(self:OnConfirmBackToHall()),
                                            nil,
                                            getCommonStr("BUTTON_CONFIRM"),
                                            nil)
            else
                self:TryDestroyMsgBox()
                local str = getCommonStr("STR_SB_NOT_READY_BACK_TO_QUALIFYINER")
                if self.matchType == "MT_PVP_3V3" then
                    str = getCommonStr("STR_SB_NOT_READY_BACK_TO_REGULAR")
                elseif self.matchType == "MT_QUALIFYING_NEWER" then
                    str = getCommonStr("STR_SB_NOT_READY_BACK_TO_QUALIFYINER")
                end
                CommonFunction.ShowTip(str, nil)
                Scheduler.Instance:AddTimer(1.5, false, self:OnConfirmBackToLadder(homeExit))

                -- self.msgBox = CommonFunction.ShowPopupMsg(getCommonStr("STR_SB_NOT_READY_BACK_TO_LADDER"),
                --                             nil,
                --                             LuaHelper.VoidDelegate(self:OnConfirmBackToLadder(homeExit)),
                --                             nil,
                --                             getCommonStr("BUTTON_CONFIRM"),
                --                             nil)
            end
        end
    end
end

function UISelectRole:TryDestroyMsgBox()
    if self.msgBox then
        NGUITools.Destroy(self.msgBox.gameObject)
        self.msgBox = nil
    end
end

function UISelectRole:OnConfirmBackToHall()
    return function()
        self:ExitAsLadder()
        TopPanelManager:ShowPanel("UIHall")
    end
end


function UISelectRole:OnConfirmBackToLadder(homeExit)
    return function()
        self:ExitAsLadder()
        local nextUI = "UIQLadder"

        if self.matchType == "MT_QUALIFYING_NEWER" then
            print("1927 - <UISelectRole> OnConfirmBackToLadder self.preUI=",self.preUI)
            if self.preUI then
                nextUI = self.preUI
            else
                nextUI = "UIQualifyingNewer"
            end
        elseif self.matchType == "MT_PVP_3V3" then
            if self.preUI then
                nextUI = self.preUI
            else
                nextUI = "UIQLadder"
            end
        end

        TopPanelManager:ShowPanel(nextUI, nil, {
            removeAccIdForBack = homeExit, nextShowUI = "UIHall"
        })
    end
end

--------------------------------------------------------------------------------
-- Function Name : ExitAsLadder
-- Create Time   : Tue Mar 22 15:45:20 2016
-- Input Value   : nil
-- Return Value  : nil
-- Description   : For ladder to handle when level UISelectRole
--------------------------------------------------------------------------------
function UISelectRole:ExitAsLadder()
    self:VisibleModel(false)
    self.msgBox = nil
    if self.isLadder and self.isLadderRegister and not QualifyingNewerAI.isAI then
        self:RegisterForLadder(false)
    end
    self.isLadder = false
end

function  UISelectRole:OnNotifyGameStart()
    return function (buf)
        print("1927 -  UISelectROle OnNotifyGameStart called")
        self:ExitAsLadder()
        if self.matchType == "MT_PVP_3V3" then
            Ladder.HandleNotifyGameStart(buf)
        elseif self.matchType == "MT_QUALIFYING_NEWER" then
            print("1927 - <UISelectRole>  buf=",buf)
            QualifyingNewer.HandleNotifyGameStart(buf)
        end


        if self.uiGameChatModule ~= nil then
            self.uiGameChatModule:CloseChatModue()
        end
        for m = 1,3 do
            NGUITools.SetActive(self.chatMsgPopItems[m].gameObject,false)
        end
    end
end

-----------------------------------------------------------
function UISelectRole:RefreshTalent(roleId)
    local roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleId)
    local config = GameSystem.Instance.talentConfig:GetConfigData(roleData.special_attr)
    if config == nil then
        print("talent data is nil ==>"..roleData.special_attr)
        do return end
    end

    self.uiTalentName.text = config.name
    self.uiTalentDesc.text = config.desc

    local str = string.split(config.icon, '&')
    local max = table.getn(str)
    if max == 2 then
        self.uiTalentIcon.atlas = ResourceLoadManager.Instance:GetAtlas(str[1]);
        self.uiTalentIcon.spriteName = str[2]
    end
end

-----------------------------
--- 获取右侧图标占位符
-----------------------------
function UISelectRole:GetPlaceIconByRoldId(roleId)
    local count = #self.rightRolePlaceIcons
    for i = 1,count do
        local tempRoldId = self.rightRolePlaceIcons[i]:GetRoleId()
        if tempRoldId == roleId then
            return self.rightRolePlaceIcons[i]
        end
    end
    return nil
end

-----------------------------
--- 根据球员角色id设置右侧选中状态
-----------------------------
function UISelectRole:SetRightIconSelectedStatusByRoleId(roldId)
    local placeIcon = self:GetPlaceIconByRoldId(roldId)
    if placeIcon then
        self:SetPlaceRoleIconSelectedIndex(placeIcon.Index)
    end
end

-----------------------------
--- 右侧球员图标点击事件处理
-----------------------------
function UISelectRole:OnPlaceRoleIconClickHanlder()
    return function(index)
        print("UISelectRole:OnPlaceRoleIconClickHanlder".."index:"..index)
        self:SetPlaceRoleIconSelectedIndex(index)
    end
end

-----------------------------
--- 根据占位index设置选中状态
-----------------------------
function UISelectRole:SetPlaceRoleIconSelectedIndex(index)
    self.currentPlaceSelectIndex = index
    local count = #self.rightRolePlaceIcons
    for i = 1,count do
        self.rightRolePlaceIcons[i]:Selected(false)
    end
    self.rightRolePlaceIcons[index]:Selected(true)
    local roleIcon = self.rightRolePlaceIcons[index]:GetRoleIcon()
    self:FocusRoleIcon(roleIcon)
end

-----------------------------------------------
--- 根据空位规则获取右侧接下来需要做出的空位
--- 规则一，如果当前位置没有球员，填充当前位置
---         下一个位置为从上到下没有占用的位置
--- 规则二，如果当前位置已经选择了球员，则更新
---         当前位置的球员，下一个位置为从上到
---          下没有占用的位置
--- 规则三，如果当前位置填充后，没有其它位置,则
---         下一个位置就是当前位置
----------------------------------------------
function UISelectRole:GetPlaceRoleIconNextIndex()
    if #self.selectedRoleIDs >= self.maxSelection then
        return self.currentPlaceSelectIndex
    end

    for i = 1 ,self.maxSelection do
        if self.rightRolePlaceIcons[i]:GetRoleId() == nil then
            return i
        end
    end
end

---------------------------------------
---获取己经选择了角色id集合
---------------------------------------
function UISelectRole:GetSelectRoleIDs()
    local ids = {}
    for i = 1,self.maxSelection do
        local id = self.rightRolePlaceIcons[i]:GetRoleId()
        if id ~= nil then
            table.insert(ids,id)
        end
    end
    return ids
end

function UISelectRole:ShowRoleDetailSkillTip()
    return function(isShow,roleId,index,skillsInfo)
        --if isShow then
            local tipGo = createUI("RoleSkillsDetailTips",self.uiRoleSkillsTipsNode)
            tipGo.transform.localPosition = Vector3.New(0,(index-1)*-120,-500)
            local t = getLuaComponent(tipGo)
            t:SetRoleId(roleId)
            t:DisplaySkills(skillsInfo)
        --else
            --CommonFunction.ClearChild(self.uiRoleSkillsTipsNode.transform)
        --end
    end
end

function UISelectRole:CallbackFuncForBuyPlayer()
    return function(bool)
        self.freshAll = bool
    end
end

return UISelectRole

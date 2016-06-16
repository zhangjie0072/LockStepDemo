TeamUpgradePopup =  {
    uiName = 'TeamUpgradePopup',
    ----------------------------
    onClose,
    --------------------------UI
    uiBtnOK,
    uiLvUpItemGrid = nil,
    uiBackTip = nil,
}

function TeamUpgradePopup:Awake()
    self.uiBtnOK          = self.transform:FindChild('ButtonOK').gameObject
    self.teamGrid         = self.transform:FindChild('Bg/Goods/Grid'):GetComponent("UIGrid")
    self.uiAnimator       = self.transform:GetComponent("Animator")
    self.uiUnlockGrid     = self.transform:FindChild("Bg/UnLock")
    self.uiUnlockIcon     = self.transform:FindChild("Bg/UnLock/Icon"):GetComponent("UISprite")
    self.uiUnlockDescribe = self.transform:FindChild("Bg/UnLock/Text"):GetComponent("UILabel")
    self.uiLvUpItemGrid   = self.transform:FindChild("Grid"):GetComponent("UIGrid")
    self.uiBackTip        = self.transform:FindChild("Background/BackTip")
    --self.leveltitle  = self.transform:FindChild("Up/Level/Leveltitle"):GetComponent("UILabel")
end

function TeamUpgradePopup:Start()
    --ModelShowItem.HideExcept(self.model_show_item)
    addOnClick(self.uiBtnOK,self:ClickOK())
    self:Refresh()
    GuideSystem.Instance:ReqBeginGuide(self.uiName)

    self.cur_level = MainPlayer.Instance.Level

    UIManager.Instance:BringPanelForward(self.gameObject)
    -- self.transform:GetComponent("UIPanel").depth = 1000
end

function TeamUpgradePopup:Refresh()
    -- left grid
    CommonFunction.ClearGridChild(self.teamGrid.transform)


    self.pre_level = MainPlayer.Instance.prev_level
    self.cur_level = MainPlayer.Instance.Level

    --self.preleveltitle.text = tostring(self.pre_level)
    --self.leveltitle.text = tostring(self.cur_level)

    print('self.pre_level='..tostring(self.pre_level))
    print('self.cur_level='..tostring(self.cur_level))

    local g = createUI('TeamUpgradeItem', self.teamGrid.transform).transform
    g:FindChild('Name'):GetComponent('UILabel').text = getCommonStr("TEAM_LEVEL")
    g:FindChild('PrevValue'):GetComponent('UILabel').text = tostring(self.pre_level)
    g:FindChild('CurValue'):GetComponent('UILabel').text =  tostring(self.cur_level)

    local pre_hp = MainPlayer.Instance.prev_hp
    local add_hp = GameSystem.Instance.TeamLevelConfigData:GetAddHp(self.cur_level)
    local cur_hp = MainPlayer.Instance.Hp
    if pre_hp >= cur_hp then
        pre_hp = cur_hp - add_hp
    else
        cur_hp = pre_hp + add_hp
    end

    local g = createUI('TeamUpgradeItem', self.teamGrid.transform).transform
    g:FindChild('Name'):GetComponent('UILabel').text = getCommonStr("CUR_HP")
    g:FindChild('PrevValue'):GetComponent('UILabel').text = tostring(pre_hp)
    g:FindChild('CurValue'):GetComponent('UILabel').text =  tostring(cur_hp)

    local pre_max_hp = GameSystem.Instance.TeamLevelConfigData:GetMaxHP(self.pre_level)
    local cur_max_hp = GameSystem.Instance.TeamLevelConfigData:GetMaxHP(self.cur_level)

    -- local g = createUI('TeamUpgradeItem', self.teamGrid.transform).transform
    -- g:FindChild('Name'):GetComponent('UILabel').text = getCommonStr("MAX_HP")
    -- g:FindChild('PrevValue'):GetComponent('UILabel').text = tostring(pre_max_hp)
    -- g:FindChild('CurValue'):GetComponent('UILabel').text =  tostring(cur_max_hp)

    local pre_max_role_quality = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(self.pre_level)
    local cur_max_role_quality = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(self.cur_level)

    local g = createUI('TeamUpgradeItem', self.teamGrid.transform).transform
    g:FindChild('Name'):GetComponent('UILabel').text = getCommonStr("PLAYER_LEVEL_LIMIT")
    g:FindChild('PrevValue'):GetComponent('UILabel').text = tostring(pre_max_role_quality)
    if pre_max_role_quality < cur_max_role_quality then
        g:FindChild('CurValue'):GetComponent('UILabel').text = tostring(cur_max_role_quality)
    else
        NGUITools.SetActive(g:FindChild('Arrow').gameObject, false)
        g:FindChild('CurValue'):GetComponent('UILabel').text = ''
    end

    self.teamGrid:Reposition()
    self:SetUnlockItem()
    self.teamGrid.gameObject:SetActive(false)
    self.uiBackTip.gameObject:SetActive(false)

    self:UnLockSkillAfterLevelUp()
end

-- function TeamUpgradePopup:ClickOK()
--	return function()
--		--ModelShowItem.ResumeHidden()
--		NGUITools.Destroy(self.gameObject)
--		--TopPanelManager:HideTopPanel()
--	end
-- end


function TeamUpgradePopup:ClickOK()
    return function ()
        local qLadder = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("UIQLadder(Clone)")
        if qLadder and qLadder.gameObject.activeSelf then
            Ladder.SendExitRoom()
            TopPanelManager:Remove("UIQLadder")
            qLadder.gameObject:SetActive(false)
        end

        if self.uiAnimator then
            self:AnimClose()
        else
            self:OnClose()
        end
    end
end

function TeamUpgradePopup:ClickItem()
    return function(item)
        self:ClickOK()()
    end
end





function TeamUpgradePopup:OnClose( ... )
    self:OnDestroy()
end

function TeamUpgradePopup:OnDestroy()
    if self.onClose then
        self.onClose()
    end

    -- NGUITools.Destroy(self.uiAnimator)
    -- NGUITools.Destroy(self.transform)
    NGUITools.Destroy(self.gameObject)
end

function TeamUpgradePopup:SetUnlockItem()
    CommonFunction.ClearGridChild(self.uiLvUpItemGrid.transform)
    local unLockdata = GameSystem.Instance.TeamLevelConfigData:GetUnLockdata(MainPlayer.Instance.Level)
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
            local t = getLuaComponent(createUI("TeamUpItem",self.uiLvUpItemGrid.transform))
            t:SetData(links:get_Item(i),subId:get_Item(i),  icons:get_Item(i), des:get_Item(i))
            t.onClick = self:ClickItem()
        end

        -- NGUITools.SetActive(self.uiUnlockGrid.gameObject, true)
        -- self.uiUnlockIcon.spriteName = unLockdata.unlock_icon
        -- self.uiUnlockDescribe.text = unLockdata.unlock_describe
        -- self.onClose = self:ShowLinkUi(unLockdata.link)
    end
    self.uiLvUpItemGrid.repositionNow = true
end

function TeamUpgradePopup:ShowLinkUi(link)
    return function()
        if link == nil then
            return
        end
        local linkId = link:get_Item(0)
        local uiName = GameSystem.Instance.TaskConfigData:GetLinkUIName(linkId)
        print('--------link uiName: ', uiName)
        local count = 0
        local enum = link:GetEnumerator()
        while enum:MoveNext() do
            count = count + 1
        end
        local subID = 0
        if count > 1 then
            subID = link:get_Item(1)
            print('--------link subID: ', subID)
        end
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
                UIStore:OpenStore()
            elseif uiName == "UIPlayerBuyDiamondGoldHP" then
                local buyProperty = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
                if subID == 2 then
                    buyProperty.BuyType = "BUY_GOLD"
                elseif subID == 4 then
                    buyProperty.BuyType = "BUY_HP"
                end
                NGUITools.Destroy(self.parent.gameObject)
                --TopPanelManager:ShowPanel("UIPlayerBuyDiamondGoldHP")
            elseif uiName == "UIPVPEntrance" then
                if subID == 1 then
                    if not validateFunc("OneVsOne") then
                        return
                    end
                end
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
                CommonFunction.ShowWait()
            elseif uiName == "UIShootGame" then
                if not validateFunc("Shoot") then
                    return
                end
                local ShootOpenReq = {
                    acc_id = MainPlayer.Instance.AccountID
                }

                local req = protobuf.encode("fogs.proto.msg.ShootOpenReq",ShootOpenReq)
                print("Send Shoot Open ShootOpenReq.acc_id=", ShootOpenReq.acc_id)
                LuaHelper.SendPlatMsgFromLua(MsgID.ShootOpenReqID,req)
                LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpenRespID, self:HandleShootOpen(), self.uiName)
                CommonFunction.ShowWaitMask()
                CommonFunction.ShowWait()
            else
                if uiName == "UIQualifying" and not validateFunc("UIQualifying") then return end
                if uiName == "UITour" and not validateFunc("UITour") then return end
                -- print(self.uiName,"------jump to uiName:",uiName)
                -- self.parent:OnClose()
                -- self.parent.parent.nextShowUI = uiName
                -- self.parent.parent:DoClose()
                -- self.parent.parent:OnClose()
                TopPanelManager:ShowPanel(uiName, subID, params)
            end
        end
    end
end

function TeamUpgradePopup:HandleShootOpenResp(buf)
    CommonFunction.HideWaitMask()
    CommonFunction.StopWait()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpenRespID, self.uiName)
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

function TeamUpgradePopup:HandleShootOpen()
    return function(buf)
        if self:HandleShootOpenResp(buf) then
            TopPanelManager:ShowPanel("UIShootGame")
        end
    end
end

function TeamUpgradePopup:HandleGetNPC()
    return function(buf)
        CommonFunction.HideWaitMask()
        CommonFunction.StopWait()
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

function TeamUpgradePopup:UnLockSkillAfterLevelUp( ... )
    local root = UIManager.Instance.m_uiRootBasePanel
    local skillManager = root.transform:FindChild("UISkillManager(Clone)")
    if skillManager then
        local skillLua = getLuaComponent(skillManager.gameObject)
        skillLua:RefreshAfterLevelUp()
    end
end

return TeamUpgradePopup

------------------------------------------------------------------------
-- class name    : TeamUpItem
-- create time   : Sat Mar 12 18:21:13 2016
------------------------------------------------------------------------

TeamUpItem =  {
    uiName     = "TeamUpItem",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    linkId,
    subId,
    clickedLadder,
    onClick,
    -----------------------
    -- Parameters Module --
    -----------------------
    uiIcon = nil,
    uiText = nil,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function TeamUpItem:Awake()
    self:UiParse()				-- Foucs on UI Parse.
end


function TeamUpItem:Start()
    addOnClick(self.gameObject, self:Click())
    self:Refresh()
end

function TeamUpItem:Refresh()

end

-- uncommoent if needed
-- function TeamUpItem:FixedUpdate()

-- end


function TeamUpItem:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function TeamUpItem:SetData(linkId,subId,  icon, text)
    print("1927 - <TeamUpItem>  linkId, subId, icon, text=",linkId, subId, icon, text)
    self.linkId = linkId
    self.subId  = subId
    self.uiIcon.spriteName = icon
    self.uiText.text = text
end


function TeamUpItem:Click()
    return function()
        print("1927 - <TeamUpItem> Click called self.linkId, self.subId=",self.linkId, self.subId)
        local qLadder = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("UIQLadder(Clone)")
        if qLadder and qLadder.gameObject.activeSelf then
            Ladder.SendExitRoom()
            TopPanelManager:Remove("UIQLadder")
            qLadder.gameObject:SetActive(false)
        end


        local uiName = GameSystem.Instance.TaskConfigData:GetLinkUIName(self.linkId)
        print('--------link uiName: ', uiName)
        -- local subID = GameSystem.Instance.TaskConfigData:GetTaskLinkSubID(self.linkId)
        local subID = self.subId
        print('--------link subID: ', subID)
        subID = subID > 0 and subID or nil

        if uiName ~= '' then
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
            elseif uiName == "UILadder" then
                self:ClickLadder()()
            else
                TopPanelManager:ShowPanel(uiName, subID)
            end
        end

        if self.onClick then
            self:onClick()
        end

    end
end


function TeamUpItem:ClickLadder()
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

function TeamUpItem:FriendListHandler()
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

function TeamUpItem:HandleCreateRoom()
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
function TeamUpItem:UiParse()
    self.uiIcon = self.transform:GetComponent("UISprite")
    self.uiText = self.transform:FindChild("Text"):GetComponent("UILabel")

end

return TeamUpItem

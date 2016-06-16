------------------------------------------------------------------------
-- class name    : LadderFriendItem
-- create time   : Thu Mar  3 16:18:39 2016
------------------------------------------------------------------------

LadderFriendItem =  {
    uiName     = "LadderFriendItem",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    uiSelect       = nil,
    uiState        = nil,
    uiRoleIcon     = nil,
    uiFriendName         = nil,
    uiLadderLvIcon = nil,
    uiLadderLvName = nil,
    uiBtn          = nil,
    uiBtnLabel     = nil,

    -----------------------
    -- Parameters Module --
    -----------------------
    onClick    = nil,
    careerIcon = nil,
    -- roleId     = nil,
    state         = nil,
    name          = nil,
    friendInfo    = nil,
    isMember      = false,
    isInviting    = false,
    onClickInvite = nil,
    member        = nil,
    isPattern     = false,
    isMaster      = false,
    inviteStart   = 0,
    inviteWait    = 0,
}

-- Button Action.
local BA = {
    HIDE          = 1,
    INVITE        = 2,
    CANCEL_INVITE = 3,
    DISABLE       = 4,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function LadderFriendItem:Awake()
    self:UiParse()				-- Foucs on UI Parse.
    self.inviteWait = tonumber(GameSystem.Instance.CommonConfig:GetString("gPVPInviteFriendTimeOut"))
end


function LadderFriendItem:Start()
    addOnClick(self.gameObject, function() if self.onClick then self:onClick() end end )
    addOnClick(self.uiBtn.gameObject, self:ClickInvite())
    self:Refresh()
end

function LadderFriendItem:Refresh()
    self:DataRefresh()
end


function LadderFriendItem:FixedUpdate()
    if self.isInviting then
        if os.time() - self.inviteStart >= self.inviteWait then
            self:SetIsInviting(false)
        end
    end
end


function LadderFriendItem:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function LadderFriendItem:Selected(isSelected)
    self.uiSelect.gameObject:SetActive(isSelected)
end


function LadderFriendItem:DataRefresh()
    local info = self.friendInfo
    local name = info.name

    local score = info.pvp_ladder_score
    local lv = GameSystem.Instance.ladderConfig:GetLevelByScore(score)
    self.uiLadderLvIcon.spriteName = lv.iconSmall
    self.uiLadderLvName.text = lv.name

    self.uiFriendName.text = name
    if not self.careerIcon then
        local t = getLuaComponent(createUI("CareerRoleIcon", self.uiRoleIcon))
        t.id = tonumber(self.friendInfo.icon)
        t.showPosition = false
        t:Refresh()
        self.careerIcon = t
    end
    self:UpdateLadderState()
end

function LadderFriendItem:ClickInvite()
    return function()
        if self.onClickInvite then
            self:onClickInvite()
        end
    end
end

function LadderFriendItem:UpdateLadderState()
    local l        = self.uiState
    local ol       = self.friendInfo.online
    local member   = self.isMember
    local inv      = self.isInviting
    local inMatch  = ol == Ladder.PS.MATCH or ol == Ladder.PS.GAME

    print("1927 - <LadderFriendItem> UpdateLadderState self.friendInfo.acc_id, ol, member, inv, inMatch, self.friendInfo.icon=",self.friendInfo.acc_id, ol, member, inv, inMatch, self.friendInfo.icon)

    if member then
        l.text = getCommonStr("STR_MEMBER2")
        self:ApplyButton(BA.DISABLE)
    elseif inv then
        l.text = getCommonStr("WAITING")
        self:ApplyButton(BA.DISABLE)
    elseif inMatch then
        l.text = getCommonStr("LADDER_STATE_IN_MATCH")
        self:ApplyButton(BA.DISABLE)
    elseif ol == Ladder.PS.OFFLINE then
        l.text = getCommonStr("STR_OFFLINE")
        self:ApplyButton(BA.DISABLE)
    elseif ol == Ladder.PS.ROOM then
        l.text = getCommonStr("STR_IN_MATCH_MEMBER")
        self:ApplyButton(BA.DISABLE)
    else
        l.text = getCommonStr("FREE_STATE")
        self:ApplyButton(BA.INVITE)
    end

    -- CarrerRoleIcon
    if self.careerIcon then
        self.careerIcon.disabled = ol == Ladder.PS.OFFLINE
        if ol == Ladder.PS.OFFLINE then
            self.uiLadderLvIcon.color = Color.New(0,1,1,1)
        else
            self.uiLadderLvIcon.color = Color.New(1,1,1,1)
        end
        self.careerIcon:Refresh()
    end

    if self.isPattern then
        self:ApplyButton(BA.DISABLE)
    end

    -- if not self.isMaster then
    --     self:ApplyButton(BA.HIDE)
    -- end
end

function LadderFriendItem:ApplyButton(ba)
    self.uiBtn.gameObject:SetActive(BA.HIDE ~= ba)
    self.uiBtn.isEnabled = BA.DISABLE~=ba

    if BA.INVITE==ba then
        self.uiBtnLabel:SetText(getCommonStr("STR_LADDER_INVITE"))
    elseif BA.CANCEL_INVITE==ba then
        self.uiBtnLabel:SetText(getCommonStr("BUTTON_CANCEL"))
    elseif BA.DISABLE==ba then
        self.uiBtnLabel:SetText(getCommonStr("STR_LADDER_INVITE"))
    end
end

function LadderFriendItem:SetData(info)
    self.friendInfo = info
end


function LadderFriendItem:SetIsInviting(isInviting)
    self.isInviting = isInviting
    if isInviting then
        self.inviteStart = os.time()
    end

    self:DataRefresh()
end

function LadderFriendItem:SetFree()
    self.friendInfo.online = Ladder.PS.NORMAL
    self:DataRefresh()
end

function LadderFriendItem:SetPattern(isPattern)
    self.isPattern = isPattern

    if self.member ~= nil then
        self:UpdateByMemberChange(self.member)
    else
        self:UpdateLadderState()
    end
    -- if isPattern then
    --	self:UpdateLadderState()
    -- else
    --	if self.member ~= nil then
    --		self:UpdateByMemberChange(self.member)
    --	else
    --		self:UpdateLadderState()
    --	end
    -- end
end


function LadderFriendItem:OnUserInfoChanged()
    return function(member)
        print("1927 - <LadderFriendItem> OnUserInfoChanged called")
        self:UpdateByMemberChange(member)
    end
end

function LadderFriendItem:UpdateByMemberChange(member)
    self.member = member
    local userInfo = member.userInfo
        print("1927 - <LadderFriendItem>  userInfo=",userInfo)
        if userInfo then
            print("1927 - <LadderFriendItem>  self.friendInfo.online=",self.friendInfo.online)
            self.isMember = userInfo.state == "RUS_NORMAL" and self.friendInfo.online ~= Ladder.PS.GAME
            if self.isMember then
                self.isInviting = false
            end
        else
            self.isMember = false
            self.isInviting = false
        end
        self:UpdateLadderState()
end



--------------------------------------------------------------------------------
-- Function Name : ApplyMasterChange
-- Create Time   : Thu Mar 31 12:00:03 2016
-- Input Value   : bool
-- Return Value  : nil
-- Description   : 根据是否是房主，影响UI改变。
--------------------------------------------------------------------------------
function LadderFriendItem:ApplyMasterChange(isMaster)
    self.isMaster = isMaster
    self:UpdateLadderState()
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
function LadderFriendItem:UiParse()
    self.uiSelect             = self.transform:FindChild("Selected")
    self.uiState              = self.transform:FindChild("State"):GetComponent("UILabel")
    self.uiRoleIcon           = self.transform:FindChild("RoleIcon"):GetComponent("Transform")
    self.uiFriendName         = self.transform:FindChild("Name"):GetComponent("UILabel")
    self.uiLadderLvIcon       = self.transform:FindChild("Icon"):GetComponent("UISprite")
    self.uiLadderLvName       = self.transform:FindChild("Icon/Name"):GetComponent("UILabel")
    self.uiBtn                = self.transform:FindChild("ButtonOK"):GetComponent("UIButton")
    self.uiBtnLabel           = self.transform:FindChild("ButtonOK/Label"):GetComponent("MultiLabel")
end

return LadderFriendItem

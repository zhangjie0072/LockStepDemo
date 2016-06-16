------------------------------------------------------------------------
-- class name    : LadderMemberIcon
-- create time   : Thu Mar  3 18:18:07 2016
------------------------------------------------------------------------

LadderMemberIcon =  {
    uiName     = "LadderMemberIcon",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------

    -----------------------
    -- Parameters Module --
    -----------------------
    roleIcon          = nil,
    userInfo          = nil,
    isMember          = false,
    userInfoChangeFun = nil,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function LadderMemberIcon:Awake()
    self:UiParse()				-- Foucs on UI Parse.
end


function LadderMemberIcon:Start()

    self:Refresh()
end

function LadderMemberIcon:Refresh()

end

-- uncommoent if needed
-- function LadderMemberIcon:FixedUpdate()

-- end


function LadderMemberIcon:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function LadderMemberIcon:SetData(userInfo, userInfoChangeFun)
    self.uiNotSelected.gameObject:SetActive(not userInfo)
    self.uiIcon.gameObject:SetActive(userInfo ~= nil)

    if userInfoChangeFun then
        self.userInfoChangeFun = userInfoChangeFun
    end

    self.userInfo = userInfo
    if self.userInfoChangeFun then
        self:userInfoChangeFun()
    end

    if userInfo then
        if not self.roleIcon  then
            self.roleIcon = getLuaComponent(createUI("CareerRoleIcon", self.uiIcon))
        end
        local t = self.roleIcon
        t.id = tonumber(userInfo.icon)
        t.transform.name = userInfo.acc_id
        t.showName = userInfo.name
        t.showPosition = false
        local state = userInfo.state
        local accId = userInfo.acc_id
        print("1927 - <LadderMemberIcon> MemberIcon SetData state=",state)
        print("1927 - <LadderMemberIcon>  accId, MainPlayer.Instance.AccountID=",accId, MainPlayer.Instance.AccountID)

        t.disabled = state == "RUS_RETURN" and accId ~= MainPlayer.Instance.AccountID


        -- if state == "RUS_BACKING" then
        --     t.disabled = state == "RUS_BACKING"
        -- end

        t:Refresh()
    else
        self:SetIsMaster(false)
        self.userInfoChangeFun = nil -- reset.
    end

    local strInfo = userInfo and userInfo.state or ""
    self:SetLadderState(self.uiState, strInfo)
end


function LadderMemberIcon:SetLadderState(l, state)
    print("1927 - LadderMemberIcon SetLadderState state=",state)
    l.gameObject:SetActive(state== "RUS_WAIT")
    if state == "RUS_WAIT" then
        l.text = getCommonStr("LADDER_STATE_WAIT")
    -- elseif state == "RUS_" then
    --	l.text = getCommonStr("LADDER_STATE_IN_MATCH")
    end
end

--------------------------------------------------------------------------------
-- Function Name : SetIsMaster
-- Create Time   : Wed Apr  6 15:04:25 2016
-- Input Value   : 是否是房主
-- Return Value  : nil
-- Description   : 用于添加房主标识
--------------------------------------------------------------------------------
function LadderMemberIcon:SetIsMaster(isMaster)
    if not self.roleIcon then
        return
    end
    self.roleIcon:SetIsMaster(isMaster)
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
function LadderMemberIcon:UiParse()
    self.uiNotSelected = self.transform:FindChild("NotSelected")
    self.uiIcon        = self.transform:FindChild("Icon")
    self.uiState       = self.transform:FindChild("State"):GetComponent("UILabel")
end

return LadderMemberIcon

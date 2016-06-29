------------------------------------------------------------------------
-- class name    : UIPracticeCourt
-- create time   : Tue Feb 23 11:10:32 2016
------------------------------------------------------------------------

UIPracticeCourt1 =  {
    uiName     = "UIPracticeCourt1",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    uiBackBtn       = nil,
    uiButtonMenu    = nil,
    uiFreePractice  = nil,
    uiPracticeMatch = nil,
    uiFreePractice1 = nil,
    uiAnimator      = nil,
    ui1Vs1Match = nil,
    -----------------------
    -- Parameters Module --
    -----------------------
    btnMenu,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIPracticeCourt1:Awake()
    self:UiParse()              -- Foucs on UI Parse.
    local t = getLuaComponent(createUI("ButtonBack", self.uiBackBtn))
    t.onClick = self:ClickBack()

    t = getLuaComponent(createUI("ButtonMenu",self.uiButtonMenu))
    t:SetParent(self.gameObject,false)
    t.parentScript = self
    self.btnMenu = t
end


function UIPracticeCourt1:Start()
    addOnClick(self.uiFreePractice1.gameObject, self:ClickItem(0))
    addOnClick(self.uiFreePractice.gameObject, self:ClickItem(1))
    addOnClick(self.uiPracticeMatch.gameObject, self:ClickItem(2))
    self:Refresh()
end

function UIPracticeCourt1:Refresh()

end

-- uncommoent if needed
-- function UIPracticeCourt:FixedUpdate()

-- end


function UIPracticeCourt1:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function UIPracticeCourt1:ClickBack()
    return function()
        self:DoClose()
    end
end

function UIPracticeCourt1:DoClose()
    if self.uiAnimator then
        print(self.uiName,":Set uinanimator close true")
        self:AnimClose()
    else
        self:OnClose()
    end
end

function UIPracticeCourt1:OnClose()
	
	print('sssssssssssssssssssdfdsgdg')
    self.btnMenu:SetParent(self.gameObject, true)

    if self.onClose then
        self.onClose()
        self.onClose = nil
        return
    end

    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        jumpToUI("UIHall")
    end
end


function UIPracticeCourt1:ClickItem(index)
    return function()
        if index == 0 then
            --TopPanelManager:ShowPanel("UISelectRole")
            -- Regular1V1Handler.SelectRole()
            if not FunctionSwitchData.CheckSwith(FSID.season) then return end

            if Ladder.CheckNetState(LuaHelper.VoidDelegate(self:OnCheckLadderOK()), self:OnCheckLadderCancel()) then
                Ladder.JoinLadder()
            end

        elseif index == 1 then
            if not FunctionSwitchData.CheckSwith(FSID.robot_train) then return end

           TopPanelManager:ShowPanel("UIPracticeCourt")
        elseif index == 2 then
            CommonFunction.ShowTip(getCommonStr('IN_CONSTRUCTING'), nil)
        end
    end
end

function UIPracticeCourt1:OnCheckLadderOK()
    return function()
        Ladder.JoinLadder()
    end
end


function UIPracticeCourt1:OnCheckLadderCancel()
    return function()
    end
end



---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.                                          --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.                                    --
-- NOTE:                                                                                         --
--  1. This function only used to parse the UI(GameObject).                                      --
--  2. The name start with self.ui which means is ONLY used for naming Prefeb.                   --
--  3. The name is according to the structure of prefab.                                         --
--  4. Please Do NOT MINDE the Comment Lines.                                                    --
--  5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.       --
---------------------------------------------------------------------------------------------------
function UIPracticeCourt1:UiParse()
    self.uiBackBtn       = self.transform:FindChild("TopLeft/ButtonBack"):GetComponent("Transform")
    self.uiFreePractice1  = self.transform:FindChild("Middle/1/FreePractice1")
    self.uiFreePractice = self.transform:FindChild("Middle/2/FreePractice")
    self.uiPracticeMatch = self.transform:FindChild("Middle/3/PracticeMatch")

end



return UIPracticeCourt1

--------------------------------------------------------------------------------
-- class name    : Combo
-- create time   : Thu May 26 16:01:54 2016
--------------------------------------------------------------------------------


Combo =  {
    uiName     = "Combo",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------

    -----------------------
    -- Parameters Module --
    -----------------------
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function Combo:Awake()
    self:UiParse()				-- Foucs on UI Parse.
end


function Combo:Start()

    self:Refresh()
end

function Combo:Refresh()

end

-- uncommoent if needed
-- function Combo:FixedUpdate()

-- end


function Combo:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function Combo:SetOn(isOn)
    self.uiOn.gameObject:SetActive(isOn)
    self.uiOff.gameObject:SetActive(not isOn)
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
function Combo:UiParse()
    self.uiOn = self.transform:FindChild("On")
    self.uiOff =self.transform:FindChild("Off")

end

return Combo

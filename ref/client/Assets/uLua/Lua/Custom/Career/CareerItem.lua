--------------------------------------------------------------------------------
-- class name    : CareerItem
-- create time   : Tue Jun 21 15:41:05 2016
--------------------------------------------------------------------------------


CareerItem =  {
    uiName     = "CareerItem",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------

    -----------------------
    -- Parameters Module --
    -----------------------
    complete,
    config,
    chapter,
    section,
    stars,

}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function CareerItem:Awake()
    self:UiParse()				-- Foucs on UI Parse.
    self.stars = {}
    for i = 1, 3 do
        self.stars[i] = self.transform:FindChild("Stars/Star"..i):GetComponent("UISprite")
    end
end


function CareerItem:Start()
    self.oriColor = self.uiIcon.color
    -- self.uiStars.gameObject:SetActive(false)
    self.transform:GetComponent("BoxCollider").enabled = false
    self.transform:FindChild("Frame/Icon"):GetComponent("BoxCollider").enabled = false

    local section = MainPlayer.Instance:GetSection(self.chapter, self.section)
    local isLocked = not MainPlayer.Instance:CheckSection(self.chapter, self.section)

    if not isLocked then
        for i = 1, 3 do
            if i <= section.medal_rank then
                self.stars[i].spriteName = "match_qualifying_10"
            else
                self.stars[i].spriteName = "match_qualifying_13"
            end
        end
    else
        for i = 1, 3 do
            self.stars[i].spriteName = "match_qualifying_13"
        end
    end

    self:Refresh()
end

function CareerItem:Refresh()
    self.uiIcon.spriteName = self.config.icon
    self:Complete(self.complete)
end

-- uncommoent if needed
-- function CareerItem:FixedUpdate()

-- end


function CareerItem:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


--------------------------------------------------------------------------------
-- Function Name : Complete
-- Create Time   : Tue Jun 21 15:46:28 2016
-- Input Value   : boolean
-- Return Value  : nil
-- Description   : true - player finish section, otherwise not.
--------------------------------------------------------------------------------
function CareerItem:Complete(finish)
    local r = finish and self.oriColor.r or 0
    local g = self.oriColor.g
    local b = self.oriColor.b
    local a = self.oriColor.a

    print("1927 - <CareerItem>  finish, r=",finish, r)
    self.uiIcon.color = Color.New(r, g, b, a)
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
function CareerItem:UiParse()
    self.uiIcon = self.transform:FindChild("Frame/Icon"):GetComponent("UISprite")
    self.uiStars = self.transform:FindChild("Stars")
end

return CareerItem

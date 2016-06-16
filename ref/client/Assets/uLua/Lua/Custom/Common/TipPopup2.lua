--------------------------------------------------------------------------------
-- class name    : TipPopup2
-- create time   : Wed May 11 14:37:06 2016
--------------------------------------------------------------------------------


TipPopup2 =  {
    uiName     = "TipPopup2",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------

    -----------------------
    -- Parameters Module --
    -----------------------
    title,                      -- 标题.
    content,                    -- 内容
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function TipPopup2:Awake()
    self:UiParse()				-- Foucs on UI Parse.
end


function TipPopup2:Start()

    self:Refresh()
end

function TipPopup2:Refresh()
    if self.title then
        self.uiTitle.text = self.title
    end

    if self.content then
        self.uiContent.text = self.content
    end

    addOnClick(self.gameObject,self:OnClick())
end

-- uncommoent if needed
-- function TipPopup2:FixedUpdate()

-- end


function TipPopup2:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------





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
function TipPopup2:UiParse()
    self.uiTitle = self.transform:FindChild("LabelName"):GetComponent("UILabel")
    self.uiContent = self.transform:FindChild("LabelTip"):GetComponent("UILabel")
end

function TipPopup2:OnClick()
    return function ()
        NGUITools.Destroy(self.gameObject)
    end
end

return TipPopup2

UIPause = {
    uiName = "UIPause",
    -- event
    outClicked = nil,
    continueClicked = nil,
    isOut = false,

    uiOutBtn,
    uiContinueBtn
}

function UIPause:Awake()
    self.uiOutBtn = self.transform:Find("Window/goout_btn"):GetComponent("UIButton")
    self.uiContinueBtn = self.transform:Find("Window/continue_btn"):GetComponent("UIButton")
end

function UIPause:Start()
    addOnClick(self.uiOutBtn.gameObject, self:OnOutClick())
    addOnClick(self.uiContinueBtn.gameObject, self:OnContinueClick())
end

-- out click event
function UIPause:OnOutClick()
    return function(btn_go)
        self.isOut = true

        self:OnClose()
    end
end

-- continue click event
function UIPause:OnContinueClick()
    return function(btn_go)
        self.isOut = false

        self:OnClose()
    end
end

-- close event
function UIPause:OnClose()
    -- c# invoke
    print (self.isOut)

    if self.isOut and self.outClicked then
        self.outClicked:DynamicInvoke(self.uiOutBtn.gameObject)
    end

    if not self.isOut and self.continueClicked then
        self.continueClicked:DynamicInvoke(self.uiContinueBtn.gameObject)
    end

    NGUITools.Destroy(self.gameObject)
end

return UIPause
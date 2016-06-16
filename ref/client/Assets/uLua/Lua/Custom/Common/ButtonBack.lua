ButtonBack = {
    uiName = 'ButtonBack',

    ----------UI
    uibackIcon,

    ----------paremeters
    onClick = nil,
    delay = 0,
}

function ButtonBack:Awake()
    self.uibackIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
    UIEventListener.Get(self.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClick())
end

function ButtonBack:Start()
    --body
end

function ButtonBack:OnClick()
    return function ()
        if self.delay ~= 0 then
            Scheduler.Instance:AddTimer(self.delay, false, self:Delay())
            return
        end

    if self.onClick then self.onClick() end
    end
end

function ButtonBack:Delay()
    return function()
        self.delay = 0
        self:OnClick()()
    end
end


function ButtonBack:SetBackIcon(Icon,alpha)
    self.uibackIcon.alpha = alpha
    self.uibackIcon.spriteName = Icon
end

return ButtonBack

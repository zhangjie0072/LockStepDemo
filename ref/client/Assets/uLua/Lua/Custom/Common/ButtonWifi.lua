ButtonWifi = {
    uiName = 'ButtonWifi',

    ----------UI
    uiIcon,

    ----------paremeters
    onClick = nil,
    lastPing = 0,
}

function ButtonWifi:Awake()
    self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
    self.uiMs = self.transform:FindChild('Num'):GetComponent('UILabel')
    UIEventListener.Get(self.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClick())
end

function ButtonWifi:Start()
    --body
    self.uiMs.color = Color.New(1,1,1.0,1)
end


function ButtonWifi:FixedUpdate()
    if os.time() - self.lastPing > 2.0 then
        self.lastPing = os.time()

        local latency = 0
        local platConn = GameSystem.Instance.mNetworkManager.m_platConn
        if platConn and platConn.m_profiler then
            latency = platConn.m_profiler.m_avgLatency * 1000
        end
        
        self.uiMs.text = tostring((latency - latency % 1).."ms")

        local green = tonumber(GameSystem.Instance.CommonConfig:GetString("gNetStateGreen"))
        local yellow = tonumber(GameSystem.Instance.CommonConfig:GetString("gNetStateYellow"))

        if latency < green then -- green
            self.uiIcon.spriteName = "hall2_03"
            self.uiMs.color = Color.New(0,1,0,1)
        elseif latency < yellow then -- yellow
            self.uiIcon.spriteName = "hall2_02"
            self.uiMs.color = Color.New(1, 0.9215686, 0.01568628, 1)
        else -- red
            self.uiIcon.spriteName = "hall2_01"
            self.uiMs.color = Color.New(1,0,0,1)
        end
    end
end

function ButtonWifi:OnClick()
    return function ()
    if self.onClick then self.onClick() end
    end
end

return ButtonWifi

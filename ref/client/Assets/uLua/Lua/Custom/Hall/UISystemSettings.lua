--encoding=utf-8
--系统设置窗口

UISystemSettings =
{
    uiName = 'UISystemSettings',
    --------------------
    btnClose,
    btnMusic,
    btnSound,
    btnCenter,
    btnComment,
    btnContactUs,
    -------------------
    MusicLua,
    SoundLua,

    animator,

    goClose,
    goMusic,
    goSound,
}

------------------------
function UISystemSettings:Awake()
    self.btnClose = self.transform:FindChild('Window/ButtonClose')
    self.btnMusic = self.transform:FindChild('Window/Middle/MusicButton')
    self.btnSound = self.transform:FindChild('Window/Middle/SoundButton')
    self.btnCenter = self.transform:FindChild('Window/Middle/CenterButton')
    self.btnComment = self.transform:FindChild('Window/Middle/CommentButton')
    self.btnContactUs = self.transform:FindChild('Window/Middle/ContactUsButton')

    self.goClose = createUI('ButtonClose', self.btnClose)
    self.goMusic = createUI('SwitchButton', self.btnMusic)
    self.goSound = createUI('SwitchButton', self.btnSound)

    self.MusicLua = getLuaComponent(self.goMusic)
    self.SoundLua = getLuaComponent(self.goSound)
    self.animator = self.transform:GetComponent('Animator')

    self.MusicLua.onValueChanged = self:MusicButtonChanged()
    self.SoundLua.onValueChanged = self:SoundButtonChanged()
end

function UISystemSettings:Start()
    getLuaComponent(self.goClose).onClick = self:ClickClose()

    if MainPlayer.Instance.CanGoCenter then
        addOnClick(self.btnCenter.gameObject, self:ClickCenter())
    end
    self.btnCenter.gameObject:SetActive(MainPlayer.Instance.CanGoCenter)

    addOnClick(self.btnContactUs.gameObject, self:ClickContactUs())

    --初始化音乐音效开关状态
    self.MusicLua:SetState(not AudioSettings.IsMusicMute())
    self.SoundLua:SetState(not AudioSettings.IsSoundMute())

    self.MusicLua:SetLabel(getCommonStr("STR_MUSIC"))
    self.SoundLua:SetLabel(getCommonStr("STR_SOUND"))
end

function UISystemSettings:Refresh()
    -- code here
end

function UISystemSettings:OnDestroy()
    Object.Destroy(self.gameObject)
end

--关闭窗口按钮
function UISystemSettings:OnClose()
    self:OnDestroy()
end

function UISystemSettings:DoClose( ... )
    -- if self.animator then
    --     self:AnimClose()
    -- else
        self:OnClose()
    -- end
end

-------------------------------------------------------
--音乐开关状态回调
function UISystemSettings:MusicButtonChanged()
    return function (ismute)
        AudioSettings.MuteMusic(not ismute)
    end
end

--音效开关状态回调
function UISystemSettings:SoundButtonChanged()
    return function (ismute)
        AudioSettings.MuteSound(not ismute)
    end
end

--个人中心
function UISystemSettings:ClickCenter()
    return function()
        MainPlayer.Instance:OpenPlayerPlat()
    end
end

--联系我们
function UISystemSettings:ClickContactUs()
    return function()
        if not MainPlayer.Instance:EnterServiceQuestion() then
            local go = createUI("UIContactUs")
            UIManager.Instance:BringPanelForward(go)
        end
    end
end

--点击关闭按钮
function UISystemSettings:ClickClose( ... )
    return function (go)
        self:DoClose()
    end
end

return UISystemSettings

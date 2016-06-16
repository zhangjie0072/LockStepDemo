

FriendsPlayerLog =  {
	uiName	= "FriendsPlayerLog",

    uiMatchNum,
    uiWinPercent,
    uiRoleIcon,

    sprBG,
}

function FriendsPlayerLog:Awake()
    self.uiMatchNum = getComponentInChild(self.transform, "Bg/MatchNum", "UILabel")
    self.uiWinPrecent = getComponentInChild(self.transform, "Bg/WinPercent", "UILabel")
    self.uiRoleIcon = getComponentInChild(self.transform, "Icon", "UISprite")
    self.sprBG = getComponentInChild(self.transform, "BG", "UISprite")
end

function FriendsPlayerLog:Start()
	
end

function FriendsPlayerLog:Update()
	
end

function FriendsPlayerLog:FixedUpdate()
	
end

function FriendsPlayerLog:OnDestroy()
	
end

function FriendsPlayerLog:OnClose()
	
end

function FriendsPlayerLog:setData(roleMatchData)
    self.uiMatchNum.text = string.format(getCommonStr("STR_FRIENDS_NUM_SECOND1"),roleMatchData.race_times)

    if roleMatchData.race_times ~= 0 then
        local per = math.ceil( (roleMatchData.win_times / roleMatchData.race_times) * 100 )
        self.uiWinPrecent.text = string.format(getCommonStr("STR_FRIENDS_NUM_WINPER"), per)
    else
        self.uiWinPrecent.text = string.format(getCommonStr("STR_FRIENDS_NUM_WINPER"), 0)
    end

    local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleMatchData.role_id)
    if roleConfig then
        self.uiRoleIcon.spriteName = roleConfig.icon_bust
        self.sprBG.spriteName = roleConfig.icon_bg
    end
end

return FriendsPlayerLog
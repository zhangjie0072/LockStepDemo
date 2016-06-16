

FriendsModelLog =  {
	uiName	= "FriendsModelLog",

    --uiBustIcon,
    uiLabTitle,
    uiLabNum,
    uiLabWinPer,
    uiBgIcon,
}

function FriendsModelLog:Awake()
    --self.uiBustIcon = getComponentInChild(self.transform, "BustIcon", "UISprite")
    self.uiLabTitle = getComponentInChild(self.transform, "LabTitle", "UILabel")
    self.uiLabNum = getComponentInChild(self.transform, "LabNum", "UILabel")
    self.uiLabWinPer = getComponentInChild(self.transform, "LabWinPer", "UILabel")
    self.uiBgIcon = getComponentInChild(self.transform, "BustIcon", "UISprite")
end

function FriendsModelLog:Start()
	
end

function FriendsModelLog:Update()
	
end

function FriendsModelLog:FixedUpdate()
	
end

function FriendsModelLog:OnDestroy()
	
end

function FriendsModelLog:OnClose()
	
end

function FriendsModelLog:setRegularData(reg)
    self.uiLabNum.text = string.format(getCommonStr("STR_FRIENDS_NUM_SECOND1"), reg.race_times)
    if reg.race_times == 0 then
        self.uiLabWinPer.text = ""
    else
        local per = math.ceil( (reg.win_times / reg.race_times) * 100 )
        self.uiLabWinPer.text = string.format(getCommonStr("STR_FRIENDS_NUM_WINPER"), per)
    end
    self.uiLabTitle.text = getCommonStr("STR_REGULAR_MATCH")

    self.uiBgIcon.spriteName = "tencent_routine"
end

function FriendsModelLog:setRankingData(rank)
    self.uiLabNum.text = string.format(getCommonStr("STR_FRIENDS_NUM_SECOND1"), rank.total_race_times)
    if rank.total_race_times == 0 then
        self.uiLabWinPer.text = ""
    else
        local per = math.ceil( (rank.total_win_times / rank.total_race_times) * 100 )
        self.uiLabWinPer.text = string.format(getCommonStr("STR_FRIENDS_NUM_WINPER"), per)
    end
    self.uiLabTitle.text = getCommonStr("STR_LABBER")

    self.uiBgIcon.spriteName = "tencent_qualifying"
end

function FriendsModelLog:setLadderData(ladder)
    self.uiLabNum.text = string.format(getCommonStr("STR_FRIENDS_NUM_SECOND1"), ladder.total_race_times)
    if ladder.total_race_times == 0 then
        self.uiLabWinPer.text = ""
    else
        local per = math.ceil( (ladder.total_win_times / ladder.total_race_times) * 100 )
        self.uiLabWinPer.text = string.format(getCommonStr("STR_FRIENDS_NUM_WINPER"), per)
    end
    self.uiLabTitle.text = getCommonStr("STR_LADDER_MATCH")

    self.uiBgIcon.spriteName = "tencent_ladder"
end

return FriendsModelLog
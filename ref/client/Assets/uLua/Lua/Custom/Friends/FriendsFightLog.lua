

FriendsFightLog =  {
	uiName	= "FriendsFightLog",

    uiLabTitle,
    uiLabNum,
    uiIcon,
    uiIconMask,
}

function FriendsFightLog:Awake()
    self.uiLabTitle = getComponentInChild(self.transform, "LabTitle", "UILabel")
    self.uiLabNum = getComponentInChild(self.transform, "LabNum", "UILabel")

    self.uiIcon = self.transform:GetComponent("UISprite")
    self.uiIconMask = getComponentInChild(self.transform, "Icon", "UISprite")
end

function FriendsFightLog:setData(data)
    self.uiLabTitle.text = data.str
    self.uiLabNum.text = string.format(getCommonStr("STR_FRIENDS_NUM_SECOND"), data.num)

    self.uiIcon.spriteName = data.icon
    self.uiIconMask.spriteName = data.icon
   	NGUITools.SetActive(self.uiIconMask.gameObject, data.num == 0)
end

function FriendsFightLog:Start()
	
end

function FriendsFightLog:Update()
	
end

function FriendsFightLog:FixedUpdate()
	
end

function FriendsFightLog:OnDestroy()
	
end

function FriendsFightLog:OnClose()
	
end



return FriendsFightLog
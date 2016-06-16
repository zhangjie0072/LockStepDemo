FriendsGiftItem =  {
	uiName	= "FriendsGiftItem",

    friendData,
	headIconScript,

	uiHeadIcon,
    uiLabName,
    uiBtnGold,
}

function FriendsGiftItem:Awake()
    self.uiHeadIcon = getChildGameObject(self.transform, "Icon")
    self.uiLabName = getComponentInChild(self.transform, "Name", "UILabel")
    self.uiBtnGold = getComponentInChild(self.transform, "Gold", "UIButton")
end

function FriendsGiftItem:Start()
	addOnClick(self.uiBtnGold.gameObject, self:OnGetGold())
end

function FriendsGiftItem:Update()

end

function FriendsGiftItem:FixedUpdate()

end

function FriendsGiftItem:OnDestroy()

end

function FriendsGiftItem:OnClose()

end

function FriendsGiftItem:setData(msg)
    self.friendData = msg

    self.uiLabName.text = msg.name

	--icon
	if not self.headIconScript then
		self.headIconScript = getLuaComponent(createUI("CareerRoleIcon", self.uiHeadIcon.transform))
		self.headIconScript.id = tonumber(self.friendData.icon)
		self.headIconScript.showPosition = false
	else
		self.headIconScript.id = tonumber(self.friendData.icon)
		self.headIconScript:Refresh()
	end

    --self.uiHeadIcon.spriteName = "icon_portrait_"..msg.icon
end

function FriendsGiftItem:OnGetGold()
	return function ()
        if not FunctionSwitchData.CheckSwith(FSID.friends_gift) then return end

        local gFriendGetGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendGetGiftTimes")
        if FriendData.Instance.get_gift_times >= gFriendGetGiftTimes then
            CommonFunction.ShowTip(getCommonStr("STR_FRIENDS_NOT_GET"), nil)
            return
        end

		local req = {
				type = 'FOT_GETAWARDS',
				all_flag = 0, --一键赠送/领取:1   否则:0
                op_friend = {
                    acc_id = self.friendData.acc_id,
                    plat_id = self.friendData.plat_id,
                },
		}

	    local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
	end
end

return FriendsGiftItem
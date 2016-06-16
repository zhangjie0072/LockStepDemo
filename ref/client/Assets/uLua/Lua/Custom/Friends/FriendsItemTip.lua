
FriendsItemTip =  {
	uiName	= "FriendsItemTip",

    platId,
	playerId,
	playerName,
	headIconScript,

	uiItemTipsGameObject,

	uiMaskGameObject,
	uiPlayerName,
	uiHeadIcon,
	uiBtnAdd,
	uiBtnInfo,
	uiBtnBlack,
	uiBtnChat,
}

function FriendsItemTip:Awake()
	
	self.uiMaskGameObject = getChildGameObject(self.transform, "Mask")

	self.uiPlayerName = getComponentInChild(self.transform, "PlayerName", "UILabel")
	self.uiHeadIcon = getChildGameObject(self.transform, "PlayerHead")

	self.uiBtnAdd = getComponentInChild(self.transform, "ButtonFriends", "UIButton")
	self.uiBtnInfo = getComponentInChild(self.transform, "ButtonData", "UIButton")
	self.uiBtnBlack = getComponentInChild(self.transform, "ButtonBlackList", "UIButton")
	self.uiBtnChat = getComponentInChild(self.transform, "ButtonPM", "UIButton")
end

function FriendsItemTip:Start()
	addOnClick(self.uiMaskGameObject, self:OnClickMask())
	addOnClick(self.uiBtnAdd.gameObject, self:OnAddFriend())
	addOnClick(self.uiBtnInfo.gameObject, self:OnFindInfo())
	addOnClick(self.uiBtnBlack.gameObject, self:OnAddBlack())
	addOnClick(self.uiBtnChat.gameObject, self:OnChat())
end

function FriendsItemTip:Update()
	
end

function FriendsItemTip:FixedUpdate()
	
end

function FriendsItemTip:OnDestroy()
	
end

function FriendsItemTip:OnClose()
	Object.Destroy(self.gameObject)
	Friends.friendItemTip = nil
end

------------------------------------------------------
function FriendsItemTip:setData( acc_id, name, icon, plat_id )
	self.uiPlayerName.text = name

    self.platId = plat_id
	self.playerId = acc_id
	self.playerName = name

    if not self.headIconScript then
		self.headIconScript = getLuaComponent(createUI("CareerRoleIcon", self.uiHeadIcon.transform))
		self.headIconScript.id = tonumber(icon)
		self.headIconScript.showPosition = false
	else
		self.headIconScript.id = tonumber(icon)
		self.headIconScript:Refresh()
	end

    local isFri = FriendData.Instance:IsFriend(acc_id)
    self.uiBtnAdd.isEnabled = (isFri==false)
    self.uiBtnBlack.isEnabled = (isFri==false)
end

function FriendsItemTip:SendMessage(op_type)
	local req = {
				type = op_type,
				op_friend = { acc_id = self.playerId },
			}

	local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
end

--添加好友
function FriendsItemTip:OnAddFriend()
	return function()
		if self.platId == MainPlayer.Instance.AccountID then
			CommonFunction.ShowTip(getCommonStr("STR_FRIENDS_NOT_ADDSELF"), nil)
			do return end
		end
		
		self:SendMessage('FOT_ADD')
		self:OnClose()
	end
end

--查询信息
function FriendsItemTip:OnFindInfo()
	return function()
		self:ReqQueryFriendInfo()
	end
end

--加入黑名单
function FriendsItemTip:OnAddBlack()
	return function()
		self:SendMessage('FOT_BLACK')
        self:OnClose()
	end
end

--私聊
function FriendsItemTip:OnChat()
	return function()
		print('私聊')
	end
end

--面板以外的蒙板区域
function FriendsItemTip:OnClickMask()
	return function()
		self:OnClose()
	end
end
function FriendsItemTip:DoClose( ... )
	-- body
	return function ( ... )
		-- body
		self:OnClose()
	end
end
--发送好友查询信息
function FriendsItemTip:ReqQueryFriendInfo()
    print(self.playerId)
      --打开好友信息界面
      	local friendsInfo = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("FriendsInfo(Clone)")
        if friendsInfo then 
        	NGUITools.Destroy(friendsInfo.gameObject)
        end
        local friInfo = createUI("FriendsInfo")
        local friLua = getLuaComponent(friInfo)
        friLua:Query(self.playerId,self.platId)
        friLua.friendInfoGotCallback = self:DoClose()

 --    local req = {
 --        friend = { 
 --            acc_id = self.playerId, 
 --            plat_id = self.platId, },
 --    }

 --    local buf = protobuf.encode("fogs.proto.msg.QueryFriendInfoReq", req)
	-- LuaHelper.SendPlatMsgFromLua(MsgID.QueryFriendInfoReqID, buf)
	-- CommonFunction.ShowWait()
 --    LuaHelper.RegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self:QueryFriendInfoHandler(), self.uiName)
end

--接收好友查询信息结果
-- function FriendsItemTip:QueryFriendInfoHandler()
-- 	return function(buf)
--         print("<<<<<<FriendsItemTip:QueryFriendInfoHandler")

--         LuaHelper.UnRegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self.uiName)
--         CommonFunction.StopWait()
--         local resp, err = protobuf.decode("fogs.proto.msg.QueryFriendInfoResp", buf)
--         if not resp then
--             error("", err)
--             do return end
--         end

--         if resp.result ~= 0 then
--             do return end 
--         end

--         --打开好友信息界面
--         local friInfo = createUI("FriendsInfo")
--         local friLua = getLuaComponent(friInfo)
--         friLua:setData(resp)

--         UIManager.Instance:BringPanelForward(friInfo)

--         self:OnClose()
--     end
-- end

return FriendsItemTip
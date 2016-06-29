
require "Custom/Chat/ChatCache"
UIChat =
{
	uiName = 'UIChat',
	-----------------------UI
	uiSmallTip,
	uiForm,
	uiBtnSetUp,
	uiBtnOpenArrow,
	uiSetUp,
	uiToggles = {},
	uiBtnCloseArrow,

	uiScrollView,
	uiProgressBar,
	uiMessageGrid,
	uiInput,
	uiSendScrollPanel,
	uiSendLabel,
	uiSendProcessBar,
	uiBtnSend,
	uiLockAnimator,
	uiBtnLock,
	uiUnReadMask,
	uiBackMask,
	uiUnReadTip,
	uiFriendGrid,

	uiOneChatMsgGrid,
	uiExpression,
	uiVoice,
	uiAnimator,
	uiMask,
	uiBox,
	uiChanelBtn = {},
	uiChannelFriendNum,
	uiChannelFriendFlag,
	uilblChatToFriend,
	ScrollViewAsyncLoadItem,
	-------------------------
	preMessage,
	chooseTab,
	locked = false,
	unReadNum = 0,
	unReadMessage = {},
	MAXNUM = 50,
	OFFSET = 10,
	MAXHEIGHT = 1725,
	MAXWORDS = 550,
	initPosX,
	initWidth,
	resetPosition = false,
	refreshTime = 0.1,
	gridType = 0,	--0显示聊天界面，1显示好友界面
	chatChannel,
	preChatChannel,--更新前的上一个聊天频道
	currentChatFriend = {name,id,},	--当前聊天好友信息
	preChatFriendId,	--上一个聊天好友ID
	friendListDirty = false,
}

typeName =
{
	getCommonStr("WORLD_CHAT"),
	getCommonStr("SYSTEM_CHAT"),
	getCommonStr("LEAGUE_CHAT"),
	getCommonStr("TEAM_CHAT"),
	nil,
	nil,
	getCommonStr("密语"),
}

messageColors =
{
	-- '[EE9A49]%s[-]',
	-- '[FF8C69]%s[-]',
	-- '[1874CD]%s[-]',
	-- '[66CD00]%s[-]',
	Color.New(250/255, 151/255, 45/255, 1),
	Color.New(250/255, 100/255, 36/255, 1),
	Color.New(39/255, 128/255, 208/255, 1),
	Color.New(103/255, 215/255, 78/255, 1),
	nil,
	nil,
	Color.New(1, 0, 236/255, 1),
	Color.New(1, 1, 1, 1),	--除了世界频道之外其他频道自己聊天消息颜色
}
function UIChat:Awake()
	self.uiSmallTip = self.transform:FindChild('SmallTip')
	self.uiForm = self.transform:FindChild('Form')

	self.uiBtnSetUp = self.transform:FindChild('SmallTip/ButtonSetUp')
	self.uiBtnOpenArrow = self.transform:FindChild('SmallTip/ButtonArrow')
	self.uiBox = self.transform:FindChild("SmallTip/BP")
	self.uiSetUp = self.transform:FindChild('SmallTip/SetUp')
	self.uiOneChatMsgGrid = self.transform:FindChild('SmallTip/Scroll/Grid'):GetComponent('UIGrid')
	self.uiToggles[1] = self.transform:FindChild('SmallTip/SetUp/ButtonWorld/HookFrame'):GetComponent('UIToggle')
	self.uiToggles[2] = self.transform:FindChild('SmallTip/SetUp/ButtonSystem/HookFrame'):GetComponent('UIToggle')
	self.uiToggles[3] = self.transform:FindChild('SmallTip/SetUp/ButtonAlliance/HookFrame'):GetComponent('UIToggle')
	self.uiToggles[4] = self.transform:FindChild('SmallTip/SetUp/ButtonRanks/HookFrame'):GetComponent('UIToggle')

	self.uiChanelBtn.system = self.transform:FindChild('Form/SwitchingChannel/ButtonSystem')
	self.uiChanelBtn.system.gameObject.name  = "system"
	self.uiChanelBtn.Party = self.transform:FindChild('Form/SwitchingChannel/ButtonRanks')
	self.uiChanelBtn.Party.gameObject.name  = "Party"
	self.uiChanelBtn.Alliance = self.transform:FindChild('Form/SwitchingChannel/ButtonAlliance')
	self.uiChanelBtn.Alliance.gameObject.name  = "Alliance"
	self.uiChanelBtn.friends = self.transform:FindChild('Form/SwitchingChannel/ButtonFriends')
	self.uiChannelFriendFlag = self.transform:FindChild('Form/SwitchingChannel/ButtonFriends/RedDot')
	self.uiChannelFriendNum = self.transform:FindChild('Form/SwitchingChannel/ButtonFriends/RedDot/Num'):GetComponent("UILabel")
	self.uiChanelBtn.friends.gameObject.name  = "friends"
	self.uiChanelBtn.World = self.transform:FindChild('Form/SwitchingChannel/ButtonWorld')
	self.uiChanelBtn.World.gameObject.name  = "World"
	self.uilblChatToFriend = self.transform:FindChild('Form/PrivateChatTitle'):GetComponent("UILabel")

	self.uiProgressBar = self.transform:FindChild('Form/ProgressBar'):GetComponent('UIScrollBar')
	self.uiScrollView = self.transform:FindChild('Form/Scroll'):GetComponent('UIScrollView')
	self.uiMessageGrid = self.transform:FindChild('Form/Scroll/Grid')--:GetComponent('UIGrid')
	self.uiFriendGrid = self.transform:FindChild('Form/Scroll/FriendGrid'):GetComponent('UIGrid')
	self.uiInput = self.transform:FindChild('Form/LabelScroll/InputAccount'):GetComponent('UIInput')
	self.uiSendScrollPanel = self.transform:FindChild('Form/LabelScroll'):GetComponent('UIPanel')
	self.uiSendLabel = self.transform:FindChild('Form/LabelScroll/InputAccount/LabelNaming'):GetComponent('UILabel')
	self.uiSendProcessBar = self.transform:FindChild('Form/LabelProgressBar'):GetComponent('UIScrollBar')
	self.uiBtnSend = self.transform:FindChild("Form/ButtonOK"):GetComponent('UIButton')
	self.uiLockAnimator = self.transform:FindChild('Form/BgFrame'):GetComponent('Animator')
	self.uiBtnLock = self.transform:FindChild('Form/BgFrame/ButtonLock')
	self.uiBtnCloseArrow = self.transform:FindChild('Form/ButtonArrow')
	self.uiUnReadMask = self.transform:FindChild('Form/UnreadMask')
	self.uiBackMask = self.transform:FindChild('Form/UnreadMask/BackMask')
	self.uiUnReadTip = self.transform:FindChild('Form/UnreadMask/Text'):GetComponent('UILabel')
	self.uiVoice = self.transform:FindChild('Form/SwitchingChannel/ButtonVoice')
	self.uiExpression = self.transform:FindChild('Form/ButtonFace')
	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiMask = self.transform:FindChild('Form/Mask')
	LuaHelper.RegisterPlatMsgHandler(MsgID.ChatRespID, self:OnChatRespHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.QueryPlayerInfoRespID, self:OnQueryPlayerInfoHandler(), self.uiName)
end

function UIChat:Start( ... )
	EventDelegate.Add(self.uiInput.onChange, LuaHelper.Callback(self:OnMessageChange()))
	MainPlayer.Instance.onNewChatMessage = self:UpdateChatBar(ChatChannelType.CCT_WORLD)

	self.uilblChatToFriend.text = ''
	NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)



	self.uiScrollView.onDragFinished = self:OnDragMessage()

	for i,v in ipairs(self.uiToggles) do
		addOnClick(self.uiToggles[i].gameObject, self:ChooseChannel(1))
	end

	for i,v in pairs(self.uiChanelBtn) do
		addOnClick(self.uiChanelBtn[i].gameObject, self:ChooseChannel(0))
	end
	addOnClick(self.uiBtnSend.gameObject, self:OnSendClick())
	addOnClick(self.uiBtnLock.gameObject, self:OnLockScreen())
	addOnClick(self.uiBox.gameObject, self:OpenChatFrame())
	addOnClick(self.uiBtnCloseArrow.gameObject, self:OpenChatFrame())
	addOnClick(self.uiMask.gameObject, self:OpenChatFrame())
	addOnClick(self.uiBtnSetUp.gameObject, self:OpenSetUp())
	addOnClick(self.uiBackMask.gameObject, self:OnLockScreen())
	addOnClick(self.uiVoice.gameObject, self:OnClickVoice())
	addOnClick(self.uiExpression.gameObject, self:OnClickExpression())

	NGUITools.SetActive(self.uiUnReadMask.gameObject, false)
	self:InitOneChatInfo()
	-- self:InitChatRoomInfo()
	self.uiFriendGrid.onCustomSort = function (x, y)
	    --[[
			有未查看密语信息的排列在前
			有过交流历史的排列在前
			没有密语信息时，友好度由高到低排列
	    ]]
	    -- warning('self.uiFriendGrid.onCustomSort ')
	    -- self.uiFriendGrid:ConstrainWithinPanel()
	    if not x then return 1 end
	    if not y then return -1 end
		local item1 = getLuaComponent(x)
		local item2 = getLuaComponent(y)
		local data1 = item1.data
		local data2 = item2.data
	    -- warning('self.uiFriendGrid.onCustomSort data1 ',data1.num,data1.id,',data2 ',data2.num,data2.id)
		if data1.num ~= data2.num then return (data1.num > data2.num and -1 or 1 )end
		local count1 = ChatCache.GetFriendMsgCountById(data1.id)
		local count2 = ChatCache.GetFriendMsgCountById(data2.id)
	    -- warning('self.uiFriendGrid.onCustomSort count1 ',count1,',count2 ',count2)
		if count1 ~= count2 then return (count1 > count2 and -1 or 1) end
		return (data1.id < data2.id and -1 or 1)
	end
	self.initPosX = self.uiSendLabel.transform.localPosition.x
	self.chatChannel = ChatChannelType.CCT_WORLD
	self.preChatChannel = self.chatChannel
	self:UpdateGridType()

	--测试
	ChatCache.uichat = self
end

function UIChat:FixedUpdate( ... )
	local labelWidth = self.uiSendLabel.width
	local limitWidth = self.uiSendScrollPanel.width
	-- print('limitWidth >= labelWidth ?' , limitWidth >= labelWidth)
	if limitWidth > labelWidth then
		local labelPos = self.uiSendLabel.transform.localPosition
		labelPos.x = self.initPosX
		self.uiSendLabel.transform.localPosition = labelPos
	else
		if self.initWidth == labelWidth then
			return
		end
		local posX = self.initPosX
		posX = posX - (labelWidth - limitWidth) - self.OFFSET
		local labelPos = self.uiSendLabel.transform.localPosition
		labelPos.x = posX
		self.uiSendLabel.transform.localPosition = labelPos
		self.initWidth = labelWidth
	end

	if self.resetPosition then
		self.refreshTime = self.refreshTime - UnityTime.fixedDeltaTime
	end
	if self.refreshTime <= 0 then
		self.resetPosition = false
		self.refreshTime = 0.1
		self.uiProgressBar.value = 0.95
	end
end

function UIChat:OnDestroy( ... )
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ChatRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.QueryPlayerInfoRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIChat:Refresh( ... )
end

function UIChat:OnClose( ... )
	-- NGUITools.Destroy(self.gameObject)
end

--------------------------------------

function UIChat:DoClose( ... )
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end
--根据聊天频道来重置聊天界面
function UIChat:ResetChannelContent( ... )
	-- body
	warning('UIChat:ResetChannelContent pre ',self.preChatChannel,',cur ',self.chatChannel)
	if self.preChatChannel ~= self.chatChannel then
		self.preChatChannel = self.chatChannel
		--聊天频道不一样，清空聊天消息		
    	CommonFunction.ClearChild(self.uiMessageGrid.transform)
    --好友聊天也要清消息
	elseif self.chatChannel == ChatChannelType.CCT_PRIVATE then
    	CommonFunction.ClearChild(self.uiMessageGrid.transform)
	end
end
function UIChat:UpdateFriendUnread( ... )
	-- body
	local unreadCount = ChatCache.getUnreadCount(1)
	NGUITools.SetActive(self.uiChannelFriendFlag.gameObject,unreadCount>0)
	NGUITools.SetActive(self.uiChannelFriendNum.gameObject,unreadCount>0)
	self.uiChannelFriendNum.text = ""..unreadCount

end
function UIChat:OpenChatFrame( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.chat) then return end

		local hall = UIManager.Instance.m_uiRootBasePanel.transform:FindChild('UIHall(Clone)')
		local hallLua = nil
		if hall then
			hallLua = getLuaComponent(hall.gameObject)
		end
		self.uilblChatToFriend.text = ''
		NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)
		--进入聊天默认显示世界频道
		self:ChooseChannel(0)(self.uiChanelBtn.World.gameObject)
		--根据大厅聊天栏消息来源设置进入聊天界面的频道
		self.chatChannel = ChatChannelType.CCT_WORLD
		self:ResetChannelContent()
		self:UpdateFriendUnread()
		if go == self.uiBox.gameObject then
			--open
			if hallLua then hallLua:SetModelActive(false) end
			NGUITools.SetActive(self.uiSmallTip.gameObject, false)
			NGUITools.SetActive(self.uiForm.gameObject, true)
			self:InitChatRoomInfo(ChatChannelType.CCT_WORLD)
			if not self.resetPosition then
				self.resetPosition = true
			end
			if self.uiAnimator then
				self.uiAnimator:SetBool('Switch', self.uiForm.gameObject.activeSelf)
			end
		elseif go == self.uiBtnCloseArrow.gameObject or go == self.uiMask.gameObject then
			--close
			if hallLua then hallLua:SetModelActive(true) end
			self:ResetChatRoom()
			NGUITools.SetActive(self.uiSmallTip.gameObject, true)
			NGUITools.SetActive(self.uiForm.gameObject, false)
			if self.uiSetUp.gameObject.activeSelf then
				self:OpenSetUp()()
			end
			if self.uiAnimator then
				self.uiAnimator:SetBool('Switch', self.uiForm.gameObject.activeSelf)
			end
		end
	end
end

function UIChat:OpenSetUp( ... )
	return function (go)
		NGUITools.SetActive(self.uiSetUp.gameObject, not self.uiSetUp.gameObject.activeSelf)
	end
end

function UIChat:UpdateChatBar( chatType )
	return function ()
		local hall = UIManager.Instance.m_uiRootBasePanel.transform:FindChild('UIHall(Clone)')
		if not hall then
			return
		end
		--更新好友未读消息

		if (chatType == ChatChannelType.CCT_WORLD or chatType == ChatChannelType.CCT_SYSTEM ) and (self.chatChannel == ChatChannelType.CCT_WORLD or self.chatChannel == ChatChannelType.CCT_SYSTEM)  then 
			local allCount = MainPlayer.Instance.WorldChatList.Count
			if allCount <= 0 then return end
			local lastMessage = MainPlayer.Instance.WorldChatList:get_Item(allCount - 1)
			if self.locked then
				NGUITools.SetActive(self.uiUnReadMask.gameObject, true)
				self.unReadNum = self.unReadNum + 1
				self.uiUnReadTip.text = string.format(getCommonStr("UNREAD_CHATMESSAGE"), self.unReadNum)
				-- self.unReadMessage[tostring(allCount)] = lastMessage
				table.insert(self.unReadMessage, lastMessage)
				-- return
			end
			if not self.locked then
				local chatItem = createUI("ChatAttrItem", self.uiMessageGrid.transform)
				local chatLua = getLuaComponent(chatItem)
				-- chatItem.gameObject.name = lastMessage.pos
				chatLua.inChatRoom = true
				chatLua:SetMessage(lastMessage)
				local num = GameUtils.GetStringLength(lastMessage.info.content)
				MainPlayer.Instance.ChatWordsNum:Add(num)
				self:SortAllMessage()
				self.uiProgressBar.value = 1
			end
		elseif chatType == ChatChannelType.CCT_LEAGUE then

		elseif chatType == ChatChannelType.CCT_TEAM then
		elseif chatType == ChatChannelType.CCT_PRIVATE then
			if self.chatChannel == ChatChannelType.CCT_PRIVATE then
				if self.currentChatFriend.id then
					if self.currentChatFriend.id > 0 then
						local messages = ChatCache.getFriendMsgById(self.currentChatFriend.id)
						if #messages > 0 then
							local lastMessage = messages[#messages]
							--需要判断新来的消息是不是对应好友的消息
							-- local childCount = self.uiMessageGrid.transform.childCount
							local isNewMessageForThisFriend = false
							-- if childCount > 0 then
							-- 	local child = self.uiMessageGrid.transform:GetChild(childCount-1).gameObject
							-- 	local childLua = getLuaComponent(child)
							-- 	if childLua.msgTime > lastMessage.time then 
							-- 		isNewMessageForThisFriend = true
							-- 	end
							-- end
							local  latestMsg  = ChatCache.getLatestFriendMsg()
							if latestMsg then
								if latestMsg.info.acc_id == lastMessage.info.acc_id then 
									isNewMessageForThisFriend = true
								end
							end
							if isNewMessageForThisFriend then
								if self.locked then
									NGUITools.SetActive(self.uiUnReadMask.gameObject, true)
									self.unReadNum = self.unReadNum + 1
									self.uiUnReadTip.text = string.format(getCommonStr("UNREAD_CHATMESSAGE"), self.unReadNum)
									table.insert(self.unReadMessage, lastMessage)
								else

									local chatItem = createUI("ChatAttrItem", self.uiMessageGrid.transform)
									local chatLua = getLuaComponent(chatItem)
									chatLua.inChatRoom = true
									chatLua:SetMessage(lastMessage)
									self:SortAllMessage()
									self.uiProgressBar.value = 1
								end
							end
						end
					end
				end
				
			end
			self:UpdateFriendUnread()--放在后面，防止消息收到了显示出来也会有未读消息
			
		end
		
		self:InitOneChatInfo()
	end
end

function UIChat:OnClickVoice( ... )
	return function (go)
		CommonFunction.ShowPopupMsg(getCommonStr('COME_SOON'),nil,nil,nil,nil,nil)
	end
end

function UIChat:OnClickExpression( ... )
	return function (go)
		CommonFunction.ShowPopupMsg(getCommonStr('COME_SOON'),nil,nil,nil,nil,nil)
	end
end

function UIChat:OnSendClick( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.chat_btn) then return end
		--好友界面不能发送信息
		if self.gridType == 1 then 
			return
		end
		local message = self.uiSendLabel.text
		--string.gsub(message, "^%s*(.-)%s*$", "%1") == string.trim
		if not message or string.gsub(message, "^%s*(.-)%s*$", "%1") == "" then
			CommonFunction.ShowPopupMsg(getCommonStr('CANT_EMPTY_MESSAGE'),nil,nil,nil,nil,nil)
			return
		end
		if self.chatChannel == ChatChannelType.CCT_PRIVATE and self.currentChatFriend.id then
			local friend = Friends.GetFriendById(self.currentChatFriend.id)
			if not friend then 
				CommonFunction.ShowPopupMsg(getCommonStr('CANT_TALK_TO_STRANGER'),nil,nil,nil,nil,nil)
				return
			elseif friend.online == 5 then
				CommonFunction.ShowPopupMsg(getCommonStr('FRIEND_IS_OFFLINE'),nil,nil,nil,nil,nil)
				return
			end
		end
		local req =
		{
			type = enumToInt(self.chatChannel),
			content = message,
			acc_id = self.currentChatFriend.id
		}

		local buffer = protobuf.encode("fogs.proto.msg.ChatReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.ChatReqID, buffer)

		self.preMessage = self.uiSendLabel.text
		self.uiSendLabel.text = ""
		self.uiInput.value = ""
	end
end

function UIChat:OnChatRespHandler( ... )
	return function (message)
		local resp = protobuf.decode("fogs.proto.msg.ChatResp", message)
		print('resp.result = ' , resp.result)
		--私聊信息有自己聊天信息
		if resp.result == 0 and resp.content and self.currentChatFriend.id then
			local msg = {type,pos,info={acc_id,content,ogri_name,},time,pos,}
			msg.type = ChatChannelType.CCT_PRIVATE
			msg.info.acc_id = self.currentChatFriend.id
			msg.info.ogri_name = self.currentChatFriend.name
			msg.info.content = resp.content
			msg.time = UnityTime.realtimeSinceStartup*10
			warning('OnChatRespHandler ..',resp.content)
			ChatCache.AddFriendMsg(msg,true)
		end
		local root = UIManager.Instance.m_uiRootBasePanel
		local mess = root.transform:FindChild('PopupMessage(Clone)')
		if not mess then
			if resp.result ~= 0 then
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
				self.uiSendLabel.text =	self.preMessage
				self.uiInput.value = self.preMessage
			end
		end
	end
end

function UIChat:OnQueryPlayerInfoHandler( ... )
	return function (message)
		print('QueryPlayerInfoResp ----------->>>>')
		local resp = protobuf.decode("fogs.proto.msg.QueryPlayerInfoResp", message)
		if resp then
			--show selected player info
			-- local chatDetail = createUI('UIChatDetail')
			-- local detailLua = getLuaComponent(chatDetail)
			-- detailLua:SetData(resp)
			-- UIManager.Instance:BringPanelForward(chatDetail.gameObject)
			if resp.id == MainPlayer.Instance.AccountID then
				print("===>>>click your self!!!")
				do return end
			end

			local uiItemTipsGameObject = createUI("FriendsItemTip")
			local tipsLua = getLuaComponent(uiItemTipsGameObject)
			tipsLua:setData(resp.id, resp.name, resp.icon, resp.plat_id)
			--UIManager.Instance.:BringPanelForward(uiItemTips.gameObject)
		end
	end
end

function UIChat:OnLockScreen( ... )
	return function (go)
		-- scroll bar value lock
		self.locked = not self.locked
		if self.uiLockAnimator then
			self.uiLockAnimator:SetBool('Switch', self.locked)
		end
		self:AfterLock()
	end
end

function UIChat:AfterLock( ... )
	if not self.locked then
		-- self.uiProgressBar.value = 1
		if self.unReadNum > 0 then
			self.unReadNum = 0
			-- self:InitChatRoomInfo()
			for k,v in pairs(self.unReadMessage) do
				local chatItem = createUI("ChatAttrItem", self.uiMessageGrid.transform)
				local chatLua = getLuaComponent(chatItem)
				-- chatItem.gameObject.name = v.pos
				chatLua.inChatRoom = true
				chatLua:SetMessage(v)
				if self.chatChannel == ChatChannelType.CCT_WORLD then
					local num = GameUtils.GetStringLength(v.info.content)
					MainPlayer.Instance.ChatWordsNum:Add(num)
				end
			end
			self.unReadMessage = {}
			self:SortAllMessage()
			NGUITools.SetActive(self.uiUnReadMask.gameObject, false)
		end
		self.uiProgressBar.value = 1
	end
end

--choose channel
function UIChat:ChooseChannel( mode )
	return function (go)
		-- print('UIChat ChooseChannel type',type,'target',go.name)
		if mode == 1 then 
			self.chooseTab = {1,0,0,0}	--default world, system, league, team
		else
			--改变了频道，聊天内容也会相应改变，如果是好友，则会回到好友界面
			if self.currentChatFriend.id then 
				self.preChatFriendId = self.currentChatFriend.id
			end
			if go.name == "friends" then 
				self.currentChatFriend.id = nil
		        self.currentChatFriend.name = nil
		        self.chatChannel = ChatChannelType.CCT_PRIVATE
		        self.gridType = 1
		        self:UpdateGridType()
		        self:ShowFriendList()		        
				self.uiSendLabel.text = ""
				self.uiInput.value = ""
				self.uiInput.enabled = false
				self.uilblChatToFriend.text = ''
				NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)
			elseif go.name == "Party"  then
		        self.currentChatFriend.id = nil
		        self.currentChatFriend.name = nil
		        self.chatChannel = ChatChannelType.CCT_TEAM
		        self:ResetChannelContent()
		        self.gridType = 0
		        self:UpdateGridType()    
				self.uiSendLabel.text = ""
				self.uiInput.value = ""
				self.uiInput.enabled = false
				self.uilblChatToFriend.text = ''
				NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)
				CommonFunction.ShowPopupMsg(getCommonStr('COME_SOON'),nil,nil,nil,nil,nil)
			elseif go.name == "Alliance"  then
		        self.currentChatFriend.id = nil
		        self.currentChatFriend.name = nil
		        self.chatChannel = ChatChannelType.CCT_LEAGUE
		         self:ResetChannelContent()
		        self.gridType = 0
		        self:UpdateGridType()
				self.uiSendLabel.text = ""
				self.uiInput.value = ""
				self.uiInput.enabled = false
				self.uilblChatToFriend.text = ''
				NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)
				CommonFunction.ShowPopupMsg(getCommonStr('COME_SOON'),nil,nil,nil,nil,nil)
			else--if go.name == "World" or go.name == "system"  then
		        self.chatChannel = ChatChannelType.CCT_WORLD
		         self:ResetChannelContent()
		        self.currentChatFriend.id = nil
		        self.currentChatFriend.name = nil
		        self.gridType = 0
		        self:UpdateGridType()
		        self:InitChatRoomInfo(self.chatChannel)
				self.uiSendLabel.text = ""
				self.uiInput.value = ""
				self.uiInput.enabled = true
				self.uilblChatToFriend.text = ''
				NGUITools.SetActive(self.uilblChatToFriend.gameObject,false)
			end
		end
		-- self:UpdateAfterChangeChannel()
	end
end

function UIChat:UpdateAfterChangeChannel( ... )
	--world channel return
	if self.chooseTab[1] == 1 then
		return
	end
	for i = 0, allCount - 1 do
		local itemMessage = MainPlayer.Instance.WorldChatList:get_Item(i)
		local item = self.uiMessageGrid.transform:FindChild(itemMessage)
		if itemMessage.type ~= 1 and self.chooseTab[itemMessage.type] == 0 then
			if item.gameObject.activeSelf then
				NGUITools.SetActive(item.gameObject, false)
			end
		else
			if not item.gameObject.activeSelf then
				NGUITools.SetActive(item.gameObject, true)
			end
		end
	end

	self:SortAllMessage()
end

--show chat room info
-- type 0 世界聊天 1-联盟聊天 2-队伍聊天 3-好友聊天 4- 系统
function UIChat:InitChatRoomInfo( chatType,id )
	if not chatType then 
		error("UIChat:InitChatRoomInfo need at least 1 parameter type 0~4!")
		return
	end
	if chatType == ChatChannelType.CCT_PRIVATE then 
		--好友私聊
		if type(id) ~= 'number' then 
			error("UIChat:InitChatRoomInfo friend need at least 2 parameter type ChatChannelType,friend id!")
			return
		end
		local messages = ChatCache.getFriendMsgById(self.currentChatFriend.id)
		if messages then
			local parent = self.uiMessageGrid.transform
			for i = 1,#messages do
				local message = messages[i]
				warning("getChatMsg info ",message.info.content)
				local chatItem = createUI("ChatAttrItem", parent)
				local chatLua = getLuaComponent(chatItem)
				chatLua.inChatRoom = true
				chatLua:SetMessage(message)
			end
		end

	else
		if MainPlayer.Instance.WorldChatList.Count <= 0 then
			return
		end
		MainPlayer.Instance.ChatWordsNum:Clear()
		local parent = self.uiMessageGrid.transform
		local lastMessage = nil
		if parent.childCount > 0 then
			lastMessage =  parent:GetChild(parent.childCount - 1)
		end

		if not lastMessage then
			local minNum = 1
			if MainPlayer.Instance.WorldChatList.Count > self.MAXNUM then
				minNum = MainPlayer.Instance.WorldChatList.Count - self.MAXNUM + 1
			end
			for i = minNum, MainPlayer.Instance.WorldChatList.Count do
				local message = MainPlayer.Instance.WorldChatList:get_Item(i - 1)
				local chatItem = createUI("ChatAttrItem", parent)
				local chatLua = getLuaComponent(chatItem)
				-- chatItem.gameObject.name = message.pos
				chatLua.inChatRoom = true
				chatLua:SetMessage(message)
				local num = GameUtils.GetStringLength(message.info.content)
				MainPlayer.Instance.ChatWordsNum:Add(num)
			end
		end
	end
	
	self:SortAllMessage()
end

--show one chat info
function UIChat:InitOneChatInfo( ... )
	if MainPlayer.Instance.WorldChatList.Count <= 0 and ChatCache.EmptyMsg() then
		return
	end

	local lastWorldMessage = nil 
	if MainPlayer.Instance.WorldChatList.Count > 0 then
		lastWorldMessage =  MainPlayer.Instance.WorldChatList:get_Item(MainPlayer.Instance.WorldChatList.Count - 1)
	end
	--好友，联盟，队伍消息在这里
	local lastFriendMessage = ChatCache.getLatestFriendMsg()
	local  message = nil
	if lastWorldMessage and lastFriendMessage then
		if lastWorldMessage.time > lastFriendMessage.time then
			message = lastWorldMessage 
		else
			message = lastFriendMessage
		end
	else
		message = lastWorldMessage or lastFriendMessage
	end
	if not  message then return end
	local chatItem = createUI("ChatAttrItem", self.uiOneChatMsgGrid.transform)
	-- chatItem.gameObject.name = lastMessage.pos
	local chatLua = getLuaComponent(chatItem)
	chatLua.inChatRoom = false
	chatLua:SetMessage(message)

	local oneMessageGrid = self.uiOneChatMsgGrid.transform
	if oneMessageGrid.childCount > 2 then
		local topMessage = oneMessageGrid:GetChild(0)
		NGUITools.Destroy(topMessage.gameObject)
	end
	self.uiOneChatMsgGrid.repositionNow = true
end
--显示好友列表
function UIChat:ShowFriendList( ... )
    -- body
    local friends = Friends.FriendList
    CommonFunction.ClearGridChild(self.uiFriendGrid.transform)
 --    if not self.ScrollViewAsyncLoadItem then 
	-- 	self.ScrollViewAsyncLoadItem = self.uiScrollView.gameObject:AddComponent("ScrollViewAsyncLoadItem")
	-- end
-- 	if true then
-- 	return false
-- end
	self.ScrollViewAsyncLoadItem.LoadCountOnce = 6 	--一帧加载6个
	local properFriends = {}
	for k,v in pairs(friends or {}) do
    	local unread  = ChatCache.getUnreadCount(1,v.acc_id)
    	-- warning('ShowFriendList unread ',unread,',online ',v.online)
    	if unread > 0 or v.online ~= 5 then 
	    	local data = {}
	    	data.name = v.name
	    	data.id = v.acc_id
	    	data.icon = v.icon
	    	data.num = unread
	    	properFriends[#properFriends+1] = data
    	end
    end
  	if #properFriends > 0 then
    	self.ScrollViewAsyncLoadItem.OnCreateItem = function ( index, parent )  
	        local item_count = self.uiFriendGrid.transform.childCount;
	        local go = nil
	        if index < item_count then
	            go = self.uiFriendGrid.transform:GetChild(index);
	        else      
		    	go = createUI('ChatFriendCell',self.uiFriendGrid.transform)
		    	local luaCom = getLuaComponent(go)
		    	local data = properFriends[index+1]
		    	luaCom.parent = self
		    	luaCom:Refresh(data)
		        luaCom.ClickCallBackFunc = self:HandlerClickFriend()
	   		end
		    return go;
		end

    	self.ScrollViewAsyncLoadItem:Refresh(#properFriends)
    end
  
    -- self.uiFriendGrid.repositionNow = true

end
function UIChat:HandlerFriendStateChanged( ... )
	-- body
	if not self.friendListDirty then 
		self.friendListDirty = true
		Scheduler.Instance:AddFrame(1,false,self:UpdateFriendList())
	end

end
function UIChat:UpdateFriendList( ... )
	-- body
	return function ( ... )
		-- body
		--好友信息更新
		self.friendListDirty = false
    	self.uiFriendGrid.repositionNow = true
    	warning('UIChat:UpdateFriendList')
	end
end
--点击好友列表中的好友回掉
function UIChat:HandlerClickFriend( )
    -- body
    return function ( data )
        -- body
        --TODO 显示好友聊天界面
        print('UIChat HandlerClickFriend')
        local chattoFriendStr = CommonFunction.GetConstString("STR_CHATING_TO_FRIEND")
        self.currentChatFriend.id = data.id
        self.currentChatFriend.name = data.name
        self.gridType = 0
        self:UpdateGridType()
        self.chatChannel = ChatChannelType.CCT_PRIVATE
        self:ResetChannelContent()
        self:InitChatRoomInfo(ChatChannelType.CCT_PRIVATE,data.id)
		self:UpdateFriendUnread()
		self.uiSendLabel.text = ""
		self.uiInput.value = ""
		self.uiInput.enabled = true		
		self.uilblChatToFriend.text = string.format(chattoFriendStr,data.name)
		NGUITools.SetActive(self.uilblChatToFriend.gameObject,true)
    end
end
function UIChat:SortAllMessage( ... )
	local parent = self.uiMessageGrid.transform
	local totalHeight = 0
	local minNum = 1
	--世界频道
	if self.chatChannel == ChatChannelType.CCT_WORLD then
		if parent.childCount > self.MAXNUM then
			minNum = parent.childCount - self.MAXNUM + 1
		end
		for i = parent.childCount - 1, minNum - 1 , -1 do
			local child = parent:GetChild(i).gameObject
			local childLua = getLuaComponent(child)
			local pos = child.transform.localPosition
			totalHeight = totalHeight + childLua:GetHeight()
			pos.y = totalHeight
			child.transform.localPosition = pos
		end
		while minNum > 1 do
			MainPlayer.Instance.ChatWordsNum:Remove(MainPlayer.Instance.ChatWordsNum:get_Item(0))
			NGUITools.Destroy(parent:GetChild(0).gameObject)
			minNum = minNum - 1
		end

		local totalNum = 0
		for i = 0, MainPlayer.Instance.ChatWordsNum.Count - 1 do
			totalNum = totalNum + MainPlayer.Instance.ChatWordsNum:get_Item(i)
		end
		--print('pre totalNum = ', totalNum)
		while (parent:GetChild(0).transform.localPosition.y >= self.MAXHEIGHT) and (totalNum >= self.MAXWORDS) do
			NGUITools.Destroy(parent:GetChild(0).gameObject)
				totalNum = totalNum - MainPlayer.Instance.ChatWordsNum:get_Item(0)
				MainPlayer.Instance.ChatWordsNum:Remove(MainPlayer.Instance.ChatWordsNum:get_Item(0))
		end
	--好友私聊
	elseif self.chatChannel == ChatChannelType.CCT_PRIVATE then
		if parent.childCount > self.MAXNUM then
			minNum = parent.childCount - self.MAXNUM + 1
		end
		for i = parent.childCount - 1, minNum - 1 , -1 do
			local child = parent:GetChild(i).gameObject
			local childLua = getLuaComponent(child)
			local pos = child.transform.localPosition
			totalHeight = totalHeight + childLua:GetHeight()
			pos.y = totalHeight
			child.transform.localPosition = pos
		end
	end
	
end

function UIChat:OnDragMessage( ... )
	return function ( ... )
		if self.uiProgressBar.value >= 0 and self.uiProgressBar.value < 0.99 then
			self.locked = true
		else
			self.locked = false
			self:AfterLock()
		end
		print('self.locked = ', self.locked)
		if self.uiLockAnimator then
			self.uiLockAnimator:SetBool('Switch', self.locked)
		end
	end
end

function UIChat:ResetChatRoom( ... )
	self.locked = false
	self.unReadNum = 0
	self.unReadMessage = {}
	self.uiProgressBar.value = 1
	NGUITools.SetActive(self.uiUnReadMask.gameObject, false)
end

function UIChat:EnabledChatSwitch(Switch)
	self.uiBtnOpenArrow.transform:GetComponent('BoxCollider').enabled = Switch
end

function UIChat:OnMessageChange( ... )
	return function ( ... )
		local str = self.uiSendLabel.text
		string.gsub(str, "%\t", "")
		string.gsub(str, "%\n", "")
		self.uiSendLabel.text = str
		--self.uiInput.value = str
	end
end
function UIChat:UpdateGridType(  )
	-- body
	if self.gridType == 0 then 
		local async = self.uiScrollView.transform:GetComponent("ScrollViewAsyncLoadItem")
		if async then 
			GameObject.Destroy(async)
		end
    	CommonFunction.ClearGridChild(self.uiFriendGrid.transform)
		NGUITools.SetActive(self.uiMessageGrid.gameObject,true)
		NGUITools.SetActive(self.uiFriendGrid.gameObject,false)
		self.uiMessageGrid.transform.parent:GetComponent('UIScrollView'):ResetPosition()
	else
		NGUITools.SetActive(self.uiMessageGrid.gameObject,false)
		NGUITools.SetActive(self.uiFriendGrid.gameObject,true)
		local async = self.uiScrollView.transform:GetComponent("ScrollViewAsyncLoadItem")
		if not async then 
			self.ScrollViewAsyncLoadItem = self.uiScrollView.gameObject:AddComponent("ScrollViewAsyncLoadItem")
		end
	end
end
return UIChat

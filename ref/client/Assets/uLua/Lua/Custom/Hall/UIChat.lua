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


	uiOneChatMsgGrid,
	uiExpression,
	uiVoice,
	uiAnimator,
	uiMask,
	uiBox,
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

	self.uiProgressBar = self.transform:FindChild('Form/ProgressBar'):GetComponent('UIScrollBar')
	self.uiScrollView = self.transform:FindChild('Form/Scroll'):GetComponent('UIScrollView')
	self.uiMessageGrid = self.transform:FindChild('Form/Scroll/Grid')--:GetComponent('UIGrid')
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
	self.uiMask = self.transform:FindChild('Mask')
	LuaHelper.RegisterPlatMsgHandler(MsgID.ChatRespID, self:OnChatRespHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.QueryPlayerInfoRespID, self:OnQueryPlayerInfoHandler(), self.uiName)
end

function UIChat:Start( ... )
	EventDelegate.Add(self.uiInput.onChange, LuaHelper.Callback(self:OnMessageChange()))
	MainPlayer.Instance.onNewChatMessage = self:UpdateChatBar()
	self.uiScrollView.onDragFinished = self:OnDragMessage()

	for i,v in ipairs(self.uiToggles) do
		addOnClick(self.uiToggles[i].gameObject, self:ChooseChannel())
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
	self.initPosX = self.uiSendLabel.transform.localPosition.x
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

function UIChat:OpenChatFrame( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.chat) then return end

		local hall = UIManager.Instance.m_uiRootBasePanel.transform:FindChild('UIHall(Clone)')
		local hallLua = nil
		if hall then
			hallLua = getLuaComponent(hall.gameObject)
		end

		if go == self.uiBox.gameObject then
			--open
			if hallLua then hallLua:SetModelActive(false) end
			NGUITools.SetActive(self.uiSmallTip.gameObject, false)
			NGUITools.SetActive(self.uiForm.gameObject, true)
			self:InitChatRoomInfo()
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

function UIChat:UpdateChatBar( ... )
	return function ()
		local hall = UIManager.Instance.m_uiRootBasePanel.transform:FindChild('UIHall(Clone)')
		if not hall then
			return
		end

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

		local message = self.uiSendLabel.text
		--string.gsub(message, "^%s*(.-)%s*$", "%1") == string.trim
		if not message or string.gsub(message, "^%s*(.-)%s*$", "%1") == "" then
			CommonFunction.ShowPopupMsg(getCommonStr('CANT_EMPTY_MESSAGE'),nil,nil,nil,nil,nil)
			return
		end

		local req =
		{
			type = enumToInt(ChatChannelType.CCT_WORLD),
			content = message,
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
				local num = GameUtils.GetStringLength(v.info.content)
				MainPlayer.Instance.ChatWordsNum:Add(num)
			end
			self.unReadMessage = {}
			self:SortAllMessage()
			NGUITools.SetActive(self.uiUnReadMask.gameObject, false)
		end
		self.uiProgressBar.value = 1
	end
end

--choose channel
function UIChat:ChooseChannel( ... )
	return function (go)
		self.chooseTab = {1,0,0,0}	--default world, system, league, team
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
function UIChat:InitChatRoomInfo( ... )
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
	self:SortAllMessage()
end

--show one chat info
function UIChat:InitOneChatInfo( ... )
	if MainPlayer.Instance.WorldChatList.Count <= 0 then
		return
	end
	local lastMessage = MainPlayer.Instance.WorldChatList:get_Item(MainPlayer.Instance.WorldChatList.Count - 1)
	local chatItem = createUI("ChatAttrItem", self.uiOneChatMsgGrid.transform)
	-- chatItem.gameObject.name = lastMessage.pos
	local chatLua = getLuaComponent(chatItem)
	chatLua.inChatRoom = false
	chatLua:SetMessage(lastMessage)
	self.uiOneChatMsgGrid.repositionNow = true

	local oneMessageGrid = self.uiOneChatMsgGrid.transform
	if oneMessageGrid.childCount > 2 then
		local topMessage = oneMessageGrid:GetChild(0)
		NGUITools.Destroy(topMessage.gameObject)
	end
end

function UIChat:SortAllMessage( ... )
	local totalHeight = 0
	local parent = self.uiMessageGrid.transform
	local minNum = 1
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
	--print('late totalNum = ', totalNum)
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

return UIChat

ChatAttrItem =
{
	uiName = 'ChatAttrItem',
	-----------------------UI
	uiChatTypeSprite,
	uiChatTypeText,
	uiChatMessage,
	uiName,
	uiBtnJoin,
	-------------------------
	accId,
	name,
	type,
	content,
	inChatRoom = false,
	height,
	banTwice,
	waitTime = 1,
}

local typeName =
{
	getCommonStr("WORLD_CHAT"),
	getCommonStr("SYSTEM_CHAT"),
	getCommonStr("LEAGUE_CHAT"),
	getCommonStr("TEAM_CHAT"),
}

local messageColors =
{
	-- '[EE9A49]%s[-]',
	-- '[FF8C69]%s[-]',
	-- '[1874CD]%s[-]',
	-- '[66CD00]%s[-]',
	Color.New(250/255, 151/255, 45/255, 1),
	Color.New(250/255, 100/255, 36/255, 1),
	Color.New(39/255, 128/255, 208/255, 1),
	Color.New(103/255, 215/255, 78/255, 1),
}

function ChatAttrItem:Awake()
	self.uiChatTypeSprite = self.transform:FindChild('ChatTypeIcon'):GetComponent('UISprite')
	self.uiChatTypeText = self.transform:FindChild('ChatTypeIcon/ChatTypeText'):GetComponent('UILabel')
	--self.uiChatMessage = self.transform:FindChild('ValueCur'):GetComponent('UILabel')
	self.uiName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiBtnJoin = self.transform:FindChild('ButtonJoin'):GetComponent('UIButton')
end

function ChatAttrItem:Start( ... )
	NGUITools.SetActive(self.uiBtnJoin.gameObject, false)
end

function ChatAttrItem:FixedUpdate( ... )
	if self.banTwice then
		self.waitTime = self.waitTime - UnityTime.fixedDeltaTime
	end
	if self.waitTime <= 0 then
		self.waitTime = 1
		self.banTwice = false
	end
end

function ChatAttrItem:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function ChatAttrItem:Refresh( ... )
	-- body
end

function ChatAttrItem:OnClose( ... )
end

--------------------------------------

function ChatAttrItem:SetMessage(message)
	if not self.inChatRoom then
		self:EnabledComponentInChildren(self.gameObject, "BoxCollider", self.inChatRoom)
	end

	self.type = message.type
	self.accId = message.info.acc_id
	self.name = message.info.ogri_name
	self.content = message.info.content

	self.uiChatTypeText.text = typeName[self.type]
	if IsNil(ChatChannelType.CCT_SYSTEM) == false then
		if self.type ~= enumToInt(ChatChannelType.CCT_SYSTEM) then
			self.uiName.text = string.format("[%s]：", self.name)
			addOnClick(self.uiName.gameObject, self:ShowPlayerDetailInfo())
		else
			self.uiName.text = ""
		end
	end

	if not self.inChatRoom then
		self.uiName.maxLineCount = 1
	end
	self.content = string.gsub(self.content, "%[%w+%]", "")
	-- print('self.content = ', self.content)
	--self.uiChatMessage.text = self.content
	self.uiName.text = self.uiName.text .. self.content
	--self.uiChatMessage.color = messageColors[self.type]
	self.uiName.color = messageColors[self.type]
	self.uiChatTypeSprite.color = messageColors[self.type]
	if self.accId == MainPlayer.Instance.AccountID then
		--self.uiChatMessage.color = Color.New(1,1,1,1)
		self.uiName.color = Color.New(1,1,1,1)
	end
end

function ChatAttrItem:ShowPlayerDetailInfo( ... )
	return function (go)
		if not self.inChatRoom then return end
		if self.banTwice then
			return
		end
		self.banTwice = true
		local req =
		{
			acc_id = self.accId,
		}
		local buffer = protobuf.encode("fogs.proto.msg.QueryPlayerInfoReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.QueryPlayerInfoReqID, buffer)
	end
end

function ChatAttrItem:EnabledComponentInChildren(go,type,switch)
	local parentTrans = go.transform
	if parentTrans then
		for i = 0, parentTrans.childCount - 1 do
			if parentTrans:GetChild(i).transform:GetComponent(type) then
				parentTrans:GetChild(i).transform:GetComponent(type).enabled = switch
			end
		end
	end
end

function ChatAttrItem:GetHeight( ... )
	self.height = self.uiName.height
	-- print('self.height = ', self.height)
	return self.height + 10
end

return ChatAttrItem

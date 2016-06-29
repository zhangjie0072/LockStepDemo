require "Custom/Chat/ChatCache"
ChatAttrItem =
{
	uiName = 'ChatAttrItem',
	-----------------------UI
	uiChatTypeSprite,
	uiChatTypeText,
	uiChatMessage,
	uiName,
	uiBtnJoin,
	uiBtnChoose,
	-------------------------
	accId,
	name,
	type,
	content,
	msgTime,
	inChatRoom = false,
	height,
	banTwice,
}



function ChatAttrItem:Awake()
	self.uiChatTypeSprite = self.transform:FindChild('ChatTypeIcon'):GetComponent('UISprite')
	self.uiChatTypeText = self.transform:FindChild('ChatTypeIcon/ChatTypeText'):GetComponent('UILabel')
	--self.uiChatMessage = self.transform:FindChild('ValueCur'):GetComponent('UILabel')
	self.uiName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiBtnJoin = self.transform:FindChild('ButtonJoin'):GetComponent('UIButton')
	-- self.uiBtnChoose = self.transform:FindChild('uiBtnChoose'):GetComponent('UIButton')
end

function ChatAttrItem:Start( ... )
	NGUITools.SetActive(self.uiBtnJoin.gameObject, false)
end

function ChatAttrItem:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
	self = nil 
end

function ChatAttrItem:Refresh( ... )
	-- body
end

function ChatAttrItem:OnClose( ... )
end

--------------------------------------
local CHAT_TO_ME = CommonFunction.GetConstString("CHAT_TO_ME")--'[%s]对[我]说:' --
local ME_CHAT_TO = CommonFunction.GetConstString("ME_CHAT_TO")--'[我]对[%s]说:' --
function ChatAttrItem:SetMessage(message)
	if not self.inChatRoom then
		self:EnabledComponentInChildren(self.gameObject, "BoxCollider", self.inChatRoom)
	end
	if type(message.type ) ~= 'number' then
		self.type = enumToInt(message.type)
	else
		self.type = message.type
	end
	self.accId = message.info.acc_id
	self.name = message.info.ogri_name
	self.content = message.info.content
	self.msgTime = message.time
	warning('ChatAttrItem ',self.type)
	self.uiChatTypeText.text = typeName[self.type]
	if IsNil(ChatChannelType.CCT_SYSTEM) == false then
		if self.type ~= enumToInt(ChatChannelType.CCT_SYSTEM) then
			if self.type == enumToInt(ChatChannelType.CCT_PRIVATE) then
				if message.pos == 1 then
					self.uiName.text = string.format(ME_CHAT_TO, self.name)
				else
					self.uiName.text = string.format(CHAT_TO_ME, self.name)
					if self.inChatRoom then
       					ChatCache.ReadFriendMsg(self.accId)	--减少一条未读消息
       				end
				end
			else
				self.uiName.text = string.format("[%s]：", self.name)
				if self.accId == MainPlayer.Instance.AccountID then
					--self.uiChatMessage.color = Color.New(1,1,1,1)
					self.uiName.color = Color.New(1,1,1,1)
				end
			end
			addOnClick(self.uiName.gameObject, self:ShowPlayerDetailInfo())
		else
			self.uiName.text = ""
		end
	end

	if not self.inChatRoom then
		self.uiName.maxLineCount = 1
	end
	self.content = string.gsub(self.content or "", "%[%w+%]", "")
	-- print('self.content = ', self.content)
	--self.uiChatMessage.text = self.content

	self.uiName.text = self.uiName.text .. self.content
	--self.uiChatMessage.color = messageColors[self.type]
	local color = nil
	if self.type ~= enumToInt(ChatChannelType.CCT_WORLD) then
		if message.pos == 1 then
			--世界频道以外的其他频道，来自我的消息均以统一颜色显示
			color = messageColors[enumToInt(ChatChannelType.CCT_PRIVATE)+1]
		end
	end
	if not color then 
		self.uiName.color = messageColors[self.type]
	else
		self.uiName.color  = color
	end
	self.uiChatTypeSprite.color = messageColors[self.type]
end

function ChatAttrItem:ShowPlayerDetailInfo( ... )
	return function (go)
		if not self.inChatRoom then return end
		if self.banTwice then
			return
		end
		self.banTwice = true
		Scheduler.Instance:AddTimer(1,false,self:BanWait())
		local req =
		{
			acc_id = self.accId,
		}
		local buffer = protobuf.encode("fogs.proto.msg.QueryPlayerInfoReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.QueryPlayerInfoReqID, buffer)
	end
end
function ChatAttrItem:BanWait( ... )
	-- body
	return function ( ... )
		-- body
		self.banTwice = false
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

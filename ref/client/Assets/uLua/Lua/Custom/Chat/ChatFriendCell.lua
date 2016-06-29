------------------------------------------------------------------------
-- class name    : ChatFriendCell
-- create time   : 2016-06-22 18:43:15
-- author        : zxb
-- description	 : 聊天框好友信息
------------------------------------------------------------------------
require "Custom/Chat/ChatCache"

ChatFriendCell = {
	uiName = "ChatFriendCell",
	
--=============================--
--==	 公有变量分隔线	 	 ==--
--=============================--
	ClickCallBackFunc = nil,--点击回调函数
	data,--={ id,name,icon,num}
	m_bDirty  = false,
	uiHeadIcon,
	uiLabName,
	uiRedDot,
	uiMsgNum,
	parent,
}

function ChatFriendCell:Awake( ... )
	addOnClick(self.gameObject,self:OnClickHanlder())
    self.uiHeadIcon = getChildGameObject(self.transform, "Icon")
    self.uiLabName = getComponentInChild(self.transform, "Name", "UILabel")
    self.uiMsgNum = getComponentInChild(self.transform, "RedDot/Num", "UILabel")
    self.uiRedDot =  self.transform:FindChild("RedDot")
end

function ChatFriendCell:Start( ... )
	
end

function ChatFriendCell:OnDestroy( ... )
	NGUITools.Destroy(self.gameObject)
	NGUITools.Destroy(self.transform)

	ChatCache.RemoveNotifyer(self.data.id)
	self = nil 
end
--------------刷新------------------
function ChatFriendCell:Refresh(data)
	if data then 
		self.data = data
	end

	self:Dirty()
end
function ChatFriendCell:Dirty( ... )
	-- body
	if not self.m_bDirty then 
		self.m_bDirty = true
		Scheduler.Instance:AddFrame(1,false,self:LateUpdate())
	end
end
function ChatFriendCell:LateUpdate( ... )
	-- body
	return function ( ... )
		-- body
		--update data

		self.m_bDirty = false
		--消息变化通知
		ChatCache.AddNotifyer(self.data.id,self:MsgChanged())

	    self.uiLabName.text = self.data.name

		--icon
		if not self.headIconScript then
			self.headIconScript = getLuaComponent(createUI("CareerRoleIcon", self.uiHeadIcon.transform))
			self.headIconScript.id = tonumber(self.data.icon)
			self.headIconScript.showPosition = false
		else
			self.headIconScript.id = tonumber(self.data.icon)
			self.headIconScript:Refresh()
		end
		self:ResetMsg(self.data.num)

	end
end
function ChatFriendCell:ResetMsg( num )
	-- body
	warning('ChatFriendCell ResetMsg ',num)
	if num then
		if num > 0 then
			NGUITools.SetActive(self.uiRedDot.gameObject,true)
			NGUITools.SetActive(self.uiMsgNum.gameObject,true)
			self.uiMsgNum.text = ''..num
			self.data.num = num
			return
		end
	end
	NGUITools.SetActive(self.uiRedDot.gameObject,false)
	NGUITools.SetActive(self.uiMsgNum.gameObject,false)
end
--好友消息更新回掉
function ChatFriendCell:MsgChanged()
	-- body
	return function ( num )
		-- body
		if num then
			-- if not self.gameObject.activeSelf then return end

			self:ResetMsg(num)
			--将信息变化通知UIChat
			self.parent:HandlerFriendStateChanged()
		end
	end
end
--=============================--
--==	 私有方法分隔线	 	 ==--
--=============================--

------------------------------------
-- 方法作用：点击此Item的回调函数
------------------------------------
function ChatFriendCell:OnClickHanlder( ... )
	return function()
		if self.ClickCallBackFunc ~= nil then
			self.ClickCallBackFunc(self.data)
		end
	end
end
return ChatFriendCell
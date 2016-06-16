------------------------------------------------------------------------
-- class name    : ChatMsgItem
-- create time   : 2016-05-10 18:43:15
-- author        : Jackwu
-- description	 : 聊天消息item,目前用于聊天历史记录item和常用消息发送item
------------------------------------------------------------------------
ChatMsgItem = {
	uiName = "ChatMsgItem",
	
--=============================--
--==	 公有变量分隔线	 	 ==--
--=============================--
	ClickCallBackFunc = nil,--点击回调函数
--=============================--
--==	 私有变量分隔线	 	 ==--
--=============================--
	_content,--消息内容
--=============================--
--==	 	UI变量 	 		 ==--
--=============================--
	_lblContent,--消息文本框
}

function ChatMsgItem:Awake( ... )
	self:UIParser()
	self:AddEvent()
end

function ChatMsgItem:Start( ... )
	
end

function ChatMsgItem:Update( ... )
	
end

function ChatMsgItem:FixedUpdate( ... )
	
end

function ChatMsgItem:OnDestroy( ... )
	
end

--------------刷新------------------
function ChatMsgItem:Refresh(subID)

end

--------------解析UI组件------------
function ChatMsgItem:UIParser( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self._lblContent = transform:GetComponent("UILabel")
end

--------------侦听事件--------------
function ChatMsgItem:AddEvent( ... )
	addOnClick(self.gameObject,self:OnClickHanlder())
end

--=============================--
--==	 公有方法分隔线	 	 ==--
--=============================--

------------------------------------
-- 方法作用：设置消息内容
-- msg : 消息
------------------------------------
function ChatMsgItem:SetContent(msg)
	self._content = msg
	self._lblContent.text = msg
end

------------------------------------
-- 方法作用：获取消息内容
------------------------------------
function ChatMsgItem:GetContent( ... )
	return self._content
end

--=============================--
--==	 私有方法分隔线	 	 ==--
--=============================--

------------------------------------
-- 方法作用：点击此Item的回调函数
------------------------------------
function ChatMsgItem:OnClickHanlder( ... )
	return function()
		if self.ClickCallBackFunc ~= nil then
			self.ClickCallBackFunc(self._content)
		end
	end
end
return ChatMsgItem

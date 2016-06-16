------------------------------------------------------------------------
-- class name    : ChatMsgItem2
-- create time   : 2016-05-19 10:24:10
-- author        : Jackwu
-- description	 : 聊天消息item2
------------------------------------------------------------------------
ChatMsgItem2 = {
	uiName = "ChatMsgItem2",
	
--=============================--
--==	 公有变量分隔线	 	 ==--
--=============================--

--=============================--
--==	 私有变量分隔线	 	 ==--
--=============================--
	_content = nil,--内容
--=============================--
--==	 	UI变量 	 		 ==--
--=============================--
	_lblContent = nil,--消息label
}

function ChatMsgItem2:Awake( ... )
	self:UIParser()
	self:AddEvent()
end

function ChatMsgItem2:Start( ... )
	
end

function ChatMsgItem2:Update( ... )
	
end

function ChatMsgItem2:FixedUpdate( ... )
	
end

function ChatMsgItem2:OnDestroy( ... )
	
end

--------------刷新------------------
function ChatMsgItem2:Refresh(subID)

end

--------------解析UI组件------------
function ChatMsgItem2:UIParser( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self._lblContent = find("ValueCur"):GetComponent("UILabel")
end

--------------侦听事件--------------
function ChatMsgItem2:AddEvent( ... )
	--addOnClick(self.uiCloseBtn.gameObject,self:OnClickHanlder())--
	
end

--=============================--
--==	 公有方法分隔线	 	 ==--
--=============================--

-------------------------------------------
-- 设置内容
-- content 内容
-------------------------------------------
function ChatMsgItem2:SetContent(content)
	self._content = content
	self._lblContent.text = self._content
end

-------------------------------------------
-- 设置组件的真实高度
-- content 内容
-------------------------------------------
function ChatMsgItem2:GetHeight()
	self.height = self._lblContent.height
	-- print('self.height = ', self.height)
	return self.height + 10
end


--=============================--
--==	 私有方法分隔线	 	 ==--
--=============================--

return ChatMsgItem2

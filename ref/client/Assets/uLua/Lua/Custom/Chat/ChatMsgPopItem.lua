------------------------------------------------------------------------
-- class name    : ChatMsgPopItem
-- create time   : 2016-05-10 18:44:34
-- author        : Jackwu
-- description	 : 聊天信息冒泡显示item,目前用于天梯赛选人界面消息提示
------------------------------------------------------------------------
ChatMsgPopItem = {
	uiName = "ChatMsgPopItem",
	
--=============================--
--==	 公有变量分隔线	 	 ==--
--=============================--

--=============================--
--==	 私有变量分隔线	 	 ==--
--=============================--	
	_content,--消息
	_showTimer,
--=============================--
--==	 	UI变量 	 		 ==--
--=============================--
	_lblContent,--消息内容label
}

function ChatMsgPopItem:Awake( ... )
	self:UIParser()
	self:AddEvent()
	self._showTimer = LuaHelper.Action(self:ClosePop())
end

function ChatMsgPopItem:Start( ... )
	
end

function ChatMsgPopItem:Update( ... )
	
end

function ChatMsgPopItem:FixedUpdate( ... )
	
end

function ChatMsgPopItem:OnDestroy( ... )
	
end

--------------刷新------------------
function ChatMsgPopItem:Refresh(subID)

end

--------------解析UI组件------------
function ChatMsgPopItem:UIParser( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self._lblContent = find("Tip"):GetComponent("UILabel")

end

--------------侦听事件--------------
function ChatMsgPopItem:AddEvent( ... )
	--addOnClick(self.uiCloseBtn.gameObject,self:OnClickHanlder())--
	
end

--=============================--
--==	 公有方法分隔线	 	 ==--
--=============================--

------------------------------------
-- 方法作用：设置内容
------------------------------------
function ChatMsgPopItem:SetContent(content)
	self._content = content
	self._lblContent.text = content
	NGUITools.SetActive(self.gameObject,true)
	Scheduler.Instance:RemoveTimer(self._showTimer)
	Scheduler.Instance:AddTimer(3, false, self._showTimer)
end

------------------------------------
-- 方法作用：关闭
------------------------------------
function ChatMsgPopItem:ClosePop( ... )
	return function()
		NGUITools.SetActive(self.gameObject,false)
	end
end

--=============================--
--==	 私有方法分隔线	 	 ==--
--=============================--

return ChatMsgPopItem

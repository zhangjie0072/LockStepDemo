------------------------------------------------------------------------
-- module name    : ChatConstMsgConfig
-- create time   : 2016-05-10 10:33:06
-- author        : Jackwu
-- description	 : 常用消息信息配置，此文件主要提拱对配置文件操作的一些接口
------------------------------------------------------------------------
print("ChatConstMsgConfig".."启动")
ChatConstMsgConfig = {

-------------公有变量-----------

-------------私有变量-----------
	_configRes = ChatConstMsgConfigRes,
}

-- 获取所以常用信息列表---
function ChatConstMsgConfig.GetAllConstMsg( ... )
	return ChatConstMsgConfig._configRes
end

-- 根据id获取对应的常用消息---
function ChatConstMsgConfig.GetMsgById(id)
	for _,v in ipairs(ChatConstMsgConfig._configRes) do
		if v['id'] == id then
			return v['info']
		end
	end
	return nil
end

-- 获取常用信息条目数--------
function ChatConstMsgConfig.GetMsgCount( ... )
	return #ChatConstMsgConfig._configRes
end
------------------------------------------------
--------		公有方法分隔线		------------
------------------------------------------------


------------------------------------------------
----------		私有方法分隔线		------------
------------------------------------------------

--------------接收消息--------------



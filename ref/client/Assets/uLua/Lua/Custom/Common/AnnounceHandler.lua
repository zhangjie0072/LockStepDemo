--项目公用函数

AnnounceHandler =
{
	unFinishPos,
}

function AnnounceHandler.Register()
	MainPlayer.Instance.onAnnounce = AnnounceHandler.OnShowAnnounce()
end

function AnnounceHandler.OnShowAnnounce( ... )
	return function ( ... )
		local listCount = MainPlayer.Instance.AnnouncementList.Count
		local root = UIManager.Instance.m_uiRootBasePanel
		if listCount <= 0 then
			local announce = root.transform:FindChild('UIAnnouncement')
			if announce.gameObject.activeSelf then
				local announceLua = getLuaComponent(announce)
				announceLua:DoClose()()
			end
			return
		end
		print('show announce')
		local announce = root.transform:FindChild('UIAnnouncement')
		if not announce.gameObject.activeSelf then
			local content = MainPlayer.Instance.AnnouncementList:get_Item(0)
			if content then
				NGUITools.SetActive(announce.gameObject, true)
				local announceLua = getLuaComponent(announce)
				announceLua:SetMessage(content)
				announceLua.onFinish = function ( ... )
					if MainPlayer.Instance.AnnouncementList.Count > 0 then
						MainPlayer.Instance.AnnouncementList:Remove(content)
						announceLua.onFinish = nil
						AnnounceHandler.OnShowAnnounce()()
					end
				end
				AnnounceHandler.AddAnnounceToChat(content)
			end
		else
			local content = MainPlayer.Instance.AnnouncementList:get_Item(0)
			if content then
				local announceLua = getLuaComponent(announce)
				announceLua:SetMessage(content)
				announceLua.onFinish = function ( ... )
					if MainPlayer.Instance.AnnouncementList.Count > 0 then
						MainPlayer.Instance.AnnouncementList:Remove(content)
						announceLua.onFinish = nil
						AnnounceHandler.OnShowAnnounce()()
					end
				end
				AnnounceHandler.AddAnnounceToChat(content)
			end
		end
	end
end

function AnnounceHandler.AddAnnounceToChat(content)
	local systemChat = ChatBroadcast.New()
	local info = ChatContent.New()
	info.content = content
	systemChat.info = info
	systemChat.type = enumToInt(ChatChannelType.CCT_SYSTEM)
	MainPlayer.Instance.WorldChatList:Add(systemChat)
	if MainPlayer.Instance.WorldChatList.Count > 100 then
		MainPlayer.Instance.WorldChatList:Remove(MainPlayer.Instance.WorldChatList:get_Item(0))
	end
	if MainPlayer.Instance.onNewChatMessage then
		print('AddAnnounce-------->>>')
		MainPlayer.Instance.onNewChatMessage:DynamicInvoke()
	end
end
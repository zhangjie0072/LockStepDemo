UIAnnouncement = 
{
	uiName = 'UIAnnouncement',
	-----------------------UI
	-- uiBackground,
	uiMask,
	uiMessage,
	-- uiAnimator,
	-------------------------
	pos,
	SMOOTH = 130,
	onFinish,
}


function UIAnnouncement:Awake()
	-- self.uiBackground = self.transform:FindChild('Background'):GetComponent('UISprite')
	self.uiMask = self.transform:FindChild('Sprite'):GetComponent('UIPanel')
	self.uiMessage = self.transform:FindChild('Sprite/Message'):GetComponent('UILabel')
	-- self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIAnnouncement:Start( ... )
	AnnounceHandler.OnShowAnnounce()()
end

function UIAnnouncement:FixedUpdate( ... )
	local messagePos = self.uiMessage.transform.localPosition
	messagePos.x = messagePos.x - UnityTime.fixedDeltaTime * self.SMOOTH
	if messagePos.x < -(self.uiMessage.width/2 + self.uiMask.width/2) then
		if self.onFinish then
			self.onFinish()
		end
		messagePos.x = self.pos
	end

	self.uiMessage.transform.localPosition = messagePos
end

function UIAnnouncement:OnDestroy( ... )
	local listCount = MainPlayer.Instance.AnnouncementList.Count
	if listCount > 0 then
		AnnounceHandler.unFinishPos = self.uiMessage.transform.localPosition.x
		print('qiehuan pos = ' , AnnounceHandler.unFinishPos)
	else
		AnnounceHandler.unFinishPos = nil
	end
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIAnnouncement:Refresh( ... )
	-- body
end

function UIAnnouncement:OnClose( ... )
	-- NGUITools.Destroy(self.gameObject)
	NGUITools.SetActive(self.gameObject, false)
end

--------------------------------------


function UIAnnouncement:DoClose( ... )
	return function ( ... )
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

function UIAnnouncement:SetMessage(message)
	self.uiMessage.text = message
	self.pos = self.uiMask.width / 2 + self.uiMessage.width / 2
	local initPos = self.uiMessage.transform.localPosition
	initPos.x = self.uiMask.width / 2 + self.uiMessage.width / 2
	self.uiMessage.transform.localPosition = initPos
	print('AnnounceHandler.unFinishPos = ' , AnnounceHandler.unFinishPos)
	if AnnounceHandler.unFinishPos then
		local forawardPos = self.uiMessage.transform.localPosition
		forawardPos.x = AnnounceHandler.unFinishPos
		self.uiMessage.transform.localPosition = forawardPos
	end
end

return UIAnnouncement

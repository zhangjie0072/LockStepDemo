PopupFrame1 = {
	uiName = 'PopupFrame1',
	titletext,
	messagetext,
	YesClick,
	NoClick,
}

function PopupFrame1:Awake()
	
	self.title = getComponentInChild(self.transform, "Title", "UILabel")
	self.message = getComponentInChild(self.transform, "Message", "UILabel")
	self.buttonYes = getChildGameObject(self.transform, "YesBtn")
	self.buttonNo = getChildGameObject(self.transform, "NoBtn")
	addOnClick(self.buttonYes,self:MakeOnOKClick())
	addOnClick(self.buttonNo,self:MakeOnCancelClick())
end

function PopupFrame1:Start()
	self.title.text = self.titletext
	self.message.text = self.messagetext
end

function PopupFrame1:MakeOnOKClick()
	return function ()
		if self.YesClick then
			self.YesClick()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

function PopupFrame1:MakeOnCancelClick()
	return function ()
		if self.NoClick then
			self.NoClick()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

return PopupFrame1
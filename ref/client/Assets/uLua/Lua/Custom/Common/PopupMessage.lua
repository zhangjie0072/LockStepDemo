PopupMessage = {
	uiName = 'PopupMessage',

	-----------------------
	message = "",
	okLabel = nil,
	cancelLabel = nil,
	onOKClick = nil,
	onCancelClick = nil,

	closeByCancel,
	closeByConfirm,

	---------------------UI
	uiMessage,
	uiTitle,
	uiCloseBtn,
	uiConfirmBtn,
	uiCancelBtn,
	uiAnimator,
}


-----------------------------------------------------------------
function PopupMessage:Awake()
	print(self.uiName, "Awake")
	self.uiMessage = self.transform:FindChild('Window/Message'):GetComponent('UILabel')
	self.uiTitle = self.transform:FindChild('Window/Title'):GetComponent('MultiLabel')
	self.uiCloseBtn = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
	self.uiConfirmBtn = self.transform:FindChild('Window/ButtonOK'):GetComponent('UIButton')
	self.uiCancelBtn = self.transform:FindChild('Window/ButtonCancel'):GetComponent('UIButton')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function PopupMessage:Start()
	print(self.uiName, "Start, message:", self.message)
	local btnClose = getLuaComponent(self.uiCloseBtn)
	btnClose.onClick = self:MakeOnCancelClick()
	addOnClick(self.uiConfirmBtn.gameObject, self:MakeOnOKClick())
	addOnClick(self.uiCancelBtn.gameObject, self:MakeOnCancelClick())

	if self.okLabel then
		self.uiConfirmBtn.transform:FindChild("Text"):GetComponent("MultiLabel"):SetText(self.okLabel)
	end

	if self.cancelLabel and self.cancelLabel ~= "" then
		self.uiCancelBtn.transform:FindChild("Text"):GetComponent("MultiLabel"):SetText(self.cancelLabel)
	end
end

function PopupMessage:FixedUpdate( ... )
	-- body
end

function PopupMessage:OnClose()
	if self.closeByCancel and self.onCancelClick then
		self.onCancelClick:DynamicInvoke(self.uiCancelBtn.gameObject)
	end
	if self.closeByConfirm and self.onOKClick then
		self.onOKClick:DynamicInvoke(self.uiConfirmBtn.gameObject)
	end
	NGUITools.Destroy(self.gameObject)
end

function PopupMessage:OnDestroy( ... )
	-- body
	print(self.uiName, "OnDestroy, message:", self.message)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-----------------------------------------------------------------
function PopupMessage:MakeOnOKClick()
	return function (go)
		self.closeByConfirm = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function PopupMessage:MakeOnCancelClick()
	return function (go)
		self.closeByCancel = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function PopupMessage:SetMessage(message, align)
	self.message = message
	if self.uiMessage then
		self.uiMessage.text = self.message
	end
	if align then
		self.uiMessage.alignment = NGUIText.Alignment.Left
	end
end

function PopupMessage:SetTitle(title)
	self.uiTitle:SetText(title)
end

return PopupMessage

--encoding=utf-8

ConfirmFrame =
{
	uiName = 'ConfirmFrame',
	
	--------------------------
	onConfirm,
	------------------------UI
	uiBtnOk,
	uiBtnCancel,
	uiBtnClose,
	uiTip,
	uiTitle,
};


-----------------------------------------------------------------
--Awake
function ConfirmFrame:Awake()
	local transform = self.transform
	self.uiTitle = transform:FindChild('Window/Title'):GetComponent('MultiLabel')
	self.uiTip = transform:FindChild('Window/Tip'):GetComponent('UILabel')
	self.uiBtnOk = transform:FindChild('Window/ButtonOK'):GetComponent('UIButton')
	self.uiBtnCancel = transform:FindChild('Window/ButtonCancel'):GetComponent('UIButton')
	self.uiBtnClose = createUI('ButtonClose', transform:FindChild('Window/ButtonClose'))
end

--Start
function ConfirmFrame:Start()
	addOnClick(self.uiBtnOk.gameObject, self:OnOKClick())
	addOnClick(self.uiBtnCancel.gameObject, self:OnCloseClick())

	local btnClose = getLuaComponent(self.uiBtnClose)
	btnClose.onClick = self:OnCloseClick()
end

function ConfirmFrame:SetTitleAndMessage(title, message)
	self.uiTitle:SetText(title)
	self.uiTip.text = message
end

function ConfirmFrame:OnOKClick( ... )
	return function (go)
		if self.onConfirm then
			self.onConfirm()
			NGUITools.Destroy(self.gameObject)
		end
	end
end

--点击关闭事件
function ConfirmFrame:OnCloseClick()
	return function (go)
		NGUITools.Destroy(self.gameObject)
	end
end

return ConfirmFrame

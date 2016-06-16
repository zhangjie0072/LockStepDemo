ButtonClose = {
	uiName = 'ButtonClose',

	----------UI
	uicloseIcon,
	
	----------paremeters
	onClick = nil,
}

function ButtonClose:Awake()
	self.uicloseIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	UIEventListener.Get(self.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClick())
end

function ButtonClose:OnClick()
	return function ()
		if self.onClick then self:onClick() end
	end
end



function ButtonClose:SetCloseIcon(icon,alpha)
	if alpha then
		self.uicloseIcon.alpha = alpha
	end
	if icon then
		self.uicloseIcon.spriteName = icon
	end
end

return ButtonClose

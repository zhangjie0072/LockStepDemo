ButtonReturn = {
	uiName = 'ButtonReturn',

	----------UI
	uireturnIcon,
	uitextLabel,
	
	----------paremeters
	onClick = nil,
	textStr = nil,
}

function ButtonReturn:Awake()
	self.uireturnIcon = self.transform:GetComponent("UISprite")
	self.uitextLabel = self.transform:FindChild("Text"):GetComponent("UILabel")
	UIEventListener.Get(self.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClick())
end

function ButtonReturn:Start()
	 if self.textStr then 
		self.textLabel.text = getCommonStr(self.textStr)
	end
end

function ButtonReturn:OnClick()
	return function ()
	if self.onClick then self.onClick() end
	end
end



function ButtonReturn:set_back_icon(icon,alpha)
	self.uireturnIcon.alpha = alpha
	self.uireturnIcon.spriteName = icon
end

return ButtonReturn

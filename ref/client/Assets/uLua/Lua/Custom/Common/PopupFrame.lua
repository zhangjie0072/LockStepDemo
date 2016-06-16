PopupFrame = {
	uiName = 'PopupFrame',

	BUTTON_COUNT = 2,

	title = "",
	showCorner = false,
	showClose = true,
	onClose = nil,
	buttonLabels = {},
	buttonHandlers = {},
	buttonTypes = {},	--"Sprite", "Normal" (default: Normal)
}


function PopupFrame:Awake()
	self.uiTitleBar = getComponentInChild(self.transform, "TitleBar", "UISprite")
	self.uiTitle = getComponentInChild(self.transform, "TitleBar/Title", "UILabel")
	self.uiClose = getChildGameObject(self.transform, "TitleBar/Close")
	self.uiCorner = getChildGameObject(self.transform, "Corner")
	self.uiButtonNodes = {}
	for i = 1, self.BUTTON_COUNT do
		self.uiButtonNodes[i] = self.transform:FindChild("ButtonNode" .. i)
	end

	if self.showClose then
		addOnClick(self.uiClose, self:MakeOnCloseClick())
	end
end

function PopupFrame:Start()
	self.uiTitle.text = self.title
	NGUITools.SetActive(self.uiCorner, self.showCorner)
	NGUITools.SetActive(self.uiClose, self.showClose)
	for i = 1, self.BUTTON_COUNT do
		local label = self.buttonLabels[i]
		local node = self.uiButtonNodes[i]
		if label and label ~= "" and node then
			local t = self.buttonTypes[i]
			local goButton
			if not t or t == "Normal" then
				goButton = createUI("ButtonOK1", node)
				local uiLabel = getComponentInChild(goButton.transform, "Label", "UILabel")
				uiLabel.text = label
			elseif t == "Sprite" then
				goButton = createUI("ButtonSprite", node)
				local sprite = getComponentInChild(goButton.transform, "Sprite", "UISprite")
				if type(label) == "string" then
					sprite.spriteName = label
				elseif type(label) == "table" then
					sprite.spriteName = label.sprite
					sprite.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/" .. label.atlas)
				end
			end
			--[[
			if i == 2 then	--第二个按钮使用不同的背景图
				goButton:GetComponent("UISprite").spriteName = "com_button_redStroke_yellow"
				goButton:GetComponent("UIButton").normalSprite =  "com_button_redStroke_yellow"
			end
			--]]
			local handler = self.buttonHandlers[i]
			if handler then
				addOnClick(goButton, handler)
			end
		end
	end
end

function PopupFrame:SetTitle(title)
	self.title = title
	self.uiTitle.text = self.title
end

function PopupFrame:MakeOnCloseClick()
	return function ()
		print("PopupFrame close")
		if self.onClose then self:onClose() end
	end
end

function PopupFrame:SetTitleColor(color)
	self.uiTitleBar.color = color
end


return PopupFrame

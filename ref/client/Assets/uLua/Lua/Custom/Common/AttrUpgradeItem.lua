AttrUpgradeItem = {
	uiName = 'AttrUpgradeItem',
	-----------UI
	uiNameLabel,
	uiPrevValue,
	uiCurValue,
	uiArrow,
	--------------parameters
	attrName = nil,
	attrSymbol = "",
	attrID = 0,
	prevValue = 0,
	curValue = 0,
	showPlus = true,
	textColor = nil,
}

function AttrUpgradeItem:Awake()
	self.uiNameLabel = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiPrevValue = getComponentInChild(self.transform, "PrevValue", "UILabel")
	self.uiCurValue = getComponentInChild(self.transform, "CurValue", "UILabel")
	self.uiArrow = getComponentInChild(self.transform, "Arrow", "UISprite")
end

function AttrUpgradeItem:Start()
	if not self.attrName then
		if self.attrSymbol ~= "" then
			self.attrName = GameSystem.Instance.AttrNameConfigData:GetAttrName(self.attrSymbol)
		elseif self.attrID ~= 0 then
			local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(self.attrID);
			self.attrName = GameSystem.Instance.AttrNameConfigData:GetAttrName(symbol)
		end
	end
	if not self.attrName then
		error("AttrListItem: attr name not found. ID:", self.attrID, "Symbol:", self.attrSymbol)
	end
	self.uiNameLabel.text = self.attrName .. ":"
	self.uiPrevValue.text = (self.showPlus and "+" or " ") .. self.prevValue
	self.uiCurValue.text = (self.showPlus and "+" or " ") .. self.curValue
	if self.textColor then
		self.uiNameLabel.color = self.textColor
		self.uiPrevValue.color = self.textColor
		self.uiCurValue.color = self.textColor
	end
	-- self.uiArrow:MakePixelPerfect()
end

return AttrUpgradeItem

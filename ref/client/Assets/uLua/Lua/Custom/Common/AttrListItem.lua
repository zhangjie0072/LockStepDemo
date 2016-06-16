AttrListItem = {
	uiName = 'AttrListItem',

	attrName = nil,
	attrSymbol = "",
	attrID = 0,
	value = 0,
	showPlus = true,
	textColor = nil,
}

function AttrListItem:Awake()
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiValue = getComponentInChild(self.transform, "Value", "UILabel")
end

function AttrListItem:Start()
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
	self.uiName.text = self.attrName .. "："
	self.uiValue.text = (self.showPlus and "+" or " ") .. self.value
	if self.textColor then
		self.uiName.color = self.textColor
		self.uiValue.color = self.textColor
	end
end

return AttrListItem

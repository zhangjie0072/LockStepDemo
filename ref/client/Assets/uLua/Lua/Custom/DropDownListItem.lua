DropDownListItem = {
	uiName = "DropDownListItem",
	----param-----
	index,
	id,
	------UI------
	uiLabel,
	uiNumLabel,

}

function DropDownListItem:Awake( ... )
	-- body
	self.uiLabel = self.transform:GetComponent("UILabel")
	self.uiNumLabel = self.transform:FindChild("Num"):GetComponent("UILabel")
end

function DropDownListItem:Start( ... )
	
end

function DropDownListItem:SetIndex(idx)
	self.index = idx
end

function DropDownListItem:GetIndex( ... )
	return self.index
end

function DropDownListItem:SetLabelText(text)
	self.uiLabel.text = text
end

function DropDownListItem:GetLabelText( ... )
	return self.uiLabel.text
end

function DropDownListItem:SetNum(level)
	self.uiNumLabel.text = level
end

function DropDownListItem:SetId(id)
	-- body
	self.id = id
end

function DropDownListItem:GetId()
	-- body
	return self.id
end

return DropDownListItem
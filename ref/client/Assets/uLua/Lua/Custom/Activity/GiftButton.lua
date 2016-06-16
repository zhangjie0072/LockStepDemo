GiftButton = 
{
	uiName = 'GiftButton',
	----------------------
	onClick,
	--------------------UI
	uiTitle,
	uiSele,
	uiRedDot,
}

function GiftButton:Awake( ... )
	self.uiTitle = self.transform:FindChild('Title'):GetComponent('MultiLabel')
	self.uiSele = self.transform:FindChild('Sele')
	self.uiRedDot = self.transform:FindChild('RedDot')
end

function GiftButton:Start( ... )
	addOnClick(self.gameObject, self:OnItemClick())
end

function GiftButton:Refresh( ... )
end

function GiftButton:OnItemClick( ... )
	return function ()
		if self.onClick then
			local str = self.uiTitle.transform:GetComponent('UILabel').text
			self.onClick(str)
		end
	end
end

function GiftButton:SetTitle(str)
	self.uiTitle:SetText(tostring(str))
end

function GiftButton:SetTipState(state)
	NGUITools.SetActive(self.uiRedDot.gameObject, state)
end

return GiftButton
--encoding=utf-8

PriceItem = {	
	uiName = 'PriceItem',
	
	uiTipsLabel,
	uiValueIcon,
	uiValueNum,
}

function PriceItem:Awake()
	local transform = self.transform
	
	self.uiTipsLabel = transform:FindChild('Tips/Label'):GetComponent('UILabel')
	self.uiTipsLabel.text = CommonFunction.GetConstString('STR_PRICE')
	self.uiValueIcon =transform:FindChild('Value/Icon'):GetComponent('UISprite')
	self.uiValueNum = transform:FindChild('Value/Num'):GetComponent('UILabel')
end

return PriceItem

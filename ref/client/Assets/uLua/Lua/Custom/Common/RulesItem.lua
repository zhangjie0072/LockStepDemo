--encoding=utf-8

RulesItem = {	
	uiName = 'RulesItem',
	
	uiNum,
	uiText,
}

function RulesItem:Awake()
	local transform = self.transform
	
	self.uiNum = transform:FindChild('Num/Label'):GetComponent('UILabel')
	self.uiText = transform:FindChild('Text'):GetComponent('UILabel')
end

--
function RulesItem:SetNum(num)
	self.uiNum.text = num
end

--
function RulesItem:SetText(text)
	self.uiText.text = text
end

return RulesItem

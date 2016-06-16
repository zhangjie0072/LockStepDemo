DaySign = 
{
	uiName = 'DaySign',
	------------------UI
	signBg,
	signLabel,
	vipicon,
	viplabel,
	getAward,
	getAwardHand,
	getAwardLight,
	rightSprite,
	--------------------
	onClick,
}

function DaySign:Awake( ... )
	self.signBg = self.transform:FindChild('BG'):GetComponent('UISprite')
	self.signLabel = self.transform:FindChild('Label'):GetComponent('UILabel')
	addOnClick(self.gameObject, self:OnItemClick())

	self.vipicon = self.transform:FindChild('Vipicon'):GetComponent('UISprite')
	self.viplabel = self.vipicon.transform:FindChild('VipLabel'):GetComponent('UILabel')
	self.getAward = self.transform:FindChild('GetAward'):GetComponent('UISprite')
	self.getAwardHand = self.transform:FindChild('GetAward/GetAwardHand'):GetComponent('UISprite')
	self.getAwardLight = self.transform:FindChild('GetAward/GetAwardLight'):GetComponent('UISprite')
	self.rightSprite = self.transform:FindChild('Right'):GetComponent('UISprite')
end

function DaySign:Start( ... )

end

function DaySign:Update( ... )
	-- body
end

function DaySign:Refresh( ... )
	-- body
end

function DaySign:OnItemClick( ... )
	return function (go)
		if self.onClick ~= nil then
			local day = string.sub(self.signLabel.text, 4, -1)
			self.onClick(day)
		end
	end
end

return DaySign
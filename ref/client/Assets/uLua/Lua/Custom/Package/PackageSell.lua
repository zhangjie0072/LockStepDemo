-- 20150612_142006



PackageSell =  {
	uiName = 'PackageSell',


	------------------------------------
	-------  ui
	uiBtnLabel,
	uiBuyTip,
	uiSellTip,
	uiBuyNum,

	-------------------------
	good_item =nil,
	on_click_sell=nil,
	onOpenBox = nil,
	goods = nil,
	onClose,

	goodsId,
	isSell = true,
	isTreasureBox,
	storeData,
	limitBuyNum,
}

function PackageSell:Awake()
	self.go = {}
	self.go.ps_iconNode = self.transform:FindChild('Window/PS_iconNode').transform
	self.go.ps_infoNode = self.transform:FindChild('Window/PS_infoNode').transform
	self.uiBtnLabel = self.transform:FindChild("Window/SellBtn/Label"):GetComponent("UILabel")
	self.uiBuyTip = self.transform:FindChild("Window/BackType/Buy")
	self.uiSellTip = self.transform:FindChild("Window/BackType/Sell")
	self.uiBuyNum = self.transform:FindChild("Window/BackType/Buy/Num"):GetComponent("UILabel")


	self.go.close_btn = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose')) --getChildGameObject( self.transform,'CloseBtn')
	--addOnClick(self.go.close_btn,self:click_close())

	self.go.sell_btn = getChildGameObject(self.transform,'Window/SellBtn')

	self.go.title = self.transform:FindChild('Window/Title'):GetComponent('UILabel')
end

function PackageSell:click_close()
	return function()
		if self.onClose then
			self.onClose()
		end
		GameObject.Destroy(self.gameObject)
	end
end

function PackageSell:click_sell()
	return function()
		if self.on_click_sell then
			self:on_click_sell()
		end
		if self.onOpenBox then
			self.onOpenBox()
		end
	end
end

function PackageSell:Start()

	local go_ps_icon = createUI('PS_icon', self.go.ps_iconNode)
	self.ps_icon = getLuaComponent(go_ps_icon)
	self.ps_icon.isTreasureBox = self.isTreasureBox

	local go_ps_info = createUI('PS_info',self.go.ps_infoNode)
	self.ps_info = getLuaComponent(go_ps_info)
	self.ps_info.isTreasureBox = self.isTreasureBox
	addOnClick(self.go.sell_btn,self:click_sell())


	local btnClose = getLuaComponent(self.go.close_btn)
	btnClose.onClick = self:click_close()
	self.ps_icon.good_item = self.good_item
	self.ps_info.good_item = self.good_item
	self.ps_info.isSell = self.isSell
	self.ps_info.goodsId = self.goodsId
	self.ps_info.ps = self


	self.ps_icon.goods = self.goods
	self.ps_icon.isSell = self.isSell
	self.ps_icon.goodsId = self.goodsId
	self.ps_icon.ps = self


	self.uiBuyTip.gameObject:SetActive(not self.isSell)
	self.uiSellTip.gameObject:SetActive(self.isSell)

	if self.limitBuyNum then
		self.uiBuyNum.text = self.limitBuyNum
	end

	self:UpdateTitle()
end

function PackageSell:UpdateTitle()
	local s={'STR_SELL','STR_SELL','STR_SELL','STR_SELL'}
	local str = getCommonStr( "STR_BUY")
	if self.isSell then
		str = getCommonStr( s[self.good_item.tab +1])
	end
	self.go.title.text = str
	self.uiBtnLabel.text = str

	if self.isTreasureBox then
		self.go.title.text = getCommonStr('BATCH_USE')
		self.uiBtnLabel.text = getCommonStr('BATCH_USE')
	end
end



return PackageSell

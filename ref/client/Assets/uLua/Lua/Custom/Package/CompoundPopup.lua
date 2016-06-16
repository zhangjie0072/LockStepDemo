CompoundPopup =  {
	uiName = 'CompoundPopup',
	-----------------------
	---------------------UI
	uiCostName,
	uiCostNumLabel,
	uiOwnedNumLabel,
	uiCostGrid,
	uiGetName,
	uiGetNumLabel,
	uiGetGrid,
	uiCounsume,
	uiButton,
	--parameters
	onClick,
	onClose,
	costID,
	costGoods,
}

function CompoundPopup:Awake()
	self.uiCostName = self.transform:FindChild("Window/BuyInfo/CostIcon/Name"):GetComponent("UILabel")
	self.uiCostNumLabel = self.transform:FindChild("Window/BuyInfo/CostIcon/CostNum"):GetComponent("UILabel")
	self.uiOwnedNumLabel = self.transform:FindChild("Window/BuyInfo/CostIcon/OwnedNum"):GetComponent("UILabel")
	self.uiCostGrid = self.transform:FindChild("Window/BuyInfo/CostIcon/GoodsIcon")
	self.uiGetName = self.transform:FindChild("Window/BuyInfo/GetIcon/Name"):GetComponent("UILabel")
	self.uiGetNumLabel = self.transform:FindChild("Window/BuyInfo/GetIcon/Num"):GetComponent("UILabel")
	self.uiGetGrid = self.transform:FindChild("Window/BuyInfo/GetIcon/GoodsIcon")
	self.uiCounsume = self.transform:FindChild("Window/Consume/GoodsIconConsume")
	self.uiButton = self.transform:FindChild("Window/ButtonOK")
	self.uiNodeClose = self.transform:FindChild("Window/ButtonClose")

	self.uiAnimator = self.transform:GetComponent("Animator")

	addOnClick(self.uiButton.gameObject, self:OnClick())
end

function CompoundPopup:Start()
	local close = getLuaComponent(createUI("ButtonClose", self.uiNodeClose))
	close.onClick = self:Close()
	self:Refresh()
	--[[
	local consume = getLuaComponent(self.uiCounsume)
	consume.rewardId = 2
	consume.rewardNum = costInfo.costing
	consume.isAdd = false
	--]]
end

function CompoundPopup:Refresh()
	while self.uiCostGrid.transform.childCount > 0 do
		NGUITools.Destroy(self.uiCostGrid.transform:GetChild(0).gameObject)
	end
	while self.uiGetGrid.transform.childCount > 0 do
		NGUITools.Destroy(self.uiGetGrid.transform:GetChild(0).gameObject)
	end
	--init cost info
	local cost = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.costID)
	self.uiCostName.text = cost.name
	local ownedValue = MainPlayer.Instance:GetGoodsCount(self.costID)
	local costInfo = GameSystem.Instance.GoodsConfigData:GetComposite(self.costID)
	self.uiCostNumLabel.text = "/" .. costInfo.src_num
	if costInfo.src_num > ownedValue then
		local color = Split(self["colorRed"],"/")
		self.uiOwnedNumLabel.color = Color.New(color[1]/255,color[2]/255,color[3]/255,color[4]/255)
	else
		local color = Split(self["colorOra"],"/")
		self.uiOwnedNumLabel.color = Color.New(color[1]/255,color[2]/255,color[3]/255,color[4]/255)
	end
	self.uiOwnedNumLabel.text = ownedValue
	local costIcon = getLuaComponent(createUI("GoodsIcon",self.uiCostGrid))
	costIcon.goods = self.costGoods
	costIcon.hideLevel = true
	costIcon.hideNeed = true
	--init get info 
	local getID = costInfo.dest_id
	local get = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(getID)
	self.uiGetName.text = get.name
	self.uiGetNumLabel.text = costInfo.dest_num
	local getIcon = getLuaComponent(createUI("GoodsIcon",self.uiGetGrid))
	getIcon.goodsID = getID
	getIcon.hideLevel = true
	getIcon.hideNeed = true
end

function CompoundPopup:OnClick()
	return function()
		if self.onClick then self:onClick() end
	end
end

function CompoundPopup:Close()
	return function ( ... )
		if self.onClose then
			self.onClose()
		end
		self:AnimClose()
	end
end

function CompoundPopup:OnClose()
	NGUITools.Destroy(self.gameObject)
end

return CompoundPopup

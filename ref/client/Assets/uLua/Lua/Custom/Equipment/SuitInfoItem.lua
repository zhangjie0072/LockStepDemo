--encoding=utf-8

SuitInfoItem = {
	uiName = "SuitInfoItem",

	-------------PARAMETERS
	goods,
	equipGoodsList = {},

	componentHeight = 0,

	parentBg,

	refreshParentSV,

	-----------------UI
	uiTitle,

	uiPartsGrid,
	uiAttrGrid,

	uiParts,
}


-----------------------------------------------------------------
function SuitInfoItem:Awake()
	local transform = self.transform

	self.uiTitle = transform:FindChild('Title'):GetComponent('MultiLabel')
	self.componentHeight = self.componentHeight + transform:FindChild('Title'):GetComponent('UILabel').height

	self.uiPartsGrid = transform:FindChild('PartsGrid'):GetComponent('UIGrid')
	self.uiAttrGrid = transform:FindChild('AttrGrid'):GetComponent('UIGrid')

	self.uiParts = {}
	self.uiParts[1] = self.uiPartsGrid.transform:FindChild('Head'):GetComponent('MultiLabel')
	self.uiParts[2] = self.uiPartsGrid.transform:FindChild('Chest'):GetComponent('MultiLabel')
	self.uiParts[3] = self.uiPartsGrid.transform:FindChild('Bracer'):GetComponent('MultiLabel')
	self.uiParts[4] = self.uiPartsGrid.transform:FindChild('Pants'):GetComponent('MultiLabel')
	self.uiParts[5] = self.uiPartsGrid.transform:FindChild('Shoe'):GetComponent('MultiLabel')
	self.componentHeight = self.componentHeight + (self.uiPartsGrid.transform:FindChild('Head'):GetComponent('UILabel').height * 5)

	self.uiSuitAttrGrid = transform:FindChild('AttrGrid'):GetComponent('UIGrid')
end

function SuitInfoItem:Start()
	self:Refresh()
end

function SuitInfoItem:FixedUpdate()
	-- body
end

function SuitInfoItem:OnClose()
	-- body
end

function SuitInfoItem:OnDestroy()	
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function SuitInfoItem:Refresh()	
	self:Init()
end


-----------------------------------------------------------------
function SuitInfoItem:Init( ... )
	print('-------------self.goods:IsSuit(): ', self.goods:IsSuit())
	if self.goods:IsSuit() == false then
		self:OnDestroy()
		return
	end

	local colorGray = Color.New(135/255, 135/255, 135/255, 1)
	local colorLight = Color.New(254/255, 131/255, 52/255, 1)

	local attrNameConfig = GameSystem.Instance.AttrNameConfigData

	for k, v in pairs(self.uiParts) do
		v:SetText(self.goods:GetName() .. getCommonStr("STR_SUIT_PART" .. k))
		v:SetColor(colorGray)
	end

	local goodsID = self.goods:GetID()
	local subCategory = self.goods:GetSubCategory()
	local suitInfo = GameSystem.Instance.GoodsConfigData:GetSuitAttrConfig(goodsID)
	if suitInfo then
		local suitNum = 0
		local index = enumToInt(subCategory)
		for k, v in pairs(self.equipGoodsList) do
			if v:GetSuitID() == suitInfo.suitID then
				index = enumToInt(v:GetSubCategory())
				self.uiParts[index]:SetColor(colorLight)

				suitNum = suitNum + 1
			end
		end

		print('----------suitNum: ', suitNum)
		--套装属性
		CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
		local enumAddnNum = suitInfo.addn_attr:GetEnumerator()
		local deCount = 2
		while enumAddnNum:MoveNext() do
			local enumAddnItem = enumAddnNum.Current.Value:GetEnumerator()
			while enumAddnItem:MoveNext() do
				local suitAttrObj = createUI('SuitAttr', self.uiAttrGrid.transform)
				local suitWidget = suitAttrObj.transform:GetComponent('UIWidget')
				self.componentHeight = self.componentHeight + suitWidget.height
				local addnHand = suitAttrObj.transform:FindChild('Hand'):GetComponent('MultiLabel')
				addnHand:SetText('（'.. deCount .. '）')

				local symbol = attrNameConfig:GetAttrSymbol(enumAddnItem.Current.Key)
				local name = attrNameConfig:GetAttrName(symbol)
				local addnName = suitAttrObj.transform:FindChild('AttrInfo2/Name'):GetComponent('MultiLabel')
				addnName:SetText(name)

				local addnValue = suitAttrObj.transform:FindChild('AttrInfo2/Value'):GetComponent('MultiLabel')
				addnValue:SetText('+' .. enumAddnItem.Current.Value)

				if deCount <= suitNum then
					addnHand:SetColor(colorLight)
					addnName:SetColor(colorLight)
					addnValue:SetColor(colorLight)
				else
					addnHand:SetColor(colorGray)
					addnName:SetColor(colorGray)
					addnValue:SetColor(colorGray)
				end
			end

			--乘系数属性
			local enumMultiItem = suitInfo.multi_attr:get_Item(enumAddnNum.Current.Key):GetEnumerator()
			while enumMultiItem:MoveNext() do
				local suitAttrObj = createUI('SuitAttr', self.uiAttrGrid.transform)
				local suitWidget = suitAttrObj.transform:GetComponent('UIWidget')
				self.componentHeight = self.componentHeight + suitWidget.height
				local addnHand = suitAttrObj.transform:FindChild('Hand'):GetComponent('MultiLabel')
				addnHand:SetText('（'.. deCount .. '）')

				local symbol = attrNameConfig:GetAttrSymbol(enumMultiItem.Current.Key)
				local name = attrNameConfig:GetAttrName(symbol)
				local addnName = suitAttrObj.transform:FindChild('AttrInfo2/Name'):GetComponent('MultiLabel')
				addnName:SetText(name)
				print(self.uiName,":name:",name)
				local addnValue = suitAttrObj.transform:FindChild('AttrInfo2/Value'):GetComponent('MultiLabel')
				addnValue:SetText(string.format(getCommonStr("STR_ATTR_MULTI"), enumMultiItem.Current.Value/10))

				if deCount <= suitNum then
					addnHand:SetColor(colorLight)
					addnName:SetColor(colorLight)
					addnValue:SetColor(colorLight)
				else
					addnHand:SetColor(colorGray)
					addnName:SetColor(colorGray)
					addnValue:SetColor(colorGray)
				end
			end
			print("=========================================")
			deCount = deCount + 1
		end
		self.uiAttrGrid.repositionNow = true

		--
		self.uiTitle:SetText(self.goods:GetName() .. '（' .. suitNum .. '/' .. table.getn(self.uiParts) .. '）')
	end

	if self.parentBg then
		self.parentBg.height = self.parentBg.height + self.componentHeight + 50
	end

	if self.refreshParentSV then
		self.refreshParentSV()
	end
end

-----------------------------------------------------------------

return SuitInfoItem
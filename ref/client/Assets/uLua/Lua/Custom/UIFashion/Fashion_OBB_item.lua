--encoding=utf-8

Fashion_OBB_item =  {
	uiName="Fashion_OBB_item",

	----------------------------------
	id,
	onChoose,
	isReputation = false,
	--钻石:0 声望:1
	realCostType,
	----------------------------------UI
	uiGoodsIcon,
	uiGoodsType,
	uiGoodsValue,
	uiCheckBox,
}


-----------------------------------------------------------------
function Fashion_OBB_item:Awake()
	self.uiGoodsIcon = getLuaComponent(createUI('GoodsIcon', self.transform:FindChild('FashionIcon')))
	self.uiGoodsType = self.transform:FindChild('Cost/CostType'):GetComponent('UISprite')
	self.uiGoodsValue = self.transform:FindChild('Cost/CostNum'):GetComponent('UILabel')
	self.uiCheckBox = self.transform:FindChild('CheckBox'):GetComponent('UIToggle')
end

function Fashion_OBB_item:Start()
	if self.realCostType == 0 then
		self.uiGoodsType.spriteName = 'com_property_diamond'
	else
		self.uiGoodsType.spriteName = 'com_property_reputation'
	end
	EventDelegate.Add(self.uiCheckBox.onChange, LuaHelper.Callback(self:OnChooseClick()))
end

function  Fashion_OBB_item:FixedUpdate()
end

function Fashion_OBB_item:OnClose()
end

function Fashion_OBB_item:OnDestroy()
	-- body
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function Fashion_OBB_item:Refresh()
end


-----------------------------------------------------------------
function Fashion_OBB_item:SetData(id)
	self.id = id
	self.uiGoodsIcon.goodsID = id
	self.uiGoodsIcon.hideLevel = true
	self.uiGoodsIcon.hideNeed = true
	local num = 0
	local fashionShopConfig = nil
	if not self.isReputation then
		fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(id)
	else
		fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(id)
	end
	local enumDis = fashionShopConfig._discountCost:GetEnumerator()
	while enumDis:MoveNext() do
		num = enumDis.Current
		break
	end
	if num == 0 then
		local enumNum = fashionShopConfig._costNum:GetEnumerator()
		while enumNum:MoveNext() do
			num = enumNum.Current
			break
		end
	end
	self.uiGoodsValue.text = num
end

function Fashion_OBB_item:OnChooseClick( ... )
	return function (go)
		local value = self.uiCheckBox.value
		if self.onChoose then
			self.onChoose(self.id, value)
		end
		--self.uiCheckBox.value = not self.uiCheckBox.value
	end
end

return Fashion_OBB_item
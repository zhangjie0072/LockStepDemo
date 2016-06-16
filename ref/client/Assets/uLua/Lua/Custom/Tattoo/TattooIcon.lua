TattooIcon = {
	uiName = 'TattooIcon',
	
	uuid = 0,

	onClick = nil,
	onReduce = nil,

	--accessible
	--goods
	--selectedNum
	--tattooConfig
}

function TattooIcon:Awake()
	self.uiReduce = getChildGameObject(self.transform, "Reduce")
	self.uiNum = getComponentInChild(self.transform, "Num", "UILabel")

	addOnClick(self.uiReduce, self:MakeOnReduce())
	addOnClick(self.gameObject, self:MakeOnClick())
end

function TattooIcon:Start()
	self.goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, self.uuid)
	self.tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), self.goods:GetLevel())
	if not self.tattooConfig then 
		error("TattooIcon: can not find tattoo config. ID:", self.goods:GetID(), "Level:", self.goods:GetLevel()) 
	end

	self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.transform))
	self.uiIcon.goods = self.goods
	self.uiIcon.hideLevel = (self.goods:GetSubCategory() == TattooType.TT_PIECE)
	addOnClick(self.uiIcon.gameObject, self:MakeOnClick())
	self.totalNum = self.goods:GetNum()
	NGUITools.SetActive(self.uiNum.gameObject, self.goods:GetSubCategory() == TattooType.TT_PIECE)

	self.selectedNum = 0
	self:Select(0)
end

function TattooIcon:Select(increaseNum)
	self.selectedNum = self.selectedNum + increaseNum
	if self.selectedNum > self.totalNum then self.selectedNum = self.totalNum end
	if self.selectedNum < 0 then self.selectedNum = 0 end
	NGUITools.SetActive(self.uiReduce, self.selectedNum ~= 0)
	self.uiNum.text = self.selectedNum .. "/" .. self.totalNum
end

function TattooIcon:MakeOnClick()
	return function ()
		if self.onClick then self:onClick() end
	end
end

function TattooIcon:MakeOnReduce()
	return function ()
		if self.onReduce then self:onReduce() end
	end
end

return TattooIcon

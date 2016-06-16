GoodsIconNumBottom = {
	uiName = 'GoodsIconNumBottom',
	
	goodsID = 0,
	num = 0,
}

function GoodsIconNumBottom:Awake()
	self.uiNum = getComponentInChild(self.transform, "Num", "UILabel")
end

function GoodsIconNumBottom:Start()
	self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.transform))
	self.uiIcon.hideLevel = true
	--self.uiIcon.hideSide = self.goodsID < 100
	self.uiIcon.goodsID = self.goodsID
	self.uiNum.text = tostring(self.num)
end

function GoodsIconNumBottom:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

return GoodsIconNumBottom

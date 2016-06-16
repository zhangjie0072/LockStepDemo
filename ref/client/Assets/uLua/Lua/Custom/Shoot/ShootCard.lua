require "common/stringUtil"
ShootCard = {
	uiName = "ShootCard",
	
	--ui
	uiCardBack,

	uiFront,
	uiName2,
	uiNum2,

	uiName1,
	uiNum1,

	uiIcon,
	uiAnimator,

	--parameters
	isClick = nil,
	index,
	parent,
	remianTimes = nil,

	useAsIcon = false,

	GoodsAtlas =  {
	property = "IconGoods",
	goods = "IconGoods",
	piece = "IconPiece",
	skill = "IconSkill",
	tattoo = "IconTattoo",
	equipment = "IconEquipment",
	fashion = "IconFashion",
	signin = "IconGoods",
	portrait = "IconPortrait",
	train = "IconTrain",
}
	
}

function ShootCard:Awake()
	--back
	self.uiCardBack = self.transform:FindChild("CardBack")
	--front
	self.uiFront = self.transform:FindChild("CardFront")
	self.uiName2 = self.transform:FindChild("CardFront/Name2"):GetComponent("UILabel")
	self.uiNum2 = self.transform:FindChild("CardFront/Num2"):GetComponent("UILabel")
	self.uiName1 = self.transform:FindChild("CardFront/Name1"):GetComponent("UILabel")
	self.uiNum1 = self.transform:FindChild("CardFront/Num1"):GetComponent("UILabel")
	self.uiIconGrid = self.transform:FindChild("CardFront/Icon")
	self.uiAnimator = self.transform:GetComponent("Animator")
	self.uiSele = self.transform:FindChild("Sele")
	addOnClick(self.uiCardBack.gameObject, self:ClickIcon())

	self.uiName1.gameObject:SetActive(self.useAsIcon)
	self.uiNum1.gameObject:SetActive(self.useAsIcon)
	self.uiName2.gameObject:SetActive(not self.useAsIcon)
	self.uiNum2.gameObject:SetActive(not self.useAsIcon)
end

function ShootCard:Start()
	
end

function ShootCard:SetFront(id , value, name, icon, isTrigger)
	if useAsIcon then
		self.uiName1.text = name
		self.uiNum1.text = value
		self.uiIcon.spriteName = icon
	else
		local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
		-- if id == GlobalConst.DIAMOND_ID then
		-- 		self.uiIcon.spriteName = 'icon_signin_diamond'
		-- elseif id == GlobalConst.GOLD_ID then
		-- 	self.uiIcon.spriteName = 'icon_signin_gold'
		-- elseif id == GlobalConst.HP_ID then
		-- 	self.uiIcon.spriteName = 'icon_signin_hp'
		-- elseif id == GlobalConst.HONOR_ID then
		-- 	self.uiIcon.spriteName = 'icon_signin_honor'
		-- elseif id == GlobalConst.PRESTIGE_ID then
		-- 	self.uiIcon.spriteName = 'icon_signin_prestige'
		-- elseif self.goodsID == GlobalConst.HONOR2_ID then
		-- 	self.uiIcon.spriteName = "icon_signin_honor1"
		-- elseif self.goodsID == GlobalConst.PRESTIGE2_ID then
		-- 	self.uiIcon.spriteName = "icon_signin_prestige1"
		-- else
		local child
		if self.uiIconGrid.transform.childCount > 0 then
			child = self.uiIconGrid.transform:GetChild(0)
		else
			child = createUI("GoodsIcon",self.uiIconGrid)
		end
		self.goodsLuaCom = getLuaComponent(child)
		self.goodsLuaCom.goodsID = id
		self.goodsLuaCom.showTips = false
		self.goodsLuaCom.hideNeed = true
		self.goodsLuaCom.hideNum = true
		self.goodsLuaCom.hideLevel = true
		self.goodsLuaCom:Refresh()
		self.uiName2.text = attr.name
		self.uiNum2.text = value

		if GoodsCategory.IntToEnum(attr.category) == GoodsCategory.GC_EQUIPMENT then
			NGUITools.SetActive(self.uiNum2.gameObject, false)
		else
			NGUITools.SetActive(self.uiNum2.gameObject, true)
		end

		if isTrigger == true then
			self.uiAnimator.enabled = true
			self.uiAnimator:SetTrigger("Turn")
		end
	end
	-- NGUITools.SetActive(self.uiCardBack.gameObject, false)
	-- NGUITools.SetActive(self.uiFront.gameObject, true)
end

function ShootCard:ClickIcon()
	return function()
		if self.onClick then self:onClick() end
	end
end

function ShootCard:OnClose()
	self.parent.clickState = false
	if self.remianTimes == 0 then
		self.parent:ShowElseCard()
	end

end

return ShootCard
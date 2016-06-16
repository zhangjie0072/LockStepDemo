NewSign =
{
	uiName = 'NewSign',
	------------------UI
	uiDay,
	uiGoodsIcon,
	-- uiGoodsIconConsume,
	-- uiGoodsIconConsumeID,
	-- uiGoodsIconConsumeNum,
	uiSignFlag,
	uiAnimator,
	uiStateIcon,
	uiGoodsName,
	-------------------
	onClick,
	isCurrentDay = false,
	changeLight,
}

local signState =
{
	signed     = 1,
	signing    = 2,
	appendSign = 3,
	neverSign  = 4,
}

function NewSign:Awake( ... )
	self.uiDay = self.transform:FindChild('Label'):GetComponent('MultiLabel')
	self.uiGoodsIcon = self.transform:FindChild('GoodsIcon/GoodsIcon')
	-- self.uiGoodsIconConsume = self.transform:FindChild('GoodsIconConsume')
	-- self.uiGoodsIconConsumeID = self.uiGoodsIconConsume:FindChild('Icon'):GetComponent('UISprite')
	-- self.uiGoodsIconConsumeNum= self.uiGoodsIconConsume:FindChild('Num'):GetComponent('UILabel')
	self.uiSignFlag = self.transform:FindChild('Right')
	self.uiStateIcon = self.transform:FindChild('BG2'):GetComponent('UISprite')
	self.uiGoodsName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiSigningFlag = self.uiGoodsIcon:FindChild('Ef_Sele'):GetComponent('UISprite')
	-- self.uiAnimator = self.transform:GetComponent('Animator')
end

function NewSign:Start( ... )
	addOnClick(self.gameObject, self:OnSignClick())
end

function NewSign:Update( ... )
	-- body
end

function NewSign:Refresh( ... )
	-- body
end
-------------------------------

function NewSign:SetData(day, state)
	NGUITools.SetActive(self.uiSignFlag.gameObject, false)
	-- NGUITools.SetActive(self.uiGoodsIconConsume.gameObject, false)

	self.uiDay:SetText('Day' .. tostring(day))
	local goodsID = GameSystem.Instance.NewComerSignConfig:GetDayAwardType(day)
	local goodsNum = GameSystem.Instance.NewComerSignConfig:GetDayAwardNum(day)
	local goods = getLuaComponent(self.uiGoodsIcon.gameObject)
	goods.goodsID = goodsID
	goods.num = goodsNum
	goods.hideNeed = true
	goods.hideLevel = true
	goods.hideNum = false
	goods.hideLight = not self.isCurrentDay

	self.uiStateIcon.gameObject:SetActive(false)
	self.uiSigningFlag.gameObject:SetActive(false)

	local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsID)

	self.uiGoodsName.text = tostring(goodsConfig.name)

	if state == signState.appendSign then
		print ("there has a appendsign")
		self.uiStateIcon.gameObject:SetActive(true)
		self.uiStateIcon.spriteName = 'sign_bg03'
		-- self.uiGoodsIconConsume.gameObject:SetActive(true)
		local appendID = GameSystem.Instance.NewComerSignConfig:GetConsumeType(day)
		local appendNum = GameSystem.Instance.NewComerSignConfig:GetConsumeNum(day)

		-- local goods_attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(appendID)
		-- local icon = goods_attr.icon
		-- if appendID == 1 then
		-- 	self.uiGoodsIconConsumeNum.color = Color.New(138/255, 216/255, 254/255, 1)
		-- elseif appendID == 2 then
		-- 	self.uiGoodsIconConsumeNum.color = Color.New(250/255, 184/255, 62/255, 1)
		-- elseif appendID == 5 then
		-- 	self.uiGoodsIconConsumeNum.color = Color.New(252/255 , 0, 255/255, 1)
		-- end
		-- self.uiGoodsIconConsumeID.spriteName = icon
		-- self.uiGoodsIconConsumeNum.text = appendNum
	elseif state == signState.signed then
		self.uiSignFlag.gameObject:SetActive(true)
		self.uiStateIcon.gameObject:SetActive(true)
		self.uiStateIcon.spriteName = 'sign_yilingqu'
	elseif state == signState.signing then
		self.uiSigningFlag.gameObject:SetActive(true)
	end
end

function NewSign:OnSignClick( ... )
	return function (go)
		if self.onClick then
			self.onClick(go)
		end
	end
end

function NewSign:SetSignedState(types)
	NGUITools.SetActive(self.uiSignFlag.gameObject, true)
	-- NGUITools.SetActive(self.uiGoodsIconConsume.gameObject, false)
	self.uiStateIcon.gameObject:SetActive(true)
	self.uiStateIcon.spriteName = 'sign_yilingqu'
	if types == 'NCST_SIGN' and self.changeLight then
		self.changeLight(self)
	end
end

return NewSign
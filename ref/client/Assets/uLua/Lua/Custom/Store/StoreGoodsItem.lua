--encoding=utf-8

StoreGoodsItem = 
{
	uiName = 'StoreGoodsItem',
	
	-------------------------------------
	pos,
	config,
	goodsID,

	sellout = false,

	parent,

	-------------------------------------UI
	uiBgNormal,
	uiBgSelect,
	uiBgSellout,

	uiName,
	uiGoodsIcon,
	uiNum,

	uiBuyInfo,
	uiBuyBgNormal,
	uiBuyBgSellout,
	uiBuyLabel,
	uiCostType,
	uiCostValue,

	uiDragSV1,
	uiDragSV2,

	m = {
		black =1,
		skill = 2,
	},

	mode,
	num,
	level,

};


-----------------------------------------------------------------
--Awake
function StoreGoodsItem:Awake()
	self.mode = self.m.black
	local transform = self.transform
	--背景
	self.uiBgNormal = transform:FindChild('BgNormal').gameObject
	self.uiBgSelect = transform:FindChild('BgSelect').gameObject
	self.uiBgSellout = transform:FindChild('SoldOut').gameObject
	--基础信息
	self.uiName = transform:FindChild('Name'):GetComponent('UILabel')
	self.uiGoodsIcon = self.transform:FindChild('GoodsIcon') --createUI('GoodsIcon', transform:FindChild('GoodsIcon').transform)
	--self.uiNum = transform:FindChild('Num'):GetComponent('UILabel')
	--贩卖信息
	self.uiBuyInfo = transform:FindChild('BuyInfo').gameObject
	self.uiCostType = transform:FindChild('BuyInfo/CostType'):GetComponent('UISprite')
	self.uiCostValue = transform:FindChild('BuyInfo/CostValue'):GetComponent('UILabel')
	--[[
	self.uiBuyBgNormal = transform:FindChild('BuyInfo/BgNormal').gameObject
	self.uiBuyBgSellout = transform:FindChild('BuyInfo/BgSellout').gameObject
	self.uiBuyLabel = transform:FindChild('BuyInfo/Label'):GetComponent('UILabel')
	self.uiBuyLabel.text = CommonFunction.GetConstString('STR_BUY')

	--Drag ScrollView
	self.uiDragSV1 = transform:GetComponent('UIDragScrollView')
	self.uiDragSV2 = transform:FindChild('BuyInfo'):GetComponent('UIDragScrollView')
	self.uiBgNormal_bg1 = transform:FindChild("BuyInfo/BgNormal/1"):GetComponent("UISprite")
	--]]

end

--Start
function StoreGoodsItem:Start()
	--addOnClick(self.uiGoodsIcon.gameObject, self:OnIconClick())
	local goodsIcon = getLuaComponent(createUI("GoodsIcon",self.uiGoodsIcon))
	goodsIcon.goodsID = self.goodsID
	goodsIcon.hideLevel = true
	goodsIcon.hideNeed = true
	goodsIcon.hideNum = false
	goodsIcon.num = self.num
	goodsIcon.showTips = false
	goodsIcon.onClick = self:OnIconClick()

	local wColors = string.split(self.nameColorW, ",")
	local gColors = string.split(self.nameColorG, ",")
	local bColors = string.split(self.nameColorB, ",")
	local pColors = string.split(self.nameColorP, ",")
	local oColors = string.split(self.nameColorO, ",")
	local nameColors = {
		Color.New(wColors[1]/255, wColors[2]/255, wColors[3]/255, wColors[4]/1),
		Color.New(gColors[1]/255, gColors[2]/255, gColors[3]/255, gColors[4]/1),
		Color.New(bColors[1]/255, bColors[2]/255, bColors[3]/255, bColors[4]/1),
		Color.New(pColors[1]/255, pColors[2]/255, pColors[3]/255, pColors[4]/1),
		Color.New(oColors[1]/255, oColors[2]/255, oColors[3]/255, oColors[4]/1)
	}
	self.uiName.color = nameColors[self.config.quality]
	self:Refresh()
end

--Update
function StoreGoodsItem:Update( ... )
	-- body
end


function StoreGoodsItem:Refresh()
	local mode = self.mode
	if mode == self.m.black then
		--self.uiBuyLabel.text = CommonFunction.GetConstString('STR_NUMBER')..tostring(self.num)
		-- NGUITools.SetActive(self.uiGoodsIcon.uiLevel.gameObject,true)
		-- NGUITools.SetActive(self.uiGoodsIcon.uiLevelBG.gameObject,true)
		-- self.uiGoodsIcon.uiLevel.text = "Lv." .. tostring(self.level)
		--self.uiBgNormal_bg1.spriteName = "com_bg_pure_round_blue"
	elseif mode == self.m.skill then
		--self.uiBgNormal_bg1.spriteName = "com_bg_pure_round_greenlight"
		--local id = self.uiGoodsIcon.goodsID
		--local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(id)
		--self.uiBuyLabel.text = getCommonStr("STR_TYPE") .. SkillUtil.GetActionTypeName(skillAttr.action_type)

	end
end

-----------------------------------------------------------------
--
function StoreGoodsItem:SetParent(parent)
	self.parent = parent
end

--
function StoreGoodsItem:OnIconClick( ... )
	return function (go)
	print("click----")
		if self.sellout then
			return
		end
		self.parent:OnItemClick()(self.gameObject)
	end
end

--
function StoreGoodsItem:Normal()
	if self.sellout then
		return
	end

	self.uiBgNormal:SetActive(true)
	self.uiBgSelect:SetActive(false)
	self.uiBgSellout:SetActive(false)
	--self.uiBuyBgNormal:SetActive(true)
	--self.uiBuyBgSellout:SetActive(false)
end

--
function StoreGoodsItem:Select()
	if self.sellout then
		return
	end

	-- self.uiBgNormal:SetActive(false)
	self.uiBgSelect:SetActive(true)
	self.uiBgSellout:SetActive(false)
end

--
function StoreGoodsItem:Sellout()
	self.sellout = true

	self.uiBgNormal:SetActive(false)
	self.uiBgSelect:SetActive(false)
	self.uiBgSellout:SetActive(true)
	--self.uiBuyBgNormal:SetActive(false)
	--self.uiBuyBgSellout:SetActive(true)
end

return StoreGoodsItem

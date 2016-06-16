--encoding=utf-8

EquipmentDetail = {
	uiName = "EquipmentDetail",

	-------------PARAMETERS
	goods,
	equipGoodsList = {},
	needResetPosition = false,
	inStore = false,

	-----------------UIs
	uiCategory,
	uiIcon,
	uiDescribeDetail,
	uiScrollBar,
	uiScrollView,
	uiScrollViewPanel,
	uiAttrGrid,

	uiSuitParts,
}

local EquipSubCategory = 
{
	"STR_EQUIP_HEAD",
	"STR_EQUIP_CLOTHES",
	"STR_EQUIP_HAND",
	"STR_EQUIP_PANTS",
	"STR_EQUIP_SHOES",
	"STR_EQUIP_PIEACE",
}

local BadgeSubCategory = 
{
	'STR_BADGE_ATTACK',
	'STR_BADGE_DEFENSE',
	'STR_BADGE_SKILL',
	'STR_BADGE_TACTICS',
}

-----------------------------------------------------------------
function EquipmentDetail:Awake()
	local transform = self.transform

	self.uiName = transform:FindChild('Name'):GetComponent('MultiLabel')

	self.uiCategory = transform:FindChild('Category'):GetComponent('MultiLabel')
	self.uiIcon = transform:FindChild('GoodsIcon')
	self.uiDescribeDetail = transform:FindChild('Describe/Detail'):GetComponent('UILabel')
	self.uiScrollBar = transform:FindChild('Schedule'):GetComponent('UIScrollBar')
	self.uiScrollView = transform:FindChild('Scroll'):GetComponent('UIScrollView')
	self.uiScrollViewPanel = transform:FindChild('Scroll'):GetComponent('UIPanel')
	self.uiAttrGrid = transform:FindChild('Scroll/AttrGrid'):GetComponent('UIGrid')

	self.uiSuitParts = transform:FindChild('Scroll/SuitParts').gameObject
end

function EquipmentDetail:Start()
	-- body
end

function EquipmentDetail:FixedUpdate()
	if self.needResetPosition then
		self.uiScrollView:ResetPosition()
		self.needResetPosition = false
	end
end

function EquipmentDetail:OnClose()
	-- body
end

function EquipmentDetail:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function EquipmentDetail:Refresh()
	self:Init()
end


-----------------------------------------------------------------
function EquipmentDetail:Init( ... )
	if self.goods:IsSuit() then
		self.uiName:SetText(self.goods:GetName() .. self.goods:GetSubName())
	else
		self.uiName:SetText(self.goods:GetName())
	end

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
	local quality = enumToInt(self.goods:GetQuality())
	--self.uiName:SetColor(nameColors[quality])

	local child
	if self.uiIcon.childCount > 0 then
		child = self.uiIcon:GetChild(0)
	end
	if child == nil then
		child = createUI('GoodsIcon', self.uiIcon)
	end
	local icon = getLuaComponent(child)
	icon.goods = self.goods
	icon.hideNeed = true
	icon.hideLevel = true
	icon:Refresh()
	local goodsID = self.goods:GetID()
	local goodsLevel = self.goods:GetLevel()

	local config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsID)
	self.uiDescribeDetail.text = config.intro

	CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
	if self.goods:GetCategory() == GoodsCategory.GC_BADGE then
		local badgeAttrConfigData = GameSystem.Instance.BadgeAttrConfigData:GetBaseConfig(self.goods:GetID())
		if badgeAttrConfigData then
			local addAttrlist = badgeAttrConfigData.addAttr
			local enum = addAttrlist:GetEnumerator()
			while enum:MoveNext() do
				local attrId = enum.Current.Key
				local attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(attrId)
				local attrNum = enum.Current.Value
				local item = createUI("AttrInfo",self.uiAttrGrid.transform)
				item:GetComponent("BoxCollider").enabled = false
				local attrItem = getLuaComponent(item)
				attrItem:SetName(attrName)
				attrItem:SetValue("+"..attrNum,true)
			end
		end
	else
		local attrNameConfig = GameSystem.Instance.AttrNameConfigData
		local equipmentConfig = GameSystem.Instance.EquipmentConfigData
		local itemConfig = equipmentConfig:GetBaseConfig(goodsID, goodsLevel)
		if itemConfig then
			local enum = itemConfig.addn_attr:GetEnumerator()
			while enum:MoveNext() do
				local attrItemObj = createUI('AttrInfo2', self.uiAttrGrid.transform)
				local script = getLuaComponent(attrItemObj)
				local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
				local name = attrNameConfig:GetAttrName(symbol)
				--script:SetData(name, enum.Current.Value)
				script:SetName(name)
				script:SetValue(enum.Current.Value)
			end
		else
			print('error -- can not get configuration by goodsID: ', goodsID, ' and level: ', goodsLevel)
		end
	end
	self.uiAttrGrid.repositionNow = true

	--套装信息
	if self.goods:IsSuit() then
		-- self.uiCategory:SetText(getCommonStr("STR_SUIT"))
		local suitObj
		if self.uiSuitParts.transform.childCount > 0 then
			suitObj = self.uiSuitParts.transform:GetChild(0)
		end
		if suitObj == nil then
			suitObj = createUI('SuitInfoItem', self.uiSuitParts.transform)
		end
		local suitScript = getLuaComponent(suitObj)
		suitScript.goods = self.goods
		suitScript.equipGoodsList = self.equipGoodsList
	else
		if self.uiSuitParts.transform.childCount > 0 then
			local suitChild = self.uiSuitParts.transform:GetChild(0)
			if suitChild then
				NGUITools.Destroy(suitChild.gameObject)
			end
		end
	end
	
	if self.goods:GetCategory() == GoodsCategory.GC_BADGE then
		local subCategory = tonumber(config.sub_category)
		if not BadgeSubCategory[subCategory] then
			self.uiCategory:SetText(getCommonStr('STR_BADGE'))
		else
			self.uiCategory:SetText(getCommonStr(BadgeSubCategory[subCategory]))
		end
	else
		local subCategory = enumToInt(self.goods:GetSubCategory())
		if not EquipSubCategory[subCategory] or self.inStore then
			self.uiCategory:SetText(getCommonStr('STR_EQUIPMENT'))
		else
			self.uiCategory:SetText(getCommonStr(EquipSubCategory[subCategory]))
		end
	end

	self.needResetPosition = true
end

-----------------------------------------------------------------

return EquipmentDetail

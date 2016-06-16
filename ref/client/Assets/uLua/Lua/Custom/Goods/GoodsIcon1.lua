------------------------------------------------------------------------
-- class name	: GoodsIcon1
-- create time   : Thu Sep 17 17:09:40 2015
------------------------------------------------------------------------

require "common/stringUtil"


GoodsIcon1 =  {
	uiName = "GoodsIcon1",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiNeed,
	uiGoodsName,
	uiMax,
	uiAddBtn,
	uiLv,
	uiCenterBg,
	--uiCricleBg,
	uiRedDot,
	uiSide,
	uiEffect,
	uiEffect1,

	-----------------------
	-- Parameters Module --
	-----------------------
	roleId,
	id,
	onClickAdd,
	need,
	icon,
	isAdd = true,
	needPlayAnimation = false,
	isMax = false,
	forceHideMax = false,
}

GoodsAtlas = GoodsAtlas or {
	property = "IconGoods",
	goods = "IconGoods",
	piece = "IconPiece",
	skill = "IconSkill",
	tattoo = "IconTattoo",
	equipment = "IconEquipment",
	fashion = "IconFashion",
	signin = "IconGoods",
	portrait = "IconPortrait",
}



---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function GoodsIcon1:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	addOnClick(self.uiAddBtn.gameObject,self:ClickAdd())
end


function GoodsIcon1:Start()
	self:Refresh()
end

function GoodsIcon1:Refresh()
	self.uiEffect.gameObject:SetActive(self.needPlayAnimation)
	self.uiEffect1.gameObject:SetActive(self.needPlayAnimation)

	local roleId = self.roleId
	local id = self.id
	local lv = MainPlayer.Instance:GetExerciseLevel(roleId, id)
	local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	local roleInfo = MainPlayer.Instance:GetRole2(roleId)
	local roleQuality = roleInfo.quality > 0 and roleInfo.quality or 1
	local maxQualityNum = GlobalConst.MAX_QUALITY_NUM
	print("maxQualityNum=",maxQualityNum)
	roleQuality = roleQuality <= maxQualityNum and roleQuality or maxQualityNum
	print("lv=",lv)
	local quality = math.modf(lv/5)
	print("quality=",quality)

	if lv < 10 then
		NGUITools.SetActive(self.uiNum1.gameObject, false)
		self.uiNum2.spriteName = "com_text_" ..tostring(lv)
	else
		NGUITools.SetActive(self.uiNum1.gameObject, true)
		local num1 = math.modf(lv/10)
		local num2 = lv % 10
		self.uiNum1.spriteName = "com_text_" ..tostring(num1)
		self.uiNum2.spriteName = "com_text_" ..tostring(num2)
	end


	local category = Split(attr.icon or "com_property_gold2", "_")
	local atlasName = "Atlas/icon/" .. GoodsAtlas[category[2]]
	local atlas = ResourceLoadManager.Instance:GetAtlas(atlasName)
	self.uiIcon.atlas = atlas
	self.uiIcon.spriteName = attr.icon

	self.uiGoodsName.text = attr.name
	print('attr.name ---- ' .. attr.name)
	self.uiNeed.text = tostring(lv) ..'/' ..tostring((roleQuality)*5)
	self.uiLv.text = "+"..tostring(lv)
	self.isMax = lv == roleQuality *5

	NGUITools.SetActive(self.uiMax.gameObject, false)
	NGUITools.SetActive(self.uiAddBtn.gameObject, true)

	-- local qualitySideColor  = {
	--	'com_card_frame_white',
	--	'com_card_frame_green',
	--	'com_card_frame_green',
	--	'com_card_frame_blue',
	--	'com_card_frame_blue',
	--	'com_card_frame_blue',
	--	'com_card_frame_purple',
	--	'com_card_frame_purple',
	--	'com_card_frame_purple',
	--	'com_card_frame_purple',
	--	'com_card_frame_yellow',
	--	'com_card_frame_yellow',
	--	'com_card_frame_yellow',
	--	'com_card_frame_yellow',
	--	'com_card_frame_yellow'
	-- }

	-- local bgColors = {
	--	'com_card_square_w_backdrop',
	--	'com_card_square_g_backdrop',
	--	'com_card_square_b_backdrop',
	--	'com_card_square_p_backdrop',
	--	'com_card_square_o_backdrop'
	-- }

	self.uiSide.spriteName = self["qualitySideColor_"..roleQuality]
	local isMax = quality == roleQuality or roleQuality == maxQualityNum
	NGUITools.SetActive(self.uiMax.gameObject, isMax and not self.forceHideMax )
	NGUITools.SetActive(self.uiEffect1.gameObject,isMax and not self.forceHideMax)
	self.uiEffect1:SetTrigger("EF_Ring")
	-- NGUITools.SetActive(self.uiAddBtn.gameObject, quality ~= roleQuality )
	if isMax then
		self.isAdd = false
	end

	NGUITools.SetActive(self.uiAddBtn.gameObject, self.isAdd)

	if quality <= 1 then
		quality = 1
	end
	if quality >5 then
		quality = 5
	end
	self.uiCenterBg.spriteName = self["qualityBackground_"..roleQuality]
	--self.uiCricleBg.color = bgColors[quality]
end

-- uncommoent if needed
-- function GoodsIcon1:FixedUpdate()

-- end


function GoodsIcon1:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.										 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function GoodsIcon1:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiSide = find("Side"):GetComponent("UISprite")
	self.uiIcon = find("Icon"):GetComponent("UISprite")
	self.uiLv = find("LevelText"):GetComponent("UILabel")
	self.uiGoodsName = find("Name"):GetComponent("UILabel")
	self.uiNeed = find("Need"):GetComponent("UILabel")
	self.uiAddBtn = find("Acquire/Add"):GetComponent("UISprite")
	-- self.uiMax = find("Acquire/Max"):GetComponent("UILabel")
	self.uiMax = find("Acquire/Back")
	self.uiCenterBg = self.uiSide.transform:FindChild('CenterBg'):GetComponent('UISprite')
	self.uiRedDot = find("RedDot"):GetComponent("UISprite")
	self.uiEffect = find("UIEffect1"):GetComponent("Animator")
	self.uiEffect1 = find("Ef_Goodsicon"):GetComponent("Animator")
	self.uiNum1 = find("Num1"):GetComponent("UISprite")
	self.uiNum2 = find("Num2"):GetComponent("UISprite")
end


function GoodsIcon1:ClickAdd()
	return function()
		if self.onClickAdd then
			self:onClickAdd()
		end
	end
end

function GoodsIcon1:SetData(roleId, id)
	self.roleId = roleId
	self.id = id
end

function GoodsIcon1:SetState(state)
	if self.uiMax.gameObject.activeSelf and state then
		NGUITools.SetActive(self.uiRedDot.gameObject, false)
		return
	end
	NGUITools.SetActive(self.uiRedDot.gameObject, state)
end

function GoodsIcon1:StartSparkle( ... )
	if self.uiEffect and self.needPlayAnimation then
		self.uiEffect:SetTrigger("EF_1")
	end
end

return GoodsIcon1

require "common/stringUtil"

GoodsIcon = {
	uiName = 'GoodsIcon',

	----------------UI
	uiIcon,
	uiSide,
	--uiLevel,
	uiNum,
	uiNeed,
	uiCenterBg,
	uiCricleBg,
	uiLabelLevel,
	uiLabelLevelBg,
	uiChipIcon,
	uiAnimator,
	uiAnimator1,
	uiLeftNum,
	uiLeftName,
	uiLeftExpNum,
	uiLighting,
	uiLighting1,
	uiMask,
	uiGender,
	uiLevelIcon,

	--签到相关
	uiSign,
	uiSignVipIcon,
	uiSignVipLabel,
	uiSignContinue,
	uiSigned,
	uiSignMask,
	uiQualityGrid,

	----------------parameters
	goods = nil,
	goodsID = 0,
	level = 0,
	num = 0,
	hideLevel = false,
	hideSide = false,
	hideNum = true,
	hideNeed = false,
	hideLight = true,
	hideSign = true,
	hideGender = true,
	inHallOfFame = false,
	levelStr = "",
	onClick = nil,
	showTips = true,
	notFixPortrait = false,
	needPlayAnimation = false,
	displayLeftNum = false,
	costNum = nil,
	onPressCB,

	isPackage = false,

	slotID, -- 用于装备更换界面使用 (enum type)
	dayID,	-- 签到天数

	talentBgColors = {
		[5] = 'com_card_square_g_backdrop',
		[8] = 'com_card_square_b_backdrop',
		[11] = 'com_card_square_p_backdrop',
		[14] = 'com_card_square_o_backdrop',
	},

	talentSide = {
		[5] = 'com_card_frame_green',
		[8] = 'com_card_frame_blue',
		[11] = 'com_card_frame_purple',
		[14] = 'com_card_frame_yellow',
	},

	lightSprite = {
		[1] = 'com_frame_E_IconWhite',
		[2] = 'com_frame_E_IconGreen',
		[3] = 'com_frame_E_IconBlue',
		[4] = 'com_frame_E_IconPurple',
		[5] = 'com_frame_E_IconYellow',
	},

	lightSprite1 = {
		[1] = 'com_frame_E_chipWhite',
		[2] = 'com_frame_E_chipGreen',
		[3] = 'com_frame_E_chipBlue',
		[4] = 'com_frame_E_chipPurple',
		[5] = 'com_frame_E_chipYellow',
	},
	genderSprite =
	{
		[1] = 'fashion_boy',
		[2] = 'fashion_girl',
	},
	qualityTb,
}

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

function GoodsIcon:Awake()
	self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	self.uiSide = self.transform:FindChild("Side"):GetComponent("UISprite")
	--self.uiLevel = self.transform:FindChild("Level"):GetComponent("UILabel")
	self.uiNum = self.transform:FindChild("Num"):GetComponent("UILabel")
	self.uiNeed = self.transform:FindChild("Need"):GetComponent("UILabel")
	self.uiCenterBg = self.transform:FindChild('CenterBg'):GetComponent('UISprite')
	self.uiCricleBg = self.transform:FindChild('CenterBg/CricleBg'):GetComponent('UISprite')
	self.uiLabelLevel = self.transform:FindChild("LevelText"):GetComponent("UILabel")
	self.uiLabelLevelBg = self.transform:FindChild("LevelText/Bg"):GetComponent("UISprite")
	self.uiChipIcon = self.transform:FindChild("ChipIcon"):GetComponent("UISprite")
	self.uiLighting = self.transform:FindChild("Ef_Sele"):GetComponent("UISprite")
	self.uiLighting1 = self.transform:FindChild("Ef_Sele1"):GetComponent("UISprite")
	self.uiMask = self.transform:FindChild("Mask"):GetComponent("UISprite")
	self.uiQualityGrid = self.transform:FindChild("Quality"):GetComponent("UIGrid")
	self.uiGender = self.transform:FindChild("Gender"):GetComponent("UISprite")
	--签到
	self.uiSign = self.transform:FindChild("Sign")
	self.uiSignVipIcon = self.uiSign.transform:FindChild("VipIcon")
	self.uiSignVipLabel = self.uiSignVipIcon.transform:FindChild("VipLabel"):GetComponent('UILabel')
	self.uiSignContinue = self.uiSign.transform:FindChild("GetContinue")
	self.uiSigned = self.uiSign.transform:FindChild("Right")
	self.uiSignMask = self.uiSign.transform:FindChild("Right/RightMask"):GetComponent('UISprite')
	--动画
	self.uiAnimator = self.transform:FindChild("Ef_GoodsiconDN"):GetComponent("Animator")
	self.uiAnimator1 = self.transform:FindChild("UIEffect1"):GetComponent("Animator")
	self.uiAnimator.gameObject:SetActive(false)
	self.uiAnimator1.gameObject:SetActive(false)
	-- 涂鸦等级图标
	self.uiLevelIcon = self.transform:FindChild("LevelIcon"):GetComponent("UISprite")

	if self.transform:FindChild("Inj") then
		self.uiLeftName = self.transform:FindChild("Inj/Name"):GetComponent("UILabel")
		self.uiLeftName.gameObject:SetActive(false)
		self.uiLeftNum = self.transform:FindChild("Inj/Num"):GetComponent("UILabel")

		self.uiLeftExpNum = self.transform:FindChild("Inj/ExpNum"):GetComponent("UILabel")
		self.uiLeftExpNum.gameObject:SetActive(false)
	end

	self.qualityTb = {}
	for i = 1, 4 do
		table.insert(self.qualityTb, self.transform:FindChild("Quality/"..i):GetComponent("UISprite"))
	end
	--添加onPress
	--self.Presstime = 0
	--UIEventListener.Get(self.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())
	addOnClick(self.gameObject,self:OnClick())
end

function GoodsIcon:Start()
	self:Refresh()
end

function GoodsIcon:Refresh()
	--self.uiAnimator.gameObject:SetActive(self.needPlayAnimation)
	self.uiAnimator1.gameObject:SetActive(self.needPlayAnimation)
	NGUITools.SetActive(self.uiChipIcon.gameObject, false)

	local configIcon
	if self.goods == nil then
		if self.goodsID ~= 0 then
			self.goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.goodsID)
			if not self.goodsConfig then
				error("GoodsIcon: Can not find goods: " .. self.goodsID)
			end
			if self.goodsID == GlobalConst.DIAMOND_ID then
				self.uiIcon.spriteName = 'icon_signin_diamond'
			elseif self.goodsID == GlobalConst.GOLD_ID then
				self.uiIcon.spriteName = 'icon_signin_gold'
			elseif self.goodsID == GlobalConst.HP_ID then
				self.uiIcon.spriteName = 'icon_signin_hp'
			elseif self.goodsID == GlobalConst.HONOR_ID then
				self.uiIcon.spriteName = 'icon_signin_honor'
			elseif self.goodsID == GlobalConst.PRESTIGE_ID then
				self.uiIcon.spriteName = 'icon_signin_prestige'
			elseif self.goodsID == GlobalConst.HONOR2_ID then
				self.uiIcon.spriteName = "icon_signin_honor1"
			elseif self.goodsID == GlobalConst.PRESTIGE2_ID then
				self.uiIcon.spriteName = "icon_signin_prestige1"
			elseif self.goodsID == GlobalConst.REPUTATION_ID then
				self.uiIcon.spriteName = "icon_signin_reputation"
			elseif self.goodsID == GlobalConst.TEAM_EXP_ID then
				self.uiIcon.spriteName = "icon_signin_exp"
			elseif self.goodsID == GlobalConst.ROLE_EXP_ID then
				self.uiIcon.spriteName = "icon_signin_exp01"
			else
				self.uiIcon.spriteName = self.goodsConfig.icon
				if string.find(self.goodsConfig.icon,"portrait") then
					self.notFixPortrait = true
				end
			end
			configIcon = self.goodsConfig.icon
			local quality = self.goodsConfig.quality
			local labels = {"white","green","blue","purple","orange"}
			local favorSideSpriteName = {'com_frame_c_frame_chipW', 'com_frame_c_frame_chipG', 'com_frame_c_frame_chipB', 'com_frame_c_frame_chipP', 'com_frame_c_frame_chipY'}
			local bgColors = {'com_card_fog_bg', 'com_frame_fog_bg'} --{'com_card_square_w_backdrop', 'com_card_square_g_backdrop', 'com_card_square_b_backdrop', 'com_card_square_p_backdrop', 'com_card_square_o_backdrop'}
			local bgLightColors = {'com_card_fog_w', 'com_card_fog_g', 'com_card_fog_b', 'com_card_fog_p', 'com_card_fog_o'}
			local bgLightColors2 = {'com_frame_fog_w', 'com_frame_fog_g', 'com_frame_fog_b', 'com_frame_fog_p', 'com_frame_fog_o'}

			--print('goodsConfig.category ========= ' .. tostring(GoodsCategory.IntToEnum(self.goodsConfig.category)))
			if GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_FAVORITE or
				(GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_EQUIPMENT and
				 EquipmentType.IntToEnum(self.goodsConfig.sub_category) == EquipmentType.ET_EQUIPMENTPIECE) then
				self.uiSide.spriteName = "com_card_frame_"..labels[quality]
				self.uiCenterBg.spriteName = bgColors[1] --bgFavorColors[quality]
				self.uiCricleBg.spriteName = bgLightColors2[quality]
				NGUITools.SetActive(self.uiChipIcon.gameObject, true)
				self.uiLighting1.spriteName = self.lightSprite[quality]
				NGUITools.SetActive(self.uiLighting1.gameObject, not self.hideLight)
			elseif GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_EQUIPMENT or
			 		GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_SKILL then
			 	if GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_SKILL then
					NGUITools.SetActive(self.uiLabelLevel.gameObject, true)
					self.uiLabelLevel.text = 'Lv.1'
				end
				self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
				self.uiCenterBg.spriteName = bgColors[1]
				self.uiCricleBg.spriteName = bgLightColors[quality]
				self.uiLighting.spriteName = self.lightSprite[quality]
				NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
			elseif GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_BADGE then
				self.uiLevelIcon.spriteName = "tencent_biaoqian"..quality
				self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
				self.uiCenterBg.spriteName = bgColors[1]
				self.uiCricleBg.spriteName = bgLightColors[quality]
				self.uiLighting.spriteName = self.lightSprite[quality]
				NGUITools.SetActive(self.uiLevelIcon.gameObject, true)
			elseif self.inHallOfFame then
				self.uiSide.spriteName =  "com_card_frame_"..labels[1]
				self.uiCenterBg.spriteName = bgColors[1]
				self.uiCricleBg.spriteName = bgLightColors[1]
				self.uiLighting.spriteName = self.lightSprite[1]
				NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
			else
				self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
				self.uiCenterBg.spriteName = bgColors[1]
				self.uiCricleBg.spriteName = bgLightColors[quality]
				self.uiLighting.spriteName = self.lightSprite[quality]
				NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
			end
		else
			error("GoodsIcon: goods not set")
		end
	else
		local goodsID = self.goods:GetID()
		if goodsID == GlobalConst.DIAMOND_ID then
			self.uiIcon.spriteName = 'icon_signin_diamond'
		elseif goodsID == GlobalConst.GOLD_ID then
			self.uiIcon.spriteName = 'icon_signin_gold'
		elseif goodsID == GlobalConst.HP_ID then
			self.uiIcon.spriteName = 'icon_signin_hp'
		elseif goodsID == GlobalConst.HONOR_ID then
			self.uiIcon.spriteName = 'icon_signin_honor'
		elseif goodsID == GlobalConst.PRESTIGE_ID then
			self.uiIcon.spriteName = 'icon_signin_prestige'
		elseif goodsID == GlobalConst.TEAM_EXP_ID then
			self.uiIcon.spriteName = "icon_signin_exp"
		elseif goodsID == GlobalConst.ROLE_EXP_ID then
			self.uiIcon.spriteName = "icon_signin_exp01"
		else
			self.uiIcon.spriteName = self.goods:GetIcon()
		end
		configIcon = self.goods:GetIcon()
		local quality = enumToInt(self.goods:GetQuality())
		local labels = {"white","green","blue","purple","orange"}

		-- print("quality=",quality)
		if quality <= 1 then
			quality = 1
		end
		if quality >5 then
			quality = 5
		end
		local favorSideSpriteName = {'com_frame_c_frame_chipW', 'com_frame_c_frame_chipG', 'com_frame_c_frame_chipB', 'com_frame_c_frame_chipP', 'com_frame_c_frame_chipY'}
		local levelBgSpriteName = {'com_card_frame_corner_w', 'com_card_frame_corner_g', 'com_card_frame_corner_b', 'com_card_frame_corner_p', 'com_card_frame_corner_o'}
		local bgColors = {'com_card_fog_bg', 'com_frame_fog_bg'} --{'com_card_square_w_backdrop', 'com_card_square_g_backdrop', 'com_card_square_b_backdrop', 'com_card_square_p_backdrop', 'com_card_square_o_backdrop'}
		local bgLightColors = {'com_card_fog_w', 'com_card_fog_g', 'com_card_fog_b', 'com_card_fog_p', 'com_card_fog_o'}
		local bgLightColors2 = {'com_frame_fog_w', 'com_frame_fog_g', 'com_frame_fog_b', 'com_frame_fog_p', 'com_frame_fog_o'}

		if self.goods:GetCategory()== GoodsCategory.GC_FAVORITE or
			(self.goods:GetCategory() == GoodsCategory.GC_EQUIPMENT and
			self.goods:GetSubCategory() == EquipmentType.ET_EQUIPMENTPIECE) then
			--NGUITools.SetActive(self.uiCenterBg.gameObject, false)
			self.uiSide.spriteName = "com_card_frame_"..labels[quality]
			self.uiCenterBg.spriteName = bgColors[1]
			self.uiCricleBg.spriteName = bgLightColors2[quality]
			NGUITools.SetActive(self.uiChipIcon.gameObject, true)
			self.uiLighting1.spriteName = self.lightSprite[quality]
			NGUITools.SetActive(self.uiLighting1.gameObject, not self.hideLight)
		elseif self.goods:GetCategory() == GoodsCategory.GC_EQUIPMENT or
			 	self.goods:GetCategory() == GoodsCategory.GC_SKILL then
			NGUITools.SetActive(self.uiLabelLevel.gameObject, true)
			self.uiLabelLevel.text = tostring(self.goods:GetLevel())
			if self.goods:GetCategory() == GoodsCategory.GC_SKILL then
				self.uiLabelLevel.text = 'Lv.' .. self.uiLabelLevel.text
			end
			self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
			self.uiLabelLevelBg.spriteName = levelBgSpriteName[quality]
			self.uiCenterBg.spriteName = bgColors[1]
			self.uiCricleBg.spriteName = bgLightColors[quality]
			self.uiLighting.spriteName = self.lightSprite[quality]
			NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
		elseif self.goods:GetCategory() == GoodsCategory.GC_BADGE then
			self.uiLevelIcon.spriteName = "tencent_biaoqian"..quality
			self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
			self.uiCenterBg.spriteName = bgColors[1]
			self.uiCricleBg.spriteName = bgLightColors[quality]
			self.uiLighting.spriteName = self.lightSprite[quality]
			NGUITools.SetActive(self.uiLevelIcon.gameObject, true)
		elseif self.inHallOfFame then
			self.uiSide.spriteName =  "com_card_frame_"..labels[1]
			self.uiCenterBg.spriteName = bgColors[1]
			self.uiCricleBg.spriteName = bgLightColors[1]
			self.uiLighting.spriteName = self.lightSprite[1]
			NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
		else
			self.uiSide.spriteName =  "com_card_frame_"..labels[quality]
			self.uiCenterBg.spriteName = bgColors[1]
			self.uiCricleBg.spriteName = bgLightColors[quality]
			self.uiLighting.spriteName = self.lightSprite[quality]
			NGUITools.SetActive(self.uiLighting.gameObject, not self.hideLight)
		end
		-- if self.levelStr=="" then
		--	self.uiLevel.text = "Lv." .. tostring(self.goods:GetLevel())
		-- else
		--	self.uiLevel.text = self.levelStr
		-- end
		self.goodsID = goodsID
	end

	local icons = Split(configIcon, "&")
	if #icons > 1 then
		configIcon = icons[1]
		self.uiQualityGrid.gameObject:SetActive(true)
		local num = tonumber(icons[3])
		if num == nil then num = 0 end
		for i = 1, 4 do
			self.qualityTb[i].gameObject:SetActive( i <= num )
			self.qualityTb[i].spriteName = icons[2]
		end
		self.uiIcon.spriteName = configIcon
		self.uiQualityGrid.repositionNow = true
	end

	local category = Split(configIcon or "com_property_gold2", "_")
	local atlasName = GoodsAtlas[category[2]]
	if atlasName then
		atlasName = "Atlas/icon/" .. atlasName
		self.goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.goodsID)
		if GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_FASHION then
			local types = Split(self.goodsConfig.sub_category, "&")
			if #types == 1 then
				if types[1] == "1" or types[1] == "2" or types[1] == "5" then
					atlasName = "Atlas/icon/iconFashion"
				else
					atlasName = "Atlas/icon/iconFashion_1"
				end
			else
				atlasName = "Atlas/icon/iconFashion"
			end
		end
		local atlas = ResourceLoadManager.Instance:GetAtlas(atlasName)
		if not atlas then error("GoodsIcon: can not load atlas, category: " .. category[2] .. " ID:" .. self.goodsID) end
		self.uiIcon.atlas = atlas
		if not atlas then error("GoodsIcon: can not load atlas, category: " .. category[2] .. " ID:" .. self.goodsID) end
		self.uiIcon.atlas = atlas
	end
	self.uiSignMask.spriteName = self.uiCenterBg.spriteName
	self.uiMask.spriteName = self.uiCenterBg.spriteName
	-- if self.levelStr=="" then
	--	if self.level ~= 0 then
	--		self.uiLevel.text = "Lv." .. tostring(self.level)
	--	end
	-- else
	--	self.uiLevel.text = self.levelStr
	-- end

	self.uiNum.text = self.num
	self.fixSize = true

	if self.displayLeftNum then
		NGUITools.SetActive(self.transform:FindChild("Inj").gameObject, true)
		local ownedNum = MainPlayer.Instance:GetGoodsCount(self.goodsID)
		if self.costNum then
			if ownedNum >= self.costNum then
				self.uiLeftNum.text = ownedNum.."/"..self.costNum
			else
				self.uiLeftNum.text = "[FF0000]"..ownedNum.."[-]".."/"..self.costNum
			end
		else
			self.uiLeftNum.text = ownedNum
		end
		self.uiLeftName.text = self.goodsConfig.name
		local useId = self.goodsConfig.use_result_id
		-- local enum = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(useId).args:GetEnumerator()
		-- if enum:MoveNext() then
		--	self.uiLeftExpNum.text = "X"..tostring(enum.Current.num_max)
		-- end
		self.hideNeed = true
		self.hideLevel = true
	end

	--NGUITools.SetActive(self.uiLevel.gameObject, not self.hideLevel)
	--NGUITools.SetActive(self.uiSide.gameObject, not self.hideSide) --没地方在用

	--特效
	local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.goodsID)
	if attr and attr.show_special_effect then
		local effect = self.transform:FindChild('SpecialEffect/' .. attr.show_special_effect)
		if effect and not effect.gameObject.activeSelf then
			NGUITools.SetActive(effect.gameObject, true)
		end
	end

	-- diplsay num even if num equals to 1.
	if self.goodsConfig.stack_num == 1 then
		self.hideNum = true
	end
	--fashion don't showTips
	if self.goodsConfig.category == 6 then
		self.showTips = false
	end

	local goodsGender = self.goodsConfig.gender
	if goodsGender then
		if goodsGender == 0 then
			self.hideGender = true
		else
			self.uiGender.spriteName = self.genderSprite[goodsGender]
		end
	else
		self.hideGender = true
	end

	NGUITools.SetActive(self.uiNum.gameObject, not self.hideNum)
	NGUITools.SetActive(self.uiNeed.gameObject, not self.hideNeed)
	NGUITools.SetActive(self.uiGender.gameObject, not self.hideGender)
	if self.isPackage then
		NGUITools.SetActive(self.uiNum.gameObject, false)
	end
end


function GoodsIcon:FixedUpdate()
	if self.fixSize and not self.notFixPortrait then
		--self.uiIcon:MakePixelPerfect()
		self.fixSize = false
	end
--[[
	if self.isPress == true then
		--调用创建
		if not self.obj then
			self:ShowTips()
		end
	end
--]]
end

function GoodsIcon:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--[[
function GoodsIcon:OnPress( ... )
	return function ( go , isPressed )
		if self.onPressCB then
			self:onPressCB(isPressed)
			return
		end
		if isPressed  then
			if not self.showTips then
				return
			end
			self.Presstime = os.time()
			self.isPress = true
		else
			self.isPress = false
			if self.obj ~= nil then
				NGUITools.Destroy(self.obj)
				self.obj = nil
			end
		end
	end
end
--]]

-- function GoodsIcon:FixedUpdate()
--	if (os.time() - self.Presstime) > 0.5 and self.isPress == true then
--		--调用创建
--		print("presstime---ostime:",os.time())
--		if not self.obj then
--			self:ShowTips()
--		end
--	end
-- end

function GoodsIcon:ShowTips()
	playSound("UI/UI_button5")
	if GoodsCategory.IntToEnum(self.goodsConfig.category) ~= GoodsCategory.GC_SKILL then
		self.obj = createUI("TipPopup")
	else
		self.obj = createUI("SkillItemTip")
	end
	local TipPopup = getLuaComponent(self.obj)
	TipPopup.id = self.goodsID
	if self.goods then
		TipPopup.goods = self.goods
	end
	UIManager.Instance:BringPanelForward(self.obj)
	--坐标设置
	local x = 0
	local y = 0
	local position = UIManager.Instance.m_uiRootBasePanel.transform:InverseTransformPoint(self.transform.position)  --IconPanelPos
	if position.x <= 0 and position.y >= 0 then
		x = 275
		y = -125
	elseif position.x >= 0 and position.y >= 0 then
		x = -275
		y = -125
	elseif position.x >= 0 and position.y <= 0 then
		x = -275
		y = 125
	elseif position.x <= 0 and position.y <= 0 then
		x = 275
		y = 125
	end
	position.x = position.x + x
	position.y = position.y + y
	position.z = -500
	--技能Tip超出边框处理
	if GoodsCategory.IntToEnum(self.goodsConfig.category) == GoodsCategory.GC_SKILL then
		if position.y > 85 then
			position.y = 85
		end
		if position.y < -250 then
			position.y = -250
		end
	end
	self.obj.transform.localPosition = position --Tippos
end

function GoodsIcon:SetNeed(need)
	NGUITools.SetActive(self.uiNeed.gameObject,need ~= nil )
	if need then
		self.uiNeed.text = tostring(need)
	end
end

function GoodsIcon:OnClick()
	return function()
		if self.onClick then
			playSound("UI/UI_button5")
			self:onClick(self)
		end
		if not self.showTips then
			return
		end
		self:ShowTips()
	end
end

function GoodsIcon:StartSparkle( ... )
	if self.uiAnimator and self.needPlayAnimation then
		self.uiAnimator:SetTrigger('EF_Down')
	end
end

function GoodsIcon:StartHighSparkle( ... )
	if self.uiAnimator1 and self.needPlayAnimation then
		self.uiAnimator1:SetTrigger('EF_1')
	end
end

function GoodsIcon:HideLight(state)
	if self.uiLighting.gameObject.activeSelf then
		NGUITools.SetActive(self.uiLighting.gameObject, not state)
	end

	if self.uiLighting1.gameObject.activeSelf then
		NGUITools.SetActive(self.uiLighting1.gameObject, not state)
	end
end

function GoodsIcon:SetMask(mask)
	self.uiMask.gameObject:SetActive(mask)
end

function GoodsIcon:SetEf_GoodsiconDN(ef)
	NGUITools.SetActive(self.uiAnimator.gameObject,ef)
	self.uiAnimator:SetTrigger('EF_Down')
end

function GoodsIcon:SetSignData(isVip, signContinue, signed, spriteName, vipLevel)
	NGUITools.SetActive(self.uiSign.gameObject, not self.hideSign)
	NGUITools.SetActive(self.uiSignVipIcon.gameObject, isVip)
	NGUITools.SetActive(self.uiSignContinue.gameObject, signContinue)
	NGUITools.SetActive(self.uiSigned.gameObject, signed)
	NGUITools.SetActive(self.uiSignMask.gameObject, signed)

	if vipLevel > 0 then
		self.uiSignVipLabel.text = string.format(getCommonStr('SIGNIN_VIP_LEVEL'), vipLevel)
	end
	--self.uiSignContinue.transform:GetComponent('UISprite').spriteName = tostring(spriteName)
	if spriteName then
		self.uiSignContinue.transform:GetComponent('UISprite').spriteName = "com_other_continue"
		NGUITools.SetActive(self.uiSignContinue.transform:Find("GetAwardHand").gameObject, false)
	elseif not spriteName then
		self.uiSignContinue.transform:GetComponent('UISprite').spriteName = ""
	end
end

return GoodsIcon

require "common/stringUtil"

GoodsIcon2 =  {
	uiName	 = "GoodsIcon2",

	--------------UI
	uiIcon,
	uiSide,
	uiStar,
	uiLabelName,
	uiLabelName1,
	uiLabelLevel,
	uiLabelLevelBg,
	uiLabelNum,
	uiCenterBg,
	uiSele,
	uiChipIcon,
	uiEffect,
	--uiCricleBg,

	--------------parameters
	pos,
	num,
	goods,
	id,
	goods_type,
	tab = 0,
	price = 0,
	sellout,
	roleId = nil,
	--name = nil,
	onClick = nil,
	levelDisplay = nil,
	isDisplayNum = true,
	isDisplayLevel = true,
	needPlayAnimation = false,
	isPackage = false,
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


function GoodsIcon2:Awake()
	self.uiGoodsIcon = self.transform:FindChild("GoodsIcon")
	self.uiStar = self.transform:FindChild("StarBack"):GetComponent("UISlider")
	self.uiLabelName = self.transform:FindChild("Name"):GetComponent("UILabel")
	self.uiLabelName1 = self.transform:FindChild("Name1"):GetComponent("UILabel")
	self.uiSele = self.transform:FindChild('Sele')
	self.uiEffect = self.transform:FindChild('UIEffect1'):GetComponent('Animator')
	--self.uiCricleBg = self.uiCenterBg.transform:FindChild('CricleBg'):GetComponent('UISprite')
end

function GoodsIcon2:Start()
	addOnClick(self.gameObject,self:OnClick())
	self:UpdateTab()
end

function GoodsIcon2:Refresh()
	self.uiEffect.gameObject:SetActive(self.needPlayAnimation)

	if self.goods then
		self.id = self.goods:GetID()
		self.goods_type = self.goods:GetCategory()
	end
	local roleId = self.roleId
	local id = self.id



	local level
	if roleId then
		level = MainPlayer.Instance:GetExerciseLevel(roleId,id)
	elseif self.level_display ~= nil then
		level = self.level_display
	elseif self.goods then
		level = self.goods:GetLevel()
	else
		level = 1
	end

	local child
	if self.uiGoodsIcon.transform.childCount > 0 then
		child = self.uiGoodsIcon.transform:GetChild(0)
	else
		child = createUI("GoodsIcon",self.uiGoodsIcon)
	end
	self.goodsLuaCom = getLuaComponent(child)
	self.goodsLuaCom.goodsID = self.id
	if self.goods then
		self.goodsLuaCom.goods = self.goods
	end
	self.goodsLuaCom.showTips = false
	self.goodsLuaCom.hideNeed = true
	self.goodsLuaCom.hideNum = not self.isDisplayNum
	self.goodsLuaCom.hideLevel = not self.isDisplayLevel
	self.goodsLuaCom.onClick = self:OnClick()
	self.goodsLuaCom:Refresh()
	-- level
	local star = level % 6
	self.uiStar.value = 1 - star * 0.2
	local lv_adds ={
		0,
		0,
		"+1",
		0,
		"+1",
		"+2",
		0,
		"+1",
		"+2",
		"+3",
		0,
		"+1",
		"+2",
		"+3",
		"+4",
	}

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
	--self.uiLabelName.color = nameColors[quality]
	--self.uiCricleBg.color = bgColors[quality]
	local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)
	self.uiLabelName.text = tostring(attr.name)
end

function GoodsIcon2:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function GoodsIcon2:HideStar(hide)
	NGUITools.SetActive(self.uiStar.gameObject, not hide)
end

function GoodsIcon2:SetData(roleId,id)
	self.roleId = roleId
	self.id = id
	self:Refresh()
end

function GoodsIcon2:SetGoodsData(info)
	self.id = info.id
	local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(info.id)

	self.uiLabelName.text = tostring(attr.name)
	self.icon = attr.icon
	self.num = info.num

	local child
	if self.uiGoodsIcon.transform.childCount > 0 then
		child = self.uiGoodsIcon.transform:GetChild(0)
	else
		child = createUI("GoodsIcon",self.uiGoodsIcon)
	end
	self.goodsLuaCom = getLuaComponent(child)
	if self.isPackage then
		self.goodsLuaCom.isPackage = true
	end
	self.goodsLuaCom.goodsID = info.id
	if self.goods then
		self.goodsLuaCom.goods = self.goods
	end
	self.goodsLuaCom.num = info.num

	if self.goods:IsEquip() then
		local pos = self:GetEquipPos()
		if pos then
			local roleID = self:GetEquipRoleID()
			if roleID then
				local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleID)
				self.goodsLuaCom.num = CommonFunction.GetConstString("") .. config.name
			end
		end
	else
		self.goodsLuaCom.num = tostring(info.num)
	end
	self.goodsLuaCom.showTips = false
	self.goodsLuaCom:Refresh()

	-- local goods_config_data = GameSystem.Instance.GoodsConfigData
	-- local good_attr_configs = goods_config_data.goodsAttrConfig
	-- local _,good_attr_config = good_attr_configs:TryGetValue(info.id)
	local good_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(info.id)
	self.price = good_attr_config.sell_price
	print(self.uiName, self.goods:GetID(), "---GetCategory:",self.goods:GetCategory())
	if self.goods and self.goods:GetCategory() == GoodsCategory.GC_EQUIPMENT then
		local equipmentConfig = GameSystem.Instance.EquipmentConfigData
		print(self.uiName,"----id:",self.goods:GetID(),"level:",self.goods:GetLevel())
		local itemConfigCur = equipmentConfig:GetBaseConfig(self.goods:GetID(), self.goods:GetLevel())
		if itemConfigCur then
			self.price = itemConfigCur.sell_price
		else
			print('errot',self.uiName, "No equipment config for goods. ID:", self.goods:GetID(), "Level:", self.goods:GetLevel())
		end
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

	--self.uiLabelName.color = nameColors[attr.quality]
	--self.uiLabelNum.color = bgColors[attr.quality]

	NGUITools.SetActive(self.uiStar.gameObject, false)
	--NGUITools.SetActive(self.uiLabelLevel.gameObject, false)
end

function GoodsIcon2:goodsRefresh( ... )

	if self.goods:GetCategory() == GoodsCategory.GC_EQUIPMENT then
		if self.goods:IsEquip() then
			local pos = self:GetEquipPos()
			if pos then
				local roleID = self:GetEquipRoleID(pos)
				if roleID then
					local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleID)
					NGUITools.SetActive(self.uiLabelName1.gameObject, true)
					--self.uiLabelNum.text = ''
					self.uiLabelName1.text = CommonFunction.GetConstString("") .. config.name
					if self.goodsLuaCom then
						self.goodsLuaCom.num = ""
						self.goodsLuaCom:Refresh()
					else
						print("error init goodsIcon first")
					end
				end
			end
		else
			-- self.uiLabelNum.text = ''
			if self.goodsLuaCom and enumToInt(self.goods:GetSubCategory()) ~= 6 then
				self.goodsLuaCom.num = ""
				self.goodsLuaCom:Refresh()
			else
				print("error init goodsIcon first")
			end
		end
	else
		NGUITools.SetActive(self.uiLabelName1.gameObject, false)
		--self.uiLabelNum.text = tostring(self.goods:GetNum())
		if self.goodsLuaCom then
			if self.goods:GetCategory() == GoodsCategory.GC_BADGE then
				local leftNum = MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(self.goods:GetID())
				self.goodsLuaCom.num = tostring(leftNum)
			else
				self.goodsLuaCom.num = tostring(self.goods:GetNum())
			end
			self.goodsLuaCom:Refresh()
		else
			print("error init goodsIcon first")
		end
	end
end

function GoodsIcon2:GetEquipPos( ... )
	local equipInfo = MainPlayer.Instance.EquipInfo
	local enum = equipInfo:GetEnumerator()
	while enum:MoveNext() do
		--if enum.Current.pos:ToString() == resp.info.pos then
			local slotInfo = enum.Current.slot_info
			local enumSlot = slotInfo:GetEnumerator()
			while enumSlot:MoveNext() do
				if enumSlot.Current.equipment_uuid == self.goods:GetUUID() then
					return  enum.Current.pos
				end
			end
		--end
	end
	return nil
end

function GoodsIcon2:GetEquipRoleID(pos)
	local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.status == pos then
			return enum.Current.role_id
		end
	end
	return nil
end

function GoodsIcon2:OnClick()
	return function()
		if self.onClick then
			playSound("UI/UI_button5")
			self.onClick(self)
		end
	end
end

function GoodsIcon2:Sellout()
	self.sellout = true
	NGUITools.Destroy(self.gameObject)
end

function GoodsIcon2:UpdateTab()
	local gt = self.goods_type
	if gt  == fogs.proto.msg.GoodsCategory.GC_SKILL then
		self.tab = 0
	elseif gt == fogs.proto.msg.GoodsCategory.GC_EQUIPMENT then
		self.tab = 1
	elseif gt == fogs.proto.msg.GoodsCategory.GC_FAVORITE then
		self.tab = 2
	elseif gt == fogs.proto.msg.GoodsCategory.GC_CONSUME then
		self.tab = 3
	end

end

function GoodsIcon2:SetSele(select)
	NGUITools.SetActive(self.uiSele.gameObject, select)
end

function GoodsIcon2:StartSparkle( ... )
	if self.uiEffect and self.needPlayAnimation then
		self.uiEffect:SetTrigger("EF_1")
	end
end
return GoodsIcon2

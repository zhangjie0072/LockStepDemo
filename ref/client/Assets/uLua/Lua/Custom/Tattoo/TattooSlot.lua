TattooSlot = {
	uiName = 'TattooSlot',
	
	slotID = 0,
	isEquiped = false,
	uuid = 0,

	-- function handler (skillSlot)
	onClick = nil,
	onUnequip = nil,
	onReplace = nil,
	onDetail = nil,
	onUpgrade = nil,
	describe = "",

	--accessible attribute:
	--tattooType
	--goods
	--level
}

function TattooSlot:Awake()
	self.uiAdd = self.transform:FindChild("Add").gameObject
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiDesc = self.transform:FindChild("Desc"):GetComponent("UILabel")

	addOnClick(self.gameObject, self:MakeOnClick())
end

function TattooSlot:Start()
	self.started = true
	self:Refresh()
end

function TattooSlot:Refresh()
	if not self.started then return end

	self.tattooType = TattooType.IntToEnum(self.slotID)
	-- set description
	if self.describe == nil then
		if self.tattooType == TattooType.TT_NECK then
			self.uiDesc.text = getCommonStr("TATTOO_TYPE_NECK")
		elseif self.tattooType == TattooType.TT_CHEST then
			self.uiDesc.text = getCommonStr("TATTOO_TYPE_CHEST")
		elseif self.tattooType == TattooType.TT_ARM then
			self.uiDesc.text = getCommonStr("TATTOO_TYPE_ARM")
		elseif self.tattooType == TattooType.TT_BACK then
			self.uiDesc.text = getCommonStr("TATTOO_TYPE_BACK")
		else
			error("TattooSlot: error slot type: " .. self.slotID)
		end
	else
		self.uiDesc.text = self.describe
	end

	if not self.isEquiped then
		NGUITools.SetActive(self.uiAdd.gameObject, true)
		NGUITools.SetActive(self.uiIconNode.gameObject, false)
	else
		NGUITools.SetActive(self.uiAdd.gameObject, false)
		NGUITools.SetActive(self.uiIconNode.gameObject, true)
		if self.uiIcon == nil then
			self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.uiIconNode))
			addOnClick(self.uiIcon.gameObject, self:MakeOnClick())
		end
		-- set goods icon
		local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, self.uuid)
		if not goods then
			error("TattooSlot: can not find tattoo, UUID: ", uuid) 
		end
		self.goods = goods
		self.level = goods:GetLevel()
		self.tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(goods:GetID(), goods:GetLevel())
		self.uiIcon.goods = goods
		self.uiIcon:Refresh()
	end
end

function TattooSlot:Equip(uuid)
	self.isEquiped = true
	self.uuid = uuid
	local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, self.uuid)
	
	self.tattooConfig = GameSystem.Instance.TattooConfig:GetTattooConfig(goods:GetID(), goods:GetLevel())
	self.describe = self.tattooConfig.name
	self:Refresh()
end

function TattooSlot:Open()
	self.isEquiped = false
	self.uuid = 0
	self.describe = ""
	self:Refresh()
end

function TattooSlot:ShowMenu()
	if self.isEquiped then
		local canUpgrade = (GameSystem.Instance.TattooConfig:GetTattooConfig(self.goods:GetID(), self.goods:GetLevel()) ~= nil)

		local go = createUI("PopupRingMenu", self.transform)
		NGUITools.BringForward(go)
		self.menu = getLuaComponent(go)
		self.menu.itemImages = {
			{atlas = "captainSystem/captainSystem", sprite = "captain_circle_unload"},
			{atlas = "captainSystem/captainSystem", sprite = "captain_circle_replace"},
			{atlas = "captainSystem/captainSystem", sprite = "captain_circle_detail"},
			canUpgrade and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_update"} or {},
		}
		self.menu.startAngle = 70
		self.menu.deltaAngle = 45
		self.menu.onClick = self:MakeOnItemClick()
		self.menu.onClickFullScreen = function () self:HideMenu() end
	end
end

function TattooSlot:HideMenu()
	if self.menu then
		NGUITools.Destroy(self.menu.gameObject)
		self.menu = nil
	end
end

--private

function TattooSlot:MakeOnClick()
	return function ()
		if self.onClick then self:onClick() end
	end
end

function TattooSlot:MakeOnItemClick()
	return function (index)
		if index == 1 then
			if self.onUnequip then self.onUnequip(self) end
		elseif index == 2 then
			if self.onReplace then self.onReplace(self) end
		elseif index == 3 then
			if self.onDetail then self.onDetail(self) end
		elseif index == 4 then
			if self.onUpgrade then self.onUpgrade(self) end
		else
			error("TattooSlot: Menu index error")
		end
	end
end

return TattooSlot

SkillSlotState = {
	LOCKED = 1,
	OPENED = 2,
	EQUIPED = 3,
}

SkillSlot = {
	uiName = 'SkillSlot',
	
	State = SkillSlotState,

	slotID = 0,
	state = SkillSlotState.LOCKED,
	isVIP = false,
	unlockLevel = 0,
	uuid = 0,

	-- function handler (skillSlot)
	onClick = nil,
	onUnequip = nil,
	onReplace = nil,
	onDetail = nil,
	onUpgrade = nil,

	--accessible attribute:
	--goods
	--skillAttr
	--level
}

function SkillSlot:Awake()
	self.uiBackground = self.transform:FindChild("Background"):GetComponent("UISprite")
	self.uiAdd = self.transform:FindChild("Add").gameObject
	self.uiVIP = self.transform:FindChild("VIP").gameObject
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiDesc = self.transform:FindChild("Desc"):GetComponent("UILabel")

	addOnClick(self.gameObject, self:MakeOnClick())

	self.descInitialColor = self.uiDesc.color
end

function SkillSlot:Start()
	self.started = true
	self:Refresh()
end

function SkillSlot:Refresh()
	if not self.started then return end

	if self.state == SkillSlot.State.LOCKED then
		self.uiBackground.spriteName = self.LockedBackground
		NGUITools.SetActive(self.uiAdd.gameObject, false)
		NGUITools.SetActive(self.uiIconNode.gameObject, false)
		NGUITools.SetActive(self.uiDesc.gameObject, true)
		self.uiDesc.color = Color.white
		if self.isVIP then
			self.uiDesc.text = getCommonStr("SKILL_UNLOCK_LEVEL_VIP"):format(self.unlockLevel)
		else
			self.uiDesc.text = getCommonStr("SKILL_UNLOCK_LEVEL"):format(self.unlockLevel)
		end
	elseif self.state == SkillSlot.State.OPENED then
		self.uiBackground.spriteName = self.NormalBackground
		NGUITools.SetActive(self.uiAdd.gameObject, true)
		NGUITools.SetActive(self.uiIconNode.gameObject, false)
		NGUITools.SetActive(self.uiDesc.gameObject, false)
	elseif self.state == SkillSlot.State.EQUIPED then
		self.uiBackground.spriteName = self.NormalBackground
		NGUITools.SetActive(self.uiAdd.gameObject, false)
		NGUITools.SetActive(self.uiIconNode.gameObject, true)
		NGUITools.SetActive(self.uiDesc.gameObject, true)
		if self.uiIcon == nil then
			self.uiIcon = getLuaComponent(createUI("GoodsIcon", self.uiIconNode))
			addOnClick(self.uiIcon.gameObject, self:MakeOnClick())
		end
		-- set goods icon
		local goods = MainPlayer.Instance:GetGoods(fogs.proto.msg.GoodsCategory.GC_SKILL, self.uuid)
		self.goods = goods
		self.level = goods:GetLevel()
		self.skillAttr = GameSystem.Instance.SkillConfig:GetSkill(goods:GetID())
		self.uiIcon.goods = goods
		self.uiIcon:Refresh()
		-- set name
		self.uiDesc.text = self.skillAttr.name
		self.uiDesc.color = self.descInitialColor
	else
		error("SkillSlot: state error.")
	end

	NGUITools.SetActive(self.uiVIP, self.isVIP)
end

function SkillSlot:Equip(uuid)
	self.state = self.State.EQUIPED
	self.uuid = uuid
	self:Refresh()
end

function SkillSlot:Open()
	self.state = self.State.OPENED
	self.uuid = 0
	self:Refresh()
end

function SkillSlot:Lock(unlockLevel)
	self.state = self.State.LOCKED
	self.unlockLevel = unlockLevel
	self.uuid = 0
	self:Refresh()
end

function SkillSlot:ShowMenu()
	if self.state == self.State.EQUIPED then
		local canUpgrade = self.skillAttr.levels:ContainsKey(self.level + 1)
		local canUnequip = self.skillAttr.type ~= SkillType.PASSIVE

		local go = createUI("PopupRingMenu", self.transform)
		NGUITools.BringForward(go)
		self.menu = getLuaComponent(go)
		self.menu.itemImages = {
			canUnequip and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_unload"} or {},
			canUnequip and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_replace"} or {},
			{atlas = "captainSystem/captainSystem", sprite = "captain_circle_detail"},
			canUpgrade and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_update"} or {},
		}
		self.menu.startAngle = 70
		self.menu.deltaAngle = 45
		self.menu.onClick = self:MakeOnItemClick()
		self.menu.onClickFullScreen = function () self:HideMenu() end
	end
end

function SkillSlot:HideMenu()
	if self.menu then
		NGUITools.Destroy(self.menu.gameObject)
		self.menu = nil
	end
end

--private

function SkillSlot:MakeOnClick()
	return function ()
		--print("SkillSlot: clicked. Goods name: " .. self.uiIcon.goods:GetName())
		if self.onClick then self:onClick() end
	end
end

function SkillSlot:MakeOnItemClick()
	return function (index)
		--print("Menu item clicked, index: " .. index)
		if index == 1 then
			if self.onUnequip then self.onUnequip(self) end
		elseif index == 2 then
			if self.onReplace then self.onReplace(self) end
		elseif index == 3 then
			if self.onDetail then self.onDetail(self) end
		elseif index == 4 then
			if self.onUpgrade then self.onUpgrade(self) end
		else
			error("SkillSlot: Menu index error")
		end
	end
end

return SkillSlot

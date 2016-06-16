--encoding=utf-8

UITattoo = {
	uiName = 'UITattoo',

	----------------------------------
	SLOT_COUNT = 4,

	-----------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UITattoo:Awake()
	local window = self.transform:FindChild("Window")

	addOnClick(window:FindChild("Background").gameObject, self:MakeOnBGClick())

	local backNode = window:FindChild("BackButtonNode")
	getLuaComponent(createUI("ButtonBack", backNode)).onClick = self:MakeOnBackClick()

	self.tmModel = window:FindChild("Model")

	local btnSkill = window:FindChild("Functions/Grid/Skill").gameObject
	local btnTraining = window:FindChild("Functions/Grid/Training").gameObject
	local btnFashion = window:FindChild("Functions/Grid/Fashion").gameObject
	addOnClick(btnSkill, self:MakeOnSkillClick())
	addOnClick(btnTraining, self:MakeOnTrainingClick())
	addOnClick(btnFashion, self:MakeOnFashionClick())

	local nodes = window:FindChild("Nodes")
	self.slots = {}
	for i = 1, self.SLOT_COUNT do
		local node = nodes:FindChild("Node" .. i)
		self.slots[i] = self:InstantiateSlot(node, "Slot" .. i)
		self.slots[i].slotID = i
	end

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UITattoo:Start()
	LuaHelper.RegisterPlatMsgHandler(MsgID.TattooOperationRespID, self:MakeOperationHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.TattooUpgradeRespID, self:MakeUpgradeHandler(), self.uiName)

	self.playerDisplay = getLuaComponent(createUI("PlayerDisplay", self.tmModel))
	self.playerDisplay:set_captain_data(MainPlayer.Instance.CaptainID, MainPlayer.Instance.Captain.m_roleInfo.bias)
	self.playerDisplay.is_captain = true
	self.playerDisplay.is_show_lock = false
end

function UITattoo:FixedUpdate()
	-- body
end

function UITattoo:OnClose()
	if self.curSlot then
		self.curSlot:HideMenu()
	end
	TopPanelManager:ShowPanel("UIHall")
end

function UITattoo:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TattooOperationRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TattooUpgradeRespID, self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UITattoo:Refresh()
	local role = MainPlayer.Instance.Captain
	if role.m_roleInfo.tattoo_slot_info then
		local enum = role.m_roleInfo.tattoo_slot_info:GetEnumerator()
		while enum:MoveNext() do	--enumerate slots
			local tattooSlot = enum.Current
			local slot = self.slots[tattooSlot.id]
			--if not slot then error("UITatto: error tattoo slot id: " .. tattooSlot.id) end

			if slot then
				if tattooSlot.tattoo_uuid ~= 0 then	--equiped
					slot:Equip(tattooSlot.tattoo_uuid)
				else	--opened
					slot:Open()
				end
			end
		end
	end
	
	if self.playerDisplay then 
		self.playerDisplay:update_id(MainPlayer.Instance.CaptainID)
		self.playerDisplay:refresh()
	end

end


-----------------------------------------------------------------
function UITattoo:InstantiateSlot(node, name)
	local go = createUI("TattooSlot", node)
	go.name = name
	local slot = getLuaComponent(go)
	slot.onClick = self:MakeOnSlotClick()
	slot.onUnequip = self:MakeOnUnequip()
	slot.onReplace = self:MakeOnReplace()
	slot.onDetail = self:MakeOnDetail()
	slot.onUpgrade = self:MakeOnUpgrade()
	return slot
end

function UITattoo:MakeOnSlotClick()
	return function (slot)
		if self.curSlot ~= nil then self.curSlot:HideMenu() end
		self.curSlot = slot
		if slot.isEquiped then
			slot:ShowMenu()
		else
			if not self:HasUnequipedTattoo(slot.tattooType) then
				CommonFunction.ShowPopupMsg(getCommonStr("GOODS_NO_THIS_TYPE"),nil,nil,nil,nil,nil)
				return
			end
			self.tattooList = TopPanelManager:ShowPanel("UIGoodsList", nil,
				{
					goodsCategory = GoodsCategory.GC_TATTOO,
					tattooType = TattooType.IntToEnum(slot.slotID),
					onOKClick = self:MakeOnEquip(),
				})
		end
	end
end

function UITattoo:MakeOnEquip()
	return function (goods)
		print("Equip tattoo, uuid: " .. tostring(goods:GetUUID()) .. " slot: " .. self.curSlot.slotID)
		local operation = {
			type = "TOT_EQUIP",
			uuid = goods:GetUUID(),
			role_id = MainPlayer.Instance.CaptainID,
			slot_id = self.curSlot.slotID,
		}

		local req = protobuf.encode("fogs.proto.msg.TattooOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.TattooOperationID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UITattoo:MakeOnUnequip()
	return function (slot)
		slot:HideMenu()

		local operation = {
			type = "TOT_UNEQUIP",
			uuid = slot.goods:GetUUID(),
			role_id = MainPlayer.Instance.CaptainID,
			slot_id = slot.slotID,
		}

		local req = protobuf.encode("fogs.proto.msg.TattooOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.TattooOperationID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UITattoo:MakeOnReplace()
	return function (slot)
		slot:HideMenu()
		if not self:HasUnequipedTattoo(slot.tattooType) then
			CommonFunction.ShowPopupMsg(getCommonStr("GOODS_NO_THIS_TYPE"),nil,nil,nil,nil,nil)
			return
		end
		self.tattooList = TopPanelManager:ShowPanel("UIGoodsList", nil,
			{
				goodsCategory = GoodsCategory.GC_TATTOO,
				tattooType = TattooType.IntToEnum(slot.slotID),
				onOKClick = self:MakeOnEquip(),
			})
	end
end

function UITattoo:MakeOnDetail()
	return function (slot)
		slot:HideMenu()
		local detail = getLuaComponent(createUI("TattooDetailPopup", self.transform))
		bringNear(detail.transform)
		NGUITools.BringForward(detail.gameObject)
		detail.tattooID = slot.goods:GetID()
		detail.level = slot.level
	end
end

function UITattoo:MakeOnUpgrade()
	return function (slot)
		slot:HideMenu()
		self.upgrade = getLuaComponent(createUI("TattooUpgradePopup", self.transform))
		bringNear(self.upgrade.transform)
		NGUITools.BringForward(self.upgrade.gameObject)
		self.upgrade.uuid = slot.goods:GetUUID()
		self.upgrade.onUpgrade = self:MakeOnConfirmUpgrade()
		self.upgrade.onClose = function () self.upgrade = nil end
	end
end

function UITattoo:MakeOnConfirmUpgrade()
	return function ()
		print("UITattoo: Upgrade tattoo: ", self.upgrade.goods:GetID())
		print("Consumables:")
		local cons = {}
		for c, n in pairs(self.upgrade.consumables) do
			print(c, n)
			if n > 0 then
				table.insert(cons, {uuid = c, num = n})
			end
		end

		local operation = {
			uuid = self.upgrade.goods:GetUUID(),
			consumables = cons,
		}

		local req = protobuf.encode("fogs.proto.msg.TattooUpgrade", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.TattooUpgradeID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UITattoo:MakeOperationHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		if not NGUITools.GetActive(self.gameObject) and not self.tattooList then return end
		local resp, err = protobuf.decode("fogs.proto.msg.TattooOperationResp", buf)
		if resp then
			if resp.result == 0 then
				if resp.type == "TOT_EQUIP" then
					self:HandleEquip(resp)
				elseif resp.type == "TOT_UNEQUIP" then
					self:HandleUnequip(resp)
				end
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITattoo: " .. err)
		end
	end
end

function UITattoo:MakeUpgradeHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		if not NGUITools.GetActive(self.gameObject) and not self.tattooList then return end
		local resp, err = protobuf.decode("fogs.proto.msg.TattooUpgradeResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleUpgrade(resp)
				playSound("UI/UI_level_up_01")
			else
				print(resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				playSound("UI/UI-wrong")
			end
		else
			error("UITattoo: " .. err)
		end
	end
end

function UITattoo:HandleUpgrade(resp)
	local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, resp.uuid)
	if not goods then
		error("UITattoo: Can not find upgraded tattoo: ", resp.uuid)
	end
	for _, slot in ipairs(self.slots) do
		if slot.uuid == goods:GetUUID() then
			slot:Equip(goods:GetUUID())
		end
	end

	if self.upgrade then 
		self.upgrade:Clear()
	end

	self.playerDisplay:refresh_ui()
end

function UITattoo:HandleEquip(resp)
	local slot = self.slots[resp.slot_id]
	slot:Equip(resp.uuid)
	TopPanelManager:HideTopPanel("UIGoodsList")
	self.tattooList = nil
	self.playerDisplay:refresh_ui()
end

function UITattoo:HandleUnequip(resp)
	local slot = self.slots[resp.slot_id]
	slot:Open()
	self.playerDisplay:refresh_ui()
end

function UITattoo:MakeOnBGClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
	end
end

function UITattoo:MakeOnBackClick()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UITattoo:MakeOnSkillClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITattoo: skill clicked.")
		TopPanelManager:ShowPanel("UISkill")
	end
end

function UITattoo:MakeOnTrainingClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITattoo: training clicked.")
		TopPanelManager:ShowPanel("UITraining")
	end
end

function UITattoo:MakeOnFashionClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITattoo: fashion clicked.")
	end
end

function UITattoo:HasUnequipedTattoo(tattooType)
	local enum = MainPlayer.Instance.TattooGoodsList:GetEnumerator()
	while enum:MoveNext() do
		if not enum.Current.Value:IsEquip() and enum.Current.Value:GetSubCategory() == tattooType then
			return true
		end
	end
end

return UITattoo

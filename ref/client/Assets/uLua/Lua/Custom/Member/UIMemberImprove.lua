--encoding=utf-8

UIMemberImprove =  {
	uiName = 'UIMemberImprove',

	-----------------parameters

	-- pre_go = nil,
	go = {},

	--------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UIMemberImprove:Awake()
	createUI('Background',self.transform)

	self.go ={}
	self.go.back_btn = self.transform:FindChild('ButtonBack/BackArrow').gameObject
	self.go.player_display_node = self.transform:FindChild('PlayerDisplayNode')
	local pd_go = createUI('PlayerDisplay',self.go.player_display_node)
	self.player_display = getLuaComponent(pd_go)

	self.go.quality_label = self.transform:FindChild('QualityTag/quality'):GetComponent('UILabel')
	self.go.upgrade_btn = self.transform:FindChild('QualityTag/Upgrade').gameObject

	self.uiAnimator = self.transform:GetComponent('Animator')

	addOnClick(self.go.upgrade_btn,self:click_upgrade())
	addOnClick(self.go.back_btn,self:click_back())

	-- tattoo
	local nodes = self.transform:FindChild("Nodes")
	self.slots = {}
	for i = 1,4 do
		local node = nodes:FindChild("Node" .. i)
		self.slots[i] = self:InstantiateSlot(node, "Slot" .. i)
		self.slots[i].slotID = i
	end
end

function UIMemberImprove:Start()
	LuaHelper.RegisterPlatMsgHandler(MsgID.TattooOperationRespID, self:MakeOperationHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.TattooUpgradeRespID, self:MakeUpgradeHandler(), self.uiName)
	self:refresh_tattoo()
end

function UIMemberImprove:FixedUpdate( ... )
	-- body
end

function UIMemberImprove:OnClose( ... )
	TopPanelManager:HideTopPanel()
end

function UIMemberImprove:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TattooOperationRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TattooUpgradeRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-----------------------------------------------------------------
function UIMemberImprove:refresh_tattoo()
	local role = MainPlayer.Instance:GetRole(self.member_item.id)
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
end




function UIMemberImprove:click_upgrade()
	return function()
		print('UIMemberImprove:click_upgrade is called')
		local t = createUI('MemberUpgradePopup',self.transform)
		self.upgrade_popup = getLuaComponent(t)
		self.upgrade_popup:set_member_item(self.member_item)
		self.upgrade_popup.close_dlg = self:upgrade_popup_close_dlg()
		NGUITools.BringForward(t)	  
	end
end

function UIMemberImprove:upgrade_popup_close_dlg()
	return function()
		print('UIMemberImrpove upgrade_popup_close_del() called')
		local item = self.member_item
		item:refresh()
		self.player_display:set_member_data(item.id,item.quality)
		self.player_display:refresh_data()
		self.member_item = item
		self.go.quality_label.text = getQualitystr(item.quality)
	end
end

function UIMemberImprove:set_member_item(item)
	self.player_display:set_member_data(item.id,item.quality)
	self.player_display:refresh()
	self.member_item = item
	self.go.quality_label.text = getQualitystr(item.quality)
end

function UIMemberImprove:InstantiateSlot(node, name)
	local go = createUI("TattooSlot", node)
	go.name = name
	local slot = getLuaComponent(go)
	slot.onClick = self:MakeOnSlotClick()
	slot.onUnequip = self:MakeOnUnequip()
	slot.onReplace = self:MakeOnReplace()
	slot.onDetail = self:MakeOnDetail()
	slot.onUpgrade = self:MakeOnUpgrade()
	slot.describe = ""
	return slot
end

-- tattoo click start
function UIMemberImprove:MakeOnSlotClick()
	return function (slot)
		if self.curSlot ~= nil then self.curSlot:HideMenu() end
		self.curSlot = slot
		if slot.isEquiped then
			slot:ShowMenu()
		else
			-- if not self:HasUnequipedTattoo(slot.tattooType) then
			-- 	CommonFunction.ShowPopupMsg(getCommonStr("GOODS_NO_THIS_TYPE"),nil,nil,nil,nil,nil)
			-- 	return
			-- end
			-- local t = {}
			-- t.goodsCategory = GoodsCategory.GC_TATTOO
			-- t.tattooType = TattooType.IntToEnum(slot.slotID)
			-- t.onOKClick = self:MakeOnEquip()
			-- self.tattooList = TopPanelManager:ShowPanel('UIGoodsList',nil,t)
		end
	end
end

function UIMemberImprove:HasUnequipedTattoo(tattooType)
	print("HasUni type="..tostring(tattooType))
	local enum = MainPlayer.Instance.TattooGoodsList:GetEnumerator()
	while enum:MoveNext() do
		print("tattoid=="..tostring(enum.Current.Value:GetID()))
		print("enum.Current.Value:IsEquip()="..tostring(enum.Current.Value:IsEquip()))
		print("enum.Current.Value:GetSubCategory()="..tostring(enum.Current.Value:GetSubCategory()))
		if not enum.Current.Value:IsEquip() and enum.Current.Value:GetSubCategory() == tattooType then
			print("return true")
			return true
		end
	end
end

function UIMemberImprove:MakeOnEquip()
	return function (goods)
		print("Equip tattoo, uuid: " .. tostring(goods:GetUUID()) .. " slot: " .. self.curSlot.slotID)
		local operation = {
			type = "TOT_EQUIP",
			uuid = goods:GetUUID(),
			role_id = self.member_item.id,
			slot_id = self.curSlot.slotID,
		}

		print("UIMemberImprove euqip tattoo role id="..tostring(operation.role_id).." slot_id="..tostring(operation.slot_id).."uuid="..tostring(operation.uuid))
		
		local req = protobuf.encode("fogs.proto.msg.TattooOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.TattooOperationID, req)
		CommonFunction.ShowWait()
	end
end

function UIMemberImprove:MakeOnConfirmUpgrade()
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
		CommonFunction.ShowWait()
	end
end

function UIMemberImprove:MakeOnUnequip()
	return function (slot)
		slot:HideMenu()
		
		local operation = {
			type = "TOT_UNEQUIP",
			uuid = slot.goods:GetUUID(),
			role_id = self.member_item.id,
			slot_id = slot.slotID,
		}
		
		local req = protobuf.encode("fogs.proto.msg.TattooOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.TattooOperationID, req)
		CommonFunction.ShowWait()
	end
end

function UIMemberImprove:MakeOnReplace()
	return function (slot)
		-- slot:HideMenu()
		-- local t = {}
		-- t.goodsCategory = GoodsCategory.GC_TATTOO
		-- t.tattooType = TattooType.IntToEnum(slot.slotID)
		-- t.onOKClick = self:MakeOnEquip()
		-- self.tattooList = TopPanelManager:ShowPanel('UIGoodsList',nil,t)
	end
end

function UIMemberImprove:MakeOnDetail()
	return function (slot)
		slot:HideMenu()
		local detail = getLuaComponent(createUI("TattooDetailPopup", self.transform))
		NGUITools.BringForward(detail.gameObject)
		detail.tattooID = slot.goods:GetID()
		detail.level = slot.level
	end
end

function UIMemberImprove:MakeOnUpgrade()
	return function (slot)
		slot:HideMenu()
		self.upgrade = getLuaComponent(createUI("TattooUpgradePopup", self.transform))
		NGUITools.BringForward(self.upgrade.gameObject)
		self.upgrade.uuid = slot.goods:GetUUID()
		self.upgrade.onUpgrade = self:MakeOnConfirmUpgrade()
	end
end

function UIMemberImprove:click_back()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

-- tattoo server start
function UIMemberImprove:MakeOperationHandler()
	return function (buf)
		-- if not  NGUITools.GetActive(self.gameObject) then
		--	return
		-- end
		CommonFunction.StopWait()
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
			error("UIMemberImprove: " .. err)
		end
	end
end

function UIMemberImprove:MakeUpgradeHandler()
	return function (buf)
		-- if not  NGUITools.GetActive(self.gameObject) then
		-- return
		-- end

		local resp, err = protobuf.decode("fogs.proto.msg.TattooUpgradeResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleUpgrade(resp)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UIMemberImprove: " .. err)
		end
	end
end

--  tattoo server-handle start
function UIMemberImprove:HandleUpgrade(resp)
	-- local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, resp.uuid)
	-- if not goods then
	-- 	error("UITattoo: Can not find upgraded tattoo: ", resp.uuid)
	-- end
	-- for _, slot in ipairs(self.slots) do
	-- 	if slot.uuid == goods:GetUUID() then
	-- 		slot:Equip(goods:GetUUID())
	-- 	end
	-- end

	-- if self.upgrade then 
	-- 	NGUITools.Destroy(self.upgrade.gameObject)
	-- 	self.upgrade = nil
	-- end
	-- self.player_display:refresh_ui()
end



function UIMemberImprove:HandleEquip(resp)
	print("UIMemberImprove HandleEquip()")

	local slot = self.slots[resp.slot_id]
	slot:Equip(resp.uuid)
	if self.tattooList then 
		TopPanelManager:HideTopPanel()
		-- print("Destory self.tattooList.transform.name=")
		-- NGUITools.Destroy(self.tattooList.gameObject)
		-- self.tattooList = nil
	end
	self.player_display:refresh_ui()

	print("UIMemberImprove HandleEquip()-end")
end

function UIMemberImprove:HandleUnequip(resp)
	print("UIMemberImprove HanldleUnequip slot_id="..tostring(resp.slot_id))
	local slot = self.slots[resp.slot_id]
	slot:Open()
	self.player_display:refresh_ui()
end

-- tatto server-handle end

return UIMemberImprove

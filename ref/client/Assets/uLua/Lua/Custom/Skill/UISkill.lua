--encoding=utf-8

UISkill = {
	uiName = 'UISkill',

	----------------------------------
	PASSIVE_SKILL_COUNT = 3,
	ACTIVE_SKILL_COUNT = 5,
	FANCY_SKILL_COUNT = 2,
	VIP_SKILL_COUNT = 3,

	SwitchType = {
		ACTIVE = 0,
		PASSIVE = 1,
	}

	----------------------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UISkill:Awake()	
	local window = self.transform:FindChild("Window")

	addOnClick(window:FindChild("Background").gameObject, self:MakeOnBGClick())

	local backNode = window:FindChild("BackButtonNode")
	getLuaComponent(createUI("ButtonBack", backNode)).onClick = self:MakeOnBackClick()

	self.tmModel = window:FindChild("Model")

	local btnTattoo = window:FindChild("Functions/Grid/Tattoo").gameObject
	local btnTraining = window:FindChild("Functions/Grid/Training").gameObject
	local btnFashion = window:FindChild("Functions/Grid/Fashion").gameObject
	addOnClick(btnTattoo, self:MakeOnTattooClick())
	addOnClick(btnTraining, self:MakeOnTrainingClick())
	addOnClick(btnFashion, self:MakeOnFashionClick())

	local switches = window:FindChild("Switches");
	self.btnActive = switches:FindChild("Active").gameObject
	self.btnPassive = switches:FindChild("Passive").gameObject
	addOnClick(self.btnActive, self:MakeOnSwitch(self.SwitchType.ACTIVE))
	addOnClick(self.btnPassive, self:MakeOnSwitch(self.SwitchType.PASSIVE))

	self.panelActive = window:FindChild("Active").gameObject
	NGUITools.SetActive(self.panelActive, true)
	self.slotActive = {}
	for i = 1, self.ACTIVE_SKILL_COUNT do
		local node = self.panelActive.transform:FindChild("Nodes/Node" .. i)
		self.slotActive[i] = self:InstantiateSlot(node, "ActiveSlot" .. i)
		self.slotActive[i].slotID = 2000 + i
	end
	self.slotVIP = {}
	for i = 1, self.VIP_SKILL_COUNT do
		local node = self.panelActive.transform:FindChild("Nodes/VipNode" .. i)
		self.slotVIP[i] = self:InstantiateSlot(node, "VipSlot" .. i)
		self.slotVIP[i].slotID = 4000 + i
		self.slotVIP[i].isVIP = true
	end
	self.panelPassive = window:FindChild("Passive").gameObject
	NGUITools.SetActive(self.panelPassive, true)
	self.slotPassive = {}
	for i = 1, self.PASSIVE_SKILL_COUNT do
		local node = self.panelPassive.transform:FindChild("Nodes/Node" .. i)
		self.slotPassive[i] = self:InstantiateSlot(node, "PassiveSlot" .. i)
		self.slotPassive[i].slotID = 1000 + i
	end
	NGUITools.SetActive(self.panelActive, true)

	addOnClick(self.transform:FindChild("Window/Buttonskillstore").gameObject,self:click_skill_store())

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UISkill:Start()
	LuaHelper.RegisterPlatMsgHandler(MsgID.SkillOperationRespID, self:MakeOperationHandler(), self.uiName)

	self.playerDisplay = getLuaComponent(createUI("PlayerDisplay", self.tmModel))
	self.playerDisplay:set_captain_data(MainPlayer.Instance.CaptainID, MainPlayer.Instance.Captain.m_roleInfo.bias)
	self.playerDisplay.is_captain = true
	self.playerDisplay.is_show_lock = false

	self:MakeOnSwitch(self.SwitchType.ACTIVE)()
end

function UISkill:FixedUpdate( ... )
	-- body
end

function UISkill:OnClose( ... )
	if self.curSlot then
		self.curSlot:HideMenu()
	end
	TopPanelManager:ShowPanel("UIHall")
end

function UISkill:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.SkillOperationRespID, self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UISkill:Refresh()
	local role = MainPlayer.Instance.Captain
	if role.m_roleInfo.skill_slot_info then
		local enum = role.m_roleInfo.skill_slot_info:GetEnumerator()
		while enum:MoveNext() do	--enumerate slots
			local skillSlot = enum.Current
			--print("Slot id: " .. skillSlot.id)
			local skillType = math.floor(skillSlot.id / 1000);
			local index = skillSlot.id % 1000;
			local slot
			print("Slot ID:", skillSlot.id, "Is unlock", skillSlot.is_unlock)
			if skillType == 1 then		--passive skill
				slot = self.slotPassive[index]
			elseif skillType == 2 then	--active skill
				if math.floor(index / 100) == 0 then
					slot = self.slotActive[index % 100]
				else
					slot = self.slotVIP[index % 100]
				end
			elseif skillType == 3 then	--fancy skill
				slot = self.slotFancy[index]
			elseif skillType == 4 then	--VIP skill
				slot = self.slotVIP[index]
			end

			if skillSlot.is_unlock == 1 then	--unlocked
				if skillSlot.skill_uuid ~= 0 then	--equiped
					slot:Equip(skillSlot.skill_uuid)
				else	--opened
					slot:Open()
				end
			else	--locked
				local slotConfig = GameSystem.Instance.SkillConfig:GetSlot(skillSlot.id)
				slot:Lock(slotConfig.level_required)
			end
		end
	end

	if self.playerDisplay then
	    self.playerDisplay:update_id(MainPlayer.Instance.CaptainID)
	    self.playerDisplay:refresh()
	end
end


-----------------------------------------------------------------
function UISkill:click_skill_store()
	return function()
		UIStore:SetType('ST_SKILL')
		UIStore:SetBackUI(self.uiName)
		if validateFunc('UIStore' or '') then
			UIStore:OpenStore()
		end
	end
end

function UISkill:InstantiateSlot(node, name)
	local go = createUI("SkillSlot", node)
	go.name = name
	local slot = getLuaComponent(go)
	slot.onClick = self:MakeOnClick()
	slot.onUnequip = self:MakeOnUnequip()
	slot.onReplace = self:MakeOnReplace()
	slot.onDetail = self:MakeOnDetail()
	slot.onUpgrade = self:MakeOnUpgrade()
	return slot
end

function UISkill:MakeOnClick()
	return function (slot)
		if self.curSlot ~= nil then self.curSlot:HideMenu() end
		self.curSlot = slot
		if slot.state == slot.State.EQUIPED then
			if slot.skillAttr.type == SkillType.PASSIVE then
				self:MakeOnUpgrade()(slot)
			else
				slot:ShowMenu()
			end
		elseif slot.state == slot.State.OPENED then
			if not self:HasUnequipedSkill() then
				CommonFunction.ShowPopupMsg(getCommonStr("GOODS_NO_THIS_TYPE"),nil,nil,nil,nil,nil)
				return
			end
			self.skillList = TopPanelManager:ShowPanel("UIGoodsList", nil,
				{
					goodsCategory = GoodsCategory.GC_SKILL,
					onOKClick = self:MakeOnEquip(),
				})
		end
	end
end

function UISkill:MakeOnEquip()
	return function (goods)
		local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(goods:GetID())
		--check role
		if skillAttr.roles.Count > 0 and not skillAttr.roles.Contains(MainPlayer.Instance.Captain.m_roleInfo.id) then
			print("Can not equip skill " .. skillAttr.id .. " to role " .. MainPlayer.Instance.Captain.m_roleInfo.id)
			local roleNames = ""
			local enum = skillAttr.roles:GetEnumerator()
			while enum:MoveNext() do
				local roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(enum.Current)
				roleNames = roleNames .. roleData.name .. " "
			end
			CommonFunction.ShowPopupMsg(string.format(getCommonStr("SKILL_EQUIPED_IN_CONDITIONS"), roleNames), self.skillList.transform,nil,nil,nil,nil)
			return
		end
		--check position
		if skillAttr.positions.Count > 0 and not skillAttr.positions:Contains(enumToInt(MainPlayer.Instance.Captain.m_position)) then
			print("Can not equip skill " .. skillAttr.id .. " to position " .. MainPlayer.Instance.Captain.m_position)
			local positionNames = ""
			local enum = skillAttr.positions:GetEnumerator()
			while enum:MoveNext() do
				positionNames = positionNames .. getCommonStr("gPositionName" .. enum.Current) .. " "
			end
			CommonFunction.ShowPopupMsg(string.format(getCommonStr("SKILL_EQUIPED_IN_ROLE_CONDITIONS"), positionNames), self.skillList.transform,nil,nil,nil,nil)
			return
		end
		--check repeat equip
		local enum = MainPlayer.Instance.Captain.m_roleInfo.skill_slot_info:GetEnumerator()
		while enum:MoveNext() do
			local slotGoods = MainPlayer.Instance:GetGoods(fogs.proto.msg.GoodsCategory.GC_SKILL, enum.Current.skill_uuid)
			if slotGoods and slotGoods:GetID() == skillAttr.id and (enum.Current.id ~= self.curSlot.slotID or enum.Current.skill_uuid == goods:GetUUID())  then
				print("Skill " .. skillAttr.id .. " already equiped. UUID: " .. goods:GetUUID() .. " equiped UUID: " .. enum.Current.skill_uuid)
				CommonFunction.ShowPopupMsg(getCommonStr("SKILL_CANNOT_EQUIPED_REPEAT"), self.skillList.transform,nil,nil,nil,nil) 
				return
			end
		end
		--check attr condition
		enum = skillAttr.equip_conditions:GetEnumerator()
		local captainAttr = MainPlayer.Instance.Captain.m_attrData.attrs
		while enum:MoveNext() do
			local symbol = GameSystem.Instance.AttrNameConfigData:GetAttrSymbol(enum.Current.Key)
			local value = captainAttr:get_Item(symbol)
			if not value or value < enum.Current.Value then
				local tip = getLuaComponent(createUI("SkillEquipConditionTip"))
				bringNear(tip.transform)
				tip.attrID = enum.Current.Key
				tip.attrValue = enum.Current.Value
				UIManager.Instance:BringPanelForward(tip.gameObject)
				return
			end
		end
		
		print("Equip skill, uuid: " .. tostring(goods:GetUUID()) .. " slot: " .. self.curSlot.slotID)
		local operation = {
			type = "SOT_EQUIP",
			role_id = MainPlayer.Instance.CaptainID,
			slot_id = self.curSlot.slotID,
			skill_uuid = goods:GetUUID(),
		}

		local req = protobuf.encode("fogs.proto.msg.SkillOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.SkillOperationID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UISkill:MakeOnUnequip()
	return function (slot)
		slot:HideMenu()

		local operation = {
			type = "SOT_UNEQUIP",
			role_id = MainPlayer.Instance.CaptainID,
			slot_id = slot.slotID,
		}

		local req = protobuf.encode("fogs.proto.msg.SkillOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.SkillOperationID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UISkill:MakeOnReplace()
	return function (slot)
		slot:HideMenu()
		if not self:HasUnequipedSkill() then
			CommonFunction.ShowPopupMsg(getCommonStr("GOODS_NO_THIS_TYPE"),nil,nil,nil,nil,nil)
			return
		end
		self.skillList = TopPanelManager:ShowPanel("UIGoodsList", nil,
			{
				goodsCategory = GoodsCategory.GC_SKILL,
				onOKClick = self:MakeOnEquip(),
			})
	end
end

function UISkill:MakeOnDetail()
	return function (slot)
		slot:HideMenu()
		local goDetail = createUI("SkillDetailPopup", self.transform)
		local detail = getLuaComponent(goDetail)
		bringNear(detail.transform)
		NGUITools.BringForward(detail.gameObject)
		detail.skillID = slot.skillAttr.id
		detail.level = slot.level
	end
end

function UISkill:MakeOnUpgrade()
	return function (slot)
		slot:HideMenu()
		local goUpgrade = createUI("SkillUpgradePopup", self.transform)
		self.upgrade = getLuaComponent(goUpgrade)
		bringNear(self.upgrade.transform)
		NGUITools.BringForward(self.upgrade.gameObject)
		self.upgrade.skillID = slot.skillAttr.id
		self.upgrade.level = slot.level + 1
		self.upgrade.onUpgrade = self:MakeOnConfirmUpgrade()
	end
end

function UISkill:MakeOnConfirmUpgrade()
	return function ()
		local operation = {
			type = "SOT_UPGRADE",
			role_id = MainPlayer.Instance.CaptainID,
			skill_uuid = self.curSlot.uuid,
		}

		local req = protobuf.encode("fogs.proto.msg.SkillOperation", operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.SkillOperationID, req)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UISkill:MakeOperationHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		if not NGUITools.GetActive(self.gameObject) and not self.skillList then return end
		local resp, err = protobuf.decode("fogs.proto.msg.SkillOperationResp", buf)
		if resp then
			if resp.result == 0 then
				if resp.type == "SOT_UPGRADE" then
					self:HandleUpgrade(resp)
					playSound("UI/UI_level_up_01")
				elseif resp.type == "SOT_EQUIP" then
					self:HandleEquip(resp)
				elseif resp.type == "SOT_UNEQUIP" then
					self:HandleUnequip(resp)
				elseif resp.type == "SOT_UNLOCK_SLOT" then
					self:HandleUnlock(resp)
				end
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)

				playSound("UI/UI-wrong")
			end
		else
			error("UISkill: " .. err)
		end
	end
end

function UISkill:HandleUpgrade(resp)
	if self.curSlot.goods and self.curSlot.goods:GetUUID() == resp.skill_uuid then
		self.curSlot:Equip(resp.skill_uuid)
	end

	if self.upgrade then 
		NGUITools.Destroy(self.upgrade.gameObject)
		self.upgrade = nil
	end

	CommonFunction.ShowPopupMsg(getCommonStr("SKILL_UPGRADE_TO"):format(resp.skill_level),nil,nil,nil,nil,nil)
end

function UISkill:HandleEquip(resp)
	local slot = self:GetSlot(resp.slot_id)
	slot:Equip(resp.skill_uuid)
	TopPanelManager:HideTopPanel("UIGoodsList")
	self.skillList = nil
end

function UISkill:HandleUnequip(resp)
	local slot = self:GetSlot(resp.slot_id)
	slot:Open()
end

function UISkill:HandleUnlock(resp)
	local slot = self:GetSlot(resp.slot_id)
	if slot.state == slot.State.LOCKED then
		slot:Open()
	end
end

function UISkill:GetSlot(slotID)
	local skillType = math.floor(slotID / 1000)
	local index = slotID % 1000
	local slot
	if skillType == 1 then
		slot = self.slotPassive[index]
	elseif skillType == 2 then
		if math.floor(index / 100) == 0 then
			slot = self.slotActive[index % 100]
		else
			slot = self.slotVIP[index % 100]
		end
	elseif skillType == 3 then
		slot = self.slotFancy[index]
	elseif skillType == 4 then
		slot = self.slotVIP[index]
	end
	return slot
end

function UISkill:HasUnequipedSkill()
	local enum = MainPlayer.Instance.SkillGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if not goods:IsEquip() then
			local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(goods:GetID())
			if skillAttr.type ~= SkillType.PASSIVE then
				return true
			end
		end
	end
	return false
end

function UISkill:MakeOnSwitch(switchType)
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		if switchType ~= self.curSwitchType then
			self.curSwitchType = switchType
			NGUITools.SetActive(self.panelActive, self.curSwitchType == self.SwitchType.ACTIVE)
			NGUITools.SetActive(self.panelPassive, self.curSwitchType == self.SwitchType.PASSIVE)
		end
	end
end

function UISkill:MakeOnBGClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
	end
end

function UISkill:MakeOnBackClick()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UISkill:MakeOnTattooClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UISkill: tattoo clicked.")
		TopPanelManager:ShowPanel("UITattoo")
	end
end

function UISkill:MakeOnTrainingClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UISkill: training clicked.")
		TopPanelManager:ShowPanel("UITraining")
	end
end

function UISkill:MakeOnFashionClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UISkill: fashion clicked.")
	end
end


return UISkill

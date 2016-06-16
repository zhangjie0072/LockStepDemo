--encoding=utf-8

UISquad = {
	uiName = "UISquad",

	-------------PARAMETERS
	roleBustItem,

	selectedRoleObject,

	selectStatus,
	selectSlotID,

	selectedRoleID,

	equipGoods = {},

	msgReceiveTrigger,

	menuScript,

	equipedList = {},

	equipTipList = {},

	changedEquipList = {},
	equipmentChange,

	errorOccur,

	isChoosePlayer = false,

	isAttrChange = false,

	playerAttr = {},

	showAttr,

	interval = 0,

	destroyTime = 0,

	isInitTip = false,

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,

	needResetPosition = false,
	detail,
	idList,
	roleIconList,
	switchState = 0,
	popupErrorTime = 0,
	banTwice = false,
	fightNum,
	-----------------UI
	uiPanel,

	uiBtnBack,
	-- uiBtnMenu,

	uiPlayer = {},
	uiTeamFight,

	uiBtnChange,
	uiBtnUp,
	uiBtnUpRedDot,

	uiRoleBustItem,

	uiScroll,
	uiAttrGrid,

	uiSuit,
	uiSuitBg,
	uiSuitTable,

	uiGoodsGrid,
	uiSlot = {},

	uiAttrItem = {},

	uiBtnEnhance,
	uiBtnEquip,
	uiBtnPath,

	uiAnimator,
	uiAttrTip,
	uiFightNum,
}


-----------------------------------------------------------------
function UISquad:Awake()
	local transform = self.transform

	self.uiPanel = transform:GetComponent("UIPanel")
	self.uiBtnBack = transform:FindChild("Top/ButtonBack")
	-- self.uiBtnMenu = transform:FindChild("Top/ButtonMenu")

	self.uiPlayer[1] = transform:FindChild("LeftRole/Player/Player1/CareerRoleIcon")
	self.uiPlayer[2] = transform:FindChild("LeftRole/Player/Player2/CareerRoleIcon")
	self.uiPlayer[3] = transform:FindChild("LeftRole/Player/Player3/CareerRoleIcon")

	self.uiTeamFight = transform:FindChild("LeftRole/Num"):GetComponent("UILabel")

	self.uiBtnChange = transform:FindChild("LeftRole/ButtonChange")
	self.uiBtnUp = transform:FindChild("LeftRole/ButtonUp")
	self.uiBtnUpRedDot = transform:FindChild("LeftRole/ButtonUp/RedDot")

	self.uiRoleBustItem = transform:FindChild("RightList/RoleBustItem")

	self.uiScroll = transform:FindChild('RightList/Scroll'):GetComponent('UIScrollView')
	self.uiAttrGrid = transform:FindChild("RightList/Scroll/Base/AttrGrid")
	self.uiAttrItem[1] = {
		value = self.uiAttrGrid:FindChild("1/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("1/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[2] = {
		value = self.uiAttrGrid:FindChild("2/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("2/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[3] = {
		value = self.uiAttrGrid:FindChild("3/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("3/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[4] = {
		value = self.uiAttrGrid:FindChild("4/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("4/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[5] = {
		value = self.uiAttrGrid:FindChild("5/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("5/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[6] = {
		value = self.uiAttrGrid:FindChild("6/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("6/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[7] = {
		value = self.uiAttrGrid:FindChild("7/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("7/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[8] = {
		value = self.uiAttrGrid:FindChild("8/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("8/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[9] = {
		value = self.uiAttrGrid:FindChild("9/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("9/Symbol"):GetComponent("UILabel"),
	}
	self.uiAttrItem[10] = {
		value = self.uiAttrGrid:FindChild("10/Value"):GetComponent("UILabel"),
		symbol = self.uiAttrGrid:FindChild("10/Symbol"):GetComponent("UILabel"),
	}

	self.uiGoodsGrid = transform:FindChild("RightList/GoodsGrid")--:GetComponent("UIGrid")
	self.uiSlot[1] = self.uiGoodsGrid:FindChild("1")
	self.uiSlot[2] = self.uiGoodsGrid:FindChild("2")
	self.uiSlot[3] = self.uiGoodsGrid:FindChild("3")
	self.uiSlot[4] = self.uiGoodsGrid:FindChild("4")
	self.uiSlot[5] = self.uiGoodsGrid:FindChild("5")
	for k, v in pairs(self.uiSlot) do
		addOnClick(v.gameObject, self:OnEquipItemClick())
	end

	self.uiSuit = transform:FindChild('RightList/Scroll/Suit').gameObject
	self.uiSuitBg = self.uiSuit.transform:FindChild('Background'):GetComponent('UISprite')
	self.uiSuitTable = self.uiSuit.transform:FindChild("SuitTable"):GetComponent('UITable')

	self.uiBtnEnhance = transform:FindChild("RightList/ButtonEnhance")
	self.uiBtnEquip = transform:FindChild("RightList/ButtonEquip")

	self.uiBtnPath = transform:FindChild("RightList/ButtonPath")

	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiFightNum = self.transform:FindChild("LeftRole/FightingForce/FightNum")

	self.fightNum = getLuaComponent(createUI("FightNum", self.uiFightNum))
end

function UISquad:Start()
	local back = getLuaComponent(createUI("ButtonBack",self.uiBtnBack))
	back.onClick = self:OnBackClick()


	-- local menu = createUI("ButtonMenu", self.uiBtnMenu)
	-- menu:GetComponent('UIPanel').depth = self.uiPanel.depth + 1
	-- self.menuScript = getLuaComponent(menu);
	-- self.menuScript:SetParent(self.gameObject, false)

	--addOnClick(self.uiBtnMenu.gameObject, self:OnMenuClick())
	addOnClick(self.uiBtnChange.gameObject, self:OnChangeClick())
	addOnClick(self.uiBtnUp.gameObject, self:OnUpClick())
	addOnClick(self.uiBtnEnhance.gameObject, self:OnEnhanceClick())
	addOnClick(self.uiBtnEquip.gameObject, self:OnEquipClick())
	addOnClick(self.uiBtnPath.gameObject, self:OnPathClick())

	self.showAttr = {}
	self.uiAttrTip = {}

	self:InitPlayerList()

	--
	LuaHelper.RegisterPlatMsgHandler(MsgID.EquipOperationRespID, self:OnEquipmentMsgHandle(), self.uiName)

end

function UISquad:FixedUpdate()
	-- self:RefreshSlotRedDot()

	if self.interval >= 0.5 then
		for k,v in pairs(self.showAttr) do
			table.insert(self.uiAttrTip, self:ShowAttrUpdate(k, v))
			break
		end
		self.interval = 0
	else
		self.interval = self.interval + UnityTime.fixedDeltaTime
	end

	if next(self.uiAttrTip) then
		self.destroyTime = self.destroyTime + 0.5 * UnityTime.fixedDeltaTime
		if self.destroyTime >= 0.5 then
			NGUITools.Destroy(self.uiAttrTip[1].gameObject)
			local tip = {}
			for i,v in ipairs(self.uiAttrTip) do
				if i ~= 1 then
					table.insert(tip, v)
				end
			end
			self.uiAttrTip = tip
			self.destroyTime = 0
		end
	end

	if self.popupErrorTime > 0 then
		self.popupErrorTime = self.popupErrorTime - UnityTime.fixedDeltaTime
	end

	if self.needResetPosition then
		self.uiScroll:ResetPosition()
		self.uiSuitTable:Reposition()
		self.needResetPosition = false
	end
end

function UISquad:OnClose()
	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end

	for k,v in pairs(self.uiAttrTip) do
		NGUITools.Destroy(v.gameObject)
	end
	self.uiAttrTip = {}
	self.showAttr = {}
end

function UISquad:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UISquad:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.EquipOperationRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UISquad:Refresh()
	self:RefreshPlayerListRedDot()
	self:InitEquipment()
	--if types and tostring(types) == 'EOT_UPGRADE_SINGLE' or tostring(types) == 'EOT_UPGRADE_AUTO' then return end
	self:InitAttr()
	-- self:RefreshRedDot()

	if MainPlayer.Instance.LinkEnable then
		self:OnPlayerClick()(self.roleIconList[MainPlayer.Instance.LinkRoleId])
		self:OnUpClick(MainPlayer.Instance.LinkTab)()
		self.nextShowUI = "UIHall"
	end
	for k, v in pairs(self.roleIconList) do
		v:Refresh()
	end

	if self.roleBustItem then
		self.roleBustItem:Refresh()
	end

end


-----------------------------------------------------------------
function UISquad:InitPlayerList( ... )
	self.selectedRoleObject = nil
	self.idList = {}
	self.roleIconList = {}
	for i = 1, 3 do
		local script = instantiateBySquad('CareerRoleIcon',
			{status = FightStatus.IntToEnum(i), transform = self.uiPlayer[i], rename=self.uiPlayer[i].parent})
		if script then
			script.onClick = self:OnPlayerClick()
			script:Refresh()
			--addOnClick(roleIcon.gameObject, self:OnPlayerClick())
			--默认选择第一个
			if i == 1 then
				self:OnPlayerClick()(script)
			end
			table.insert(self.idList, script.id)
			self.roleIconList[script.id] = script
		end
	end
end

function UISquad:InitRoleBustItem( ... )
	if self.selectedRoleID == nil then
		print("error -- select one role first !")
		return
	end

	local roleBustItemObj
	if self.uiRoleBustItem.transform.childCount > 0 then
		roleBustItemObj = self.uiRoleBustItem.transform:GetChild(0)
	end
	if roleBustItemObj == nil then
		roleBustItemObj = createUI('RoleBustItem1', self.uiRoleBustItem.transform)
	end
	self.roleBustItem = getLuaComponent(roleBustItemObj)
	self.roleBustItem.id = self.selectedRoleID
	self.roleBustItem:Refresh()
end

function UISquad:ClearAttr( ... )
	for k,  v in pairs(self.uiAttrItem) do
		v.value.text = '0'
	end
end

function UISquad:InitAttr( ... )
	self:ClearAttr()
	local attrList = {}
	local suitList = {}

	local equipmentConfig = GameSystem.Instance.EquipmentConfigData

	for k, goods in pairs(self.equipGoods) do
		local config = equipmentConfig:GetBaseConfig(goods:GetID(), goods:GetLevel())
		if config then
			local enum = config.addn_attr:GetEnumerator()
			while enum:MoveNext() do
				local attrID = enum.Current.Key
				--attrList[enum.Current.Key] = attrList[enum.Current.Key] and attrList[enum.Current.Key] + enum.Current.Value or enum.Current.Value
				attrList[attrID] = attrList[attrID] or 0
				attrList[attrID] = attrList[attrID] + MainPlayer.Instance:GetEquipmentAttr(
					self.selectedRoleID,
					attrID,
					MainPlayer.Instance.EquipInfo,
					MainPlayer.Instance.SquadInfo)
			end
		else
			print('error -- can not get configuration by goodsID: ', goods:GetID(), ' and level: ', goods:GetLevel())
		end
		local suitConfig = GameSystem.Instance.GoodsConfigData:GetSuitAttrConfig(goods:GetID())
		if suitConfig then
			if suitList[suitConfig.suitID] == nil then
				local enum = suitConfig.addn_attr:GetEnumerator()
				while enum:MoveNext() do
					local enumAttr = enum.Current.Value:GetEnumerator()
					while enumAttr:MoveNext() do
						local attrID = enumAttr.Current.Key
						local addnAttr = 0
						local multiAttr = 0
						attrList[attrID] = attrList[attrID] or 0
						-- attrList[attrID] = attrList[attrID] + MainPlayer.Instance:GetEquipmentSuitAttr(
						--	self.selectedRoleID,
						--	attrID,
						--	addnAttr, multiAttr,
						--	MainPlayer.Instance.EquipInfo,
						--	MainPlayer.Instance.SquadInfo)
						--attrList[attrID] = attrList[attrID] + attrList[attrID] *  multiAttr / 1000
					end
				end
				suitList[suitConfig.suitID] = suitConfig.suitID
			end
		end
	end
	local attrNameConfig = GameSystem.Instance.AttrNameConfigData
	local vTbl = {}
	local attrData = MainPlayer.Instance:GetRoleAttrsByID(self.selectedRoleID).attrs

	-- for attrID, attrValue in pairs(attrList) do
	--	local symbol = attrNameConfig:GetAttrSymbol(attrID)
	--	local attrAll = attrData[symbol]
	--	for k, v in pairs(self.uiAttrItem) do
	--		if v.symbol.text == symbol then
	--			vTbl[symbol] = vTbl[symbol] and vTbl[symbol] + attrValue or attrValue
	--			local eqValue = vTbl[symbol]
	--			local vStr = tostring(attrAll)
	--			if eqValue ~= 0 then
	--				vStr = vStr .."[d58b0e](+"..eqValue..")[-]"
	--			end
	--			v.value.text = vStr
	--		end
	--	end
	-- end
	local roleInfo = MainPlayer.Instance:GetRole2(self.selectedRoleID)
	for k, v in pairs(self.uiAttrItem) do
		local symbol = v.symbol.text
		local attr = attrNameConfig:GetAttrData(symbol)
		local attrID = attr.id
		local attrAll = attrData:get_Item(symbol)
		local vStr = tostring(attrAll)

		local attrValue = attrList[attrID]
		if attrValue then
			vTbl[symbol] = vTbl[symbol] and vTbl[symbol] + attrValue or attrValue
			local equipValue = vTbl[symbol]

			local talent = GameSystem.Instance.RoleBaseConfigData2:GetTalent(self.selectedRoleID)
			-- get equipment suit addr value.
			local equipInfo = MainPlayer.Instance.EquipInfo
			local squadInfo = MainPlayer.Instance.SquadInfo
			local addnAttr, addnAttr, _ =  MainPlayer.Instance:GetEquipmentSuitAttr(self.selectedRoleID,attrID, nil, nil, equipInfo,  squadInfo )
			-- print("1927 -  attrID=", attrID, "addnAttr=",addnAttr, "multiAttr=", multiAttr)
			equipValue = equipValue + addnAttr
			equipValue = equipValue

			-- get equipment suit multi value.
			local _, multiAttrValue = MainPlayer.Instance:GetAttrValue(roleInfo, attrID, nil, equipInfo, squadInfo, nil)
			-- print("1927 -  multiAttrValue=",multiAttrValue)
			equipValue = equipValue + math.modf(multiAttrValue)


			if equipValue ~= 0 then
				vStr = vStr .."[d58b0e](+"..equipValue..")[-]"
			end
		end
		v.value.text = vStr
	end

	local commonList = {}		--common arg
	for k,v in pairs(self.playerAttr[self.selectedRoleID]) do
		if v and vTbl[k] then
			commonList[k] = 1
			if v < vTbl[k] then
				local kk = tostring(k) .. ',' .. tostring(vTbl[k] - v)
				self.showAttr[kk] = 1
			elseif v > vTbl[k] then
				local kk = tostring(k) .. ',' .. tostring(v - vTbl[k])
				self.showAttr[kk] = -1
			end
		end
	end

	if not next(vTbl) then
		commonList = self.playerAttr[self.selectedRoleID]
		for k,v in pairs(commonList) do
			if v ~= 0 then
				local kk = tostring(k) .. ',' .. tostring(v)
				self.showAttr[kk] = -1
			end
		end
	end

	if not next(self.playerAttr[self.selectedRoleID]) and self.isInitTip == true then
		self.playerAttr[self.selectedRoleID] = vTbl
		self.isInitTip = false
	end

	if self.isInitTip == false and not next(self.playerAttr[self.selectedRoleID]) then
		commonList = vTbl
		for k,v in pairs(commonList) do
			if v ~= 0 then
				local kk = tostring(k) .. ',' .. tostring(v)
				self.showAttr[kk] = 1
			end
		end
	end

	if next(commonList) then
		for k,v in pairs(self.playerAttr[self.selectedRoleID]) do
			local num = 0
			for m,n in pairs(commonList) do
				if k == m then
					num = 1
				end
			end
			if num == 0 and v ~= 0 then
				local kk = tostring(k) .. ',' .. tostring(v)
				self.showAttr[kk] = -1
			end
		end

		for k,v in pairs(vTbl) do
			local num = 0
			for m,n in pairs(commonList) do
				if k == m then
					num = 1
				end
			end
			if num == 0 and v ~= 0 then
				local kk = tostring(k) .. ',' .. tostring(v)
				self.showAttr[kk] = 1
			end
		end
	end

	self.playerAttr[self.selectedRoleID] = vTbl
	self.needResetPosition = true

	self.fightNum:SetNum(GetTeamFight())
end

function UISquad:InitEquipment( ... )
	if self.selectedRoleID == nil or  self.selectStatus == nil then
		print("error -- select one role first !")
		return
	end
	local equipInfo = MainPlayer.Instance.EquipInfo
	local enum = equipInfo:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.pos == self.selectStatus then
			local slotInfo = enum.Current.slot_info
			local enumSlot = slotInfo:GetEnumerator()
			while enumSlot:MoveNext() do

				self.equipedList[enumSlot.Current.id] = enumSlot.Current.equipment_uuid
				-- if enumSlot.Current.id > 5 then
				--	print('error -- equipment num can not big than 5!')
				--	break
				-- end
				local enumID = enumSlot.Current.id
				local intID = enumToInt(enumID)
				local uuid = enumSlot.Current.equipment_uuid
				local child
				local slotGoodsIcon = self.uiSlot[intID]:FindChild('Icon')
				local slotIDLabel = self.uiSlot[intID]:FindChild('SlotID'):GetComponent('UILabel')
				slotIDLabel.text = enumID
				if slotGoodsIcon.childCount > 0 then
					child = slotGoodsIcon:GetChild(0)
				end
				local goods
				if uuid and uuid ~= 0 then
					goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_EQUIPMENT, uuid)
					if child == nil then
						child = createUI('GoodsIcon', slotGoodsIcon)
					end
					local goodsIcon = getLuaComponent(child)
					goodsIcon.goods = goods
					goodsIcon.hideNeed = true
					goodsIcon.hideLevel = true
					goodsIcon.needPlayAnimation = true
					goodsIcon.onClick = self:OnEquipmentInfoClick()
					goodsIcon.slotID = enumID
					goodsIcon:Refresh()
					-- if self.changedEquipList[intID] then
					-- 	goodsIcon:StartSparkle()
					-- end
				else
					if child then
						NGUITools.Destroy(child.gameObject)
					end
				end
				self.equipGoods[intID] = goods
			end
			break
		end
	end

	self:RefreshSlotRedDot()
	self:InitSuitAttr()
end

function UISquad:InitSuitAttr()
	self.uiSuit:SetActive(false)
	self.uiSuitBg.height = 1
	local cleaned = false
	local initTbl = {}
	for k, v in pairs(self.equipGoods) do
		if v:IsSuit() and initTbl[v:GetSuitID()] == nil then
			self.uiSuit:SetActive(true)
			if cleaned == false then
				CommonFunction.ClearTableChild(self.uiSuitTable.transform)
				cleaned = true
			end
			local suitObj = createUI('SuitInfoItem', self.uiSuitTable.transform)
			local suitScript = getLuaComponent(suitObj)
			suitScript.goods = v
			suitScript.equipGoodsList = self.equipGoods
			suitScript.parentBg = self.uiSuitBg
			suitScript.refreshParentSV = self:ResetScrollViewPosition()

			initTbl[v:GetSuitID()] = v:GetSuitID()
		end
	end
	self.uiSuitTable.repositionNow = true
	--self.needResetPosition = true
end

function UISquad:ResetScrollViewPosition()
	return function ( ... )
		self.needResetPosition = true
	end
end

function UISquad:OnBackClick( ... )
	return function (go)
		self:DoClose()
	end
end

function UISquad:OnMenuClick( ... )
	return function (go)
		-- body
	end
end

function UISquad:OnChangeClick( ... )
	return function (go)
		local showList = {}
		local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
		while enum:MoveNext() do
			showList[enum.Current.role_id] = enumToInt(enum.Current.status)
		end
		self.nextShowUI = "UIMember"
		self.nextShowUIParams = {isGoBack = true, showlist = showList, parent = self,toReplaceRoleId = self.selectedRoleID}
		getLuaComponent(self.selectedRoleObject.gameObject):SetSele(false)
		self:DoClose()
	end
end

function UISquad:OnUpClick(index)
	return function()
		self.detail = getLuaComponent(createUI("RoleDetail", self.transform))
		self.detail:SetData(self.selectedRoleID, self)
		self.detail.onClickClose = self:ClickDetailClose()
		self.detail.roleInPreState = self.roleBustItem
		-- self.detail.displayExerciseId = MainPlayer.Instance.LinkExerciseId
		UIManager.Instance:BringPanelForward(self.detail.gameObject)
		if index then
			self.detail.clickIndex = index
		end
		-- self.detail.displayExerciseId = exerciseId

	end
end


function UISquad:ClickDetailClose()
	return function()
		if self.detail then
			local curTab = self.detail.curTab
			NGUITools.Destroy( self.detail.gameObject)
			if self.switchState ~= 0 then
				local ri = 0
				for k, v in pairs(self.idList) do
					if v == self.roleBustItem.id then
						ri = k + self.switchState
					end
				end
				self:OnPlayerClick()(self.roleIconList[self.idList[ri]])
				self:OnUpClick(curTab)()
			else
				if MainPlayer.Instance.LinkRoleId ~= 0 then

					self:OnPlayerClick()(self.roleIconList[MainPlayer.Instance.LinkRoleId])
					self:OnUpClick(curTab)()
				end
			end
			self.switchState = 0

			self:Refresh()
			-- self.detail = nil
		end
	end
end
--一键强化
function UISquad:OnEnhanceClick( ... )
	return function (go)
		if self.banTwice == true then
			return
		end
		self.banTwice = true
		local slotInfo = {}
		for k, v in pairs(self.equipedList) do
			if v ~= 0 then
				local slotIDStr = k:ToString()
				table.insert(slotInfo, {id=slotIDStr, equipment_uuid = v})
			end
		end
		--发送消息
		local equipOperation = {
			type = 'EOT_UPGRADE_AUTO',
			info = {
				pos = self.selectStatus:ToString(),
				slot_info = slotInfo,
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
		CommonFunction.ShowWait()
	end
end

--一键装备
function UISquad:OnEquipClick( ... )
	return function (go)
		local equipmentList = {}
		--删选出品质最高的未装备的物品
		local enum = MainPlayer.Instance.EquipmentGoodsList:GetEnumerator()
		while enum:MoveNext() do
			local goodsUnequip = enum.Current.Value
			local cate = goodsUnequip:GetSubCategory()

			if goodsUnequip:IsEquip() == false and enumToInt(cate) ~= enumToInt(EquipmentType.ET_EQUIPMENTPIECE) then
				local qualityUnequip = enumToInt(goodsUnequip:GetQuality())
				local goodsEquip = self.equipGoods[enumToInt(cate)]
				if (goodsEquip == nil)
					or (qualityUnequip > enumToInt(goodsEquip:GetQuality()))
					or (qualityUnequip == enumToInt(goodsEquip:GetQuality()) and goodsUnequip:IsSuit() and goodsEquip:IsSuit() == false)
					or (qualityUnequip == enumToInt(goodsEquip:GetQuality()) and ( (goodsUnequip:IsSuit() and goodsEquip:IsSuit()) or (not goodsUnequip:IsSuit() and not goodsEquip:IsSuit()) )and goodsUnequip:GetLevel() > goodsEquip:GetLevel()) then
				--if equipmentList[cate] then
						equipmentList[cate] = goodsUnequip
						self.equipGoods[enumToInt(cate)] = goodsUnequip
					--else
						--equipmentList[cate] = goodsUnequip
						--self.equipGoods[enumToInt(cate)] = goodsUnequip
					--end
				end
			end
		end

		--
		local slotInfo = {}
		for k, v in pairs(equipmentList) do
			local slotIDStr = EquipmentSlotID.IntToEnum(enumToInt(k)):ToString()
			table.insert(slotInfo, {id=slotIDStr, equipment_uuid = v:GetUUID()})
		end

		if #slotInfo == 0 then
			if self.popupErrorTime <= 0 then
				CommonFunction.ShowPopupMsg(getCommonStr("EQUIPMENT_NOT_EXIST"), nil, nil, nil, nil, nil)
				self.popupErrorTime = 1
			end
			return
		end

		--发送消息
		local equipOperation = {
			type = 'EOT_EQUIP',
			info = {
				pos = self.selectStatus:ToString(),
				slot_info = slotInfo,
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
		CommonFunction.ShowWait()
	end
end

function UISquad:OnPathClick( ... )
	return function (go)
		-- body
	end
end

function UISquad:OnPlayerClick( ... )
	return function (iconScript)
		if self.selectedRoleObject == iconScript.gameObject then
			return
		end
		if self.selectedRoleObject then
			getLuaComponent(self.selectedRoleObject.gameObject):SetSele(false)
		end
		self.selectedRoleObject = iconScript.gameObject
		self.selectedRoleID = iconScript.id
		self.selectStatus = iconScript.status
		iconScript:SetSele(true)
		self.playerAttr[self.selectedRoleID] = {}
		self.isInitTip = true
		-- self.changedEquipList = {}
		self:InitRoleBustItem()
		self:InitEquipment()
		self:InitAttr()
		self.isChoosePlayer = false
		local roleState = (UpdateRedDotHandler.roleSkillList[self.selectedRoleID] and next(UpdateRedDotHandler.roleSkillList[self.selectedRoleID]) ~= nil)
					or UpdateRedDotHandler.roleLevelUpList[self.selectedRoleID]
					or UpdateRedDotHandler.roleEnhanceList[self.selectedRoleID]
					or UpdateRedDotHandler.roleImproveList[self.selectedRoleID] ~= nil
		NGUITools.SetActive(self.uiBtnUpRedDot.gameObject, roleState)
	end
end

function UISquad:OnEquipItemClick( ... )
	return function (go)
		local icon = go.transform:FindChild('Icon')
		if icon.childCount == 0 then
			local slotIDLabel = go.transform:FindChild('SlotID'):GetComponent('UILabel')
			local slotID = slotIDLabel.text
			if not self:OwnedSlot(EquipmentSlotID[slotID]) then
				CommonFunction.ShowTip(getCommonStr("NOT_OWNED_SUCH_CATEGORY_EQUIPMENT"), nil)
				return
			end

			self.selectSlotID = EquipmentSlotID[slotID]
			local script = getLuaComponent(createUI('UIEquipment', nil))
			script.equipGoodsList = self.equipGoods
			script.parent = self
			UIManager.Instance:BringPanelForward(script.gameObject)
		end
	end
end

function UISquad:OwnedSlot(slotId)
	local enum = MainPlayer.Instance.EquipmentGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		print("1927 -  goods:GetSubCategory()=",goods:GetSubCategory())
		print("1927 -  enumToInt(slotId)=",enumToInt(slotId))
		if enumToInt(goods:GetSubCategory()) == enumToInt(slotId) then
			return true
		end
	end
end

function UISquad:OnEquipmentInfoClick( ... )
	return function (goodsScript)
		local obj = createUI('EquipmentChange', self.transform)
		UIManager.Instance:BringPanelForward(obj.gameObject)
		local script = getLuaComponent(obj)
		self.selectSlotID = goodsScript.slotID
		script.goods = goodsScript.goods
		script.equipGoodsList = self.equipGoods
		script.parent = self
		self.equipmentChange = script
	end
end

function UISquad:OnEquipmentMsgHandle( ... )
	return function (buf)
		-- self.changedEquipList = {}
		local resp, err = protobuf.decode("fogs.proto.msg.EquipOperationResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				if self.equipmentChange then
					self.equipmentChange.operationSucess = true
				end
				for _, v in pairs(resp.info) do
					for k, slotInfo in pairs(v.slot_info) do
						local equipInfo = MainPlayer.Instance.EquipInfo
						local enum = equipInfo:GetEnumerator()
						while enum:MoveNext() do
							if enum.Current.pos:ToString() == v.pos then
								local enumSlot = enum.Current.slot_info:GetEnumerator()
								while enumSlot:MoveNext() do
									if enumSlot.Current.id:ToString() == slotInfo.id then
										-- if enumSlot.Current.equipment_uuid ~= slotInfo.equipment_uuid then
										-- 	local id = enumToInt(enumSlot.Current.id)
										-- 	self.changedEquipList[id] = 1
										-- end
										enumSlot.Current.equipment_uuid = slotInfo.equipment_uuid
										enumSlot.Current.equipment_id = slotInfo.equipment_id
										enumSlot.Current.equipment_level = slotInfo.equipment_level

										UpdateRedDotHandler.MessageHandler("Squad")
										UpdateRedDotHandler.MessageHandler("RoleDetail")
										break
									end
								end
								break
							end
						end
					end
				end
				--refresh slot redDot
				self:RefreshSlotRedDot()
				self:RefreshPlayerListRedDot()
				self.banTwice = false
			else
				if self.equipmentChange then
					self.equipmentChange.operationSucess = false
					self.banTwice = false
				end

				if self.errorOccur then
					self.errorOccur()
					self.banTwice = false
				end
				if resp.result == 456 then
					self:ShowBuyTip("BUY_GOLD") --一键强化金币不足提示购买
				else
					CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
					self.banTwice = false
				end
			end
		else
			error("OnEquipmentMsgHandle: " .. err)
			self.banTwice = false
		end
		--CommonFunction.ShowTip()
		self:InitSuitAttr()
		self:Refresh(resp.type)
		-- self:RefreshRedDot()
		self.isChoosePlayer = false
		self.roleBustItem:Refresh()
		if self.msgReceiveTrigger then
			self.msgReceiveTrigger()
			--self.msgReceiveTrigger = nil
		end
	end
end

function UISquad:SetModelActive(active)
	-- can't delete
end

function UISquad:RefreshRedDot( ... )
	self.equipTipList = {}

	--删选出品质最高的未装备的物品
	local equipmentList = {}
	local enum = MainPlayer.Instance.EquipmentGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if goods:IsEquip() == false then
			local cate = goods:GetSubCategory()
			if equipmentList[cate] then
				local curQuality = enumToInt(equipmentList[cate]:GetQuality())
				local nextQuality = enumToInt(goods:GetQuality())
				if curQuality < nextQuality then
					equipmentList[cate] = goods
				end
			else
				equipmentList[cate] = goods
			end
		end
	end

	--当前的装备信息
	local equipInfo = MainPlayer.Instance.EquipInfo
	local enum = equipInfo:GetEnumerator()
	while enum:MoveNext() do
		local info = {}
		local equip = enum.Current
		local pos = equip.pos
		local enumSlot = equip.slot_info:GetEnumerator()
		while enumSlot:MoveNext() do
			local id = EquipmentType.IntToEnum(enumToInt(enumSlot.Current.id))
			if equipmentList[id] then
				local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_EQUIPMENT,enumSlot.Current.equipment_uuid)
				if goods then
					local goodsQuality = enumToInt(goods:GetQuality())
					local equipQuality = enumToInt(equipmentList[id]:GetQuality())
					if goodsQuality < equipQuality then
						table.insert(info, enumToInt(id))
					elseif goodsQuality == equipQuality then
						local goodsLevel = goods:GetLevel()
						local equipLevel = equipmentList[id]:GetLevel()
						if goodsLevel < equipLevel then
							table.insert(info, enumToInt(id))
						end
					end
				else
					table.insert(info, enumToInt(id))
				end
			end
		end
		self.equipTipList[pos] = info
	end

	-- self:RefreshPlayerListRedDot()
	self.isChoosePlayer = false
end

function UISquad:RefreshPlayerListRedDot( ... )
	for i = 1, 3 do
		local lua = getLuaComponent(self.uiPlayer[i]:GetChild(0).gameObject)
		local roleState = (UpdateRedDotHandler.roleSkillList[self.idList[i]] and next(UpdateRedDotHandler.roleSkillList[self.idList[i]]) ~= nil)
							or UpdateRedDotHandler.roleLevelUpList[self.idList[i]]
							or UpdateRedDotHandler.roleEnhanceList[self.idList[i]]
							or UpdateRedDotHandler.roleImproveList[self.idList[i]] ~= nil
		local state = #UpdateRedDotHandler.equipTipList[FightStatus.IntToEnum(i)] > 0 or roleState
		lua:SetState(state)
		print('roleState = ', roleState)
		if self.selectedRoleID == self.idList[i] then
			NGUITools.SetActive(self.uiBtnUpRedDot.gameObject, roleState)
		end
	end
end

function UISquad:RefreshSlotRedDot()
	for i=1,5 do
		local go = self.uiSlot[i].gameObject
		local redDot = go.transform:FindChild("RedDot") --GetChild(4)
		if redDot.gameObject.activeSelf then
			NGUITools.SetActive(redDot.gameObject, false)
		end
	end
	for i,v in ipairs(UpdateRedDotHandler.equipTipList[self.selectStatus]) do
		local go = self.uiSlot[v].gameObject
		local redDot = go.transform:FindChild("RedDot") --GetChild(4)
		if not redDot.gameObject.activeSelf then
			NGUITools.SetActive(redDot.gameObject, true)
		end
	end
end

function UISquad:ShowAttrUpdate(namenum, isAdd)
	local attr = createUI('AttrUpdate', self.uiRoleBustItem.transform)
	local name = attr.transform:FindChild('Sprite/Label_Attr'):GetComponent('UILabel')
	local value = attr.transform:FindChild('Sprite/Label_Value'):GetComponent('UILabel')
	local split = string.split(namenum, ',')


	if tostring(split[1]) == 'block' then
		name.text = getCommonStr('ACTION_TYPE_4')
	elseif tostring(split[1]) == 'anti_block' then
		name.text = getCommonStr('STR_ANTI_BLOCK')
	elseif tostring(split[1]) == 'steal' then
		name.text = getCommonStr('ACTION_TYPE_8')
	elseif tostring(split[1]) == 'control' then
		name.text = getCommonStr('STR_CONTROL')
	elseif tostring(split[1]) == 'disturb' then
		name.text = getCommonStr('STR_DISTURB')
	elseif tostring(split[1]) == 'anti_disturb' then
		name.text = getCommonStr('STR_ANTI_DISTURB')
	elseif tostring(split[1]) == 'rebound' then
		name.text = getCommonStr('ACTION_TYPE_5')
	elseif tostring(split[1]) == 'strength' then
		name.text = getCommonStr('STR_CONFRONT')
	elseif tostring(split[1]) == 'pass' then
		name.text = getCommonStr('ACTION_TYPE_7')
	elseif tostring(split[1]) == 'interception' then
		name.text = getCommonStr('ACTION_TYPE_24')
	end

	if isAdd == 1 then
		value.text = '+' .. tostring(split[2])
		name.color = Color.New(58/255, 238/255, 89/255, 1)
	elseif isAdd == -1 then
		value.text = '-' .. tostring(split[2])
		name.color = Color.New(255/255, 0, 0, 1)
	end

	local attrList = {}
	for k,v in pairs(self.showAttr) do
		if k ~= namenum then
			attrList[k] = v
		end
	end
	self.showAttr = attrList
	return attr
end

function UISquad:GetRoleBoundary()
	if self.idList then
		return self.idList[1], self.idList[#self.idList]
	end
end
-----------------------------------------------------------------

function UISquad:ShowBuyTip(type)
	local str
	if type == "BUY_GOLD" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
	elseif type == "BUY_DIAMOND" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
	elseif type == "BUY_HP" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
	end
	self.msg = CommonFunction.ShowPopupMsg(str, nil,
													LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
													LuaHelper.VoidDelegate(self:FramClickClose()),
													getCommonStr("BUTTON_CONFIRM"),
													getCommonStr("BUTTON_CANCEL"))
end

function UISquad:ShowBuyUI(type)
	return function()
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
		self.banTwice = false
	end
end

function UISquad:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.banTwice = false
	end
end


return UISquad

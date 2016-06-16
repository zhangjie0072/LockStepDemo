--encoding=utf-8

EquipmentChange = {
	uiName = "EquipmentChange",

	-------------PARAMETERS
	goods,
	parent,

	isClickedEnhance = false,
	isEnhance = false,
	operationSucess = false,
	equipGoodsList = {},
	isChange,
	needResetPosition = false,

	-----------------UI
	uiLeftTransform,
	uiRightTransform,
	--left UI
	uiLeftIcon,

	uiEquipmentName,
	uiEquipmentLevel,
	uiRoleName,
	uiScroll,
	uiAttrGridCur,
	uiAttrGridChange,

	uiSuitParts,

	uiBtnChange,
	uiBtnUnequip,
	uiBtnEnhance,
	uiBtnEnhanceText,

	uiBtnClose,

	--right UI
	uiRightIcon,

	-- uiLevel,
	uiAttrGrid,
	uiConsume,

	-- uiBtnBack,
	uiBtnEnhanceAuto,
	uiBtnEnhanceOnce,

	uiAnimator,
}


-----------------------------------------------------------------
function EquipmentChange:Awake()
	self.uiLeftTransform = self.transform:FindChild('Left')
	self.uiRightTransform = self.transform:FindChild('Right')
	--left
	self.uiBtnClose = self.uiRightTransform:FindChild('ButtonClose')

	self.uiLeftIcon = self.uiRightTransform:FindChild('GoodsIcon')

	self.uiEquipmentName = self.uiRightTransform:FindChild('Name1'):GetComponent('UILabel')
	self.uiRoleName = self.uiRightTransform:FindChild('Name2'):GetComponent('UILabel')
	self.uiEquipmentLevel = self.uiRightTransform:FindChild('Level'):GetComponent('UILabel')
	self.uiScroll = self.uiRightTransform:FindChild('Scroll'):GetComponent('UIScrollView')
	self.uiAttrGridCur = self.uiScroll.transform:FindChild('CurrentAttr/AttrGrid'):GetComponent('UIGrid')
	self.uiAttrGridChange = self.uiScroll.transform:FindChild('NextAttr/AttrGrid'):GetComponent('UIGrid')

	self.uiSuitParts = self.uiScroll.transform:FindChild('SuitParts')

	self.uiBtnChange = self.uiRightTransform:FindChild('ButtonChange')
	self.uiBtnUnequip = self.uiRightTransform:FindChild('ButtonUnequip')
	self.uiBtnEnhance = self.uiRightTransform:FindChild('ButtonEnhance')
	self.uiBtnEnhanceText = self.uiRightTransform:FindChild('ButtonEnhance/Text'):GetComponent("MultiLabel")

	--right
	self.uiRightIcon = self.uiLeftTransform:FindChild('GoodsIcon')
	self.uiAttrGrid = self.uiLeftTransform:FindChild('AttrParticular/AttrGrid'):GetComponent('UIGrid')
	self.uiConsume = self.uiLeftTransform:FindChild('AttrParticular/Consume/Num'):GetComponent('UILabel')

	-- self.uiBtnBack = self.uiLeftTransform:FindChild('ButtonBack')
	self.uiBtnEnhanceAuto = self.uiLeftTransform:FindChild('ButtonEnhanceAuto')
	self.uiBtnEnhanceOnce = self.uiLeftTransform:FindChild('ButtonEnhanceOnce')


	self.uiAnimator = self.transform:GetComponent('Animator')
end

function EquipmentChange:Start()
	local close = getLuaComponent(createUI("ButtonClose",self.uiBtnClose))
	close.onClick = self:OnCloseClick()

	addOnClick(self.uiBtnChange.gameObject, self:OnChangeClick())
	addOnClick(self.uiBtnUnequip.gameObject, self:OnUnequipClick())
	addOnClick(self.uiBtnEnhance.gameObject, self:OnEnhanceClick())
	addOnClick(self.uiBtnEnhanceAuto.gameObject, self:OnEnhanceAutoClick())
	addOnClick(self.uiBtnEnhanceOnce.gameObject, self:OnEnhanceOnceClick())

	self:Init()

	self.transform:GetComponent('UIPanel').depth = self.parent.uiPanel.depth + 3

	self.parent.msgReceiveTrigger = self:OnTrigger() --self:OnCloseClick()
	self.parent.errorOccur = self:OnCloseClick()

	NGUITools.SetActive(self.uiLeftTransform.gameObject, false)
end

function EquipmentChange:FixedUpdate()
	if self.needResetPosition then
		self.uiScroll:ResetPosition()
		self.needResetPosition = false
	end
end

function EquipmentChange:OnClose()
	-- if self.isClickedEnhance then
	--	local obj = createUI('EquipmentEnhance', self.parent.transform)
	--	local script = getLuaComponent(obj)
	--	script.goods = self.goods
	--	script.parent = self.parent
	--	self.isClickedEnhance = false
	-- end

	self.parent.msgReceiveTrigger = nil
	self.parent.errorOccur = nil
	self.parent.equipmentChange = nil
	NGUITools.Destroy(self.gameObject)
	if self.isChange then
		local script = getLuaComponent(createUI('UIEquipment', nil))
		script.parent = self.parent
		UIManager.Instance:BringPanelForward(script.gameObject)
	end
end

function EquipmentChange:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function EquipmentChange:Refresh()
	self:Init()
end


-----------------------------------------------------------------
function EquipmentChange:Init( ... )
	local child
	if self.uiLeftIcon.childCount > 0 then
		child = self.uiLeftIcon:GetChild(0)
	end
	if child == nil then
		child = createUI('GoodsIcon', self.uiLeftIcon)
	end
	local icon = getLuaComponent(child)
	icon.goods = self.goods
	icon.hideNeed = true
	icon.hideLevel = true
	icon.hideNum = true
	-- icon.needPlayAnimation = true
	-- icon.isDisplayLevel = false
	icon:Refresh()
	-- icon:goodsRefresh()
	-- icon:HideStar(true)
	if self.goods:IsEquip() then
		local pos = self:GetEquipPos()
		if pos then
			local roleID = self:GetEquipRoleID(pos)
			self.uiRoleName.text = MainPlayer.Instance:GetRole(roleID).m_name
		end
	end
	self.uiEquipmentLevel.text = 'Lv.' .. self.goods:GetLevel()
	self.uiEquipmentName.text = self.goods:GetName()

	local goodsID = self.goods:GetID()
	local goodsLevel = self.goods:GetLevel()

	CommonFunction.ClearGridChild(self.uiAttrGridCur.transform)
	CommonFunction.ClearGridChild(self.uiAttrGridChange.transform)
	local levelItem = createUI('AttrInfo2', self.uiAttrGridCur.transform)
	local script = getLuaComponent(levelItem)
	local name = CommonFunction.GetConstString('EQUIPMENT_LEVEL_QUOTA')
	script:SetName(name)
	script:SetValue(goodsLevel .. '/' .. MainPlayer.Instance.Level)

	local attrNameConfig = GameSystem.Instance.AttrNameConfigData
	local equipmentConfig = GameSystem.Instance.EquipmentConfigData
	local itemConfigCur = equipmentConfig:GetBaseConfig(goodsID, goodsLevel)
	local itemConfigNext = equipmentConfig:GetBaseConfig(goodsID, goodsLevel+1)
	local changeTbl = {}
	if itemConfigCur then
		local enum = itemConfigCur.addn_attr:GetEnumerator()
		while enum:MoveNext() do
			local attrID = enum.Current.Key
			local attrValue = enum.Current.Value

			local attrItemObj = createUI('AttrInfo2', self.uiAttrGridCur.transform)
			local script = getLuaComponent(attrItemObj)
			local symbol = attrNameConfig:GetAttrSymbol(attrID)
			local name = attrNameConfig:GetAttrName(symbol)
			script:SetName(name)
			script:SetValue('+' .. attrValue)
			if itemConfigNext then
				local nextValue = itemConfigNext:GetAddnAttr(attrID)
				local change = nextValue - attrValue
				table.insert(changeTbl, {key = name, value = change})
			end
		end
	else
		print('error -- can not get configuration by goodsID: ', goodsID, ' and level: ', goodsLevel)
	end
	self.uiAttrGridCur.repositionNow = true

	for k, v in ipairs(changeTbl) do
		local attrItemObj = createUI('AttrInfo2', self.uiAttrGridChange.transform)
		local script = getLuaComponent(attrItemObj)
		script:SetName(v.key)
		script:SetValue('+' .. v.value)
	end
	self.uiAttrGridChange.repositionNow = true

	--套装属性
	if self.goods:IsSuit() then
		local suitObj = createUI('SuitInfoItem', self.uiSuitParts.transform)
		local suitScript = getLuaComponent(suitObj)
		suitScript.goods = self.goods
		suitScript.equipGoodsList = self.equipGoodsList
		suitScript.refreshParentSV = self:ResetScrollViewPosition()
	end
end

function EquipmentChange:ResetScrollViewPosition()
	return function ( ... )
		self.needResetPosition = true
	end
end

function EquipmentChange:OnCloseClick( ... )
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function EquipmentChange:OnChangeClick( ... )
	return function (go)
		self:OnCloseClick()()
		self.isChange = true
	end
end

function EquipmentChange:OnUnequipClick( ... )
	return function (go)
		local slotIDStr = type(self.parent.selectSlotID) == 'string' and self.parent.selectSlotID or self.parent.selectSlotID:ToString()
		local equipOperation = {
			type = 'EOT_UNEQUIP',
			info = {
				pos = self.parent.selectStatus:ToString(),
				slot_info = {
					{
						id = slotIDStr,
						equipment_uuid = self.goods:GetUUID(),
					},
				},
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
	end
end

function EquipmentChange:OnEnhanceClick( ... )
	return function (go)
		-- self.isClickedEnhance = true

		-- self:OnCloseClick()()
		if not self.isClickedEnhance then
			if self.uiAnimator then
				self.uiAnimator:SetBool('Leftout', true)
				self.uiAnimator:SetBool('Switch', false)
			end
			NGUITools.SetActive(self.uiLeftTransform.gameObject, true)
			UIManager.Instance:BringPanelForward(self.uiLeftTransform.gameObject)
			self:InitEnhance()()
			self.isClickedEnhance = true
			self.uiBtnEnhanceText:SetText(getCommonStr("STR_BACK"))
		else
			if self.uiAnimator then
				self.uiAnimator:SetBool('Switch', true)
				self.uiAnimator:SetBool('Leftout', false)
			end
			NGUITools.SetActive(self.uiLeftTransform.gameObject, false)
			self.isClickedEnhance = false
			self.uiBtnEnhanceText:SetText(getCommonStr("STR_ENHANCE"))
		end
	end
end

function EquipmentChange:InitEnhance( ... )
	return function ( ... )
		local child
		if self.uiRightIcon.childCount > 0 then
			child = self.uiRightIcon:GetChild(0)
		end
		if child == nil then
			child = createUI('GoodsIcon', self.uiRightIcon)
		end

		local icon = getLuaComponent(child)
		icon.goods = self.goods
		icon.hideNeed = true
		icon.hideLevel = true
		icon.hideNum = true
		icon.needPlayAnimation = true
		icon:Refresh()
		-- icon:goodsRefresh()
		-- icon:HideStar(true)
		if self.isEnhance == true then
			if self.operationSucess then
				icon:StartHighSparkle()
			end
			self.isEnhance = false
		end

		local goodsID = self.goods:GetID()
		local goodsLevel = self.goods:GetLevel()

		CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
		local levelItem = createUI('EquipmentAttrItem', self.uiAttrGrid.transform)
		local script = getLuaComponent(levelItem)
		script:SetName(CommonFunction.GetConstString('LEVEL_GROW'))
		script:SetValueCur(goodsLevel)
		script:SetValueChange(goodsLevel + 1)

		local attrNameConfig = GameSystem.Instance.AttrNameConfigData
		local equipmentConfig = GameSystem.Instance.EquipmentConfigData
		local itemConfigCur = equipmentConfig:GetBaseConfig(goodsID, goodsLevel)
		local itemConfigNext = equipmentConfig:GetBaseConfig(goodsID, goodsLevel+1)
		if itemConfigCur and itemConfigNext then
			local enum = itemConfigCur.addn_attr:GetEnumerator()
			while enum:MoveNext() do
				local attrItemObj = createUI('EquipmentAttrItem', self.uiAttrGrid.transform)
				local script = getLuaComponent(attrItemObj)
				local symbol = attrNameConfig:GetAttrSymbol(enum.Current.Key)
				local name = attrNameConfig:GetAttrName(symbol)
				script:SetName(name)
				script:SetValueCur(enum.Current.Value)
				local nextValue = itemConfigNext:GetAddnAttr(enum.Current.Key)
				script:SetValueChange(nextValue)
			end
		else
			print('error -- can not get configuration by goodsID: ', goodsID, ' and level: ', goodsLevel)
		end
		self.uiAttrGrid.repositionNow = true

		self.uiConsume.text = itemConfigNext.sacrifice_consume
	end
end

function EquipmentChange:OnEnhanceAutoClick( ... )
	return function (go)
		--判断消耗
		if MainPlayer.Instance.Gold < tonumber(self.uiConsume.text) then
			if self.banTwice == true then
				return
			end
			self.banTwice = true
			self:ShowBuyTip("BUY_GOLD")
			return
		end

		local slotIDStr = type(self.parent.selectSlotID) == 'string' and self.parent.selectSlotID or self.parent.selectSlotID:ToString()
		local equipOperation = {
			type = 'EOT_UPGRADE_AUTO',
			info = {
				pos = self.parent.selectStatus:ToString(),
				slot_info = {
					{
						id = slotIDStr,
						equipment_uuid = self.goods:GetUUID(),
					},
				},
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
		self.isEnhance = true
	end
end

function EquipmentChange:OnEnhanceOnceClick( ... )
	return function (go)
		--判断消耗
		if MainPlayer.Instance.Gold < tonumber(self.uiConsume.text) then
			if self.banTwice == true then
				return
			end
			self.banTwice = true
			self:ShowBuyTip("BUY_GOLD")
			return
		end

		local slotIDStr = type(self.parent.selectSlotID) == 'string' and self.parent.selectSlotID or self.parent.selectSlotID:ToString()
		local equipOperation = {
			type = 'EOT_UPGRADE_SINGLE',
			info = {
				pos = self.parent.selectStatus:ToString(),
				slot_info = {
					{
						id = slotIDStr,
						equipment_uuid = self.goods:GetUUID(),
					},
				},
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
		self.isEnhance = true
	end
end

function EquipmentChange:OnTrigger( ... )
	return function ( ... )
		if not self.isEnhance then
			self:OnCloseClick()()
		else
			self:Init()
			self:InitEnhance()()
		end
	end
end

function EquipmentChange:GetEquipPos( ... )
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

function EquipmentChange:GetEquipRoleID(pos)
	local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.status == pos then
			return enum.Current.role_id
		end
	end
	return nil
end
-----------------------------------------------------------------

function EquipmentChange:ShowBuyTip(type)
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

function EquipmentChange:ShowBuyUI(type)
	return function()
	    self.banTwice = false
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
	end
end

function EquipmentChange:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.banTwice = false
	end
end

return EquipmentChange

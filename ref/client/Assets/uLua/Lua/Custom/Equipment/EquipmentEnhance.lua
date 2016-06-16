--encoding=utf-8

EquipmentEnhance = {
	uiName = "EquipmentEnhance",

	-------------PARAMETERS
	goods,
	parent,
	isEnhance = false,

	-----------------UI
	uiIcon,
	uiLevel,
	uiAttrGrid,
	uiConsume,

	uiBtnBack,
	uiBtnEnhanceAuto,
	uiBtnEnhanceOnce,

	uiAnimator,
}


-----------------------------------------------------------------
function EquipmentEnhance:Awake()
	local transform = self.transform:FindChild('Window')

	self.uiIcon = transform:FindChild('GoodsIcon')
	self.uiLevel = transform:FindChild('AttrParticular/Level/Num'):GetComponent('UILabel')
	self.uiAttrGrid = transform:FindChild('AttrParticular/AttrGrid'):GetComponent('UIGrid')
	self.uiConsume = transform:FindChild('AttrParticular/Consume/Num'):GetComponent('UILabel')

	self.uiBtnBack = transform:FindChild('ButtonBack')
	self.uiBtnEnhanceAuto = transform:FindChild('ButtonEnhanceAuto')
	self.uiBtnEnhanceOnce = transform:FindChild('ButtonEnhanceOnce')

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function EquipmentEnhance:Start()
	addOnClick(self.uiBtnBack.gameObject, self:OnBackClick())
	addOnClick(self.uiBtnEnhanceAuto.gameObject, self:OnEnhanceAutoClick())
	addOnClick(self.uiBtnEnhanceOnce.gameObject, self:OnEnhanceOnceClick())

	self:Init()()

	self.transform:GetComponent('UIPanel').depth = self.parent.uiPanel.depth + 3

	self.parent.msgReceiveTrigger = self:Init()
	self.parent.errorOccur = self:OnBackClick()
end

function EquipmentEnhance:FixedUpdate()
	-- body
end

function EquipmentEnhance:OnClose()
	self.parent.msgReceiveTrigger = nil
	self.parent.errorOccur = nil
	NGUITools.Destroy(self.gameObject)
end

function EquipmentEnhance:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function EquipmentEnhance:Refresh()
	self:Init()()
end


-----------------------------------------------------------------
function EquipmentEnhance:Init( ... )
	return function ( ... )
		local child
		if self.uiIcon.childCount > 0 then
			child = self.uiIcon:GetChild(0)
		end
		if child == nil then
			child = createUI('GoodsIcon2', self.uiIcon)
		end

		local icon = getLuaComponent(child)
		icon.goods = self.goods
		icon.needPlayAnimation = true
		icon.isDisplayLevel = false
		icon:Refresh()
		icon:goodsRefresh()
		icon:HideStar(true)
		if self.isEnhance == true then
			icon:StartSparkle()
			self.isEnhance = false
		end

		local goodsID = self.goods:GetID()
		local goodsLevel = self.goods:GetLevel()
		self.uiLevel.text = goodsLevel .. '/' .. MainPlayer.Instance.Level

		CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
		local levelItem = createUI('EquipmentAttrItem', self.uiAttrGrid.transform)
		local script = getLuaComponent(levelItem)
		script:SetName(CommonFunction.GetConstString('PLAYER_LEVEL'))
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

function EquipmentEnhance:OnBackClick( ... )
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
		self.parent.msgReceiveTrigger = nil
	end
end

function EquipmentEnhance:OnEnhanceAutoClick( ... )
	return function (go)
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

function EquipmentEnhance:OnEnhanceOnceClick( ... )
	return function (go)
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

-----------------------------------------------------------------

return EquipmentEnhance

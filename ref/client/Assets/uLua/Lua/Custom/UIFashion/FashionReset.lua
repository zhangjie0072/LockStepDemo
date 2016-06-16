--encoding=utf-8

FashionReset =  {
	uiName = "FashionReset",
	----------------------

	uiGoodsIcon,
	uiGoodsName,
	uiOwnerName,
	uiAttrGrid,
	uiBtnClose,
	uiBtnReset,
	uiBtnResetNum,
	uiBtnResetIcon,
	uiBtnOperate,
	uiBtnOperateLabel,
	uiAnimator,

	---------------------

	attrConfig,
	onReset,
	goods,
	onOperate,
	isOperate = false,
	isRoll = false,
	isReset = false,
	fashionConfig,
	rollTime = 1.0,
	attrIndex = 0,
}

-----------------------------------------------------------------
function FashionReset:Awake()
	self.uiGoodsIcon = self.transform:FindChild('Window/Fashion/GoodsIcon')
	self.uiGoodsName = self.transform:FindChild('Window/Fashion/GoodsName'):GetComponent('UILabel')
	self.uiOwnerName = self.transform:FindChild('Window/Fashion/UserName'):GetComponent('UILabel')
	self.uiAttrGrid = self.transform:FindChild('Window/Fashion/AttrInfoGrid'):GetComponent('UIGrid')
	self.uiBtnClose = self.transform:FindChild('Window/Bg/ButtonClose')
	self.uiBtnReset = self.transform:FindChild('Window/Reset'):GetComponent('UIButton')
	self.uiBtnResetNum = self.transform:FindChild('Window/Reset/Num'):GetComponent('UILabel')
	self.uiBtnResetIcon = self.transform:FindChild('Window/Reset/Icon'):GetComponent('UISprite')
	self.uiBtnOperate = self.transform:FindChild('Window/Wear'):GetComponent('UIButton')
	self.uiBtnOperateLabel = self.transform:FindChild('Window/Wear/Text'):GetComponent('MultiLabel')
	self.uiAnimator = self.transform:GetComponent('Animator')

	self.attrConfig = GameSystem.Instance.AttrNameConfigData
end

function FashionReset:Start()
	addOnClick(createUI('ButtonClose', self.uiBtnClose.transform), self:CloseClick())
	addOnClick(self.uiBtnReset.gameObject, self:OnResetClick())
	addOnClick(self.uiBtnOperate.gameObject, self:OnOperateClick())
	LuaHelper.RegisterPlatMsgHandler(MsgID.FashionResetAttrRespID, self:FashionResetRespHandler(), self.uiName)

	local fashionAttr = self.fashionConfig:GetRandomFashionAttr()
end

function FashionReset:FixedUpdate()
	if self.isRoll and self.rollTime >= 0 then
		self.rollTime = self.rollTime - UnityTime.fixedDeltaTime
		local fashionAttr = self.fashionConfig:GetRandomFashionAttr()
		local parent = self.uiAttrGrid.transform
		if self.attrIndex > parent.transform.childCount - 1 then
			self.attrIndex = 0
		end
		local attr = parent:GetChild(self.attrIndex)
		if fashionAttr and attr then
			local name = self.attrConfig:GetAttrNameById(fashionAttr.player_attr_id)
			local num = fashionAttr.player_attr_num
			local attrInfo = getLuaComponent(attr.gameObject)
			attrInfo:SetName(name)
			attrInfo:SetValue(num)
			self.attrIndex = self.attrIndex + 1
		end
		if self.attrIndex >= 2 then
			self.attrIndex = 0
		end
	elseif self.isRoll and self.rollTime < 0 then
		self.goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, self.goods:GetUUID())
		self:SetFashionAttr()
		self.uiBtnReset.isEnabled = true
		self.uiBtnReset.transform:GetComponent("BoxCollider").enabled = true
		self.isRoll = false
	end
end

function FashionReset:OnClose()
	if self.onReset and self.isReset then
		self.onReset()
		self.isReset = false
	end
	if self.onOperate and self.isOperate then
		self.onOperate()
		self.isOperate = false
	end
	NGUITools.Destroy(self.gameObject)
end

function FashionReset:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.FashionResetAttrRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function FashionReset:Refresh()
end


-----------------------------------------------------------------
function FashionReset:CloseClick( ... )
	return function (go)
		self:DoClose()
	end
end

function FashionReset:DoClose( ... )
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function FashionReset:OnResetClick( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.clothes_reset) then return end
		if go == self.uiBtnReset.gameObject then
			local uuid = self.goods:GetUUID()
			local req = {
					uuid = uuid,
				}
			local buf = protobuf.encode('fogs.proto.msg.FashionResetAttrReq', req)
			LuaHelper.SendPlatMsgFromLua(MsgID.FashionResetAttrReqID, buf)
			CommonFunction.ShowWait()
		end
	end
end

function FashionReset:OnOperateClick( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.clothes_wear) then return end
		self.isOperate = true
		self:DoClose()
	end
end

function FashionReset:SetData(goods, belongID, playerID)
	self.goods = goods

	self.fashionConfig = GameSystem.Instance.FashionConfig
	local goodsicon = getLuaComponent(createUI('GoodsIcon', self.uiGoodsIcon.transform))
	goodsicon.goods = goods
	goodsicon.hideNeed = true
	goodsicon.hideNum = true
	goodsicon.hideLevel = true
	goodsicon.hideGender = false

	self.uiGoodsName.text = self.goods:GetName()
	if belongID and belongID ~= 0 then
		local owner = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(belongID)
		if owner then
			self.uiOwnerName.text = owner.name
		end
		if belongID == playerID then
			self.uiBtnOperateLabel:SetText(getCommonStr('UNEQUIP'))
		end
	end

	self:SetFashionAttr()

	local fashionID = self.goods:GetID()
	local fashionData = self.fashionConfig:GetFashionData(fashionID)
	if fashionData then
		if fashionData.reset_id == GlobalConst.DIAMOND_ID then
			self.uiBtnResetIcon.spriteName = 'com_property_diamond'
		elseif fashionData.reset_id == GlobalConst.GOLD_ID then
			self.uiBtnResetIcon.spriteName = 'com_property_gold'
		elseif fashionData.reset_id == GlobalConst.HP_ID then
			self.uiBtnResetIcon.spriteName = 'com_property_hp'
		elseif fashionData.reset_id == GlobalConst.HONOR_ID then
			self.uiBtnResetIcon.spriteName = 'com_property_honor'
		elseif fashionData.reset_id == GlobalConst.PRESTIGE_ID then
			self.uiBtnResetIcon.spriteName = 'com_property_prestige'
		elseif fashionData.reset_id == GlobalConst.HONOR2_ID then
			self.uiBtnResetIcon.spriteName = "com_property_honor1"
		elseif fashionData.reset_id == GlobalConst.PRESTIGE2_ID then
			self.uiBtnResetIcon.spriteName = "com_property_prestige1"
		elseif fashionData.reset_id == GlobalConst.REPUTATION_ID then
			self.uiBtnResetIcon.spriteName = "com_property_reputation"
		elseif fashionData.reset_id == GlobalConst.TEAM_EXP_ID then
			self.uiBtnResetIcon.spriteName = "com_property_exp"
		elseif fashionData.reset_id == GlobalConst.ROLE_EXP_ID then
			self.uiBtnResetIcon.spriteName = "com_property_exp01"
		else
			self.uiBtnResetIcon.spriteName = self.goods:GetIcon()
		end
		self.uiBtnResetNum.text = fashionData.reset_num
	else
		error('FashionReset.lua: Fashion ResetConfig Error!')
	end
end

function FashionReset:SetFashionAttr()
	CommonFunction.ClearGridChild(self.uiAttrGrid.transform)
	local attrIDList = self.goods:GetFashionAttrIDList()
	local enum = attrIDList:GetEnumerator()
	while enum:MoveNext() do
		local attr_id = enum.Current
		print('attr_id = ', attr_id)
		if attr_id ~= 0 then
			local fashionAttr = self.fashionConfig:GetFashionAttr(attr_id)
			local name = self.attrConfig:GetAttrNameById(fashionAttr.player_attr_id)
			local num = fashionAttr.player_attr_num
			local attrInfo =  getLuaComponent(createUI("AttrInfo1", self.uiAttrGrid.transform))
			attrInfo:SetName(name)
			attrInfo:SetValue(num)
		end
	end

	self.uiAttrGrid.repositionNow = true
	self.uiAttrGrid:Reposition()
end

function FashionReset:FashionResetRespHandler( ... )
	return function (message)
		local resp = protobuf.decode('fogs.proto.msg.FashionResetAttrResp', message)
		CommonFunction.StopWait()
		print('resp.result = ', resp.result)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
			return
		end

		self.uiBtnReset.isEnabled = false
		self.uiBtnReset.transform:GetComponent("BoxCollider").enabled = false
		self.isRoll = true
		self.isReset = true
		self.rollTime = 1.0
	end
end

return FashionReset

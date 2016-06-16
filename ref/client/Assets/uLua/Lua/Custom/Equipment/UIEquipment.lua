--encoding=utf-8

UIEquipment = {
	uiName = "UIEquipment",

	-------------PARAMETERS
	detailScript,
	parent,

	selectStatus,
	selectSlotID,

	selectItemObj,

	equipGoodsList = {},

	-----------------UI
	uiBtnClose,

	uiDetail,
	uiBtnEquip,

	uiScrollView,
	uiSVIncLoad,
	uiGrid,

	uiAnimator,
}


-----------------------------------------------------------------
function UIEquipment:Awake()
	local transform = self.transform

	self.uiBtnClose = transform:FindChild("ButtonClose")

	self.uiDetail = transform:FindChild("EquipmentDetailShow/DetailShow/EquipmentDetail")
	self.uiBtnEquip = transform:FindChild("EquipmentDetailShow/DetailShow/ButtonEquip")

	self.uiScrollView = transform:FindChild("SelectZone/SelectScrollView"):GetComponent("UIScrollView")
	self.uiSVIncLoad = self.uiScrollView:GetComponent("ScrollViewIncLoad")
	self.uiGrid = transform:FindChild("SelectZone/SelectScrollView/SelectGrid"):GetComponent("UIGrid")

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIEquipment:Start()
	local close = getLuaComponent(createUI("ButtonClose",self.uiBtnClose))
	close.onClick = self:OnCloseClick()

	addOnClick(self.uiBtnEquip.gameObject, self:OnEquipClick())

	self:Refresh()

	-- self.transform:GetComponent('UIPanel').depth = self.parent.uiPanel.depth + 3

	self.parent.msgReceiveTrigger = self:OnCloseClick()
end

function UIEquipment:FixedUpdate()
	-- body
end

function UIEquipment:OnClose()
	self.parent.msgReceiveTrigger = nil
	self.parent.errorOccur = nil
	NGUITools.Destroy(self.gameObject)
end

function UIEquipment:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIEquipment:Refresh()
	self.selectItemObj = nil
	self:InitEquipmentList()
end


-----------------------------------------------------------------
function UIEquipment:InitEquipmentList( ... )
	--sort
	MainPlayer.Instance:SortGoodsDesc(MainPlayer.Instance.EquipmentGoodsList)

	local goodsList = {}
	local enum = MainPlayer.Instance.EquipmentGoodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if enumToInt(goods:GetSubCategory()) == enumToInt(self.parent.selectSlotID) then
			table.insert(goodsList, goods)
		end
	end

	local isSelect = false
	self.uiSVIncLoad.onAcquireItem = function (index, parent)
		index = index + 1
		if index < 1 or index > table.getn(goodsList) then return nil end
		local goods = goodsList[index]
		local item = getLuaComponent(createUI('GoodsIcon2', parent))
		item.gameObject.name = string.format("GoodsIcon2_%05d", index)
		item.goods = goods
		item.isDisplayLevel = false
		item.onClick = self:OnItemClick()
		item:Refresh()
		item:goodsRefresh()
		item:HideStar(true)
		if isSelect == false then
			self:OnItemClick()(item.gameObject)
			isSelect = true
		end
		return item.gameObject
	end
	self.uiSVIncLoad.onDestroyItem = function (index, object)
		if self.selectItemObj and object == self.selectItemObj.gameObject then
			self.selectItemObj = nil
		end
	end
	self.uiSVIncLoad:Refresh()
end

function UIEquipment:InitEquipmentDetail(go)
	local goodsItem = getLuaComponent(go.transform)
	local child
	if self.uiDetail.childCount > 0 then
		child = self.uiDetail:GetChild(0)
	end
	if child == nil then
		child = createUI('EquipmentDetail', self.uiDetail)
	end
	self.detailScript = getLuaComponent(child)
	self.detailScript.goods = goodsItem.goods
	self.detailScript.equipGoodsList = self.equipGoodsList
	self.detailScript:Refresh()
end

function UIEquipment:OnCloseClick( ... )
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIEquipment:OnEquipClick( ... )
	return function (go)
		local operType
		if self.detailScript == nil then
			print("error : equipment detail is not init")
			return
		end
		if self.detailScript.goods:IsEquip() and self.equipGoodsList[self.parent.selectSlotID] ~= nil then
			operType = 'EOT_EXCHANGE'
		else
			operType = 'EOT_EQUIP'
		end
		local slotIDStr = type(self.parent.selectSlotID) == 'string' and self.parent.selectSlotID or self.parent.selectSlotID:ToString()
		local equipOperation = {
			type = operType,
			info = {
				pos = self.parent.selectStatus:ToString(),
				slot_info = {
					{
						id = slotIDStr,
						equipment_uuid = self.detailScript.goods:GetUUID(),
					},
				},
			},
		}
		local msg = protobuf.encode("fogs.proto.msg.EquipOperation", equipOperation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EquipOperationID, msg)
	end
end

function UIEquipment:OnItemClick()
	return function (go)
		if self.selectItemObj == go then
			return
		end
		if self.selectItemObj then
			local goods = getLuaComponent(self.selectItemObj.gameObject)
			goods:SetSele(false)
		end
		self.selectItemObj = go
		local goods = getLuaComponent(self.selectItemObj.gameObject)
		goods:SetSele(true)

		self:InitEquipmentDetail(go)
	end
end

-----------------------------------------------------------------

return UIEquipment

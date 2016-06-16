--encoding=utf-8

EquipmentAttrItem = {
	uiName = "EquipmentAttrItem",

	-------------PARAMETERS
	goods,

	-----------------UI
	uiName,
	uiValueCur,
	uiValueChange,

	uiDragSV,
}


-----------------------------------------------------------------
function EquipmentAttrItem:Awake()
	local transform = self.transform

	self.uiName = transform:FindChild('Name'):GetComponent('UILabel')
	self.uiValueCur = transform:FindChild('ValueCur'):GetComponent('UILabel')
	self.uiValueChange = transform:FindChild('ValueChange'):GetComponent('UILabel')
	self.uiDragSV = transform:GetComponent('UIDragScrollView')
end

function EquipmentAttrItem:Start()
	-- body
end

function EquipmentAttrItem:FixedUpdate()
	-- body
end

function EquipmentAttrItem:OnClose()
	-- body
end

function EquipmentAttrItem:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function EquipmentAttrItem:Refresh()
	-- body
end


-----------------------------------------------------------------
function EquipmentAttrItem:SetName(nameStr)
	self.uiName.text = nameStr
end

function EquipmentAttrItem:SetValueCur(value)
	self.uiValueCur.text = value
end

function EquipmentAttrItem:SetValueChange(value)
	self.uiValueChange.text = value
end

function EquipmentAttrItem:SetDragSV(scrollView)
	self.uiDragSV.scrollView = scrollView
end

-----------------------------------------------------------------

return EquipmentAttrItem
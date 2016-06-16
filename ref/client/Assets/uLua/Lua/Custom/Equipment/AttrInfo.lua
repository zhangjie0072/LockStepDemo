--encoding=utf-8

AttrInfo = {
	uiName = "AttrInfo",

	-------------PARAMETERS

	-----------------UI
	uiName,
	uiValue,

	uiDragSV,


}


-----------------------------------------------------------------
function AttrInfo:Awake()
	local transform = self.transform

	self.uiName = transform:FindChild('Name'):GetComponent('UILabel')
	self.uiValue = transform:FindChild('Value'):GetComponent('UILabel')
	self.uiDragSV = transform:GetComponent('UIDragScrollView')
end

function AttrInfo:Start()
	-- body
end

function AttrInfo:FixedUpdate()
	-- body
end

function AttrInfo:OnClose()
	-- body
end

function AttrInfo:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function AttrInfo:Refresh()
	-- body
end


-----------------------------------------------------------------
function AttrInfo:SetName(name)
	self.uiName.text = name
end

function AttrInfo:SetValue(value, colored)
	self.uiValue.text = value
	-- self.uiValue.color = colored and Color.New(0.1, 0.93, 0.35, 1.0)  or Color.New(1.0, 1.0, 1.0, 1.0)
end

function AttrInfo:SetDragSV(scrollView)
	self.uiDragSV.scrollView = scrollView
end

-----------------------------------------------------------------

return AttrInfo

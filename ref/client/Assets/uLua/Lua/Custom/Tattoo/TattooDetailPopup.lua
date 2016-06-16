TattooDetailPopup = {
	uiName = 'TattooDetailPopup',
	
	tattooID = 0,
	level = 0,
}

function TattooDetailPopup:Awake()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiIconNode = self.transform:FindChild("IconNode")
	self.uiGrid = getComponentInChild(self.transform, "Attrs/Grid", "UIGrid")
end

function TattooDetailPopup:Start()
	bringFrontZOrder(self.transform)

	self.uiFrame.showCorner = false
	self.uiFrame.title = getCommonStr("TATTOO_DETAIL")
	self.uiFrame.onClose = self:MakeOnClose()
	local goIcon = createUI("TattooIconDetail", self.uiIconNode)
	self.uiIcon = getLuaComponent(goIcon)
	self.uiIcon.tattooID = self.tattooID
	self.uiIcon.level = self.level

	local tattooAttr = GameSystem.Instance.TattooConfig:GetTattooConfig(self.tattooID, self.level)
	if not tattooAttr then error("TattooDetailPopup: error tattoo ID: " .. self.tattooID .. " Level: " .. self.level) end

	local enum = tattooAttr.addn_attr:GetEnumerator()
	while enum:MoveNext() do
		local goItem = createUI("AttrListItem", self.uiGrid.transform)
		local item = getLuaComponent(goItem)
		item.attrID = enum.Current.Key
		item.value = enum.Current.Value
		item.showPlus = true
	end
end

function TattooDetailPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return TattooDetailPopup

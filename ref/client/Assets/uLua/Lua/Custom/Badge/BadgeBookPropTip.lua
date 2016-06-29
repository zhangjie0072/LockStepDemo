-- 涂鸦墙属性TIPS
BadgeBookPropTip = {
	uiName = "BadgeBookPropTip",
	--- params-----
	book,
	---ui----
	uiAttrInfoGrid,
}
function BadgeBookPropTip:Awake( ... )
	self.uiAttrInfoGrid = self.transform:FindChild("AddAttr/AttrInfo3Grid"):GetComponent("UIGrid")
end

function BadgeBookPropTip:Start( ... )

end

function BadgeBookPropTip:FixedUpdate( ... )
	-- body
end

function BadgeBookPropTip:SetData(data)
	self.book = data
	local attr = {}
	local slotList = self.book.slot_list
	local count = slotList.Count
	for i=1,count do
		local badgeId = slotList:get_Item(i-1).badge_id
		if badgeId and badgeId~=0 then
			local badgeAttrConfigData = GameSystem.Instance.BadgeAttrConfigData:GetBaseConfig(badgeId)
			if badgeAttrConfigData then
				local addAttrlist = badgeAttrConfigData.addAttr
				local enum = addAttrlist:GetEnumerator()
				while enum:MoveNext() do
					local attrId = enum.Current.Key
					local attrNum = enum.Current.Value
					if not attr[attrId] then
						attr[attrId] = attrNum
					else
						attr[attrId] = attr[attrId]+attrNum
					end
				end
			end
		end
	end
	for k,v in pairs(attr) do
		local attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(k)
		local item = createUI("AttrInfo",self.uiAttrInfoGrid.transform)
		item:GetComponent("BoxCollider").enabled = false
		local attrItem = getLuaComponent(item)
		attrItem:SetName(attrName)
		attrItem:SetValue("+"..v,true)
	end
end

function BadgeBookPropTip:Get( ... )
	-- body
end

function BadgeBookPropTip:OnDestroy( ... )
	-- body
end

return BadgeBookPropTip
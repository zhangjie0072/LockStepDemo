BadgeAttrList = {
	uiName = "BadgeAttrList",
	badgeId,
	------UI-------
	uiBadgeGrid,
}

function BadgeAttrList:Awake( ... )
	self.uiBadgeGrid = self.transform:GetComponent("UIGrid")
	if not self.uiBadgeGrid then
		print("it's not work without 'UIGrid' Component")
	end
end

function BadgeAttrList:Start( ... )
	-- body
end
--------------Update-----------------
function BadgeAttrList:Update( ... )
	-- body
end

--------------FixedUpdate----------------
function BadgeAttrList:FixedUpdate( ... )
	-- body
end

--------------OnDestory-----------------
function BadgeAttrList:OnDestroy( ... )
	-- body
end

function BadgeAttrList:SetBadgeId(id)
	CommonFunction.ClearGridChild(self.transform)
	self.uiBadgeGrid:Reposition()
	self.badgeId = id
	if self.badgeId then
		local badgeAttrConfigData = GameSystem.Instance.BadgeAttrConfigData:GetBaseConfig(self.badgeId)
		if badgeAttrConfigData then
			local addAttrlist = badgeAttrConfigData.addAttr
			local enum = addAttrlist:GetEnumerator()
			while enum:MoveNext() do
				local attrId = enum.Current.Key
				local attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(attrId)
				local attrNum = enum.Current.Value
				local item = createUI("AttrInfo",self.uiBadgeGrid.transform)
				item:GetComponent("BoxCollider").enabled = false
				local attrItem = getLuaComponent(item)
				attrItem:SetName(attrName)
				attrItem:SetValue("+"..attrNum,true)
			end
		end
	end
	self.uiBadgeGrid.repositionNow = true
end

return BadgeAttrList


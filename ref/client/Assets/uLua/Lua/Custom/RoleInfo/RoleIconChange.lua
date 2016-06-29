--个人信息-头像unit
local RoleIconChange = {
	uiName = "RoleIconChange",
	
	--UI
	uiIcon = nil,
	uiLocked = nil,
	uiToggle = nil,


	--数据

}
function RoleIconChange:Awake( ... )
	-- body
end
function RoleIconChange:Start( ... )
	-- body

    addOnClick(self.uiIcon.gameObject, self:OnClick())
end
function RoleIconChange:SetData( info ,defaultChoosed)
	-- body
	local id = info.id 
	local locked = info.locked 

	NGUITools.SetActive(self.uiLocked.gameObject,locked)
	self.uiToggle.value = defaultChoosed or false

end
function RoleIconChange:OnClick()
	return function ( go )
		-- body
		self.uiToggle.value  = true
		if self.onClick then 
			self.onClick()
		end
	end
end
function RoleIconChange:OnDestroy( ... )
	-- body	
	GameObject.Destroy(self.gameObject)
	GameObject.Destroy(self.transform)
	self = nil	
end
return RoleIconChange
UIRolePresented = {
	uiName = 'UIRolePresented',

	-----------------------parameters
	roleList = nil,

	-----------------------UI
	uiRole = nil,

}

function UIRolePresented:Awake()
	self.uiRole = {}
	self.uiRole[1] = getLuaComponent(createUI("RoleBustItem1", self.transform:FindChild("Role/RoleBustItem1")))
	self.uiRole[2] = getLuaComponent(createUI("RoleBustItem1", self.transform:FindChild("Role/RoleBustItem2")))
	self.uiRole[3] = getLuaComponent(createUI("RoleBustItem1", self.transform:FindChild("Role/RoleBustItem3")))
end

function UIRolePresented:Start()
	local enum = self.roleList:GetEnumerator()
	local i = 1
	while enum:MoveNext() do
		local obj = self.uiRole[i]
		obj.id = enum.Current
		obj.hideFightNum = true
		obj.onClickSelect = self:OnClickCard()
		obj:SetResetDisplay()
		i = i + 1
	end
end

function UIRolePresented:OnClickCard()
	return function(go)
		local obj = getLuaComponent(createUI("RolePresented", self.transform))
		obj.id = go.id
	end
end

return UIRolePresented

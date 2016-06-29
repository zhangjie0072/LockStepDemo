--encoding=utf-8

FashionRole =  {
	uiName="FashionRole",

	----------------------------------
	onChoose,
	onBack,
	pos = 0,
	posMap = {0,3,1,2,5,4},

	----------------------------------UI
	uiBtnBack,
	uiRoleGrid,
	uiAnimator,
	uiAllPosition,
	uiCPosition,
	uiPFPosition,
	uiPGPosition,
	uiSGPosition,
	uiSFPosition,
}


-----------------------------------------------------------------
function FashionRole:Awake()
	self.uiBtnBack = createUI('ButtonClose', self.transform:FindChild('ButtonBack'))
	self.uiRoleGrid = self.transform:FindChild('RoleScroll/Role'):GetComponent('UIGrid')
	self.uiAllPosition = self.transform:FindChild('Position/All')
	self.uiCPosition = self.transform:FindChild('Position/C')
	self.uiPFPosition = self.transform:FindChild('Position/PF')
	self.uiPGPosition = self.transform:FindChild('Position/PG')
	self.uiSGPosition = self.transform:FindChild('Position/SG')
	self.uiSFPosition = self.transform:FindChild('Position/SF')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function FashionRole:Start()
	addOnClick(self.uiBtnBack.gameObject, self:ClickBack())
	
	addOnClick(self.uiAllPosition.gameObject, self:ChoosePlayerByPos())
	addOnClick(self.uiCPosition.gameObject, self:ChoosePlayerByPos())
	addOnClick(self.uiPFPosition.gameObject, self:ChoosePlayerByPos())
	addOnClick(self.uiPGPosition.gameObject, self:ChoosePlayerByPos())
	addOnClick(self.uiSGPosition.gameObject, self:ChoosePlayerByPos())
	addOnClick(self.uiSFPosition.gameObject, self:ChoosePlayerByPos())
	self:Refresh()
end

function  FashionRole:FixedUpdate()
end

function FashionRole:OnClose()
	if self.onBack then
		self.onBack()
	end
	NGUITools.Destroy(self.gameObject)
end

function FashionRole:OnDestroy()
	-- body
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function FashionRole:Refresh()
	CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	local roles = MainPlayer.Instance:GetRoleIDList()
	local enum = roles:GetEnumerator()
	while enum:MoveNext() do
		local roleId = enum.Current
		local position = enumToInt(GameSystem.Instance.RoleBaseConfigData2:GetPosition(roleId))
		if self.pos == 0 then
			local role = getLuaComponent(createUI('CareerRoleIcon', self.uiRoleGrid.transform))
			role.id = roleId
			role.gameObject.name = tostring(roleId)
			role.isShowName = true
			role.onClick = self:OnClick()
		else
			if position == self.pos then
				local role = getLuaComponent(createUI('CareerRoleIcon', self.uiRoleGrid.transform))
				role.id = roleId
				role.gameObject.name = tostring(roleId)
				role.isShowName = true
				role.onClick = self:OnClick()
			end
		end
	end
	self.uiRoleGrid.repositionNow = true
	
	self.uiRoleGrid.onCustomSort = function (x, y)
		return self:RoleCompare(getLuaComponent(x).id, getLuaComponent(y).id)
	end
end


-----------------------------------------------------------------
function FashionRole:ClickBack()
	return function (go)
		self:DoClose()
	end
end

function FashionRole:OnClick( ... )
	return function (role)
		if self.onChoose then
			self.onChoose(role.id)
		end
		self:DoClose()
	end
end

function FashionRole:DoClose( ... )
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function FashionRole:ChoosePlayerByPos( ... )
	return function (go)
		if go == self.uiAllPosition.gameObject then
			self.pos = 0
		elseif go == self.uiCPosition.gameObject then
			self.pos = 3
		elseif go == self.uiPFPosition.gameObject then
			self.pos = 1
		elseif go == self.uiPGPosition.gameObject then
			self.pos = 4
		elseif go == self.uiSGPosition.gameObject then
			self.pos = 5
		elseif go == self.uiSFPosition.gameObject then
			self.pos = 2
		end
		self:Refresh()
	end
end

function FashionRole:RoleCompare(xid, yid)
	local datax = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(xid)
	local datay = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(yid)

	local position = self.pos
	local ax = datax.position == FashionRole.posMap[position] or position == 1
	local ay = datay.position == FashionRole.posMap[position] or position == 1

	if not ax and ay then
		return 1
	elseif ax and not ay then
		return -1
	elseif not ax and not ay then
		return 0
	end
	
	local xInSquad = MainPlayer.Instance:IsInSquad(xid)
	local yInSquad = MainPlayer.Instance:IsInSquad(yid)
	
	if xInSquad and not yInSquad then
		return -1
	elseif not xInSquad and yInSquad then
		return 1
	end

	local xInfo = MainPlayer.Instance:GetRole2(xid)
	local yInfo = MainPlayer.Instance:GetRole2(yid)

	if xInfo.level < yInfo.level then
		return 1
	elseif xInfo.level > yInfo.level then
		return -1
	end

	if xInfo.quality < yInfo.quality then
		return 1
	elseif xInfo.quality > yInfo.quality then
		return -1
	end

	if xInfo.star < yInfo.star then
		return 1
	elseif xInfo.star > yInfo.star then
		return -1
	end
	local xTalent = GameSystem.Instance.RoleBaseConfigData2:GetTalent(xid)
	local yTalent = GameSystem.Instance.RoleBaseConfigData2:GetTalent(yid)

	if xTalent < yTalent then
		return 1
	elseif xTalent > yTalent then
		return -1
	end
	
	if xid < yid then
		return 1
	elseif xid > yid then
		return -1
	else
		return 0
	end
end

return FashionRole

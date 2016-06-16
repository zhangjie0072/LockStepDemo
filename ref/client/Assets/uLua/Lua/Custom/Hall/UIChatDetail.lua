UIChatDetail = 
{
	uiName = 'UIChatDetail',
	-----------------------UI
	uiBtnClose,
	uiAnimator,
	uiID,
	uiLevel,
	uiTitle,
	uiSquadGrid,
	-------------------------
}

function UIChatDetail:Awake()
	self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
	self.uiID = self.transform:FindChild('Window/ID/Num'):GetComponent('UILabel')
	self.uiLevel = self.transform:FindChild('Window/LV/Num'):GetComponent('UILabel')
	self.uiTitle = self.transform:FindChild('Window/Title'):GetComponent('MultiLabel')
	self.uiSquadGrid = self.transform:FindChild('Window/RoleList/Scroll/Grid'):GetComponent('UIGrid')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIChatDetail:Start( ... )
	addOnClick(self.uiBtnClose.gameObject, self:DoClose())
end

function UIChatDetail:FixedUpdate( ... )
end

function UIChatDetail:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIChatDetail:Refresh( ... )
	-- body
end

function UIChatDetail:OnClose( ... )
	NGUITools.Destroy(self.gameObject)
end

--------------------------------------

function UIChatDetail:DoClose( ... )
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIChatDetail:SetData(message)
	self.uiID.text = message.id
	self.uiLevel.text = message.level
	self.uiTitle:SetText(message.name)

	CommonFunction.ClearGridChild(self.uiSquadGrid.transform)
	for i,v in ipairs(message.squad) do
	 	local role = createUI('CareerRoleIcon', self.uiSquadGrid.transform)
	 	local roleLua = getLuaComponent(role.gameObject)
	 	roleLua.showPosition = false
	 	roleLua.id = v.role_id
	end

	self.uiSquadGrid.repositionNow = true
end

return UIChatDetail

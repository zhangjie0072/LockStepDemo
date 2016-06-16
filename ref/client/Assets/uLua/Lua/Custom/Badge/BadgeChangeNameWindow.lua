BadgeChangeNameWindow = {
	uiName = "BadgeChangeNameWindow",
	-----params-----
	okClickCallback	 = nil,
	tempName 		 = nil,
	-----UI--------
	uiInputLabel,
	uiBtnCancel,
	uiBtnOk,
	uiAnimator,
}

function BadgeChangeNameWindow:Awake( ... )
	self:UIParise()
	self:AddEvent()
end

function BadgeChangeNameWindow:Start( ... )
	
end

function BadgeChangeNameWindow:FixedUpdate( ... )
	-- body
end

function BadgeChangeNameWindow:OnDestroy( ... )
	-- body
end

function BadgeChangeNameWindow:SetInputDefaultValue(value)
	self.uiInputLabel.value = value
end

function BadgeChangeNameWindow:UIParise( ... )
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiInputLabel = find("Window/Input"):GetComponent("UIInput")
	self.uiBtnCancel = find("Window/ButtonCancel")
	self.uiBtnOk = find("Window/ButtonOK")
	self.uiAnimator = transform:GetComponent("Animator")
end

function BadgeChangeNameWindow:AddEvent( ... )
	addOnClick(self.uiBtnCancel.gameObject,self:OnCancelClickHanlder())
	addOnClick(self.uiBtnOk.gameObject,self:OnOkClickHanlder())
end

function BadgeChangeNameWindow:OnNameChange( ... )
	return function()
		
	end
end

function BadgeChangeNameWindow:OnCancelClickHanlder( ... )
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function BadgeChangeNameWindow:OnOkClickHanlder( ... )
	return function()
		local str = tostring(self.uiInputLabel.value)
		string.gsub(str, "%\t", "")
		string.gsub(str, "%\n", "")
		print("readly change name name is:"..str, utf8strlen(str))
		if utf8strlen(str) > 5 then
			CommonFunction.ShowTip("涂鸦墙名称不可超过5字[-]",nil)
			return 
		end
		if self.okClickCallback then
			self.okClickCallback(str)
		end
		self:OnCancelClickHanlder()()
	end
end

function BadgeChangeNameWindow:OnClose( ... )
	GameObject.Destroy(self.gameObject)
end

return BadgeChangeNameWindow


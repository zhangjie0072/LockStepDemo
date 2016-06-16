ActivityItem = 
{
	uiName = 'ActivityItem',
	activityName,
	dot,
	onClick,
	state = false
}

function ActivityItem:Awake( ... )
	self.activityName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.dot = self.transform:FindChild('Dot'):GetComponent("UISprite")
	addOnClick(self.gameObject, self:OnItemClick())
end

function ActivityItem:Start( ... )
	self.dot.gameObject:SetActive(false)
end

function ActivityItem:Update( ... )
	-- body
end

function ActivityItem:Refresh( ... )
	-- body
end

function ActivityItem:OnItemClick( ... )
	return function (go)
		if self.onClick ~= nil then
			self.onClick(self.activityName.text)
		end
	end
end

function ActivityItem:RefreshItem(state)
	self.dot.gameObject:SetActive(state)
end

return ActivityItem
------------------------------------------------------------------------
-- class name    : BadgetItem --徽章item
-- create time   : 10:10 3-11-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgetItem = {
	uiName = "badgeItem",
	-----vars----
	badgeId = nil,
	---param----
	equipCallback = nil,
	----UI------
	uiIconNode    = nil,
	uiGrid    	  = nil,
	uiBadgeIcon   = nil,
	uiAttr 		  = nil,
}

function BadgetItem:Awake( ... )
	-- body
	self:UIParise()
end

function BadgetItem:Start( ... )
	-- body
	self:AddEvent()
end

function BadgetItem:FixedUpdate( ... )
	-- body
end

function BadgetItem:OnDestroy( ... )
	Object.Destroy(self.gameObject)
end

function BadgetItem:SetBadgeId(badgeId)
	self.badgeId = badgeId
	if self.badgeId then
		local badgeGoods = MainPlayer.Instance:GetBadgesGoodByID(self.badgeId)
		self.uiBadgeIcon:SetId(self.badgeId)
		self.uiBadgeIcon:SetNum(BadgeSystemInfo:GetBadgeleftNumExceptUsed(self.badgeId,BadgeSystemVar.currentBookId))
		local attrT = getLuaComponent(self.transform:FindChild("Grid"))
		attrT:SetBadgeId(self.badgeId)
	end
end


function BadgetItem:Refresh()
	-- if self.uiBadgeIcon then
	-- 	local num =  MainPlayer.Instance.badgeSystemInfo:GetBadgeById(self.badgeId).num
	-- 	self.uiBadgeIcon:SetNum(num)
	-- 	if num==0 then
	-- 		self:OnDestroy()
	-- 	end
	-- end
end

function BadgetItem:UIParise( ... )
	-- body
	local transform = self.transform
	local find  = function(name)
		return transform:FindChild(name)
	end

	self.uiIconNode = find("IconNode")
	self.uiGrid = find("Grid"):GetComponent("UIGrid")
	self.uiBadgeIcon = getLuaComponent(createUI("BadgeIcon",self.uiIconNode.transform))
end

function BadgetItem:AddEvent( ... )
	addOnClick(self.transform.gameObject,self:OnClick())
end

function BadgetItem:OnClick( ... )
	return function()
		print("BadgetItem:OnClick")
		if self.equipCallback then
			self.equipCallback(self.badgeId)
		end
	end
end

return BadgetItem
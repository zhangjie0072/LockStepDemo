
--个人信息-球员展示界面
local PlayerExhibition = {
	uiName = "PlayerExhibition",
	
	--UI
	uiIcon = nil,
	uiLocked = nil,
	uiToggle = nil,


	--数据

}
function PlayerExhibition:Awake( ... )
	-- body
end
function PlayerExhibition:Start( ... )
	-- body

    addOnClick(self.uiIcon.gameObject, self:OnClick())
end
function PlayerExhibition:SetData( info ,defaultChoosed)
	-- body
	local id = info.id 
	local locked = info.locked 

	NGUITools.SetActive(self.uiLocked.gameObject,locked)
	self.uiToggle.value = defaultChoosed or false

end
function PlayerExhibition:OnClick()
	return function ( go )
		-- body
		self.uiToggle.value  = true
		if self.onClick then 
			self.onClick()
		end
	end
end
function PlayerExhibition:OnDestroy( ... )
	-- body	
	GameObject.Destroy(self.gameObject)
	GameObject.Destroy(self.transform)
	self = nil	
end
return PlayerExhibition
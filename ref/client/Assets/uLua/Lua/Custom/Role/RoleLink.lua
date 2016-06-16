------------------------------------------------------------------------
-- class name	: RoleLink
-- create time   : 20150827_135550
------------------------------------------------------------------------



RoleLink =  {
	uiName	 = "RoleLink",
	------------------------------------
	-------  UI
	uiCloseBtn,
	uiGrid,

	------------------------------------
	-------  Data
	onClickClose,
}




function RoleLink:Awake()
	self.go = {}		-- create this for contained gameObject
	self:UiParse()
end


function RoleLink:Start()
	addOnClick(self.uiCloseBtn.gameObject,self:ClickClose())
	self:Refresh()
end


function RoleLink:Refresh()

end


-- parse the prefeb
function RoleLink:UiParse()
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end
	self.uiGrid = find("Window/Scroll/Grid"):GetComponent("UIGrid")
	self.uiCloseBtn = find("Window/ButtonClose"):GetComponent("UIButton")
end

function RoleLink:ClickClose()
	return function()
		if self.onClickClose then
			self:onClickClose()
		end
	end
end

function RoleLink:SetData(roleId)
	local baseData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleId)
	local accessWay = baseData.access_way
	local awItems = Split(accessWay,'&')
	for k,v in pairs(awItems) do
		local awItem = Split(v,':')
		local t = createUI('RoleLinkItem',self.uiGrid.transform)
		local script = getLuaComponent(t)
		script:SetData(awItem[1],awItem[2])
	end
end




return RoleLink

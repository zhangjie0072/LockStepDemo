--encoding=utf-8

QualifyingOutsPopup = 
{
	uiName = 'QualifyingOutsPopup',
	---------UI
	uiGrid,
	uiBtnClose,
	---------parameters
	battle = {},
	onClose,
}


-----------------------------------------------------------------
--Awake
function QualifyingOutsPopup:Awake()
	self.uiGrid = getChildGameObject(self.transform,"Window/Scroll/Grid")
	self.uiBtnClose = self.transform:FindChild("Window/ButtonClose")
	self.uiTitle = self.transform:FindChild("Window/Title1"):GetComponent("MultiLabel")
	self.uiTitle:SetText( getCommonStr("QUALIFYING_RECORDS"))
end

function QualifyingOutsPopup:Start()
	local close = getLuaComponent(createUI("ButtonClose",self.uiBtnClose))
	close.onClick = self:MakeOnClose()
	for i,v in ipairs(self.battle) do
		local info = getLuaComponent(createUI("QualifyingOutsItem",self.uiGrid.transform))
		info.time = v.time
		info.name = v.name
		info.ranking = v.ranking
		info.state = v.state
		info.transform.gameObject.name = 100000000000 - v.time
	end
end

function QualifyingOutsPopup:MakeOnClose()
	return function ()
		if self.onClose then
			self.onClose()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

return QualifyingOutsPopup
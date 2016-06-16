BullFightNpc =  {
	uiName	 = "BullFightNpc",
	--------parameters
	id,
	hardLevel,
	cost,
	unLockLevel,
	complete,
	onClick,
	-- ui
	uiIcon,
	uiDifficulty,
	uiLableName,
	uiPosition,
	uiLock,
	uiLockLevel,
	uiIsSelected,

}




function BullFightNpc:Awake()
	--card
	self.uiIcon = self.transform:FindChild("Card/Icon"):GetComponent("UISprite")
	self.uiDifficulty = self.transform:FindChild("Card/Difficulty"):GetComponent("UILabel")
	self.uiLableName = self.transform:FindChild("Card/Name"):GetComponent("UILabel")
	self.uiPosition = self.transform:FindChild("Card/Position"):GetComponent("UISprite")
	--mask
	self.uiLock = self.transform:FindChild("Lock").gameObject
	self.uiLockLevel = self.transform:FindChild("Lock/Level"):GetComponent("UILabel")
	--is selected
	self.uiIsSelected = self.transform:FindChild("Card/IsSelected").gameObject
	--clear
	self.uiClear = self.transform:FindChild("Card/Clear").gameObject
end

function BullFightNpc:Start()
	addOnClick(self.gameObject, self:OnClick())
	self:Refresh()
end

function BullFightNpc:Refresh()
	local npcConfig = GameSystem.Instance.NPCConfigData:GetConfigData(self.id)
	if not npcConfig then error(self.uiName, "Can't find npc config id:", self.id) end
	local shap_id = GameSystem.Instance.NPCConfigData:GetShapeID(self.id)
	--icon
	self.uiIcon.atlas = getBustAtlas(shap_id)
	self.uiIcon.spriteName = "icon_bust_"..tostring( shap_id)
	--position
	self.uiPosition.spriteName = self["position_" .. npcConfig.position]
	--name
	self.uiLableName.text = npcConfig.name
	--hardlevel
	self.uiDifficulty.text = getCommonStr("DIFFICULTY_" .. self.hardLevel)
	--is luck
	if MainPlayer.Instance.Level < self.unLockLevel then
		NGUITools.SetActive(self.uiLock, true)
		self.uiLockLevel.text = string.format(getCommonStr("STR_SINGLE_LEVEL"),self.unLockLevel)
	else
		NGUITools.SetActive(self.uiLock, false)
	end
	--clear
	if self.complete == true then
		NGUITools.SetActive(self.uiClear, true)
	end
end

function BullFightNpc:SetLock(isLock)
	if self.isLock == true then
		NGUITools.SetActive(self.uiLock, false)
	end
end

function BullFightNpc:OnClick()
	return function()
		if self.onClick then print("item:",self) self:onClick() end
	end
end

return BullFightNpc

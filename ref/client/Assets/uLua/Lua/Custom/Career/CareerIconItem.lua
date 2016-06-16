RoleIconItem =  {
	uiName = 'RoleIconItem',
	-----------UI
	uiExp,
	uiExpProgress,
	uiExpLevel,
	uiExpText,
	uiExpNum,
	uiSide,
	uiIcon,
	uiPosition,
	uiNameLabel,
	uiStar,
	-----------parameters
	id=0,
	npc = false,
}

function RoleIconItem:Awake()
	-------Exp
	self.uiExp = self.transform:FindChild("Exp")
	self.uiExpProgress = self.uiExp:FindChild("Bgexp/Progress")
	self.uiExpLevel = self.uiExp:FindChild("Bgexp/Level")
	self.uiExpText = self.uiExp:FindChild("Text")
	self.uiExpNum = self.uiExp:FindChild("ExpNum")
	-------info
	self.uiSide = self.transform:FindChild("Info/Side"):GetComponent("UISprite")
	self.uiIcon = self.transform:FindChild("Info/Icon"):GetComponent("UISprite")
	self.uiPosition = self.transform:FindChild("Info/Position"):GetComponent("UISprite")
	self.uiNameLabel = self.transform:FindChild("Info/Name"):GetComponent("UILabel")
	self.uiStar = self.transform:FindChild("Info/Star"):GetComponent("UILabel")
end

function RoleIconItem:Start()
	self:SetById( )
end

function RoleIconItem:SetById( )
	local id = self.id
	local positions ={'PF','SF','C','PG','SG'}
	if self.npc == true then
		NGUITools.SetActive(self.uiExp.gameObject,false)
		print("NPCID:",id)
		local npcConfig = GameSystem.Instance.NPCConfigData:GetConfigData(id)
		local shap_id = GameSystem.Instance.NPCConfigData:GetShapeID(id)
		self.uiIcon.atlas = getPortraitAtlas(shap_id)
		self.uiIcon.spriteName = npcConfig.icon
		self.uiPosition.spriteName = 'PT_'..positions[npcConfig.position]
	end
	
end

return RoleIconItem

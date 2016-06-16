

NewRoleBustItem1 =  {
	uiName	= "NewRoleBustItem1",

    id,
    isHas,
	position,
    onClickSelect,
    positions = {'PF','SF','C','PG','SG'},

    uiBg,
    uiIcon,
    uiPosition,
    uiName,
    uiMaskUp,
}

function NewRoleBustItem1:Awake()
    self.uiBg               = self.transform:FindChild("Info/BG"):GetComponent("UISprite")
    self.uiIcon             = self.transform:FindChild("Info/Icon"):GetComponent("UISprite")
    self.uiPosition         = self.transform:FindChild("Info/Position"):GetComponent("UISprite")
	self.uiName             = self.transform:FindChild("Info/Name"):GetComponent("UILabel")
    self.uiMaskUp           = self.transform:FindChild("MaskUp")
end

function NewRoleBustItem1:Start()
	addOnClick(self.gameObject,self:MakeOnCard())

    self:Refresh()
end

function NewRoleBustItem1:MakeOnCard()
	return function()
		if self.onClickSelect then
			playSound("UI/UI_button5")
			self.onClickSelect(self)
		end
	end
end

function NewRoleBustItem1:Refresh()
    local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)

	self.position = config.position
    self.uiBg.spriteName = config.icon_bg
    self.uiIcon.spriteName = config.icon_bust
	self.uiPosition.spriteName = "PT_"..self.positions[config.position]
	self.uiName.text = config.name

    NGUITools.SetActive(self.uiMaskUp.gameObject, not self.isHas)
end



function NewRoleBustItem1:Update()
	
end

function NewRoleBustItem1:FixedUpdate()
	
end

function NewRoleBustItem1:OnDestroy()
	
end

function NewRoleBustItem1:OnClose()
	
end

return NewRoleBustItem1
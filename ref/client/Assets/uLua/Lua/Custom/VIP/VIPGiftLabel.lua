------------------------------------------------------------------------
-- class name	: VIPGiftLabel
-- create time   : 20150807_112958
------------------------------------------------------------------------


VIPGiftLabel =  {
	uiName	 = "VIPGiftLabel",
	label,

}




function VIPGiftLabel:Awake()
	self.go = {} 		-- create this for contained gameObject
	self.go.icon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	self.go.label = self.transform:FindChild("Label"):GetComponent("UILabel")
end


function VIPGiftLabel:OnDestroy()
	
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function VIPGiftLabel:Start()
	self.go.label.text = self.label
	if self.id == 2 then
		self.go.icon.spriteName = "com_property_gold2"
	elseif self.id == 1 then
		self.go.icon.spriteName = "com_property_diamond2"
	end
end

-- uncommoent if needed
-- function VIPGiftLabel:FixedUpdate()

-- end


-- uncommoent if needed
-- function VIPGiftLabel:Update()
	

-- end




return VIPGiftLabel

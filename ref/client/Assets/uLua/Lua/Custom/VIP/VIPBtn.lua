------------------------------------------------------------------------
-- class name	: VIPBtn
-- create time   : 20150804_143017
------------------------------------------------------------------------





VIPBtn =  {
	uiName	 = "VIPBtn",
	on_click = nil,

}




function VIPBtn:Awake()
	self.go = {} 		-- create this for contained gameObject
	self.go.sprite = self.transform:GetComponent("UISprite")
	self.go.label = self.transform:FindChild("Label"):GetComponent("UILabel")
	addOnClick(self.gameObject,self:click())
end

function VIPBtn:set_label(text)
	self.go.label.text = text
end


function VIPBtn:click()
	return function()
		if self.on_click then
			self.on_click()
		end
	end
end

function VIPBtn:set_selected(selected)
	if selected then
		self.go.sprite.spriteName = "com_button_cutpage_property2"
		self.go.label.color = Color.black
		self.go.label.effectColor= Color.black
	else
		self.go.sprite.spriteName = "com_button_cutpage_property"
		self.go.label.color = Color.white
		self.go.label.effectColor= Color.white
	end
end



function VIPBtn:OnDestroy()
	
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function VIPBtn:Start()


end

-- uncommoent if needed
-- function VIPBtn:FixedUpdate()

-- end


-- uncommoent if needed
-- function VIPBtn:Update()
	

-- end




return VIPBtn

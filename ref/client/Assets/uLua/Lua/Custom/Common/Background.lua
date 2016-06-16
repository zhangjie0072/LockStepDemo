------------------------------------------------------------------------
-- class name	: Background
-- create time   : 20150720_153036
------------------------------------------------------------------------





Background =  {
	uiName	 = "Background", 

}




function Background:Awake()
	self.go = {} 		-- create this for contained gameObject
	self.go.floor = self.transform:FindChild("BGYellow"):GetComponent("UISprite")
	self.go.bg = self.transform:GetComponent("UISprite")
	self.dbg0 = self.transform:FindChild("DBG_0"):GetComponent("DBG_0")
	self.dbg1 = self.transform:FindChild("DBG_1"):GetComponent("DBG_1")

end


function Background:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function Background:Start()

end

function Background:set_dbg_y(y)
	self.dbg0:set_y(y)
	self.dbg1:set_y(y)
end


function Background:set_floor_y(y)
	local pos = self.go.floor.transform.localPosition
	pos.y = y 
	self.go.floor.transform.localPosition = pos
end

function Background:set_bg(sprite)
	self.go.bg.spriteName = sprite
end


function Background:set_floor(sprite)
	self.go.floor.spriteName = sprite
end

-- uncommoent if needed
-- function Background:FixedUpdate()

-- end


-- uncommoent if needed
-- function Background:Update()
	

-- end




return Background

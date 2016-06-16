------------------------------------------------------------------------
-- class name	: RoleCompare
-- create time   : 20150721_211922  -- copy from MemberCompare
------------------------------------------------------------------------


RoleCompare =  {
	uiName	 = "RoleCompare", 

}




function RoleCompare:Awake()
	self.go = {} 		-- create this for contained gameObject
	self.go.item0 = self.transform:FindChild("Item0")
	self.go.item1 = self.transform:FindChild("Item1")
	self.go.p0 = self.transform:FindChild("p0")
	self.go.p1 = self.transform:FindChild("p1")
	addOnClick(self.transform:FindChild("mask").gameObject,self:click_back())
end


function RoleCompare:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function RoleCompare:Start()
	local t =createUI("RoleBustItem",self.go.item0.transform)
	self.item0 = getLuaComponent(t)
	self.item0:set_data(self.id0,false)
	self.item0:refresh()
	self.item0:set_panel_depth(10)

	-- self.item0:set_selected(false)
	self.item0:light(true)
	-- self.item0:set_cselect_state()
	-- self.item0:enabled_bc(false)

	t =createUI("PlayerDisplay",self.go.p0.transform)
	self.p0 = getLuaComponent(t)
	t.transform:FindChild("Info"):GetComponent("TweenScale").duration = 0
	t.transform:FindChild("Info"):GetComponent("TweenPosition").duration = 0
	-- todo:
	self.p0:set_member_data(self.id0,1)
	self.p0:visible_bias_btn(false)

	if self.id1 then
		t =createUI("RoleBustItem",self.go.item1.transform)
		self.item1 = getLuaComponent(t)
		self.item1:set_data(self.id1,false)
		self.item1:refresh()
		-- self.item1:set_selected(false)
		self.item1:light(true)
		self.item1:set_panel_depth(10)		
		-- self.item1:enabled_bc(false)
		t =createUI("PlayerDisplay",self.go.p1.transform)
		self.p1 = getLuaComponent(t)
		t.transform:FindChild("Info"):GetComponent("TweenScale").duration = 0
		t.transform:FindChild("Info"):GetComponent("TweenPosition").duration = 0

		-- todo:
		self.p1:set_member_data(self.id1,1)
		self.p1:visible_bias_btn(false)
	end

end


function RoleCompare:click_back()
	return function()
		print("RoleCompare:click_back()")
		if self.on_click_back then
			print("RoleCompare:click_back() on_click_back() called")
			self.on_click_back()
		end
	end
end

function RoleCompare:destroy()
		NGUITools.Destroy(self.gameObject)	
end

-- uncopmmoent if needed
-- function RoleCompare:FixedUpdate()

-- end


-- uncommoent if needed
-- function RoleCompare:Update()
	

-- end




return RoleCompare

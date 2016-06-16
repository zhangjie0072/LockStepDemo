------------------------------------------------------------------------
-- class name	: VIPInfo
-- create time   : 20150804_143017
------------------------------------------------------------------------




VIPInfo =  {
	uiName	 = "VIPInfo", 

}




function VIPInfo:Awake()
	self.go = {} 		-- create this for contained gameObject
		
	self.go.level = self.transform:FindChild("Level"):GetComponent("UILabel")
	self.go.data = self.transform:FindChild("Process/Data"):GetComponent("UILabel")
	self.config = GameSystem.Instance.VipPrivilegeConfig

	self.go.recharge = self.transform:FindChild("Tip/Recharge"):GetComponent("UILabel")
	self.go.next_level = self.transform:FindChild("Tip/NextLevel"):GetComponent("UILabel")
	self.go.data = self.transform:FindChild("Process/Data"):GetComponent("UILabel")
	self.go.progress_bar = self.transform:FindChild("Process"):GetComponent("UIProgressBar")
	self.go.icon = self.transform:FindChild("Tip/Icon")
	
end

function VIPInfo:set_data(level,cost)
	self.level = level

	if level >= 15 then
		self.go.level.text = getCommonStr("STR_VIP_HIGHT")
		NGUITools.SetActive(self.go.icon.gameObject,false)
		NGUITools.SetActive(self.go.recharge.gameObject,false)
		NGUITools.SetActive(self.go.next_level.gameObject,false)
		NGUITools.SetActive(self.go.data.gameObject,false)
		NGUITools.SetActive(self.go.progress_bar.gameObject,false)
	else	
		local next_vip_data = self.config:GetVipData(level+1)
		local next_cost_off = next_vip_data.consume - cost
	
		self.go.level.text = getCommonStr("LABEL_VIP_LEVEL") .. " " ..tostring(level)
		self.go.recharge.text = getCommonStr("STR_PAY_AGAIN") .." "..tostring(next_cost_off)

		self.go.next_level.text = getCommonStr("STR_VIP_UP") .." "..tostring(level+1)
		self.go.data.text = tostring(cost) .."/"..tostring(next_vip_data.consume)
		self.go.progress_bar.value = cost/next_vip_data.consume
	end
	
	
end

function VIPInfo:OnDestroy()
	
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function VIPInfo:Start()


end

-- uncommoent if needed
-- function VIPInfo:FixedUpdate()

-- end


-- uncommoent if needed
-- function VIPInfo:Update()
	

-- end




return VIPInfo

NewComerTrialPop = {
	uiName = "NewComerTrialPop",
	-----UI--------
	btnClose,
	btn1,
	btn2,
	lbl1,
	lbl2,
	icon1,
	icon2,
	uiLblCostStr,
	iconGold,
	uiAnimator,
	onClickbtn,
}

function NewComerTrialPop:Awake( ... )
	self:UIParise()
	self:AddEvent()
end

function NewComerTrialPop:Start( ... )
	
end

function NewComerTrialPop:FixedUpdate( ... )
	-- body
end

function NewComerTrialPop:OnDestroy( ... )
	-- body
end

function NewComerTrialPop:UIParise( ... )
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.btnClose = createUI('ButtonClose',self.transform:FindChild("Window/ButtonClose"))
	self.btn1 = find("Window/Button1"):GetComponent("UIButton")
	self.btn2 = find("Window/Button2"):GetComponent("UIButton")
	self.uiLblCostStr = find("Window/Button2/Label"):GetComponent("UILabel")
	self.lbl1 = find("Window/Num1"):GetComponent("UILabel")
	self.icon1 = find("Window/Icon1")
	self.icon2 = find("Window/Icon2")
	self.iconGold = find("Window/GoodsIconConsume")
	self.uiAnimator = transform:GetComponent("Animator")
end

function NewComerTrialPop:AddEvent( ... )
	addOnClick(self.btnClose.gameObject,self:OnCloseClickHanlder())
	addOnClick(self.btn1.gameObject,self:OnClickHanlder("fragment"))
	addOnClick(self.btn2.gameObject,self:OnClickHanlder("diamont"))
end

function NewComerTrialPop:OnClickHanlder(awardtype)
	return function()
		if self.onClickbtn then
			self.onClickbtn(awardtype)
		end
	end
end

function NewComerTrialPop:OnCloseClickHanlder( ... )
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function NewComerTrialPop:OnClose( ... )
	GameObject.Destroy(self.gameObject)
end

return NewComerTrialPop
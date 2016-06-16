CareerSweepPopup = {
	uiName = 'CareerSweepPopup',
	---------------UI
	uiScroll,
	uiAwardGrid,
	uiBtnOk,
	uiBtnOkLabel,
	
	--------------parameters
	chapterID,
	sectionID,
	times = nil,
	awardsTable = nil,
	onClose,
	hpNotEnough = false,
	timesNotEnough = false,
	pos = {0, 70 ,70 ,70, 70}
}

function CareerSweepPopup:Awake()
	self.uiAnimator = self.transform:GetComponent("Animator")
	self.transform = self.transform:FindChild("Window")
	self.uiScroll = self.transform:FindChild("Scroll"):GetComponent("UIScrollView")
	self.uiAwardGrid = self.transform:FindChild("Scroll/Grid"):GetComponent("UIGrid")
	self.uiBtnOk = self.transform:FindChild("ButtonOK")
	self.uiBtnOkLabel = self.uiBtnOk:FindChild("TextLabel"):GetComponent("MultiLabel")
	self.uiGridAnimator = self.transform:FindChild("Scroll/Grid"):GetComponent("Animator")
	self.uiProcessBar = self.transform:FindChild("bar"):GetComponent("UIProgressBar")
	self.AnimationResp = self.uiAwardGrid.transform:GetComponent("AnimationResp")
	addOnClick(self.uiBtnOk.gameObject, self:MakeOnOK())
	-- self.AnimationResp:AddResp(self:MoveUpGrid(), self.uiAwardGrid.gameObject)

end

function CareerSweepPopup:Start()
	self.uiBtnOkLabel:SetText( getCommonStr("STR_CONFIRM"))
	self:Refresh()
end

function CareerSweepPopup:FixedUpdate()
	if not self:IsAnimation() and not self.beActive then
		self.AnimationResp:AddResp(self:MoveUpGrid(), self.uiAwardGrid.gameObject)
		self.uiGridAnimator:SetTrigger("Trigger" .. self.times)
		-- self.AnimationResp:AddResp(self:MoveUpGrid(), self.uiAwardGrid.gameObject)
		self.beActive = true
	end

	-- if 	(self.hpNotEnough == true or self.timesNotEnough == true)
	-- 	and self.beActive == true
	-- 	and not self:IsGridAnimation()
	-- 	and not self.gridActive then
	-- 	print(self.uiName,"----move up grid")
	-- 	self.gridActive = true
	-- 	-- self.uiGridAnimator.enabled = false
	-- 	self:MoveUpGrid()
	-- end

end

function CareerSweepPopup:Refresh()
	local section = MainPlayer.Instance:GetSection(self.chapterID, self.sectionID)
	print(self.uiName,"----times:",self.times)
	for i = 1, self.times do
		local error = false
		local curTime = section.challenge_times - self.times + i
		local awardsTable = {}
		-- print("awardsTable:",table.getn(self.awardsTable[i].awards))
		-- printTable(self.awardsTable[i].awards)
		if self.awardsTable[i].result ~= 0 then 
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(self.awardsTable[i].result),nil)
			error = true
		end
		if error == true then
			break
		end
		for _,x in pairs(self.awardsTable[i].awards) do
			awardsTable[x.id] = x.value
		end
		local awardPane = getLuaComponent(createUI("CareerSweepResultPane", self.uiAwardGrid.transform))
		awardPane.gameObject.name = (1000 + tostring(curTime))..awardPane.gameObject.name
		awardPane.times = i
		awardPane.awards = awardsTable
		awardPane.icon_adjust = true
	end
	self:IsShowTip()
	-- self.uiAwardGrid:Reposition()
	-- self.uiScroll:ResetPosition()
end

function CareerSweepPopup:MakeOnOK()
	return function ()
		print("CareerSweep animator:",self.uiAnimator)
		if self.uiAnimator then
			print("CareerSweep animation close")
			self:AnimClose()
		else
			print("CareerSweep close")
			self:OnClose()
		end
	end
end

function CareerSweepPopup:OnClose( ... )
	self:OnDestroy()
end

function CareerSweepPopup:OnDestroy()
	if self.onClose then
		self.onClose()
	end
	Object.Destroy(self.uiAnimator)
	--Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)

	self.AnimationResp:RemoveResp(self.uiAwardGrid.gameObject)
end

function CareerSweepPopup:IsAnimation()
	local animator = self.uiAnimator
	for i = 0 ,(animator.layerCount - 1) do
		local isInTransition = animator:IsInTransition(i)
		local time = animator:GetCurrentAnimatorStateInfo(i).normalizedTime
		if isInTransition or time < 1 then
			return true
		end
	end
	return false
end

function CareerSweepPopup:IsShowTip()
	if self.hpNotEnough == true then
		--体力不足只能扫荡times
		local obj = createUI("CareerSweepTip", self.uiAwardGrid.transform)
		obj.transform:FindChild("Tip"):GetComponent("UILabel").text =  getCommonStr("STR_HP_NOTENOUGH_SWEEP"):format(self.times)
	elseif self.timesNotEnough == true then
		--次数不足只能扫荡times
		local obj = createUI("CareerSweepTip", self.uiAwardGrid.transform)
		obj.transform:FindChild("Tip"):GetComponent("UILabel").text = getCommonStr("STR_TIMES_NOTENOUGH_SWEEP"):format(self.times) 
	end
end

function CareerSweepPopup:MoveUpGrid()
	return function()
		if self.hpNotEnough == true or self.timesNotEnough == true then
			print(self.uiName,"---animator resp")
			self.uiGridAnimator.enabled = false
			local obj = self.uiAwardGrid.gameObject 
			self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(0, obj.transform.localPosition.y + self.pos[self.times], 0))
			self.TweenPosition.method = UITweener.Method.EaseInOut
			-- self.uiProcessBar:ForceUpdate()
			-- self.uiProcessBar.value = 1
			-- self.uiScroll:OnScrollBar()
		end
	end
end

function CareerSweepPopup:IsGridAnimation()
	local animator = self.uiGridAnimator
	for i = 0 ,(animator.layerCount - 1) do
		local isInTransition = animator:IsInTransition(i)
		local time = animator:GetCurrentAnimatorStateInfo(i).normalizedTime
		if isInTransition or time < 1 then
			return true
		end
	end
	return false
end

return CareerSweepPopup
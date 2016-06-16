TrainingItemState = {
	IDLE = 1,
	COOLING = 2,
	LOCKED = 3,
}

TrainingItem = {
	uiName = 'TrainingItem',
	
	trainingInfo,

	-- function handler (skillSlot)
	onClick = nil,
	onDetail = nil,
	onUpgrade = nil,

	--accessible
	--trainingConfig
	--trainingLevelConfig
}

function TrainingItem:Awake()
	self.uiBackground = self.transform:GetComponent("UISprite")
	self.uiProgress = getComponentInChild(self.transform, "Progress", "UIProgressBar")
	self.uiProgressLabel = getComponentInChild(self.uiProgress.transform, "Label", "UILabel")
	self.uiProgressFore1 = getChildGameObject(self.uiProgress.transform, "Fore1")
	self.uiProgressFore2 = getChildGameObject(self.uiProgress.transform, "Fore2")
	self.uiProgressGrids = {}
	for i = 1, self.uiProgressFore2.transform.childCount do
		self.uiProgressGrids[i] = getChildGameObject(self.uiProgressFore2.transform, tostring(i))
	end
	self.uiName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiTime = getComponentInChild(self.transform, "Time", "UILabel")
	self.uiUnlock = getChildGameObject(self.transform, "Unlock")
	self.uiUnlockLevel = getComponentInChild(self.uiUnlock.transform, "Label", "UILabel")

	addOnClick(self.gameObject, self:MakeOnClick())
end

function TrainingItem:Start()
	self:Refresh()
end

function TrainingItem:FixedUpdate()
	if self.trainingInfo.state == TrainingState.TS_COOLING then
		local elapsedTime = os.time() - self.cdStartingTime
		local remainingTime = self.remainingTime - elapsedTime
		self:SetTimeProgress(remainingTime)	--设置剩余时间进度条
		self.uiTime.text = self:GetTimeLabel(remainingTime)
	end
end

function TrainingItem:Refresh()
	--if not self.trainingInfo then return end

	self.trainingConfig = GameSystem.Instance.TrainingConfig:GetTrainingConfig(self.trainingInfo.id)
	self.trainingLevelConfig = GameSystem.Instance.TrainingConfig:GetTrainingLevelConfig(self.trainingInfo.id, self.trainingInfo.level)
	if self.trainingInfo.level > 0 then
		self.prevLevelConfig = GameSystem.Instance.TrainingConfig:GetTrainingLevelConfig(self.trainingInfo.id, self.trainingInfo.level - 1)
	end
	self.maxLevel = self:GetMaxLevel()

	self.uiName.text = self.trainingConfig.name

	if self.trainingInfo.state == TrainingState.TS_IDLE then		--空闲
		local unlockLevel = self.trainingLevelConfig.level_limit
		if unlockLevel <= MainPlayer.Instance.Level	then	--可升级
			self.uiBackground.spriteName = self.NormalBackground
			self.uiProgress.value = self.trainingInfo.level / self.maxLevel
			NGUITools.SetActive(self.uiProgressLabel.gameObject, true)
			if self.trainingInfo.level == 0 then
				self.uiProgressLabel.text = getCommonStr("TRAINING_UNTRAINED")
			else
				self.uiProgressLabel.text = "Lv." .. self.trainingInfo.level
			end
			NGUITools.SetActive(self.uiProgressFore1, self.trainingInfo.level > 0)
			NGUITools.SetActive(self.uiProgressFore2, false)
			NGUITools.SetActive(self.uiTime.gameObject, false)
			NGUITools.SetActive(self.uiUnlock, false)
		else														--未解锁
			self.uiBackground.spriteName = self.LockedBackground
			NGUITools.SetActive(self.uiProgressLabel.gameObject, true)
			if self.trainingInfo.level == 0 then
				self.uiProgressLabel.text = getCommonStr("TRAINING_UNTRAINED")
			else
				self.uiProgressLabel.text = "Lv." .. self.trainingInfo.level
			end
			NGUITools.SetActive(self.uiProgressFore1, false)
			NGUITools.SetActive(self.uiProgressFore2, false)
			NGUITools.SetActive(self.uiTime.gameObject, false)
			NGUITools.SetActive(self.uiUnlock, true)
			self.uiUnlockLevel.text = tostring(unlockLevel)
		end
		self.remainingTime = nil
		self.cdStartingTime = nil

	elseif self.trainingInfo.state == TrainingState.TS_COOLING then	--冷却中
		self.uiBackground.spriteName = self.NormalBackground
		NGUITools.SetActive(self.uiProgressLabel.gameObject, false)
		NGUITools.SetActive(self.uiProgressFore1, false)
		NGUITools.SetActive(self.uiProgressFore2, true)
		self:SetTimeProgress(self.trainingInfo.remaining_time)	--设置剩余时间进度条
		NGUITools.SetActive(self.uiTime.gameObject, true)
		self.uiTime.text = self:GetTimeLabel(self.trainingInfo.remaining_time)
		if not self.remainingTime or not self.cdStartingTime then
			self.remainingTime = self.trainingInfo.remaining_time
			self.cdStartingTime = os.time()
		end
		NGUITools.SetActive(self.uiUnlock, false)

	else
		error("TrainingItem: error state:", self.trainingInfo.state)
	end
end

function TrainingItem:GetMaxLevel()
	local enum = self.trainingConfig.lv_data:GetEnumerator()
	while enum:MoveNext() do end
	local maxLevel = enum.Current.Key
	local position = enumToInt(MainPlayer.Instance.Captain.m_position)
	local limitLevel = self.trainingConfig.lv_limit:get_Item(position)
	if limitLevel then maxLevel = limitLevel end
	return maxLevel
end

function TrainingItem:SetTimeProgress(remainingTime)
	if remainingTime < 0 then remainingTime = 0 end
	local elapsedTime = self.prevLevelConfig.cd - remainingTime
	local count = #self.uiProgressGrids
	local gridNum = math.ceil(elapsedTime / self.prevLevelConfig.cd * count)
	--print(elapsedTime, self.prevLevelConfig.cd, gridNum)
	for i = 1, count do
		NGUITools.SetActive(self.uiProgressGrids[i], i <= gridNum)
	end
end

function TrainingItem:GetTimeLabel(time)
	if time < 0 then time = 0 end
	local hours = math.floor(time / 3600)
	local minutes = math.floor(time % 3600 / 60)
	local seconds = math.floor(time % 60)
	return string.format("%02d:%02d:%02d", hours, minutes, seconds)
end

function TrainingItem:ShowMenu()
	local canUpgrade = (self.trainingInfo.state == TrainingState.TS_IDLE) and 
		(self.trainingLevelConfig.level_limit <= MainPlayer.Instance.Level)
	local canDetail = self.trainingInfo.level > 0

	if canUpgrade or canDetail then
		local go = createUI("PopupRingMenu", self.transform)
		UIManager.Instance:BringPanelForward(go)
		self.menu = getLuaComponent(go)
		self.menu.itemImages = {
			canDetail and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_detail"} or {},
			canUpgrade and {atlas = "captainSystem/captainSystem", sprite = "captain_circle_update"} or {}
		}
		self.menu.startAngle = 155
		self.menu.deltaAngle = 50
		self.menu.onClick = self:MakeOnItemClick()
		self.menu.onClickFullScreen = function () self:HideMenu() end
	end
end

function TrainingItem:HideMenu()
	if self.menu then
		NGUITools.Destroy(self.menu.gameObject)
		self.menu = nil
	end
end

function TrainingItem:MakeOnClick()
	return function ()
		if self.onClick then self:onClick() end
	end
end

function TrainingItem:MakeOnItemClick()
	return function (index)
		if index == 1 then
			if self.onDetail then self.onDetail(self) end
		elseif index == 2 then
			if self.onUpgrade then self.onUpgrade(self) end
		else
			error("TattooSlot: Menu index error")
		end
	end
end

return TrainingItem

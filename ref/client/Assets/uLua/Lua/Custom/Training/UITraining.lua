--encoding=utf-8

UITraining = {
	uiName = 'UITraining',

	----------------------------------

	SLOT_COUNT = 8,

	----------------------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UITraining:Awake()
	local window = self.transform:FindChild("Window")

	addOnClick(window:FindChild("Background").gameObject, self:MakeOnBGClick())

	local backNode = window:FindChild("BackButtonNode")
	getLuaComponent(createUI("ButtonBack", backNode)).onClick = self:MakeOnBackClick()

	self.tmModel = window:FindChild("Model")

	local btnSkill = window:FindChild("Functions/Grid/Skill").gameObject
	local btnTattoo = window:FindChild("Functions/Grid/Tattoo").gameObject
	local btnFashion = window:FindChild("Functions/Grid/Fashion").gameObject

	self.uiAnimator = self.transform:GetComponent('Animator')

	addOnClick(btnSkill, self:MakeOnSkillClick())
	addOnClick(btnTattoo, self:MakeOnTattooClick())
	addOnClick(btnFashion, self:MakeOnFashionClick())

	local nodes = window:FindChild("Nodes")
	self.slots = {}
	for i = 1, self.SLOT_COUNT do
		local node = nodes:FindChild("Node" .. i)
		self.slots[i] = self:InstantiateSlot(node, "Training" .. i)
		self.slots[i].slotID = i
	end

	LuaHelper.RegisterPlatMsgHandler(MsgID.OpenTrainingRespID, self:MakeOpenTrainingHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.StartTrainingRespID, self:MakeStartTrainingHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyTrainingInfoID, self:MakeNotifyTrainingInfoHandler(), self.uiName)
end

function UITraining:Start()
	self.playerDisplay = getLuaComponent(createUI("PlayerDisplay", self.tmModel))
	self.playerDisplay:set_captain_data(MainPlayer.Instance.CaptainID, MainPlayer.Instance.Captain.m_roleInfo.bias)
	self.playerDisplay.is_captain = true
	self.playerDisplay.is_show_lock = false
end

function UITraining:FixedUpdate()
	-- body
end

function UITraining:OnClose()
	if self.curSlot then
		self.curSlot:HideMenu()
	end
	TopPanelManager:ShowPanel("UIHall")
end

function UITraining:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.OpenTrainingRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.StartTrainingRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyTrainingInfoID, self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-----------------------------------------------------------------
--Called by TopPanelManager, return false to disable displaying
function UITraining:OnShow()
	if self.dataRetrieved then
		self.dataRetrieved = false
		print("UITraining: OnShow, true")
		return true
	else
		local req = {
			captain_id = MainPlayer.Instance.CaptainID
		}

		local data = protobuf.encode("fogs.proto.msg.OpenTraining", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.OpenTrainingID, data)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
		print("UITraining: OnShow, false")
		return false
	end
end

function UITraining:MakeOpenTrainingHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		print("OpenTrainingHandler")
		local resp, err = protobuf.decode("fogs.proto.msg.OpenTrainingResp", buf)
		if resp then
			if resp.result == 0 then
				local trainings = MainPlayer.Instance:GetCaptainInfo(resp.captain_id).training
				self:UpdateTrainingInfo(trainings, resp.training)
				self.dataRetrieved = true
				TopPanelManager:ShowPanel("UITraining")
				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITraining:", err)
		end
	end
end

function UITraining:MakeNotifyTrainingInfoHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		local resp, err = protobuf.decode("fogs.proto.msg.NotifyTrainingInfo", buf)
		if resp then
			local trainings = MainPlayer.Instance:GetCaptainInfo(resp.captain_id).training
			self:UpdateTrainingInfo(trainings, resp.training)
			self:Refresh()
		else
			error("UITraining:", err)
		end
	end
end

function UITraining:UpdateTrainingInfo(trainings, source)
	for _, src in ipairs(source) do
		local dest
		for i = 0, trainings.Count - 1 do
			local training = trainings[i]
			if training.id == src.id then
				dest = training
				break
			end
		end
		if not dest then
			dest = TrainingInfo.New()
			trainings:Add(dest)
		end
		dest.id = src.id
		dest.level = src.level
		dest.state = TrainingState[src.state]
		dest.remaining_time = src.remaining_time
	end
end

function UITraining:Refresh()
	local trainings = MainPlayer.Instance:GetCaptainInfo(MainPlayer.Instance.CaptainID).training
	local enum = trainings:GetEnumerator()
	local index = 1
	while enum:MoveNext() do
		self.slots[index].trainingInfo = enum.Current
		self.slots[index]:Refresh()
		index = index + 1
	end

	if self.playerDisplay then
		self.playerDisplay:update_id(MainPlayer.Instance.CaptainID)
		self.playerDisplay:refresh()
	end

end

function UITraining:InstantiateSlot(node, name)
	local go = createUI("TrainingItem", node)
	go.name = name
	local slot = getLuaComponent(go)
	slot.onClick = self:MakeOnSlotClick()
	slot.onDetail = self:MakeOnDetail()
	slot.onUpgrade = self:MakeOnUpgrade()
	return slot
end

function UITraining:MakeOnSlotClick()
	return function (slot)
		if self.curSlot ~= nil then self.curSlot:HideMenu() end
		self.curSlot = slot
		if slot.trainingInfo.state == TrainingState.TS_IDLE then
			self:MakeOnUpgrade()(slot)
		else
			self:MakeOnDetail()(slot)
		end
	end
end

function UITraining:MakeOnDetail()
	return function (slot)
		slot:HideMenu()
		local detail = getLuaComponent(createUI("TrainingDetailPopup", self.transform))
		bringNear(detail.transform)
		NGUITools.BringForward(detail.gameObject)
		detail.trainingID = slot.trainingInfo.id
		detail.level = slot.trainingInfo.level
	end
end

function UITraining:MakeOnUpgrade()
	return function (slot)
		slot:HideMenu()
		self.upgrade = getLuaComponent(createUI("TrainingUpgradePopup", self.transform))
		bringNear(self.upgrade.transform)
		NGUITools.BringForward(self.upgrade.gameObject)
		self.upgrade.trainingID = slot.trainingInfo.id
		self.upgrade.fromLevel = slot.trainingInfo.level
		self.upgrade.onUpgrade = self:MakeOnConfirmUpgrade()
	end
end

function UITraining:MakeOnConfirmUpgrade()
	return function ()
		print("UITraining: Upgrade training: ", self.upgrade.trainingID)

		local req = {
			captain_id = MainPlayer.Instance.CaptainID,
			training_id = self.upgrade.trainingID,
		}

		local data = protobuf.encode("fogs.proto.msg.StartTraining", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.StartTrainingID, data)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UITraining:MakeStartTrainingHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.StartTrainingResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleUpgrade(resp)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				playSound("UI/UI-wrong")
			end
		else
			error("UITraining: " .. err)
		end
	end
end

function UITraining:HandleUpgrade(resp)
	playSound("UI/UI_level_up_01")
	if resp.captain_id == MainPlayer.Instance.CaptainID then
		local slot
		for _, s in ipairs(self.slots) do
			if s.trainingInfo.id == resp.training_id then
				slot = s
				break
			end
		end
		local trainingInfo
		local enum = MainPlayer.Instance:GetCaptainInfo(MainPlayer.Instance.CaptainID).training:GetEnumerator()
		while enum:MoveNext() do
			if enum.Current.id == resp.training_id then
				trainingInfo = enum.Current
				break
			end
		end
		trainingInfo.level = trainingInfo.level + 1
		trainingInfo.state = TrainingState.TS_COOLING
		trainingInfo.remaining_time = slot.trainingLevelConfig.cd
		print("Update, ID:", trainingInfo.id, "time:", slot.trainingLevelConfig.cd)
		slot.trainingInfo = trainingInfo
		slot:Refresh()
		if self.upgrade then
			NGUITools.Destroy(self.upgrade.gameObject)
			self.upgrade = nil
		end
		CommonFunction.ShowPopupMsg(getCommonStr("TRAINING_SUCCEED"),nil,nil,nil,nil,nil)
		self.playerDisplay:refresh_ui()
	end
end

function UITraining:MakeOnBGClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
	end
end

function UITraining:MakeOnBackClick()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UITraining:MakeOnSkillClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITraining: skill clicked.")
		TopPanelManager:ShowPanel("UISkill")
	end
end

function UITraining:MakeOnTattooClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITraining: tattoo clicked.")
		TopPanelManager:ShowPanel("UITattoo")
	end
end

function UITraining:MakeOnFashionClick()
	return function ()
		if self.curSlot then self.curSlot:HideMenu() end
		print("UITraining: fashion clicked.")
	end
end

return UITraining

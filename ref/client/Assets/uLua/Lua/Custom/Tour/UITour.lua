--encoding=utf-8

UITour = {
	uiName = "UITour",

	maxTourID = 5,

	-------------PARAMETERS
	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	openStore = false,
	showCompletePopup = false,
	onClose,

	-----------------UI
	uiBackGrid,
	uiButtonMenuGrid,
	uiTitle,
	uiBtnTourShop,
	uiBtnRule,
	uiBtnRuleLabel,
	uiItemNodes,
	uiRemainTimes,
	uiTimeTip,
	btnBuyTime,

	uiAnimator,
	uiBack,

	onTourInfoUpdated = nil,
	lastChallengeTime,
	challengeRestoreTime,
	elapsedRestoreTime,
}


-----------------------------------------------------------------
function UITour:Awake()
	self.uiPropertyGrid = self.transform:FindChild("Top/PlayerProperty")
	self.uiBackGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiButtonMenuGrid = self.transform:FindChild("Top/ButtonMenu")
	self.uiTitle = self.transform:FindChild("Top/Title"):GetComponent("MultiLabel")
	self.uiTitle:SetText(getCommonStr("STR_STREET_TOUR"))
	self.uiRemainTimes = self.transform:FindChild("Bottom/Badge/Num"):GetComponent("UILabel")
	self.uiTimeTip = self.transform:FindChild("Bottom/TimeTip"):GetComponent("UILabel")
	self.btnBuyTime = self.transform:FindChild("Bottom/Badge/Add").gameObject
	--btn
	self.uiBtnTourShop = getChildGameObject(self.transform,"Bottom/TourShop")
	self.uiBtnRule = getChildGameObject(self.transform, "Bottom/ButtonRule")
	self.uiBtnRuleLabel = self.uiBtnRule.transform:FindChild("Text"):GetComponent("MultiLabel")
	self.uiBtnRuleLabel:SetText( getCommonStr("STR_HONOR_COMPETITION_RULES"))

	self.uiItemNodes = {}
	for i = 1, self.maxTourID do
		table.insert(self.uiItemNodes, self.transform:FindChild("Map/Node" .. i))
	end

	self.uiAnimator = self.transform:GetComponent('Animator')

	--新增进入巡回赛商店事件
	addOnClick(self.uiBtnTourShop, self:MakeOnTourShop())
	addOnClick(self.uiBtnRule, self:MakeOnRule())
	addOnClick(self.btnBuyTime, self:MakeOnBuyTime())
end

function UITour:Start()
	----show complete popup
	if self.showCompletePopup == true then
		local obj = getLuaComponent(createUI("GoodsAcquirePopup"))
		obj.tourUse = true
	end

	self.uiProperty = getLuaComponent(createUI("PlayerProperty" ,self.uiPropertyGrid))
	self.uiProperty.showPrestige = true
	-------create button
	local back = getLuaComponent(createUI("ButtonBack",self.uiBackGrid))
	back.onClick = self:MakeOnBack()
	self.uiBtnMenu = createUI("ButtonMenu", self.uiButtonMenuGrid)
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	-------
	LuaHelper.RegisterPlatMsgHandler(MsgID.TourResetRespID, self:MakeTourResetHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.UpdateTourInfoRespID, self:MakeUpdateTourInfoHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.TourBuyChallengeTimesRespID, self:MakeBuyChallengeTimesHandler(), self.uiName)
end

function UITour:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TourResetRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.UpdateTourInfoRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.TourBuyChallengeTimesRespID, self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UITour:Refresh()
	self.totalTimes = GameSystem.Instance.CommonConfig:GetUInt("gTourTotalTimes")
	print(self.uiName, "TotalTimes", self.totalTimes)
	self.lastChallengeTime = MainPlayer.Instance.tourInfo.last_challenge_time
	self.challengeRestoreTime = GameSystem.Instance.CommonConfig:GetUInt("gTourRestoreTime")
	self.curTourID = MainPlayer.Instance.CurTourID
	print(self.uiName, "CurTourID:", self.curTourID)
	self.tourData = GameSystem.Instance.TourConfig:GetTourData(self.curTourID, MainPlayer.Instance.Level)
	if self.tourData then
		self.gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(self.tourData.gameModeID)
	end

	-- tour items
	if not self.tourItems then
		self.tourItems = {}
		for i = 1, self.maxTourID do
			local item = getLuaComponent(createUI("TourItem", self.uiItemNodes[i].transform))
			item.name = "TourItem" .. i
			item.tourID = i
			item.isCurrent = (i == self.curTourID)
			item.onClick = self:MakeOnTourClick()
			self.tourItems[i] = item
		end
	else
		for i = 1, self.maxTourID do
			local item = self.tourItems[i]
			item.isCurrent = (i == self.curTourID)
			item:Refresh()
		end
	end

	--设置返回的界面
	UIStore:SetBackUI(self.uiName)
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
	self.uiProperty:Refresh()

	self:ReqUpdateTourInfo()
	if not self.showCompletePopup then
		self.onTourInfoUpdated = function ()
			self.onTourInfoUpdated = nil
			if self.curTourID == 0 then
				self:StartNewRoundPrompt()
			end
		end
	end
end

function UITour:RefreshChallengeTimes()
	local remain = MainPlayer.Instance.tourInfo.challenge_times
	self.uiRemainTimes.text = remain .. "/" .. self.totalTimes
	print(self.uiName, "RefreshChallengeTimes, remain:", remain, "total:", self.totalTimes)
	NGUITools.SetActive(self.uiTimeTip.gameObject, remain < self.totalTimes)
	NGUITools.SetActive(self.btnBuyTime, remain == 0)
end

function UITour:FixedUpdate()
	if self.challenge_times and self.totalTimes then
		--print(self.uiName, "ChallengeTimes:", self.challenge_times, "Total times:", self.totalTimes)
		if self.challenge_times < self.totalTimes then
			self.elapsedRestoreTime = self.elapsedRestoreTime + UnityTime.fixedDeltaTime
			if self.elapsedRestoreTime > self.challengeRestoreTime then	-- can restore one challenge time, req to update
				self.elapsedRestoreTime = self.elapsedRestoreTime - self.challengeRestoreTime
				self:ReqUpdateTourInfo()
			else
				local countdown = self.challengeRestoreTime - self.elapsedRestoreTime
				if countdown < 3600 then
					local minutes = math.floor(countdown / 60)
					local seconds = math.floor(countdown % 60)
					self.uiTimeTip.text = getCommonStr("TOUR_CHALLENGE_TIMES_COUNTDOWN2"):format(minutes, seconds)
				else
					local hours = math.floor(countdown / 3600)
					local minutes = math.floor((countdown % 3600) / 60)
					self.uiTimeTip.text = getCommonStr("TOUR_CHALLENGE_TIMES_COUNTDOWN1"):format(hours, minutes)
				end
			end
			NGUITools.SetActive(self.uiTimeTip.gameObject, true)
		else
			NGUITools.SetActive(self.uiTimeTip.gameObject, false)
		end
	end
end

function UITour:OnClose()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, true)

	if self.onClose then 
		self.onClose()
		self.onClose = nil
		return
	end
	print(self.uiName,"TopPanelManager nextShowUI:",self.nextShowUI)
	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	elseif self.openStore == true then
		UIStore:SetType('ST_TOUR')
		UIStore:SetBackUI(self.uiName)
		UIStore:OpenStore()
		self.openStore = false
	else
		-- if self.uiBack then
		-- 	jumpToUI(self.uiBack)
		-- else
		-- 	TopPanelManager:HideTopPanel()
		-- end
		TopPanelManager:HideTopPanel()
	end
end

function UITour:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UITour:ReqUpdateTourInfo()
	local data = protobuf.encode("fogs.proto.msg.UpdateTourInfoReq", {})
	LuaHelper.SendPlatMsgFromLua(MsgID.UpdateTourInfoReqID, data)
	CommonFunction.ShowWaitMask()
	CommonFunction.ShowWait()
	print(self.uiName, "Send update tour info req")
end

function UITour:StartNewRoundPrompt()
	if self.challenge_times == 0 then
		return
	end
	CommonFunction.ShowPopupMsg(getCommonStr("TOUR_START_NEW_ROUND_PROMPT"), nil, function ()
		local data = protobuf.encode("fogs.proto.msg.TourResetReq", {})
		LuaHelper.SendPlatMsgFromLua(MsgID.TourResetReqID, data)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait(0)
	end, function() end, nil, nil)
end

-----------------------------------------------------------------
function UITour:MakeOnTourClick()
	return function(item)
		if item.tourID == 1 and self.curTourID == 0 then
			self:StartNewRoundPrompt()
			return
		end
		--小于当前id时不显示点击按钮，大于当前id时显示未解锁
		if item.tourID == MainPlayer.Instance.CurTourID then
			self.TourSectionPopup = createUI("TourSectionPopup",self.transform)
			local obj = getLuaComponent(self.TourSectionPopup)
			if MainPlayer.Instance.CurTourID >= item.tourID then
				obj.tourID = item.tourID
				obj.is_current = item.isCurrent
			end
			UIManager.Instance:BringPanelForward(self.TourSectionPopup)
		end
	end
end

function UITour:MakeOnBack()
	return function ()
		self:DoClose()
	end
end

function UITour:MakeOnRule()
	return function ()
		self.rule = getLuaComponent(createUI("MatchRulePopup",self.transform))
		self.rule.leagueType = GameMatch.LeagueType.eTour
	end
end

function UITour:MakeOnTourShop()
	return function (go)
		--print("Enter Tour Shop")
		if validateFunc('UIStore') then
			self.openStore = true
			self:DoClose()
		end
	end
end

function UITour:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.rule.gameObject)
	end
end

function UITour:MakeOnBuyTime()
	return function ()
		-- Prompt
		local cost = GameSystem.Instance.TourConfig:GetResetCost(MainPlayer.Instance.tourInfo.buy_times + 1)
		local totalBuyTimes = GameSystem.Instance.VipPrivilegeConfig:GetVipData(MainPlayer.Instance.Vip).reset_tour_times
		local remainTimes = totalBuyTimes - MainPlayer.Instance.tourInfo.buy_times

			-- Check buy times
			if remainTimes <= 0 then
				local targetVip
				local enum = GameSystem.Instance.VipPrivilegeConfig.Vipdatas:GetEnumerator()
				while enum:MoveNext() do
					local vipData = enum.Current.Value
					if vipData.reset_tour_times > totalBuyTimes then
						targetVip = enum.Current.Key
						break
					end
				end
				if MainPlayer.Instance.Vip ~= GameSystem.Instance.VipPrivilegeConfig.maxVip then
					CommonFunction.ShowPopupMsg(getCommonStr("TOUR_CAN_NOT_BUY_CHALLENGE"):format(targetVip), nil, nil, nil, nil, nil)
				else
					CommonFunction.ShowPopupMsg(getCommonStr("TOUR_CAN_NOT_BUY_CHALLENGE_FOR_VIP_MAX"), nil, nil, nil, nil, nil)
				end
				return
			end
			CommonFunction.ShowPopupMsg(getCommonStr("TOUR_BUY_CHALLENGE_TIME_PROMPT"):format(cost.Value, remainTimes), nil, function ()			
			-- Check cost
			if cost.Value > MainPlayer.Instance:GetGoodsCount(cost.Key) then
				CommonFunction.ShowPopupMsg(getCommonStr("NOT_ENOUGH_DIAMOND"), nil, nil, nil, nil, nil)
				return
			end
			-- Send msg
			local req = {}
			local buf = protobuf.encode("fogs.proto.msg.TourBuyChallengeTimesReq", req)
			LuaHelper.SendPlatMsgFromLua(MsgID.TourBuyChallengeTimesReqID, buf)
			CommonFunction.ShowWaitMask()
			CommonFunction.ShowWait()
		end, function () end, nil, nil)
	end
end

function UITour:MakeTourResetHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.TourResetResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleReset(resp)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITour:", err)
		end
	end
end

function UITour:HandleReset(resp)
	self:HandleUpdateInfo(resp)
	MainPlayer.Instance.CurTourID = 1
	self:Refresh()
end

function UITour:MakeUpdateTourInfoHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.UpdateTourInfoResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleUpdateInfo(resp)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITour:", err)
		end
	end
end

function UITour:HandleUpdateInfo(resp)
	print(self.uiName, "Handle update info")
	local tour_info = resp.tour_info
	local tourInfo = MainPlayer.Instance.tourInfo
	if tour_info.npc and table.getn(tour_info.npc) > 0 then
		tourInfo.npc:Clear()
		for _, id in ipairs(tour_info.npc) do
			tourInfo.npc:Add(id)
		end
	end
	printTable(tour_info)
	tourInfo.challenge_times = tour_info.challenge_times
	tourInfo.last_challenge_time = tour_info.last_challenge_time
	-- tourInfo.buy_times = tour_info.buy_times
	self.challenge_times = tour_info.challenge_times
	self.lastChallengeTime = tour_info.last_challenge_time
	self.elapsedRestoreTime = (GameSystem.mTime - self.lastChallengeTime) % self.challengeRestoreTime

	self:RefreshChallengeTimes()

	if self.onTourInfoUpdated then
		self.onTourInfoUpdated()
	end
end

function UITour:MakeBuyChallengeTimesHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.TourBuyChallengeTimesResp", buf)
		if resp then
			if resp.result == 0 then
				self:HandleBuyChallengeTimes(resp)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITour:", err)
		end
	end
end

function UITour:HandleBuyChallengeTimes(resp)
	-- time can be 0 or 1,  bought already so set it 1.
	MainPlayer.Instance.tourInfo.challenge_times= 1
	MainPlayer.Instance.tourInfo.buy_times = resp.times
	self.challenge_times = 1 
	print(self.uiName, "HandleBuyChallengeTimes, times:", self.challenge_times)
	self:Refresh()
end

function UITour:SetModelActive(active)
	-- body
end

return UITour, "UITour"

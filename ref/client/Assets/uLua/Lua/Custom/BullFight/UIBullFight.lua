UIBullFight =  {
	uiName     = "UIBullFight",
	-- parameters
	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	curItem = nil,
	gameMode,
	npcs = nil,
	itemTable = nil,
	curIndex = nil,
	uiBack,
	preSelected,
	onClose = nil,
	showLocalTimer= 0,
	-- ui
	uiBackGrid,
	uiPlayerInfoGrid,
	uiTitleLabel,
	uiMenuGrid,
	uiNpcGrid,
	uiDifficultLabel,
	uiLeftTimesLabel,
	uiLeftTimesButton,
	uiAwardsGrid,
	uiStartBtn,
	uiBackButton,
	uiPlayerInfo,
	uiBtnMenu,
	uiBullFightNpc = {},

	uiAnimator,
}

function UIBullFight:Awake()
	--ui top
	self.uiBackGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiPlayerInfoGrid = self.transform:FindChild("Top/PlayerInfoGrids")
	self.uiTitleLabel = self.transform:FindChild("Top/Title"):GetComponent("UILabel")
	self.uiMenuGrid = self.transform:FindChild("Top/ButtonMenu")
	--ui middle
	self.uiNpcGrid = self.transform:FindChild("Middle/Grid")
	--ui bottom
	self.uiDifficultLabel = self.transform:FindChild("Bottom/LabelDifficulty"):GetComponent("UILabel")
	self.uiLeftTimesLabel = self.transform:FindChild("Bottom/LeftTime/Label"):GetComponent("UILabel")
	self.uiLeftTimesButton = self.transform:FindChild("Bottom/LeftTime/Add").gameObject
	self.uiAwardsGrid = self.transform:FindChild("Bottom/Scroll/Grid")
	self.uiStartBtn = self.transform:FindChild("Bottom/ButtonStart").gameObject
	--npc
	self.uiBullFightNpc[1] = self.uiNpcGrid.transform:FindChild("BullFightNPC1")
	self.uiBullFightNpc[2] = self.uiNpcGrid.transform:FindChild("BullFightNPC2")
	self.uiBullFightNpc[3] = self.uiNpcGrid.transform:FindChild("BullFightNPC3")
	self.uiBullFightNpc[4] = self.uiNpcGrid.transform:FindChild("BullFightNPC4")
	--add click
	addOnClick(self.uiLeftTimesButton, self:OnClickTimes())
	addOnClick(self.uiStartBtn, self:OnStartClick())

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIBullFight:Start()
	self.uiBackButton = getLuaComponent(createUI("ButtonBack", self.uiBackGrid))
	self.uiBackButton.onClick = self:OnBackClick()
	self.uiPlayerInfo = getLuaComponent(createUI("PlayerProperty" ,self.uiPlayerInfoGrid))
	self.uiBtnMenu = createUI("ButtonMenu", self.uiMenuGrid)
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self

	self.onNewDayCome = LuaHelper.Action(function () self:Refresh() end)

	MainPlayer.Instance:AddOnMidNightCome(self.onNewDayCome)
	self:Refresh()
end

function UIBullFight:OnDestroy()
	MainPlayer.Instance:RemoveOnMidNightCome(self.onNewDayCome)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIBullFight:Refresh()
	self.npcs = nil
	self.npcs = {}
	self.itemTable = nil
	self.itemTable = {}
	local enum_npc = MainPlayer.Instance.BullFightNpc:GetEnumerator()
	while enum_npc:MoveNext() do
		table.insert(self.npcs, enum_npc.Current)
	end
	self.gameMode = fogs.proto.msg.GameMode.GM_BullFight
	local complete = MainPlayer.Instance.BullFight.complete
	local enumComplete = complete:GetEnumerator()
	-- CommonFunction.ClearGridChild(self.uiNpcGrid.transform)

	-- Config
	local config = GameSystem.Instance.bullFightConfig
	local enum = config.levels:GetEnumerator()
	local index = 1
	while enum:MoveNext() do
		local hard = enum.Current.Key
		local level = enum.Current.Value
		local bullItem = getLuaComponent(self.uiBullFightNpc[index].gameObject) --getLuaComponent(createUI("BullFightNpc", self.uiNpcGrid.transform))
		bullItem.id = self.npcs[index]
		local bullLv = GameSystem.Instance.bullFightConfig:GetFightLevel(hard)
		bullItem.hardLevel = hard
		bullItem.cost = bullLv.win_hp_cost
		bullItem.unLockLevel = bullLv.unlock_level
		enumComplete:MoveNext()
		bullItem.complete = (enumComplete.Current==1)
		bullItem.onClick = self:ClickNpcItem()
		table.insert(self.itemTable, bullItem)
		-- all levels have same cost.
		index = index + 1
		if index > 4 then
			break
		end
	end
	self.uiNpcGrid:GetComponent("UIGrid"):Reposition()


	if self.curIndex == nil then
		local hardLevel = MainPlayer.Instance.BullFightHard
		self:SetCurItem(hardLevel)
	else
		self:SetCurItem(self.curIndex)
	end
	self.allowPlayTimes = GameSystem.Instance.CommonConfig:GetUInt("gBullFightTimes") + MainPlayer.Instance.BullFight.bullfight_buy_times
	self.playedTimes = MainPlayer.Instance.BullFight.times

	self.uiLeftTimesLabel.text = tostring(self.allowPlayTimes - self.playedTimes ).. "/" .. tostring(GameSystem.Instance.CommonConfig:GetUInt("gBullFightTimes"))
	if (self.allowPlayTimes - self.playedTimes ) == 0 then
		NGUITools.SetActive(self.uiLeftTimesButton, true)
		self.uiStartBtn.transform:GetComponent("UIButton").isEnabled = false
	else
		NGUITools.SetActive(self.uiLeftTimesButton, false)
		self.uiStartBtn.transform:GetComponent("UIButton").isEnabled = true
	end


	if self.reStart then
		self:OnStartClick()()
		if self.uiChallenge then

			self.uiChallenge:BullFightProc()()
			self.uiChallenge:OnStartBullFightClick()()
		end
	end

	if self.preSelected and self.preSelected >= 1 and self.preSelected <= #self.uiBullFightNpc then
		self:SetCurItem(self.preSelected)
		self.preSelected  = nil
	end

end

function UIBullFight:OnBackClick()
	return function()
		self:DoClose()
	end
end

function UIBullFight:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIBullFight:OnClose( )
	local btnMenu = getLuaComponent(self.uiBtnMenu)
	btnMenu:SetParent(self.gameObject,true)
	btnMenu.parentScript = self

	if self.onClose then
		self.onClose()
		self.onClose = nil
		return
	end

	-- if MainPlayer.Instance.LinkEnable then
	--	TopPanelManager:ShowPanel(MainPlayer.Instance.LinkUiName)
	--	return
	-- end

	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		-- if self.uiBack then
		--	jumpToUI(self.uiBack)
		--	self.uiBack = nil
		-- else
		--	TopPanelManager:HideTopPanel()
		-- end
		TopPanelManager:HideTopPanel()
	end
end

function UIBullFight:ClickNpcItem()
	return function(item)
		if item.unLockLevel > MainPlayer.Instance.Level then
			CommonFunction.ShowTip(string.format(getCommonStr("STR_CHALLENGE_LEVEL_NOT_ENOUGH_AND_UNLOCK_LEVEL"), item.unLockLevel), nil)
			return
		end
		self:SetCurItem(item.hardLevel)
	end
end

function UIBullFight:SetCurItem(index)
	if self.curIndex then
		NGUITools.SetActive(self.itemTable[self.curIndex].uiIsSelected, false)
	end
	NGUITools.SetActive(self.itemTable[index].uiIsSelected, true)
	self.curIndex = index
	CommonFunction.ClearGridChild(self.uiAwardsGrid.transform)
	rewardId = GameSystem.Instance.bullFightConfig:GetFightLevel(self.itemTable[self.curIndex].hardLevel).reward_id
	self.uiDifficultLabel.text = getCommonStr("DIFFICULTY_" .. self.itemTable[self.curIndex].hardLevel) .. getCommonStr("STR_DIFFICULTY")
	local addedID = {}
	local rewardsList = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(rewardId)
	local enum = rewardsList:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current
		local id = v.award_id
		print("award id :",id)
		if not addedID[id] then
			addedID[id] = true
			local obj = self.uiAwardsGrid.transform:FindChild(id)
			if not obj then
				local g = createUI("GoodsIcon", self.uiAwardsGrid.transform)
				local t = getLuaComponent(g)
				g.gameObject.name = id
				t.goodsID = id
				t.hideLevel = true
				t.hideNeed = true
			end
		end
	end
	self.uiAwardsGrid:GetComponent("UIGrid"):Reposition()
end

function UIBullFight:OnStartClick()
	return function()
		-- Check the Condistion.
		if self.allowPlayTimes <= self.playedTimes then
			if self.showLocalTimer <= 0 then
				CommonFunction.ShowTip(getCommonStr("STR_NO_FREE_TODAY"), nil)
				self.showLocalTimer = 1
			end
			return
		end

		-- if self.hpRequire > MainPlayer.Instance.Hp then
		--	CommonFunction.ShowTip(getCommonStr("CAREER_NOT_ENOUGH_HP"), nil)
		--	return
		-- end

		local Item = self.itemTable[self.curIndex]

		self.curNpc = Item.id
		local matchId = enumToInt(self.gameMode)
		local hard = Item.hardLevel
		local unlock_level = 0
		unlock_level = GameSystem.Instance.bullFightConfig:GetFightLevel(hard).unlock_level

		if unlock_level > MainPlayer.Instance.Level then
			CommonFunction.ShowTip(string.format(getCommonStr("STR_CHALLENGE_LEVEL_NOT_ENOUGH_AND_UNLOCK_LEVEL"), unlock_level), nil)
			return
		end

		local roles ={}

		local gameMode = self.gameMode
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(gameMode)

		local index = 1
		if fightRoleInfoList then
			local enum = fightRoleInfoList:GetEnumerator()
			while enum:MoveNext() do
				roles[index] = enum.Current.role_id
				index = index + 1
			end
		end

		self.uiChallenge = TopPanelManager:ShowPanel('UIChallenge',
								  nil ,
								  {
									  gameType = "BullFight" ,
									  modeType = self.gameMode,
									  roleNum = 1,
									  difficult = hard,
									  npcTable = {
										  self.curNpc
									  },
									  -- nextShowUI = "UIBullFight",
								  }
		)

	end
end

function UIBullFight:OnClickTimes()
	return function()
		--judge vip level
		local vip = self:GetVip()
		-- if vip >= GameSystem.Instance.VipPrivilegeConfig.Vipdatas.Count - 1 then
		--	CommonFunction.ShowPopupMsg(getCommonStr("BUY_TIME_USE_UP"),nil,nil,nil,nil,nil)
		--	return
		-- end
		local curBuyTimes = MainPlayer.Instance.BullFight.bullfight_buy_times
		local timelimit = GameSystem.Instance.VipPrivilegeConfig:GetBullFightBuyTimes(vip)
		if curBuyTimes >= timelimit then
			--tip times limit
			--[[
			local vipUp = self:GetTimesUpVip()
			if vipUp == false then
				CommonFunction.ShowPopupMsg(getCommonStr("BUY_TIME_USE_UP"),nil,nil,nil,nil,nil)
				return
			end
			local timesUp =  GameSystem.Instance.VipPrivilegeConfig:GetBullFightBuyTimes(vipUp)
			local message = string.format(getCommonStr("STR_BULLFIGHT_BUYLIMIT"), vipUp, timesUp)
			CommonFunction.ShowPopupMsg(message,nil,nil,nil,nil,nil)
			--]]
			self:ShowBuyTip("BUY_DIAMOND",true)

		else
			--get consume
			local remainTimes = timelimit - curBuyTimes
			local consume_data = GameSystem.Instance.bullFightConfig:GetBullFightConsume(curBuyTimes + 1)
			local name = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(consume_data.consume_type).name
			local message = string.format(getCommonStr("STR_BULLFIGHT_BUYTIPS"),tostring(consume_data.consume_value),name,remainTimes)
			self.msg = CommonFunction.ShowPopupMsg(message, nil, LuaHelper.VoidDelegate(self:SendReq(consume_data)), LuaHelper.VoidDelegate(self:FramClickClose()),getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL"))
		end
	end
end

function UIBullFight:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end

end

function UIBullFight:SendReq(data)
	return function()
		if data.consume_type == 1 then
		--钻石
		self:ShowBuyTip("BUY_DIAMOND")
		return
	elseif data.consume_type == 2 then
		--金币
		self:ShowBuyTip("BUY_GOLD")
		return
	elseif data.consume_type == 4 then
		--体力
		self:ShowBuyTip("BUY_HP")
		return
	end
		print(self.uiName,": send message buy times")
		local req = {
			acc_id = MainPlayer.Instance.AccountID,
		}
		local buf = protobuf.encode("fogs.proto.msg.BuyBullFightTimesReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.BuyBullFightTimesReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BuyBullFightTimesRespID, self:BuyTimesHandler(), self.uiName)
		CommonFunction.ShowWait()
	end
end
function UIBullFight:BuyTimesHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.BuyBullFightTimesResp", buf)
		CommonFunction.StopWait()
		if resp then
			print(self.uiName,": receive msg buy times")
			if resp.result == 0 then
				MainPlayer.Instance.BullFight.bullfight_buy_times = MainPlayer.Instance.BullFight.bullfight_buy_times + 1
				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error(self.uiName,":", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyBullFightTimesRespID, self.uiName)
	end
end

function UIBullFight:GetVip()
	return MainPlayer.Instance.Vip
end

function UIBullFight:GetTimesUpVip()
	local vip = self:GetVip()
	local times = GameSystem.Instance.VipPrivilegeConfig:GetBullFightBuyTimes(vip)
	local index = vip

	while GameSystem.Instance.VipPrivilegeConfig:GetBullFightBuyTimes(index) <= times do
		index = index + 1
		if index > GameSystem.Instance.VipPrivilegeConfig.maxVip then
		return false
		end
	end
	return index
end

function UIBullFight:ShowBuyTip(type, isVip)
	local str
	if isVip == true then
		str = getCommonStr("STR_VIP_BUY_TIMES")
	else
		if type == "BUY_GOLD" then
			str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
		elseif type == "BUY_DIAMOND" then
			str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
		elseif type == "BUY_HP" then
			str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
		end
	end
	self.msg = CommonFunction.ShowPopupMsg(str, nil,
													LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
													LuaHelper.VoidDelegate(self:FramClickClose()),
													getCommonStr("BUTTON_CONFIRM"),
													getCommonStr("BUTTON_CANCEL"))
end

function UIBullFight:ShowBuyUI(type)
	return function()
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
	end
end

function UIBullFight:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end
end

function UIBullFight:FixedUpdate()
	if self.showLocalTimer > 0 then
		self.showLocalTimer = self.showLocalTimer - UnityTime.fixedDeltaTime
	end
end

return UIBullFight

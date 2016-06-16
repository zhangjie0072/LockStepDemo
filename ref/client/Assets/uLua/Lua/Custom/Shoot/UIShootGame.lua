require "common/StringUtil"

UIShootGame = {
	uiName = "UIShootGame",
	--ui
	uiAwardsTitle,
	uiAwardsTitle,
	uiGoodsTitle,
	uiGoodsTitle,
	uiRemainTimesTitle,
	uiRemainTimesTitle,
	uiReaminTimes,
	uiTabYouMe,
	uiTabYouMeSele,
	uiTabSpeed,
	uiTabSpeedSele,
	uiTabSecond,
	uiTabSecondSele,
	uiItemGrid,
	uiInfoGrid,
	uiButtonBackGrid,
	uiMenuGrid,
	uiPlayerInfoGrid,
	uiAwardsGrid,
	ton,
	uiAdd,
	uiButtonStart,
	uiBackButton,
	uiPlayerInfo,
	uiBtnMenu,
	uiAnimator,

	--parameters
	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	gameMode,
	npcs,
	playedTimes,
	valueTable,
	uiBack,
	reStart = false,
	--variables
	curYDay,
	onClose,
}

function UIShootGame:Awake()
	-- 比赛类型结构体成员映射表
	self.buyTypeTable = {}
	self.buyTypeTable["GM_GrabZone"] = "grab_zone_buy_times"
	self.buyTypeTable["GM_GrabPoint"] = "grab_point_buy_times"
	self.buyTypeTable["GM_MassBall"] = "mass_ball_buy_times"
	self.playeTimesTable = {}
	self.playeTimesTable["GM_GrabZone"] = "grab_zone_times"
	self.playeTimesTable["GM_GrabPoint"] = "grab_point_times"
	self.playeTimesTable["GM_MassBall"] = "mass_ball_times"
	--title
	self.uiAwardsTitle = self.transform:FindChild("Right/Top/Title"):GetComponent("UILabel")
	self.uiAwardsTitle.text = getCommonStr("AWARD_DETAIL")
	self.uiGoodsTitle = self.transform:FindChild("Right/Bottom/Title"):GetComponent("UILabel")
	self.uiGoodsTitle.text = getCommonStr("STR_REWARDED_MAY")
	self.uiRemainTimesTitle = self.transform:FindChild("Right/Bottom/Text"):GetComponent("UILabel")
	self.uiRemainTimesTitle.text = getCommonStr("STR_SHOOT_LEFT_TIMES")
	self.uiReaminTimes = self.transform:FindChild("Right/Bottom/Num"):GetComponent("UILabel")
	--icon
	self.uiTabYouMe = self.transform:FindChild("Left/Grid/Youme")
	self.uiTabYouMeSele = self.transform:FindChild("Left/Grid/Youme/Sele")
	self.uiTabSpeed = self.transform:FindChild("Left/Grid/Speed")
	self.uiTabSpeedSele = self.transform:FindChild("Left/Grid/Speed/Sele")
	self.uiTabSecond = self.transform:FindChild("Left/Grid/Seconds")
	self.uiTabSecondSele = self.transform:FindChild("Left/Grid/Seconds/Sele")
	--grid
	self.uiItemGrid = {}
	self.uiItemGrid[1] = self.transform:FindChild("Left/Grid/1")
	self.uiItemGrid[2] = self.transform:FindChild("Left/Grid/2")
	self.uiItemGrid[3] = self.transform:FindChild("Left/Grid/3")
	self.uiInfoGrid = self.transform:FindChild("Right/Top/TipGrid")
	self.uiButtonBackGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiMenuGrid = self.transform:FindChild("Top/ButtonMenu")
	self.uiPlayerInfoGrid = self.transform:FindChild("Top/PlayerInfo")
	self.uiAwardsGrid = self.transform:FindChild("Right/Bottom/Grid")
	--button
	self.uiAdd = self.transform:FindChild("Right/Bottom/Add").gameObject
	self.uiButtonStart = self.transform:FindChild("Right/Bottom/ButtonOK").gameObject

	self.uiMask = self.transform:FindChild("Mask").gameObject

	addOnClick(self.uiButtonStart, self:ClickStart())
	addOnClick(self.uiAdd, self:OnClickTimes())

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIShootGame:Start()
	self.uiBackButton = getLuaComponent(createUI("ButtonBack", self.uiButtonBackGrid))
	self.uiBackButton.onClick = self:OnBackClick()
	self.uiPlayerInfo = getLuaComponent(createUI("PlayerProperty" ,self.uiPlayerInfoGrid))
	self.uiBtnMenu = createUI("ButtonMenu", self.uiMenuGrid)
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	self.curYDay = os.date("*t", GameSystem.mTime).yday

	LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpenRespID, self:HandleShootOpen(), self.uiName)
end

function UIShootGame:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpenRespID,  self.uiName)
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIShootGame:FixedUpdate()
	local yDay = os.date("*t", GameSystem.mTime).yday
	if yDay ~= self.curYDay then	-- new day come
		self.waitSendReq = true
		self.waitBeginTime = GameSystem.mTime
		NGUITools.SetActive(self.uiMask, true)
		self.curYDay = yDay
	end
	if self.waitSendReq and GameSystem.mTime - self.waitBeginTime > 5 then
		self.waitSendReq = false
		NGUITools.SetActive(self.uiMask, false)
		print(self.uiName, "Send shoot open req")
		UICompetition.SendShootOpenReq()
	end
end

function UIShootGame:Refresh()
	if MainPlayer.Instance.ShootGameModeInfo.Count == 0 then
		UICompetition.SendShootOpenReq()
		return
	end

	local squence = GlobalConst.GShOOTSEQUENCE
	print("squence:",squence)
	local list = Split(squence, "&")
	printTable(list)

	local enum = MainPlayer.Instance.ShootGameModeInfo:GetEnumerator()
	local gameMode = nil
	while enum:MoveNext() do
		-- GameMode
		local gameModeInfo = enum.Current
		gameMode = gameModeInfo.game_mode
		self.gameMode = gameMode
		MainPlayer.Instance.curShootGameMode = gameMode

		-- NPC
		self.npcs = {}
		table.insert(self.npcs, gameModeInfo.npc)
		-- Play times
		self.playedTimes = gameModeInfo.times
		self.valueTable = {}
		self.matchType = gameMode
		print(self.uiName,"--gameMode:",enumToInt(self.gameMode))
	end
	--item refresh
	self:ClearGridItem()
	local initList = self:GetInitList(list)
	for i = 1, 3 do
		local item = getLuaComponent(createUI("ShootItem", self.uiItemGrid[i]))
		item.matchType = fogs.proto.msg.GameMode.IntToEnum(initList[i])
		print("item matchType:",initList[i])
		if i == 2 then
			item.isToday = true
			item.onClick = self:ClickStart()
		elseif i == 3 then
			item.isTomorrow = true
		end
	end
	--awards refresh
	local level = MainPlayer.Instance.Level
	self:DataRefresh(enumToInt(self.gameMode), level)

	if self.reStart == true then self:RestartGame() end
end

function UIShootGame:DataRefresh(game_mode_id, level)
	-- awards info show shoot tip
	CommonFunction.ClearGridChild(self.uiInfoGrid.transform)
	local info = GameSystem.Instance.shootGameConfig:GetShootInfo(game_mode_id, level)
	local score = Split(info.score_level, "&")
	local rewardNum = Split(info.rewards_num, "&")
	for i = 1 , table.getn(score) do
		self:ShowTips(score[i], rewardNum[i])
	end
	self.uiInfoGrid:GetComponent("UIGrid"):Reposition()
	--rewards goods show goods by rewards_id
	CommonFunction.ClearGridChild(self.uiAwardsGrid.transform)
	local rewardsList = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(info.reward_id)

	local awardTable = self:SortList(rewardsList)
	for k,v in ipairs(awardTable) do
		print(self.uiName,"-----k:",k,"------prob:",v)
		local g = createUI("GoodsIcon", self.uiAwardsGrid.transform)
		local t = getLuaComponent(g)
		g.gameObject.name = v
		t.goodsID = v
		t.hideLevel = true
		t.hideNeed = true
	end
	-- end
	self.uiAwardsGrid:GetComponent("UIGrid"):Reposition()
	--remain times show
	self.allowPlayTimes = info.times + MainPlayer.Instance.ShootInfo[self.buyTypeTable[tostring(self.matchType)]]
	--set global
	ShootAllowTimes = self.allowPlayTimes
	-- self.playedTimes = MainPlayer.Instance.ShootInfo[self.playeTimesTable[tostring(self.matchType)]]
	print(self.uiName,"playedTimes:",self.playedTimes,"allowPlayTimes:",self.allowPlayTimes)
	self.uiReaminTimes.text = (self.allowPlayTimes - self.playedTimes) .. "/" ..info.times
	if (self.allowPlayTimes - self.playedTimes) == 0 then
		NGUITools.SetActive(self.uiAdd, true)
		self.uiButtonStart.transform:GetComponent("UIButton").isEnabled = false
	else
		NGUITools.SetActive(self.uiAdd, false)
		self.uiButtonStart.transform:GetComponent("UIButton").isEnabled = true
	end
end

function UIShootGame:ShowTips(score ,rewardNum)
	local tip = getLuaComponent(createUI("ShootTip",self.uiInfoGrid))
	tip.score = score
	tip.num = rewardNum
end

function UIShootGame:ClickStart()
	return function()
		-- Check the Condistion.
		if self.allowPlayTimes <= self.playedTimes then
			CommonFunction.ShowTip(getCommonStr("STR_NO_FREE_TODAY"), nil)
			return
		end

		TopPanelManager:ShowPanel('UIChallenge',
								  nil ,
								  {
									  gameType = "Shoot" ,
									  modeType = self.gameMode,
									  roleNum = 1,
									  difficult = hard,
									  npcTable = self.npcs,
									  -- nextShowUI = "UIShootGame",
								  }
		)

	end
end

function UIShootGame:ClickBuyTimes()
	return function()

	end
end

function UIShootGame:OnBackClick()
	return function()
		self:DoClose()
	end
end

function UIShootGame:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIShootGame:OnClose( )
	local btnMenu = getLuaComponent(self.uiBtnMenu)
	btnMenu:SetParent(self.gameObject,true)
	btnMenu.parentScript = self
	
	if self.onClose then 
		self.onClose()
		self.onClose = nil
		return
	end
	-- if self.nextShowUI then
	-- 	TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
	-- 	self.nextShowUI = nil
	-- else
	-- 	if self.uiBack then
	-- 		jumpToUI(self.uiBack)
	-- 		self.uiBack = nil
	-- 	else
	-- 		TopPanelManager:HideTopPanel()
	-- 	end
	-- end
	TopPanelManager:HideTopPanel()
end

function UIShootGame:ClearGridItem()
	for i = 1, 3 do
		if self.uiItemGrid[i].childCount > 0 then
			NGUITools.Destroy(self.uiItemGrid[i]:GetChild(0).gameObject)
		end
	end
end

function UIShootGame:GetInitList(list)
	local initList = {}
	local index
	for k,v in ipairs(list) do
		print("list value:",v)
		if tonumber(v) == enumToInt(self.matchType) then
			index = k
			break
		end
	end
	if index == 1 then
		initList[1] = tonumber(list[3])
		initList[2] = tonumber(list[1])
		initList[3] = tonumber(list[2])
	elseif index == 2 then
		initList[1] = tonumber(list[1])
		initList[2] = tonumber(list[2])
		initList[3] = tonumber(list[3])
	elseif index == 3 then
		initList[1] = tonumber(list[2])
		initList[2] = tonumber(list[3])
		initList[3] = tonumber(list[1])
	end

	return initList
end

function UIShootGame:OnClickTimes()
	return function()
		--judge vip level
		local vip = self:GetVip()

		-- if vip >= GameSystem.Instance.VipPrivilegeConfig.Vipdatas.Count - 1 then
		--	CommonFunction.ShowPopupMsg(getCommonStr("BUY_TIME_USE_UP"),nil,nil,nil,nil,nil)
		--	return
		-- end
		local curBuyTimes = MainPlayer.Instance.ShootInfo[self.buyTypeTable[tostring(self.matchType)]]
		local timelimit = GameSystem.Instance.VipPrivilegeConfig:GetShootGameBuyTimes(vip)
		print(self.uiName,"curBuyTimes:",curBuyTimes,"timelimit:",timelimit)
		if curBuyTimes >= timelimit then
			--tip times limit

			--[[ 取消vip等级提示
			local vipUp = self:GetTimesUpVip()
			if vipUp == false then
				CommonFunction.ShowPopupMsg(getCommonStr("BUY_TIME_USE_UP"),nil,nil,nil,nil,nil)
				return
			end
			local timesUp =  GameSystem.Instance.VipPrivilegeConfig:GetShootGameBuyTimes(vipUp)
			print(self.uiName,"vipUp:",vipUp,"timesUp:",timesUp)
			local message = string.format(getCommonStr("STR_BULLFIGHT_BUYLIMIT"), vipUp, timesUp)
			CommonFunction.ShowPopupMsg(message,nil,nil,nil,nil,nil)
			--]]
			self:ShowBuyTip("BUY_DIAMOND",true)

		else
			--get consume
			local remainTimes = timelimit - curBuyTimes
			local consume_data = GameSystem.Instance.shootGameConfig:GetShootGameConsume(curBuyTimes + 1)
			local name = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(consume_data.consume_type).name
			local message = string.format(getCommonStr("STR_BULLFIGHT_BUYTIPS"),tostring(consume_data.consume_value),name,remainTimes)
			self.msg = CommonFunction.ShowPopupMsg(message, nil, LuaHelper.VoidDelegate(self:SendReq(consume_data)), LuaHelper.VoidDelegate(self:FramClickClose()),getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL"))
		end
	end
end

function UIShootGame:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end

end

function UIShootGame:SendReq(data)
	return function()
	if data.consume_type == 1 then
		--钻石
		local diamond = MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree
		if diamond < data.consume_value then
			self:ShowBuyTip("BUY_DIAMOND")
			return
		end
	elseif data.consume_type == 2 then
		--金币
		if MainPlayer.Instance.Gold < data.consume_value then
			self:ShowBuyTip("BUY_GOLD")
			return
		end
	elseif data.consume_type == 4 then
		--体力
		self:ShowBuyTip("BUY_HP")
		return
	end
	print(self.uiName,": send message buy times")
		local req = {
			game_mode = self.matchType:ToString(),
		}
		local buf = protobuf.encode("fogs.proto.msg.ShootBuyTimesReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.ShootBuyTimesReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.ShootBuyTimesRespID, self:BuyTimesHandler(), self.uiName)
		CommonFunction.ShowWait()
	end
end
function UIShootGame:BuyTimesHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.ShootBuyTimesResp", buf)
		CommonFunction.StopWait()
		if resp then
			print(self.uiName,": receive msg buy times")
			if resp.result == 0 then
				MainPlayer.Instance.ShootInfo[self.buyTypeTable[tostring(self.matchType)]] =
				MainPlayer.Instance.ShootInfo[self.buyTypeTable[tostring(self.matchType)]] + 1
				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error(self.uiName,":", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootBuyTimesRespID, self.uiName)
	end
end

function UIShootGame:GetVip()
	return MainPlayer.Instance.Vip
end

function UIShootGame:GetTimesUpVip()
	local vip = self:GetVip()
	local times = GameSystem.Instance.VipPrivilegeConfig:GetShootGameBuyTimes(vip)
	local index = vip
	while GameSystem.Instance.VipPrivilegeConfig:GetShootGameBuyTimes(index) <= times do
		index = index + 1
		if index > GameSystem.Instance.VipPrivilegeConfig.maxVip then
			return false
		end
	end
	return index
end

function UIShootGame:RestartGame()
	local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.matchType)
	local enum = fightRoleInfoList:GetEnumerator()
	local i = 1
	local list = {}
	while enum:MoveNext() do
		list[i] = {}
		list[i].role_id = enum.Current.role_id
		list[i].status = enum.Current.status:ToString()
		i = i + 1
	end
	local fightList = {}
	fightList = {
				game_mode = self.matchType:ToString(),
				fighters = {
					{role_id = list[1].role_id,	status = "FS_MAIN",},
				}
			}
	local startShoot = {
		fight_list = fightList,
		game_mode = self.matchType:ToString(),
	}
	local enterGame = {
		acc_id = MainPlayer.Instance.AccountID,
		type = 'MT_SHOOT',
		shoot = startShoot,
		game_mode = self.matchType:ToString(),
	}

	print("enterGame.game_mode=",enterGame.game_mode)
	local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
	LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
	LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:StartEnterShootGameHandler(fightList.fighters[1].role_id), self.uiName)
	CommonFunction.ShowWaitMask()
	CommonFunction.ShowWait()
end

function UIShootGame:StartEnterShootGameHandler(roleId)
	return function(buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				local sessionId = resp.shoot.session_id
				print("sessionId=",sessionId)
				print("roleId=",roleId)
				self:StartShootGame(sessionId, roleId, self.npcs[1])
				-- self:SectionStart(resp.career.session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), nil)
			end
		else
			error("UIShoot match -handler", err)
		end
	end
end

function UIShootGame:StartShootGame(sessionId, roleId, npcId)
	print("sessionId=",sessionId)
	print("roleId=",roleId)
	print("npcId=",npcId)
	print("self.modeType=",self.matchType)
	print("enumToInt(self.modeType)=",enumToInt(self.matchType))
	CurNPC = npcId
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	local match = GameSystem.Instance.shootGameConfig:GetShootInfo(enumToInt(self.matchType), MainPlayer.Instance.Level)
	GameSystem.Instance.mClient:CreateShootMatch(sessionId, self.matchType,roleId, npcId, match.game_mode_id, enumToInt(self.matchType))
end

function UIShootGame:HandleShootOpen()
	return function(buf)
		print(self.uiName, "Handle shoot open resp")
		if UICompetition.HandleShootOpenResp(buf) then
			self:Refresh()
		end
	end
end

function UIShootGame:ShowBuyTip(type, isVip)
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

function UIShootGame:ShowBuyUI(type)
	return function()
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
	end
end

function UIShootGame:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end
end

--返回权重最高1 2 最低
function UIShootGame:SortList(list)
	local enum = list:GetEnumerator()
	local awardTable = {}
	local temp = 0
	while enum:MoveNext() do
		local x = enum.Current.award_prob - temp
		table.insert(awardTable,{id = enum.Current.award_id, prob = x})
		temp = enum.Current.award_prob
	end

	table.sort(awardTable, function(goods1, goods2)
		if goods1.prob > goods2.prob then
			return true
		else
			return false
		end
	end)
	local result = {}
	table.insert(result, awardTable[1].id)
	table.insert(result, awardTable[2].id)
	table.insert(result, awardTable[table.getn(awardTable)].id)
	return result
end

return UIShootGame

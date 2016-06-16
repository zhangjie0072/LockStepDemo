--encoding=utf-8

UIQualifying = {
	uiName = 'UIQualifying',

	--------------------parameters
	remain_time,
	cd,

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	openStore = false,
	banTwice = false,
	msg,
	fightNum,
	------------------UI
	uiBtnMenu,
	uiPlayerProperty,

	uiRivalGrid,
	uiTeamGrid,
	uiButtonExchange,
	uiButtonTeam,
	uiButtonRule,
	uiButtonRecord,
	uiButtonRewards,
	uiButtonRankList,
	uiMyRank,
	uiProperty,
	uiRemainTime,
	uiCdLabel,
	uiCdRefresh,
	uiCdButton,
	uiCdCost,
	uiAnimator,
	uiBuyBtn,
	uiFightNum,
}


-----------------------------------------------------------------
function UIQualifying:Awake()
	--PlayerProperty显示
	self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
	self.uiPlayerProperty = createUI('PlayerProperty', self.transform:FindChild("Top/PlayerInfoGrids"))
	self.uiRivalGrid = self.transform:FindChild("RivalPane/Grid")
	self.uiTeamGrid = self.transform:FindChild("BackBottom/Grid"):GetComponent("UIGrid")
	self.uiButtonExchange = self.transform:FindChild("Left/Refresh/Exchange").gameObject
	--self.uiButtonTeam = self.transform:FindChild("Left/MyTeam/ButtonOK").gameObject
	self.uiButtonRule = self.transform:FindChild("RivalPane/FuncBar/Rule").gameObject
	self.uiButtonRecord = self.transform:FindChild("RivalPane/FuncBar/Record").gameObject
	self.uiButtonRewards = self.transform:FindChild("RivalPane/FuncBar/Rewards").gameObject
	self.uiButtonBackGrid =	self.transform:FindChild("Top/ButtonBack")
	--self.uiButtonRankList = self.transform:FindChild("RivalPane/FuncBar/RankingList").gameObject
	---------label
	self.uiTitile = self.transform:FindChild("Top/Title"):GetComponent("UILabel")
	self.uiTitile.text = getCommonStr("STR_LABBER")
	-- self.uiTeamLabel = self.transform:FindChild("Left/MyTeam"):GetComponent("UILabel")
	-- self.uiTeamLabel.text = getCommonStr("QUALIFYING_DEFEND")
	-- self.uiTeamBtnLabel =self.transform:FindChild("Left/MyTeam/ButtonOK/Text"):GetComponent("MultiLabel")
	-- self.uiTeamBtnLabel:SetText( getCommonStr("STR_TEAM_FIGHT"))
	self.uiExchangeLabel = self.transform:FindChild("Left/Refresh/Exchange/Text"):GetComponent("MultiLabel")
	self.uiExchangeLabel:SetText( getCommonStr("STR_TEAM_EXCHANGE"))
	self.uiRuleLabel = self.transform:FindChild("RivalPane/FuncBar/Rule/Text"):GetComponent("MultiLabel")
	self.uiRuleLabel:SetText( getCommonStr("STR_RULE_EXPLAIN"))
	self.uiRewardsLabel = self.transform:FindChild("RivalPane/FuncBar/Rewards/Text"):GetComponent("MultiLabel")
	self.uiRewardsLabel:SetText( getCommonStr("STR_AWARDS_EXCHANGE"))
	self.uiRecordLabel = self.transform:FindChild("RivalPane/FuncBar/Record/Text"):GetComponent("MultiLabel")
	self.uiRecordLabel:SetText( getCommonStr("QUALIFYING_RECORDS"))
	self.uiMyRankLabel = self.transform:FindChild("RivalPane/FuncBar/MyRankLabel"):GetComponent("UILabel")
	self.uiMyRankLabel.text = getCommonStr("STR_MY_RANK")
	----------------
	self.uiMyRank = self.transform:FindChild("RivalPane/FuncBar/MyRankLabel/MyRank"):GetComponent("UILabel")
	self.uiRemainTime = self.transform:FindChild("Left/Refresh/RemainTime")
	self.uiCdLabel = self.uiRemainTime:GetComponent("UILabel")
	self.uiCdRefresh = self.transform:FindChild("Left/Refresh/Refresh")
	self.uiCdButton = self.transform:FindChild("Left/Refresh/Refresh/Refresh")
	self.uiCdCost = getChildGameObject(self.transform,"Left/Refresh/Refresh/GoodsIconConsume")
	self.uiChallengeTimes = getComponentInChild(self.transform,"Left/Refresh/Time/Num","UILabel")
	self.uiFightNum = self.transform:FindChild("BackBottom/FightingForce/FightNum")

	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiBuyBtn = self.transform:FindChild("Left/Refresh/Time/Add"):GetComponent("UIButton")


	addOnClick(self.uiButtonExchange,self:MakeOnRefresh())
	--addOnClick(self.uiButtonTeam,self:MakeOnDefendRole())
	addOnClick(self.uiButtonRule,self:MakeOnRule())
	addOnClick(self.uiButtonRecord,self:MakeRecord())
	addOnClick(self.uiButtonRewards,self:MakeOnRewards())
	--addOnClick(self.uiButtonRankList,self:MakeOnRankList())
	addOnClick(self.uiCdButton.gameObject,self:TimeRefresh())
	addOnClick(self.uiBuyBtn.gameObject, self:ClickBuy())
	-----初始化
	--排行
	local  curRank = MainPlayer.Instance.QualifyingRanking
	if curRank == 0 then
		self.uiMyRank.text = GameSystem.Instance.CommonConfig:GetUInt("QualifyingMaxRank")..getCommonStr("OVER_MAXRANK")
	else
		self.uiMyRank.text = curRank
	end
	--冷却时间
	self.diff = os.time() - GameSystem.mTime
	self.last_time = MainPlayer.Instance.QualifyingInfo.battle_time
	--挑战次数
	self:UpdateFightTimes()
	--消息注册
	LuaHelper.RegisterPlatMsgHandler(MsgID.FlushRivalRespID, self:RivalRefresh(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.SetDefendRespID, self:DefendRoleRefreshHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.ResetBattleCDRespID, self:MakeCDHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.GetBattleNewsRespID, self:MakeRecordHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.QualifyingRankingNotifyID, self:RefreshRank(), self.uiName)

	self:SendFlushRivalReq()

	self.fightNum = getLuaComponent(createUI("FightNum", self.uiFightNum))

end

function UIQualifying:Start()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self

	local property = getLuaComponent(self.uiPlayerProperty)
	property.showHonor = true

	-- local goodsIcon = getLuaComponent(self.uiCdCost)
	-- goodsIcon.isAdd = false
	-- goodsIcon.rewardNum = 2
	self.uibtnBack = getLuaComponent(createUI("ButtonBack",self.uiButtonBackGrid))
	self.uibtnBack.onClick = self:MakeOnBack()

	self.actionOnReconn = LuaHelper.Action(self:MakeOnReconn())

	PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionOnReconn
end

function UIQualifying:MakeOnReconn()
	return function ()
		print(self.uiName, "OnReconn")
		self:SendFlushRivalReq()
	end
end

function UIQualifying:FixedUpdate()
	local now_time = os.time()
	self.remain_time = now_time - self.diff - self.last_time
	if self.remain_time < 1 and MainPlayer.Instance.QualifyingInfo.run_times < 5 and self.last_time ~= 0 then
		self.cd = 1 - self.remain_time
		NGUITools.SetActive(self.uiCdCost,true)
		NGUITools.SetActive(self.uiCdRefresh.gameObject,true)
		self.uiCdLabel.text = getCommonStr("REMAIN_TIME")..":"..self:GetTimeLabel(self.cd)
	else
		NGUITools.SetActive(self.uiRemainTime.gameObject,false)
		NGUITools.SetActive(self.uiCdRefresh.gameObject,false)
	end
end

function UIQualifying:OnClose()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, true)

	if self.onClose then 
		self.onClose()
		self.onClose = nil
		return
	end

	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	elseif self.openStore == true then
		UIStore:SetType('ST_HONOR')
		UIStore:SetBackUI(self.uiName)
		UIStore:OpenStore()
		self.openStore = false
	else
		jumpToUI("UIHall")
	end
end

function UIQualifying:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIQualifying:OnDestroy()
	PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionOnReconn

	--取消消息注册
	--LuaHelper.UnregisterMsgHandler(MsgID.TourResetRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.FlushRivalRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.SetDefendRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ResetBattleCDRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBattleNewsRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.QualifyingRankingNotifyID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIQualifying:Refresh()
	--防守阵容
	self.defend_list = {}
	-- local  defendRoleInfoList = MainPlayer.Instance.QualifyingInfo.role
	-- print(MainPlayer.Instance.QualifyingInfo.role)
	-- if defendRoleInfoList.Count ~= 0 then
	--	local i = 1
	--	local enum = defendRoleInfoList:GetEnumerator()
	--	while enum:MoveNext() do
	--		-- self.defend_list[i] = {}
	--		self.defend_list[ enum.Current.id] = i
	--		i = i + 1
	--	end
	-- end
	local i = 1
	local defendRoleInfoList = MainPlayer.Instance.SquadInfo
	local enum = defendRoleInfoList:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.status == FightStatus.FS_MAIN then
			self.defend_list[enum.Current.role_id] = 1
		elseif enum.Current.status == FightStatus.FS_ASSIST1 then
			self.defend_list[enum.Current.role_id] = 2
		elseif enum.Current.status == FightStatus.FS_ASSIST2 then
			self.defend_list[enum.Current.role_id] = 3
		end
	end
	--local grid = self.uiRivalGrid
	-- for i = 0 , (grid.childCount - 1) do
	--	--getLuaComponent(grid:GetChild(i).gameObject):Refresh()
	--	-- NGUITools.SetActive(grid:GetChild(i).gameObject, false)
	--	-- NGUITools.SetActive(grid:GetChild(i).gameObject, true)
	-- end
	-- NGUITools.SetActiveChildren(self.uiRivalGrid.gameObject,false)
	-- NGUITools.SetActiveChildren(self.uiRivalGrid.gameObject,true)
	--NGUITools.MakePixelPerfect(self.uiRivalGrid)
	if self.RivalInfo then
		self:InitRival()
	end
	self:DefendRoleRefresh()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
	self.fightNum:SetNum(GetTeamFight())
end

-----------------------------------------------------------------
function UIQualifying:DefendRoleRefresh()
	CommonFunction.ClearGridChild(self.uiTeamGrid.transform)
	for id, index  in pairs(self.defend_list) do
		local icon = getLuaComponent(createUI("CareerRoleIcon",self.uiTeamGrid.transform))
		icon.id = id
		icon.gameObject.name = index
	end
	self.uiTeamGrid:Reposition()
end

function UIQualifying:DefendRoleRefreshHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.SetDefendResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				local role = MainPlayer.Instance.QualifyingInfo.role
				role:Clear()
				for i = 1, 3 do
					local info = RoleInfo.New()
					info.id = resp.fight_list.fighters[i].role_id
					role:Add(info)
					i = i + 1
				end
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
			self:Refresh()
		else
			error("UIQualifying:", err)
		end
	end
end

function UIQualifying:GetTimeLabel(cd)
	local minutes = math.floor(cd / 60)
	local seconds = math.floor(cd % 60)
	return string.format("%02d:%02d", minutes, seconds)
end

function UIQualifying:MakeOnRewards()
	return function (go)
		if validateFunc('UIStore') then
			self.openStore = true
			self:DoClose()
		end
	end
end

function UIQualifying:MakeOnRankList( ... )
	return function (go)
		TopPanelManager:ShowPanel('UIRankList')
	end
end

function UIQualifying:TimeRefresh()
	return function ()
	--·冷却时间刷新
		local req = {
		}
		local buf = protobuf.encode("fogs.proto.msg.ResetBattleCDReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.ResetBattleCDReqID, buf)
		CommonFunction.ShowWait();

	end
end


function UIQualifying:ClickBuy()
	return function()
		local vip = MainPlayer.Instance.Vip
		local curBuyTimes = MainPlayer.Instance.QualifyingInfo.buy_times
		local timelimit = GameSystem.Instance.VipPrivilegeConfig:GetQualifyingBuyTimes(vip)

		if curBuyTimes >= timelimit then
			local vipUp = self:GetTimesUpVip()
			if vipUp == false then
				CommonFunction.ShowPopupMsg(getCommonStr("BUY_TIME_USE_UP"),nil,nil,nil,nil,nil)
				return
			end
			-- local timesUp =  GameSystem.Instance.VipPrivilegeConfig:GetQualifyingBuyTimes(vipUp)
			-- local message = string.format(getCommonStr("STR_BULLFIGHT_BUYLIMIT"), vipUp, timesUp)
			-- CommonFunction.ShowPopupMsg(message,nil,nil,nil,nil,nil)
			self:ShowBuyTip("STR_VIP_BUY_TIMES",true)
		else
			--get consume
			local remainTimes = timelimit - curBuyTimes
			local consume_data = GameSystem.Instance.qualifyingConfig:GetQualifyingConsume(curBuyTimes + 1)
			local name = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(consume_data.consume_type).name
			local message = string.format(getCommonStr("STR_BULLFIGHT_BUYTIPS"),tostring(consume_data.consume_value),name,remainTimes)
			self.msg = CommonFunction.ShowPopupMsg(message, nil, LuaHelper.VoidDelegate(self:SendReq(consume_data)), LuaHelper.VoidDelegate(self:FramClickClose()),getCommonStr("BUTTON_CONFIRM"), getCommonStr("BUTTON_CANCEL"))
		end
	end
end

function UIQualifying:GetTimesUpVip()
	local vip = MainPlayer.Instance.Vip
	local times = GameSystem.Instance.VipPrivilegeConfig:GetQualifyingBuyTimes(vip)
	local index = vip

	while GameSystem.Instance.VipPrivilegeConfig:GetQualifyingBuyTimes(index) <= times do
		index = index + 1
		if index > GameSystem.Instance.VipPrivilegeConfig.maxVip then
			return false
		end
	end
	return index
end


function UIQualifying:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end
end


function UIQualifying:SendReq(data)
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
			-- acc_id = MainPlayer.Instance.AccountID,
		}
		local buf = protobuf.encode("fogs.proto.msg.QualifyingBuyTimesReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.QualifyingBuyTimesReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.QualifyingBuyTimesRespID, self:BuyTimesHandler(), self.uiName)
	end
end


function UIQualifying:BuyTimesHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.QualifyingBuyTimesResp", buf)
		if resp then
			print(self.uiName,": receive msg buy times resp.times=", resp.times)
			if resp.result == 0 then
				MainPlayer.Instance.QualifyingInfo.buy_times = resp.times
				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error(self.uiName,":", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.QualifyingBuyTimesRespID, self.uiName)
		self:UpdateFightTimes()
	end
end

function UIQualifying:MakeCDHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.ResetBattleCDResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance.QualifyingInfo.battle_time = 0
				self.last_time = MainPlayer.Instance.QualifyingInfo.battle_time
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UIQualifying:", err)
		end
	end
end

function UIQualifying:RivalRefresh()
	return function (buf)
		--对手刷新
		local resp, err = protobuf.decode("fogs.proto.msg.FlushRivalResp", buf)
		CommonFunction.StopWait()
		print("---------flushresult:",resp)
		if resp then
			if resp.result == 0 then
				self.RivalInfo= {}
				for i = 1, 3 do
					self.RivalInfo[i] = {}
					self.RivalInfo[i]["name"] = resp.info[i].name
					self.RivalInfo[i]["ranking"] = resp.info[i].ranking
					self.RivalInfo[i]["show_id"] = resp.info[i].show_id
					self.RivalInfo[i]["player_type"] = resp.info[i].player_type
					self.RivalInfo[i]["level"] = resp.info[i].level
					self.RivalInfo[i]["fightPower"] = resp.info[i].fight_power
					self.RivalInfo[i]["simple_info"] = {}
					for j = 1, 3 do
						self.RivalInfo[i]["simple_info"][j] = {}
						self.RivalInfo[i]["simple_info"][j].lev = resp.info[i].role_info[j].level
						self.RivalInfo[i]["simple_info"][j].id = resp.info[i].role_info[j].id
						self.RivalInfo[i]["simple_info"][j].star = resp.info[i].role_info[j].star
						self.RivalInfo[i]["simple_info"][j].quality = resp.info[i].role_info[j].quality
						--printTable(self.RivalInfo[i]["simple_info"][j])
					end
					-- print(self.uiName,"-----fight power:",self.RivalInfo[i]["fightPower"])
				end
				self:InitRival()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UIQualifying:", err)
		end
	end
end

function UIQualifying:InitRival()
	CommonFunction.ClearGridChild(self.uiRivalGrid.transform)
	for i = 1, 3 do
		local RivalCard = getLuaComponent(createUI("RivalCard",self.uiRivalGrid))
		RivalCard.name = self.RivalInfo[i]["name"]
		RivalCard.ranking_num = self.RivalInfo[i]["ranking"]
		RivalCard.show_id = self.RivalInfo[i]["show_id"]
		RivalCard.player_type = self.RivalInfo[i]["player_type"]
		RivalCard.level = self.RivalInfo[i]["level"]
		RivalCard.fightPower = self.RivalInfo[i]["fightPower"]
		RivalCard.simple_info = {}
		for j = 1, 3 do
			RivalCard.simple_info[j] = {}
			-- print("RivalInfo_lev:",self.RivalInfo[i]["simple_info"][j].lev)
			RivalCard.simple_info[j].lev = self.RivalInfo[i]["simple_info"][j].lev
			RivalCard.simple_info[j].id = self.RivalInfo[i]["simple_info"][j].id
			RivalCard.simple_info[j].star = self.RivalInfo[i]["simple_info"][j].star
			RivalCard.simple_info[j].quality = self.RivalInfo[i]["simple_info"][j].quality
		end
		RivalCard.onChallenge = self:MakeOnChallenge()
	end
	local grid = self.uiRivalGrid:GetComponent("UIGrid")
	grid:Reposition()
end

function UIQualifying:MakeOnChallenge()
	--·挑战流程
	return function(RivalCard)
		print("---------card:",RivalCard)
		--挑战次数判断
		if MainPlayer.Instance.QualifyingInfo.run_times - MainPlayer.Instance.QualifyingInfo.buy_times >= GlobalConst.QUALIFYING_TIMES then
			--ìáê?′?êy2?1?
			CommonFunction.ShowPopupMsg(getCommonStr("NOT_ENOUGH_TIMES"),nil,nil,nil,nil,nil)
			return
		end
		--冷却时间判断
		if self.remain_time < 1 and self.last_time ~= 0 then
			CommonFunction.ShowPopupMsg(getCommonStr("QUALIFYING_TIME_CD"),nil,nil,nil,nil,nil)
			return
		end
		------judge fighters
		local _,x = self:GetFighters()
		if x < 3 then
			CommonFunction.ShowPopupMsg(getCommonStr("NOT_ENOUGH_ROLES"),nil,nil,nil,nil,nil)
			return
		end
		--fill fighters
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(GameMode.GM_3On3)
		if not fightRoleInfoList then
			fightRoleInfoList = FightRoleInfoList.New()
			MainPlayer.Instance.FightRoleList:Add(GameMode.GM_3On3, fightRoleInfoList)
		end
		self:QualifyingFillFighters(fightRoleInfoList, self:GetFighters())

		--send msg
		local QualifyingMatchReq = {
			type = 'MT_QUALIFYING',
			fight_list = {
				game_mode = GameMode.GM_3On3:ToString(),
				fighters = self:GetFighters()
				},
			rival_ranking = RivalCard.ranking_num,
			name = RivalCard.name,
		}

		local Req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_QUALIFYING',
			qualifying = QualifyingMatchReq,
			game_mode = GameMode.GM_3On3:ToString(),
		}
		local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", Req)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID, buf)
	end
end

function UIQualifying:GetFighters()
	local count = 0
	local fighters = {}
	for id ,index in pairs( self.defend_list) do
		fighters[ index] = {}
		fighters[ index].role_id = id
		count = count + 1
		if index == 1 then
			fighters[ index].status = "FS_MAIN"
		elseif index == 2 then
			fighters[ index].status = "FS_ASSIST1"
		elseif index == 3 then
			fighters[ index].status = "FS_ASSIST2"
		end
	end
	return fighters , count
end

function UIQualifying:QualifyingFillFighters( list , fighters)
	list:Clear()
	local info = FightRole.New()
	info.role_id = fighters[1].role_id
	info.status = FightStatus.FS_MAIN
	list:Add(info)
	local info = FightRole.New()
	info.role_id = fighters[2].role_id
	info.status = FightStatus.FS_ASSIST1
	list:Add(info)
	local info = FightRole.New()
	info.role_id = fighters[3].role_id
	info.status = FightStatus.FS_ASSIST2
	list:Add(info)
end

function UIQualifying:SendFlushRivalReq()
	--·发送刷新消息
	local req = {
	acc_id = MainPlayer.Instance.AccountID
	}
	local buf = protobuf.encode("fogs.proto.msg.FlushRivalReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.FlushRivalReqID, buf)
	CommonFunction.ShowWait()
end

function UIQualifying:MakeOnRefresh()
	return function()
		--删除已创建
		CommonFunction.ClearGridChild(self.uiRivalGrid)
		self:SendFlushRivalReq()
	end
end

function UIQualifying:MakeOnDefendRole()
	return function ()
		--防守阵容设置
		--TopPanelManager:ShowPanel('UIMember',nil ,{isGoBack = true, backClick = self:MakeOnDefendSelected(), showlist = self.defend_list,})
		TopPanelManager:ShowPanel('UISquad')
	end
end

function UIQualifying:MakeOnDefendSelected()
	return function ()
		--·防守阵容确定
		local SetDefendReq = {
			fight_list = {
					game_mode = "GM_3On3",
					fighters = self:GetFighters() ,
				}
		}

		local buf = protobuf.encode("fogs.proto.msg.SetDefendReq", SetDefendReq)
		LuaHelper.SendPlatMsgFromLua(MsgID.SetDefendReqID, buf)
		CommonFunction.ShowWait()

	end
end

--进攻阵容确定进入比赛
--[[
function UIQualifying:MakeOnRoleSelected(RivalCard)
	return function ()
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(GameMode.GM_3On3)
		if not fightRoleInfoList then
			fightRoleInfoList = FightRoleInfoList.New()
			MainPlayer.Instance.FightRoleList:Add(GameMode.GM_3On3, fightRoleInfoList)
		end
		self.UIQualifySelect_fight:FillFightRoleInfoList(fightRoleInfoList)

		local QualifyingMatchReq = {
			type = 'MT_QUALIFYING',
			fight_list = self.UIQualifySelect_fight:GetFightRoleInfoListTable(GameMode.GM_3On3),
			rival_ranking = RivalCard.ranking_num,
			name = RivalCard.name,
		}
		local Req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_QUALIFYING',
			qualifying = QualifyingMatchReq,
			game_mode = GameMode.GM_3On3:ToString(),
		}
		local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", Req)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID, buf)

	end
end
--]]

function UIQualifying:MakeOnRule()
	return function ()
		if self.banTwice then
			return
		end
		--??ê?1??ò
		local qualifying = MainPlayer.Instance.QualifyingInfo
		local cur_rank = MainPlayer.Instance.QualifyingRanking
		local rule = getLuaComponent(createUI("QualifyingRulePopup",self.transform))
		UIManager.Instance:BringPanelForward(rule.gameObject)
		rule.cur_rank = cur_rank
		--rule.cur_rank = 4
		rule.max_rank = qualifying.max_ranking
		rule.onClose = function ( ... )
			self.banTwice = false
		end
		print("cur_rank:",cur_rank,"max_ranking:",max_ranking)
		if rule.cur_rank ~= 0 then
			local data = GameSystem.Instance.qualifyingConfig:GetAwardsData(cur_rank)
			--local data = GameSystem.Instance.qualifyingConfig:GetAwardsData(4)
			rule.rank_min = data.rank_min
			rule.rank_max = data.rank_max
			print(data.databyid)
			local temp = {}
			local enum = data.databyid:GetEnumerator()
			local i =1
			while enum:MoveNext() do
				rule.include = true
				rule.rewardTable[i] = {}
				rule.rewardTable[i].id = enum.Current.id
				rule.rewardTable[i].value = enum.Current.value
				i = i + 1
			end
		end
		self.banTwice = true
	end
end

function UIQualifying:MakeRecord()
	return function ()
		--发送记录 消息

		local req = {
			acc_id = MainPlayer.Instance.AccountID
		}
		local buf = protobuf.encode("fogs.proto.msg.GetBattleNews", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetBattleNewsID, buf)
		CommonFunction.ShowWait()
	end
end

function UIQualifying:MakeRecordHandler()
	return function (buf)
	    CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		local resp, err = protobuf.decode("fogs.proto.msg.GetBattleNewsResp", buf)
		if resp then
			if resp.result == 0 then
				local outs = getLuaComponent(createUI("QualifyingOutsPopup",self.transform))
				outs.onClose = function ( ... )
					self.banTwice = false
				end
				UIManager.Instance:BringPanelForward(outs.gameObject)
				local battleInfo = resp.info
				for i,v in ipairs(battleInfo) do
					outs.battle[i] = {}
						outs.battle[i].time = v.time
						outs.battle[i].name = v.name
						outs.battle[i].ranking = v.ranking
						outs.battle[i].state = v.state
				end
				self.banTwice = true
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UIQualifying:", err)
		end
	end
end

function UIQualifying:MakeOnBack()
	return function ()
		self:DoClose()
	end
end

function UIQualifying:SetModelActive(active)
	-- body
end


function UIQualifying:UpdateFightTimes()
	local total = GlobalConst.QUALIFYING_TIMES
	local left = total - MainPlayer.Instance.QualifyingInfo.run_times + MainPlayer.Instance.QualifyingInfo.buy_times
	self.uiChallengeTimes.text = left.."/"..total
	-- self.uiBuyBtn.gameObject:SetActive(left <= 0)
end

function UIQualifying:RefreshRank()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.QualifyingRankingNotify", buf)
		print(self.uiName,"get QualifyingRankingNotify cur_rank:",resp.cur_ranking)
		if resp then
			self.uiMyRank.text = resp.cur_ranking
		else
			error("UIQualifying:", err)
		end
	end
end

function UIQualifying:ShowBuyTip(type)
	local str
	if type == "BUY_GOLD" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
	elseif type == "BUY_DIAMOND" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
	elseif type == "BUY_HP" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
	elseif type == "STR_VIP_BUY_TIMES" then
		str = getCommonStr("STR_VIP_BUY_TIMES")
		type = "BUY_DIAMOND"
	end
	self.msg = CommonFunction.ShowPopupMsg(str, nil, 
													LuaHelper.VoidDelegate(self:ShowBuyUI(type)), 
													LuaHelper.VoidDelegate(self:FramClickClose()),
													getCommonStr("BUTTON_CONFIRM"), 
													getCommonStr("BUTTON_CANCEL"))				
end

function UIQualifying:ShowBuyUI(type)
	return function()
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
		self.banTwice = false
	end
end

function UIQualifying:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.banTwice = false
	end
end

return UIQualifying

--encoding=utf-8

PvpTip = PvpTip or
{
	btnMenu,
	uiName = 'PvpTip',

	beginTime = nil,
	matchBeginWaitTime = nil,

	data = nil,
	averTime = nil,
	onCancel,
	onTop,
	onDown,
	uiMask,
	-- uiMask = nil,
	--current_match_type,
	--------------------UI
	uiWaitAverTime,
	uiArrow,
	uiArrow1,
	uiAimator = nil,

	uiChallenge = nil
}

local match_type_1on1_plus = 'MT_PVP_1V1_PLUS'
local match_type_3on3 = 'MT_PVP_3V3'

function PvpTip:Awake( ... )

	local tipLabel = self.transform:FindChild('labelTip'):GetComponent('UILabel')
	self.uiWaitAverTime = self.transform:FindChild('WaitedTime'):GetComponent('UILabel')
	self.uiArrow = self.transform:FindChild('Arrow'):GetComponent('UISprite')
	self.uiArrow1 = self.transform:FindChild('SmallTip/Arrow2'):GetComponent('UISprite')
	self.waitTimeLabel = self.transform:FindChild('labelWaitTime'):GetComponent('UILabel')
	self.waitTimeLabel.text = '0'
	self.uiMask = self.transform:FindChild('Mask')

	-- local PvpBeginTime
	--global set
	if not PvpShow then
		self.beginTime = os.time()
		PvpBeginTime = self.beginTime
		PvpShow = true
	else
		self.beginTime = PvpBeginTime
	end

	tipLabel.text = getCommonStr('MATCH_TIP')
	local cancel = self.transform:FindChild('buttonCancel/ButtonOK1/Label'):GetComponent('UILabel')
	cancel.text = getCommonStr('STR_CLOSE_MATCH')
	local cancelBtn = self.transform:FindChild('buttonCancel/ButtonOK1'):GetComponent('UIButton')
	addOnClick(cancelBtn.gameObject, self:OnCancelClick())
	self.uiAimator = self.transform:GetComponent('Animator')

	LuaHelper.RegisterPlatMsgHandler(MsgID.NotifyGameStartID, self:OnNotifyGameStart(), self.uiName)
	self.notifyGameStart = nil
	self.matchBeginWaitTime = nil
end

function PvpTip:Start( ... )
	print("---msg register notifygame start")
	addOnClick(self.uiArrow.gameObject, self:ChangeTipState())
	addOnClick(self.uiArrow1.gameObject, self:ChangeTipState())
	local timeLabel = self.transform:FindChild('labelTime'):GetComponent('UILabel')
	timeLabel.text = getCommonStr('MATCH_TIME')
	self.diff = os.time() - GameSystem.mTime
	if self.averTime then
		self.uiWaitAverTime.text = os.date('%M:%S', self.averTime)
	end

	self.actionOnDisconn = LuaHelper.Action(self:MakeOnDisconn())

	PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected + self.actionOnDisconn
end

function PvpTip:FixedUpdate( ... )
	if self.beginTime then
		local waitTime = tonumber(os.time()) - tonumber(self.beginTime)
		self.waitTimeLabel.text = tostring(waitTime) .. '秒'
	end
	if self.matchBeginWaitTime and self.data then
		local matchWaitTime = tonumber(os.time()) - tonumber(self.matchBeginWaitTime)
		self.popup:SetMessage(getCommonStr('MATCH_JOIN_GAME') .. tostring(10 - matchWaitTime))

		if matchWaitTime >= 10 then
			self:ForceEnterGame(self.data)
			self.beginTime = nil
			self.matchBeginWaitTime = nil
			PvpShow = false
		end
	end
end

function PvpTip:Refresh( ... )

end

function PvpTip:OnDestroy( ... )
	-- body
	print(self.uiName, "OnDestroy")
	--LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.AsynEnterRoomRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
	-- PvpShow = nil
	--LuaHelper.UnRegisterPlatMsgHandler(MsgID.RankListRespID, self.uiName)

	-- PvpShow = false
	PlatNetwork.Instance.onDisconnected = PlatNetwork.Instance.onDisconnected - self.actionOnDisconn
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function PvpTip:OnClose( ... )
	NGUITools.Destroy(self.gameObject)
end

function  PvpTip:OnNotifyGameStart( ... )
	return function (buf)
		self.popup = CommonFunction.ShowPopupMsg(getCommonStr('MATCH_JOIN_GAME'), nil, LuaHelper.VoidDelegate(self:OnOkStartMatch(buf)), 
			nil, getCommonStr("BUTTON_CONFIRM"), "").table
		self.notifyGameStart = true	
		self.beginTime = nil
		self.data = buf
		print(self.uiName,"--set time")
		self.matchBeginWaitTime = os.time()
		NGUITools.SetActive(self.uiMask.gameObject, false)
		--self:CloseTip()
	end
end

function PvpTip:OnOkStartMatch(buf)
	return function (go)
		print(self.uiName, 'OnOkStartMatch')
		self.popup = nil

		LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
		CommonFunction.HideWaitMask()
		local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)
		if resp then
			print(self.uiName,"--set time nil")
			self.beginTime = nil
			self.matchBeginWaitTime = nil
			PvpShow =  false
			if resp.type == match_type_1on1_plus then
		    	  self:StartMatch(resp)
		    elseif resp.type == match_type_3on3 then
		    	  self:StartMatch3On3(resp)
		    end
		    self:CloseTip()
		end

		-- self.beginTime = nil
		-- self.matchBeginWaitTime = nil
		-- PvpShow = false
		-- self:CloseTip()
	end
end

function PvpTip:ForceEnterGame(buf)
  print(self.uiName, 'ForceEnterGame')

  LuaHelper.UnRegisterPlatMsgHandler(MsgID.NotifyGameStartID, self.uiName)
  CommonFunction.HideWaitMask()
  local resp, err = protobuf.decode("fogs.proto.msg.NotifyGameStart", buf)
  if resp then
	printTable(resp)
	if resp.type == match_type_1on1_plus then
		 self:StartMatch(resp)
	elseif resp.type == match_type_3on3 then
		self:StartMatch3On3(resp)
	end
	self:CloseTip()
  end
end

--杩涘叆loading鐣岄潰
function PvpTip:StartMatch( notifyGameStart )
	-----TeamName
	local myName,rivalName
	local myList = RoleInfoList.New()
	local rivalList = RoleInfoList.New()
	for _,v in pairs(notifyGameStart.data) do
		myName = v.name
		for _,role in pairs(v.roles) do
			local gameRoleInfo = RoleInfo.New()
			gameRoleInfo.id = role.id
			gameRoleInfo.fight_power = role.fight_power
			gameRoleInfo.acc_id = v.acc_id

			gameRoleInfo.star = role.star
			gameRoleInfo.quality = role.quality
			gameRoleInfo.level = role.level
			print("pvp 1 + 2 plus myteam id :",role.id)
			myList:Add( gameRoleInfo)
		end
	end
	for _,v in pairs(notifyGameStart.rival_data) do
		rivalName = v.name
		for _,role in pairs(v.roles) do
			local gameRoleInfo = RoleInfo.New()
			gameRoleInfo.id = role.id
			gameRoleInfo.fight_power = role.fight_power
			gameRoleInfo.acc_id = v.acc_id
			gameRoleInfo.star = role.star
			gameRoleInfo.quality = role.quality
			gameRoleInfo.level = role.level
			print("pvp 1 + 2 plus rivalteam id :",role.id)
			rivalList:Add( gameRoleInfo)
		end
	end
	print("myname:",myName,"-----rivalName:",rivalName)

	if UIChallenge then
		UIChallenge.isStartMatch = false
	elseif UI1V1Plus then
		UI1V1Plus.isStartMatch = false
	end

	local challenge  = notifyGameStart.challenge_plus
	local session_id	= notifyGameStart.session_id

	local matchConfig = GameMatch.Config.New()

	matchConfig.leagueType	= GameMatch.LeagueType.ePVP
	matchConfig.type		= GameMatch.Type.ePVP_1PLUS
	matchConfig.sceneId		= challenge.scene_id
	matchConfig.MatchTime	= 180
	matchConfig.session_id	= session_id
	matchConfig.ip			= challenge.game_ip
	matchConfig.port		= challenge.game_port

	print("match id "..session_id)
	print("match server ip "..challenge.game_ip)
	print("match server port "..challenge.game_port)

	--GameSystem.Instance.mClient.onGameServerConn = LuaHelper.Action(self:OnGameServerConn())

	GameSystem.Instance.mClient:CreateNewMatch(matchConfig)

	if self.popup then NGUITools.Destroy(self.popup.gameObject) end

	local goUILoading  = createUI("UIChallengeLoading_1")
  self.uiLoading = goUILoading:GetComponent("UIChallengeLoading")
  self.uiLoading.pvp = true
  local pos = goUILoading.transform.localPosition
  pos.z = -400
  goUILoading.transform.localPosition = pos
  if self.uiLoading ~= nil   then
	--self.uiLoading.onComplete = LuaHelper.Action(self:OnLoadingComplete())
	self.uiLoading.scene_name = challenge.scene_id
	self.uiLoading.myName = myName
	self.uiLoading.rivalName = rivalName
	self.uiLoading.my_role_list = myList
	self.uiLoading.rival_list = rivalList
	self.uiLoading:Refresh(false)

	NGUITools.BringForward(goUILoading)
  end


	if self.btnMenu then
		NGUITools.Destroy(self.btnMenu)
	end
end

function PvpTip:StartMatch3On3( notifyGameStart )
	-----TeamName
	-- local myName = notifyGameStart.data.name
	-- local rivalName = notifyGameStart.rival_data.name
	-- print("myname:",myName,"-----rivalName:",rivalName)
	-----TeamRoles
	local myList = RoleInfoList.New()
	local TeamMatesNameList = StringList.New()
	for k,v in pairs(notifyGameStart.data) do
		local gameRoleInfo = RoleInfo.New()
		for _,role in pairs(v.roles) do
			gameRoleInfo.id = role.id
			gameRoleInfo.fight_power = role.fight_power
			gameRoleInfo.acc_id = v.acc_id
			gameRoleInfo.star = role.star
			gameRoleInfo.quality = role.quality
			gameRoleInfo.level = role.level
			print("my list id :",role.id)
		end
		print("name list mine:",v.name)
		TeamMatesNameList:Add(v.name)
		myList:Add( gameRoleInfo)
	end
	local rivalList = RoleInfoList.New()
	local RivalsNameList = StringList.New()
	for k,v in pairs(notifyGameStart.rival_data) do
		local gameRoleInfo = RoleInfo.New()
		for _,role in pairs(v.roles) do
			gameRoleInfo.id = role.id
			gameRoleInfo.fight_power = role.fight_power
			gameRoleInfo.acc_id = v.acc_id
			gameRoleInfo.star = role.star
			gameRoleInfo.quality = role.quality
			gameRoleInfo.level = role.level
			print("rival list id :",role.id)
		end
		print("name list rival:",v.name)
		RivalsNameList:Add(v.name)
		rivalList:Add( gameRoleInfo)
	end
	local myRoles = myList
	local rivalRoles = rivalList
	--print("myRoles:",table.getn(myRoles),"rivalRoles:",table.getn(rivalRoles))
	if UIChallenge then
		UIChallenge.isStartMatch = false
	elseif UI1V1Plus then
		UI1V1Plus.isStartMatch = false
	end

	local challenge  = notifyGameStart.challenge_ex
	local session_id	= notifyGameStart.session_id

	local matchConfig = GameMatch.Config.New()

	matchConfig.leagueType	= GameMatch.LeagueType.ePVP
	matchConfig.type		= GameMatch.Type.ePVP_3On3
	matchConfig.sceneId		= challenge.scene_id
	matchConfig.MatchTime	= 180
	matchConfig.session_id	= session_id
	matchConfig.ip			= challenge.game_ip
	matchConfig.port		= challenge.game_port

	print("match id "..session_id)
	print("match server ip "..challenge.game_ip)
	print("match server port "..challenge.game_port)

	--GameSystem.Instance.mClient.onGameServerConn = LuaHelper.Action(self:OnGameServerConn())

	GameSystem.Instance.mClient:CreateNewMatch(matchConfig)

	if self.popup then NGUITools.Destroy(self.popup.gameObject) end

	local goUILoading	= createUI("UIChallengeLoading_1")
	self.uiLoading = goUILoading:GetComponent("UIChallengeLoading")
	self.uiLoading.pvp = true
	if self.uiLoading ~= nil   then

		if rivalRoles.Count >= 2 then
		  self.uiLoading.single = false
		else
		  self.uiLoading.single = true
	  end

		self.uiLoading.scene_name = challenge.scene_id
		self.uiLoading.my_role_name_list = TeamMatesNameList
		self.uiLoading.rival_name_list = RivalsNameList
		self.uiLoading.my_role_list = myRoles
		self.uiLoading.rival_list = rivalRoles

		self.uiLoading:Refresh(false)

		NGUITools.BringForward(goUILoading)
	end

	if self.btnMenu then
		NGUITools.Destroy(self.btnMenu)
	end
end

function PvpTip:CloseTip(immediately)
	PvpShow = false
	if self.uiAimator and not immediately then
		self:AnimClose()
	else
		NGUITools.Destroy(self.gameObject)
	end
end

--[[
--]]
function PvpTip:OnGameServerConn()
	return function(go)

		print("OnGameServerConn")

		local matchConfig = GameSystem.Instance.mClient.mCurMatch.m_config
		local data = {
			acc_id = MainPlayer.Instance.AccountID,
			session = matchConfig.session_id
		}

		local buf = protobuf.encode("fogs.proto.msg.EnterGameSrv", data)
		LuaHelper.SendGameMsgFromLua(MsgID.EnterGameSrvID, buf)
		LuaHelper.RegisterGameMsgHandler(MsgID.EnterGameSrvRespID, self:OnEnterGameSrv(), self.uiName)

	end
end

function PvpTip:OnEnterGameSrv()
	return function (message)

		LuaHelper.UnRegisterGameMsgHandler(MsgID.EnterGameSrvRespID, self.uiName)

		print( "enter game srv resp" )
		local resp, err = protobuf.decode('fogs.proto.msg.EnterGameSrvResp', message)
		if resp == nil then
			print('error -- EnterGameSrvResp error: ', err)
			return
		end

		local req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = current_match_type,
			game_mode = GameMode.GM_PVP:ToString(),
		}
		local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", req)

		LuaHelper.SendGameMsgFromLua(MsgID.EnterGameReqID, buf)
		LuaHelper.RegisterGameMsgHandler(MsgID.EnterGameRespID, self:OnEnterGame(), self.uiName)
	end
end


function PvpTip:OnEnterGame()
	return function (message)

		LuaHelper.UnRegisterGameMsgHandler(MsgID.EnterGameRespID, self.uiName)
		--LuaHelper.RegisterGameMsgHandler(MsgID.MatchBeginID, self:OnMatchBegin(), self.uiName)

		print('on enter game')

		local resp, err = protobuf.decode('fogs.proto.msg.EnterGameResp', message)
		if resp == nil then
			print('error -- EnterGameResp error: ', err)
			return
		end

		for i,playerData in ipairs(resp.challenge.rival_data) do
			print( "playerData id: " .. playerData.acc_id )
			local curMatch = GameSystem.Instance.mClient.mCurMatch
			local data = PlayerDataBridge.New()
			data.acc_id = playerData.acc_id
			data.name	= playerData.name
			data.level	= playerData.level
			data.is_room_master	=	playerData.is_room_master
			data.is_room_ready	=	playerData.is_room_ready
			data.is_home_field	=	playerData.is_home_field
			--equipments
			for k,v in ipairs(playerData.equipments) do
				local equipInfo = EquipInfo.New()
				equipInfo.pos = FightStatus[v.pos]
				for _,x in ipairs(v.slot_info) do
					local equipmentSlot = EquipmentSlot.New()
					-- print("equipmentSlotID:",x.id,"------:",EquipmentSlotID[x.id])
					equipmentSlot.id = EquipmentSlotID[x.id]
					equipmentSlot.equipment_id = x.equipment_id
					equipmentSlot.equipment_uuid = x.equipment_uuid
					equipmentSlot.equipment_level = x.equipment_level
					equipInfo.slot_info:Add(equipmentSlot)
				end
				data.equipInfos:Add(equipInfo)
			end
			--squad
			for k,v in ipairs(playerData.squad) do
				local fightRole = FightRole.New()
				fightRole.role_id = v.role_id
				fightRole.status = FightStatus[v.status]
				data.squadInfos:Add(fightRole)
			end

			for i,roleData in ipairs(playerData.roles) do
				local RoleInfo = RoleInfo.New()
				RoleInfo.id = roleData.id
				RoleInfo.level = roleData.level
				RoleInfo.exp = roleData.exp

				RoleInfo.index = roleData.index
				RoleInfo.quality = roleData.quality
				RoleInfo.star = roleData.star
				for k,v in pairs(roleData.skill_slot_info) do
				  local SkillSlotProto = SkillSlotProto.New()
				  SkillSlotProto.id = v.id
				  SkillSlotProto.is_unlock = v.is_unlock
				  SkillSlotProto.skill_uuid = v.skill_uuid
				  RoleInfo.skill_slot_info:Add(SkillSlotProto)
				end
				for k,v in pairs(roleData.exercise) do
				  local ExerciseInfo = ExerciseInfo.New()
				  ExerciseInfo.id = v.id
				  ExerciseInfo.star = v.star
				  ExerciseInfo.quality = v.quality
				  RoleInfo.exercise:Add(ExerciseInfo)
				end
				for k,v in pairs(roleData.fashion_slot_info) do
				  local FashionSlotProto = FashionSlotProto.New()
				  FashionSlotProto.id = v.id
				  FashionSlotProto.fashion_uuid = v.fashion_uuid
				  FashionSlotProto.goods_id = v.goods_id
				  RoleInfo.fashion_slot_info:Add(FashionSlotProto)
				end

				data.roles:Add(RoleInfo)
			end

			curMatch:CreateRoomUser(data)
		end


		self.loadingComplete = true

		if self.loadingComplete then
			LuaHelper.RegisterGameMsgHandler(MsgID.PVPReadyRespID, self:OnReady(), self.uiName)
			local req = {
				type = current_match_type,
			}
			local buf = protobuf.encode("fogs.proto.msg.PVPReadyReq", req)
			LuaHelper.SendGameMsgFromLua(MsgID.PVPReadyReqID, buf)
			print('on ready')
			self.loadingComplete = false
		end


	end
end


--[[
function PvpTip:OnLoadingComplete()
	return function(go)

	   LuaHelper.RegisterGameMsgHandler(MsgID.PVPLoadCompleteRespID, self:OnLoadCompleteResp(), self.uiName)
	   local req = {
		type = current_match_type,
	  }
	  local buf = protobuf.encode("fogs.proto.msg.PVPLoadComplete", req)
	  LuaHelper.SendGameMsgFromLua(MsgID.PVPLoadCompleteID, buf)
	end
end


function PvpTip:OnLoadCompleteResp()
   return function(message)
	  --show icon
	  LuaHelper.UnRegisterGameMsgHandler(MsgID.PVPLoadCompleteRespID, self.uiName)
	  print('on load complete')
   end
end


function PvpTip:OnReady()
	return function(message)
	LuaHelper.UnRegisterGameMsgHandler(MsgID.PVPReadyRespID, self.uiName)
	print("ready")
	if self.uiLoading == nil then
		print("uiLoading is nil")
	end

	local resp, err = protobuf.decode('fogs.proto.msg.PVPReadyResp', message)
	if resp == nil then
		print('pvp ready resp error: ', err)
		return
	end

	self.uiLoading.ready = true
	local curMatch = GameSystem.Instance.mClient.mCurMatch

	--鏆傛椂婊¤冻1v1

		local fightRoleInfo = FightRoleInfo.New()
		for _,v in ipairs(resp.home_position.fighters) do
			local fightrole = FightRole.New()
			fightrole.role_id = v.role_id
			fightrole.status = FightStatus[v.status]
			fightRoleInfo.fighters:Add(fightrole)
			curMatch:SetPlayerPos(TeamType.TT_HOME, fightrole)
		end


		local fightRoleInfo = FightRoleInfo.New()
		for _,v in ipairs(resp.away_position.fighters) do
			local fightrole = FightRole.New()
			fightrole.role_id = v.role_id
			fightrole.status = FightStatus[v.status]
			fightRoleInfo.fighters:Add(fightrole)
			curMatch:SetPlayerPos(TeamType.TT_AWAY, fightrole)
		end
	end
end
]]

function PvpTip:EnterRoomResp()
	return function (message)
		local resp, err = protobuf.decode('fogs.proto.msg.AsynEnterRoomResp', message)
		if resp == nil then
			print('error -- EnterRoomResp error: ', err)
			return
		end

		if resp.result ~= 0 then
			print('error --  EnterRoomResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end

		if resp.type == 'MT_REGULAR' then
			GameSystem.Instance.mClient:CreateNewMatch(GameType.MT_Ready);
		end

		LuaHelper.UnRegisterPlatMsgHandler(MsgID.AsynEnterRoomRespID, self.uiName)
	end
end

--[[
function PvpTip:Destroy( ... )
	print("pvp tip Destroy!!!!")
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.AsynEnterRoomRespID, self.uiName)
end
]]

function PvpTip:OnCancelClick( ... )
	return function (go)
		LuaHelper.RegisterPlatMsgHandler(MsgID.CancelMatchRespID, self:CancelMatchHandler(), self.uiName)

		local req =
		{
			acc_id = MainPlayer.Instance.AccountID,
			type = current_match_type,
		}
		print("current_match_type:",current_match_type)
		local buf = protobuf.encode('fogs.proto.msg.CancelMatchReq', req)
		LuaHelper.SendPlatMsgFromLua(MsgID.CancelMatchReqID, buf)
		print('send')
	end
end

function PvpTip:CancelMatchHandler( ... )
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.CancelMatchRespID, self.uiName)
		local resp, err = protobuf.decode('fogs.proto.msg.CancelMatchResp', message)
		if resp == nil then
			print('error -- CancelMatchResp error: ', err)
			return
		end

		if resp.result ~= 0 and ErrorID.IntToEnum(resp.result) ~= ErrorID.NOT_IN_MATCH_QUEUE  then
			print('error --  CancelMatchResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end

		if self.onCancel then
			self.onCancel(false)
		end

		self.beginTime = nil
		self:CloseTip()
	end
end

function PvpTip:ChangeTipState( ... )
	return function (go)
		if self.uiAimator then
			if self.uiAimator:GetBool("New Bool") then
				if self.onDown then
					self.onDown()
				end
				self.uiAimator:SetBool("New Bool", false)
			else
				if self.onTop then
					self.onTop()
				end
				self.uiAimator:SetBool("New Bool", true)
			end
		end
	end
end

function PvpTip:MakeOnDisconn()
	return function ()
		print(self.uiName, "OnDisconn")
		if self.popup and self.popup.gameObject then
			print(self.uiName, type(self.popup.gameObject))
			print(self.uiName, self.popup.gameObject.name)
			NGUITools.Destroy(self.popup.gameObject)
		end
		if self.onCancel then
			self.onCancel(false)
		end
		self:CloseTip(true)
	end
end

return PvpTip

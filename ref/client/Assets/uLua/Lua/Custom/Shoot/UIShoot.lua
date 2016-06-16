------------------------------------------------------------------------

-- class name    : UIShoot
-- create time   : Fri Sep 11 11:30:57 2015
------------------------------------------------------------------------
UIShoot =  {
	uiName     = "UIShoot",
	curTab = 0, 					-- 
	shootItems = nil,
	curShootItem,
	playedTimes,
	allowPlayTimes,
	hpRequire,
	isShoot = true,	
	reStart = false,
	----------------------- 
	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	------------------------------------
	-------  UI
	uiBackBtn        , 
	uiButtonMenu     , 
	uiTitle          , 
	uiTitleShadow    , 
	uiTabYouMe       , 
	uiTabYouMeSele   , 
	uiTabSpeed       , 
	uiTabSecond      , 
	uiShootItemGrid  , 
	uiRewardGoodGrid , 
	uiStartBtn       , 
	uiLeftTimes      , 
	uiBtnMenu,
	uiTabYouMeValue,
	uiTabSpeedValue,
	uiTabSecondValue,
	uiBack = nil,

	uiAnimator,
}

---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIShoot:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function UIShoot:Start()
	-- self.isShoot = MainPlayer.Instance.IsLastShootGame
	-- local isShoot = self.isShoot
	-- print("isShoot:",isShoot)
	-- if not isShoot then
	-- 	self.uiTabYouMe.spriteName = "career_Ozd_bullfight"
	-- 	self.uiTabYouMeSele.spriteName = "career_Ozd_bullfight"
	-- 	NGUITools.SetActive(self.uiTabSpeed.gameObject, false)
	-- 	NGUITools.SetActive(self.uiTabSecond.gameObject, false)
		
	-- 	self.uiTitle.text = getCommonStr("STR_ONE_ON_ONE")
	-- 	self.uiTitleShadow.text = getCommonStr("STR_ONE_ON_ONE")
	-- else
	-- 	self.uiTitle.text = getCommonStr("STR_SHOOT")
	-- 	self.uiTitleShadow.text = getCommonStr("STR_SHOOT")
	-- end

	local g = createUI("ButtonBack", self.uiBackBtn.transform)
	local t = getLuaComponent(g)
	t.onClick = self:ClickBack()

	self.uiBtnMenu = createUI("ButtonMenu", self.uiButtonMenu.transform)
	t = getLuaComponent(self.uiBtnMenu)
	t:SetParent(self.gameObject, false)
	t.parentScript = self
	addOnClick(self.uiStartBtn.gameObject,self:ClickStart())

	self:Refresh()
	if self.reStart == true then
		self:RestartGame()
	end
end

function UIShoot:Refresh()
	self.isShoot = MainPlayer.Instance.IsLastShootGame
	local isShoot = self.isShoot
	print("isShoot:",isShoot)
	if not isShoot then
		self.uiTabYouMe.spriteName = "career_Ozd_bullfight"
		self.uiTabYouMeSele.spriteName = "career_Ozd_bullfight"
		NGUITools.SetActive(self.uiTabSpeed.gameObject, false)
		NGUITools.SetActive(self.uiTabSecond.gameObject, false)
		
		self.uiTitle.text = getCommonStr("STR_ONE_ON_ONE")
		self.uiTitleShadow.text = getCommonStr("STR_ONE_ON_ONE")
	else
		self.uiTabYouMe.spriteName = "career_Ozd_youMe_gray"
		self.uiTabYouMeSele.spriteName = "career_Ozd_youMe_gray"
		NGUITools.SetActive(self.uiTabSpeed.gameObject, true)
		NGUITools.SetActive(self.uiTabSecond.gameObject, true)
		self.uiTitle.text = getCommonStr("STR_SHOOT")
		self.uiTitleShadow.text = getCommonStr("STR_SHOOT")
	end

	-- local g = createUI("ButtonBack", self.uiBackBtn.transform)
	-- local t = getLuaComponent(g)
	-- t.onClick = self:ClickBack()

	-- g = createUI("ButtonMenu", self.uiButtonMenu.transform)
	-- t = getLuaComponent(g)
	-- t:SetParent(self.gameObject, false)
	-- t.parentScript = self
	-- addOnClick(self.uiStartBtn.gameObject,self:ClickStart())

	self:DataRefresh()
end

-- uncommoent if needed
-- function UIShoot:FixedUpdate()

-- end


function UIShoot:OnClose( ... )
	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		if self.uiBack then
			jumpToUI(self.uiBack)
			self.uiBack = nil
		else
			TopPanelManager:HideTopPanel()
		end
	end
end

function UIShoot:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIShoot:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.         								 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.   								 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.    				 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function UIShoot:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end
	
	self.uiBackBtn        = find("Top/ButtonBack"):GetComponent("Transform")
	
	self.uiButtonMenu     = find("Top/ButtonMenu"):GetComponent("Transform")
	self.uiTitle          = find("Top/Title"):GetComponent("UILabel")
	self.uiTitleShadow    = find("Top/Title/Shadow"):GetComponent("UILabel")
	
	self.uiTabYouMe       = find("Left/Grid/Youme"):GetComponent("UISprite")
	self.uiTabYouMeSele   = find("Left/Grid/Youme/Sele"):GetComponent("UISprite")
	self.uiTabSpeed       = find("Left/Grid/Speed"):GetComponent("UISprite")
	self.uiTabSecond      = find("Left/Grid/Seconds"):GetComponent("UISprite")
	self.uiShootItemGrid  = find("Right/Top/Grid"):GetComponent("UIGrid")	
	
	self.uiRewardGoodGrid = find("Right/Bottom/Grid"):GetComponent("UIGrid")
	self.uiStartBtn       = find("Right/Bottom/ButtonOK"):GetComponent("UIButton")
	self.uiLeftTimes      = find("Right/Bottom/ButtonOK/Text"):GetComponent("MultiLabel")
	self.uiAnimator 		  = self.transform:GetComponent("Animator")

	addOnClick(self.uiTabYouMe.gameObject ,self:ClickIcon(1))
	addOnClick(self.uiTabSpeed.gameObject ,self:ClickIcon(2))
	addOnClick(self.uiTabSecond.gameObject ,self:ClickIcon(3))

end

function UIShoot:DataRefresh()
	self.npcs = {}
	self.shootItems = {}

	if self.isShoot then
		local enum = MainPlayer.Instance.ShootGameModeInfo:GetEnumerator()
		local gameMode = nil
		while enum:MoveNext() do
			-- GameMode
			local gameModeInfo = enum.Current
			gameMode = gameModeInfo.game_mode
			self.gameMode = gameMode
			MainPlayer.Instance.curShootGameMode = gameMode
			
			-- NPC
			local enum_npc = gameModeInfo.npc:GetEnumerator()
			while enum_npc:MoveNext() do
				table.insert(self.npcs, enum_npc.Current)
			end

			-- Play times 
			self.playedTimes = gameModeInfo.times
			self.valueTable = {}
			self.valueTable[1] = gameMode == fogs.proto.msg.GameMode.GM_GrabZone
			self.valueTable[2] = gameMode == fogs.proto.msg.GameMode.GM_GrabPoint
			self.valueTable[3] = gameMode == fogs.proto.msg.GameMode.GM_MassBall
			self.uiTabYouMe:GetComponent("UIToggle").value = gameMode == fogs.proto.msg.GameMode.GM_GrabZone
			self.uiTabSpeed:GetComponent("UIToggle").value = gameMode == fogs.proto.msg.GameMode.GM_GrabPoint
			self.uiTabSecond:GetComponent("UIToggle").value = gameMode == fogs.proto.msg.GameMode.GM_MassBall
		end

		local complete
		if gameMode == fogs.proto.msg.GameMode.GM_GrabZone then
			complete = MainPlayer.Instance.ShootInfo.grab_zone_complete 
		elseif gameMode == fogs.proto.msg.GameMode.GM_MassBall then
			complete = MainPlayer.Instance.ShootInfo.mass_ball_complete 
		elseif gameMode == fogs.proto.msg.GameMode.GM_GrabPoint then
			complete = MainPlayer.Instance.ShootInfo.grab_point_complete 				
		end

		local enumComplete = complete:GetEnumerator()
		print("gameMode=",gameMode)

		CommonFunction.ClearGridChild(self.uiShootItemGrid.transform)
		local index = 1
		while enumComplete:MoveNext() do
			local t = getLuaComponent(createUI("ShootItem", self.uiShootItemGrid.transform))
			
			local match = GameSystem.Instance.shootMatchConfig:GetShootMatch(enumToInt(gameMode), index)
			t:SetData( match.id,  match.hard, index)
			t :SetFinish(enumComplete.Current == 1)
			t.onClick = self:ClickShootItem()
			table.insert(self.shootItems, t)

			-- every match has same played times
			self.allowPlayTimes = match.times
			self.hpRequire = match.win_hp_cost
			index = index + 1

		end
		--set global
		ShootAllowTimes = self.allowPlayTimes
		HPRequire = self.hpRequire

		self.uiShootItemGrid:Reposition()
	else
		-- GameMode
		self.gameMode = fogs.proto.msg.GameMode.GM_BullFight

		-- NPC
		local enum_npc = MainPlayer.Instance.BullFightNpc:GetEnumerator()
		--print("bullfight npc count:",MainPlayer.Instance.BullFightNpc.Count)
		while enum_npc:MoveNext() do
			table.insert(self.npcs, enum_npc.Current)
		end

		local complete = MainPlayer.Instance.BullFight.complete
		local enumComplete = complete:GetEnumerator()

		CommonFunction.ClearGridChild(self.uiShootItemGrid.transform)
		-- Config
		local config = GameSystem.Instance.bullFightConfig
		local enum = config.levels:GetEnumerator()
		local index = 1
		while enum:MoveNext() do
			local hard = enum.Current.Key
			local level = enum.Current.Value
			local bullItem = getLuaComponent(createUI("ShootItem", self.uiShootItemGrid.transform))
			bullItem:SetBullData(hard, index)
			enumComplete:MoveNext()
			bullItem:SetFinish(enumComplete.Current==1)
			bullItem.onClick = self:ClickShootItem()
			-- all levels have same cost.
			self.hpRequire = level.win_hp_cost
			table.insert(self.shootItems, bullItem)
			index = index + 1
		end
		self.uiShootItemGrid:Reposition()
		
		self.allowPlayTimes = GameSystem.Instance.CommonConfig:GetUInt("gBullFightTimes")
		self.playedTimes = MainPlayer.Instance.BullFight.times
	end

	if self.curShootItem == nil then
		local hardLevel = 1
		if self.gameMode == fogs.proto.msg.GameMode.GM_BullFight then
			hardLevel = MainPlayer.Instance.BullFightHard
		elseif self.gameMode == fogs.proto.msg.GameMode.GM_GrabZone then
			hardLevel = MainPlayer.Instance.GrabZoneHard
		elseif self.gameMode == fogs.proto.msg.GameMode.GM_GrabPoint then
			hardLevel = MainPlayer.Instance.GrabPointHard
		elseif self.gameMode == fogs.proto.msg.GameMode.GM_MassBall then
			hardLevel = MainPlayer.Instance.MassBallHard
		end
		self:SetShootItem(self.shootItems[hardLevel])
	end
	
	self:SetShootItem(self.curShootItem)
	-- diplay left times.
	self.uiLeftTimes:SetText(getCommonStr("STR_START").."  "..tostring(self.allowPlayTimes - self.playedTimes).. "/" .. tostring(self.allowPlayTimes))

end

function UIShoot:SetShootItem(item)
	for k,v in pairs(self.shootItems) do
		v:SetSelected(v==item)
	end
	self.curShootItem = item

	CommonFunction.ClearGridChild(self.uiRewardGoodGrid.transform)
	local rewardId
	if self.isShoot then
		local match = GameSystem.Instance.shootMatchConfig:GetShootMatch(enumToInt(self.gameMode), item.hard)
		rewardId = match.reward_id
	else
		rewardId = GameSystem.Instance.bullFightConfig:GetFightLevel(item.hard).reward_id
	end
	local rewardsList = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(rewardId)
	local enum = rewardsList:GetEnumerator()

	while enum:MoveNext() do
		local v = enum.Current
		local id = v.award_id
		if id > 7 then
			local g = createUI("GoodsIcon", self.uiRewardGoodGrid.transform)
			local t = getLuaComponent(g)
			t.goodsID = id
			t.hideLevel = true
			t.hideNeed = true
		end
	end
	self.uiRewardGoodGrid:Reposition()
end

function UIShoot:ClickShootItem()
	return function(item)
		local unlock_level = 0
		if self.isShoot then
			unlock_level = GameSystem.Instance.shootMatchConfig:GetShootMatch(enumToInt(self.gameMode), item.hard).unlock_level
		else
			unlock_level = GameSystem.Instance.bullFightConfig:GetFightLevel(item.hard).unlock_level
		end
		if unlock_level > MainPlayer.Instance.Level then
			CommonFunction.ShowTip(string.format(getCommonStr("STR_CHALLENGE_LEVEL_NOT_ENOUGH_AND_UNLOCK_LEVEL"), unlock_level), nil)
			return
		end
		self:SetShootItem(item)
	end
end

function UIShoot:ClickBack()
	return function()
		self:DoClose()
	end
end


function UIShoot:ClickStart()
	return function()
		-- Check the Condistion.
		if self.allowPlayTimes <= self.playedTimes then
			CommonFunction.ShowTip(getCommonStr("STR_NO_FREE_TODAY"), nil)
			return
		end

		if self.hpRequire > MainPlayer.Instance.Hp then
			CommonFunction.ShowTip(getCommonStr("CAREER_NOT_ENOUGH_HP"), nil)
			return
		end
		
		local shootItem = self.curShootItem
		
		self.curNpc = self.npcs[shootItem.index]
		self.gameModeId = shootItem.gameModeId

		local matchId = enumToInt(self.gameMode)
		local hard = shootItem.hard
		Hard = shootItem.hard
		local unlock_level = 0
		if self.isShoot then
			local sm_level = GameSystem.Instance.shootMatchConfig:GetShootMatch(matchId, hard)
			unlock_level = sm_level.unlock_level
		else
			unlock_level = GameSystem.Instance.bullFightConfig:GetFightLevel(hard).unlock_level
		end

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

		if self.isShoot then
			TopPanelManager:ShowPanel('UIChallenge',
									  nil ,
									  {
										  gameType = "Shoot" ,
										  modeType = self.gameMode, 
										  roleNum = 1,
										  difficult = hard, 
										  npcTable = {
											  self.curNpc
										  },
										  nextShowUI = "UIShoot",
									  }
			)
		else
			TopPanelManager:ShowPanel('UIChallenge',
									  nil ,
									  {
										  gameType = "BullFight" ,
										  modeType = self.gameMode, 
										  roleNum = 1,
										  difficult = hard, 
										  npcTable = {
											  self.curNpc
										  },
									  }
			)			
		end
	end
end

function UIShoot:ClickIcon(index)
	return function()
		if self.valueTable[index] == false then
			CommonFunction.ShowPopupMsg(getCommonStr('GAME_SHOOT_NOT_OPEN'),nil,nil,nil,nil,nil)
		end
		self.uiTabYouMe:GetComponent("UIToggle").value = self.valueTable[1]
		self.uiTabSpeed:GetComponent("UIToggle").value = self.valueTable[2]
		self.uiTabSecond:GetComponent("UIToggle").value = self.valueTable[3]
	end
end

function UIShoot:RestartGame()
	local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.gameMode)
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
				game_mode = self.gameMode:ToString(),
				fighters = {
					{role_id = list[1].role_id,	status = "FS_MAIN",},
				}
			}
	local startShoot = {
		diffictly  = Hard, 
		fight_list = fightList, 
		game_mode = self.gameMode:ToString(), 
	}
	local enterGame = {
		acc_id = MainPlayer.Instance.AccountID,
		type = 'MT_SHOOT',
		shoot = startShoot, 
		game_mode = self.gameMode:ToString(), 
	}

	print("enterGame.game_mode=",enterGame.game_mode)
	local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
	LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
	LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:StartEnterShootGameHandler(fightList.fighters[1].role_id), self.uiName)
	CommonFunction.ShowWaitMask()	
	CommonFunction.ShowWait()	
	self.starting = true

end

function UIShoot:StartEnterShootGameHandler(roleId)
	return function(buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		self.starting = false
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				local sessionId = resp.shoot.session_id
				print("sessionId=",sessionId)
				-- local roleId = self.selectSquad.selectedRoles[1]
				-- local roleId = self.roleLuaTable[1].id
				print("roleId=",roleId)
				self:StartShootGame(sessionId, roleId, CurNPC)
				-- self:SectionStart(resp.career.session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), nil)
			end
		else
			error("UIShoot match -handler", err)
		end
	end
end

function UIShoot:StartShootGame(sessionId, roleId, npcId)
	print("sessionId=",sessionId)
	print("roleId=",roleId)
	print("npcId=",npcId)
	print("self.modeType=",self.gameMode)
	print("enumToInt(self.modeType)=",enumToInt(self.gameMode))

	-- if self.modeType == fogs.proto.msg.GameMode.GM_GrabZone then
	-- 	MainPlayer.Instance.GrabZoneHard = self.difficult
	-- elseif self.modeType == fogs.proto.msg.GameMode.GM_GrabPoint then
	-- 	MainPlayer.Instance.GrabPointHard = self.difficult
	-- elseif self.modeType == fogs.proto.msg.GameMode.GM_MassBall then
	-- 	MainPlayer.Instance.MassBallHard = self.difficult
	-- end
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	local match = GameSystem.Instance.shootMatchConfig:GetShootMatch(enumToInt(self.gameMode), Hard)
	print("match.gameModeId=",match.gameModeId)
	GameSystem.Instance.mClient:CreateShootMatch(sessionId, self.gameMode,roleId, npcId,match.gameModeId)
end

return UIShoot

--enxcoding=utf-8

UIChallenge = UIChallenge or
{	
	uiName = 'UIChallenge',

	----------------parameters
	IconIndex = nil,			--challenge need
	roleNum = 3,   			--all need
	npcToStart,				--tour need
	tourID,					--tour need
	npcTable = {},  		--career and tour use
	chapterID,  			--career use
	sectionID,  			--career use
	difficult,				-- Shoot, BullFight
	roleLuaTable = {},   	--all need
	showRoleList = nil,   	--all need
	gameType = "Challenge", 				--all need
	modeType = GameMode.GM_PVP_1V1_PLUS, 
	isStartMatch = false,   --challenge need
	autoMatch = false, -- challenge use

  tip = nil,

	functionT = {						---- CareerProc ,TourProc ,ChallengeProc, ShootProc, BullFightProc
		["Career"]		 = nil,
		["Challenge"]	  = nil,
		["Tour"]		   = nil,
		["Shoot"]		  = nil, -- Start with Shoot Module
		["BullFight"]	  = nil, -- Start with BullFight Module.
	},

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	onClose,

	----------------UI
	uiBtnMenu,
	uiBackBtn,
	uiGamesGrid,
	uiRolePos,
	uiStartBtn,
	uiAnimator,
}

local match_type_1on1_plus = 'MT_PVP_1V1_PLUS'
local match_type_3on3 = 'MT_PVP_3V3'

local gameMode = GameMode.GM_PVP_1V1_PLUS

function UIChallenge:Awake( ... )
	-- body
	self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
	self.uiTimeLabel = self.transform:FindChild("Right/TimeTip/Label"):GetComponent("UILabel")
	self.uiMask = self.transform:FindChild("Mask").gameObject
	self.uiTitle = self.transform:FindChild("Top/Title"):GetComponent("MultiLabel")
	self.uiUpdateTip = self.transform:FindChild("Right/UpdateTip"):GetComponent("UILabel")
	self.uiTimeTip = self.transform:FindChild("Right/TimeTip"):GetComponent("UILabel")
	self.uiBackBtn = self.transform:FindChild('Top/ButtonBack')
	local backButton = getLuaComponent(createUI('ButtonBack', self.uiBackBtn.transform))
	backButton.onClick = self:OnBack()
	---gameType
	self.uiCareer = self.transform:FindChild('LeftCareer')
	self.uiChallenge = self.transform:FindChild('LeftChallenge')
	------------rolegrid
	self.uiRoleGrid = self.transform:FindChild('Right/RoleGrid'):GetComponent("UIGrid")
	--Btn
	self.uiStartBtn = self.transform:FindChild('Right/ButtonOK'):GetComponent("UIButton")
	self.uiStartBtnLabel = self.transform:FindChild('Right/ButtonOK/Label'):GetComponent('MultiLabel')
	self.uiChallengeBtn = self.transform:FindChild("Right/ButtonChange")
	self.uiChallengeBtnLabel = self.transform:FindChild("Right/ButtonChange/Label"):GetComponent("MultiLabel")
	self.uiChallengeBtnLabel:SetText(getCommonStr("STR_CHANGE_ROLE"))

	self.uiAnimator = self.transform:GetComponent('Animator')

	---------functionT
	self.functionT["Career"] = self:CareerProc()
	self.functionT["Challenge"] = self:ChallengeProc()
	self.functionT["Tour"] = self:TourProc()
	self.functionT["Shoot"] = self:ShootProc()
	self.functionT["BullFight"] = self:BullFightProc()	

end

function UIChallenge:Start( ... )
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	addOnClick(self.uiChallengeBtn.gameObject,self:MakeOnChange())

	-- self.actionReconn = LuaHelper.Action(self:MakeOnReconnected())
	-- PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionReconn
end

function UIChallenge:FixedUpdate()
	-- body	
	
	--[[
	if self.tip.beginTime then
    local waitTime = tonumber(os.time()) - tonumber(self.tip.beginTime)
    self.tip.waitTimeLabel.text = tostring(waitTime) .. '秒'
  end

  if self.tip.matchBeginWaitTime and self.data then
    local matchWaitTime = tonumber(os.time()) - tonumber(self.matchBeginWaitTime)
    self.popup:SetMessage(getCommonStr('MATCH_JOIN_GAME') .. tostring(10 - matchWaitTime))
    
    print(tostring(10 - matchWaitTime))
    
    if matchWaitTime >= 10 then
      self:ForceEnterGame(self.data)
      self.beginTime = nil
      self.matchBeginWaitTime = nil
      PvpShow = false
    end
  end
	]]
end

function UIChallenge:MakeOnReconnected()
	return function ()
		print(self.uiName, "UIChallenge on reconnected, isStartMatch:", UIChallenge.isStartMatch)
		PvpShow = false
		if UIChallenge.isStartMatch then
			self:OnStartChallengClick()()
		end
	end
end

function UIChallenge:OnClose()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, true)

	if self.onClose then 
		self.onClose()
		self.onClose = nil
		return
	end

	if self.nextShowUI then
		print("TopPanelManager:ShowPanel:",self.uiName)
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	-- elseif self.gameType == "Career" then 
	-- 	TopPanelManager:HideTopPanel()
	-- else
	-- 	jumpToUI("UIHall") 	
	else
		print("TopPanelManager:HideTopPanel:",self.uiName)
		TopPanelManager:HideTopPanel()
	end
end

function UIChallenge:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIChallenge:OnDestroy( ... )
	-- body
	-- PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionReconn
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIChallenge:Refresh( ... )
	self.functionT[ self.gameType]()
	--self:RoleRefresh()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
end


-----------------------------------------------------------------
function UIChallenge:TourProc()
	return function()
	-----show left by gametpye
	NGUITools.SetActive(self.uiTitle.gameObject,false)
	--self.uiStartBtnLabel:SetText( getCommonStr("CAREER_BUTTON_START")) 
	if self.roleNum == 1 then self.firstClick = false else self.firstClick = true end
	NGUITools.SetActive( self.uiCareer.gameObject, true)
	addOnClick(self.uiStartBtn.gameObject, self:OnStartTourClick())
	---------npc set active false
	for k = 1 , 3 do
		local grid = self.uiCareer:FindChild("Care"..k)
		local obj = grid:FindChild("RoleIconItem(Clone)")
		if obj then
			NGUITools.Destroy(obj.gameObject)
		end
	end
	---------npc show
	local j = 1
	print("self.roleNum:",self.roleNum)
	for j = 1, self.roleNum do
		local grid = self.uiCareer:FindChild("Care"..j)
		local npc = getLuaComponent(createUI("RoleIconItem",grid))
		npc.id = self.npcTable[j]
		npc.npc = true
		j = j + 1
	end 	
	-------role show
	self:CreateFightRole()
	self.uiRoleGrid:Reposition()
	self:SetStartBtn()
	self:SetTipsLabel()
	end
end

function UIChallenge:OnStartTourClick()
	return function()
		if self.starting then return end
		if self:CountRoleNum() ~= true then
			CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
			return
		end

		--if self.modeType == GameMode.GM_Career3On3 then
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
		if not fightRoleInfoList then
			fightRoleInfoList = FightRoleInfoList.New()
			MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
		end
		self:CareerFillFighters(fightRoleInfoList)
		--end

		local fightList
		if self.roleNum == 1 then
			fightList = self:Fill1ON1List()
		else
			fightList = self:Fill3ON3List()
		end
		local tour = {
			fight_list = fightList
		}
		local req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_TOUR',
			tour = tour,
			game_mode = self.modeType:ToString(),
		}

		local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeTourMatchHandler(), self.uiName)
		CommonFunction.ShowWaitMask()
		self.starting = true
	end
end

function UIChallenge:MakeTourMatchHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		self.starting = false
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		if resp then
			if resp.result == 0 then
				self:StartTourMatch(resp.tour.session_id, resp.tour.cur_tour_id)
				--LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITour:", err) 
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	end
end

function UIChallenge:StartTourMatch( session_id, tourID)
	assert(tourID == self.tourID, "UITour: started tour not cur tour.")
	local teammates = UintList.New()
	local fightlist
	if self.roleNum == 1 then
		fightlist =self:Fill1ON1List().fighters
	else
		fightlist = self:Fill3ON3List().fighters
	end
	for _,v in pairs(fightlist) do
		teammates:Add(v.role_id)
	end
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	tourData = GameSystem.Instance.TourConfig:GetTourData(self.tourID, MainPlayer.Instance.Level)
	GameSystem.Instance.mClient:CreateNewMatch(tourData.gameModeID, session_id, false, GameMatch.LeagueType.eTour, teammates, self.npcToStart)
	--GameSystem.Instance.mClient:CreateNewMatchWithLoading(tourData.gameModeID, session_id, GameMatch.LeagueType.eTour, teammates, self.npcToStart)
	--delete uichalleng in panel list
	TopPanelManager:PanelListPopBack()
end


function UIChallenge:CareerProc()
	return function()
	-----show left by gametpye
	NGUITools.SetActive(self.uiTitle.gameObject,false)
	--self.uiStartBtnLabel:SetText( getCommonStr("CAREER_BUTTON_START")) 
	if self.roleNum == 1 then self.firstClick = false else self.firstClick = true end
	NGUITools.SetActive( self.uiCareer.gameObject, true)
	addOnClick(self.uiStartBtn.gameObject, self:OnStartCareerClick())
	---------npc set active false
	for k = 1 , 3 do
		local grid = self.uiCareer:FindChild("Care"..k)
		local obj = grid:FindChild("RoleIconItem(Clone)")
		if obj then
			NGUITools.Destroy(obj.gameObject)
		end
	end
	---------npc show
	local j = 1
	for j = 1, self.roleNum do
		local grid = self.uiCareer:FindChild("Care"..j)
		local npc = getLuaComponent(createUI("RoleIconItem",grid))
		npc.id = self.npcTable[j]
		npc.npc = true
		j = j + 1
	end 	
	-------role show
	self:CreateFightRole()
	self.uiRoleGrid:Reposition()
	self:SetStartBtn()
	self:SetTipsLabel()
	end
end

function UIChallenge:OnStartCareerClick()
	return function ()
		if self.starting then return end
		if self:CountRoleNum() ~= true then
			CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
			return
		end

		--if self.modeType == GameMode.GM_Career3On3 then
			local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
			if not fightRoleInfoList then
				fightRoleInfoList = FightRoleInfoList.New()
				MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
			end
			self:CareerFillFighters(fightRoleInfoList)
			print("CareerFillFighters:",fightRoleInfoList.Count)
		--end

		local fightList
		if self.roleNum == 1 then
			fightList = self:Fill1ON1List()
		else
			fightList = self:Fill3ON3List()
		end
		local career = {
			chapter_id = self.chapterID,
			section_id = self.sectionID,
			fight_list = fightList,
		}
		
		
		local enterGame = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_CAREER',

			career = career,
			game_mode = self.modeType:ToString(),
		}
		local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeCareerMatchHandler(), self.uiName)
		CommonFunction.ShowWaitMask()
		self.starting = true
	end
end

function UIChallenge:MakeCareerMatchHandler()
	return function(buf)
		CommonFunction.HideWaitMask()
		self.starting = false
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		if resp then
			if resp.result == 0 then
				self:CareerSectionStart(resp.career.session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UICareerSection: ", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	end
end

function UIChallenge:CareerSectionStart( session_id)
	local section = MainPlayer.Instance:GetSection(self.chapterID, self.sectionID)
	local needPlot = not section.is_complete
	local teammates = UintList.New()
	local fightlist
	if self.roleNum == 1 then
		fightlist =self:Fill1ON1List().fighters
	else
		fightlist = self:Fill3ON3List().fighters
	end
	for _,v in pairs(fightlist) do
		teammates:Add(v.role_id)
	end
	--set global
	local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.sectionID)
	CurChapterID = self.chapterID
	CurSectionID = self.sectionID
	CurSectionComplete = not needPlot
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	GameSystem.Instance.mClient:CreateNewMatch(sectionConfig.game_mode_id, session_id, needPlot, GameMatch.LeagueType.eCareer, teammates, nil)
	--GameSystem.Instance.mClient:CreateNewMatchWithLoading(sectionConfig.game_mode_id, session_id, GameMatch.LeagueType.eCareer, teammates)
	UICareerSection.OnMatchCreated()
	--delete uichalleng in panel list
	TopPanelManager:PanelListPopBack()
end

function UIChallenge:CareerFillFighters( list)
	list:Clear()
	if self.roleNum == 1 then 
		local info = FightRole.New()	
		info.role_id = self:Fill1ON1List().fighters[1].role_id 
		info.status = FightStatus.FS_MAIN
		list:Add(info)
	else 
		local info = FightRole.New()
		info.role_id = self.roleLuaTable[1].id
		info.status = FightStatus.FS_MAIN
		list:Add(info)
		if self.roleNum == 1 then return end
		info = FightRole.New()
		info.role_id = self.roleLuaTable[2].id
		info.status = FightStatus.FS_ASSIST1
		list:Add(info)
		info = FightRole.New()
		info.role_id = self.roleLuaTable[3].id
		info.status = FightStatus.FS_ASSIST2
		list:Add(info)
	end
end

function UIChallenge:ChallengeProc()
	return function ()
		-----show left by gametpye
		-- self.roleNum = 3
		self.uiTitle:SetText(getCommonStr("STR_CHALLENGE_GAME"))
		self.uiTimeLabel.text = tostring( 10 - MainPlayer.Instance.PvpRunTimes).."/10"
		self.firstClick = true
		self.numByType = {1, 3}  -----rolenum by modetype
		self.gameModeTable = {GameMode.GM_PVP_3V3,GameMode.GM_PVP_1V1_PLUS}
		NGUITools.SetActive( self.uiChallenge.gameObject, true)
		NGUITools.SetActive( self.uiUpdateTip.gameObject, true)
		self.uiChallengeGrid = self.transform:FindChild('LeftChallenge/Scroll/Grid')

		if self.addFlag == nil then
			addOnClick(self.uiStartBtn.gameObject, self:OnStartChallengClick())
			self.toggleTable = {}
			for i = 1 ,5 do
				local go = self.uiChallengeGrid:FindChild('ChoseToggle' .. i).gameObject
				addOnClick(go,self:MakeOnIcon(i))
				self.toggleTable[i] = go.transform
				print("UIChallenge toggle",i,":",go.transform)
			end
			----pvp tip 
			if PvpShow == true then
				self.tip = createUI('PvpTip',  self.transform.parent.transform)
				UIManager.Instance:BringPanelForward(self.tip.gameObject)
				--self.tip.transform:GetComponent('UIPanel').depth = 50
				local luacom = getLuaComponent(self.tip.gameObject)
				luacom.btnMenu = self.uiBtnMenu.gameObject
				luacom.onCancel = self:IsCancel()
			end
			---default 
			self:MakeOnIcon(1)()
		end
		-- self.IconIndex = 1	
		------show Role
		self:CreateFightRole()
		self.uiRoleGrid:Reposition()
		self:SetStartBtn()
		self:SetTipsLabel()
		self:SetToggle()
		--self:RoleRefresh()
		if self.autoMatch == true then
			self:OnStartChallengClick()()
		end
	end
end

function UIChallenge:OnStartChallengClick( ... )
	return function ()
		print("pvp fightnum:",self:CountRoleNum())
		if self:CountRoleNum() ~= true then
			CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
			return
		end
		
		local avg = GameSystem.Instance.mNetworkManager.m_platConn.m_profiler.m_avgLatency
		if  avg >= GlobalConst.PVP_VALID_LATENCY then
      CommonFunction.ShowPopupMsg(getCommonStr('STR_PVP_INVALID_LATENCY'),nil,nil,nil,nil,nil)
      return
    end

		if PvpShow == true then
			return
		end


	    print('icon index' .. self.IconIndex)
	    local operation = nil
		if self.IconIndex == 2 then
		--[[
		    for i = 0, 2 then
		      local data = KeyValueData.New()
		      data.key = 
		    
		    end
		    ]]
		
  			operation = {
		        acc_id = MainPlayer.Instance.AccountID,
		        type = match_type_1on1_plus,
		        challenge = {
          			play_way = tostring(enumToInt(gameMode)),
		          	fight_list = {
			            game_mode = gameMode:ToString(),
			            fighters = self:Fill1ON1List(),
			         },
        		},
        
        		game_mode = gameMode:ToString(),
      		}
      		print('game mode' .. 1)
		elseif self.IconIndex == 1 then
			local roleID = self:Fill1ON1List().fighters[1].role_id
	        local fightValue = MainPlayer.Instance:GetRoleAttrsByID(roleID).fightingCapacity

		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
			if not fightRoleInfoList then
				fightRoleInfoList = FightRoleInfoList.New()
				MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
			end
			self:CareerFillFighters(fightRoleInfoList)


	       	print('roleID = ' , roleID)
	        print('fightValue = ', fightValue)
	  		operation = {
		        acc_id = MainPlayer.Instance.AccountID,
		        type = match_type_3on3,
	        	challenge_ex = {
		          	--play_way = tostring(enumToInt(gameMode)),
		          	fight_list = {
		            	game_mode = self.modeType:ToString(),
		            	fighters = self:Fill1ON1List().fighters,
		          	},
	        	},
	        	
	        	game_mode = self.modeType:ToString(),
	        	fight_power = {{id = roleID, value = fightValue}},
	      	}
        	print('game mode' .. 2)
		else
		 	CommonFunction.ShowPopupMsg(getCommonStr('IN_CONSTRUCTING'),nil,nil,nil,nil,nil)
		  	print('icon index' .. self.IconIndex)
		  	return
		end

		--register
		LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, self:StartChallengeResp(), self.uiName)
		
		--print('play_way ====== ' .. operation.challenge.play_way)

		local req = protobuf.encode("fogs.proto.msg.StartMatchReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, req)		
	end
end

function UIChallenge:StartChallengeResp( ... )
	return function (message)
		print('pppppp')
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.StartMatchRespID, self.uiName)

		local resp, err = protobuf.decode('fogs.proto.msg.StartMatchResp', message)
		if resp == nil then
			print('error -- StartMatchResp error: ', err)
			return
		end

		if resp.result ~= 0 then
			print('error --  StartMatchResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		
		-- print(self.uiName,"----reconnected parent:",self.transform.parent)
		local tipGo = createUI('PvpTip',  UIManager.Instance.m_uiRootBasePanel.transform)
		tipGo.transform:GetComponent('UIPanel').depth = 50
		
		-- local tip = tipGo.transform:FindChild("PvpTip")
		local luacom = getLuaComponent(tipGo.gameObject)
		luacom.btnMenu = self.uiBtnMenu.gameObject
		luacom.averTime = resp.aver_time
		luacom.onCancel = self:IsCancel()
		if self.modeType == GameMode.GM_PVP_1V1_PLUS then
			current_match_type = 'MT_PVP_1V1_PLUS'
		elseif self.modeType == GameMode.GM_PVP_3V3 then
			current_match_type = 'MT_PVP_3V3'
		end
		UIChallenge.isStartMatch = true
		self.banTwice = true
	end
end

function UIChallenge:IsCancel( ... )
	return function (isCancel)
		UIChallenge.isStartMatch = isCancel
		self.banTwice = false
	end
end

---- challenge use
function UIChallenge:MakeOnIcon( index)   
	return function()
		if index ~= 1 then --and index ~= 2 then
			CommonFunction.ShowPopupMsg(getCommonStr('IN_CONSTRUCTING'),nil,nil,nil,nil,nil)
			print('Under constructing')
			return
		end 
		if self.IconIndex and self.IconIndex == index then
			return
		else
			self.addFlag = true
			self.IconIndex = index	
			self.roleNum = self.numByType[ index]
			self.modeType = self.gameModeTable[index]
			self:ChallengeProc()()
		end
	end
end


function UIChallenge:CreateFightRole()
	CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	self.showRoleList = {}	
	for i = 1, 3 do
		local role = self:InitRole('RoleBustItem1', 
			{status = FightStatus.IntToEnum(i), transform = self.uiRoleGrid.transform})
		role.gameObject.name = i
		role.useType = "Challenge"
  		role.onClickSelect = self:MakeOnChoose()
  		self.roleLuaTable[i] = role
  		self.showRoleList[ role.id] = i
  		role.gameObject.name = i
  		if i == 3 then break end
	end
	
	if self.roleNum == 3 then
		self:MakeOnChoose()()
	end
	self:ConfirmInclude()
end


function UIChallenge:InitRole( prefabName , parent)
	local squadInfo = MainPlayer.Instance.SquadInfo
	local enum = squadInfo:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.status == parent.status then --FS_MAIN, FS_ASSIST1, FS_ASSIST2
			child = createUI(prefabName, parent.transform)
			local script = getLuaComponent(child)
			script.id = enum.Current.role_id
			script.status = enum.Current.status
			return script
		end
	end
end

function UIChallenge:RoleRefresh()
	for _,v in pairs( self.roleLuaTable) do
		print("RoleRefresh") 
		v:Refresh()
	end
end

function UIChallenge:SetStartBtn()
	print("self.firstClick:",self.firstClick)
	if self.firstClick == false then
		self.uiStartBtn.enabled = false
		self.uiStartBtn.transform:GetComponent("BoxCollider").enabled = false
		print("self.uiStartBtn.enabled:",self.uiStartBtn.enabled)
	else
		self.uiStartBtn.enabled = true
		self.uiStartBtn.transform:GetComponent("BoxCollider").enabled = true
	end
end

function UIChallenge:SetTipsLabel()
	if self.roleNum == 1 then 
		for i = 1 , table.getn(self.roleLuaTable) do
			self.roleLuaTable[i].uiTipsOutLabel.text = "1ON1"
		end
	elseif  self.roleNum == 3 then
		for i = 1 , table.getn(self.roleLuaTable) do
			self.roleLuaTable[i].uiTipsOutLabel.text = "3ON3"
		end
	end
end

function UIChallenge:SetToggle()
	for i = 1,5 do
		if i == self.IconIndex then
			self.toggleTable[i]:GetComponent("UIToggle").value = true
			print("UIChallenge Set",i,"-value true")
		else
			self.toggleTable[i]:GetComponent("UIToggle").value = false
			print("UIChallenge Set",i,"-value false")
		end
	end
end

------- all use
function UIChallenge:ConfirmInclude()
	if self.roleNum == 3 then NGUITools.SetActive(self.uiMask,false) return end

	local flag = 0
	local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
	if fightRoleInfoList then
		local enum = fightRoleInfoList:GetEnumerator()
		while enum:MoveNext() do
			if self.showRoleList[ enum.Current.role_id] then
				print("ConfirmInclude:",self.showRoleList[ enum.Current.role_id])
				self:MakeOnChoose()( self.roleLuaTable[ self.showRoleList[ enum.Current.role_id]])
				flag = 1 
				break
			end
		end
	end
	if flag == 0 then
		NGUITools.SetActive(self.uiMask,true)
	end
end	

function UIChallenge:MakeOnChoose( )
	--鏍规嵁rolenum
	return function ( go)
		print("ConfirmInclude choose")
		if self.roleNum == 3 then
			return
		end
		if self.roleNum == 1 then 
			--鐐瑰嚮涓�釜鍏朵粬涓や釜鐏版帀
			print("numnum:",table.getn(self.roleLuaTable))
			for i = 1 , table.getn(self.roleLuaTable) do 
				if go ~= self.roleLuaTable[i] then
					---mask up
					print("NGUITools:true")
					NGUITools.SetActive(self.roleLuaTable[i].uiMaskUp.gameObject,true)
					NGUITools.SetActive(self.roleLuaTable[i].uiTipsOut.gameObject,false)
				else
					print("NGUITools:false")
					NGUITools.SetActive(self.roleLuaTable[i].uiMaskUp.gameObject,false)
					NGUITools.SetActive(self.roleLuaTable[i].uiTipsOut.gameObject,true)
				end
			end
			self.firstClick = true
		elseif self.roleNum == 3 then
			if not self.firstClick then
				for i = 1 , table.getn(self.roleLuaTable) do 
					if go ~= self.roleLuaTable[i] then
						---mask up
						print("NGUITools:true")
						NGUITools.SetActive(self.roleLuaTable[i].uiMask.gameObject,true)
						--NGUITools.SetActive(self.roleLuaTable[i].uiTipsOut.gameObject,false)
					else
						print("NGUITools:false")
						NGUITools.SetActive(self.roleLuaTable[i].uiMask.gameObject,false)
						--NGUITools.SetActive(self.roleLuaTable[i].uiTipsOut.gameObject,true)
					end
				end
				self.firstClick = true
			else
				if NGUITools.GetActive(go.uiMask.gameObject) == true then
					NGUITools.SetActive(go.uiMask.gameObject,false)
					NGUITools.SetActive(go.uiTipsOut.gameObject,true)
				end
			end
		end
		NGUITools.SetActive(self.uiMask,false)
		self:SetStartBtn()
	end
end

function UIChallenge:OnBack( ... )
	return function ( ... )
	  --if not PvpShow then
		--  self:DoClose()
		--end
		self:DoClose()
	end
end

----- all use 
function UIChallenge:Fill1ON1List(  )
	local fightList = {}
	for i = 1 , table.getn(self.roleLuaTable) do 
		if NGUITools.GetActive(self.roleLuaTable[i].uiMaskUp.gameObject) == false then
			print("self.roleLuaTable[i].id:",self.roleLuaTable[i].id) 
			fightList = {
				game_mode = self.modeType:ToString(),
				fighters = {
					{role_id = self.roleLuaTable[i].id,	status = "FS_MAIN",},
				}
			}
			printTable(fightList)
		end
	end
	return fightList
end

function UIChallenge:Fill3ON3List()
	local fightList = {
		game_mode = self.modeType:ToString(),
		fighters = {
			{role_id = self.roleLuaTable[1].id,	status = "FS_MAIN",},
			{role_id = self.roleLuaTable[2].id,	status = "FS_ASSIST1",},
			{role_id = self.roleLuaTable[3].id,	status = "FS_ASSIST2",},
		}
	}
	return fightList
end

------- all use 
function UIChallenge:CountRoleNum()
	local count = 0
	if self.roleNum == 1 then
		--print("tablenum:",table.getn(self.roleLuaTable))
		for i = 1 , table.getn(self.roleLuaTable) do 
			if NGUITools.GetActive(self.roleLuaTable[i].uiMaskUp.gameObject) == false then
				count = count + 1
			end
		end
		if count == 1 then 
			return true
		else 
			return false
		end
	elseif self.roleNum == 3 then 
		for i = 1 , table.getn(self.roleLuaTable) do 
			if NGUITools.GetActive(self.roleLuaTable[i].uiMaskUp.gameObject) == false then
				count = count + 1
			end
		end
		if count == 3 then 
			return true
		else 
			return false
		end	
	end
end

function UIChallenge:MakeOnChange()
	return function()
		--TopPanelManager:ShowPanel('UIMember',nil ,{ showlist = self.showRoleList,})
		TopPanelManager:ShowPanel('UISquad')
	end
end

------------------------------------
-------  Shoot Module
function UIChallenge:ShootProc()
	return function()
		-----show left by gametpye
		NGUITools.SetActive(self.uiTitle.gameObject,false)

		if self.roleNum == 1 then
			self.firstClick = false
		else
			self.firstClick = true
		end
		
		NGUITools.SetActive( self.uiCareer.gameObject, true)
		addOnClick(self.uiStartBtn.gameObject, self:OnStartShootClick())
		---------npc set active false
		for k = 1 , 3 do
			local grid = self.uiCareer:FindChild("Care"..k)
			local obj = grid:FindChild("RoleIconItem(Clone)")
			if obj then
				NGUITools.Destroy(obj.gameObject)
			end
		end
		
		---------npc show
		local j = 1
		print("self.roleNum:",self.roleNum)
		for j = 1, self.roleNum do
			local grid = self.uiCareer:FindChild("Care"..j)
			local npc = getLuaComponent(createUI("RoleIconItem",grid))
			npc.id = self.npcTable[j]
			npc.npc = true
			j = j + 1
		end
		
		-------role show
		self:CreateFightRole()
		self.uiRoleGrid:Reposition()
		self:SetStartBtn()
		self:SetTipsLabel()
	end
end

function UIChallenge:OnStartShootClick()
	return function()
		if self.starting then return end
		if self:CountRoleNum() ~= true then
			CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
			return
		end
		
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
		if not fightRoleInfoList then
			fightRoleInfoList = FightRoleInfoList.New()
			MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
		end
		self:CareerFillFighters(fightRoleInfoList)
		
		local fightList
		fightList = self:Fill1ON1List()
				
		local startShoot = {
			fight_list = fightList, 
			game_mode = self.modeType:ToString(), 
		}
		local enterGame = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_SHOOT',
			shoot = startShoot, 
			game_mode = self.modeType:ToString(), 
		}

		print("enterGame.game_mode=",enterGame.game_mode)
		local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:StartEnterShootGameHandler(fightList.fighters[1].role_id), self.uiName)
		CommonFunction.ShowWaitMask()		
		self.starting = true
	end
end

function UIChallenge:StartEnterShootGameHandler(roleId)
	return function(buf)
		CommonFunction.HideWaitMask()
		self.starting = false
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				local sessionId = resp.shoot.session_id
				print("sessionId=",sessionId)
				print("roleId=",roleId)
				self:StartShootGame(sessionId, roleId, self.npcTable[1])
				-- self:SectionStart(resp.career.session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), nil)
			end
		else
			error("UIShoot match -handler", err)
		end
	end
end

function UIChallenge:StartShootGame(sessionId, roleId, npcId)
	print("sessionId=",sessionId)
	print("roleId=",roleId)
	print("npcId=",npcId)
	print("self.modeType=",self.modeType)
	print("enumToInt(self.modeType)=",enumToInt(self.modeType))
	CurNPC = npcId
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	local match = GameSystem.Instance.shootGameConfig:GetShootInfo(enumToInt(self.modeType), MainPlayer.Instance.Level)
	GameSystem.Instance.mClient:CreateShootMatch(sessionId, self.modeType,roleId, npcId, match.game_mode_id, enumToInt(self.modeType))
	--delete uichalleng in panel list
	TopPanelManager:PanelListPopBack()
end




------------------------------------
-------  BullFight Module

function UIChallenge:BullFightProc()
	return function()
		-----show left by gametpye
		NGUITools.SetActive(self.uiTitle.gameObject,false)

		if self.roleNum == 1 then
			self.firstClick = false
		else
			self.firstClick = true
		end
		
		NGUITools.SetActive( self.uiCareer.gameObject, true)
		addOnClick(self.uiStartBtn.gameObject, self:OnStartBullFightClick())
		---------npc set active false
		for k = 1 , 3 do
			local grid = self.uiCareer:FindChild("Care"..k)
			local obj = grid:FindChild("RoleIconItem(Clone)")
			if obj then
				NGUITools.Destroy(obj.gameObject)
			end
		end
		
		---------npc show
		local j = 1
		print("self.roleNum:",self.roleNum)
		for j = 1, self.roleNum do
			local grid = self.uiCareer:FindChild("Care"..j)
			local npc = getLuaComponent(createUI("RoleIconItem",grid))
			npc.id = self.npcTable[j]
			npc.npc = true
			j = j + 1
		end
		
		-------role show
		self:CreateFightRole()
		self.uiRoleGrid:Reposition()
		self:SetStartBtn()
		self:SetTipsLabel()
	end
end


function UIChallenge:OnStartBullFightClick()
	return function()
		if self.starting then return end
		if self:CountRoleNum() ~= true then
			CommonFunction.ShowPopupMsg(getCommonStr('CAREER_NOT_ENOUGH_PLAYER'),nil,nil,nil,nil,nil)
			return
		end
		
		--if self.modeType == GameMode.GM_Career3On3 then
		local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(self.modeType)
		if not fightRoleInfoList then
			fightRoleInfoList = FightRoleInfoList.New()
			MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
		end
		self:CareerFillFighters(fightRoleInfoList)

		local fightList
		fightList = self:Fill1ON1List()
		
		local mode_type = fogs.proto.msg.GameMode.GM_BullFight
		print("mode_type=",mode_type)
		
		local bull_fight = {
			diffictly  = self.difficult, 
			fight_list = fightList
		}
		
		local enterGame = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_BULLFIGHT',
			
			bull_fight = bull_fight, 
			game_mode = "GM_BullFight", 
		}

		print("enterGame.game_mode=",enterGame.game_mode)
		local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:StartEnterBullFightHandler(fightList.fighters[1].role_id), self.uiName)
		CommonFunction.ShowWaitMask()		
		self.starting = true
	end
end


function UIChallenge:StartEnterBullFightHandler(roleId)
	return function(buf)
		CommonFunction.HideWaitMask()
		self.starting = false
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				local sessionId = resp.shoot.session_id
				print("sessionId=",sessionId)
				print("roleId=",roleId)
				self:StartBullFightGame(sessionId, roleId, self.npcTable[1])

			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), nil)
			end
		else
			error("UIShoot match -handler", err)
		end
	end
end

function UIChallenge:StartBullFightGame(sessionId, roleId, npcId)
	print("sessionId=",sessionId)
	print("roleId=",roleId)
	print("npcId=",npcId)
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.uiBtnMenu.gameObject)
	MainPlayer.Instance.BullFightHard = self.difficult
	GameSystem.Instance.mClient:CreateBullFightMatch(sessionId, roleId, npcId)
	--delete uichalleng in panel list
	TopPanelManager:PanelListPopBack()	
end

function UIChallenge:SetModelActive(active)
	-- body
end

return UIChallenge

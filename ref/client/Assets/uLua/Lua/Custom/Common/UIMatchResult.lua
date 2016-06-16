--encoding=utf-8

UIMatchResult = {
	uiName = 'UIMatchResult',

	----------------------------------
	isWin = false,
	leagueType = GameMatch.LeagueType.eNone,
	awards,
	increaseExp,
	onAgain,
	jumpToRole = false,
	jumpToSquad = false,
	jumpToTraing = false,
	firstComplete = false,
	maxPlayerLv,
	----------------------------------UI
	uiWin,
	uiWinText,
	uiWinTop,
	uiWinStars = {},
	uiWinStarLabel = {},
	uiWinLevel,
	uiWinExp,
	uiWinExpLabel,
	uiGetGoodsText,
	uiWinStarsNode,
	-- uiWinRoleList,
	uiWinGoodsScroll,
	uiWinGoodsList,
	uiGoodsIconConsumeGird,

	uiLose,
	uiLoseText,
	uiLoseTop,
	uiLoseTitle,
	uiLoseTips,
	uiLosePromoteTips,
	uiLosePromoteTraining,
	uiLosePromoteAdvance,
	uiLosePromoteEnhance,

	uiButtonAgain,
	uiButtonConfirm,
	uiButtonConfirm1,

	uiAnimator,
}


-----------------------------------------------------------------
function UIMatchResult:Awake()
	local transform = self.transform:FindChild("Window").transform

	self.maxPlayerLv = GameSystem.Instance.CommonConfig:GetUInt("gPlayerMaxLevel")
	--
	self.uiWin = transform:FindChild('Win').gameObject
	self.uiWinText = transform:FindChild('Win/Text'):GetComponent('UILabel')
	self.uiWinTop = transform:FindChild('Win/Top')
	for i = 1, 3 do
		self.uiWinStars[i] = getComponentInChild(self.uiWin.transform, "Top/Star" .. i, "UISprite")
		self.uiWinStarLabel[i] = getComponentInChild (self.uiWin.transform, "Top/Star" .. i.."/Label", "UILabel")
	end
	self.uiWinLevel = transform:FindChild('Win/Exp/Level'):GetComponent('UILabel')
	self.uiWinText = transform:FindChild("Win/Exp/Text"):GetComponent("UILabel")
	self.uiWinExp = transform:FindChild('Win/Exp/Bgexp'):GetComponent('UISlider')
	self.uiWinExpLabel = transform:FindChild('Win/Exp/ExpNum'):GetComponent('UILabel')
	self.uiWinGrid = transform:FindChild("Win/Grid")
	self.uiGetGoodsText = transform:FindChild("Win/GetGoods"):GetComponent("UISprite")
	self.uiWinStarsNode = transform:FindChild("Win/Sprite")
	
	-- self.uiWinRoleList = transform:FindChild('Win/RoleList'):GetComponent('UIGrid')
	self.uiWinGoodsScroll = transform:FindChild('Win/GoodsList'):GetComponent('UIScrollView')
	self.uiWinGoodsList = transform:FindChild('Win/GoodsList/Grid'):GetComponent('UIGrid')
	self.uiGoodsIconConsumeGird = transform:FindChild("Win/GoodsIconConsume"):GetComponent("UIGrid")

	self.uiLose = transform:FindChild('Lose').gameObject
	self.uiLoseText = transform:FindChild('Lose/Text'):GetComponent('UILabel')
	self.uiLoseTop = transform:FindChild('Lose/Top')
	self.uiLoseTitle = transform:FindChild('Lose/Title'):GetComponent('UILabel')
	self.uiLoseTips = transform:FindChild('Lose/Tips'):GetComponent('UILabel')
	self.uiLosePromoteTips = transform:FindChild('Lose/PromoteTips'):GetComponent('UIGrid')
	self.uiLosePromoteTraining = transform:FindChild('Lose/PromoteTips/Training').gameObject
	self.uiLosePromoteAdvance = transform:FindChild('Lose/PromoteTips/Advance').gameObject
	self.uiLosePromoteEnhance = transform:FindChild('Lose/PromoteTips/Enhance').gameObject

	self.uiButtonAgain = transform:FindChild('ButtonAgain/ButtonAgain').gameObject
	self.uiButtonConfirm = transform:FindChild('ButtonConfirm/ButtonConfirm').gameObject
	self.uiButtonConfirm1 = transform:FindChild('ButtonConfirm1/ButtonConfirm1').gameObject
	self.uiBtnEnhance = self.transform:FindChild("Window/Lose/PromoteTips/Enhance")
	self.uiBtnTraining = self.transform:FindChild("Window/Lose/PromoteTips/Training")
	self.uiBtnAdvance = self.transform:FindChild("Window/Lose/PromoteTips/Advance")

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIMatchResult:Start()
	-- addOnClick(self.uiLosePromoteTraining, self:OnTrainingClick())
	-- addOnClick(self.uiLosePromoteAdvance, self:OnAdvanceClick())
	-- addOnClick(self.uiLosePromoteEnhance, self:OnEnhanceClick())
	addOnClick(self.uiButtonAgain, self:OnAgainClick())
	addOnClick(self.uiButtonConfirm, self:OnConfirmClick())
	addOnClick(self.uiButtonConfirm1, self:OnConfirmClick())

	self.uiWinStarsNode.gameObject:SetActive(false)
	NGUITools.SetActive(self.uiButtonConfirm1 , false)
	if self.isWin == false then
		addOnClick(self.uiBtnEnhance.gameObject, self:OnEnhanceClick())
		addOnClick(self.uiBtnTraining.gameObject, self:OnTrainingClick())
		addOnClick(self.uiBtnAdvance.gameObject, self:OnAdvanceClick())
	end
	if self.leagueType == GameMatch.LeagueType.eTour or
		self.leagueType == GameMatch.LeagueType.eQualifying then
		NGUITools.SetActive(self.uiButtonAgain , false)
		NGUITools.SetActive(self.uiButtonConfirm , false)
		NGUITools.SetActive(self.uiButtonConfirm1 , true)
	elseif self.leagueType == GameMatch.LeagueType.ePractise then
		if GuideSystem.Instance.curModule then
			NGUITools.SetActive(self.uiButtonAgain, false)
			NGUITools.SetActive(self.uiButtonConfirm , false)
			NGUITools.SetActive(self.uiButtonConfirm1 , true)
		end
	elseif self.leagueType == GameMatch.LeagueType.eCareer and
			GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID).type == 2 then -- Ò»´ÎÐÔ¹Ø¿¨²»ÄÜÖØÐÂ¿ªÊ¼,ÐÇµÈ
		if MainPlayer.Instance:CheckSectionComplete(CurChapterID, CurSectionID) or self.isWin then
			NGUITools.SetActive(self.uiButtonAgain , false)
			NGUITools.SetActive(self.uiButtonConfirm , false)
			NGUITools.SetActive(self.uiButtonConfirm1 , true)
		end
		NGUITools.SetActive(self.uiWinTop.gameObject, false)
	end
	-- self:SetExpData()
	self:Refresh()
	self:SetExpData()
end


function UIMatchResult:FixedUpdate()
	-- body
	if self.isShowExp == nil and self.isWin == true then
		self:ShowExp()
		self.isShowExp = true
	end
	
	if self.addExp then
		if self.addExp > 0 and self:IsAnimation() == false then
			self.startExp = self.startExp + 2
			if self.startExp >= self.maxExp then
				self.startExp = 0
				self.maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
				self.curLevel = self.curLevel + 1
				self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
			end
			self.uiWinExp.value = self.startExp / self.maxExp
			self.addExp = self.addExp - 2
		else
			self.uiWinExp.value = self.startExp / self.maxExp
		end
		if MainPlayer.Instance.Level ~= self.maxPlayerLv then
			self.uiWinExpLabel.text = self.startExp .. '/' .. self.maxExp
		else
			self.uiWinExp.value = 1.0 
			self.uiWinExpLabel.text = "MAX"
		end
		print(self.uiName,"------------progress value:",self.uiWinExp.value)	
	end
end

function UIMatchResult:OnClose()
	--jumpToUI(self.leagueType)
	self:EndGame()()
end

function UIMatchResult:OnDestroy()
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIMatchResult:Refresh()
	self.uiWin:SetActive(self.isWin)
	self.uiLose:SetActive(not self.isWin)


	if self.isWin then
		print("self.leagueType = ", self.leagueType)
		if self.leagueType == GameMatch.LeagueType.eCareer then
			self:RefreshWinCareer()
		elseif self.leagueType == GameMatch.LeagueType.eTour then
			self:RefreshWinTour()
			MainPlayer.Instance.MaxTourID = MainPlayer.Instance.CurTourID
			if  MainPlayer.Instance.CurTourID < 5 then
				MainPlayer.Instance.CurTourID = MainPlayer.Instance.CurTourID + 1
			else
				MainPlayer.Instance.CurTourID = 0
			end
			-- MainPlayer.Instance.MaxTourID = MainPlayer.Instance.CurTourID
			-- MainPlayer.Instance.TourFailTimes = 0
		elseif self.leagueType == GameMatch.LeagueType.eQualifying then
			self:RefreshWinQualifying()
		elseif self.leagueType == GameMatch.LeagueType.ePractise then
			self:RefreshPractise()
		elseif self.leagueType == GameMatch.LeagueType.eBullFight then
			self:RefreshBullFight()
		elseif self.leagueType == GameMatch.LeagueType.eShoot then
			self:RefreshShoot()
		end
	else
		if self.leagueType == GameMatch.LeagueType.eTour then
			MainPlayer.Instance.CurTourID = 0
		end
		NGUITools.SetActive(self.btnSkill, GameSystem.Instance.FunctionConditionConfig:ValidateFunc("UISkill"))
		NGUITools.SetActive(self.btnTattoo, GameSystem.Instance.FunctionConditionConfig:ValidateFunc("UITattoo"))
		NGUITools.SetActive(self.btnTraining, GameSystem.Instance.FunctionConditionConfig:ValidateFunc("UITraining"))
	end

	if self.isWin ==false and self.leagueType == GameMatch.LeagueType.eTour then
		MainPlayer.Instance.TourFailTimes = MainPlayer.Instance.TourFailTimes + 1
	end

	GuideSystem.Instance:ReqBeginGuide(self.uiName)
end


-----------------------------------------------------------------
function UIMatchResult:RefreshWinCareer()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp
	local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID)
	local section = MainPlayer.Instance:GetSection(CurChapterID, CurSectionID)

    local sectionType = GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID).type
	self.uiGetGoodsText.gameObject:SetActive(sectionType == 2)
	self.uiWinStarsNode.gameObject:SetActive(sectionType ~= 2)
	
	local stars = 3
	for i = 1, 3 do
		if i > section.medal_rank then
			self.uiWinStars[i].spriteName = "match_result_star5_gray"
			stars = stars - 1
		end
	end
	if stars > 0 then
		UpdateRedDotHandler.MessageHandler("Career")
	end

	local id, value
	id = sectionConfig.one_star_id
	value = sectionConfig.one_star_value
	self.uiWinStarLabel[1].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)
	id = sectionConfig.two_star_id
	value =  sectionConfig.two_star_value
	self.uiWinStarLabel[2].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)
	id = sectionConfig.three_star_id
	value =  sectionConfig.three_star_value
	self.uiWinStarLabel[3].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)


	self:ShowAwards()

	local gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
	local modeType = GameMode.IntToEnum(enumToInt(gameMode.matchType))
	-- local s, fightRoleInfoList = MainPlayer.Instance.FightRoleList:TryGetValue(modeType)
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	-- item.npc = false
	--	if self.increaseExp and self.increaseExp ~= 0 then
	--		item.addExp = self.increaseExp
	--	end
	--	-- item.aptitudeSprite.gameObject:SetActive(false)
	--	-- local npc_id = enum.Current.role_id
	--	-- print('npc_id = ' .. npc_id)
	--	-- item:update_by_id(npc_id)
	-- end
end

function UIMatchResult:RefreshWinTour()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp


	for i = 1, 3 do
		NGUITools.SetActive(self.uiWinStars[i].gameObject, false)
	end

	self:ShowAwards()

	local tourData = GameSystem.Instance.TourConfig:GetTourData(MainPlayer.Instance.Level, MainPlayer.Instance.CurTourID)
	local gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(tourData.gameModeID)
	local modeType = GameMode.IntToEnum(enumToInt(gameMode.matchType))
	-- local s, fightRoleInfoList = MainPlayer.Instance.FightRoleList:TryGetValue(modeType)
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	item.addExp = self.increaseExp
	-- end
end

function UIMatchResult:RefreshPractise()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp
	for i = 1, 3 do
		NGUITools.SetActive(self.uiWinStars[i].gameObject, false)
	end
	-- local fightRoleInfoList = MainPlayer.Instance.SquadInfo
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	item.addExp = 0
	-- end
end

function UIMatchResult:RefreshBullFight()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp
	for i = 1, 3 do
		NGUITools.SetActive(self.uiWinStars[i].gameObject, false)
	end
	local modeType = GameMode.GM_BullFight
	-- local s, fightRoleInfoList = MainPlayer.Instance.FightRoleList:TryGetValue(modeType)
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	item.addExp = self.increaseExp
	-- end

	self:ShowAwards()
end


function UIMatchResult:RefreshShoot()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp
	for i = 1, 3 do
		NGUITools.SetActive(self.uiWinStars[i].gameObject, false)
	end

	local modeType = MainPlayer.Instance.curShootGameMode
	-- local s, fightRoleInfoList = MainPlayer.Instance.FightRoleList:TryGetValue(modeType)
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	item.addExp = self.increaseExp
	-- end

	self:ShowAwards()
end

function UIMatchResult:RefreshWinQualifying()
	-- self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	-- local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	-- local curExp = MainPlayer.Instance.Exp
	-- for i=1, MainPlayer.Instance.Level - 1 do
	-- 	--maxExp = maxExp + GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- 	curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	-- end
	-- self.uiWinExp.value = curExp / maxExp
	-- self.uiWinExpLabel.text = curExp .. '/' .. maxExp
	for i = 1, 3 do
		NGUITools.SetActive(self.uiWinStars[i].gameObject, false)
	end

	-- local modeType = GameMode.GM_3On3
	-- local s, fightRoleInfoList = MainPlayer.Instance.FightRoleList:TryGetValue(modeType)
	-- local enum = fightRoleInfoList:GetEnumerator()
	-- while enum:MoveNext() do
	--	local item = getLuaComponent(createUI("CareerIconItem", self.uiWinRoleList.transform))
	--	item.id = enum.Current.role_id
	--	item.npc = false
	--	item.addExp = self.increaseExp
	-- end
	self:ShowAwards()
end

function UIMatchResult:OnAgainClick()
	return function ()
		-- hander shoot again
		if self.leagueType == GameMatch.LeagueType.eShoot then
			-- Check the Condistion.
			local enum = MainPlayer.Instance.ShootGameModeInfo:GetEnumerator()
			local gameMode = nil
			local playedTimes
			while enum:MoveNext() do
			-- Play times
				local gameModeInfo = enum.Current
				playedTimes = gameModeInfo.times
				break
			end
			if ShootAllowTimes <= playedTimes then
				CommonFunction.ShowTip(getCommonStr("STR_NO_FREE_TODAY"), nil)
				return
			end
		end

		self.onAgain = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMatchResult:OnConfirmClick()
	return function ()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMatchResult:OnEnhanceClick()
	return function ()
		self.jumpToRole = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMatchResult:OnTrainingClick()
	return function ()
		self.jumpToTraing = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMatchResult:OnAdvanceClick()
	return function ()
		self.jumpToSquad = true
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIMatchResult:MakeOnSkill()
	return function ()
		jumpToUI("UISkill")
	end
end

function UIMatchResult:MakeOnTattoo()
	return function ()
		jumpToUI("UITattoo")
	end
end

function UIMatchResult:MakeOnTraining()
	return function ()
		print("MakeOnTraining")
		jumpToUI("UIRole")
	end
end

function UIMatchResult:EndGame()
	return function ()
		if self.onAgain then
			if self.leagueType == GameMatch.LeagueType.eCareer then
				jumpToUI("UICareer", nil, {reStart = true})
			elseif self.leagueType == GameMatch.LeagueType.ePractise then
				GameSystem.Instance.mClient.mCurMatch.m_StateMachine.m_curState:SendPractiseResult()
			elseif self.leagueType == GameMatch.LeagueType.eShoot then
				jumpToUI("UIShootGame",nil, {reStart = true})
			elseif self.leagueType == GameMatch.LeagueType.eBullFight then
				self:SendGetNPC()
			end
			return
		end

		if self.jumpToRole == true then
			jumpToUI("UIRole", nil, {defaultEnhance = true})
			MainPlayer.Instance.LinkRoleId       = 0
			MainPlayer.Instance.LinkExerciseId   = 0
			MainPlayer.Instance.LinkExerciseLeft = true
			MainPlayer.Instance.LinkTab          = 0
			MainPlayer.Instance.LinkUiName       = ""
			MainPlayer.Instance.LinkEnable       = false
			return
		elseif self.jumpToSquad == true then
			jumpToUI("UISquad")
			return
		elseif self.jumpToTraing == true then
			jumpToUI("UIRole", nil, {defaultExercise = true})
			MainPlayer.Instance.LinkRoleId       = 0
			MainPlayer.Instance.LinkExerciseId   = 0
			MainPlayer.Instance.LinkExerciseLeft = true
			MainPlayer.Instance.LinkTab          = 0
			MainPlayer.Instance.LinkUiName       = ""
			MainPlayer.Instance.LinkEnable       = false
			return
		end

		if self.leagueType == GameMatch.LeagueType.eCareer then
			print("CurChapterID ,CurSectionID:",CurChapterID,"--",CurSectionID)
			local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
			local chapterID
			while enum:MoveNext() do
				if MainPlayer.Instance:CheckChapter(enum.Current.Key) then
					chapterID = enum.Current.Key
				else
					break
				end
			end
			if CurChapterID ~= chapterID and self.firstComplete == false then
				--local subID = CurChapterID % 10000
				jumpToUI("UICareer", CurChapterID)
			else
				--local subID = chapterID % 10000
				jumpToUI("UICareer", chapterID)
			end
		elseif self.leagueType == GameMatch.LeagueType.eTour then
			if self.isWin and MainPlayer.Instance.CurTourID == 0 then
				jumpToUI("UITour", nil , {showCompletePopup = true, uiBack = "UICompetition"})
			else
				jumpToUI("UITour", nil, {uiBack = "UICompetition"})
			end
		elseif self.leagueType == GameMatch.LeagueType.eQualifying then
			jumpToUI("UIQualifying")
		elseif self.leagueType == GameMatch.LeagueType.ePractise then
			GameSystem.Instance.mClient.mCurMatch.m_StateMachine.m_curState:SendPractiseResult()
		elseif self.leagueType == GameMatch.LeagueType.eBullFight then
			self:SendGetNPC()
		elseif self.leagueType == GameMatch.LeagueType.eShoot then
			jumpToUI("UIShootGame", nil, {uiBack = "UICompetition"})
		end
	end
end

function UIMatchResult:SendGetNPC()
	local GetBullFightNpcReq = {
			acc_id = MainPlayer.Instance.AccountID
		}
	local req = protobuf.encode("fogs.proto.msg.GetBullFightNpcReq",GetBullFightNpcReq)
	LuaHelper.SendPlatMsgFromLua(MsgID.GetBullFightNpcReqID,req)
	LuaHelper.RegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self:HandleBullFightGetNPC(), self.uiName)
	CommonFunction.ShowWaitMask()
end

function UIMatchResult:HandleBullFightGetNPC()
	return function(buf)
		print("HandleBullFightGetNPC called()")
		CommonFunction.HideWaitMask()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.GetBullFightNpcResp", buf)
		print("HandleBullFightGetNPC resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance.BullFightNpc:Clear()
				for k, v in ipairs(resp.npc) do
					MainPlayer.Instance.BullFightNpc:Add(v)
				end
				if self.onAgain then
					jumpToUI("UIBullFight", nil , {isShoot = false, reStart = true})
				else
					jumpToUI("UIBullFight", nil, {uiBack = "UICompetition"})
				end
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
			end
		else
			error("UIMatchResult:HandleBullFightGetNPC -handler", err)
		end
	end			
end

function UIMatchResult:MakeOnRoleSelected()
	local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID)
	local gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
	self.game_mode_id = sectionConfig.game_mode_id
	local modeType = GameMode.IntToEnum(enumToInt(gameMode.matchType))
	local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(modeType)
	local enum = fightRoleInfoList:GetEnumerator()
	local i = 1
	self.fightList = {}
	while enum:MoveNext() do
		self.fightList[i] = {}
		self.fightList[i].role_id = enum.Current.role_id
		self.fightList[i].status = enum.Current.status:ToString()
		i = i + 1
	end
	local career = {
		chapter_id = CurChapterID,
		section_id = CurSectionID,
		fight_list = {
			game_mode = modeType:ToString(),
			fighters = self.fightList,
		}
	}
	local enterGame = {
		acc_id = MainPlayer.Instance.AccountID,
		type = 'MT_CAREER',

		career = career,
		game_mode = modeType:ToString(),
	}

	--local buf = protobuf.encode("fogs.proto.msg.StartSectionMatch", req)
	--LuaHelper.SendPlatMsgFromLua(MsgID.StartSectionMatchID, buf)
	local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
	LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
	LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeStartMatchHandler(), self.uiName)
	CommonFunction.ShowWaitMask()
end

function UIMatchResult:MakeStartMatchHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		--print("lllllllllllllllaaaaaa:",resp.result)
		if resp then
			if resp.result == 0 then
				self:SectionStart(resp.career.session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UICareerSection: ", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	end
end

function UIMatchResult:SectionStart(session_id)
	--print("starstarstarstarstarstar")
	local section = MainPlayer.Instance:GetSection(CurChapterID, CurSectionID)
	local needPlot = not section.is_complete
	--²»ÏÈ×ª´æÒ»´Î£¬UnityÒª±À¡£¡£¡£Òª±À¡£¡£
	local teammates = UintList.New()
	for _,v in pairs(self.fightList) do
		teammates:Add(tonumber(v.role_id))
	end
	--set global
	--CurLoadingImage = "Texture/LoadShow"
	GameSystem.Instance.mClient:CreateNewMatch(self.game_mode_id, session_id, needPlot, GameMatch.LeagueType.eCareer, teammates, nil)
end

function UIMatchResult:ShowAwards()
	print("awards count:",self.awards.Count)
	local enum = self.awards:GetEnumerator()
	while enum:MoveNext() do
		print("Current id:",enum.Current.id,"--value:",enum.Current.value)
		if (enum.Current.id < 4012 or enum.Current.id > 4015) and (enum.Current.id ~= 6) then
			if enum.Current.id == 5 then
				if self.uiWinGrid:FindChild("1") then
					local obj = getLuaComponent(self.uiWinGrid:FindChild("1"))
					obj.rewardNum = obj.rewardNum + enum.Current.value
					obj:Refresh()
					self.addExp = obj.rewardNum
				else
					local goods = getLuaComponent(createUI("GoodsIconConsume", self.uiWinGrid))
					goods.rewardId = 5
					goods.rewardNum = enum.Current.value
					goods.transform.gameObject.name = 1
					self.addExp = goods.rewardNum
				end
			elseif enum.Current.id == 2 then
				if self.uiWinGrid:FindChild("2") then
					local obj = getLuaComponent(self.uiWinGrid:FindChild("2"))
					obj.rewardNum = obj.rewardNum + enum.Current.value
					obj:Refresh()
				else
					local goods = getLuaComponent(createUI("GoodsIconConsume", self.uiWinGrid))
					goods.rewardId = 2
					goods.rewardNum = enum.Current.value
					goods.transform.gameObject.name = 2
				end
			else
				local goods = getLuaComponent(createUI('GoodsIcon',self.uiWinGoodsList.transform))
				goods.goodsID = enum.Current.id
				goods.num = enum.Current.value
				goods.hideLevel = true
				goods.hideNum = false
				goods.hideNeed = true
			end
		end
	end
	self.uiWinGoodsList.repositionNow = true
	self.uiWinGoodsScroll:ResetPosition()
end

function UIMatchResult:SetExpData()
	-- if self.isWin == false or PreExp == nil then
	-- 	return
	-- end
	if self.addExp == nil then
		return
	end
----------------经验条动画初始化
	self.nowExp = MainPlayer.Instance.Exp
	self.startExp = self.nowExp - self.addExp
	--设置开始经验值starExp
	local level = MainPlayer.Instance.Level
	self.curLevel = level
	if level > 1 and self.startExp < GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level - 1) then
		self.maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level - 1)
		self.curLevel = level - 1
		for i = 1, (level - 2) do
			self.startExp = self.startExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
		end
	else
		self.maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level)

		for i = 1, (level - 1 ) do
			self.startExp = self.startExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
		end
	end

	--设置当前经验值

	local curExp = self.nowExp
	for i = 1, MainPlayer.Instance.Level - 1 do
		curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	end

	--当前经验值显示
	local max = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
	self.uiWinExp.value = curExp / max
	--------------------------------------------------------
end

function UIMatchResult:IsAnimation()
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

function UIMatchResult:ShowExp()
	if self.addExp and self.addExp > 0 then
		self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), self.curLevel)
	else
		self.uiWinLevel.text = string.format(getCommonStr('STR_TEAM_LEVEL'), MainPlayer.Instance.Level)
		local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(MainPlayer.Instance.Level)
		local curExp = MainPlayer.Instance.Exp
		for i=1, MainPlayer.Instance.Level - 1 do
			curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
		end
		if MainPlayer.Instance.Level ~= self.maxPlayerLv then
			self.uiWinExp.value = curExp / maxExp
			self.uiWinExpLabel.text = curExp .. '/' .. maxExp
		else
			self.uiWinExp.value = 1.0
			self.uiWinExpLabel.text = "MAX"
		end
	end
end

return UIMatchResult

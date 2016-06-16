require "Custom/Career/CareerUtil"
require "Custom/Team/UICreateTeam"
require "common/StringUtil"

TourSectionPopup = {
	uiName = "TourSectionPopup",
	maxTourID = 5,
	-----------------UI
	uiStartLabel,
	uiRemainLabel,
	uiModeTitle,
	uiTitle,
	uiTeamName,
	uiRoundTitle,
	uiAwardsLabel,
	uiAwardsGrid,
	uiCloseGrid,
	uiRoleGrid,
	uiBtnStart,
	-----------------parameters
	tourID = 0,
	is_current = true,
	onClose,
	-----------------variable
	roleNum = 1,
	modeType = GameMode.GM_None,
	npcForShow = nil,
	npcForGame = nil,
}

function TourSectionPopup:Awake()
	self.uiStartLabel = self.transform:FindChild("Window/Middle/Right/Start/Text"):GetComponent("MultiLabel")
	self.uiStartLabel:SetText(getCommonStr("CAREER_BUTTON_START"))
	self.uiRemainLabel = self.transform:FindChild("Window/Middle/RemainTimes"):GetComponent("UILabel")
	self.uiModeTitle = self.transform:FindChild("Window/Top/Text"):GetComponent("MultiLabel")
	self.uiTitle = self.transform:FindChild("Window/Middle/Title"):GetComponent("MultiLabel")
	self.uiTeamName = self.transform:FindChild("Window/Middle/TeamName"):GetComponent("MultiLabel")
	self.uiTitle:SetText( getCommonStr("STR_SECTION_DETAIL"))
	self.uiRoundTitle = self.transform:FindChild("Window/Title"):GetComponent("MultiLabel")
	self.uiAwardsLabel = getComponentInChild(self.transform, "Window/Middle/Award/Text", "UILabel")
	self.uiAwardsGrid = getComponentInChild(self.transform, "Window/Middle/Award/Scroll/Grid", "UIGrid")
	self.uiCloseGrid = self.transform:FindChild("Window/ButtonClose")
	self.uiRoleGrid = self.transform:FindChild("Window/Middle/Enemy/Scroll/Grid")
	self.uiBtnStart = self.transform:FindChild("Window/Middle/Right/Start").gameObject
	self.uiIcon = self.transform:FindChild("Window/Top/SectionIcon"):GetComponent("UISprite")
	--self.rule = self.transform:FindChild("SectionInfo/Top/Rule").gameObject
	--self.sweep = self.transform:FindChild("SectionInfo/Right/Sweep").gameObject

	--addOnClick(self.sweep,self:MakeOnSweep())
	addOnClick(self.uiBtnStart,self:MakeOnStart())
	--addOnClick(self.rule,self:MakeOnRule())
end

function TourSectionPopup:Start()
	self.modeStr = {
		"MATCH_TYPE_NAME_eReboundStorm",
		"MATCH_TYPE_NAME_eBlockStorm",
		"MATCH_TYPE_NAME_eUltimate21",
		"MATCH_TYPE_NAME_eMassBall",
		"MATCH_TYPE_NAME_eGrabZone",
		"MATCH_TYPE_NAME_eGrabPoint",
		"MATCH_TYPE_NAME_eBullFight",
		"MATCH_TYPE_NAME_eCareer3On3",
	}
	self.uiTeamName:SetText(UICreateTeam.GenerateName())
	---create btn
	local close = getLuaComponent(createUI("ButtonClose",self.uiCloseGrid))
	close.onClick = self:MakeOnClose()
	--label
	self.uiRoundTitle:SetText( getCommonStr("TOUR_ROUND_NUM"):format(self.tourID))
	self.uiAwardsLabel.text = getCommonStr("STR_REWARDED")
	----------------------
	self.curTourID = MainPlayer.Instance.CurTourID
	self.tourData = GameSystem.Instance.TourConfig:GetTourData(MainPlayer.Instance.Level, self.tourID)
	if not self.tourData then error(self.uiName, "Can't find tour data. Level:", MainPlayer.Instance.Level, "ID:", self.tourID) end
	self.gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(self.tourData.gameModeID)
	self.modeType = GameMode.IntToEnum(enumToInt(self.gameMode.matchType))
	--print(self.uiName,"---id:",self.tourData.gameModeID)

	local icon = Split(self[self.gameMode.scene], "_")
	local at
	if icon[2] == "bg1" then
		at = "Atlas/career/career"
	elseif icon[2] == "bg2" then
		at = "Atlas/career/career2"
	end
	self.uiIcon.atlas = ResourceLoadManager.Instance:GetAtlas(at)
	self.uiIcon.spriteName = self[self.gameMode.scene]
	local index = 0
	if enumToInt( self.modeType) == 6 then
		index = 8
	else
		index = enumToInt( self.modeType) % 100 + 1
	end
	self.uiModeTitle:SetText( getCommonStr( self.modeStr[ index]))
	print("GameMode:",getCommonStr( self.modeStr[ index]))
	----show awards
	local awardsTable = {}
	local enum = self.tourData.winAwards:GetEnumerator()
	while enum:MoveNext() do
		local pack = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(enum.Current)
		local enum_pack = pack:GetEnumerator()
		while enum_pack:MoveNext() do
			local id = enum_pack.Current.award_id
			local value = enum_pack.Current.award_value
			awardsTable[id] = value
		end
		-- local id = pack[0].award_id
		-- local value = pack[0].award_value
		-- awardsTable[id] = value
	end
	CareerUtil.ListAwards(awardsTable, self.uiAwardsGrid.transform, true, false)
	self.uiAwardsGrid:Reposition()
	self:Refresh()
end

function TourSectionPopup:Refresh()
	if self.is_current == false then
		NGUITools.Destroy(self.uiBtnStart)
	end
	self:ShowRival()
end

function TourSectionPopup:ShowRival()
	self.npcForGame = UintList.New()
	self.npcForShow = {}
	print(self.uiName, "matchType:",self.gameMode.matchType)
	if self.gameMode.matchType == GameMatch.Type.eCareer3On3
		or self.gameMode.matchType == GameMatch.Type.e3On3
		or self.gameMode.matchType == GameMatch.Type.eAsynPVP3On3 then
		self.roleNum = 3
	end
	local enum = MainPlayer.Instance.tourInfo.npc:GetEnumerator()
	while enum:MoveNext() do
		self.npcForGame:Add(enum.Current)
		table.insert(self.npcForShow, enum.Current)
		if self.roleNum == 1 then
			break
		end
	end

	local starEnum = self.tourData.star:GetEnumerator()
	local qualityEnum = self.tourData.quality:GetEnumerator()

	for _, id in ipairs(self.npcForShow) do
		starEnum:MoveNext()
		qualityEnum:MoveNext()
		local role = getLuaComponent(createUI("CareerRoleIcon",self.uiRoleGrid))
		role.id = id
		role.npc = false
		if starEnum and starEnum.Current then
			role.qStar = starEnum.Current
		end
		if qualityEnum and qualityEnum.Current then
			role.qQuality = qualityEnum.Current
		end
		role.qLevel = MainPlayer.Instance.Level
		print("RoleId:", id)
	end
end

function TourSectionPopup:MakeOnClose()
	return function ()
		if self.onClose then
			self.onClose()
		end
		NGUITools.Destroy(self.gameObject)
	end
end

function TourSectionPopup:OnDestroy()
	--body

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function TourSectionPopup:MakeOnStart()
	return function ()
		if self.modeType == GameMode.GM_Career3On3 then
			self.roles = {}
			local squadInfo = MainPlayer.Instance.SquadInfo
			local enum = squadInfo:GetEnumerator()
			while enum:MoveNext() do
				if enum.Current.status == FightStatus.FS_MAIN then
					self.roles[1] = enum.Current.role_id
				elseif enum.Current.status == FightStatus.FS_ASSIST1 then
					self.roles[2] = enum.Current.role_id
				elseif enum.Current.status == FightStatus.FS_ASSIST2 then
					self.roles[3] = enum.Current.role_id
				end
			end
			local fightList = {
			game_mode = self.modeType:ToString(),
			fighters = {
					{role_id = self.roles[1],	status = "FS_MAIN",},
					{role_id = self.roles[2],	status = "FS_ASSIST1",},
					{role_id = self.roles[3],	status = "FS_ASSIST2",},
				}
			}

			local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(GameMode.GM_Career3On3)
			if not fightRoleInfoList then
				fightRoleInfoList = FightRoleInfoList.New()
				MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
			end
			self:TourFillFighters(fightRoleInfoList, fightList.fighters)

			if self.starting then return end
			self.starting = true
			self:MakeOnRoleSelected(fightList)()

			LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeTourStartHandler(), self.uiName)
		else
			TopPanelManager:ShowPanel('UIChallenge',nil ,{  gameType = "Tour" ,
														modeType = self.modeType ,
														roleNum = self.roleNum,
														npcTable = self.npcForShow,
														npcToStart = self.npcForGame,
														tourID = self.curTourID,
														})
		end
	end
end

function TourSectionPopup:TourFillFighters( list , fighters)
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

function TourSectionPopup:MakeTourStartHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		self.starting = false
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		if resp then
			if resp.result == 0 then
				print("start!!!!")
				self:StartMatch(resp.tour.session_id, resp.tour.cur_tour_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("UITour:", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	end
end

function TourSectionPopup:MakeOnRoleSelected(fightList)
	return function ()
		local tour = {
			fight_list = fightList
		}
		local req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_TOUR',
			tour = tour,
			game_mode = self.modeType:ToString(),
		}

		print(self.uiName, "send enter game")
		local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID, buf)

		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function TourSectionPopup:StartMatch(session_id, tourID)
	assert(tourID == self.curTourID, "UITour: started tour not cur tour.")
	local teammates = UintList.New()
	teammates:Add(tonumber(self.roles[1]))
	teammates:Add(tonumber(self.roles[2]))
	teammates:Add(tonumber(self.roles[3]))
	CurLoadingImage = "Texture/LoadShow"
	--GameSystem.Instance.mClient:CreateNewMatchWithLoading(self.tourData.gameModeID, session_id, GameMatch.LeagueType.eTour, teammates, self.npcForGame)
	GameSystem.Instance.mClient:CreateNewMatch(self.tourData.gameModeID, session_id, false, GameMatch.LeagueType.eTour, teammates, self.npcForGame)
end

return TourSectionPopup

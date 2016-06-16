--encoding=utf-8
require "common/StringUtil"
require "Custom/Career/CareerUtil"

UICareerSection = {
	uiName = 'UICareerSection',

	---------------parameters
	chapterID = 0,
	sectionID = 0,
	click = true,
	is_boss = false,
	showRemain = false,
	hideStar = false,

	roleNum = 1, --参赛球员数量
	modeType = GM_None,
	enemylist = nil,
	btnMenu,
	onClose,
	---------------UI
	uiStars = nil,
	uiStarConditions = nil,
	uiTitle,
	uiScroll,
	uiGrid,
	uiBtnStart,
	uiAnimator,
	uiStar,
	uiGetAward,
}


-----------------------------------------------------------------
function UICareerSection:Awake()
	self.uiStars = {}
	self.uiAnimator = self.transform:GetComponent('Animator')
	self.transform = self.transform:FindChild("Window").transform
	for i = 1, 3 do
		self.uiStars[i] = self.transform:FindChild("Top/Star/"..i):GetComponent("UISprite")
	end
	self.uiStarConditions = {}
	for i = 1, 3 do
		self.uiStarConditions[i] = getComponentInChild(self.transform, "Middle/Stars/Star" .. i .. "/Text", "UILabel")
	end
	self.uiStar = self.transform:FindChild("Middle/Stars")
	--bg
	self.uiSectionBg = self.transform:FindChild("Top/SectionIcon"):GetComponent("UISprite")
	--grid
	self.uiScroll = getComponentInChild(self.transform, "Middle/Award/Scroll", "UIScrollView")
	self.uiGrid = getComponentInChild(self.uiScroll.gameObject.transform, "Grid", "UIGrid")
	self.uiEnemyGrid = self.transform:FindChild("Middle/Enemy/Scroll/Grid")
	--title
	self.uiTitle = getComponentInChild(self.transform, "Title", "MultiLabel")
	self.uiModeTitle = getComponentInChild(self.transform, "Top/Text", "MultiLabel")
	self.uiEnemyTitle = getComponentInChild(self.transform, "Middle/Enemy/Text", "UILabel")
	self.uiEnemyTitle.text = getCommonStr("QUALIFYING_RIVALINFO")
	self.uiAwardTitle = getComponentInChild(self.transform, "Middle/Award/Text", "UILabel")
	self.uiAwardTitle.text = getCommonStr("STR_FIELD_FIRST_WIN")
	--btn
	self.uiButtonClose = self.transform:FindChild("ButtonClose")
	self.uiBtnStart = getChildGameObject(self.transform, "Middle/Start") --开始
	self.uiGetAward = self.transform:FindChild("Middle/Award/Scroll/GetAward"):GetComponent('UISprite')
	addOnClick(self.uiBtnStart, self:MakeOnStart())
end

function UICareerSection:Start()
	-----hide star
	if self.hideStar == true then
		for i = 1, 3 do
			NGUITools.SetActive(self.uiStars[i].gameObject,false)
		end
		NGUITools.SetActive(self.uiStar.gameObject,false)
		self.uiAwardTitle.text = getCommonStr("STR_SINGLE_SECTION_ACQUIRE")
	end
	-------
	local ButtonClose = getLuaComponent(createUI("ButtonClose",self.uiButtonClose))
	ButtonClose.onClick = self:OnClickClose()
	--------enemy scroll
	self.sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.sectionID)
	self.gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(self.sectionConfig.game_mode_id)
	self.modeType = GameMode.IntToEnum(enumToInt(self.gameMode.matchType))
	self.section = MainPlayer.Instance:GetSection(self.chapterID, self.sectionID)

	--set bg
	local icon = Split(self.sectionConfig.icon_level, "_")
	local at
	if icon[2] == "bg1" then
		at = "Atlas/career/career"
	elseif icon[2] == "bg2" then
		at = "Atlas/career/career2"
	end
	self.uiSectionBg.atlas = ResourceLoadManager.Instance:GetAtlas(at)
	self.uiSectionBg.spriteName = self.sectionConfig.icon_level

	local matchType = self.gameMode.matchType

	self.uiModeTitle:SetText( getCommonStr("MATCH_TYPE_NAME_" .. tostring(matchType)))

	self.enemylist = {}
	if matchType == GameMatch.Type.e3On3
		or matchType == GameMatch.Type.eCareer3On3
		or matchType == GameMatch.Type.eAsynPVP3On3 then
		self.roleNum = 3
	end
	local MainPlayerId = MainPlayer.Instance.CaptainID
	local position = GameSystem.Instance.RoleBaseConfigData2:GetPosition(MainPlayerId)
	local npcId = self.gameMode:GetMappedNPC(position)
	if npcId ~= 0 then 
		table.insert(self.enemylist, npcId)
	else
		for i = 0, self.roleNum-1 do
			local enum = self.gameMode.unmappedNPC[i]:GetEnumerator()
			while enum:MoveNext() do
				table.insert(self.enemylist, enum.Current)
			end
		end
	end
	for _, npcId in ipairs(self.enemylist) do
		local item = getLuaComponent(createUI("CareerRoleIcon", self.uiEnemyGrid))
		item.npc = true
		item.id = npcId
		print("section npc id: " .. npcId)
	end
	--------
	self:Refresh()
end

function UICareerSection:FixedUpdate( ... )
	-- body
end

function UICareerSection:OnClose( ... )
	self:OnDestroy()
end

function UICareerSection:OnDestroy()
	if self.onClose then
		self.onClose()
	end
	
	Object.Destroy(self.uiAnimator)
	--Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UICareerSection:Refresh()
	--Title
	-- if self.uiTitle:GetText() ~= self.sectionConfig.name then
	--	self.uiTitle:SetText( self.sectionConfig.name)
	-- end
	self.uiTitle:SetText( self.sectionConfig.name)
	---------star conditions
	local id, value
	id = self.sectionConfig.one_star_id
	value = self.sectionConfig.one_star_value
	self.uiStarConditions[1].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)
	id = self.sectionConfig.two_star_id
	value =  self.sectionConfig.two_star_value
	self.uiStarConditions[2].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)
	id = self.sectionConfig.three_star_id
	value =  self.sectionConfig.three_star_value
	self.uiStarConditions[3].text = GameSystem.Instance.CareerConfigData.GetStarConditionString( id, value)

	--stars
	for i = 1,3 do
		if i<=self.section.medal_rank then
			self.uiStars[i].spriteName = "career_star"
		else
			self.uiStars[i].spriteName = "career_star_Dim"
		end
	end
	--awards
	if self.section.is_complete then
		NGUITools.SetActive(self.uiGetAward.gameObject, true)
		--self.uiAwardTitle.text = getCommonStr("STR_REWARDED_MAY")
	else
		NGUITools.SetActive(self.uiGetAward.gameObject, false)
		--self.uiAwardTitle.text = getCommonStr("STR_FIELD_FIRST_WIN")
	end
	CommonFunction.ClearGridChild(self.uiGrid.transform)
	--local awards = CareerConfig.GetSectionGoodsList(self.sectionID)
	local awards = CareerConfig.GetSectionAllGoodsList(self.sectionConfig.award_id)
	local awardsTable = {}
	local enum = awards:GetEnumerator()
	while enum:MoveNext() do
		--warning(enum.Current.award_id, enum.Current.award_value)
		awardsTable[enum.Current.award_id] = enum.Current.award_value
	end
	CareerUtil.ListAwards(awardsTable, self.uiGrid.transform , true , true, true)
	self.uiGrid:Reposition()
	self.uiScroll:ResetPosition()
end

-----------------------------------------------------------------
function UICareerSection:OnClickClose()
	return function()
	print("UICareerSection animator:",self.uiAnimator)
		if self.uiAnimator then
			print("UICareerSection animation close")
			self:AnimClose()
		else
			print("UICareerSection close")
			self:OnClose()
		end
	end
end

function UICareerSection:MakeOnStart()
	return function ()
		local titleStr = getCommonStr("MATCH_TYPE_NAME_" .. tostring(self.gameMode.matchType))
		local maxPlayerNum = 1
		if self.modeType == GameMode.GM_Career3On3 then
			maxPlayerNum = 3
		end
		local selectRole = TopPanelManager:ShowPanel("UISelectRole", nil, { maxSelection = maxPlayerNum, title=titleStr, noPlayerText = getCommonStr("PLEASE_SELECT_PLAYER_FOR_MATCH")})
		selectRole.onStart = self:StartAfterSelectRoles(selectRole)
		selectRole.sendChangeRole = true
	end
end

function UICareerSection:StartAfterSelectRoles(selectRole)
	return function (ids)
		selectRole:VisibleModel(false)
		self.roles = ids
		local fightList
		if self.modeType == GameMode.GM_Career3On3 then
			fightList = {
			game_mode = self.modeType:ToString(),
			fighters = {
					{role_id = self.roles[1],	status = "FS_MAIN",},
					{role_id = self.roles[2],	status = "FS_ASSIST1",},
					{role_id = self.roles[3],	status = "FS_ASSIST2",},
				}
			}
			local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(GameMode.GM_Career3On3)
			if fightRoleInfoList == nil then
				fightRoleInfoList = FightRoleInfoList.New()
				MainPlayer.Instance.FightRoleList:Add(self.modeType, fightRoleInfoList)
			end
			self:CareerFillFighters(fightRoleInfoList, fightList.fighters)
		else
			fightList = {
			game_mode = self.modeType:ToString(),
			fighters = {
					{role_id = self.roles[1],	status = "FS_MAIN",},
				}
			}
		end
		-- local squadInfo = MainPlayer.Instance.SquadInfo
		-- local enum = squadInfo:GetEnumerator()
		-- while enum:MoveNext() do
		-- 	if enum.Current.status == FightStatus.FS_MAIN then
		-- 		self.roles[1] = enum.Current.role_id
		-- 	elseif enum.Current.status == FightStatus.FS_ASSIST1 then
		-- 		self.roles[2] = enum.Current.role_id
		-- 	elseif enum.Current.status == FightStatus.FS_ASSIST2 then
		-- 		self.roles[3] = enum.Current.role_id
		-- 	end
		-- end

		self:MakeOnRoleSelected(fightList)()
	end
end

function UICareerSection:CareerFillFighters( list , fighters)
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

function UICareerSection:MakeOnRoleSelected(fightList)
	return function ()
		if self.starting then return end
		local career = {
			chapter_id = self.chapterID,
			section_id = self.sectionID,
			fight_list = fightList
		}

		local enterGame = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_CAREER',

			career = career,
			game_mode = self.modeType:ToString(),
		}

		--local buf = protobuf.encode("fogs.proto.msg.StartSectionMatch", req)
		--LuaHelper.SendPlatMsgFromLua(MsgID.StartSectionMatchID, buf)
		local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeStartMatchHandler(), self.uiName)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
		self.starting = true
	end
end

function UICareerSection:MakeStartMatchHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		self.starting = false
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

function UICareerSection:SectionStart(session_id)
	--print("starstarstarstarstarstar")
	local needPlot = not self.section.is_complete
	local teammates = UintList.New()
	--不先转存一次，Unity要崩。。。要崩。。
	local id1 = self.roles[1]
	teammates:Add(tonumber(id1))
	if table.getn(self.roles) == 3 then
		local id2 = self.roles[2]
		teammates:Add(tonumber(id2))
		local id3 = self.roles[3]
		teammates:Add(tonumber(id3))
	end
	--set global
	CurChapterID = self.chapterID
	CurSectionID = self.sectionID
	CurSectionComplete = not needPlot
	CurLoadingImage = "Texture/LoadShow"
	NGUITools.Destroy(self.btnMenu)
	GameSystem.Instance.mClient:CreateNewMatch(self.sectionConfig.game_mode_id, session_id, needPlot, GameMatch.LeagueType.eCareer, teammates, nil)
	UICareerSection.OnMatchCreated()
end

function UICareerSection.OnMatchCreated()
	if CurChapterID == 10001 and CurSectionID == 10111 then
		local exercised = ConditionValidator.Instance:Validate(ConditionType.AnyRoleAnyExercised, "")
		local guideCompleted = MainPlayer.Instance:IsGuideCompleted(10012)
		GameSystem.Instance.mClient.mCurMatch.neverWin = not (exercised or guideCompleted)
	end
end

function UICareerSection:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
	end

end

return UICareerSection

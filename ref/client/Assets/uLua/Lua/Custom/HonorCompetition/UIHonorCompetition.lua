--encoding=utf-8

UIHonorCompetition = UIHonorCompetition or
{	
	uiName = 'UIHonorCompetition',

	----------------------------------
	uiBack,
	
	----------------------------------
	curRankType,

	curRankList,
	lastRankList,

	firstRank,
	selectedRoles = {}

	----------------------------------UI
	--返回
	uiButtonBack,
	--资产
	uiProperty,
	--标题
	uiTitle,
	--荣誉商店
	uiButtonHonorStore,
	--规则
	uiButtonRules,
	--剩余次数
	uiRemainTimes,
	--次数购买
	uiButtonPlus,
	--开始比赛
	uiButtonStart,
	--
	uiSwitchCurrent,
	--背景色
	uiBackground,
	--队员一
	uiMember1,
	--队员二
	uiMember2,

	--排行榜
	uiRankList,
	uiButtonSwitchCurrent,
	uiButtonSwitchLast,
	uiAnimator,
}


-----------------------------------------------------------------
--Awake
function UIHonorCompetition:Awake( ... )
	local transform = self.transform

	--返回
	local go = createUI('ButtonBack', transform:FindChild('ButtonBack'))
	self.uiButtonBack = getLuaComponent(go)
	--self.uiButtonBack:set_back_icon("com_bg_pure_browndeep", 1.0)
	self.uiButtonBack.onClick = self:OnBackClick()	

	--资产信息
	self.uiProperty = createUI('PlayerProperty', transform:FindChild('PlayerInfo').transform)

	--标题
	self.uiTitle = transform:FindChild('Title'):GetComponent('UILabel')
	self.uiTitle.text = getCommonStr('STR_HONOR_COMPETITION')
	--荣誉商店
	self.uiButtonHonorStore = transform:FindChild('ButtonStore'):GetComponent('UIButton')
	addOnClick(self.uiButtonHonorStore.gameObject, self:OnHonorStoreClick())
	--争霸赛规则
	self.uiButtonRules = transform:FindChild('ButtonRules'):GetComponent('UIButton')
	addOnClick(self.uiButtonRules.gameObject, self:OnRulesClick())
	self.isonrule = false
	transform:FindChild('ButtonRules/Label'):GetComponent('UILabel').text = 
		getCommonStr('STR_HONOR_COMPETITION_RULES')
	--剩余次数
	self.uiRemainTimes = transform:FindChild('RemainTimes'):GetComponent('UILabel')
	self:RefreshChallengeTimes()
	self.uiButtonPlus = transform:FindChild('ButtonPlus'):GetComponent('UIButton')
	addOnClick(self.uiButtonPlus.gameObject, self:OnPlusClick())
	--开始
	self.uiButtonStart = transform:FindChild('ButtonStart'):GetComponent('UIButton')
	addOnClick(self.uiButtonStart.gameObject, self:OnStartClick())
	transform:FindChild('ButtonStart/Label'):GetComponent('UILabel').text = 
		getCommonStr('STR_START')
	--背景
	self.uiBackground = transform:FindChild('Background'):GetComponent('UISprite')
	--添加队员
	self.uiMember1 = transform:FindChild('Member1').transform:GetComponent('UIButton')
	self.uiMember2 = transform:FindChild('Member2').transform:GetComponent('UIButton')
	self.uiMember1card = transform:FindChild('Member1').transform:FindChild('Card').transform
	self.uiMember2card = transform:FindChild('Member2').transform:FindChild('Card').transform
	addOnClick(self.uiMember1.gameObject, self:OnMemberClick())
	addOnClick(self.uiMember2.gameObject, self:OnMemberClick())

	-----------------------------------------------------------
	--排行榜列表
	self.uiRankList = transform:FindChild('Ranking'):FindChild('RankDetail')
	local list = self.uiRankList.transform
	--排行按钮
	self.btnRank = list:FindChild('Button'):GetComponent('UIButton')
	self.btnRankLabel = list:FindChild('Button/Label'):GetComponent('UILabel')
	addOnClick(self.btnRank.gameObject, self:OnShowRankListClick())
	self.showRankList = false
	--排行榜切换按钮
	self.uiSwitchCurrent = list:FindChild('Switch/Current').gameObject
	addOnClick(self.uiSwitchCurrent.gameObject, self:OnSwitchCurrentClick())
	list:FindChild('Switch/Current/Label'):GetComponent('UILabel').text = 
		getCommonStr('STR_CURRENT_SCORE_RANK')

	local switchLast = list:FindChild('Switch/Last').gameObject
	addOnClick(switchLast.gameObject, self:OnSwitchLastClick())	
	list:FindChild('Switch/Last/Label'):GetComponent('UILabel').text = 
		getCommonStr('STR_LAST_SCORE_RANK')
	--排行标题
	list:FindChild('Bg/SymbolText/Label1'):GetComponent('UILabel').text = 
		getCommonStr('STR_RANKING')
	list:FindChild('Bg/SymbolText/Label2'):GetComponent('UILabel').text = 
		getCommonStr('CAPTAIN_NAME')
	list:FindChild('Bg/SymbolText/Label3'):GetComponent('UILabel').text = 
		getCommonStr('STR_SCORE')
	--[[list:FindChild('SymbolText/Label4'):GetComponent('UILabel').text = 
		getCommonStr('PLAYER_LEVEL')]]
	--我的荣誉排名
	local selfRank = list:FindChild('Top')
	self.myRank = selfRank:FindChild('Ranking'):GetComponent('UILabel')
	self.myName = selfRank:FindChild('Name'):GetComponent('UILabel')
	self.myScore = selfRank:FindChild('Score'):GetComponent('UILabel')

	--self:Refresh()

	self.uiAnimator = self.transform:GetComponent('Animator')
end

--Start
function UIHonorCompetition:Start( ... )
	--显示球员
	self.model = createUI('ModelShowItem', self.transform:FindChild('Model')):GetComponent('ModelShowItem')
end

--Update
function UIHonorCompetition:FixedUpdate( ... )
	-- body
end

function UIHonorCompetition:OnClose( ... )	
	self.selectedRoles[1] = nil
	self.selectedRoles[2] = nil
	if self.member then
		self.member:ClearSelectedList()
	end
	jumpToUI(self.uiBack)
end

function UIHonorCompetition:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--Refresh
function UIHonorCompetition:Refresh( ... )
	--默认显示本周排行
	self:OnSwitchCurrentClick()(self.uiSwitchCurrent)
	--显示队长模型
	self.model.ModelID = MainPlayer.Instance.CaptainID
	self.model.PlayNeedBall = false
	--根据队长位置设置背景
	self:SetRolebg()
	--self:InitRankingList()
	local localLua = getLuaComponent(self.uiProperty)
	localLua.showHonor = true
	if self.selectedRoles[1] ~= nil and self.selectedRoles[2] ~= nil then
		for i,v in ipairs(self.selectedRoles) do
			local item = nil
			if i == 1 then
				item = getLuaComponent(createUI('CaptainBustitem', self.uiMember1card))
			elseif i == 2 then
				item = getLuaComponent(createUI('CaptainBustitem', self.uiMember2card))
			end
			item:set_data(v, false)
			item.id = v
			item:refresh()
		end
	end
	localLua:Refresh()
end


-----------------------------------------------------------------
function UIHonorCompetition:SetRolebg( ... )
	-- 获取队长职位
	local position_type = GameSystem.Instance.RoleBaseConfigData2:GetPosition(MainPlayer.Instance.CaptainID)
	local position = enumToInt(position_type)
	local bgw = {"com_bg_pure_purplelight" , "com_bg_pure_bluelight" , "com_bg_pure_brownlight" , "com_bg_pure_greenlight" , "com_bg_pure_goldenlight"}
	local btnbg = {"com_bg_pure_purpledeep" , "com_bg_pure_bluedeep" , "com_bg_pure_browndeep" , "com_bg_pure_greendeep" , "com_bg_pure_goldendeep"}
	self.uiBackground.spriteName = bgw[position]
	self.uiButtonBack:set_back_icon(btnbg[position], 1.0)
end

--设置返回界面
function UIHonorCompetition:SetBackUI(uiBackName)
	self.uiBack = uiBackName
end

--返回点击处理
function UIHonorCompetition:OnBackClick()
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

--显示界面
function UIHonorCompetition:ShowUI()
	--创建任务界面
	TopPanelManager:ShowPanel(self.uiName)
end

--荣誉商店点击处理
function UIHonorCompetition:OnHonorStoreClick()
	return function (go)
		UIStore:SetType('ST_HONOR')
		UIStore:SetBackUI(self.uiName)
		UIStore:OpenStore()
	end
end

--比赛规则点击处理
function UIHonorCompetition:OnRulesClick()
	return function (go)
		local uiRulesParent = self.transform:FindChild('Rules')
		uiRulesParent.gameObject:SetActive(true)
		--规则按钮开关
		if self.isonrule == false then
			self.goRules = getLuaComponent(createUI('HonorCompetitionRules', uiRulesParent))
			self.goRules:Init()
			if self.model.gameObject.activeSelf == true then
				self.model.gameObject:SetActive(false)
			end
			self.isonrule = true
		else
			self.goRules:OnCloseClick()
			if self.showRankList == false then
				self.model.gameObject:SetActive(true)
			end
			self.isonrule = false
		end
	end
end

--购买挑战次数点击处理
function UIHonorCompetition:OnPlusClick()
	return function (go)
		local total_point = GameSystem.Instance.PVPPointConfig:GetLevelInfo(MainPlayer.Instance.Vip).point;
		if MainPlayer.Instance.PvpRunTimes < total_point then
			CommonFunction.ShowPopupMsg(
				CommonFunction.GetConstString("PVP_EXHOUST_POINT"),
				self.transform,nil,nil,nil,nil)
			return
		end

		local info = GameSystem.Instance.PVPPointConfig:GetLevelInfo(MainPlayer.Instance.Vip);
		local remain_times = info.max_charge_time - MainPlayer.Instance.PvpPointBuyTimes;
		if remain_times <= 0 then
			CommonFunction.ShowPopupMsg(
				CommonFunction.GetConstString("PVP_MAX_CHARGE_TIMES_REACHED"),
				self.transform,nil,nil,nil,nil)
			return
		end

		local cost = GameSystem.Instance.PVPPointConfig:GetChargeCost(MainPlayer.Instance.PvpPointBuyTimes + 1)

		local uiBuyTimesParent = self.transform:FindChild('BuyTimes')
		uiBuyTimesParent.gameObject:SetActive(true)
		local goBuyTimes = createUI('HonorCompetitionBuyTimes', uiBuyTimesParent)
		local buyTimes = getLuaComponent(goBuyTimes)
		buyTimes.parent = self

		buyTimes:Init(cost)
	end
end

--开始点击处理
function UIHonorCompetition:OnStartClick()
	return function (go)
		if self.enoughTimes <= 0 then
			CommonFunction.ShowPopupMsg(getCommonStr("NOT_ENOUGH_CHALLENGE_TIMES"),nil,nil,nil,nil,nil)
			return
		end
		if not self.selectedRoles[1] or not self.selectedRoles[2] then
			CommonFunction.ShowPopupMsg(getCommonStr("CAREER_NOT_ENOUGH_PLAYER"),nil,nil,nil,nil,nil)
			return
		else
			self.model.gameObject:SetActive(false)
		end
		self:MakeOnRoleSelected()
	end
end

function UIHonorCompetition:MakeOnRoleSelected( ... )
	local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(GameMode.GM_3On3)
	if not fightRoleInfoList then
		fightRoleInfoList = FightRoleInfoList.New()
		MainPlayer.Instance.FightRoleList:Add(GameMode.GM_3On3, fightRoleInfoList)
	end
	self:FillFightRoleInfoList(fightRoleInfoList)
	local honorCompetition = {
		type = 'MT_REGULAR',
		acc_id = MainPlayer.Instance.AccountID,
		fight_list = self:GetFightRoleInfoListTable(GameMode.GM_3On3)
	}
	local req = {
		acc_id = MainPlayer.Instance.AccountID,
		type = 'MT_REGULAR',
		honorCompetition = honorCompetition,
		game_mode = GameMode.GM_3On3:ToString(),
	}

	local buf = protobuf.encode("fogs.proto.msg.EnterGameReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID, buf)
	LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeHonorCompetitionStartHandler(), self.uiName)
	CommonFunction.ShowWaitMask()
	CommonFunction.ShowWait()
end

function UIHonorCompetition:FillFightRoleInfoList(list)
	list:Clear()
	local info = FightRole.New()
	info.role_id = MainPlayer.Instance.CaptainID
	info.status = FightStatus.FS_MAIN
	list:Add(info)
	info = FightRole.New()
	info.role_id = self.selectedRoles[1]
	info.status = FightStatus.FS_ASSIST1
	list:Add(info)
	info = FightRole.New()
	info.role_id = self.selectedRoles[2]
	info.status = FightStatus.FS_ASSIST2
	list:Add(info)
end

function UIHonorCompetition:GetFightRoleInfoListTable(gameMode)
	local list = {
		game_mode = gameMode:ToString(),
		fighters = {
			{role_id = MainPlayer.Instance.CaptainID, status = "FS_MAIN"},
			{role_id = self.selectedRoles[1], status = "FS_ASSIST1"},
			{role_id = self.selectedRoles[2], status = "FS_ASSIST2"},
		},
	}
	return list
end

--进入荣誉争霸赛处理
function UIHonorCompetition:MakeHonorCompetitionStartHandler( ... )
	return function (buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		if resp then
			if resp.type == 'MT_REGULAR' and resp.honorCompetition.result == 0 then
				self:StartMatch(resp.honorCompetition.rival_info, resp.honorCompetition.session_id)
			else
				print("result" .. resp.honorCompetition.result)
				CommonFunction.ShowPopupMsg(ErrorID.IntToEnum(resp.honorCompetition.result),nil,nil,nil,nil,nil)
			end
		end
	end
end

--进入loading界面
function UIHonorCompetition:StartMatch(rival_info, session_id)
	local teammates = UintList.New()
	if self.selectedRoles then
		local id1 = self.selectedRoles[1]
		local id2 = self.selectedRoles[2]
		teammates:Add(tonumber(id1))
		teammates:Add(tonumber(id2))
	end

	local matchInfo = rival_info[1]
	GameSystem.Instance.mClient:CreateNewHonorMatchWithLoading(matchInfo, matchInfo.members[1], matchInfo.members[2], matchInfo.members[3], session_id, teammates)
end

--本周排行点击处理
function UIHonorCompetition:OnSwitchCurrentClick()
	return function (go)
		self.curRankType = 'RT_CUR_REGULAR_POINTS'
		local current = self.uiSwitchCurrent.transform:GetComponent("UISprite")
		local currentlabel = current.gameObject.transform:FindChild('Label'):GetComponent("UILabel")
		local last = self.uiRankList.transform:FindChild('Switch/Last'):GetComponent("UISprite")
		local lastlabel = last.gameObject.transform:FindChild('Label'):GetComponent("UILabel")
		if self.curRankList == nil then
			self:RankListRequest()
			self.firstRank = true
		else
			self:InitRankingList()
		end
		current.spriteName = 'com_button_cutpage_property2'
		currentlabel.color = Color.New(0,0,0,1)
		currentlabel.effectStyle = UILabel.Effect.Outline
		currentlabel.effectColor = Color.New(0,0,0,1)
		last.spriteName = 'com_button_cutpage_property'
		lastlabel.color = Color.New(255,255,255,1)
	end
end

--上周排行点击处理
function UIHonorCompetition:OnSwitchLastClick()
	return function (go)
		self.curRankType = 'RT_LAST_REGULAR_POINTS'
		local current = self.uiSwitchCurrent.transform:GetComponent("UISprite")
		local currentlabel = current.gameObject.transform:FindChild('Label'):GetComponent("UILabel")
		local last = self.uiRankList.transform:FindChild('Switch/Last'):GetComponent("UISprite")
		local lastlabel = last.gameObject.transform:FindChild('Label'):GetComponent("UILabel")
		if self.lastRankList == nil then
			self:RankListRequest()
			self.firstRank = true
		else
			self:InitRankingList()
		end
		last.spriteName = 'com_button_cutpage_property2'
		lastlabel.color = Color.New(0,0,0,1)
		lastlabel.effectStyle = UILabel.Effect.Outline
		lastlabel.effectColor = Color.New(0,0,0,1)
		current.spriteName = 'com_button_cutpage_property'
		currentlabel.color = Color.New(255,255,255,1)
	end
end

--添加球员处理
function UIHonorCompetition:OnMemberClick( ... )
	return function (go)
		self.select = go
		if self.select == self.uiMember1.gameObject then
			self.memberid = 1
		else
			self.memberid = 2
		end
		self.member = TopPanelManager:ShowPanel('UIMember', nil, {chooseplayer = true, memberid = self.memberid})
		if self.selectedRoles[1] == nil and self.selectedRoles[2] == nil then
			self.member:ClearSelectedList()
		end
		self.member.on_choose = self:GetChoosePlayer()
	end
end

--点击排行榜按钮处理
function UIHonorCompetition:OnShowRankListClick( ... )
	return function (go)
		if self.showRankList == false then
			self.btnRankLabel.text = getCommonStr('STR_RANKLISTBACK')
			if self.model.gameObject.activeSelf == true then
				self.model.gameObject:SetActive(false)
			end
			self.uiMember1.gameObject:SetActive(false)
			self.uiMember2.gameObject:SetActive(false)
			self.showRankList = true
		else
			self.btnRankLabel.text = getCommonStr('STR_RANKLIST')
			if self.isonrule == false then
				self.model.gameObject:SetActive(true)
			end
			self.showRankList = false
			self.uiMember1.gameObject:SetActive(true)
			self.uiMember2.gameObject:SetActive(true)
		end
	end
end

--请求排行信息
function UIHonorCompetition:RankListRequest()
	local rankListReq = {  
		type = self.curRankType
	}
	local msg = protobuf.encode("fogs.proto.msg.RankListReq", rankListReq)
	LuaHelper.SendPlatMsgFromLua(MsgID.RankListReqID, msg)
	LuaHelper.RegisterPlatMsgHandler(MsgID.RankListRespID, self:RankListResp(), self.uiName)
	CommonFunction.ShowWait()
end

--
function UIHonorCompetition:RankListResp()
	--解析pb
	return function(message)
		local resp, err = protobuf.decode('fogs.proto.msg.RankListResp', message)
		CommonFunction.StopWait()
		if resp == nil then
			print('error -- RankListResp error: ', err)
			return
		end

		--local testt = protobuf.encode("fogs.proto.msg.RankListResp", resp)
		--print('------------testt: ', testt)
		--MainPlayer.Instance:SetRankingList(testt)
		if resp.result ~= 0 then
			print('error --  RankListResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		if resp.type == 'RT_CUR_REGULAR_POINTS' then
			self.curRankList = resp.rank_list
		elseif resp.type == 'RT_LAST_REGULAR_POINTS' then
			self.lastRankList = resp.rank_list
		else
			print('error --  invalid rank type: ', resp.type)
			return
		end

		if self.firstRank then
			self:InitRankingList()
			self.firstRank = false
		end
	end
end

function UIHonorCompetition:InitRankingList()
	local list = self.curRankType == 'RT_CUR_REGULAR_POINTS' and self.curRankList or self.lastRankList

	local parent = self.uiRankList.transform
	local uiRankSV = parent:FindChild('RankList'):GetComponent('UIScrollView')
	local uiRankGrid = parent:FindChild('RankList/Grid'):GetComponent('UIGrid')
	--local selfRanking = self.transform:FindChild('Ranking/RankDetail/Panel/SelfRanking'):GetComponent('UIGrid')

	CommonFunction.ClearGridChild(uiRankGrid.transform)
	--CommonFunction.ClearGridChild(selfRanking.transform)
	local barNum = 0
	local selfInit = false
	for k, v in pairs(list) do
		print('------------v.id : ', v.id)
		barNum = barNum + 1
		local item = self:InitRankingBar(k, v, uiRankGrid.transform, barNum)
		if item then
			item:SetDragSV(uiRankSV)
		end
	end
	local emptynum = 8 - barNum
	print("emptynum:" .. tostring(emptynum))
	if emptynum > 0 then
		for i=1,emptynum do
			local emptyitem = getLuaComponent(CommonFunction.InstantiateObject('Prefab/GUI/RankingBarItem', uiRankGrid.transform))
			if emptyitem then
				emptyitem:SetRanking('', barNum + i)
				emptyitem:SetDragSV(uiRankSV)
			end
		end
	end
	--如果list是空的或者第一次打荣誉争霸赛，初始化队长信息
	if next(list) == nil or self.myName.text ~= MainPlayer.Instance.Name .. '(' .. MainPlayer.Instance.Level .. ')' then
		self.myName.text = MainPlayer.Instance.Name .. '(' .. MainPlayer.Instance.Level .. ')'
		self.myRank.text = tostring('—')
		self.myScore.text = tostring('—')
	end
	uiRankGrid.repositionNow = true
	uiRankSV:ResetPosition()
end

function UIHonorCompetition:InitRankingBar(k, info, parent, barNum)
	local go = CommonFunction.InstantiateObject('Prefab/GUI/RankingBarItem', parent)
	if go == nil then
		Debugger.Log('-- InstantiateObject falied ')
		return
	end
	local item = getLuaComponent(go)
	item:SetRanking(k, barNum)
	--item:SetInfoLevel('lv.' .. info.level)
	item:SetInfoName(info.name .. '(' .. info.level .. ')')
	item:SetScore(info.points)
	if info.name == MainPlayer.Instance.Name then
		self:CapitalInfo(info, barNum)
	end
	return item
end

function UIHonorCompetition:RefreshChallengeTimes()
	local challengeTimes = GameSystem.Instance.PVPPointConfig:GetLevelInfo(MainPlayer.Instance.Vip).point;
	local leftTimes = challengeTimes - MainPlayer.Instance.PvpRunTimes
	self.enoughTimes = leftTimes
	if self.uiRemainTimes == nil then
		self.uiRemainTimes = self.transform:FindChild('RemainTimes'):GetComponent('UILabel')
	end
	self.uiRemainTimes.text = string.format(getCommonStr('STR_REMAIN_TIMES'), leftTimes, challengeTimes)
end

--
function UIHonorCompetition:EnterRoomResp()
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
			GameSystem.Instance.mClient:CreateNewMatch(GameMatch.Type.eReady);
		end

		LuaHelper.UnRegisterPlatMsgHandler(MsgID.AsynEnterRoomRespID, self.uiName)
	end
end

--显示队长信息
function UIHonorCompetition:CapitalInfo(info, rank)
	self.myRank.text = tostring(rank)
	self.myName.text = info.name .. '(' .. info.level .. ')'
	self.myScore.text = tostring(info.points)
end

--获取选择球员
function UIHonorCompetition:GetChoosePlayer( ... )
	return function (id)
		if self.select == self.uiMember1.gameObject then
			while self.uiMember1card.childCount > 0 do
				NGUITools.Destroy(self.uiMember1card:GetChild(0).gameObject)
			end
			local item1 = getLuaComponent(createUI('CaptainBustitem', self.uiMember1card))
			item1:set_data(id, false)
			item1.id = id
			self.selectedRoles[1] = item1.id
			item1:refresh()
		elseif self.select == self.uiMember2.gameObject then
			while self.uiMember2card.childCount > 0 do
				NGUITools.Destroy(self.uiMember2card:GetChild(0).gameObject)
			end
			local item2 = getLuaComponent(createUI('CaptainBustitem', self.uiMember2card))
			item2:set_data(id, false)
			item2.id = id
			self.selectedRoles[2] = item2.id
			item2:refresh()
		end
	end
end

function UIHonorCompetition:Destroy( ... )
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.AsynEnterRoomRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.RankListRespID, self.uiName)
end

return UIHonorCompetition

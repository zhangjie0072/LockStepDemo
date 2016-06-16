--encoding=utf-8

UIHonorCompetitionResult = UIHonorCompetitionResult or
{
	uiName = 'UIHonorCompetitionResult',

	----------------------------------
	uiBack,

	----------------------------------
	result, -- 1: win, 0: lose
	hasResult = false,

	matchData = {},

	----------------------------------UI
	--胜负背景
	uiHomeBg,
	uiAwayBg,
	--胜负图标
	uiHomeResult,
	uiAwayResult,
	--比赛数据
	uiMatchDis1,
	uiMatchDis2,
	uiMatchDis3,
	uiMatchDis4,
	uiMatchDis5,
	uiMatchDis6,

	--获得奖励提示
	uiAwardTips,
	--获得荣誉
	uiAwardHonorPoints,
	--确定
	uiButtonOK,

	--比赛结果
	uiResult,
	uiAnimator,
}


-----------------------------------------------------------------
--Awake
function UIHonorCompetitionResult:Awake( ... )
	local transform = self.transform

	--比赛数据
	self.uiMatchDis1 = transform:FindChild('MatchData/Data/Dis/Name'):GetComponent('UILabel')
	self.uiMatchDis1.text = getCommonStr('STR_TEAM_DATA')
	self.uiMatchDis2 = transform:FindChild('MatchData/Data/Dis/Score'):GetComponent('UILabel')
	self.uiMatchDis2.text = getCommonStr('STR_GAME_SCORE')
	self.uiMatchDis3 = transform:FindChild('MatchData/Data/Dis/Assist'):GetComponent('UILabel')
	self.uiMatchDis3.text = getCommonStr('STR_GAME_ASSIST')
	self.uiMatchDis4 = transform:FindChild('MatchData/Data/Dis/Rebound'):GetComponent('UILabel')
	self.uiMatchDis4.text = getCommonStr('STR_GAME_REBOUND')
	self.uiMatchDis5 = transform:FindChild('MatchData/Data/Dis/Steal'):GetComponent('UILabel')
	self.uiMatchDis5.text = getCommonStr('STR_GAME_STEAL')
	self.uiMatchDis6 = transform:FindChild('MatchData/Data/Dis/Block'):GetComponent('UILabel')
	self.uiMatchDis6.text = getCommonStr('STR_GAME_BLOCK')

	--获得奖励提示
	self.uiAwardTips = transform:FindChild('AwardBottom/Tips/Label'):GetComponent('UILabel')
	self.uiAwardTips.text = getCommonStr('STR_REWARDED')
	--获得荣誉
	self.uiAwardHonorPoints = transform:FindChild('AwardBottom/Value/Num'):GetComponent('UILabel')
	--确定
	self.uiButtonOK = transform:FindChild('ButtonOK').gameObject
	addOnClick(self.uiButtonOK, self:OnConfirmClick())

	--比赛结果
	self.uiResult = transform:FindChild('Result')

	self.uiAnimator = self.transform:GetComponent('Animator')

	--默认显示本周排行
	self:InitMatchData()

	--
	LuaHelper.RegisterPlatMsgHandler(MsgID.ExitGameRespID, self:ExitRaceResp(), self.uiName)
end

--Start
function UIHonorCompetitionResult:Start( ... )
	--body
end

--Update
function UIHonorCompetitionResult:FixedUpdate( ... )
	--body
end

function UIHonorCompetitionResult:OnClose( ... )
	TopPanelManager:ShowPanel('UIHonorCompetition')
end

function UIHonorCompetitionResult:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIHonorCompetitionResult:Refresh( ... )
	-- body
end


-----------------------------------------------------------------
--
function UIHonorCompetitionResult:SetBackUI(uiBackName)
	self.uiBack = uiBackName
end


--
function UIHonorCompetitionResult.SetMatchResult(transform, result, sessionID, side, scoreHome, scoreAway)
	--UIHonorCompetitionResult.result = result
	--UIHonorCompetitionResult.hasResult = true

	--胜负背景
	local uiHomeBg = transform:FindChild('TeamTop/Home/Bg'):GetComponent('UISprite')
	local uiAwayBg = transform:FindChild('TeamTop/Away/Bg'):GetComponent('UISprite')
	--胜负图标
	local uiHomeResult = transform:FindChild('TeamTop/Home/Result')
	local uiAwayResult = transform:FindChild('TeamTop/Away/Result')
	if result then
		uiHomeBg.spriteName = 'com_bg_pure_yellowlight'
		uiAwayBg.spriteName = 'com_bg_pure_graylight'
		CommonFunction.InstantiateObject('Prefab/GUI/MatchResultWin', uiHomeResult.transform)
		CommonFunction.InstantiateObject('Prefab/GUI/MatchResultLose', uiAwayResult.transform)
	else
		uiHomeBg.spriteName = 'com_bg_pure_graylight'
		uiAwayBg.spriteName = 'com_bg_pure_yellowlight'
		CommonFunction.InstantiateObject('Prefab/GUI/MatchResultLose', uiHomeResult.transform)
		CommonFunction.InstantiateObject('Prefab/GUI/MatchResultWin', uiAwayResult.transform)
	end

	local honorCompetition = {
		type = 'MT_REGULAR',
		session_id = sessionID,
		main_role_side = side,
		score_home = scoreHome,
		score_away = scoreAway,
	}

	local req = {
		acc_id = MainPlayer.Instance.AccountID,
		type = 'MT_REGULAR',
		exit_type = 'EMT_END',
		honorCompetition = honorCompetition,
	}

	local msg = protobuf.encode("fogs.proto.msg.ExitGameReq", req)
	LuaHelper.SendPlatMsgFromLua(MsgID.ExitGameReqID, msg)

end

--
function UIHonorCompetitionResult.InitPlayerMatchData(transform, playerID, name, score, assist, rebound, steal, block)
	print('---------*** name: ', name)
	print('---------*** score: ', score)
	print('---------*** assist: ', assist)
	print('---------*** rebound: ', rebound)
	print('---------*** steal: ', steal)
	print('---------*** block: ', block)
	local id = 'Player' .. playerID
	--比赛数据
	local player = transform:FindChild('MatchData/Data/' .. id)
	if player then
		player:FindChild('Name'):GetComponent('UILabel').text = name
		player:FindChild('Score'):GetComponent('UILabel').text = score
		player:FindChild('Assist'):GetComponent('UILabel').text = assist
		player:FindChild('Rebound'):GetComponent('UILabel').text = rebound
		player:FindChild('Steal'):GetComponent('UILabel').text = steal
		player:FindChild('Block'):GetComponent('UILabel').text = block
	end
end

--
function UIHonorCompetitionResult:AddHonorPoints(winning_streak)
	print("--------Winning streak: ", winning_streak)
	local award_pack_id = GameSystem.Instance.WinningStreakAwardConfig:GetAwardPackID(winning_streak)
	local award_pack = GameSystem.Instance.CareerConfigData:GetAwardPackConfig(award_pack_id)
	print('-----------award_pack.awards[0].award_value: ', award_pack.awards:get_Item(0).award_value)
	self.uiAwardHonorPoints.text = tostring(award_pack.awards:get_Item(0).award_value)
	--[[
	for k, v in pairs(award_pack.awards) do
		if v.award_id == GlobalConst.GOLD_ID or
			v.award_id == GlobalConst.HP_ID or 
			v.award_id == GlobalConst.DIAMOND_ID or 
			v.award_id == GlobalConst.HONOR_ID then
			self.uiAwardHonorPoints.text = v.award_value
		end
	end
	--]]
end

--
function UIHonorCompetitionResult:ExitRaceResp()
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.ExitGameRespID, self.uiName)
		local resp, err = protobuf.decode('fogs.proto.msg.ExitGameResp', message)
		if resp == nil then
			print('error -- ExitGameResp error: ', err)
			return
		end

		if resp.honorCompetition.result ~= 0 then
			print('error --  ExitGameResp return failed: ', resp.honorCompetition.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.honorCompetition.result),nil)
			return
		end

		--print('---------################: ', resp.honorCompetition.type)
		if resp.honorCompetition.type == 'MT_REGULAR' then
			self:AddHonorPoints(resp.honorCompetition.winning_streak)
			MainPlayer.Instance.WinningStreak = resp.honorCompetition.winning_streak
			MainPlayer.Instance.CurScore = resp.honorCompetition.cur_regular_points
			MainPlayer.Instance.PvpRunTimes = resp.honorCompetition.run_times
		end
	end
end

--返回确定处理
function UIHonorCompetitionResult:OnConfirmClick()
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

--初始化比赛数据
function UIHonorCompetitionResult:InitMatchData()
	
end


return UIHonorCompetitionResult

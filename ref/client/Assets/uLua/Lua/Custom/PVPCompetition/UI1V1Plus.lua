--enxcoding=utf-8

UI1V1Plus = UI1V1Plus or
{
	uiName = 'UI1V1Plus',

	----------------parameters
	modeType = GameMode.GM_PVP_1V1_PLUS,
	totalFightCapacity,
	isStartMatch = false,
	banTwice = false,
	nextShowUIParams,
	nowDate,
	autoMatch = false,
	fightNum,
	onClose,
	----------------UI
	uiBack,
	uiPlayerProperty,
	uiBtnMenu,
	uiBackBtn,
	uiStartBtn,
	uiRoleGrid,
	uiReputationStore,
	uiWinOdds,
	uiTimeTip,
	uiGradingTip,
	uiChallengeScore,
	uiAnimator,
	uiFightNum,
}

local gameMode = GameMode.GM_PVP_1V1_PLUS

function UI1V1Plus:Awake( ... )
	-- body
	self.uiPlayerProperty = createUI('PlayerProperty', self.transform:FindChild('Top/PlayerInfoGrids'))
	self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
	self.uiBackBtn = createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack').transform)
	self.uiStartBtn = self.transform:FindChild('ButtonOK'):GetComponent('UIButton')
	self.uiRoleGrid = self.transform:FindChild('RoleGrid'):GetComponent('UIGrid')
	self.uiReputationStore = self.transform:FindChild('ReputationShop')
	self.uiTimeTip = self.transform:FindChild('TimeTip')
	self.uiWinOdds = self.transform:FindChild('BackBottom/WinTip/Num'):GetComponent('UILabel')
	self.uiGradingTip = self.transform:FindChild('GradingTip'):GetComponent('UILabel')
	self.uiChallengeScore = self.transform:FindChild('BackBottom/ScoreTip/Num'):GetComponent('UILabel')

	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiFightNum = self.transform:FindChild("BackBottom/FightingForce/FightNum")

	self.fightNum = getLuaComponent(createUI("FightNum", self.uiFightNum))
end

function UI1V1Plus:Start( ... )
	local time = GameSystem.mTime
	self.nowDate = os.date("*t",time)
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	addOnClick(self.uiBackBtn.gameObject, self:OnBack())
	addOnClick(self.uiReputationStore.gameObject, self:OpenStore())
	addOnClick(self.uiStartBtn.gameObject, self:OnStartChallengClick())
	-- addOnClick(self.uiChallengeBtn.gameObject,self:MakeOnChange())

	-- self:CreateFightRole()
	-- self:DisPlayFightInfo()
	self:Refresh()

	-- self.actionOnReconn = LuaHelper.Action(self:MakeOnReconnected())

	-- PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionOnReconn

	if self.autoMatch == true then
		self:OnStartChallengClick()()
		self.autoMatch = false
	end
end

function UI1V1Plus:FixedUpdate()
	-- body
end

function UI1V1Plus:MakeOnReconnected()
	return function ()
		print(self.uiName, "UI1V1Plus on reconnected, isStartMatch:", UI1V1Plus.isStartMatch)
		PvpShow = false
		if UI1V1Plus.isStartMatch then
			self:OnStartChallengClick()()
		end
	end
end

function UI1V1Plus:OnClose()
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
	else
		print('self.uiBack = ', self.uiBack)
		if self.uiBack then
			TopPanelManager:ShowPanel('UIPVPEntrance')
			-- jumpToUI(self.uiBack)
		else
			TopPanelManager:HideTopPanel()
		end
		local root = UIManager.Instance.m_uiRootBasePanel
		local pvpTip = root.transform:FindChild('PvpTip(Clone)')
		if pvpTip then
			getLuaComponent(pvpTip.gameObject).uiAimator:SetBool('New Bool', true)
		end
		-- jumpToUI("UIHall")
	end
end

function UI1V1Plus:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UI1V1Plus:OnDestroy( ... )
	-- body
	-- PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionOnReconn
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UI1V1Plus:Refresh( ... )
	local playerProperty = getLuaComponent(self.uiPlayerProperty)
	playerProperty.showReputation = true
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
	self:CreateFightRole()
	self:DisPlayFightInfo()

	-- local root = UIManager.Instance.m_uiRootBasePanel.transform
	-- local go = root:FindChild('PvpTip(Clone)')
	-- if go then
	--	local pvpTip = getLuaComponent(go)
	--	pvpTip.onTop = function ( ... )
	--		NGUITools.SetActive(self.uiTimeTip.gameObject, false)
	--	end
	--	pvpTip.onDown = function ( ... )
	--		NGUITools.SetActive(self.uiTimeTip.gameObject, true)
	--	end
	--	if pvpTip.uiAimator then
	--		if pvpTip.uiAimator:GetBool("New Bool") then
	--			pvpTip.uiAimator:SetBool("New Bool", false)
	--		end
	--	end
	-- end
end


-----------------------------------------------------------------

function UI1V1Plus:OnBack( ... )
	return function ( ... )
	  --if not PvpShow then
		--  self:DoClose()
		--end
		self:DoClose()
	end
end

function UI1V1Plus:CreateFightRole( ... )
	CommonFunction.ClearGridChild(self.uiRoleGrid.transform)
	for i = 1, 3 do
		local role = self:InitRole('RoleBustItem1',
			{status = FightStatus.IntToEnum(i), transform = self.uiRoleGrid.transform})
		role.gameObject.name = i
		role.useType = "Challenge"
		-- role.onClickSelect = self:MakeOnChoose()
		role.gameObject.name = i
	end

	self.uiRoleGrid.repositionNow = true
end

function UI1V1Plus:MakeOnChoose( ... )
	return function (go)

	end
end

function UI1V1Plus:InitRole(prefabName , parent)
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

function UI1V1Plus:OpenStore( ... )
	return function (go)
		-- UIStore:SetType('ST_REPUTATION')
		-- UIStore:SetBackUI(self.uiName)
		-- UIStore:OpenStore()
		self.nextShowUI = "UIFashion"
		self.nextShowUIParams = {reputationStore = true, titleStr = 'STR_REPUTATION_STORE'}
		self:DoClose()
	end
end

function UI1V1Plus:DisPlayFightInfo( ... )
	self.totalFightCapacity = GetTeamFight()
	self.fightNum:SetNum(self.totalFightCapacity)

	local totalTimes = MainPlayer.Instance.PvpPlusInfo.race_times
	local winTimes = MainPlayer.Instance.PvpPlusInfo.win_times
	print('totalTimes = ' .. tostring(totalTimes))
	print('winTimes = ' .. tostring(winTimes))
	if totalTimes == 0 or winTimes == 0 then
		self.uiWinOdds.text = '--'
	else
		self.uiWinOdds.text = string.format('%.2f', winTimes/totalTimes * 100) .. '%'
	end

	if totalTimes < 10 then
		NGUITools.SetActive(self.uiGradingTip.gameObject, true)
		self.uiGradingTip.text = string.format(getCommonStr('STR_GRADINGTIP'), totalTimes)
		self.uiChallengeScore.text = getCommonStr('NO_AVAILABLE')
	else
		NGUITools.SetActive(self.uiGradingTip.gameObject, false)
		NGUITools.SetActive(self.uiChallengeScore.gameObject, true)
		self.uiChallengeScore.text = MainPlayer.Instance.PvpPlusInfo.score
	end
end

function UI1V1Plus:OnStartChallengClick( ... )
	return function ()
		print("time hour:",self.nowDate["hour"])
		local open = tonumber(GlobalConst.CHALLENGE_OPEN)
		local close = tonumber(GlobalConst.CHALLENGE_CLOSE)
		if self.nowDate["hour"] < open or self.nowDate["hour"] >= close then
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_CHALLENGE_TIP"):format(open,close), nil, nil,nil,nil,nil)
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

		local fightPowerList = KeyValueDataList.New()
		local enum = MainPlayer.Instance.SquadInfo:GetEnumerator()
	while enum:MoveNext() do
	  local data = KeyValueData.New()
	  data.id = enum.Current.role_id
	  local attr = MainPlayer.Instance:GetRoleAttrsByID(data.id)
	  data.value = attr.fightingCapacity
	  fightPowerList:Add(data)
	end

		local operation = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_PVP_1V1_PLUS',
			challenge_plus = {
				fight_power = self.totalFightCapacity
			},

			fight_power = {
			  {id = fightPowerList:get_Item(0).id, value = fightPowerList:get_Item(0).value},
			  {id = fightPowerList:get_Item(1).id, value = fightPowerList:get_Item(1).value},
			  {id = fightPowerList:get_Item(2).id, value = fightPowerList:get_Item(2).value},
			},

			game_mode = gameMode:ToString(),
		}

		--register
		LuaHelper.RegisterPlatMsgHandler(MsgID.StartMatchRespID, self:StartChallengeResp(), self.uiName)

		print(self.uiName, "Send StartMatchReq")
		local req = protobuf.encode("fogs.proto.msg.StartMatchReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.StartMatchReqID, req)
	end
end

function UI1V1Plus:StartChallengeResp( ... )
	return function (message)
		if self.banTwice then
			return
		end
		print(self.uiName, 'StartChallengeResp')
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


		local tipGo = createUI('PvpTip')--,  self.transform.parent.transform)
		tipGo.transform:GetComponent('UIPanel').depth = 50

		-- local tip = tipGo.transform:FindChild("PvpTip")
		local luacom = getLuaComponent(tipGo.gameObject)
		luacom.onTop = function ( ... )
			NGUITools.SetActive(self.uiTimeTip.gameObject, false)
		end
		luacom.onDown = function ( ... )
			NGUITools.SetActive(self.uiTimeTip.gameObject, true)
		end
		luacom.btnMenu = self.uiBtnMenu.gameObject
		luacom.averTime = resp.aver_time
		luacom.onCancel = self:IsCancel()
		current_match_type = 'MT_PVP_1V1_PLUS'
		UI1V1Plus.isStartMatch = true
		self.banTwice = true
	end
end

function UI1V1Plus:IsCancel( ... )
	return function (isCancel)
		UI1V1Plus.isStartMatch = isCancel
		self.banTwice = false
	end
end

function UI1V1Plus:SetModelActive(active)
	-- body
end

return UI1V1Plus

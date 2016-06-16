--encoding=utf-8

UICompetition = UICompetition or
{
	uiName = 'UICompetition',

	----------------------------------
	nextShowUI,       
	nextShowUISubID,
	nextShowUIParams,

	uiBack,

	----------------------------------UI
	--返回
	uiButtonBack,
	--标题
	uiTitle,
	--巡回赛
	uiMatchTour,
	uiMatchTourLabel,
	uiMatchTourCondition,
	--斗牛
	uiMatchBullfight,
	uiMatchBullfightLabel,
	uiMatchBullfightCondition,
	--投篮赛
	uiMatchShooting,
	uiMatchShootingLabel,
	uiMatchShootingCondition,
	--
	uiBtnMenu,

	uiAnimator,
	onClose,
}

local matchState =
{
	normal = Color.New(1,1,1,1),
	disable = Color.New(60/255,60/255,60/255,1)
}

-----------------------------------------------------------------
--Awake
function UICompetition:Awake( ... )
	local transform = self.transform

	--返回
	local go = createUI('ButtonBack', transform:FindChild('Top/ButtonBack'))
	self.uiButtonBack = getLuaComponent(go)
	self.uiButtonBack.onClick = self:OnBackClick()

	--标题
	--self.uiTitle = transform:FindChild('Top/Title'):GetComponent('MultiLabel')
	self.uiMenuGrid = transform:FindChild("Top/ButtonMenu")
	self.uiBtnMenu = createUI("ButtonMenu",self.uiMenuGrid)
	local btnMenu = getLuaComponent(self.uiBtnMenu)
	btnMenu:SetParent(self.gameObject,false)
	btnMenu.parentScript = self
	--巡回赛
	self.uiMatchTour = transform:FindChild('Middle/TourItem'):GetComponent('UISprite')
	self.uiMatchTourLabel = transform:FindChild('Middle/TourItem/LabelName1'):GetComponent('UILabel')
	self.uiMatchTourCondition = transform:FindChild('Middle/TourItem/LabelConditio1'):GetComponent('UILabel')
	addOnClick(self.uiMatchTour.gameObject, self:OnTourClick())

	--1V1
	self.uiMatchBullfight = transform:FindChild('Middle/OneononeItem'):GetComponent('UISprite')
	self.uiMatchBullfightLabel = transform:FindChild('Middle/OneononeItem/LabelName2'):GetComponent('UILabel')
	self.uiMatchBullfightCondition = transform:FindChild('Middle/OneononeItem/LabelConditio2'):GetComponent('UILabel')
	addOnClick(self.uiMatchBullfight.gameObject, self:OnBullfightClick())

	--3V3
	self.uiMatchShooting = transform:FindChild('Middle/ShootItem'):GetComponent('UISprite')
	self.uiMatchShootingLabel = transform:FindChild('Middle/ShootItem/LabelName3'):GetComponent('UILabel')
	self.uiMatchShootingCondition = transform:FindChild('Middle/ShootItem/LabelConditio3'):GetComponent('UILabel')
	addOnClick(self.uiMatchShooting.gameObject, self:OnShootingClick())

	self.uiAnimator = self.transform:GetComponent('Animator')
end

--Start
function UICompetition:Start( ... )
	--body
	LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpenRespID, self:HandleShootOpen(), self.uiName)
end

--Update
function UICompetition:FixedUpdate( ... )
	-- body
end

function UICompetition:OnClose( ... )
	if self.onClose then 
		--print("uiBack",self.uiName,"--:",self.onClose)
		self.onClose()
		self.onClose = nil
		return
	end

	local btnMenu = getLuaComponent(self.uiBtnMenu)
	btnMenu:SetParent(self.gameObject,true)
	btnMenu.parentScript = self
	print("uiBack:",self.uiName,"--:",self.nextShowUI)
	if self.nextShowUI then
		self.nextShowUIParams = {uiBack = "UICompetition"}
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end
end

function UICompetition:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UICompetition:OnDestroy( ... )
	-- body
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpenRespID,  self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--Refresh
function UICompetition:Refresh( ... )
	local btnMenu = getLuaComponent(self.uiBtnMenu)
	btnMenu:Refresh()

	local level = MainPlayer.Instance.Level
	local shootLevel = GameSystem.Instance.FunctionConditionConfig:GetFuncCondition('Shoot').conditionParams:get_Item(0)
	local bullFightLevel = GameSystem.Instance.FunctionConditionConfig:GetFuncCondition('BullFight').conditionParams:get_Item(0)
	local tourLevel = GameSystem.Instance.FunctionConditionConfig:GetFuncCondition('UITour').conditionParams:get_Item(0)
	self.uiMatchShootingCondition.text = string.format(getCommonStr("STR_SINGLE_LEVEL"), tonumber(shootLevel))
	self.uiMatchShootingCondition.gameObject:SetActive(level < tonumber(shootLevel))
	self.uiMatchBullfightCondition.text = string.format(getCommonStr("STR_SINGLE_LEVEL"), tonumber(bullFightLevel))
	self.uiMatchBullfightCondition.gameObject:SetActive(level < tonumber(bullFightLevel))
	self.uiMatchTourCondition.text = string.format(getCommonStr("STR_SINGLE_LEVEL"), tonumber(tourLevel))
	self.uiMatchTourCondition.gameObject:SetActive(level < tonumber(tourLevel))

	if level < tonumber(shootLevel) then
		self.uiMatchShooting.color = matchState.disable

		self.uiMatchShootingLabel.color = matchState.disable
		self.uiMatchBullfight.color = matchState.disable
		self.uiMatchBullfightLabel.color = matchState.disable
		self.uiMatchTour.color = matchState.disable
		self.uiMatchTourLabel.color = matchState.disable
	elseif level < tonumber(bullFightLevel) then
		self.uiMatchShooting.color = matchState.normal
		self.uiMatchShootingLabel.color = matchState.normal
		self.uiMatchBullfight.color = matchState.disable
		self.uiMatchBullfightLabel.color = matchState.disable
		self.uiMatchTour.color = matchState.disable
		self.uiMatchTourLabel.color = matchState.disable
	elseif level < tonumber(tourLevel) then
		self.uiMatchShooting.color = matchState.normal
		self.uiMatchShootingLabel.color = matchState.normal
		self.uiMatchBullfight.color = matchState.normal
		self.uiMatchBullfightLabel.color = matchState.normal
		self.uiMatchTour.color = matchState.disable
		self.uiMatchTourLabel.color = matchState.disable
	else
		self.uiMatchShooting.color = matchState.normal
		self.uiMatchShootingLabel.color = matchState.normal
		self.uiMatchBullfight.color = matchState.normal
		self.uiMatchBullfightLabel.color = matchState.normal
		self.uiMatchTour.color = matchState.normal
		self.uiMatchTourLabel.color = matchState.normal
	end
end


-----------------------------------------------------------------
--设置返回界面
function UICompetition:SetBackUI(uiBackName)
	self.uiBack = uiBackName
end

--返回点击处理
function UICompetition:OnBackClick()
	return function (go)
		self:DoClose()
	end
end

--显示界面
function UICompetition:ShowUI()
	--创建任务界面
	TopPanelManager:ShowPanel(self.uiName)
end

--巡回赛按钮点击处理
function UICompetition:OnTourClick()
	return function (go)
		if validateFunc('UITour') then
			self.nextShowUI = "UITour"
			self:DoClose()
		end
	end
end


function UICompetition:OnBullfightClick()
	return function (go)
		if not validateFunc("BullFight") then
			return
		end

		local GetBullFightNpcReq = {
			acc_id = MainPlayer.Instance.AccountID
		}

		local req = protobuf.encode("fogs.proto.msg.GetBullFightNpcReq",GetBullFightNpcReq)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetBullFightNpcReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self:HandleGetNPC(), self.uiName)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function UICompetition:OnShootingClick()
	return function ()
		if not validateFunc("Shoot") then
			return
		end

		self.SendShootOpenReq()
	end
end

function UICompetition.SendShootOpenReq()
	local ShootOpenReq = {
		acc_id = MainPlayer.Instance.AccountID
	}

	local req = protobuf.encode("fogs.proto.msg.ShootOpenReq",ShootOpenReq)
	print("Send Shoot Open ShootOpenReq.acc_id=", ShootOpenReq.acc_id)
	LuaHelper.SendPlatMsgFromLua(MsgID.ShootOpenReqID,req)
	CommonFunction.ShowWaitMask()
	CommonFunction.ShowWait()
end

function UICompetition:HandleShootOpen()
	return function(buf)
		if self.HandleShootOpenResp(buf) then
			self.nextShowUI = "UIShootGame"
			self:DoClose()
		end
	end
end

function UICompetition.HandleShootOpenResp(buf)
	CommonFunction.HideWaitMask()
	CommonFunction.StopWait()
	local resp, err = protobuf.decode("fogs.proto.msg.ShootOpenResp", buf)
	print("resp.result=",resp.result)
	if resp then
		if resp.result == 0 then
			MainPlayer.Instance:ClearShootGameModeInfo()
			for k, v in pairs(resp.game_mode_info ) do
				local value  = v
				local gameModeInfo = GameModeInfo.New()
				gameModeInfo.game_mode = GameMode[v.game_mode]
				gameModeInfo.times = v.times
				gameModeInfo.npc = v.npc
				MainPlayer.Instance:AddShootGameModeInfo(gameModeInfo)
			end
			MainPlayer.Instance.IsLastShootGame = true
			return true
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
		end
	else
		error("UICompetition:HandleShootOpen -handler", err)
	end
	return false
end

function UICompetition:HandleGetNPC()
	return function(buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.GetBullFightNpcResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance.BullFightNpc:Clear()
				for k, v in ipairs(resp.npc) do
					MainPlayer.Instance.BullFightNpc:Add(v)
				end
				MainPlayer.Instance.IsLastShootGame = false
				self.nextShowUI = "UIBullFight"
				self:DoClose()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
			end
		else
			error("UICompetition:HandleGetNPC -handler", err)
		end
	end
end

function UICompetition:SetModelActive(active)
	-- don't delete
end

return UICompetition

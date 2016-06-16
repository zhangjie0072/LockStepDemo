--encoding=utf-8

------------------------------------------------------------------------
-- class name	: UIPractice
-- create time   : 20150703_104219
------------------------------------------------------------------------


UIPractice =  {
	uiName = "UIPractice",

	----------------parameters

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	onClose,

	----------------UI

	uiAnimator,
}


-----------------------------------------------------------------
function UIPractice:Awake()
	self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
	self.uiBackGrid = self.transform:FindChild("Top/ButtonBack")
	self.uiPropertyGrid = self.transform:FindChild("Top/PlayerProperty")
	self.uiRemainTimes = self.transform:FindChild("Buttom/Times/Num"):GetComponent("UILabel")
	self.uiCoolingTimes = self.transform:FindChild("Buttom/CoolingTimes/Num"):GetComponent("UILabel")
	self.uiCooling = self.transform:FindChild("Buttom/CoolingTimes")
	-- background
	--createUI("Background",self.transform:FindChild("Background"))
	--返回键底图替换
	--getLuaComponent(back_btn):set_bPracticeItem3ack_icon("com_bg_pure_greendeep", 1.0)
	self.uiGrid = self.transform:FindChild("Scroll/Grid1"):GetComponent("UIGrid")
	self.uiPracticeItem = {}
	self.uiPracticeItem[1] = self.uiGrid.transform:FindChild("PracticeItem1")
	self.uiPracticeItem[2] = self.uiGrid.transform:FindChild("PracticeItem2")
	self.uiPracticeItem[3] = self.uiGrid.transform:FindChild("PracticeItem3")
	self.uiPracticeItem[4] = self.uiGrid.transform:FindChild("PracticeItem4")
	self.uiPracticeItem[5] = self.uiGrid.transform:FindChild("PracticeItem5")

	--self.go.grid1	   = scroll:FindChild("Grid1"):GetComponent("UIGrid")
	--self.go.finish_info = self.transform:FindChild("Panel/FinishInfo"):GetComponent("UILabel")
	-- self.go.player_info = self.transform:FindChild("PlayerInfo")

	self.uiAnimator = self.transform:GetComponent('Animator')

	-- Hide the Text
	local t = self.transform:FindChild("Buttom/Times")
	if t then
		t.gameObject:SetActive(false)
	end

	if self.uiCooling then
		self.uiCooling.gameObject:SetActive(false)
	end

	if self.uiCoolingTimes then
		self.uiCoolingTimes.gameObject:SetActive(false)
	end
end

function UIPractice:Start()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	local backBtn = getLuaComponent(createUI("ButtonBack",self.uiBackGrid))
	backBtn.onClick = self:OnClickBack()
	--createUI("PlayerProperty",self.uiPropertyGrid)
end

function UIPractice:FixedUpdate()
	-- local now_time = os.time()
	-- self.remain_time = now_time - self.diff - self.lastTime
	-- -- print(self.uiName,"cd:",self.remain_time)
	-- if self.remain_time <= tonumber(GlobalConst.GPRACRICECD) and MainPlayer.Instance:GetRemainTimes() > 0 and self.remain_time >0 then
	--	if not self.cooling then
	--		NGUITools.SetActive(self.uiCooling.gameObject, true)
	--		NGUITools.SetActive(self.uiCoolingTimes.gameObject,true)
	--		self.cooling = true
	--	end
	--	self.cd = tonumber(GlobalConst.GPRACRICECD) - self.remain_time
	--	self.uiCoolingTimes.text = self:GetTimeLabel(self.cd)
	-- else
	--	if self.cooling then
	--		NGUITools.SetActive(self.uiCoolingTimes.gameObject,false)
	--		NGUITools.SetActive(self.uiCooling.gameObject, false)
	--		self:Refresh()
	--		self.cooling = false
	--	end
	-- end
end

function UIPractice:Refresh( ... )
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
	self.uiRemainTimes.text = MainPlayer.Instance:GetRemainTimes() .. "/" .. 4

	--冷却时间
	self.diff = os.time() - GameSystem.mTime
	self.lastTime = MainPlayer.Instance.practice_cd

	self.itemList = {}
	--CommonFunction.ClearGridChild(self.uiGrid.transform)
	local allNum = 0
	local finishNum = 0
	local enum = GameSystem.Instance.PractiseConfig.configs:GetEnumerator()
	local zero_tag = true	-- true is for grid0 while false for grid1
	while enum:MoveNext() do
		local key = enum.Current.Key
		local value = enum.Current.Value

		print("Key = "..tostring(key))
		print("value.title = "..tostring(value.title))
		print("value.type = "..tostring(value.type))
		local tm = self.uiPracticeItem[allNum + 1]
		CommonFunction.ClearChild(tm)
		if value.type == PractiseData.Type.Normal then
			local item = createUI("PracticeItem",tm)
			local script = getLuaComponent(item)
			script:SetId(key)
			script.onClick = self:click_item()
			if script.isGray then
				finishNum = finishNum + 1
			end
			allNum = allNum + 1
			table.insert(self.itemList,script)
		end
	end

	-- self.uiGrid:Reposition()
end

function UIPractice:OnClose()
	print('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa')
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
		print('@@jump to hall')
		jumpToUI("UIHall")
	end
end

function UIPractice:DoClose()
	if self.uiAnimator then
		print(self.uiName,":Set uinanimator close true")
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIPractice:OnDestroy()
	--body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-----------------------------------------------------------------
function UIPractice:OnClickBack()
	return function()
		self:DoClose()
	end
end

function UIPractice:GetTimeLabel(cd)
	local minutes = math.floor(cd / 60)
	local seconds = math.floor(cd % 60)
	return string.format("%02d:%02d", minutes, seconds)
end

function UIPractice:click_item()
	return function(item)
		print("UIPractice:click_item called()")
		print("to send id = "..tostring(item.id))

		if not FunctionSwitchData.CheckSwith(FSID.rebot_train_4) then return end
		-- if MainPlayer.Instance:IsPractiseCompleted(item.id) == true then
		--	print("return")
		--	CommonFunction.ShowPopupMsg(getCommonStr('GAME_NOT_OPEN'),nil,nil,nil,nil,nil)
		--	return
		-- end
		if self.waitResp then return end
		local operation = {
			id = item.id
		}
		local enterGame = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 'MT_PRACTICE',
			practice = operation,
			game_mode = GameMode.GM_None:ToString(),
		}

		--local req = protobuf.encode("fogs.proto.msg.BeginPractice",operation)
		--LuaHelper.SendPlatMsgFromLua(MsgID.BeginPracticeID,req)
		local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:shandle_practice(), self.uiName)
		self.waitResp = true
	end
end

function UIPractice:shandle_practice()
	return function(buf)
		self.waitResp = false
		CommonFunction.StopWait()
		--local resp, err = protobuf.decode("fogs.proto.msg.BeginPracticeResp", buf)
		local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
		if resp then
			if resp.result == 0 then
				local session_id = resp.practice.session_id
				print('resp.id='..tostring(resp.practice.id))
				print('resp.session_id='..tostring(resp.practice.session_id))
				local practice_config = GameSystem.Instance.PractiseConfig:GetConfig(resp.practice.id)

				-- Client.CreateMatch(practice_config,session_id)
				GameSystem.Instance.mClient:CreateMatch(practice_config,session_id)
				-- UIPractiseListControl.CreateMatch(practice_config,session_id)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("shandle_practice: " .. err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
	end
end

function UIPractice:SetModelActive(active)
	-- body
end

return UIPractice

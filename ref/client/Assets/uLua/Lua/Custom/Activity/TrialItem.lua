--encoding=utf-8
require "Custom/Career/CareerUtil"

TrialItem =
{
	uiName = 'TrialItem',
	-----------------parameters
	index,
	day,
	parent = "UIHall",
	--------------------UI
	uiGrid,
	uiProcess,
	uiPercentLabel,
	uiCloseGrid,
	uiButtonGray,
	uiIcon,
	uiButtonGrayAnimator,
	uiTitle,
	uiAimator,
	uiLblTitle,
	state = 0,
	showProgress = 1,--default show progress label
}


-----------------------------------------------------------------
function TrialItem:Awake( ... )

	self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	self.uiLblTitle = self.transform:FindChild("Name1"):GetComponent("UILabel")
	self.uiActivity = self.transform:FindChild("Name"):GetComponent("UILabel")
	self.uiScore = self.transform:FindChild("Award/Cola/Reward"):GetComponent("UILabel")
	self.uiAward = self.transform:FindChild("Award/Grid")
	self.uiButton = self.transform:FindChild("ButtonGo")
	self.uiButtonLabel = self.uiButton.transform:FindChild("Text"):GetComponent("MultiLabel")
	self.uiButtonRead = self.transform:FindChild("ButtonRead")
	self.uiButtonAnimator = self.transform:FindChild("Ef_Button1").gameObject
	self.uiLblSchedule = self.transform:FindChild('Progress'):GetComponent('UILabel')
	addOnClick(self.uiButton.gameObject, self:OnClick())

	--msg register
	-- LuaHelper.RegisterPlatMsgHandler(MsgID.GetNewComerTrialAwardsRespID, self:GetAwardsHandler(), self.uiName)
end

function TrialItem:Start( ... )

	--body
	if self.hideButton == true then
		NGUITools.SetActive(self.uiButton.gameObject, false)
	end
	self:Refresh()
end
function TrialItem:Refresh( ... )

	self.data = GameSystem.Instance.trialConfig:GetTrialDataByIndex(self.day, self.index)
	-- if self.data then
	-- 	print('id '..self.data.id..',index '..self.data.index..',activity '..self.data.activity..',score '..self.data.score)
	-- end
	-- CommonFunction.ClearGridChild(self.uiAward.transform)
	--awards

	if self.initAwards == nil then
		local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(self.data.award_id)
      	if awardConfig then
			-- awardsTable[enum.Current.Key] = enum.Current.Value
			-- print(self.uiName,"--id:",awardConfig.awards:get_Item(0).award_id,"---value:",awardConfig.awards:get_Item(0).award_value)
			local go = CommonFunction.InstantiateObject('Prefab/GUI/GoodsIconConsume', self.uiAward.transform)
            if go == nil then
                Debugger.Log('-- InstantiateObject falied ')
                return
            end
            local taskAward = getLuaComponent(go)
            taskAward.isTask = true
            taskAward:SetData(awardConfig.awards:get_Item(0).award_id, awardConfig.awards:get_Item(0).award_value, false)
		end
		self.uiAward:GetComponent("UIGrid").repositionNow = true
		self.uiAward:GetComponent("UIGrid"):Reposition()
		self.initAwards = true
	end
	--icon
	self.uiIcon.spriteName = self.data.icon
	--name
	self.uiActivity.text = self.data.activity
	--score
	self.uiScore.text = '+'..self.data.score--string.format(CommonFunction.GetConstString('STR_TOTAL_ADD'), self.data.score)

	--title
	self.uiLblTitle.text = self.data.title
	--state 2:可领取 1:已领取

	-- self.state = MainPlayer.Instance:IsTrialState(self.day, self.index) --接口已废弃
	-- print(self.uiName,"----parent day:",self.parent.RealDay,"----day:",self.day,"----index:",self.index,"----state:",self.state)

	-- self.showProgress = math.random(0,1)
	if self.showProgress == 1 and self.state ~=3 then
		--设置进度
		--富文本颜色
		NGUITools.SetActive(self.uiLblSchedule.gameObject,true)
	    local strFormatWhite = '[FFFFFF]'
	    local strFormatWhiteEnd = '[-]'
	    local strFormatGreen = '[B0FD04]'
	    local strFormatGreenEnd = '[-]'
		if self.schedule then
			if self.schedule.condition_need > self.schedule.condition_cur then
			      self.uiLblSchedule.text = strFormatWhite..self.schedule.condition_cur..strFormatWhiteEnd..strFormatGreen.."/" .. self.schedule.condition_need..strFormatGreenEnd
	        else
	            self.uiLblSchedule.text = strFormatGreen..self.schedule.condition_cur.."/" ..self.schedule.condition_need..strFormatGreenEnd
			end
		else
			self.uiLblSchedule.text = strFormatWhite..'0/0'..strFormatWhiteEnd
		end
	else
		NGUITools.SetActive(self.uiLblSchedule.gameObject,false)
	end

	--设置按键状态

	if self.state == 3 then
		NGUITools.SetActive(self.uiButton.gameObject, false)
		NGUITools.SetActive(self.uiButtonRead.gameObject, true)
		NGUITools.SetActive(self.uiButtonAnimator, false)
	elseif self.state == 2 then
		self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
		if  self.day>self.parent.RealDay then
			self.uiButton.transform:GetComponent('UIButton').isEnabled = false
			self.uiLblSchedule.text = string.format(getCommonStr('STR_FIELD_Traildaylimit'),self.day)
			NGUITools.SetActive(self.uiLblSchedule.gameObject,true)
		else
			NGUITools.SetActive(self.uiButtonAnimator, true)
			local reddot = self.uiButton.transform:FindChild('RedDot')
			if reddot then
				NGUITools.SetActive(reddot.gameObject,true)
			end
		end
	else
		-- print('name '..self.data.title..',link count '..self.data.link.Count..',link 0 '..self.data.link:get_Item(0))
		if MainPlayer.Instance.NewComerSign.open_flag == 0 then
			self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
			self.uiButton.transform:GetComponent('UIButton').isEnabled = false
			-- NGUITools.SetActive(self.uiButton.gameObject, false)
			NGUITools.SetActive(self.uiButtonRead.gameObject, false)
			NGUITools.SetActive(self.uiButtonAnimator, false)
		else
			if self.data.link.Count <=0  then
				self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
				self.uiButton.transform:GetComponent('UIButton').isEnabled = false
				NGUITools.SetActive(self.uiButtonLabel.gameObject,true)
				NGUITools.SetActive(self.uiButton.gameObject,true)
			elseif self.data.link:get_Item(0) <= 0 then
				self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
				self.uiButton.transform:GetComponent('UIButton').isEnabled = false
				NGUITools.SetActive(self.uiButtonLabel.gameObject,true)
				NGUITools.SetActive(self.uiButton.gameObject,true)
			else
				self.uiButtonLabel:SetText(CommonFunction.GetConstString('STR_LINK'))
				NGUITools.SetActive(self.uiButtonAnimator, false)
				if MainPlayer.Instance.NewComerSign.open_flag == 0 then
					self.uiButtonLabel:SetText(CommonFunction.GetConstString('RECEIVE'))
					self.uiButton.transform:GetComponent('UIButton').isEnabled = false
					-- NGUITools.SetActive(self.uiButton.gameObject, false)
					NGUITools.SetActive(self.uiButtonRead.gameObject, false)
					NGUITools.SetActive(self.uiButtonAnimator, false)
				end
			end
		end


	end

end

function TrialItem:OnClose( ... )
	NGUITools.Destroy(self.gameObject)
	self.parent:SetModelActive(true)
end

function TrialItem:OnDestroy( ... )
	-- body
end

function TrialItem:OnClick()
	return function (go)
	--todo test animation
    if not FunctionSwitchData.CheckSwith(FSID.active7_task) then return end

	--动画执行过程不允许点击按钮
		if self.parent.AnimExcecuting then
			---
			return
		end
		if self.state == 2 then --任务完成，领取操作
			-- local req = {
			-- 	-- type = "NCTAT_NORMAL",
			-- 	-- day = self.day,
			-- 	-- index = self.index,

			-- }
			-- local msg = protobuf.encode("fogs.proto.msg.GetNewComerTrialAwardsReq", req)
			-- LuaHelper.SendPlatMsgFromLua(MsgID.GetNewComerTrialAwardsReqID, msg)
			 local req = {
                acc_id = MainPlayer.Instance.AccountID,
                id = self.id,
            }
            local msg = protobuf.encode("fogs.proto.msg.GetTaskAward", req)
			LuaHelper.RegisterPlatMsgHandler(MsgID.GetTaskAwardRespID, self.parent:GetTaskRewardHandler(self.data.award_id), self.parent.uiName)
            LuaHelper.SendPlatMsgFromLua(MsgID.GetTaskAwardReqID, msg)
            CommonFunction.ShowWait()
			self.parent:ClickAnimation(self.uiButton.transform)
			--注册回复处理消息
			-- if self.is_rank == false then
			-- 	self.parent.parent:SetModelActive(false)
			-- end
		else --任务未完成，前往操作
			local linkId = self.data.link:get_Item(0)
			local uiName = GameSystem.Instance.TaskConfigData:GetLinkUIName(linkId)
			print('--------link uiName: ', uiName)
			local count = 0
			local enum = self.data.link:GetEnumerator()
			while enum:MoveNext() do
				count = count + 1
			end
			local subID = 0
			if count > 1 then
				subID = self.data.link:get_Item(1)
				print('--------link subID: ', subID)
			end
			subID = subID > 0 and subID or nil
			if uiName ~= '' then
                --前往商店的方式要单独提出来
                if uiName == 'UIStore' then
                    if subID == 1 then
                        UIStore:SetType('ST_BLACK')
                    elseif subID == 2 then
                        UIStore:SetType('ST_SKILL')
                    elseif subID == 4 then
                        UIStore:SetType('ST_HONOR')
                    end
                    TaskRespHandler.isOpen = false
                    UIStore:OpenStore()
                elseif uiName == "UIPlayerBuyDiamondGoldHP" then
                    local buyProperty = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
                    if subID == 2 then
                        buyProperty.BuyType = "BUY_GOLD"
                    elseif subID == 4 then
                        buyProperty.BuyType = "BUY_HP"
                    end
                    buyProperty.isTaskLink = true
                    TaskRespHandler.isOpen = false
                    -- NGUITools.Destroy(self.parent.gameObject)
                    --TopPanelManager:ShowPanel("UIPlayerBuyDiamondGoldHP")
                elseif uiName == "UIChallenge" then
                    local open = tonumber(GlobalConst.CHALLENGE_OPEN)
                    local close = tonumber(GlobalConst.CHALLENGE_CLOSE)
                    local nowHTime = tonumber(os.date("%H", GameSystem.mTime))
                    if nowHTime < open or nowHTime > close then
                        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_CHALLENGE_TIP"):format(open,close), nil, nil,nil,nil,nil)
                        return
                    end
                    TopPanelManager:ShowPanel(uiName, subID)
                    TaskRespHandler.isOpen = false
                    self.parent:OnCloseClick()()
                elseif uiName == "UIBullFight" then
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
                elseif uiName == "UIShootGame" then
                    local ShootOpenReq = {
                        acc_id = MainPlayer.Instance.AccountID
                    }

                    local req = protobuf.encode("fogs.proto.msg.ShootOpenReq",ShootOpenReq)
                    print("Send Shoot Open ShootOpenReq.acc_id=", ShootOpenReq.acc_id)
                    LuaHelper.SendPlatMsgFromLua(MsgID.ShootOpenReqID,req)
                    LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpenRespID, self:HandleShootOpen(), self.uiName)
                    CommonFunction.ShowWaitMask()
                elseif uiName == "UILadder" then
                    self:ClickLadder()()
                else
                    -- TopPanelManager:ShowPanel(uiName, subID)
                    TaskRespHandler.isOpen = false
                    -- print(self.uiName,"------jump to uiName:",uiName,"-----current uiName:",self.parent.parent.uiName)
                    -- if self.parent.parent.uiName ~= uiName then
                    --  self.parent.parent.nextShowUI = uiName
                    --  self.parent.parent:DoClose()
                    -- end
                    -- self.parent:OnClose()

                    TopPanelManager:ShowPanel(uiName, subID)
                end
            end
		end
	end
end

function TrialItem:HandleShootOpenResp(buf)
	CommonFunction.HideWaitMask()
	CommonFunction.StopWait()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpenRespID, self.uiName)
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

function TrialItem:HandleShootOpen()
	return function(buf)
	    CommonFunction.StopWait()
		if self:HandleShootOpenResp(buf) then
			TopPanelManager:ShowPanel("UIShootGame")
		end
	end
end


function TrialItem:HandleGetNPC()
	return function(buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		self.parent:OnCloseClick()()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.GetBullFightNpcResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance.BullFightNpc:Clear()
				for k, v in ipairs(resp.npc) do
					MainPlayer.Instance.BullFightNpc:Add(v)
				end
				MainPlayer.Instance.IsLastShootGame = false
				-- self.nextShowUI = "UIBullFight"
				-- self:DoClose()
				TopPanelManager:ShowPanel("UIBullFight")
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
			end
		else
			error("UICompetition:HandleGetNPC -handler", err)
		end
	end
end

return TrialItem

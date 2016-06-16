require "Custom/Common/Timer"

-- This is not for UI.
MatchGuide = {
	name = "MatchGuide",
	match,
	guideTip,
	timers,
	remindedTip,
}

function MatchGuide:Init(match)
	self.match = match
	self.ball = self.match.mCurScene.mBall
	self.mainRole = self.match.m_mainRole
	self.stealState = self.mainRole.m_StateMachine:GetState(PlayerState.State.eSteal)
	self.timers = {}
	self.remindedTip = {}

	self.ball.onShoot = self:MakeOnShoot()
	self.mainRole.m_StateMachine.onStateChanged = self:MakeOnPlayerStateChanged()
	self.stealState.onSteal = self:MakeOnSteal()

	-- load tip resource
	local obj = createUI("GuideTip_3")
	self.guideTip = obj:GetComponent("GuideTip")
	if not self.guideTip then
		self.guideTip = obj:AddComponent("GuideTip")
	end
	self.guideTip:GetComponent("UIPanel").depth = 1000;
	self.guideTip.transform.localPosition = Vector3.New(18, -316, 0);
	self.guideTip.firstButtonVisible = false
	self.guideTip:Hide();
end

function MatchGuide:Update(deltaTime)
	for i, timer in ipairs(self.timers) do
		if timer then
			timer:Update(deltaTime)
			if timer:IsStopped() then
				self.timers[i] = false
			end
		end
	end
end

function MatchGuide:ShowTip(tip, autoHide, repeatable, onHide)
	if repeatable or not self.remindedTip[tip] then
		if self.tipAutoHideTimer then
			self.tipAutoHideTimer:Stop()
			if self.onHide then self.onHide() end
		end
		self.guideTip.tip = getCommonStr("MATCH_GUIDE_TIP_" .. tip)
		self.guideTip:Show()
		self.onHide = onHide
		if autoHide then
			self.tipAutoHideTimer = self:DoLater(5,
			function (timer)
				self.guideTip:Hide()
				if self.onHide then self.onHide() end
				self.onHide = nil
				self.tipAutoHideTimer = nil
			end)
		end
		self.remindedTip[tip] = true
	end
end

function MatchGuide:DoLater(delay, func)
	local timer = Timer.New(delay, func, false)
	table.insert(self.timers, timer)
	return timer;
end

function MatchGuide:OnMatchStateChange(oldState, newState)
	if newState.m_eState == MatchState.State.eTipOff then	--跳球提示按篮板键
		self:ShowTip("ReboundTipOff", false)
		newState.onCounterDone = LuaHelper.Action(function () self.guideTip:Hide() end)
	elseif newState.m_eState == MatchState.State.eBegin then	--第一次交换球权
		self:ShowTip("SwitchRole", true)
	end
end

function MatchGuide:MakeOnShoot()
	return function (ball)
		local shooter = ball.m_actor
		if shooter == self.mainRole then
			if shooter:IsDefended(IM.Number.zero, IM.Number.zero) then
				--空位投篮
				self:ShowTip("ShootUndefended", true, true)
            --[[
			elseif not ball.m_isLayup and not ball.m_isDunk
				and shooter.m_InfoVisualizer.m_strengthBar.rate_adjustment < 0 then
				--力度条
				local strengthBarGuide = createUI("StrengthBarGuide", self.guideTip.transform:FindChild("Frame"))
				strengthBarGuide.transform.localPosition = Vector3.New(-135, 0, 0)
				self:ShowTip("ShootStrengthBar", true, true, function () NGUITools.Destroy(strengthBarGuide) end)
				local tip = self.guideTip.transform:FindChild("Tip"):GetComponent("UILabel")
				tip.text = "\n              " .. tip.text
            --]]
			end
		end
	end
end

function MatchGuide:MakeOnPlayerStateChanged()
	return function (oldState, newState)
		if newState.m_eState == PlayerState.State.eBlock then
			if not newState.m_success then
				local shooter = self.ball.m_actor
				if not shooter then
					shooter = self.ball.m_owner
				end
				if tostring(newState.m_failReason) == "InvalidArea" then
					--盖帽距离
					self:ShowTip("BlockTooFar", true, true)
				elseif tostring(newState.m_failReason) == "TooEarly" then
					--盖帽过早
					self:ShowTip("BlockTooEarly", true, true)
				elseif tostring(newState.m_failReason) == "TooLate" then
					--盖帽太晚
					self:ShowTip("BlockTooLate", true, true)
				end
			end
		elseif newState.m_eState == PlayerState.State.eRebound then
			if not newState.m_success then
				local dist2Ball = GameUtils.HorizonalDistance(self.mainRole.position, self.ball.position)
				local reboundDist = PlayerState_Rebound.GetDefaultMaxDist(self.mainRole)
				if dist2Ball.CompareTo(reboundDist) > 0 then
					--篮板太远
					self:ShowTip("ReboundTooFar", true, true)
				elseif newState.tooLate then
					--篮板起跳太晚
					self:ShowTip("ReboundTooLate", true, true)
				else
					--篮板起跳太早
					self:ShowTip("ReboundTooEarly", true, true)
				end
			end
		end
	end
end

function MatchGuide:MakeOnSteal()
	return function ()
		if not self.stealState.m_success then
			if not PlayerState_Steal.InStealPosition(self.mainRole, self.ball) then
				--抢断距离太远
				self:ShowTip("StealTooFar", true, true)
			end
		end
	end
end

return MatchGuide

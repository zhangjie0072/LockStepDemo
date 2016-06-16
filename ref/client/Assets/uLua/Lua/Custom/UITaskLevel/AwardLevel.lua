
AwardLevel =  {
	uiName="AwardLevel",
	onChoose = false,
	uiChoose,
	leftLine,
	rightLine,
	leftPos,
	rightPos,
	uiSpState,
	uiLblLevel,
	mbPressed = false,
	isShowTips = false,
	uiToggle = nil,
	enableClick = true,--press时禁止toggle和点击事件
	state = 0,
	level = 0,
	uiGoLineParent = nil,
	lineOffset,
	uiAnimator,

}


-----------------------------------------------------------------
function AwardLevel:Awake()
	self.leftLine = self.transform:FindChild('Point/Line2')
	self.rightLine = self.transform:FindChild('Point/Line1')
	self.uiGoLineParent = self.transform:FindChild('Point')
	self.leftPos = self.transform:FindChild('Left')
	self.rightPos = self.transform:FindChild('Right')
	self.uiChoose = self.transform:FindChild('Point/Line')
	self.uiSpState = self.transform:FindChild('Left/AwardLevel'):GetComponent('UISprite')
	self.uiToggle = self.transform:FindChild('Left/AwardLevel'):GetComponent('UIToggle')
	self.uiAnimator = self.transform:FindChild('Left/AwardLevel'):GetComponent('Animator')
	self.uiAnimator.enabled = false
	self.uiLblLevel = self.transform:FindChild('Point/Career/Num'):GetComponent('UILabel')
	self.lineOffset = self.uiSpState.transform.position - self.uiGoLineParent.transform.position
	-- addOnClick(self.uiSpState.gameObject,self:OnOpenClick())


end

function AwardLevel:Start()

	self:Refresh()
	-- EventDelegate.Add(self.uiToggle.onChange, LuaHelper.Callback(self:ChooseChanged()))
	-- if self.level <= MainPlayer.Instance.Level then
	UIEventListener.Get(self.uiSpState.gameObject).onClick = LuaHelper.VoidDelegate(self:OnOpenClick())
	-- end
	--UIEventListener.Get(self.uiSpState.gameObject).onPress = LuaHelper.BoolDelegate(self:OnPress())
end


function AwardLevel:OnClose()
end

function AwardLevel:SetDragSV(scrollView)
    self.transform:GetComponent('UIDragScrollView').scrollView = scrollView
end

function AwardLevel:SetParent(parent)
    self.parent = parent
end
function AwardLevel:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function AwardLevel:Refresh()
	if not self.Config then
		return
	else
		local  pos = self.Config.flag
		if pos == 0 then
			self.uiSpState.transform.parent = self.leftPos
			self.uiSpState.transform.localPosition = Vector3.New(0, 0, 0)
			self.uiGoLineParent.transform.parent = self.leftPos
			self.uiGoLineParent.transform.localPosition = Vector3.New(0, 0, 0)-self.lineOffset
			NGUITools.SetActive(self.leftLine.gameObject, false)
		elseif pos == 1 then
			self.uiSpState.transform.parent = self.rightPos
			self.uiSpState.transform.localPosition = Vector3.New(0, 0, 0)
			self.uiGoLineParent.transform.parent = self.rightPos
			self.uiGoLineParent.transform.localPosition = Vector3.New(0, 0, 0)-self.lineOffset
			NGUITools.SetActive(self.rightLine.gameObject, false)
		end
		local  maxLevel = GlobalConstLua.MaxRoleLevel
		if  self.level >=  maxLevel or self.level>= self.parent.levelRewardList.Count then
			NGUITools.SetActive(self.rightLine.gameObject, false)
			NGUITools.SetActive(self.leftLine.gameObject, false)
		end
		self.uiLblLevel.text = self.level
		if self.level <= MainPlayer.Instance.Level then
			if self.state == 0 then
				self.uiSpState.spriteName = 'tencent_box4'
				self.uiAnimator.enabled = true
			else
				self.uiSpState.spriteName = 'tencent_box5'
				self.uiAnimator.enabled = false
                self.uiAnimator.transform.localRotation = Vector3.New(0,0,0)
			end
		else
			self.uiSpState.spriteName = 'tencent_box6'
				self.uiAnimator.enabled = false
                self.uiAnimator.transform.localRotation = Vector3.New(0,0,0)
		end
	end
end
-- function AwardLevel:ChooseChanged( ... )
-- 	-- body
-- 	return function ( go )
-- 		-- body
-- 		-- local x = 0
-- 		-- local y = 0
-- 		-- if self.uiToggle.value then x =1 end
-- 		-- if self.onChoose then y =1 end

-- 		-- print('choose change  of '..self.level..',uiToggle '..x..',choose '..y)
-- 		if self.onChoose and self.uiToggle.value==false then
-- 			self.onChoose = false
-- 			-- self:Refresh()
-- 		elseif self.uiToggle.value==true then
-- 			self.onChoose  = true
-- 			-- self.parent:ClickRewardItem(self)
-- 			-- self:Refresh()
-- 		end
-- 	end
-- end
function AwardLevel:OnEnable( ... )
	-- body
	-- Scheduler.Instance:AddTimer(0.33, false, self:ToggleChoose())
	if self. parent then
		if self.parent.mChoosedLevel == self.level then
			self.uiToggle.value = true
			self.onChoose = true
			print('set toggle to true level.. '..self.level)
		else
			self.uiToggle.value = false
			self.onChoose = false
		end
	end
end
function AwardLevel:ToggleChoose( ... )
	-- body
	return function (  )
		-- body
		-- self.uiToggle.value = true
	end
end
function AwardLevel:OnOpenClick()
    return function (go)
  --       if self.isShowTips then return end
  		if not FunctionSwitchData.CheckSwith(FSID.daily_btn) then return end

        local choose = 0
        if self.onChoose then choose = 1 end
		-- print('AwardLevel:OnOpenClick '..choose..',level '..self.level)
		if not self.onChoose then
			self.onChoose  = true
			self.parent:ClickRewardItem(self)
		end
		-- 如果可以领取就直接领取奖励
		if self.level<=MainPlayer.Instance.Level and  self.state == 0 then
			--等级奖励状态请求
	        local req = {
	        	level = self.level
	        }
	        CommonFunction.ShowWait()
	        local msg = protobuf.encode("fogs.proto.msg.GetLevelAwardReq", req)
	        LuaHelper.SendPlatMsgFromLua(MsgID.GetLevelAwardReqID, msg)
	        TaskLevelData.AwardRespCallBack = self:HandleGetReward()
	    else
	    	self:ShowTips()()
		end
	end
end
function AwardLevel:HandleGetReward( ... )
	return function ( resp )
		-- print('reward info '..resp.level..',-'..resp.result..',c '..#resp.awards)

        if resp.result ~=0 then
            --error
            --no reward info
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
            return
        end

		-- 本地填充数据
		local idx
		local levelAwards = MainPlayer.Instance.playerLevelAwardInfo:GetEnumerator()
		while levelAwards:MoveNext() do
			if levelAwards.Current.Value == 0 and levelAwards.Current.Key == self.level then
				idx = levelAwards.Current.Key
				break
			end
		end

		if idx then MainPlayer.Instance.playerLevelAwardInfo:set_Item(idx,1) end

		self.state = 1
		self:Refresh()
		self.parent:RefreshRedDot()

        local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(self.Config.award_id)
        local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.parent.transform))
        for i = 0, awardConfig.awards.Count - 1 do
            getGoods:SetGoodsData(awardConfig.awards:get_Item(i).award_id,awardConfig.awards:get_Item(i).award_value)
        end
        getGoods.onClose = function ( ... )
            -- self.banTwice = false
        end
		-- for k,value in pairs(resp.awards or {}) do
		-- 	local awardConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(self.Config.award_id)
	 --        local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.parent.transform))
	 --        for i = 0, awardConfig.awards.Count - 1 do
	 --            getGoods:SetGoodsData(awardConfig.awards:get_Item(i).award_id,awardConfig.awards:get_Item(i).award_value)
	 --        end
	 --        getGoods.onClose = function ( ... )
	 --            -- self.banTwice = false
	 --        end
	 --        break
		-- end
		TaskLevelData.AwardRespCallBack  = nil
	end
end
-----------------------------------------------------------------
function AwardLevel:SetData(Config)
	self.Config = Config
	-- print('awardlevel level '..self.level..',choosed level '..self.parent.mChoosedLevel)
	if self.parent.mChoosedLevel == self.level then
		self.uiToggle.startsActive = true
		self.onChoose = true
		print('set toggle to true')
	else
		self.onChoose = false
	end
end
--[[
function AwardLevel:OnPress( ... )
	return function ( go , isPressed )
		if isPressed  then
			self.mbPressed = true
			Scheduler.Instance:AddTimer(0.5, false, self:ShowTips())
		else
			self.mbPressed = false
			self.isShowTips = false
			if self.tipsObj ~= nil then
				NGUITools.Destroy(self.tipsObj)
				self.tipsObj = nil
				Scheduler.Instance:AddTimer(0.1, false, self:EnableClick())
			end
		end

	end
end
function AwardLevel:EnableClick( ... )
	return function ( go )

		-- if self.level <= MainPlayer.Instance.Level then
		UIEventListener.Get(self.uiSpState.gameObject).onClick = LuaHelper.VoidDelegate(self:OnOpenClick())
		-- end
		-- UIEventListener.Get(self.uiSpState.gameObject).onClick = LuaHelper.VoidDelegate(self:OnOpenClick())
		self.uiToggle.enabled = true
	end
end
--]]
--长按显示奖励在左侧或右侧
function AwardLevel:ShowTips()
	return function ( go )
		--if self.mbPressed == false or self.isShowTips == true then
		--	return
		--end
		self.enableClick = false
		--UIEventListener.Get(self.uiSpState.gameObject).onClick = nil
		--self.isShowTips = true
		--self.uiToggle.enabled = false
	    playSound("UI/UI_button5")
	    local  awardpackId =  self.Config.award_id or 0
	    local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(awardpackId)
	    if not AwarConfig then return end
		local goodsID = AwarConfig.awards:get_Item(0).award_id
		local goodsNum = AwarConfig.awards:get_Item(0).award_value
		if goodsID then
			local spriteName
			if goodsID == 1 then
				spriteName = 'com_property_diamond'
			elseif goodsID == 2  then
				spriteName = 'com_property_gold'
			end
			if self.Config.flag == 0 then
				self.tipsObj = CommonFunction.InstantiateObject("Prefab/GUI/ActivityTip", self.rightPos.transform)
				self.tipsObj.transform.localPosition = Vector3.New(45,-145,0)
	    	else
	    		self.tipsObj = CommonFunction.InstantiateObject("Prefab/GUI/ActivityTip", self.leftPos.transform)
				self.tipsObj.transform.localPosition = Vector3.New(120,-145,0)
	    	end
			local icon = self.tipsObj.transform:FindChild("Icon"):GetComponent("UISprite")
			local num = self.tipsObj.transform:FindChild("Num"):GetComponent("UILabel")
			icon.spriteName = spriteName
			num.text = goodsNum
		end
	end

end
return AwardLevel
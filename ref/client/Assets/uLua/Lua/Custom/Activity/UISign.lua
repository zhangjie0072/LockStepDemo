--encoding=utf-8

UISign =
{
	uiName = 'UISign',

	----------------------------------
	onClose,
	listcount = 0,
	viplev,
	currentSign,
	signDay = 0,
	newPlayerID,
	parent,
	signlists,
	fillTimes,
	banTwice = false,

	----------------------------------UI
	uiBtnClose,
	uiAimator,
	uiLeftGrid,
	uiDayGrid,
	uiMonthTotalTimes,
	uiFillSignTimes,
	uiFillSignBtn,
	uiTotalSignTimes,
	uiReceiveSignButton,
	uiReceiveSignButtonRedDot,
	uiScrollViewAsyncLoadItem, -- Added by Conglin
}

-----------------------------------------------------------------
function UISign:Awake()

	self.uiDayGrid = self.transform:FindChild('Window/ScrollView/Grid')
	self.uiScrollViewAsyncLoadItem = self.transform:FindChild('Window/ScrollView'):GetComponent("ScrollViewAsyncLoadItem")

	self.uiMonthTotalTimes = self.transform:FindChild('Window/Left/MonthTimes'):GetComponent('UILabel')
	self.uiFillSignTimes = self.transform:FindChild('Window/Left/Tip'):GetComponent('UILabel')
	self.uiFillSignBtn = self.transform:FindChild('Window/Left/FillSignButton'):GetComponent('UIButton')
	self.uiReceiveSignButton = self.transform:FindChild('Window/Left/ReceiveSignButton'):GetComponent('UIButton')
	self.uiReceiveSignButtonRedDot = self.transform:FindChild('Window/Left/ReceiveSignButton/RedDot'):GetComponent('UISprite')
	self.uiTotalSignTimes = self.transform:FindChild('Window/Left/MonthTimesNum'):GetComponent('UILabel')
	self.uiLeftGrid = self.transform:FindChild('Window/Left/Grid')--GetComponent('Grid')
	self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))

	self.uiAimator = self.transform:GetComponent('Animator')
end

function function_name( index, paren )
		print("!!! index=".. index)
		local ui  = createUI('ButtonClose', parent);
		return ui.gameObject;
end

-- function UISign:OnEnable()
-- 	print("OnEnableOnEnableOnEnableOnEnableOnEnable")
-- end

function UISign:Start()
	local btnClose = getLuaComponent(self.uiBtnClose)
	btnClose.onClick = self:OnCloseClick()


	addOnClick(self.uiFillSignBtn.gameObject, self:OnFillSignClick())
	addOnClick(self.uiReceiveSignButton.gameObject, self:OnTotalAwardClick())

	if self:GetVip() == 0 then
		self.viplev = 0
	else
		self.viplev = 2
	end

	--本月累计签到次数 = 签到次数 + 补签次数
	self.uiMonthTotalTimes.text = MainPlayer.Instance.signInfo.signed_times --self.listcount
	--补签剩余次数 = vip补签次数 - 补签次数
	self.fillTimes = self.viplev - MainPlayer.Instance.signInfo.append_sign_times
	self.uiFillSignTimes.text = string.format(getCommonStr('SIGNIN_ENOUGH_FILLSIGN_TIMES'), self.fillTimes)
	if self.fillTimes <= 0 then
		self.uiFillSignBtn.normalSprite = "com_button_yellow_1"
		self.uiFillSignBtn.transform:GetComponent('BoxCollider').enabled = false
	else
		self.uiFillSignBtn.normalSprite = "com_button_yellow7up"
		self.uiFillSignBtn.transform:GetComponent('BoxCollider').enabled = true
	end
	self:InitDaySign()
	self:OnGetMonthAward()
end

function UISign:FixedUpdate()
	-- body
end

function UISign:OnClose()
	if self.onClose then
		self.onClose()
	end

	NGUITools.Destroy(self.gameObject)
	self.parent:SetModelActive(true)
end

function UISign:OnDestroy()
	self.signlists = {}
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--刷新签到状态
function UISign:Refresh( ... )
	--body
end


-----------------------------------------------------------------
--设置父
function UISign:SetParent(parent)
	self.parent = parent
end

function UISign:CreateItem(lists, signlists, i)
	local daySign = createUI('GoodsIcon', self.uiDayGrid.transform)
	local daySignLua = getLuaComponent(daySign.gameObject)
	daySignLua.showTips = false
	daySignLua.hideNeed = true
	daySignLua.hideLevel = true
	daySignLua.hideNum = false
	daySignLua.hideSign = false
	daySignLua.goodsID = GameSystem.Instance.signConfig:GetDaySignData(i).sign_award
	daySignLua.num = GameSystem.Instance.signConfig:GetDaySignData(i).award_count

	-- 调整特效层级
	local ef = daySign.transform:FindChild("SpecialEffect/Ef_Blink2")
	ef:GetComponent("ParticleSystemRenderer").material.renderQueue = 3042

	-- daySignLua:Refresh()
	-- daySignLua.onClick = self:OnItemClick(i)

	-- print("@@sgin count=".. lists)

	if i <= lists then
		local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(i).vip_level
		if needviplevel and needviplevel ~= 0 then
			if signlists[i] and signlists[i] == 1 then
				if tonumber(i) == tonumber(lists) and MainPlayer.Instance.signInfo.signed == 1 then
					daySignLua:SetSignData(true, true, false, true, needviplevel)
					daySignLua.onClick = self:OnItemClick(i)
				else
					daySignLua:SetSignData(true, false, true, false, needviplevel)
				end
				-- l.signBg.color = Color.New(0/255, 161/255, 221/255, 1)
			elseif signlists[i] and signlists[i] == 2 then
				daySignLua:SetSignData(true, false, true, false, needviplevel)
			end
		else
			daySignLua:SetSignData(false, false, true, false, 0)
		end
	else
		daySignLua.onClick = self:OnItemClick(i)
		local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(i).vip_level
		if needviplevel and needviplevel ~= 0 then
			if i == lists + 1 and MainPlayer.Instance.signInfo.signed == 0 then
				daySignLua:SetSignData(true, true, false, false, needviplevel)
				daySignLua.hideLight = false
			else
				daySignLua:SetSignData(true, false, false, false, needviplevel)
			end
		else
			if i == lists + 1 and MainPlayer.Instance.signInfo.signed == 0 then
				daySignLua:SetSignData(false, true, false, false, 0)
				daySignLua.hideLight = false
			else
				daySignLua:SetSignData(false, false, false, false, 0)
			end
		end
	end

	return daySign.gameObject
end


--初始化每项
function UISign:InitDaySign( ... )
	-- body
	local lists = self.listcount
	local signlists = self.signlists
	local initdaysCount = GameSystem.Instance.signConfig:GetSignDays()

	self.uiScrollViewAsyncLoadItem.OnCreateItem = function( index, parent)
		return self:CreateItem(lists, signlists, index + 1)
	end

	self.uiScrollViewAsyncLoadItem:Refresh(initdaysCount)
end

--点击签到
function UISign:OnItemClick(dayID)

	return function (item)
		if not FunctionSwitchData.CheckSwith(FSID.checkin_any) then return end

		local signlists = self.signlists
		self.signDay = tonumber(dayID)
		self.currentSign = self.uiDayGrid.transform:GetChild(dayID - 1)
		if signlists then
			for i,v in ipairs(signlists) do
				if tonumber(i) == tonumber(dayID) then
					local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(i).vip_level
					if needviplevel and needviplevel ~= 0 and signlists[i] == 1 then
						self:OnGetVipAward(dayID)
						return
					end
				end
			end
		end

		--如果今天正常签到了之后禁止继续签到
		if MainPlayer.Instance.signInfo and MainPlayer.Instance.signInfo.signed == 1 then
			CommonFunction.ShowPopupMsg(getCommonStr('SIGNIN_REPEAT'),nil,nil,nil,nil,nil)
			return
		end
		--不能跳签
		if tonumber(dayID) ~= self.listcount + 1 and MainPlayer.Instance.signInfo.signed == 0 then
			CommonFunction.ShowPopupMsg(getCommonStr('SIGNIN_INTURN'),nil,nil,nil,nil,nil)
			return
		elseif tonumber(dayID) ~= self.listcount and MainPlayer.Instance.signInfo.signed == 1 then
			CommonFunction.ShowPopupMsg(getCommonStr('SIGNIN_INTURN'),nil,nil,nil,nil,nil)
			return
		end

		print('current day = ' .. tostring(dayID))
		local req =
		{
			type = 'ST_NORMAL'	--NORMAL = 1 APPEND = 2;
		}
		local buf = protobuf.encode('fogs.proto.msg.SignReq', req)
		LuaHelper.SendPlatMsgFromLua(MsgID.SignReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.SignRespID, self:OnSign(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function UISign:OnCloseClick( ... )
	return function (go)
		-- if self.uiAimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

--签到处理
function UISign:OnSign( ... )
	return function (message)
		CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.SignRespID, self.uiName)

		local resp, err = protobuf.decode('fogs.proto.msg.SignResp', message)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		self.banTwice = true
		MainPlayer.Instance.signInfo.signed_times = MainPlayer.Instance.signInfo.signed_times + 1
		local child = self.currentSign.gameObject
		local l = getLuaComponent(child)
		l:HideLight(true)
		local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).vip_level
		if needviplevel == nil or needviplevel == 0 then	--正常签到
			--禁止今天的签到
			l:SetSignData(false, false, true, false, 0)
			child.transform:GetComponent('BoxCollider').enabled = false

		else 		--vip签到
			if needviplevel and needviplevel <= self:GetVip() then
				if self.signlists[self.signDay] == nil then
					table.insert(self.signlists, 2)
					MainPlayer.Instance.signInfo.sign_list:Add(2)

					--禁止今天的签到
					l:SetSignData(true, false, true, false, needviplevel)
					child.transform:GetComponent('BoxCollider').enabled = false

					if MainPlayer.Instance.signInfo then
						MainPlayer.Instance.signInfo.signed = 1		--设置是否签到
						--refresh sign state
						-- self.parent:RefreshSignState()
					end
					self.listcount = self.listcount + 1
					self.uiMonthTotalTimes.text = MainPlayer.Instance.signInfo.signed_times --self.listcount
				else
					self:OnGetVipAward(self.signDay)	--补领vip奖励
					self:OnGetMonthAward()
					return
				end
				--CommonFunction.ShowPopupMsg(getCommonStr('SIGNIN_SUCCESS'),nil,nil,nil,nil,nil)
				self:OnGetMonthAward()
				local goodsID = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).sign_award
				local num = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).award_count

				if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID) then
					local popup = getLuaComponent(createUI("RoleAcquirePopup"))
					popup:SetData(goodsID)
					popup.onCloseClick = function ( ... )
						self.banTwice = false
					end
					if self.newPlayerID and self.newPlayerID == goodsID then
						popup.IsInClude = false
						self.newPlayerID = nil
					else
						popup.IsInClude = true
						local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).name
						local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).recruit_output_value
						popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
					end
				else
					local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
					getGoods:SetGoodsData(goodsID, 2 * num)
					getGoods.onClose = function ( ... )
						self.banTwice = false
					end
				end
				return
			elseif needviplevel > self:GetVip() then
				local currentGetaward = getLuaComponent(self.currentSign.gameObject)
				currentGetaward:SetSignData(true, true, false, true, needviplevel)
			end
		end

		if MainPlayer.Instance.signInfo then
			MainPlayer.Instance.signInfo.signed = 1		--设置是否签到
			--refresh sign state
			-- self.parent:RefreshSignState()
		end

		table.insert(self.signlists, 1)
		MainPlayer.Instance.signInfo.sign_list:Add(1)
		self.listcount = self.listcount + 1
		self.uiMonthTotalTimes.text = MainPlayer.Instance.signInfo.signed_times --self.listcount
		--CommonFunction.ShowPopupMsg(getCommonStr('SIGNIN_SUCCESS'),nil,nil,nil,nil,nil)
		self:OnGetMonthAward()

		local goodsID = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).sign_award
		local num = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).award_count
		if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID) then
			local popup = getLuaComponent(createUI("RoleAcquirePopup"))
			popup:SetData(goodsID)
			popup.onCloseClick = function ( ... )
				self.banTwice = false
			end
			if self.newPlayerID and self.newPlayerID == goodsID then
				popup.IsInClude = false
				self.newPlayerID = nil
			else
				popup.IsInClude = true
				local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).name
				local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).recruit_output_value
				popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
			end
		else
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			getGoods:SetGoodsData(goodsID, num)
			getGoods.onClose = function ( ... )
				self.banTwice = false
			end
		end
	end
end

--补签条件
function UISign:FillSignCondition( ... )
	local createTime = MainPlayer.Instance.CreateTime
	local refreshTime = GameSystem.Instance.CommonConfig:GetUInt('gNewDayHour')
	--createTime = createTime - refreshTime * 3600
	local createyear = os.date('%Y', createTime)
	local createmonth = os.date('%m', createTime)
	local createday = os.date('%d', createTime)
	-- print(tostring(os.date('%Y%m%d%H',createTime)))

	local currentTime = GameSystem.mTime-- - refreshTime * 3600
	local currentyear = os.date('%Y', currentTime)
	local currentmonth = os.date('%m', currentTime)
	local currentday = os.date('%d', currentTime)
	-- print(tostring(os.date('%Y%m%d%H',currentTime)))

	--print(createday,os.date('%H', createTime),tonumber(currentday))
	if createyear ~= currentyear then createyear = currentyear end

	if createyear == currentyear and createmonth ~= currentmonth then
		createmonth = currentmonth
		createday = 1
	end
	-- print('currentday = ', currentday)
	-- print('createday = ', createday)
	-- print('self.listcount = ', self.listcount)
	if createyear == currentyear and createmonth == currentmonth then
		if tonumber(currentday) - tonumber(createday) >= self.listcount then
			return true
		else
			return false
		end
	else
		return false
	end
	return false
end

--补签
function UISign:OnFillSignClick( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.checkin_re) then return end

		if tonumber(self.fillTimes) <= 0 then
			CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_FILLSIGNTIMES'),nil,nil,nil,nil,nil)
			return
		end

		if self:FillSignCondition() == false then
			CommonFunction.ShowPopupMsg(getCommonStr("ENOUGH_SIGNIN"),nil,nil,nil,nil,nil)
			return
		end

		--补签请求
		local req =
		{
			type = 'ST_APPEND'	--NORMAL = 1 APPEND = 2;
		}
		local buf = protobuf.encode('fogs.proto.msg.SignReq', req)
		LuaHelper.SendPlatMsgFromLua(MsgID.SignReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.SignRespID, self:OnFillSign(), self.uiName)
		CommonFunction.ShowWait()
	end
end

--补签处理
function UISign:OnFillSign( ... )
	return function (message)
	    CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.SignRespID, self.uiName)
		local resp, err = protobuf.decode('fogs.proto.msg.SignResp', message)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end

		self.banTwice = true
		self.fillTimes = self.fillTimes - 1
		if self.fillTimes <= 0 then
			self.uiFillSignBtn.normalSprite = "com_button_yellow_1"
			self.uiFillSignBtn.transform:GetComponent('BoxCollider').enabled = false
		else
			self.uiFillSignBtn.normalSprite = "com_button_yellow7up"
			self.uiFillSignBtn.transform:GetComponent('BoxCollider').enabled = true
		end
		self.uiFillSignTimes.text = string.format(getCommonStr('SIGNIN_ENOUGH_FILLSIGN_TIMES'), self.fillTimes)	--剩余补签次数减一
		MainPlayer.Instance.signInfo.append_sign_times = MainPlayer.Instance.signInfo.append_sign_times + 1	--补签次数加一
		MainPlayer.Instance.signInfo.signed_times = MainPlayer.Instance.signInfo.signed_times + 1
		self.listcount = self.listcount + 1		--累计签到加一
		self.uiMonthTotalTimes.text = MainPlayer.Instance.signInfo.signed_times --self.listcount

		local child = self.uiDayGrid.transform:GetChild(self.listcount - 1)
		local l = getLuaComponent(child.gameObject)
		local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(self.listcount).vip_level
		if needviplevel and needviplevel ~= 0 then		--如果是vip
			if needviplevel <= self:GetVip() then
				l:SetSignData(true, false, true, false, needviplevel)
				child.transform:GetComponent('BoxCollider').enabled = false

				table.insert(self.signlists, 2)
				MainPlayer.Instance.signInfo.sign_list:Add(2)
				-- self.parent:RefreshSignState()

				local goodsID = GameSystem.Instance.signConfig:GetDaySignData(self.listcount).sign_award
				local num = GameSystem.Instance.signConfig:GetDaySignData(self.listcount).award_count
				if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID) then
					local popup = getLuaComponent(createUI("RoleAcquirePopup"))
					popup:SetData(goodsID)
					popup.onCloseClick = function ( ... )
						self.banTwice = false
					end
					if self.newPlayerID and self.newPlayerID == goodsID then
						popup.IsInClude = false
						self.newPlayerID = nil
					else
						popup.IsInClude = true
						local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).name
						local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).recruit_output_value
						popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
					end
				else
					local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
					getGoods:SetGoodsData(goodsID, 2 * num)
					getGoods.onClose = function ( ... )
						self.banTwice = false
					end
				end
				self:OnGetMonthAward()
				return
			else
				local currentGetaward = getLuaComponent(child)
				currentGetaward:SetSignData(true, true, false, true, needviplevel)
			end
		else
			l:SetSignData(false, false, true, false, 0)
			child.transform:GetComponent('BoxCollider').enabled = false
		end

		table.insert(self.signlists, 1)
		MainPlayer.Instance.signInfo.sign_list:Add(1)
		-- self.parent:RefreshSignState()
		local goodsID = GameSystem.Instance.signConfig:GetDaySignData(self.listcount).sign_award
		local num = GameSystem.Instance.signConfig:GetDaySignData(self.listcount).award_count
		if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID) then
			local popup = getLuaComponent(createUI("RoleAcquirePopup"))
			popup:SetData(goodsID)
			popup.onCloseClick = function ( ... )
				self.banTwice = false
			end
			if self.newPlayerID and self.newPlayerID == goodsID then
				popup.IsInClude = false
				self.newPlayerID = nil
			else
				popup.IsInClude = true
				local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).name
				local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).recruit_output_value
				popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
			end
		else
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			getGoods:SetGoodsData(goodsID, num)
			getGoods.onClose = function ( ... )
				self.banTwice = false
			end
		end
		--CommonFunction.ShowPopupMsg(getCommonStr('FILLSIGNIN_SUCCESS'),nil,nil,nil,nil,nil)

		self:OnGetMonthAward()
	end
end

function UISign:OnGetMonthAward( ... )
	UpdateRedDotHandler.MessageHandler("Sign")

	local v = MainPlayer.Instance.signInfo.sign_award
	local t = MainPlayer.Instance.signInfo.signed_times
	local totalaward = GameSystem.Instance.signConfig:GetMonthSignData(v)
	local isLight = false
	self.uiReceiveSignButton.normalSprite = 'com_button_yellow_1'
	self.uiReceiveSignButton.transform:GetComponent('BoxCollider').enabled = false
	-- NGUITools.SetActive(self.uiReceiveSignButtonRedDot.gameObject, t >= totalaward.sign_times)
	print('sign_award = ' , v)
	print('t = ' , t)
	print('totalaward.sign_times = ' , totalaward.sign_times)
	if t >= totalaward.sign_times then
		self.uiReceiveSignButton.normalSprite = 'com_button_yellow7up'
		self.uiReceiveSignButton.transform:GetComponent('BoxCollider').enabled = true
		isLight = true
	end

	if totalaward then
		self.uiTotalSignTimes.text = '/' .. tostring(totalaward.sign_times)
		local parent = self.uiLeftGrid.transform
		while parent.transform.childCount > 0 do
			NGUITools.Destroy(parent.transform:GetChild(0).gameObject)
		end
		local grid = self.uiLeftGrid

		if totalaward.award_count1 > 0 then
			local g = createUI('GoodsIcon', grid.transform)
			local l = getLuaComponent(g)
			l.hideNeed = true
			l.hideLevel = true
			l.hideNum = false
			l.hideLight = not isLight
			l.uiNum.gameObject:SetActive(true)
			l.goodsID = totalaward.sign_award1
			l.num = totalaward.award_count1
			-- addOnClick(g.gameObject, self:OnTotalAwardClick())
		end
		if totalaward.award_count2 > 0 then
			local g = createUI('GoodsIcon', grid.transform)
			local l = getLuaComponent(g)
			l.hideNeed = true
			l.hideLevel = true
			l.hideNum = false
			l.hideLight = not isLight
			l.uiNum.gameObject:SetActive(true)
			l.goodsID = totalaward.sign_award2
			l.num = totalaward.award_count2
			-- addOnClick(g.gameObject, self:OnTotalAwardClick())
		end
		if totalaward.award_count3 > 0 then
			local g = createUI('GoodsIcon', grid.transform)
			local l = getLuaComponent(g)
			l.hideNeed = true
			l.hideLevel = true
			l.hideNum = false
			l.hideLight = not isLight
			l.uiNum.gameObject:SetActive(true)
			l.goodsID = totalaward.sign_award3
			l.num = totalaward.award_count3
			-- addOnClick(g.gameObject, self:OnTotalAwardClick())
		end

		grid:GetComponent('UIGrid').repositionNow = true
	end
end

function UISign:OnTotalAwardClick( ... )
	return function ( ... )
		if not FunctionSwitchData.CheckSwith(FSID.checkin_continuous) then return end

		local v = MainPlayer.Instance.signInfo.sign_award
		local t = MainPlayer.Instance.signInfo.signed_times --self.listcount
		local totalaward = GameSystem.Instance.signConfig:GetMonthSignData(v)
		if not totalaward or t < totalaward.sign_times then
			CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_SIGNIN'),nil,nil,nil,nil,nil)
			return
		end

		local req =
		{
			times = self.listcount,
			type = 'SAT_DAY'
		}
		local buf = protobuf.encode('fogs.proto.msg.GetSignAwardReq', req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetSignAwardReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetSignAwardRespID, self:OnGetTotalAwardRespHandler(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function UISign:OnGetTotalAwardRespHandler( ... )
	return function (message)
		CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetSignAwardRespID, self.uiName)
		local resp, err = protobuf.decode('fogs.proto.msg.GetSignAwardResp', message)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end

		self.banTwice = true
		local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
		getGoods.onClose = function ( ... )
			self.banTwice = false
		end
		local v = MainPlayer.Instance.signInfo.sign_award
		local totalaward = GameSystem.Instance.signConfig:GetMonthSignData(v)
		if totalaward.award_count1 > 0 then
			local goodsID = totalaward.sign_award1
			local num = totalaward.award_count1
			getGoods:SetGoodsData(goodsID, num)
		end
		if totalaward.award_count2 > 0 then
			local goodsID = totalaward.sign_award2
			local num = totalaward.award_count2
			getGoods:SetGoodsData(goodsID, num)
		end
		if totalaward.award_count3 > 0 then
			local goodsID = totalaward.sign_award3
			local num = totalaward.award_count3
			getGoods:SetGoodsData(goodsID, num)
		end

		local parent = self.uiLeftGrid.transform
		while parent.transform.childCount > 0 do
			NGUITools.Destroy(parent.transform:GetChild(0).gameObject)
		end

		MainPlayer.Instance.signInfo.sign_award = MainPlayer.Instance.signInfo.sign_award + 1
		--CommonFunction.ShowPopupMsg(getCommonStr('RECEIVE_SUCCESS'),nil,nil,nil,nil,nil)
		self:OnGetMonthAward()
		-- self.parent:RefreshSignState()
	end
end

function UISign:OnGetVipAward(times)
	local req =
	{
		times = times,
		type = 'SAT_VIP'
	}
	local buf = protobuf.encode('fogs.proto.msg.GetSignAwardReq', req)
	LuaHelper.SendPlatMsgFromLua(MsgID.GetSignAwardReqID, buf)
	LuaHelper.RegisterPlatMsgHandler(MsgID.GetSignAwardRespID, self:OnGetVipAwardRespHandler(), self.uiName)
	CommonFunction.ShowWait()
end

function UISign:OnGetVipAwardRespHandler( ... )
	return function (message)
		CommonFunction.StopWait()
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetSignAwardRespID, self.uiName)
		local resp, err = protobuf.decode('fogs.proto.msg.GetSignAwardResp', message)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
			return
		end

		self.banTwice = true
		local needviplevel = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).vip_level
		local child = self.currentSign.gameObject
		local l = getLuaComponent(child)
		l:SetSignData(true, false, true, true, needviplevel)
		child.transform:GetComponent('BoxCollider').enabled = false

		self.signlists[self.signDay] = 2
		MainPlayer.Instance.signInfo.sign_list:set_Item(self.signDay - 1, 2)
		printTable(self.signlists)

		--CommonFunction.ShowPopupMsg(getCommonStr('VIP_SIGNIN_SUCCESS'),nil,nil,nil,nil,nil)
		local goodsID = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).sign_award
		local num = GameSystem.Instance.signConfig:GetDaySignData(self.signDay).award_count
		if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID) then
			local popup = getLuaComponent(createUI("RoleAcquirePopup"))
			popup:SetData(goodsID)
			popup.onCloseClick = function ( ... )
				self.banTwice = false
			end
			if self.newPlayerID and self.newPlayerID == goodsID then
				popup.IsInClude = false
				self.newPlayerID = nil
			else
				popup.IsInClude = true
				local roleName = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).name
				local Num = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goodsID).recruit_output_value
				popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
			end
		else
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			getGoods:SetGoodsData(goodsID, num)
			getGoods.onClose = function ( ... )
				self.banTwice = false
			end
		end
	end
end

--获取当前vip等级
function UISign:GetVip()
	local level = 0
	local config= GameSystem.Instance.VipPrivilegeConfig
	for i=1,15 do
		local vip_data = config:GetVipData(i)
		if vip_data.consume <= MainPlayer.Instance.VipExp then
			level = i
		else
			break
		end
	end
	return level
end

function UISign:GetNewRole(id)
	self.newPlayerID = id
end

return UISign

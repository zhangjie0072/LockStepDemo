--encoding=utf-8

ActivityWindow =
{
	uiName = 'ActivityWindow',
	--parameters
	addClickFlag = nil,
	giftBoxList,
	vitalityValue,
	parent,
	--UI
	uiProgress,
	uiBox,
	uiTotalActivityNum,
	initX = 175,
	initY = -120,
}

--Awake
function ActivityWindow:Awake()
	self.uiProgress = self.transform:FindChild("Process"):GetComponent("UIProgressBar")
	self.uiTotalActivityNum = self.transform:FindChild("TotalActive/Num"):GetComponent("UILabel")
	LuaHelper.RegisterPlatMsgHandler(MsgID.GetActivityAwardsRespID, self:ActivityAwardsHandler(), self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.UpdateActivityInfoID, self:UpdateActivityInfoHandler(), self.uiName)
end

--Start
function ActivityWindow:Start()

	self.addClickFlag = false
	self:Refresh()
end

function ActivityWindow:FixedUpdate( ... )
end

function ActivityWindow:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetActivityAwardsRespID, self.uiName)
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.UpdateActivityInfoID, self.uiName)

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function ActivityWindow:Refresh()
	self.uiBox = {}
	local progress = {}
	local progressSetion = {}
	local preValue = 0
	--set box  1:未达到,2:已达到,3:已领取
	local i = 1
	local enum = MainPlayer.Instance.activityInfo.gift:GetEnumerator()
	while enum:MoveNext() do
		local gift = enum.Current
		self.uiBox[i] = self.transform:FindChild("BoxGrid/Box" .. i)
		local bag = self.uiBox[i]:FindChild("Bag" .. i)
		local value = self.uiBox[i]:FindChild("Num"):GetComponent("UILabel")
		local data = GameSystem.Instance.activityConfig:GetActivityData(i)
		local sprite = bag:GetComponent("UISprite").spriteName
		if gift == 1 then
			bag:GetComponent("UISprite").spriteName = data.icon
			bag:GetComponent("Animator").enabled = false
		elseif gift == 2 then
			bag:GetComponent("UISprite").spriteName = data.icon
			bag:GetComponent("Animator").enabled = true
		elseif gift == 3 then
			bag:GetComponent("UISprite").spriteName = self["openedIcon"]
			bag:GetComponent("Animator").enabled = false
			local rote = bag.transform.localRotation
			rote.x = 0
			rote.y = 0
			rote.z = 0
			bag.transform.localRotation = rote
		end
		value.text = data.activity
		progress[i] = data.activity
		progressSetion[i] = data.activity - preValue
		preValue = data.activity

		if self.addClickFlag == false then
			print(self.uiName,":------addOnClick")
			addOnClick(bag.gameObject, self:ClickBox(i, gift))
			--UIEventListener.Get(bag.gameObject).onPress = LuaHelper.BoolDelegate(self:MakeOnAwardPress(i))
		end
		if i == 5 then
			break
		end
		i = i + 1
	end
	self.addClickFlag = true
	--set progress
	local activityValue = MainPlayer.Instance.activityInfo.activity
	self.uiTotalActivityNum.text = activityValue
	local index = 0
	for i = 1, 5 do
		index = index + 1
		if activityValue < progress[i] then
			break
		end
	end
	print(self.uiName,"-----value:",activityValue,"--sectionValue:",progress[index],"-- i:",index)
	local sectionValue = 0
	if index == 1 then
		self.uiProgress.value = (activityValue / progress[index]) * (1 / 9)
	else
		self.uiProgress.value = (1 / 9) + (2 / 9) * ((index - 2) + (activityValue - progress[index - 1]) / (progress[index] - progress[index - 1]))
	end
	print(self.uiName,"------progress value:",self.uiProgress.value)
end

function ActivityWindow:ClickBox(i)
	return function()
		if not FunctionSwitchData.CheckSwith(FSID.daily_btn) then return end

		--judge condition
		local gift = MainPlayer.Instance.activityInfo.gift:get_Item(i - 1)
		if gift == 1 then --活跃度未到
			 self:ShowActivityAwardTip(i)
			-- CommonFunction.ShowPopupMsg(getCommonStr("STR_ACTIVITY_LACK"),nil,nil,nil,nil,nil)
			return
		elseif gift == 3 then --宝箱已开启
			CommonFunction.ShowPopupMsg(getCommonStr("STR_BOX_OPENED"),nil,nil,nil,nil,nil)
			return
		end
		--send msg
		print(self.uiName,":----send msg:",i)
		local req = {
			id = i,
		}
		local buf = protobuf.encode("fogs.proto.msg.GetActivityAwardsReq", req)
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.GetActivityAwardsReqID, buf)
		--register msg
		-- LuaHelper.RegisterPlatMsgHandler(MsgID.GetActivityAwardsRespID, self:ActivityAwardsHandler(), self.uiName)
	end
end

function ActivityWindow:ShowActivityAwardTip(i)
	--if self.activityTip then
	--	return
	--end

	local awardID = i
	local data = GameSystem.Instance.activityConfig:GetActivityData(awardID)
	local awardPackConfig = GameSystem.Instance.AwardPackConfigData
	local enum = data.awards:GetEnumerator()
	while enum:MoveNext() do
		local awardPackID = enum.Current
		local packList = awardPackConfig:GetAwardPackDatasByID(awardPackID)
		local packEnum = packList:GetEnumerator()
		while packEnum:MoveNext() do
			local goodsID = packEnum.Current.award_id
			local goodsNum = packEnum.Current.award_value
			if goodsID then
				local spriteName
				if goodsID == 1 then
					spriteName = 'com_property_diamond'
				elseif goodsID == 2  then
					spriteName = 'com_property_gold'
				end
				self.activityTip = createUI("ActivityTip", self.uiBox[i].transform)
				local tipPos = self.activityTip.transform.localPosition
				tipPos.x = self.initX
				tipPos.y = self.initY
				self.activityTip.transform.localPosition = tipPos
				local icon = self.activityTip.transform:FindChild("Icon"):GetComponent("UISprite")
				local num = self.activityTip.transform:FindChild("Num"):GetComponent("UILabel")
				icon.spriteName = spriteName
				num.text = goodsNum
			end
		end
	end
end
--[[
function ActivityWindow:HideAwardTip( ... )
	if self.activityTip then
		NGUITools.Destroy(self.activityTip.gameObject)
		self.activityTip = nil
	end
end
function ActivityWindow:MakeOnAwardPress(i)
	return function (go, isPress)
		local gift = MainPlayer.Instance.activityInfo.gift:get_Item(i - 1)
		if gift == 1 then --活跃度未到
			if isPress then
				self:ShowActivityAwardTip(i)
			else
				self:HideAwardTip()
			end
		end
	end
end
--]]
function ActivityWindow:ActivityAwardsHandler()
	return function (buf)
		print("activity----resp")
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.GetActivityAwardsResp", buf)
		if resp then
			if resp.result == 0 then
				print(self.uiName,":",resp.info.gift,"--count:",table.getn(resp.info.gift))
				for i = 0, (MainPlayer.Instance.activityInfo.gift.Count - 1) do
					MainPlayer.Instance.activityInfo.gift:set_Item(i, resp.info.gift[i + 1])
				end
				MainPlayer.Instance.activityInfo.activity = resp.info.activity
				print(self.uiName,"----awards:",resp.awards,"--count:",resp.awards.Count)
				local goodsAquire = getLuaComponent(createUI("GoodsAcquirePopup"))
				for _,x in pairs(resp.awards) do
					goodsAquire:SetGoodsData(x.id, x.value)
				end
				self:Refresh()

				-- local menu = getLuaComponent(self.parent.uiBtnMenu)
				-- menu:RefreshMenuTips()
				UpdateRedDotHandler.MessageHandler("Daily")
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("ActivityWindow:", err)
		end
		-- LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetActivityAwardsRespID, self.uiName)
	end
end

function ActivityWindow:UpdateActivityInfoHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.UpdateActivityInfo", buf)
		if resp then
			for i = 0, (MainPlayer.Instance.activityInfo.gift.Count - 1) do
					MainPlayer.Instance.activityInfo.gift:set_Item(i, resp.info.gift[i + 1])
			end
			MainPlayer.Instance.activityInfo.activity = resp.info.activity
			self:Refresh()
		end
	end
end

return ActivityWindow

require "common/stringUtil"
UITourover = {
	uiName = "UITourover",
	--ui
	uiGrid,
	uiNum,
	--parameters
	awardsNum,
	increaseRxp,
	clickState = false,
	awards,
	awardsTable = {},
	diamondAwards,
	-- diamondAwardsTable = {},
	awardsList,
	turnType = {
		[1] = "SDTCT_EXPENSIVE",
		[2] = "SDTCT_MEDIUM",
		[3] = "SDTCT_CHEAP",
	},
	cardTable = {},
	allCardTable = {},
	indexTable = {},
	
}

function UITourover:Awake()
	self.uiButtonGrid = self.transform:FindChild("Windows/ButtonBack")

	self.uiGrid = self.transform:FindChild("Windows/Grid")
	self.uiGrid = {}
	for i = 1 , self["cardNum"] do
		self.uiGrid[i] = self.transform:FindChild("Windows/Grid/" .. i )
	end
	self.uiConsumeGrid = {}
	for k = 1, 3 do
		self.uiConsumeGrid[k] = self.transform:FindChild("Windows/Consume" .. k)
	end
	self.uiNum = self.transform:FindChild("Windows/Num"):GetComponent("UILabel")
	self.uiBg = self.transform:FindChild("Windows/Bg").gameObject
	self.uiAnimator = self.transform:GetComponent("Animator")
	addOnClick(self.uiBg, self:ClickTurnBack())
end

function UITourover:Start()
	self:RandomIndex()
	self.uiBackButton = getLuaComponent(createUI("ButtonBack", self.uiButtonGrid.transform))
	self.uiBackButton.onClick = self:OnBackClick()

	self.awardsList = KeyValueDataList.New()
	self.uiNum.text = self.awardsNum .. "/" .. self.awardsNum
	self.remianTimes = self.awardsNum
	--init card
	local awards = self.awards:GetEnumerator()
	local i = 1
	while awards:MoveNext() do
		self.awardsTable[i] = {}
		self.awardsTable[i]["id"] = awards.Current.id
		self.awardsTable[i]["value"] = awards.Current.value
		local card = getLuaComponent(createUI("ShootCard", self.uiGrid[self.indexTable[i]]))
		card:SetFront(awards.Current.id, awards.Current.value, nil, nil, false)
		card.onClick = self:ClickAwardsCard()
		card.index = i
		card.parent = self
		card.isClick = false
		self.cardTable[i] = card
		self.allCardTable[i] = card
		i =  i + 1
	end

	local diamondAwards = self.diamondAwards:GetEnumerator()
	while diamondAwards:MoveNext() do
		print("shoot card count:", i)
		local card = getLuaComponent(createUI("ShootCard", self.uiGrid[i]))
		card:SetFront(diamondAwards.Current.id, diamondAwards.Current.value, nil, nil, false)
		card.turnType = self.turnType[9 - i]
		card.onClick = self:ClickDiamondsCard()
		card.parent = self
		self.allCardTable[i] = card
		i = i + 1
	end
	--init consume
	local consumeList = Split(GlobalConst.SHOOTCARD_DIAMOND, '&')
	for j = 1, 3 do
		local consume = getLuaComponent(createUI("GoodsIconConsume", self.uiConsumeGrid[4 - j]))
		consume.rewardId = 1
		consume.rewardNum = consumeList[j]
		consume.isAdd = false
	end
	-- self:InitCard()
	
	self.actionOnReconn = LuaHelper.Action(self:MakeOnReconn())
	PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected + self.actionOnReconn
end

function UITourover:OnDestroy()
	PlatNetwork.Instance.onReconnected = PlatNetwork.Instance.onReconnected - self.actionOnReconn
end

function UITourover:MakeOnReconn()
	return function ()
		print(self.uiName, "OnReconn")
		self.reconnTime = UnityTime.time
	end
end

function UITourover:FixedUpdate()
	-- TODO 临时解决方案，重连后延迟一秒，避免卡顿导致的动画播放问题
	if self.reconnTime and UnityTime.time - self.reconnTime > 1 then
		self:ShowResult()()
		self.reconnTime = nil
	end
end

function UITourover:OnBackClick()
	return function()
		print(self.uiName,"--remianTimes:",self.remianTimes)
		if self.remianTimes ~= 0 then
			--tip
			self.msg = CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_SHOOT_OPRN_TIP"), 
										nil,
										LuaHelper.VoidDelegate(self:FramClickClose()), 
										LuaHelper.VoidDelegate(self:ShowResult()),
										getCommonStr("BUTTON_CANCEL"), 
										getCommonStr("BUTTON_CONFIRM"))
			return
		end
		self:ShowResult()()
	end
end

function UITourover:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg)
	end
end

function UITourover:ShowResult()
	return function()
		local result = getLuaComponent(createUI("UIMatchResult"))
		result.isWin = true
		result.leagueType = GameMatch.LeagueType.eShoot
		result.awards = self.awardsList
		NGUITools.Destroy(self.gameObject)
	end
end

function UITourover:ClickAwardsCard()
	return function(go)
		if self.clickState then
			return
		end
		self.clickState = true 
		print("open shoot card")
		if self.remianTimes == 0 then
			--tips time use up
			return
		end

		if not self.clickAwards then
			self.clickAwards = 1
		else
			self.clickAwards = self.clickAwards + 1
		end

		self.remianTimes = self.remianTimes - 1
		self.uiNum.text = self.remianTimes .. "/" .. self.awardsNum

		go.remianTimes = self.remianTimes
		go:SetFront(self.awardsTable[self.clickAwards].id, self.awardsTable[self.clickAwards].value, nil, nil, true)
		local data = KeyValueData.New()	
		data.id = self.awardsTable[self.clickAwards].id
		data.value = self.awardsTable[self.clickAwards].value
		self.awardsList:Add(data)
		self.cardTable[go.index].isClick = true
	end
end

function UITourover:ClickDiamondsCard()
	return function(go)
		if self.reconnTime then
			return
		end
		if self.clickState then
			return
		end
		self.clickState = true 
		--registe
		LuaHelper.RegisterPlatMsgHandler(MsgID.ShootOpendCardRespID, self:ClickCardHandler(go), self.uiName)
		local ShootOpenCardReq = {
			type = go.turnType
		}

		local buf = protobuf.encode("fogs.proto.msg.ShootOpenCardReq", ShootOpenCardReq)
		LuaHelper.SendPlatMsgFromLua(MsgID.ShootOpenCardReqID, buf)
		CommonFunction.ShowWait()
		print("send shoot open card req:",ShootOpenCardReq.type)
	end
end

function UITourover:ClickCardHandler(go)
	return function(buf)
		print("recieve shoot open card resp")
		CommonFunction.StopWait()
		local resp, err = protobuf.decode("fogs.proto.msg.ShootOpendCardResp", buf)
		if resp then
			if resp.result == 0 then
				local data = KeyValueData.New()
				print("resp awards id:",resp.awards.id,"---value:",resp.awards.value)
				data.id = resp.awards.id
				data.value = resp.awards.value
				self.awardsList:Add(data)
				go:SetFront(data.id, data.value, nil, nil, true)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				self.clickState = false 
			end
		else
			error("UITourover:", err)
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.ShootOpendCardRespID, self.uiName)
	end
end

function UITourover:ShowElseCard()
	local i = self.awardsNum + 1
	for k,v in pairs(self.cardTable) do
		if v.isClick ~= true then
			v.uiAnimator.enabled = true
			v:SetFront(self.awardsTable[i].id, self.awardsTable[i].value, nil, nil, true)
			i = i + 1
		else
			NGUITools.SetActive(v.uiSele.gameObject, true)
		end
	end
end

function UITourover:ClickTurnBack()
	return function()
		for k,v in pairs(self.allCardTable) do
			v.uiAnimator.enabled = true
			v.uiAnimator:SetTrigger("Turnback")
		end
		self.uiAnimator.enabled = true
		self.uiAnimator:SetTrigger("Click")
	end
end

function UITourover:RandomIndex()
	local a = {}
	math.randomseed(os.time())
	local i = 1
	repeat
		local x = math.random(1, 5)
		if a[x] ~= 1 then
		 	a[x] = 1
		 	self.indexTable[i] = x
		 	i = i + 1
		end
	until  i == 6
end

return UITourover

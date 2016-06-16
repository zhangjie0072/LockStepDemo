require "Custom/Career/CareerUtil"

StarAwardType = {
	BRONZE = 1,
	SILVER = 2,
	GOLDEN = 3,
}

CareerAwardDetailPopup = {
	uiName = 'CareerAwardDetailPopup',

	-----------------------parameters
	AwardType = StarAwardType,
	chapterID = 0,
	awardType ,
	Click = nil,
	currentAward,
	onClose,

	-----------------------UI
	--uiTitle,
	uiScroll,
	uiGrid,
	uiBtnClose,
	uiBtnGet,
	uiAnimator,
}

function CareerAwardDetailPopup:Awake()
	local transform = self.transform:FindChild('Window').transform
	--self.uiTitle = transform:FindChild("Title"):GetComponent("UILabel")
	self.uiScroll = transform:FindChild("Middle/Scroll"):GetComponent("UIScrollView")
	self.uiGrid = transform:FindChild("Middle/Scroll/Grid"):GetComponent("UIGrid")
	self.uiBtnClose = transform:FindChild("ButtonClose")
	self.uiBtnGet = transform:FindChild("ButtonGet")

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function CareerAwardDetailPopup:Start()
	local closeBtn = getLuaComponent(createUI("ButtonClose",self.uiBtnClose))
	closeBtn.onClick = self:OnClickClose()
	addOnClick(self.uiBtnGet.gameObject,self:MakeOnReceive())
	-----------
	self.chapterConfig = GameSystem.Instance.CareerConfigData:GetChapterData(self.chapterID)
	self.chapter = MainPlayer.Instance:GetChapter(self.chapterID)

	local receiveStarNum
	local awardPackID
	local received
	if self.awardType == StarAwardType.BRONZE then
		receiveStarNum = self.chapterConfig.bronze_value
		awardPackID = self.chapterConfig.bronze_award
		received = self.chapter.is_bronze_awarded
	elseif self.awardType == StarAwardType.SILVER then
		receiveStarNum = self.chapterConfig.silver_value
		awardPackID = self.chapterConfig.silver_award
		received = self.chapter.is_silver_awarded
	elseif self.awardType == StarAwardType.GOLDEN then
		receiveStarNum = self.chapterConfig.gold_value
		awardPackID = self.chapterConfig.gold_award
		received = self.chapter.is_gold_awarded
	end
	local canReceive = self.chapter.star_num >= receiveStarNum and not received

	self.currentAward = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(awardPackID)
	if self.currentAward then
		CareerUtil.ListAwardsNumBottom(self.currentAward, self.uiGrid.transform)
		self.uiGrid:Reposition()
		self.uiScroll:ResetPosition()
	else
		error(self.uiName, "No awardPack ID:", awardPackID)
	end
end

function CareerAwardDetailPopup:FixedUpdate( ... )
	-- body
end

function CareerAwardDetailPopup:OnClose( ... )
	if self.onClose then
		self.onClose()
	end
	NGUITools.Destroy(self.gameObject)
end

function CareerAwardDetailPopup:OnDestroy()
	-- LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetChapterStarAwardRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function CareerAwardDetailPopup:MakeOnReceive()
	return function ()
		local req = {
			chapter_id = self.chapterID,
			award_type = self.awardType,
		}

		local buf = protobuf.encode("fogs.proto.msg.GetChapterStarAward", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetChapterStarAwardID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetChapterStarAwardRespID, self:MakeGetAwardHandler(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function CareerAwardDetailPopup:MakeGetAwardHandler()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.GetChapterStarAwardResp", buf)
		CommonFunction.StopWait()
		if resp then
			LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetChapterStarAwardRespID, self.uiName)
			if resp.result == 0 then
				if resp.award_type == 1 then
					self.chapter.is_bronze_awarded = true
					if self.Click then
						self.Click()
					end
					--CommonFunction.ShowPopupMsg(getCommonStr("CAREER_GET_BRONZE_AWARD"),nil,nil,nil,nil,nil)
				elseif resp.award_type == 2 then
					self.chapter.is_silver_awarded = true
					if self.Click then
						self.Click()
					end
					--CommonFunction.ShowPopupMsg(getCommonStr("CAREER_GET_SILVER_AWARD"),nil,nil,nil,nil,nil)
				elseif resp.award_type == 3 then
					self.chapter.is_gold_awarded = true
					if self.Click then
						self.Click()
					end
					--CommonFunction.ShowPopupMsg(getCommonStr("CAREER_GET_GOLD_AWARD"),nil,nil,nil,nil,nil)
				end

				local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
				local enum = self.currentAward:GetEnumerator()
				while enum:MoveNext() do
					local v = enum.Current
					getGoods:SetGoodsData(v.award_id,v.award_value)
				end

				NGUITools.Destroy(self.gameObject)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
		else
			error("CareerAwardDetailPopup:", err)
		end
	end
end

function CareerAwardDetailPopup:OnClickClose()
	return function ()
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

return CareerAwardDetailPopup

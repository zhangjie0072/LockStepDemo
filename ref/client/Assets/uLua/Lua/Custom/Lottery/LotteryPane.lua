LotteryPane = {
	uiName = "LotteryPane",

	type = 0,
	isFake = false,

	onBuy = nil,
}

function LotteryPane:Awake()
	self.wgtBG = getComponentInChild(self.transform, "Bg/Color", "UIWidget")
	self.sprIcon = getComponentInChild(self.transform, "Panel/Detail/Icon/Icon", "UISprite")
	self.lblTitle = getComponentInChild(self.transform, "Bg/Name", "UILabel")
	self.lblDesc = getComponentInChild(self.transform, "Panel/Detail/Desc", "UILabel")
	self.lblTip = getComponentInChild(self.transform, "Panel/Detail/Tip", "UILabel")
	self.lblTipConsume = getComponentInChild(self.transform, "Panel/Detail/TipConsume", "UILabel")
	self.lblTip1 = getComponentInChild(self.transform, "Panel/Detail/BuyOne/Tip", "UILabel")
	self.lblTipConsume1 = getComponentInChild(self.transform, "Panel/Detail/BuyOne/TipConsume", "UILabel")
	self.btnBuy1 = getChildGameObject(self.transform, "Panel/Detail/BuyOne/ButtonOne")
	self.lblTip2 = getComponentInChild(self.transform, "Panel/Detail/BuyTen/Tip", "UILabel")
	self.btnBuy2 = getChildGameObject(self.transform, "Panel/Detail/BuyTen/ButtonTen")

	addOnClick(self.btnBuy1, function () if self.onBuy then self.onBuy(self.type, 1, self.isFree) end end)
	addOnClick(self.btnBuy2, function () if self.onBuy then self.onBuy(self.type, 10, false) end end)

	self.osTime = GameSystem.mTime
end

function LotteryPane:Start()
	UIManager.Instance:BringPanelForward(getChildGameObject(self.transform, "Panel"))
	local colorStr = self["BGColor"..self.type]
	local colorComps = {}
	colorStr:gsub("[^&]+", function (c) table.insert(colorComps, tonumber(c) / 255) end)
	self.wgtBG.color = Color.New(unpack(colorComps))
	self.sprIcon.spriteName = self["Icon"..self.type]

	local obj = getChildGameObject(self.transform, "Panel/Detail/GoodsIconConsume")
	NGUITools.SetActive(obj, true)
	self.iconConsume = getLuaComponent(obj)
	self.iconConsume.isAdd = false
	obj = getChildGameObject(self.transform, "Panel/Detail/BuyOne/GoodsIconConsume")
	NGUITools.SetActive(obj, true)
	self.iconConsume1 = getLuaComponent(obj)
	self.iconConsume1.isAdd = false
	obj = getChildGameObject(self.transform, "Panel/Detail/BuyTen/GoodsIconConsume")
	NGUITools.SetActive(obj, true)
	self.iconConsume2 = getLuaComponent(obj)
	self.iconConsume2.isAdd = false

	self.lottery = GameSystem.Instance.LotteryConfig:GetLottery(self.type, MainPlayer.Instance.Level)
	local confName = (self.type == 1 and "gNormalLotteryRestoreTime" or "gSpecialLotteryRestoreTime")
	self.restoreTime = GameSystem.Instance.CommonConfig:GetUInt(confName)
	confName = (self.type == 1 and "gNormalLotteryFreeTimes" or "gSpecialLotteryFreeTimes")
	self.totalFreeTimes = GameSystem.Instance.CommonConfig:GetUInt(confName)

	self.lblTitle.text = getCommonStr("LOTTERY_TITLE_" .. self.type)
	self.lblDesc.text = getCommonStr("LOTTERY_DESC_" .. self.type)
	self.lblTip2.text = getCommonStr("LOTTERY_TIP_BUY_MULTI_" .. self.type)

	self:Refresh()
end

function LotteryPane:FixedUpdate()
	self.osTime = self.osTime + UnityTime.fixedDeltaTime

	if self.isFake then return end

	--print("LotteryPane:FixedUpdate", "FreeTimes:", self.freeTimes, "TotalFreeTimes", self.totalFreeTimes)
	local hasFreeTimes = self.freeTimes > 0 or self.totalFreeTimes == 0
	if hasFreeTimes and not self.isFree then
		self.lastElapsedTime = self.osTime - self.lastTime
		if self.restoreTime > self.lastElapsedTime then
			local tip = getCommonStr("STR_FREE_COUNT_DOWN"):format(self.GetTimeStr(self.restoreTime - self.lastElapsedTime))
			self.lblTip.text = tip
			self.lblTip1.text = tip
		else
			self:Refresh()
		end
	end
end

function LotteryPane:Refresh()
	NGUITools.SetActive(self.iconConsume.gameObject, not self.isFake)
	NGUITools.SetActive(self.iconConsume1.gameObject, not self.isFake)
	NGUITools.SetActive(self.iconConsume2.gameObject, not self.isFake)

	if self.isFake then return end

	local lotteryInfo = MainPlayer.Instance.LotteryInfo
	self.freeTimes = lotteryInfo["free_times" .. self.type]
	self.lastTime = lotteryInfo["last_time" .. self.type]
	
	--print("LotteryPane:Refresh", "FreeTimes:", self.freeTimes, "TotalFreeTimes", self.totalFreeTimes, "type:", self.type)

	self.lastElapsedTime = self.osTime - self.lastTime
	--print(self.freeTimes, self.lastTime, self.lastElapsedTime, self.restoreTime)

	local hasFreeTimes = self.freeTimes > 0 or self.totalFreeTimes == 0
	if hasFreeTimes and self.lastElapsedTime >= self.restoreTime then
		self.isFree = true
		NGUITools.SetActive(self.iconConsume.gameObject, false)
		NGUITools.SetActive(self.iconConsume1.gameObject, false)
		NGUITools.SetActive(self.lblTipConsume.gameObject, true)
		NGUITools.SetActive(self.lblTipConsume1.gameObject, true)
		local tip = getCommonStr("STR_REMAIN_FREE_TIMES_TODAY"):format(self.freeTimes, self.totalFreeTimes)
		if self.totalFreeTimes == 0 then tip = "" end
		self.lblTip.text = tip
		self.lblTip1.text = tip
	else
		self.isFree = false
		self.iconConsume:SetData(self.lottery.consume_id, self.lottery.consume_num_single, false)
		self.iconConsume1:SetData(self.lottery.consume_id, self.lottery.consume_num_single, false)
		NGUITools.SetActive(self.iconConsume.gameObject, true)
		NGUITools.SetActive(self.iconConsume1.gameObject, true)
		NGUITools.SetActive(self.lblTipConsume.gameObject, false)
		NGUITools.SetActive(self.lblTipConsume1.gameObject, false)
		if not hasFreeTimes then
			local tip = getCommonStr("STR_NO_FREE_TODAY")
			self.lblTip.text = tip
			self.lblTip1.text = tip
		end
	end
	self.iconConsume2:SetData(self.lottery.consume_id, self.lottery.consume_num_multi, false)
end

function LotteryPane.GetTimeStr(seconds)
	local h = math.floor(seconds / 3600)
	local m = math.floor(seconds % 3600 / 60)
	local s = math.floor(seconds % 60)
	return string.format("%02d:%02d:%02d", h, m, s)
end

return LotteryPane

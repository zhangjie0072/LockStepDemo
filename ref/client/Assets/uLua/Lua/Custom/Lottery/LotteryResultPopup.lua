LotteryResultPopup = {
	uiName = "LotteryResultPopup",
	------------------------------
	type,
	count,
	awards,
	onRebuy,
	onClose,
	----------------------------UI
	uiMask,
	uiAnimator,
}

function LotteryResultPopup:Awake()
	self.grid = self.transform:FindChild('IconPosition') --getComponentInChild(self.transform, "Grid", "UIGrid")
	self.title = getComponentInChild(self.transform, "Window/Title", "UILabel")
	self.btnRebuy = getChildGameObject(self.transform, "Window/Button/Rebuy")
	self.lblRebuy = getComponentInChild(self.btnRebuy.transform, "Label", "UILabel")
	self.btnOK = getChildGameObject(self.transform, "Window/Button/OK")
	self.lblTipConsume = getComponentInChild(self.transform, "Window/TipConsume", "UILabel")
	self.uiMask = self.transform:FindChild('Mask'):GetComponent('UISprite')
	self.uiAnimator = self.transform:GetComponent('Animator')

	addOnClick(self.btnRebuy, self:MakeOnRebuy())
	addOnClick(self.btnOK, self:MakeOnOK())
	-- addOnClick(self.uiMask.gameObject, self:MakeOnOK())
end

function LotteryResultPopup:Start()
	local lotteryInfo = MainPlayer.Instance.LotteryInfo
	self.freeTimes = lotteryInfo["free_times" .. self.type]
	self.lastTime = lotteryInfo["last_time" .. self.type]
	self.lastElapsedTime = os.time() - self.lastTime
	local confName = (self.type == 1 and "gNormalLotteryRestoreTime" or "gSpecialLotteryRestoreTime")
	self.restoreTime = GameSystem.Instance.CommonConfig:GetUInt(confName)

	self.isFree = (self.count == 1 and self.freeTimes > 0 and self.lastElapsedTime >= self.restoreTime)

	-- self.iconConsume = getLuaComponent(getChildGameObject(self.transform, "Window/GoodsIconConsume"))
	-- self.iconConsume.isAdd = false
	-- if not self.isFree then
	-- 	self.lottery = GameSystem.Instance.LotteryConfig:GetLottery(self.type, MainPlayer.Instance.Level)
	-- 	local consume_num = (self.count == 1 and self.lottery.consume_num_single or self.lottery.consume_num_multi)
	-- 	self.iconConsume:SetData(self.lottery.consume_id, consume_num, false)
	-- 	NGUITools.SetActive(self.iconConsume.gameObject, true)
	-- 	NGUITools.SetActive(self.lblTipConsume.gameObject, false)
	-- else
	-- 	NGUITools.SetActive(self.iconConsume.gameObject, false)
	-- 	NGUITools.SetActive(self.lblTipConsume.gameObject, true)
	-- end

	self.lblRebuy.text = getCommonStr(self.count == 1 and "BUY_ONE_MORE" or "BUY_TEN_MORE")

	local count = table.getn(self.awards)
	local depth = 3022--self.transform:GetComponent("UIPanel").startingRenderQueue + 1
	local icon = nil
	for i, goods in ipairs(self.awards) do
		if count <= 1 then
			local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.grid:GetChild(10).transform))
			goodsIcon.goodsID = goods.id
			goodsIcon.num = goods.value
			goodsIcon.hideLevel = true
			goodsIcon.hideNeed = true
			goodsIcon.hideNum = false
			local g = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goods.id)
			if g.category == 9 then
				goodsIcon.hideNum = true
			end
			if self.type == 1 and (g.quality >= 2 and g.quality <= 3) then
				local effect = goodsIcon.transform:FindChild('SpecialEffect/Ef_Blink1')
				effect:GetComponent("ParticleSystemRenderer").material.renderQueue = depth
				NGUITools.SetActive(effect.gameObject, true)
			elseif self.type == 2 and (g.quality >= 4 and g.quality <= 5) then
				local effect = goodsIcon.transform:FindChild('SpecialEffect/Ef_Blink1')
				effect:GetComponent("ParticleSystemRenderer").material.renderQueue = depth
				NGUITools.SetActive(effect.gameObject, true)
			end
		else
			local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.grid:GetChild(i - 1).transform))
			goodsIcon.goodsID = goods.id
			goodsIcon.num = goods.value
			goodsIcon.hideLevel = true
			goodsIcon.hideNeed = true
			goodsIcon.hideNum = false
			local g = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goods.id)
			if g.category == 9 then
				goodsIcon.hideNum = true
			end
			if self.type == 1 and (g.quality >= 2 and g.quality <= 3) then
				local effect = goodsIcon.transform:FindChild('SpecialEffect/Ef_Blink1')
				effect:GetComponent("ParticleSystemRenderer").material.renderQueue = depth
				NGUITools.SetActive(effect.gameObject, true)
			elseif self.type == 2 and (g.quality >= 4 and g.quality <= 5) then
				local effect = goodsIcon.transform:FindChild('SpecialEffect/Ef_Blink1')
				effect:GetComponent("ParticleSystemRenderer").material.renderQueue = depth
				NGUITools.SetActive(effect.gameObject, true)
			end
		end
	end
	--self.grid:Reposition()
end

function LotteryResultPopup:MakeOnRebuy()
	return function ()
		local info = self.uiAnimator:GetCurrentAnimatorStateInfo(0)
		if info.normalizedTime < 1 then
			return
		end

		if self.onRebuy then
			self.onRebuy(self.type, self.count, self.isFree)
		end
		NGUITools.Destroy(self.gameObject)
	end
end

function LotteryResultPopup:MakeOnOK()
	return function ()
		if self.onClose then
			self.onClose()
		end
		-- NGUITools.Destroy(self.gameObject)
	end
end

return LotteryResultPopup

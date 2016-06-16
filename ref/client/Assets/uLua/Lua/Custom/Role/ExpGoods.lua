------------------------------------------------------------------------
-- class name    : ExpGoods
-- create time   : Wed Dec  2 16:09:48 2015
------------------------------------------------------------------------

ExpGoods =  {
	uiName     = "ExpGoods",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiGoodsIcon ,
	uiAdd       ,
	uiText      ,

	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	weight,
	goodsIcon,
	onGoodsPressed,
	onGoodsClick,
	isPressed,
	pressedTime,
	prePressedTime,
	aniDur,
	durBase,
	roleDetail,
	onClickBuy,
	usedCount = 0,
	sendDur,
	toSendGoods,
	toSendNum = 0,
	toSendStart,
	goodsEnough = true,
	toSendGoodsTb,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function ExpGoods:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	self.durBase = tonumber(GameSystem.Instance.CommonConfig:GetString("gExpDurBase"))
	self.durRate = tonumber(GameSystem.Instance.CommonConfig:GetString("gExpDurRate"))
	self.durMax = tonumber(GameSystem.Instance.CommonConfig:GetString("gExpDurMax"))
	self.sendDur = 1/ tonumber(GameSystem.Instance.CommonConfig:GetString("gCountPerSec"))
	self.toSendGoodsTb = {}
--	testClickNum = 0
end


function ExpGoods:Start()
	self.aniDur = self.durBase
	self.transform.name = self.id
	addOnClick(self.gameObject, self:ClickBuy())
	self:Refresh()
end

function ExpGoods:Refresh()
	local id = self.id
	local goodsList = MainPlayer.Instance:GetGoodsList(GoodsCategory.GC_CONSUME, id)
	local goodsNum= goodsList.Count

	local goodsAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	local enum = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(goodsAttr.use_result_id).args:GetEnumerator()
	enum:MoveNext()
	local awardList = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(enum.Current.id)
	local awardEnum = awardList:GetEnumerator()
	awardEnum:MoveNext()
	local imporveExpNum = awardEnum.Current.award_value
	self.uiAdd.gameObject:SetActive(goodsNum == 0 )
	self.uiGoodsIcon.gameObject:SetActive(goodsNum ~= 0)
	if goodsNum == 0 then
		self.roleDetail:RefreshRedLv()
	end
	self.uiText.text = "EXP+"..imporveExpNum

	if goodsNum ~= 0 then
		if self.goodsIcon == nil then
			local g = getLuaComponent(createUI("GoodsIcon", self.uiGoodsIcon.transform))
			g.goodsID            = id
			g.displayLeftNum     = goodsNum
			g.onPressCB          = self:PressGoods()
			g.onClick            = self:ClickGoods()
			g.needPlayAnimation  = true
			self.goodsIcon         = g
			else
			self.goodsIcon:Refresh()
		end
	else
		self.isPressed = false
	end
end


function ExpGoods:FixedUpdate()
	if self.respError then
		self.respErrorCounter =	self.respErrorCounter + UnityTime.fixedDeltaTime
		if self.respErrorCounter >= self.respErrorDur then
			self.respError = nil
			self.respErrorCounter = 0
		else
			-- display error then send next time.
			return
		end
	end

	if self.isPressed then
		local passed2 = UnityTime.time - self.prePressedTime
		local num = 0

		if self.goodsEnough then
			local passed = math.modf(UnityTime.time - self.pressedTime)
			local powerRate = math.min(passed, self.durMax)
			self.aniDur = self.durBase * math.pow(self.durRate, powerRate)
			num = math.modf((passed2)/self.aniDur + 0.5)
			-- print("1927 - fiexed num=",num, "passed2=", passed2, "self.aniDur=", self.aniDur, "passed=", passed , "testClickNum=", testClickNum, "fixtime=", UnityTime.fixedDeltaTime, "framCount=",  UnityEngine.UnityTime.frameCount, "UnityTime.time=", UnityTime.time, "1/dur=", 1/self.aniDur)

		end
		if num >= 1 then
			self.prePressedTime = UnityTime.time
			self:ClickGoods()(self.goodsIcon,self.goodsIcon, num)
		end
	end

	if self.toSendStart then
		if self.toSendStart + self.sendDur <= UnityTime.time then
			-- error("self.toSendStart=", self.toSendStart, "self.sendDur=", self.sendDur, "UnityTime.time=", UnityTime.time)
			self:SendGoods()
		end
	end
end


function ExpGoods:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function ExpGoods:UiParse()
	self.uiGoodsIcon = self.transform:FindChild("GoodsIcon"):GetComponent("Transform")
	self.uiAdd       = self.transform:FindChild("Add"):GetComponent("UISprite")
	self.uiText      = self.transform:FindChild("Text"):GetComponent("UILabel")
end

function ExpGoods:SetData(storeData, roleDetail)
	self.id          = storeData.store_good_id
	self.storeData   = storeData
	self.weight      = storeData.store_goods_weight
	self.roleDetail  = roleDetail
end

function ExpGoods:ClickBuy()
	return function()
		if self.onClickBuy then
			if MainPlayer.Instance:GetGoodsCount(self.id) == 0 then
				self:onClickBuy()
			end
		end
	end
end


function ExpGoods:PressGoods()
	return function(item, pressed)
		self.isPressed = pressed
		if pressed then
			self.pressedTime = UnityTime.time
			self.prePressedTime = UnityTime.time
--			print("1927 - presGoods self.prePressedTime=",self.prePressedTime)
		else
			self.aniDur = self.durBase
			self:CanclePressed()
		end
	end
end

function ExpGoods:ClickGoods()
	return function(item, item, num)
		if num == nil then
			num = 1
		end
		local goods, usedCount = self:FindAvailableGoods(self.id)
		if goods then
			num = math.min(num, goods:GetNum() - self.usedCount)
			if self.toSendStart == nil then
				self.toSendStart = UnityTime.time
			end
			self.usedCount = self.usedCount + num
			self.toSendGoods = goods
			self.toSendNum = self.toSendNum + num
		else
			if self.usedCount == 0 then
				CommonFunction.ShowTip(getCommonStr("NO_CURRENT_ITEM"), nil)
				self.aniDur = 1.0
			end
		end
		self.goodsEnough = goods ~= nil or self.usedCount ~= 0
	end
end


function ExpGoods:SendGoods()
	if self.toSendGoods then
		self.roleDetail:SendGoodsUse(self, self.toSendGoods, self.toSendNum)
		table.insert(self.toSendGoodsTb, self.toSendNum)
		self.toSendNum = 0
		self.toSendGoods = nil
		self.toSendStart = nil
	end
end

function ExpGoods:FindAvailableGoods(goodsId)
	local category = fogs.proto.msg.GoodsCategory.GC_CONSUME
	local goodsList = MainPlayer.Instance:GetGoodsList(category, goodsId)

	local enum = goodsList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current
		if goods and goods:GetNum() - self.usedCount > 0 then
			return goods, self.usedCount
		end
	end
end

function ExpGoods:RecvGoodsUse(num)
	local success = num ~= nil
	if num ~= nil then
		if #self.toSendGoodsTb >= 1 and num == self.toSendGoodsTb[1] then
			table.remove(self.toSendGoodsTb, 1)
		else
			error("error for num="..num, "self.toSendNumTb[1]=", self.toSendNumTb[1])
		end
	else
		if #self.toSendGoodsTb >= 1 then
			num = self.toSendGoodsTb[1]
			table.remove(self.toSendGoodsTb, 1)
		else
			error("num = nil and self.toSendGoodsTb size is smaller than 1")
		end
	end
	self.usedCount = self.usedCount - num
	if self.isPressed and success then
		self.goodsIcon:StartSparkle()
	end
	self:Refresh()
end


function ExpGoods:GetAniDur()
	return self.aniDur
end

function ExpGoods:CanclePressed()
	self.isPressed = false
end


return ExpGoods

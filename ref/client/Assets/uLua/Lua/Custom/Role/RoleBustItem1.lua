------------------------------------------------------------------------
-- class name	: RoleBustItem1
-- create time   : 20150813_134526
------------------------------------------------------------------------
require "common/stringUtil"

RoleBustItem1 =  {
	uiName	 = "RoleBustItem1",
	----------------------------
	is_UIMember = false,
	--------------------------UI
	uiSide,
	uiTalent,
	uiLevel,
	uiIcon,
	uiPosition,
	uiRoleName,
	uiStar1,
	uiStar2,
	uiStar3,
	uiLock,
	uiRecruitText,
	uiRedDot,
	uiExpTitle,
	uiExpNum,
	uiExpValue,
	uiExpLvAnimator,
	uiFightNode,
	uiFightNumGrid,
	uiQualityShadeGrid,
	uiCardBustitemAnim,
	uiBg,
	------------------------parameters
	id,
	useType,
	onClickSelect,
	position,
	showRecruit = false,
	onClickRecruit,
	position,
	isResetDisplay = false, -- used for display reset role.
	roleInfo,
	positions = {'PF','SF','C','PG','SG'},
	displayExpSchedule = false,
	isExpMoving = false,
	aniDur,
	prevTime,
	targetLv,
	hideFightNum = false,
	fightNumSprites = nil,
	starList = nil,
	qualityColLvTb,
	qualityCardTb,
	qualityShadeTb,
	fightNum,
	selectSideTb,
	baseData = nil,
}




function RoleBustItem1:Awake()
	-----------role info
	self.uiSide             = self.transform:FindChild("Info/Side"):GetComponent("UISprite")
	self.uiTalent           = self.transform:FindChild("Info/Talent"):GetComponent("UISprite")
	self.uiLevel            = self.transform:FindChild("Info/Level"):GetComponent("UILabel")
	self.uiLock             = self.transform:FindChild("Info/Lock"):GetComponent("UISprite")
	self.uiIcon             = self.transform:FindChild("Info/Icon"):GetComponent("UISprite")
	self.uiPosition         = self.transform:FindChild("Info/Position"):GetComponent("UISprite")
	self.uiRoleName         = self.transform:FindChild("Info/Name"):GetComponent("UILabel")
	self.uiMask             = self.transform:FindChild("Mask")
	self.uiMaskUp           = self.transform:FindChild("MaskUp")
	self.uiTipsOut          = self.transform:FindChild("TipsOut")
	self.uiTipsOutLabel     = self.transform:FindChild("TipsOut/Label"):GetComponent("UILabel")
	self.uiRecruit          = self.transform:FindChild("Recuit")
	self.uiRecruitBtn       = self.transform:FindChild("Recuit/ButtonRecuit"):GetComponent("UIButton")
	self.uiRecruitText      = self.transform:FindChild("Recuit/ButtonRecuit/Text"):GetComponent("MultiLabel")
	self.uiRedDot           = self.transform:FindChild("Info/RedDot"):GetComponent("UISprite")
	self.uiExpSchedule      = self.transform:FindChild("ExpSchedule")
	self.uiExpTitle         = self.transform:FindChild("ExpSchedule/Title"):GetComponent("UILabel")
	self.uiExpNum           = self.transform:FindChild("ExpSchedule/Num"):GetComponent("UILabel")
	self.uiExpValue         = self.transform:FindChild("ExpSchedule"):GetComponent("UIProgressBar")
	self.uiExpLvAnimator    = self.transform:FindChild("ExpSchedule/E_Levelup"):GetComponent("Animator")
	self.uiFightNode        = self.transform:FindChild("Info/FightingForce/FightNum")
	self.uiQualityShadeGrid = self.transform:FindChild("Info/Quality"):GetComponent("UIGrid")
	self.uiCardBustitemAnim	= self.transform:FindChild("Ef_Bustitem"):GetComponent('Animator')
	self.uiBg               = self.transform:FindChild("Info/BG"):GetComponent("UISprite")

	-----------recuit

	addOnClick(self.uiMask.gameObject,self:MakeOnCard())
	addOnClick(self.gameObject,self:MakeOnCard())
	--addOnClick(self.go.panel2_bg.gameObject, self:click())
	--addOnClick(self.go.card_c_select.gameObject, self:click_cselect())
	NGUITools.SetActive(self.uiMask.gameObject,false)
	NGUITools.SetActive(self.uiMaskUp.gameObject,false)
	NGUITools.SetActive(self.uiTipsOut.gameObject,false)
	NGUITools.SetActive(self.uiTipsOutLabel.gameObject,false)

	self.starList = {}
	for i = 1, 5 do
		table.insert(self.starList, 1, self.transform:FindChild("Info/Star/Star"..i):GetComponent("UISprite"))
	end
	local colStr = GameSystem.Instance.CommonConfig:GetString("gQualityColLv")
	self.qualityColLvTb = {}
	local items = Split(colStr, "&")
	for k, v in pairs(items) do
		table.insert(self.qualityColLvTb, tonumber(v))
	end

	local cardStr = GameSystem.Instance.CommonConfig:GetString("gQualityCard")
	self.qualityCardTb = {}
	items = Split(cardStr, "&")
	for k, v in pairs(items) do
		table.insert(self.qualityCardTb, v)
	end



	self.qualityShadeTb = {}
	for i = 1, 4 do
		table.insert(self.qualityShadeTb, self.transform:FindChild("Info/Quality/"..i):GetComponent("UISprite"))
	end

	self.uiQualityShadeGrid.onCustomSort = function(x, y)
		return tonumber(x.transform.name) < tonumber(y.transform.name) and -1 or 1
	end

	local selectSideStr = GameSystem.Instance.CommonConfig:GetString("gRoleSelectSide")
	self.selectSideTb = {}
	items = Split(selectSideStr, "&")
	for k, v in pairs(items) do
		table.insert(self.selectSideTb, v)
	end

end

function RoleBustItem1:Start()
	--------------
	if self.useType == "Challenge" then
		NGUITools.SetActive(self.uiRecruit.gameObject,false)
	end
	self.consume = getLuaComponent(self.transform:FindChild("Recuit/GoodsIconConsume").gameObject)
	self.consume.isAdd = false

	addOnClick(self.uiRecruitBtn.gameObject,self:ClickRecruit())
	self.fightNum = getLuaComponent(createUI("FightNum", self.uiFightNode))
	if self.fightNum == nil then
		CommonFunction.Break()
	end
	self:Refresh()
end

function RoleBustItem1:Refresh()
	if self.id == nil then
		print('error -- set role id first !')
		return
	end
	--------------------
	local config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local position = config.position
	local talent = config.talent
	local name = config.name
	self.baseData = config
	--------------static
	self.uiIcon.atlas = getBustAtlas( self.id)
	self.uiIcon.spriteName = "icon_bust_"..tostring( self.id)
	self.uiIcon:MakePixelPerfect()
	self.uiTalent.spriteName = self["CardTalent_" .. talent]
	self.uiPosition.spriteName = "PT_"..self.positions[position]
	self.uiRoleName.text = name
	-----------------------------dynamic
	self.isOwned = self:Owned()
	self:SetLevel()
	self:SetQuality()
	self:SetStar()
	self:SetLock()
	self:SetExp()
	self:SetFightNum()

	self.position = position
	self.uiBg.spriteName = config.icon_bg
	NGUITools.SetActive(self.uiRecruit.gameObject, self.showRecruit)

	if self.showRecruit then
		local costId = self.baseData.recruit_consume_id
		local costValue = self.baseData.recruit_consume_value

		local ownedNum = MainPlayer.Instance:GetGoodsCount(costId)
		if ownedNum >= costValue then
			self.uiRecruitText:SetText( getCommonStr("STR_RECRUIT"))
		else
			self.uiRecruitText:SetText( getCommonStr("WANTED"))
		end
		local str = ownedNum .. "/" .. costValue
		self.consume:SetData(costId, str)
		self.consume.isPiece = true
	end

	if self.isResetDisplay then
		self:SetResetDisplay()
	end

end

function RoleBustItem1:Owned()
	if MainPlayer.Instance:HasRole(self.id) then
		self.roleInfo = MainPlayer.Instance:GetRole2(self.id)
		return true
	else
		return false
	end
end

function RoleBustItem1:SetLevel(needRefresh)
	if needRefresh then
		self.roleInfo = MainPlayer.Instance:GetRole2(self.id)
	end
	if self.isOwned then
		NGUITools.SetActive(self.uiLevel.gameObject, true)
		self.uiLevel.text = "Lv."..self.roleInfo.level
	else
		NGUITools.SetActive(self.uiLevel.gameObject, false)
	end
end

function RoleBustItem1:SetLock()
	NGUITools.SetActive(self.uiLock.gameObject,not self.isOwned)
end

function RoleBustItem1:SetQuality()
	local quality = 1
	if self.isOwned then
		quality = self.roleInfo.quality
	end
	if self.isResetDisplay or self.showRecruit then
		quality = 1
	end

	local lvBase = 0
	for i = 1, 4 do
		if quality >= self.qualityColLvTb[i] then
			lvBase = lvBase + 1
		else
			break
		end
	end

	local offBase = 0
	if quality ~= 1 then
		offBase = quality - self.qualityColLvTb[lvBase]
	end
	self.uiSide.spriteName = self.qualityCardTb[lvBase + 1]
	local coltb = {
		"gree",
		"blue",
		"purple",
		"orange"
	}

	for i = 1, 4 do
		self.qualityShadeTb[i].gameObject:SetActive(i <= lvBase)
		if i <= lvBase  then
			self.qualityShadeTb[i].spriteName = i <= offBase and "com_card_d_"..coltb[lvBase].."2" or "com_card_d_"..coltb[lvBase].."1"
		end
	end
	self.uiQualityShadeGrid.repositionNow = true
	self.uiMask:GetComponent("UISprite").spriteName = self.selectSideTb[lvBase + 1]
end

function RoleBustItem1:SetStar()
	local initStar = self.baseData.init_star
	local star = self.roleInfo and self.roleInfo.star or initStar


	if self.isResetDisplay or self.showRecruit then
		star = initStar
	end

	for i = 1, 5 do
		if star <= 5 then
			self.starList[i].spriteName = star >= i and "career_star" or "career_star_Dim"
		else
			self.starList[i].spriteName = (star -5 )>= i and "com_card_star_purple" or "career_star"
		end
	end
end

function RoleBustItem1:ClickRecruit()
	return function()
		if self.onClickRecruit then
			self:onClickRecruit()
		end
	end
end

function RoleBustItem1:MakeOnCard()
	return function()
		if self.onClickSelect then
			playSound("UI/UI_button5")
			self.onClickSelect(self)
		end
	end
end

function RoleBustItem1:SetMaskActive(active)
	NGUITools.SetActive(self.uiMaskUp.gameObject,active)
end

-- make rolebust item display default.
function RoleBustItem1:SetResetDisplay()
	self.isResetDisplay = true	-- add this line to avoid use this function outter.
	self.uiLevel.text = "Lv.1"
	self.quality = 1
	self:SetQuality()
	-- for i = 1, 5 do
	--	self.starList[i].gameObject:SetActive(false)
	-- end
	NGUITools.SetActive(self.uiLock.gameObject,false)
	NGUITools.SetActive(self.uiRecruit.gameObject, false)
end

function RoleBustItem1:SetState(state)
	NGUITools.SetActive(self.uiRedDot.gameObject, state)
end

function RoleBustItem1:SetPiece()
	if self.baseData == nil then
		self.baseData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	end
	local costId = self.baseData.recruit_consume_id
	local costValue = self.baseData.recruit_consume_value
	local ownedNum = MainPlayer.Instance:GetGoodsCount(costId)
	if ownedNum >= costValue then
		self.uiRecruitText:SetText(getCommonStr("STR_RECRUIT"))
	else
		self.uiRecruitText:SetText(getCommonStr("WANTED"))
	end
	local str = ownedNum .. "/" .. costValue
	self.consume = getLuaComponent(self.transform:FindChild("Recuit/GoodsIconConsume").gameObject)
	self.consume:SetData(costId, str)
	self.consume.isPiece = true
end

function RoleBustItem1:SetExp()
	NGUITools.SetActive(self.uiExpSchedule.gameObject, self.displayExpSchedule)

	if not self.displayExpSchedule or self.isExpMoving then
		return
	end
	local roleInfo = MainPlayer.Instance:GetRole2(self.id)
	local level = roleInfo.level
	if self.targetLv then
		level = level <= self.targetLv and level or self.targetLv -- limit level.
	end
	local currExp = roleInfo.exp
	for i = 1, level - 1 do
		local exp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		currExp = currExp - exp
	end
	currExp = currExp >=0 and currExp or 0

	local nextExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(level)

	self.uiExpTitle.text = "Lv."..level

	self.uiExpNum.text = tostring(currExp).."/".. tostring(nextExp)
	self.uiExpValue.value = currExp / nextExp
end

function RoleBustItem1:StartExpAni(prevLv, prevExp, targetLv, targetExp, dur)
	self.targetLv = targetLv
	self.isExpMoving = true
	self.curExpTotal = prevExp
	self.curExp = prevExp

	self.targetExpTotal = targetExp
	self.targetExp = targetExp

	for i = 1, prevLv - 1 do
		local maxExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		self.curExp = self.curExp - maxExp
		self.targetExp = self.targetExp - maxExp
	end

	self.expSpeed = (targetExp-prevExp)/dur
	self.curLv = prevLv

end

function RoleBustItem1:SetFightNum()
	if self.fightNum == nil then
		return
	end

	local fn
	if not self.showRecruit and not self.hideFightNum then
		local attrData = MainPlayer.Instance:GetRoleAttrsByID(self.id)
		fn = math.modf(attrData.fightingCapacity)
	end

	if self.showRecruit or self.isResetDisplay then
		fn = MainPlayer.Instance:CalcBaseFighting(self.id)
	end
	self.fightNum:SetNum(fn)
end


function RoleBustItem1:FixedUpdate()
	local inter = UnityTime.fixedDeltaTime

	if self.isExpMoving then
		local roleLvConfig = GameSystem.Instance.RoleLevelConfigData
		local delta = self.expSpeed * inter
		self.curExp = math.modf(self.curExp + delta)
		self.curExpTotal = math.modf(self.curExpTotal + delta)
		self.curExpTotal = self.curExpTotal <= self.targetExpTotal and self.curExpTotal or self.targetExpTotal


		local curLv = self:GetLvByExp(self.curExpTotal)
		-- print("add self.expSpeed * inter=",self.expSpeed * inter, "curLv=", curLv)
		if curLv ~= self.curLv then
			self.uiExpLvAnimator:SetTrigger("E_LP")
			for i = self.curLv, curLv - 1 do
				self.curExp = self.curExp - roleLvConfig:GetMaxExp(i)
				self.targetExp = self.targetExp - roleLvConfig:GetMaxExp(i)
			end
			self.curLv = curLv
		end

		if self.curExp >= self.targetExp then
			self.isExpMoving = false
			self.curExp = self.targetExp
		end

		self.curExp = self.curExp >= 0 and self.curExp or 0
		if self.targetLv then
			self.curLv = self.curLv <= self.targetLv and self.curLv or self.targetLv
		end

		local nextExp = roleLvConfig:GetMaxExp(self.curLv)
		self.uiExpTitle.text = "Lv."..self.curLv
		self.uiExpNum.text = tostring(self.curExp).."/".. tostring(nextExp)
		self.uiExpValue.value = self.curExp / nextExp
	end
end


function RoleBustItem1:GetLvByExp(exp)
	local i = 1
	while true do
		exp = exp - GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		if exp < 0 then
			return i
		end
		i = i + 1
	end

end

function RoleBustItem1:ShowEfShine(state)
	NGUITools.SetActive(self.uiCardBustitemAnim.gameObject, state)
end

return RoleBustItem1

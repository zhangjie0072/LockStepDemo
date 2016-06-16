------------------------------------------------------------------------
-- class name    : RoleDetail
-- create time   : Tue Oct 27 15:14:53 2015
------------------------------------------------------------------------
require "common/stringUtil"

RoleDetail =  {
	uiName     = "RoleDetail",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiMask                    ,
	uiRightNode               ,
	uiRoleBustItem            ,
	uiExerciseTab             ,
	uiFreeStyleTab            ,
	uiEnhanceStarTab          ,
	uiExerciseTabRedDot		  ,
	uiEnhanceStarTabRedDot    ,
	uiButtonLeft              ,
	uiButtonRight             ,

	uiRoleNode          ,
	uiRoleAttr          ,
	uiIllustrateBtn     ,
	uiRoleDetailBtn     ,
	uiClose             ,
	uiFightPropertyGrid ,
	uiBasePropertyGrid  ,
	uiFightPropertyNode ,
	uiBasePropertyNode  ,
	uiArrowUp           ,
	uiArrowDown         ,
	uiPolygon1          ,

	------------------------------------
	-------  Exercise Module
	uiExerciseNode          ,
	uiExerciseGrid          ,
	uiExerciseProgressBar   ,
	uiExerciseProgressText  ,
	uiImproveRoleQualityBtn ,
	uiExerciseMaxLevel      ,
	uiEf_RoleUP             ,

	------------------------------------
	-------  Exercise Enhance Module.
	uiExerciseEnhanceNode        ,
	uiCurExerciseCostLeftIcon    ,
	uiCurExerciseCostIcon        ,
	uiCurExerciseItem            ,
	uiRoleLinkGrid               ,
	uiAttrUpgradeGrid            ,
	uiExerciseEnhanceConsume     ,
	uiEnhanceExerciseBtn         ,
	uiEnhanceOneClickExerciseBtn ,
	uiEnhanceExerciseText        ,
	uiEnhanceExerciseBack        ,
	uiEnhanceNeedLabel           ,
	uiEnhanceExerciseSubTitle    ,
	uiEnhanceExerciseCurMaxLevel ,
	uiEnhanceExerciseName        ,


	------------------------------------
	-------  FreeStyle Module.
	uiFreeStyleNode ,
	uiFreeStyleGrid ,

	------------------------------------
	-------  Enhance Star Moduel.
	uiEnhanceStarNode        ,
	uiEnhanceStarLeft        ,
	uiEnhanceStarMiddle      ,
	uiEnhanceStarRight       ,
	uiEnhanceStarArrow       ,
	uiEnhanceStarConsumeGrid ,
	uiEnhanceStarBtn         ,
	uiEnhanceStarBtnRedDot	 ,
	uiAnimator               ,

	uiLvUpTab         ,
	uiLvUpNode        ,
	uiExpGoodsGrid    ,
	uiLvUpProcessNode ,
	uiLvUpLv          ,
	uiLvUpProcessBar  ,
	uiLvUpData        ,
	uiLvUpAnim        ,
	uiLeft            ,
	uiLvUpRedDot      ,



	-----------------------
	-- Parameters Module --
	-----------------------
	id,							-- role id.
	preUi,
	state,						-- "stateExercise" "stateExerciseEnhance" "stateFreeStyle" "stateEnhanceStar"
	roleBustItem,				-- lua script.
	roleInPreState,				-- role in pre state
	exerciseList,				--
	exerciseIdList,				--
	redExerciseList,			--
	isIllustrate = true,
	curExerciseItem,			--
	curExerciseCostLeftIcon,		--
	curExerciseCostIcon,		--
	curExerciseItemSub,			--
	enhanceStarLeft,			--
	ehanceStarMiddle,			--
	enhanceStarRight,			--
	showAttr,					--
	attrTip,					--
	roleDataDetail,
	destroyTime = 0,
	interval = 0,				--
	isEnhance,					--
	curStar,					--
	movey = 0,					--
	moving = false,
	maxPage = 1,
	movePos = 200,
	gridList,
	curPage = 0,
	oriFighty = 0,
	oriBasey = 0,
	roleGraphics,
	skills,
	skillAquirePopup,
	qualityAnimatorDur,
	qualitySkillId,
	qualityAnimatorCounter = 0,
	isLeftoutState = false,
	leftoutSet = false,

	canEnhance = false,
	canTrain = false,


	-- const value.
	totalQualityProgress = 25,
	roleMaxQuality       = 0,
	roleMaxLevel         = 0,
	roleMaxStar          = 10,
	canEnhanceExercise,

	mExpGoods,
	prevExp,
	prevLv,
	goodsEnough,
	aniDur = nil,
	prePressedTime = nil ,
	curExpGoods = nil,
	expBar = nil,
	isLvUpLeftoutState = false,
	isLvUpLeftoutSet = false,
	leftLink,
	packageSell,
	toBuyId,
	toBuyNum,
	isPreUnregister  = false ,
	clickIndex,
	tabList,
	curTab,
	arrowLeftClick = false,
	enhanceStarGoodsList,
	minExp,
	respError,
	respErrorTime,
	respErrorResetDur = 1,
	expStoreList = nil,
	attrInfoList,
	buyErrCd = 0,
	banTwice = false,
	yMin,
	yMax,
	onExerciseResp = nil,
}


-- state define.
local st = {
	exercise        = 1,
	exerciseEnhance = 2,
	freeStyle       = 3,
	starEnhance     = 4,
	lvUp            = 5,
}


---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleDetail:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	self.roleMaxLevel = GameSystem.Instance.CommonConfig:GetUInt("gPlayerMaxLevel")
	self.roleMaxQuality = GameSystem.Instance.CommonConfig:GetUInt("gMaxQualityNum")
	self.onExerciseResp = self.uiEf_RoleUP:GetComponent("AnimationResp")
	self.onExerciseResp:AddResp(self:OnAnimationResp(), self.uiEf_RoleUP.gameObject)
end


function RoleDetail:Start()
	self.roleBustItem = getLuaComponent(createUI("RoleBustItem1", self.uiRoleBustItem))
	self.roleBustItem.id = self.id
	local buttonClose = getLuaComponent(createUI("ButtonClose", self.uiClose))
	buttonClose.onClick = self:ClickClose()
	local enhanceExerciseReturn = getLuaComponent(createUI("ButtonClose", self.uiEnhanceExerciseBack))
	enhanceExerciseReturn.onClick = self:ExerciseSetClose()
	-- enhanceExerciseReturn.onClick = self.uiEnhanceLua.onClose
	enhanceExerciseReturn:SetCloseIcon("com_button_close_b")

	self.gridList ={}
	table.insert(self.gridList, self.uiBasePropertyNode)
	table.insert(self.gridList, self.uiFightPropertyNode)
	self.oriFighty = self.uiFightPropertyNode.transform.localPosition.y
	self.oriBasey = self.uiBasePropertyNode.transform.localPosition.y
	self.roleGraphics = getLuaComponent(self.uiPolygon1.gameObject)
	self.yMin = self.gridList[1].transform.localPosition.y
	self.yMax = self.yMin + self.maxPage * self.movePos

	self.tabList = {}
	table.insert(self.tabList, self.uiExerciseTab)
	table.insert(self.tabList, self.uiLvUpTab)
	table.insert(self.tabList, self.uiFreeStyleTab)
	table.insert(self.tabList, self.uiEnhanceStarTab)

	------------------------------------
	-------  Click Function Start.
	addOnClick(self.uiExerciseTab.gameObject,    self:ClickTab(1))
	addOnClick(self.uiLvUpTab.gameObject, self:ClickTab(2))
	addOnClick(self.uiFreeStyleTab.gameObject,   self:ClickTab(3))
	addOnClick(self.uiEnhanceStarTab.gameObject, self:ClickTab(4))


	addOnClick(self.uiIllustrateBtn.gameObject,  self:ClickIllustrate(true))
	addOnClick(self.uiRoleDetailBtn.gameObject,    self:ClickIllustrate(false))
	addOnClick(self.uiImproveRoleQualityBtn.gameObject, self:ClickImproveRoleQuality(false))
	addOnClick(self.uiEnhanceExerciseBtn.gameObject,    self:ClickEnhanceExercise(1))
	addOnClick(self.uiEnhanceOneClickExerciseBtn.gameObject, self:ClickEnhanceExercise(2))
	addOnClick(self.uiEnhanceStarBtn.gameObject,    self:ClickEnhanceStar())
	addOnClick(self.uiButtonLeft.gameObject, self:ClickButtonLeft(true))
	addOnClick(self.uiButtonRight.gameObject, self:ClickButtonLeft(false))
	------------------------------------
	-------  Click Function End.

	self.showAttr = {}
	self.attrTip = {}
	self.roleDataDetail = {}
	self:UpdateDataDetail(true)

	self.uiFightPropertyGrid:Reposition()
	self.uiBasePropertyGrid:Reposition()

  local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	--local skills = GameSystem.Instance.qualityAttrConfig:GetSkills(self.id)
	--local data = { 1, 3, 6, 10 }


	CommonFunction.ClearGridChild(self.uiFreeStyleGrid.transform)
	local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	self.skills = data.training_skill_show

	self.qualityAnimatorDur = tonumber(self.qualityAnimatorDur)
	-- self:InitEnhanceState()
	LuaHelper.RegisterPlatMsgHandler(MsgID.UseGoodsRespID, self:OnRecevieUseGoods(), self.uiName)

	self.expBar = getLuaComponent(self.uiLvUpProcessNode)
	self.expBar:SetData(self)
	self.leftLink = getLuaComponent(self.uiLeft.gameObject)
	self.minExp = self:GetMinExp()

	self.expStoreList = {}
	local storeList = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsDataList(enumToInt(StoreType.ST_EXP))
	enum = storeList:GetEnumerator()
	while enum:MoveNext() do
		table.insert(self.expStoreList,enum.Current.store_good_id )
	end

	self:Refresh()
end

function RoleDetail:Refresh()
	-- self:RefreshTrainState()
	self:SetState(st.exercise)

	local linkJump = false
	if self.state == st.exercise and MainPlayer.Instance.LinkRoleId  ~= 0 then
		linkJump = true
		if MainPlayer.Instance.LinkTab == 1 then
			local item = self.exerciseList[MainPlayer.Instance.LinkExerciseId]
			self.tabList[MainPlayer.Instance.LinkTab]:GetComponent("UIToggle").value = true
			self:ClickExerciseItem()(item)
			if MainPlayer.Instance.LinkExerciseLeft then
				self:ClickExerciseCostItem()(self.curExerciseCostIcon)
			end
		elseif MainPlayer.Instance.LinkTab == 4 then
			self.clickIndex = MainPlayer.Instance.LinkTab
			self:ClickTab(self.clickIndex)()
			self:ClickStarGoods()(self.enhanceStarGoodsList[MainPlayer.Instance.LinkExerciseId])
		end
		-- self.displayExerciseId = nil
		self:ResetLinkInfo()
	end


	if not linkJump then
		if  self.clickIndex then
			self:ClickTab(self.clickIndex)()
			self.clickIndex = nil
		else
			self:ClickTab(1)()
		end
	end
	self:UpdateArrow()
end

-- uncommoent if needed
function RoleDetail:FixedUpdate()
	if self.interval >= 0.5 then
		for k,v in pairs(self.showAttr) do
			table.insert(self.attrTip, self:ShowAttrUpdate(k, v))
			break
		end
		self.interval = 0
	else
		self.interval = self.interval + UnityTime.fixedDeltaTime
	end


	if next(self.attrTip) then
		self.destroyTime = self.destroyTime + 0.5 * UnityTime.fixedDeltaTime
		if self.destroyTime >= 0.5 then
			NGUITools.Destroy(self.attrTip[1].gameObject)
			local tip = {}
			for i,v in ipairs(self.attrTip) do
				if i ~= 1 then
					table.insert(tip, v)
				end
			end
			self.attrTip = tip
			self.destroyTime = 0
		end
	end

	if NGUITools.GetActive(self.uiEnhance.gameObject) and not self.getExerciseLua then
		self.uiEnhanceLua = getLuaComponent(self.uiEnhance.gameObject)
		self.uiEnhanceLua.onClose = self:ClickExerciseReturn()
		self.getExerciseLua = true
	end

	if self.qualitySkillId then
		self.qualityAnimatorCounter = self.qualityAnimatorCounter + UnityTime.fixedDeltaTime
		if self.qualityAnimatorCounter >= self.qualityAnimatorDur then
			local c = getLuaComponent(createUI("SkillAcquirePopup", self.transform))
			c:SetData(self.qualitySkillId)
			c.onClose = self:CloseSkillAqcuire()
			self.skillAquirePopup = c
			self.qualitySkillId = nil
		end
	end


	if self.respError and self.respErrorTime + self.respErrorResetDur < UnityTime.time then
		self.respError = nil
	end

	if self.buyErrCd > 0 then
		self.buyErrCd = self.buyErrCd - UnityTime.fixedDeltaTime
	end
end


function RoleDetail:OnDestroy()
	if not self.isPreUnregister then
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName)
	end
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
function RoleDetail:UiParse()
	-- Please Do NOT MIND the comment Lines.
	------------------------------------
	-------  Common Mudule
	self.uiMask                 = self.transform:FindChild("Mask"):GetComponent("UISprite")
	self.uiRightNode            = self.transform:FindChild("Right"):GetComponent("Transform")
	self.uiRoleBustItem         = self.transform:FindChild("Role/RoleBustItem"):GetComponent("Transform")
	self.uiExerciseTab          = self.transform:FindChild("Right/Sw/Grid/Train"):GetComponent("UISprite")
	self.uiFreeStyleTab         = self.transform:FindChild("Right/Sw/Grid/Attribute"):GetComponent("UISprite")
	self.uiEnhanceStarTab       = self.transform:FindChild("Right/Sw/Grid/Enhance"):GetComponent("UISprite")
	self.uiExerciseTabRedDot    = self.transform:FindChild("Right/Sw/Grid/Train/RedDot"):GetComponent("UISprite")
	self.uiEnhanceStarTabRedDot = self.transform:FindChild("Right/Sw/Grid/Enhance/RedDot"):GetComponent("UISprite")
	self.uiButtonLeft           = self.transform:FindChild("ButtonLeft"):GetComponent("UIButton")
	self.uiButtonRight          = self.transform:FindChild("ButtonRight"):GetComponent("UIButton")

	self.uiRoleNode          = self.transform:FindChild("Role"):GetComponent("Transform")
	self.uiRoleAttr          = self.transform:FindChild("Role/Attr"):GetComponent("Transform")
	self.uiIllustrateBtn     = self.transform:FindChild("Role/IllustratedBtn"):GetComponent("UISprite")
	self.uiRoleDetailBtn     = self.transform:FindChild("Role/DetailBtn"):GetComponent("UISprite")
	self.uiClose             = self.transform:FindChild("Right/ButtonBackX"):GetComponent("Transform")

	self.uiFightPropertyGrid              = self.transform:FindChild("Role/Attr/Attr/AttrPandect/Grid"):GetComponent("UIGrid")
	self.uiBasePropertyGrid               = self.transform:FindChild("Role/Attr/Attr/AttrBase/Grid"):GetComponent("UIGrid")
	self.uiFightPropertyGrid.hideInactive = false
	self.uiBasePropertyGrid.hideInactive  = false

	self.uiFightPropertyNode = self.transform:FindChild("Role/Attr/Attr/AttrPandect")
	self.uiBasePropertyNode  = self.transform:FindChild("Role/Attr/Attr/AttrBase")
	self.uiArrowUp           = self.transform:FindChild("Role/Attr/ArrowUp"):GetComponent("UISprite")
	self.uiArrowDown         = self.transform:FindChild("Role/Attr/ArrowDown"):GetComponent("UISprite")
	self.uiPolygon1          = self.transform:FindChild("Role/Attr/Background/Polygon1"):GetComponent("UISprite")

	------------------------------------
	-------  Exercise Module
	self.uiExerciseNode              = self.transform:FindChild("Right/Exercise"):GetComponent("Transform")
	self.uiExerciseGrid              = self.transform:FindChild("Right/Exercise/Train/Goods"):GetComponent("UIGrid")
	self.uiExerciseGrid.hideInactive = false

	self.uiExerciseProgressBar   = self.transform:FindChild("Right/Exercise/Train/Schedule/Back"):GetComponent("UIProgressBar")
	self.uiExerciseProgressText  = self.transform:FindChild("Right/Exercise/Train/Schedule/Num"):GetComponent("UILabel")
	self.uiImproveRoleQualityBtn = self.transform:FindChild("Right/Exercise/Train/ButtonYellow"):GetComponent("UIButton")
	self.uiExerciseMaxLevel      = self.transform:FindChild("Right/Exercise/Train/BackTitle/LabelLevel"):GetComponent("UILabel")
	self.uiEf_RoleUP             = self.transform:FindChild("Right/Exercise/Ef_RoleUP"):GetComponent("Animator")



	------------------------------------
	-------  Exercise Enhance Module.
	self.uiExerciseEnhanceNode          = self.transform:FindChild("ExerciseEnhance"):GetComponent("UIPanel")
	self.uiCurExerciseCostLeftIcon      = self.transform:FindChild("ExerciseEnhance/Left/GoodsIcon"):GetComponent("Transform")
	self.uiCurExerciseCostIcon          = self.transform:FindChild("ExerciseEnhance/Right/Up/GoodsGrid/GoodsIcon"):GetComponent("Transform")
	self.uiCurExerciseItem              = self.transform:FindChild("ExerciseEnhance/Right/GoodsIcon"):GetComponent("Transform")

	self.uiRoleLinkGrid                 = self.transform:FindChild("ExerciseEnhance/Left/Scroll/GainGrid"):GetComponent("UIGrid")
	self.uiRoleLinkGrid.hideInactive    = false
	self.uiAttrUpgradeGrid              = self.transform:FindChild("ExerciseEnhance/Right/AttrGrid"):GetComponent("UIGrid")
	self.uiAttrUpgradeGrid.hideInactive = false
	self.uiExerciseEnhanceConsume       = self.transform:FindChild("ExerciseEnhance/Right/Up/GoodsIconConsume/GoodsIconConsume"):GetComponent("Transform")

	self.uiEnhanceExerciseBtn         = self.transform:FindChild("ExerciseEnhance/Right/Up/ButtonYellow_1"):GetComponent("UIButton")
	self.uiEnhanceExerciseText        = self.transform:FindChild("ExerciseEnhance/Right/Up/ButtonYellow_1/Text"):GetComponent("MultiLabel")
	self.uiEnhanceOneClickExerciseBtn = self.transform:FindChild("ExerciseEnhance/Right/Up/ButtonYellow_2"):GetComponent("UIButton")
	self.uiEnhanceExerciseBack        = self.transform:FindChild("ExerciseEnhance/Right/ButtonBack1"):GetComponent("Transform")
	self.uiEnhanceNeedLabel           = self.transform:FindChild("ExerciseEnhance/Right/Up/GoodsGrid/Num"):GetComponent("MultiLabel")
	self.uiEnhanceExerciseSubTitle    = self.transform:FindChild("ExerciseEnhance/Right/SkillLabel"):GetComponent("UILabel")
	self.uiEnhanceExerciseCurMaxLevel = self.transform:FindChild("ExerciseEnhance/Right/CurLevel"):GetComponent("UILabel")
	self.uiEnhance                    = self.transform:FindChild("ExerciseEnhance")
	self.uiEnhanceExerciseName        = self.transform:FindChild("ExerciseEnhance/Left/Name"):GetComponent("UILabel")


	------------------------------------
	-------  FreeStyle Module.
	self.uiFreeStyleNode = self.transform:FindChild("Right/Attribute"):GetComponent("Transform")
	self.uiFreeStyleGrid = self.transform:FindChild("Right/Attribute/Bottom/SkillScroll/SkillGrid"):GetComponent("UIGrid")
	self.uiFreeStyleGrid.hideInactive = false
	------------------------------------
	-------  Enhance Star Moduel.
	self.uiEnhanceStarNode        = self.transform:FindChild("Right/Enhance"):GetComponent("Transform")
	self.uiEnhanceStarLeft        = self.transform:FindChild("Right/Enhance/Top/Left"):GetComponent("Transform")
	self.uiEnhanceStarMiddle      = self.transform:FindChild("Right/Enhance/Top/Middle"):GetComponent("Transform")
	self.uiEnhanceStarRight       = self.transform:FindChild("Right/Enhance/Top/Right"):GetComponent("Transform")
	self.uiEnhanceStarArrow       = self.transform:FindChild("Right/Enhance/Top/Arrow"):GetComponent("Transform")
	self.uiEnhanceStarConsumeGrid = self.transform:FindChild("Right/Enhance/Top/Grid"):GetComponent("UIGrid")
	self.uiEnhanceStarConsumeGrid.hideInactive = false
	self.uiEnhanceStarBtn         = self.transform:FindChild("Right/Enhance/Top/ButtonEnhance"):GetComponent("UIButton")
	self.uiEnhanceStarBtnRedDot	  = self.transform:FindChild("Right/Enhance/Top/ButtonEnhance/RedDot"):GetComponent("UISprite")
	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiExerciseAnimator = self.transform:FindChild("ExerciseEnhance"):GetComponent("Animator")


	------------------------------------
	-------  Lv Up
	self.uiLvUpTab = self.transform:FindChild("Right/Sw/Grid/PlayerUp"):GetComponent("UISprite")
	self.uiLvUpNode = self.transform:FindChild("Right/PlayerUp"):GetComponent("Transform")
	self.uiExpGoodsGrid = self.transform:FindChild("Right/PlayerUp/Top/GoodsGrid"):GetComponent("UIGrid")
	self.uiLvUpProcessNode = self.transform:FindChild("Right/PlayerUp/Process")
	self.uiLvUpLv = self.transform:FindChild("Right/PlayerUp/Process/LabelTitle"):GetComponent("UILabel")
	self.uiLvUpProcessBar = self.transform:FindChild("Right/PlayerUp/Process/Back"):GetComponent("UIProgressBar")
	self.uiLvUpData = self.transform:FindChild("Right/PlayerUp/Process/Data"):GetComponent("UILabel")
	self.uiLvUpAnim = self.transform:FindChild("Right/PlayerUp/Process/E_Levelup"):GetComponent("Animator")
	self.uiLeft = self.transform:FindChild("Left")
	self.uiLvUpRedDot = self.transform:FindChild("Right/Sw/Grid/PlayerUp/RedDot"):GetComponent("UISprite")
end


function RoleDetail:SetData(id,preUi)
	self.id = id
	self.preUi = preUi
end


------------------------------------
-------  Click Function Start.
function RoleDetail:ClickTab(tab)
	return function()
		self.tabList[tab]:GetComponent("UIToggle").value = true
		if self.curTab == tab then
			return
		end

		if tab == 1 then
			self:SetState(st.exercise)
		elseif tab == 2 then
			self:SetState(st.lvUp)
		elseif tab == 3 then
			self:SetState(st.freeStyle)
		elseif tab == 4 then
			self:SetState(st.starEnhance)
		end
	end
end


function RoleDetail:ClickIllustrate(isIllustrate)
	return function()
		if self.isIllustrate == isIllustrate then
			return
		end

		self.isIllustrate = isIllustrate
		self:DataRefresh()

		if not isIllustrate then
			self:SetPage(0)
			self.roleGraphics:SetData(self.id)
			local pos = self.uiFightPropertyNode.transform.localPosition
			pos.y = self.oriFighty
			self.uiFightPropertyNode.transform.localPosition = pos

			pos = self.uiBasePropertyNode.transform.localPosition
			pos.y = self.oriBasey
			self.uiBasePropertyNode.transform.localPosition = pos
			CommonFunction.ClearGridChild(self.uiFightPropertyGrid.transform)
			CommonFunction.ClearGridChild(self.uiBasePropertyGrid.transform)
			self.attrInfoList = {}
			print("#### self.id=", self.id)
			local attrData = MainPlayer.Instance:GetRoleAttrsByID(self.id).attrs
			local enum = attrData:GetEnumerator()
			while enum:MoveNext() do
				local symbol = enum.Current.Key
				local value = enum.Current.Value

				local attrNameConfig = GameSystem.Instance.AttrNameConfigData
				local attrNameData = attrNameConfig:GetAttrData(symbol)
				local display = attrNameData.display
				local type = attrNameData.type
				local name = attrNameData.name

				if display == 1 then
					if type == AttributeType.HEDGING then
						local g = createUI("AttrInfo1", self.uiFightPropertyGrid.transform)
						local t = getLuaComponent(g)
						t:SetName(name)
						t:SetValue(value)
						self.attrInfoList[name] = t
						UIEventListener.Get(g).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
						UIEventListener.Get(g).onPress = LuaHelper.BoolDelegate(self:OnPress())
					elseif type == AttributeType.BASIC then
						local g = createUI("AttrInfo1", self.uiBasePropertyGrid.transform)
						local t = getLuaComponent(g)
						t:SetName(name)
						t:SetValue(value)
						self.attrInfoList[name] = t
						UIEventListener.Get(g).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
						UIEventListener.Get(g).onPress = LuaHelper.BoolDelegate(self:OnPress())
					end
				end
			end
			self.uiFightPropertyGrid:Reposition()
			self.uiBasePropertyGrid:Reposition()
		end
	end
end

function RoleDetail:ClickImproveRoleQuality()
	return function()
		if MainPlayer.Instance:GetRole2(self.id).quality == self.roleMaxQuality then
			CommonFunction.ShowTip(getCommonStr("STR_QUALITY_CANNOT_IMPROVE"), nil)
			return
		end
		local roleId = self.id
		print("Train OnClickUpgrade for role quality roleId =", roleId )
		local operation = {
			role_id = roleId,
		}
		local req = protobuf.encode("fogs.proto.msg.ImproveQualityReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.ImproveQualityReqID,req)
	end
end

function RoleDetail:ClickEnhanceExercise(enhanceType)
	return function()
		local roleId = self.curExerciseItemSub.roleId
		local exerciseId = self.curExerciseItemSub.id

		if enhanceType == 1 then
			print("canEnhanceExercise=",self.canEnhanceExercise)

			if not self.canEnhanceExercise then
				if not self.isLeftoutState then
					self.uiExerciseAnimator:SetTrigger("Leftout")
					self.isLeftoutState = true
					self.leftoutSet = true
				else
					self.uiExerciseAnimator:SetBool("Switch", self.leftoutSet)
					self.leftoutSet = not self.leftoutSet
				end
				return
			end
		end

		if self.curExerciseItemSub.isMax then
			CommonFunction.ShowTip(getCommonStr("EXERCISE_REACH_MAX"), nil)
			return
		end
		if self.banTwice == true then
			return
		end
		self.banTwice = true
		local enough, notEnoughStr = self:CheckExerciseEnough(exerciseId)
		if notEnoughStr == 0 then
			return
		end
		if not enough then
			CommonFunction.ShowTip(notEnoughStr, nil)
			return
		end

		local operation = {
			role_id = roleId,
			exercise_id = exerciseId,
			type = enhanceType,
		}
		local req = protobuf.encode("fogs.proto.msg.EnhanceExerciseReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnhanceExerciseReqID,req)
	end
end

function RoleDetail:ClickEnhanceStar()
	return function()
		if self.curStar >= self.roleMaxStar then
			CommonFunction.ShowTip(getCommonStr("STR_ROLE_ENHANCE_MAX"), nil)
			return
		end

		if MainPlayer.Instance.Gold < self.enHanceConsume then
			if self.banTwice == true then
				return
			end
			self.banTwice = true
			self:ShowBuyTip("BUY_GOLD")
			return
		end

		local roleId = self.id
		print("ClickEnhanceStar roleId =", roleId )
		local operation = {
			role_id = roleId,
		}
		local req = protobuf.encode("fogs.proto.msg.EnhanceLevelReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.EnhanceLevelReqID,req)
	end
end

function RoleDetail:ClickButtonLeft(isLeft)
	return function()
		self.uiButtonLeft.gameObject:SetActive(false)
		self.uiButtonRight.gameObject:SetActive(false)
		if isLeft then
			self.preUi.switchState = -1
		else
			self.preUi.switchState = 1
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName)
		self.isPreUnregister = true

		if self.state == st.exerciseEnhance then
			MainPlayer.Instance.LinkUiName = self.preUi.uiName
			MainPlayer.Instance.LinkRoleId = self.id

			local ei = 0
			for i = 1, #self.exerciseIdList do
				if self.exerciseIdList[i] == self.curExerciseItem.id then
					local sign = isLeft and -1 or 1
					ei = i + sign
				end
			end
			MainPlayer.Instance.LinkExerciseId = self.exerciseIdList[ei]
			MainPlayer.Instance.LinkTab = self.state == st.exerciseEnhance and 1 or 4
			MainPlayer.Instance.LinkExerciseLeft = self.leftoutSet
			self.preUi.switchState = 0
		end
		self:ClickClose()()
	end
end


function RoleDetail:ClickClose()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function RoleDetail:ClickExerciseItem()
	return function(item)
		self.curExerciseItem = item
		self.isLeftoutState = false
		self.leftoutSet = false
		self:SetState(st.exerciseEnhance)
	end
end

function RoleDetail:ClickExerciseCostItem()
	return function(item)
		if not self.isLeftoutState then
			self.uiExerciseAnimator:SetTrigger("Leftout")
			self.isLeftoutState = true
			self.leftoutSet = true
		else
			self.uiExerciseAnimator:SetBool("Switch", self.leftoutSet)
			self.leftoutSet = not self.leftoutSet
		end
	end
end

function RoleDetail:ExerciseSetClose()
	return function()
		if self.uiExerciseAnimator then
			print(self.uiName,"set trigger close")
			self.uiExerciseAnimator:SetTrigger("Close")
		else
			self:ClickExerciseReturn()()
		end
	end
end

function RoleDetail:ClickExerciseReturn()
	return function()
		print(self.uiName,"set state")
		self:SetState(st.exercise)
	end
end


------------------------------------
-------  Click Function End.

function RoleDetail:DataRefresh()
	local state = self.state
	self.curStar = MainPlayer.Instance:GetRole2(self.id).star
	print("DataRefresh state=", state)

	local baseConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)

	if self.started then
		self.roleBustItem:Refresh()
		self.roleInPreState:Refresh()
	end
	print('state : ' .. tostring(state))
	if state == st.exercise then
		self:CovertToExercise(baseConfig)
	elseif state == st.lvUp then
		self:CovertToLvUp()
	elseif state == st.exerciseEnhance then
		self:CovertToExerciseEnhance()
	elseif state == st.freeStyle then
		self:CovertToFreeStyle()
	elseif state == st.starEnhance then
		self:CovertToStar()
	end

	-- Role
	NGUITools.SetActive(self.uiRoleBustItem.gameObject, self.isIllustrate)
	NGUITools.SetActive(self.uiRoleAttr.gameObject, not self.isIllustrate)
	-- self.uiLvUpRedDot.gameObject:SetActive(CanRoleLvUp(self.id))
	self:RefreshRedLv()
end


function RoleDetail:SetState(state)
	print("state=",state)
	if self.state == st.starEnhance then
		if self.isLvUpLeftoutSet then
			self.isLvUpLeftoutSet = false
			self.uiAnimator:SetBool("Switch", false)
		end
	end

	if st.exercise == state then
		self.curTab = 1
	elseif st.lvUp == state then
		self.curTab = 2
	elseif st.freeStyle == state then
		self.curTab = 3
	elseif st.starEnhance == state then
		self.curTab = 4
	end

	self.state = state
	-- set visible.
	-- State.
	local isTabState = state == st.exercise or
		state == st.freeStyle or
		state == st.starEnhance or
		state == st.lvUp

	NGUITools.SetActive(self.uiRightNode.gameObject, isTabState)
	NGUITools.SetActive(self.uiRoleNode.gameObject, isTabState)
	NGUITools.SetActive(self.uiExerciseNode.gameObject, state == st.exercise)
	NGUITools.SetActive(self.uiExerciseEnhanceNode.gameObject, state == st.exerciseEnhance)
	NGUITools.SetActive(self.uiFreeStyleNode.gameObject, state == st.freeStyle)
	NGUITools.SetActive(self.uiEnhanceStarNode.gameObject, state == st.starEnhance)

	NGUITools.SetActive(self.uiLvUpNode.gameObject, state == st.lvUp)
	-- if self:IsInSquad() then
	if (UpdateRedDotHandler.roleSkillList[self.id] and next(UpdateRedDotHandler.roleSkillList[self.id]) ~= nil) or UpdateRedDotHandler.roleImproveList[self.id] ~= nil then
		NGUITools.SetActive(self.uiExerciseTabRedDot.gameObject, true)
	else
		NGUITools.SetActive(self.uiExerciseTabRedDot.gameObject, false)
	end
	if UpdateRedDotHandler.roleEnhanceList[self.id] ~= nil then
		NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, UpdateRedDotHandler.roleEnhanceList[self.id])--state ~= st.starEnhance and self.canEnhance)
	end
	-- end
	
	self:DataRefresh()
end


function RoleDetail:CovertToExercise(baseConfig)
	local roleQuality = MainPlayer.Instance:GetRole2(self.id).quality
	if roleQuality ~= GlobalConst.MAX_QUALITY_NUM then
		self.uiExerciseMaxLevel.text = tostring(roleQuality * 5)
	else
		self.uiExerciseMaxLevel.text = tostring((roleQuality -1 )* 5)
	end
	-- clear.
	self.exerciseIdList = {}
	self.exerciseList = {}
	CommonFunction.ClearGridChild(self.uiExerciseGrid.transform)

	local lvOff = 0				--

	-- local redDotNum = 5
	local exercises = baseConfig.training_slots
	local enum = exercises:GetEnumerator()
	while enum:MoveNext() do
		local exerciseId = enum.Current
		local exerciseItem = getLuaComponent(createUI("GoodsIcon1", self.uiExerciseGrid.transform))
		local exerciseLevel = MainPlayer.Instance:GetExerciseLevel(self.id, exerciseId)
		exerciseItem.isDisplayNum = false
		exerciseItem.needPlayAnimation = true
		exerciseItem:SetData(self.id, exerciseId)
		exerciseItem.onClickAdd = self:ClickExerciseItem()
		exerciseItem:SetState(false)
		exerciseItem.transform.name = tostring(exerciseId)
		exerciseItem:Refresh()
		self.exerciseList[exerciseItem.id] = exerciseItem
		table.insert(self.exerciseIdList, exerciseId)

		lvOff = lvOff + roleQuality * 5 - exerciseLevel

		-- if self.redExerciseList[exerciseId] and self:IsInSquad() then
		-- 	if exerciseLevel < 5 * roleQuality then
		-- 		exerciseItem:SetState(true)
		-- 		redDotNum = redDotNum - 1
		-- 	end
		-- end

		if UpdateRedDotHandler.roleSkillList[self.id] and 
			UpdateRedDotHandler.roleSkillList[self.id][exerciseId] and not self.isMax then
			exerciseItem:SetState(true)
		end
	end
	self.uiExerciseGrid:Reposition()
	-- if redDotNum == 5 then self.canTrain = false end

	-- progress bar.
	local proV1 = self.totalQualityProgress - lvOff
	local proV2 = self.totalQualityProgress
	self.uiExerciseProgressText.text = tostring(proV1) .. "/" .. tostring(proV2)
	self.uiExerciseProgressBar.value = proV1 / proV2

	if lvOff == 0 and roleQuality ~= self.roleMaxQuality then
		self.uiImproveRoleQualityBtn.normalSprite = "com_button_yellow7up"
	else
		self.uiImproveRoleQualityBtn.normalSprite = "com_button_yellow_1"
	end

	print("this id :",self.id,"quality:",MainPlayer.Instance:GetRole2(self.id).quality)

	if self.totalQualityProgress-lvOff >= self.totalQualityProgress and roleQuality ~= self.roleMaxQuality then
		self.uiImproveRoleQualityBtn.transform:FindChild('Ef_Button').gameObject:SetActive(true)
	end

	self.uiImproveRoleQualityBtn.isEnabled = roleQuality ~= self.roleMaxQuality + 1
	self:UpdateAttrInfo()
end


function RoleDetail:CovertToExerciseEnhance()
	local wColors = string.split(self["nameColorW"], ",")
	local gColors = string.split(self["nameColorG"], ",")
	local bColors = string.split(self["nameColorB"], ",")
	local pColors = string.split(self["nameColorP"], ",")
	local oColors = string.split(self["nameColorO"], ",")
	local nameColors = {
		Color.New(wColors[1]/255, wColors[2]/255, wColors[3]/255, wColors[4]/1),
		Color.New(gColors[1]/255, gColors[2]/255, gColors[3]/255, gColors[4]/1),
		Color.New(bColors[1]/255, bColors[2]/255, bColors[3]/255, bColors[4]/1),
		Color.New(pColors[1]/255, pColors[2]/255, pColors[3]/255, pColors[4]/1),
		Color.New(oColors[1]/255, oColors[2]/255, oColors[3]/255, oColors[4]/1)
	}

	local exerciseId = self.curExerciseItem.id
	local exerciseLevel = MainPlayer.Instance:GetExerciseLevel(self.id, exerciseId)
	local roleQuality = MainPlayer.Instance:GetRole2(self.id).quality
	local attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(exerciseId)

	if self.curExerciseItemSub == nil then
		self.curExerciseItemSub = getLuaComponent(createUI("GoodsIcon1", self.uiCurExerciseItem))
	end
	self.curExerciseItemSub.isDisplayNum = false
	self.curExerciseItemSub.needPlayAnimation = true
	self.curExerciseItemSub.isAdd = false
	self.curExerciseItemSub.forceHideMax = true
	self.curExerciseItemSub:SetData(self.curExerciseItem.roleId, exerciseId)
	self.curExerciseItemSub:Refresh()

	local skillUpConfig = GameSystem.Instance.skillUpConfig
	self.uiEnhanceExerciseSubTitle.text = attr.name
	self.uiEnhanceExerciseCurMaxLevel.text = tostring(roleQuality * 5)

	-- Cost
	local costId
	local skillConsume = skillUpConfig:GetSkillConsume(exerciseId,exerciseLevel + 1)
	local enum = skillConsume and skillConsume:GetEnumerator()
	if enum then
		while enum:MoveNext() do
			local id = enum.Current.Key
			local value = enum.Current.Value

			if id == 2 then
				-- gold
				local goodsIconConsume = getLuaComponent(self.uiExerciseEnhanceConsume)
				goodsIconConsume.rewardId = id
				goodsIconConsume.rewardNum = value
				goodsIconConsume.isAdd = false
				goodsIconConsume:Refresh()
			else
				if self.curExerciseCostLeftIcon == nil then
					self.curExerciseCostLeftIcon = getLuaComponent(createUI("GoodsIcon", self.uiCurExerciseCostLeftIcon))
					self.curExerciseCostLeftIcon.hideNeed = true
				end
				self.curExerciseCostLeftIcon.goodsID = id
				self.curExerciseCostLeftIcon:Refresh()
				if self.curExerciseCostIcon == nil then
					self.curExerciseCostIcon = getLuaComponent(createUI("GoodsIcon", self.uiCurExerciseCostIcon))
				end
				local costAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
				self.uiEnhanceExerciseName.text = costAttr.name
				self.uiEnhanceExerciseName.color = nameColors[costAttr.quality]
				local ownedNum = MainPlayer.Instance:GetGoodsCount(id)
				self.curExerciseCostIcon.goodsID = id
				self.curExerciseCostIcon.needPlayAnimation = true
				self.curExerciseCostIcon.hideNeed = true
				self.curExerciseCostIcon:SetNeed()
				self.uiEnhanceNeedLabel:SetText(tostring(ownedNum) .. "/" .. tostring(value))
				self.curExerciseCostIcon.onClick = self:ClickExerciseCostItem()
				self.curExerciseCostIcon.hideLevel = true
				self.curExerciseCostIcon:Refresh()
				if ownedNum < value then
					self.uiEnhanceExerciseText:SetText(getCommonStr("STR_GET_WAYS"))
				else
					self.uiEnhanceExerciseText:SetText(getCommonStr("STR_UPGRADE_SKILL"))
				end
				self.canEnhanceExercise = ownedNum >= value
				costId = id
			end
		end
	end

	if self.isEnhance then
		self.curExerciseCostIcon:StartSparkle()
	end

	-- AttrUpgrade
	CommonFunction.ClearGridChild(self.uiAttrUpgradeGrid.transform)
	local nextLevel = exerciseLevel + 1
	local attrSymbol = skillUpConfig:GetSkillAttrSymbol(exerciseId, nextLevel)
	if attrSymbol then
		enum = attrSymbol:GetEnumerator()
		while enum:MoveNext() do
			local symbol = enum.Current.Key
			local value = enum.Current.Value
			local name = GameSystem.Instance.AttrNameConfigData:GetAttrName(symbol)

			local t = getLuaComponent(createUI("AttrUpgradeItem1", self.uiAttrUpgradeGrid.transform))
			t.attrName = name
			t.curValue = value
			t.showPlus = false
			if exerciseLevel ~= 0 then
				local curDic = skillUpConfig:GetSkillAttrSymbol(exerciseId, exerciseLevel)
				t.prevValue = curDic:get_Item(symbol)
				if self.isEnhance then
					local interpolation = t.curValue - t.prevValue
					self.showAttr[name] = interpolation
				end
			else
				t.prevValue = 0
			end
		end
		self.isEnhance = false
		self.uiAttrUpgradeGrid:Reposition()
	end

	-- RoleLink
	print(self.uiName,"costId:",costId)
	if costId == nil then
		return
	end
	local accessWayType = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(costId).access_way_type
	local accessWay = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(costId).access_way

	CommonFunction.ClearGridChild(self.uiRoleLinkGrid.transform)
	if accessWayType == 1 then
		local awItems = Split(accessWay,'&')
		for k,v in pairs(awItems) do
			if v ~= "" then
				local aw_item = Split(v,':')
				local script = getLuaComponent(createUI('RoleLinkItem2',self.uiRoleLinkGrid.transform))
				script:SetData(accessWayType, aw_item[1],aw_item[2])
				script.roleId = self.id
				script.exerciseId = self.curExerciseItem.id
				script.linkTab = self.curTab
				script.linkUi = self.preUi.uiName
				script.parent = self
			end
		end
	elseif accessWayType == 2 then
		local script = getLuaComponent(createUI('RoleLinkItem2',self.uiRoleLinkGrid.transform))
		local script = getLuaComponent(t)
		script:SetData(accessWayType, accessWay)
		script.roleId = self.id
		script.exerciseId = costId
		script.linkTab = self.curTab
		script.linkUi = self.preUi.uiName
		script.parent = self
	end
	self:UpdateArrow()
	self.uiRoleLinkGrid:Reposition()
end

function RoleDetail:CovertToFreeStyle()

	local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	local skills = data.training_skill_show
	--local skills = GameSystem.Instance.qualityAttrConfig:GetSkills(self.id)
	local data = { 1, 3, 6, 10 }
	CommonFunction.ClearGridChild(self.uiFreeStyleGrid.transform)
	enum = skills:GetEnumerator()
	local index = 1
	while enum:MoveNext() do
		--local skill = enum.Current
		--if skill.value then
			local g = createUI("SkillCare",self.uiFreeStyleGrid.transform)
			g.transform.name = tostring(index)
			local t = getLuaComponent(g)
			t:SetData(self.id, enum.Current, data[index]/14)
			index = index + 1
		--end
	end
	self.uiFreeStyleGrid:Reposition()
end

function RoleDetail:CovertToStar()
	local curStar = self.curStar
	local isMaxStar = curStar == self.roleMaxStar

	NGUITools.SetActive(self.uiEnhanceStarLeft.gameObject, not isMaxStar)
	NGUITools.SetActive(self.uiEnhanceStarRight.gameObject, not isMaxStar)
	NGUITools.SetActive(self.uiEnhanceStarArrow.gameObject, not isMaxStar)
	NGUITools.SetActive(self.uiEnhanceStarConsumeGrid.gameObject, not isMaxStar)
	NGUITools.SetActive(self.uiEnhanceStarMiddle.gameObject, isMaxStar)
	NGUITools.SetActive(self.uiEnhanceStarBtnRedDot.gameObject, false)
	if not isMaxStar then
		self.canEnhance = false
		if self.enhanceStarLeft == nil then
			self.enhanceStarLeft = getLuaComponent(createUI("RoleStarCompare", self.uiEnhanceStarLeft))
		end
		self.enhanceStarLeft:SetData(self.id, curStar)
		self.enhanceStarLeft.isNext = false

		if self.enhanceStarRight == nil then
			self.enhanceStarRight = getLuaComponent(createUI("RoleStarCompare", self.uiEnhanceStarRight))
		end
		self.enhanceStarRight:SetData(self.id, curStar + 1, self.enhanceStarLeft)
		self.enhanceStarRight.isNext = true

		local starData = GameSystem.Instance.starAttrConfig:GetStarAttr(self.id,curStar + 1)
		local enum = starData.consume:GetEnumerator()
		CommonFunction.ClearGridChild(self.uiEnhanceStarConsumeGrid.transform)
		self.enhanceStarGoodsList = {}
		-- local totalNum = 0
		local num = 0
		while enum:MoveNext() do
			local consumeId = enum.Current.Key
			local consumeValue = enum.Current.Value
			-- totalNum = totalNum + 1
			if consumeId ~= 2 then
				local t = getLuaComponent(createUI("GoodsIcon", self.uiEnhanceStarConsumeGrid.transform))
				t.goodsID = consumeId
				t.displayLeftNum = true
				t.costNum = consumeValue
				t.onClick = self:ClickStarGoods()
				local ownedNum = MainPlayer.Instance:GetGoodsCount(consumeId)
				t:SetMask(ownedNum < consumeValue)
				self.enhanceStarGoodsList[consumeId] = t
			else
				local t = getLuaComponent(createUI("GoodsIconConsume", self.uiEnhanceStarConsumeGrid.transform))
				t.rewardId = consumeId
				t.rewardNum = consumeValue
				t.isAdd = false
				self.enHanceConsume = consumeValue
			end
			-- local ownedNum = MainPlayer.Instance:GetGoodsCount(consumeId)
			-- if ownedNum >= consumeValue then
			-- 	num = num + 1
			-- end
		end
		-- if totalNum == num and self:IsInSquad() then
		-- 	NGUITools.SetActive(self.uiEnhanceStarBtnRedDot.gameObject, true)
		-- 	self.canEnhance = true
		-- end
		-- print('UpdateRedDotHandler.roleEnhanceList = ', UpdateRedDotHandler.roleEnhance)
		if UpdateRedDotHandler.roleEnhanceList[self.id] ~= nil then
			print('UpdateRedDotHandler.roleEnhanceList[self.id] = ', UpdateRedDotHandler.roleEnhanceList[self.id])
			NGUITools.SetActive(self.uiEnhanceStarBtnRedDot.gameObject, UpdateRedDotHandler.roleEnhanceList[self.id])
			NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, UpdateRedDotHandler.roleEnhanceList[self.id])
		end
		self.uiEnhanceStarConsumeGrid:Reposition()
	else
		if self.enhanceStarMiddle == nil then
			self.enhanceStarMiddle = getLuaComponent(createUI("RoleStarCompare", self.uiEnhanceStarMiddle))
		end
		if UpdateRedDotHandler.roleEnhanceList[self.id] ~= nil then
			NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, UpdateRedDotHandler.roleEnhanceList[self.id])
		end
		self.enhanceStarMiddle:SetData(self.id, curStar)
	end
end


function RoleDetail:CovertToLvUp()
	self.mExpGoods = {}
	CommonFunction.ClearGridChild(self.uiExpGoodsGrid.transform)
	local storeList = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsDataList(enumToInt(StoreType.ST_EXP))
	local enum = storeList:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current.store_good_id
		local v = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
		local s = getLuaComponent(createUI("ExpGoods", self.uiExpGoodsGrid.transform))
		s:SetData(enum.Current, self)
		s.onClickBuy = self:ClickExpGoodsBuy()
		self.mExpGoods[id] = v
	end
	self.uiExpGoodsGrid.repositionNow = true

	local roleInfo = MainPlayer.Instance:GetRole2(self.id)
	local curLv = roleInfo.level

	local curExp =  roleInfo.exp
	local costExp = 0
	for i = 1, curLv - 1 do
		costExp = costExp + GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
	end
	curExp = curExp - costExp
	local nextExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(curLv)
	if curLv == self.roleMaxLevel then
		nextExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(curLv-1)
	end

	self.uiLvUpLv.text = "Lv."..curLv
	if curLv >= self.roleMaxLevel then
		self.uiLvUpData.text = "MAX"
		self.uiLvUpProcessBar.value = 1.0
	else
		self.uiLvUpProcessBar.value = curExp/nextExp
		self.uiLvUpData.text = tostring(curExp).."/"..tostring(nextExp)
	end

	self.isLvUpLeftoutState = false
	self.isLvUpLeftoutSet = false
end

function RoleDetail:OnClose()
	if self.onClickClose then
		self:onClickClose()
	end
end

function RoleDetail:EnhanceExerciseFromC(resp)
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			print("EnhanceExerciseFromC",resp.role_id)
			self.curExerciseItemSub:StartSparkle()
			self.curExerciseCostIcon:StartSparkle()
			-- self.isEnhance = true
			-- self:RefreshTrainState(resp.exercise)

			self:DataRefresh()
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("EnhanceExerciseFromC(): " .. err)
	end
end

function RoleDetail:improveQualityFromC(resp)
	CommonFunction.HideWaitMask()
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			-- self.isImprove = true
			local quality = resp.new_quality
			-- local data = { 2, 4, 7, 11 }
			-- for k, v in pairs(data) do
			--	if quality == v then
			--		self.qualitySkillId = self.skills[k]
			--		self.qualityAnimatorCounter = 0
			--	end
			-- end
			-- self:RefreshTrainState()
			UpdateRedDotHandler.MessageHandler("Role")
			UpdateRedDotHandler.MessageHandler("Squad")
			UpdateRedDotHandler.MessageHandler("RoleDetail")

			self.uiImproveRoleQualityBtn.transform:FindChild('Ef_Button').gameObject:SetActive(false)
			self.uiEf_RoleUP:SetTrigger("EF_UP")
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("improveQualityFromC(): " .. err)
	end
end


-- function RoleDetail:EventResp(value)
--	if value == "qualityAnimationFinish" then
--		self:DataRefresh()
--	end
-- end


function RoleDetail:EnhanceLevelFromC(resp)
	CommonFunction.HideWaitMask()
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			print("s_hander role enhance level response id",resp.role.id)
			CommonFunction.ShowTip(getCommonStr("ROLE_ENHANCE_STAR_SUCCESS"), nil)
			-- self:RefreshTrainState()
			UpdateRedDotHandler.MessageHandler("Role")
			UpdateRedDotHandler.MessageHandler("Squad")
			UpdateRedDotHandler.MessageHandler("RoleDetail")

			self:DataRefresh()
			self:UpdateDataDetail()
			local c = getLuaComponent(createUI("RoleAdvancePopup", self.transform))
			local star = MainPlayer.Instance:GetRole2(self.id).star
			c:SetData(self.id, star)
			self:UpdateAttrInfo()
			-- self:RefreshCanEnhance()
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("EnhanceLevelFromC(): " .. err)
	end
end

function RoleDetail:CheckExerciseEnough(exerciseId)
	local level = MainPlayer.Instance:GetExerciseLevel(self.id,exerciseId)
	local config = GameSystem.Instance.skillUpConfig
	local skillConsume = config:GetSkillConsume(exerciseId, level + 1)
	local enum = skillConsume and skillConsume:GetEnumerator()
	if enum then
		while enum:MoveNext() do
			local id = enum.Current.Key
			local value = enum.Current.Value
			if id == 2 then
				if MainPlayer.Instance.Gold < value then
					if self.banTwice == true then
						return
					end
					self.banTwice = true
					self:ShowBuyTip("BUY_GOLD")
					return false , 0
				end
			else
				local owned = MainPlayer.Instance:GetGoodsCount(id)
				if owned < value then
					return false, getCommonStr("STR_EXERCIE_LESS"):format(GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).name)
				end
				self.banTwice = false
			end
		end
	end
	return true
end

function RoleDetail:UpdateDataDetail(init)
	local attr_data = MainPlayer.Instance:GetRoleAttrsByID(self.id).attrs
	local enum = attr_data:GetEnumerator()
	while enum:MoveNext() do
		local symbol = enum.Current.Key
		local value = enum.Current.Value

		local attr_name_config = GameSystem.Instance.AttrNameConfigData
		local attr_name_data = attr_name_config:GetAttrData(symbol)
		local display = attr_name_data.display
		local name = attr_name_data.name
		if display==1 then
			if init then
				self.roleDataDetail[name] = value
			else
				local preValue = self.roleDataDetail[name]
				if preValue < value then
					self.showAttr[name] = value - preValue
				end
				self.roleDataDetail[name] = value
			end
		end
	end
end


function RoleDetail:ShowAttrUpdate(names,values)
	local attr = createUI('AttrUpdate', self.roleBustItem.transform)
	local name = attr.transform:FindChild('Sprite/Label_Attr'):GetComponent('UILabel')
	local value = attr.transform:FindChild('Sprite/Label_Value'):GetComponent('UILabel')

	name.text = names
	name.color = Color.New(58/255, 238/255, 89/255, 1)
	value.text = '+' .. tostring(values)

	local attrList = {}
	for k,v in pairs(self.showAttr) do
		if k ~= names then
			attrList[k] = v
		end
	end
	self.showAttr = attrList
	return attr
end


function RoleDetail:MoveDrag()
	return function(go, vec)
		if self.moving then
			return
		end
		if self.curPage == 0 and vec.y <= 0 then
			return
		elseif self.curPage == self.maxPage and vec.y >= 0 then
			return
		end

		local yOff = vec.y
		local curY = self.gridList[1].transform.localPosition.y
		if curY + yOff > self.yMax then
			yOff = self.yMax - curY
		elseif curY + yOff < self.yMin then
			yOff = self.yMin - curY
		end

		self.movey = self.movey + yOff

		local list = self.gridList
		for k,v in pairs(list) do
			local pos = v.transform.localPosition
			pos.y = pos.y + yOff
			v.transform.localPosition = pos
		end
	end
end


function RoleDetail:OnPress()
	return function(go, pressed)
		if not pressed then
			if self.movey > 0 then
				if self.movey > 50 then
					self:MoveRight()
				else
					self:MoveBack(false, false)
				end
			elseif self.movey < 0 then
				if self.movey < -50 then
					self:MoveLeft()
				else
					self:MoveBack(true, false)
				end
			end
		else
			self.movey = 0
		end
	end
end


function RoleDetail:MoveLeft(immediately)
	if not self.moving then
		local list = self.gridList
		if self.curPage > 0 then
			for k,v in pairs(list) do
				local newPos = Vector3.New(v.transform.localPosition.x,v.transform.localPosition.y - self.movePos - self.movey, 0)
				if immediately then
					v.transform.localPosition = newPos
				else
					local tween = TweenPosition.Begin(v.gameObject, 0.5, newPos)
					tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
					self.moving = true
				end
			end
			self:SetPage(self.curPage - 1)
		else
			self:MoveBack(true, immediately)
		end
	end
	self.movey = 0
end


function RoleDetail:MoveRight(immediately)
	if not self.moving then
		local list = self.gridList

		if self.curPage < self.maxPage then
			for k,v in pairs(list) do
				local newPos = Vector3.New(v.transform.localPosition.x ,v.transform.localPosition.y + self.movePos - self.movey, 0)
				if immediately then
					v.transform.localPosition = newPos
				else
					local tween = TweenPosition.Begin(v.gameObject,0.5,newPos)
					tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
					self.moving = true
				end
			end
			self:SetPage(self.curPage + 1)
		else
			self:MoveBack(false, immediately)
		end
	end
	self.movey = 0
end


function RoleDetail:MoveBack(isLeft, immediately)
	local list = self.gridList
	if isLeft then
		for k,v in pairs(list) do
			local newPos = Vector3.New(v.transform.localPosition.x ,v.transform.localPosition.y - self.movey, 0)
			if immediately then
				v.transform.localPosition = newPos
			else
				local tween = TweenPosition.Begin(v.gameObject,0.5,newPos)
				tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
		end
	else
		for k,v in pairs(list) do
			local newPos = Vector3.New(v.transform.localPosition.x ,v.transform.localPosition.y - self.movey, 0)
			if immediately then
				v.transform.localPosition = newPos
			else
				local tween = TweenPosition.Begin(v.gameObject,0.5,newPos)
				tween:SetOnFinished(LuaHelper.Callback(self:MoveFinish()))
				self.moving = true
			end
		end
	end
end

function RoleDetail:MoveFinish()
	return function()
		self.moving = false
	end
end


function RoleDetail:SetPage(page)
	self.curPage = page
	print("curPage = ",self.curPage)
	NGUITools.SetActive(self.uiArrowUp.gameObject, self.curPage ~= 0)
	NGUITools.SetActive(self.uiArrowDown.gameObject, self.curPage ~= self.maxPage)
end

function RoleDetail:CloseSkillAqcuire()
	return function()
		self:ClickTab(3)()
		self.uiFreeStyleTab:GetComponent("UIToggle").value = true
	end
end

function RoleDetail:SendGoodsUse(expGoods, goods,goodsNum)
	self.curExpGoods = expGoods
	local operation = {
		uuid     = goods:GetUUID(),
		category = tostring(fogs.proto.msg.GoodsCategory.GC_CONSUME),
		target   = self.id,
		exp_card = 1,
		num      = goodsNum,
	}

	self.prevExp = MainPlayer.Instance:GetRole2(self.id).exp
	self.prevLv = MainPlayer.Instance:GetRole2(self.id).level

	local req = protobuf.encode("fogs.proto.msg.UseGoods",operation)
	CommonFunction.ShowWait()
	LuaHelper.SendPlatMsgFromLuaNoWait(MsgID.UseGoodsID,req)
end


function RoleDetail:PressedExpItem()
	return function(item, pressed)
	end
end

function RoleDetail:OnRecevieUseGoods()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.UseGoodsResp", buf)
		CommonFunction.StopWait()
		if resp then
			print("resp.result=",resp.result)
			if resp.result == 0 then
				local target       = resp.target
				local target_exp   = resp.target_exp
				local target_level = resp.target_level
				--print("1927 -  resp.target_exp=",resp.target_exp, "resp.target_level=", resp.target_level)

				MainPlayer.Instance:SetRoleLvAndExp(target,target_level,target_exp)
				local role = self.roleBustItem
				role:SetLevel(true)
				role:SetExp()
				self.curExpGoods:RecvGoodsUse(resp.num)
				self.expBar:StartExpAni(self.prevLv, self.prevExp, target_level, target_exp, self.curExpGoods:GetAniDur())
				if target_level >= 50 or target_level == MainPlayer.Instance.Level then
					UpdateRedDotHandler.MessageHandler("Role")
					UpdateRedDotHandler.MessageHandler("Squad")
					UpdateRedDotHandler.MessageHandler("RoleDetail")
				end
				self:RefreshRedLv()
			else
				if self.respError ~= resp.result then
					CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
					self.respErrorTime = UnityTime.time
					if self.curExpGoods then
						self.curExpGoods:RecvGoodsUse(nil)
						self.curExpGoods:CanclePressed()
					end
				end
				self.respError = resp.result

			end
		else
			error("OnRecevieUseGoodsd resp(): " .. err)
		end
	end
end

function RoleDetail:LvUp()
	self.uiLvUpAnim:SetTrigger("E_LP")
	self.roleInPreState:Refresh()
	self.roleBustItem:Refresh()
	self:UpdateAttrInfo()
end

function RoleDetail:RefreshRedLv()
	if UpdateRedDotHandler.roleLevelUpList[self.id] ~= nil then
		self.uiLvUpRedDot.gameObject:SetActive(UpdateRedDotHandler.roleLevelUpList[self.id])
	end
end

function RoleDetail:ClickStarGoods()
	return function(item)
		if not self.isLvUpLeftoutSet or self.leftLink.id == item.goodsID then
			self.isLvUpLeftoutSet = not self.isLvUpLeftoutSet
			self.uiAnimator:SetBool("Switch", self.isLvUpLeftoutSet)
			self.uiRoleNode.gameObject:SetActive(MainPlayer.Instance.LinkRoleId == 0 )
		end
		self.leftLink:SetData(item.goodsID, self.id, self.curTab, self.preUi.uiName)
	end
end

function RoleDetail:ClickExpGoodsBuy()
	return function(expGoodsItem)
		if self.packageSell ~= nil then
			return
		end

		local minLv = expGoodsItem.storeData.apply_min_level
		if MainPlayer.Instance.Level < minLv then
			CommonFunction.ShowTip(string.format(getCommonStr("NOT_REACH_LEVEL_TO_USE"), minLv), nil)
			return
		end

		local p = getLuaComponent(createUI("PackageSell"))
		p.goodsId = expGoodsItem.id
		p.on_click_sell = self:Buy()
		p.onClose = self:ClosePackageSell()
		p.isSell = false
		p.storeData = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsData(7, expGoodsItem.id)
		local enum = GameSystem.Instance.VipPrivilegeConfig:GetVipData(MainPlayer.Instance.Vip).exp_buytimes:GetEnumerator()
		for i=1, expGoodsItem.weight do
			enum:MoveNext()
		end
		local buyNum = MainPlayer.Instance.VipExpGoodsBuyInfo:get_Item(expGoodsItem.id)
		p.limitBuyNum = enum.Current - buyNum
		self.packageSell = p
	end
end

function RoleDetail:Buy()
	return function(ps)
		--判断砖石
		local cost = ps.ps_info:GetPrice() * ps.ps_info.cur_num
		local owned_diamond = MainPlayer.Instance.DiamondBuy + MainPlayer.Instance.DiamondFree
		print(self.uiName,"------owned diamond:",owned_diamond,"------cost:",cost)
		if owned_diamond < cost then
			self:ShowBuyTip("BUY_DIAMOND")
			return
		end

		local buyStoreGoods = {
			store_id = "ST_EXP",
			info = {},
		}
		self.toBuyId = ps.storeData.store_good_id
		self.toBuyNum = ps.ps_info.cur_num
		table.insert(buyStoreGoods.info, {pos = ps.storeData.store_goods_weight,num = ps.ps_info.cur_num})

		local msg = protobuf.encode("fogs.proto.msg.BuyStoreGoods", buyStoreGoods)
		LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyResp(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function RoleDetail:BuyResp()
	return function(message)
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID,  self.uiName)

		local resp, err = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
		if resp == nil then
			Debugger.LogError('------BuyStoreGoodsResp error: ', err)
			return
		end
		Debugger.Log('---2---------resp: {0}', resp.store_id)
		if resp.result ~= 0 then
			Debugger.Log('-----------1: {0}', resp.result)
			if self.buyErrCd <= 0 then
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				playSound("UI/UI-wrong")
				self.buyErrCd = 1
			end
			return
		end

		if self.toBuyId and self.toBuyNum then
			local num = MainPlayer.Instance.VipExpGoodsBuyInfo:get_Item(self.toBuyId)
			MainPlayer.Instance:SetVipExpGoodsBuyTimes(self.toBuyId, num + self.toBuyNum)
		end

		self:DataRefresh()
		NGUITools.Destroy(self.packageSell.gameObject)
		self.packageSell = nil
	end
end

function RoleDetail:ClosePackageSell()
	return function()
		self.packageSell = nil
	end
end


function RoleDetail:ResetLinkInfo()
	--print("TopPanelManager reset role link info")
	MainPlayer.Instance.LinkRoleId       = 0
	MainPlayer.Instance.LinkExerciseId   = 0
	MainPlayer.Instance.LinkExerciseLeft = true
	MainPlayer.Instance.LinkTab          = 0
	MainPlayer.Instance.LinkUiName       = ""
	MainPlayer.Instance.LinkEnable       = false
end

function RoleDetail:UpdateArrow()
	if self.preUi then
		local min, max, arrowId
		if self.state ~= st.exerciseEnhance then
			min, max = self.preUi:GetRoleBoundary()
			arrowId = self.id
		else
			min, max = self.exerciseIdList[1], self.exerciseIdList[#self.exerciseIdList]
			arrowId = self.curExerciseItem.id
		end
		self.uiButtonLeft.gameObject:SetActive( arrowId ~= min)
		self.uiButtonRight.gameObject:SetActive( arrowId ~= max)
	else
		self.uiButtonLeft.gameObject:SetActive(false)
		self.uiButtonRight.gameObject:SetActive(false)
	end
end

function RoleDetail:GetMinExp()
	local min = 0
	local storeList = GameSystem.Instance.StoreGoodsConfigData:GetStoreGoodsDataList(enumToInt(StoreType.ST_EXP))
	local enum = storeList:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current.store_good_id
		local useId = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).use_result_id
		local goodUseEnum = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(useId).args:GetEnumerator()
		goodUseEnum:MoveNext()
		local num = goodUseEnum.Current.num_max
		if min == 0 then
			min = num
		elseif min > num then
			min = num
		end
	end
	return min
end

function RoleDetail:UpdateAttrInfo()
	if not self.attrInfoList then
		return
	end

	local attrData = MainPlayer.Instance:GetRoleAttrsByID(self.id).attrs
	local enum = attrData:GetEnumerator()
	while enum:MoveNext() do
		local symbol = enum.Current.Key
		local value = enum.Current.Value

		local attrNameConfig = GameSystem.Instance.AttrNameConfigData
		local attrNameData = attrNameConfig:GetAttrData(symbol)
		local name = attrNameData.name
		local t = self.attrInfoList[name]

		if t then
			t:SetValue(value)
		end
	end
end


function RoleDetail:RefreshCanEnhance()
	-- if not self:IsInSquad() then
	-- 	self.canEnhance = false
	-- 	NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, self.canEnhance)
	-- 	return
	-- end

	-- local roleId = self.id
	-- local curStar = MainPlayer.Instance:GetRole2(roleId).star
	-- local starData = GameSystem.Instance.starAttrConfig:GetStarAttr(roleId,curStar + 1)
	-- if not starData then
	-- 	self.canEnhance = false
	-- 	NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, self.canEnhance)
	-- 	return
	-- end

	-- local  i = 0
	-- local totalNum = 0
	-- local enum = starData.consume:GetEnumerator()
	-- while enum:MoveNext() do
	-- 	totalNum = totalNum + 1
	-- 	local consumeId = enum.Current.Key
	-- 	local consumeValue = enum.Current.Value
	-- 	local ownedNum = MainPlayer.Instance:GetGoodsCount(consumeId)
	-- 	if ownedNum >= consumeValue then
	-- 		i = i + 1
	-- 	end
	-- end
	-- if i == totalNum then
	-- 	self.canEnhance = true
	-- 	NGUITools.SetActive(self.uiEnhanceStarTabRedDot.gameObject, self.canEnhance)
	-- 	return
	-- end
end

function RoleDetail:ShowBuyTip(type)
	local str
	if type == "BUY_GOLD" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
	elseif type == "BUY_DIAMOND" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("DIAMOND"))
	elseif type == "BUY_HP" then
		str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
	end
	self.msg = CommonFunction.ShowPopupMsg(str, nil,
													LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
													LuaHelper.VoidDelegate(self:FramClickClose()),
													getCommonStr("BUTTON_CONFIRM"),
													getCommonStr("BUTTON_CANCEL"))
end

function RoleDetail:ShowBuyUI(type)
	return function()
		self.banTwice = false
		if type == "BUY_DIAMOND" then
			TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
			return
		end
		local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
		go.BuyType = type
	end
end

function RoleDetail:FramClickClose()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.banTwice = false
	end
end

function RoleDetail:OnAnimationResp()
	return function(v)
		self:DataRefresh()
	end
end

return RoleDetail

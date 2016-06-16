------------------------------------------------------------------------
-- class name	: RoleAcquirePopupNew
-- create time   : Tue Sep 15 20:35:56 2015
------------------------------------------------------------------------

RoleAcquirePopupNew = {
	uiName = "RoleAcquirePopupNew",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	-- uiRoleBustItem,
	uiModel,
	uiModelName,
	uiBtnOK,
	uiBtnShow,
	uiParticle,
	uiSpecialEffect,

	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	IsInClude,
	contentStr,
	onCloseClick,
	totalAward,
	delayTime = nil,
	isParticlePlaying = false,
	onBack,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleAcquirePopupNew:Awake()
	self:UiParse() -- Foucs on UI Parse.
end

function RoleAcquirePopupNew:Start()
	addOnClick(self.uiBtnOK.gameObject, self:OnConfirmClick())
	-- addOnClick(self.uiMask.gameObject, self:ClickBack())

	-- local g,t
	-- g = createUI("RoleBustItem1",self.uiRoleBustItem.transform)
	-- t = getLuaComponent(g)
	-- t.onClickSelect = self:ClickBack()
	-- t.id = self.id
	-- t.isResetDisplay = true
	-- self:Refresh()
end

function RoleAcquirePopupNew:Refresh()
	self:SetData()
end

function RoleAcquirePopupNew:OnClose( ... )
	if self.onBack then
		self.onBack()
	end
	if self.uiParticle then
		NGUITools.Destroy(self.uiParticle.gameObject)
		self.uiParticle = nil
	end
	TopPanelManager:HideTopPanel()
	-- NGUITools.Destroy(self.gameObject)
end
-- uncommoent if needed
function RoleAcquirePopupNew:FixedUpdate()
	if self.delayTime ~= nil then
		if self.delayTime > 0 then
			self.delayTime = self.delayTime - UnityTime.fixedDeltaTime
		end
		if self.delayTime <= 0 and self.isParticlePlaying then
			self.uiParticle:Play()
			self.isParticlePlaying = false
		end
	end
end


function RoleAcquirePopupNew:OnDestroy()

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
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleAcquirePopupNew:UiParse()
	self.uiModel = self.transform:FindChild('Model'):GetComponent('ModelShowItem')
	self.uiModelName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiBtnOK = self.transform:FindChild('ButtonOK'):GetComponent('UIButton')
	self.uiBtnShow = self.transform:FindChild('ButtonShow'):GetComponent('UIButton')
	self.uiSpecialEffect = self.transform:FindChild('SpecialEffect')
end

function RoleAcquirePopupNew:SetData(id)
	-- self.id = id
	self.uiModel.Rotation = true
	self.uiModel.ModelID = self.id
	local player = MainPlayer.Instance:GetRole(self.id)
	self.uiModelName.text = player.m_name

	if not self.uiParticle then
		local effectName = GameSystem.Instance.RoleBaseConfigData2:GetPlayerEffectByID(self.uiModel.ModelID)
		if effectName == nil then 
			warning('effectName of role id '..self.id..' does not exist')
			return
		end

		local effectPath = string.format("Prefab/Effect/%s", effectName)
		local effectObj = ResourceLoadManager.Instance:GetResources(effectPath, true)
		if effectObj then
			local effectGameObj = newObject(effectObj)
			effectGameObj.transform.parent = self.uiSpecialEffect.transform
			local particlePos = effectGameObj.transform.localPosition
			local particleScale = effectGameObj.transform.localScale
			particleScale = Vector3.New(1, 1, 1)
			particlePos = Vector3.zero
			effectGameObj.transform.localScale = particleScale
			effectGameObj.transform.localPosition = particlePos
			self.uiParticle = effectGameObj.transform:GetComponent("ParticleSystem")
			self.isParticlePlaying = true
			self.delayTime = 0.5
		else
			error("Load Effect Resources Fail!")
		end
		print('create effect ----------->>>>>>>>>>>>>>>>>>>>.')
	end
end

function RoleAcquirePopupNew:OnConfirmClick( ... )
	return function (go)
		self:OnClose()
	end
end

function RoleAcquirePopupNew:ClickBack()
	return function()
		if self.onClose then
			self.onClose:DynamicInvoke()
		else
			if self.onCloseClick and (self:CheckRoleInclude(self.id) ~= true) then
				self.onCloseClick()
			end

			-- if self.totalAward then
			-- 	local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			-- 	local awardEnum = self.totalAward:GetEnumerator()
			-- 	while awardEnum:MoveNext() do
			-- 		local award = string.split(awardEnum.Current, ':')
			-- 		getGoods:SetGoodsData(tonumber(award[1]), tonumber(award[2]))
			-- 	end
			-- end

			-- judge id is included
			-- if self.IsInClude then
			-- 	print("is inluded")
			-- 	local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
			-- 	if data.recruit_output_id and data.recruit_output_value then
			-- 		local luaCom = getLuaComponent(createUI("GoodsAcquirePopup", UIManager.Instance.m_uiRootBasePanel.transform))
			-- 		luaCom:SetGoodsData(data.recruit_output_id, data.recruit_output_value)
			-- 		luaCom.isRoleRecruit = true
			-- 		luaCom.id = data.recruit_output_id
			-- 		luaCom.num = data.recruit_output_value
			-- 		luaCom.onClose = self.onCloseClick
			-- 		luaCom:SetContent(self.contentStr)
			-- 	end
			-- else
			-- 	-- 如果组成了新图鉴
		 --        if MainPlayer.Instance.NewMapIDList.Count > 0 then
			-- 		local goodsAcquire = getLuaComponent(createUI('GoodsAcquirePopup'))
			-- 		goodsAcquire:SetNewMapData(MainPlayer.Instance.NewMapIDList:get_Item(0))
			-- 		goodsAcquire.onClose = self.onCloseClick
			-- 		if MainPlayer.Instance.NewMapIDList.Count > 0 then
			-- 			goodsAcquire.nextMaps = MainPlayer.Instance.NewMapIDList:get_Item(0)
			-- 		end
			-- 		UIManager.Instance:BringPanelForward(goodsAcquire.gameObject)
		 --        end
			-- end
			NGUITools.Destroy(self.gameObject)
		end
	end
end

return RoleAcquirePopupNew
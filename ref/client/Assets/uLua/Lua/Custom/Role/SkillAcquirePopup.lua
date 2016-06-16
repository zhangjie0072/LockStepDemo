------------------------------------------------------------------------
-- class name    : SkillAcquirePopup
-- create time   : Fri Nov  6 17:32:11 2015
------------------------------------------------------------------------

SkillAcquirePopup =  {
	uiName     = "SkillAcquirePopup",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiSkillIcon , 
	uiName      , 
	uiTip1      , 
	uiTip2      , 
	uiButtonOk  , 
	uiAnimator   , 
	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	onClose, 
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function SkillAcquirePopup:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function SkillAcquirePopup:Start()
	addOnClick(self.uiButtonOk.gameObject, self:ClickOk())
	
	self:Refresh()
end

function SkillAcquirePopup:Refresh()
	
end

-- uncommoent if needed
-- function SkillAcquirePopup:FixedUpdate()

-- end


function SkillAcquirePopup:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.         								 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.   								 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.    				 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function SkillAcquirePopup:UiParse()
	self.uiSkillIcon = self.transform:FindChild("SkillIcon"):GetComponent("Transform")
	self.uiName      = self.transform:FindChild("Name"):GetComponent("UILabel")
	self.uiTip1      = self.transform:FindChild("Tip1"):GetComponent("UILabel")
	self.uiTip2      = self.transform:FindChild("Tip2"):GetComponent("UILabel")
	self.uiButtonOk  = self.transform:FindChild("ButtonOK"):GetComponent("UIButton")
	self.uiAnimator   = self.transform:GetComponent('Animator')
end


function SkillAcquirePopup:SetData(id)
	self.id = id
	
	local icon = getLuaComponent(createUI("GoodsIcon", self.uiSkillIcon))
	icon.goodsID = id
	icon.hideNeed = true

	local config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	local skillConfig = GameSystem.Instance.SkillConfig:GetSkill(id)

	self.uiName.text = config.name
	self.uiTip1.text = config.intro
	self.uiTip2.text = skillConfig.intro
end


function SkillAcquirePopup:ClickOk()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end		
	end
end


function SkillAcquirePopup:OnClose()
	if self.onClose then
		self:onClose()
	end
	NGUITools.Destroy(self.gameObject)
end


return SkillAcquirePopup

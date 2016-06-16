------------------------------------------------------------------------
-- class name    : RoleAdvancePopup
-- create time   : Fri Nov  6 19:52:45 2015
------------------------------------------------------------------------

RoleAdvancePopup =  {
	uiName     = "RoleAdvancePopup",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiMask     , 
	uiIcon     , 
	uiAnimator , 
	
	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	star,
	roleStar, 
}

---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleAdvancePopup:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function RoleAdvancePopup:Start()
	addOnClick(self.uiMask.gameObject, self:Click())
	self:Refresh()
end

function RoleAdvancePopup:Refresh()
	
end

-- uncommoent if needed
-- function RoleAdvancePopup:FixedUpdate()

-- end


function RoleAdvancePopup:OnDestroy()
	
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
function RoleAdvancePopup:UiParse()
	self.uiMask     = self.transform:FindChild("Mask"):GetComponent("UISprite")
	self.uiIcon     = self.transform:FindChild("RoleBustItem"):GetComponent("Transform")
	self.uiAnimator = self.transform:GetComponent("Animator")
end

function RoleAdvancePopup:SetData(id, star)
	self.id = id
	self.star = star

	local r = getLuaComponent(createUI("RoleStarCompare", self.uiIcon))
	r:SetData(id, star)
	
end

function RoleAdvancePopup:Click()
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function RoleAdvancePopup:OnClose()
	NGUITools.Destroy(self.gameObject)
end

return RoleAdvancePopup

------------------------------------------------------------------------
-- class name	: RoleAttrItem1
-- create time   : Mon Sep 14 11:21:23 2015
------------------------------------------------------------------------

RoleAttrItem1 = {
	uiName = "RoleAttrItem1",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiName,
	uiValue, 
	
	-----------------------
	-- Parameters Module --
	-----------------------
	name,
	value,
	onClick, 
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleAttrItem1:Awake()
	self:UiParse() -- Foucs on UI Parse.
end

function RoleAttrItem1:Start()
	addOnClick(self.gameObject,self:Click())
	self:Refresh()
end

function RoleAttrItem1:Refresh()
	if self.name then
		self.uiName.text = self.name
	end
	if self.value then
		self.uiValue.text = tostring(self.value)
	end
end

-- uncommoent if needed
-- function RoleAttrItem1:FixedUpdate()

-- end

function RoleAttrItem1:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.   								 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleAttrItem1:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiName = find("Name"):GetComponent("UILabel")
	self.uiValue = find("Value"):GetComponent("UILabel")

	--self.uiBackground = find("Background"):GetComponent("UISprite")
	
end

function RoleAttrItem1:SetData(name, value)
	self.name = name
	self.value = value
end


function RoleAttrItem1:Click()
	return function()
		if self.onClick  then
			self:onClick(self)
		end
	end
end


return RoleAttrItem1
------------------------------------------------------------------------
-- class name	: RoleAttrItem
-- create time   : Tue Sep 15 15:28:21 2015
------------------------------------------------------------------------

RoleAttrItem =  {
	uiName	 = "RoleAttrItem",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiProgress, 
	uiName,
	uiValue, 
	
	-----------------------
	-- Parameters Module --
	-----------------------
	name,
	cur,
	max, 
	onClick, 
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleAttrItem:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function RoleAttrItem:Start()
	addOnClick(self.gameObject,self:Click())
	
	self:Refresh()
end

function RoleAttrItem:Refresh()
	self.uiName.text = tostring(self.name)
	self.uiValue.text  = tostring(self.cur) .. "/" .. tostring(self.max)
	self.uiProgress.value = self.cur/self.max
end

-- uncommoent if needed
-- function RoleAttrItem:FixedUpdate()

-- end


function RoleAttrItem:OnDestroy()
--	Object.Destroy(self.uiAnimator)
--	Object.Destroy(self.transform)
--	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.		 								 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.   								 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleAttrItem:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiProgress = find("Progress"):GetComponent("UIProgressBar")
	--self.uiProgress_Value = find("Progress/Value"):GetComponent("UISprite")

	self.uiName = find("Name"):GetComponent("UILabel")

	self.uiValue = find("Value"):GetComponent("UILabel")
end


function RoleAttrItem:SetData(name, cur, max)
	self.name = name
	self.cur = cur
	self.max = max
end


function RoleAttrItem:Click()
	return function()
		if self.onClick then
			self:onClick()
		end
	end
end

return RoleAttrItem

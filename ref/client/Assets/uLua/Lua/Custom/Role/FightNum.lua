------------------------------------------------------------------------
-- class name    : FightNum
-- create time   : Mon Jan 11 22:28:14 2016
------------------------------------------------------------------------

FightNum =  {
	uiName     = "FightNum",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiGrid = nil,
	-----------------------
	-- Parameters Module --
	-----------------------
	fightNumSprites = nil,
	fightNum = nil,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function FightNum:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	self.fightNumSprites = {}
	for i = 1, 6 do
		table.insert(self.fightNumSprites, self.uiGrid.transform:FindChild(tostring(i)):GetComponent("UISprite"))
	end
end


function FightNum:Start()
	self:Refresh()
end

function FightNum:Refresh()

end

-- uncommoent if needed
-- function FightNum:FixedUpdate()

-- end


function FightNum:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function FightNum:SetNum(fightNum)
	if self.fightNum == fightNum then
		return
	end
	self.fightNum = fightNum
	local num = {}
	for i = 1, 6 do
		local temp = fightNum / math.pow(10, i-1)
		if temp < 1 then
			break
		end
		local v = math.fmod(math.modf(temp) , 10)
		table.insert(num, 1, v)
	end
	for i = 1, 6 do
		local display = i <= #num
		local fs = self.fightNumSprites[i]
		fs.gameObject:SetActive(display)
		if display then
			fs.spriteName = "com_bg_pure_" .. num[i]
		end
	end
end



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
function FightNum:UiParse()
	self.uiGrid = self.transform:GetComponent("UIGrid")
end

return FightNum

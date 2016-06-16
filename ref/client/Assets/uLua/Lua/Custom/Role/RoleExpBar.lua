------------------------------------------------------------------------
-- class name    : RoleExpBar
-- create time   : Thu Dec  3 22:47:11 2015
------------------------------------------------------------------------

RoleExpBar =  {
	uiName     = "RoleExpBar",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------

	-----------------------
	-- Parameters Module --
	-----------------------
	targetLv       ,
	isExpMoving    ,
	curExpTotal    ,
	curExp         ,
	targetExpTotal ,
	targetExp      ,
	expSpeed       ,
	curLv          ,
	roleDetail     ,
	aniDur         ,
	roleMaxLevel   ,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleExpBar:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	self.roleMaxLevel = GameSystem.Instance.CommonConfig:GetUInt("gPlayerMaxLevel")
end


function RoleExpBar:Start()
	self:Refresh()
end

function RoleExpBar:Refresh()

end

function RoleExpBar:FixedUpdate()
	local inter = UnityTime.fixedDeltaTime

	if self.isExpMoving then
		local roleLvConfig = GameSystem.Instance.RoleLevelConfigData
		local delta = self.expSpeed * inter
		self.curExp = math.modf(self.curExp + delta)
		self.curExpTotal = math.modf(self.curExpTotal + delta)
		self.curExpTotal = math.min(self.curExpTotal, self.targetExpTotal)

		local curLv = self:GetLvByExp(self.curExpTotal)
		if curLv ~= self.curLv then
			self.roleDetail:LvUp()
			for i = self.curLv, curLv - 1 do
				local lvMaxExp = roleLvConfig:GetMaxExp(i)
				local curExpOff =  self.curExp - lvMaxExp
				local targetExpOff = self.targetExp - lvMaxExp
				self.curExp = curExpOff
				self.targetExp = targetExpOff
			end
			self.curLv = curLv
		end
		if self.curExp >= self.targetExp then
			self.isExpMoving = false
			self.curExp = self.targetExp
		end
		self.curExp = math.max(self.curExp, 0)
		if self.targetLv then
			self.curLv = self.curLv <= self.targetLv and self.curLv or self.targetLv
		end

		local nextExp = roleLvConfig:GetMaxExp(self.curLv)
		self.uiLv.text = "Lv."..self.curLv

		if self.curLv >= self.roleMaxLevel then
			self.uiExpData.text = "MAX"
			self.uiProcess.value = 1.0
		else
			self.uiExpData.text = tostring(self.curExp).."/".. tostring(nextExp)
			self.uiProcess.value = self.curExp / nextExp
		end
	end
end

function RoleExpBar:OnDestroy()

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
function RoleExpBar:UiParse()
	self.uiLv = self.transform:FindChild("LabelTitle"):GetComponent("UILabel")
	self.uiProcess = self.transform:FindChild("Back"):GetComponent("UIProgressBar")
	--self.uiFore = self.transform:FindChild("Fore"):GetComponent("UISprite")
	--self.uiTip = self.transform:FindChild("Tip"):GetComponent("UISprite")
	self.uiExpData = self.transform:FindChild("Data"):GetComponent("UILabel")
end



function RoleExpBar:StartExpAni(prevLv, prevExp, targetLv, targetExp, dur)
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

function RoleExpBar:GetLvByExp(exp)
	local i = 1
	while true do
		exp = exp - GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		if exp <= 0 then
			return i
		end
		i = i + 1
	end
end

function RoleExpBar:SetData(roleDetail)
	self.roleDetail = roleDetail
end

return RoleExpBar

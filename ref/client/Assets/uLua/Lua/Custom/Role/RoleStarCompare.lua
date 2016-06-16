------------------------------------------------------------------------
-- class name    : RoleStarCompare
-- create time   : Wed Oct 28 22:00:30 2015
------------------------------------------------------------------------

RoleStarCompare =  {
	uiName     = "RoleStarCompare",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------

	-----------------------
	-- Parameters Module --
	-----------------------
	roleId,						-- role id
	star,						-- role star.
	isNext = false,
	skillValue,
	upSkillV = true,
	datas,
	leftRole,
	careerRoleIcon,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleStarCompare:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function RoleStarCompare:Start()
	local careerIcon = getLuaComponent(createUI("CareerRoleIcon", self.uiCareerRoleIcon.transform))
	careerIcon.id = self.roleId
	careerIcon.showPosition = false
	self.careerRoleIcon = careerIcon
	careerIcon:Refresh()

	self:Refresh()
end

function RoleStarCompare:Refresh()
	local attrs = GameSystem.Instance.starAttrConfig:GetStarAttr(self.roleId, self.star == 0 and 1 or self.star).attrs
	local roleAttrs = MainPlayer.Instance:GetRoleAttrsByID(self.roleId).attrs


	if self.isNext and self.upSkillV then
		self.skillValue = {}
		local findids = {1, 2, 3, 4, 5, 6, 7, 17, 20}
		for k, v in pairs(findids) do
			self.skillValue[v] = self:GetAttrOffValue(v)
		end
		self.upSkillV = false
	end


	print("roleAttrs=",roleAttrs, "self.roleId=", self.roleId)
	local enum = roleAttrs:GetEnumerator()
	local twoPoint = 0
	local twoPointCount = 0
	local datas = { 0, 0, 0	}

	while enum:MoveNext() do
		local symbol = enum.Current.Key
		local nValue = enum.Current.Value
		local id = GameSystem.Instance.AttrNameConfigData:GetAttrData(symbol).id

		if id >= 1 and id <= 7 and id ~= 3 then
			twoPointCount = twoPointCount + 1
			twoPoint = twoPoint + nValue
			if self.isNext then
				local offValue = self.skillValue[id]
				twoPoint = twoPoint + offValue
			end
		elseif id == 3 then
			datas[2]= nValue
			if self.isNext then
				local offValue = self.skillValue[id]
				datas[2] = datas[2] + offValue
			end
		-- elseif id == 17 then
		--	datas[3] = nValue
		--	if self.isNext then
		--		local offValue = self.skillValue[id]
		--		datas[3] = datas[3] + offValue
		--	end
		elseif id == 20 then
			datas[3] = nValue
			if self.isNext then
				local offValue = self.skillValue[id]
				datas[3] = datas[3] + offValue
			end
		end
	end

	local strs = {
		self.twoPointStr, self.threePointStr, self.staminaStr
	}

	datas[1] = math.modf(twoPoint / twoPointCount)

	CommonFunction.ClearGridChild(self.uiAttrInfoGrid.transform)

	self.datas = {}
	for i = 1, 3 do
		local t = getLuaComponent(createUI("AttrInfo", self.uiAttrInfoGrid.transform))
		t:SetName(getCommonStr(strs[i]))
		local same = true
		if self.isNext and self.leftRole then
			same =self.leftRole.datas[i] == datas[i]
		end
		t:SetValue(datas[i], not same)
		table.insert(self.datas, datas[i])
	end

	self.uiAttrInfoGrid:Reposition()

	if self.star >= 10 then
		self.uiV2.gameObject:SetActive(true)
		self.uiV1.spriteName = self.numTextPre .. tostring(math.modf(self.star/10))
		self.uiV2.spriteName = self.numTextPre .. tostring(self.star%10)
	else
		self.uiV1.spriteName = self.numTextPre .. tostring(self.star)
		self.uiV2.gameObject:SetActive(false)
	end
	self.careerRoleIcon.displayStar = self.star
	self.careerRoleIcon:Refresh()
end

-- uncommoent if needed
-- function RoleStarCompare:FixedUpdate()

-- end


function RoleStarCompare:OnDestroy()

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
function RoleStarCompare:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiCareerRoleIcon = find("Icon/CareerRoleIcon"):GetComponent("Transform")
	self.uiAttrInfoGrid = find("Grid"):GetComponent("UIGrid")
	self.uiV1 = find("Name/V1"):GetComponent("UISprite")
	self.uiV2 = find("Name/V2"):GetComponent("UISprite")
end


function RoleStarCompare:SetData(roleId, star, leftRole)
	self.roleId = roleId
	if self.star and self.star ~= star then
		self.upSkillV = true
	end
	self.leftRole = leftRole
	self.star = star
	if self.started then
		self:Refresh()
	end
end

function RoleStarCompare:GetAttrOffValue(skillId)
	if self.star == 0 then
		return 0
	end

	local preValue = 0
	if self.star > 1 then
		local starAttrs = GameSystem.Instance.starAttrConfig:GetStarAttr(self.roleId, self.star-1)
		local value = starAttrs:GetAttrValue(skillId)
		preValue = preValue + value
	end

	local starAttrs = GameSystem.Instance.starAttrConfig:GetStarAttr(self.roleId, self.star)
	local value = starAttrs:GetAttrValue(skillId)
	return value - preValue
end


return RoleStarCompare

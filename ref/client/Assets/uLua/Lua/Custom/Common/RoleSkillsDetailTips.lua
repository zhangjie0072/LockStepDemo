------------------------------------------------------------------------
-- class name    : RoleSkillsDetailTips
-- create time   : 2016-05-17 11:14:28
-- author        : Jackwu
-- description	 : 玩员角色身上佩带的技能详情
------------------------------------------------------------------------
RoleSkillsDetailTips = {
	uiName = "RoleSkillsDetailTips",
	
--=============================--
--==	 公有变量分隔线	 	 ==--
--=============================--

--=============================--
--==	 私有变量分隔线	 	 ==--
--=============================--
	_roleId = nil,--球员的id
--=============================--
--==	 	UI变量 	 		 ==--
--=============================--
	_roleIconNode = nil,--球员头像加载node
	_sprCareerIcon = nil,--球员职业图标
	_lblCareerDes = nil,--球员职业文字描述
	_lblRoleName = nil,--球员名称
	_lblAbilityDes = nil,--球员能力描述
	_gridSkill = nil,--技能grid
	_skillNameGroup = {},--技能名字

}

function RoleSkillsDetailTips:Awake( ... )
	self:UIParser()
	self:AddEvent()
end

function RoleSkillsDetailTips:Start( ... )
	
end

function RoleSkillsDetailTips:Update( ... )
	
end

function RoleSkillsDetailTips:FixedUpdate( ... )
	
end

function RoleSkillsDetailTips:OnDestroy( ... )
	
end

--------------刷新------------------
function RoleSkillsDetailTips:Refresh(subID)

end

--------------解析UI组件------------
function RoleSkillsDetailTips:UIParser( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

 	self._roleIconNode = find("Icon")
 	self._sprCareerIcon = find("CareerIcon"):GetComponent("UISprite")
 	self._lblCareerDes = find("Label1"):GetComponent("UILabel")
 	self._lblAbilityDes = find("Label2"):GetComponent("UILabel")
 	self._lblRoleName = find("Label3"):GetComponent("UILabel")
 	self._gridSkill = find("SkillGrid"):GetComponent("UIGrid")

 	self._skillNameGroup = {}
 	for i=1,4 do
 		table.insert(self._skillNameGroup,find("Name"..i):GetComponent("UILabel"))
 		self._skillNameGroup[i].text = ""
 	end
end

--------------侦听事件--------------
function RoleSkillsDetailTips:AddEvent( ... )
	--addOnClick(self.uiCloseBtn.gameObject,self:OnClickHanlder())--
	addOnClick(self.gameObject,self:OnClick())
end

--=============================--
--==	 公有方法分隔线	 	 ==--
--=============================--

-------------------------------------------
-- 设置角色id
-- roleId : 角色id
-------------------------------------------
function RoleSkillsDetailTips:SetRoleId(roleId)
	self._roleId = roleId
	self:RefreshView()
end

-------------------------------------------
-- 获取球员的id
-- roleId : 角色id
-------------------------------------------
function RoleSkillsDetailTips:GetRoleId()
	return self._roleId
end

--=============================--
--==	 私有方法分隔线	 	 ==--
--=============================--

-------------------------------------------
-- 刷新信息
-------------------------------------------
function RoleSkillsDetailTips:RefreshView( ... )
	if self._roleId == nil then
		return 
	end
	local positions ={'PF','SF','C','PG','SG'}
	local roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self._roleId)
	self._sprCareerIcon.spriteName = tostring(PositionType.IntToEnum(roleData.position))
	self._lblCareerDes.text = getCommonStr(positions[roleData.position])
	self._lblRoleName.text = roleData.name
	local specialityInfoStr = roleData.specialityInfo
    if specialityInfoStr == nil then
        specialityInfoStr = ""
    end
	self._lblAbilityDes.text = specialityInfoStr

	CommonFunction.ClearChild(self._roleIconNode.transform)
	local roleIconT= getLuaComponent(createUI("CareerRoleIcon", self._roleIconNode.transform))
	roleIconT:SetClickEnable(false)
	roleIconT.id = self._roleId

	-- local roleInfo = MainPlayer.Instance:GetRole2(self._roleId)
 --    if roleInfo then
 --        local skills = roleInfo.skill_slot_info
 --        self:DisplaySkills(skills)
 --    else
 --    	self:DisplaySkills(nil)
 --    end
end

-------------------------------------------
-- 显示技能
-------------------------------------------
function RoleSkillsDetailTips:DisplaySkills(skills)
    CommonFunction.ClearChild(self._gridSkill.transform)
    local len = #self._skillNameGroup
    for i=1,len do
    	self._skillNameGroup[i].text = ""
    end
    -- if skills then
    --     local count = #skills
    --     for i = 0, count - 1 do
    --         local skill = skills:get_Item(i)
    --         if skill.skill_uuid ~= 0 then
    --             local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, skill.skill_uuid)
    --             local goodsIcon = getLuaComponent(createUI("GoodsIcon", self._gridSkill.transform))
    --             goodsIcon.goods = goods
    --             goodsIcon.hideLevel = true
    --             goodsIcon.hideNeed = true
    --             goodsIcon.showTips = false
    --             local goodsAttr =GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goods:GetID())
    --             if goodsAttr then
    --            		self._skillNameGroup[i].text = goodsAttr.name
    --            	end
    --         end
    --     end
    -- end
    print("打印角色信息………………………………………………………………………………………………………………………………")
    if skills then
    	local count = #skills
    	print("打印角色信息长度………………………………………………………………………………………………………………………………"..count)
    	for i=1,count do
    		local skill = skills[i]
    		print("Skill信息 ：skill_uuid:"..skill.skill_uuid.."..skill_id:"..skill.skill_id)
    		if skill.skill_uuid ~= 0 then
                local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(skill.skill_id)
                local goodsIcon = getLuaComponent(createUI("GoodsIcon", self._gridSkill.transform))
                goodsIcon.goodsID = skill.skill_id
				goodsIcon.level = skill.skill_level
                goodsIcon.hideNeed = true
                goodsIcon.showTips = false
                if skillAttr then
               		self._skillNameGroup[i].text = skillAttr.name
               	end
            end	
    	end
    end
    self._gridSkill.repositionNow = true

end

function RoleSkillsDetailTips:OnClick()
    return function ()
        NGUITools.Destroy(self.gameObject)
    end
end

return RoleSkillsDetailTips

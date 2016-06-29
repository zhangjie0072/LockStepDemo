NewRoleDetail =  {
	uiName	= "NewRoleDetail",
    nextShowUI = "UIRole",
    positions = {'PF','SF','C','PG','SG'},
	posMap = {0,3,1,2,5,4},

    --data
	id,
	pos,
    roleData,
    roleIDList,
    roleMyIDList,
    backCallBackFunc = nil,

    --ui
    uiAttGrid,
    uiAttTable,
    uiBackBtn,
    uiPostion,
    uiPositionName,
    uiRoleName,
    uiRoleDesc,
    uiRoleModel,

    uiTalentIcon,
    uiTalentName,
    uiTalentDesc,

    uiSkillGrid,
    uiBtnChangeSkill,

    uiSkillManager,

    uiBtnLeft,
    uiBtnRight,

    uiSkillRoot,

    uiBuyRoot,
	uiBuyGoldRoot,
    uiBuyGold,
	uiBuyDiamondRoot,
    uiBuyDiamond,
	uiGetDesc,
    uiBuyButton,

	uiBuyWindow,
}

function NewRoleDetail:Awake()
    self.uiBackBtn = getChildGameObject(self.transform, "TopLeft/ButtonBack")
    self.uiAttGrid = getComponentInChild(self.transform, "Right/Right/Grid", "UIGrid")

    self.uiPostion = getComponentInChild(self.transform, "Left/Left/Icon", "UISprite")
    self.uiPositionName = getComponentInChild(self.transform, "Left/Left/Icon/PositionName", "UILabel")
    self.uiRoleName = getComponentInChild(self.transform, "Left/Left/Icon/Name", "UILabel")
    self.uiRoleDesc = getComponentInChild(self.transform, "Right/Right/Describe", "UILabel")
    self.uiRoleModel = getComponentInChild(self.transform, "Left/Left/Model", "ModelShowItem")

    self.uiTalentIcon = getComponentInChild(self.transform, "Right/Right/Talent/Icon/Sprite", "UISprite")
    self.uiTalentName = getComponentInChild(self.transform, "Right/Right/Talent/Name", "UILabel")
    self.uiTalentDesc = getComponentInChild(self.transform, "Right/Right/Talent/Describe", "UILabel")

    self.uiSkillGrid = getComponentInChild(self.transform, "Right/Right/Skill/Grid", "UIGrid")
    self.uiBtnChangeSkill = getChildGameObject(self.transform, "Right/Right/Skill/BtnChangeSkill")

    self.uiBtnLeft = getChildGameObject(self.transform, "Left/Left/Left")
    self.uiBtnRight = getChildGameObject(self.transform, "Left/Left/Right")

    self.uiSkillRoot = getChildGameObject(self.transform, "Right/Right/Skill")
    self.uiBuyRoot = getChildGameObject(self.transform, "Right/Right/Buy")

	self.uiBuyGoldRoot = getChildGameObject(self.transform, "Right/Right/Buy/BgGold")
	self.uiBuyDiamondRoot = getChildGameObject(self.transform, "Right/Right/Buy/BgDiamond")

    local buyGold = getChildGameObject(self.transform, "Right/Right/Buy/BgGold/GameObject")
    local buyDiamond = getChildGameObject(self.transform, "Right/Right/Buy/BgDiamond/GameObject")
    local gold = createUI("GoodsIconConsume", buyGold.transform)
    self.uiBuyGold = getLuaComponent(gold)
	self.uiBuyGold.isAdd = false
    local diamond = createUI("GoodsIconConsume", buyDiamond.transform)
    self.uiBuyDiamond = getLuaComponent(diamond)
	self.uiBuyDiamond.isAdd = false
	self.uiGetDesc = getComponentInChild(self.transform, "Right/Right/Buy/GetDesc", "UILabel")

    self.uiBuyButton = getChildGameObject(self.transform, "Right/Right/Buy/ButtonOK1")

    --八项属性
    self.uiAttTable = {}
    for i=1, 8 do
        local item = createUI("RoleAttrItem", self.uiAttGrid.transform)
        table.insert(self.uiAttTable, item)
    end
end

function NewRoleDetail:OnEnable()
    self.uiAttGrid:Reposition()
end

function NewRoleDetail:Start()
    --返回按钮
	local g = createUI("ButtonBack", self.uiBackBtn.transform)
	local t = getLuaComponent(g)
	t.onClick = self:ClickBack()

    self.uiAttGrid:Reposition()

    --变更技能
    addOnClick(self.uiBtnChangeSkill, self:OnChangeSkill())

    --左右切换球员
    addOnClick(self.uiBtnLeft, self:OnChangeRoleLeft())
    addOnClick(self.uiBtnRight, self:OnChangeRoleRight())

    --购买
    addOnClick(self.uiBuyButton, self:OnBuy())

	self:RefreshData()
end

function NewRoleDetail:FillRoleList()
	--self.roleIDList = {}
    self.roleMyIDList = {}
	local myRoleList = MainPlayer.Instance:GetRoleIDList()

    --玩家拥有的球员先放入列表
	local enum = myRoleList:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current
		local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
        table.insert(self.roleMyIDList, data.id)
	end
end


function NewRoleDetail:OnBuy()
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.players_btn) then return end

        if not self.uiBuyWindow then
			self.uiBuyWindow = createUI("NewRoleBuy")
			local s = getLuaComponent(self.uiBuyWindow)
			s.id = self.roleData.id
			s.roleData = self.roleData
			s.onCloseClick = function ( ... )
				self:RefreshData()
        			self.uiBuyWindow = nil
			end
			s.onBuyNewPlayer = self:OnBuyClosed()
		end
	end
end

function NewRoleDetail:OnBuyClosed()
    return function (go)
        self.uiBuyWindow = nil
        self:ShowNewRole()
	end
end

function NewRoleDetail:ShowNewRole()
	local roleAcquireLua = TopPanelManager:ShowPanel("RoleAcquirePopupNew", nil, {id = self.roleData.id})
	roleAcquireLua.onBack = function ( ... )
		self:RefreshData()
	end
end

function NewRoleDetail:OnChangeRoleLeft()
    return function (go)
        self:GetLeftRight(true)
	end
end

function NewRoleDetail:OnChangeRoleRight()
    return function (go)
        self:GetLeftRight(false)
	end
end

function NewRoleDetail:GetLeftRight(isLeft)
    local left_id = self.roleIDList[1]
    local right_id = left_id

    local max = table.getn(self.roleIDList)
    for i=1, max do
        local id = self.roleIDList[i]
        if id == self.roleData.id then
            if i <= 1 then --最左边(--球员详细界面，点击左右箭头切换球员，选到最左或最右的球员时，箭头还在点击没有反应。修改为循环展示球员)
                left_id = self.roleIDList[max]
                if max >= i+1 then
                    right_id = self.roleIDList[i+1]
                end
            elseif i >= max then --最右边了(--球员详细界面，点击左右箭头切换球员，选到最左或最右的球员时，箭头还在点击没有反应。修改为循环展示球员)
                right_id = self.roleIDList[1]
                if i >= 2 then
                    left_id = self.roleIDList[i-1]
                end
            else
                left_id = self.roleIDList[i-1]
                right_id = self.roleIDList[i+1]
            end

            break
        end
    end

    if isLeft then
        if left_id ~= self.roleData.id then
            self.id = left_id
			self:RefreshData()
        end
    else
        if right_id ~= self.roleData.id then
            self.id = right_id
			self:RefreshData()
        end
    end

end

function NewRoleDetail:OnChangeSkill()
	return function (go)
		    self.uiSkillManager = TopPanelManager:ShowPanel("UISkillManager",
									nil, { defaultRoleID = self.roleData.id })

			self.uiSkillManager.defaultRoleID = self.roleData.id
			if self.uiSkillManager.mainPlayer then
				self.uiSkillManager:ShowWearSkills(self.roleData.id)
			end

			self.uiSkillManager.onCloseEvent = self:OnSkillManagerClose()

			print( "open UISkillManager=>"..self.roleData.id )
	end
end

function NewRoleDetail:OnSkillManagerClose()
	return function(go)
		print("----------------self.uiSkillManager"..tostring(self.uiSkillManager))
		if self.uiSkillManager then
			self.uiSkillManager.onCloseEvent = nil

			self.id = self.roleData.id
			self:RefreshData()
			print( "close UISkillManager=>"..self.roleData.id )
		end
	end
end

function NewRoleDetail:OnClose()
	if self.onClickClose then
		self:onClickClose()
	end
	if self.backCallBackFunc ~= nil then
		self.backCallBackFunc(false)
	end
	TopPanelManager:HideTopPanel()
end

function NewRoleDetail:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function NewRoleDetail:ClickBack()
	return function()
		self:DoClose()
	end
end

--左右切换球员按钮功能开关
function NewRoleDetail:SetLeftRightBtnsVisible(bool)
	NGUITools.SetActive(self.uiBtnLeft,bool)
	NGUITools.SetActive(self.uiBtnRight,bool)
end

function NewRoleDetail:RefreshData()
    if self.id == nil then
        error("role id is not setting")
        return
    end

	print("NewRoleDetail:Refresh=>"..self.id)

	self.roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	self:FillRoleList()

    local isMy = self:IsMyRole(self.roleData.id)
    print("IsMyRole==>>"..tostring(isMy))
    NGUITools.SetActive(self.uiSkillRoot, isMy)
    NGUITools.SetActive(self.uiBuyRoot, not isMy)

    local config = self.roleData
    self.uiPostion.spriteName = "PT_"..self.positions[config.position]
    self.uiPositionName.text = getCommonStr(self.positions[config.position])
    self.uiRoleName.text = config.name
    self.uiRoleDesc.text = config.intro

    if isMy then
        self:RefreshSkill()
    else
		NGUITools.SetActive(self.uiGetDesc.gameObject, false)
		NGUITools.SetActive(self.uiBuyGoldRoot, false)
		NGUITools.SetActive(self.uiBuyDiamondRoot, false)
		NGUITools.SetActive(self.uiBuyButton.gameObject, false)

		--购买开销
		local consume = self.roleData.recruit_consume
		local cur_enum = consume:GetEnumerator()
		local index = 0
		while cur_enum:MoveNext() do
			local key = cur_enum.Current.Key
			local value = cur_enum.Current.Value

			index = index + 1
			if index == 1 then
				self.uiBuyGold:SetData(key, value)
				NGUITools.SetActive(self.uiBuyGoldRoot, true)
			elseif index == 2 then
				self.uiBuyDiamond:SetData(key, value)
				NGUITools.SetActive(self.uiBuyDiamondRoot, true)
			end
		end

		if index == 0 then
			print(self.roleData.recruit_consume_string)
			self.uiGetDesc.text = self.roleData.recruit_consume_string
			NGUITools.SetActive(self.uiGetDesc.gameObject, true)
		else
			NGUITools.SetActive(self.uiBuyButton.gameObject, true)
		end

    end

    --3D模型展示
	self.uiRoleModel.Rotation = true
    self.uiRoleModel.ModelID = self.roleData.id

    self:RefreshAtt()
    self:RefreshTalent()

end

function NewRoleDetail:RefreshSkill()
    CommonFunction.ClearGridChild(self.uiSkillGrid.transform)
	self.uiSkillGrid.repositionNow = true

    local roleInfo = MainPlayer.Instance:GetRole2(self.roleData.id)
	if roleInfo then
		local roleSkillInfo = roleInfo.skill_slot_info

        local pos = 1
        local enum = roleSkillInfo:GetEnumerator()
		while enum:MoveNext() do
            if pos <= 4 then
			    local skillUUID = enum.Current.skill_uuid
			    if skillUUID ~= 0 then
				    local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, skillUUID)
				    local goodsIcon = createUI('GoodsIcon', self.uiSkillGrid.transform)
				    local goodsIconLua = getLuaComponent(goodsIcon.gameObject)
				    print('goodsID = ', goods:GetID())
				    goodsIconLua.goods = goods
				    goodsIconLua.hideNeed = true
				    goodsIconLua.hideNum = false
				    pos = pos + 1
			    end
            end
		end

        self.uiSkillGrid.repositionNow = true
	else
		error('do not have this player!')
	end
end

function NewRoleDetail:RefreshTalent()
    local config = GameSystem.Instance.talentConfig:GetConfigData(self.roleData.special_attr)
    if config == nil then
        print("talent data is nil ==>"..self.roleData.special_attr)
        do return end
    end

    self.uiTalentName.text = config.name
    self.uiTalentDesc.text = config.desc

	local str = string.split(config.icon, '&')
	local max = table.getn(str)
	if max == 2 then
		self.uiTalentIcon.atlas = ResourceLoadManager.Instance:GetAtlas(str[1]);
		self.uiTalentIcon.spriteName = str[2]
	end
end

function NewRoleDetail:RefreshAtt()
    local roleId = self.roleData.id
    local attrConfig = GameSystem.Instance.RoleBaseConfigData2
	local attrNameConfig = GameSystem.Instance.AttrNameConfigData
	--libing 策划要求talnet不参与运算
	--local talent = attrConfig:GetTalent(roleId)
	local talent = 1
	local function getAttr(name)
		return attrConfig:GetAttrValue(roleId, attrNameConfig:GetAttrData(name).id)
	end

    local max = 800
    local d1 = 0
    local data = 0

    --近投
	d1 = 0
	data = getAttr("shoot_near")
	d1 = data
	data = getAttr("layup_near")
	d1 = d1 + data
	d1 = d1 * talent / max / 2
    local s1 = getLuaComponent(self.uiAttTable[1])
	s1:SetData(getCommonStr("ACTION_TYPE_25"), d1, 1)
    s1:Refresh()

    --中投
	d1 = 0
	data = getAttr("shoot_middle")
	d1 = data
	data = getAttr("layup_middle")
	d1 = d1 + data
	d1 = d1 * talent / max / 2
    local s1 = getLuaComponent(self.uiAttTable[2])
	s1:SetData(getCommonStr("ACTION_TYPE_26"), d1, 1)
    s1:Refresh()

    --远投
	data = getAttr("shoot_far")
	data = data * talent / max
    local s2 = getLuaComponent(self.uiAttTable[3])
    s2:SetData(getCommonStr("ACTION_TYPE_27"), data, 1)
    s2:Refresh()

    --扣篮
	d1 = 0
	data = getAttr("dunk_near")
	d1 = data
	data = getAttr("dunk_middle")
	d1 = d1 + data
	d1 = d1 * talent / max / 2
    local s1 = getLuaComponent(self.uiAttTable[4])
	s1:SetData(getCommonStr("ACTION_TYPE_3"), d1, 1)
    s1:Refresh()

    --篮板 属性最大值为400而不是max变量生命中的800，所以除以2
	data = getAttr("rebound")
	data = data * talent * 2 / max
    local s5 = getLuaComponent(self.uiAttTable[5])
	s5:SetData(getCommonStr("ACTION_TYPE_5"), data, 1)
    s5:Refresh()

    --盖帽 属性最大值为400而不是max变量生命中的800，所以除以2
	data = getAttr("block")
	data = data * talent * 2 / max
    local s5 = getLuaComponent(self.uiAttTable[6])
	s5:SetData(getCommonStr("ACTION_TYPE_4"), data, 1)
    s5:Refresh()

    --传球 属性最大值为400而不是max变量生命中的800，所以除以2
	data = getAttr("pass")
	data = data * talent * 2 / max
    local s5 = getLuaComponent(self.uiAttTable[7])
	s5:SetData(getCommonStr("ACTION_TYPE_7"), data, 1)
    s5:Refresh()

    --控球 属性最大值为400而不是max变量生命中的800，所以除以2
	data = getAttr("control")
	data = data * talent * 2 / max
    local s6 = getLuaComponent(self.uiAttTable[8])
	s6:SetData(getCommonStr("STR_CONTROL"), data, 1)
    s6:Refresh()
end

function NewRoleDetail:IsMyRole(id)
    if self.roleMyIDList == nil then
        return false
    end

    for i=1, #self.roleMyIDList do
        if id == self.roleMyIDList[i] then
            return true
        end
    end

    return false
end

function NewRoleDetail:Update()

end

function NewRoleDetail:FixedUpdate()

end

function NewRoleDetail:OnDestroy()

end

return NewRoleDetail
-- 20150626_151648




CaptainUpgrade =  {
	uiName = 'CaptainUpgrade',
	go = {},
}




function CaptainUpgrade:Awake()
	self.go = {}
	
	self.go.ok_btn = self.transform:FindChild('OKBtn').gameObject

	addOnClick(self.go.ok_btn,self:click_ok())

	self.go.left_grid = self.transform:FindChild('Left/Grid'):GetComponent("UIGrid")
	self.go.right_grid = self.transform:FindChild('Right/Scroll/Grid'):GetComponent("UIGrid")

	self.model_show_item = self.transform:FindChild('ModelShowItem'):GetComponent("ModelShowItem")
	self.go.title = self.transform:FindChild("title"):GetComponent("UILabel")
end


function CaptainUpgrade:click_ok()
	return function()
		ModelShowItem.ResumeHidden()
		NGUITools.Destroy(self.gameObject)
		--TopPanelManager:HideTopPanel()
	end
end



function CaptainUpgrade:Refresh()
	self.model_show_item.ModelID = MainPlayer.Instance.CaptainID
	
	self.pre_level = MainPlayer.Instance.prev_level
	self.cur_level = MainPlayer.Instance.Level

	self.go.title.text = string.format(getCommonStr("STR_CAPTAIN_UPGRADE_TO"),tostring(MainPlayer.Instance.Level))

	local cid = MainPlayer.Instance.CaptainID
	local cbias = enumToInt(MainPlayer.Instance:GetCaptainBias(cid))

	print('self.pre_level='..tostring(self.pre_level))
	print('self.cur_level='..tostring(self.cur_level))

	local pre_attr = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(cid,cbias,self.pre_level)
	local cur_attr = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(cid,cbias,self.cur_level)
	
	local cur_enum = cur_attr.attrs:GetEnumerator()
	
	local item_list = {}
	CommonFunction.ClearGridChild(self.go.right_grid.transform)
	while cur_enum:MoveNext() do
		local key = cur_enum.Current.Key
		local value = cur_enum.Current.Value
		
		local name = GameSystem.Instance.AttrNameConfigData.AttrName[key]
		
		local pre_value = pre_attr.attrs[key]
		local cur_value = cur_attr.attrs[key]
		
		if pre_value~= cur_value then
			table.insert(item_list,{["name"]=name,["pre_value"]=pre_value,["cur_value"]=cur_value})
		end
	end

	for k,v in pairs(item_list) do
	print('k='..tostring(k))
	print("v.name="..tostring(v.name))
	print("v.pre_value="..tostring(v.pre_value))
	print("v.cur_value="..tostring(v.cur_value))
	local g = createUI("AttrUpgradeListItem",self.go.right_grid.transform)
	local script = getLuaComponent(g)
	script.attrName = v.name
	script.prevValue = v.pre_value
	script.curValue = v.cur_value
	script.showPlus = false
	end

	-- left grid
	CommonFunction.ClearGridChild(self.go.left_grid.transform)

	local pre_max_hp = GameSystem.Instance.TeamLevelConfigData:GetMaxHP(self.pre_level)
	local cur_max_hp = GameSystem.Instance.TeamLevelConfigData:GetMaxHP(self.cur_level)
	pre_max_hp = MainPlayer.Instance.prev_hp
	cur_max_hp = MainPlayer.Instance.Hp


	local g = createUI("AttrUpgradeListItem",self.go.left_grid.transform)
	local script = getLuaComponent(g)
	script.attrName = getCommonStr("CUR_HP")
	script.prevValue = pre_max_hp
	script.curValue = cur_max_hp
	script.showPlus = false

	local pre_max_role_quality = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(self.pre_level)
	local cur_max_role_quality = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(self.cur_level)

	g = createUI("AttrUpgradeListItem",self.go.left_grid.transform)
	script = getLuaComponent(g)
	script.attrName = getCommonStr("STR_MEMBER_QUALITY_LIMIT")
	local pre_quality = enumToInt(pre_max_role_quality)
	local cur_quality = enumToInt(cur_max_role_quality)

	script.prevValue = getQualitystr(pre_quality)
	script.curValue = getQualitystr(cur_quality)
	script.showPlus = false


	local pre_max_tattoo = GameSystem.Instance.TeamLevelConfigData:GetMaxTattoo(self.pre_level)
	local cur_max_tattoo = GameSystem.Instance.TeamLevelConfigData:GetMaxTattoo(self.cur_level)

	g = createUI("AttrUpgradeListItem",self.go.left_grid.transform)
	script = getLuaComponent(g)
	script.attrName = getCommonStr("STR_TATTOO_LIMIT")
	script.prevValue = tostring(pre_max_tattoo)
	script.curValue = tostring(cur_max_tattoo)
	script.showPlus = false


	local pre_max_train = GameSystem.Instance.TeamLevelConfigData:GetMaxTrain(self.pre_level)
	local cur_max_train = GameSystem.Instance.TeamLevelConfigData:GetMaxTrain(self.cur_level)

	g = createUI("AttrUpgradeListItem",self.go.left_grid.transform)
	script = getLuaComponent(g)
	script.attrName = getCommonStr("STR_TRAIN_LIMIT")
	script.prevValue = tostring(pre_max_train)
	script.curValue = tostring(cur_max_train)
	script.showPlus = false


	local pre_max_ps = GameSystem.Instance.TeamLevelConfigData:GetMaxPassiveSkill(self.pre_level)
	local cur_max_ps = GameSystem.Instance.TeamLevelConfigData:GetMaxPassiveSkill(self.cur_level)

	g = createUI("AttrUpgradeListItem",self.go.left_grid.transform)
	script = getLuaComponent(g)
	script.attrName = getCommonStr("STR_PASSIVE_SKILL_LIMIT")
	script.prevValue = tostring(pre_max_ps)
	script.curValue = tostring(cur_max_ps)
	script.showPlus = false




	self.go.left_grid:Reposition()
	self.go.right_grid:Reposition()
end

function CaptainUpgrade:Start()
	ModelShowItem.HideExcept(self.model_show_item)
	self:Refresh()
end


return CaptainUpgrade

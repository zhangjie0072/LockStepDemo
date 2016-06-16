-- 20150617_144817




MemberDispChar =  {
	uiName = 'MemberDispChar',
	
	member_item,
	member_skill_list={},

}




function MemberDispChar:Awake()
	self.go = {}
	self.go.quality = self.transform:FindChild('Scroll/Quality/Label'):GetComponent('UILabel')
	self.go.talent_items_node = self.transform:FindChild('Scroll/Talent/TalentItemsNode')

	local telent_go = createUI('TalentItems',self.go.talent_items_node)
	self.talent_items = getLuaComponent(telent_go)

	self.go.skill_grid = self.transform:FindChild('Scroll/Skill/Grid'):GetComponent('UIGrid')

end



function MemberDispChar:Start()
	

end




function MemberDispChar:set_data(id,md)
	self.id = id
	self.md = md
	
	local attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.id,self.md.quality)
	self.talent_items:set_talent(attr_data.talent)
	
	local quality_strs = {'D-','D','D+','C-','C','C+','B-','B','B+','A-','A','A+','S-','S','S+'}
	self.go.quality.text = quality_strs[attr_data.quality]

	local role_config = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	if NGUITools.GetActive(self.go.skill_grid.gameObject) then
		print("Grids is active")
		CommonFunction.ClearGridChild(self.go.skill_grid.transform)
		self.member_skill_list = {}
		
		local enum = role_config.passive_skill:GetEnumerator()
		print('enum type here='..type(enum))
		while enum:MoveNext() do
			local skill_id = enum.Current
			print("skillid="..tostring(skill_id))
			local t = createUI("MemberSkillItem",self.go.skill_grid.transform)
			local script = getLuaComponent(t)
			table.insert(self.member_skill_list,script)
		end
		self.go.skill_grid:Reposition()
	else
		print("Grids is not active!!!!!!!!!!!!")
	end
end





return MemberDispChar

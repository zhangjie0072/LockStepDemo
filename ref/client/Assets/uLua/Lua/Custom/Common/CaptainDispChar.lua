-- 20150623_152156




CaptainDispChar =  {
	uiName = 'CaptainDispChar',
	
	go = {},

}




function CaptainDispChar:Awake()
	self.go={}
	 specail scroll
	self.go.scroll = self.transform:FindChild("Scroll").gameObject
	 specail 
	self.go.sp_property_grid = self.transform:FindChild('Scroll/SP_Property/Grid'):GetComponent('UIGrid')
	
	self.go.sp_skill_grid = self.transform:FindChild('Scroll/SP_Skill/Grid'):GetComponent('UIGrid')
	self.go.sp_skill_icon = self.transform:FindChild('Scroll/SP_Skill/Icon'):GetComponent('UISprite')

end

function CaptainDispChar:set_data(id,cd)
	self.cd = {}
	self.id = id
	self.cd = cd

	self:refresh_data()
end


function CaptainDispChar:refresh_data()
	local owned = MainPlayer.Instance:HasCaptain(self.id)
	local has_specail_skill = false -- display the specail skill nor not

	local base_data_config = GameSystem.Instance.RoleBaseConfigData2
	local captain_data_list = base_data_config:GetCaptainRoleConfig()
	
	local enum = captain_data_list:GetEnumerator()

	while enum:MoveNext() do
		-- RoleBaseData
		local item = enum.Current
		if item.id == self.id then
			self.role_base_data = item
			break
		end
	end
	
	local data = self.role_base_data
	print("CaptainDisplay self.go.sp_property_grid.transform="..tostring(self.go.sp_property_grid.transform))
	print("CaptainDisplay self.go.sp_property_grid.transform.name="..tostring(self.go.sp_property_grid.transform.name))
	CommonFunction.ClearGridChild(self.go.sp_property_grid.transform)
	if data.special_attrs then 
		enum = data.special_attrs:GetEnumerator()
		while enum:MoveNext() do
			local key = enum.Current.Key
			local value = enum.Current.Value
			print('specail key='..tostring(key))
			print('specail value='..tostring(value))
			local attr_name = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(tonumber(key))
			local AttrListItem=createUI('AttrListItem',self.go.sp_property_grid.transform)
			local UIWidget=AttrListItem:GetComponent('UIWidget')
			UIWidget.width=190
			--print("depth:"..UIWidget.width)
			local obj=getLuaComponent(AttrListItem)
			obj.value=tostring(value)
			obj.attrName=attr_name
			has_specail_skill = true
			
		end
	end   
	self.go.sp_property_grid:Reposition()


	CommonFunction.ClearGridChild(self.go.sp_skill_grid.transform)
	NGUITools.SetActive(self.go.sp_skill_icon.gameObject,false)
	if data.special_skill and data.special_skill ~= 0 then
		print('specail skill id ='..tostring(data.special_skill))
		-- TODO: change it latter
		self.go.sp_skill_icon.spriteName = 'icon_skill_'..tostring(data.special_skill)
		NGUITools.SetActive(self.go.sp_skill_icon.gameObject,true)
		local skillAttrConfig = GameSystem.Instance.SkillConfig:GetSkill(data.special_skill)
		local skill_level = skillAttrConfig:GetSkillLevel(1);
		local attrs = skill_level.additional_attrs
		local cast=GameSystem.Instance.SkillConfig:GetSkill(data.special_skill).cast
		local SkillLabel=createUI("SkillLabel",self.go.sp_skill_grid.transform)
		local obj=getLuaComponent(SkillLabel)
		obj:set_text(cast)

		local enum = attrs:GetEnumerator()


		while enum:MoveNext() do
			local key = enum.Current.Key
			local value = enum.Current.Value
			
			local attr_name = GameSystem.Instance.AttrNameConfigData:GetAttrName(key)
			
			local AttrListItem = createUI('AttrListItem',self.go.sp_skill_grid.transform)
			local UIWidget=AttrListItem:GetComponent('UIWidget')
			UIWidget.width=166
			local obj = getLuaComponent(AttrListItem)
			obj.attrName=attr_name
			obj.value=tostring(value)
			has_specail_skill = true
		end
		self.go.sp_skill_grid:Reposition()
	end
	
	NGUITools.SetActive(self.go.scroll,has_specail_skill)

end




function CaptainDispChar:Start()


end


return CaptainDispChar

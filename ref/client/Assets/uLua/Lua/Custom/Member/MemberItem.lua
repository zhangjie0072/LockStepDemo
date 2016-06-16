-- 20150615_193036


MemberItem =  {
	uiName = 'MemberItem',
	
	go ={
		-- bg_frame= nil,
	},

	id=0,
	role_base_config= nil,
	is_owned=false,
	on_click = nil,
	attr_data=nil,
	npc = false,
}




function MemberItem:Awake()
	print(self.go)
	print(self.transform)
	print("self.transform.name="..tostring(self.transform.name))
	

	self.go={}

	--local bg =  self.transform:FindChild('NameBackground').gameObject:GetComponent('UISprite')

	--self.go.name_bg = bg
	self.go.icon = self.transform:FindChild('Icon'):GetComponent('UISprite')
	
	--self.go.type = self.transform:FindChild('Type'):GetComponent('UISprite')
	self.go.position = self.transform:FindChild('Position').gameObject:GetComponent('UISprite')
	self.go.name = self.transform:FindChild('Name').gameObject:GetComponent('UILabel')

	self.go.btn = self.transform:FindChild('BG'):GetComponent('UIButton')
	--addOnClick(self.go.btn.gameObject,self:click())
	self.go.bg_frame = self.transform:FindChild('BG'):GetComponent('UISprite')
	self.go.bg_frame.spriteName = 'com_frame_head'
	self:set_selected(false)
end

function MemberItem:click()
	return function()
		if self.on_click then 
			self:on_click()
		end
	end
end



function MemberItem:Start()


end


function MemberItem:set_role_base_config(config)
	self.role_base_config = config
end



function MemberItem:set_selected(is_selected)
	if is_selected then 
		print('MemberItem:set_seletect--true')

		print(self.go.bg_frame.gameObject)
		print('MemberItem:set_selected called id='..tostring(self.id)..' '..self.role_base_config.name)

		NGUITools.SetActive(self.go.bg_frame.gameObject,false)
		-- NGUITools.SetActive(self.go_bg_frame.gameObject,false)
	else
		NGUITools.SetActive(self.go.bg_frame.gameObject,true)
		if self.is_owned then 
			--self.go.bg_frame.spriteName = 'com_frame_member_brown'
			self.go.bg_frame.spriteName = 'com_frame_head'
			self.go.btn.normalSprite = 'com_frame_member_brown'
			--self.go.name_bg.spriteName = 'com_frame_member_yellow'
		else
			self.go.bg_frame.spriteName = 'com_frame_head'
			self.go.btn.normalSprite = 'com_frame_member_black'
			--self.go.name_bg.spriteName = 'com_frame_member_gray'
		end


	end
	
end

function MemberItem:update()
	self:update_by_id(self.id)
end

function MemberItem:refresh()
	self:update_by_id(self.id)
end

function MemberItem:update_by_id( id )
	if self.npc == false then
		self.id = id
		print('MemberItem:update_by_id='..tostring(id))
		self.go.icon.atlas = getPortraitAtlas(id)
		self.go.icon.spriteName = 'icon_portrait_'..tostring(self.id)	
		self:set_role_base_config(GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id))		
		self.go.name.text = self.role_base_config.name		
		-- (1:PF,2:SF,3:C,4:PG,5:SG)
		local positions ={'PF','SF','C','PG','SG'}
		self.go.position.spriteName = 'PT_'..positions[self.role_base_config.position]
		-- local types ={'',}
		-- self.go.type.spriteName = 
		self:update_is_owned()
		self:update_attr_data()
		local bias={'attack','defense','balance'}
		--print("self.attr_data.bias="..tostring(self.attr_data.bias))
		--self.go.type.spriteName = 'com_frame_member_'..bias[self.attr_data.bias]
		--self.go.type:MakePixelPerfect()
	else
		self.id = id
		self:set_role_base_config(GameSystem.Instance.NPCConfigData:GetConfigData(self.id))
		self.go.name.text = self.role_base_config.name
		local shap_id = GameSystem.Instance.NPCConfigData:GetShapeID(self.id)
		self.go.icon.atlas = getPortraitAtlas(shap_id)
		self.go.icon.spriteName = self.role_base_config.icon
		print("444444444----",self.go.icon.spriteName)
		-- (1:PF,2:SF,3:C,4:PG,5:SG)
		local positions ={'PF','SF','C','PG','SG'}
		self.go.position.spriteName = 'PT_'..positions[self.role_base_config.position]
		-- local types ={'',}
		-- self.go.type.spriteName = 
		self:update_is_owned()
		self:update_attr_data()
		local bias={'attack','defense','balance'}
		--print("self.attr_data.bias="..tostring(self.attr_data.bias))
		--self.go.type.spriteName = 'com_frame_member_'..bias[self.attr_data.bias]
		--self.go.type:MakePixelPerfect()
	end

end


function MemberItem:update_attr_data()
	self.quality = 1
	if self.is_owned then
		local player = MainPlayer.Instance:GetRole(self.id)
		self.quality = player:GetQuality()
	end
	self.attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.id,self.quality)
end


function MemberItem:update_is_owned()
	self.is_owned = MainPlayer.Instance:HasRole(self.id)
	
end

return MemberItem

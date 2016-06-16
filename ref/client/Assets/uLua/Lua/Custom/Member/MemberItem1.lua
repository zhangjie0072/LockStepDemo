-- 20150615_193036


MemberItem1 =  {
	uiName = 'MemberItem1',
	
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




function MemberItem1:Awake()
	print(self.go)
	print(self.transform)
	print("self.transform.name="..tostring(self.transform.name))
	

	self.go={}

	--local bg =  self.transform:FindChild('NameBackground').gameObject:GetComponent('UISprite')

	--self.go.name_bg = bg
	self.go.icon = self.transform:FindChild('icon'):GetComponent('UISprite')
	
	--self.go.type = self.transform:FindChild('Type'):GetComponent('UISprite')
	self.go.position = self.transform:FindChild('position').gameObject:GetComponent('UISprite')
	--self.go.name = self.transform:FindChild('Name').gameObject:GetComponent('UILabel')

	self.labelExp = self.transform:FindChild('exp/labelExp'):GetComponent('UILabel')
	self.bgExp = self.transform:FindChild('exp/bgExp'):GetComponent('UISprite')
	self.bgFrame = self.transform:FindChild('bgFrame'):GetComponent('UISprite')
	self.levelLabel = self.transform:FindChild('labelLevel'):GetComponent('UILabel')
	self.aptitudeSprite = self.transform:FindChild('aptitude'):GetComponent('UISprite')
	self.upgrade = self.transform:FindChild('upgrade')
	self.bias = self.transform:FindChild('type'):GetComponent('UISprite')
	--self.go.btn = self.transform:FindChild('BG'):GetComponent('UIButton')
	--addOnClick(self.go.btn.gameObject,self:click())
	--self.go.bg_frame = self.transform:FindChild('BG'):GetComponent('UISprite')
	--self.go.bg_frame.spriteName = 'com_frame_head'
	--self:set_selected(false)
	self.addlevel = false
end

function MemberItem1:click()
	return function()
		if self.on_click then 
			self:on_click()
		end
	end
end



function MemberItem1:Start()

	self.fixedDeltaTime = 2
end


function MemberItem1:set_role_base_config(config)
	self.role_base_config = config
end



--[[function MemberItem1:set_selected(is_selected)
	if is_selected then 
		print('MemberItem1:set_seletect--true')

		--print(self.go.bg_frame.gameObject)
		print('MemberItem1:set_selected called id='..tostring(self.id)..' '..self.role_base_config.name)

		--NGUITools.SetActive(self.go.bg_frame.gameObject,false)
		-- NGUITools.SetActive(self.go_bg_frame.gameObject,false)
	else
		--NGUITools.SetActive(self.go.bg_frame.gameObject,true)
		if self.is_owned then 
			--self.go.bg_frame.spriteName = 'com_frame_member_brown'
			--self.go.bg_frame.spriteName = 'com_frame_head'
			--self.go.btn.normalSprite = 'com_frame_member_brown'
			--self.go.name_bg.spriteName = 'com_frame_member_yellow'
		else
			--self.go.bg_frame.spriteName = 'com_frame_head'
			--self.go.btn.normalSprite = 'com_frame_member_black'
			--self.go.name_bg.spriteName = 'com_frame_member_gray'
		end


	end	
end]]

--动态增加exp
function MemberItem1:FixedUpdate( ... )
	if self.addExp > 0 then
		self.role.exp = self.role.exp + self.fixedDeltaTime
		--如果升级
		if self.role.exp >= self.maxExp then
			
			--因为球队等级限制队员等级
			local teamLimitLevel = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(MainPlayer.Instance.Level)
			if self.role.level >= teamLimitLevel then
				CommonFunction.ShowPopupMsg(getCommonStr('TEAMLEVEL_LIMIT_PLAYERLEVEL'),nil,nil,nil,nil,nil)
				self.bgExp:SetDimensions(self.fixedDeltaTime* self.k, 20)
				self.addExp = 0
				return
			end

			MainPlayer.Instance:GetRole2(self.id).level = MainPlayer.Instance:GetRole2(self.id).level + 1
			MainPlayer.Instance:GetRole2(self.id).exp = 0 
			self.role = MainPlayer.Instance:GetRole2(self.id)
			self.maxExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(self.role.level)
			print('upgrade==========!!')
			self.upgrade.gameObject:SetActive(true)
			self.k = 100 / GameSystem.Instance.RoleLevelConfigData:GetMaxExp(self.role.level)
			self.levelLabel.text = self.role.level
			self:Refresh(self.id)
		end
		local nowExp = self.role.exp * self.k
		print('self.maxExp ===== ' .. self.maxExp)
		print('nowExp ====== ' .. nowExp)
		self.bgExp:SetDimensions(nowExp, 20)		--设置初始经验
		self.addExp = self.addExp - self.fixedDeltaTime
		print('addExp leave exp' .. self.addExp)
	end
end

function MemberItem1:Refresh(id)
	local roleinfo = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
	local biasSprites = {'com_icon_attack','com_icon_defense','com_icon_balance'}
	self.bias.spriteName = biasSprites[roleinfo.bias]
	--self.bias:MakePixelPerfect()
	local Colors = {
		Color.New(24/255, 178/255, 53/255, 1),		--green
		Color.New(0, 129/255, 230/255, 1),		--blue
		Color.New(114/255, 16/255, 201/255, 1),	--purple
		Color.New(247/255, 198/255, 4/255, 1),		--yellow
		Color.New(1, 1, 1, 1),		--white
	}
	if roleinfo.talent == 5 then		--8 14 11 5
		self.bgFrame.color = Colors[1]
	elseif roleinfo.talent == 8 then		--8 14 11 5
		self.bgFrame.color = Colors[2]
	elseif roleinfo.talent == 11 then		--8 14 11 5
		self.bgFrame.color = Colors[3] 
	elseif roleinfo.talent == 14 then		--8 14 11 5
		self.bgFrame.color = Colors[4]
	else
		self.bgFrame.color = Colors[5]
	end

	print('MemberItem1:update_by_id='..tostring(id))
	self.go.icon.atlas = getPortraitAtlas(id)
	self.go.icon.spriteName = 'icon_portrait_'..tostring(self.id)	
	local positions ={'PF','SF','C','PG','SG'}
	self.go.position.spriteName = 'PT_'..positions[roleinfo.position]
	self:update_is_owned()
	self:update_attr_data()
end

function MemberItem1:update_by_id( id )
	if self.npc == false then
		self.id = id
		--显示球员数据,role的服务器计算过返回的数据
		print("---------------id: ", id)
		--球员 MainPlayer.Instance:GetRole2(id)
		self.role = MainPlayer.Instance:GetRole2(id)
		print('self.role.exp ======== ' .. self.role.exp)
		print('self.add.exp ======== ' .. self.addExp)
		print('self.role.level ======== ' .. self.role.level)
		--当前等级最大经验
		self.maxExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(self.role.level)

		self.labelExp.text = '+' .. self.addExp
		--经验条系数
		self.k = 100/self.maxExp
		self.bgExp:SetDimensions(self.role.exp * self.k, 20)		--设置初始经验
		self.levelLabel.text = self.role.level 			--设置初始等级
		self.upgrade.gameObject:SetActive(false)
		self:Refresh(id)
	else
		self.id = id
		self:set_role_base_config(GameSystem.Instance.NPCConfigData:GetConfigData(self.id))
		--self.go.name.text = self.role_base_config.name
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


function MemberItem1:update_attr_data()
	self.quality = 1
	if self.is_owned then
		local player = MainPlayer.Instance:GetRole(self.id)
		self.quality = player:GetQuality()
	end
	self.attr_data = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.id,self.quality)
end


function MemberItem1:update_is_owned()
	self.is_owned = false
	self.is_owned = MainPlayer.Instance:HasRole(self.id)
end

return MemberItem1

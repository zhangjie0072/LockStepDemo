-- 20150618_105831








CaptainItem =  {
	uiName = 'CaptainItem',

	go ={
	-- bg_frame= nil,
	},

	id=0,
	role_base_config= nil,
	is_owned=false,
	on_click = nil,
	attr_data=nil,
	bias=1,
	scroll = nil,

}




function CaptainItem:Awake()
	print(self.go)
	print(self.transform)
	print("self.transform.name="..tostring(self.transform.name))


	self.go={}

	local bg =  self.transform:FindChild('NameBackground').gameObject:GetComponent('UISprite')
	print(bg)
	self.go.name_bg = bg
	self.go.drag_scroll_view = self.transform:FindChild("BG"):GetComponent("UIDragScrollView")
	self.go.icon = self.transform:FindChild('Icon'):GetComponent('UISprite')
	self.go.type = self.transform:FindChild('Type'):GetComponent('UISprite')
	self.go.position = self.transform:FindChild('Position').gameObject:GetComponent('UISprite')
	self.go.name = self.transform:FindChild('Name').gameObject:GetComponent('UILabel')

	self.go.btn = self.transform:FindChild('BG'):GetComponent('UIButton')
	addOnClick(self.go.btn.gameObject,self:click())
	self.go.bg_frame = self.transform:FindChild('BG'):GetComponent('UISprite')
	self.go.current = self.transform:FindChild('Current').gameObject
end

function CaptainItem:click()
	return function()
		if not self.is_owned then
			-- self.bias = 1
		end

		if self.on_click then 
			self:on_click()
		end
	end
end



function CaptainItem:Start()
	self.go.drag_scroll_view.scrollView = self.scroll
end


function CaptainItem:set_role_base_config(config)
	self.role_base_config = config
end



function CaptainItem:set_selected(is_selected)
	if is_selected then 
		NGUITools.SetActive(self.go.bg_frame.gameObject,false)
	else
		NGUITools.SetActive(self.go.bg_frame.gameObject,true)
		if self.is_owned then 
			self.go.bg_frame.spriteName = 'com_frame_captain_brown'
			self.go.btn.normalSprite = 'com_frame_captain_brown'
			self.go.name_bg.spriteName = 'com_frame_captain_yellow'
		else
			self.go.bg_frame.spriteName = 'com_frame_captain_black'
			self.go.btn.normalSprite = 'com_frame_captain_black'
			self.go.name_bg.spriteName = 'com_frame_captain_gray'
		end
	end
end


function CaptainItem:refresh()
	self:update_by_id(self.id)
end

function CaptainItem:update_by_id( id )
	self.id = id
	
	self.go.icon.atlas = getPortraitAtlas(id) 
	self.go.icon.spriteName = 'icon_portrait_'..tostring(self.id)

	self:set_role_base_config(GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id))

	self.go.name.text = self.role_base_config.name
	local positions ={'PF','SF','C','PG','SG'}
	self.go.position.spriteName = 'PT_'..positions[self.role_base_config.position]

	self:update_is_owned()
	self:update_attr_data()

	NGUITools.SetActive(self.go.current,self.id == MainPlayer.Instance.CaptainID)
end

function CaptainItem:hide_current()
	NGUITools.SetActive(self.go.current,false)
end


function CaptainItem:update_attr_data()
	self.bias = 1
	local level = 1
	if self.is_owned then
		local role_info = MainPlayer.Instance:GetCaptainInfo(self.id)
		quality = role_info.quality
		self.bias = role_info.bias
		if self.bias <= 0 or self.bias > 3 then
			error('bias error='..tostring(self.bias))
		end
		-- level =			-- 
		level = MainPlayer.Instance.Level
	end
	-- todo
	quality =1
	self.attr_data = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(self.id,self.bias,level)

end



function CaptainItem:owned()
	return MainPlayer.Instance:HasCaptain(self.id)
end

function CaptainItem:update_is_owned()
	self.is_owned = MainPlayer.Instance:HasCaptain(self.id)
end

return CaptainItem

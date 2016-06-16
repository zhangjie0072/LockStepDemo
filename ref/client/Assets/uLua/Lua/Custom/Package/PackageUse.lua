-- 20150612_142006


PackageUse =  {
	uiName = 'PackageUse',
	
	good_item =nil,
	on_click_use=nil,
}

function PackageUse:Awake()
	self.go = {}
	self.go.ps_iconNode = self.transform:FindChild('PS_iconNode').transform
	self.go.ps_infoNode = self.transform:FindChild('PS_infoNode').transform
	
	local go_ps_icon = createUI('PS_icon', self.go.ps_iconNode)
	self.ps_icon = getLuaComponent(go_ps_icon)

	--local go_ps_info = createUI('PS_info',self.go.ps_infoNode)
	--self.ps_info = getLuaComponent(go_ps_info)

	self.go.close_btn = getChildGameObject( self.transform,'CloseBtn')
	addOnClick(self.go.close_btn,self:click_close())

	self.go.use_btn = getChildGameObject(self.transform,'UseBtn')
	self.go.use_btn_lbl = self.transform:FindChild('UseBtn/Label'):GetComponent("UILabel")
	self.go.use_btn_lbl.text = getCommonStr("STR_USE")

	addOnClick(self.go.use_btn,self:click_use())
	self.go.title = self.transform:FindChild('Title'):GetComponent('UILabel')
end

function PackageUse:click_close()
	return function()
		GameObject.Destroy(self.gameObject)
	end
end

function PackageUse:click_use()
	return function()
		if self.on_click_use then
			self:on_click_use()
		end
	end
end

function PackageUse:Start()
	self.ps_icon.good_item = self.good_item
	--self.ps_info.good_item = self.good_item
	self:update_title()
end

function PackageUse:update_title()
	self.go.title.text = getCommonStr("STR_USE")
end


return PackageUse

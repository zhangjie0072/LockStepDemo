-- 20150617_144817



TalentItems =  {
	uiName = 'TalentItems',
	
	talent = 1,
	mode =0,
	is_show_last=true,
	blink_counter = 0,
}

-- mode 
-- 0 : show all
-- 1 : show part
-- 2 : blink


function TalentItems:FixedUpdate()
	if self.mode ~= 2 then
		return
	end
	self.blink_counter = self.blink_counter+1

	if self.blink_counter > 10 then 
		local last =self.go.stars[self.talent+1].gameObject
		NGUITools.SetActive(last,self.is_show_last)
		self.is_show_last = not self.is_show_last
		self.blink_counter = 0
	end
	
	
end

function TalentItems:Awake()
	self.go ={}
	self.go.stars ={}

	for i=1,5 do
		self.go.stars[i] = self.transform:FindChild("star"..tostring(i-1)):GetComponent('UISprite')
	end

end



-- function TalentItems:Start()

-- end


function TalentItems:set_mode(mode)
	self.mode = mode
end

function TalentItems:set_active(index,is_active)
	NGUITools.SetActive(self.go.stars[index].gameObject,is_active)
end

function TalentItems:hide_all()
	for index=1,5 do
		NGUITools.SetActive(self.go.stars[index].gameObject,false)
	end
end


function TalentItems:set_talent(talent)
	self.talent =talent 
	self:hide_all()
	if self.mode == 0 then
		for i = 1, 5 do
			self:set_active(i,true)
			if i <= self.talent then
				self.go.stars[i].spriteName = 'com_icon_starRank'
			else
				self.go.stars[i].spriteName = 'com_icon_starRank_dark'
			end
		end
	elseif self.mode ==1 then
		for i=1,self.talent do
			self.go.stars[i].spriteName = 'com_icon_starRank'
			self:set_active(i,true)
		end
	elseif self.mode==2 then
		for i=1,self.talent+1 do
			self.go.stars[i].spriteName = 'com_icon_starRank'
			self:set_active(i,true)
		end
	end

end

return TalentItems

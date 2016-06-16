MatchTypeIcon = {
	uiName = 'MatchTypeIcon',
	
	matchType = GameMatch.Type.eNone,
	isDisabled = false,
	id,
	isInfo = false,
	is_boss,
	fix = true,
	iconWidth,
	iconHeight, 
}

function MatchTypeIcon:Start()
	if self.isInfo == false then
		local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.id)
		print("sectionConfig:",sectionConfig,"id:",self.id)
		self.uiIcon = self.gameObject:GetComponent("UISprite")
		if self.is_boss == true then
			local id = tonumber(sectionConfig.icon)
			self.uiIcon.atlas = getPortraitAtlas(id)
			self.uiIcon.spriteName = 'icon_portrait_'..tostring(id)	
			--print("------------sectionConfig:",self.id,"atlas:",self.uiIcon.atlas,"sprite:",self.uiIcon.spriteName)
		elseif self.isDisabled == false then
			self.uiIcon.spriteName = "career_flag"
		else 
			self.uiIcon.spriteName = sectionConfig.icon
		end
	else
		self.uiIcon = self.gameObject:GetComponent("UISprite")
		local strMatchType = tostring(self.matchType)
		print("spppppppp:",strMatchType)
		self.uiIcon.spriteName = "career_Ozd_" .. self["Icon_" .. strMatchType] .. (self.isDisabled and "_gray" or "")
	end
	
	if self.fix then
		print("icon fix calledd")
		self.uiIcon:MakePixelPerfect()
	else
		self.uiIcon.width = self.iconWidth
		self.uiIcon.height = self.iconHeight
	end
	
end

return MatchTypeIcon

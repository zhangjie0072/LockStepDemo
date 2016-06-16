-- 20150617_144817


MemberSkillItem =  {
	uiName = 'MemberSkillItem',
	
	skill_id,
	go = {},
}




function MemberSkillItem:Awake()
	self.go = {}

	self.go.icon = self.transform:GetComponent('UISprite')

end



function MemberSkillItem:Start()
	

end

function MemberSkillItem:set_skill_id(skill_id)
	self.skill_id = skill_id
	self.go.icon.spriteName = 'icon_skill'..tostring(self.skill_id)
end




return MemberSkillItem

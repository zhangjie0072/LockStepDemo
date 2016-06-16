-- 20150619_102102


MemberSkillChangeItem =  {
	uiName = 'MemberSkillChangeItem',
	
	go ={},
	id=0,
	label="",
}




function MemberSkillChangeItem:Awake()
	self.go ={}
	self.go.icon = self.transform:GetComponent('UISprite')
	self.go.label = self.transform:FindChild('Label'):GetComponent('UILabel')
end



function MemberSkillChangeItem:Start()
	self.go.icon.spriteName = 'icon_skill_'..tostring(self.id)
	self.go.label.text = self.label
end


return MemberSkillChangeItem

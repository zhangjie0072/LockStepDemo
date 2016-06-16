-- 20150623_152156



SkillLabel =  {
	uiName = 'SkillLabel',
	
	go = {},


}




function SkillLabel:Awake()
	self.go = {}
	self.go.label = self.transform:GetComponent('UILabel')

end



function SkillLabel:set_text(str)
	self.go.label.text = str
end

function SkillLabel:Start()


end


return SkillLabel

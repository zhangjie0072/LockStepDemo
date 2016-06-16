-- 20150617_144817




PD_PropertyItem =  {
	uiName = 'PD_PropertyItem',
	go = {},
}




function PD_PropertyItem:Awake()
	self.go ={}
	self.go.process= self.transform:FindChild('Process'):GetComponent('UIProgressBar')
	self.go.name = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.go.value = self.transform:FindChild('Value'):GetComponent('UILabel')
	self.go.recommend = self.transform:FindChild("Recommend").gameObject
end



function PD_PropertyItem:Start()

end


function PD_PropertyItem:set_value( process,name,value,recommend)
	self.go.process.value = process
	self.go.name.text = tostring(name)
	self.go.value.text = tostring(value)

	NGUITools.SetActive(self.go.recommend,recommend)
end

return PD_PropertyItem

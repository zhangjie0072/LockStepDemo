ExerciseEnhance = {
	uiName = "ExerciseEnhance",
	--parameters
	onClose,
}

function ExerciseEnhance:Awake()
	self.uiAnimator = self.transform:FindChild("Animator")
end

function ExerciseEnhance:Start()
	
end

function ExerciseEnhance:OnClose()
	if self.onClose then 
		print(self.uiName,"onClose after animation")
		self:onClose() 
	end
end

return ExerciseEnhance
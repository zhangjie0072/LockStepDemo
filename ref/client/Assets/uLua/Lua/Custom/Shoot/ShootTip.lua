ShootTip = {
	uiName = "ShootTip",
	--ui

	--parameters
	score,
	num,
	
}

function ShootTip:Awake()
	self.uiScore = self.transform:FindChild("Num"):GetComponent("UILabel")
	self.uiRewardsNum = self.transform:FindChild("Num1"):GetComponent("UILabel")
end

function ShootTip:Start()
	self.uiScore.text = self.score
	self.uiRewardsNum.text = self.num
end

return ShootTip
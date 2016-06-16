ExpBar = 
{
	uiName = 'ExpBar',
}

function ExpBar:Awake()
	self.progress = self.gameObject:GetComponent("UIProgressBar")
	self.label = getComponentInChild(self.transform, "Label", "UILabel")
end

function ExpBar:Refresh(curExp, level)
	local maxExp = GameSystem.Instance.TeamLevelConfigData:GetMaxExp(level)
	for i = 1, level - 1 do
		curExp = curExp - GameSystem.Instance.TeamLevelConfigData:GetMaxExp(i)
	end
	self.progress.value = curExp / maxExp;
	self.label.text = curExp .. "/" .. maxExp;
	NGUITools.SetActive(self.progress.foregroundWidget.gameObject, curExp > 0)
end

return ExpBar

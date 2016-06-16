--encoding=utf-8

QualifyingOutsItem = 
{
	uiName = 'QualifyingOutsItem',
	-----------------UI
	uiDate,
	uiTimes,
	uiContent,
	------------------parameters
	time, 
	name,
	ranking,
	state,
}


-----------------------------------------------------------------
--Awake
function QualifyingOutsItem:Awake()
	self.uiDate = getComponentInChild(self.transform,"Date","UILabel")
	self.uiTimes = getComponentInChild(self.transform,"Time","UILabel")
	self.uiContent = getComponentInChild(self.transform,"Content","UILabel")
end

function QualifyingOutsItem:Start()
	print("qualifying state:",self.state)
	self.uiDate.text = os.date("%m-%d", self.time)
	self.uiTimes.text = os.date("%H:%M:%S", self.time)
	if self.state == 1 then
			self.uiContent.text = getCommonStr("QUALIFYING_WIN"):format(self.name, self.ranking)
	elseif self.state == 2 then
		if self.ranking == 0 then
			self.uiContent.text = getCommonStr("QUALIFYING_LOSE_OVER"):format(self.name, GameSystem.Instance.CommonConfig:GetUInt("QualifyingMaxRank")..getCommonStr("OVER_MAXRANK"))
		else 
			self.uiContent.text = getCommonStr("QUALIFYING_LOSE"):format(self.name, self.ranking)
		end
	elseif self.state == 3 then
		self.uiContent.text = getCommonStr("STR_WIN_QUALIFYING"):format(self.name)
	elseif self.state == 4 then
		self.uiContent.text = getCommonStr("STR_LOSE_QUALIFYING"):format(self.name)
	end
end

return QualifyingOutsItem
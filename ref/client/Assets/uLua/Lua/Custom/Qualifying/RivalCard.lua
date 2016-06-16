--encoding=utf-8

RivaCard =
{
	uiName = 'RivaCard',
	----------------UI
	uiRoleGrid,
	uiNameLabel,
	uiRank,
	uiRank,
	uiRankNumLabel,
	uiIcon,
	uiLev,
	uiButtonChallenge,
	uiFightNum,
	----------------parameters
	show_id,
	name,
	ranking_num,
	simple_info = {},
	onClick = nil,
	onChallenge =nil,
	level,
	player_type,
	fightPower,
	star,
	quality,

}


-----------------------------------------------------------------
--Awake
function RivaCard:Awake()
	self.uiRoleGrid = self.transform:FindChild("Grid"):GetComponent("UIGrid")
	self.uiRole = {}
	self.uiRole[1] = self.uiRoleGrid.transform:FindChild("CareerRoleIcon1")
	self.uiRole[2] = self.uiRoleGrid.transform:FindChild("CareerRoleIcon2")
	self.uiRole[3] = self.uiRoleGrid.transform:FindChild("CareerRoleIcon3")
	self.uiNameLabel = getComponentInChild(self.transform,"Name","UILabel")
	self.uiRank = getComponentInChild(self.transform,"Label_Ranklist","UILabel")
	self.uiRank.text = getCommonStr("STR_RANKING")
	self.uiRankNumLabel =getComponentInChild(self.transform,"Label_Ranklist/Num","UILabel")
	self.uiIcon = getComponentInChild(self.transform,"Background/Icon","UISprite")
	self.uiLev = getComponentInChild(self.transform,"Label_Level","UILabel")
	self.uiButtonChallenge = getChildGameObject(self.transform,"ButtonChallenge")
	self.uiFightNum = self.transform:FindChild("FightNum")
	addOnClick(self.uiButtonChallenge,self:MakeOnButton())
end

function RivaCard:Start()
	self:Refresh()
end

function RivaCard:MakeOnButton()
	return function ()
		if self.onChallenge then
			self.onChallenge(self)
		end
	end
end

-----------------------------------------------------------------
function RivaCard:Refresh( ... )
	print("Refresh( ...)")
	if self.level then
		self.uiLev.text = "Lv."..self.level
	end
	if self.ranking_num then
		self.uiRankNumLabel.text = self.ranking_num
	end
	if self.name then
		self.uiNameLabel.text = self.name
	end
	if self.show_id then
		self.uiIcon.atlas = getBustAtlas( self.show_id)
		self.uiIcon.spriteName = "icon_bust_"..tostring(self.show_id)
	end
	------create role uiIcon
	local RobotPlayer
	if self.player_type == "ROBOT" then
		RobotPlayer = GameSystem.Instance.qualifyingConfig:GetRobotPlayer(self.level)
	end
	local index = 0
	for i, v in pairs(self.simple_info) do
		-- local roleIcon = getLuaComponent(self.uiRole[index + 1])
		local roleIcon = getLuaComponent(createUI("CareerRoleIcon", self.uiRoleGrid.transform))
		-- print("roleIcon_id:",v.id,"index:",index)
		roleIcon.id = v.id
		if self.player_type == "ROBOT" then
			roleIcon.isRobot = true
			--roleIcon.talent = RobotPlayer[ index].aptitude
			roleIcon.talent =  GameSystem.Instance.RoleBaseConfigData2:GetConfigData(v.id).talent

			local attrData = GameSystem.Instance.NPCConfigData:GetQualifyingNPCAttrData(v.id, v.lev);
			MainPlayer.Instance:CalcFightingCapacity(v.id, v.star, attrData);
			self.fightPower = self.fightPower + attrData.fightingCapacity
			roleIcon.qQuality = v.quality
			roleIcon.qLevel = v.lev
			roleIcon.qStar = v.star
		else
			roleIcon.isQReal = true
			roleIcon.qQuality = v.quality
			roleIcon.qLevel = v.lev
			roleIcon.qStar =v.star
		end
		index =index + 1
		roleIcon:Refresh()
	end
	-- NGUITools.SetActiveChildren(self.uiRoleGrid.gameObject,false)
	-- NGUITools.SetActiveChildren(self.uiRoleGrid.gameObject,true)

	--set fight power
	-- print(self.uiName,"-----fight power:",self.fightPower)
	if self.player_type == "ROBOT" then
		local num = getLuaComponent(createUI("FightNum", self.uiFightNum))
		num:SetNum(self.fightPower)
	else
		local num = getLuaComponent(createUI("FightNum", self.uiFightNum))
		num:SetNum(self.fightPower)
	end

	self.uiRoleGrid.repositionNow = true
	self.uiRoleGrid:Reposition()
end

return RivaCard

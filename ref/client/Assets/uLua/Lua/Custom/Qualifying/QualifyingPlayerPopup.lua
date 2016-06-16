--encoding=utf-8

QualifyingPlayerPopup = 
{
	uiName = 'QualifyingPlayerPopup',
	name,
	player_type,
	lev_num,
	vipLevnum,
	simple_info = {},
}


-----------------------------------------------------------------
--Awake
function QualifyingPlayerPopup:Awake()
	self.teamName = getComponentInChild(self.transform,"Upside/teamName","UILabel")
	self.teamLev = getComponentInChild(self.transform,"Upside/teamLev","UILabel")
	self.vipLev = getComponentInChild(self.transform,"Upside/VipIcon/LabelVipLevel","UILabel")
	self.tm = self.transform:FindChild("Upside/Grid")
	self.vipIcon = self.transform:FindChild("Upside/VipIcon")
end

function QualifyingPlayerPopup:Start()
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.uiFrame = getLuaComponent(goFrame)
	self.uiFrame.title = CommonFunction.GetConstString("QUALIFYING_RIVALINFO")
	self.uiFrame.showCorner = true
	self.uiFrame.BUTTON_COUNT = 0
	self.uiFrame.onClose = self:MakeOnClose()
	print("self.player_type:",self.player_type)
	if self.player_type == "ROBOT" or self.vipLevnum == 0 then
		NGUITools.SetActive(self.vipIcon.gameObject,false)
	else
		self.vipLev.text = self.vipLevnum
	end
	self.teamName.text = self.name
	self.teamLev.text = self.lev_num
	--判断是机器人还是玩家
	print("leleleleleleleleelelel:",self.lev_num)
	if self.player_type == 'ROBOT' then
		--根据机器人的等级读取配置）
		print("lelelelelelelelelel:",self.lev_num)
		local RobotPlayer = GameSystem.Instance.qualifyingConfig:GetRobotPlayer(self.lev_num)
		for i = 1, 3 do
			--头像位置显示
			printTable(self.simple_info[i])
			local id = self.simple_info[i].id
			local player = getChildGameObject(self.tm,"player"..i)
			local portrait = getComponentInChild(player.transform,"Icon","UISprite")
			local role_position = getComponentInChild(player.transform,"Profession","UISprite")
			portrait.atlas = getPortraitAtlas(id)
			portrait.spriteName = 'icon_portrait_'..tostring(id)
			local role_config = GameSystem.Instance.RoleBaseConfigData:GetConfigData(id)	
			print("ixixixixiixixi----:",role_config)
			local positions ={'PF','SF','C','PG','SG'}
			role_position.spriteName = 'PT_'..positions[role_config.position]
			--等级显示
			local level = getComponentInChild(player.transform,"Level","UILabel")
			level.text = self.simple_info[i].lev
			--星级处理
			--[[
			local starNum = self.simple_info[i].star
			local x = 5 - starNum
			if x > 0 then 
				for j = 1,x do
					NGUITools.SetActive(getChildGameObject(player.transform,"star"..j),false)
				end
			end	
			--]]
			
			--name
			local namelabel = getComponentInChild(player.transform,"labelName","UILabel")
			namelabel.text = role_config.name
			--星等处理
			local starlabel = getComponentInChild(player.transform,"starName","UILabel")
			--starlabel.text = "+"..self.simple_info[i].star
			starlabel.text = "+"..RobotPlayer[ i - 1].star
			--边框处理/资质（根据机器人的等级读取配置quality）
			local side = getComponentInChild(player.transform,"Side","UISprite")
			local apititude = getComponentInChild(player.transform,"aptitude","UISprite")
			print("RobotPlayer.aptitude:",RobotPlayer[ i - 1].aptitude,"----",i)
			if RobotPlayer[ i - 1].aptitude == 1 then
				side.spriteName = "com_frame_green"
				apititude.spriteName = "QT_C"
			elseif RobotPlayer[ i - 1].aptitude == 2 then
				side.spriteName = "com_frame_blue"
				apititude.spriteName = "QT_B"
			elseif RobotPlayer[ i - 1].aptitude == 3 then
				side.spriteName = "com_frame_purple"
				apititude.spriteName = "QT_A"
			elseif RobotPlayer[ i - 1].aptitude == 4 then
				side.spriteName = "com_frame_golden"
				apititude.spriteName = "QT_S"
			end
		end
	elseif self.player_type == 'PLAYER' then
		--读取
		for i = 1, 3 do
		--头像位置显示
			printTable(self.simple_info[i])
			local id = self.simple_info[i].id
			local player = getChildGameObject(self.tm,"player"..i)
			local portrait = getComponentInChild(player.transform,"Icon","UISprite")
			local role_position = getComponentInChild(player.transform,"Profession","UISprite")
			portrait.atlas = getPortraitAtlas(id)
			portrait.spriteName = 'icon_portrait_'..tostring(id)
			local role_config = GameSystem.Instance.RoleBaseConfigData:GetConfigData(id)	
			print("ixixixixiixixi----:",role_config)
			local positions ={'PF','SF','C','PG','SG'}
			role_position.spriteName = 'PT_'..positions[role_config.position]
			--等级显示
			local level = getComponentInChild(player.transform,"Level","UILabel")
			level.text = self.simple_info[i].lev	
			--name
			local namelabel = getComponentInChild(player.transform,"labelName","UILabel")
			namelabel.text = role_config.name
			--星等处理
			local starlabel = getComponentInChild(player.transform,"starName","UILabel")
			starlabel.text = "+"..self.simple_info[i].star
			--starlabel.text = "+"..RobotPlayer[ i + 1].star
			--边框处理/资质
			--print("ssssssssssssssssssss:",id)
			local talent = GameSystem.Instance.RoleBaseConfigData2:GetIntTalent(id)
			print("talent:",talent)
			local side = getComponentInChild(player.transform,"Side","UISprite")
			local apititude = getComponentInChild(player.transform,"aptitude","UISprite")
			if talent == 1 then
				side.spriteName = "com_frame_green"
				apititude.spriteName = "QT_C"
			elseif talent == 2 then
				side.spriteName = "com_frame_blue"
				apititude.spriteName = "QT_B"
			elseif talent == 3 then
				side.spriteName = "com_frame_purple"
				apititude.spriteName = "QT_A"
			elseif talent == 4 then
				side.spriteName = "com_frame_golden"
				apititude.spriteName = "QT_S"
			end
		end
	end
	-- for i = 1, 3 do
	-- 	printTable(self.simple_info[i])
	-- 	local id = self.simple_info[i].id
	-- 	local player = getChildGameObject(self.tm,"player"..i)
	-- 	local portrait = getComponentInChild(player.transform,"Icon","UISprite")
	-- 	local role_position = getComponentInChild(player.transform,"Profession","UISprite")
	-- 	portrait.atlas = getPortraitAtlas(id)
	-- 	portrait.spriteName = 'icon_portrait_'..tostring(id)
	-- 	local role_config = GameSystem.Instance.RoleBaseConfigData:GetConfigData(id)	
	-- 	print("ixixixixiixixi----:",role_config)
	-- 	local positions ={'PF','SF','C','PG','SG'}
	-- 	role_position.spriteName = 'PT_'..positions[role_config.position]
	-- 	local level = getComponentInChild(player.transform,"Level","UILabel")
	-- 	level.text = self.simple_info[i].lev
	-- 	--星级处理
	-- 	--[[
	-- 	local starNum = self.simple_info[i].star
	-- 	local x = 5 - starNum
	-- 	if x > 0 then 
	-- 		for j = 1,x do
	-- 			NGUITools.SetActive(getChildGameObject(player.transform,"star"..j),false)
	-- 		end
	-- 	end	
	-- 	--]]
		
	-- 	--name
	-- 	local namelabel = getComponentInChild(player.transform,"labelName","UILabel")
	-- 	namelabel.text = role_config.name
	-- 	local starlabel = getComponentInChild(player.transform,"starName","UILabel")
	-- 	starlabel.text = "+"..self.simple_info[i].star
	-- 	--边框处理/资质
	-- 	local side = getComponentInChild(player.transform,"Side","UISprite")
	-- 	local apititude = getComponentInChild(player.transform,"aptitude","UISprite")
	-- 	if self.simple_info[i].quality == 1 then
	-- 		side.spriteName = "com_frame_green"
	-- 		apititude.spriteName = "QT_C"
	-- 	elseif self.simple_info[i].quality == 2 then
	-- 		side.spriteName = "com_frame_blue"
	-- 		apititude.spriteName = "QT_B"
	-- 	elseif self.simple_info[i].quality == 3 then
	-- 		side.spriteName = "com_frame_purple"
	-- 		apititude.spriteName = "QT_A"
	-- 	elseif self.simple_info[i].quality == 4 then
	-- 		side.spriteName = "com_frame_golden"
	-- 		apititude.spriteName = "QT_S"
	-- 	end
	-- end
end

function QualifyingPlayerPopup:MakeOnClose()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return QualifyingPlayerPopup
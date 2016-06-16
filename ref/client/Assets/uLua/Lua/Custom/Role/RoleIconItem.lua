RoleIconItem =  {
	uiName = 'RoleIconItem',
	-----------UI
	uiExp,
	uiExpProgress,
	uiExpLevel,
	uiExpText,
	uiExpNum,
	uiSide,
	uiIcon,
	uiPosition,
	uiNameLabel,
	uiStar,
	uiUpgrade,
	uiTip,
	uiLevelBg,
	uiBg,
	-----------parameters
	id = 0,
	npc = false,
	addExp = 0,
	roleExp, 
}

function RoleIconItem:Awake()
	-------Exp
	self.uiExp = self.transform:FindChild("Exp")
	self.uiExpProgress = self.uiExp:FindChild("Bgexp/Progress"):GetComponent('UISlider')
	self.uiExpLevel = self.uiExp:FindChild("Bgexp/Level"):GetComponent('UILabel')
	self.uiLevelBg = self.uiExp:FindChild("Bgexp/LevelBg"):GetComponent('UISprite')
	self.uiExpText = self.uiExp:FindChild("Text")
	self.uiExpNum = self.uiExp:FindChild("ExpNum"):GetComponent('UILabel')
	self.uiBg = self.transform:FindChild("Background"):GetComponent('UISprite')
	-------info
	self.uiSide = self.transform:FindChild("Info/Side"):GetComponent("UISprite")
	self.uiIcon = self.transform:FindChild("Info/Icon"):GetComponent("UISprite")
	self.uiIcon2 = self.transform:FindChild("Info/Icon2")
	self.uiPosition = self.transform:FindChild("Info/Position"):GetComponent("UISprite")
	self.uiNameLabel = self.transform:FindChild("Info/Name"):GetComponent("UILabel")
	self.uiStar = self.transform:FindChild("Info/Star"):GetComponent("UILabel")
	------------
	self.uiUpgrade = self.transform:FindChild("Upgrade"):GetComponent('UISprite')
	self.uiTip = self.transform:FindChild("Tip")
end

function RoleIconItem:Start()
	self:SetById( )
end

function RoleIconItem:SetById( )
	local id = self.id
	local positions ={'PF','SF','C','PG','SG'}
	if self.npc == true then
		NGUITools.SetActive(self.uiStar.gameObject,false)
		NGUITools.SetActive(self.uiExp.gameObject,false)
		NGUITools.SetActive(self.uiUpgrade.gameObject,false)
		print("NPCID:",id)
		local npcConfig = GameSystem.Instance.NPCConfigData:GetConfigData(id)
		local shap_id = GameSystem.Instance.NPCConfigData:GetShapeID(id)
		self.uiNameLabel.text = npcConfig.name
		self.uiIcon.atlas = getBustAtlas(shap_id)
		--self.uiIcon.spriteName = npcConfig.icon
		self.uiIcon.spriteName = "icon_bust_"..tostring( shap_id)
		if self.uiIcon2 then
			self.uiIcon2:GetComponent("UISprite").atlas = getBustAtlas(shap_id)
			self.uiIcon2:GetComponent("UISprite").spriteName = "icon_bust_"..tostring( shap_id)
		end
		self.uiPosition.spriteName = 'PT_'..positions[npcConfig.position]

		local quality = npcConfig.talent

		self.uiSide.spriteName = self["FrameTalent_" .. quality]
		self.uiLevelBg.spriteName = self["BgCornerTalent_" .. quality]
		self.uiBg.spriteName = self["BgTalent_" .. quality]
	else
		NGUITools.SetActive(self.uiStar.gameObject,false)
		NGUITools.SetActive(self.uiUpgrade.gameObject,false)
		local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
		self.uiIcon.spriteName = "icon_portrait_"..tostring(roleConfig.shape)
		self.uiIcon.atlas = getPortraitAtlas(roleConfig.shape)
		self.uiNameLabel.text = roleConfig.name
		self.uiPosition.spriteName = 'PT_'..positions[roleConfig.position]
		self.role = MainPlayer.Instance:GetRole2(id)
	
		self.startExp = self.role.exp
		for i = 1,self.role.level - 1 do
			self.startExp = self.startExp - GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		end
		if self.addExp and self.addExp >0 then
			self.role.exp = self.role.exp + self.addExp
		end
		self.roleExp = self.role.exp
		print(self.uiName,"-id:",id,"--exp:",self.roleExp)
		self.maxExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(self.role.level)
		self.uiExpLevel.text = tostring(self.role.level)		
		self.uiExpNum.text = '+' .. tostring(self.addExp)

		-- for i = 1, (self.role.level - 1) do
		-- 	self.roleExp = self.roleExp - GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
		-- end
		self.uiExpProgress.value = self.startExp / self.maxExp

		local quality = GameSystem.Instance.RoleBaseConfigData2:GetIntTalent(self.role.id)

		self.uiSide.spriteName = self["FrameTalent_" .. quality]
		self.uiLevelBg.spriteName = self["BgCornerTalent_" .. quality]
		self.uiBg.spriteName = self["BgTalent_" .. quality]
	end
end

function RoleIconItem:FixedUpdate( ... )
	if self.addExp > 0 then
		self.startExp = self.startExp + 2
		--如果升级
		if self.startExp >= self.maxExp then
			
			--因为球队等级限制队员等级
			local teamLimitLevel = GameSystem.Instance.TeamLevelConfigData:GetMaxRoleQuality(MainPlayer.Instance.Level)
			if self.role.level >= teamLimitLevel then
				NGUITools.SetActive(self.uiTip.gameObject, true)
				self.uiExpProgress.value = 0.99
				self.addExp = 0
				NGUITools.SetActive(self.uiExpText.gameObject, false)
				NGUITools.SetActive(self.uiExpNum.gameObject, false)
				local total = 0
				for i = 1, self.role.level do
					total = total + GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
				end
				self.role.exp = total
				return
			end

			MainPlayer.Instance:GetRole2(self.id).level = MainPlayer.Instance:GetRole2(self.id).level + 1
			-- MainPlayer.Instance:GetRole2(self.id).exp = 0
			self.startExp = 0 
			self.role = MainPlayer.Instance:GetRole2(self.id)
			self.maxExp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(self.role.level)
			NGUITools.SetActive(self.uiUpgrade.gameObject,true)
			self.uiExpLevel.text = self.role.level
			--self:Refresh(self.id)
			NGUITools.SetActive(self.uiExpText.gameObject, true)
			NGUITools.SetActive(self.uiExpNum.gameObject, true)
		else
			NGUITools.SetActive(self.uiExpText.gameObject, true)
			NGUITools.SetActive(self.uiExpNum.gameObject, true)
			-- self.uiExpProgress.value = self.roleExp / self.maxExp
		end
		self.uiExpProgress.value = self.startExp / self.maxExp
		--print(self.uiName,"--value:",self.uiExpProgress.value)
		self.addExp = self.addExp - 2
	elseif not self.npc then	--TODO 临时解决方案
		self.uiExpProgress.value = self.startExp / self.maxExp
	end
end

return RoleIconItem

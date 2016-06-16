------------------------------------------------------------------------
-- class name	: RoleLinkItem
-- create time   : 20150827_135550
------------------------------------------------------------------------


RoleLinkItem = {
	uiName = "RoleLinkItem",

	----------------------------
	currentChapterID = 0,
	isLock = true,

	--------------------------UI
	uiLabelChaper,
	uiLabelSectionName,
	uiLock,
	uiLabelNum,
	uiBtnGo,
	roleId = 0,
	exerciseId = 0,
}




function RoleLinkItem:Awake()
	self:UiParse()
end


function RoleLinkItem:Start()
	addOnClick(self.uiBtnGo.gameObject,self:Click())
	if self.currentChapterID == 0 then
		local index = 0
		local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
		while enum:MoveNext() do
			index = index + 1
			if MainPlayer.Instance:CheckChapter(enum.Current.Key) then
				self.currentChapterID = enum.Current.Key
			else
				break
			end
		end
	end

	self:Refresh()
end

function RoleLinkItem:Refresh()

	if self.chapterId and self.sectionId then
		local chapterId = tonumber(self.chapterId)
		local sectionId = tonumber(self.sectionId)
		local chapter_index = getCommonStr("STR_CH_"..tostring(chapterId%10000))
		local chapter_str = string.format(getCommonStr("CAREER_CHAPTER"),chapter_index)
		self.uiLabelChaper:SetText(chapter_str)
		local config = GameSystem.Instance.CareerConfigData:GetSectionData(sectionId)
		local sectionName = config.name

		self.section = MainPlayer.Instance:GetSection(chapterId, sectionId)
		if self.currentChapterID >= chapterId and self.section then
			self.uiLock.gameObject:SetActive(false)
			self.uiBtnGo.gameObject:SetActive(true)
			self.isLock = false
			local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(sectionId)
			local limitTime = sectionConfig.daily_times > 0
			if limitTime then
				self.uiLabelNum:SetText( tostring(sectionConfig.daily_times - self.section.challenge_times).."/"..tostring(sectionConfig.daily_times))
			end
			NGUITools.SetActive(self.uiLabelNum.gameObject, limitTime)
		else
			self.uiLock.gameObject:SetActive(true)
			self.uiBtnGo.gameObject:SetActive(false)
			self.isLock = true
			sectionName = sectionName .. getCommonStr("PLAYER_PIECEADD_SECTION_IS_UNLOCKED")
			NGUITools.SetActive(self.uiLabelNum.gameObject, false)
		end
		self.uiLabelSectionName:SetText(sectionName)
	else
		self.uiBtnGo.gameObject:SetActive(false)
		self.isLock = false
		self.uiLabelNum.gameObject:SetActive(false)
		self.uiLabelSectionName.gameObject:SetActive(false)
		self.uiLabelChaper:SetText(self.chapterId)

		-- self.uiLabelSectionName:SetText(self.chapterId)
		-- self.uiLabelChaper.gameObject:SetActive(false)

	end

end


-- parse the prefeb
function RoleLinkItem:UiParse()
	self.uiLabelChaper = self.transform:FindChild("LabelChapter"):GetComponent("MultiLabel")
	self.uiLabelSectionName = self.transform:FindChild("LabelSectionName"):GetComponent("MultiLabel")
	self.uiLock = self.transform:FindChild("Lock"):GetComponent("UISprite")
	self.uiLabelNum = self.transform:FindChild('LabelNum'):GetComponent('MultiLabel')
	self.uiBtnGo = self.transform:FindChild('ButtonGo')
end

function RoleLinkItem:Click()
	return function()
		if not self.isLock then
			-- TopPanelManager:ShowPanel("UICareer", nil, {sectionID = self.sectionId , chapterID = tonumber(self.chapterId)})
			jumpToUI("UICareer",nil,{sectionID = self.sectionId , chapterID = tonumber(self.chapterId)})
			MainPlayer.Instance.LinkRoleId = self.roleId
			MainPlayer.Instance.LinkExerciseId = self.exerciseId
		end
	end
end


function RoleLinkItem:SetData(chapterId,sectionId)
	self.chapterId = chapterId
	self.sectionId = sectionId
end


function RoleLinkItem:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


return RoleLinkItem

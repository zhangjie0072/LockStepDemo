------------------------------------------------------------------------
-- class name    : RoleLinkItem2
-- create time   : Tue Oct 27 22:16:33 2015
------------------------------------------------------------------------
require "common/StringUtil"

RoleLinkItem2 =  {
	uiName     = "RoleLinkItem2",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiBackground       = nil,
	uiLabelChapter     = nil,
	uiLabelSectionName = nil,
	uiLock             = nil,
	uiLabelNum         = nil,
	uiBackShader       = nil,

	-----------------------
	-- Parameters Module --
	-----------------------
	accessType,
	chapterId,					--
	sectionId,					--
	bullHard,
	isLock,						-- display lock, and do not jump to carrer.
	currentChapterID = 0,
	roleId,
	exerciseId,
	linkTab,
	linkUi,
	parent,
}


---------------------------------------------------------------
-- below functions are system function callback from system. --
---------------------------------------------------------------
function RoleLinkItem2:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function RoleLinkItem2:Start()
	------------------------------------
	-------  Add Click Start
	addOnClick(self.uiBackground.gameObject, self:Click())
	------------------------------------
	-------  Add Click End
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

function RoleLinkItem2:Refresh()
	if self.accessType == 1 then
		local chapterId = tonumber(self.chapterId)
		local sectionId = tonumber(self.sectionId)
		local chapter_index = getCommonStr("STR_CH_"..tostring(chapterId%10000))
		local chapter_str = string.format(getCommonStr("CAREER_CHAPTER"),chapter_index)
		self.uiLabelChapter:SetText(chapter_str)
		local config = GameSystem.Instance.CareerConfigData:GetSectionData(sectionId)
		local sectionName = config.name
		self.uiLabelSectionName:SetText(sectionName)
		local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(sectionId)
		local section = MainPlayer.Instance:GetSection(chapterId, sectionId)
		if self.currentChapterID >= chapterId and section then
			self.uiLock.gameObject:SetActive(false)
			self.isLock = false
			local limitTime = sectionConfig.daily_times > 0
			if limitTime then
				self.uiLabelNum:SetText( tostring(sectionConfig.daily_times - section.challenge_times).."/"..tostring(sectionConfig.daily_times))
			end
			NGUITools.SetActive(self.uiLabelNum.gameObject, limitTime)
		else
			self.uiLock.gameObject:SetActive(true)
			self.isLock = true
			sectionName = sectionName .. getCommonStr("PLAYER_PIECEADD_SECTION_IS_UNLOCKED")
			NGUITools.SetActive(self.uiLabelNum.gameObject, false)
		end
		print("#### sectionConfig.icon_level=",sectionConfig.icon_level)

		local icon = Split(sectionConfig.icon_level, "_")
		local at
		if icon[2] == "bg1" then
			at = "Atlas/career/career"
		elseif icon[2] == "bg2" then
			at = "Atlas/career/career2"
		end
		self.uiBackBg.atlas = ResourceLoadManager.Instance:GetAtlas(at)
		self.uiBackground.normalSprite = sectionConfig.icon_level
	elseif self.accessType == 2 then -- bull fight.
		local fightLv =  GameSystem.Instance.bullFightConfig:GetFightLevel(self.bullHard)
		self.isLock = MainPlayer.Instance.Level < fightLv.unlock_level
		self.uiLock.gameObject:SetActive(self.isLock)
		self.uiBackBg.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/career/career")
		self.uiBackground.normalSprite = "career_bg1_beach" -- fixed background.
		self.uiLabelChapter:SetText(getCommonStr("BULL_FIGHT"))
		self.uiLabelSectionName:SetText(getCommonStr("STR_DIFFICULTY")..self.bullHard)

		local allowPlayTimes = GameSystem.Instance.CommonConfig:GetUInt("gBullFightTimes") + MainPlayer.Instance.BullFight.bullfight_buy_times
		local playedTimes = MainPlayer.Instance.BullFight.times

		if allowPlayTimes - playedTimes > 0 then
			self.uiLabelNum:SetText(tostring(allowPlayTimes - playedTimes ).. "/" .. tostring(GameSystem.Instance.CommonConfig:GetUInt("gBullFightTimes")))
		end
	end
end

-- uncommoent if needed
-- function RoleLinkItem2:FixedUpdate()

-- end


function RoleLinkItem2:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleLinkItem2:UiParse()
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiBackground       = find("Background"):GetComponent("UIButton")
	self.uiBackBg		= find("Background"):GetComponent("UISprite")
	self.uiLabelChapter     = find("LabelChapter"):GetComponent("MultiLabel")
	self.uiLabelSectionName = find("LabelSectionName"):GetComponent("MultiLabel")
	self.uiLock             = find("Lock"):GetComponent("UISprite")
	self.uiLabelNum         = find("LabelNum"):GetComponent("MultiLabel")
	self.uiBackShade        = find("BackShade")
end


function RoleLinkItem2:SetData(accessType, chapterId, sectionId)
	self.accessType = accessType
	if accessType == 1 then
		self.chapterId  = chapterId
		self.sectionId  = sectionId
	elseif accessType == 2 then
		self.bullHard = chapterId
	end
end



------------------------------------
-------  Click Function Start
function RoleLinkItem2:Click()
	return function()
		print("RoleLinkItem2 self.isLock=",self.isLock)
		if not self.isLock then
			-- handle jump action.
			if self.accessType == 1 then
				-- jumpToUI("UICareer",nil,{sectionID = self.sectionId , chapterID = tonumber(self.chapterId)})
				print(self.uiName,"--------showPanel uicareer")
				TopPanelManager:ShowPanel('UICareer',
						  tonumber(self.chapterId) ,
						  {
							  sectionID = self.sectionId ,
							  --chapterID = tonumber(self.chapterId),
							  jumpFromRole  = true,
						  })
				MainPlayer.Instance.LinkUiName = self.linkUi
				MainPlayer.Instance.LinkTab = self.linkTab
				MainPlayer.Instance.LinkRoleId = self.roleId
				MainPlayer.Instance.LinkExerciseId = self.exerciseId
				MainPlayer.Instance.LinkEnable = true
				if self.parent then
					NGUITools.Destroy(self.parent.gameObject)
				end
			elseif self.accessType == 2 then
				if not validateFunc("BullFight") then
					return
				end

				local GetBullFightNpcReq = {
					acc_id = MainPlayer.Instance.AccountID
				}

				local req = protobuf.encode("fogs.proto.msg.GetBullFightNpcReq",GetBullFightNpcReq)
				CommonFunction.ShowWait()
				LuaHelper.SendPlatMsgFromLua(MsgID.GetBullFightNpcReqID,req)
				LuaHelper.RegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID, self:HandleGetNPC(), self.uiName)
				CommonFunction.ShowWaitMask()

			end
		else
			CommonFunction.ShowTip(getCommonStr("LINK_LOCKED"), nil)
		end
	end
end



function RoleLinkItem2:HandleGetNPC()
	return function(buf)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetBullFightNpcRespID,  self.uiName)
		local resp, err = protobuf.decode("fogs.proto.msg.GetBullFightNpcResp", buf)
		print("resp.result=",resp.result)
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance.BullFightNpc:Clear()
				for k, v in ipairs(resp.npc) do
					MainPlayer.Instance.BullFightNpc:Add(v)
				end
				MainPlayer.Instance.LinkUiName = self.linkUi
				MainPlayer.Instance.LinkTab = self.linkTab
				MainPlayer.Instance.LinkRoleId = self.roleId
				MainPlayer.Instance.LinkExerciseId = self.exerciseId
				MainPlayer.Instance.LinkEnable = true
				MainPlayer.Instance.IsLastShootGame = false

				-- jumpToUI("UIBullFight",nil,{preSelected = tonumber(self.bullHard)})
				TopPanelManager:ShowPanel('UIBullFight',
						  nil ,
						  {
							  preSelected = tonumber(self.bullHard),
						  })
				NGUITools.Destroy(self.parent.gameObject)
				-- self.nextShowUI = "UIBullFight"
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
			end
		else
			error("UICompetition:HandleGetNPC -handler", err)
		end
	end
end

return RoleLinkItem2

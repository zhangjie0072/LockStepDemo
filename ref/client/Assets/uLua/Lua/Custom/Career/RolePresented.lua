RolePresented = {
	uiName = 'RolePresented',

	-----------------------parameters
	id = nil,

	-----------------------UI
	uiIcon,
	uiName,
	uiDeviation,
	uiProfession,
	uiButtonOK,
	uiButtonCancel,
	
}

function RolePresented:Awake()
	self.uiIcon = self.transform:FindChild("Window/Icon"):GetComponent("UISprite")
	self.uiName = self.transform:FindChild("Window/Name"):GetComponent("UILabel")
	self.uiDeviation = self.transform:FindChild("Window/Deviation"):GetComponent("UILabel")
	self.uiProfession = self.transform:FindChild("Window/Profession"):GetComponent("UILabel")
	self.uiButtonOK = self.transform:FindChild("Window/ButtonOK")
	self.uiButtonCancel = self.transform:FindChild("Window/ButtonCancel")
	addOnClick(self.uiButtonOK.gameObject, self:OnClickOK())
	addOnClick(self.uiButtonCancel.gameObject, self:OnClickCancel())
end

function RolePresented:Start()
	local positions ={'PF','SF','C','PG','SG'}
	local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	self.uiIcon.spriteName = "icon_bust_"..tostring(roleConfig.shape)
	self.uiIcon.atlas = getBustAtlas(roleConfig.shape)
	self.uiName.text = roleConfig.name
	self.uiDeviation.text = roleConfig.goodAt
	self.uiProfession.text = getCommonStr(positions[roleConfig.position])
end

function RolePresented:OnClickOK()
	return function(go)
		local req = {
			role_id = self.id,
			flag = 1,
			chapter = CurChapterID,
			section_id = CurSectionID,
		}
		local buf = protobuf.encode("fogs.proto.msg.InviteRoleReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.InviteRoleReqID, buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.InviteRoleRespID, self:GetRole(), self.uiName)
		CommonFunction.ShowWait()
	end
end

function RolePresented:OnClickCancel()
	return function(go)
		NGUITools.Destroy(self.gameObject)
	end
end

function RolePresented:GetRole()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.InviteRoleResp", buf)
		CommonFunction.StopWait()
		if resp then
			LuaHelper.UnRegisterPlatMsgHandler(MsgID.InviteRoleRespID, self.uiName)
			if resp.result == 0 then
				MainPlayer.Instance:SetGetRole(CurChapterID, CurSectionID)
				local RoleInfo = RoleInfo.New()
				RoleInfo.id = resp.role.id
				RoleInfo.exp = resp.role.exp
				RoleInfo.quality = resp.role.quality
				RoleInfo.star = resp.role.star
				-- local SkillSlotProtoList = SkillSlotProtoList.New()
				for k,v in pairs(resp.role.skill_slot_info) do
					local SkillSlotProto = SkillSlotProto.New()
					SkillSlotProto.id = v.id
					SkillSlotProto.is_unlock = v.is_unlock
					SkillSlotProto.skill_uuid = v.skill_uuid
					RoleInfo.skill_slot_info:Add(SkillSlotProto) 
				end
				-- RoleInfo.skill_slot_info = SkillSlotProtoList
				for k,v in pairs(resp.role.exercise) do
					local ExerciseInfo = ExerciseInfo.New()
					ExerciseInfo.id = v.id
					ExerciseInfo.star = v.star
					ExerciseInfo.quality = v.quality
					RoleInfo.exercise:Add(ExerciseInfo) 
				end
				-- RoleInfo.exercise = ExerciseInfoList
				for k,v in pairs(resp.role.fashion_slot_info) do
					local FashionSlotProto = FashionSlotProto.New()
					FashionSlotProto.id = v.id
					FashionSlotProto.fashion_uuid = v.fashion_uuid
					RoleInfo.fashion_slot_info:Add(FashionSlotProto) 
				end
				-- RoleInfo.fashion_slot_info = FashionSlotProtoList

				--MainPlayer.Instance:AddInviteRoleInList(RoleInfo)
				self:OnClickCancel()()
				local parent = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("UIRolePresented(Clone)").gameObject
				NGUITools.Destroy(parent)
				local card = getLuaComponent(createUI("RoleAcquirePopup"))
				card.id = resp.role.id
				card.IsInClude = MainPlayer.Instance:HasRole(resp.role.id)
				local goods = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(resp.role.id)
				local goodsID = goods.recruit_output_id
				local goodsNum = goods.recruit_output_value
				local goodsName = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsID).name
				local conStr = string.format(getCommonStr('STR_ROLE_RECTUIT_AWARDS_FORLUA'),goodsName,goodsNum)
				--print("***************content str****************"..conStr)
				card.contentStr = conStr
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
		else
			error("UICareerSection:", err)
		end
	end
end

return RolePresented
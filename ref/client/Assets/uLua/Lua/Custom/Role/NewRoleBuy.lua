

NewRoleBuy =  {
	uiName	= "NewRoleBuy",

	--data
	id,
	roleData,
	onCloseClick,
	uiBackBtn,
	onBuyNewPlayer,

	--ui
	uiButtonClose,
	uiBtn_1,
	uiBtn_2,
	uiConsumeGrid, --用于动态控制界面排序
	uiButtonGrid,  --用于动态控制界面排序
	uiGoodsIconConsume_1,
	uiGoodsIconConsume_2,

	uiHeadIcon,
	uiCareerRoleIcon,
}

function NewRoleBuy:Awake()
	self.uiBackBtn 		= getChildGameObject(self.transform, "Window/ButtonClose")
	self.uiBtn_1 		= getChildGameObject(self.transform, "Window/Button/Btn1")
	self.uiBtn_2	 	= getChildGameObject(self.transform, "Window/Button/Btn2")
	self.uiConsumeGrid  = getComponentInChild(self.transform, "Window/Consume", "UIGrid")
	self.uiButtonGrid  	= getComponentInChild(self.transform, "Window/Button", "UIGrid")
	self.uiHeadIcon 	= getChildGameObject(self.transform, "Window/Icon")

	self.uiGoodsIconConsume_1 = createUI("GoodsIconConsume", self.uiConsumeGrid.transform)
	local script_1 = getLuaComponent(self.uiGoodsIconConsume_1)
	script_1.isAdd = false
	script_1.isBG = false

	self.uiGoodsIconConsume_2 = createUI("GoodsIconConsume", self.uiConsumeGrid.transform)
	local script_2 = getLuaComponent(self.uiGoodsIconConsume_2)
	script_2.isAdd = false
	script_2.isBG = false
end

function NewRoleBuy:Start()
	local uiBtnClose = createUI('ButtonClose', self.uiBackBtn.transform)
	local closeBtn = getLuaComponent(uiBtnClose)
	closeBtn.onClick = self:OnCloseClick()

	addOnClick(self.uiBtn_1, self:OnBuy())
	addOnClick(self.uiBtn_2, self:OnBuy())

	self.transform.localPosition = Vector3.New(self.transform.localPosition.x,self.transform.localPosition.y,-700)

	self:Refresh()
end

function NewRoleBuy:Refresh()
	--设置头像
	if not self.uiCareerRoleIcon then
		self.uiCareerRoleIcon = createUI('NewRoleBustItem1', self.uiHeadIcon.transform)
	end
	local role = getLuaComponent(self.uiCareerRoleIcon)
	role.id = self.roleData.id
	role.isHas = true
	role.transform.name = tostring(self.roleData.id)

	self:RefreshConsume()
	self.uiConsumeGrid:Reposition()
	self.uiButtonGrid:Reposition()

	print("-----------------------------------")
end

function NewRoleBuy:OnCloseClick()
	return function (go)
		self:DoClose()
	end
end

function NewRoleBuy:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function NewRoleBuy:OnBuy()
	return function (go)
		print( "OnBuy=>".. go.name )

		if not FunctionSwitchData.CheckSwith(FSID.players_btn) then return end

		local array = string.split(go.name, '_')
		self:SendBuyMessage(tonumber(array[1]), tonumber(array[2]))
	end
end

function NewRoleBuy.ChangeButtonText(ctr, ConsumeType)
	local str = ""
	if ConsumeType == 1 then
		str = getCommonStr("STR_BUY_DIAMONDS")
	elseif ConsumeType == 2 then
		str = getCommonStr("STR_BUY_GOLD")
	else
		str = getCommonStr("BUY_CONVERT")
	end

	local label = getComponentInChild(ctr.transform, "Label", "UILabel")
	label.text = str

	local shadow = getComponentInChild(ctr.transform, "Label/Shadow", "UILabel")
	shadow.text = str
end

function NewRoleBuy:RefreshConsume()
	NGUITools.SetActive(self.uiGoodsIconConsume_1, false)
	NGUITools.SetActive(self.uiBtn_1, false)

	NGUITools.SetActive(self.uiGoodsIconConsume_2, false)
	NGUITools.SetActive(self.uiBtn_2, false)

	--购买开销
	local consume = self.roleData.recruit_consume
	local cur_enum = consume:GetEnumerator()

	local index = 0
	while cur_enum:MoveNext() do
		local key = cur_enum.Current.Key
		local value = cur_enum.Current.Value

		index = index + 1
		if index == 1 then
			if key == 1 then
				NGUITools.SetActive(self.uiGoodsIconConsume_2, true)
				NGUITools.SetActive(self.uiBtn_2, true)
				self.uiBtn_2.name = string.format("%d_%d", key, value)
				NewRoleBuy.ChangeButtonText(self.uiBtn_2, key)

				local script_2 = getLuaComponent(self.uiGoodsIconConsume_2)
				script_2:SetData(key, value)
			else
				NGUITools.SetActive(self.uiGoodsIconConsume_1, true)
				NGUITools.SetActive(self.uiBtn_1, true)
				self.uiBtn_1.name = string.format("%d_%d", key, value)
				NewRoleBuy.ChangeButtonText( self.uiBtn_1, key)

				local script_1 = getLuaComponent(self.uiGoodsIconConsume_1)
				script_1:SetData(key, value)
				if key ~= 2 then
					self.uiConsumeGrid.transform.localPosition = Vector3.New(self.uiConsumeGrid.transform.localPosition.x + 12, self.uiConsumeGrid.transform.localPosition.y, self.uiConsumeGrid.transform.localPosition.z)
				end
			end
		elseif index == 2 then
			NGUITools.SetActive(self.uiGoodsIconConsume_2, true)
			NGUITools.SetActive(self.uiBtn_2, true)
			self.uiBtn_2.name = string.format("%d_%d", key, value)
			NewRoleBuy.ChangeButtonText(self.uiBtn_2, key)

			local script_2 = getLuaComponent(self.uiGoodsIconConsume_2)
			script_2:SetData(key, value)
		end
	end
end

function NewRoleBuy:SendBuyMessage(ConsumeType, num)
	print(ConsumeType)
	print(num)
	local rst = Consume.CheckConsume(ConsumeType, num)
	print(rst)
	if not rst then
		return
	end

	local req = {
			role_id = self.id,
			flag = ConsumeType
		}
	local buf = protobuf.encode("fogs.proto.msg.InviteRoleReq", req)
	CommonFunction.ShowWait()
	LuaHelper.SendPlatMsgFromLua(MsgID.InviteRoleReqID, buf)
	LuaHelper.RegisterPlatMsgHandler(MsgID.InviteRoleRespID, self:GetRole(), self.uiName)
end

function NewRoleBuy:GetRole()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.InviteRoleResp", buf)
		CommonFunction.StopWait()
		if resp then
			LuaHelper.UnRegisterPlatMsgHandler(MsgID.InviteRoleRespID, self.uiName)
			if resp.result == 0 then
				--MainPlayer.Instance:SetGetRole(CurChapterID, CurSectionID)
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

				MainPlayer.Instance:AddInviteRoleInList(RoleInfo)

				-- self:OnClose()
				self:OnBuyNewPlayer()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
				return
			end
		else
			error("UICareerSection:", err)
		end
	end
end




function NewRoleBuy:Update()

end

function NewRoleBuy:FixedUpdate()

end

function NewRoleBuy:OnDestroy()

end

function NewRoleBuy:OnClose()
	if self.onCloseClick then
		self.onCloseClick()
	end

	NGUITools.Destroy(self.gameObject)
end

function NewRoleBuy:OnBuyNewPlayer( ... )
	if self.onBuyNewPlayer then
		self.onBuyNewPlayer()
	end

	NGUITools.Destroy(self.gameObject)
end

return NewRoleBuy
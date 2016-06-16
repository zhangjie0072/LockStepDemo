-- 20150618_202617



MemberUpgradePopup =  {
	uiName = 'MemberUpgradePopup',

	go = {},
}




function MemberUpgradePopup:Awake()
	self.go = {}
	local goFrame = getChildGameObject(self.transform, "PopupFrame")
	goFrame = WidgetPlaceholder.Replace(goFrame)
	self.popup_frame = getLuaComponent(goFrame)
	self.popup_frame.onClose = self:click_close()
	local popup_frame_position = goFrame.transform.localPosition
	-- self.go.close = self.transform:FindChild('Close').gameObject
	-- addOnClick(self.go.close,self:click_close())


	self.go.talent_node = self.transform:FindChild('Talent/TalentItemsNode')   
	local t = createUI('TalentItems',self.go.talent_node)
	self.talent_items = getLuaComponent(t)

	self.go.upgrade_btn = self.transform:FindChild('Upgrade').gameObject
	addOnClick(self.go.upgrade_btn,self:click_upgrade())


	self.go.property_grid = self.transform:FindChild('Property/scroll/grid'):GetComponent('UIGrid')

	self.go.cost_icon = self.transform:FindChild('Cost/Consume/Icon'):GetComponent('UISprite')
	self.go.cost_label = self.transform:FindChild('Cost/Consume/Num'):GetComponent('UILabel')
	self.go.gold_cost_icon = self.transform:FindChild('Cost/GoldConsume/Icon'):GetComponent('UISprite')
	self.go.gold_cost_label = self.transform:FindChild('Cost/GoldConsume/Num'):GetComponent('UILabel')


	self.go.skill_grid = self.transform:FindChild('MemberSkill/Grid'):GetComponent('UIGrid')
	self.go.grade_from = self.transform:FindChild("Grade/From"):GetComponent("UILabel")
	
	self.go.grade_to = self.transform:FindChild("Grade/To"):GetComponent("UILabel")


	-- STR_MEMBER_UPGRADE_TO
	self.go.title = self.transform:FindChild('Title'):GetComponent('UILabel')
	
	addOnClick(self.transform:FindChild("Cost/Get").gameObject,self:click_get())
end


function MemberUpgradePopup:click_get()
	return function()
		print('MemberOpenPopup:click_get() is called')
		
		local t = createUI('PieceLink',self.transform)
		self.piece_link = getLuaComponent(t)
		self.piece_link:set_member_item(self.member_item)
		NGUITools.BringForward( t)

	end
end



function MemberUpgradePopup:close()
	if self.close_dlg then
		self.close_dlg()
	end
	
	print("self=="..tostring(self))
	print("self.transform.name="..tostring(self.transform.name))
	print("self.gameObject="..tostring(self.gameObject))

	LuaHelper.UnRegisterPlatMsgHandler(MsgID.ImproveQualityRespID,self.uiName)

	NGUITools.Destroy(self.gameObject)
end

function MemberUpgradePopup:click_close()
	return function()
		print("MemberUpgradePopup click_close()")
		self:close()
	end
end


function MemberUpgradePopup:set_member_item(item)
	self.member_item = item
end


function MemberUpgradePopup:click_upgrade()
	return function()
		print('MemberUpgradePopUp:click_upgrade() is called')
		if self.need_num > self.cur_num then
			CommonFunction.ShowTip(getCommonStr('STR_CANNOT_UPGRADE_MEMBER_FOR_NOT_ENOUGH_PIECE'))
			return
		end
		
		print('member_id='..tostring(self.member_item.id))
		
		local operation = {
			role_id = self.member_item.id
		}

		local req = protobuf.encode('fogs.proto.msg.ImproveQualityReq',operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.ImproveQualityReqID,req)
		CommonFunction.ShowWait()

	end
end

function MemberUpgradePopup:Start()
	local v = self.transform.localPosition
	v.z = -500
	self.transform.localPosition = v




	LuaHelper.RegisterPlatMsgHandler(MsgID.ImproveQualityRespID, self:s_handle_improve_quality_resp(), self.uiName)

	if self.member_item.quality >= 15 then
		-- todo check the max
		CommonFunction.ShowTip('You are already get max grade!!!')
		return
	end


	 title
	local quality_strs = {'D-','D','D+','C-','C','C+','B-','B','B+','A-','A','A+','S-','S','S+'}
	self.go.title.text = getCommonStr('STR_MEMBER_UPGRADE')
-- ..quality_strs[self.member_item.quality+1]
	self.go.grade_from.text = quality_strs[self.member_item.quality]
	self.go.grade_to.text = quality_strs[self.member_item.quality+1]

	-- local cur = self.member_item.attr_data

	
	local cur = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.member_item.id,self.member_item.quality)
	
	local next  = GameSystem.Instance.AttrDataConfigData:GetRoleAttrData(self.member_item.id,self.member_item.quality+1)
	
	local cur_talent = cur.talent
	local next_talent = next.talent

	-- if cur_talent == next_talent then
	--	self.talent_items:set_mode(1)
	-- else
	--	self.talent_items:set_mode(2)
	-- end
	self.talent_items:set_mode(0)

	self.talent_items:set_talent(cur_talent)



	-- property
	local cur_attrs = cur.attrs
	local next_attrs = next.attrs

	local enum_cur = cur_attrs:GetEnumerator()

	local enum_next = next_attrs:GetEnumerator()

	CommonFunction.ClearGridChild(self.go.property_grid.transform)

	while enum_cur:MoveNext() do
		local cur_key = enum_cur.Current.Key
		local cur_value = enum_cur.Current.Value
		local next_value = next_attrs[cur_key]
		
		if cur_value ~= next_value then
			local g= createUI('AttrUpgradeListItem',self.go.property_grid.transform)
			local script = getLuaComponent(g)
			
			script.attrSymbol = cur_key
			script.prevValue = cur_value
			script.curValue = next_value
			script.showPlus = false
		end
	end

	self.go.property_grid:Reposition()



	local cur_RQ_data = GameSystem.Instance.RoleQualityConfigData:GetRoleQualityData(self.member_item.id,self.member_item.quality)
	local next_RQ_data = GameSystem.Instance.RoleQualityConfigData:GetRoleQualityData(self.member_item.id,self.member_item.quality+1)
	

	local enum_piece = next_RQ_data.piece_id:GetEnumerator()
	enum_piece:MoveNext()
	local piece_id = enum_piece.Current 


	enum_piece = next_RQ_data.piece_num:GetEnumerator()
	enum_piece:MoveNext()
	local piece_num = enum_piece.Current
	enum_piece:MoveNext()
	local piece_num1 = enum_piece.Current

	self.go.cost_icon.spriteName = 'icon_piece_'..tostring(piece_id)
	
	local owned_piece = MainPlayer.Instance:GetGoodsCount(piece_id);
	self.go.cost_label.text = tostring(owned_piece)..'/'.. tostring(piece_num)
	self.go.gold_cost_label.text = tostring(piece_num1)

	self.cur_num = owned_piece
	self.need_num= piece_num

	-- skill change

	
	local cur_skills = cur_RQ_data.skills
	local next_skills = next_RQ_data.skills

	local cur_enum_skill = cur_skills:GetEnumerator()
	local next_enum_skill = next_skills:GetEnumerator()
	
	CommonFunction.ClearGridChild(self.go.skill_grid.transform)
	while next_enum_skill:MoveNext() do
		local next_skill_id = next_enum_skill.Current.Key
		local next_skill_level = next_enum_skill.Current.Value

		local is_new = not cur_skills:ContainsKey(next_skill_id)

		if is_new then
			local t = createUI('MemberSkillChangeItem',self.go.skill_grid.transform)
			local script = getLuaComponent(t)
			script.id = next_skill_id
			script.label = getCommonStr('STR_NEW_SKILL')
		else 
			local cur_skill_level = cur_skills[next_skill_id]
			if next_skill_level ~= cur_skill_level then
				local t = createUI('MemberSkillChangeItem',self.go.skill_grid.transform)
				local script = getLuaComponent(t)
				script.id = next_skill_id
				local str = getCommonStr('STR_UPGRADE_TO')
				str = string.format(str,next_skill_level)
				script.label = str
			end
			
		end
	end
	self.go.skill_grid:Reposition()
end


function MemberUpgradePopup:s_handle_improve_quality_resp()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.ImproveQualityResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				local role = MainPlayer.Instance:GetRole(resp.role_id)
				if role ~= nil then
					role.m_roleInfo.quality = resp.new_quality
					self.member_item:refresh()
					self:close()
					CommonFunction.ShowTip(getCommonStr("STR_UPGRADE_MEMBER_SUCCESS"))
					playSound("UI/UI_level_up_01")
				else
					error("cannot find for role id="..tostring(resp.role_id))
				end
			else
				-- error("Member Upgrade failed for"..tostring(ErrorID.IntToEnum(resp.result)))
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_invite_role(): " .. err)
		end
	end
end

return MemberUpgradePopup

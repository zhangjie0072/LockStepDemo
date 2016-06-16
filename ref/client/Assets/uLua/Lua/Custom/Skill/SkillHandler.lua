local function HandleUpgrade(resp)
	local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, resp.skill_uuid)
	goods:SetLevel(resp.skill_level)
end

local function HandleEquip(resp)
	local enum = MainPlayer.Instance.Captain.m_roleInfo.skill_slot_info:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.id == resp.slot_id then
			if enum.Current.skill_uuid ~= 0 then
				local oldGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, enum.Current.skill_uuid)
				oldGoods:Unequip()
			end
			enum.Current.skill_uuid = resp.skill_uuid
			local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, resp.skill_uuid)
			goods:Equip()
			break
		end
	end
end

local function HandleUnequip(resp)
	local enum = MainPlayer.Instance.Captain.m_roleInfo.skill_slot_info:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.id == resp.slot_id then
			if enum.Current.skill_uuid ~= 0 then
				local oldGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_SKILL, enum.Current.skill_uuid)
				oldGoods:Unequip()
			end
			enum.Current.skill_uuid = 0
			break
		end
	end
end

local function HandleUnlock(resp)
	local enum = MainPlayer.Instance.Captain.m_roleInfo.skill_slot_info:GetEnumerator()
	while enum:MoveNext() do
		if enum.Current.id == resp.slot_id then
			enum.Current.is_unlock = 1
			break
		end
	end
end

local function OperationHandler(buf)
	local resp, err = protobuf.decode("fogs.proto.msg.SkillOperationResp", buf)
	if resp then
		if resp.result == 0 then
			if resp.type == "SOT_UPGRADE" then
				HandleUpgrade(resp)
			elseif resp.type == "SOT_EQUIP" then
				HandleEquip(resp)
			elseif resp.type == "SOT_UNEQUIP" then
				HandleUnequip(resp)
			elseif resp.type == "SOT_UNLOCK_SLOT" then
				HandleUnlock(resp)
			end
		end
	else
		error("SkillHandler: " .. err)
	end
end

SkillHandler =
{
	uiName = 'SkillHandler',
}
function SkillHandler.Register()
	LuaHelper.RegisterPlatMsgHandler(MsgID.SkillOperationRespID, OperationHandler, SkillHandler.uiName)
end

SkillHandler.Register()

local function HandleEquip(resp)
	-- local player = MainPlayer.Instance:GetRole(resp.role_id)
	-- if not player then error("TattooHandler: equip, role ID error: ", resp.role_id) end
	
	-- local enum = player.m_roleInfo.tattoo_slot_info:GetEnumerator()
	-- while enum:MoveNext() do
	-- 	if enum.Current.id == resp.slot_id then
	-- 		if enum.Current.tattoo_uuid ~= 0 then
	-- 			local oldGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, enum.Current.tattoo_uuid)
	-- 			oldGoods:Unequip()
	-- 		end
	-- 		enum.Current.tattoo_uuid = resp.uuid
	-- 		local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, resp.uuid)
	-- 		goods:Equip()
	-- 		print("TattooHandler:", "Equiped, ID:", goods:GetID())
	-- 		break
	-- 	end
	-- end
end

local function HandleUnequip(resp)
	-- local player = MainPlayer.Instance:GetRole(resp.role_id)
	-- if not player then error("TattooHandler: unequip, role ID error: ", resp.role_id) end

	-- local enum = player.m_roleInfo.tattoo_slot_info:GetEnumerator()
	-- while enum:MoveNext() do
	-- 	if enum.Current.id == resp.slot_id then
	-- 		if enum.Current.tattoo_uuid ~= 0 then
	-- 			local oldGoods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_TATTOO, enum.Current.tattoo_uuid)
	-- 			oldGoods:Unequip()
	-- 		end
	-- 		enum.Current.tattoo_uuid = 0
	-- 		break
	-- 	end
	-- end
end

local function OperationHandler(buf)
	local resp, err = protobuf.decode("fogs.proto.msg.TattooOperationResp", buf)
	if resp then
		if resp.result == 0 then
			if resp.type == "TOT_EQUIP" then
				HandleEquip(resp)
			elseif resp.type == "TOT_UNEQUIP" then
				HandleUnequip(resp)
			end
		end
	else
		error("TattooHandler: " .. err)
	end
end

TattooHandler = 
{
	uiName = 'TattooHandler',
}

function TattooHandler.Register()
	LuaHelper.RegisterPlatMsgHandler(MsgID.TattooOperationRespID, OperationHandler, TattooHandler.uiName)
end

TattooHandler.Register()

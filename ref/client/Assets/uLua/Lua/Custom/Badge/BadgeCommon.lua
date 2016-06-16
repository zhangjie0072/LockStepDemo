local modeName = "BadgeCommon"
BadgeCategroy= 
{
	CategroyOfRed		= "1",
	CategroyOfBlue 		= "2",
	CategroyOfGreen 	= "3",
	CategroyOfGolden 	= "4",
}

BadgeSlotInfoUpDateCB 	= nil
BadgeBookNameUpdateCB 	= nil
IsGotoLotteryFromBadgeStorePanel		= false
NeedPlayUnlockEffectSlots = nil


function AddEvent()
	LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeSlotInfoUpdateID, BadgeSlotInfoUpdateHanlder, modeName)
end

function GetBadgeTypeStringByCategory(gc)
	local typeString = ""
	if gc == BadgeCategroy.CategroyOfRed then
		 typeString = CommonFunction.GetConstString("STR_BADGE_ATTACK")
	elseif gc  == BadgeCategroy.CategroyOfBlue then
		 typeString = CommonFunction.GetConstString("STR_BADGE_DEFENSE")
	elseif gc == BadgeCategroy.CategroyOfGreen then
		 typeString = CommonFunction.GetConstString("STR_BADGE_SKILL")
	elseif gc == BadgeCategroy.CategroyOfGolden then
		 typeString = CommonFunction.GetConstString("STR_BADGE_TACTICS")
	end
	
	return typeString
end

function BadgeSlotInfoUpdateHanlder(buf)
	print("涂鸦数据更新,更新时间："..os.time())
	local resp,err = protobuf.decode("fogs.proto.msg.BadgeSlotInfoUpdate",buf)
	if resp then
		local preNum = BadgeSystemInfo:GetAllOwnBadgeBooksNum()
		for k,v in pairs(resp.datas) do
			local bookId = v.book_id
			local slotId  = v.slot_id
			local badgeId  = v.badge_id
			-- update book ---
			local slot = BadgeSystemInfo:GetBadgeSlotByBookIdAndSlotId(bookId,slotId)
			if v.slot_status == "LOCKED" then
				slot.status = BadgeSlotStatus.LOCKED
			elseif v.slot_status == "LOCKED_CANPRE_OPEN" then
				slot.status = BadgeSlotStatus.LOCKED_CANPRE_OPEN
			elseif v.slot_status == "LOCKED_WILL_OPEN" then
				slot.status = BadgeSlotStatus.LOCKED_WILL_OPEN
			elseif v.slot_status == "UNLOCK" then
				if slot.status ~= BadgeSlotStatus.LOCKED and BadgeSlotInfoUpDateCB == nil then
					if not NeedPlayUnlockEffectSlots then
						NeedPlayUnlockEffectSlots = {}
					end
					table.insert(NeedPlayUnlockEffectSlots,slotId)
				end
				slot.status = BadgeSlotStatus.UNLOCK
			end
			slot.badge_id = badgeId
			if BadgeSlotInfoUpDateCB ~= nil then
				BadgeSlotInfoUpDateCB(v.slot_id,true)
			end
			if BadgeBookNameUpdateCB ~= nil then
				BadgeBookNameUpdateCB()
			end
		end
		local currentNum = BadgeSystemInfo:GetAllOwnBadgeBooksNum()
		local rootTrans = UIManager.Instance.m_uiRootBasePanel
		local go = rootTrans.transform:FindChild('UIBadge(Clone)')
		if preNum < currentNum and (not go or (go and not go.gameObject.activeSelf)) then
			HaveNewBadge = true
			UpdateRedDotHandler.MessageHandler("Badge")
		end
	end
end

BadgeSystemVar = {
	currentBookId = 0,
	currentSelectSlotId = nil,
}

BadgeSystemInfo = MainPlayer.Instance.badgeSystemInfo
BadgeAttrData = GameSystem.Instance.BadgeAttrConfigData
BadgeSlotConfigData  = GameSystem.Instance.BadgeSlotsConfig 
GoodsConfigData =  GameSystem.Instance.GoodsConfigData

AddEvent()



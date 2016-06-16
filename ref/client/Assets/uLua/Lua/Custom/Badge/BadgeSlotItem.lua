BadgeSlotItem={
	uiName = "BadgeSlotItem",
	----------Param----------
	slotConfigData,
	currentStatus = nil,
	badgeId = nil,
	parents = nil,
	--------------UI--------------
	uiLock,
	uiIcon,
	uiBuy,
	uiLabel,
	uiSelectIcon,
	uiEffectAni,
}

function BadgeSlotItem:Awake()
	self:UIParise()
end

function BadgeSlotItem:Start( ... )
	-- self.uiIcon = getLuaComponent(createUI("BadgeIcon",self.transform:FindChild("IconNode"))
	self.transform.localPosition = Vector3.New(self.slotConfigData.layoutPosx,self.slotConfigData.layoutPosy,0)
	-- self.uiLabel.text = self.slotConfigData.name
	----addEvent-----
	addOnClick(self.transform.gameObject,self:OnClick())
	-- self:UpdateViewByStatus()
	self:RegisterMsgHanlder()

end

function BadgeSlotItem:FixedUpdate( ... )
	-- body
end

function BadgeSlotItem:OnDestroy( ... )
	-- body
end


function BadgeSlotItem:UIParise( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end
	self.uiLock = find("lock"):GetComponent("UISprite")
	self.uiBuy = find("Buy")
	self.uiLabel = find("Label"):GetComponent("UILabel")
	self.uiLabel.text = ""
	self.uiIcon = find("IconNode")
	self.uiSelectIcon = find("Sele")
	self.uiEffectAni = find("effect"):GetComponent("Animator")
end

function BadgeSlotItem:UpdateViewByStatus(playEffect)
	-- print("槽位"..self.slotConfigData.id.."视图更新")
	-- print("当前的状态为：",self.currentStatus)
	local status = self.currentStatus
	self.uiLabel.text = ""
	self:Reset()
	if BadgeSystemVar.currentSelectSlotId == self.slotConfigData.id then
		self:SetSelect(true)
	end
	----锁定----
	if status == BadgeSlotStatus.LOCKED then
		self.uiLabel.text = ""
		NGUITools.SetActive(self.uiLock.gameObject, true)
		NGUITools.SetActive(self.uiBuy.gameObject,false)
	-----可提前开启----
	elseif status == BadgeSlotStatus.LOCKED_CANPRE_OPEN then
		NGUITools.SetActive(self.uiLock.gameObject, false)
		NGUITools.SetActive(self.uiBuy.gameObject,true)
		self.uiLabel.text = ""
	-----即将开启-----
	elseif status == BadgeSlotStatus.LOCKED_WILL_OPEN then
		NGUITools.SetActive(self.uiLock.gameObject, false)
		NGUITools.SetActive(self.uiBuy.gameObject,false)
		-- print("此槽位开放的等级是：self.slotConfigData.requireLevel"..self.slotConfigData.requireLevel)
		self.uiLabel.text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT8"),self.slotConfigData.requireLevel)
	-----解锁无徽章------
	elseif status == BadgeSlotStatus.UNLOCK then
		---
		NGUITools.SetActive(self.uiLock.gameObject, false)
		NGUITools.SetActive(self.uiBuy.gameObject,false)
		self.uiLabel.text = ""
		--如果这个槽位上有id存在，就说明这个槽位是己经装备的
		if self.badgeId and self.badgeId ~=0 then
			local goods = MainPlayer.Instance:GetBadgesGoodByID(self.badgeId)
			if goods then
				local iconitem = getLuaComponent(createUI("BadgeIcon",self.uiIcon))
				iconitem:SetId(self.badgeId)
				if playEffect then
					iconitem:PlayAnimator()
				end
			else
				print("has no badge in package,the badgeid:"..self.badgeId)
			end
		end
	end
	-- -----解锁有徽章------
	-- elseif status == BadgeSlotStatus.UNLOCKED_WiTH_BADGE then
	-- 	NGUITools.SetActive(self.uiLock.gameObject, false)
	-- 	NGUITools.SetActive(self.uiBuy.gameObject,false)
	-- 	-- NGUITools.SetActive(self.uiIcon.gameObject,true)
	-- 	self.uiLabel.text = ""
	-- 	if self.badgeId~=nil then
	-- 		local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.badgeId)
	-- 		if goods then
	-- 			local iconitem = getLuaComponent(createUI("BadgeIcon",self.uiIcon))
	-- 			iconitem:SetIcon(goods.icon)
	-- 		else
	-- 			print("badge isn't exist in config file,check it please ,the id is:",self.badgeId)
	-- 		end
	-- 	end
	-- end
end

function BadgeSlotItem:OnClick( ... )
	return function()
		-- print("*********** slot item click**************** slotid:"..self.slotConfigData.id)
		if self.parents then
			self.parents:ResetAllSelectStatus()
		end
		self:SetSelect(true)
		local status = self.currentStatus
			----锁定----
		if status == BadgeSlotStatus.LOCKED then
			-- STR_BADGE_UNLOCK_LEVEL_LIMIT
			-- CommonFunction.ShowTip("need least"..self.slotConfigData.requireLevel.."to unlock",nil)
			CommonFunction.ShowTip(string.format(CommonFunction.GetConstString("STR_BADGE_UNLOCK_LEVEL_LIMIT"),self.slotConfigData.requireLevel),nil)
		-----可提前开启 or 即将开启----
		elseif status == BadgeSlotStatus.LOCKED_CANPRE_OPEN or status == BadgeSlotStatus.LOCKED_WILL_OPEN then
			local window = createUI("BadgeSlotRelativeWindow")
			local t = getLuaComponent(window)
			t:ShowUnLockPanel(self.slotConfigData)
			t.unlockCallBack = self:BadgeSlotUnlockCallBack()
			UIManager.Instance:BringPanelForward(window)
			self.parents:ShowMainInfoPanel()(true)
		-----解锁无徽章------
		elseif status == BadgeSlotStatus.UNLOCK then
			BadgeSystemVar.currentSelectSlotId = self.slotConfigData.id
			self.parents:BadgeSlotClickedHanlder()()
		end
	end
end

function BadgeSlotItem:BadgeSlotUnlockCallBack( ... )
	return function()
		local req = {
			slot_id = self.slotConfigData.id,
			book_id = BadgeSystemVar.currentBookId
		}
		-- print("解锁请求,BOOK_ID:"..req.book_id.."SLOTID:"..req.slot_id.."当前时间"..os.time())
		CommonFunction.ShowWait()
		local buf = protobuf.encode("fogs.proto.msg.BadgeUnlockSlotReq",req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeUnlockSlotRespID,self:BadgeUnlockSlotRespHanlder(),self.uiName)
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeUnlockSlotReqID,buf)	
	end
end

function BadgeSlotItem:Reset()
	if self.uiIcon ~= nil and self.uiIcon.transform~= nil then
		CommonFunction.ClearChild(self.uiIcon.transform)      
	end                                                                                                                                                
	self:SetSelect(false)
end

-- 播放槽位上的特效--
function BadgeSlotItem:PlayAnimator( ... )
	if self.uiEffectAni then
		NGUITools.SetActive(self.uiEffectAni.gameObject,true)
		self.uiEffectAni:SetBool("PlayTrigger",true)
	end
end

--------取得这个槽位的iD---------------
function BadgeSlotItem:GetID( ... )
	return self.slotConfigData.id
end

function BadgeSlotItem:SetSlotStatus(status,badgeid,playEffect)
	-- print("BadgeSlotIitem Status ----------------")
	-- print(status)
	-- print(badgeid)
	-- print("BadgeSlotIitem Status ----------------")
	self.currentStatus = status
	self.badgeId = badgeid
	self:UpdateViewByStatus(playEffect)
end

function BadgeSlotItem:SetSelect(value)
	NGUITools.SetActive(self.uiSelectIcon.gameObject,value)
end

function BadgeSlotItem:RegisterMsgHanlder( ... )
	
end

function BadgeSlotItem:BadgeUnlockSlotRespHanlder( ... )
	return function(buf)
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeUnlockSlotRespID,self.uiName)
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeUnlockSlotResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				-- CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
			-- print("解锁成功"..os.time())
			-- CommonFunction.ShowTip("解锁成功",nil)
			self:PlayAnimator()
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_UNLOCK_BADGE_SUCESS"),nil,nil,nil,nil,nil)
		end
	end
end


return BadgeSlotItem
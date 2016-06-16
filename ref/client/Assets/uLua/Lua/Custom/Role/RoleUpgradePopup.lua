RoleUpgradePopup =  {
	uiName	 = "RoleUpgradePopup", 

	------------------UI
	uiMemberRuleMemberItemIcon,
	uibackgroud,
	uibackgroundBackground,
	uibackgroundBackgroundSprite,
	uipopupFrame3,
	uibuttonUpgrade,
	uibuttonUpgradeLabel,
	uibuttonUpgradeBg,
	uimask,
	uimemberRule,
	uimemberRuleCost,
	uimemberRuleCostGoldConsumeNum,
	uimemberRuleMemberItem,
	uimemberRuleMemberItemIconIconFrame,
	uimemberRuleMemberItemPosition,
	uimemberRuleMemberItemName,
	uimemberRuleName,
	uimemberRuleTrainGrid,
	uimemberRuleLabelLevel,
	uimemberRuleTitle,
	uimemberTrain,
	uimemberTrainName,
	uimemberTrainNameBgTitle,
	uimemberTrainBg,
	uimemberTrainGrid,
	uimemberTrainTitle,
	uimemberTrainGoods,
	uimemberTrainGoodsGoodsIcon0,
	uimemberTrainGoodsGoodsIcon1,
	uimemberTrainGoodsGoodsIcon2,
	uimemberTrainGoodsGoodsIcon3,
	uimemberTrainLabelConsume,
	uimemberTrainLabelConsumeGoodsIconConsume,
	---------------------parameters
	popupFrame,
	goodsIconConsume,
	goodsIcon1,
	goodsIcon2,
	goodsIcon3,
	trainId,
	roleId,
	mode,
}




function RoleUpgradePopup:Awake()
	self.uiMemberRuleMemberItemIcon = self.transform:FindChild("memberRule/memberItem/icon"):GetComponent("UISprite")
	self.uibackgroud = self.transform:FindChild("background"):GetComponent("UISprite")
	self.uibackgroundBackground = self.transform:FindChild("background/background"):GetComponent("UISprite")
	self.uibackgroundBackgroundSprite = self.transform:FindChild("background/background/sprite"):GetComponent("UISprite")
	self.uipopupFrame3 = self.transform:FindChild("popupFrame3"):GetComponent("Transform")
	self.uibuttonUpgrade = self.transform:FindChild("buttonUpgrade"):GetComponent("UIButton")
	self.uibuttonUpgradeLabel = self.transform:FindChild("buttonUpgrade/label"):GetComponent("UILabel")
	self.uibuttonUpgradeBg = self.transform:FindChild("buttonUpgrade/bg"):GetComponent("UISprite")
	self.uimask = self.transform:FindChild("mask"):GetComponent("UISprite")
	self.uimemberRule = self.transform:FindChild("memberRule"):GetComponent("Transform")
	self.uimemberRuleCost = self.transform:FindChild("memberRule/cost"):GetComponent("Transform")
	self.uimemberRuleCostGoldConsumeNum = self.transform:FindChild("memberRule/cost/goldConsume/num"):GetComponent("UILabel")
	self.uimemberRuleMemberItem = self.transform:FindChild("memberRule/memberItem"):GetComponent("Transform")
	self.uimemberRuleMemberItemIconIconFrame = self.transform:FindChild("memberRule/memberItem/icon/iconFrame"):GetComponent("UISprite")
	self.uimemberRuleMemberItemPosition = self.transform:FindChild("memberRule/memberItem/position"):GetComponent("UISprite")
	self.uimemberRuleMemberItemName = self.transform:FindChild("memberRule/memberItem/name"):GetComponent("UILabel")
	self.uimemberRuleName = self.transform:FindChild("memberRule/name"):GetComponent("UILabel")
	self.uimemberRuleTrainGrid = self.transform:FindChild("memberRule/trainGrid"):GetComponent("UIGrid")
	self.uimemberRuleLabelLevel = self.transform:FindChild("memberRule/labelLevel"):GetComponent("UILabel")
	self.uimemberRuleTitle = self.transform:FindChild("memberRule/title"):GetComponent("UILabel")
	self.uimemberTrain = self.transform:FindChild("memberTrain"):GetComponent("Transform")
	self.uimemberTrainName = self.transform:FindChild("memberTrain/name"):GetComponent("UILabel")
	self.uimemberTrainNameBgTitle = self.transform:FindChild("memberTrain/name/bgTitle"):GetComponent("UISprite")
	self.uimemberTrainBg = self.transform:FindChild("memberTrain/bg"):GetComponent("UISprite")
	self.uimemberTrainGrid = self.transform:FindChild("memberTrain/grid"):GetComponent("UIGrid")
	self.uimemberTrainTitle = self.transform:FindChild("memberTrain/title"):GetComponent("UILabel")
	self.uimemberTrainGoods = self.transform:FindChild("memberTrain/goods"):GetComponent("Transform")
	self.uimemberTrainGoodsGoodsIcon0 = self.transform:FindChild("memberTrain/goods/goodsIcon0"):GetComponent("Transform")
	self.uimemberTrainGoodsGoodsIcon1 = self.transform:FindChild("memberTrain/goods/goodsIcon1"):GetComponent("Transform")
	self.uimemberTrainGoodsGoodsIcon2 = self.transform:FindChild("memberTrain/goods/goodsIcon2"):GetComponent("Transform")
	self.uimemberTrainGoodsGoodsIcon3 = self.transform:FindChild("memberTrain/goods/goodsIcon3"):GetComponent("Transform")
	self.uimemberTrainLabelConsume = self.transform:FindChild("memberTrain/labelConsume"):GetComponent("UILabel")
	self.uimemberTrainLabelConsumeGoodsIconConsume = self.transform:FindChild("memberTrain/labelConsume/goodsIconConsume"):GetComponent("Transform")


	local g,t
	g  = WidgetPlaceholder.Replace(self.uipopupFrame3.gameObject)
	self.popupFrame = getLuaComponent(g)
	self.popupFrame.onClose = self:OnClickClose()
	self.popupFrame:SetTitleColor(Color.New(0.317,0.317,0.317,1.0))

	-- gold cost
	g = createUI("GoodsIconConsume",self.uimemberTrainLabelConsumeGoodsIconConsume)
	self.goodsIconConsume = getLuaComponent(g)
	self.goodsIconConsume.reward_id = 2

	-- goods icons
	g = createUI("GoodsIcon2",self.uimemberTrainGoodsGoodsIcon0.transform)

	self.goodsIcon0 = getLuaComponent(g)
	
	
	g = createUI("GoodsIcon",self.uimemberTrainGoodsGoodsIcon1.transform)
	self.goodsIcon1 = getLuaComponent(g)
	self.goodsIcon1.showTips = false

	g = createUI("GoodsIcon",self.uimemberTrainGoodsGoodsIcon2.transform)
	self.goodsIcon2 = getLuaComponent(g)
	self.goodsIcon2.showTips = false
	
	g = createUI("GoodsIcon",self.uimemberTrainGoodsGoodsIcon3.transform)
	self.goodsIcon3 = getLuaComponent(g)
	self.goodsIcon3.showTips = false
	-- mode train
	addOnClick(self.uibuttonUpgrade.gameObject,self:OnClickUpgrade())
end

function RoleUpgradePopup:Start()
	self:Refresh()
end

function RoleUpgradePopup:FixedUpdate()
	if self.showTips ~= nil and self.showTips then
		if not self.transform:FindChild("TipWithTween(Clone)") then
			print("			self.showTips = false")
			self.showTips = false
			CommonFunction.HideWaitMask()
		end
	end
end

function RoleUpgradePopup:OnDestroy()
	--body   
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function RoleUpgradePopup:Refresh()

	local traind_id = self.trainId
	
	local roleId = self.roleId
	local mode = self.mode
	self.level = MainPlayer.Instance:GetExerciseLevel(roleId,trainId)

	if mode == 0 then
		self:RefreshExercise()
	elseif mode == 1 then
		self:Refresh1()
		self:SetAvatar(roleId)
	elseif mode == 2 then
		self:Refresh2()
		self:SetAvatar(roleId)
	end
end

function RoleUpgradePopup:Refresh1()
	local id = roleId
	self.uimemberRuleTitle.text = getCommonStr("STR_MEMBER_UPGRADE")
	self.uibuttonUpgradeLabel.text = getCommonStr("UPGRADE1")

	local roleInfo = MainPlayer.Instance:GetRole2(id)
	local roleQuality = 1
	if roleInfo then
		roleQuality = roleInfo.quality
	end
	-- set cost gold
	local consume = GameSystem.Instance.qualityAttrCorConfig:GetConsume(id, roleQuality+1)
	
	local enum = consume:GetEnumerator()
	local costGold = 0
	while enum:MoveNext() do
		local id = enum.Current.Key
		local value = enum.Current.value
		if id == 2 then
			costGold = value
		end
	end
	self.uimemberRuleCostGoldConsumeNum.text = tostring(costGold )


	self.trainList = {}
	

	CommonFunction.ClearGridChild(self.uimemberRuleTrainGrid.transform)
	local baseData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.roleId)
	local trains = baseData.training_slots
	local enum = trains:GetEnumerator()
	while enum:MoveNext() do
		local trainid = enum.Current
		local trainItem =  getLuaComponent(createUI("GoodsIcon2",self.uimemberRuleTrainGrid.transform))
		trainItem.levelDisplay = roleQuality * 6	
		trainItem:SetData(roleId,trainid)
		trainItem:HideStar(true)
		table.insert(self.trainList,trainItem)
	end
	
	self.uimemberRuleTrainGrid:Reposition()

end

function RoleUpgradePopup:Refresh2()
	self.uimemberRuleTitle.text = getCommonStr("STR_ENHANCE_STAR")
	self.uibuttonUpgradeLabel.text = getCommonStr("STR_ENHANCE")

	local id = roleId

	self.consumeList = {}
	local curStar = MainPlayer.Instance:GetRole2(self.roleId).star
	local starData = GameSystem.Instance.starAttrConfig:GetStarAttr(self.roleId,curStar + 1)

	CommonFunction.ClearGridChild(self.uimemberRuleTrainGrid.transform)
	
	local enum = starData.consume:GetEnumerator()
	while enum:MoveNext() do
		local consumeId = enum.Current.Key
		local consumeValue = enum.Current.Value

		if consumeId == 2 then
			self.uimemberRuleCostGoldConsumeNum.text = tostring(consumeValue)
			-- self.goodsIconConsume.reward_num = tostring(consumeValue)
		else
			local consumeItem =  getLuaComponent(createUI("GoodsIcon",self.uimemberRuleTrainGrid.transform))
			local ownedNum = MainPlayer.Instance:GetGoodsCount(consumeId)
			consumeItem.goodsID = consumeId
			consumeItem.hideLevel = true
			consumeItem:SetNeed(tostring(ownedNum).."/"..tostring(consumeValue))
			table.insert(self.consumeList,consumeItem)
		end
	end
	
	self.uimemberRuleTrainGrid:Reposition()
	
end

function RoleUpgradePopup:EnhanceExerciseFromC(resp)
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			print("s_hander train role id",resp.role_id)
			self:RefreshAllGoodsIcons()
			self.showTips = true
			self.tw = CommonFunction.ShowTip(getCommonStr("ROLE_ENHANCE_EXERCISE_SUCCESS"),self.transform)
			self:Refresh()
		else
			CommonFunction.HideWaitMask()
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("EnhanceExerciseFromC(): " .. err)
	end
end

function RoleUpgradePopup:RefreshAllGoodsIcons()
	for k,v in pairs(self.goodsIconList) do 
		v:Refresh()
	end
end

function RoleUpgradePopup:EnhanceLevelFromC(resp)
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			print("s_hander role enhance level response id",resp.role.id)
			CommonFunction.ShowTip(getCommonStr("ROLE_ENHANCE_STAR_SUCCESS"))
			self:Refresh()
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("EnhanceLevelFromC(): " .. err)
	end
	
end


function RoleUpgradePopup:improveQualityFromC(resp)
	if resp then
		print("resp.result=",resp.result)
		if resp.result == 0 then
			print("improveQualityFromC role_id=",resp.role_id)
			print("improveQualityFromC new_quality=",resp.new_quality)
			print("improveQualityFromC cur_pieces=",resp.cur_pieces)
			CommonFunction.ShowTip(getCommonStr("ROLE_IMPROVE_QUAULITTY_SUCCESS"))
			-- close this ui after improve quality.
			if self.onClickClose then
				self.onClickClose()
			end
			-- self:Refresh()
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error("improveQualityFromC(): " .. err)
	end
	
end

function RoleUpgradePopup:OnClickUpgrade()
	return function()
		if self.mode == 0 then
			if self.exerciseEnough ~= nil then
				CommonFunction.ShowTip(self.exerciseEnough)
				return 
			end
			CommonFunction.ShowWaitMask()			
			local roleId = self.roleId
			local exercise_id = self.trainId
			-- optional uint32 roleId			= 1;
			-- optional uint32 training_id		= 2;
			print("Train OnClickUpgrade for Exercise roleid =", roleId," exercise_id=",exercise_id)
			local operation = {
				role_id = roleId,
				exercise_id = exercise_id
			}

			local req = protobuf.encode("fogs.proto.msg.EnhanceExerciseReq",operation)
			LuaHelper.SendPlatMsgFromLua(MsgID.EnhanceExerciseReqID,req)
		elseif self.mode == 1 then
			-- quality
			local roleId = self.roleId
			print("Train OnClickUpgrade for role quality roleId =", roleId )
			local operation = {
				role_id = roleId,
			}
			
			local req = protobuf.encode("fogs.proto.msg.ImproveQualityReq",operation)
			LuaHelper.SendPlatMsgFromLua(MsgID.ImproveQualityReqID,req)
		elseif self.mode == 2 then
			-- star
			local roleId = self.roleId
			print("Train OnClickUpgrade for Star  roleid =", roleId )
			local operation = {
				role_id = roleId,
			}
			
			local req = protobuf.encode("fogs.proto.msg.EnhanceLevelReq",operation)
			LuaHelper.SendPlatMsgFromLua(MsgID.EnhanceLevelReqID,req)
		end
	end
end

function RoleUpgradePopup:SetRoleData(roleId)
	print("role upgrade popup role roleId="..tostring(roleId))
	self:SetMode(1)
	self.roleId = roleId
	self:Refresh()
	
end

function RoleUpgradePopup:SetRoleStarData(roleId)
	-- TODO:
	self:SetMode(2)
	self.roleId = roleId

	self:Refresh()
end

function RoleUpgradePopup:SetAvatar(roleId)
	local role_info = MainPlayer.Instance:GetRole2(roleId)
	local quality = role_info.quality
	
	self.uiMemberRuleMemberItemIcon.spriteName = "icon_portrait_"..roleId
	self.uiMemberRuleMemberItemIcon.atlas = getPortraitAtlas(roleId)
	self.uimemberRuleMemberItemIconIconFrame.color = getQualityColor( quality)

	print("roleId=",roleId)

	self.uimemberRuleName.text = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(roleId).intro

end

function RoleUpgradePopup:RefreshExercise()
	local trainId = self.trainId
	local roleId = self.roleId
	print("role upgrade popup trainId=",trainId)
	print("role upgrade popup roleId=",roleId)

	self.uimemberTrainTitle.text = getCommonStr("STR_EXERCISE_UPGRADE")
	self.uibuttonUpgradeLabel.text = getCommonStr("STR_TRAINING")
	
	self.uimemberTrainName.text = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.trainId).name
	self.level = MainPlayer.Instance:GetExerciseLevel(roleId,trainId)
	local level = self.level
	
	print("role upgrade popup level=",level)
	if level ~= 0 and level% 5 == 0 then
		self.uimemberTrainTitle.text = getCommonStr("EXERCISE_ENHANCE_LEVEL")
	end
	
	local config = GameSystem.Instance.skillUpConfig
	local consume = config:GetSkillConsume(trainId, level + 1)


	local csize = consume.Count - 1

	self.goodsIcon0:set_data(self.roleId,self.trainId)
	self.goodsIconList = {}
	if csize == 1 then
		table.insert(self.goodsIconList,self.goodsIcon2)
	elseif csize ==2 then
		table.insert(self.goodsIconList,self.goodsIcon1)
		table.insert(self.goodsIconList,self.goodsIcon3)
	elseif csize ==3 then
		table.insert(self.goodsIconList,self.goodsIcon1)
		table.insert(self.goodsIconList,self.goodsIcon2)
		table.insert(self.goodsIconList,self.goodsIcon3)		
	end
	self:HideAllGoodsIcon()

	self.exerciseEnough = nil
	local enum = consume:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current.Key
		local value = enum.Current.Value
		if id == 2 then
			--  gold
			print("reward_num ="..tostring(value))
			self.goodsIconConsume.reward_num = value
			self.goodsIconConsume:Refresh()
		else
			local ic = self:GetAvailableGoodsIcon()
			ic.goodsID = id
			local owned = MainPlayer.Instance:GetGoodsCount(id)
			ic:SetNeed(tostring(owned).."/"..tostring(value))
			ic.hideLevel = true
			ic:Refresh()

			if owned < value and self.exerciseEnough == nil then
				self.exerciseEnough = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).name .. getCommonStr("STR_NUM_NOT_ENOUGH")
			end				
		end
	end


	-- left upgrade info offset display.
	local curr_attr = config:GetSkillAttr(trainId,level)
	local next_attr = config:GetSkillAttr(trainId,level+1)

	-- local curr_enum = curr_attr:GetEnumerator()
	local next_enum = next_attr:GetEnumerator()

	CommonFunction.ClearGridChild(self.uimemberTrainGrid.transform)
	while next_enum:MoveNext() do
		local next_id = next_enum.Current.Key
		local next_value = next_enum.Current.Value
		local curr_value = 0
		if level ~= 0  then
			curr_value = curr_attr[next_id]
		end

		print("next_id = "..tostring(next_id))
		print("curr_value = "..tostring(curr_value))
		print("next_value = "..tostring(next_value))
		g = createUI("AttrUpgradeListItem",	self.uimemberTrainGrid.transform)
		local item = getLuaComponent(g)
		item.prevValue = curr_value
		item.curValue = next_value
		item.showPlus = false
		item.attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(next_id)
	end
	self.uimemberTrainGrid:Reposition()
end


function RoleUpgradePopup:SetTrainData(roleId,trainId)
	-- TODO: level max?
	-- mode: 0 for trainning
	-- mode: 1 for role quality
	-- mode: 2 for role star

	self:SetMode(0)

	self.trainId = trainId
	self.roleId = roleId
	self.goodsIcon0:set_data(roleId,trainId)	

	self:Refresh()

end


function RoleUpgradePopup:SetMode(mode)
	self.mode = mode
	NGUITools.SetActive(self.uimemberTrain.gameObject, mode == 0)
	NGUITools.SetActive(self.uimemberRule.gameObject, mode == 1 or mode == 2)	
end

function RoleUpgradePopup:OnClickClose()
	return function()
		if self.onClickClose then
			self.onClickClose()
		end
	end
end

function RoleUpgradePopup:destroy()
	NGUITools.Destroy(self.gameObject)
end


-- uncommoent if needed

-- uncommoent if needed
-- function RoleUpgradePopup:Update()
	

-- end



function RoleUpgradePopup:HideAllGoodsIcon()
	NGUITools.SetActive(self.goodsIcon1.transform.parent.gameObject,false)
	NGUITools.SetActive(self.goodsIcon2.transform.parent.gameObject,false)
	NGUITools.SetActive(self.goodsIcon3.transform.parent.gameObject,false)	
end
	
function RoleUpgradePopup:GetAvailableGoodsIcon()
	local l = self.goodsIconList
	for i=1,#l do
		ic = l[i]
		if not NGUITools.GetActive(ic.gameObject) then
			NGUITools.SetActive(ic.transform.parent.gameObject,true)
			return ic
		end
	end
		
end


return RoleUpgradePopup

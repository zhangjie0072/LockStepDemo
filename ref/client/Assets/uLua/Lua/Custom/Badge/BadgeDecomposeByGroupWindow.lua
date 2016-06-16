BadgeDecomposeByGroupWindow = {
	uiName = "BadgeDecomposeByGroupWindow",
	------params----------
	levelOneSelect = false,
	levelTwoSelect = false,
	levelThreeSelect = false,
	levelFourSelect = false,
	resultNum = 0,
	resultId  = 0,
	refreshCallBack = nil,
	levelOneBadgeSelectGroup,
	levelTwoBadgeSelectGroup,
	levelThreeBadgeSelectGroup,
	levelFourBadgeSelectGroup,
	currentDetailLevelSelect = nil,
	------UI--------------
	uiCloseBtn,
	uiAnimator,
	uiMainPanel,
	uiDetailPanel,
	uiDetailScrollView,
	uiDetailGrid,
	uiResultNumLabel,
}

function BadgeDecomposeByGroupWindow:Awake( ... )
	self:UIParise()
	self:AddEvent()
end

function BadgeDecomposeByGroupWindow:Start( ... )
	self:InitView()
end

function BadgeDecomposeByGroupWindow:FixedUpdate( ... )
	-- body
end

function BadgeDecomposeByGroupWindow:OnDestroy( ... )
	-- body
end

function BadgeDecomposeByGroupWindow:UIParise( ... )
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiCloseBtn = createUI("ButtonClose",find("Window/ButtonClose"))
	self.uiMainPanel = find("Window/MainPanel")
	self.uiDetailPanel = find("Window/DetailPanel")
	self.uiCheckLevelOneDetailBtn = find("Window/MainPanel/Grid/1/OK")
	self.uiCheckLevelTwoDetailBtn = find("Window/MainPanel/Grid/2/OK")
	self.uiCheckLevelThreeDetailBtn = find("Window/MainPanel/Grid/3/OK")
	self.uiCheckLevelFourDetailBtn = find("Window/MainPanel/Grid/4/OK")
	self.uiDetailScrollView = find("Window/DetailPanel/ScrollView"):GetComponent("UIScrollView")
	self.uiDetailGrid = find("Window/DetailPanel/ScrollView/Grid"):GetComponent("UIGrid")
	self.uiDetailOkBtn = find("Window/DetailPanel/OK")
	self.uiDetailNOBtn = find("Window/DetailPanel/NO")
	self.uiDecomposeBtn = find("Window/MainPanel/OK"):GetComponent("UIButton")
	self.uiResultNumLabel = find("Window/MainPanel/Point/Num"):GetComponent("UILabel")
	self.uiResultNumLabel.text = "0"
	self.item1 = find("Window/MainPanel/Grid/1"):GetComponent("UIToggle")
	self.item2 = find("Window/MainPanel/Grid/2"):GetComponent("UIToggle")
	self.item3 = find("Window/MainPanel/Grid/3"):GetComponent("UIToggle")
	self.item4 = find("Window/MainPanel/Grid/4"):GetComponent("UIToggle")
	self.uiAnimator = transform:GetComponent("Animator")
end

function BadgeDecomposeByGroupWindow:ShowPanel( ... )
	NGUITools.SetActive(self.uiMainPanel.gameObject,true)
	NGUITools.SetActive(self.uiDetailPanel.gameObject,false)
end

function BadgeDecomposeByGroupWindow:AddEvent( ... )
	addOnClick(self.uiCloseBtn.gameObject,self:OnClickHanlder())
	addOnClick(self.uiCheckLevelOneDetailBtn.gameObject,self:CheckDetailByLevel(1))
	addOnClick(self.uiCheckLevelTwoDetailBtn.gameObject,self:CheckDetailByLevel(2))
	addOnClick(self.uiCheckLevelThreeDetailBtn.gameObject,self:CheckDetailByLevel(3))
	addOnClick(self.uiCheckLevelFourDetailBtn.gameObject,self:CheckDetailByLevel(4))
	addOnClick(self.uiDetailNOBtn.gameObject,self:DetailNOHanlder())
	addOnClick(self.uiDetailOkBtn.gameObject,self:DetailOkHanlder())
	addOnClick(self.uiDecomposeBtn.gameObject,self:DecomposeHanlder())
	-- addOnClick(self.item1.gameObject,self:OnItemSelectHanlder(1))
	-- addOnClick(self.item2.gameObject,self:OnItemSelectHanlder(2))
	-- addOnClick(self.item3.gameObject,self:OnItemSelectHanlder(3))
	-- addOnClick(self.item4.gameObject,self:OnItemSelectHanlder(4))
	EventDelegate.Add(self.item1.onChange, LuaHelper.Callback(self:OnLevelOneItemSelect()))
	EventDelegate.Add(self.item2.onChange, LuaHelper.Callback(self:OnLevelTwoItemSelect()))
	EventDelegate.Add(self.item3.onChange, LuaHelper.Callback(self:OnLevelThreeItemSelect()))
	EventDelegate.Add(self.item4.onChange, LuaHelper.Callback(self:OnLevelFourItemSelect()))
end

function BadgeDecomposeByGroupWindow:OnLevelOneItemSelect()
	return function()
		self.levelOneSelect = self.item1.value
		NGUITools.SetActive(self.uiCheckLevelOneDetailBtn.gameObject,self.levelOneSelect)
		if self.levelOneSelect then
			self.levelOneBadgeSelectGroup = self:GetBadgesByLevel(1)
		else
			self.levelOneBadgeSelectGroup = nil
		end
		self:RefreshMetrialsNum()
	end
end

function BadgeDecomposeByGroupWindow:OnLevelTwoItemSelect()
	return function()
		self.levelTwoSelect = self.item2.value
		NGUITools.SetActive(self.uiCheckLevelTwoDetailBtn.gameObject,self.levelTwoSelect)
		if self.levelTwoSelect then
			self.levelTwoBadgeSelectGroup = self:GetBadgesByLevel(2)
		else
			self.levelTwoBadgeSelectGroup = nil
		end
		self:RefreshMetrialsNum()
	end
end

function BadgeDecomposeByGroupWindow:OnLevelThreeItemSelect()
	return function()
		self.levelThreeSelect = self.item3.value
		NGUITools.SetActive(self.uiCheckLevelThreeDetailBtn.gameObject,self.levelThreeSelect)
		if self.levelThreeSelect then
			self.levelThreeBadgeSelectGroup = self:GetBadgesByLevel(3)
		else
			self.levelThreeBadgeSelectGroup = nil
		end
		self:RefreshMetrialsNum()
	end
end

function BadgeDecomposeByGroupWindow:OnLevelFourItemSelect()
	return function()
		self.levelFourSelect = self.item4.value
		NGUITools.SetActive(self.uiCheckLevelFourDetailBtn.gameObject,self.levelFourSelect)
		if self.levelFourSelect then
			self.levelFourBadgeSelectGroup = self:GetBadgesByLevel(4)
		else
			self.levelFourBadgeSelectGroup = nil
		end
		self:RefreshMetrialsNum()
	end
end

function BadgeDecomposeByGroupWindow:InitView()
	self.item1.transform:FindChild("Text"):GetComponent("UILabel").text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT26"),1)
	self.item2.transform:FindChild("Text"):GetComponent("UILabel").text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT26"),2)
	self.item3.transform:FindChild("Text"):GetComponent("UILabel").text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT26"),3)
	self.item4.transform:FindChild("Text"):GetComponent("UILabel").text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT26"),4)
	self:Reset()
end

function BadgeDecomposeByGroupWindow:Reset()
	self.levelOneSelect = false
	self.levelTwoSelect = false
	self.levelThreeSelect = false
	self.levelFourSelect = false
	self.uiDecomposeBtn.isEnabled = false
	self.item1.value = false
	self.item2.value = false
	self.item3.value = false
	self.item4.value = false
	self:RefreshMetrialsNum()
end

function BadgeDecomposeByGroupWindow:DecomposeHanlder( ... )
	return function()
		local req = {
			uuids = self:GetAllGoods()
		}
		for k,v in pairs(req.uuids) do
			print("要被分解的物品UUID:"..v)
		end
		local buf = protobuf.encode("fogs.proto.msg.BadgeDecomposeByGroupReq",req)
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeDecomposeByGroupReqID,buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeDecomposeByGroupRespID,self:BadgeDecomposeRespHandler(),self.uiName)
	end
end

function BadgeDecomposeByGroupWindow:BadgeDecomposeRespHandler()
	return function(buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeDecomposeByGroupRespID,self.uiName)
		CommonFunction.StopWait()
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeDecomposeByGroupResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			local num = tonumber(self.uiResultNumLabel.text)
			getGoods:SetGoodsData(4024, num)
			self:Reset()
			if self.refreshCallBack then
				self:refreshCallBack()
			end
		end
	end
end

function BadgeDecomposeByGroupWindow:GetAllGoods()
	local temp = {}
	-- if self.levelOneSelect then table.insert(temp,1) end
	-- if self.levelTwoSelect then table.insert(temp,2) end
	-- if self.levelThreeSelect then table.insert(temp,3) end
	-- if self.levelFourSelect then table.insert(temp,4) end
	if self.levelOneBadgeSelectGroup then
		for k,v in pairs(self.levelOneBadgeSelectGroup) do
			local goods = MainPlayer.Instance:GetBadgesGoodByID(v)
			table.insert(temp,goods:GetUUID())
		end
	end

	if self.levelTwoBadgeSelectGroup then
		for k,v in pairs(self.levelTwoBadgeSelectGroup) do
			local goods = MainPlayer.Instance:GetBadgesGoodByID(v)
			table.insert(temp,goods:GetUUID())
		end
	end

	if self.levelFourBadgeSelectGroup then
		for k,v in pairs(self.levelFourBadgeSelectGroup) do
			local goods = MainPlayer.Instance:GetBadgesGoodByID(v)
			table.insert(temp,goods:GetUUID())
		end
	end

	if self.levelThreeBadgeSelectGroup then
		for k,v in pairs(self.levelThreeBadgeSelectGroup) do
			local goods = MainPlayer.Instance:GetBadgesGoodByID(v)
			table.insert(temp,goods:GetUUID())
		end
	end
	return temp
end

function BadgeDecomposeByGroupWindow:RefreshMetrialsNum()
	local totalNum = 0
	if self.levelOneBadgeSelectGroup then
		for k,v in pairs(self.levelOneBadgeSelectGroup) do
			totalNum = totalNum + self:GetEncomposeReslutGoodNumByGoodsId(v)
		end
	end

	if self.levelTwoBadgeSelectGroup then
		for k,v in pairs(self.levelTwoBadgeSelectGroup) do
			totalNum = totalNum + self:GetEncomposeReslutGoodNumByGoodsId(v)
		end
	end

	if self.levelThreeBadgeSelectGroup then
		for k,v in pairs(self.levelThreeBadgeSelectGroup) do
			totalNum = totalNum + self:GetEncomposeReslutGoodNumByGoodsId(v)
		end
	end

	if 	self.levelFourBadgeSelectGroup then
		for k,v in pairs(self.levelFourBadgeSelectGroup) do
			totalNum = totalNum + self:GetEncomposeReslutGoodNumByGoodsId(v)
		end
	end
	self.resultNum = totalNum
	self.uiResultNumLabel.text = totalNum
	self.uiDecomposeBtn.isEnabled = (totalNum>0)
end

function BadgeDecomposeByGroupWindow:DetailNOHanlder( ... )
	return function()
		NGUITools.SetActive(self.uiDetailPanel.gameObject,false)
		NGUITools.SetActive(self.uiMainPanel.gameObject,true)
		NGUITools.SetActive(self.uiCloseBtn.gameObject,true)
	end
end

function BadgeDecomposeByGroupWindow:DetailOkHanlder( ... )
	return function()
		NGUITools.SetActive(self.uiDetailPanel.gameObject,false)
		NGUITools.SetActive(self.uiMainPanel.gameObject,true)
		NGUITools.SetActive(self.uiCloseBtn.gameObject,true)
		local temp = {}
		local count = self.uiDetailGrid.transform.childCount
		for i=0,count-1 do
			local t = getLuaComponent(self.uiDetailGrid.transform:GetChild(i).gameObject)
			if not t:IsSelect() then
				table.insert(temp,t:GetId())
			end
		end
		if #temp<=0 then
			self:RefreshMetrialsNum()
			return
		end
		if self.currentDetailLevelSelect == 1 then
			if self.levelOneBadgeSelectGroup and #self.levelOneBadgeSelectGroup > 0 then
				for k,v in pairs(temp) do
					local index = self:GetIndex(v,self.levelOneBadgeSelectGroup)
					if level ~= -1 then
						table.remove(self.levelOneBadgeSelectGroup,index)
					end
				end
			end
		end

		if self.currentDetailLevelSelect == 2 then
			if self.levelTwoBadgeSelectGroup and #self.levelTwoBadgeSelectGroup > 0  then
				for k,v in pairs(temp) do
					local index = self:GetIndex(v,self.levelTwoBadgeSelectGroup)
					if level ~= -1 then
						table.remove(self.levelTwoBadgeSelectGroup,index)
					end
				end
			end
		end

		if self.currentDetailLevelSelect == 3 then
			if self.levelThreeBadgeSelectGroup and #self.levelThreeBadgeSelectGroup > 0  then
				for k,v in pairs(temp) do
					local index = self:GetIndex(v,self.levelThreeBadgeSelectGroup)
					if level ~= -1 then
						table.remove(self.levelThreeBadgeSelectGroup,index)
					end
				end
			end
		end

		if self.currentDetailLevelSelect == 4 then
			if self.levelFourBadgeSelectGroup  and #self.levelFourBadgeSelectGroup > 0  then
				for k,v in pairs(temp) do
					local index = self:GetIndex(v,self.levelFourBadgeSelectGroup)
					if level ~= -1 then
						table.remove(self.levelFourBadgeSelectGroup,index)
					end
				end
			end
		end
		self:RefreshMetrialsNum()
	end
end

function BadgeDecomposeByGroupWindow:GetIndex(d,tt)
	for k,v in pairs(tt) do
		if v == d then
			return k
		end
	end
	return -1
end

function BadgeDecomposeByGroupWindow:CheckDetailByLevel(level)
	return function()
		self.currentDetailLevelSelect = level
		if self.currentDetailLevelSelect == 1 then
			self.levelOneBadgeSelectGroup = self:GetBadgesByLevel(1)
		end
		if self.currentDetailLevelSelect == 2 then
			self.levelTwoBadgeSelectGroup = self:GetBadgesByLevel(2)
		end
		if self.currentDetailLevelSelect == 3 then
			self.levelThreeBadgeSelectGroup = self:GetBadgesByLevel(3)
		end
		if self.currentDetailLevelSelect == 4 then
			self.levelFourBadgeSelectGroup = self:GetBadgesByLevel(4)
		end
		NGUITools.SetActive(self.uiDetailPanel.gameObject,true)
		NGUITools.SetActive(self.uiMainPanel.gameObject,false)
		NGUITools.SetActive(self.uiCloseBtn.gameObject,false)
		local tempAllgoods = self:GetBadgesByLevel(level)
		print("TempAllGoods.lenght:",#tempAllgoods)
		CommonFunction.ClearGridChild(self.uiDetailGrid.transform)
		for i,v in ipairs(tempAllgoods) do
			local t = getLuaComponent(createUI("BadgeDecomposeGoodsItem",self.uiDetailGrid.transform))
			t:SetId(v)
		end
		self.uiDetailGrid:Reposition()
		self.uiDetailScrollView:ResetPosition()
	end
end

function BadgeDecomposeByGroupWindow:GetBadgesByLevel(level)
	local allGoods = MainPlayer.Instance.BadgeGoodsList
	local group = {}
	local enum = allGoods:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current.Value
		if enumToInt(v:GetQuality()) == level then
			if MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(v:GetID()) > 0 then
				table.insert(group,v:GetID())
			end
		end
	end
	return group
end

function BadgeDecomposeByGroupWindow:OnClickHanlder( ... )
	return function()
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

function BadgeDecomposeByGroupWindow:OnClose( ... )
	GameObject.Destroy(self.gameObject)
end

function BadgeDecomposeByGroupWindow:GetEncomposeReslutGoodNumByGoodsId(goodsId)
	local goodsNum = MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(goodsId)
	local attrConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodsId)
	local useReslutId = attrConfig.use_result_id
	local useConfig = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(useReslutId)
	local id = useConfig.args:get_Item(0).id
	--local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(id)
	--local awardValue = AwarConfig.awards:get_Item(0).award_value
	return useConfig.args:get_Item(0).num_min*goodsNum
end

function BadgeDecomposeByGroupWindow:GetDecomposeReslutIdByGoodsId(goodsId)
	local attrConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodId)
	local useReslutId = attrConfig.use_result_id
	local useConfig = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(useReslutId)
	local id = useConfig.args:get_Item(0).id
	return id
	--local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(id)
	--local awardId = AwarConfig.awards:get_Item(0).award_id
	--return awardId
end

return BadgeDecomposeByGroupWindow
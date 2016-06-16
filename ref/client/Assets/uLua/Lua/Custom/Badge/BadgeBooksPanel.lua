------------------------------------------------------------------------
-- class name    : BadgeBooksPanel
-- create time   : 19:41 3-8-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgeBooksPanel ={
	uiName = "BadgeBooksPanel",
	---------params----------
	allslotsConfigData = nil,
	allslotsItems ={},
	currentRightPanelMode,
	tempSlotSelectId=nil,
	isInited = false,
	badgeDataList = nil,
	isShowEquipmentPanel = false,
	isShowChangePanel = false,
	isRefreshEquipBadgeList = false,

	------UI------
	uiBadgeSlotContainer,
	uiBooksMainInfoPanel,---默认界面
	uiBooksReplacePanel,---切换徽章界面
	uiBooksEquipmentPanel,---装备徽章界面（主要显示徽章列表）
	uiBooksReplacePanelBadgeIcon,
	-----UI for Equimpent Panel,
	uiEquipmentScrollView,
	uiEpuipmentGrid,
	uiEquipWrapContext,
	uiEquipmentWarpContent,
	uiNoLabel,
	----UI for Change Panel
	uiChangePanel,
	uiChangeIcon,
	uiChangeAttrGrid,
	uiChangeScrollView,
	uiChangeItemGrid,
	uiChangeGoodsNameLabel,
}

local PanelMode =
{
	MAIN_INFO_PANEL = 1,
	REPACE_PANEL	= 2,
	EQUIPMENT_PANEL = 3,
	CHANGE_PANEL 	= 4,
}

--------------Awake--------------
function BadgeBooksPanel:Awake( ... )
	self:UIParse()
end
---------------Start--------------
function BadgeBooksPanel:Start( ... )
	self:AddEvent()
	self:RegisterMsgHanlder()
	self.uiEquipmentWarpContent.onInitializeItem = self:UpdateBadgeItemList()
	-- allBadgeConfigData = GameSystem.Instance.GoodsConfigData:GetGoodsDicByCategory(10)
	-- self.RegisterMsgHanlder()
end

--------------Update-----------------
function BadgeBooksPanel:Update( ... )
	-- body
end

--------------FixedUpdate----------------
function BadgeBooksPanel:FixedUpdate( ... )

	if self.isShowEquipmentPanel == true then
		self.isShowEquipmentPanel = false
		self:ShowEquipmentPanel()
	end
	if self.isRefreshEquipBadgeList == true then
		self.isRefreshEquipBadgeList = false
		self:RefreshEquipBadgeList()
	end
	if self.isShowChangePanel == true then
		self.isShowChangePanel = false
		self:ShowChangePanel()
	end
end

--------------OnDestory-----------------
function BadgeBooksPanel:OnDestroy( ... )
	-- body
end

function BadgeBooksPanel:UIParse( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	----RightSide Panel
	self.uiBooksMainInfoPanel = find("Right/MainInfo")
	self.uiBooksReplacePanel = find("Right/Replace")
	self.uiBooksEquipmentPanel  = find("Right/Equipment")
	self.uiBooksReplacePanelBadgeIcon = getLuaComponent(createUI("BadgeIcon",self.uiBooksReplacePanel.transform:FindChild("Icon")))

	self.uiBadgeSlotContainer = find("Middle/WearArea")
	self.uiEquipmentScrollView = find("Right/Equipment/ScrollView"):GetComponent("UIScrollView")
	self.uiEpuipmentGrid = find("Right/Equipment/ScrollView/Grid"):GetComponent("UIGrid")
	self.uiEquipmentWarpContent = find("Right/Equipment/ScrollView/Grid"):GetComponent("UIWrapContent")
	self.uiNoLabel = find("Right/Equipment/NoLabel")

	self.uiChangePanel = find("Right/Change")
	self.uiChangeIcon = getLuaComponent(createUI("BadgeIcon",find("Right/Change/IconNode").transform))
	self.uiChangeAttrGrid = find("Right/Change/Grid"):GetComponent("UIGrid")
	self.uiChangeItemGrid = find("Right/Change/ScrollView/Grid"):GetComponent("UIGrid")
	self.uiChangeScrollView = find("Right/Change/ScrollView"):GetComponent("UIScrollView")
	self.uiChangeGoodsNameLabel = find("Right/Change/Name"):GetComponent("UILabel")
	for i = 0, 6 do
		local item = getLuaComponent(createUI("BadgeItem", self.uiEquipmentWarpContent.transform))
		item.gameObject.name = tostring(i)
		NGUITools.SetActive(item.gameObject, false)
	end
	self.uiEquipmentWarpContent:SortAlphabetically()
end

function BadgeBooksPanel:UpdateBadgeItemList( ... )
	return function(obj,index,realIndex)
		-- print("WrapContent List Update!***********************"..index.."**"..realIndex)
		if self.badgeDataList == nil then
			-- print("has NO badgeDatalist")
			return
		end
		realIndex = math.abs(realIndex)
		local useIndex = realIndex + 1
		if useIndex > table.getn(self.badgeDataList) then
			NGUITools.SetActive(obj, false)
			return
		end
		local badgeId = self.badgeDataList[useIndex]
		NGUITools.SetActive(obj, true)
		local t = getLuaComponent(obj)
		t:SetBadgeId(badgeId)
	end
end

function BadgeBooksPanel:InitAllSlotsItem( ... )
	if self.isInited == true then
		return
	end
	-- print("InitAllSlotsItem")
	self.isInited = true
	if self.allslotsConfigData == nil then
		if not BadgeSlotConfigData then
			BadgeSystemInfo = MainPlayer.Instance.badgeSystemInfo
			BadgeAttrData = GameSystem.Instance.BadgeAttrConfigData
			BadgeSlotConfigData = GameSystem.Instance.BadgeSlotsConfig
			GoodsConfigData = GameSystem.Instance.GoodsConfigData
		end
		self.allslotsConfigData = BadgeSlotConfigData:GetAllConfigs()
		-- print("allslotsConfigdataLenght()"..self.allslotsConfigData.Count)
		-- print("InitSlot")
		self.allslotsItems = {}
		local enum = self.allslotsConfigData:GetEnumerator()
		while enum:MoveNext() do
			local v = enum.Current.Value
			local slotitem = getLuaComponent(createUI("BadgeSlotItem", self.uiBadgeSlotContainer))
			slotitem.gameObject.name = "BadgeSlotItem" .. v.id
			slotitem.slotConfigData = v
			slotitem.parents = self
			table.insert(self.allslotsItems,slotitem)
		end
	end
end
--------初始化显示-------------
function BadgeBooksPanel:ShowPanel()
	-- print("BadgeBooksPanel:ShowPanel,the bookidis:")
	if 	BadgeSlotInfoUpDateCB == nil then
	 	BadgeSlotInfoUpDateCB =	self:UpdateSlotItemStatus()
	end
	self:InitAllSlotsItem()
	self:ResetAllSlotItem()
	self:UpdateSlotItemStatus()(0,false)
	self:ShowMainInfoPanel()()
	self:ShowUnlockEffect()
	self.uiBadgeSlotContainer:GetComponent("UIPanel"):Refresh()
end

function BadgeBooksPanel:ShowUnlockEffect()
	if NeedPlayUnlockEffectSlots then
		for k,v in pairs(NeedPlayUnlockEffectSlots) do
			self:GetSlotItemBySlotId(v):PlayAnimator()
		end
	end
	NeedPlayUnlockEffectSlots = nil
end

--更新指定的槽位信息,0表示所有槽位
function BadgeBooksPanel:UpdateSlotItemStatus()
	return function(slotId,playEffect)
		local slotsList = self:GetSlotsData()
		local count = slotsList.Count
		for i=1,count do
			local slotdata = slotsList:get_Item(i-1)
			if slotdata.id == slotId or slotId == 0 then
				local item = self:GetSlotItemBySlotId(slotdata.id)
				if item then
					item:SetSlotStatus(slotdata.status,slotdata.badge_id,playEffect)
				end
				if slotId~=0 then
					break
				end
			end
		end
		if self.currentRightPanelMode == MAIN_INFO_PANEL then
			self:UpdateMainInfoDataView()
		elseif self.currentRightPanelMode == EQUIPMENT_PANEL then
			self:RefreshEquipBadgeList()
		end
	end
end

function BadgeBooksPanel:UpdateMainInfoDataView()
	local totalLevelLabel = self.uiBooksMainInfoPanel.transform:FindChild("Num"):GetComponent("UILabel")
	totalLevelLabel.text = self:GetCurrentTotalBadgeLevel()
	local gridNode = self.uiBooksMainInfoPanel.transform:FindChild("ScrollView/Grid")
	CommonFunction.ClearGridChild(gridNode:GetComponent("UIGrid").transform)
	local attrs  = self:GetCurrentSlotProiveAttr()
	for k,v in pairs(attrs) do
		local attrName = GameSystem.Instance.AttrNameConfigData:GetAttrNameById(k)
		local attrItem = getLuaComponent(createUI("AttrInfo",gridNode.transform))
		attrItem:SetName(attrName)
		attrItem:SetValue("+"..v,true)
	end
	gridNode:GetComponent("UIGrid"):Reposition()
end

function BadgeBooksPanel:GetSlotsData( ... )
	local bookData = BadgeSystemInfo:GetBadgeBookByBookId(BadgeSystemVar.currentBookId)
	if bookData then
		-- print("--begin目前涂鸦页的槽位数据begin-- BOOKID:"..bookData.id.."BOOKName:"..bookData.name)
		local count = bookData.slot_list.Count
		-- for i=1,count do
		-- 	local slotdata = bookData.slot_list:get_Item(i-1)
		-- 	print("SLOTID:"..slotdata.id)
		-- 	print("Status:",slotdata.status)
		-- 	print("BadgeID:"..slotdata.badge_id)
		-- end
		-- print("--end目前涂鸦页的槽位数据end--")
		return bookData.slot_list
	end
	return nil
end

function BadgeBooksPanel:GetSlotRealDataBySlotId(slotId)
	local slotList = self:GetSlotsData()
	local count = slotList.Count
	for i=1,count do
		local slot = slotList:get_Item(i-1)
		if slot.id == slotId then
			return slot
		end
	end
end

function BadgeBooksPanel:ResetAllSlotItem()
	local count = #self.allslotsItems
	for i=1,count do
		self.allslotsItems[i]:Reset()
	end
end

function BadgeBooksPanel:ResetAllSelectStatus( ... )
	-- print("BadgeBooksPanel:ResetAllSlotItem")
	local count = table.getn(self.allslotsItems)
	-- print("count:"..count)
	for i=1,count do
		self.allslotsItems[i]:SetSelect(false)
	end
	 BadgeSystemVar.currentSelectSlotId = nil
	 self.tempSlotSelectId = nil
end

function BadgeBooksPanel:GetSlotItemBySlotId(slotid)
	local count = #self.allslotsItems
	for i=1,count do
		local item = self.allslotsItems[i]
		if item:GetID() == slotid then
			return item
		end
	end
end

function BadgeBooksPanel:SetAllPanelHide()
	if IsNil(self.uiBooksEquipmentPanel) == false then
		NGUITools.SetActive(self.uiBooksEquipmentPanel.gameObject,false)
	end
	if IsNil(self.uiBooksReplacePanel) == false then
		NGUITools.SetActive(self.uiBooksReplacePanel.gameObject,false)
	end
	if IsNil(self.uiBooksMainInfoPanel) == false then
		NGUITools.SetActive(self.uiBooksMainInfoPanel.gameObject,false)
	end
	if IsNil(self.uiChangePanel) == false then
		NGUITools.SetActive(self.uiChangePanel.gameObject,false)
	end
end

function BadgeBooksPanel:OnShowEquipmentClick()
	return function(isAll)
		if not FunctionSwitchData.CheckSwith(FSID.scrawl_wear) then return end
		self:ShowEquipmentPanel(isAll)
	end
end

function BadgeBooksPanel:ShowEquipmentPanel(isAll)
	if isAll then
		BadgeSystemVar.currentSelectSlotId = nil
	end
	self.currentRightPanelMode = PanelMode.EQUIPMENT_PANEL
	self:SetAllPanelHide()
	if IsNil(self.uiBooksEquipmentPanel) then
		self.uiBooksEquipmentPanel  = self.transform:FindChild("Right/Equipment")
	end
	NGUITools.SetActive(self.uiBooksEquipmentPanel.gameObject,true)
	-- local grid = self.transform:FindChild("Right/Equipment/ScrollView/Grid")
	self:RefreshEquipBadgeList()
end

function BadgeBooksPanel:RefreshEquipBadgeList()
	-- CommonFunction.ClearGridChild(self.uiEpuipmentGrid.transform)
	-- print("刷新涂鸦列表,列表类型是：",self:GetBadgeCategory())
	self.badgeDataList = self:GetBadgeListByCatetory(self:GetBadgeCategory())
	table.sort(self.badgeDataList, self:Sort())
	-- print("刷新涂鸦列表,列表长度：",#self.badgeDataList)
	-- for k,v in pairs(self.badgeDataList) do
	-- 	print("涂鸦Id是："..v)
	-- end
	NGUITools.SetActive(self.uiNoLabel.gameObject,#self.badgeDataList<=0)
	self.uiEquipmentWarpContent:SortAlphabetically()
	self.uiEquipmentScrollView:ResetPosition()
	local listCount = table.getn(self.badgeDataList)
	self.uiEquipmentWarpContent.minIndex = -listCount+1
	self.uiEquipmentWarpContent.maxIndex = 0
	local count = self.uiEquipmentWarpContent.transform.childCount
	-- print("BadgeItemCount:"..count)
	for i = 0, count - 1 do
		local go = self.uiEquipmentWarpContent.transform:GetChild(i).gameObject
		go.name = tostring(i)
		local t = getLuaComponent(go)
		t.equipCallback = self:EquipBadgeCallBack()
		self:UpdateBadgeItemList()(go, i, i)
	end
	self.uiEquipmentWarpContent:SortAlphabetically()
	self.uiEquipmentScrollView:ResetPosition()

	-- self.uiEquipmentWarpContent:WrapContent()
	-- local count = #datalist
	-- if count>0 then
	-- 	for i=1,count do
	-- 		-- 除去使用过后有剩余的
	-- 		local tempBadgeId = datalist[i]
	-- 		print("涂鸦ID:"..tempBadgeId.."涂鸦数量(所有的):"..MainPlayer.Instance:GetBadgesGoodByID(tempBadgeId):GetNum())
	-- 		local leftNum = BadgeSystemInfo:GetBadgeleftNumExceptUsed(tempBadgeId)
	-- 		print("涂鸦ID:"..tempBadgeId.."涂鸦数量(排除后的):"..leftNum)
	-- 		if leftNum>0 then
	-- 			local badgeGodos = MainPlayer.Instance:GetBadgesGoodByID(tempBadgeId)
	-- 			local item = getLuaComponent(createUI("BadgeItem",self.uiEpuipmentGrid.transform))
	-- 			item.badgeId = tempBadgeId
	-- 			item.equipCallback = self:EquipBadgeCallBack()
	-- 		end
	-- 	end
	-- 	self.uiEpuipmentGrid:Reposition()
	-- 	self.uiEquipmentScrollView:ResetPosition();
	-- end
end

function BadgeBooksPanel:Sort( ... )
	return function(id1,id2)
		local goods1 = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id1)
		local goods2 = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id2)
		return goods1.quality>goods2.quality
	end
end

function BadgeBooksPanel:GetBadgeCategory( ... )
	if not BadgeSystemVar.currentSelectSlotId then
		return BadgeCategory.CG_ALL
	else
		local slotconfig = BadgeSlotConfigData:GetConfig(BadgeSystemVar.currentSelectSlotId)
		return slotconfig.category
	end
end


-- function BadgeBooksPanel:UpdateBadgeItem( ... )
-- 	if self.currentRightPanelMode == PanelMode.EQUIPMENT_PANEL then
-- 		local childCount = self.uiEpuipmentGrid.transform.childCount
-- 		for i=1,childCount do
-- 			local item = getLuaComponent(self.uiEpuipmentGrid.transform:Find("BadgeItem(clone)"))
-- 			item:Refresh()
-- 		end
-- 	end
-- end

function BadgeBooksPanel:ShowReplacePanel()
	return function(badgeId)
		self.currentRightPanelMode = PanelMode.REPACE_PANEL
		self:SetAllPanelHide()
		NGUITools.SetActive(self.uiBooksReplacePanel.gameObject,true)
		local badgeNameLabel = self.uiBooksReplacePanel.transform:FindChild("BadgeNameLabel"):GetComponent("UILabel")
		local goods = MainPlayer.Instance:GetBadgesGoodByID(badgeId)
		self.uiBooksReplacePanelBadgeIcon:SetId(badgeId)
		badgeNameLabel.text = goods:GetName()
		local gridNode = self.uiBooksReplacePanel:FindChild("Grid")
		local attrT = getLuaComponent(gridNode)
		attrT:SetBadgeId(badgeId)
	end
end

function BadgeBooksPanel:ShowMainInfoPanel()
	return function(noreset)
		self.currentRightPanelMode = PanelMode.MAIN_INFO_PANEL
		self:SetAllPanelHide()
		NGUITools.SetActive(self.uiBooksMainInfoPanel.gameObject,true)
		self:UpdateMainInfoDataView()
		if noreset == true then return end
		self:ResetAllSelectStatus()
	end
end

function BadgeBooksPanel:OnShowChangeClick()
	return function()
		self:ShowChangePanel()
	end
end

function BadgeBooksPanel:ShowChangePanel()
	self.currentRightPanelMode = PanelMode.CHANGE_PANEL
	self:SetAllPanelHide()
	NGUITools.SetActive(self.uiChangePanel.gameObject,true)
	CommonFunction.ClearGridChild(self.uiChangeItemGrid.transform)
	local badgeSlot = BadgeSystemInfo:GetBadgeSlotByBookIdAndSlotId(BadgeSystemVar.currentBookId,BadgeSystemVar.currentSelectSlotId)
	if badgeSlot then
		local badgeId = badgeSlot.badge_id
		if badgeId~=0 then
			self.uiChangeIcon:SetId(badgeId)
			local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(badgeId)
			self.uiChangeGoodsNameLabel.text = goodsConfig.name
			local t = getLuaComponent(self.uiChangeAttrGrid.gameObject)
			t:SetBadgeId(badgeId)
			-- show list ---
			local datasList = self:GetBadgeListByCatetory(self:GetBadgeCategory())
			table.sort(datasList, self:Sort())
			for k,v in pairs(datasList) do
				-- print("v:",v,"K:",k)
				local item = createUI("BadgeItem",self.uiChangeItemGrid.transform)
				local tt = getLuaComponent(item)
				tt:SetBadgeId(v)
				tt.equipCallback = self:ChangeBadgeCallBack()
			end
			self.uiChangeItemGrid:Reposition()
			self.uiChangeScrollView:ResetPosition()
		end
	end
end

function BadgeBooksPanel:ChangeBadgeCallBack( ... )
	return function(badgeId)
		local req = {
			badge_id = badgeId,
			book_id = BadgeSystemVar.currentBookId,
			slot_id = BadgeSystemVar.currentSelectSlotId,
		}
		-- print("装备涂鸦,BOOK_ID:"..BadgeSystemVar.currentBookId.."BadgeID:"..badgeId)
		local buf = protobuf.encode("fogs.proto.msg.BadgeChangeReq",req)
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeChangeReqID,buf)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeChangeRespID,self:BadgeChangeRespHanlder(),self.uiName)
	end
end

function BadgeBooksPanel:BadgeChangeRespHanlder( ... )
	return function(buf)
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeChangeRespID,self.uiName)
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeChangeResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
			print("更换成功！")
			if self.currentRightPanelMode == PanelMode.CHANGE_PANEL then
				self:ShowChangePanel()
			end
		end
	end
end

function BadgeBooksPanel:BadgeSlotClickedHanlder( ... )
	return function()
		print(BadgeSystemVar.currentSelectSlotId,self:GetBadgeCategory())
		if self.tempSlotSelectId == BadgeSystemVar.currentSelectSlotId then
			return
		end
		self.tempSlotSelectId = BadgeSystemVar.currentSelectSlotId
		-- print("点击槽位,BOOK_ID:"..BadgeSystemVar.currentBookId.."SLOTID:"..BadgeSystemVar.currentSelectSlotId)
		local slotData = BadgeSystemInfo:GetBadgeSlotByBookIdAndSlotId(BadgeSystemVar.currentBookId,BadgeSystemVar.currentSelectSlotId)
		local badgeId = slotData.badge_id
		--如果槽位上的涂鸦
		if badgeId and badgeId ~= 0 then
			-- print("BadgeSlotClickedHanlder:HasBadge:"..badgeId)
			self:ShowReplacePanel()(badgeId)
		else
			-- print("BadgeSlotClickedHanlder:NoBadge")
			self:ShowEquipmentPanel()
		end
	end
end

--卸下涂鸦
function BadgeBooksPanel:UnequipBadge( ... )
	return function( ... )
		local slotData = BadgeSystemInfo:GetBadgeSlotByBookIdAndSlotId(BadgeSystemVar.currentBookId,BadgeSystemVar.currentSelectSlotId)
		local inlcude_badge_id = slotData.badge_id
		--如果槽位上的涂鸦
		if inlcude_badge_id and inlcude_badge_id~=0 then
			-- self:ShowReplacePanel(inlcude_badge_id)
			local req = {
				book_id = BadgeSystemVar.currentBookId,
				slot_id = BadgeSystemVar.currentSelectSlotId
			}
			-- print("卸下涂鸦,BOOK_ID:"..BadgeSystemVar.currentBookId.."SLOTID:"..BadgeSystemVar.currentSelectSlotId)
			CommonFunction.ShowWait()
			local buf = protobuf.encode("fogs.proto.msg.BadgeUnequipReq",req)
			LuaHelper.SendPlatMsgFromLua(MsgID.BadgeUnequipReqID,buf)
		end
	end
end

function BadgeBooksPanel:AddEvent( ... )
	addOnClick(self.transform:FindChild("Right/MainInfo/ButtonOK").gameObject,self:OnShowEquipmentClick(true))
	addOnClick(self.transform:FindChild("Right/Equipment/ButtonX").gameObject,self:ShowMainInfoPanel())
	addOnClick(self.transform:FindChild("Right/Replace/ButtonOK").gameObject,self:OnShowChangeClick())
	addOnClick(self.transform:FindChild("Right/Replace/ButtonX").gameObject,self:ShowMainInfoPanel())
	addOnClick(self.transform:FindChild("Right/Replace/ButtonRemove").gameObject,self:UnequipBadge())
	addOnClick(self.transform:FindChild("Right/Equipment/ButtonOK").gameObject,self:ToGetBadge())
	addOnClick(self.transform:FindChild("Right/Change/ButtonX").gameObject,self:ShowMainInfoPanel())
	addOnClick(self.transform:FindChild("Right/Change/ButtonOK").gameObject,self:ToGetBadge())
end


-----------------data handel function------------------
function BadgeBooksPanel:GetBadgeListByCatetory(catetory)
	local allBadgeGoods = MainPlayer.Instance.BadgeGoodsList
	local tempList = {}
	local count = allBadgeGoods.Count
	local enum = allBadgeGoods:GetEnumerator()
	while enum:MoveNext() do
		local badgeGoods = enum.Current.Value
		local cg = badgeGoods:GetBadgeCategory()
		if cg == catetory or catetory == BadgeCategory.CG_ALL then
			local num = BadgeSystemInfo:GetBadgeleftNumExceptUsed(badgeGoods:GetID(),BadgeSystemVar.currentBookId)
			if num>0 then
				table.insert(tempList,badgeGoods:GetID())
			end
		end
	end
	return tempList
end

function BadgeBooksPanel:ToGetBadge( ... )
	return function()
		if not FunctionSwitchData.CheckSwith(FSID.store) then return end

		TopPanelManager:ShowPanel("UILottery",nil,nil)
	end
end


function BadgeBooksPanel:GetCurrentSlotProiveAttr( ... )
	local count = #self.allslotsItems
	local attr = {}
	for i=1,count do
		local item = self.allslotsItems[i]
		local badgeId = item.badgeId
		if badgeId and badgeId~=0 then
			local badgeAttrConfigData = BadgeAttrData:GetBaseConfig(badgeId)
			if badgeAttrConfigData then
				local addAttrlist = badgeAttrConfigData.addAttr
				local enum = addAttrlist:GetEnumerator()
				while enum:MoveNext() do
					local attrId = enum.Current.Key
					local attrNum = enum.Current.Value
					if not attr[attrId] then
						attr[attrId] = attrNum
					else
						attr[attrId] = attr[attrId]+attrNum
					end
				end
			end
		end
	end
	return attr
end

function BadgeBooksPanel:GetCurrentTotalBadgeLevel( ... )
	local count = #self.allslotsItems
	local level = 0
	for i=1,count do
		local item = self.allslotsItems[i]
		local badgeId = item.badgeId
		if badgeId and badgeId~=0 then
			local badgeAttrConfigData = BadgeAttrData:GetBaseConfig(badgeId)
			level = level+badgeAttrConfigData.level
		end
	end
	return level
end
--------------------------------------message sender -----------------
function BadgeBooksPanel:EquipBadgeCallBack( ... )
	return function(badgeId)
		-- print("EquipBadgeCallBack:"..badgeId)
		local req = {
			badge_id = badgeId,
			book_id = BadgeSystemVar.currentBookId,
		}
		-- print("装备涂鸦,BOOK_ID:"..BadgeSystemVar.currentBookId.."BadgeID:"..badgeId)
		CommonFunction.ShowWait()
		local buf = protobuf.encode("fogs.proto.msg.BadgeEquipReq",req)
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeEquipReqID,buf)
	end
end

--------------------------------------message hanlder------------------------
function BadgeBooksPanel:RegisterMsgHanlder( ... )
	LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeEquipRespID,self:BadgeEquipRespHanlder(),self.uiName)
	LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeUnequipRespID,self:BadgeUnequipRespHanlder(),self.uiName)
end

function BadgeBooksPanel:BadgeEquipRespHanlder( ... )
	return function(buf)
		if not FunctionSwitchData.CheckSwith(FSID.scrawl_wear) then return end

		-- print("BadgeBooksPanel:BadgeEquipRespHanlder")
		CommonFunction.StopWait()
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeEquipResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
			if self.currentRightPanelMode == PanelMode.EQUIPMENT_PANEL then
				self.isRefreshEquipBadgeList = true
				--self:RefreshEquipBadgeList()
			elseif self.currentRightPanelMode == PanelMode.CHANGE_PANEL then
				self.isShowChangePanel = true
				--self:ShowChangePanel()()
			end
		end
	end
end

function BadgeBooksPanel:BadgeUnequipRespHanlder( ... )
	return function(buf)
	CommonFunction.StopWait()
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeUnequipResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			self.isShowEquipmentPanel = true
			--self:ShowEquipmentPanel()
		end
	end
end

return BadgeBooksPanel

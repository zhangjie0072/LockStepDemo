--encoding=utf-8

UIPackage =  {
	uiName = 'UIPackage',

	---------------------
	curSelectCategory,
	curSelectGoodsList,

	curSelectItemObj,
	curSelectGoods,
	infactGoodsListNum,

	msgReceiveTrigger,

	needReintialize = true,
	banTwice = false,
	repeatBuy = false,

	-------------------UI
	uiBtnBack,
	-- uiBtnMenu,

	uiLeft,
	uiGoodsDetail,
	uiIsNotEquipment,
	uiIsEquipment,
	uiBtnSell,

	uiRight,
	uiBtnCategoryAll,
	uiBtnCategoryEquipment,
	uiBtnCategoryConsume,
	uiBtnCategoryConsumeTip,
	uiBtnCategoryFavor,
	uiBtnCategoryFodder,

	uiGoodsListSV,
	uiGoodsListGrid,
	uiGoodsListSVIncLoad,

	uiAnimator,
	uiProgressBar,
	uiUpArrow,
	uiDownArrow,
	uiCompoundText,
	uiEmptyText,

	onTipRefresh,
}


-----------------------------------------------------------------
function UIPackage:Awake()
	local transform = self.transform

	self.uiBtnBack = transform:FindChild('Top/ButtonBack')
	-- self.uiBtnMenu = transform:FindChild('Top/ButtonMenu')

	self.uiLeft = transform:FindChild('Left')
	self.uiGoodsDetail = transform:FindChild('Left/GoodsDetail')
	self.uiIsNotEquipment = transform:FindChild('Left/GoodsDetail/IsNotEquipment')
	self.uiIsEquipment = transform:FindChild('Left/GoodsDetail/IsEquipment')
	self.uiBtnSell = transform:FindChild('Left/ButtonSell').gameObject

	self.uiRight = transform:FindChild('Right')
	self.uiBtnCategoryAll = transform:FindChild('Right/Grid/All').gameObject
	self.uiBtnCategoryEquipment = transform:FindChild('Right/Grid/Equipment').gameObject
	self.uiBtnCategoryConsume = transform:FindChild('Right/Grid/Consume').gameObject
	self.uiBtnCategoryConsumeTip = transform:FindChild('Right/Grid/Consume/Tip'):GetComponent('UISprite')
	self.uiBtnCategoryFavor = transform:FindChild('Right/Grid/Favor').gameObject
	self.uiBtnCategoryFodder = transform:FindChild('Right/Grid/Fodder').gameObject

	self.uiGoodsListSV = transform:FindChild('Right/ScrollView'):GetComponent('UIScrollView')
	self.uiGoodsListGrid = transform:FindChild('Right/ScrollView/Grid'):GetComponent('UIGrid')
	self.uiGoodsListSVIncLoad = self.uiGoodsListSV:GetComponent("ScrollViewAsyncLoadItem")
	self.uiEmptyText = transform:FindChild('Right/EmptyText')

	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiProgressBar = self.transform:FindChild('Right/Schedule'):GetComponent('UIScrollBar')
	self.uiUpArrow = self.transform:FindChild('Right/Up')
	self.uiDownArrow = self.transform:FindChild('Right/Down')
	self.uiCompound = self.transform:FindChild("Left/ButtonCompound")
	self.uiCompoundText = self.transform:FindChild("Left/ButtonCompound/Text"):GetComponent("MultiLabel")

	self.onTipRefresh = LuaHelper.Action(self:TipRefresh())
end

function UIPackage:Start()
	local backObj = createUI('ButtonBack', self.uiBtnBack)
	local backScript = getLuaComponent(backObj)
	backScript.onClick = self:OnClickBack()
	NGUITools.SetActive(self.uiBtnCategoryConsumeTip.gameObject, UpdateRedDotHandler.UpdateState["UIPackage"])
	-- local menuObj = createUI('ButtonMenu', self.uiBtnMenu)
	-- local menuScript = getLuaComponent(menuObj)
	-- menuScript:SetParent(self.gameObject, false)

	addOnClick(self.uiBtnSell, self:OnClickSell())
	addOnClick(self.uiCompound.gameObject, self:OnClickCompound())

	addOnClick(self.uiBtnCategoryAll, self:OnClickAll())
	addOnClick(self.uiBtnCategoryEquipment, self:OnClickEquipmnet())
	addOnClick(self.uiBtnCategoryConsume, self:OnClickConsume())
	addOnClick(self.uiBtnCategoryFavor, self:OnClickFavor())
	addOnClick(self.uiBtnCategoryFodder, self:OnClickFodder())

	-- 礼包小红点
	NGUITools.SetActive(self.uiBtnCategoryConsumeTip.gameObject, NeedGetGift)
end

function UIPackage:OnEnable()
	print('---------------------------UIBadge:OnEnable-----------------------')
	Scheduler.Instance:AddFrame(1, false, self.onTipRefresh)
end

function UIPackage:FixedUpdate()
	if self.curSelectGoodsList then
		local goodsNum = self.uiGoodsListGrid.transform.childCount
		if goodsNum <= 8 then
			NGUITools.SetActive(self.uiDownArrow.gameObject, false)
			NGUITools.SetActive(self.uiUpArrow.gameObject, false)
			return
		end
	end
	local value = self.uiProgressBar.value
	if value >= 0.90 and self:ShowDownArrow() == 2 then
		NGUITools.SetActive(self.uiDownArrow.gameObject, false)
		NGUITools.SetActive(self.uiUpArrow.gameObject, true)
	elseif value <= 0.10 and self:ShowUpArrow() == 1 then
		NGUITools.SetActive(self.uiDownArrow.gameObject, true)
		NGUITools.SetActive(self.uiUpArrow.gameObject, false)
	else
		NGUITools.SetActive(self.uiDownArrow.gameObject, true)
		NGUITools.SetActive(self.uiUpArrow.gameObject, true)
	end
end

function UIPackage:OnClose()
	self:DestroyHold()
	self.curSelectGoodsList = nil
	TopPanelManager:HideTopPanel()
end

function UIPackage:OnDestroy()
	--LuaHelper.UnRegisterPlatMsgHandler(MsgID.SellGoodsRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
	self:DestroyHold()
end

function UIPackage:Refresh()
	self.needReintialize = true
	if self.curSelectGoodsList then
		self:InitGoodsList(self.curSelectGoodsList)
	else
		if self.curSelectCategory == self.uiBtnCategoryAll then
			self:InitAll()
		else
			self:OnClickAll()(self.uiBtnCategoryAll)
		end
	end
end

function UIPackage:RefreshList()
	if self.curSelectCategory == self.uiBtnCategoryAll then
		self:InitAll()
	elseif self.curSelectCategory == self.uiBtnCategoryEquipment then
		self:InitEquipment()
	elseif self.curSelectCategory == self.uiBtnCategoryConsume then
		self:InitConsume()
	elseif self.curSelectCategory == self.uiBtnCategoryFavor then
		self:InitFavor()
	elseif self.curSelectCategory == self.uiBtnCategoryFodder then
		self:InitFodder()
	end
end

-----------------------------------------------------------------
function UIPackage:TipRefresh()
	return function ()
		print('---------------------------TipRefresh------------------')
		self.transform:GetComponent('UIPanel'):Refresh()
		local go = self.transform:FindChild('Top/Title/Shadow').gameObject
		NGUITools.SetActive(go, false)
		NGUITools.SetActive(go, true)
	end
end

function UIPackage:InitGoodsList(categoryList)
	self:DestroyHold()
	self.curSelectItemObj = nil
	self.curSelectGoods = nil
	if self.curSelectGoodsList ~= categoryList then
		self.curSelectGoodsList = categoryList
	end

	--判断是否需要重新初始化
	--if self.needReintialize == false then
		--return
	--end

	-- 不知道这个遍历存在的意义
	-- local goodsList = {}
	-- local enum = categoryList:GetEnumerator()
	-- while enum:MoveNext() do
	-- 	local goods = enum.Current.Value
	-- 	if (not goods:IsEquip()) or (goods:GetCategory() == GoodsCategory.GC_BADGE) then
	-- 		table.insert(goodsList, goods)
	-- 	end
	-- end
	-- self.infactGoodsListNum = table.getn(goodsList)

	-- --sort
	-- MainPlayer.Instance:SortGoodsDescExpPriority(categoryList)
	--init
	self.uiLeft.gameObject:SetActive(false)

	--返回值是2个，第1个是函数，第2个是需要处理的物品总数
	local goods_count = 0;
	self.uiGoodsListSVIncLoad.OnCreateItem, goods_count = self:MakeOnAcquireItem(categoryList)
	self.uiGoodsListSVIncLoad:Refresh(goods_count)

	self.needReintialize = false
end

function UIPackage:MakeOnAcquireItem(categoryList)
	local goodsList = {}
	local enum = categoryList:GetEnumerator()
	while enum:MoveNext() do
		local goods = enum.Current.Value
		if not goods:IsEquip() or (goods:GetCategory() == GoodsCategory.GC_BADGE) then
			-- 除去己经使用的涂鸦
			if not (goods:GetCategory() == GoodsCategory.GC_BADGE and MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(goods:GetID())<=0) then
				table.insert(goodsList, goods)
			end
		end
	end
	local goods_count = table.getn(goodsList)
	print(self.uiName, "Goods count:", goods_count)
	NGUITools.SetActive(self.uiEmptyText.gameObject,not (goods_count> 0))
	local isSelect = false

	local OnCreateItem = function (index, parent)
		index = index + 1
		if index > table.getn(goodsList) then
			return nil
		end
		local goods = goodsList[index]
		if self.curSelectGoods and goods:GetID() == self.curSelectGoods:GetID() and not self.curSelectItemObj.activeSelf then
			NGUITools.SetActive(self.curSelectItemObj, false)
			self.curSelectItemObj.transform.parent = self.uiGoodsListGrid.transform
			return self.curSelectItemObj
		end

		local item = getLuaComponent(createUI("GoodsIcon2", parent))
		item.isPackage = true
		item.gameObject.name = string.format("GoodsIcon2_%05d", index)
		item.isDisplayLevel = false
		item.goods = goods
		item.onClick = self:OnClickItem()
		item:Refresh()
		local info  = nil
		if goods:GetCategory() == GoodsCategory.GC_BADGE then
			local leftNum = MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(goods:GetID())
			info = {id = goods:GetID(), num = leftNum}
			item.uiLabelName1.text = string.format(getCommonStr("STR_FIELD_PROMPT45"),MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(goods:GetID()))
		else
			info = {id = goods:GetID(), num = goods:GetNum()}
			item.uiLabelName1.text = string.format(getCommonStr("STR_FIELD_PROMPT45"),goods:GetNum())
		end
		item:SetGoodsData(info)
		item:goodsRefresh()
		NGUITools.SetActive(item.uiLabelName1.transform.gameObject, true)
		local goodsNamePos = item.uiLabelName.transform.localPosition
		goodsNamePos.y = 75
		item.uiLabelName.transform.localPosition = goodsNamePos
		if isSelect == false then
			self:OnClickItem()(item)
			isSelect = true
		end
		return item.gameObject
	end

	return OnCreateItem, goods_count --返回值是2个，第1个是函数，第2个是需要处理的物品总数
end

function UIPackage:MakeOnDestroyItem()
	return function (index, object)
		if object == self.curSelectItemObj then
			-- self.curSelectItemObj = nil
		end
	end
end

--
function UIPackage:InitAll( ... )
	self:InitGoodsList(MainPlayer.Instance.AllGoodsList)
end

--
function UIPackage:InitEquipment( ... )
	local list = GoodsDictionary.New()
	-- local piece = MainPlayer.Instance.EquipmentPieceList
	local equip = MainPlayer.Instance.NewGoodsTabData
	local enum1 = equip:GetEnumerator()
	while enum1:MoveNext() do
		list:Add(enum1.Current.Key, enum1.Current.Value)
	end
	-- local enum2 = piece:GetEnumerator()
	-- while enum2:MoveNext() do
	--	list:Add(enum2.Current.Key, enum2.Current.Value)
	-- end
	self:InitGoodsList(list)
	-- self:InitGoodsList(MainPlayer.Instance.EquipmentPieceList)
end

--
function UIPackage:InitConsume( ... )
	self:InitGoodsList(MainPlayer.Instance.NewGiftTabData)
end

--
function UIPackage:InitFavor( ... )
	self:InitGoodsList(MainPlayer.Instance.NewBadgeTabData)
end

--
function UIPackage:InitFodder( ... )
	self:InitGoodsList(MainPlayer.Instance.MaterialList)
end

--
function UIPackage:InitDetail( ... )
	-- body
end

-------------------------------------------------------------------
function UIPackage:OnClickBack( ... )
	return function (go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIPackage:OnClickAll( ... )
	return function (go)
		if self.curSelectCategory == go then
			return
		end
		self.curSelectCategory = go
		go:GetComponent("UIToggle").value = true
		self:InitAll()
	end
end

function UIPackage:OnClickEquipmnet( ... )
	return function (go)
		if self.curSelectCategory == go then
			return
		end
		self.curSelectCategory = go
		self:InitEquipment()
	end
end

function UIPackage:OnClickConsume( ... )
	return function (go)
		if self.curSelectCategory == go then
			return
		end
		self.curSelectCategory = go

		-- 查看后重置背包小红点
		if NeedGetGift then
			NeedGetGift = false
			UpdateRedDotHandler.MessageHandler("Package")
			NGUITools.SetActive(self.uiBtnCategoryConsumeTip.gameObject, NeedGetGift)
		end
		self:InitConsume()
	end
end

function UIPackage:OnClickFavor( ... )
	return function (go)
		if self.curSelectCategory == go then
			return
		end
		self.curSelectCategory = go
		self:InitFavor()
	end
end

function UIPackage:OnClickFodder( ... )
	return function (go)
		if self.curSelectCategory == go then
			return
		end
		self.curSelectCategory = go
		self:InitFodder()
	end
end


function UIPackage:OnClickItem( ... )
	return function (script)
		if not script then return end

		self.uiLeft.gameObject:SetActive(true)

		if self.curSelectItemObj == script.gameObject then
			return
		end
		if self.curSelectItemObj then
			local goods = getLuaComponent(self.curSelectItemObj.gameObject)
			goods:SetSele(false)
			self:DestroyHold()
		end
		self.curSelectItemObj = script.gameObject
		self.curSelectGoods = script.goods
		script:SetSele(true)
		if self.curSelectGoods:GetCategory() == GoodsCategory.GC_CONSUME then
			if enumToInt(self.curSelectGoods:GetSubCategory()) ==  1 or
				enumToInt(self.curSelectGoods:GetSubCategory()) ==  2 then
				self.uiCompoundText:SetText(getCommonStr("STR_GOODSUSE"))
			end
		else
			self.uiCompoundText:SetText(getCommonStr("STR_COMPOUND"))
		end

		local detailObj
		if self.curSelectGoods:GetCategory() == GoodsCategory.GC_EQUIPMENT or self.curSelectGoods:GetCategory() == GoodsCategory.GC_BADGE then
			self.uiIsNotEquipment.gameObject:SetActive(false)
			self.uiIsEquipment.gameObject:SetActive(true)
			if self.uiIsEquipment.childCount > 0 then
				detailObj = self.uiIsEquipment:GetChild(0)
			end
			if detailObj == nil then
				detailObj = createUI('EquipmentDetail', self.uiIsEquipment)
			end

			local detailScript = getLuaComponent(detailObj)
			detailScript.goods = self.curSelectGoods
			detailScript:Refresh()
		else
			self.uiIsNotEquipment.gameObject:SetActive(true)
			self.uiIsEquipment.gameObject:SetActive(false)
			if self.uiIsNotEquipment.childCount > 0 then
				detailObj = self.uiIsNotEquipment:GetChild(0)
			end
			if detailObj == nil then
				detailObj = createUI('GoodsDetail', self.uiIsNotEquipment)
			end

			local detailScript = getLuaComponent(detailObj)
			--detailScript:SetData(self.curSelectGoods:GetID(), self.curSelectGoods:GetNum())
			detailScript:SetData(self.curSelectGoods)
		end

		self.uiBtnSell:SetActive(self.curSelectGoods:CanSell())
		if GameSystem.Instance.GoodsConfigData:CanComposite(script.id) then
			NGUITools.SetActive(self.uiCompound.gameObject, true)
			self.uiCompound:FindChild("Text"):GetComponent("MultiLabel"):SetText(getCommonStr("STR_COMPOUND"))
			--pos reset
			local pos = self.uiBtnSell.transform.localPosition
			pos.x = -79
			self.uiBtnSell.transform.localPosition = pos
			pos = self.uiCompound.transform.localPosition
			pos.x = 79
			self.uiCompound.transform.localPosition = pos
		elseif (self.curSelectGoods:GetCategory() == GoodsCategory.GC_CONSUME and enumToInt(self.curSelectGoods:GetSubCategory()) == 2) or
			self.curSelectGoods:GetCategory() == GoodsCategory.GC_BADGE or
			(self.curSelectGoods:GetCategory() == GoodsCategory.GC_CONSUME and enumToInt(self.curSelectGoods:GetSubCategory()) == 8) then
			NGUITools.SetActive(self.uiCompound.gameObject, true)
			self.uiCompound:FindChild("Text"):GetComponent("MultiLabel"):SetText(getCommonStr("STR_GOODSUSE"))
			--pos reset
			local pos = self.uiCompound.transform.localPosition
			pos.x = 57
			self.uiCompound.transform.localPosition = pos
		else
			NGUITools.SetActive(self.uiCompound.gameObject, false)
			--pos reset
			local pos = self.uiBtnSell.transform.localPosition
			pos.x = 57
			self.uiBtnSell.transform.localPosition = pos
			--碎片增加使用功能
			if self.curSelectGoods:GetCategory() == GoodsCategory.GC_FAVORITE then
				local list = Split(self.curSelectGoods:GetIcon(),'_')
				local has = MainPlayer.Instance:HasRole(list[3])
				if not has then
					NGUITools.SetActive(self.uiBtnSell.gameObject, false)
					NGUITools.SetActive(self.uiCompound.gameObject, true)
					self.uiCompound:FindChild("Text"):GetComponent("MultiLabel"):SetText(getCommonStr("STR_GOODSUSE"))
					pos = self.uiCompound.transform.localPosition
					pos.x = 57
					self.uiCompound.transform.localPosition = pos
				end
			end
		end



	end
end

function UIPackage:OnClickSell( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.bag_sell) then return end
		if self.banTwice then
			return
		end
		local sellObj = createUI("PackageSell")--, self.transform)
		self.script = getLuaComponent(sellObj)
		self.script.good_item = getLuaComponent(self.curSelectItemObj)
		self.script.goods = self.curSelectGoods
		self.script.goodsId = self.curSelectGoods:GetID()
		self.script.on_click_sell = self:DoSell(self.script)
		self.script.onClose = function ( ... )
			self.banTwice = false
		end
		UIManager.Instance:BringPanelForward(sellObj)

		self.msgReceiveTrigger = self.script:click_close()
		self.banTwice = true
	end
end

function UIPackage:OnClickCompound(...)
	return function(go)
		if not FunctionSwitchData.CheckSwith(FSID.bag_use) then return end

		local goods = self.curSelectGoods
		if self.banTwice then
			return
		end
		self.banTwice = true
		if GameSystem.Instance.GoodsConfigData:CanComposite(goods:GetID()) then
			local obj = getLuaComponent(createUI("CompoundPopup"))
			obj.costID = goods:GetID()
			obj.onClick = self:DoCompound(goods)
			obj.onClose = function ( ... )
				self.needReintialize = true
				self:RefreshList()
				self.banTwice = false
			end
			obj.costGoods = goods
			UIManager.Instance:BringPanelForward(obj.gameObject)
			self.compoundPopup = obj
		elseif goods:GetCategory() == GoodsCategory.GC_BADGE then
			TopPanelManager:ShowPanel("UIBadge",1,nil)
			self.banTwice = false
		elseif enumToInt(goods:GetSubCategory()) == 8 then
			TopPanelManager:ShowPanel("UIBadge",2,nil)
			self.banTwice = false
		--碎片增加使用功能
		elseif goods:GetCategory() == GoodsCategory.GC_FAVORITE then
			local detailPanel = TopPanelManager:ShowPanel("NewRoleDetail",nil,nil)
            if detailPanel ~= nil then
            	local list = Split(goods:GetIcon(),'_')
                detailPanel.id = list[3]
                detailPanel:SetLeftRightBtnsVisible(false)
                if detailPanel.roleData then
                	detailPanel:RefreshData()
                end
            end
            self.banTwice = false
		else
			if goods:GetNum() < 5 then
				local box = getLuaComponent(createUI('TreasureBox'))
				box.goods = goods
				box.goodsNum = 1
				box.onClose = function ( ... )
					self:RefreshList()
					self.banTwice = false
				end
				if NeedGetGift then
					UpdateRedDotHandler.MessageHandler("Package")
					NGUITools.SetActive(self.uiBtnCategoryConsumeTip.gameObject, NeedGetGift)
					NeedGetGift = false
				end
			else
				local sellObj = createUI("PackageSell")--, self.transform)
				self.script = getLuaComponent(sellObj)
				self.script.isTreasureBox = true
				self.script.good_item = getLuaComponent(self.curSelectItemObj)
				self.script.goods = self.curSelectGoods
				self.script.goodsId = self.curSelectGoods:GetID()
				self.script.onOpenBox = function ( ... )
					local box = getLuaComponent(createUI('TreasureBox'))
					box.goods = goods
					print('cur_num = ', self.script.ps_info.cur_num)
					box.goodsNum = self.script.ps_info.cur_num
					box.onClose = function ( ... )
						self:RefreshList()
					end
					self.banTwice = false
					NGUITools.Destroy(sellObj.gameObject)
				end
				self.script.onClose = function ( ... )
					self.banTwice = false
				end
				UIManager.Instance:BringPanelForward(sellObj)

				self.msgReceiveTrigger = self.script:click_close()
			end
		end
	end
end


function UIPackage:DoCompound(goods)
	return function()
		print(self.uiName,":compound req")
		local operation = {
			uuid = goods:GetUUID(),
			category = goods:GetCategory():ToString()
		}
		print('uuid = ', operation.uuid)
		CommonFunction.ShowWaitMask()
		local req = protobuf.encode("fogs.proto.msg.CompositeGoods",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.CompositeGoodsID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.CompositeGoodsRespID, self:CompositeGoodsRespHandle(goods), self.uiName)
		CommonFunction.SHowWait()
	end
end

function UIPackage:CompositeGoodsRespHandle(goods)
	return function(buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.CompositeGoodsRespID, self.uiName)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, error = protobuf.decode('fogs.proto.msg.CompositeGoodsResp',buf)
		if resp then
			if resp.result == 0 then
				local id = GameSystem.Instance.GoodsConfigData:GetComposite(goods:GetID()).dest_id
				local name = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).name
				local str = getCommonStr("STR_COMPOSITE_SUCSECC"):format(tostring(name))
				CommonFunction.ShowPopupMsg(str,nil,nil,nil,nil,nil)
				-- self.needReintialize = true
				-- self:RefreshList()
				self.compoundPopup:Refresh()
				-- if self.compoundPopup then
				--	self.compoundPopup:Close()()
				-- end
				-- self.compoundPopup = nil
			else
				if self.compoundPopup then
					self.compoundPopup:Close()()
				end
				self.needReintialize = true
				self:RefreshList()
				self.banTwice = false
				-- self.compoundPopup = nil
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				playSound("UI/UI-wrong")
			end
			-- self.banTwice = false
		end
	end
end

function UIPackage:DoSell(script)
	return function (go)
		print(self.uiName,"--sell req:",self.curSelectGoodsList.Count)
		if self.repeatBuy then
			return
		end
		self.repeatBuy = true
		local operation ={
			info = {
				{
					uuid = self.curSelectGoods:GetUUID(),
					num = script.ps_info.cur_num,
					category = self.curSelectGoods:GetCategory():ToString()
				}
			}
		}
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
		local req = protobuf.encode("fogs.proto.msg.SellGoods",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.SellGoodsID,req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.SellGoodsRespID, self:SellGoodsRespHandle(), self.uiName)
	end
end

function UIPackage:SellGoodsRespHandle( ... )
	return function(buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.SellGoodsRespID, self.uiName)
		CommonFunction.HideWaitMask()
		CommonFunction.StopWait()
		local resp, error = protobuf.decode('fogs.proto.msg.SellGoodsResp',buf)
		if resp then
			if resp.result == 0 then
				local id = self.curSelectGoods:GetID()
				local name = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).name
				local sellId = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).sell_id
				local sellType = '金币'
				if sellId then
					sellType = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(sellId).name
				end
				local num = tostring(self.script.ps_info.goodsConsume.rewardNum)--:GetPrice())
				local str = getCommonStr("ST_SELL_REWARDS_GOLDS"):format(tostring(name), num, sellType)
				CommonFunction.ShowPopupMsg(str,nil,nil,nil,nil,nil)
				self.needReintialize = true
				print(self.uiName,"--sell resp:",self.curSelectGoodsList.Count)
				self:RefreshList()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				playSound("UI/UI-wrong")
			end
		end
		if self.msgReceiveTrigger then
			self.msgReceiveTrigger()
		end
		self.banTwice = false
		self.repeatBuy = false
	end
end

function UIPackage:ShowUpArrow( ... )
	local parentGrid = self.uiGoodsListGrid.transform
	local beforeChild = parentGrid:GetChild(0).gameObject
	local beforeName = beforeChild.gameObject.name
	local beforeIndex = string.sub(beforeName, -5, -1)
	if tonumber(beforeIndex) == 1 then
		return 1
	end
	return 0
end

function UIPackage:ShowDownArrow( ... )
	local parentGrid = self.uiGoodsListGrid.transform
	local lastChild = parentGrid:GetChild(parentGrid.childCount - 1).gameObject
	local lastName = lastChild.gameObject.name
	local lastIndex = string.sub(lastName, -5, -1)
	if tonumber(lastIndex) == self.infactGoodsListNum then
		return 2
	end
	return 0
end


function UIPackage:DestroyHold()
	if self.curSelectItemObj and not self.curSelectItemObj.activeSelf then
		NGUITools.Destroy(self.curSelectItemObj)
		self.curSelectItemObj = nil
	end
end

return UIPackage

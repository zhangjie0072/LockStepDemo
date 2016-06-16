GoodsAcquirePopup = {
	uiName = 'GoodsAcquirePopup',
	----------------UI
	uiConfirmBtn,
	uiGoodsScrollView,
	uiGoodsGrid,
	uiContent,
	uiContent1,
	uiContent2,
	uiOpenBox,
	----------------parameters
	id,
	num,
	isRoleRecruit = false,
	tourUse = false,
	onClose,
	jumpToPractise = false,
	nextMaps = nil,
	BoxsList,--宝箱id
	--
	goods,
	goodsNum,
	awards,
	newPlayerID = {},
}

function GoodsAcquirePopup:Awake()
	self.uiAnimator        = self.transform:GetComponent("Animator")
	self.uiConfirmBtn      = self.transform:FindChild('ButtonOK'):GetComponent('UIButton')
	self.uiGoodsScrollView = self.transform:FindChild("Goods/List"):GetComponent("UIScrollView")
	self.uiGoodsGrid       = self.transform:FindChild('Goods/List/Grid'):GetComponent('UIGrid')
	self.uiContent         = self.transform:FindChild('Goods/Content'):GetComponent('UILabel')
	self.uiContent1        = self.transform:FindChild("Goods/Content1"):GetComponent('UILabel')
	self.uiContent2        = self.transform:FindChild("Goods/Content2"):GetComponent('UILabel')
	self.uiOpenBox			= self.transform:FindChild("ButtonOpenBox2"):GetComponent('UIButton')
	self.uiBackTip         = self.transform:FindChild("Background/BackTip"):GetComponent("MultiLabel")
	self.goodsList         = {}
	self.BoxsList         = {}
	end

function GoodsAcquirePopup:Start()
	if not self.tourUse then
		self.uiBackTip:SetText(getCommonStr('STR_GOODS_ACQUIRE'))
	else
		self.uiBackTip:SetText(getCommonStr('STR_TOUR_COMPLETE_TITLE'))
		NGUITools.SetActive(self.uiContent1.gameObject, true)
	end
	addOnClick(self.uiConfirmBtn.gameObject, self:OnConfirmClick())
	self:Refresh()
end

function GoodsAcquirePopup:Refresh()
	self.uiGoodsGrid:Reposition()
	self.uiGoodsGrid.onReposition = function ()
		if self.uiGoodsGrid.transform.childCount > 5 then -- multi line
			self.uiGoodsScrollView.contentPivot = UIWidget.Pivot.Top
		end
		self.uiGoodsScrollView:ResetPosition()
	end
end

function GoodsAcquirePopup:OpenTreaureBox()
	-- print("????????????????????????GoodsAcquirePopup:OpenTreaureBox")
	if not FunctionSwitchData.Cancel(FSID.goods) then
		self:OnClose()
		return
	end

	if #self.BoxsList ==0 then
		self:OnClose()
		print("a1")
		return
	end
	local enum = MainPlayer.Instance.NewGiftTabData:GetEnumerator()
	local isOpen=false
	while enum:MoveNext() do
		local goods = enum.Current.Value
		for k,v in pairs(self.BoxsList) do
			if v.goodsID == goods:GetID() then
				self.goods =goods
				self.goodsNum = v.num
				table.remove(self.BoxsList,k)
				local info = self.uiAnimator:GetCurrentAnimatorStateInfo(0)
				local openNum
				if self.goodsNum then
					openNum = self.goodsNum
				else
					openNum = self.goods:GetNum()
				end
				print('openNum = ', openNum)
				local info =
				{
					uuid = self.goods:GetUUID(),
					category = self.goods:GetCategory():ToString(),
					num = openNum,
				}
				LuaHelper.RegisterPlatMsgHandler(MsgID.UseGoodsRespID, self:OpenTreasureRespHandle(), self.uiName)
				local req = protobuf.encode("fogs.proto.msg.UseGoods", info)
				LuaHelper.SendPlatMsgFromLua(MsgID.UseGoodsID, req)
				CommonFunction.ShowWait()
				isOpen=true
				print("a2")
				break;
			end
		end
		if isOpen==true then
			break;
		end
	end

	NeedGetGift = false

	if isOpen==false then
		 error("没有对应宝箱")
	end
end

function GoodsAcquirePopup:TreasureBoxPopup()
	print("GoodsAcquirePopup:TreasureBoxPopup")
	local awardsPopup = getLuaComponent(createUI('TreasureBoxPopup'))
	awardsPopup.totalAwards = self.awards
	if self.newPlayerID then
		awardsPopup.newPlayerID = self.newPlayerID
	end
	UIManager.Instance:BringPanelForward(awardsPopup.gameObject)
	--teamupgradepopup depth == 20
	if awardsPopup.transform:GetComponent('UIPanel').depth <= 20 then
		awardsPopup.transform:GetComponent('UIPanel').depth = 21
	end
	awardsPopup.onClose = function () self:OpenTreaureBox() end
end


function GoodsAcquirePopup:OnClose( ... )
	print("GoodsAcquirePopup.BoxsList",#self.BoxsList)

	if self.onClose then
		self.onClose()
	end
	if self.nextMaps then
        local goodsAcquire = getLuaComponent(createUI('GoodsAcquirePopup'))
    	goodsAcquire:SetNewMapData(self.nextMaps)
    	if MainPlayer.Instance.NewMapIDList.Count > 0 then
    		goodsAcquire.nextMaps = MainPlayer.Instance.NewMapIDList:get_Item(0)
    	end
        UIManager.Instance:BringPanelForward(goodsAcquire.gameObject)
        self.nextMaps = nil
    elseif #self.BoxsList>0 then
    	-- print ("open box-----------")
		self:OpenTreaureBox()
		return
	end
	if self.jumpToPractise == true then
		jumpToUI("UIPractice")
	end

	print(self.uiName,":------destroy")

	NGUITools.Destroy(self.gameObject)


	-- if self.isRoleRecruit == true then
		-- local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)
		-- CommonFunction.ShowPopupMsg(getCommonStr('STR_ROLE_RECTUIT_AWARDS'):format(goodsConfig.name, self.num),nil,nil,nil,nil,nil)
	-- end
end

function GoodsAcquirePopup:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function GoodsAcquirePopup:OnConfirmClick()
	return function ()
		self.uiOpenBox.gameObject:SetActive(false);
		-- print("GoodsAcquirePopup animator:",self.uiAnimator)
		if self.uiAnimator then
			print("GoodsAcquirePopup animation close")
			self:AnimClose()
		else
			print("GoodsAcquirePopup close")
			self:OnClose()
		end
	end
end

function GoodsAcquirePopup:SetGiftGoodsData(id)
	local goods_use_config = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(id)
	local args = goods_use_config.args
	local enum = args:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current
		local gu_id = v.id
		local gu_num  = v.num_max
		local g = createUI("GoodsIcon",self.uiGoodsGrid.transform)
		local t = getLuaComponent(g)
		t.goodsID = gu_id
		t.num = gu_num
		t.hideLevel = true
		t.hideNum = false
		t.hideNeed = true
		self.goodsList[gu_id] = gu_num
	end
	self.uiGoodsGrid:Reposition()
	self.uiGoodsScrollView:ResetPosition()
end

function GoodsAcquirePopup:SetGoodsData(id, num)
	local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	if not goodsConfig then error("Goods config not found. ID:", id) end
	local g = createUI("GoodsIcon",self.uiGoodsGrid.transform)
	local t = getLuaComponent(g)
	t.goodsID = goodsConfig.id
	t.num = num
	t.hideLevel = true
	t.hideNum = false
	t.hideNeed = true
	if goodsConfig.category == 4 and tonumber(goodsConfig.sub_category)==2 then
		self.uiOpenBox.gameObject:SetActive(true);
		table.insert(self.BoxsList, t)

		NeedGetGift = true
	end
end


function GoodsAcquirePopup:SetContent(str)
	NGUITools.SetActive(self.uiContent.gameObject, true)
	self.uiContent.text = str
end

function GoodsAcquirePopup:HasGoods(id)
	local num = self.goodsList[id]
	return num ~= nil and num > 0
end

function GoodsAcquirePopup:SetNewMapData(id)
	local gridPos = self.uiGoodsGrid.transform.localPosition
	gridPos.y = 0
	self.uiGoodsGrid.transform.localPosition = gridPos
	local config = GameSystem.Instance.MapConfig:GetMapGroupDataByID(id)
	local roleList = config.groupIDs
	for i = 1, roleList.Count do
		local goodsLua = getLuaComponent(createUI("GoodsIcon", self.uiGoodsGrid.transform))
		goodsLua.goodsID = roleList:get_Item(i - 1)
		goodsLua.hideLevel = true
		goodsLua.hideNum = true
		goodsLua.hideNeed = true
	end

	NGUITools.SetActive(self.uiContent.gameObject, true)
	NGUITools.SetActive(self.uiContent2.gameObject, true)
	self.uiContent.text = string.format(getCommonStr('GETNEWMAP'), string.format("[00CD00]%s[-]", config.groupName))
	self.uiContent2.text = config.describe

	MainPlayer.Instance.NewMapIDList:Remove(id)
end


function GoodsAcquirePopup:OpenTreasureRespHandle()
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName)
		local resp = protobuf.decode("fogs.proto.msg.UseGoodsResp", message)
		CommonFunction.StopWait()
		print('resp.result = ', resp.result)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		self.awards = resp.awards
		self:TreasureBoxPopup()
	end
end


return GoodsAcquirePopup

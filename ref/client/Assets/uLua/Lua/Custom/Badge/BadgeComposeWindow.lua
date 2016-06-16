------------------------------------------------------------------------
-- class name    : BadgeComposeWindow
-- create time   : 17:13 3-10-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgeComposeWindow =
{
	uiName = "BadgeComposeWindow",
	leftPanelisShow = false,
	leftPanelisBorn = false,
	badgeConfigData = nil,
	refreshHanlder = nil,
	isCanbeCompose = false,
	-----UI-----------
	uiCloseBtn,
	uiExchangeBtn,
	uiComposeBtn,
	uiAnimator,
	---right panel---
	uiRightGoodsIcon,
	uiRightBadgeNameLabel,
	uiRightBadgeDesLabel,
	uiRightBadgeTypeLabel,
	uiRightBadgeMaterialNumLabel,
	uiHasNumLabel,
	----leftPanel
	uiLeftPanel,
	uiLeftMaterialGrid,
	uiLeftButtonOk,
	uiLeftBtnLabel,
	uiLeftBadgeNameLabel,
	uiLeftGoodsIcon,
}

function BadgeComposeWindow:Awake( ... )
	-- body
	self:UIParise()
end

function BadgeComposeWindow:Start( ... )
	-- body
	self:AddEvent()
	self:RefreshRightPanel()
end
--------------Update-----------------
function BadgeComposeWindow:Update( ... )
	-- body
end

--------------FixedUpdate----------------
function BadgeComposeWindow:FixedUpdate( ... )
	-- body
end

function BadgeComposeWindow:OnDestroy( ... )
	-- body
end

function BadgeComposeWindow:RefreshWindow( ... )
	return function()
		print("RefreshComposeWindow")
		self:RefreshRightPanel()
		self:RefreshLeftPanel()
	end
end

function BadgeComposeWindow:UIParise( ... )
	-- body
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiAnimator = transform:GetComponent("Animator")
	self.uiCloseBtn = createUI("ButtonClose",find("Right/ButtonClose"))
	self.uiExchangeBtn = find("Right/ButtonChange"):GetComponent("UIButton")
	self.uiComposeBtn = find("Right/ButtonEnhance")
	self.uiLeftPanel = find("Left")
	self.uiRightGoodsIcon = getLuaComponent(createUI("BadgeIcon",find("Right/GoodsIcon").transform))
	self.uiRightBadgeNameLabel = find("Right/Name1"):GetComponent("UILabel")
	self.uiRightBadgeDesLabel = find("Right/Descript"):GetComponent("UILabel")
	self.uiRightBadgeTypeLabel = find("Right/Name2"):GetComponent("UILabel")
	self.uiLeftMaterialGrid = find("Left/AttrParticular/AttrGrid")
	self.uiLeftButtonOk = find("Left/ButtonEnhanceAuto")
	self.uiLeftBtnLabel = find("Left/ButtonEnhanceAuto/Text"):GetComponent("MultiLabel")
	self.uiAnimator = self.transform:GetComponent("Animator")
	self.uiRightBadgeMaterialNumLabel = find("Right/Middle/Num"):GetComponent("UILabel")
	self.uiLeftBadgeNameLabel = find("Left/AttrParticular/Name"):GetComponent("UILabel")
	self.uiLeftGoodsIcon = getLuaComponent(createUI("BadgeIcon",self.uiLeftPanel.transform:FindChild("GoodsIcon").transform))
	self.uiHasNumLabel = find("Right/Have"):GetComponent("UILabel")
end

function BadgeComposeWindow:AddEvent( ... )
	-- body
	addOnClick(self.uiCloseBtn.gameObject,self:OnClickClose())
	addOnClick(self.uiExchangeBtn.gameObject,self:OnEncomposeBadge())
	addOnClick(self.uiComposeBtn.gameObject,self:OnOpenComposePanel())
	addOnClick(self.uiLeftButtonOk.gameObject,self:OnComposeHandler())

end

function BadgeComposeWindow:OnComposeHandler( ... )
	return function()
		if not self.isCanbeCompose then
			IsGotoLotteryFromBadgeStorePanel = true
			local t = TopPanelManager:ShowPanel("UILottery",2,nil)
			t.refreshCallBack = self:RefreshWindow()
			return
		end
		local req = {
			dest_id = self.badgeConfigData.id
		}
		-- print("涂鸦合成请求：要合成的ID是：",req.dest_id)
		CommonFunction.ShowWait()
		local buf = protobuf.encode("fogs.proto.msg.BadgeComposeReq",req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeComposeRespID,self:BadgeComposeHanlder(),self.uiName)
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeComposeReqID,buf)
	end
end

function BadgeComposeWindow:OnClickClose( ... )
	return function()
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

function BadgeComposeWindow:OnClose()
	GameObject.Destroy(self.gameObject)
end

function BadgeComposeWindow:OnEncomposeBadge( ... )
	return function()
		if not FunctionSwitchData.CheckSwith(FSID.scrawl_disintegrate) then return end

		local leftNum = BadgeSystemInfo:GetBadgeLeftNumExceptAllUsed(self.badgeConfigData.id)
		if leftNum <=0 then
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("NOT_ENOUGH_BADGE_DECOMPOSE"),nil,nil,nil,nil,nil)
			return
		end
		local req = {
			badge_id = self.badgeConfigData.id
		}
		-- print("涂鸦分解请求：要分解的ID是：",req.badge_id)
		CommonFunction.ShowWait()
		local buf = protobuf.encode("fogs.proto.msg.BadgeDecomposeReq",req)
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeDecomposeRespID,self:BadgeDecomposeHanlder(),self.uiName)
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeDecomposeReqID,buf)
	end
end

function BadgeComposeWindow:OnOpenComposePanel( ... )
	return function()
		-- print("doing******************here")
		-- NGUITools.SetActive(self.uiLeftPanel.gameObject,true)
		if not FunctionSwitchData.CheckSwith(FSID.scrawl_make) then return end

		if self.uiAnimator then
			if not self.leftPanelisShow then
				self.leftPanelisShow = true
				self.uiAnimator.enabled = true
				if not self.leftPanelisBorn then
					self.uiAnimator:SetBool("Leftout", true)
					self.leftPanelisBorn = true
				else
					self.uiAnimator:SetBool("Switch", false)
				end
				self:RefreshLeftPanel()
			else
				self.leftPanelisShow = false
				self.uiAnimator.enabled = true
				self.uiAnimator:SetBool("Switch", true)
			end

		end
	end
end

function BadgeComposeWindow:RefreshRightPanel( ... )
	self.uiRightGoodsIcon:SetId(self.badgeConfigData.id)
	self.uiRightBadgeNameLabel.text = self.badgeConfigData.name
	local currentGoodsId = self.badgeConfigData.id
	local goodsBaseConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(currentGoodsId)
	-- self.uiLeftPanel.transform:FindChild("GoodsIcon"):GetComponent("UISprite").spriteName = goodsBaseConfig.icon
	local quality = goodsBaseConfig.quality
	-- self.uiRightBadgeNameLabel.color = getQualityColorNew(quality)
	self.uiRightBadgeDesLabel.text = self.badgeConfigData.purpose
	self.uiRightBadgeTypeLabel.text = self:GetTypeStringByBadgeId(self.badgeConfigData.id)
	local t = getLuaComponent(self.transform:FindChild("Right/Middle/NextAttr/AttrGrid"))
	t:SetBadgeId(self.badgeConfigData.id)
	self.uiHasNumLabel.text =string.format(CommonFunction.GetConstString("GOODS_OWNED_NUM2"),MainPlayer.Instance:GetGoodsCount(self.badgeConfigData.id))
	local id,awardValue = self:GetEncomposeReslutGoodsIdAndGoodNumByGoodsId(self.badgeConfigData.id)
	self.uiRightBadgeMaterialNumLabel.text = awardValue

	local goods = MainPlayer.Instance:GetBadgesGoodByID(self.badgeConfigData.id)
	if not goods then
		self.uiExchangeBtn.isEnabled = false
		return
	end
	-- local leftNum = BadgeSystemInfo:GetBadgeLeftNumExceptAllUsed(self.badgeConfigData.id)
	local leftNum = goods:GetNum()
	if leftNum<=0 then
		self.uiExchangeBtn.isEnabled = false
	else
		self.uiExchangeBtn.isEnabled = true
	end
end

function BadgeComposeWindow:RefreshLeftPanel( ... )
	local currentGoodsId = self.badgeConfigData.id
	local goodsBaseConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(currentGoodsId)
	-- self.uiLeftPanel.transform:FindChild("GoodsIcon"):GetComponent("UISprite").spriteName = goodsBaseConfig.icon
	self.uiLeftGoodsIcon:SetId(goodsBaseConfig.id)
	self.uiLeftBadgeNameLabel.text = goodsBaseConfig.name
	local quality = goodsBaseConfig.quality
	-- self.uiLeftBadgeNameLabel.color = getQualityColorNew(quality)
	local composeBadgeConfig = GameSystem.Instance.GoodsComposeNewConfigData:GetBaseConfig(currentGoodsId)
	CommonFunction.ClearGridChild(self.uiLeftMaterialGrid)
	if composeBadgeConfig then
		reqireMaterialsList = composeBadgeConfig.needIDs
		local emun = reqireMaterialsList:GetEnumerator()
		local isOk = true
		while emun:MoveNext() do
			local itemdata = emun.Current
			-- print("需要的物口ID"..itemdata.Key.."需要的物品数量："..itemdata.Value)
			local materialItem = getLuaComponent(createUI("BadgeMaterial",self.uiLeftMaterialGrid))
			materialItem:SetMaterialId(itemdata.Key)
			materialItem:SetNeedNum(itemdata.Value)
			if isOk then
				isOk = materialItem.isOk
			end
		end
		-- print("合成材料可以不:",isOk)
		self.uiLeftMaterialGrid:GetComponent("UIGrid"):Reposition()
		if isOk then
			self.uiLeftBtnLabel:SetText(CommonFunction.GetConstString("STR_COMPOUND"))
			self.isCanbeCompose = true
		else
			self.uiLeftBtnLabel:SetText(CommonFunction.GetConstString("STR_BADGE_GET"))
			self.isCanbeCompose = false
		end
	end
end

function BadgeComposeWindow:GetTypeStringByBadgeId(badgeId)
	local goodsBaseConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(badgeId)
	if goodsBaseConfig then
		local typeString = GetBadgeTypeStringByCategory(goodsBaseConfig.sub_category)
		return typeString
	end
	return ""
end


function BadgeComposeWindow:BadgeDecomposeHanlder( ... )
	return function(buf)
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeDecomposeRespID,self.uiName)
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeDecomposeResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			-- CommonFunction.ShowPopupMsg("分解成功",nil,nil,nil,nil,nil)
			local id, num = self:GetEncomposeReslutGoodsIdAndGoodNumByGoodsId(self.badgeConfigData.id)
			print("encompose info:"..id.."num:"..num)
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			getGoods:SetGoodsData(id, num)
			self:RefreshWindow()()
			if self.refreshHanlder then
				self.refreshHanlder()
			end
		end
	end
end

function BadgeComposeWindow:BadgeComposeHanlder( ... )
	return function(buf)
		CommonFunction.StopWait()
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeComposeRespID,self.uiName)
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeComposeResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
			getGoods:SetGoodsData(self.badgeConfigData.id, 1)
			self.uiRightGoodsIcon:PlayAnimator()
			self:RefreshWindow()()
			if self.refreshHanlder then
				self.refreshHanlder()
			end
		end
	end
end

function BadgeComposeWindow:GetEncomposeReslutGoodsIdAndGoodNumByGoodsId(goodId)
	local attrConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(goodId)
	local useReslutId = attrConfig.use_result_id
	local useConfig = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(useReslutId)
	local id = useConfig.args:get_Item(0).id
	local value = useConfig.args:get_Item(0).num_min
	return id, value
	--local AwarConfig = GameSystem.Instance.AwardPackConfigData:GetAwardPackByID(id)
	--local awardValue = AwarConfig.awards:get_Item(0).award_value
	--local awardId = AwarConfig.awards:get_Item(0).award_id
	--return awardId,awardValue
end

return BadgeComposeWindow
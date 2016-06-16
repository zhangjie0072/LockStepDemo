TreasureBox = {
	uiName = 'TreasureBox',
	----------------UI
	uiButtonOK,
	uiAnimator,
	----------------parameters
	goods,
	goodsNum,
	onClose,
	awards,
	newPlayerID = {},
}

function TreasureBox:Awake()
	self.uiButtonOK = self.transform:FindChild('ButtonOK')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function TreasureBox:Start()
	addOnClick(self.uiButtonOK.gameObject, self:OnOpenTreasure())
	LuaHelper.RegisterPlatMsgHandler(MsgID.UseGoodsRespID, self:OpenTreasureRespHandle(), self.uiName)
end

function TreasureBox:Refresh()
end

function TreasureBox:OnDestroy( ... )
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function TreasureBox:OnClose( ... )
	if self.onClose then self.onClose() end

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

	NeedGetGift = false
	UpdateRedDotHandler.MessageHandler("Package")

	NGUITools.Destroy(self.gameObject)
end

function TreasureBox:DoClose( ... )
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function TreasureBox:OnOpenTreasure( ... )
	return function (go)
		local info = self.uiAnimator:GetCurrentAnimatorStateInfo(0)
		if not info:IsName("TrBoxP_Loop") then
			return
		end
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
		local req = protobuf.encode("fogs.proto.msg.UseGoods", info)
		LuaHelper.SendPlatMsgFromLua(MsgID.UseGoodsID, req)
		CommonFunction.ShowWait()
	end
end

function TreasureBox:OpenTreasureRespHandle( ... )
	return function (message)
		local resp = protobuf.decode("fogs.proto.msg.UseGoodsResp", message)
		CommonFunction.StopWait()
		print('resp.result = ', resp.result)
		if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		self.awards = resp.awards
		self:DoClose()
	end
end

function TreasureBox:GetNewRole(id)
	if not self.newPlayerID[id] then
		self.newPlayerID[id] = 1
	end
end

return TreasureBox

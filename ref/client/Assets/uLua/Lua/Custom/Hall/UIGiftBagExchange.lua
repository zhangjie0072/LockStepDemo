UIGiftBagExchange = {
	uiName = "UIGiftBagExchange",
	-----UI--------
	uiInputLabel,
	uiBtnClose,
	uiBtnOk,
	uiAnimator,
}

function UIGiftBagExchange:Awake( ... )
	self:UIParise()
	self:AddEvent()
end

function UIGiftBagExchange:Start( ... )

end

function UIGiftBagExchange:FixedUpdate( ... )
	-- body
end

function UIGiftBagExchange:OnDestroy( ... )
	-- body
end

function UIGiftBagExchange:UIParise( ... )
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiInputLabel = find("Window/Input"):GetComponent("UIInput")
	--self.uiInputLabel.finallyLimit = 5
	self.uiBtnClose = createUI('ButtonClose',self.transform:FindChild("Window/ButtonClose"))
	self.uiBtnOk = find("Window/ButtonOK")
	self.uiAnimator = transform:GetComponent("Animator")
end

function UIGiftBagExchange:AddEvent( ... )
	addOnClick(self.uiBtnClose.gameObject,self:OnCloseClickHanlder())
	addOnClick(self.uiBtnOk.gameObject,self:OnOkClickHanlder())
end


function UIGiftBagExchange:OnCloseClickHanlder( ... )
	return function()
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

function UIGiftBagExchange:OnOkClickHanlder( ... )
	return function()

		if not FunctionSwitchData.CheckSwith(FSID.gift_btn) then return end

		local str = tostring(self.uiInputLabel.value)
		if str == '' then
			CommonFunction.ShowTip("请输入礼包码[-]",nil)
			return
		end
		MainPlayer.Instance:SendGiftExchangeCode( str, 'http://223.202.94.26:22888/recharge/giftCode.php', nil)
	end
end

function UIGiftBagExchange:OnClose( ... )
	GameObject.Destroy(self.gameObject)
end

return UIGiftBagExchange
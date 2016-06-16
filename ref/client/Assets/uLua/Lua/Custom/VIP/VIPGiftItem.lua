------------------------------------------------------------------------
-- class name	: VIPGiftItem
-- create time   : 20150805_102213
------------------------------------------------------------------------


VIPGiftItem =  {
	uiName	 = "VIPGiftItem", 
	on_click = nil,

	-- state
	state_buy_available   = 1,
	state_buy_bought	  = 2,
	state_buy_unavailable = 3,
	-----------------------UI
	uiCheck,
	uiCheckLabel,
	uiBtnBuy,
}




function VIPGiftItem:Awake()
	self.go = {} 		-- create this for contained gameObject
	self.go.name = self.transform:FindChild("Name"):GetComponent('MultiLabel')	--:GetComponent("UILabel")
	self.go.describe = self.transform:FindChild("Vip"):GetComponent('MultiLabel')	--:GetComponent("UILabel")
	self.go.cost = self.transform:FindChild("ButtonBuy/Text"):GetComponent('MultiLabel')	--:GetComponent("UILabel")	
	--self.go.bg = self.transform:FindChild("BG"):GetComponent("UIButton")
	self.uiCheck = self.transform:FindChild('ButtonCheck'):GetComponent('UIButton')
	self.uiCheckLabel = self.transform:FindChild('ButtonCheck/Text'):GetComponent('MultiLabel')
	self.uiBtnBuy = self.transform:FindChild('ButtonBuy'):GetComponent('UIButton')
	self.config = GameSystem.Instance.GoodsConfigData
end



function VIPGiftItem:CheckClick()
	return function()
		local vipGiftDetail = getLuaComponent(createUI('VipGiftItemContent'))
		vipGiftDetail:SetTip(getCommonStr("STR_VIP_CAN_BUY_BELOW"))
		vipGiftDetail:SetData(self.id)
	end
end

function VIPGiftItem:OnGetClick( ... )
	return function()
		if self.on_click then
			self.on_click(self)
		end
	end
end

function VIPGiftItem:OnDestroy()
	
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function VIPGiftItem:selected(selected)
	local go = self.transform:FindChild("Sele").gameObject
	NGUITools.SetActive(go,selected)
end

function VIPGiftItem:Start()
	addOnClick(self.uiCheck.gameObject,self:CheckClick())
	self.uiCheckLabel:SetText(getCommonStr('STR_EXAMINE'))
	addOnClick(self.uiBtnBuy.gameObject, self:OnGetClick())
end


function VIPGiftItem:set_data(id,vip,cost,level)
	self.id = id
	self.vip = vip
	self.cost = cost
	self.level = level
	
	local attr = self.config:GetgoodsAttrConfig(self.id)
	self.go.name:SetText(attr.name)
	self.intro = attr.intro

	self.go.cost:SetText(tostring(cost))

	if self.level >= vip then
		self.go.describe:SetText("VIP " .. tostring(vip) .. "  "..getCommonStr("STR_BUY_AVAILABLE"))
		self.go.describe:GetComponent('UILabel').color = Color.white
		self.state = VIPGiftItem.state_buy_available
	else
		self.go.describe:SetText("VIP " .. tostring(vip))
		self.go.describe:GetComponent('UILabel').color = Color.red
		self.state = VIPGiftItem.state_buy_unavailable
	end
	
end

function VIPGiftItem:set_bought()
	print("set_bought is called")
	self.go.describe:SetText("VIP " .. tostring(self.vip) .. "  "..getCommonStr("STR_BOUGHT"))
	self.go.describe:GetComponent('UILabel').color = Color.white
	self.state = VIPGiftItem.state_buy_bought
	--self.go.bg.normalSprite = "com_bg_pure_round_5pix_gray"
	
end


-- uncommoent if needed
-- function VIPGiftItem:FixedUpdate()

-- end


-- uncommoent if needed
-- function VIPGiftItem:Update()
	

-- end




return VIPGiftItem

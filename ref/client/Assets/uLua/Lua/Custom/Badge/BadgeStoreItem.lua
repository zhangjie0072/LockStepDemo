------------------------------------------------------------------------
-- class name    : BadgeStoreItem
-- create time   : 14:20 3-9-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgeStoreItem = {
	uiName = "BadgeStoreItem",
	storeConfigData = nil,
	itemSelectHanlder,
	-----------UI------------
	uiNameLabel,
	uiBackTypeMask,
	uiGoodsIcon,
	SelectHanlder,
	sprGoodsIcon,
}


function BadgeStoreItem:Awake( ... )
	-- body
	self:UIParise()
end

function BadgeStoreItem:Start( ... )
	self:RefreshView()
	addOnClick(self.transform.gameObject,self:ShowBadgeComposeWindow())
end

function BadgeStoreItem:RefreshView()
	if self.storeConfigData ~=nil then
		self.uiGoodsIcon:SetId(self.storeConfigData.id)
		self.uiNameLabel.text = self.storeConfigData.name
		local goodsAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.storeConfigData.id)
		local quality = goodsAttr.quality
		--self.uiNameLabel.color = getQualityColorNew(quality)
		self:UpdateNum()
		local t = getLuaComponent(self.transform:FindChild("Grid"))
		t:SetBadgeId(self.storeConfigData.id)
	end
end

function BadgeStoreItem:UpdateNum( ... )
	if self.storeConfigData then
		local badgeGoods =  MainPlayer.Instance:GetBadgesGoodByID(self.storeConfigData.id)
		-- local leftNum =BadgeSystemInfo:GetBadgeLeftNumExceptAllUsed(self.storeConfigData.id)
		if badgeGoods then
			local leftNum = badgeGoods:GetNum()
			if leftNum>0 then 
				self.uiGoodsIcon:SetNum(leftNum)
				self.sprGoodsIcon.color = Color.New(1,1,1,1)
				NGUITools.SetActive(self.uiBackTypeMask.gameObject,false)
			else
				NGUITools.SetActive(self.uiBackTypeMask.gameObject,false)
				self.sprGoodsIcon.color = Color.New(0,1,1,1)
				self.uiGoodsIcon:SetNum("")
			end
		else
			NGUITools.SetActive(self.uiBackTypeMask.gameObject,false)
			self.sprGoodsIcon.color = Color.New(0,1,1,1)
			self.uiGoodsIcon:SetNum("")
		end
	end
end

function BadgeStoreItem:OnDestroy( ... )
	-- body
end

function BadgeStoreItem:UIParise( ... )
	-- body
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end
	self.uiGoodsIcon = getLuaComponent(createUI("BadgeIcon",find("IconNode")))
	self.sprGoodsIcon = self.uiGoodsIcon.transform:FindChild("Icon"):GetComponent("UISprite")
	self.uiNameLabel = find("Name"):GetComponent("UILabel")
	self.uiBackTypeMask = find("BackType"):GetComponent("UISprite")
end

function BadgeStoreItem:ShowBadgeComposeWindow( ... )
	-- body
	return function()
		if self.itemSelectHanlder then
			self.itemSelectHanlder(self.storeConfigData)
		end
		-- local window = createUI("BadgeComposeWindow")
		-- local t = getLuaComponent(window.gameObject)
		-- t.badgeConfigData = self.storeConfigData
		-- UIManager.Instance:BringPanelForward(window)
	end
end

return BadgeStoreItem
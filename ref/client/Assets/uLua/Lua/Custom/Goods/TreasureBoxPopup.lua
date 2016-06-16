TreasureBoxPopup = {
	uiName = 'TreasureBoxPopup',
	----------------UI
	uiAnimator,
	uiTenGrid,
	uiFiveGrid,
	uiFourGrid,
	uiButtonOK,
	----------------parameters
	totalAwards,
	num = 0,
	currentNum = 0,
	awardsList = {},
	awardsIndex = 1,
	parentGrid,
	onClose,
	waitTime = 0.25,
	isLoop = false,
	newPlayerID,
}

function TreasureBoxPopup:Awake()
	self.uiTenGrid = self.transform:FindChild('Goods/10')
	self.uiFiveGrid = self.transform:FindChild('Goods/5')
	self.uiFourGrid = self.transform:FindChild('Goods/4')
	self.uiButtonOK = self.transform:FindChild('ButtonOK')
	self.uiAnimator = self.transform:GetComponent('Animator')
end

function TreasureBoxPopup:Start()
	addOnClick(self.uiButtonOK.gameObject, self:OnClick())
	local preIndex = 1
	for k,v in pairs(self.totalAwards) do
		local value = { id = v.id, num = v.value}
		self.awardsList[preIndex] = value
		preIndex = preIndex + 1
		self.num = self.num + 1
	end
	self:Refresh()
end

function TreasureBoxPopup:FixedUpdate( ... )
	if self.isLoop and self.waitTime > 0 then
		--self.isLoop = false
		self.waitTime = self.waitTime - UnityTime.fixedDeltaTime
	end
	if self.waitTime <= 0 then
		self.waitTime = 0.25
		self.isLoop = false

		local pos = 0
		while self.currentNum > 0 do
			local goods = getLuaComponent(createUI('GoodsIcon', self.parentGrid.transform:FindChild('GoodsIcon' .. pos).transform))
			goods.goodsID = self.awardsList[self.awardsIndex].id
			goods.num = self.awardsList[self.awardsIndex].num
			goods.hideNeed = true
			goods.hideLevel = true
			pos = pos + 1
			self.awardsIndex = self.awardsIndex + 1
			self.currentNum = self.currentNum - 1
			self.num = self.num - 1
			if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goods.goodsID) then

				local popup = getLuaComponent(createUI("RoleAcquirePopup"))
				popup:SetData(goods.goodsID)
				if self.newPlayerID and self.newPlayerID[goods.goodsID] and self.newPlayerID[goods.goodsID] == 1 then
					popup.IsInClude = false
					self.newPlayerID[goods.goodsID] = nil
				else
					popup.IsInClude = true
					local roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goods.goodsID)
					if roleData then
						local roleName = roleData.name
						local Num = roleData.recruit_output_value
						popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
					end
				end
			end
		end
	end
end

function TreasureBoxPopup:Refresh()
	self.currentNum = self.num
	print('self.num = ', self.num)
	-- print('self.awardsIndex = ', self.awardsIndex)
	-- print('#self.awardsList = ', #self.awardsList)
	-- print('self.awardsIndex >= #self.awardsList ?', self.awardsIndex >= #self.awardsList)
	if self.num <= 0 then --or self.awardsIndex > #self.awardsList then
		self:DoClose()
		return
	end

	--clear grid
	local pos = 0
	if self.parentGrid then
		while pos < self.parentGrid.transform.childCount do
			local child = self.parentGrid.transform:FindChild('GoodsIcon' .. pos).transform
			NGUITools.Destroy(child:GetChild(0).gameObject)
			pos = pos + 1
		end
	end

	if self.num > 5 then
		self.parentGrid = self.uiTenGrid
		if self.num > 10 then
			self.currentNum = 10
		end
	elseif self.num % 2 == 1 then
		self.parentGrid = self.uiFiveGrid
	elseif self.num % 2 == 0 then
		self.parentGrid = self.uiFourGrid
	end

	if not self.isLoop then
		pos = 0
		while self.currentNum > 0 do
			local goods = getLuaComponent(createUI('GoodsIcon', self.parentGrid.transform:FindChild('GoodsIcon' .. pos).transform))
			goods.goodsID = self.awardsList[self.awardsIndex].id
			goods.num = self.awardsList[self.awardsIndex].num
			goods.hideNeed = true
			goods.hideLevel = true
			pos = pos + 1
			self.awardsIndex = self.awardsIndex + 1
			self.currentNum = self.currentNum - 1
			self.num = self.num - 1

			if GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goods.goodsID) then
				local popup = getLuaComponent(createUI("RoleAcquirePopup"))
				popup:SetData(goods.goodsID)
				if self.newPlayerID and self.newPlayerID[goods.goodsID] and self.newPlayerID[goods.goodsID] == 1 then
					popup.IsInClude = false
					self.newPlayerID[goods.goodsID] = nil
				else
					popup.IsInClude = true
					local roleData = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(goods.goodsID)
					if roleData then
						local roleName = roleData.name
						local Num = roleData.recruit_output_value
						popup.contentStr = string.format(getCommonStr('STR_ROLE_SIGN_AWARDS'), roleName, roleName, Num)
					end
				end
			end
		end
	end
end

function TreasureBoxPopup:OnClose( ... )
	if self.onClose then
		self.onClose()
	end
	NGUITools.Destroy(self.gameObject)
end

function TreasureBoxPopup:DoClose( ... )
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function TreasureBoxPopup:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function TreasureBoxPopup:OnClick( ... )
	return function (go)
		NeedGetGift = false

		if not self.isLoop then
			if self.uiAnimator then
				self.uiAnimator:SetBool('Loop', true)
				self.isLoop = true
			end
			self:Refresh()
		end
	end
end

return TreasureBoxPopup

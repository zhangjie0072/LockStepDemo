--encoding=utf-8

StoreEntrance =
{
	uiName = 'StoreEntrance',
	
	pos,
	goodsItem,

	rotationReset = false,

	backUI,

	--------------------------

	uiRotation,
	uiBlackStore,
	uiSkillStore,
};


-----------------------------------------------------------------
--Awake
function StoreEntrance:Awake()
	local transform = self.transform
	
	addOnClick(self.gameObject, self:OnStoreClick())
	--Rotation Component
	self.uiRotation = transform:FindChild('Detail'):GetComponent('TweenRotation')

	self.uiRotation:ResetToBeginning()
	local uiSkillStore = transform:FindChild('Detail/Skill/Sprite')
	--add eventListener to black store button
	addOnClick(uiSkillStore.gameObject, self:OnSkillStoreClick())

	local uiBlackStore = transform:FindChild('Detail/Black/Sprite')
	--add eventListener to skill store button
	addOnClick(uiBlackStore.gameObject, self:OnBlackStoreClick())

end

--Start
function StoreEntrance:Start()
	-- body
end

--Update
function StoreEntrance:Update( ... )
	-- body
end

--Refresh
function StoreEntrance:Refresh( ... )
	if self.rotationReset == true then
		self.uiRotation:ResetToBeginning()
	end
	self.rotationReset = false
	UIStore:SetBackUI(self.backUI.uiName)
end


-----------------------------------------------------------------
--
function StoreEntrance:SetBackUI(backUI)
	self.backUI = backUI
end

---
function StoreEntrance:OnStoreClick()
	return function (go)
		--[[
		-- if GameSystem.Instance.FunctionConditionConfig:ValidateFunc(UIStore.uiName) == false then
		-- 	CommonFunction.ShowPopupMsg(GameSystem.Instance.FunctionConditionConfig:GetFuncCondition(UIStore.uiName).lockTip)
		-- 	return
		-- end
		--]]
		if self.rotationReset == false then
			self.uiRotation:PlayForward()
			self.rotationReset = true
		else
			self.uiRotation:PlayReverse()
			self.rotationReset = false
		end
	end
end

--
function StoreEntrance:OnSkillStoreClick()
	return function (go)
		UIStore:SetType('ST_SKILL')
		if validateFunc('UIStore' or '') then
			UIStore:OpenStore()
		end
		self.uiRotation:ResetToBeginning()
		self.rotationReset = false
	end
end

--
function StoreEntrance:OnBlackStoreClick()
	return function (go)
		UIStore:SetType('ST_BLACK')
		if validateFunc('UIStore' or '') then
			UIStore:OpenStore()
		end
		self.uiRotation:ResetToBeginning()
		self.rotationReset = false
	end
end

return StoreEntrance

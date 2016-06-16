------------------------------------------------------------------------
-- class name    : BadgeSlotRelativeWindow
-- create time   : 21:37 3-9-2016
-- author        : Jackwu
------------------------------------------------------------------------
BadgeSlotRelativeWindow = 
{
	uiName = "BadgeSlotRelativeWindow",
	---------------params------------
	unlockCallBack = nil,
	addBookPageCallBack = nil,
	unlockData = nil,
	---------------UI----------------
	uiAddNewPanel,
	uiUnlockPanel,
	uiButtonClose,
	uiWindowTitle,
	uiUnlockButton,
	uiAddNewPageButton,
	uiAnimator,
	uiUnlockUseGoodsIcon,

}

-------------Awake--------------
function BadgeSlotRelativeWindow:Awake( ... )
	-- body
	self:UIParise()
end
-------------Start---------------
function BadgeSlotRelativeWindow:Start( ... )
	-- body
	self:AddEvent()
end
----------------parise UI Element----------
function BadgeSlotRelativeWindow:UIParise( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end
	self.uiButtonClose = createUI("ButtonClose",find("Window/ButtonClose").transform)
	self.uiAddNewPanel = find("Window/AddNew")
	self.uiUnlockPanel = find("Window/Unlock")
	self.uiUnlockButton = find("Window/Unlock/OK")
	self.uiAddNewPageButton = find("Window/AddNew/OK")
	self.uiAnimator = self.transform:GetComponent("Animator")
	self.uiUnlockUseGoodsIcon = find("Window/Unlock/GoodsIconConsume/GoodsIconConsume/Icon"):GetComponent("UISprite")
end
-----------------AddEventListener--------------
function BadgeSlotRelativeWindow:AddEvent( ... )
	-- body
	addOnClick(self.uiButtonClose.gameObject,self:OnClickClose())
	addOnClick(self.uiUnlockButton.gameObject,self:OnUnLockSlot())
	addOnClick(self.uiAddNewPageButton.gameObject,self:OnAddNewPage())
end

function BadgeSlotRelativeWindow:OnUnLockSlot( ... )
	return function()
		if self.unlockCallBack then
			if Consume.CheckConsume(self.unlockData.unlockCostGoodsId,self.unlockData.unlockCostGoodsNum) then
				self.unlockCallBack()
				self:OnClickClose()()
			end
		end
	end
end

function BadgeSlotRelativeWindow:OnAddNewPage( ... )
	return function()
		if self.addBookPageCallBack then
			if Consume.CheckConsume(1,100) then
				self.addBookPageCallBack()
				self:OnClickClose()()
			end
		end
	end
end
--------------ÏÔÊ¾½âËø´°¿Ú-----------
function BadgeSlotRelativeWindow:ShowUnLockPanel(slotdata)
	self.unlockData = slotdata
	local slotConfigData = slotdata
	local contextLabel = self.uiUnlockPanel.transform:FindChild("Text"):GetComponent("UILabel")
	contextLabel.text = string.format(CommonFunction.GetConstString("STR_FIELD_PROMPT23"),slotConfigData.requireLevel)
	local consumeNumLabel = self.uiUnlockPanel.transform:FindChild("GoodsIconConsume/GoodsIconConsume/Num"):GetComponent("UILabel")
	-- print("need cost money:"..slotConfigData.unlockCostGoodsNum)
	consumeNumLabel.text = slotConfigData.unlockCostGoodsNum
	local goodsConfig = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(slotConfigData.unlockCostGoodsId)
	if goodsConfig then
		self.uiUnlockUseGoodsIcon.spriteName = goodsConfig.icon
	end
	local okLabel = self.uiUnlockPanel.transform:FindChild("OK/Label"):GetComponent("UILabel")
	okLabel.text = CommonFunction.GetConstString("STR_BADGE_UNLOCK_NOW")
	NGUITools.SetActive(self.uiUnlockPanel.gameObject,true)
	NGUITools.SetActive(self.uiAddNewPanel.gameObject,false)
end

--------------ÏÔÊ¾Ìí¼ÓÐÂ»ÕÕÂ²á´°¿Ú-----
function BadgeSlotRelativeWindow:ShowAddNewPanel( ... )                                                                                                                                                                                                                                                                                                                                                                                                          
	NGUITools.SetActive(self.uiAddNewPanel.gameObject,true)
	NGUITools.SetActive(self.uiUnlockPanel.gameObject,false)
end

function BadgeSlotRelativeWindow:OnClickClose( ... )
	return function()
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function BadgeSlotRelativeWindow:OnClose( ... )
	GameObject.Destroy(self.gameObject)	
end

return BadgeSlotRelativeWindow
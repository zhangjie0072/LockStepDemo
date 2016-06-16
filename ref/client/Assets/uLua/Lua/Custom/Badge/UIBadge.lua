------------------------------------------------------------------------
-- class name    : UIBadge
-- create time   : 15:32 3-8-2016
-- author        : Jackwu
------------------------------------------------------------------------

UIBadge=UIBadge or
{
	uiName = "UIBadge",
	-------params-------
	currentViewPanel 	= nil;
	nextShowUI 			= nil,
	nextShowUISubID 	= nil,
	nextShowUIParams 	= nil,
	onClose 			= nil,
	currentStoreViewCategory = nil,
	hasInstance = false,
	------UI for Common-------
	uiBooksTagButton,
	uiStoreTagButton,
	uiBooksPanel,
	uiStorePanel,
	uiTopPanel,
	uiMainTitle,
	uiBtnMenu,
	uiBtnBack,
	uiAnimator,

	------UI for Books Module-----


}


function UIBadge:Awake()
	self:UIParse()
end

function UIBadge:Start()
	self:AddEvent()
	-- self.badgeSystemData= MainPlayer.Instance.badgeSystemInfo
	-- self.badgeSystemData:forTestData()
	-- BadgeSystemInfo:forTestData()
	local t = getLuaComponent(self.uiTopPanel)
	t:SetParent(self)
end

function UIBadge:FixedUpdate()

end

function UIBadge:OnDestroy()

end
-------侦听所有事件-------
function UIBadge:AddEvent()
	addOnClick(self.uiBtnBack.gameObject, self:ClickBack())
	addOnClick(self.uiBooksTagButton.gameObject,self:OnClickBooks())
	addOnClick(self.uiStoreTagButton.gameObject,self:OnClickStore())
end

function UIBadge:Refresh(subID)
	-- print("UIBadge:Refresh**********************************")
	self.currentViewPanel = nil
	HaveNewBadge = false
	UpdateRedDotHandler.MessageHandler("Badge")
	if not subID or subID == 1 or subID == 0 then
		if IsGotoLotteryFromBadgeStorePanel == false then
			self.uiBooksTagButton.transform:GetComponent("UIToggle").startsActive = true
			self.uiBooksTagButton.transform:GetComponent("UIToggle").value = true
			self:OnClickBooks()(self.uiBooksTagButton.gameObject)
		else
			self.uiStoreTagButton.transform:GetComponent("UIToggle").startsActive = true
			self.uiStoreTagButton.transform:GetComponent("UIToggle").value = true
			self:OnClickStore()(self.uiStoreTagButton.gameObject)
		end
		subID = nil
		IsGotoLotteryFromBadgeStorePanel = false
	elseif subID == 2 then
		self.uiStoreTagButton.transform:GetComponent("UIToggle").startsActive = true
		self.uiStoreTagButton.transform:GetComponent("UIToggle").value = true
		self:OnClickStore()(self.uiStoreTagButton.gameObject)
		subID = nil
	end
end

-- 切换到涂鸦墙页面,每次都显示第一页
function UIBadge:OnClickBooks()
	return function(go)
		if self.currentViewPanel == go then
			return
		end
		-- print("UIbadge *****************************")
		self.currentViewPanel = go
		local t = getLuaComponent(self.uiTopPanel)
		t:SetCurrentBookId(1)
		-- self:ShowBooksPanel(true)
	end
end

function UIBadge:OnClickStore()
	return function(go)
		if not FunctionSwitchData.CheckSwith(FSID.scrawl) then return end

		if self.currentViewPanel == go then
			return
		end
		self.currentViewPanel = go
		-- go:GetComponent("UIToggle").value =true
		self:ShowStorePanel()
	end
end

function UIBadge:UIParse()
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiBooksTagButton = find('Left/BooksBtn'):GetComponent('UISprite')
	self.uiStoreTagButton = find('Left/StoreBtn'):GetComponent('UISprite')
	self.uiBooksPanel = find("Book")
	self.uiStorePanel = find("Store")
	self.uiBtnBack =  createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack'))
	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiTopPanel = find("Top/DropDownArea")
end

function UIBadge:ShowBooksPanel(isDataChange)
	-- print("ShowBooksPanel()()()()()()()()()()()()")
	hasInstance = true
	NGUITools.SetActive(self.uiBooksPanel.gameObject,true)
	NGUITools.SetActive(self.uiStorePanel.gameObject,false)
	NGUITools.SetActive(self.uiTopPanel.gameObject,true)
	local t = getLuaComponent(self.uiBooksPanel)
	if isDataChange then
		t:ShowPanel()
	else
		t:ShowMainInfoPanel()()
	end
end

function UIBadge:ShowStorePanel()
	print("ShowStorePanel")
	NGUITools.SetActive(self.uiBooksPanel.gameObject,false)
	NGUITools.SetActive(self.uiStorePanel.gameObject,true)
	NGUITools.SetActive(self.uiTopPanel.gameObject,false)
	local t = getLuaComponent(self.uiStorePanel)
	t:ShowPanel()
end

function UIBadge:ClickBack()
	return function()
		BadgeSystemVar.currentBookId = 0
		BadgeSlotInfoUpDateCB = nil
		BadgeBookNameUpdateCB = nil
		NeedPlayUnlockEffectSlots = nil
		self:DoClose()
	end
end

function UIBadge:OnClose()
	if self.onClose then
		--print("uiBack",self.uiName,"--:",self.onClose)
		self.onClose()
		self.onClose = nil
		return
	end

	-- local menuBtn = getLuaComponent(self.uiBtnMenu)
	-- menuBtn:SetParent(self.gameObject, true)
	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end
end

function UIBadge:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
	-- self.transform:FindChild("Left/BooksBtn"):GetComponent("UIToggle").value = true
end

return UIBadge
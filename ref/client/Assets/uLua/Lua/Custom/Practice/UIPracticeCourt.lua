------------------------------------------------------------------------
-- class name    : UIPracticeCourt
-- create time   : Tue Feb 23 11:10:32 2016
------------------------------------------------------------------------

UIPracticeCourt =  {
	uiName     = "UIPracticeCourt",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiBackBtn       = nil,
	uiPlayerInfo    = nil,
	uiButtonMenu    = nil,
	uiFreePractice  = nil,
	uiPracticeMatch = nil,
	uiFocusPractice = nil,
	uiAnimator      = nil,
	ui1Vs1Match = nil,
	-----------------------
	-- Parameters Module --
	-----------------------
	btnMenu,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function UIPracticeCourt:Awake()
	self:UiParse()				-- Foucs on UI Parse.
	local t = getLuaComponent(createUI("ButtonBack", self.uiBackBtn))
	t.onClick = self:ClickBack()

	t = getLuaComponent(createUI("ButtonMenu",self.uiButtonMenu))
	t:SetParent(self.gameObject,false)
	t.parentScript = self
	self.btnMenu = t
end


function UIPracticeCourt:Start()
	addOnClick(self.uiFreePractice.gameObject, self:ClickItem(0))
	addOnClick(self.uiPracticeMatch.gameObject, self:ClickItem(1))
	addOnClick(self.uiFocusPractice.gameObject, self:ClickItem(2))
	addOnClick(self.ui1Vs1Match.gameObject, self:ClickItem(3))
	self:Refresh()
end

function UIPracticeCourt:Refresh()

end

-- uncommoent if needed
-- function UIPracticeCourt:FixedUpdate()

-- end


function UIPracticeCourt:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------
function UIPracticeCourt:ClickBack()
	return function()
		self:DoClose()
	end
end

function UIPracticeCourt:DoClose()
	if self.uiAnimator then
		print(self.uiName,":Set uinanimator close true")
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIPracticeCourt:OnClose()
	self.btnMenu:SetParent(self.gameObject, true)

	if self.onClose then
		self.onClose()
		self.onClose = nil
		return
	end

	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		print('@@a333333333333333333')
		-- modify by conglin
		TopPanelManager:ShowPanel("UIPracticeCourt1", nil, nil)
		--jumpToUI("UIPracticeCourt1")
	end
end


function UIPracticeCourt:ClickItem(index)
	return function()
		if index == 0 then
			-- 3v3
			if not FunctionSwitchData.CheckSwith(FSID.robot_train_3) then return end

			local t = TopPanelManager:ShowPanel("UISelectRole", nil, { maxSelection = 3, title=getCommonStr("STR_PRACTICE")})
			t.onStart = function(ids)
				local roleIds = UintList.New()
				for k, v in pairs(ids) do
					roleIds:Add(v)
				end
				t:VisibleModel(false)
				GameSystem.Instance.mClient:CreatePracticeVsMatch(roleIds)
			end

		elseif index == 1 then
			-- freedom
			if not FunctionSwitchData.CheckSwith(FSID.robot_train_free) then return end

			local t = TopPanelManager:ShowPanel("UISelectRole", nil, { maxSelection = 1, title=getCommonStr("STR_FREETRAIN")})
			t.onStart = function(ids)
				t:VisibleModel(false)
				GameSystem.Instance.mClient:CreateFreePracticeMatch(ids[1])
			end
		elseif index == 2 then
			if not FunctionSwitchData.CheckSwith(FSID.robot_train_4) then return end

			local t = TopPanelManager:ShowPanel("UIPractice")
			t.nextShowUI = self.uiName
		elseif index == 3 then
			-- 1v1
			if not FunctionSwitchData.CheckSwith(FSID.robot_train_1) then return end

			local t = TopPanelManager:ShowPanel("UISelectRole", nil, { maxSelection = 1,is1vs1PracticeMatch=true, title=getCommonStr("1vs1Á·Ï°Èü")})
			t.onStart = function(ids)
				t:VisibleModel(false)
				GameSystem.Instance.mClient:CreateFreePracticeMatch(ids[1])
			end
		end
	end
end

---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function UIPracticeCourt:UiParse()
	self.uiBackBtn       = self.transform:FindChild("Top/ButtonBack"):GetComponent("Transform")
	self.uiPlayerInfo    = self.transform:FindChild("Top/PlayerInfoGrids"):GetComponent("Transform")
	self.uiButtonMenu    = self.transform:FindChild("Top/ButtonMenu"):GetComponent("Transform")

	self.uiFreePractice  = self.transform:FindChild("Middle/FreePractice"):GetComponent("UISprite")
	self.uiPracticeMatch = self.transform:FindChild("Middle/PracticeMatch"):GetComponent("UISprite")
	self.uiFocusPractice = self.transform:FindChild("Middle/FocusPractice"):GetComponent("UISprite")

	self.ui1Vs1Match= self.transform:FindChild("Middle/FreePractice1")
end

return UIPracticeCourt

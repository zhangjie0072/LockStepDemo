--enxcoding=utf-8

UIPVPEntrance =
{	
	uiName = 'UIPVPEntrance',

	----------------parameters
	IconIndex = nil,			--challenge need
	roleNum = 3,   			--all need
	gameType = "Challenge", 				--all need
	modeType = GameMode.GM_PVP_1V1_PLUS, 

	nextShowUI,
	nextShowUISubID,
	nextShowUIParams,
	onClose,

	----------------UI
	uiBtnMenu,
	uiBackBtn,
	uiGamesGrid,
	-- uiChallengeGrid,
	-- uiOneVsOnePlus,
	-- uiOneVsOne,
	uiAnimator,
}

function UIPVPEntrance:Awake( ... )
	-- body
	self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
	self.uiTitle = self.transform:FindChild("Top/Title"):GetComponent("MultiLabel")
	self.uiBackBtn = getLuaComponent(createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack').transform))
	---gameType
	self.uiCareer = self.transform:FindChild('LeftCareer')
	self.uiChallenge = self.transform:FindChild('LeftChallenge')

	self.uiAnimator = self.transform:GetComponent('Animator')
end

function UIPVPEntrance:Start( ... )
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, false)
	menu.parentScript = self
	self:ChallengeProc()()
	addOnClick(self.uiBackBtn.gameObject, self:OnBack())
end

function UIPVPEntrance:FixedUpdate()
	-- body	
	
	--[[
	if self.tip.beginTime then
    local waitTime = tonumber(os.time()) - tonumber(self.tip.beginTime)
    self.tip.waitTimeLabel.text = tostring(waitTime) .. '秒'
  end

  if self.tip.matchBeginWaitTime and self.data then
    local matchWaitTime = tonumber(os.time()) - tonumber(self.matchBeginWaitTime)
    self.popup:SetMessage(getCommonStr('MATCH_JOIN_GAME') .. tostring(10 - matchWaitTime))
    
    print(tostring(10 - matchWaitTime))
    
    if matchWaitTime >= 10 then
      self:ForceEnterGame(self.data)
      self.beginTime = nil
      self.matchBeginWaitTime = nil
      PvpShow = false
    end
  end
	]]
end

function UIPVPEntrance:OnClose()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:SetParent(self.gameObject, true)

	if self.onClose then 
		self.onClose()
		self.onClose = nil
		return
	end

	if self.showRegular1V1 then
		Regular1V1Handler.SelectRole()
		self.showRegular1V1 = false
	elseif self.nextShowUI then
		-- self.nextShowUIParams = {uiBack = "UIPVPEntrance"}
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	elseif self.gameType == "Career" then 
		TopPanelManager:HideTopPanel()
	else
		TopPanelManager:ShowPanel('UIHall')
		--jumpToUI("UIHall") 
	end
end

function UIPVPEntrance:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function UIPVPEntrance:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIPVPEntrance:Refresh( ... )
	--self:RoleRefresh()
	local menu = getLuaComponent(self.uiBtnMenu)
	menu:Refresh()
end


-----------------------------------------------------------------
function UIPVPEntrance:ChallengeProc()
	return function ()
		-----show left by gametpye
		self.uiTitle:SetText(getCommonStr("STR_CHALLENGE_GAME"))
		self.firstClick = true
		self.numByType = {3 , 1}  -----rolenum by modetype
		self.gameModeTable = {GameMode.GM_PVP_1V1_PLUS, GameMode.GM_PVP_3V3}
		NGUITools.SetActive( self.uiChallenge.gameObject, true)
		self.uiChallengeGrid = self.transform:FindChild('LeftChallenge/Grid')

		if self.addFlag == nil then
			self.toggleTable = {}
			for i = 1 ,3 do
				local go = self.uiChallengeGrid:FindChild('ChoseToggle' .. i).gameObject
				addOnClick(go,self:MakeOnIcon(i))
				self.toggleTable[i] = go.transform
				print("UIPVPEntrance toggle",i,":",go.transform)
			end
			--default 
			-- self:MakeOnIcon(1)()
		end
	end
end

---- challenge use
function UIPVPEntrance:MakeOnIcon( index)   
	return function()
		if index ~= 1 and index ~= 2 then
			CommonFunction.ShowPopupMsg(getCommonStr('IN_CONSTRUCTING'),nil,nil,nil,nil,nil)
			print('Under constructing')
			return
		end
		self.addFlag = true
		self.IconIndex = index	
		self.roleNum = self.numByType[ index]
		self.modeType = self.gameModeTable[index]
		self:ChallengeProc()()
		self:JoinInGame(index)
		-- self:SetToggle()
	end
end

function UIPVPEntrance:JoinInGame(index)
	if index == 1 then
		--if validateFunc('OneVsOne') then
			--self.nextShowUI = "UI1V1Plus"
			--self.nextShowUIParams = {uiBack = "UIPVPEntrance"}
			self.showRegular1V1 = true
			self:DoClose()
		--end
	elseif index == 2 then
		self.nextShowUI = "UIChallenge"
		self.nextShowUIParams = { gameType = "Challenge" , roleNum = 1, nextShowUI = "UIPVPEntrance"}
		self:DoClose()
	end
end

-- function UIPVPEntrance:SetToggle()
-- 	for i = 1,3 do
-- 		if i == self.IconIndex then
-- 			self.toggleTable[i]:GetComponent("UIToggle").value = true
-- 			print("UIPVPEntrance Set",i,"-value true")
-- 		else
-- 			self.toggleTable[i]:GetComponent("UIToggle").value = false
-- 			print("UIPVPEntrance Set",i,"-value false")
-- 		end
-- 	end
-- end

function UIPVPEntrance:OnBack( ... )
	return function ( ... )
	  --if not PvpShow then
		--  self:DoClose()
		--end
		self:DoClose()
	end
end

function UIPVPEntrance:SetModelActive(active)
	-- body
end

return UIPVPEntrance

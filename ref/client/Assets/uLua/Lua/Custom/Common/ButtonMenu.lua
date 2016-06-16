--encoding=utf-8

ButtonMenu =
{
	uiName = 'ButtonMenu',
	instances = {},
	REFRESH_MENUTIPS_DELAY = 2,
	----------UI
	uigrid,
	uiSwitch1,
	uiSwitch2,
	uiArrow,
	uiMember,
	uiSquad,
	uiPackage,
	uiRank,
	uiTask,
	uiMask,
	uiRedDot1,
	uiMemberRedDot,
	uiSquadRedDot,
	uiTaskRedDot,
	uiRankRedDot,
	----------parameters
	parent,
	parentScript,
	isOpened = false,
	TaskUI,
	--GetRoleIdList = {},
	-- animatorPreTime,
	-- animatorTime,
}

function ButtonMenu:Awake()
	-- local transform = self.transform
	-- self.uiSwitch1 = transform:FindChild('Switch').gameObject
	-- self.uiRedDot1 = transform:FindChild('Switch/Tip'):GetComponent('UISprite')

	-- self.uiPanel = transform:FindChild("Panel").gameObject
	-- self.uiSwitch2 = self.uiPanel.transform:FindChild('Switch').gameObject
	-- self.uiRedDot2 = self.uiPanel.transform:FindChild('Switch/Tip'):GetComponent('UISprite')
	-- self.uiArrow = self.uiPanel.transform:FindChild('Switch/Arrow'):GetComponent('UISprite')
	-- self.uigrid = self.uiPanel.transform:FindChild('Grid').gameObject
	-- self.uiMember = self.uiPanel.transform:FindChild('Grid/Member').gameObject
	-- self.uiSquad = self.uiPanel.transform:FindChild('Grid/Squad').gameObject
	-- self.uiSquadRedDot = self.uiPanel.transform:FindChild('Grid/Squad/Tip').gameObject
	-- self.uiMemberRedDot = self.uiPanel.transform:FindChild('Grid/Member/Tip').gameObject
	-- self.uiPackage = self.uiPanel.transform:FindChild('Grid/Package').gameObject
	-- self.uiRank = self.uiPanel.transform:FindChild('Grid/Rank').gameObject
	-- self.uiRankRedDot = self.uiPanel.transform:FindChild('Grid/Rank/Tip').gameObject
	-- self.uiTask = self.uiPanel.transform:FindChild('Grid/Task').gameObject
	-- self.uiTaskRedDot = self.uiPanel.transform:FindChild('Grid/Task/Tip').gameObject
	-- self.uiMask = self.uiPanel.transform:FindChild('Mask').gameObject

	-- self.uiAnimator = self.uiPanel:GetComponent("Animator")
end

function ButtonMenu:Start()
	-- addOnClick(self.uiSwitch1, self:OnSwitch())
	-- addOnClick(self.uiSwitch2, self:OnSwitch())
	-- addOnClick(self.uiMember, self:OnMemberClick())
	-- addOnClick(self.uiSquad, self:OnSquadClick())
	-- addOnClick(self.uiPackage, self:OnPackageClick())
	-- addOnClick(self.uiRank, self:OnRankClick())
	-- addOnClick(self.uiTask, self:OnTaskClick())
	-- addOnClick(self.uiMask, self:OnSwitch())
	-- self:Refresh()

	-- UpdateRedDotHandler.MessageHandler("Task")
	-- UpdateRedDotHandler.MessageHandler("Daily")
	-- UpdateRedDotHandler.MessageHandler("Role")
	-- UpdateRedDotHandler.MessageHandler("Squad")
	-- UpdateRedDotHandler.MessageHandler("RoleDetail")

	-- self:SetRedDot()
	-- ButtonMenu.instances[self] = true

	self.gameObject:SetActive(false)
end

function ButtonMenu:OnDestroy()
	-- ButtonMenu.instances[self] = nil
	
	-- Object.Destroy(self.uiAnimator)
	-- Object.Destroy(self.transform)
	-- Object.Destroy(self.gameObject)
end

function ButtonMenu:FixedUpdate()
	-- if self.hideOnTransDone and not self.uiAnimator:IsInTransition(0) then
	-- 	NGUITools.SetActive(self.uiSwitch2, false)
	-- 	self.hideOnTransDone = false
	-- end

	-- -- if self.refreshTime and CommonFunction.TickTime() - self.refreshTime >= self.REFRESH_MENUTIPS_DELAY then
	-- -- 	self:RefreshMenuTips()
	-- -- 	self.refreshTime = nil
	-- -- end
end

function ButtonMenu:Refresh( ... )
	-- if not self then
	-- 	for k, v in pairs(ButtonMenu.instances) do
	-- 		if v and k.gameObject.activeInHierarchy then
	-- 			k:Refresh()
	-- 		end
	-- 	end
	-- 	return
	-- end
	-- --self:RefreshMenuTips()
	-- -- self.refreshTime = CommonFunction.TickTime()
	-- self:HideMenu()
end

function ButtonMenu:HideMenu()
	-- if self.isOpened then
	-- 	self.uiAnimator.enabled = true
	-- 	self.uiAnimator:SetBool("BoolSwitch", false)
	-- 	--self.uiArrow.flip = UIBasicSprite.Flip.Nothing
	-- 	self.isOpened = false
	-- 	NGUITools.SetActive(self.uiMask, false)
	-- 	NGUITools.SetActive(self.uiSwitch2, false)
	-- end
end

function ButtonMenu:OnMemberClick()
	-- return function(go)
	-- 	if getLuaComponent(self.parent).uiName == 'UIRole' then return end
	-- 	if getLuaComponent(self.parent).uiName == 'UIFashion' then
	-- 		getLuaComponent(self.parent).isInitAnimator = false
	-- 		getLuaComponent(self.parent).isWardrobe = false
	-- 	end

	-- 	self:HideMenu()
	-- 	-- if self.parentScript then
	-- 	-- 	self.parentScript.nextShowUI = "UIRole"
	-- 	-- 	self.parentScript:DoClose()
	-- 	-- else
	-- 	-- 	TopPanelManager:ShowPanel('UIRole')
	-- 	-- end
	-- 	self.parentScript.onClose = function() TopPanelManager:ShowPanel('UIRole') end
	-- 	self.parentScript:DoClose()
	-- end
end

function ButtonMenu:OnSquadClick()
	-- return function(go)
	-- 	if getLuaComponent(self.parent).uiName == 'UISquad' then return end
	-- 	if getLuaComponent(self.parent).uiName == 'UIFashion' then
	-- 		getLuaComponent(self.parent).isInitAnimator = false
	-- 		getLuaComponent(self.parent).isWardrobe = false
	-- 	end

	-- 	self:HideMenu()
	-- 	-- if self.parentScript then
	-- 	-- 	self.parentScript.nextShowUI = "UISquad"
	-- 	-- 	self.parentScript:DoClose()
	-- 	-- else
	-- 	-- 	TopPanelManager:ShowPanel('UISquad')
	-- 	-- end
	-- 	self.parentScript.onClose = function() TopPanelManager:ShowPanel('UISquad') end
	-- 	self.parentScript:DoClose()
	-- end
end

function ButtonMenu:OnPackageClick( go )
	-- return function()
	-- 	if getLuaComponent(self.parent).uiName == 'UIPackage' then return end
	-- 	if getLuaComponent(self.parent).uiName == 'UIFashion' then
	-- 		getLuaComponent(self.parent).isInitAnimator = false
	-- 		getLuaComponent(self.parent).isWardrobe = false
	-- 	end

	-- 	self:HideMenu()
	-- 	if self.parentScript then
	-- 		self.parentScript.nextShowUI = "UIPackage"
	-- 		self.parentScript:DoClose()
	-- 	else
	-- 		TopPanelManager:ShowPanel('UIPackage')
	-- 	end
	-- end
end

function ButtonMenu:OnRankClick( go )
	-- return function (go)
	-- 	if self.isClicking == true then
	-- 		return
	-- 	end
	-- 	local req = {
	-- 		acc_id = MainPlayer.Instance.AccountID,
	-- 		type = 3, --日常任务
	-- 	}
	-- 	local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
	-- 	LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

	-- 	TaskRespHandler.parent = getLuaComponent(self.parent)
	-- 	TaskRespHandler.buttonMenu = self
	-- 	self:HideMenu()
	-- end
end

function ButtonMenu:OnTaskClick( go )
	-- return function (go)
	-- 	print(self.uiName,"send task req")
	-- 	local req = {
	-- 		acc_id = MainPlayer.Instance.AccountID,
	-- 		type = 2, --普通任务
	-- 	}
	-- 	local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
	-- 	LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

	-- 	TaskRespHandler.parent = getLuaComponent(self.parent)
	-- 	TaskRespHandler.buttonMenu = self
	-- 	self:HideMenu()
	-- end
end

function ButtonMenu:SetParent(parent,hideModel)
	-- self.parent = parent
	-- TaskRespHandler.hideModel = hideModel
end

function ButtonMenu:OnSwitch( ... )
	-- return function (go)
	-- 	--self:ChangeSwitch()
	-- 	if not self.isOpened then
	-- 		--self.uiArrow.flip = UIBasicSprite.Flip.Vertically
	-- 		self.isOpened = true
	-- 		NGUITools.SetActive(self.uiSwitch2, true)
	-- 		UIManager.Instance:BringPanelForward(self.uiPanel)
	-- 	else
	-- 		--self.uiArrow.flip = UIBasicSprite.Flip.Nothing
	-- 		self.isOpened = false
	-- 		self.hideOnTransDone = true
	-- 	end
	-- 	if self.isOpened and self.parentScript and self.parentScript.uiName == "UIHall" then
	-- 		self.parentScript:CloseChatSetting()()
	-- 	end
	-- 	NGUITools.SetActive(self.uiMask, self.isOpened)
	-- end
end

function ButtonMenu:SetRedDot( ... )
	-- if self.uiTaskRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["ButtonMenu"]["UITask"] then
	-- 	NGUITools.SetActive(self.uiTaskRedDot.gameObject, UpdateRedDotHandler.UpdateState["ButtonMenu"]["UITask"])
	-- end
	-- if self.uiRankRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIDaily"] then
	-- 	NGUITools.SetActive(self.uiRankRedDot.gameObject, UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIDaily"])
	-- end
	-- if self.uiMemberRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIRole"] then
	-- 	NGUITools.SetActive(self.uiMemberRedDot.gameObject, UpdateRedDotHandler.UpdateState["ButtonMenu"]["UIRole"])
	-- end
	-- -- print('UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"]' , UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"])
	-- if self.uiSquadRedDot.gameObject.activeSelf ~= UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"] then
	-- 	NGUITools.SetActive(self.uiSquadRedDot.gameObject, UpdateRedDotHandler.UpdateState["ButtonMenu"]["UISquad"])
	-- end

	-- local taskDot = self.uiTaskRedDot.gameObject.activeSelf
	-- local dailyDot = self.uiRankRedDot.gameObject.activeSelf
	-- local memberDot = self.uiMemberRedDot.gameObject.activeSelf
	-- local squadDot = self.uiSquadRedDot.gameObject.activeSelf
	-- -- print('squadDot.state = ', squadDot)
	-- -- print('taskDot ----- ' .. tostring(taskDot) .. ', dailyDot ------ ' .. tostring(dailyDot))
	-- local menuDot = taskDot or dailyDot or memberDot or squadDot
	-- if self.uiRedDot1.gameObject.activeSelf ~= menuDot then
	-- 	NGUITools.SetActive(self.uiRedDot1.gameObject, menuDot)
	-- end
	-- if self.uiRedDot2.gameObject.activeSelf ~= menuDot then
	-- 	NGUITools.SetActive(self.uiRedDot2.gameObject, menuDot)
	-- end
end

function ButtonMenu:DestroyTaskUI()
	-- print(self.uiName,"----taskui:",self.taskUI)
	-- if self.taskUI then
	-- 	print(self.uiName,"------clear taskui")
	-- 	NGUITools.Destroy(self.taskUI.gameObject)
	-- 	self.taskUI = nil 
	-- end
end

return ButtonMenu

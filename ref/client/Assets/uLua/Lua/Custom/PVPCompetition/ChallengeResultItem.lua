--encoding=utf-8

ChallengeResultItem = {
	uiName = 'ChallengeResultItem',
	----------------------UI
	uiIcon,
	uiPlayerName,
	uiPosition,
	uiMvp,
	uiShoot,
	ui3Points,
	uiRebound,
	uiAssist,
	uiSteal,
	uiBlock,
	uiScore,
	uiBg,
	uiAddFriend,
	uiSureTip,
	---------------------parameters
	id,
	isMvp,
	shootTimes,
	onTarget,
	farShootTimes,
	farOnTarget,
	rebound,
	assist,
	steal,
	block,
	score,
	name = nil,
	roleInfo,
}


-----------------------------------------------------------------
--Awake
function ChallengeResultItem:Awake( ... )
	self.tmIcon = self.transform:FindChild("Icon")
	self.uiPlayerName = self.transform:FindChild("Name"):GetComponent("UILabel")
	self.uiMvp = self.transform:FindChild("MVP")
	self.uiPostion = self.transform:FindChild("Profession"):GetComponent("UISprite")
	self.uiShoot = self.transform:FindChild("Shoot"):GetComponent("UILabel")
	self.ui3Points = self.transform:FindChild("Trisection"):GetComponent("UILabel")
	self.uiRebound = self.transform:FindChild("Backboard"):GetComponent("UILabel")
	self.uiAssist = self.transform:FindChild("Assist"):GetComponent("UILabel")
	self.uiSteal = self.transform:FindChild("Steal"):GetComponent("UILabel")
	self.uiBlock = self.transform:FindChild("Block"):GetComponent("UILabel")
	self.uiScore = self.transform:FindChild("Score"):GetComponent("UILabel")
	-- self.uiTeamName = self.transform:FindChild("TeamName"):GetComponent("UILabel")
	self.uiBg = self.transform:FindChild('BGShade1'):GetComponent('UISprite')
	self.uiAddFriend = self.transform:FindChild("AddFriend")
end

function ChallengeResultItem:Start()
	addOnClick(self.uiAddFriend.gameObject, self:OnAddFriend())
	--自己或已经是好友了，不再显示加好友标记
	local isFri = Friends.IsFriend(self.id)--FriendData.Instance:IsFriend(self.id)
	if self.id == MainPlayer.Instance.AccountID or isFri then
		NGUITools.SetActive(self.uiAddFriend.gameObject, false)
	end

	local positions ={'PF','SF','C','PG','SG'}
	self.uiIcon = createUI("CareerRoleIcon", self.tmIcon)
	local player = getLuaComponent( self.uiIcon)
	player.id = self.id
	player.showPosition = false
	if self.roleInfo then
		player.otherInfo = self.roleInfo
	end
	local roleConfig
	if self.id >= 10000 then
		player.npc = true
		roleConfig = GameSystem.Instance.NPCConfigData:GetConfigData(self.id)
	else
		roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
	end
	self.uiPostion.spriteName = 'PT_'..positions[roleConfig.position]
	if self.name == nil then
		self.uiPlayerName.text = roleConfig.name
	else
		self.uiPlayerName.text = self.name
	end
	self.uiShoot.text = self.onTarget .. "-" .. self.shootTimes
	self.ui3Points.text = self.farOnTarget .. "-" .. self.farShootTimes
	self.uiRebound.text = tostring(self.rebound)
	self.uiAssist.text = tostring(self.assist)
	self.uiSteal.text = tostring(self.steal)
	self.uiBlock.text = tostring(self.block)
	self.uiScore.text = tostring(self.score)
	print("self.isMvp:",self.isMvp)
	if tonumber(self.isMvp) == 1 then
		print("MVP")
		NGUITools.SetActive( self.uiMvp.gameObject, true)
	end
	if self.name == MainPlayer.Instance.Name then
		NGUITools.SetActive(self.uiBg.gameObject, true)
		-- NGUITools.SetActive(self.uiTeamName.gameObject, true)
		-- self.uiTeamName.text = self.name
	end
end

function ChallengeResultItem:OnAddFriend()
	return function ()
		local message = getCommonStr('STR_FRIENDS_SURE_ADD')
		self.uiSureTip = CommonFunction.ShowPopupMsg(message, nil,
			LuaHelper.VoidDelegate(self:OnBtnOK()),
			LuaHelper.VoidDelegate(self:OnBtnCancel()),
			getCommonStr("BUTTON_CONFIRM"),
			getCommonStr("BUTTON_CANCEL"))
	end
end

function ChallengeResultItem:OnBtnOK()
	return function()

		local req = {
			type = "FOT_ADD",
			op_friend = {
				acc_id = self.id,
			},
		}

		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
	end
end

--Cancel回调
function ChallengeResultItem:OnBtnCancel()
	return function()
		NGUITools.Destroy(self.uiSureTip.gameObject)
	end
end

return ChallengeResultItem

require "Custom/Friends/FriendExtraAwardsConfigRes"
FriendsListItem =  {
uiName	= "FriendsListItem",

friendData,

msg,
msgType,
listType,
headIconScript,

uiHeadIcon,
uiLabLvNum,
uiLabVipNum,
uiLabPlayerName,
uiLabOnline,

uiFriendsListTransform,
uiBtnDel,
uiBtnChat,
uiBtnGiveGlod,

uiApplyList,
uiBtnIgnore,
uiBtnAgree,

uiBlackList,
uiBtnDelBlack,

uiNearyList,
uiBtnShowInfo,
uiBtnAdd,
uiSpFaction,	--友好度图片
uiLblFaction,	--友好度lable
uiFriInfo,
uiFirstWin,
uiFirstWinFlag,

sprGoldArrows,
sprGoldBg,

tfFriendsList,
actionOnTimer,
timerAdded = false,
parent = nil,
}

function FriendsListItem:Awake()

	self.uiHeadIcon = getChildGameObject(self.transform, "Icon")
	self.uiLabLvNum = getComponentInChild(self.transform, "Type/Num", "UILabel")
	self.uiLabVipNum = getComponentInChild(self.transform, "Vocation/Num", "UILabel")
	self.uiLabPlayerName = getComponentInChild(self.transform, "Name", "UILabel")
	self.uiLabOnline = getComponentInChild(self.transform, "State", "UILabel")

	self.uiFriendsListTransform = self.transform:FindChild("FriendsList")
	self.uiBtnDel = getComponentInChild(self.uiFriendsListTransform, "Del", "UIButton")
	self.uiBtnChat = getComponentInChild(self.uiFriendsListTransform, "PM", "UIButton")
	self.uiBtnGiveGlod = getComponentInChild(self.uiFriendsListTransform, "Gold", "UIButton")
	self.sprGoldArrows = self.uiBtnGiveGlod.transform:FindChild("Sprite"):GetComponent('UISprite')
	self.sprGoldBg = self.uiBtnGiveGlod.transform:FindChild("Bg"):GetComponent('UISprite')

	self.uiApplyList = self.transform:FindChild("ApplyList")
	self.uiBtnIgnore = getComponentInChild(self.uiApplyList, "btnIgnore", "UIButton")
	self.uiBtnAgree = getComponentInChild(self.uiApplyList, "btnAdd", "UIButton")

	self.uiBlackList = self.transform:FindChild("BlackList")
	self.uiBtnDelBlack = getComponentInChild(self.uiBlackList, "Del", "UIButton")

	self.uiNearyList = self.transform:FindChild("NearbyList")
	self.uiBtnShowInfo = getComponentInChild(self.uiNearyList, "btnShowInfo", "UIButton")
	self.btnAdd = getComponentInChild(self.uiNearyList, "btnAdd", "UIButton")
	self.uiFirstWin = getComponentInChild(self.transform, "FirstWin/FirstWinCD", "UILabel")
	self.uiFirstWinFlag = self.transform:FindChild("FirstWin")
	self.uiSpFaction = self.transform:FindChild("Faction"):GetComponent('UISprite')
	self.uiLblFaction = self.transform:FindChild("Faction/FactionLabel"):GetComponent('UILabel')

	
end

function FriendsListItem:Start()

	--addOnClick(self.uiHeadIcon.gameObject, self:FriendsInfo())
	-- self.uiLblFaction.text = ''
	addOnClick(self.uiBtnDel.gameObject, self:DelFriend())
	addOnClick(self.uiBtnChat.gameObject, self:OnChat())
	addOnClick(self.uiBtnGiveGlod.gameObject, self:OnGive())

	addOnClick(self.uiBtnIgnore.gameObject, self:OnBtnIgnore())
	addOnClick(self.uiBtnAgree.gameObject, self:OnBtnAgree())

	addOnClick(self.uiBtnDelBlack.gameObject, self:OnBtnDelBlack())

	addOnClick(self.uiBtnShowInfo.gameObject, self:OnShowInfo())
	addOnClick(self.btnAdd.gameObject, self:OnAddFriends())
    -- NGUITools.SetActive(self.uiFirstWinFlag.gameObject, false)
    self.actionOnTimer = LuaHelper.Action(self:OnTimer())
end

function FriendsListItem:OnDestroy()

    Scheduler.Instance:RemoveTimer(self.actionOnTimer)
    self = nil 
end

function FriendsListItem:OnClose()

end
local STR_FIRST_WIN_OK = getCommonStr("STR_FIRST_WIN_OK")
local STR_FIRST_WIN_TIME = getCommonStr("STR_FIRST_WIN_TIME")
function FriendsListItem:OnTimer()
	-- body
	return function ()
		-- body
		if not self.friendData then
			 Scheduler.Instance:RemoveTimer(self.actionOnTimer) 
			 return 
		end
		if not self.gameObject.activeSelf then return end
		self.mFirstWinCD  = self.mFirstWinCD - 1

        if self.mFirstWinCD <= 0 then
            NGUITools.SetActive(self.uiFirstWinFlag.gameObject, false)
            Scheduler.Instance:RemoveTimer(self.actionOnTimer)
        else
			local server_time = GameSystem.mTime
            local diff = FirstWinCD * 60 - (server_time - self.mFirstWinCD)
            if diff <= 0 then
                self.uiFirstWin.text = STR_FIRST_WIN_OK
                Scheduler.Instance:RemoveTimer(self.actionOnTimer)

            else
                local timeStr = getTimeStr( diff )
                self.uiFirstWin.text = string.format(STR_FIRST_WIN_TIME, timeStr)
				-- Scheduler.Instance:AddTimer(1,false,self:OnTimer())

            end
        end
	end
end
----------------------------------------------

function FriendsListItem:setData( msg, lst )
	if self.friendData == msg then return end
	self.friendData = msg
	self.listType = lst

	self:RefreshData()
end

local STR_FACTION_FORMAT = CommonFunction.GetConstString('STR_FACTION_FORMAT')
function FriendsListItem:RefreshData()
	if not self.headIconScript then
		self.headIconScript = getLuaComponent(createUI("CareerRoleIcon", self.uiHeadIcon.transform))
		self.headIconScript.onClick = self:FriendsInfo()
		self.headIconScript.showPosition = false
		self.headIconScript.id = tonumber(self.friendData.icon)
	else
		self.headIconScript.id = tonumber(self.friendData.icon)
		self.headIconScript:Refresh()
	end
	warning('friend info ',self.friendData.name,',online',self.friendData.online,'firstwinCD',self.friendData.first_win_time)
	--self.uiHeadIcon.spriteName = "icon_portrait_"..self.friendData.icon
	self.uiLabPlayerName.text = self.friendData.name
	self.uiLabLvNum.text = self.friendData.level
	self.uiLabVipNum.text = self.friendData.vip_level

	local diff = function(sec)
		local date = {}
		date.day = math.floor(sec / 86400)
		date.hour = math.floor((sec % 86400) / 3600)
		date.min = math.floor((sec % 3600) / 60)
		date.sec = math.floor(sec % 60)
		return date
	end

	local diff_sec = GameSystem.mTime - self.friendData.logout_time
	local dd = diff( diff_sec )
	local logoutstr;
	if dd.day >= 30 then
		logoutstr = string.format(getCommonStr('STR_FRIENDS_GOOUT_TIME1'))
	elseif dd.day > 0 then
		logoutstr = string.format(getCommonStr('STR_FRIENDS_GOOUT_TIME2'), dd.day)
	elseif dd.hour > 0 then
		logoutstr = string.format(getCommonStr('STR_FRIENDS_GOOUT_TIME3'), dd.hour)
	elseif dd.min > 0 then
		logoutstr = string.format(getCommonStr('STR_FRIENDS_GOOUT_TIME4'), dd.min)
	else
		logoutstr = string.format(getCommonStr('STR_FRIENDS_GOOUT_TIME5'))
	end

	if self.friendData.online == PlayerStateOnLine.OFFLINE then
		self.uiLabOnline.text = logoutstr
		NGUITools.SetActive(self.uiFirstWinFlag.gameObject,false)
	else
		local inMatch  = self.friendData.online == 2 or self.friendData.online == 3
		if inMatch then
			self.uiLabOnline.text = getCommonStr("STR_IH_GAME")
		else
			self.uiLabOnline.text = getCommonStr('STR_ONLINE')
		end
		if Friends.IsFriend(self.friendData.acc_id) then
			Scheduler.Instance:RemoveTimer(self.actionOnTimer)
			Scheduler.Instance:AddTimer(1,true,self.actionOnTimer)

            NGUITools.SetActive(self.uiFirstWinFlag.gameObject, true)
			--首胜数据
			self.mFirstWinCD = self.friendData.first_win_time
			self:OnTimer()()
		else
			NGUITools.SetActive(self.uiFirstWinFlag.gameObject,false)
		end
	end


	self.uiBtnIgnore.isEnabled = true
	if self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY then
		NGUITools.SetActive(self.uiFriendsListTransform.gameObject, true)
		NGUITools.SetActive(self.uiApplyList.gameObject, false)
		NGUITools.SetActive(self.uiBlackList.gameObject, false)
		NGUITools.SetActive(self.uiNearyList.gameObject, false)

		--好友列表中打开时,直接弹出玩家信息界面
		self.headIconScript.onClick = self:OnShowInfo()

		local gFriendPresendGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendPresendGiftTimes")
		if FriendData.Instance.present_times >= gFriendPresendGiftTimes then
			self.uiBtnGiveGlod.isEnabled = (self.friendData.present_flag == 0)
			if (self.friendData.present_flag == 0) == false then
				self.sprGoldArrows.color = Color.New(0 , 1, 1, 1)
				self.sprGoldBg.color = Color.New(0 , 1, 1, 1)
			else
				self.sprGoldArrows.color = Color.New(1 , 1, 1, 1)
				self.sprGoldBg.color = Color.New(1 , 1, 1, 1)
			end
		else
			self.uiBtnGiveGlod.isEnabled = (self.friendData.present_flag == 0)
			if (self.friendData.present_flag == 0) == false then
				self.sprGoldArrows.color = Color.New(0 , 1, 1, 1)
				self.sprGoldBg.color = Color.New(0 , 1, 1, 1)
			else
				self.sprGoldArrows.color = Color.New(1 , 1, 1, 1)
				self.sprGoldBg.color = Color.New(1 , 1, 1, 1)
			end
		end
	elseif self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY_APPLY then
		NGUITools.SetActive(self.uiFriendsListTransform.gameObject, false)
		NGUITools.SetActive(self.uiApplyList.gameObject, true)
		NGUITools.SetActive(self.uiBlackList.gameObject, false)
		NGUITools.SetActive(self.uiNearyList.gameObject, false)
	elseif self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY_BLACK then
		NGUITools.SetActive(self.uiFriendsListTransform.gameObject, false)
		NGUITools.SetActive(self.uiApplyList.gameObject, false)
		NGUITools.SetActive(self.uiBlackList.gameObject, true)
		NGUITools.SetActive(self.uiNearyList.gameObject, false)

		--点击黑名单中的头像，修改为无任何反馈
		self.headIconScript.onClick = nil
	else
		NGUITools.SetActive(self.uiFriendsListTransform.gameObject, false)
		NGUITools.SetActive(self.uiApplyList.gameObject, false)
		NGUITools.SetActive(self.uiBlackList.gameObject, false)
		NGUITools.SetActive(self.uiNearyList.gameObject, true)
	end
	if Friends.IsFriend(self.friendData.acc_id) then
		NGUITools.SetActive(self.uiSpFaction.gameObject,true)
		local shinwaka = self.friendData.shinwakan
		local color_str
		local extro = 0
		for _,v in ipairs(FriendExtraAwardsConfigRes) do
			if shinwaka >= v['shinwakan'] then
				extro = v['awards_percent']
				color_str = v['colour']
			else
				break
			end
		end

		self.uiLblFaction.text = string.format(STR_FACTION_FORMAT,shinwaka,extro)..'%'
		if color_str then
			local colors = string.split(color_str,',')
			if colors then
				self.uiSpFaction.color = Color.New(colors[1],colors[2],colors[3],1)
			end
		end
	else
		NGUITools.SetActive(self.uiSpFaction.gameObject,false)
	end
end

--忽略添加好友
function FriendsListItem:OnBtnIgnore()
	return function()
		local req = {
		type = "FOT_IGNORE_APPLY",
		op_friend = {
		acc_id = self.friendData.acc_id,
		plat_id = self.friendData.plat_id,
		},
		}

		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)

		self.uiBtnIgnore.isEnabled = false
		CommonFunction.ShowWaitMask()
	end
end

function FriendsListItem:CheckFriendsNum()
	local friend_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY)
	local gFriendsMax = GameSystem.Instance.CommonConfig:GetUInt("gFriendsMax")
	if friend_count >= gFriendsMax then
		CommonFunction.ShowTip(getCommonStr("REACH_FRIENDS_MAX"), nil)
		return false
	else
		return true
	end
end

--同意添加好友
function FriendsListItem:OnBtnAgree()
	return function()
		if not self:CheckFriendsNum() then
			return
		end

		--local message = getCommonStr('STR_FRIENDS_SURE_AGREE')

		self.msgType = 'FOT_AGREE_APPLY'
		self:OnBtnOK()()
        CommonFunction.ShowWaitMask()
	end
end

--黑名单-解禁
function FriendsListItem:OnBtnDelBlack()
	return function()
        if self.msg then
			return
		end

		local message = getCommonStr('STR_FRIENDS_SURE_RBLACK')

		self.msgType = 'FOT_DEL_BLACK'
		self.msg = CommonFunction.ShowPopupMsg(message, nil,
		LuaHelper.VoidDelegate(self:OnBtnOK()),
		LuaHelper.VoidDelegate(self:OnBtnCancel()),
		getCommonStr("BUTTON_CONFIRM"),
		getCommonStr("BUTTON_CANCEL"))
	end
end

--点击头像弹出的功能菜单
function FriendsListItem:FriendsInfo()
	return function ()
	    if self.listType == nil then
		    do return end
	    end

	    Friends.OpenFriendItemTip(self.friendData.acc_id,
								    self.friendData.name,
								    self.friendData.icon,
								    self.friendData.plat_id)
    end
end

--赠送好友金币
function FriendsListItem:OnGive()
	return function()
		if not FunctionSwitchData.CheckSwith(FSID.friends_gold) then return end

		local gFriendPresendGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendPresendGiftTimes")
		if FriendData.Instance.present_times >= gFriendPresendGiftTimes then
			CommonFunction.ShowTip(getCommonStr('STR_FRIENDS_NOT_GIVE'), nil)
			return
		end

		--local message = getCommonStr('STR_FRIENDS_SURE_GIVE')
		self.msgType = 'FOT_PRESEND'
		self:OnBtnOK()()
		self.uiBtnGiveGlod.isEnabled = false
		self.sprGoldArrows.color = Color.New(0 , 1, 1, 1)
		self.sprGoldBg.color = Color.New(0 , 1, 1, 1)
        CommonFunction.ShowWaitMask()
	end
end

--删除好友
function FriendsListItem:DelFriend()
	return function()
		if self.msg then
			return
		end

		local message = getCommonStr('STR_FRIENDS_SURE_DEL')

		self.msgType = 'FOT_DEL_FRIEND'
		self.msg = CommonFunction.ShowPopupMsg(message, nil,
		LuaHelper.VoidDelegate(self:OnBtnOK()),
		LuaHelper.VoidDelegate(self:OnBtnCancel()),
		getCommonStr("BUTTON_CONFIRM"),
		getCommonStr("BUTTON_CANCEL"))
	end
end

--查询好友信息
function FriendsListItem:OnShowInfo()
	return function()
		-- local req = {
		-- friend = {
		-- acc_id = self.friendData.acc_id,
		-- plat_id = self.friendData.plat_id, },
		-- }

		-- local buf = protobuf.encode("fogs.proto.msg.QueryFriendInfoReq", req)
		-- LuaHelper.SendPlatMsgFromLua(MsgID.QueryFriendInfoReqID, buf)

		-- LuaHelper.RegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self:QueryFriendInfoHandler(), self.uiName)
		-- CommonFunction.ShowWaitMask()
		-- CommonFunction.ShowWait()
		local friendsInfo = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("FriendsInfo(Clone)")
        if friendsInfo then
        	NGUITools.Destroy(friendsInfo.gameObject)
        end
		if not self.uiFriInfo then
			self.uiFriInfo = createUI("FriendsInfo")
			local friLua = getLuaComponent(self.uiFriInfo)
			-- friLua:setData(resp)
        	friLua:Query(self.friendData.acc_id,self.friendData.plat_id)
			friLua.onCloseEvent = function()
				self.uiFriInfo = nil
			end
			-- UIManager.Instance:BringPanelForward(self.uiFriInfo)
		end
	end
end

--添加好友
function FriendsListItem:OnAddFriends()
	return function()
		if not self:CheckFriendsNum() then
			return
		end

		--local message = getCommonStr('STR_FRIENDS_SURE_ADD')

		self.msgType = 'FOT_ADD'
		self:OnBtnOK()()
        CommonFunction.ShowWaitMask()
	end
end

function FriendsListItem:OnChat()
	return function()
		self.parent:ChatToFriend(self.friendData.acc_id)
	end
end

--OK回调
function FriendsListItem:OnBtnOK()
	return function()
		print(self.msgType)

		self.msg = nil

		local req = {
			type = self.msgType,
			op_friend = {
				acc_id = self.friendData.acc_id,
				plat_id = self.friendData.plat_id,
			},
		}

		if self.msgType == 'FOT_DEL_FRIEND' then
			local lua = getLuaComponent(self.tfFriendsList)
			lua.isDel = true
			lua.DelNum = self.transform.gameObject.name
		end

		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)

		CommonFunction.ShowWaitMask()
	end
end

--Cancel回调
function FriendsListItem:OnBtnCancel()
	return function()
		NGUITools.Destroy(self.msg.gameObject)
		self.msg = nil
	end
end

--接收好友查询信息结果
-- function FriendsListItem:QueryFriendInfoHandler()
-- 	return function(buf)
-- 		print("<<<<<<FriendsItemTip:QueryFriendInfoHandler")
-- 		CommonFunction.StopWait()
-- 		LuaHelper.UnRegisterPlatMsgHandler(MsgID.QueryFriendInfoRespID, self.uiName)

-- 		local resp, err = protobuf.decode("fogs.proto.msg.QueryFriendInfoResp", buf)
-- 		if not resp then
-- 			error(err)
-- 			return
-- 		end

-- 		if resp.result ~= 0 then
-- 			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
--             return
-- 		end

-- 		--打开好友信息界面
-- 		if not self.uiFriInfo then
-- 			self.uiFriInfo = createUI("FriendsInfo")
-- 			local friLua = getLuaComponent(self.uiFriInfo)
-- 			friLua:setData(resp)
-- 			friLua.onCloseEvent = function()
-- 				self.uiFriInfo = nil
-- 			end
-- 			UIManager.Instance:BringPanelForward(self.uiFriInfo)
-- 		end
-- 	end
-- end

return FriendsListItem
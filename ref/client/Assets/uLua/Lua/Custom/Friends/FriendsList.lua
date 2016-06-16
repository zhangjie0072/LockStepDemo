FriendsList =  {
	uiName	= "FriendsList",

	datas,
	listType,  --当前数据类型
    listName,  --当前列表的名称

    gFriendsMax,
    gBlackListMax,
    gFriendsApplyMax,
    gFriendGetGiftTimes,
    gFriendPresendGiftTimes,
    gSearchPoolFriendLevelDiff,

    friendChangedFunc,

    last_sysn_time,  --上次同步时间
    isSysn = false,	 --是否正在同步
    isDel = false,   --是否正在删除好友
    DelNum,   --删除好友预制体Num
    --UI

	uiBtnFriendsList,	--左侧菜单，好友列表
	uiBtnApplyList,		--左侧菜单，申请列表
	uiBtnBlackList,		--左侧菜单，黑名单列表
	uiBtnNearList,		--左侧菜单，附近的人列表
	uiBtnGiftList,		--左侧菜单，接收礼物

	uiTxtListNum,       --好友数量 10/100, 列表中的内容的数量
	uiBtnFriendsAdd,    --添加好友

    scroll,
	grid,
    btnBack,

    uiReceive,      --列表下方的文字描述
	uiEmptyText,	--空列表后的文字信息

    uiBtnQuickGive, --一键赠送
    uiBtnQuickGet,  --一键领取

	uiFriendsAdd,

	uiApplyRedDotGameObj,  --申请列表的小红点
	uiGiftRedDotGameObj,   --礼物列表的小红点

	friendsPrefabs,  --好友列表预制体
}

function FriendsList:Awake()
	local transform = self.transform
	local find = function( struct )
		return transform:FindChild(struct)
	end

	self.btnBack = createUI("ButtonBack", self.transform:FindChild('Top/ButtonBack'))

	self.uiBtnFriendsList = find("Left/Friends"):GetComponent('UIToggle')
	self.uiBtnApplyList = find("Left/Apply"):GetComponent("UIToggle")
	self.uiBtnBlackList = find("Left/BlackList"):GetComponent("UIToggle")
	self.uiBtnNearList = find("Left/Nearby"):GetComponent("UIToggle")
	self.uiBtnGiftList = find("Left/Gift"):GetComponent("UIToggle")

	self.uiEmptyText = find("Right/EmptyText"):GetComponent("UILabel")
    self.uiReceive = find("Right/ReceiveNum"):GetComponent("UILabel")

    self.uiTxtListNum = getComponentInChild(self.transform, "Right/ReceiveNum/Num", "UILabel")
    self.uiBtnFriendsAdd = getComponentInChild(self.transform, "Right/ButtonChange", "UIButton")

    self.uiBtnQuickGive = getComponentInChild(self.transform, "Right/BtnQuickGive", "UIButton")
    self.uiBtnQuickGet = getComponentInChild(self.transform, "Right/BtnQuickGet", "UIButton")

	self.uiApplyRedDotGameObj = getChildGameObject(self.transform, "Left/Apply/RedDot")
	self.uiGiftRedDotGameObj = getChildGameObject(self.transform, "Left/Gift/RedDot")

    self.scroll = {}
    self.grid = {}
    self.friendsPrefabs = {}
    for i=1, 5 do
        self.scroll[i] = getComponentInChild(self.transform, "Right/Wear/ScrollView"..i, "UIScrollView")
        self.grid[i] = getComponentInChild(self.scroll[i].transform, "Grid", "UIWrapContent")
        local perfabName = "FriendsListItem"
        if i == 5 then
            perfabName = "FriendsGiftItemRow"
        end

        for j=1, 4 do
        	local Item = createUI(perfabName, self.grid[i].transform)
        	if i == 1 then
	            local lua = getLuaComponent(Item)
	            lua.tfFriendsList = self.transform
	            self.friendsPrefabs[j] = Item
	        end
        end
    end

	self.datas = {}

    self.gFriendsMax = GameSystem.Instance.CommonConfig:GetUInt("gFriendsMax")
    self.gBlackListMax = GameSystem.Instance.CommonConfig:GetUInt("gBlackListMax")
    self.gFriendsApplyMax = GameSystem.Instance.CommonConfig:GetUInt("gFriendsApplyMax")
    self.gFriendGetGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendGetGiftTimes")
    self.gFriendPresendGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendPresendGiftTimes")
    self.gSearchPoolFriendLevelDiff = GameSystem.Instance.CommonConfig:GetUInt("gSearchPoolFriendLevelDiff")

    self.friendChangedFunc = FriendData.FriendListChangedDelegate(self:RefreshListData())
    FriendData.Instance:RegisterOnListChanged(self.friendChangedFunc)

    self.last_sysn_time = os.time()
end

function FriendsList:Start()
	EventDelegate.Add(self.uiBtnFriendsList.onChange, LuaHelper.Callback(self:onToggleChange()))
	EventDelegate.Add(self.uiBtnApplyList.onChange, LuaHelper.Callback(self:onToggleChange()))
	EventDelegate.Add(self.uiBtnBlackList.onChange, LuaHelper.Callback(self:onToggleChange()))
	EventDelegate.Add(self.uiBtnNearList.onChange, LuaHelper.Callback(self:onToggleChange()))
	EventDelegate.Add(self.uiBtnGiftList.onChange, LuaHelper.Callback(self:onToggleChange()))

    addOnClick(self.uiBtnFriendsAdd.gameObject, self:OnBtnFriendsAdd())
    addOnClick(self.uiBtnQuickGive.gameObject, self:OnQuickGive())
    addOnClick(self.uiBtnQuickGet.gameObject, self:OnQuickGet())

    NGUITools.SetActive(self.uiBtnQuickGive.gameObject, false)
    NGUITools.SetActive(self.uiBtnQuickGet.gameObject, false)

	--返回按钮
	local backBtn = getLuaComponent(self.btnBack)
	backBtn.onClick = self:OnBack()

    for i=1, 4 do
        NGUITools.SetActive(self.scroll[i].gameObject, false)
        self.grid[i].onInitializeItem = self:OnUpdateItem1()
    end
    NGUITools.SetActive(self.scroll[1].gameObject, true)
    NGUITools.SetActive(self.scroll[5].gameObject, false)
    self.grid[5].onInitializeItem = self:OnUpdateItem2()

	NGUITools.SetActive(self.uiEmptyText.gameObject, false)

	NGUITools.SetActive(self.uiApplyRedDotGameObj, false)
	NGUITools.SetActive(self.uiGiftRedDotGameObj, false)
end

function FriendsList:Refresh()
	self:RefreshRedDot()
	--每大于5分钟时，刷新一次数据
	local time = os.time()
	if time - self.last_sysn_time > 300 then
		print("req server data list")
		self.last_sysn_time = time

		--刷新好友
		local req = { type = 'FOT_QUERY', }
		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
		--刷新申请列表
		local req = { type = 'FOT_QUERY_APPLY', }
		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
		--刷新黑名单
		local req = { type = 'FOT_QUERY_BLACK', }
		local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)

		self.isSysn = true
	end
	self.uiBtnFriendsList.value = true
end

function FriendsList:RefreshRedDot()
	local apply_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY_APPLY)
	local gift_count = FriendData.Instance:GetListCount(FriendOperationType.FOT_QUERY_GIFT)

	print( string.format("FriendsList:RefreshRedDot==>>> apply:%d gift:%d", apply_count, gift_count) )

	NGUITools.SetActive(self.uiApplyRedDotGameObj, apply_count > 0)

	--还有没有领取的礼物，同时已领取的次数小于最大限制
	NGUITools.SetActive(self.uiGiftRedDotGameObj, gift_count > 0 and FriendData.Instance.get_gift_times < self.gFriendGetGiftTimes)
end

function FriendsList:Update()

end

function FriendsList:FixedUpdate()

end

function FriendsList:OnDestroy()
	FriendData.Instance:UnRegisterOnListChanged(self.friendChangedFunc)
end

--界面关闭时回调函数
function FriendsList:OnClose()
	if self.onClose then
		--print("uiBack",self.uiName,"--:",self.onClose)
		self.onClose()
		self.onClose = nil
		return
	end

	if self.nextShowUI then
		TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
		self.nextShowUI = nil
	else
		TopPanelManager:HideTopPanel()
	end
end


--------------------------------------------------------------------------------------------------
--执行关闭操作
function FriendsList:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

--返回按钮点击事件
function FriendsList:OnBack()
	return function(go)
		self:DoClose()
	end
end

--一键赠送
function FriendsList:OnQuickGive()
	return function()
        if not FunctionSwitchData.CheckSwith(FSID.friends_gold) then return end

        local gFriendPresendGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendPresendGiftTimes")
		if FriendData.Instance.present_times >= gFriendPresendGiftTimes then
			CommonFunction.ShowTip(getCommonStr('STR_FRIENDS_NOT_GIVE'), nil)
			return
		end

		local req = {
			type = 'FOT_PRESEND',
            all_flag = 1,
	    }

	    local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
		CommonFunction.ShowWaitMask()
	end
end

--一键领取
function FriendsList:OnQuickGet()
	return function()
        if not FunctionSwitchData.CheckSwith(FSID.friends_gift) then return end

		local listdata = self.datas[FriendOperationType.FOT_QUERY_GIFT]
		if listdata == nil then
			print("no gift give")
			return
		end

		local max = table.getn(listdata)
		if max <= 0 then
			print("no gift give 111")
			return
		end

        local gFriendGetGiftTimes = GameSystem.Instance.CommonConfig:GetUInt("gFriendGetGiftTimes")
        if FriendData.Instance.get_gift_times >= gFriendGetGiftTimes then
            CommonFunction.ShowTip(getCommonStr("STR_FRIENDS_NOT_GET"), nil)
            print("gift is max")
            return
        end

		local req = {
			type = 'FOT_GETAWARDS',
            all_flag = 1,
        }

        print("send get message")

	    local buf = protobuf.encode("fogs.proto.msg.FriendOperationReq", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.FriendOperationReqID, buf)
		CommonFunction.ShowWaitMask()
	end
end

function FriendsList:OnBtnFriendsAdd()
    return function()
        if not FunctionSwitchData.CheckSwith(FSID.friends_add) then return end

        if not self.uiFriendsAdd then
			self.uiFriendsAdd = createUI("FriendsAdd")
			local script = getLuaComponent(self.uiFriendsAdd)
			script.onCloseEvent = function()
				self.uiFriendsAdd = nil
				print("FriendsAdd:onCloseEvent")
			end
			UIManager.Instance:BringPanelForward(self.uiFriendsAdd)
		end
    end
end

--左侧功能列表菜单变化
function FriendsList:onToggleChange()
	return function()
		if UIToggle.current.value then
			print(UIToggle.current.name)
			if UIToggle.current.name == 'Nearby' then
				for i=1, 5 do
                    NGUITools.SetActive(self.scroll[i].gameObject, false)
                end
				self.uiEmptyText.enabled = true
                NGUITools.SetActive(self.uiReceive.gameObject, false)
				NGUITools.SetActive(self.uiBtnQuickGive.gameObject, false)
                NGUITools.SetActive(self.uiBtnQuickGet.gameObject, false)
            else
                NGUITools.SetActive(self.uiReceive.gameObject, true)
			    self:ReqFriendsList(UIToggle.current.name)
			end
			if UIToggle.current.name == 'Gift' then
				NGUITools.SetActive(self.uiBtnFriendsAdd.gameObject, false)
			else
				NGUITools.SetActive(self.uiBtnFriendsAdd.gameObject, true)
			end
		end
	end
end

function FriendsList:RefreshListData()
	return function(lst)
		self:RefreshRedDot()

		local _, isCurr = self:isCurrTogType(lst)
		if not isCurr then
			return
		end

		if self.isSysn then
			self.isSysn = false
		end

        for i=1, 5 do
            NGUITools.SetActive(self.scroll[i].gameObject, false)
        end

		self.listType = lst
		self.datas[self.listType] = FriendData.Instance:GetList(self.listType)
        local listdata = self.datas[self.listType]
        local max = table.getn(listdata)

        self.uiBtnQuickGive.isEnabled = false
        for k,v in pairs(listdata) do
        	if v.present_flag == 0 then
        		self.uiBtnQuickGive.isEnabled = true
        		break
        	end
        end

        NGUITools.SetActive(self.uiEmptyText.gameObject,(not (max > 0))) --列表空空如也文字提示
        self.uiBtnQuickGet.isEnabled = (max > 0)

        local uiIndex
        if self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY then
            uiIndex = 1 --好友列表
			self.uiEmptyText.text = getCommonStr('STR_FIELD_PROMPT3')
            self.uiReceive.text = string.format(getCommonStr('STR_FRIENDS_NUM'), max, self.gFriendsMax, FriendData.Instance.present_times, self.gFriendPresendGiftTimes)
        elseif self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY_APPLY then
            uiIndex = 2 --申请信息
			self.uiEmptyText.text = getCommonStr('STR_FIELD_PROMPT19')
            self.uiReceive.text = string.format(getCommonStr('STR_FRIENDS_APPLY_NUM'), max)
        elseif self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY_BLACK then
            uiIndex = 3 --黑名单
			self.uiEmptyText.text = getCommonStr('STR_FIELD_PROMPT20')
            self.uiReceive.text = string.format(getCommonStr('STR_FRIENDS_BLACK_NUM'), max, self.gBlackListMax)
        elseif self.listType == fogs.proto.msg.FriendOperationType.FOT_QUERY_GIFT then
            uiIndex = 5 --礼物列表
			self.uiEmptyText.text = getCommonStr('STR_FIELD_PROMPT6')
            self.uiReceive.text = string.format(getCommonStr('STR_FRIENDS_GIFT_NUM'), FriendData.Instance.get_gift_times, self.gFriendGetGiftTimes)
        else
            uiIndex = 4
			self.uiEmptyText.text = ""
			print("uindex => 4  lst:"..tostring(lst))
			return
        end

        NGUITools.SetActive(self.scroll[uiIndex].gameObject, true)
        local uigrid = self.grid[uiIndex]
        local uiscroll = self.scroll[uiIndex]
        --删除好友特殊刷新
        if self.isDel then
        	self.isDel = false
        	uigrid.minIndex = -(max-1)
        	local num = math.abs(tonumber(self.DelNum))
        	for i = 1, 4 do
        		local name = math.abs(tonumber(self.friendsPrefabs[i].name))
        		if name >= num then
        			self:OnUpdateItem1()(self.friendsPrefabs[i], name, name)
        		end
        	end
        else
        	if uiIndex ~= 5 then
	            --一行一列表格
			    uigrid.minIndex = -(max-1)
			    uigrid.maxIndex = 0
			    local itemscount = uigrid.transform.childCount
	            local updateFunc = self:OnUpdateItem1()
			    for i=0, itemscount-1 do
				    local item = uigrid.transform:GetChild(i)
	                item.name = tostring(i)
	                updateFunc(item.gameObject, i, i)
			    end
	        else
	            --一行两列列表
	            local row = math.ceil(max / 2)
	            uigrid.minIndex = -(row - 1)
	            uigrid.maxIndex = 0
	            local itemscount = uigrid.transform.childCount
	            local updateFunc = self:OnUpdateItem2()
	            for i=0, itemscount-1 do
	                local item = uigrid.transform:GetChild(i)
	                item.name = tostring(i)
	                updateFunc(item.gameObject, i, i)
	            end
	        end
        	uigrid:SortAlphabetically()
        	uiscroll:ResetPosition()
        end
        uiscroll.transform:GetComponent("UIPanel"):Refresh()
	end
end

function FriendsList:OnUpdateItem1()
	return function(obj, index, realIndex)
		local i = math.abs(realIndex) + 1

		local listdata = self.datas[self.listType]
		if listdata == nil then
			do return end
		end

        local max = table.getn(listdata)
		if i > max then
            NGUITools.SetActive(obj, false)
			do return end
		end

        NGUITools.SetActive(obj, true)

		local script = getLuaComponent(obj)
		local data = listdata[i]
		script:setData(data, self.listType)
	end
end

function FriendsList:OnUpdateItem2()
    return function(obj, index, realindex)
        local i = math.abs(realindex) + 1

		local listdata = self.datas[self.listType]
		if listdata == nil then
			do return end
		end

        local max = table.getn(listdata)
        local row = math.ceil(max / 2)
		if i > row then
            NGUITools.SetActive(obj, false)
			do return end
		end

        NGUITools.SetActive(obj, true)

        local temp_index = i * 2
        local c1 = obj.transform:GetChild(0)
        local c2 = obj.transform:GetChild(1)

		local script1 = getLuaComponent(c1)
        script1:setData(listdata[temp_index-1])

        if max >= temp_index then
            local script2 = getLuaComponent(c2)
            script2:setData(listdata[temp_index])
            NGUITools.SetActive(c2.gameObject, true)
        else
            NGUITools.SetActive(c2.gameObject, false)
        end
    end
end


function FriendsList:ReqFriendsList(name)
    NGUITools.SetActive(self.uiBtnQuickGive.gameObject, false)
    NGUITools.SetActive(self.uiBtnQuickGet.gameObject, false)

	local lst
	if name == 'Friends' then
		lst = FriendOperationType.FOT_QUERY
        NGUITools.SetActive(self.uiBtnQuickGive.gameObject, true)
	elseif name == 'Apply' then
	    lst = FriendOperationType.FOT_QUERY_APPLY
    elseif name == 'BlackList' then
        lst = FriendOperationType.FOT_QUERY_BLACK
    elseif name == 'Nearby' then
        do return end --暂时没有这个功能
    elseif name == 'Gift' then
        lst = FriendOperationType.FOT_QUERY_GIFT
        NGUITools.SetActive(self.uiBtnQuickGet.gameObject, true)
    end

    self.listName = name
    self:RefreshListData()(lst)
end

function FriendsList:isCurrTogType(lst)
   local uiIndex;
   if lst == fogs.proto.msg.FriendOperationType.FOT_QUERY then
       uiIndex = 1 --好友列表
   elseif lst == fogs.proto.msg.FriendOperationType.FOT_QUERY_APPLY then
       uiIndex = 2 --申请信息
   elseif lst == fogs.proto.msg.FriendOperationType.FOT_QUERY_BLACK then
       uiIndex = 3 --黑名单
   elseif lst == fogs.proto.msg.FriendOperationType.FOT_QUERY_GIFT then
       uiIndex = 5 --礼物列表
   else
       uiIndex = 4
   end

   local ret = function(index, l, n)
       if uiIndex == l then
           return (self.listName == n)
       else
           return false
       end
   end

   if ret(uiIndex, 1, "Friends") or
       ret(uiIndex, 2, "Apply") or
       ret(uiIndex, 3, "BlackList") or
       ret(uiIndex, 4, "Nearby") or
       ret(uiIndex, 5, "Gift") then
       return uiIndex, true
   else
       return uiIndex, false
   end
end

return FriendsList
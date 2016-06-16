FriendsAdd =  {
	uiName	= "FriendsAdd",

    --data
    listData,
	onCloseEvent,

    --ui
	uiBtnClose,
    uiGridWrap,
    uiTxtInput,     --输入框
    uiBtnSearch,    --查找
    uiBtnChange,    --换一批

    uiTogName,
    uiTogID,
}

function FriendsAdd:Awake()
    self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
    
    self.uiGridWrap = getComponentInChild(self.transform, "Window/Down/Wear/ScrollView/Grid", "UIWrapContent")
    local grid = getComponentInChild(self.transform, "Window/Down/Wear/ScrollView/Grid", "UIGrid")
    for j=1, 4 do
        createUI("FriendsListItem", grid.transform)
    end
    grid:Reposition()

    self.uiTogName = getComponentInChild(self.transform, "Window/Up/NameIcon", 'UIToggle')
    self.uiTogID = getComponentInChild(self.transform, "Window/Up/IDIcon", 'UIToggle')

    self.uiTxtInput = getComponentInChild(self.transform, "Window/Up/Input", "UIInput")
    self.uiBtnSearch = getComponentInChild(self.transform, "Window/Up/ButtonSearch", "UIButton")
    self.uiBtnChange = getComponentInChild(self.transform, "Window/Down/ButtonChange", "UIButton")
end

function FriendsAdd:Start()
	local closeBtn = getLuaComponent(self.uiBtnClose)
	closeBtn.onClick = self:OnCloseClick()

    addOnClick(self.uiBtnSearch.gameObject, self:OnSearchClick())
    addOnClick(self.uiBtnChange.gameObject, self:OnChangeClick())

    self.uiGridWrap.onInitializeItem = self:OnUpdateItem()
	
	self:OnChangeClick()()
end

function FriendsAdd:Update()
	
end

function FriendsAdd:FixedUpdate()
	
end

function FriendsAdd:OnDestroy()
	
end

function FriendsAdd:OnClose()
	if self.onCloseEvent then
		self.onCloseEvent()
	end
	NGUITools.Destroy(self.gameObject)
    self.gameObject = nil
end

function FriendsAdd:DoClose()
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

--关闭
function FriendsAdd:OnCloseClick()
	return function(go)
		self:DoClose()
	end
end

function FriendsAdd:OnSearchClick()
	return function(go)
		if self.uiTxtInput.value == "" then
			return
		end
		
        local req = {
            type = 'SFT_PRECISE',
         }
        
        if self.uiTogName.value then
            req.name = self.uiTxtInput.value
        else
            req.acc_id = tonumber(self.uiTxtInput.value)
        end
        
	    local buf = protobuf.encode("fogs.proto.msg.SearchFriend", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.SearchFriendID, buf)

        LuaHelper.RegisterPlatMsgHandler(MsgID.SearchFriendRespID, self:SearchFriendRespHandler(), self.uiName)
		CommonFunction.ShowWaitMask()
		CommonFunction.ShowWait()
	end
end

function FriendsAdd:OnChangeClick()
	return function(go)
		local req = {
				type = 'SFT_CHANGE',
			}

	    local buf = protobuf.encode("fogs.proto.msg.SearchFriend", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.SearchFriendID, buf)

        LuaHelper.RegisterPlatMsgHandler(MsgID.SearchFriendRespID, self:SearchFriendRespHandler(), self.uiName)
        CommonFunction.ShowWaitMask()
        CommonFunction.ShowWait()
	end
end

function FriendsAdd:SearchFriendRespHandler()
	return function(buf)
		print("FriendsAdd:SearchFriendRespHandler")
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.SearchFriendRespID, self.uiName)
        CommonFunction.StopWait()
        local resp, err = protobuf.decode("fogs.proto.msg.SearchFriendResp", buf)
        if not resp then
            error("FriendsAdd:SearchFriendRespHandler resp is nil")
            do return end
        end

        if resp.result ~= 0 then
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            do return end 
        end

        if not self.gameObject then
            return
        end

		printTable(resp.info, true, nil, 1)
        self.listData = resp.info
        self:RefreshListData()
    end
end

function FriendsAdd:RefreshListData()
    local max = table.getn(self.listData)
    print("friends:"..max)
	print(self.uiGridWrap)
	print(self.uiGridWrap.minIndex)
	print(-(max-1))

	
    self.uiGridWrap.minIndex = -(max-1)
	self.uiGridWrap.maxIndex = 0
	local itemscount = self.uiGridWrap.transform.childCount
	for i=0, itemscount-1 do
		local item = self.uiGridWrap.transform:GetChild(i)
		if i < max then
			local index = i + 1
			print("id=>"..self.listData[index].acc_id)
			NGUITools.SetActive(item.gameObject, true)

			local script = getLuaComponent(item)
			script:setData(self.listData[index], nil)
		else
			NGUITools.SetActive(item.gameObject, false)
		end
	end
end

function FriendsAdd:OnUpdateItem()
	return function(obj, index, realIndex)
		local i = math.abs(realIndex) + 1

		if self.listData == nil then
			do return end
		end
		
		if i > table.getn(self.listData) then
			do return end
		end

		local script = getLuaComponent(obj)
		local data = self.listData[i]
		script:setData(data, nil)
	end
end

return FriendsAdd
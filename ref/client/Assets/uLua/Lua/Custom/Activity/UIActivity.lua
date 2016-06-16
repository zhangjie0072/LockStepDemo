--encoding=utf-8

UIActivity = 
{
	uiName = 'UIActivity',

	-----------------parameters
	currentItem,

	--------------------UI
	uiAnimator,
}


-----------------------------------------------------------------
function UIActivity:Awake( ... )
	self.rightGrid = self.transform:FindChild('Right/Scroll/Grid')
	self.left = self.transform:FindChild('Left')
	self.leftScrollContent = self.left:FindChild('Scroll/Content')
	self.getaward = self.transform:FindChild('GetAward'):GetComponent('UIButton')
	self.getawardbg = self.getaward.gameObject.transform:FindChild('Background'):GetComponent('UISprite')
	self.getawardlabel = self.getaward.gameObject.transform:FindChild('Label'):GetComponent('UILabel')

	self.uiAnimator = self.transform:GetComponent('Animator')

	addOnClick(self.getaward.gameObject, self:OnGetAward())
end

function UIActivity:Start( ... )
	self.popupFrame = self.transform:FindChild('PopupFrame')
	local frame = getLuaComponent(self.popupFrame)
	frame.title = getCommonStr(getCommonStr('LABEL_HALL_ACTIVITY'))
	frame.onClose = self:OnCloseClick()
	
	--初始item
	self:InitActivityItem()
	self:RefreshContent(getCommonStr('DAILY_WELFARE'))
end

--固定更新
function UIActivity:FixedUpdate( ... )
	--local totalMinute = cur_hourTime * 60 + cur_minuteTime
	local inTime = GameSystem.Instance.presentHpConfigData:IsGetPresentHP()
	--print("inTime=====>>>>>>" .. tostring(inTime))
	if inTime == 1 and MainPlayer.Instance.MoonHp == 0 then
		self.gethp = true
	elseif inTime == 2 and MainPlayer.Instance.EvenHp == 0 then
		self.gethp = true
	else
		self.gethp = false
	end

	if self.currentItem ~= nil then
		local l = getLuaComponent(self.currentItem.gameObject)
		if l.activityName.text == getCommonStr('DAILY_WELFARE') then
			l.state = self.gethp
			l:RefreshItem(l.state)
		end
	end
	self:Refresh()
end

function UIActivity:OnClose( ... )
	NGUITools.Destroy(self.gameObject)
	self.parent:SetModelActive(true)
end

function UIActivity:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function UIActivity:Refresh( ... )
	-- body
	--print('self.gethp------>>>>' .. tostring(self.gethp))
	self.itemName = getLuaComponent(self.currentItem).activityName.text
	if self.itemName == getCommonStr('DAILY_WELFARE') then
		if self.gethp == true then
			self.getaward.enabled = true
			self.getawardbg.color = Color.New(255/255, 255/255, 255/255, 1)
		else
			self.getaward.enabled = false
			self.getawardbg.color = Color.New(128/255, 128/255, 128/255, 1)
		end
	elseif self.itemName == getCommonStr('DAILY_SIGNIN') then
		self.getaward.enabled = true
		self.getawardbg.color = Color.New(255/255, 255/255, 255/255, 1)
	end
end


-----------------------------------------------------------------
--初始活动项
function UIActivity:InitActivityItem( ... )
	--先清除grid的子项
	CommonFunction.ClearGridChild(self.rightGrid.transform)
	--每日赠送体力
	local activityitem = createUI('ActivityItem', self.rightGrid.transform)
	--初始每日赠送体力为当前item
	self.currentItem = activityitem
	activityitem.transform:FindChild('BG'):GetComponent('UISprite').color = Color.New(1,1,1,1)
	local itemlabel = activityitem.transform:FindChild('Slogan'):GetComponent('UILabel')
	itemlabel.text = GameSystem.Instance.presentHpConfigData:GetTimeInterval()
	--初始每日领取的状态
	local l = getLuaComponent(activityitem)
	l.activityName.text = getCommonStr('DAILY_WELFARE')
	l.state = self.gethp
	l:RefreshItem(l.state)
	l.onClick = self:OnClickItem()

	--每日签到
	local signitem = createUI('ActivityItem', self.rightGrid.transform)
	local l = getLuaComponent(signitem)
	l.activityName.text = getCommonStr('DAILY_SIGNIN')
	l.onClick = self:OnClickItem()
end

--刷新内容
function UIActivity:RefreshContent(name)
	while self.leftScrollContent.transform.childCount > 0 do
		NGUITools.Destroy(self.leftScrollContent.transform:GetChild(0).gameObject)
	end
	local lefttitle = self.left:FindChild('LabelTitle'):GetComponent('UILabel')
	lefttitle.text = getCommonStr(name)
	if name == getCommonStr('DAILY_WELFARE') then
		local content = createUI('ActivityContent', self.leftScrollContent.transform)
		content.transform.localPosition = Vector3.New(-135, 160, 0)
		local tips = content.transform:FindChild('Tip'):GetComponent('UILabel')
		tips.text = '每日' .. GameSystem.Instance.presentHpConfigData:GetTimeInterval() .. '可领取' .. GameSystem.Instance.presentHpConfigData:GetHP() .. '点体力'
		local goods = createUI('GoodsIconConsume', content.transform:FindChild('AwardContent').transform)
		goods.transform.localPosition = Vector3.New(-47, -47, 0)
		GameObject.Destroy(goods.transform:GetComponent('LuaComponent'))
		goods.transform:FindChild('Icon'):GetComponent('UISprite').spriteName = 'com_property_hp'
		local configHp = GameSystem.Instance.presentHpConfigData:GetHP()
		print('configHp:' .. tostring(configHp))
		if configHp ~= '' and configHp ~= nil then
			goods.transform:FindChild('Num'):GetComponent('UILabel').text = configHp
		end
	elseif name == getCommonStr('DAILY_SIGNIN') then
		local content = createUI('ActivityContent', self.leftScrollContent.transform)
		content.transform.localPosition = Vector3.New(-135, 160, 0)
		local tips = content.transform:FindChild('Tip'):GetComponent('UILabel')
		tips.text = getCommonStr('SIGNIN_CONTENT')
		content.transform:FindChild('Award').gameObject:SetActive(false)
	end
end

function UIActivity:SetParent(parent)
	self.parent = parent
end

--关闭
function UIActivity:OnCloseClick( ... )
	return function(go)
		if self.uiAnimator then
			self:AnimClose()
		else
			self:OnClose()
		end
	end
end

function UIActivity:OnClickItem( ... )
	return function (name)
		if name == self.itemName then
			return
		end
		self:RefreshContent(name)
		local itemscount = self.rightGrid.transform.childCount
		for i=0,itemscount - 1 do
			local item = self.rightGrid.transform:GetChild(i)
			local l = getLuaComponent(item.gameObject)
			if name == l.activityName.text then
				self.currentItem = item
				item:FindChild('BG'):GetComponent('UISprite').color = Color.New(1,1,1,1)
				if name == getCommonStr('DAILY_WELFARE') then
					self.getawardlabel.text = getCommonStr('RECEIVE')
				elseif name == getCommonStr('DAILY_SIGNIN') then
					self.getawardlabel.text = getCommonStr('SIGNIN')
				end
			else
				item:FindChild('BG'):GetComponent('UISprite').color = Color.New(239/255,216/255,164/255,1)
			end
		end
	end
end

--点击领取按钮
function UIActivity:OnGetAward( ... )
	return function(go)
		self.itemName = getLuaComponent(self.currentItem).activityName.text
		if self.itemName == getCommonStr('DAILY_WELFARE') then
			local req = 
			{
				acc_id = MainPlayer.Instance.AccountID
			}
			local buf = protobuf.encode('fogs.proto.msg.GetFreeHpReq', req)
			if self.gethp == true then
				LuaHelper.SendPlatMsgFromLua(MsgID.GetFreeHpReqID, buf)
				LuaHelper.RegisterPlatMsgHandler(MsgID.GetFreeHpRespID, self:OnGetFreeHpRespHandler(), self.uiName)
				CommonFunction.ShowWait()
			end
		elseif self.itemName == getCommonStr('DAILY_SIGNIN') then
			print('Now I am signin-------------->>>>>>>>>>')
			self.parent:OpenSignInRespHandler()
		end
	end
end
--处理回复消息
function UIActivity:OnGetFreeHpRespHandler( ... )
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetFreeHpRespID, self.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode('fogs.proto.msg.GetFreeHpResp', message)
		if resp ~= nil then
			print('resp.result =====>>>>>>' .. tostring(resp.result))
			if resp.result ~= 0 then
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
				return
			end
			MainPlayer.Instance.MoonHp = resp.noon_hp
			MainPlayer.Instance.EvenHp = resp.even_hp
			CommonFunction.ShowPopupMsg(getCommonStr('RECEIVE_SUCCESS'),nil,nil,nil,nil,nil)
			--[[local l = getLuaComponent(self.currentItem.gameObject)
			l.state = false
			l:RefreshItem(l.state)
			self:Refresh()--]]
		else
			error("UIActivity:", err)
		end

	end
end

return UIActivity

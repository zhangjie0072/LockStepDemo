--encoding=utf-8

--邮件类型
MailType =
{
	SYSTEM			= 1, --系统邮件
	OTHER				= 2, --其它
}

--邮件状态
MailState =
{
	UNREAD			= 1, --未读取
	READ_NOT_GET		= 2, --读取并且未领取附件
	READ_GET			= 3, --读取并且已领取附件
	READ_WITHOUT		= 4, --读取，无附件
}


UIMail = UIMail or
{
	uiName = 'UIMail',

	----------------------------------
	mailList = {},

	parent,
	onClose,
	banTwice = false,

	----------------------------------UI
	--
	uiMailList,
	--邮件列表
	uiListSV,
	uiListGrid,
	uiCloseBtn,
	--
	--uiDetail,
	uiDetailContent,
	uiAnimator,

	sprBgIcon,
}


-----------------------------------------------------------------
--Awake
function UIMail:Awake( ... )
	local transform = self.transform

	--邮件列表
	self.uiMailList = transform:FindChild('Window/MailList')

	self.uiListSV = transform:FindChild('Window/MailList/List'):GetComponent('UIScrollView')
	self.uiListGrid = transform:FindChild('Window/MailList/List/Grid'):GetComponent('UIGrid')
	self.uiCloseBtn = createUI('ButtonClose', transform:FindChild('Window/ButtonClose'))
	--邮件内容
	--addOnClick(self.uiCloseBtn.gameObject, self:OnCloseClick())
	--
	--self:Refresh()
	self.sprBgIcon = transform:FindChild('Window/BgIcon'):GetComponent('UISprite')

	self.uiAnimator = self.transform:GetComponent('Animator')
end

--Start
function UIMail:Start( ... )
	--body
	NGUITools.BringForward(self.gameObject)
	--关闭
	--local popupFrame = getLuaComponent(self.transform:FindChild('PopupFrame').gameObject)
	--popupFrame.onClose = self:OnCloseClick()
	local btnClose = getLuaComponent(self.uiCloseBtn)
	btnClose.onClick = self:OnCloseClick()
end

--Update
function UIMail:FixedUpdate( ... )
	-- body
end

function UIMail:OnClose( ... )
	if self.onClose then
		self.onClose()
	end
	NGUITools.Destroy(self.gameObject)
	self.parent:SetModelActive(true)
end

function UIMail:OnDestroy( ... )
	-- body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

--Refresh
function UIMail:Refresh()
	NGUITools.BringForward(self.gameObject)
end


-----------------------------------------------------------------
--
function UIMail:SetParent(parent)
	self.parent = parent
end

--
function UIMail:SetMailList(mailList)
	self.mailList = mailList
end

--
function UIMail:InitMailList( ... )
	--排序
	self:SortingMails()
	--清除实例化控件
	CommonFunction.ClearGridChild(self.uiListGrid.transform)
	--初始化邮件列表
	for k, v in pairs(self.mailList) do
		local go = CommonFunction.InstantiateObject('Prefab/GUI/MailUnreadItem', self.uiListGrid.transform)
		local itemicon = go.transform:FindChild('Icon'):GetComponent('UISprite')
		--local itemSprite = go.transform:GetComponent('UISprite')
		if v.state == MailState.UNREAD then
			itemicon.spriteName = 'mail_icon_close'
			--local r, g, b = 241, 217, 165
			--itemSprite.color = Color.New(r/255, g/255, b/255, 1)
		else
			itemicon.spriteName = 'mail_icon_open'
			-- if v.state == MailState.READ_GET then
			-- 	itemicon.spriteName = 'mail_icon_open'
			-- elseif v.state == MailState.READ_NOT_GET then
			-- 	itemicon.spriteName = 'mail_icon_close'
			-- end
			--local r, g, b = 255, 255, 255
			--itemSprite.color = Color.New(r/255, g/255, b/255, 1)
		end

		local item =getLuaComponent(go)
		item:SetData(v)
		item:SetParent(self)
		item:SetDragSV(self.uiListSV)
		item.isOpen = false
		addOnClick(go.gameObject, item:OnOperClick())

	end
	self.uiListGrid.repositionNow = true
	self.uiListSV:ResetPosition()
	if not (#self.mailList > 0) then
		NGUITools.SetActive(self.sprBgIcon.gameObject, true)
	end
end

--
function UIMail:SortingMails()
	local mailnum = table.getn(self.mailList)
	if mailnum <= 1 then
		return
	end

	table.sort(self.mailList,
		function (mail1, mail2)
			if mail1 == nil and mail2 ~= nil then
				return false
			end
			if mail1 ~= nil and mail2 == nil then
				return true
			end
			if (next(mail1.attachment.attachment) and next(mail2.attachment.attachment))
				or (not next(mail1.attachment.attachment) and not next(mail2.attachment.attachment))  then
				if mail1.state == mail2.state then
					if mail1.send_time == mail2.send_time then
						return mail1.id > mail2.id
					else
						return mail1.send_time > mail2.send_time
					end
				else
					return mail1.state < mail2.state
				end
			elseif next(mail1.attachment.attachment) and not next(mail2.attachment.attachment) then
				return true
			elseif not next(mail1.attachment.attachment) and next(mail2.attachment.attachment) then
				return false
			-- else
			-- 	return mail1.state < mail2.state
			end
		end)
end

--
function UIMail:ReadMail(uuid)
	for k, v in pairs(self.mailList) do
		if v.uuid == uuid then
			if v.attachment.attachment and next(v.attachment.attachment) then
				v.state = MailState.READ_NOT_GET
			else
				v.state = MailState.READ_WITHOUT
			end
			return v
		end
	end
end

--由id读取邮件改成从服务器直接获取信息
function UIMail:ReadMailResp()
	return function (message)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.ReadMailRespID, MailUnreadItem.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode('fogs.proto.msg.ReadMailResp', message)
		if resp == nil then
			Debugger.LogError('------ReadMailResp error: ', err)
			return
		end

		if resp.result ~= 0 then
			print('error --  ReadMailResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		local data = self:ReadMail(resp.mail_id)
		if data then
			self:OnOpenMail(data, resp.mail_id)
		end
	end
end

--点击关闭事件
function UIMail:OnCloseClick()
	return function (go)
		-- if self.uiAnimator then
		-- 	self:AnimClose()
		-- else
			self:OnClose()
		-- end
	end
end

--
function UIMail:OnOpenMail(data, mail_id)
	--
	--[[
	self.uiDetail = createUI('MailDetail', self.transform)
	self.uiDetail.transform.localScale = Vector3.one
	UIManager.Instance:BringPanelForward(self.uiDetail)

	self.uiDetail.transform:FindChild('Frame/TitleBar'):GetComponent('UISprite').color = Color.New(85/255, 85/255, 85/255, 1)
	local frame = getLuaComponent(self.uiDetail.transform:FindChild('Frame').gameObject)
	frame:SetTitle(CommonFunction.GetConstString('STR_READMAIL'))
	frame.onClose = self:OnDetailCloseClick()
	--]]
	--
	self.uiDetailContent = createUI('MailDetailContent')
	UIManager.Instance:BringPanelForward(self.uiDetailContent)
	local detail = getLuaComponent(self.uiDetailContent)
	detail:SetData(data)
	detail:SetParent(self)
	detail:InitAttachment()

	for k, v in pairs(self.mailList) do
		if v.uuid == mail_id then
			self.selected_mail = v
			if v.state == MailState.UNREAD then
				v.state = MailState.READ_NOT_GET
			end
		end
	end

	--
	-- frame.showCorner = false
	-- frame.buttonLabels = {''}
	-- frame.buttonHandlers = {}
	if next(data.attachment.attachment)
		and data.state ~= MailState.READ_GET
		and data.state ~= MailState.READ_WITHOUT then
		detail.uiAwardBtn.gameObject:SetActive(true)
		--awardBtn.gameObject:SetActive(true)
		--addOnClick(awardBtn.gameObject, detail:OnGetAttachmentClick())
	end
	if not next(data.attachment.attachment)
		or data.state == MailState.READ_GET
		or data.state == MailState.READ_WITHOUT then
		NGUITools.Destroy(detail.uiAwardBtn.gameObject)
	end
end

--
function UIMail:OnDetailCloseClick()
	return function (go)
		NGUITools.Destroy(self.uiDetailContent.gameObject)
		self:InitMailList()
	end
end

--
function UIMail:GetAttachment(uuid)
	local list = {}
	local num = 1 		--table.sort 从下标1开始排
	for k, v in pairs(self.mailList) do
		local d = false
		if v.get_delete == 1 and v.uuid == uuid then
			d = true
		elseif v.get_delete == 0 and v.uuid == uuid then
			v.state = MailState.READ_GET
		end
		if d == false then
			list[num] = v
			num = num + 1
		end
	end
	self.mailList = list
end

--
function UIMail:GetAttachmentResp()
	return function (message)
		if self.banTwice then
			return
		end
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.GetAttachmentRespID, UIMail.uiName)
		CommonFunction.StopWait()
		local resp, err = protobuf.decode('fogs.proto.msg.GetAttachmentResp', message)
		if resp == nil then
			Debugger.LogError('------GetAttachmentResp error: ', err)
			return
		end

		if resp.result ~= 0 then
			print('error --  GetAttachmentResp return failed: ', resp.result)
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			return
		end
		self.banTwice = true
		local data = self:ReadMail(resp.mail_id)
		local getGoods = getLuaComponent(createUI('GoodsAcquirePopup', self.transform))
		for i,v in ipairs(data.attachment.attachment) do
			getGoods:SetGoodsData(v.id, v.value)
		end
		getGoods.onClose = function ( ... )
			self.banTwice = false
		end

		self:GetAttachment(resp.mail_id)
		MainPlayer.Instance:GetMailAttachment(resp.mail_id)
		self:OnDetailCloseClick()()

		UpdateRedDotHandler.MessageHandler("Mail")
		-- self.parent:RefreshMailTip(self.mailList)
		--CommonFunction.ShowPopupMsg(getCommonStr("RECEIVE_SUCCESS"),nil,nil,nil,nil,nil)
	end
end


return UIMail

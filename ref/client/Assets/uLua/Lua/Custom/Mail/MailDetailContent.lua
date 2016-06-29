--encoding=utf-8


MailDetailContent = MailDetailContent or
{
	uiName = 'MailDetailContent',

	----------------------------------
	data,

	parent,

	----------------------------------UI
	uiCloseBtn,
	uiAwardBtn,
	uiContent,
	uiAttachment,
	uiGoodsGrid,
	uiSender,
}

MailState =
{
	UNREAD			= 1,
	READ_NOT_GET	= 2,
	READ_GET		= 3,
	READ_WITHOUT	= 4,
}

-----------------------------------------------------------------
--Awake
function MailDetailContent:Awake( ... )
	local transform = self.transform

	--
	self.uiTitle = transform:FindChild('Window/MailContent/Content/Title'):GetComponent('UILabel')
	self.uiContent = transform:FindChild('Window/MailContent/Content/LabelContent'):GetComponent('UILabel')

	--
	self.uiAttachment = transform:FindChild('Window/MailContent/Bottom')

	self.uiGoodsGrid = transform:FindChild('Window/MailContent/Bottom/Grid'):GetComponent('UIGrid')
	--
	self.uiSender = transform:FindChild('Window/MailContent/Content/Addresser'):GetComponent('UILabel')
	self.uiCloseBtn = createUI('ButtonClose', transform:FindChild('Window/ButtonClose'))
	--

	self.uiAwardBtn = transform:FindChild('Window/MailContent/Bottom/GetAward'):GetComponent('UIButton')
	local awardBtnLable = self.uiAwardBtn.gameObject.transform:FindChild('Text'):GetComponent('UILabel')
	awardBtnLable.text = CommonFunction.GetConstString('RECEIVE')
end

--Start
function MailDetailContent:Start( ... )
	local btnClose = getLuaComponent(self.uiCloseBtn)
	btnClose.onClick = self:OnCloseClick()
	addOnClick(self.uiAwardBtn.gameObject, self:OnGetAttachmentClick())
end

function MailDetailContent:OnCloseClick( ... )
	return function (go)
		NGUITools.Destroy(self.gameObject)
		self.parent:InitMailList()
	end
end

--Update
function MailDetailContent:Update( ... )
	-- body
end

--Refresh
function MailDetailContent:Refresh()

end


-----------------------------------------------------------------
--
function MailDetailContent:SetData(data)
	self.data = data
end

--
function MailDetailContent:SetParent(parent)
	self.parent = parent
end

--
function MailDetailContent:InitAttachment()
	self.uiTitle.text = self.data.title
	self.uiContent.text = self.data.content
	self.uiSender.text = CommonFunction.GetConstString('STR_SENDER') .. self.data.sender

	CommonFunction.ClearGridChild(self.uiGoodsGrid.transform)

	--添加附件
	--[[local attachment = GameSystem.Instance.AwardPackConfigData:GetAwardPackDatasByID(self.data.attachment)
	if attachment == nil then
		self.uiAttachment.gameObject:SetActive(false)
		return
	end
	--READ_GET = 3
	if self.data.state == 3 then
		return
	end

	for i = 0, attachment.Count - 1 do
		local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.uiGoodsGrid.transform))
		goodsIcon.goodsID = attachment[i].award_id
		goodsIcon.num = attachment[i].award_value
		goodsIcon.hideLevel = true
		goodsIcon.hideNum = false
	end]]
	if self.data.state == MailState.READ_GET then
		return
	end

	local attachment = self.data.attachment.attachment
	for i,v in ipairs(attachment) do
		local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.uiGoodsGrid.transform))
		goodsIcon.goodsID = v.id
		goodsIcon.num = v.value
		goodsIcon.hideLevel = true
		goodsIcon.hideNum = false
		goodsIcon.hideNeed = true
	end


	self.uiGoodsGrid.repositionNow = true
end

--
function MailDetailContent:OnGetAttachmentClick( ... )
	return function (go)
		if not FunctionSwitchData.CheckSwith(FSID.email_accessory) then return end

		local req = {mail_id = self.data.uuid}
		local msg = protobuf.encode("fogs.proto.msg.GetAttachment", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetAttachmentID, msg)
		CommonFunction.ShowWait()
		--注册回复处理消息
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetAttachmentRespID, self.parent:GetAttachmentResp(), self.parent.uiName)
	end
end


return MailDetailContent
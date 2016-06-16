--encoding=utf-8


MailDetail = MailDetail or
{
	uiName = 'MailDetail',
	
	----------------------------------
	data,
	
	parent,

	----------------------------------UI
	
}


-----------------------------------------------------------------
--Awake
function MailDetail:Awake( ... )
	local transform = self.transform

	--
	self.uiTitle = transform:FindChild('Label'):GetComponent('UILabel')
	self.uiContent = transform:FindChild('Scroll/MailContent/Content/LabelContent'):GetComponent('UILabel')

	--
	self.uiAttachment = transform:FindChild('Scroll/MailContent/Bottom')

	self.uiGoodsList = transform:FindChild('Scroll/MailContent/Bottom/GoodsList'):GetComponent('UIScrollView')
	self.uiGoodsGrid = transform:FindChild('Scroll/MailContent/Bottom/GoodsList/Grid'):GetComponent('UIGrid')
	--
	self.uiSender = transform:FindChild('Scroll/MailContent/Content/Addresser'):GetComponent('UILabel')

	--
end

--Start
function MailDetail:Start( ... )
	--body
end

--Update
function MailDetail:Update( ... )
	-- body
end

--Refresh
function MailDetail:Refresh()

end


-----------------------------------------------------------------
--
function MailDetail:SetData(data)
	self.data = data
end

--
function MailDetail:SetParent(parent)
	self.parent = parent
end

--
function MailDetail:InitAttachment()
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
	local attachment = self.data.attachment.attachment
	for i,v in ipairs(attachment) do
		local goodsIcon = getLuaComponent(createUI("GoodsIcon", self.uiGoodsGrid.transform))
		goodsIcon.goodsID = v.id
		goodsIcon.num = v.value
		goodsIcon.hideLevel = true
		goodsIcon.hideNum = false
	end


	self.uiGoodsGrid.repositionNow = true
	self.uiGoodsList:ResetPosition()
end

--
function MailDetail:OnGetAttachmentClick( ... )
	return function (go)
		local req = {mail_id = self.data.uuid}
		local msg = protobuf.encode("fogs.proto.msg.GetAttachment", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.GetAttachmentID, msg)
		CommonFunction.ShowWait()
		--注册回复处理消息
		LuaHelper.RegisterPlatMsgHandler(MsgID.GetAttachmentRespID, self.parent:GetAttachmentResp(), self.parent.uiName)
	end
end


return MailDetail
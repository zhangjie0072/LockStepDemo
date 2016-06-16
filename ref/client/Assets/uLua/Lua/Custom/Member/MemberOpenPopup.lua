-- 20150616_195039


MemberOpenPopup =  {
	uiName = 'MemberOpenPopup',
	go={},
	member_item= nil,
}




function MemberOpenPopup:Awake()
	self.go = {}
	self.go.member_item_node = self.transform:FindChild('MemberItemNode')
	self.go.close_btn = self.transform:FindChild('Close').gameObject

	self.go.open_btn = self.transform:FindChild('OpenBtn').gameObject


	self.go.cost_icon = self.transform:FindChild('SubtitlePrice/Price/Icon'):GetComponent('UISprite')
	
	self.go.cost_num = self.transform:FindChild('SubtitlePrice/Price/Num'):GetComponent('UILabel')

	self.go.get_btn = self.transform:FindChild('SubtitlePrice/Get').gameObject

	addOnClick(self.go.close_btn,self:click_close())
	addOnClick(self.go.open_btn,self:click_open())
	addOnClick(self.go.get_btn,self:click_get())

end

function MemberOpenPopup:Start()
	local pos = self.transform.position
	pos.z = -500*0.002234637
	self.transform.position = Vector3.New(pos.x,pos.y,pos.z)

	local member_item = createUI('MemberItem',self.go.member_item_node)
	local item_script = getLuaComponent(member_item)
	 item_script:set_role_base_config(self.member_item.role_base_config)
	local id = self.member_item.id
	item_script:update_by_id(id)

	local quality = self.member_item.quality
	print('MemberOpenPopup:Start quality='..tostring(quality))

	local quality_data= GameSystem.Instance.RoleQualityConfigData:GetRoleQualityData(id,quality)
	
	local piece_enum = quality_data.piece_id:GetEnumerator()
	piece_enum:MoveNext()
	local piece_id = piece_enum.Current

	self.cur_num = MainPlayer.Instance:GetGoodsCount(piece_id)

	piece_enum = quality_data.piece_num:GetEnumerator()
	piece_enum:MoveNext()
	self.need_num = piece_enum.Current
	

	local goods_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(piece_id)
	
	
	self.go.cost_icon.spriteName = goods_config.icon
	print('MemberOpenPopUp goods_config.icon='..goods_config.icon)

	self.go.cost_num.text = tostring(self.cur_num)..'/'..tostring(self.need_num)

end


function MemberOpenPopup:close()
	if self.gameObject then
	NGUITools.Destroy(self.gameObject)
	end
end

function MemberOpenPopup:click_close()
	return function()
	  self:close()
	end
end


function MemberOpenPopup:click_get()
	return function()
	  print('MemberOpenPopup:click_get() is called')
	  
	  local t = createUI('PieceLink',self.transform)
	  self.piece_link = getLuaComponent(t)
	  self.piece_link:set_member_item(self.member_item)
	  NGUITools.BringForward( t)

	end
end


function MemberOpenPopup:click_open()
	return function()
	  if self.need_num > self.cur_num then
		  CommonFunction.ShowTip(getCommonStr("RECURIT_LACK_OF_GOODS"))
		  playSound("UI/UI-wrong")
		  return
	  end

	  print('member_id = '..tostring(self.member_item.id))

	  local operation = {
		  role_id = self.member_item.id
	  }

	  local req = protobuf.encode("fogs.proto.msg.InviteRoleReq",operation)
	  LuaHelper.SendPlatMsgFromLua(MsgID.InviteRoleReqID,req)
	end
end





return MemberOpenPopup

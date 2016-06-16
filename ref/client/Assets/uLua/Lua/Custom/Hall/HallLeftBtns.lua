require "Custom/Mail/UIMail"



HallLeftBtns = 
	{
	uiName = 'HallLeftBtns',
	}




-- HallLeftBtns._transform = nil

function HallLeftBtns:Awake()
	print('HallLeftBtns.Init is called')
	

	local mail = self.transform:FindChild("Mail").gameObject
	addOnClick(mail, self:OnClickMail())
	--UIEventListener.Get(mail).onClick = LuaHelper.VoidDelegate(self:OnClickMail)

	local recharge = self.transform:FindChild("Recharge").gameObject
	UIEventListener.Get(recharge).onClick = LuaHelper.VoidDelegate(self.OnClickRecharge)

	local activity = self.transform:FindChild("Activity").gameObject
	--UIEventListener.Get(activity).onClick = LuaHelper.VoidDelegate(self.OnClickActivity)
	addOnClick(activity, self:OnClickActivity())
end


function HallLeftBtns.Start()
	
end





function HallLeftBtns:OnClickMail()
	return function (go)	  
	print("HallLeftBtns.OnClickMail=",go);

	local msg = protobuf.encode("fogs.proto.msg.OpenMailSys", {})
	LuaHelper.SendPlatMsgFromLua(MsgID.OpenMailSysID, msg)
    CommonFunction.ShowWait()
	--注册打开商店的回复处理消息
	local hall = getLuaComponent(self.transform.parent)
	LuaHelper.RegisterPlatMsgHandler(MsgID.MailInfoNotifyID, hall:MailInfoNotify(), UIMail.uiName)
	end
end

function HallLeftBtns.OnClickRecharge( go )
	print("HallLeftBtns.OnClickRecharge=",go);
	CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil,nil,nil);
end


function HallLeftBtns:OnClickActivity()
	return function (go)
		print("HallLeftBtns.OnClickActivity=",go)
		local hall = getLuaComponent(self.transform.parent)
		hall:OpenActivity()
	end
end



return HallLeftBtns

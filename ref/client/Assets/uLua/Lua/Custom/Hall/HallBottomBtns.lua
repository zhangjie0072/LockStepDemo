

HallBottomBtns = 
	{
	uiName = 'HallBottomBtns',
	}

function HallBottomBtns:Awake()
	local transform = self.transform   

	self._grid = transform:FindChild('Grid').gameObject
	
	local member = transform:FindChild('Grid/Member').gameObject
	addOnClick(member, self:OnClickMember())

	local package = transform:FindChild('Grid/Package').gameObject
	addOnClick(package, self:OnClickPackage())

	local rank = transform:FindChild('Grid/Rank').gameObject
	addOnClick(rank, self:OnClickRank())

	local club = transform:FindChild('Grid/Club').gameObject
	addOnClick(club, self:OnClickClub())
	
	local social = transform:FindChild('Grid/Social').gameObject
	addOnClick(social, self:OnClickSocial())

	local task = transform:FindChild('Grid/Task').gameObject
	addOnClick(task, self:OnClickTask())

	self._popBox = transform:FindChild('PopBar'):GetComponent('UIToggle')
	self._toShow = transform:FindChild('PopBar/ToShow').gameObject
	self._toHide = transform:FindChild('PopBar/ToHide').gameObject
	UIEventListener.Get(self._popBox.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClickPopBox())

	self:UpdatePopBox()
end


function HallBottomBtns:OnClickPopBox(go)
	return function()
	self:UpdatePopBox()
	end
end


function HallBottomBtns:UpdatePopBox()
	local v = self._popBox.value
	
	self._grid:SetActive(v)
	self._toShow:SetActive(not v)
	self._toHide:SetActive( v)
end


function HallBottomBtns:OnClickMember()
	return function(go)
		TopPanelManager:ShowPanel('UIMember')
	end
end


function HallBottomBtns:OnClickPackage( go )
	return function()
		local parent_go = self.transform.parent.gameObject
		NGUITools.SetActive(parent_go,false)
		local uiBase = UIManager.Instance.m_uiRootBasePanel.transform
		local package = CommonFunction.InstantiateObject("Prefab/GUI/UIPackage",uiBase)
		local package_script = getLuaComponent(package)
		package_script.pre_go = parent_go
		NGUITools.BringForward(package)
	end
end

function HallBottomBtns:OnClickRank( go )
	return function (go)	  
		--CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil);
		local hall = getLuaComponent(self.transform.parent)
		local req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 3, --日常任务
		}
		local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)
		CommonFunction.ShowWait()

		--注册回复处理消息
		TaskRespHandler.parent = hall
		LuaHelper.RegisterPlatMsgHandler(MsgID.TaskInfoRespID, TaskRespHandler.process(), UITask.uiName)
	end
end

function HallBottomBtns:OnClickClub( go )
	return function (go)	  
		--CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil);
		TopPanelManager:ShowPanel("UILottery")
	end
end



function HallBottomBtns:OnClickSocial( go )
	return function (go)	  
		--CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil);
		UIManager.Instance:BringPanelForward(createUI("UICreateTeam"))
	end
end


function HallBottomBtns:OnClickTask( go )
	return function (go)   
		local hall = getLuaComponent(self.transform.parent)
		local req = {
			acc_id = MainPlayer.Instance.AccountID,
			type = 2, --普通任务
		}
		local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)
		CommonFunction.ShowWait()

		--注册回复处理消息
		TaskRespHandler.parent = hall
		LuaHelper.RegisterPlatMsgHandler(MsgID.TaskInfoRespID, TaskRespHandler.process(), UITask.uiName)
	end
end




return HallBottomBtns

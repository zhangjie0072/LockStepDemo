--encoding=utf-8

UICreateTeam = {
	uiName = "UICreateTeam",

	----------------------------------

	nameBase1,
	nameBase2,
	nameBase3,
	nameCount1,
	nameCount2,
	nameCount3,


	-----------------UI

	uiAnimator,
	uiInput,
}


-----------------------------------------------------------------
function UICreateTeam:Awake()
	self.lblName = getComponentInChild(self.transform, "Window/InputAccount/LabelNaming", "UILabel")
	self.btnOK = getChildGameObject(self.transform, "Window/ButtonOK")
	self.btnRandom = getChildGameObject(self.transform, "Window/RandomName")

	self.uiAnimator = self.transform:GetComponent('Animator')
	self.uiInput = self.transform:FindChild("Window/InputAccount"):GetComponent("UIInput")
	self.uiInput.finallyLimit = 7
	self.uiInput.defaultText = getCommonStr("LOGIN_TIPS")

	addOnClick(self.btnOK, self:MakeOnOK())
	addOnClick(self.btnRandom, self:MakeOnRandom())

	UICreateTeam.LoadAllNameBase()
end

function UICreateTeam:Start()
	local pos = self.transform.localPosition
	pos.z = -500
	self.transform.localPosition = pos

	LuaHelper.RegisterPlatMsgHandler(MsgID.CreateTeamRespID, self:MakeCreateTeamHandler(), self.uiName)
end

function UICreateTeam:FixedUpdate()
	-- body
end

function UICreateTeam:OnClose()
	-- body
end

function UICreateTeam:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.CreateTeamRespID, self.uiName)

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-----------------------------------------------------------------
function UICreateTeam.LoadAllNameBase()
	if not UICreateTeam.nameBase1 then
		math.randomseed(os.time())
		UICreateTeam.LoadNameBase(1)
	end
	if not UICreateTeam.nameBase2 then
		UICreateTeam.LoadNameBase(2)
	end
	if not UICreateTeam.nameBase3 then
		UICreateTeam.LoadNameBase(3)
	end
end

function UICreateTeam.LoadNameBase(index)
	local text = ResourceLoadManager.Instance:GetConfigText("Config/Func/NameBase" .. index)
	loadstring(text)()
	local nameTable ={
		NameBase1,
		NameBase2,
		NameBase3,
	}
	UICreateTeam["nameBase" .. index] = nameTable[index]
	UICreateTeam["nameCount" .. index] = table.getn(nameTable[index])
end

function UICreateTeam.GenerateName()
	UICreateTeam.LoadAllNameBase()

	local name1 = UICreateTeam.nameBase1[math.floor(math.random(1, UICreateTeam.nameCount1))]
	local name2 = UICreateTeam.nameBase2[math.floor(math.random(1, UICreateTeam.nameCount2))]
	local name3 = UICreateTeam.nameBase3[math.floor(math.random(1, UICreateTeam.nameCount3))]
	if name3 == " " then
		return name1 .. name2
	else
		return name1 .. name3 .. name2
	end
end

function UICreateTeam:MakeOnRandom()
	return function ()
		self.lblName.text = UICreateTeam.GenerateName()
		self.uiInput.value = self.lblName.text
	end
end

function UICreateTeam:MakeOnOK()
	return function ()
	if self.lblName.text == getCommonStr("LOGIN_TIPS") then
		CommonFunction.ShowPopupMsg(getCommonStr('LOGIN_TIPS'),nil,nil,nil,nil,nil)
		return
	end

	if string.find(self.lblName.text,"\n") or string.find(self.lblName.text," ") then
		CommonFunction.ShowPopupMsg(getCommonStr("INVALID_NAME"),nil,nil,nil,nil,nil)
		return
	end
		local req = {
			name = self.lblName.text
		}

		local buf = protobuf.encode("fogs.proto.msg.CreateTeam", req)
		LuaHelper.SendPlatMsgFromLua(MsgID.CreateTeamID, buf)
		CommonFunction.ShowWaitMask()
	end
end

function UICreateTeam:MakeCreateTeamHandler()
	return function (buf)
		CommonFunction.HideWaitMask()
		local resp, err = protobuf.decode("fogs.proto.msg.CreateTeamResp", buf)
		if resp then
			if resp.result == 0 then
				print("Change team name succeed", resp.name)
				MainPlayer.Instance.Name = resp.name
				MainPlayer.Instance.CreateStep = MainPlayer.Instance.CreateStep + 1
				NGUITools.Destroy(self.gameObject)
				MainPlayer.Instance:AddCreateNewRoleLog(false)
				MainPlayer.Instance:AddCreateNewRoleLog(true)
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error(err)
		end
	end
end

return UICreateTeam

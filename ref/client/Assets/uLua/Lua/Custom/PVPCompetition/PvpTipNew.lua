module("PvpTipNew", package.seeall)

uiName = 'PvpTipNew'

--parameters
averTime = 0
matchType = ""
onCancel = nil
onTop = nil
onDown = nil
onCancelMatchHandler = nil
onCancelClick = nil

--variables
waitTime = 0
actionOnTimer = nil



function Awake(self)
    self.uiWaitAverTime = self.transform:FindChild('WaitedTime'):GetComponent('UILabel')
    self.uiArrow = self.transform:FindChild('Arrow'):GetComponent('UISprite')
    self.uiArrow1 = self.transform:FindChild('SmallTip/Arrow2'):GetComponent('UISprite')
    self.waitTimeLabel = self.transform:FindChild('labelWaitTime'):GetComponent('UILabel')
    self.waitTimeLabel.text = '0'
    self.uiMask = self.transform:FindChild('Mask')

    local cancelBtn = self.transform:FindChild('buttonCancel/ButtonOK1'):GetComponent('UIButton')
    self.colliderCancel = self.transform:FindChild('buttonCancel/ButtonOK1'):GetComponent('BoxCollider')

    self.uiCancleText = self.transform:FindChild('buttonCancel/ButtonOK1/Label'):GetComponent("MultiLabel")

    self.uiAnimator = self.transform:GetComponent('Animator')

    addOnClick(cancelBtn.gameObject, self:MakeOnCancelClick())
    addOnClick(self.uiArrow.gameObject, self:ChangeTipState())
    addOnClick(self.uiArrow1.gameObject, self:ChangeTipState())

    LuaHelper.RegisterPlatMsgHandler(MsgID.CancelMatchRespID, self:CancelMatchHandler(), self.uiName)
end

function Start(self)
    self.uiWaitAverTime.text = os.date('%M:%S', self.averTime)
    self.actionOnTimer = LuaHelper.Action(self:MakeOnTimer())
    Scheduler.Instance:AddTimer(1, true, self.actionOnTimer)
end

function OnDestroy(self)
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.CancelMatchRespID, self.uiName)
    Scheduler.Instance:RemoveTimer(self.actionOnTimer)
end

function Close(self)
    if self.uiAnimator then
        self.uiAnimator:SetTrigger("Close")
    end
end

function MakeOnTimer(self)
    return function ()
        self.waitTime = self.waitTime + 1
        self.waitTimeLabel.text = tostring(self.waitTime)
    end
end

function OnClose(self)
    NGUITools.Destroy(self.gameObject)
end

function MakeOnCancelClick(self)
    return function (go)
        self.colliderCancel.enabled = false
        local req =
        {
            acc_id = MainPlayer.Instance.AccountID,
            type = self.matchType,
        }
        print(self.uiName, "matcType:", self.matchType)
        local buf = protobuf.encode('fogs.proto.msg.CancelMatchReq', req)
        LuaHelper.SendPlatMsgFromLua(MsgID.CancelMatchReqID, buf)
    end
end

function CancelMatchHandler(self)
    return function (message)
        print("1927 - <PvpTipNew> CancelMatchHandler called")
        local resp, err = protobuf.decode('fogs.proto.msg.CancelMatchResp', message)
        if resp == nil then
            print(self.uiName, 'error -- CancelMatchResp error: ', err)
            return
        end

        if resp.result ~= 0 and ErrorID.IntToEnum(resp.result) ~= ErrorID.NOT_IN_MATCH_QUEUE  then
            print(self.uiName, 'error --  CancelMatchResp return failed: ', resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            return
        end

        if self.onCancelMatchHandler then
            self.onCancelMatchHandler(message)
        end

        if self.onCancel then
            self.onCancel(false)
        end
    end
end

function ChangeTipState(self)
    return function (go)
        if self.uiAnimator then
            if self.uiAnimator:GetBool("New Bool") then
                if self.onDown then
                    self.onDown()
                end
                self.uiAnimator:SetBool("New Bool", false)
            else
                if self.onTop then
                    self.onTop()
                end
                self.uiAnimator:SetBool("New Bool", true)
            end
        end
    end
end


function SetCancelText(self, text)
    self.uiCancleText:SetText(text)
end

return PvpTipNew

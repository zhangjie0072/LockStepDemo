------------------------------------------------------------------------
-- class name    : UIGameChat
-- create time   : 2016-05-10 14:14:05
-- author        : Jackwu
-- description   : 比赛时聊天功能（目前用于天梯赛选人界面）
------------------------------------------------------------------------
UIGameChat = {
    uiName = "UIGameChat",

--=============================--
--==     公有变量分隔线      ==--
--=============================--

--=============================--
--==     私有变量分隔线      ==--
--=============================--
    _curtRoomId = nil,--当前聊天的房间ID
    _isSendAble = true,--是否可以发送消息
    _sendTimerCallback,--发送消息频率计时器
    _openHistoryFirstTime = true,--是否是第一次打开聊天记录面板
    _inputCharLimitNum = GameSystem.Instance.CommonConfig:GetUInt("gPVPChatWordLimit"),--输入的限制
    _msgSendCd = GameSystem.Instance.CommonConfig:GetFloat("gPVPChatMaxTalkCD")/1000,--发送信息的Cd
--=============================--
--==        UI变量           ==--
--=============================--

    _btnSend,--发送按钮
    _inputMsg,--消息输入框
    _lblLastMsglabel,--最近消息文本模型
    _panelDefault,--默认面板
    _panelSend,--发送消息面板
    _svConstMsg,--常用信息Sv
    _gridConstMsg, --常用信息grid
    _svHistory,--历史消息信息Sv
    _gridHistory,--历史消信息grid
    _togHistory,--历史消息toggle
    _togConstMsg, --常用信息toggle
    _defalutClickArea, --默认显示点击区域
    _sprMask, --mask
    _tfTipsNode,--Tips冒泡节点

}

function UIGameChat:Awake( ... )
    self:UIParser()
    self:AddEvent()

    NGUITools.SetActive(self._panelDefault.gameObject,false)
    NGUITools.SetActive(self._panelSend.gameObject,false)
    ChatDataCenter.ChatUpdateFunc = self:UpdateLastMsg()
    ChatDataCenter.ChatRoomIdUpdate = self:RoomIdUpdate()
end

function UIGameChat:Start( ... )
    self:InitConstMsgList()
    self._sendTimerCallback = LuaHelper.Action(self:SendTimerComplete())
end

function UIGameChat:Update( ... )

end

function UIGameChat:FixedUpdate( ... )

end

function UIGameChat:OnDestroy( ... )

end

--------------刷新------------------
function UIGameChat:Refresh(subID)

end

--------------解析UI组件------------
function UIGameChat:UIParser( ... )
    local transform = self.transform
    local find = function(struct)
        return transform:FindChild(struct)
    end

    self._panelDefault = find("DefaultMode")
    self._lblLastMsglabel = find("DefaultMode/LastMsgLabel"):GetComponent("UILabel")
    self._panelSend = find("SendMode")
    self._inputMsg = find("SendMode/InputArea/InputAccount"):GetComponent("UIInput")
    self._btnSend = find("SendMode/ButtonSend")
    self._svHistory = find("SendMode/ContentArea/History/Scroll"):GetComponent("UIScrollView")
    self._gridHistory = find("SendMode/ContentArea/History/Scroll/Grid")--GetComponent("UIGrid")
    self._svConstMsg = find("SendMode/ContentArea/ConstMsg/Scroll"):GetComponent("UIScrollView")
    self._gridConstMsg = find("SendMode/ContentArea/ConstMsg/Scroll/Grid"):GetComponent("UIGrid")
    self._togHistory = find("SendMode/ContentArea/History")
    self._togConstMsg = find("SendMode/ContentArea/ConstMsg")
    self._defalutClickArea = find("DefaultMode/ClickArea")
    self._sprMask = find("SendMode/Mask")
    self._tfTipsNode = find("SendMode/TipNode")
    -- print("UIGameChat:通过配置文件读取到的配置数据：".."charlimt:"..self._inputCharLimitNum.."sendCd"..self._msgSendCd)
    self._inputMsg.finallyLimit = self._inputCharLimitNum
end

--------------侦听事件--------------
function UIGameChat:AddEvent( ... )

    addOnClick(self._defalutClickArea.gameObject,self:OnDefaultPanelClickHandler())
    addOnClick(self._btnSend.gameObject,self:OnSendMsgHanlder())
    addOnClick(self._togHistory.gameObject,self:OnOpenHistoryList())
    addOnClick(self._togConstMsg.gameObject,self:OnOpenConstMsgList())
    addOnClick(self._sprMask.gameObject,self:OnBackDefaultClickPanel())
end

--=============================--
--==     公有方法分隔线      ==--
--=============================--


--=============================--
--==     私有方法分隔线      ==--
--=============================--

------------------------------------------------------
-- 方法作用：默认面板点击处理，点击跳转到发送消息面板
------------------------------------------------------
function UIGameChat:OnDefaultPanelClickHandler( ... )
    return function()
        NGUITools.SetActive(self._panelDefault.gameObject,false)
        NGUITools.SetActive(self._panelSend.gameObject,true)
        self:OnOpenConstMsgList()()
        self._togConstMsg:GetComponent("UIToggle").value = true
    end
end

------------------------------------
-- 方法作用：点击发送消息 ,包知的处理有非空处理，发送频率的控制
------------------------------------
function UIGameChat:OnSendMsgHanlder( ... )
    return function()
        if not self._isSendAble then
            CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_DONT_SEND_MSG_FAST"),self._tfTipsNode.transform,nil,nil,nil,nil)
            return
        end
        local message = self._inputMsg.value
        if not message or string.gsub(message, "^%s*(.-)%s*$", "%1") == "" then
            CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_HAS_NO_CONTENT"),self._tfTipsNode.transform,nil,nil,nil,nil)
            return
        end
        -- print("你要准备发送的内容是："..message)
        self._isSendAble = false
        Scheduler.Instance:AddTimer(self._msgSendCd, false, self._sendTimerCallback)
        self._inputMsg.value = ""

        if QualifyingNewerAI.isAI then
            QualifyingNewerAI.ReceiveChatMsg(message)
        else
            ChatDataCenter.SendChatMessage(self._curtRoomId,message)
        end

    end
end

-------------------------------------
-- 方法作用：打开查看历史记录信息
-------------------------------------
function UIGameChat:OnOpenHistoryList( ... )
    return function()
        NGUITools.SetActive(self._svConstMsg.gameObject,false)
        NGUITools.SetActive(self._svHistory.gameObject,true)
        if self._openHistoryFirstTime then
            self._openHistoryFirstTime = false
            self:SortAllMessage()
            ChatDataCenter.PrintAllChatMessage(self._curtRoomId)
            local chatContents = ChatDataCenter.GetLatestChatMsgData(self._curtRoomId)
            if chatContents == nil then
                -- print("聊天记录为空")
                return
            end
            for _,v in pairs(chatContents) do
                -- self:UpdateLastMsg()(self._curtRoomId,v.ogri_name..":"..v.content)
                local msgItem = getLuaComponent(createUI("ChatMsgItem2",self._gridHistory.transform))
                msgItem:SetContent(v.ogri_name..":"..v.content)
            end
        end
        self:SortAllMessage()
        self._svHistory:ResetPosition()
    end
end

------------------------------------
-- 方法作用：打开查看常用信息操作面板
------------------------------------
function UIGameChat:OnOpenConstMsgList( ... )
    return function()
        NGUITools.SetActive(self._svConstMsg.gameObject,true)
        NGUITools.SetActive(self._svHistory.gameObject,false)
    end
end

------------------------------------
-- 方法作用：更新最近一条消息
------------------------------------
function UIGameChat:UpdateLastMsg()
    return function(roomid,msg)
        -- print("UIGameChat..更新聊天数据".."RoomID:"..roomid.."..msg:"..msg)
        --还末打开过聊天界面
        self._lblLastMsglabel.text = msg
        if self._openHistoryFirstTime then
            return
        end
        if self._curtRoomId == roomid then
            local chatStr = msg
            local msgItem = getLuaComponent(createUI("ChatMsgItem2",self._gridHistory.transform))
            msgItem:SetContent(msg)
            self:SortAllMessage()
            self._svHistory:ResetPosition()
        end
    end
end

------------------------------------
-- 方法作用：聊天历史记录手动排列
------------------------------------
function UIGameChat:SortAllMessage( ... )
    local totalHeight = 0
    local parent = self._gridHistory.transform
    local minNum = 1
    for i = parent.childCount - 1, minNum - 1 , -1 do
        local child = parent:GetChild(i).gameObject
        local childLua = getLuaComponent(child)
        local pos = child.transform.localPosition
        totalHeight = totalHeight + childLua:GetHeight()
        pos.y = totalHeight
        child.transform.localPosition = pos
    end
end

------------------------------------
-- 方法作用：初始化常用信息列表
------------------------------------
function UIGameChat:InitConstMsgList( ... )
    local constMsgData = ChatConstMsgConfig.GetAllConstMsg()
    if constMsgData == nil then
        -- print("常用信息配置是空的...")
        return
    end
    for _,v in ipairs(constMsgData) do
        local contentStr = v['info']
        local msgItem = getLuaComponent(createUI("ChatMsgItem",self._gridConstMsg.transform))
        msgItem:SetContent(contentStr)
        msgItem.ClickCallBackFunc = self:OnSelectItemHanlder()
    end
    self._gridConstMsg:Reposition()
    self._svConstMsg:ResetPosition()
end

------------------------------------
-- 方法作用：选择常用信息处理
------------------------------------
function UIGameChat:OnSelectItemHanlder( ... )
    return function(msg)
        if QualifyingNewerAI.isAI then
            QualifyingNewerAI.ReceiveChatMsg(msg)
        else
            ChatDataCenter.SendChatMessage(self._curtRoomId,msg)
        end
        self:OnBackDefaultClickPanel()()
    end
end

------------------------------------
-- 方法作用：点击mask区域消息框消息
------------------------------------
function UIGameChat:OnBackDefaultClickPanel( ... )
    return function(msg)
        print("doing!")
        NGUITools.SetActive(self._panelDefault.gameObject,true)
        NGUITools.SetActive(self._panelSend.gameObject,false)
        local chatContents = ChatDataCenter.GetLatestChatMsgData(ChatDataCenter.TestRoomId)
        if chatContents ~= nil then
            local lastMsgStr = chatContents[#chatContents]
            if lastMsgStr then
                self._lblLastMsglabel.text = lastMsgStr.ogri_name..":"..lastMsgStr.content
            end
        end
    end
end

------------------------------------
-- 方法作用：发送消频率倒计时
------------------------------------
function UIGameChat:SendTimerComplete()
    return function()
        self._isSendAble = true
    end
end

------------------------------------
-- 方法作用：打开聊天功能
------------------------------------
function UIGameChat:OpenChatModule(roomid)
    -- print("UIGameChat 打开..roomID:"..roomid)
    CommonFunction.ClearGridChild(self._gridHistory.transform)
    self._lblLastMsglabel.text = ""
    self._svHistory:ResetPosition()
    self._curtRoomId = roomid
    NGUITools.SetActive(self._panelDefault.gameObject,true)
    NGUITools.SetActive(self._panelSend.gameObject,false)
end

------------------------------------
-- 方法作用：送闭聊天模块
------------------------------------
function UIGameChat:CloseChatModue()
    NGUITools.SetActive(self._panelDefault.gameObject,false)
    NGUITools.SetActive(self._panelSend.gameObject,false)
    CommonFunction.ClearGridChild(self._gridHistory.transform)
    self._lblLastMsglabel.text = ""
    self._inputMsg.value = ""
end

------------------------------------
-- 方法作用：更新房间id信息
------------------------------------
function UIGameChat:RoomIdUpdate()
    return function(roomid)
        self:OpenChatModule(roomid)
    end
end
return UIGameChat

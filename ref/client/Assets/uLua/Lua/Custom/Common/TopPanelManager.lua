TopPanelManager = {
    panelList = List:New(),
    hidenList = List:New()
}

PanelInfo = {
    name = "",
    luaCom = nil,
    subID = nil,
    paramsRecord = nil,
}

function PanelInfo.New(panelName, luaComponent)
    local info = {
        name = panelName,
        luaCom = luaComponent,
    }
    setmetatable(info, PanelInfo)
    return info
end

function PanelInfo.__eq(a, b)
    return a.name == b.name
end

function TopPanelManager:ShowPanel(panelName, subID, params)
    -- self.hidenList:PrintList("hidenList")
    print("TopPanelManager panelName:",panelName)
    if not validateFunc(panelName or '') then
        return
    end

    print("TopPanelManager: loadedLevelName:", Application.loadedLevelName, panelName)
    if (Application.loadedLevelName == 'Match') ~= checkMatchUI(panelName) and not checkCommonUI(panelName) then
        print("TopPanelManager: jumpToUI")
        jumpToUI(panelName, subID, params)
        return
    end
    if not self then error("TopPanelManager: Call TopPanelManager with ':'.") end
    --当前上层界面
    local curTop = self.panelList:Back()

    local isUIHall = false
    if curTop then
        isUIHall =  (curTop.name == "UIHall")
    end

    print('-----------------------------ssssssssss------------------------')

    --检查是否已经创建
    local panelInfo
    local itr = self.panelList:FindLast(PanelInfo.New(panelName))	--显示列表中
    if not itr then	--没有，再看隐藏列表
        itr = self.hidenList:FindLast(PanelInfo.New(panelName))
    end
    if not itr then	--未创建，先创建
        print('TopPanelManager not created, now create')
        local panel_go = createUI(panelName)
        --local black_boarder = createUI("BlackBoarder",panel_go.transform)
        local luaCom = getLuaComponent(panel_go)
        if not luaCom then
            error("TopPanelManager: Can not create " .. panelName .. ", it does not exist, or no lua component")
        end
        panelInfo = PanelInfo.New(panelName, luaCom)
        if panelInfo.luaCom.OnShow and not panelInfo.luaCom:OnShow() then
            NGUITools.SetActive(panelInfo.luaCom.gameObject, false)	--不显示
            self.panelList:PushFront(panelInfo)	--放到最下层

            if isUIHall and ( panelInfo.name == "UITaskLevel") then
                print('conglin--a1')
                panelInfo.luaCom:ResetLevel()
            end
            return panelInfo.luaCom
        end
    else	--已创建则取出来
            --luaCom的信息判断否有
        if itr:Value().luaCom == nil then
            print("TopPanelManager, UI has been destroyed, recreate.")
            local newLuaCom = getLuaComponent(createUI(panelName))
            itr:Value().luaCom = newLuaCom
        end
        print('TopPanelManager created already')
        panelInfo = itr:Value()
        if panelInfo.luaCom.OnShow and not panelInfo.luaCom:OnShow() then return panelInfo.luaCom end
        self.panelList:Erase(itr)
    end
    if isUIHall and ( panelInfo.name == "UITaskLevel") then
         print('conglin--a2')
        panelInfo.luaCom:ResetLevel()
    end
    self.panelList:PushBack(panelInfo)	--放到最上层
    NGUITools.SetActive(panelInfo.luaCom.gameObject, true)	--显示
    if curTop ~= panelInfo and UseAnimatorShade[panelInfo.luaCom.uiName] then
        CommonFunction.SetAnimatorShade(true)
    end
    UIManager.Instance:BringPanelForward(panelInfo.luaCom.gameObject)	--顶层显示
    --传入参数
    panelInfo.luaCom.subID = subID
    panelInfo.subID = subID
    panelInfo.paramsRecord = params
    if params then
        for k, v in pairs(params) do
            panelInfo.luaCom[k] = v
        end
    end
    local onStarted = function ()
        if panelInfo.luaCom.Refresh then --调用Refresh（如果有）
            print("TopPanelManager, Call Refresh,", "Panel:", panelName, "sub ID:", subID)
            panelInfo.luaCom:Refresh(subID)
        end
        -- if not params or not params.reStart then
        --     GuideSystem.Instance:ReqBeginGuide(panelName)
        -- end
        TopPanelManager.ValidateAllNextFrame(panelName)
    end

    if panelInfo.luaCom.started then
        onStarted()
    else
        panelInfo.luaCom.onStarted = onStarted
    end

    --隐藏先前的上层界面
    if curTop and curTop.luaCom and IsNil(curTop.luaCom.gameObject) == false and curTop.name ~= panelName then
        NGUITools.SetActive(curTop.luaCom.gameObject, false)
    end
    self.panelList:PrintList("panelList")
    return panelInfo.luaCom
end

function TopPanelManager:HideTopPanel()

    -- self.hidenList:PrintList("hidenList")

    print ('@@HideTopPanel by conglin')

    if not self then error("TopPanelManager: Call TopPanelManager with ':'.") end
    --隐藏当前上层界面，并移到隐藏列表
    local topPanelInfo = self.panelList:PopBack()
    local prevPanelInfo = self.panelList:Back()
    if topPanelInfo then
        NGUITools.SetActive(topPanelInfo.luaCom.gameObject, false)
        if self.hidenList:FindLast(topPanelInfo) == nil then
            self.hidenList:PushFront(topPanelInfo)
        end
    else
        error("TopPanelManager: No top panel.")
    end
    self.panelList:PrintList("panelList")
    --显示当前顶层界面
    if prevPanelInfo then
        if prevPanelInfo.luaCom == nil then
            if (Application.loadedLevelName == 'Match') ~= checkMatchUI(prevPanelInfo.name) and not checkCommonUI(prevPanelInfo.name) then
                jumpToUI(prevPanelInfo.name)
                print("TopPanelManager hide jumpToUI:", prevPanelInfo.name)
                return
            else
                local newLuaCom = getLuaComponent(createUI(prevPanelInfo.name))
                prevPanelInfo.luaCom = newLuaCom
            end
        end
        -- --设置参数
        prevPanelInfo.luaCom.subID = prevPanelInfo.subID
        --TODO 重新传入参数会出问题，是否有必要？

        -- if prevPanelInfo.paramsRecord then
        --  for k, v in pairs(prevPanelInfo.paramsRecord) do
        --      prevPanelInfo.luaCom[k] = v
        --  end
        -- end

        NGUITools.SetActive(prevPanelInfo.luaCom.gameObject, true)

        if UseAnimatorShade[prevPanelInfo.luaCom.uiName] then
            CommonFunction.SetAnimatorShade(true)
        end

        if prevPanelInfo.luaCom.started and prevPanelInfo.luaCom.Refresh then	--调用Refresh（如果有）
            prevPanelInfo.luaCom:Refresh()
            GuideSystem.Instance:ReqBeginGuide(prevPanelInfo.name)
            TopPanelManager.ValidateAllNextFrame(prevPanelInfo.name)
        else
            --
            local onStarted = function ()
                if prevPanelInfo.luaCom.Refresh then --调用Refresh（如果有）
                    prevPanelInfo.luaCom:Refresh(prevPanelInfo.subID)
                end
                GuideSystem.Instance:ReqBeginGuide(prevPanelInfo.name)
                TopPanelManager.ValidateAllNextFrame(prevPanelInfo.name)
            end

            if prevPanelInfo.luaCom.started then
                onStarted()
            else
                prevPanelInfo.luaCom.onStarted = onStarted
            end
        end

    else
        jumpToUI("UIHall")
    end
end

function TopPanelManager.ValidateAllNextFrame(uiName)
    Scheduler.Instance:AddFrame(1, false, TopPanelManager.MakeValidateAll(uiName))
end

function TopPanelManager.MakeValidateAll(uiName)
    local validateAll = function ()
        ConditionallyActive.ValidateAll(uiName)
    end
    return LuaHelper.Action(validateAll)
end

function TopPanelManager:Remove(name)
    local itr = self.panelList:FindLast(PanelInfo.New(name))
    if itr then
    self.panelList:Erase(itr)
    else
    error("cannot remove"..tostring(name))
    end

end

function TopPanelManager:Clear()
    self.panelList:Clear()
    self.hidenList:Clear()
end

function TopPanelManager:SetLuaComNil(panelName)
    local itr = self.panelList:FindLast(PanelInfo.New(panelName))	--显示列表中
    if not itr then	--没有，再看隐藏列表
        itr = self.hidenList:FindLast(PanelInfo.New(panelName))
    end
    if itr then
        itr:Value().luaCom = nil
        print("TopPanelManager set luacom nil:",panelName)
    end
end

function TopPanelManager:PanelListPopBack()
    self.panelList:PopBack()
end

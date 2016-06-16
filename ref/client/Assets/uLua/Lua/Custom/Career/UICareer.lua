--encoding=utf-8

require "common/StringUtil"

UICareer = {
    uiName = 'UICareer',

    -----------------parameters
    chapterID = 0,
    sectionID,
    move = false,
    boxcollider = {},
    mapTable = {},
    reStart = false,

    nextShowUI,
    nextShowUISubID,
    nextShowUIParams,
    banTwice = false,
    onClose,
    jumpFromRole = false,

    --------------------UI
    uiAwardArea,
    uiTopNode,
    uiStarProgress,
    uibtnBag1,
    uiBag1Name,
    uibtnBag2,
    uiBag2Name,
    uibtnBag3,
    uiBag3Name,
    uiNameGet,
    uiStarNum1,
    uiStarNum2,
    uiStarNum3,
    uiChapterNO,
    uiChapterTitle,
    uibtnPrev,
    uibtnNext,
    uiAnimator,
}


-----------------------------------------------------------------
function UICareer:Awake()
    local tm = self.transform:FindChild("Bottom")
    self.uiBtnMenu = createUI("ButtonMenu",self.transform:FindChild("Top/ButtonMenu"))
    self.uiTopNode = self.transform:FindChild("Top")
    self.uiAwardArea = tm.transform:FindChild("AwardBoxArea")
    self.uiStarProgress= tm:FindChild("AwardBoxArea/Prog1"):GetComponent("UIProgressBar")
    self.uibtnBag1 = getChildGameObject(tm, "AwardBoxArea/Bag1")
    self.uiBag1Name=self.uibtnBag1.transform:GetComponent("UISprite")
    self.uibtnBag2 = getChildGameObject(tm, "AwardBoxArea/Bag2")
    self.uiBag2Name=self.uibtnBag2.transform:GetComponent("UISprite")
    self.uibtnBag3 = getChildGameObject(tm, "AwardBoxArea/Bag3")
    self.uiBag3Name=self.uibtnBag3.transform:GetComponent("UISprite")
    self.uiNameGet = tm:FindChild("AwardBoxArea/Container/NumGet"):GetComponent("UILabel")
    self.uiStarNum1 = tm:FindChild("AwardBoxArea/Num1"):GetComponent("UILabel")
    self.uiStarNum2 = tm:FindChild("AwardBoxArea/Num2"):GetComponent("UILabel")
    self.uiStarNum3 = tm:FindChild("AwardBoxArea/Num3"):GetComponent("UILabel")
    self.uiChapterNO = getComponentInChild(tm, "Chapter/NO", "MultiLabel")
    print("self.uiChapterNO:",self.uiChapterNO)
    self.uiChapterTitle = getComponentInChild(tm, "Chapter/Title", "MultiLabel")
    self.uibtnPrev = self.transform:FindChild("Prev").gameObject
    self.uibtnNext = self.transform:FindChild("Next").gameObject
    --btn
    -- self.uibgArrow = self.transform:FindChild("bg/bg/arrow"):GetComponent("UISprite")
    -- self.uicareerBtnsMember = self.transform:FindChild("CareerBottomBtns/grid/member"):GetComponent("UIButton")
    -- self.uicareerBtnsPackage = self.transform:FindChild("CareerBottomBtns/grid/package"):GetComponent("UIButton")
    -- self.uicareerBtnsTask = self.transform:FindChild("CareerBottomBtns/grid/task"):GetComponent("UIButton")
    -- self.uicareerBtnsClub = self.transform:FindChild("CareerBottomBtns/grid/club"):GetComponent("UIButton")
    -- self.bg_bg = self.transform:FindChild("bg/bg"):GetComponent("UIButton")
    addOnClick(self.uibtnPrev, self:MakeOnPrev())
    addOnClick(self.uibtnNext, self:MakeOnNext())
    addOnClick(self.uibtnBag1, self:MakeOnBag1())
    addOnClick(self.uibtnBag2, self:MakeOnBag2())
    addOnClick(self.uibtnBag3, self:MakeOnBag3())
    -- addOnClick(self.uicareerBtnsMember.gameObject,self:click_member())
    -- addOnClick(self.uicareerBtnsPackage.gameObject,self:click_package())
    -- addOnClick(self.uicareerBtnsTask.gameObject,self:click_task())
    -- addOnClick(self.uicareerBtnsClub.gameObject,self:click_club())
    -- addOnClick(self.bg_bg.gameObject, self:click_left_show())

    self.uiAnimator = self.transform:GetComponent('Animator')
    print("UICareer animator:",self.uiAnimator)
end

function UICareer:Start()
    local btnMenu = getLuaComponent(self.uiBtnMenu)
    btnMenu:SetParent(self.gameObject,false)
    btnMenu.parentScript = self
    self.mapTable = {}
    self.btnBack = getLuaComponent(createUI("ButtonBack",self.uiTopNode:FindChild("ButtonBack")))
    self.btnBack.onClick = self:MakeOnBack()
    local tween = self.transform:FindChild("MapGrid/Map")
    self.uiMap = createUI("Map",tween)
    self.uiMap1 = createUI("Map",tween)
    local pos1 = self.uiMap.transform.localPosition
    pos1.x = pos1.x -1280
    self.uiMap1.transform.localPosition = pos1
    self.uiMap2 = createUI("Map",tween)
    local pos2 = self.uiMap.transform.localPosition
    pos2.x = pos2.x +1280
    self.uiMap2.transform.localPosition = pos2
end

function UICareer:FixedUpdate( ... )
    -- body
end

function UICareer:OnClose( ... )
    local btnMenu = getLuaComponent(self.uiBtnMenu)
    btnMenu:SetParent(self.gameObject,true)
    btnMenu.parentScript = self

    if self.onClose then
        self.onClose()
        self.onClose = nil
        return
    end

    if MainPlayer.Instance.LinkEnable then
        TopPanelManager:HideTopPanel()
        return
    end

    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        TopPanelManager:ShowPanel("UIHall")
    end
end

function UICareer:DoClose()
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
end

function UICareer:OnDestroy( ... )
    -- body
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UICareer:Refresh(subID)
    local btnMenu = getLuaComponent(self.uiBtnMenu)
    btnMenu:Refresh()
    -- set to player's current chapter or specified chapter by subID
    if subID and subID > 10000 and self.jumpFromRole ~= true then
        self.chapterID = subID
    end
    --jump from role
    print(self.uiName,"---chapterID:",self.chapterID)
    print(self.uiName,"---jumpFromRole:",self.jumpFromRole,"---subID:",subID)
    -- if self.jumpFromRole and subID and subID ~= self.chapterID then
    if subID and subID ~= self.chapterID then
        local mapGrid = self.transform:FindChild("MapGrid/Map").transform
        print(self.uiName,"----count:",mapGrid.childCount)
        while mapGrid.childCount > 0 do
            local go = mapGrid:GetChild(0).gameObject
            NGUITools.Destroy(go)
        end
        self.jumpFromRole = false
        self.chapterID = subID
        print(self.uiName,"-----:",self.chapterID)
        self:InitMap()
    end

    if self.chapterID == 0 then
        local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
        while enum:MoveNext() do
            local configID = enum.Current.Key
            if MainPlayer.Instance:CheckChapter(configID) then
                self.chapterID = configID
            else
                break
            end
        end
    end

    self.chapterConfig = GameSystem.Instance.CareerConfigData:GetChapterData(self.chapterID)

    -- fetch data and config
    local chapter = MainPlayer.Instance:GetChapter(self.chapterID)
    print("UICareer----log:",self.chapterID)
    if not chapter then
        -- CommonFunction.ShowPopupMsg(getCommonStr("CAREER_CHAPTER_LOCK"),nil,nil,nil,nil,nil)
        -- 章节没有开启就关闭星级奖励ui
        self.uiAwardArea.gameObject:SetActive(false)
    else
        self.uiAwardArea.gameObject:SetActive(true)

        if(chapter.is_bronze_awarded == true) then
            self.uiBag1Name.spriteName = "career_bag_e"
            --print('22222------------',self.uiBag1Name)
        else
            self.uiBag1Name.spriteName = "career_bag3"
        end
        if(chapter.is_silver_awarded == true) then
            self.uiBag2Name.spriteName = "career_bag_e"
        else
            self.uiBag2Name.spriteName = "career_bag2"
        end
        if(chapter.is_gold_awarded == true) then
            self.uiBag3Name.spriteName = "career_bag_e"
        else
            self.uiBag3Name.spriteName = "career_bag1"
        end

        self.chapter = chapter
        self:RefreshAward()
    end

    --prev button and next button
    local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    enum:MoveNext()
    local firstChapterID = enum.Current.Key
    -- print ("first chapter id:", firstChapterID)
    NGUITools.SetActive(self.uibtnPrev, self.chapterID ~= firstChapterID)
    local nextEnabled = self.chapterConfig.next_chapter_id ~= 0
    NGUITools.SetActive(self.uibtnNext, nextEnabled)
    --title
    local chapterNO = 0
    enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    while enum:MoveNext() do
        chapterNO = chapterNO + 1
        if enum.Current.Key == self.chapterID then break end
    end
    local nums = getCommonStr("CHINESE_NUM")
    --utf-8 encoding, a chinese character has 3 bytes
    if chapterNO > 10 then
        local n1 = math.modf( chapterNO / 10)
        local n2 = chapterNO % 10
     self.uiChapterNO:SetText( getCommonStr("CAREER_CHAPTER"):format(nums:sub(30 * n1 + 1 ,30 * n1 + 3)..nums:sub(n2 * 3 + 1, n2 * 3 + 3)))
    else
    self.uiChapterNO:SetText( getCommonStr("CAREER_CHAPTER"):format(nums:sub(chapterNO * 3 + 1, chapterNO * 3 + 3)))
    end
    self.uiChapterTitle:SetText( self.chapterConfig.name)

    --section items
    self:RefreshSections()
end


-----------------------------------------------------------------
function UICareer:RefreshAward()
    --------num
    local num1 = self.chapterConfig.bronze_value
    local num2 = self.chapterConfig.silver_value
    local num3 = self.chapterConfig.gold_value
    local getNum = self.chapter.star_num
    self.uiNameGet.text = getNum
    print("num1:",num1,"num2:",num2,"num3:",num3,"getNum:",getNum)
    local bronze_value = math.max(math.min(self.chapter.star_num / num1, 1),0) /3
    local silver_value = math.max(math.min((self.chapter.star_num - num1)
                                    / (num2 - num1), 1), 0) / 3
    local gold_value = math.max(math.min((self.chapter.star_num - num2)
                                    / (num3 - num2), 1), 0) / 3
    self.uiStarProgress.value =bronze_value + silver_value + gold_value
    print("bronze_value:",bronze_value,"silver_value:",silver_value,"gold_value:",gold_value)
    print("value:",self.uiStarProgress.value)
    self.uiStarNum1.text = num1
    self.uiStarNum2.text = num2
    self.uiStarNum3.text = num3
    ----------bag tween
    local chapter = MainPlayer.Instance:GetChapter(self.chapterID)
    if getNum >= num1 and chapter.is_bronze_awarded == false then
        self.uibtnBag1.transform:GetComponent("Animator").enabled = true
    else
        self.uibtnBag1.transform:GetComponent("Animator").enabled = false
        local rote = self.uibtnBag1.transform.localRotation
        rote.x = 0
        rote.y = 0
        rote.z = 0
        self.uibtnBag1.transform.localRotation = rote
    end
    if getNum >= num2 and chapter.is_silver_awarded == false then
        self.uibtnBag2.transform:GetComponent("Animator").enabled = true
    else
        self.uibtnBag2.transform:GetComponent("Animator").enabled = false
        local rote = self.uibtnBag2.transform.localRotation
        rote.x = 0
        rote.y = 0
        rote.z = 0
        self.uibtnBag2.transform.localRotation = rote
    end
    if getNum >= num3 and chapter.is_gold_awarded == false then
        self.uibtnBag3.transform:GetComponent("Animator").enabled = true
    else
        self.uibtnBag3.transform:GetComponent("Animator").enabled = false
        local rote = self.uibtnBag3.transform.localRotation
        rote.x = 0
        rote.y = 0
        rote.z = 0
        self.uibtnBag3.transform.localRotation = rote
    end
end

function UICareer:RefreshSections()
    if self.move == true then
        return
    end
    --创建当前章节
    self:Create_Chapter( self.chapterID ,self.uiMap)
    --创建上一章节item
    local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    while enum:MoveNext() do
        if enum.Current.Value.next_chapter_id == self.chapterID then
            self.prechapterID = enum.Current.Key
            break
        end
    end

    if self.prechapterID then
        self:Create_Chapter( self.prechapterID, self.uiMap1)
    end
    --创建下一章节的item

    self.nextchapterID = self.chapterConfig.next_chapter_id
    if self.nextchapterID ~= 0 then
        self:Create_Chapter( self.nextchapterID ,self.uiMap2)
    end

    if self.reStart == true then
        self:MakeOnRoleSelected()
        self.reStart = false
    end

    --实现关卡跳转
    if self.sectionID and self.sectionID ~= 0 then
        for i = 0, self.mapTable[self.chapterID].transform.childCount-1 do
            local go = self.mapTable[self.chapterID].transform:GetChild(i).gameObject
            if string.find(go.name, 'CareerSectionItem') ~= nil then
                local item = getLuaComponent(go)
                if item and item.sectionID == tonumber(self.sectionID) then
                    self:MakeOnSectionClick()(item)

                    break
                end
            end
        end
        self.sectionID = nil
    end
end

function UICareer:MakeOnSectionClick()
    return function (item)
        if not FunctionSwitchData.CheckSwith(FSID.career_matching) then return end

        if self.banTwice then
            return
        end
        if MainPlayer.Instance:CheckSection(self.chapterID, item.sectionID) then
            --TopPanelManager:ShowPanel("UICareerSection", nil, {chapterID = self.chapterID, sectionID = item.sectionID})
            --判断关卡类型
            local hideStar = false
            local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(item.sectionID)
            if sectionConfig.type == 2 then
                --judge complete
                if MainPlayer.Instance:CheckSectionComplete(self.chapterID, item.sectionID) then
                    return
                end
                hideStar = true
            end
            local flag = item.sectionID % (10000 + (tonumber(self.chapterID) % 10000) * 100)
            local UICareerSection = createUI('UICareerSection',self.transform)
            local obj = getLuaComponent(UICareerSection)
            if sectionConfig.type == 1 then
                obj.is_boss = true
                obj.showRemain = true
            end
            obj.btnMenu = self.uiBtnMenu.gameObject
            obj.chapterID = self.chapterID
            obj.sectionID = item.sectionID
            obj.hideStar = hideStar
            obj.onClose = function ( ... )
                self.banTwice = false
            end
            --print('-------chapterID:',self.chapterID,'-----------sectionID:',item.sectionID)
            UIManager.Instance:BringPanelForward(UICareerSection.gameObject)
            self.banTwice = true
        else
            --CommonFunction.ShowPopupMsg(getCommonStr("CAREER_SECTION_LOCK"),nil,nil,nil,nil,nil)
        end
    end
end

function UICareer:MakeOnPrev()
    return function ()

        local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
        while enum:MoveNext() do
            if enum.Current.Value.next_chapter_id == self.chapterID then
                self.chapterID = enum.Current.Key
                break
            end
        end
        self.move = true
        self.uibtnNext.transform:GetComponent("UIButton").isEnabled = false
        self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = false
        self:Refresh()

        self:MovePrev()
    end
end

function UICareer:MovePrev()
    local obj = self.transform:FindChild("MapGrid/Map").gameObject
    self:SetBoxcollider( false)
    self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x + 1280, -233.5, 0))
    self.TweenPosition.method = UITweener.Method.EaseInOut
    self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:movePrv_finish()))
end

function UICareer:movePrv_finish()
    return function ()
        self.uibtnNext.transform:GetComponent("UIButton").isEnabled = true
        self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = true
        self.uiMap2 = self.uiMap
        self.uiMap = self.uiMap1
        local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
        while enum:MoveNext() do
            if enum.Current.Value.next_chapter_id == self.chapterID then
                self.prechapterID = enum.Current.Key
                break
            end
        end

        self.tween_state = false
        --开启boxcollider
        self:SetBoxcollider(true)

        self.uiMap1 = self.mapTable[self.prechapterID]

        if not self.prechapterID then

            return
        end

        if	not self.uiMap1 then
            local tween = self.transform:FindChild("MapGrid/Map")
            self.uiMap1 = createUI("Map",tween)
            local pos1 = self.uiMap.transform.localPosition
            pos1.x = pos1.x -1280
            self.uiMap1.transform.localPosition = pos1
            self:Create_Chapter( self.prechapterID , self.uiMap1)
        end
    end
end

function UICareer:moveNext_finish()
    return function ()
        self.uibtnNext.transform:GetComponent("UIButton").isEnabled = true
        self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = true
        self.uiMap1 = self.uiMap
        self.uiMap = self.uiMap2
        self.nextChapterID = self.chapterConfig.next_chapter_id
        self.uiMap2 = self.mapTable[ self.nextChapterID]
        if not self.uiMap2 and self.nextChapterID ~= 0 then
            local tween = self.transform:FindChild("MapGrid/Map")
            self.uiMap2 = createUI("Map",tween)
            local pos1 = self.uiMap.transform.localPosition
            pos1.x = pos1.x + 1280
            self.uiMap2.transform.localPosition = pos1
            self:Create_Chapter( self.nextChapterID , self.uiMap2)
        end
        self.tween_state = false
        --开启boxcollider
        self:SetBoxcollider( true)
    end
end


function UICareer:MakeOnNext()
    return function ()
        local nextChapterID = self.chapterConfig.next_chapter_id
        -- local needLevel = CareerConfig.chapterConfig:get_Item(nextChapterID).unlock_level
        -- if needLevel > MainPlayer.Instance.Level then
        --  CommonFunction.ShowPopupMsg(getCommonStr("CAREER_UNLOCK_NEED_LEVEL"):format(needLevel),nil,nil,nil,nil,nil)
        --  return
        -- elseif not MainPlayer.Instance:CheckChapter(self.chapterConfig.next_chapter_id) then
        --  CommonFunction.ShowPopupMsg(getCommonStr("CAREER_CHAPTER_LOCK"),nil,nil,nil,nil,nil)
        --  return
        if tonumber(nextChapterID) == 0 then
            CommonFunction.ShowPopupMsg("end",nil,nil,nil,nil,nil)
            return
        end

        self.chapterID = nextChapterID
        self.move = true
        self:Refresh()
        self:MoveNext()
    end
end

function UICareer:MoveNext()
    print(self.uiName,"--move next")
    local obj = self.transform:FindChild("MapGrid/Map").gameObject
    self:SetBoxcollider( false)
    self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x - 1280, -233.5, 0))
    self.TweenPosition.method = UITweener.Method.EaseInOut
    self.uibtnNext.transform:GetComponent("UIButton").isEnabled = false
    self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = false
    self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:moveNext_finish()))
end

function UICareer:MakeOnBack()
    return function ()
        self:DoClose()
    end
end

function UICareer:MakeOnBag1()
    return function ()
        if not FunctionSwitchData.CheckSwith(FSID.career_box) then return end

        if self.banTwice then
            return
        end
        local chapter = MainPlayer.Instance:GetChapter(self.chapterID)
        if chapter.is_bronze_awarded == true then
            CommonFunction.ShowPopupMsg(getCommonStr("CAREER_STAR_AWARD_ALREADY_GET"),nil,nil,nil,nil,nil)
        else
            local awardDetail = getLuaComponent(createUI("CareerAwardDetailPopup"), self.transform)
            awardDetail.chapterID = self.chapterID
            awardDetail.awardType = awardDetail.AwardType.BRONZE
            awardDetail.Click = self:ui_Refresh()
            awardDetail.onClose = function ( ... )
                self.banTwice = false
            end
            UIManager.Instance:BringPanelForward(awardDetail.gameObject)
            self.banTwice = true

            UpdateRedDotHandler.MessageHandler("Career")
        end
    end
end

function UICareer:ui_Refresh()
    return function()
        self.banTwice = false
        self:Refresh()
    end
end

function UICareer:MakeOnBag2()
    return function ()
        if not FunctionSwitchData.CheckSwith(FSID.career_box) then return end

        if self.banTwice then
            return
        end
        local chapter = MainPlayer.Instance:GetChapter(self.chapterID)
        if chapter.is_silver_awarded == true then
            CommonFunction.ShowPopupMsg(getCommonStr("CAREER_STAR_AWARD_ALREADY_GET"),nil,nil,nil,nil,nil)
        else
            local awardDetail = getLuaComponent(createUI("CareerAwardDetailPopup"), self.transform)
            awardDetail.chapterID = self.chapterID
            awardDetail.awardType = awardDetail.AwardType.SILVER
            awardDetail.Click = self:ui_Refresh()
            awardDetail.onClose = function ( ... )
                self.banTwice = false
            end
            UIManager.Instance:BringPanelForward(awardDetail.gameObject)
            self.banTwice = true

            UpdateRedDotHandler.MessageHandler("Career")
        end
    end
end

function UICareer:MakeOnBag3()
    return function ()
        if not FunctionSwitchData.CheckSwith(FSID.career_box) then return end

        if self.banTwice then
            return
        end
        local chapter = MainPlayer.Instance:GetChapter(self.chapterID)
        if chapter.is_gold_awarded == true then
            CommonFunction.ShowPopupMsg(getCommonStr("CAREER_STAR_AWARD_ALREADY_GET"),nil,nil,nil,nil,nil)
        else
            local awardDetail = getLuaComponent(createUI("CareerAwardDetailPopup"), self.transform)
            awardDetail.chapterID = self.chapterID
            awardDetail.awardType = awardDetail.AwardType.GOLDEN
            awardDetail.Click = self:ui_Refresh()
            awardDetail.onClose = function ( ... )
                self.banTwice = false
            end
            UIManager.Instance:BringPanelForward(awardDetail.gameObject)
            self.banTwice = true

            UpdateRedDotHandler.MessageHandler("Career")
        end
    end
end

function UICareer:SetModelActive(active)
    -- can't delete
end
-- function UICareer:click_member()
--	return function()
--		TopPanelManager:ShowPanel('UIRole')
--	end
-- end

-- function UICareer:click_package()
--	return function()
--		TopPanelManager:ShowPanel("UIPackage")
--	end
-- end

-- function UICareer:click_task()
--	return function()
--		local hall = getLuaComponent(self.transform)
--		local req = {
--			acc_id = MainPlayer.instance.AccountID,
--			type = 2,
--		}
--		local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
--		LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

--		TaskRespHandler.parent = self
--		TaskRespHandler.hideModel = false
--	end
-- end

-- function UICareer:click_club()
--	return function()
--		local hall = getLuaComponent(self.transform)
--       local req = {
--           acc_id = MainPlayer.instance.AccountID,
--           type = 3,
--       }
--       local msg = protobuf.encode("fogs.proto.msg.TaskInfoReq", req)
--       LuaHelper.SendPlatMsgFromLua(MsgID.TaskInfoReqID, msg)

--       TaskRespHandler.parent = self
--       TaskRespHandler.hideModel = false
--	end
-- end

-- function UICareer:click_left_show()
--	return function()
--		self:action_left_show(not self.left_show)
--	end
-- end

-- function UICareer:action_left_show(show)
--	self.left_show = show
--	NGUITools.SetActive(self.uicareerBtnsMember.gameObject,self.left_show)
--	NGUITools.SetActive(self.uicareerBtnsPackage.gameObject,self.left_show)
--	NGUITools.SetActive(self.uicareerBtnsTask.gameObject,self.left_show)
--	NGUITools.SetActive(self.uicareerBtnsClub.gameObject,self.left_show)
--	if not show then
--		self.uibgArrow.flip = UIBasicSprite.Flip.Nothing
--	else
--		self.uibgArrow.flip = UIBasicSprite.Flip.Vertically
--	end
-- end

function UICareer:MoveOnPress(  )
    return function ( obj,isPress)
        if self.tween_state == true then
            return
        end

        if not GuideSystem.Instance.guideHiding then
            return
        end

        if isPress then
            --点击图片的位置随delta移动
            self.getx = 0
            self:MoveDrag()
        else
            --禁用boxcollider
            self:SetBoxcollider( false)
            local obj = self.transform:FindChild("MapGrid/Map").gameObject
            --松开事 判断移动距离 > 1/2时移动 < 移回
            if self.getx > 300 then
                local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
                while enum:MoveNext() do
                    if enum.Current.Value.next_chapter_id == self.chapterID then
                        self.chapterID = enum.Current.Key
                        break
                    end
                end
                self.move = true
                self.uibtnNext.transform:GetComponent("UIButton").isEnabled = false
                self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = false
                self:Refresh()
                self.tween_state = true
                --禁用boxcollider
                self:SetBoxcollider( false)
                self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x + (1280 - self.getx)  , -233.5, 0))
                self.TweenPosition.method = UITweener.Method.EaseInOut
                self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:movePrv_finish()))
            elseif self.getx< -300 then
                local nextChapterID = self.chapterConfig.next_chapter_id
                local needLevel = CareerConfig.chapterConfig:get_Item(nextChapterID).unlock_level
                if needLevel > MainPlayer.Instance.Level then
                    CommonFunction.ShowPopupMsg(getCommonStr("CAREER_UNLOCK_NEED_LEVEL"):format(needLevel),nil,nil,nil,nil,nil)
                    self.tween_state = true
                    self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x - self.getx, -233.5, 0))
                    self.TweenPosition.method = UITweener.Method.EaseInOut
                    self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:move_return()))
                    return
                end
                self.chapterID = self.chapterConfig.next_chapter_id
                self.move = true
                self.uibtnNext.transform:GetComponent("UIButton").isEnabled = false
                self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = false
                self:Refresh()
                self.tween_state = true
                self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x - (1280 + self.getx) , -233.5, 0))
                self.TweenPosition.method = UITweener.Method.EaseInOut
                self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:moveNext_finish()))
            else
                self.uibtnNext.transform:GetComponent("UIButton").isEnabled = false
                self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = false
                self.tween_state = true
                self.TweenPosition = TweenPosition.Begin(obj,0.5,Vector3.New(obj.transform.localPosition.x - self.getx, -233.5, 0))
                self.TweenPosition.method = UITweener.Method.EaseInOut
                self.TweenPosition:SetOnFinished(LuaHelper.Callback(self:move_return()))
            end
        end
    end
end

function UICareer:move_return( )
    return function ( ... )
        self.tween_state = false
        self.uibtnNext.transform:GetComponent("UIButton").isEnabled = true
        self.uibtnPrev.transform:GetComponent("UIButton").isEnabled = true
        --开启boxcollider
        self:SetBoxcollider( true)
    end
end

function UICareer:MoveDrag( flag )
    return function ( go,delta )
        if self.tween_state == true then
            return
        end

        if not GuideSystem.Instance.guideHiding then
            return
        end


        if (self.chapterID == 10001 and delta.x > 0) or (self.chapterID == self:GetEndID() and delta.x < 0) then
            return
        end
        self.getx = self.getx + delta.x
        local obj = self.transform:FindChild("MapGrid/Map").gameObject
        --self.TweenPosition = TweenPosition.Begin(obj,0.1,Vector3.New(obj.transform.localPosition.x + delta.x, -233.5, 0))
        -- self.TweenPosition.method = UITweener.Method.EaseInOut
        local pos = obj.transform.localPosition
        pos.x = pos.x + delta.x
        obj.transform.localPosition = pos
    end
end

function UICareer:GetEndID()
    local endID
    local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    while enum:MoveNext() do
        if enum.Current.Value.next_chapter_id == 0 then
            endID = enum.Current.Key
        end
    end
    return endID
end

function UICareer:SetBoxcollider( flag )
    -- body
    for i,v in ipairs(self.boxcollider[self.chapterID]) do
        v.enabled = flag
    end
end

function UICareer:Create_Chapter(chapterID,map)
    if self.mapTable[ chapterID] then return end
    print(self.uiName,":jumpto--------------chapterID:",chapterID,"----------------map:",map)

    local prevCoordX, prevCoordY
    local  boss_sectionID = 0
    map.name = "Map" .. chapterID
    self.boxcollider[chapterID] = {}
    self.boxcollider[chapterID][1] = map:GetComponent("BoxCollider")

    local i = 2
    UIEventListener.Get( map).onPress = LuaHelper.BoolDelegate(self:MoveOnPress())
    UIEventListener.Get( map).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())

    local chapterConfig = GameSystem.Instance.CareerConfigData:GetChapterData(chapterID)
    local  background = map.transform:FindChild("BGground")
    --ResourceLoadManager.Instance:LoadAloneImage("Texture/"..chapterConfig.area, self:OnBgTextureLoad(), self:OnBgTextureLoadFailed(), ResourceLoadType.AssetBundle)
    background:GetComponent("UITexture").mainTexture = ResourceLoadManager.Instance:GetResources("Texture/"..chapterConfig.area, true)
    local prevCoordX, prevCoordY
    local sectionID = chapterConfig.first_section_id
    while sectionID ~= 0 do
        local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(sectionID)
        local item = getLuaComponent(createUI("CareerSectionItem", map.transform))
        item.gameObject.name = "CareerSectionItem" .. sectionID
        self.boxcollider[chapterID][i] = item.gameObject:GetComponent("BoxCollider")
        i = i + 1
        item.chapterID = chapterID
        item.sectionID = sectionID
        item.onClick = self:MakeOnSectionClick()
        UIEventListener.Get( item.gameObject).onPress = LuaHelper.BoolDelegate(self:MoveOnPress())
        UIEventListener.Get( item.gameObject).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
        item.transform.localPosition = Vector3.New(sectionConfig.coord_x, sectionConfig.coord_y, 0)
        NGUITools.AdjustDepth(item.gameObject, 1)
        if prevCoordX and prevCoordY then
            local line = createUI("SectionLine", map.transform)
            local v1 = Vector3.New(prevCoordX, prevCoordY, 0)
            local v2 = Vector3.New(sectionConfig.coord_x, sectionConfig.coord_y, 0)
            local v3 = v2 - v1
            line.transform.right = v3:Normalize()
            line.transform.localPosition = (v1 + v2) / 2
            line:GetComponent("UIWidget").width = v3:Magnitude()
        end
        if boss_sectionID ~= 0 and MainPlayer.Instance:CheckSection(chapterID, boss_sectionID) then
            --创建图标和连线
            local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(boss_sectionID)
            local item = getLuaComponent(createUI("CareerSectionItem", map.transform))
            item.gameObject.name = "CareerSectionItem" .. boss_sectionID
            self.boxcollider[chapterID][i] = item.gameObject:GetComponent("BoxCollider")
            i = i + 1
            item.chapterID = chapterID
            item.sectionID = boss_sectionID
            item.is_boss = true
            item.onClick = self:MakeOnSectionClick()
            UIEventListener.Get( item.gameObject).onPress = LuaHelper.BoolDelegate(self:MoveOnPress())
            UIEventListener.Get( item.gameObject).onDrag = LuaHelper.VectorDelegate(self:MoveDrag())
            item.transform.localPosition = Vector3.New(sectionConfig.coord_x, sectionConfig.coord_y, 0)
            NGUITools.AdjustDepth(item.gameObject, 1)
            -- if prevCoordX and prevCoordY then
            --	local line = createUI("SectionLine", map.transform)
            --	line:GetComponent("UISprite").spriteName = "career_line_02"
            --	local v1 = Vector3.New(prevCoordX, prevCoordY, 0)
            --	local v2 = Vector3.New(sectionConfig.coord_x, sectionConfig.coord_y, 0)
            --	local v3 = v2 - v1
            --	line.transform.right = v3:Normalize()
            --	line.transform.localPosition = (v1 + v2) / 2
            --	line:GetComponent("UIWidget").width = v3:Magnitude()
            -- end
        end
        prevCoordX = sectionConfig.coord_x
        prevCoordY = sectionConfig.coord_y
        boss_sectionID = 0
        if string.find(sectionConfig.next_section_id, "&") then
            local ID = Split(sectionConfig.next_section_id, '&')
            sectionID = tonumber(ID[1])
            boss_sectionID = tonumber(ID[2])
        else
            sectionID = tonumber(sectionConfig.next_section_id)
        end
    end
    self.mapTable[chapterID] = map
end

function UICareer:OnBgTextureLoad( ... )
    return function (go)
        print("-----------------------Career Bg Texture Load finish")
        background:GetComponent("UITexture").mainTexture = go
    end
end

function UICareer:OnBgTextureLoadFailed( ... )
    return function (str)
        print("-----------------------Career Bg Texture Load failed: ", str)
    end
end

function UICareer:MakeOnRoleSelected()
    local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID)
    local gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
    self.game_mode_id = sectionConfig.game_mode_id
    local modeType = GameMode.IntToEnum(enumToInt(gameMode.matchType))
    local fightRoleInfoList = MainPlayer.Instance:GetFightRoleList(modeType)
    local enum = fightRoleInfoList:GetEnumerator()
    local i = 1
    self.fightList = {}
    while enum:MoveNext() do
        self.fightList[i] = {}
        self.fightList[i].role_id = enum.Current.role_id
        self.fightList[i].status = enum.Current.status:ToString()
        i = i + 1
    end
    local career = {
        chapter_id = CurChapterID,
        section_id = CurSectionID,
        fight_list = {
            game_mode = modeType:ToString(),
            fighters = self.fightList,
        }
    }
    local enterGame = {
        acc_id = MainPlayer.Instance.AccountID,
        type = 'MT_CAREER',

        career = career,
        game_mode = modeType:ToString(),
    }

    --local buf = protobuf.encode("fogs.proto.msg.StartSectionMatch", req)
    --LuaHelper.SendPlatMsgFromLua(MsgID.StartSectionMatchID, buf)
    local req = protobuf.encode("fogs.proto.msg.EnterGameReq",enterGame)
    LuaHelper.SendPlatMsgFromLua(MsgID.EnterGameReqID,req)
    LuaHelper.RegisterPlatMsgHandler(MsgID.EnterGameRespID, self:MakeStartMatchHandler(), self.uiName)
    CommonFunction.ShowWaitMask()
    CommonFunction.ShowWait()
end

function UICareer:MakeStartMatchHandler()
    return function (buf)
        CommonFunction.HideWaitMask()
        CommonFunction.StopWait()
        local resp, err = protobuf.decode("fogs.proto.msg.EnterGameResp", buf)
        --print("lllllllllllllllaaaaaa:",resp.result)
        if resp then
            if resp.result == 0 then
                self:SectionStart(resp.career.session_id)
            else
                CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
            end
        else
            error("UICareerSection: ", err)
        end
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.EnterGameRespID, self.uiName)
    end
end

function UICareer:SectionStart(session_id)
    --print("starstarstarstarstarstar")
    local section = MainPlayer.Instance:GetSection(CurChapterID, CurSectionID)
    local needPlot = not section.is_complete
    --不先转存一次，Unity要崩。。。要崩。。
    local teammates = UintList.New()
    for _,v in pairs(self.fightList) do
        teammates:Add(tonumber(v.role_id))
    end
    --set global
    --CurLoadingImage = "Texture/LoadShow"
    NGUITools.Destroy(self.uiBtnMenu.gameObject)
    GameSystem.Instance.mClient:CreateNewMatch(self.game_mode_id, session_id, needPlot, GameMatch.LeagueType.eCareer, teammates, nil)
end

function UICareer:InitMap()
    self.mapTable = nil
    self.mapTable = {}
    local tween = self.transform:FindChild("MapGrid/Map")
    self.uiMap = createUI("Map",tween)
    self.uiMap1 = createUI("Map",tween)
    local pos1 = self.uiMap.transform.localPosition
    pos1.x = pos1.x -1280
    self.uiMap1.transform.localPosition = pos1
    self.uiMap2 = createUI("Map",tween)
    local pos2 = self.uiMap.transform.localPosition
    pos2.x = pos2.x +1280
    self.uiMap2.transform.localPosition = pos2

    --reset config
    self.chapterConfig = GameSystem.Instance.CareerConfigData:GetChapterData(self.chapterID)

    --创建当前章节
    print(self.uiName,"-----current chapterID:",self.chapterID)
    self:Create_Chapter(self.chapterID ,self.uiMap)
    --创建上一章节item
    self.prechapterID = nil
    local enum = GameSystem.Instance.CareerConfigData.chapterConfig:GetEnumerator()
    while enum:MoveNext() do
        print(self.uiName,"-----chapterID:",self.chapterID)
        if enum.Current.Value.next_chapter_id == self.chapterID then
            self.prechapterID = enum.Current.Key
            break
        end
    end

    if self.prechapterID then
        self:Create_Chapter( self.prechapterID, self.uiMap1)
    end
    print(self.uiName,"-----current prechapterID:",self.prechapterID)
    --创建下一章节的item

    self.nextchapterID = self.chapterConfig.next_chapter_id
    if self.nextchapterID ~= 0 then
        self:Create_Chapter( self.nextchapterID ,self.uiMap2)
    end
    print(self.uiName,"-----current nextchapterID:",self.nextchapterID)

    --实现关卡跳转
    if self.sectionID and self.sectionID ~= 0 then
        for i = 0, self.mapTable[self.chapterID].transform.childCount-1 do
            local go = self.mapTable[self.chapterID].transform:GetChild(i).gameObject
            if string.find(go.name, 'CareerSectionItem') ~= nil then
                local item = getLuaComponent(go)
                if item and item.sectionID == tonumber(self.sectionID) then
                    self:MakeOnSectionClick()(item)

                    break
                end
            end
        end
        self.sectionID = nil
    end
    --生涯 -》球员-》生涯 -》球员 -》生涯  位置偏差
    local pos = tween.transform.localPosition
    pos.x = -500
    tween.transform.localPosition = pos
end

return UICareer

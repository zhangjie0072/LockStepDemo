require "common/StringUtil"

CareerSectionItem = {
    uiName = 'CareerSectionItem',

    ----------------UI
    uiRepeatableDone,
    uiRepeatableReady,
    uiRepeatableDisable,
    uiDisposableDisable,
    uiDisposableReady,
    uiDisposableDone,
    uiChampionDone,

    ----------------parameters
    chapterID = 0,
    sectionID = 0,
    onClick = nil,
    activate = nil,
    beActivate = nil,
    item = nil,
}

function CareerSectionItem:Awake()
    local transform = self.transform

    self.uiRepeatableDone = transform:FindChild('repeatableDone'):GetComponent('UISprite')
    self.uiRepeatableReady = transform:FindChild('repeatableReady'):GetComponent('UISprite')
    self.uiRepeatableDisable = transform:FindChild('repeatableDisable'):GetComponent('UISprite')
    self.uiDisposableDisable = transform:FindChild('disposableDisable'):GetComponent('UISprite')
    self.uiDisposableReady = transform:FindChild('disposableReady'):GetComponent('UISprite')
    self.uiDisposableDone = transform:FindChild('disposableDone'):GetComponent('UISprite')
    self.uiChampionDone = transform:FindChild('championDone'):GetComponent('UISprite')
    self.uiChampionDoneAnimator = self.uiChampionDone.transform:GetComponent("Animator")
    self.uiEffect = transform:FindChild("UIEffect1")
    self.uiEffectAnimator = self.uiEffect:GetComponent("Animator")
    self.uiCareerAnimator = UIManager.Instance.m_uiRootBasePanel.transform:FindChild("UICareer(Clone)"):GetComponent("Animator")
    self.uiRepeatableReadyEffect = self.uiRepeatableReady.transform:FindChild("texiao")
    self.uiRepeatableReadyArrow = self.uiRepeatableReady.transform:FindChild("Arrow")
    self.uiDisposableReadyArrow = self.uiDisposableReady.transform:FindChild("Arrow")
    self.uiStarNode = self.transform:FindChild("Stars")
    self.uiStars = {}
    for i = 1, 3 do
        self.uiStars[i] = getComponentInChild(self.transform, "Stars/Star" .. i, "UISprite")
    end

    -- libing 生涯常态关卡特效指引取消
    self.uiRepeatableReady.gameObject:SetActive(false)
    self.uiRepeatableReadyEffect.gameObject:SetActive(false)
end

function CareerSectionItem:Start()
    addOnClick(self.gameObject, function() if self.onClick then self:onClick() end end)
    print("self.sectionID:",self.sectionID)
    local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(self.sectionID)
    local gameMode = GameSystem.Instance.GameModeConfig:GetGameMode(sectionConfig.game_mode_id)
    local section = MainPlayer.Instance:GetSection(self.chapterID, self.sectionID)

    self.item = getLuaComponent(createUI(sectionConfig.frame, self.transform))
    self.item.config = sectionConfig
    self.item.chapter = self.chapterID
    self.item.section = self.sectionID

    local isLocked = not MainPlayer.Instance:CheckSection(self.chapterID, self.sectionID)
    if not isLocked then
        for i = 1, 3 do
            if i <= section.medal_rank then
                self.uiStars[i].spriteName = "match_qualifying_10"
            else
                self.uiStars[i].spriteName = "match_qualifying_13"
            end
        end
    else
        for i = 1 , 3 do
            self.uiStars[i].spriteName = "match_qualifying_13"
        end
    end

    for i = 1, 3 do
        self.uiStars[i].gameObject:SetActive(false)
    end

    local activateSectionID,beActivateSectionID = self:CheckAnimationSection()
    print("activateSectionID:",activateSectionID,"beActivateSectionID:",beActivateSectionID)
    if sectionConfig.type == 0 then -- 普通
        if isLocked then
            self.uiRepeatableDisable.gameObject:SetActive(true)
            NGUITools.SetActive(self.uiStarNode.gameObject,false)
            self.item.complete = false
        elseif section.is_complete then
            self.item.complete = true
            self.uiRepeatableDone.gameObject:SetActive(false)
        else
            self.uiRepeatableDone.gameObject:SetActive(false)
            self.item.complete = true

            -- libing 生涯常态关卡特效指引取消
            -- self.uiRepeatableReady.gameObject:SetActive(true)
            -- self.uiRepeatableReadyArrow.gameObject:SetActive(true)
            -- if GuideSystem.Instance.curModule then
            --  self.uiRepeatableReadyEffect.gameObject:SetActive(false)
            -- end
        end
        --activate
        if activateSectionID and self.sectionID == activateSectionID then
            if CurActivateID and CurActivateID == self.sectionID then
                return
            end
            --play section animation
            print("play animation up:",self.sectionID)
            NGUITools.SetActive(self.uiEffect.gameObject, true)
            self.activate = true
        end
    elseif sectionConfig.type == 1 then --精英
        local id = tonumber(sectionConfig.icon)
        self.uiChampionDone.atlas = getPortraitAtlas(id)
        self.uiChampionDone.spriteName = 'icon_portrait_'..tostring(id)
        self.uiChampionDone:MakePixelPerfect()
        --be activated
        if beActivateSectionID and self.sectionID == beActivateSectionID then
            --play boss animation
            if CurBeActivatedID and CurBeActivatedID == self.sectionID then
                return
            end
            print("play animation dn:",self.sectionID)
            NGUITools.SetActive(self.uiEffect.gameObject, true)
            self.uiChampionDoneAnimator.enabled = true
            self.beActivate = true
        else
            self.uiChampionDone.gameObject:SetActive(true)
        end
    elseif sectionConfig.type == 2 then --一次性
        if isLocked then
            self.uiDisposableDisable.gameObject:SetActive(true)
        elseif section.is_complete then
            self.uiDisposableDone.gameObject:SetActive(true)
        else
            self.uiDisposableReady.gameObject:SetActive(true)
            self.uiDisposableReadyArrow.gameObject:SetActive(true)
        end

        NGUITools.SetActive(self.uiStarNode.gameObject,false)
    end
end

function CareerSectionItem:CheckAnimationSection()
    if not CurSectionID or not CurChapterID then
        return false
    end

    --and CurSectionComplete ~= section.is_complete
    local sectionConfig = GameSystem.Instance.CareerConfigData:GetSectionData(CurSectionID)
    local section = MainPlayer.Instance:GetSection(CurChapterID, CurSectionID)
    if string.find(sectionConfig.next_section_id, "&") and CurSectionComplete ~= section.is_complete then
        local ID = Split(sectionConfig.next_section_id, '&')
        sectionID = tonumber(ID[1])
        boss_sectionID = tonumber(ID[2])
        return CurSectionID, boss_sectionID
    else
        return false
    end
end

function CareerSectionItem:FixedUpdate()
    if self.activate == true and not self:IsTeamUpgradeVisible() and  not self:IsAnimation() then
        print("~!~!~!~!~!~!~!~!~!:EF_1up:",self.sectionID)
        self.uiEffectAnimator:SetTrigger("EF_1up")
        CurActivateID = self.sectionID
        self.activate = nil
    end

    -- if not self.xxx and not self:IsAnimation() then
    --  print("~!~!~!~!~!~!~!~!~!:EF_1up:",self.sectionID,"--isplaying:",self:IsAnimation())
    --  self.uiEffectAnimator:SetTrigger("EF_1up")
    --  self.xxx = true
    -- end


    if self.beActivate == true and not self:IsTeamUpgradeVisible() and not self:IsAnimation() then
        print("~!~!~!~!~!~!~!~!~!:EF_1dn:",self.sectionID)
        self.uiChampionDone.gameObject:SetActive(true)
        self.uiEffectAnimator:SetTrigger("EF_1dn")
        CurBeActivatedID = self.sectionID
        self.beActivate = nil
    end
end

function CareerSectionItem:IsTeamUpgradeVisible()
    return UIManager.Instance.m_uiRootBasePanel.transform:FindChild("TeamUpgradePopup(Clone)")
end

function CareerSectionItem:IsAnimation()
    local animator = self.uiCareerAnimator
    for i = 0 ,(animator.layerCount - 1) do
        local isInTransition = animator:IsInTransition(i)
        local time = animator:GetCurrentAnimatorStateInfo(i).normalizedTime
        if isInTransition or time < 1 then
            return true
        end
    end
    return false
end

return CareerSectionItem

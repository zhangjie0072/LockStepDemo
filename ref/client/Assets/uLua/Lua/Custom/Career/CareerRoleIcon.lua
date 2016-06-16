require "common/stringUtil"

CareerRoleIcon =  {
    uiName = 'CareerRoleIcon',
    -----------UI
    uiIcon,
    uiPostion,
    uiBGframe,
    uiRedDot,
    uiSele,
    uiRoleName,
    uiTalent,
    uiBgCorner,
    uiLevel,
    uiTalentBg,
    uiStarGrid,
    uiReady,
    uiPosition,
    uiMaster,
    -----------parameters
    id=0,
    status = 'FS_NONE',
    npc = false,
    isRobot = false,
    isShowName = false,
    ranking,
    talent,
    onClick,
    onClose,
    showPosition = true,
    displayLevel = true,
    gSideTalent = nil,
    gSideQuality = nil,
    starList,
    qualityColLvTb,
    qualityShadeTb,
    displayStar,
    isQReal = false,
    qQuality,
    qLevel,
    qStar,
    otherInfo,
    disabled = false,
    showName,					-- finally use showName,
}

---------------------------------------------------------------------


function CareerRoleIcon:Awake()
    self.uiTalent = self.transform:FindChild("Talent/Label"):GetComponent("UILabel")
    self.uiLevel = self.transform:FindChild("Level"):GetComponent("UILabel")
    self.uiBgCorner = self.transform:FindChild("Talent"):GetComponent("UISprite")
    self.uiStarGrid = self.transform:FindChild("Star"):GetComponent("UIGrid")
    self.uiQualityGrid = self.transform:FindChild("Quality"):GetComponent("UIGrid")
    self.uiMask = self.transform:FindChild("Mask").gameObject
    self.uiButtonClose = self.transform:FindChild("ButtonX").gameObject
    self.uiSelected = self.transform:FindChild("ButtonV").gameObject
    self.uiRanking = getComponentInChild(self.transform, "Rank", "UISprite")
    self.uiReady = self.transform:FindChild("Ready")
    self.uiPostion = self.transform:FindChild("Position"):GetComponent("UISprite")
    self.uiMaster = self.transform:FindChild("Master")

    self.qualityShadeTb = {}
    for i = 1, 4 do
        table.insert(self.qualityShadeTb, self.transform:FindChild("Quality/QualityShade"..i):GetComponent("UISprite"))
    end

    local colStr = GameSystem.Instance.CommonConfig:GetString("gQualityColLv")
    self.qualityColLvTb = {}
    local items = Split(colStr, "&")
    for k, v in pairs(items) do
        table.insert(self.qualityColLvTb, tonumber(v))
    end


    self.starList = {}
    for i = 1, 5 do
        table.insert(self.starList, self.transform:FindChild("Star/Star"..i):GetComponent("UISprite"))
    end

    CareerRoleIcon.LoadSide()
    local child = self.transform:FindChild("Icon")
    if child ~= nil then
        self.uiIcon = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("Position")
    if child ~= nil then
        self.uiPostion = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("BG")
    if child ~= nil then
        self.uiBGframe = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("BG/BG")
    if child ~= nil then
        self.uiBGBack = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("RedDot")
    if child ~= nil then
        self.uiRedDot = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("Sele")
    if child ~= nil then
        self.uiSele = child:GetComponent("UISprite")
    end

    child = self.transform:FindChild("Name")
    if child ~= nil then
        self.uiRoleName = child:GetComponent("UILabel")
    end

    -- self.uiPostion = self.transform:FindChild("Position"):GetComponent("UISprite")
    -- self.uiBGframe = self.transform:FindChild("BG"):GetComponent("UISprite")
    -- self.uiBGBack  = self.transform:FindChild("BG/BG"):GetComponent("UISprite")
    -- self.uiRedDot = self.transform:FindChild("RedDot"):GetComponent("UISprite")
    -- self.uiSele = self.transform:FindChild("Sele"):GetComponent("UISprite")
    -- self.uiRoleName = self.transform:FindChild("Name"):GetComponent("UILabel")
    addOnClick(self.gameObject, self:OnClick())
    addOnClick(self.uiButtonClose, self:OnClose())
end

function CareerRoleIcon:Start()

    self:Refresh( )
end

function CareerRoleIcon:FixUpdate( ... )
    -- body
end

function CareerRoleIcon:OnClose( ... )
    -- body
end

function CareerRoleIcon:OnDestroy( ... )
    -- body
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function CareerRoleIcon:Refresh( ... )
    -- print(self.uiName, "Refresh", self.id)
    self:SetById( )
end
---------------------------------------------------------------------


function CareerRoleIcon:SetById( )
    if self.id == 0 then return end

    local id = self.id
    local positions ={'PF','SF','C','PG','SG'}

    if self.isQReal then
        self.uiIcon.atlas = getPortraitAtlas(id)
        self.uiIcon.spriteName = 'icon_portrait_'..tostring(id)

        local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
        if not roleConfig then error(self.uiName, "No base config for role ID:", id) end

        self.uiLevel.text = self.qLevel
        self:SetStar(self.qStar)

        if self.uiPostion ~= nil then
            if self.showPosition == true then
                self.uiPostion.spriteName = 'PT_'..positions[roleConfig.position]
            else
                NGUITools.SetActive( self.uiPostion.gameObject , false)
            end
        end

        if self.uiRoleName ~= nil then
            if self.isShowName then
                self.uiRoleName.text = roleConfig.name
            else
                NGUITools.SetActive(self.uiRoleName.gameObject, false)
            end
        end

        ---frame
        if self.isRobot == false then
            self.talent = GameSystem.Instance.RoleBaseConfigData2:GetIntTalent(id)
        end
        local quality = self.qQuality
        self:SetSideByQuality(quality)
        --self:SetQuality(quality)
    elseif not self.npc then
        self.uiIcon.atlas = getPortraitAtlas(id)
        self.uiIcon.spriteName = 'icon_portrait_'..tostring(id)

        local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
        if not roleConfig then error(self.uiName, "No base config for role ID:", id) end

        local roleInfo = MainPlayer.Instance:GetRole2(id)

        if self.otherInfo then
            roleInfo = self.otherInfo
        end
        if roleInfo and self.displayLevel then
            self.uiLevel.text = roleInfo.level
        else
            self.uiLevel.gameObject:SetActive(false)
        end

        if self.qLevel then
            self.uiLevel.text = self.qLevel
            self.uiLevel.gameObject:SetActive(true)
        end


        if not self.displayStar and roleInfo and not self.displayStar and not self.qStar then
            self:SetStar(roleInfo.star)
        end

        if self.displayStar then
            self:SetStar(self.displayStar)
        end

        if self.qStar then
            self:SetStar(self.qStar)
        end

        if self.uiPostion ~= nil then
            if self.showPosition == true then
                self.uiPostion.spriteName = 'PT_'..positions[roleConfig.position]
            else
                NGUITools.SetActive( self.uiPostion.gameObject , false)
            end
        end

        if self.uiRoleName ~= nil then
            if self.isShowName then
                self.uiRoleName.text = roleConfig.name
            else
                NGUITools.SetActive(self.uiRoleName.gameObject, false)
            end
        end

        ---frame
        if self.isRobot == false then
            self.talent = GameSystem.Instance.RoleBaseConfigData2:GetIntTalent(id)
        end
        local quality = 1
        if roleInfo and not self.qQuality then
            quality = roleInfo.quality
        end
        if self.qQuality then
            quality = self.qQuality
        end

        self:SetSideByQuality(quality)
        --self:SetQuality(quality)

    else
        -- print("NPCID:",id)
        local npcConfig = GameSystem.Instance.NPCConfigData:GetConfigData(id)
        local shap_id = GameSystem.Instance.NPCConfigData:GetShapeID(id)
        self.talent = npcConfig.talent
        self.uiIcon.atlas = getPortraitAtlas(shap_id)
        self.uiIcon.spriteName = npcConfig.icon

        if self.uiPostion ~= nil then
            if self.showPosition == true then
                self.uiPostion.spriteName = 'PT_'..positions[npcConfig.position]
            else
                NGUITools.SetActive( self.uiPostion.gameObject , false)
            end
        end

        if self.isShowName then
            self.uiRoleName.text = npcConfig.name
        else
          if self.uiRoleName ~= nil then
              NGUITools.SetActive(self.uiRoleName.gameObject, false)
            end
        end

        self.uiLevel.gameObject:SetActive(false)
        self:SetSideByTalent(self.talent)
    end
    -- print(tostring(self.talent))
    self.uiTalent.text = getQualitystr(self.talent)
    --NGUITools.SetActive(self.uiMask, self.disabled)
    if self.disabled then
        self.uiIcon.color = Color.New(0,1,1,1)
        self.uiBGframe.color = Color.New(0,1,1,1)
        self.uiPostion.color = Color.New(0,1,1,1)
    else
        self.uiIcon.color = Color.New(1,1,1,1)
        self.uiBGframe.color = Color.New(1,1,1,1)
        self.uiPostion.color = Color.New(1,1,1,1)
    end

    self.uiBgCorner.gameObject:SetActive(false)
    self.uiLevel.gameObject:SetActive(false)
    for i=1,#self.starList do
        self.starList[i].gameObject:SetActive(false)
    end

    if self.ranking then
        NGUITools.SetActive(self.uiRanking.gameObject, true)
        self.uiRanking.spriteName = self["RankingIcon" .. self.ranking]
    end

    if self.showName and self.uiRoleName then
        self.uiRoleName.gameObject:SetActive(true)
        self.uiRoleName.text = self.showName
    end
end

function CareerRoleIcon:OnClick()
    return function()
        if self.onClick then self.onClick(self) end
    end
end

function CareerRoleIcon:OnClose()
    return function ()
        if self.onClose then self.onClose(self) end
    end
end

function CareerRoleIcon:SetState(state)
    NGUITools.SetActive(self.uiRedDot.gameObject, state)
end

function CareerRoleIcon:SetSele(sele)
    NGUITools.SetActive(self.uiSele.gameObject, sele)
end

function CareerRoleIcon:SetSelected(selected)
    NGUITools.SetActive(self.uiSelected, selected)
end

function CareerRoleIcon:EnableClose(enable)
    NGUITools.SetActive(self.uiButtonClose, enable)
end

function CareerRoleIcon:SetSideByTalent(talent)
    -- Not need for Tencent.
    -- local str = CareerRoleIcon.gSideTalent[talent]
    -- if str == nil then
    --	error( "SetSideBytalent for talent=", talent)
    --	return
    -- end
    -- local side = Split(str, "&")
    -- self.uiBGframe.spriteName = side[1]
    -- self.uiBgCorner.spriteName = side[2]
end

function CareerRoleIcon:SetSideByQuality(quality)
    -- Not need for Tencent.
    -- local str
    -- for k, v in pairs(CareerRoleIcon.gSideQuality) do
    --	if quality >= k then
    --		str = CareerRoleIcon.gSideQuality[k]
    --	end
    -- end

    -- if str == nil then
    --	error( "SetSideBytalent for talent=", quality)
    --	return
    -- end

    -- local side = Split(str, "&")
    -- self.uiBGframe.spriteName = side[1]
    -- self.uiBgCorner.spriteName = side[2]
end



function CareerRoleIcon.LoadSide()
    if CareerRoleIcon.gSideTalent == nil then
        CareerRoleIcon.gSideTalent = {}
        local str = GameSystem.Instance.CommonConfig:GetString("gCareerRISideTalent")
        local items = Split(str, "&")
        for k, v in pairs(items) do
            local item = Split(v, ":")
            if #item ~= 3 then
                error("gCareerRISideTalent error for ", v )
                return
            end
            CareerRoleIcon.gSideTalent[tonumber(item[1])] = item[2].."&"..item[3]
        end
    end

    if CareerRoleIcon.gSideQuality == nil then
        CareerRoleIcon.gSideQuality = {}
        local str = GameSystem.Instance.CommonConfig:GetString("gCareerRISideQuality")
        local items = Split(str, "&")
        for k, v in pairs(items) do
            local item = Split(v, ":")
            if #item ~= 3 then
                error("gCareerRISideTalent error for ", v )
                return
            end
            CareerRoleIcon.gSideQuality[tonumber(item[1])] = item[2].."&"..item[3]
        end
    end
end

function CareerRoleIcon:SetStar(star)
    for i = 1, 5 do
        self.starList[i].gameObject:SetActive(true)
    end

    for i = 1, 5 do
        if star <= 5 then
            if star >= i then
                self.starList[i].spriteName = "career_star"
            else
                self.starList[i].gameObject:SetActive(false)
            end
        else
            self.starList[i].spriteName = (star -5 )>= i and "com_card_star_purple" or "career_star"
        end
    end
    self.uiStarGrid.repositionNow = true
end
--[[
function CareerRoleIcon:SetQuality(quality)
    local lvBase = 0
    for i = 1, 4 do
        if quality >= self.qualityColLvTb[i] then
            lvBase = lvBase + 1
        else
            break
        end
    end

    local offBase = 0
    if quality >1 then
        offBase = quality - self.qualityColLvTb[lvBase]
    end

    local coltb = {
        "gree",
        "blue",
        "purple",
        "orange"
    }

    for i = 1, 4 do
        self.qualityShadeTb[i].gameObject:SetActive(i <= lvBase)
        if i <= lvBase  then
            if i <= offBase then
                self.qualityShadeTb[i].gameObject:SetActive(true)
                self.qualityShadeTb[i].spriteName = "com_card_d_"..coltb[lvBase]
            else
                self.qualityShadeTb[i].gameObject:SetActive(false)
            end
        end
    end
    self.uiQualityGrid.repositionNow = true
end
--]]

--------------------------------------------------------------------------------
-- Function Name : SetIsMaster
-- Create Time   : Wed Apr  6 15:38:19 2016
-- Input Value   : 是否是房主
-- Return Value  : nil
-- Description   : 根据是否是房主，显示或隐藏房主图标
--------------------------------------------------------------------------------
function CareerRoleIcon:SetIsMaster(isMaster)
    if not self.uiMaster then
        return
    end
    self.uiMaster.gameObject:SetActive(isMaster)
end

function CareerRoleIcon:SetClickEnable(bool)
    local boxCollider = self.transform:GetComponent("BoxCollider")
    if boxCollider then
        boxCollider.enabled = bool
    end
end




return CareerRoleIcon

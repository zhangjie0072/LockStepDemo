GoodsIconConsume =  {
    uiName = "GoodsIconConsume",

    ------------------
    rewardId = 1,
    rewardNum = 0,
    totalNum = nil,
    isAdd = true,
    isGray = nil,
    isPiece = false,
    isScrawl = false,
    isTask = false,
    isBG = false,
    customStr = nil,

    ----------------UI
    uiIcon,
    uiNum,
    uiBG,
    uiPlayerChip,
    playerChip,
}

function GoodsIconConsume:Awake()
    self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
    self.uiNum  = self.transform:FindChild("Num"):GetComponent("UILabel")
    self.uiBG   = self.transform:FindChild("BG")
    self.uiPlayerChipNode = self.transform:FindChild("PlayerChip")
end

function GoodsIconConsume:Start()
    if self.uiBG ~= nil then
        NGUITools.SetActive(self.uiBG.gameObject, false)
    end

    self:Refresh()
end

function GoodsIconConsume:Refresh()
    if not self.rewardId then return end
    -- print(self.uiName, "id:", self.rewardId, "num:", self.rewardNum)

    -- set icon
    local goods_attr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.rewardId)
    local icon = goods_attr.icon
    if self.isGray then
        icon = icon
    end

    self.isPiece = false
    self.uiIcon.gameObject:SetActive(true)
    if self.playerChip then GameObject.Destroy(self.playerChip.gameObject)  self.playerChip = nil end


    --临时处理这样，等策划修改配置文件后，修改
    if self.rewardId == 1 or self.rewardId == 2 or self.rewardId == 5 or self.rewardId == 8 then
        self.uiIcon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/tencent/tencom")
        -- self.isPiece = false
        self.isScrawl = false
    elseif self.rewardId == 4024 then
        self.uiIcon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/tencent/tencom")
        self.isScrawl = true
    elseif self.rewardId == 10 then
        self.uiIcon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/tencent/tencom")
    else
        self.uiIcon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/common/common2")
        self.isPiece = true
    end

    if self.rewardId == 5 then --球队经验特殊处理
        self.uiIcon.spriteName = "com_property_exp"
    else
        self.uiIcon.spriteName = icon
    end

    --是否显示背景
    if self.uiBG ~= nil then
        NGUITools.SetActive(self.uiBG.gameObject, self.isBG)
    end

    --涂鸦碎片特殊处理
    if self.isScrawl == true then
        self.uiIcon.spriteName = "tencent_08"
    end

    --球员碎片特殊处理
    if self.isPiece == true then
        -- self.uiIcon.spriteName = "com_frame_c_frame_chipC"
        self.uiIcon.gameObject:SetActive(false)

        -- 创建一个球员碎片
        self.playerChip = createUI('GoodsIcon', self.uiPlayerChipNode)
        local chipLua = getLuaComponent(self.playerChip.gameObject)
        chipLua.goodsID = self.rewardId
        chipLua.hideNum = true
        chipLua.hideNeed = true

        -- showoff effect
        local effect = self.playerChip.transform:Find("SpecialEffect").gameObject
        if effect.activeSelf then effect:SetActive(false) end

        local chipIcon = self.playerChip.transform:Find("ChipIcon"):GetComponent("UISprite")
        chipIcon.width = 31
        chipIcon.height = 29
    end

    -- set num
    local numStr
    --自定义字符不等于nil，使用者在外部设置显示内容
    if not self.customStr then
        if self.isAdd == true then
            numStr = "+"..tostring(self.rewardNum)
        else
            numStr = tostring(self.rewardNum)
            if self.totalNum then
                if self.totalNum >= self.rewardNum then
                    numStr = tostring(self.totalNum) .. "/" .. numStr
                else
                    numStr = "[FF0000]" .. tostring(self.totalNum) .. "[-]/" .. numStr
                end
            end
        end
    else
        numStr = self.customStr
    end

    local multiLabel = self.uiNum.transform:GetComponent("MultiLabel")
    if multiLabel then
        multiLabel:SetText(numStr)
    else
        self.uiNum.text = numStr
    end

    if self.isTask then
        if self.rewardId == 1 then
            self.uiNum.color = Color.New(138/255, 216/255, 254/255, 1)
        elseif self.rewardId == 2 then
            self.uiNum.color = Color.New(250/255, 184/255, 62/255, 1)
        elseif self.rewardId == 5 then
            self.uiNum.color = Color.New(252/255 , 0, 255/255, 1)
        end
    end
end

function GoodsIconConsume:OnDestroy()
    --body
    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function GoodsIconConsume:SetData(id,num,isGray)
    self.rewardId   = id
    self.rewardNum  = num
    self.isGray = isGray
    self:Refresh()
end

return GoodsIconConsume

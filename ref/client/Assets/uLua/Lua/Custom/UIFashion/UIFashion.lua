--encoding=utf-8

UIFashion =  {
    uiName="UIFashion",

    ----------------------------------
    titleStr = 'STR_ROLE_FASHION',
    reputationStore = false,    --声望商店
    isWardrobe = false,
    selectedTab,
    gender = 0,
    playerId,
    tabState,
    zoneTabState,
    selecStoreItem,
    selecWardrobeItem,
    initPosX,
    isInitAnimator = false,
    -- initPlayerId,
    banTwice = false,
    banTwice1 = false,
    barValue = 0,

    tryDressList = {0,0,0,0,0},
    DressList = {0,0,0,0,0},
    noDress =
    {
        'fashion_b_hat',
        'fashion_b_clothes',
        'fashion_b_pant',
        'fashion_b_shoe',
        'fashion_b_back'
    },
    yesDress =
    {
        'fashion_b_hat_w',
        'fashion_b_clothes_w',
        'fashion_b_pant_w',
        'fashion_b_shoe_w',
        'fashion_b_back_w'
    },

    ----------------------------------UI
    uiTitle,
    uiBtnMenu,
    uiAnimator,
    uiChangeAimator,
    uiBtnBack,
    uiPlayerPropertyLua,
    uiWardrobe,
    uiShop,
    uiShopToggle,
    uiWardrobeToggle,
    uiSelectZone,
    uiZone,
    uiModel,
    uiModelShowItem,
    uiLeftKey,
    uiBuySelected,
    uiFashionRedDot,
    uiSubstitute,
    uiSelectTabGrid,
    uiSelectScrollView,
    uiSelectSVIncLoad,
    uiSelectGrid,
    uiZoneTabGrid,
    uiZoneScrollBar,
    uiZoneScrollView,
    uiZoneSVIncLoad,
    uiZoneGrid,
    uiBuyPos,
    uiEmptyText,

    uiLeftPart = {},
    uiWardrobeTab = {},
    uiFashionStoreTab = {},
    uiWardrobeTabTips = {},

    uiFashionBuyOne,
}


-----------------------------------------------------------------
function UIFashion:Awake()
    -- self.uiProcessBar = self.transform:FindChild("ProcessBar"):GetComponent("UIProgressBar")
    self.uiTitle = self.transform:FindChild('Top/Title'):GetComponent('MultiLabel')
    self.uiBtnMenu = createUI('ButtonMenu', self.transform:FindChild('Top/ButtonMenu'))
    self.uiBtnBack = createUI('ButtonBack', self.transform:FindChild('Top/ButtonBack'))
    self.uiWardrobe = self.transform:FindChild('Tab/Wardrobe'):GetComponent('UISprite')
    self.uiShop = self.transform:FindChild('Tab/Shop'):GetComponent('UISprite')
    self.uiWardrobeToggle = self.transform:FindChild('Tab/Wardrobe'):GetComponent('UIToggle')
    self.uiShopToggle = self.transform:FindChild('Tab/Shop'):GetComponent('UIToggle')
    self.uiFashionRedDot = self.transform:FindChild('MyWardrobe/RedDot'):GetComponent('UISprite')
    -----
    self.uiModel = self.transform:FindChild('Model')
    self.uiModelShowItem = self.transform:FindChild('Model/ModelShowItem'):GetComponent("ModelShowItem")

    --left
    self.uiLeftKey = self.transform:FindChild('LeftKey')

    self.uiLeftPart[1] = self.uiLeftKey.transform:FindChild('Head'):GetComponent('UISprite')
    self.uiLeftPart[2] = self.uiLeftKey.transform:FindChild('Clothes'):GetComponent('UISprite')
    self.uiLeftPart[3] = self.uiLeftKey.transform:FindChild('Trousers'):GetComponent('UISprite')
    self.uiLeftPart[4] = self.uiLeftKey.transform:FindChild('Shoes'):GetComponent('UISprite')
    self.uiLeftPart[5] = self.uiLeftKey.transform:FindChild('Special'):GetComponent('UISprite')

    --button
    self.uiBuySelected = self.transform:FindChild('BuySelected')--:GetComponent('UIButton')
    self.uiSubstitute = self.transform:FindChild('Substitute')--:GetComponent('UIButton')

    --selectzone
    self.uiSelectZone = self.transform:FindChild('AllZone/SelectZone')
    self.uiSelectTabGrid = self.transform:FindChild('AllZone/SelectZone/Position1/TabGrid')
    self.uiSelectScrollView = self.transform:FindChild('AllZone/SelectZone/Position1/SelectScrollView'):GetComponent('UIScrollView')
    self.uiSelectSVIncLoad = self.uiSelectScrollView:GetComponent("ScrollViewAsyncLoadItem")
    self.uiSelectGrid = self.transform:FindChild('AllZone/SelectZone/Position1/SelectScrollView/SelectGrid'):GetComponent('UIGrid')

    self.uiFashionStoreTab[1] = self.uiSelectTabGrid:FindChild('TabHead'):GetComponent('UIToggle')
    self.uiFashionStoreTab[2] = self.uiSelectTabGrid:FindChild('TabClothes'):GetComponent('UIToggle')
    self.uiFashionStoreTab[3] = self.uiSelectTabGrid:FindChild('TabTrouses'):GetComponent('UIToggle')
    self.uiFashionStoreTab[4] = self.uiSelectTabGrid:FindChild('TabShoes'):GetComponent('UIToggle')
    self.uiFashionStoreTab[5] = self.uiSelectTabGrid:FindChild('TabSpecial'):GetComponent('UIToggle')
    self.uiFashionStoreTab[6] = self.uiSelectTabGrid:FindChild('TabSuit'):GetComponent('UIToggle')
    --zone
    self.uiZone = self.transform:FindChild('AllZone/MyZone')
    self.uiZoneTabGrid = self.transform:FindChild('AllZone/MyZone/Position2/TabGrid')
    self.uiZoneScrollView = self.transform:FindChild('AllZone/MyZone/Position2/SelectScrollView'):GetComponent('UIScrollView')
    self.uiZoneScrollBar = self.transform:FindChild('AllZone/MyZone/Position2/Schedule'):GetComponent('UIScrollBar')
    self.uiZoneSVIncLoad = self.uiZoneScrollView:GetComponent("ScrollViewAsyncLoadItem")
    self.uiZoneGrid = self.transform:FindChild('AllZone/MyZone/Position2/SelectScrollView/SelectGrid'):GetComponent('UIGrid')
    self.uiEmptyText = self.transform:FindChild('AllZone/MyZone/Position2/EmptyText')

    self.uiWardrobeTab[1] = self.uiZoneTabGrid:FindChild('TabHead'):GetComponent('UIToggle')
    self.uiWardrobeTab[2] = self.uiZoneTabGrid:FindChild('TabClothes'):GetComponent('UIToggle')
    self.uiWardrobeTab[3] = self.uiZoneTabGrid:FindChild('TabTrouses'):GetComponent('UIToggle')
    self.uiWardrobeTab[4] = self.uiZoneTabGrid:FindChild('TabShoes'):GetComponent('UIToggle')
    self.uiWardrobeTab[5] = self.uiZoneTabGrid:FindChild('TabSpecial'):GetComponent('UIToggle')
    self.uiWardrobeTab[6] = self.uiZoneTabGrid:FindChild('TabSuit'):GetComponent('UIToggle')
    self.uiWardrobeTabTips[1] = self.uiZoneTabGrid:FindChild('TabHead/Tip'):GetComponent('UISprite')
    self.uiWardrobeTabTips[2] = self.uiZoneTabGrid:FindChild('TabClothes/Tip'):GetComponent('UISprite')
    self.uiWardrobeTabTips[3] = self.uiZoneTabGrid:FindChild('TabTrouses/Tip'):GetComponent('UISprite')
    self.uiWardrobeTabTips[4] = self.uiZoneTabGrid:FindChild('TabShoes/Tip'):GetComponent('UISprite')
    self.uiWardrobeTabTips[5] = self.uiZoneTabGrid:FindChild('TabSpecial/Tip'):GetComponent('UISprite')
    self.uiWardrobeTabTips[6] = self.uiZoneTabGrid:FindChild('TabSuit/Tip'):GetComponent('UISprite')

    self.uiBuyPos = self.transform:FindChild('BuyPos')
    self.uiAnimator = self.transform:GetComponent('Animator')
    self.uiChangeAimator = self.transform:FindChild('AllZone'):GetComponent('Animator')
end

function UIFashion:Start()
    local menu = getLuaComponent(self.uiBtnMenu)
    menu:SetParent(self.gameObject, true)
    menu.parentScript = self
    -- self:Refresh()

    addOnClick(self.uiBtnBack.gameObject, self:ClickBack())
    -- addOnClick(self.uiWardrobe.gameObject, self:ChangeWardrobe())
    addOnClick(self.uiWardrobe.gameObject, self:SwitchWardrobe())
    addOnClick(self.uiShop.gameObject, self:SwitchShop())
    self.uiShopToggle.value = true
    -- self.uiWardrobeToggle.value = false

    for i=1,#self.uiWardrobeTab do
        addOnClick(self.uiFashionStoreTab[i].gameObject, self:ClickTab())
        addOnClick(self.uiWardrobeTab[i].gameObject, self:ClickZoneTab())
    end

    for i=1,#self.uiLeftPart do
        addOnClick(self.uiLeftPart[i].gameObject, self:ClickUnloadPart())
    end

    addOnClick(self.uiSubstitute.gameObject, self:ClickSubstitute())
    addOnClick(self.uiBuySelected.gameObject, self:ClickBuySelected())
    local modelConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(MainPlayer.Instance.CaptainID)
    self.gender = modelConfig.gender
    self.initPosX = self.uiSubstitute.transform.localPosition.x

    local uiPlayerProperty = self.transform:FindChild('Top/PlayerInfoGrids').gameObject
    self.uiPlayerPropertyLua = getLuaComponent(uiPlayerProperty)
    -- self:ClickTab()(self.uiFashionStoreTab[1].gameObject)
    -- self:ClickZoneTab()(self.uiWardrobeTab[1].gameObject)
    -- NGUITools.SetActive(self.uiBuySelected.gameObject, false)
    LuaHelper.RegisterPlatMsgHandler(MsgID.FashionOperationRespID, self:FashionOperationRespHandler(), self.uiName)

    NGUITools.SetActive(self.uiSelectZone.gameObject, true);
    NGUITools.SetActive(self.uiZone.gameObject, false);
end

function UIFashion:FixedUpdate()
end

function UIFashion:OnClose()
    local menu = getLuaComponent(self.uiBtnMenu)
    menu:SetParent(self.gameObject, true)
    menu.parentScript = self
    -- self.isInitAnimator = false
    self.playerId = nil
    self.selecStoreItem = nil
    -- print('self.initPlayerId :' .. tostring(self.initPlayerId))
    -- MainPlayer.Instance.CaptainID = self.initPlayerId
    local pos = self.uiSubstitute.transform.localPosition
    pos.x = self.initPosX
    self.uiSubstitute.transform.localPosition = pos
    if self.onClose then
        self.onClose()
        self.onClose = nil

        -- NGUITools.Destroy(self.gameObject)
        return
    end

    if self.nextShowUI then
        TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
        self.nextShowUI = nil
    else
        TopPanelManager:HideTopPanel()
    end
    -- NGUITools.Destroy(self.gameObject)
end

function UIFashion:OnDestroy()
    LuaHelper.UnRegisterPlatMsgHandler(MsgID.FashionOperationRespID, self.uiName)


    Object.Destroy(self.uiAnimator)
    Object.Destroy(self.transform)
    Object.Destroy(self.gameObject)
end

function UIFashion:Refresh()
    -- self.uiWardrobe.spriteName = 'fashion_wardrobe'
    self.isWardrobe = false
    self.isInitAnimator = false
    NGUITools.SetActive(self.uiFashionRedDot.gameObject, false)
    self:RefreshFashionTips()
    --self:ClickZoneTab()(self.uiWardrobeTab[1].gameObject)
    self.uiFashionStoreTab[1].value = true
    self:ClickTab()(self.uiFashionStoreTab[1].gameObject)
    local pos = self.uiSubstitute.transform.localPosition
    -- pos.x = pos.x - 100
    self.uiSubstitute.transform.localPosition = pos
    NGUITools.SetActive(self.uiBuySelected.gameObject, not self.isWardrobe)
    --NGUITools.SetActive(self.uiBuySelected.gameObject, false)
    print('self.reputationStore = ' .. tostring(self.reputationStore))
    self:SetTitle(self.titleStr)
    if self.reputationStore then
        self.uiPlayerPropertyLua.showReputation = true
        self.uiPlayerPropertyLua:RefreshReputationStore()
    else
        self.uiPlayerPropertyLua.showReputation = false
        self.uiPlayerPropertyLua.uiReputation.gameObject:SetActive(true)
    end
    local menu = getLuaComponent(self.uiBtnMenu)
    menu:Refresh()
    self:RefreshModel()
    self:UpdateFashionStore()()
    --self:UpdateWardrobe()()
    self:EnabledButton(true)
end


-----------------------------------------------------------------
function UIFashion:SetInitDress( ... )
    local mId = self:FindDressFasionIDList()
    self.DressList = mId
    self:UpdateLeftIcons(self.DressList)
end

function UIFashion:RefreshModel( ... )
    print('MainPlayer.Instance.CaptainID ======= ' .. MainPlayer.Instance.CaptainID)
    print('self.playerId ======= ' .. tostring(self.playerId))
    if not self.playerId then
        self.playerId = MainPlayer.Instance.CaptainID
        -- self.initPlayerId = MainPlayer.Instance.CaptainID
    end
    self.uiModelShowItem.IsFashion = true
    self.uiModelShowItem.Rotation = true
    self.uiModelShowItem.ModelID = self.playerId
    self.uiModelShowItem.PlayNeedBall = false
    self.uiModelShowItem._playerModel.layerName = "GUI"
    local modelConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.playerId)
    self.gender = modelConfig.gender
    self:SetInitDress()
end

function UIFashion:DoClose( ... )
    if self.uiAnimator then
        self:AnimClose()
    else
        self:OnClose()
    end
    if self.uiChangeAimator then
        self.uiChangeAimator:SetBool('Close', true)
    end
end

-- 非腾讯版本的
-- function UIFashion:ChangeWardrobe( ... )
--  return function (go)
--      if self.uiChangeAimator and not self.isInitAnimator then
--          self.uiChangeAimator:SetBool('Z0-1', true)
--          self.isInitAnimator = true
--      else
--          self.uiChangeAimator:SetBool('ZoneSwitch', self.isWardrobe)
--      end
--      if self.isWardrobe then
--          self.uiWardrobe.spriteName = 'fashion_wardrobe'
--          if self.tabState then
--              self.tabState.value = true
--          else
--              self.uiFashionStoreTab[1].value = true
--              self.tabState = self.uiFashionStoreTab[1]
--          end
--          self.selecStoreItem = nil
--          self:UpdateFashionStore()()
--      else
--          self.uiWardrobe.spriteName = 'fashion_shop'
--          --娓呴櫎鎵�湁鏃惰鍟嗗簵閫夐」
--          self:ResetInitDress()
--          -- self:ResetInitDress()
--          --------------------------------
--          if self.zoneTabState then
--              self.zoneTabState.value = true
--          else
--              self.uiWardrobeTab[1].value = true
--              self.zoneTabState = self.uiWardrobeTab[1]
--          end
--          self:UpdateWardrobe()()
--      end
--      self.isWardrobe = not self.isWardrobe
--  end
-- end

--腾讯版本的
-- function UIFashion:ChangeWardrobeNew( ... )
--     return function (go)
--         if self.uiChangeAimator and not self.isInitAnimator then
--             self.uiChangeAimator:SetBool('Z0-1', true)
--             self.isInitAnimator = true
--         else
--             self.uiChangeAimator:SetBool('ZoneSwitch', self.isWardrobe)
--         end

--         self.isWardrobe = not self.isWardrobe
--         if not self.isWardrobe then
--             self.uiWardrobe.spriteName = 'fashion_wardrobe'
--             if self.tabState then
--                 self.tabState.value = true
--             else
--                 self.uiFashionStoreTab[1].value = true
--                 self.tabState = self.uiFashionStoreTab[1]
--             end
--             self.selecStoreItem = nil
--             self:UpdateFashionStore()()
--         else
--             self:ClickZoneTab()(self.uiWardrobeTab[1].gameObject)

--             self.uiWardrobe.spriteName = 'fashion_shop'
--             --娓呴櫎鎵�湁鏃惰鍟嗗簵閫夐」
--             self:ResetInitDress()
--             -- self:ResetInitDress()
--             --------------------------------
--             if self.zoneTabState then
--                 self.zoneTabState.value = true
--             else
--                 self.uiWardrobeTab[1].value = true
--                 self.zoneTabState = self.uiWardrobeTab[1]
--             end
--             self:UpdateWardrobe()()
--         end
--         NGUITools.SetActive(self.uiFashionRedDot.gameObject, (UpdateRedDotHandler.UpdateState["UIFashion"].Count > 0 and not self.isWardrobe))
--         NGUITools.SetActive(self.uiBuySelected.gameObject, not self.isWardrobe)
--         local pos = self.uiSubstitute.transform.localPosition
--         if self.isWardrobe then
--             pos.x = pos.x + 100
--         else
--             pos.x = pos.x - 100
--         end
--         self.uiSubstitute.transform.localPosition = pos
--     end
-- end
--新版时装界面
function UIFashion:SwitchShop( ... )
    return function ( go )
        if not self.isWardrobe then
            return
        end
        self.uiShopToggle.value = true
        -- self.uiWardrobeToggle.value = false
        -- 修改为tab页后没有动画了
        -- if self.uiChangeAimator and not self.isInitAnimator then
        --     self.uiChangeAimator:SetBool('Z0-1', true)
        --     self.isInitAnimator = true
        -- else
        --     self.uiChangeAimator:SetBool('ZoneSwitch', self.isWardrobe)
        -- end

        NGUITools.SetActive(self.uiSelectZone.gameObject, true);
        NGUITools.SetActive(self.uiZone.gameObject, false);

        self.isWardrobe = false
        if self.tabState then
            self.tabState.value = true
        else
            self.uiFashionStoreTab[1].value = true
            self.tabState = self.uiFashionStoreTab[1]
        end
        self.selecStoreItem = nil
        self:UpdateFashionStore()()
        NGUITools.SetActive(self.uiFashionRedDot.gameObject, (UpdateRedDotHandler.UpdateState["UIFashion"].Count > 0 and not self.isWardrobe))
        NGUITools.SetActive(self.uiBuySelected.gameObject, not self.isWardrobe)
        local pos = self.uiSubstitute.transform.localPosition
        pos.x = pos.x - 100
        self.uiSubstitute.transform.localPosition = pos
    end
end
function UIFashion:SwitchWardrobe( ... )
    return function (go)
        if self.isWardrobe then
            return
        end
        -- self.uiShopToggle.value = false
        -- self.uiWardrobeToggle.value = true
        -- if self.uiChangeAimator and not self.isInitAnimator then
        --     self.uiChangeAimator:SetBool('Z0-1', true)
        --     self.isInitAnimator = true
        -- else
        --     self.uiChangeAimator:SetBool('ZoneSwitch', self.isWardrobe)
        -- end

        NGUITools.SetActive(self.uiSelectZone.gameObject, false);
        NGUITools.SetActive(self.uiZone.gameObject, true);

        self.isWardrobe = true

        self:ClickZoneTab()(self.uiWardrobeTab[1].gameObject)
        self:ResetInitDress()
        if self.zoneTabState then
            self.zoneTabState.value = true
        else
            self.uiWardrobeTab[1].value = true
            self.zoneTabState = self.uiWardrobeTab[1]
        end
        self:UpdateWardrobe()()
        NGUITools.SetActive(self.uiFashionRedDot.gameObject, (UpdateRedDotHandler.UpdateState["UIFashion"].Count > 0 and not self.isWardrobe))
        NGUITools.SetActive(self.uiBuySelected.gameObject, not self.isWardrobe)
        local pos = self.uiSubstitute.transform.localPosition
        pos.x = pos.x + 100
        self.uiSubstitute.transform.localPosition = pos
    end
end
function UIFashion:RefreshFashionGoods(list)
    -- self.tabState.value = true
    self.uiSelectSVIncLoad.OnCreateItem = function (index, parent)
        if index < 0 or index >= list.Count then return nil end
        local item = list:get_Item(index)
        local fashionShopItem = getLuaComponent(createUI('FashionShopItem', parent))
        fashionShopItem.gameObject.name = string.format("FashionShopItem%05d", index)
        fashionShopItem.isReputation = self.reputationStore
        fashionShopItem:SetData(item._fashionID)
        fashionShopItem.onClick = self:OnTryDress()
        fashionShopItem.isInStore = true
        for i,v in ipairs(self.tryDressList) do
            if v ~= 0 then
                if item._fashionID == v then
                    -- local roleShape = GameSystem.Instance.RoleShapeConfig.configs:get_Item(self.playerId)
                    -- local ids = {roleShape.hair_id, roleShape.upper_id, roleShape.lower_id, roleShape.shoes_id, roleShape.back_id}
                    -- if ids[i] ~= v then
                        fashionShopItem:IsTryDress(true)
                    -- end
                    self.selecStoreItem = fashionShopItem
                    break
                end
            end
        end
        return fashionShopItem.gameObject;
    end

    self.uiSelectSVIncLoad:Refresh(list.Count)
end

function UIFashion:RefreshWardrobeGoods(types)
    local goodsList = {}
    local goodsEnum = MainPlayer.Instance.FashionGoodsList:GetEnumerator()
    while goodsEnum:MoveNext() do
        local goods = goodsEnum.Current.Value
        local goodsCategroy = goods:GetFashionSubCategory()
        local enum = goodsCategroy:GetEnumerator()
        while enum:MoveNext() do
            local categroy = enum.Current
            if types == categroy then
                table.insert(goodsList, goods)
            end
            break
        end
    end

    local goods_count = #goodsList;
    NGUITools.SetActive(self.uiEmptyText.gameObject,not (goods_count > 0))

    self.uiZoneSVIncLoad.OnCreateItem = function (index, parent)
        if index < 0 or index >= goods_count then return nil end
        local goods = goodsList[index + 1]
        local fashionShopItem = getLuaComponent(createUI('FashionShopItem', parent))
        fashionShopItem.gameObject.name = string.format("FashionShopItem%05d", index)
        -- fashionShopItem.uuid = goods:GetUUID()
        -- fashionShopItem:SetWardrobeData(goods:GetID())
        fashionShopItem:SetWardrobeDataNew(goods)
        --暂时注释 fashionShopItem.onClick = self:OnDress()
        fashionShopItem.onClick = self:OnOpenDetail()
        fashionShopItem.isInStore = false
        if self.selecWardrobeItem and self.selecWardrobeItem.uuid == goods:GetUUID() then
            fashionShopItem:ChangeState(true)
            self.selecWardrobeItem = fashionShopItem
        end
        return fashionShopItem.gameObject
    end
    self.uiZoneSVIncLoad:Refresh(goods_count)
end

function UIFashion:ClickBack()
    return function(go)
        -- if self.playerId == MainPlayer.Instance.CaptainID then
            self:ResetInitDress()
        -- end
        self:EnabledButton(false)
        self:DoClose()
    end
end
--------------------store
function UIFashion:ClickTab( ... )
    return function (go)
        if self.selectedTab == go then
            return
        end
        local types = nil
        self.selectedTab = go
        if go == self.uiFashionStoreTab[1].gameObject then
            types = 1
        elseif go == self.uiFashionStoreTab[2].gameObject then
            types = 2
        elseif go == self.uiFashionStoreTab[3].gameObject then
            types = 3
        elseif go == self.uiFashionStoreTab[4].gameObject then
            types = 4
        elseif go == self.uiFashionStoreTab[5].gameObject then
            types = 5
        elseif go == self.uiFashionStoreTab[6].gameObject then
            types = 6
        end
        self.tabState = self.uiFashionStoreTab[types]
        local fashionList = self:GetStoreList(self.tabState)
        self.selecStoreItem = nil
        self:RefreshFashionGoods(fashionList)
    end
end
---------------------wardrobe
function UIFashion:ClickZoneTab( ... )
    return function (go)
        if self.selectedTab == go then
            return
        end
        local types = nil
        self.selectedTab = go
        if go == self.uiWardrobeTab[1].gameObject then
            types = 1
        elseif go == self.uiWardrobeTab[2].gameObject then
            types = 2
        elseif go == self.uiWardrobeTab[3].gameObject then
            types = 3
        elseif go == self.uiWardrobeTab[4].gameObject then
            types = 4
        elseif go == self.uiWardrobeTab[5].gameObject then
            types = 5
        elseif go == self.uiWardrobeTab[6].gameObject then
            types = 0
        end
        if types == 0 then
            self.zoneTabState = self.uiWardrobeTab[6]
        else
            self.zoneTabState = self.uiWardrobeTab[types]
            UpdateRedDotHandler.OperateFashionTip("Delete", types)
            self:RefreshFashionTips()
        end
        self.selecWardrobeItem = nil
        self:RefreshWardrobeGoods(types)
        self.barValue = 0
    end
end
--------------------------leftclick
function UIFashion:ClickUnloadPart( ... )
    return function (go)
        local types = nil
        for i=1,5 do
            if go == self.uiLeftPart[i].gameObject then
                types = i
                break
            end
        end

        if self.uiLeftPart[types].spriteName ~= self.yesDress[types] then return end

        if  self.isWardrobe then
            local uuid = self:GetOwnFashionUUID(self.DressList[types])
            local item = {id = self.DressList[types], isInStore = false, uuid = uuid}
            self:OnDressDown(item)
            if self.selecStoreItem then
                self.selecStoreItem:IsTryDress(false)
            end
            self.selecWardrobeItem = nil
        else
            local initDress = self:FindDressFasionIDList()
            local roleShape = GameSystem.Instance.RoleShapeConfig.configs:get_Item(self.playerId)
            local ids = {roleShape.hair_id, roleShape.upper_id, roleShape.lower_id, roleShape.shoes_id, roleShape.back_id}
            if self.DressList[types] ~= initDress[types] or self.DressList[types] == self.tryDressList[types] then
                if types ~= 5 then
                    if initDress[types] ~= self.tryDressList[types] then
                        local item = {id = initDress[types], isInStore = true}
                        self:OnDressUp(item)
                    end
                else
                    if initDress[5] ~= self.tryDressList[5] then
                        local item = {id = self.DressList[5], isInStore = true, 0, true}
                        self:OnDressDown(item)
                    end
                    self.DressList[5] = 0
                end
                self:ChangeTryList(self.tryDressList[types])
                if self.selecStoreItem then
                    self.selecStoreItem:IsTryDress(false)
                end
                self.selecStoreItem = nil
                self:UpdateFashionStore()()
            end
        end
    end
end
----------------------------------------------

function UIFashion:ClickSubstitute( ... )
    return function (go)
        if self.banTwice then
            return
        end
        self.banTwice = true
        local fashionRole = getLuaComponent(createUI('FashionRole', self.uiBuyPos.transform))
        fashionRole.onChoose = self:OnChoosePlayer()
        fashionRole.onBack = function ( ... )
            self:SetModelActive(true)
            self.banTwice = false
        end
        self:SetModelActive(false)
        --手动移动到最上层
        UIManager.Instance:BringPanelForward(fashionRole.gameObject)
    end
end

function UIFashion:OnChoosePlayer( ... )
    return function (id)
        if self.playerId == id then
            return
        end
        self.banTwice = false
        -- self:ResetInitDress()	--閲嶇疆鑰佺悆鍛�
        -- local modelConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(id)
        -- local isDiffer = (self.gender == modelConfig.gender)
        self.playerId = id
        -- MainPlayer.Instance.CaptainID = id   --璧嬪�鍒锋柊鏃惰
        self.tryDressList = {0,0,0,0,0}
        self:RefreshModel()
        -- if not isDiffer then
            self.selectedTab = nil
            if self.tabState == self.uiFashionStoreTab[1] then
                self:ClickTab()(self.uiFashionStoreTab[1].gameObject)
            elseif self.tabState == self.uiFashionStoreTab[2] then
                self:ClickTab()(self.uiFashionStoreTab[2].gameObject)
            elseif self.tabState == self.uiFashionStoreTab[3] then
                self:ClickTab()(self.uiFashionStoreTab[3].gameObject)
            elseif self.tabState == self.uiFashionStoreTab[4] then
                self:ClickTab()(self.uiFashionStoreTab[4].gameObject)
            elseif self.tabState == self.uiFashionStoreTab[5] then
                self:ClickTab()(self.uiFashionStoreTab[5].gameObject)
            elseif self.tabState == self.uiFashionStoreTab[6] then
                self:ClickTab()(self.uiFashionStoreTab[6].gameObject)
            end
        -- end
        if self.isWardrobe then
            self.tabState.value = true
        else
            if self.zoneTabState then
                self.zoneTabState.value = true
            end
        end
        self:SetModelActive(true)
    end
end

function UIFashion:ClickBuySelected( ... )
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.clothes_fastbuy) then return end

        local isEmpty = true
        for i,v in ipairs(self.tryDressList) do
            if v ~= 0 then
                isEmpty = false
            end
        end
        if isEmpty then
            CommonFunction.ShowPopupMsg(getCommonStr('UI_FASHION_YOU_ARE_NOT_SELECT_FASHION'),nil,nil,nil,nil,nil)
            return
        end
        if self.banTwice then
            return
        end
        self.banTwice = true
        local fashionOBB = getLuaComponent(createUI('Fashion_OBB', self.uiBuyPos.transform))
        UIManager.Instance:BringPanelForward(fashionOBB.gameObject)
        fashionOBB.isReputation = self.reputationStore
        local newList = {}
        local list = {}
        for i,v in ipairs(self.tryDressList) do
            if v ~= 0 then
                if not newList[v] then
                    newList[v] = 1
                    table.insert(list, v)
                end
            end
        end
        -- print('-------------------')
        -- printTable(list)
        -- print('-------------------')
        fashionOBB:SetData(list)
        fashionOBB.onClose = function ( ... )
            self.banTwice = false

            self:Refresh()
        end
        fashionOBB.onBuy = self:UpdateWardrobe(list)
    end
end

function UIFashion:SetModelActive(state)
    NGUITools.SetActive(self.uiModel.transform.gameObject, state)
end

function UIFashion:OnTryDress( ... )
    return function (item)
        if not FunctionSwitchData.CheckSwith(FSID.clothes_wear) then return end

        if item and item.isTryDress then
            self:ConfirmBuyOneTryDress(item)
            return
        end
        -- if self.selecStoreItem == item then
        --  return
        -- end
        local hasDress = false
        local category = self:GetFashionType(item.id)
        if item.id == self.DressList[category[1]] then
            hasDress = true
        end
        --涔嬪墠椤�
        if self.selecStoreItem then
            self.selecStoreItem:ChangeState(false)
            self.selecStoreItem:IsTryDress(false)
            self:OnDressDown(self.selecStoreItem)
        end
        self.selecStoreItem = item
        item:ChangeState(true)
        item:IsTryDress(true)
        if not hasDress then
            self:OnDressUp(item)
        else
            for i,v in ipairs(category) do
                self.tryDressList[v] = self.DressList[v]
            end
            self:UpdateLeftIcons(self.DressList)
        end
    end
end

function UIFashion:OnDress(fashionItem)
    return function (item)
        if not item and fashionItem then
            item = fashionItem
        end
        -- if item.belongId ~= 0 then
            if item.belongId == self.playerId then	--鍗镐笅
                self:OnDressDown(item)
                self.selecWardrobeItem = nil
                return
            end
        -- end
        -- if self.selecWardrobeItem == item then
        --  return
        -- end
        if item.gender ~= self.gender and item.gender ~= 0 then
            CommonFunction.ShowPopupMsg(getCommonStr('UI_FASHION_DRY_DRESSON_FAILED_FOR_GENDER'),nil,nil,nil,nil,nil)
            return
        end
        --涔嬪墠椤�
        if self.selecWardrobeItem then
            self.selecWardrobeItem:ChangeState(false)
            self:OnDressDown(self.selecWardrobeItem)
        end
        self.selecWardrobeItem = item
        item:ChangeState(true)
        self:OnDressUp(item)
    end
end

function UIFashion:OnOpenDetail( ... )
    return function (item)
        local fashionReset = createUI("FashionReset", self.transform)
        local resetLua = getLuaComponent(fashionReset.gameObject)
        resetLua:SetData(item.goods, item.belongId, self.playerId)
        resetLua.onOperate = self:OnDress(item)
        resetLua.onReset = function ()
            item:ResetFashionShopItem()
        end

        UIManager.Instance:BringPanelForward(fashionReset.gameObject)
    end
end

function UIFashion:OnDressUp(item)
    local id = item.id
    local fashionShopConfig
    if not self.reputationStore then
        fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(id)
    else
        fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(id)
    end
    -- local atlas = ResourceLoadManager.Instance:GetAtlas(fashionShopConfig:getAtlas())
    -- local spriteName = fashionShopConfig:getSpriteName()
    local gender = nil
    if fashionShopConfig then
        gender = fashionShopConfig:getGender()
    else
        gender = self.gender
    end
    local roleShape = GameSystem.Instance.RoleShapeConfig.configs:get_Item(self.playerId)
    local ids = {roleShape.hair_id, roleShape.upper_id, roleShape.lower_id, roleShape.shoes_id, roleShape.back_id}

    local category, length = self:GetFashionType(id)

    local isBack = false
    print('gender ----------- ' .. tostring(gender))
    if self.gender == gender or gender == 0 then
        for i,v in ipairs(category) do
            if self.DressList[v] ~= 0 then
                print('dress down DressList')
                print('DressList[v] = ' .. self.DressList[v])
                local isBag = false
                if v == 5 then isBag = true end
                if not item.isInStore then
                    local uuid = self:GetOwnFashionUUID(self.DressList[v])
                    if uuid ~= 0 then
                        self:DressDown(self.DressList[v], item.isInStore, uuid, isBag)
                        self:ChangeDressList(self.DressList[v])
                    end
                else
                    self:DressDown(self.DressList[v], item.isInStore, 0, isBag)
                    self:ChangeDressList(self.DressList[v])
                end
            end
            if v == 1 then
                if item.isInStore then
                    self:ChangeTryList(self.tryDressList[1])
                    self.tryDressList[1] = id
                end
                self.DressList[1] = id
            elseif v == 2 then
                if item.isInStore then
                    self:ChangeTryList(self.tryDressList[2])
                    self.tryDressList[2] = id
                end
                self.DressList[2] = id
            elseif v == 3 then
                if item.isInStore then
                    self:ChangeTryList(self.tryDressList[3])
                    self.tryDressList[3] = id
                end
                self.DressList[3] = id
            elseif v == 4 then
                if item.isInStore then
                    self:ChangeTryList(self.tryDressList[4])
                    self.tryDressList[4] = id
                end
                self.DressList[4] = id
            elseif v == 5 then
                if item.isInStore then
                    self:ChangeTryList(self.tryDressList[5])
                    self.tryDressList[5] = id
                end
                self.DressList[5] = id
                isBack = true
            end
        end
        print('isBack : ' .. tostring(isBack))
        self:DressUp(id, item.isInStore, item.uuid, isBack)

        for i,v in ipairs(self.DressList) do
            if v == 0 and ids[i] then -- and item.isInStore then
                if i ~= 5 then
                    self:DressUp(ids[i], item.isInStore)
                else
                    self:DressUp(ids[i], item.isInStore, 0, isBack)
                end
                self.DressList[i] = ids[i]
            end
        end
        if item.isInStore then
            self:UpdateLeftIcons(self.DressList)
        end

        print('222222222222222222222')
        printTable(self.DressList)
        print('222222222222222222222')
    end
end

function UIFashion:OnDressDown(item)
    if not FunctionSwitchData.CheckSwith(FSID.clothes_unfix) then return end

    local id = item.id
    local fashionShopConfig
    if not self.reputationStore then
        fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetConfig(id)
    else
        fashionShopConfig = GameSystem.Instance.FashionShopConfig:GetReputationConfig(id)
    end
    -- local atlas = ResourceLoadManager.Instance:GetAtlas(fashionShopConfig:getAtlas())
    -- local spriteName = fashionShopConfig:getSpriteName()
    local gender = nil
    if fashionShopConfig then
        gender = fashionShopConfig:getGender()
    else
        gender = self.gender
    end

    local category, length = self:GetFashionType(id)
    printTable(category)

    print('gender ----------- ' .. tostring(gender))
    if self.gender == gender or gender == 0 then
        local isBack = false
        for i,v in ipairs(category) do
            if v == 5 then
                isBack = true
                break
            end
        end
        --鍗镐笅
        self:DressDown(id, item.isInStore, item.uuid, isBack)
    end
end

function UIFashion:DressUp(fashionId, isInStore, uuid, isBack)
    print('upppppppppppppppp ---- ' .. fashionId)
    print('isInStore -------- ' .. tostring(isInStore))
    print('uuid ---------- ' .. tostring(uuid))
    -- self.uiModelShowItem._playerModel:_DressUp('',fashionId)
    if not isInStore and uuid and uuid ~= 0 then
        -- local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, uuid)
        local infos = {}
        local info = {type = enumToInt(FashionOperationType.FOT_EQUIP), uuid = uuid}
        printTable(info)
        table.insert(infos, info)
        local req =
        {
            info = infos,
            role_id = self.playerId,
        }
        local buf = protobuf.encode('fogs.proto.msg.FashionOperation', req)
        LuaHelper.SendPlatMsgFromLua(MsgID.FashionOperationID, buf)
        CommonFunction.ShowWait()
    else
        if not isBack then
            self.uiModelShowItem._playerModel:_DressUp('',fashionId)
        else
            self.uiModelShowItem._playerModel:_FittingUp(fashionId)
        end
    end
end

-- isInstore--true, isBack--false
function UIFashion:DressDown(fashionId, isInStore, uuid, isBack)
    print('downnnnnnnnnnnnnn ---- ' .. fashionId)
    -- self.uiModelShowItem._playerModel:_DressDown('',fashionId)
    if not isInStore then
        -- local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, uuid)
        local infos = {}
        local info = {type = enumToInt(FashionOperationType.FOT_UNEQUIP), uuid = uuid}
        table.insert(infos, info)
        local req =
        {
            info = infos,
            role_id = self.playerId,
        }
        local buf = protobuf.encode('fogs.proto.msg.FashionOperation', req)
        LuaHelper.SendPlatMsgFromLua(MsgID.FashionOperationID, buf)
        CommonFunction.ShowWait()
    else
        if not isBack then
            self.uiModelShowItem._playerModel:_DressDown('',fashionId)
        else
            self.uiModelShowItem._playerModel:_FittingDown(fashionId)
        end
    end
end

function UIFashion:FashionOperationRespHandler( ... )
    return function (message)
    CommonFunction.StopWait()
        local resp, err = protobuf.decode('fogs.proto.msg.FashionOperationResp', message)

        print('resp pppppppppppppppppp')
        local info = resp.resp_info
        local role = MainPlayer.Instance:GetRole2(self.playerId)
        for i,v in ipairs(info) do
            print('v.type : ' .. tostring(v.type))
            print('v.result : ' .. tostring(v.result))
            print('v.uuid : ' .. tostring(v.uuid))
            if v.result == 0 then
                local fashionEnum = role.fashion_slot_info:GetEnumerator()
                while fashionEnum:MoveNext() do
                    local partId = fashionEnum.Current.id
                    -- local fashionuuId = fashionEnum.Current.fashion_uuid
                    local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, v.uuid)
                    -- local ret, goods = MainPlayer.Instance.FashionGoodsList:TryGetValue(v.uuid)
                    local categorys = goods:GetFashionSubCategory()
                    if v.type == enumToInt(FashionOperationType.FOT_UNEQUIP) then
                        local enum = categorys:GetEnumerator()
                        while enum:MoveNext() do
                            local id = enum.Current
                            if tonumber(id) == partId then
                                -- print('xiexia v.uuid --------------- ' .. v.uuid .. ' goodsId ------------ ' .. goods:GetID())
                                fashionEnum.Current.fashion_uuid = 0
                                fashionEnum.Current.goods_id = 0
                            end
                        end
                    elseif v.type == enumToInt(FashionOperationType.FOT_EQUIP) then
                        local enum = categorys:GetEnumerator()
                        while enum:MoveNext() do
                            local id = enum.Current
                            if tonumber(id) == partId then
                                -- print('zhuangbei v.uuid --------------' .. v.uuid .. ' goodsId ------------ ' .. goods:GetID())
                                fashionEnum.Current.fashion_uuid = v.uuid
                                fashionEnum.Current.goods_id = goods:GetID()
                                self:ExchangeDress(v.uuid)
                            end
                        end
                    end
                end
            end
        end

        self.barValue = self.uiZoneScrollBar.value
        --鍒锋柊琛ｆ┍
        self:UpdateWardrobe()()
        self:RefreshModel()
    end
end

--Try Dress Reset
function UIFashion:ResetInitDress()
    local isEmpty = true
    for i,v in ipairs(self.tryDressList) do
        if v ~= 0 then
            isEmpty = false
        end
    end
    if not isEmpty then
        print('reset')
        self.uiModelShowItem.IsFashion = true
        self.uiModelShowItem.Rotation = true
        self.uiModelShowItem.ModelID = self.playerId
        self.uiModelShowItem.PlayNeedBall = false
        self.uiModelShowItem._playerModel.layerName = "GUI"
    end
    self.tryDressList = {0,0,0,0,0}     --璇曠┛鍒楄〃娓呯┖
    self:SetInitDress()
end
--Get Own Dress
function UIFashion:FindDressFasionIDList()
    --鐞冨憳鍒濆閮ㄤ綅
    local roleShape = GameSystem.Instance.RoleShapeConfig.configs:get_Item(self.playerId)
    local ids = {roleShape.hair_id, roleShape.upper_id, roleShape.lower_id, roleShape.shoes_id, roleShape.back_id}

    local role = MainPlayer.Instance:GetRole2(self.playerId)
    local fashionEnum = role.fashion_slot_info:GetEnumerator()
    while fashionEnum:MoveNext() do
        local partId = fashionEnum.Current.id
        local fashionuuId = fashionEnum.Current.fashion_uuid
        if fashionuuId ~= 0 and partId ~= 0 then
            local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, fashionuuId)
            ids[partId] = goods:GetID()
        end
    end
    return	ids
end

function UIFashion:GetFashionType(id)
    local goodsInfo = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
    local length = string.len(goodsInfo.sub_category)
    local c = {}
    if length == 1 then
        table.insert(c, tonumber(goodsInfo.sub_category))
    else
        local g = string.split(goodsInfo.sub_category, '&')
        for i,v in ipairs(g) do
            if tonumber(v) ~= 0 then
                table.insert(c, tonumber(v))
            end
        end
    end
    return c, length
end

function UIFashion:GetOwnFashionUUID(id)
    local role = MainPlayer.Instance:GetRole2(self.playerId)
    local fashionEnum = role.fashion_slot_info:GetEnumerator()
    while fashionEnum:MoveNext() do
        local partId = fashionEnum.Current.id
        local fashionuuId = fashionEnum.Current.fashion_uuid
        if fashionuuId ~= 0 then
            local goods = MainPlayer.Instance:GetGoods(GoodsCategory.GC_FASHION, fashionuuId)
            -- local ret, goods = MainPlayer.Instance.FashionGoodsList:TryGetValue(fashionuuId)
            if id == goods:GetID() then
                return fashionuuId
            end
        end
    end
    return 0
end

function UIFashion:UpdateWardrobe(list)
    return function ( ... )
        self.banTwice = false
        if self.zoneTabState then
            --鍒锋柊琛ｆ┍
            if self.zoneTabState == self.uiWardrobeTab[1] then
                self:RefreshWardrobeGoods(1)
            elseif self.zoneTabState == self.uiWardrobeTab[2] then
                self:RefreshWardrobeGoods(2)
            elseif self.zoneTabState == self.uiWardrobeTab[3] then
                self:RefreshWardrobeGoods(3)
            elseif self.zoneTabState == self.uiWardrobeTab[4] then
                self:RefreshWardrobeGoods(4)
            elseif self.zoneTabState == self.uiWardrobeTab[5] then
                self:RefreshWardrobeGoods(5)
            elseif self.zoneTabState == self.uiWardrobeTab[6] then
                self:RefreshWardrobeGoods(0)
            end
        end
        if list then
            self:DressAfterBuy(list)
        end
        self.uiZoneScrollBar.value = self.barValue
    end
end

function UIFashion:DressAfterBuy(list)
    --重置
    self:ResetInitDress()

    for i,v in ipairs(list) do
        print('id =' .. tonumber(v))
        local goodsList = MainPlayer.Instance:GetGoodsList(GoodsCategory.GC_FASHION, tonumber(v))
        print(self.uiName,"----list count:",goodsList.Count)
        if goodsList.Count - 1 >= 0 then
            local goods = goodsList:get_Item(goodsList.Count - 1)
            local item = {id = tonumber(v), isInStore = false, uuid = goods:GetUUID()}
            self:OnDressUp(item)
        end
    end
end

function UIFashion:UpdateFashionStore()
    return function ( ... )
        if not self.tabState then return end
        --鍒锋柊琛ｆ┍
        local fightList = self:GetStoreList(self.tabState)
        self:RefreshFashionGoods(fightList)
        self:UpdateLeftIcons(self.DressList)
    end
end

function UIFashion:GetStoreList(tab)
    if tab == self.uiFashionStoreTab[1] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cHeadSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cHeadSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationHeadSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationHeadSort_w
        end
    elseif tab == self.uiFashionStoreTab[2] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cClothesSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cClothesSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationClothesSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationClothesSort_w
        end
    elseif tab == self.uiFashionStoreTab[3] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cTrouserseSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cTrouserseSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationTrouserseSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationTrouserseSort_w
        end
    elseif tab == self.uiFashionStoreTab[4] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cShoesSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cShoesSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationShoesSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationShoesSort_w
        end
    elseif tab == self.uiFashionStoreTab[5] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cBackSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cBackSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationBackSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationBackSort_w
        end
    elseif tab == self.uiFashionStoreTab[6] then
        if self.gender == 1 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cSuiteSort
        elseif self.gender == 2 and not self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.cSuiteSort_w
        elseif self.gender == 1 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationSuiteSort
        elseif self.gender == 2 and self.reputationStore then
            return GameSystem.Instance.FashionShopConfig.reputationSuiteSort_w
        end
    end
end

function UIFashion:ChangeTryList(id)
    local newList = {}
    for i,v in ipairs(self.tryDressList) do
        if tonumber(v) == id then
            table.insert(newList, 0)
        else
            table.insert(newList, tonumber(v))
        end
    end
    self.tryDressList = newList
end

function UIFashion:ChangeDressList(id)
    local newList = {}
    for i,v in ipairs(self.DressList) do
        if tonumber(v) == id then
            table.insert(newList, 0)
        else
            table.insert(newList, tonumber(v))
        end
    end
    self.DressList = newList
end

function UIFashion:UpdateLeftIcons(list)
    local dress = nil
    local roleShape = GameSystem.Instance.RoleShapeConfig.configs:get_Item(self.playerId)
    local ids = {roleShape.hair_id, roleShape.upper_id, roleShape.lower_id, roleShape.shoes_id, roleShape.back_id}
    -- local ids = self:FindDressFasionIDList()
    for i,v in ipairs(list) do
        if v == 0 then
            dress = self.noDress
        elseif v ~= ids[i] or v == self.tryDressList[i] then
            dress = self.yesDress
        else
            dress = self.noDress
        end
        self.uiLeftPart[i].spriteName = dress[i]
    end
end

function UIFashion:ConfirmBuyOneTryDress(item)
    if item.isTryDress then
        if self.banTwice then
            return
        end
        self.banTwice = true
        self.uiFashionBuyOne = createUI('FashionBuy', self.uiBuyPos.transform)
        UIManager.Instance:BringPanelForward(self.uiFashionBuyOne.gameObject)
        local fashionName = self.uiFashionBuyOne.transform:FindChild('Window/ItemName'):GetComponent('UILabel')
        local goodsIcon = getLuaComponent(createUI('GoodsIcon', self.uiFashionBuyOne.transform:FindChild('Window/ItemIcon')))
        local goodsIconConsume = getLuaComponent(createUI('GoodsIconConsume', self.uiFashionBuyOne.transform:FindChild('Window/Costs/GoodsIconConsume')))
        local btnOk = self.uiFashionBuyOne.transform:FindChild('Window/ButtonOK')
        local btnClose = createUI('ButtonClose', self.uiFashionBuyOne.transform:FindChild('Window/ButtonClose'))

        fashionName.text = item.uiItemName.text
        goodsIcon.goodsID = item.id
        goodsIcon.hideLevel = true
        goodsIcon.hideNeed = true
        goodsIconConsume.isAdd = false
        print('----------------------------item.realCostType:' .. item.realCostType)
        if  item.realCostType ~= 1 then
            print('-----------------------------------realCostType = 0---SetData = 1')
            goodsIconConsume:SetData(1, item.cost, false)
        else
            print('-----------------------------------realCostType = 1---SetData = 10')
            goodsIconConsume:SetData(10, item.cost, false)
        end
        addOnClick(btnOk.gameObject, self:OnBuyOne(item))
        addOnClick(btnClose.gameObject, self:OnCloseFashionBuyOne())
    end
end

function UIFashion:OnCloseFashionBuyOne( ... )
    return function (go)
        self.banTwice = false
        NGUITools.Destroy(self.uiFashionBuyOne.gameObject)
    end
end

function UIFashion:OnBuyOne(item)
    return function (go)
        if not FunctionSwitchData.CheckSwith(FSID.clothes_ok) then return end

        if item.realCostType ~= 1 then
            local ownDiamond = MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy
            if item.cost > ownDiamond then
                -- CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_DIAMOND'),nil,nil,nil,nil,nil)
                print(self.uiName,"------- can not buy")
                if self.banTwice1 == true then
                    return
                end
                self.banTwice1 = true
                self:ShowBuyTip("BUY_DIAMOND")
                return
            end
        else
            local ownReputation = MainPlayer.Instance.Reputation
            if item.cost > ownReputation then
                CommonFunction.ShowPopupMsg(getCommonStr('NOT_ENOUGH_REPUTATION'),nil,nil,nil,nil,nil)
                return
            end
        end

        local pos = self:FindFashionPos(item.id)
        local storeType = nil
        if self.reputationStore then
            storeType = 'ST_REPUTATION'
        else
            storeType = 'ST_FASHION'
        end
        local buyStoreGoods = {
            store_id = storeType,	--fashion store
            info = {	},
        }

        table.insert(buyStoreGoods.info, {pos = pos,})
        local msg = protobuf.encode("fogs.proto.msg.BuyStoreGoods", buyStoreGoods)
        LuaHelper.SendPlatMsgFromLua(MsgID.BuyStoreGoodsID, msg)
        CommonFunction.ShowWait()
        --娉ㄥ唽璐拱鍟嗗搧鐨勫洖澶嶅鐞嗘秷鎭�
        LuaHelper.RegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self:BuyStoreGoodsResp(item.id), self.uiName)
    end
end

function UIFashion:FindFashionPos(id)
    if not self.reputationStore then
        local fashionItem = GameSystem.Instance.FashionShopConfig.configs:get_Item(id)
        local index = GameSystem.Instance.FashionShopConfig.configsSort:IndexOf(fashionItem)
        return index + 1
    else
        local fashionItem = GameSystem.Instance.FashionShopConfig.reputationConfigs:get_Item(id)
        local index = GameSystem.Instance.FashionShopConfig.reputationConfigsSort:IndexOf(fashionItem)
        return index + 1
    end
end

function UIFashion:BuyStoreGoodsResp(id)
    --瑙ｆ瀽pb
    return function(message)
        LuaHelper.UnRegisterPlatMsgHandler(MsgID.BuyStoreGoodsRespID, self.uiName)
        CommonFunction.StopWait()
        local resp, err = protobuf.decode('fogs.proto.msg.BuyStoreGoodsResp', message)
        if resp == nil then
            Debugger.LogError('------BuyStoreGoodsResp error: ', err)
            return
        end
        Debugger.Log('---2---------resp: {0}', resp.store_id)
        if resp.result ~= 0 then
            Debugger.Log('-----------1: {0}', resp.result)
            CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil,nil)
            playSound("UI/UI-wrong")
            return
        end
        self.banTwice = false

        local list = {}
        table.insert(list, id)
        self:DressAfterBuy(list)

        NGUITools.Destroy(self.uiFashionBuyOne.gameObject)

        CommonFunction.ShowPopupMsg(getCommonStr('BUY_FASHION_SUCCESS'),nil,nil,nil,nil,nil)
    end
end

function UIFashion:ExchangeDress(uuid)
    local enum = MainPlayer.Instance.PlayerList:GetEnumerator()
    while enum:MoveNext() do
        local role = enum.Current.m_roleInfo
        if role.id ~= self.playerId then
            local fashionEnum = role.fashion_slot_info:GetEnumerator()
            local own = false
            while fashionEnum:MoveNext() do
                local fuuid = fashionEnum.Current.fashion_uuid
                if fuuid ~= 0 and fuuid == uuid then
                    fashionEnum.Current.fashion_uuid = 0
                    fashionEnum.Current.goods_id = 0
                    own = true
                end
            end
            if own then return end
        end
    end
end

function UIFashion:SetTitle(str)
    self.uiTitle:SetText(getCommonStr(str))
end

function UIFashion:EnabledButton(enabled)
    self.uiBuySelected.transform:GetComponent('BoxCollider').enabled = enabled
    self.uiSubstitute.transform:GetComponent('BoxCollider').enabled = enabled
end

function UIFashion:ShowBuyTip(type)
    local str
    if type == "BUY_GOLD" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("GOLD"))
    elseif type == "BUY_DIAMOND" then
        str = getCommonStr("YOUR_DIMAND_LACK_AND_SWITCH_DISABLE")
    elseif type == "BUY_HP" then
        str = string.format(getCommonStr("STR_BUY_THE_COST"),getCommonStr("HP"))
    end
    if type == "BUY_DIAMOND" then
        self.msg = CommonFunction.ShowTip(str, nil)
    else
        self.msg = CommonFunction.ShowPopupMsg(str, nil,
                                            LuaHelper.VoidDelegate(self:ShowBuyUI(type)),
                                            LuaHelper.VoidDelegate(self:FramClickClose()),
                                            getCommonStr("BUTTON_CONFIRM"),
                                            getCommonStr("BUTTON_CANCEL"))
    end
end

function UIFashion:ShowBuyUI(type)
    return function()
        self.banTwice1 = false
        if type == "BUY_DIAMOND" then
            if self.uiFashionBuyOne ~= nil then
                NGUITools.Destroy(self.uiFashionBuyOne.gameObject)
                self.uiFashionBuyOne = nil
            end
            self:ClickBack()()
            TopPanelManager:ShowPanel("VIPPopup", nil, {isToCharge=true})
            return
        end
        local go = getLuaComponent(createUI("UIPlayerBuyDiamondGoldHP"))
        go.BuyType = type
    end
end

function UIFashion:FramClickClose()
    return function()
        NGUITools.Destroy(self.msg.gameObject)
        self.banTwice1 = false
    end
end

function UIFashion:RefreshFashionTips( ... )
    local fashionTipList = UpdateRedDotHandler.UpdateState["UIFashion"]
    --print('fashionTipList = ', fashionTipList == nil)
    --print('fashionTipList.Count = ', fashionTipList.Count > 0)
    for i=1,6 do
        if fashionTipList:Contains(i) then
            NGUITools.SetActive(self.uiWardrobeTabTips[i].gameObject, true)
        else
            NGUITools.SetActive(self.uiWardrobeTabTips[i].gameObject, false)
        end
    end
end

return UIFashion

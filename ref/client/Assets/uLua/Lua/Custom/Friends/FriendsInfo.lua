--好友玩家信息界面
FriendsInfo =  {
    uiName	= "FriendsInfo",

    --data
    match_data,         --//比赛信息
    curr_match_data,    --//当前显示的比赛信息
    role_data,          --//球员数据
    onCloseEvent,
    headIconScript,

    --ui
    uiLeft,
    uiRight,

    --base info
    uiHeadIcon,
    uiPlayerName,
    uiLevel,
    uiVIP,
    uiID,
    uiLabHold,
    uiLabGuild,
    uiLabCorps,

    --match info
    uiRegularMatchGO,
    uiRankingMatchGO,
    uiLadderMatchGO,
    uiGridMatch,

    --hot role
    uiGridHotRole,

    -- ladder ,
    uiLadderIcon,
    uiLadderLabel,

    uiBtnClose,

    startPos,
    --好友信息状态，如果是已经设置就不再请求
    friendQueryed = false,
    friendInfoGotCallback = nil,--好友信息获取后可能存在的操作
}

function FriendsInfo:Awake()
    self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/Top/ButtonClose'))

    self.match_data = { }
    self.regular_data = { }

    self.uiLeft = {}
    self.uiLeft[1] = getComponentInChild(self.transform, "Window/Left/TogBase", 'UIToggle')
    self.uiLeft[2] = getComponentInChild(self.transform, "Window/Left/TogMatch", 'UIToggle')
    self.uiLeft[3] = getComponentInChild(self.transform, "Window/Left/TogHotRole", 'UIToggle')

    self.uiRight = {}
    self.uiRight[1] = getChildGameObject(self.transform, "Window/Right/BaseInfo")
    self.uiRight[2] = getChildGameObject(self.transform, "Window/Right/MatchInfo")
    self.uiRight[3] = getChildGameObject(self.transform, "Window/Right/HotRoleInfo")

    --baseinfo
    self.uiHeadIcon = getChildGameObject(self.uiRight[1].transform, "Icon")
    self.uiPlayerName = getComponentInChild(self.uiRight[1].transform, "Name", "UILabel")
    self.uiLevel = getComponentInChild(self.uiRight[1].transform, "Level/Num", "UILabel")
    self.uiVIP = getComponentInChild(self.uiRight[1].transform, "VIP/Num", "UILabel")
    self.uiID = getComponentInChild(self.uiRight[1].transform, "ID/Num", "UILabel")
    self.uiLabHold = getComponentInChild(self.uiRight[1].transform, "Hold/Bg/Text", "UILabel")
    self.uiLabGuild = getComponentInChild(self.uiRight[1].transform, "Guild/Bg/Text", "UILabel")
    self.uiLabCorps = getComponentInChild(self.uiRight[1].transform, "Corps/Bg/Text", "UILabel")
    self.uiLadderIcon = getComponentInChild(self.uiRight[1].transform, "tianti", "UISprite")
    self.uiLadderLabel = getComponentInChild(self.uiRight[1].transform, "tianti/Num", "UILabel")

    self.uiQualifyingIcon = getComponentInChild(self.transform, "Window/Right/BaseInfo/paiwei", "UISprite")
    self.uiQualifyingName = getComponentInChild(self.uiQualifyingIcon.transform, "Num", "UILabel")

    --match
    local modellogGrid = getComponentInChild(self.uiRight[2].transform, "modellogGrid", "UIGrid")
    self.uiRegularMatchGO = createUI("FriendsModelLog", modellogGrid.transform)
    self.uiRegularMatchGO.name = "uiRegularMatchGO"
    self.uiRankingMatchGO = createUI("FriendsModelLog", modellogGrid.transform)
    self.uiRankingMatchGO.name = "uiRankingMatchGO"
    self.uiLadderMatchGO = createUI("FriendsModelLog", modellogGrid.transform)
    self.uiLadderMatchGO.name = "uiLadderMatchGO"
    NGUITools.SetActive(self.uiRegularMatchGO, true)
    NGUITools.SetActive(self.uiRankingMatchGO, true)
    NGUITools.SetActive(self.uiLadderMatchGO, true)
    modellogGrid:Reposition()

    self.uiGridMatch = getComponentInChild(self.uiRight[2].transform, "Wear/ScrollView/Grid", "UIGrid")
    for i=1, 10 do
        local fightLog = createUI("FriendsFightLog", self.uiGridMatch.transform)
        NGUITools.SetActive(fightLog, true)
    end
    self.uiGridMatch:Reposition()

    --hotrole
    self.uiHotRoleScrollview = getComponentInChild(self.uiRight[3].transform, "Wear/ScrollView/", "UIScrollView")
    self.uiGridHotRole = getComponentInChild(self.uiRight[3].transform, "Wear/ScrollView/SpringFrame/WrapContent", "UIWrapContent")
    for i=1, 5 do
        local playerLog = createUI("FriendsPlayerLog", self.uiGridHotRole.transform)
        NGUITools.SetActive(playerLog, true)
    end

    startPos = self.uiHotRoleScrollview.transform.localPosition
    NGUITools.SetActive(self.gameObject,false)

end
function FriendsInfo:Query(id,plat_id)
    -- body
    if not id then 
        error('error!accountId must be set after FriendsInfo was created,usage:FriendsInfo:Query(acc_id,plat_id)')
        return
    end
    print('playerid '..id)
    Friends.ShowAccountInfo(id,self,plat_id)
end
function FriendsInfo:Start()
    local closeBtn = getLuaComponent(self.uiBtnClose)
    closeBtn.onClick = self:OnCloseClick()

    for i=1, 3 do
        EventDelegate.Add(self.uiLeft[i].onChange, LuaHelper.Callback(self:onToggleChange()))
    end

    local max = table.getn(self.role_data)
    if max < 5 then
        self.uiHotRoleScrollview.onDragFinished = self:OnDragFinished()
    end

    self.uiGridHotRole.onInitializeItem = self:OnUpdateHotRoleItem()

    local regularTog = self.uiRegularMatchGO:GetComponent("UIToggle")
    local rankTog = self.uiRankingMatchGO:GetComponent("UIToggle")
    local ladderTog = self.uiLadderMatchGO:GetComponent("UIToggle")
    regularTog.value = true
    EventDelegate.Add(regularTog.onChange, LuaHelper.Callback(self:onMatchTypeChange()))
    EventDelegate.Add(rankTog.onChange, LuaHelper.Callback(self:onMatchTypeChange()))
    EventDelegate.Add(ladderTog.onChange, LuaHelper.Callback(self:onMatchTypeChange()))

end

function FriendsInfo:Update()

end

function FriendsInfo:FixedUpdate()

end

function FriendsInfo:OnDestroy()

end

function FriendsInfo:OnClose()
    if self.onCloseEvent then
        self.onCloseEvent()
    end

    NGUITools.Destroy(self.gameObject)
end

-------------------------------------------

function FriendsInfo:DoClose()
    -- if self.uiAnimator then
    --     self:AnimClose()
    -- else
        self:OnClose()
    -- end
end

--关闭
function FriendsInfo:OnCloseClick()
    return function(go)
        self:DoClose()
    end
end

--左侧功能列表菜单变化
function FriendsInfo:onToggleChange()
    return function()
        if UIToggle.current.value then
            for i=1, 3 do
                if self.uiLeft[i].gameObject.name == UIToggle.current.name then
                    NGUITools.SetActive(self.uiRight[i], true)
                else
                    NGUITools.SetActive(self.uiRight[i], false)
                end
            end
        end
    end
end

--对战资料，比赛类型选项改变
function FriendsInfo:onMatchTypeChange()
    return function()
        if UIToggle.current.value then
            if UIToggle.current.name == self.uiRegularMatchGO.name then
                self:RefreshMatchList(self.match_data['regular'])
            elseif UIToggle.current.name == self.uiRankingMatchGO.name then
                self:RefreshMatchList(self.match_data['ranking'])
            elseif UIToggle.current.name == self.uiLadderMatchGO.name then
                self:RefreshMatchList(self.match_data['ladder'])
            end
        end
    end
end

function FriendsInfo:setData(resp) --QueryFriendInfoResp

    NGUITools.SetActive(self.gameObject,true)
    if self.friendInfoGotCallback then 
        self.friendInfoGotCallback()
        self.friendInfoGotCallback = nil
    end
    UIManager.Instance:BringPanelForward(self.gameObject)
    --基本资料
    if not self.headIconScript then
        self.headIconScript = getLuaComponent(createUI("CareerRoleIcon", self.uiHeadIcon.transform))
        self.headIconScript.id = tonumber(resp.icon)
        self.headIconScript.showPosition = false
    else
        self.headIconScript.id = tonumber(resp.icon)
        self.headIconScript:Refresh()
    end

    self.uiPlayerName.text = resp.name
    self.uiLevel.text = resp.level
    self.uiVIP.text = resp.vip_level
    self.uiID.text = resp.acc_id
    self.uiLabHold.text = string.format(tostring(getCommonStr("STR_FRIENDS_HOLD")), resp.role_count, resp.fashion_count)
    if resp.guild_name == "" then
        self.uiLabGuild.text = getCommonStr("STR_FRIENDS_NOT_JOIN")
    else
        self.uiLabGuild.text = resp.guild_name
    end
    if resp.team_name == "" then
        self.uiLabCorps.text = getCommonStr("STR_FRIENDS_NOT_JOIN2")
    else
        self.uiLabCorps.text = resp.team_name
    end

    local ladderLv = GameSystem.Instance.ladderConfig:GetLevelByScore(resp.pvp_ladder_score)
    self.uiLadderIcon.spriteName = ladderLv.iconSmall
    self.uiLadderLabel.text = ladderLv.name

    local grade = GameSystem.Instance.qualifyingNewConfig:GetGrade(resp.qualifying_new.score)

    self.uiQualifyingIcon.spriteName = grade.icon_small
    self.uiQualifyingName.text = grade.title

    --对战资料
    local reg_script = getLuaComponent(self.uiRegularMatchGO)
    reg_script:setRegularData(resp.regular)
    local rank_script = getLuaComponent(self.uiRankingMatchGO)
    rank_script:setRankingData(resp.qualifying_new)
    local ladder_script = getLuaComponent(self.uiLadderMatchGO)
    ladder_script:setLadderData(resp.pvp_ladder)

    self.match_data['regular'] = self:MakeRegularTable(resp.regular, 'regular')
    self.match_data['ranking'] = self:MakeRegularTable(resp.qualifying_new, 'ranking')
    self.match_data['ladder'] = self:MakeRegularTable(resp.pvp_ladder, 'ladder')

    self:RefreshMatchList(self.match_data['regular'])

    --常用球员
    self.role_data = resp.role_data
    self:RefreshHotRoleList()
end

-- function FriendsInfo.comps(a,b)
--     return a.num < b.num
-- end

function FriendsInfo:MakeRegularTable(match, match_type)
    local datas = {}

    if match_type == 'ladder' then
        table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_MVP_TIEMS"),          ['num'] = match.mvp_times           ,['icon'] = 'tencent_info_02'})         --MVP次数
    end

    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_WIN_TIMES"),          ['num'] = match.win_times           ,['icon'] = 'tencent_info_03'})         --胜利次数
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_WIN_MAX_STREAK"),     ['num'] = match.max_winning_streak  ,['icon'] = 'tencent_info_06'})         --最大连胜次数
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_WIN_STREAK"),         ['num'] = match.winning_streak      ,['icon'] = 'tencent_info_06'})         --连胜次数
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_KILL_GOAL_TIMES"),    ['num'] = match.kill_goal_times     ,['icon'] = 'tencent_info_09'})         --绝杀次数
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_SCORE_KING"),         ['num'] = match.score_king          ,['icon'] = 'tencent_info_05'})         --得分王
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_REBOUND_KING"),       ['num'] = match.rebound_king        ,['icon'] = 'tencent_info_04'})         --篮板王
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_BLOCK_KING"),         ['num'] = match.block_king          ,['icon'] = 'tencent_info_08'})         --盖帽王
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_ASSIST_KING"),        ['num'] = match.assist_king         ,['icon'] = 'tencent_info_01'})         --助攻王
    table.insert(datas, { ['str'] = getCommonStr("STR_FRIENDS_STEAL_KING"),         ['num'] = match.steal_king          ,['icon'] = 'tencent_info_07'})         --抢断王

    --table.sort(datas,FriendsInfo.comps);

    return datas
end

--刷新对战资料
function FriendsInfo:RefreshMatchList(datas)
    self.curr_match_data = datas
    local max = table.getn(datas)

    local itemscount = self.uiGridMatch.transform.childCount
    for i=0, itemscount-1 do
        local item = self.uiGridMatch.transform:GetChild(i)
        if i >= max then
            NGUITools.SetActive(item.gameObject, false)
        else
            NGUITools.SetActive(item.gameObject, true)
            local script = getLuaComponent(item.gameObject)
            script:setData(self.curr_match_data[i+1])
        end
    end
end

--刷新常用球员列表
function FriendsInfo:RefreshHotRoleList()
    local max = table.getn(self.role_data)
    self.uiGridHotRole.minIndex = 0
    self.uiGridHotRole.maxIndex = max - 1

    local itemscount = self.uiGridHotRole.transform.childCount
    local updateFunc = self:OnUpdateHotRoleItem()
    for i=0, itemscount-1 do
        local item = self.uiGridHotRole.transform:GetChild(i)
        item.name = tostring(i)
        updateFunc(item.gameObject, i, i)
    end
end

function FriendsInfo:OnStart( ... )
    -- body
end

function FriendsInfo:OnDragFinished()
    return function()
        if self.uiHotRoleScrollview.transform.localPosition ~= startpos then
            SpringPanel.Begin(self.uiHotRoleScrollview.gameObject, startPos, 1).strength = 10
        end
    end
end

function FriendsInfo:OnUpdateHotRoleItem()
    return function(obj, index, realIndex)
        local i = math.abs(realIndex) + 1

        if self.role_data == nil then
            do return end
        end

        local max = table.getn(self.role_data)
        if i > max then
            NGUITools.SetActive(obj, false)
            do return end
        end

        NGUITools.SetActive(obj, true)

        local script = getLuaComponent(obj)
        script:setData(self.role_data[i])
    end
end

return FriendsInfo

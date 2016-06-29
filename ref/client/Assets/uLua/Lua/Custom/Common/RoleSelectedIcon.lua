------------------------------------------------------------------------
-- class name    : RoleSelectedIcon
-- create time   : 2016-05-16 15:11:43
-- author        : Jackwu
-- description   : 用于球员选择界面，右侧己选球员显示icon
------------------------------------------------------------------------
RoleSelectedIcon = {
    uiName = "RoleSelectedIcon",

--=============================--
--==     公有变量分隔线      ==--
--=============================--
    ClickCallback = nil,--点击回调方法
    Index = nil,--index
    ClickAble = true,--是否可以点击
--=============================--
--==     私有变量分隔线      ==--
--=============================--
    COLOR_DEFAULT = Color.New(201/255,173/255,133/255),
    COLOR_SELECT = Color.New(0.24,0.87,0.92,1),
    _roleId = nil,--角色id
    _roleIcon = nil,--球员角色icon
    _pressTime = 0,--按下的时间
    _isPress = false,--按下的时间
    _pressEnabled = false,--是否支持长按功能
    _pressCallback = nil,--按下回调
    _skillsInfo = nil,--角色技能信息
    _uiWidget = nil,--widget
--=============================--
--==        UI变量           ==--
--=============================--
    _roleIconNode = nil,--球员角色icon加载节点
    _bgSprite = nil,--背景
    _isClicked = false, --是否被点击
}

function RoleSelectedIcon:Awake( ... )
    self:UIParser()
    self:AddEvent()
end

function RoleSelectedIcon:Start( ... )

end

function RoleSelectedIcon:Update( ... )

end
--[[
function RoleSelectedIcon:FixedUpdate( ... )
    if not self._pressEnabled then
        return
    end
    if (os.time() - self._pressTime) > 0.1 and self._isPress == true then
        print("PressDown")
        self._isPress = false
        if self._pressCallback ~= nil and self._roleId ~= nil then
            self._pressCallback(true,self._roleId,self.Index,self._skillsInfo)
        end
    end
end
--]]

function RoleSelectedIcon:OnDestroy( ... )

end

--------------刷新------------------
function RoleSelectedIcon:Refresh(subID)

end

--------------解析UI组件------------
function RoleSelectedIcon:UIParser( ... )
    local transform = self.transform
    local find = function(struct)
        return transform:FindChild(struct)
    end

    self._roleIconNode = find("RoleIconNode")
    self._bgSprite = transform:GetComponent("UISprite")
    self._uiWidget = self._bgSprite.transform:GetComponent("UIWidget")
    self._uiWidget.color = self.COLOR_DEFAULT
end

--------------侦听事件--------------
function RoleSelectedIcon:AddEvent( ... )
    --addOnClick(self.uiCloseBtn.gameObject,self:OnClickHanlder())-
    addOnClick(self.gameObject,self:OnClickHanlder())
end

--=============================--
--==     公有方法分隔线      ==--
--=============================--

-------------------------------------------
-- 设置icon选中状态
-- bool : true->选中状态 ,false->末选中状态
-------------------------------------------
function RoleSelectedIcon:Selected(bool)
    self._isClicked = bool
    if bool then
        -- self._bgSprite.spriteName = "com_tencent_t101"
        self._uiWidget.color = self.COLOR_SELECT
    else
        -- self._bgSprite.spriteName = "com_bg_bg06"
        self._uiWidget.color = self.COLOR_DEFAULT
    end
end

function RoleSelectedIcon:Visible(bool)
    NGUITools.SetActive(self.gameObject,bool)
end
--=============================--
--==     私有方法分隔线      ==--
--=============================--
function RoleSelectedIcon:OnClickHanlder()
    return function()
        print("RoleSelectedIcon:Click!")
        if not self.ClickAble then
            return
        end
        if self.ClickCallback ~= nil then
            if not self._isClicked then
                self:Selected(true)
                self.ClickCallback(self.Index)
            end
        end
    end
end

-------------------------------------------
-- 设置角色id
-- id: 角色id  如果id为空表示删除角色
-------------------------------------------
function RoleSelectedIcon:SetRoleId(id)
    if self._roleId == id or id == 0 then
        return
    end
    self._roleId = id
    if self._roleId ~= nil and id ~= 0  then
        print("1927 - <RoleSelectedIcon>  self._roleIconNode=",self._roleIconNode)
        CommonFunction.ClearChild(self._roleIconNode)
        self._roleIcon = getLuaComponent(createUI("CareerRoleIcon", self._roleIconNode))
        self._roleIcon:SetClickEnable(false)
        self._roleIcon.id = id
        self._roleIcon.isShowName = false
    else
        CommonFunction.ClearChild(self._roleIconNode)
        self._roleIcon = nil
    end
end

-------------------------------------------
-- 设置球员角色icon.transform的名字
-------------------------------------------
function RoleSelectedIcon:SetRoleIconName(name)
    if self._roleIcon ~= nil then
        self._roleIcon.transform.name = name
    end
end

-------------------------------------------
-- roleIcon的角色按钮的可见性
-------------------------------------------
function RoleSelectedIcon:CloseButtonVisible(bool)
    if self._roleIcon ~= nil then
        self._roleIcon:EnableClose(bool)
    end
end

-------------------------------------------
-- 添加角色球员关闭回调函数
-------------------------------------------
function RoleSelectedIcon:AddRoleIconCloseCallCack(func)
    if self._roleIcon ~= nil then
        self._roleIcon.onClose = func
    end
end

-------------------------------------------
-- 获取角色id
-------------------------------------------
function RoleSelectedIcon:GetRoleId()
    return self._roleId
end

-------------------------------------------
-- 获取角色icon
-------------------------------------------
function RoleSelectedIcon:GetRoleIcon()
    return self._roleIcon
end

-------------------------------------------
-- 设置"Readly"的图标可见性
-------------------------------------------
function RoleSelectedIcon:SetRealyVisible(bool)
    if self._roleIcon ~= nil then
        self._roleIcon.uiReady.gameObject:SetActive(bool)
    end
end

-------------------------------------------
-- 设置球员名称可见性
-------------------------------------------
function RoleSelectedIcon:SetRoleNameVisible(bool)
    if self._roleIcon ~= nil then
        self._roleIcon.uiRoleName.gameObject:SetActive(bool)
    end
end

-------------------------------------------
-- 设置可点击性
-------------------------------------------
function RoleSelectedIcon:SetPressAnabled(bool,callback)
    self._pressEnabled = bool
    self._pressCallback = callback
    --if self._pressEnabled then
        UIEventListener.Get(self.gameObject).onClick = LuaHelper.VoidDelegate(self:OnClick())
    --end
end

-------------------------------------------
-- 按下事件
-------------------------------------------
--[[
function RoleSelectedIcon:OnPress()
    return function(go, isPressed)
        print("RoleSelectedIcon:OnPress doing!")
        if isPressed then
            self._pressTime = os.time()
            self._isPress = true
        else
            print("PressUp")
            self._isPress = false
            if self._pressCallback ~= nil then
                self._pressCallback(false)
            end
        end
    end
end
--]]
function RoleSelectedIcon:OnClick()
    return function ()
        if self._roleId ~= nil and id ~= 0  then
            self._pressCallback(true,self._roleId,self.Index,self._skillsInfo)
        end
    end
end

function RoleSelectedIcon:SetSkillsInfo(skills)
    self._skillsInfo = skills
end

function RoleSelectedIcon:GetSkillsInfo( ... )
    return self._skillsInfo
end

return RoleSelectedIcon

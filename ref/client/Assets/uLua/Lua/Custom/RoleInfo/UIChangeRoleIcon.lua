--个人信息-更换头像界面
local UIChangeRoleIcon = {
	uiName = "UIChangeRoleIcon",

	--UI
	uiGridIcon = nil,
	uiBtnSelect = {},
	uiBtnUse = nil,
	uiShowIcon = nil,
	uiDetail = nil,
	ScrollViewAsyncLoadItem = nil,
	--数据
	m_bDirty = false,
	scrollType = 0,-- 0--all 1-vip特权 2-活动 3-皮肤 4-对战
}
function UIChangeRoleIcon:Awake( ... )
	-- body
end
function UIChangeRoleIcon:Start( ... )
	-- body
end
function UIChangeRoleIcon:Dirty( ... )
	-- body
end
function UIChangeRoleIcon:Refresh( ... )
	-- body
end
function UIChangeRoleIcon:LateRefresh( ... )
	-- body
	return function ( ... )
		-- body
	end
end
function UIChangeRoleIcon:OnDestroy( ... )
	-- body
	GameObject.Destroy(self.gameObject)
	GameObject.Destroy(self.transform)
	self = nil	
end
function UIChangeRoleIcon:OnClose( ... )
	-- body
end
function UIChangeRoleIcon:OnSelect( ... )
	-- body
	return function ( go )
		-- body
		local name = go.name
		if tonumber(name) ~= self.scrollType then
			self.scrollType = tonumber(name)
			self:RefreshIconList()
		end
	end
end
function UIChangeRoleIcon:OnChoose( ... )
	-- body
	return function ( id )
		-- body
		--TODO 显示头像和描述
	end
end
function UIChangeRoleIcon:UnLock( id )
	-- body
	--TODO

end
function UIChangeRoleIcon:RefreshIconList( ... )
	-- body
    CommonFunction.ClearGridChild(self.uiGridIcon.transform)
	self.ScrollViewAsyncLoadItem.LoadCountOnce = 6 	--一帧加载6个
	local propers = {}
	for k,v in pairs(friends or {}) do
    	local data = {}
    	propers[#propers+1] = data
    end
  	if #propers > 0 then
    	self.ScrollViewAsyncLoadItem.OnCreateItem = function ( index, parent )  
	        local item_count = self.uiGridIcon.transform.childCount;
	        local go = nil
	        if index < item_count then
	            go = self.uiGridIcon.transform:GetChild(index);
	        else      
		    	go = createUI('RoleIconChange',self.uiGridIcon.transform)
		    	local luaCom = getLuaComponent(go)
		    	local data = propers[index+1]
		    	luaCom.parent = self
		    	luaCom:Refresh(data)
		        luaCom.onClick = self:OnChoose()
	   		end
		    return go;
		end

    	self.ScrollViewAsyncLoadItem:Refresh(#propers)
    end
end
return UIChangeRoleIcon
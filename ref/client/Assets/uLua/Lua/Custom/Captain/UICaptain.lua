-- 20150618_105831


UICaptain =  {
	uiName='UICaptain',
	go={},
	list_g = {},
	list_d = {},
	current_item = nil,
}

function UICaptain:Awake()
	local g = createUI('Background',self.transform)
	self.background = getLuaComponent(g)
	
	self.background:set_bg("com_bg_pure_purplelight")
	self.background:set_floor("com_bg_pure_purple")
	self.background:set_floor_y(-335)
	self.background:set_dbg_y(40)

	g = createUI("ButtonBack",self.transform:FindChild("ButtonBack"))
	self.back_btn = getLuaComponent(g)
	self.back_btn.onClick = self:click_back()

	
	self.go = {}
	self.team = 1
	-- self.go.action_btn = self.transform:FindChild('ActionBtn').gameObject
	-- self.go.action_label = self.transform:FindChild('ActionBtn/Label'):GetComponent("UILabel")
	self.go.grid = self.transform:FindChild('Scroll/MemberGrid').gameObject:GetComponent('UIGrid')
	self.go.grid2 = self.transform:FindChild('Scroll/MemberGrid2').gameObject:GetComponent('UIGrid')

	self.go.player_display_node = self.transform:FindChild('PlayerDisplayNode')
	self.go.scroll = self.transform:FindChild("Scroll"):GetComponent("UIScrollView")
	
	local pd_go = createUI('PlayerDisplay',self.go.player_display_node)
	self.player_display = getLuaComponent(pd_go)
	self.player_display.is_captain = true
	self.player_display.switch_dlg = self:dlg_switch()
 
	-- addOnClick(self.go.action_btn,self:click_action())

	addOnClick(self.transform:FindChild("Team/Team1").gameObject,self:click_team1())
	addOnClick(self.transform:FindChild("Team/Team2").gameObject,self:click_team2())
	self.go.process_bar = self.transform:FindChild("ProcessBar"):GetComponent("UIProgressBar")
	self.go.left_arrow = self.transform:FindChild("ButtonLeft").gameObject
	self.go.right_arrow = self.transform:FindChild("ButtonRight").gameObject

	self.go.people = self.transform:FindChild("People").gameObject
	self.go.team1_toggle = self.transform:FindChild("Team/Team1"):GetComponent("UIToggle")
	self.go.team2_toggle = self.transform:FindChild("Team/Team2"):GetComponent("UIToggle")

end


function UICaptain:click_team1()
	return function()
		self:select_team(1)
	end
end

function UICaptain:click_team2()
	return function()
		self:select_team(2)
	end
end

function UICaptain:update_action_button()
	self.current_item:update_action_button()
	-- if  not self.player_display.is_show_mode then
	-- 	NGUITools.SetActive(self.go.action_btn,false)
	-- 	return
	-- end
	-- local visiual_action_btn = false
	-- local item = self.current_item
	-- local owned = item:owned()
	-- if owned then 
	-- 	if item.id ~= MainPlayer.Instance.CaptainID then
	-- 		self.go.action_label.text = getCommonStr('STR_ENTER_ON_THE_STAGE')
	-- 		visiual_action_btn = true
	-- 	else
	-- 		visiual_action_btn = false
	-- 	end
	-- else
	-- 	self.go.action_label.text = getCommonStr('STR_RECRUIT')
	-- 	visiual_action_btn = true
	-- end   
	-- NGUITools.SetActive(self.go.action_btn,visiual_action_btn)
end

function UICaptain:dlg_switch()
	return function()
		self:update_action_button()
	end
end




function UICaptain:Start()
	LuaHelper.RegisterPlatMsgHandler(MsgID.SwitchCaptainRespID, self:s_handle_switch_captain(), self.uiName)

	local role_base_config = GameSystem.Instance.RoleBaseConfigData2:GetConfig()
	local enum = role_base_config:GetEnumerator()
	self.list_g = {}
	self.list_d = {}
	
	local captain_item = nil
	while enum:MoveNext() do
		local c_item = enum.Current
		local type = c_item.type
		local init_state = c_item.init_state
		if type==1 then 
			local id = c_item.id
			
			local ss = createUI('CaptainBustItem')
			local item =getLuaComponent(ss)
			item.scroll = self.scroll
			item.ss = ss
			-- item:set_role_base_config(c_item)
			item:set_data(id,true)
			item.on_click = self:click_item()
			item.on_click_action = self:click_action()

			if init_state == 1 then
				table.insert(self.list_g,item)
			elseif init_state == 0 then
				table.insert(self.list_d,item)
			end

			if id == MainPlayer.Instance.CaptainID then
				captain_item = item 
			end
		end
	end

	-- table.sort(self.list_g,
	-- 		   function(x,y) 
	-- 			   if x.attr_data.bias < y.attr_data.bias  then
	-- 				   return true
	-- 			   elseif x.attr_data.bias > y.attr_data.bias  then
	-- 				   return false
	-- 			   else
	-- 				   if x.role_base_config.position < y.role_base_config.position then
	-- 					   return true
	-- 				   elseif x.role_base_config.position > y.role_base_config.position then 
	-- 					   return false
	-- 				   else
	-- 				   end
	-- 			   end
	-- end)
 
	
	-- table.sort(self.list_d,
	-- 		   function(x,y) 
	-- 			   if x.attr_data.bias < y.attr_data.bias  then
	-- 				   return true
	-- 			   elseif x.attr_data.bias > y.attr_data.bias  then
	-- 				   return false
	-- 			   else
	-- 				   if x.role_base_config.position < y.role_base_config.position then
	-- 					   return true
	-- 				   elseif x.role_base_config.position > y.role_base_config.position then 
	-- 					   return false
	-- 				   else
	-- 				   end
	-- 			   end
	-- end)
	

	-- CommonFunction.ClearGridChild(self.go.grid.transform)
	-- for k,v in pairs(self.list_g) do
	-- 	v.ss.transform.parent = self.go.grid.transform
	-- end

	self:select_team(2)
	self:select_team(1)

	NGUITools.SetActive(self.go.people,true)
	self.player_display:visible(false)
end


function UICaptain:OnDestroy()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.SwitchCaptainRespID, self.uiName)
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function UICaptain:get_current_team()
	if self.team == 1 then
		return self.list_g,self.go.grid
	end
	return self.list_d,self.go.grid2
end


function UICaptain:set_current_item(item)
	if self.current_item == item then
		return
	end
	
	NGUITools.SetActive(self.go.people,item == nil)
	self.player_display:visible(item~=nil)

	if item ~= nil then
		self.player_display:set_captain_data(item.id,item.bias)
	end
	self.current_item = item
end

function UICaptain:select_team(team)
	if self.team == team then
		return
	end
	self:set_current_item(nil)
	-- self.current_item = nil
	self:unselected_all()

	self.team = team 
	local lst,grid = self:get_current_team()
	-- local captain_item = lst[1]

	for k,v in pairs(lst) do
		local item = v
		if item.id == MainPlayer.Instance.CaptainID then
			-- captain_item = item
		end
	end
	
	for k,v in pairs(lst) do
		v.ss.transform.parent = grid.transform
	end

	grid:Reposition()
	NGUITools.SetActive(self.go.grid.gameObject,self.go.grid == grid)
	NGUITools.SetActive(self.go.grid2.gameObject,self.go.grid2 == grid)
	self.go.scroll:ResetPosition()
	
	-- if not captain_item then
	-- 	captain_item = lst[1]
	-- end
	
	-- self:click_item_action(captain_item)
end

function UICaptain:Refresh()
	if self.team ~= 1 then
		local t1 = self.go.team1_toggle
		local t2 = self.go.team2_toggle
		t1.value = true
		t2.value = false
	end
	self.team = 2
	self:select_team(1)
	-- self:refresh_all_item()
	self:unselected_all()
end


function UICaptain:click_back()
	return function()
		TopPanelManager:HideTopPanel()
	end
end

function UICaptain:s_handle_switch_captain()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.SwitchCaptainResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				MainPlayer.Instance:SwitchCaptain(resp.id)
				-- self.current_item:refresh()
				self:refresh_all_item()
				self:update_action_button()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_switch_captain(): " .. err)
		end
	end
end

function UICaptain:on_buy_resp_from_csharp(resp)
	self.captain_open_popup:close()
	self:action_enter_to_stage()
	
	if resp.result == 0 then
		CommonFunction.ShowTip(getCommonStr("STR_CAPTAIN_OPEN_SUCCESS"))
		playSound("UI/UI_level_up_01")
	else
		playSound("UI/UI-wrong")
	end
end

function UICaptain:s_handle_buy_captain()
	return function (buf)
		local resp, err = protobuf.decode("fogs.proto.msg.BuyCaptainResp", buf)
		CommonFunction.StopWait()
		if resp then
			if resp.result == 0 then
				self.captain_open_popup:close()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_buy_captain(): " .. err)
		end
	end
end

function UICaptain:unselected_all()
	local lst = self:get_current_team()
	for i=1,#lst do
		lst[i]:set_selected(false)
	end
end

function UICaptain:click_item_action(item)
	if self.current_item == item then
		return
	end
	local refresh_player_display = false
	if self.current_item == nil then
		refresh_player_display = true
	end

	self:set_current_item(item)
	self:unselected_all()
	item:set_selected(true)
	self:update_action_button()

end

function UICaptain:click_item()
	return function(item)
		self:click_item_action(item)
	end
end



function UICaptain:click_action()
	return function()
		local owned = self.current_item:owned()
		if owned then
			if self.current_item.id ~= MainPlayer.Instance.CaptainID then
				self:action_enter_to_stage()
			end
		else
			local pop = createUI('CaptainOpenPopup',self.transform)
			self.captain_open_popup= getLuaComponent(pop)
			self.captain_open_popup.captain_item = self.current_item
		end
	end
end


function UICaptain:action_enter_to_stage()
	local operation = {
		id = self.current_item.id
	}

	local req = protobuf.encode("fogs.proto.msg.SwitchCaptain",operation)
	LuaHelper.SendPlatMsgFromLua(MsgID.SwitchCaptainID,req)
	CommonFunction.ShowWait()
end


function UICaptain:refresh_all_item()
	local lst = self:get_current_team()
	for k,item in pairs(lst) do
		item:refresh()
	end
end

function  UICaptain:FixedUpdate()
	if self.go.process_bar then
		local value = self.go.process_bar.value
		local left = self.go.left_arrow
		local right = self.go.right_arrow
		if value >= 1.0 then
			NGUITools.SetActive(right,false)
			NGUITools.SetActive(left,true)
		elseif value <= 0 then
			NGUITools.SetActive(right,true)
			NGUITools.SetActive(left,false)
		else
			NGUITools.SetActive(right,true)
			NGUITools.SetActive(left,true)
		end
	end
end



return UICaptain

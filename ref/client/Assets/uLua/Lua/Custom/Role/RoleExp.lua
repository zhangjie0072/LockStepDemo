------------------------------------------------------------------------
-- class name	: RoleExp
-- create time   : 20150827_135550
------------------------------------------------------------------------



RoleExp =  {
	uiName	 = "RoleExp", 
}




function RoleExp:Awake()
	self.go = {} 		-- create this for contained gameObject
	self:ui_parse()
	local g,t -- maybe not use.

	g  = WidgetPlaceholder.Replace(self.go.popup_frame.gameObject)
	self.popup_frame = getLuaComponent(g)
	self.popup_frame.onClose = self:click_close()
	self.popup_frame.title = getCommonStr("STR_USE_EXP_CARD")

	g = createUI("RoleBustItem",self.go.top_role_bust_item.transform)
	self.role_item = getLuaComponent(g)
	
	self:add_click()
end

-- parse the prefeb
function RoleExp:ui_parse()
	self.go.popup_frame = self.transform:FindChild("popupFrame"):GetComponent("Transform")	
	self.go.top_role_bust_item = self.transform:FindChild("top/roleBustItem"):GetComponent("Transform")

	-- level
	self.go.top_label_level = self.transform:FindChild("top/labelLevel"):GetComponent("UILabel")	
	self.go.top_label_level_label_level_up = self.transform:FindChild("top/labelLevel/labelLevelUp"):GetComponent("UILabel")
	self.go.middle_progress = self.transform:FindChild("middle/progress"):GetComponent("UIProgressBar")
	self.go.middle_progress_label = self.transform:FindChild("middle/progress/label"):GetComponent("UILabel")	

	-- scroll
	self.go.bottom_scroll = self.transform:FindChild("bottom/scroll"):GetComponent("UIScrollView")
	self.go.bottom_scroll_grid = self.transform:FindChild("bottom/scroll/grid"):GetComponent("UIGrid")
	
end

-- add click functions here
function RoleExp:add_click()
	
end


function RoleExp:click_goods_item()
	return function(item)
		print("item=",item)
		local uuid = item.goods:GetUUID()
		local category = item.goods:GetCategory()
		local num = 1
		local target = self.id
		print("uuid=",uuid)		
		print("category=",category)
		print("num=", num )
		print("target=",target)		

		local operation = {
			uuid = uuid,
			category = tostring(category),
			num = num,
			target = self.id

		}

		local req = protobuf.encode("fogs.proto.msg.UseGoods",operation)
		CommonFunction.ShowWait()
		LuaHelper.SendPlatMsgFromLua(MsgID.UseGoodsID,req)
		
	end
end



function RoleExp:on_recevie_use_goods()
	return function (buf)
		self:unregister()
		local resp, err = protobuf.decode("fogs.proto.msg.UseGoodsResp", buf)
		CommonFunction.StopWait()
		if resp then
			print("resp.result=",resp.result)
			if resp.result == 0 then
				local target = resp.target
				local target_exp = resp.target_exp
				local target_level = resp.target_level
				print("target=",target)
				print("target_exp=",target_exp)
				print("target_level=",target_level)
				MainPlayer.Instance:SetRoleLvAndExp(target,target_level,target_exp)

				self:Refresh()
			else
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
			end
		else
			error("s_handle_switch_captain(): " .. err)
		end
	end
end


function RoleExp:Refresh()
	local id = self.id
	
	self.goods_list = {
	}

	
	CommonFunction.ClearGridChild( self.go.bottom_scroll_grid.transform)

	local consumes = MainPlayer.Instance.ConsumeGoodsList
	local enum = consumes:GetEnumerator()
	while enum:MoveNext() do
		local uid = enum.Current.Key
		local v = enum.Current.Value
		local id = v:GetID()

		print("id=",id)
		local subc = enumToInt(v:GetSubCategory())
		print("subc=",subc)

		if subc == 1 then
			local g = createUI("GoodsIcon",self.go.bottom_scroll_grid.transform)
			local t = getLuaComponent(g)
			t.goods = v
			t.goodsID = id
			t.hideLevel = true

			
			t.num = MainPlayer.Instance:GetGoodsCount(id)
			t.hideNum = false
			print("t.num=",t.num)
			t.on_click = self:click_goods_item()
			table.insert(self.goods_list,t)
			
		end
	end
	self.go.bottom_scroll_grid:Reposition()


	-- exp
	local role_data = MainPlayer.Instance:GetRole2(id)
	local cur_exp = role_data.exp
	local cur_lv = role_data.level


	local cost_exp = 0
	for i = 1, cur_lv - 1 do
		cost_exp = cost_exp + GameSystem.Instance.RoleLevelConfigData:GetMaxExp(i)
	end

	cur_exp = cur_exp - cost_exp

	local next_exp = GameSystem.Instance.RoleLevelConfigData:GetMaxExp(cur_lv)
	self.go.middle_progress_label.text = tostring(cur_exp).."/"..tostring(next_exp)
	self.go.middle_progress.value = (cur_exp)/next_exp
	self.go.top_label_level.text = "Lv."..tostring(cur_lv)
	
	-- role 
	self.role_item:set_data(id)
	self.role_item:Refresh()
end




function RoleExp:Start()
	self:register()
	
	self:Refresh()
end

function RoleExp:set_data(id)
	self.id = id

	

end

function RoleExp:click_close()
	return function()
		print(" RoleExp:click_close, b=",self.on_click_close)
		if self.on_click_close then
			self:on_click_close()
		end
	end
end


function RoleExp:register()
	LuaHelper.RegisterPlatMsgHandler(MsgID.UseGoodsRespID, self:on_recevie_use_goods(), self.uiName)
end

function RoleExp:unregister()
	LuaHelper.UnRegisterPlatMsgHandler(MsgID.UseGoodsRespID, self.uiName) 
end


--function RoleExp:send_XXX()
--	local operation = {
--			id = id
--		}
--
--		local req = protobuf.encode("fogs.proto.msg.XXXX",operation)
--		LuaHelper.SendPlatMsgFromLua(MsgID.SwitchCaptainID,req)
--end




function RoleExp:OnDestroy()
	--body
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end
	   

-- uncommoent if needed
-- function RoleExp:FixedUpdate()

-- end


-- uncommoent if needed
-- function RoleExp:Update()
	

-- end




return RoleExp

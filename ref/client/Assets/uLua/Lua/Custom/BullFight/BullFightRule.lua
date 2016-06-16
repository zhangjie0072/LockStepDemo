------------------------------------------------------------------------
-- class name	: BullFightRule
-- create time   : 20150907_135659
------------------------------------------------------------------------

BullFightRule =  {
	uiName = "BullFightRule",
	go = nil, 					-- this is gameObject array, see ui_parse function.
	popup_frame, 				-- for popup frame lua module.
	rule_pane, 					-- rule pane.
	on_close_click, 			-- close click
	
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function BullFightRule:Awake()
	self.go = {} 		-- create this for contained gameObject
	self:ui_parse()
	
	 local g,t -- maybe not use.
	self:add_click()
end


function BullFightRule:Start()
	self:register()   			-- register the proto function call back.

	local g, t
	self.go.popup_frame= WidgetPlaceholder.Replace(self.go.popup_frame)
	self.popup_frame = getLuaComponent(self.go.popup_frame)
	self.popup_frame.title = getCommonStr("BULL_FIGHT_RULE")
	NGUITools.SetActive(self.go.popup_frame.gameObject,true)
	-- self.popup_frame.showCorner = true
	self.popup_frame.onClose = self:click_close()
	
	-- addOnClick(self.go.mask.gameObject,self:click_close())

	local rule = getCommonStr("BULL_FIGHT_RULE_DES")
	self.rule_pane = getLuaComponent(createUI("RulePane1", self.go.scroll_rules_botton_rule))
	self.rule_pane.rule = rule
	
	
	self:Refresh()
end


function BullFightRule:click_close()
	return function()
		if self.on_close_click then
			self:on_close_click()
		end
	end
end



function BullFightRule:Refresh()
	
end

-- uncommoent if needed
-- function BullFightRule:FixedUpdate()

-- end



function BullFightRule:OnDestroy()
	self:unregister()			-- unregister the proto function call back.
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.			   --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.	   --
-- NOTE:														   --
--      1. this function only used to parse the UI(GameObject).	   --
--      2. the name start with self.go, go means GameObject.	   --
--      3.the name is according to the structure of prefab. 	   --
---------------------------------------------------------------------
function BullFightRule:ui_parse()
	self.go.mask = self.transform:FindChild("mask"):GetComponent("UISprite")

	self.go.popup_frame = self.transform:FindChild("popupFrame"):GetComponent("Transform")

	--self.go.bg = self.transform:FindChild("bg"):GetComponent("UISprite")

	--self.go.scroll = self.transform:FindChild("scroll"):GetComponent("UIScrollView")
	--self.go.scroll_rules = self.transform:FindChild("scroll/rules"):GetComponent("Transform")
	--self.go.scroll_rules_botton = self.transform:FindChild("scroll/rules/botton"):GetComponent("Transform")
	--self.go.scroll_rules_botton_label_rule = self.transform:FindChild("scroll/rules/botton/labelRule"):GetComponent("UILabel")
	--self.go.scroll_rules_botton_bg = self.transform:FindChild("scroll/rules/botton/bg"):GetComponent("UISprite")
	--self.go.scroll_rules_botton_bg_titlt = self.transform:FindChild("scroll/rules/botton/bgTitlt"):GetComponent("UISprite")
	self.go.scroll_rules_botton_rule = self.transform:FindChild("scroll/rules/botton/rule"):GetComponent("Transform")

end

-- add click functions adds here
function BullFightRule:add_click()
	
end


function BullFightRule:set_data()
	
end

function BullFightRule:click_close()
	return function()
		if self.on_click_close then
			self.on_click_close()
		end
	end
end

-- register the proto function call back.
function BullFightRule:register()
	-- FIXME: below is the example.
	-- LuaHelper.RegisterPlatMsgHandler(MsgID.IDXXX, self:on_recevie_XXX(), self.uiName)
end

function BullFightRule:unregister()
	-- FIXME: below is the example.	
	-- LuaHelper.UnRegisterPlatMsgHandler(MsgID.IDXXX, self.uiName) 
end


--function BullFightRule:send_XXX()
--	local operation = {
--			id = id
--		}
--
--		local req = protobuf.encode("fogs.proto.msg.XXXX",operation)
--		LuaHelper.SendPlatMsgFromLua(MsgID.SwitchCaptainID,req)
--end

--function BullFightRule:on_recevie_XXX()
--	return function (buf)
--		local resp, err = protobuf.decode("fogs.proto.msg.XXXResp", buf)
--		if resp then
--			if resp.result == 0 then
--			else
--				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result))
--			end
--		else
--			error("s_handle_switch_captain(): " .. err)
--		end
--	end
--end

-- uncommoent if needed
-- function BullFightRule:Update()


-- end




return BullFightRule

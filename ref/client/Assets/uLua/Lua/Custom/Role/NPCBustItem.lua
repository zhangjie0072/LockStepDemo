------------------------------------------------------------------------
-- class name	: NPCBustItem
-- create time   : 20150902_160547
------------------------------------------------------------------------

NPCBustItem =  {
	uiName	 = "NPCBustItem", 
	go = nil, 					-- this is gameObject array, see ui_parse function.
	id, 						-- NPC id.
	on_click,					-- click item
	hard,						-- hard level 
}

---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function NPCBustItem:Awake()
	self.go = {} 		-- create this for contained gameObject
	self:ui_parse()
	
	self:add_click()
end


function NPCBustItem:Start()
	-- self:register()   			-- register the proto function call back.
	self.go.panel2_icon:MakePixelPerfect()
	self:Refresh()
end

function NPCBustItem:Refresh()
	
end

-- uncommoent if needed
function NPCBustItem:FixedUpdate()
	local alpha = self.go.panel2.alpha
	self.go.panel2.alpha = 0.99
	self.go.panel2.alpha = alpha
end



function NPCBustItem:OnDestroy()
	-- self:unregister()			-- unregister the proto function call back.
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function NPCBustItem:set_data(id,hard)
	self.id = id
	self.hard = hard
	local npc_config = GameSystem.Instance.NPCConfigData:GetConfigData(self.id)
	local shape_id = npc_config.shape
	local position = npc_config.position
	self.go.card_name.text = npc_config.name
	self.go.panel2_icon.spriteName = "icon_bust_"..tostring(shape_id)
	self.go.panel2_icon:MakePixelPerfect()
	self.go.panel2_icon.atlas = getBustAtlas(shape_id)
	self:set_star(hard)

	local mp_lv = MainPlayer.Instance.Level
	local bf_lv = GameSystem.Instance.bullFightConfig:GetFightLevel(self.hard)
	local positions ={'PF','SF','C','PG','SG'}
	self.go.card_position.spriteName = "PT_"..positions[position]

	if mp_lv >= bf_lv.unlock_level then
		local cc={229,163,104}
		local pfc={163,193,243}
		local sfc = {92,199,193}
		local pgc = {177,225,70}
		local sgc={235,221,68}
		local tc={pfc,sfc,cc,pgc,sgc}
		local pc = tc[position]
		self.go.panel2_bg.defaultColor = Color.New(pc[1]/255, pc[2]/255, pc[3]/255 )
		self.go.panel2_bg.hover = Color.New(pc[1]/255, pc[2]/255, pc[3]/255 )
		self.go.panel2_bg.pressed = Color.New(pc[1]/255, pc[2]/255, pc[3]/255 )		
	else
		self.go.panel2_bg.defaultColor = Color.gray
		self.go.panel2_bg.hover = Color.gray
		self.go.panel2_bg.pressed = Color.gray
		self.go.lock.text = string.format(getCommonStr("SKILL_UNLOCK_LEVEL"), bf_lv.unlock_level)
		NGUITools.SetActive(self.go.lock.gameObject,true)
	end
end


---------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.			   --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.	   --
-- NOTE:														   --
--	  1. this function only used to parse the UI(GameObject).	   --
--	  2. the name start with self.go, go means GameObject.	   --
--	  3. the name is according to the structure of prefab. 	   --
---------------------------------------------------------------------
function NPCBustItem:ui_parse()
	--self.go.model				=				 self.transform:FindChild("model"):GetComponent("Transform")
	
	self.go.panel2			   =				 self.transform:FindChild("panel2"):GetComponent("UIPanel")
	self.go.panel2_bg			=				 self.transform:FindChild("panel2/bg"):GetComponent("UIButton")
	self.go.panel2_icon		  =				 self.transform:FindChild("panel2/icon"):GetComponent("UISprite")
	
	--self.go.card				 =				 self.transform:FindChild("card"):GetComponent("Transform")
	self.go.card_select		  =				 self.transform:FindChild("card/select"):GetComponent("UISprite")
	--self.go.card_bg_lock		 =				 self.transform:FindChild("card/bgLock"):GetComponent("UISprite")
	--self.go.card_name_background =				 self.transform:FindChild("card/nameBackground"):GetComponent("UISprite")
	self.go.card_position		=				 self.transform:FindChild("card/position"):GetComponent("UISprite")
	self.go.card_name			  =				 self.transform:FindChild("card/name"):GetComponent("UILabel")
	-- self.go.card_role = self.transform:FindChild("card/role"):GetComponent("UISprite")

	--self.go.star = self.transform:FindChild("star"):GetComponent("Transform")
	-- self.go.star_label = self.transform:FindChild("star/label"):GetComponent("UILabel")
	self.go.star_0 = self.transform:FindChild("star/0"):GetComponent("UISprite")
	self.go.star_1 = self.transform:FindChild("star/1"):GetComponent("UISprite")
	self.go.star_2 = self.transform:FindChild("star/2"):GetComponent("UISprite")
	self.go.star_3 = self.transform:FindChild("star/3"):GetComponent("UISprite")
	self.go.star_4 = self.transform:FindChild("star/4"):GetComponent("UISprite")
	self.go.star_5 = self.transform:FindChild("star/5"):GetComponent("UISprite")	

	self.go.lock = self.transform:FindChild("lock"):GetComponent("UILabel")
	
end

-- add click functions adds here
function NPCBustItem:add_click()
	addOnClick(self.go.panel2_bg.gameObject,self:click())
end


function NPCBustItem:click()
	return function()
		if self.on_click then
			self:on_click(self)
		end
	end
end







function NPCBustItem:click_close()
	return function()
		if self.on_click_close then
			self.on_click_close()
		end
	end
end

-- register the proto function call back.
function NPCBustItem:register()
	-- FIXME: below is the example.
	-- LuaHelper.RegisterPlatMsgHandler(MsgID.IDXXX, self:on_recevie_XXX(), self.uiName)
end

function NPCBustItem:unregister()
	-- FIXME: below is the example.	
	-- LuaHelper.UnRegisterPlatMsgHandler(MsgID.IDXXX, self.uiName) 
end


--function NPCBustItem:send_XXX()
--	local operation = {
--			id = id
--		}
--
--		local req = protobuf.encode("fogs.proto.msg.XXXX",operation)
--		LuaHelper.SendPlatMsgFromLua(MsgID.SwitchCaptainID,req)
--end

--function NPCBustItem:on_recevie_XXX()
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



function NPCBustItem:set_star(star)
	local stars = {
		self.go.star_0,	self.go.star_1,	self.go.star_2,	self.go.star_3,	self.go.star_4, self.go.star_5
	}
	
	for i=1,#stars do
		NGUITools.SetActive(stars[i].gameObject,i <= star)
	end

	
	-- FIXME: color adjust
	local colors = {
		Color.New(1.0,1.0,1.0,1.0),
		Color.green,
		Color.blue,
		Color.New(0.71,0.4,0.9,1.0),
		Color.yellow,
		Color.red
	}
	self.go.card_select.color = colors[star]

	
end


	   


-- uncommoent if needed
-- function NPCBustItem:Update()
	

-- end




return NPCBustItem

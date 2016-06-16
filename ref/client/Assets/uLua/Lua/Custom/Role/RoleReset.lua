------------------------------------------------------------------------
-- class name	: RoleReset
-- create time   : Mon Sep 21 14:45:53 2015
------------------------------------------------------------------------

RoleReset =  {
	uiName	 = "RoleReset",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	
	
	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	onClickCancle, 
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleReset:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function RoleReset:Start()
	self.itemL = getLuaComponent(createUI("RoleBustItem1", self.uiItemL.transform))
	self.itemL.id = self.id
	self.itemR = getLuaComponent(createUI("RoleBustItem1", self.uiItemR.transform))
	self.itemR.id = self.id
	self.itemR.isResetDisplay = true

	addOnClick(self.uiResetBtn.gameObject,self:ClickReset())
	addOnClick(self.uiCancelBtn.gameObject,self:ClickCancle())
	
	self:Refresh()
end

function RoleReset:Refresh()
	
end

-- uncommoent if needed
-- function RoleReset:FixedUpdate()

-- end


function RoleReset:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.		 								 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.   								 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleReset:UiParse()
		-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	--self.uiMask = find("Mask"):GetComponent("UISprite")

	self.uiItemL = find("RoleBustItemL"):GetComponent("Transform")
	self.uiItemR = find("RoleBustItemR"):GetComponent("Transform")
	
	--self.uiArrow = find("Arrow"):GetComponent("UISprite")

	--self.uiTip = find("Tip"):GetComponent("UILabel")

	self.uiResetBtn = find("ButtonReset"):GetComponent("UIButton")
	--self.uiButtonReset_Text = find("ButtonReset/Text"):GetComponent("MultiLabel")
	--self.uiButtonReset_Text_Shadow = find("ButtonReset/Text/Shadow"):GetComponent("UILabel")

	self.uiCancelBtn = find("ButtonCancel"):GetComponent("UIButton")
	--self.uiButtonCancel_Text = find("ButtonCancel/Text"):GetComponent("MultiLabel")
	--self.uiButtonCancel_Text_Shadow = find("ButtonCancel/Text/Shadow"):GetComponent("UILabel")
end

function RoleReset:SetData(id)
	self.id = id
end


function RoleReset:ClickCancle()
	return function()
		if self.onClickCancle then
			self:onClickCancle()
		end
	end
end

function RoleReset:ClickReset()
	return function()
		local id = self.id
		print("Reset role id =",id)			
		local operation = {
			role_id = id
		}
		local req = protobuf.encode("fogs.proto.msg.ResetRoleReq",operation)
		LuaHelper.SendPlatMsgFromLua(MsgID.ResetRoleReqID,req)
		CommonFunction.ShowWaitMask()
	end
end


return RoleReset



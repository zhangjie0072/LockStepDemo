------------------------------------------------------------------------
-- class name	: RoleAcquirePopup
-- create time   : Tue Sep 15 20:35:56 2015
------------------------------------------------------------------------

RoleAcquirePopup = {
	uiName = "RoleAcquirePopup",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiRoleBustItem,

	-----------------------
	-- Parameters Module --
	-----------------------
	id, 
	IsInClude,
	contentStr,
	onCloseClick,
	totalAward,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function RoleAcquirePopup:Awake()
	self:UiParse() -- Foucs on UI Parse.
end

function RoleAcquirePopup:Start()
	addOnClick(self.uiMask.gameObject, self:ClickBack())

	local g,t
	g = createUI("RoleBustItem1",self.uiRoleBustItem.transform)
	t = getLuaComponent(g)
	t.onClickSelect = self:ClickBack()
	t.id = self.id
	t.isResetDisplay = true
	self:Refresh()
end

function RoleAcquirePopup:Refresh()

end

-- uncommoent if needed
-- function RoleAcquirePopup:FixedUpdate()

-- end


function RoleAcquirePopup:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------


---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.											 --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.									 --
-- NOTE:																						 --
-- 	1. This function only used to parse the UI(GameObject). 									 --
-- 	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
-- 	3. The name is according to the structure of prefab.										 --
-- 	4. Please Do NOT MINDE the Comment Lines.													 --
-- 	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function RoleAcquirePopup:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiMask = find("Mask"):GetComponent("UISprite")

	self.uiRoleBustItem = find("RoleBustItem"):GetComponent("Transform")

end

function RoleAcquirePopup:SetData(id)
	self.id = id
end

function RoleAcquirePopup:ClickBack()
	return function()
		if self.onClose then
			self.onClose:DynamicInvoke()
		else
			if self.onCloseClick and (self:CheckRoleInclude(self.id) ~= true) then
				self.onCloseClick()
			end

			if self.totalAward then
				local getGoods = getLuaComponent(createUI('GoodsAcquirePopup'))
				local awardEnum = self.totalAward:GetEnumerator()
				while awardEnum:MoveNext() do
					local award = string.split(awardEnum.Current, ':')
					getGoods:SetGoodsData(tonumber(award[1]), tonumber(award[2]))
				end
			end

			-- judge id is included
			if self.IsInClude then
				print("is inluded"..self.id)
				local data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
				if data.recruit_output_id and data.recruit_output_value then
					local luaCom = getLuaComponent(createUI("GoodsAcquirePopup", UIManager.Instance.m_uiRootBasePanel.transform))
					luaCom:SetGoodsData(data.recruit_output_id, data.recruit_output_value)
					luaCom.isRoleRecruit = true
					luaCom.id = data.recruit_output_id
					luaCom.num = data.recruit_output_value
					luaCom.onClose = self.onCloseClick
					luaCom:SetContent(self.contentStr)
				end
			else
				-- 如果组成了新图鉴
		        if MainPlayer.Instance.NewMapIDList.Count > 0 then
					local goodsAcquire = getLuaComponent(createUI('GoodsAcquirePopup'))
					goodsAcquire:SetNewMapData(MainPlayer.Instance.NewMapIDList:get_Item(0))
					goodsAcquire.onClose = self.onCloseClick
					if MainPlayer.Instance.NewMapIDList.Count > 0 then
						goodsAcquire.nextMaps = MainPlayer.Instance.NewMapIDList:get_Item(0)
					end
					UIManager.Instance:BringPanelForward(goodsAcquire.gameObject)
		        end
			end
			NGUITools.Destroy(self.gameObject)
		end
	end
end

function RoleAcquirePopup:CheckRoleInclude(id)
	print("include id :",id)
	return MainPlayer.Instance:HasRole(id)
end

return RoleAcquirePopup
------------------------------------------------------------------------
-- class name    : LeftLink
-- create time   : Fri Dec  4 18:01:09 2015
------------------------------------------------------------------------

LeftLink =  {
	uiName     = "LeftLink",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------

	-----------------------
	-- Parameters Module --
	-----------------------
	id,
	icon,
	roleId,
	curTab,
	preUiName,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function LeftLink:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function LeftLink:Start()

	self:Refresh()
end

function LeftLink:Refresh()

end

-- uncommoent if needed
-- function LeftLink:FixedUpdate()

-- end


function LeftLink:OnDestroy()

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
--	1. This function only used to parse the UI(GameObject).										 --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.					 --
--	3. The name is according to the structure of prefab.										 --
--	4. Please Do NOT MINDE the Comment Lines.													 --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.		 --
---------------------------------------------------------------------------------------------------
function LeftLink:UiParse()
	--self.uiText = self.transform:FindChild("Text"):GetComponent("MultiLabel")
	--self.uiShadow = self.transform:FindChild("Text/Shadow"):GetComponent("UILabel")

	self.uiGoodsIcon = self.transform:FindChild("GoodsIcon"):GetComponent("Transform")

	self.uiName = self.transform:FindChild("Name"):GetComponent("UILabel")

	--self.uiScroll = self.transform:FindChild("Scroll"):GetComponent("UIScrollView")
	self.uiGrid = self.transform:FindChild("Scroll/GainGrid"):GetComponent("UIGrid")

	--self.uiBackground = self.transform:FindChild("Background"):GetComponent("UISprite")
	--self.uiBackTitle = self.transform:FindChild("Background/BackTitle"):GetComponent("UISprite")
	--self.uiBackType1 = self.transform:FindChild("Background/BackType1"):GetComponent("UISprite")
	--self.uiBackShade = self.transform:FindChild("Background/BackShade"):GetComponent("UISprite")

end



function LeftLink:SetData(id, roleId, curTab, preUiName)
	self.id = id
	self.roleId = roleId
	self.curTab = curTab
	self.preUiName = preUiName

	if not self.icon then
		local g = getLuaComponent(createUI("GoodsIcon", self.uiGoodsIcon))
		g.hideNeed = true
		self.icon = g
	end

	print("LeftLink self.id=",self.id)
	self.icon.goodsID = self.id
	self.icon:Refresh()


	local costAttr = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	self.uiName.text = costAttr.name
	local accessWayType = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).access_way_type
	local accessWay = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id).access_way
	CommonFunction.ClearGridChild(self.uiGrid.transform)
	if accessWayType == 1 then
		local awItems = Split(accessWay,'&')
		for k,v in pairs(awItems) do
			if v ~= "" then
				local aw_item = Split(v,':')
				print("aw_item=",aw_item)
				local script = getLuaComponent(createUI('RoleLinkItem2',self.uiGrid.transform))
				script:SetData(accessWayType, aw_item[1],aw_item[2])
				script.roleId = self.roleId
				script.exerciseId = self.id
				script.linkTab = self.curTab
				script.linkUi = self.preUiName
			end
		end
	elseif accessWayType == 2 then
		local script = getLuaComponent(createUI('RoleLinkItem2',self.uiGrid.transform))
		print("666 accessWay2=",accessWay)
		script:SetData(accessWayType, accessWay)
		script.roleId = self.roleId
		script.exerciseId = self.id
		script.linkTab = self.curTab
		script.linkUi = self.preUiName
	end
	self.uiGrid.repositionNow = true

end


return LeftLink

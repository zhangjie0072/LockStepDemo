------------------------------------------------------------------------
-- class name    : VIPItem
-- create time   : Wed Nov 25 18:36:07 2015
------------------------------------------------------------------------

VIPItem =  {
	uiName     = "VIPItem",
	--------------------------------------------------------------------
	-- UI Module: Name Start with 'ui',  such as uiButton, uiClick	  --
	--------------------------------------------------------------------
	uiBg         ,
	uiIcon       ,
	uiExtDiamondNode,
	uiExtDiamond ,
	uiDiamond    ,
	uiRmb        ,
	uiRecommend  ,
	uiNode       ,
	uiIcon4      ,
	uiIcon3      ,
	uiIcon2      ,
	uiIcon1      ,
	uiTextIcon   ,

	-----------------------
	-- Parameters Module --
	-----------------------
	data,
	onClick,
}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function VIPItem:Awake()
	self:UiParse()				-- Foucs on UI Parse.
end


function VIPItem:Start()
	self:Refresh()

	addOnClick(self.uiBg.gameObject, self:Click())
end

function VIPItem:Refresh()
	local data = self.data
	if data.recharge % 100 == 0 then
		self.uiRmb.text = string.format(getCommonStr("RMB").. data.recharge/100)
	else
		self.uiRmb.text = string.format(getCommonStr("RMB")..data.recharge/100)
	end
	--self.uiDiamond.text = data.diamond..getCommonStr("DIAMOND")
	-- self.uiDiamond.text = data.diamond
	self:SetDiamond(data.diamond)
	self.uiRecommend.gameObject:SetActive(data.recommend == 1)
	if data.icon then
		self.uiIcon.spriteName = data.icon
	end

	local enum = MainPlayer.Instance.VipRechargeList:GetEnumerator()
	for i=1, data.id do
		enum:MoveNext()
	end
	if enum.Current == 0 then
		self.uiExtDiamond.text = data.ext_diamond
	else
		self.uiExtDiamondNode.gameObject:SetActive(false)
	end
	self.uiIcon:MakePixelPerfect()
end

-- uncommoent if needed
-- function VIPItem:FixedUpdate()

-- end


function VIPItem:OnDestroy()
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


function VIPItem:UiParse()
	self.uiBg         = self.transform:FindChild("BG"):GetComponent("UISprite")
	self.uiIcon       = self.transform:FindChild("BG/Icon"):GetComponent("UISprite")
	--self.uiExtDiamond = self.transform:FindChild("BG/Icon/Tip"):GetComponent("UILabel")
	self.uiExtDiamondNode = self.transform:FindChild("BG/BackType"):GetComponent("UISprite")
	self.uiExtDiamond = self.transform:FindChild("BG/BackType/Num1"):GetComponent("UILabel")
	-- self.uiDiamond    = self.transform:FindChild("Icon/Num1"):GetComponent("UILabel")
	self.uiRmb        = self.transform:FindChild("Num2"):GetComponent("UILabel")
	self.uiRecommend  = self.transform:FindChild("Bg"):GetComponent("UISprite")
	self.uiNode     = self.transform:FindChild("BG/Node"):GetComponent("Transform")
	self.uiIcon4    = self.transform:FindChild("BG/Node/Icon4"):GetComponent("UISprite")
	self.uiIcon3    = self.transform:FindChild("BG/Node/Icon3"):GetComponent("UISprite")
	self.uiIcon2    = self.transform:FindChild("BG/Node/Icon2"):GetComponent("UISprite")
	self.uiIcon1    = self.transform:FindChild("BG/Node/Icon1"):GetComponent("UISprite")
	self.uiTextIcon = self.transform:FindChild("BG/Node/TextIcon"):GetComponent("UISprite")
end

function VIPItem:SetData(data)
	self.data = data
end

function VIPItem:Click()
	return function()
		if self.onClick then
			self:onClick(self)
		end
	end
end

function VIPItem:SetDiamond(diamond)
	local t = {
		self.uiIcon1,
		self.uiIcon2,
		self.uiIcon3,
		self.uiIcon4,
	}
	local base = 1000
	local bit = 1
	for i=4, 1, -1 do
		if (diamond / base) >= 1 then
			bit = i
			break
		end
		base = base/10
	end

	local w = 0
	for i=1,4 do
		if i <= bit then
			t[i].gameObject:SetActive(true)
			local a = math.modf(diamond/math.pow(10, (i-1)))
			local b = a % 10
			t[i].spriteName = self.vipIconNamePre..b
			w = w + t[i].width
		else
			t[i].gameObject:SetActive(false)
		end
	end

	local f = self.uiTextIcon
	local wt = f.width
	w = w + self.uiTextIcon.width
	local pos = f.transform.localPosition
	pos.x = (w-wt)/2
	f.transform.localPosition = pos
end

return VIPItem

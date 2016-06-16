
HallOfFame =  {
	uiName	= "HallOfFame",
	----------------------UI
	uiTitle,
	uiAttrName,
	-- uiAttrNum,
	uiRoleGrid,
	------------------------
}

local specialAttr = 
{
	sType1 = 200,
	sType2 = 201,
	sType3 = 202,
}

function HallOfFame:Awake()
	self.uiTitle = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiAttrName = self.transform:FindChild('AttributeName'):GetComponent('UILabel')
	-- self.uiAttrNum = self.transform:FindChild('AttributeName/AttributeNum'):GetComponent('UILabel')
	self.uiRoleGrid = self.transform:FindChild('Middle/Grid'):GetComponent('UIGrid')
end

function HallOfFame:Start()
end

function HallOfFame:FixedUpdate()
end

function HallOfFame:OnClose()
end

function HallOfFame:DoClose()	
	if self.uiAnimator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

function HallOfFame:OnDestroy()
	
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function HallOfFame:Refresh()
end
--------------------------------
function HallOfFame:SetData(id)
	local squadInfo = MainPlayer.Instance.SquadInfo
	local mapConfig = GameSystem.Instance.MapConfig:GetMapGroupDataByID(id)
	self.uiTitle.text = mapConfig.groupName

	local attrId = mapConfig.attrID
	-- self.uiAttrNum.text = "+" .. mapConfig.attrNum

	local roleIDList = mapConfig.groupIDs
	local isOwn = true
	for i = 1, roleIDList.Count do
		local role = createUI("GoodsIcon", self.uiRoleGrid.transform)
		local roleLua = getLuaComponent(role)
		roleLua.goodsID = roleIDList:get_Item(i - 1)
		roleLua.hideNum = true
		roleLua.hideNeed = true
		roleLua.hideLevel = true
		roleLua.inHallOfFame = true
		roleLua.showTips = false
		local role = MainPlayer.Instance:GetRole2(roleLua.goodsID)
		if role then
			roleLua:SetMask(false)
		else
			isOwn = false
			roleLua:SetMask(true)
		end
	end
	if isOwn then
		self.uiAttrName.text = mapConfig.describe
	else
		self.uiAttrName.text = string.format('[%s]', getCommonStr('INACTIVE')) .. mapConfig.describe
		self.uiAttrName.color = Color.New(75/255,75/255,75/255,1)
	end
end

function HallOfFame:SetDelegate(delegate)
	local parent = self.uiRoleGrid.transform
	for i = 1, parent.childCount do
		UIEventListener.Get(parent:GetChild(i - 1).gameObject).onDrag = delegate
	end
end

return HallOfFame

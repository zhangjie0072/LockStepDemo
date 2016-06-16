-- 批量分解详情界面item
BadgeDecomposeGoodsItem = {
	uiName = "BadgeDecomposeGoodsItem",
	-------params----
	_id,
	-------UI--------
	_badgeIcon,
	_togSelect,
	_nameLabel,
}

function BadgeDecomposeGoodsItem:Awake( ... )
	self._badgeIcon = getLuaComponent(createUI("BadgeIcon",self.transform:FindChild("IconNode").transform))
	self._togSelect = self.transform:FindChild("NameIcon"):GetComponent("UIToggle")
	self._nameLabel = self.transform:FindChild("Name"):GetComponent("UILabel")
end
---------------Start--------------
function BadgeDecomposeGoodsItem:Start( ... )

end

--------------Update-----------------
function BadgeDecomposeGoodsItem:FixedUpdate( ... )
	-- body
end

--------------OnDestory-----------------
function BadgeDecomposeGoodsItem:OnDestroy( ... )
	-- body
end

function BadgeDecomposeGoodsItem:GetId( ... )
	return self._id
end

function BadgeDecomposeGoodsItem:IsSelect()
	return self._togSelect.value 
end

function BadgeDecomposeGoodsItem:SetId(id)
	self._id = id
	self._badgeIcon:SetId(id)
	local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	if goods then
		self._nameLabel.text = goods.name 
		self._badgeIcon:SetNum(MainPlayer.Instance.badgeSystemInfo:GetBadgeLeftNumExceptAllUsed(id))
	end
	local t = getLuaComponent(self.transform:FindChild("Grid").transform)
	t:SetBadgeId(id)
end

return BadgeDecomposeGoodsItem
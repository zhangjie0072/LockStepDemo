VipGiftItemContent = 
{
	uiName = 'VipGiftItemContent',
	------------------------------
	----------------------------UI
	uiBtnClose,
	uiBtnConfirm,
	uiTipLabel,
	uiGoodsGrid,
}

function VipGiftItemContent:Awake( ... )
	self.uiBtnClose = createUI('ButtonClose', self.transform:FindChild('Window/ButtonClose'))
	self.uiBtnConfirm = self.transform:FindChild('Window/ButtonOK'):GetComponent('UIButton')
	self.uiTipLabel = self.transform:FindChild('Window/Tip'):GetComponent('UILabel')
	self.uiGoodsGrid = self.transform:FindChild('Window/Grid'):GetComponent('UIGrid')
end

function VipGiftItemContent:Start( ... )
	local btnClose = getLuaComponent(self.uiBtnClose)
	btnClose.onClick = self:OnClose()
	addOnClick(self.uiBtnConfirm.gameObject, self:OnClose())
end

function VipGiftItemContent:OnClose( ... )
	return function (go)
		NGUITools.Destroy(self.gameObject)
	end
end

function VipGiftItemContent:SetTip(str)
	self.uiTipLabel.text = tostring(str)
end

function VipGiftItemContent:SetData(id)
	local goods_use_config = GameSystem.Instance.GoodsConfigData:GetGoodsUseConfig(id)
	local args = goods_use_config.args
	local enum = args:GetEnumerator()
	while enum:MoveNext() do
		local v = enum.Current
		local gu_id = v.id
		local gu_num  = v.num_max
		local g = createUI("GoodsIcon",self.uiGoodsGrid.transform)
		local t = getLuaComponent(g)
		t.goodsID = gu_id
		t.num = gu_num
		t.hideLevel = true
		t.hideNum = false
		t.hideNeed = true
	end
end

return VipGiftItemContent
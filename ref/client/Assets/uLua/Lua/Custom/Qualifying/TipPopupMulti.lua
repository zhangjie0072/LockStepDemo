TipPopupMulti = {
	uiName = 'TipPopupMulti',
	---------------------
	title,
	uiGridReward = nil,
}

function TipPopupMulti:Awake()
	self.title = self.transform:FindChild("Text"):GetComponent("UILabel")
	self.uiGridReward = self.transform:FindChild("Grid"):GetComponent("UIGrid")
end

function TipPopupMulti:Start()
	addOnClick(self.gameObject,self:OnClick())
end
--ref UIQualifyingNew.lua.ShowAwardTip
--data数据为{{id,num}。。。}
function TipPopupMulti:SetData( data )
	-- body
	for k,v in pairs(data or {}) do 
		print('data id '..v.id..',data num '..v.num)
		local g = createUI("GoodsIcon", self.uiGridReward.transform)
		local t = getLuaComponent(g)
		g.gameObject.name = v.id
		t.goodsID = v.id
		t.num = v.num
		t.hideLevel = true
		t.hideNeed = true
		t.hideNum = false
	end
    self.uiGridReward.repositionNow = true

end
function TipPopupMulti:Refresh()

end

function TipPopupMulti:OnClick()
	return function ()
		NGUITools.Destroy(self.gameObject)
	end
end

return TipPopupMulti
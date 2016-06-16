--encoding=utf-8

BlackListDetail = 
{
	uiName = 'BlackListDetail',
	-------------------------------------


	-------------------------------------UI
	uiuiTitle,
	uiGoodsIcon,
	uiOwnedLbl,
	uiOwnedNum,
	uiGoodsIntro,

};


-----------------------------------------------------------------
--Awake
function BlackListDetail:Awake()
	local transform = self.transform

	self.uiTitle = transform:FindChild('Title'):GetComponent('UILabel')
	self.uiGoodsIcon = transform:FindChild('GoodsIcon')
	self.uiOwnedLbl = transform:FindChild('OwnedLbl'):GetComponent('UILabel')
	self.uiOwnedLbl.text = CommonFunction.GetConstString('STR_OWNED')
	self.uiOwnedNum = transform:FindChild('OwnedNum'):GetComponent('UILabel')
	self.uiGoodsIntro = transform:FindChild('Intro'):GetComponent('UILabel')
end

--Start
function BlackListDetail:Start()

end

--Update
function BlackListDetail:Update( ... )
	-- body
end


-----------------------------------------------------------------
--
function BlackListDetail:Init(config)

	self.uiTitle.text = config.name
	local go = createUI('GoodsIcon', self.uiGoodsIcon.transform)
	self.uiGoodsIcon = getLuaComponent(go)
	self.uiGoodsIcon.goodsID = config.id
	self.uiGoodsIcon.hideLevel = true
	self.uiOwnedNum.text = MainPlayer.Instance:GetGoodsCount(config.id)
	self.uiGoodsIntro.text = config.intro
end


return BlackListDetail
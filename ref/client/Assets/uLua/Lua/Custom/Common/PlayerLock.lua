-- 20150628_184736


PlayerLock =  {
	uiName = 'PlayerLock',
	
	go = {},


}




function PlayerLock:Awake()
	self.go = {}
	self.go.num = self.transform:FindChild("Num"):GetComponent("UILabel")
	self.go.icon = self.transform:FindChild("Icon"):GetComponent("UISprite")
end


function PlayerLock:set_data(id,num)
	self.id= id
	if id == 1 then 
	  self.go.icon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/common/Common3")
	  self.go.icon.spriteName = "com_property_diamond2"

	else
	  local goods_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	  self.go.icon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/icon/iconPiece")
	  self.go.icon.spriteName = goods_config.icon
	end
	self.go.num.text = tostring(num)
	
end

function PlayerLock:set_visible(isvisisble)
	NGUITools:SetActive(self.gameObject,isvisisble)
end

function PlayerLock:Start()

end


return PlayerLock



GoodsItem =  {
	uiName = 'GoodsItem',
	
	title = "title",
	action_type = "",
	id = 0,
	level =1,
	onClick,
	icon = nil,
	background = nil,
	button = nil,
	can_sell = 0,
	tab = 0,
	goods_type =nil,
	uuid=0,
	sub_category=nil,
	goods=nil,
	skill_action="",
	price = 0,
	is_dummy=false,
	side=nil,
	isRecommanded = false,
	good_attr_config,
}





function GoodsItem:Awake()
	self.go = {} -- create table for ui.
	
	self.name = getComponentInChild(self.transform,"Name","UILabel")
	self.label = getComponentInChild(self.transform,"Label","UILabel")
	self.icon = getComponentInChild(self.transform,"GoodsIcon/Icon","UISprite")
	self.background = self.transform:GetComponent('UISprite')
	self.button = self.transform:GetComponent('UIButton')
	
	self.go.side = getComponentInChild(self.transform,"GoodsIcon/Side","UISprite")
	self.go.good_icon = self.transform:FindChild('GoodsIcon').gameObject
	self.side = getComponentInChild(self.transform,"GoodsIcon/Side","UISprite")
	self.go.select = self.transform:FindChild("Select").gameObject
	self.go.recommand = self.transform:FindChild("Recommand").gameObject;

	-- display the level label
	self.go.level = self.transform:FindChild("GoodsIcon/Level"):GetComponent("UILabel")
	self.go.level_bg = self.transform:FindChild("GoodsIcon/LevelBG"):GetComponent("UISprite")
end



function GoodsItem:start_dummy()
	NGUITools.SetActive( self.name.gameObject,false)
	NGUITools.SetActive( self.icon.gameObject,false)
	NGUITools.SetActive( self.label.gameObject,false)
	NGUITools.SetActive( self.go.side.gameObject,false)
	NGUITools.SetActive( self.go.good_icon,false)
	NGUITools.SetActive( self.side.gameObject,false)

end

function GoodsItem:set_selected(is_selected)
	NGUITools.SetActive(self.go.select,is_selected)
end



function GoodsItem:update_tab()
	local gt = self.goods_type
	if gt == fogs.proto.msg.GoodsCategory.GC_SKILL then
		self.tab = 0
	-- elseif gt == fogs.proto.msg.GoodsCategory.GC_TATTOO then
	-- 	self.tab = 1
	-- 	if self.goods:GetSubCategory() == TattooType.TT_PIECE then
	-- 		self.tab = 3
	-- 	end
	elseif gt == fogs.proto.msg.GoodsCategory.GC_FAVORITE then
		self.tab = 2
	elseif gt == fogs.proto.msg.GoodsCategory.GC_CONSUME then
		self.tab = 3
	end

end


function GoodsItem:refresh()
	self.level = self.goods:GetLevel()
	if self.tab==0 then
		self.go.level.text = "Lv."..tostring(self.level)
	elseif self.tab==1 then
		self.go.level.text = "Lv."..tostring(self.level)
	elseif self.tab==2 then
		local num = self.goods:GetNum()
		self.label.text = 'X'..tostring(num)

	elseif self.tab==3 then
		local num = self.goods:GetNum()
		self.label.text = 'X'..tostring(num)
	end
end

function GoodsItem:start_skill()
	self._skill_attr = GameSystem.Instance.SkillConfig:GetSkill(self.id)

	self.name.text = self._skill_attr.name

	local action_type_index = self._skill_attr.action_type
	local action_type_str = 'ACTION_TYPE_' .. action_type_index
	
	self.label.text = getCommonStr('STR_TYPE')..getCommonStr(action_type_str)

	
	local icon = self.good_attr_config.icon


	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas('Atlas/icon/iconSkill')
	self.icon.spriteName = icon
	self.can_sell = self.good_attr_config.can_sell
end


function GoodsItem:start_material()
	-- local good_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)
	self.name.text = self.good_attr_config.name
	local atlas_name = "iconSkill"
	self.icon.spriteName = self.good_attr_config.icon
	if string.find(self.good_attr_config.icon,"goods") then
		atlas_name = "iconGoods"
	end
	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/icon/".. atlas_name)
	
	local num = self.goods:GetNum()
	self.label.text = 'X'..tostring(num)
end


function GoodsItem:type_to_tattoo_str(t)
	local types ={'TATTOO_TYPE_NECK','TATTOO_TYPE_CHEST','TATTOO_TYPE_ARM','TATTOO_TYPE_BACK','TATTOO_TYPE_PIECE'}

	if t== fogs.proto.msg.TattooType.TT_NECK then
		return types[1]
	elseif t== fogs.proto.msg.TattooType.TT_CHEST then
		return types[2]
	elseif t == fogs.proto.msg.TattooType.TT_ARM then
		return types[3]
	elseif t == fogs.proto.msg.TattooType.TT_BACK then
		return types[4]
	elseif t == fogs.proto.msg.TattooType.TT_PIECE then
		return types[5]
	end
end



function GoodsItem:start_tattoo()

	local tatto_lv_config_data = GameSystem.Instance.TattooConfig:GetTattooConfig(self.id,self.level)
	-- local good_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)

	self.name.text = self.good_attr_config.name

	local tp = self:type_to_tattoo_str(self.sub_category)
	self.label.text = getCommonStr('STR_TYPE')..getCommonStr(tp)

	-- local goods_config_data = GameSystem.Instance.GoodsConfigData
	-- local good_attr_configs = goods_config_data.goodsAttrConfig
	-- local _,good_attr_config = good_attr_configs:TryGetValue(self.id)
	local icon = self.good_attr_config.icon
	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/icon/iconTattoo") 
	
	self.icon.spriteName = icon
	self.can_sell = good_attr_config.can_sell

end



function GoodsItem:start_favorite()
	-- local good_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)

	self.name.text = self.good_attr_config.name

	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas('Atlas/icon/iconPiece')
	if string.find(self.good_attr_config.icon,"goods")  then
		self.icon.atlas = ResourceLoadManager.Instance:GetAtlas('Atlas/icon/iconGoods')
	end
	-- if self.id > 10000 then
	-- 	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas('Atlas/icon/iconGood')
	-- end
	self.icon.spriteName = self.good_attr_config.icon

	local num = self.goods:GetNum()
	self.label.text = 'X'..tostring(num)

end


function GoodsItem:start_consumables()
	-- local good_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)
	self.name.text = self.good_attr_config.name
	self.icon.atlas = ResourceLoadManager.Instance:GetAtlas('Atlas/icon/iconGoods')
	if self.goods:GetSubCategory() == TattooType.TT_PIECE then
		self.icon.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/icon/iconTattoo") 
	end

	self.icon.spriteName = self.good_attr_config.icon

	local num = self.goods:GetNum()
	self.label.text = 'X'..tostring(num)
	
end


function GoodsItem:Start()
	self:set_selected(false)   

	NGUITools.SetActive(self.go.recommand, self.isRecommanded)

	self.is_dummy = not self.goods

	if self.is_dummy then
		self:start_dummy()
		return
	end

	self.id = self.goods:GetID()
	self.level = self.goods:GetLevel()
	self.goods_type = self.goods:GetCategory()
	self.uuid = self.goods:GetUUID()
	self.sub_category = self.goods:GetSubCategory()
	self.level = self.goods:GetLevel()
	local quality = enumToInt(self.goods:GetQuality())
	local labels = {"white","green","blue","purple","golden"}
	self.go.side.spriteName = "com_frame_"..labels[quality]
	
	self.go.level_bg.spriteName = "com_frame_"..labels[quality].."_corner"

	self:update_tab()
	-- local goods_config_data = GameSystem.Instance.GoodsConfigData
	-- local good_attr_configs = goods_config_data.goodsAttrConfig
	-- local _,good_attr_config = good_attr_configs:TryGetValue(self.id)

	self.goods_attr_config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.id)
	self.price = self.good_attr_config.sell_price

	

	if self.tab == 0 then
		self:start_material()
	elseif self.tab == 1 then
		self:start_tattoo()
	elseif self.tab == 2 then
		self:start_favorite()
	elseif self.tab == 3 then
		self:start_consumables()
	end   
	addOnClick(self.transform.gameObject,self:click())


	self:refresh()
end



function GoodsItem:click()
	return function()
		if self.onClick then self:onClick() end
	end
end


return GoodsItem

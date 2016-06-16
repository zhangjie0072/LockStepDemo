-- 20150619_102102



PieceLink =  {
	uiName = 'PieceLink',
	
	go = {},
	piece_id = 0,
	zorder = -300
}




function PieceLink:Awake()
	self.go = {}

	self.go.icon = self.transform:FindChild('Piece'):GetComponent('UISprite')
	self.go.name = self.transform:FindChild('Piece/Name'):GetComponent('UILabel')
	self.go.num = self.transform:FindChild('Piece/Num'):GetComponent('UILabel')
	self.go.grid = self.transform:FindChild('Scroll/Grid'):GetComponent('UIGrid')
	self.go.close = self.transform:FindChild('Close').gameObject

	addOnClick(self.go.close,self:click_close())
end


function PieceLink:set_member_item(item)
	self.member_item = item
end

function PieceLink:click_close()
	return function()
		NGUITools.Destroy(self.gameObject)
	end
end

function Split(str, delim, maxNb)   
	-- Eliminate bad cases...   
	if string.find(str, delim) == nil then  
		return { str }  
	end  
	if maxNb == nil or maxNb < 1 then  
		maxNb = 0	-- No limit   
	end  
	local result = {}  
	local pat = "(.-)" .. delim .. "()"   
	local nb = 0  
	local lastPos   
	for part, pos in string.gfind(str, pat) do  
		nb = nb + 1  
		result[nb] = part   
		lastPos = pos   
		if nb == maxNb then break end  
	end  
	-- Handle the last field   
	if nb ~= maxNb then  
		result[nb + 1] = string.sub(str, lastPos)   
	end  
	return result   
end  



function PieceLink:Start()
	bringNear(self.transform,self.zorder)
	local cur_RQ_data = GameSystem.Instance.RoleQualityConfigData:GetRoleQualityData(self.member_item.id,self.member_item.quality)
	
	local enum_piece = cur_RQ_data.piece_id:GetEnumerator()
	enum_piece:MoveNext()
	self.piece_id = enum_piece.Current
	
	--  todo
	local icon_name ='icon_piece_'..tostring(self.piece_id)

	self.go.icon.spriteName = 'icon_piece_'..tostring(self.piece_id)
	self.go.name.text = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.piece_id).name
	local owned_piece = MainPlayer.Instance:GetGoodsCount(self.piece_id);
	self.go.num.text = getCommonStr('STR_NUMBER')..tostring(owned_piece)
	CommonFunction.ClearGridChild(self.go.grid.transform)
	local role_base_data = GameSystem.Instance.RoleBaseConfigData2:GetConfigData (self.member_item.id)

	local access_way = tostring(role_base_data.access_way)
	local aw_items = Split(access_way,'&')
	CommonFunction.ClearGridChild(self.go.grid.transform)
	for k,v in pairs(aw_items) do
		local aw_item = Split(v,':')
		local t = createUI('PL_item',self.go.grid.transform)
		local script = getLuaComponent(t)
		script.icon_name = icon_name
		script.chapter_id = aw_item[1]
		script.section_id = aw_item[2]

	end
	self.go.grid:Reposition()
end




return PieceLink

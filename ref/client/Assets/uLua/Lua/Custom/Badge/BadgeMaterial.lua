BadgeMaterial = {
	uiName = "BadgeMaterial",
	----Material id----
	materialId = nil,
	needsNum = 0,
	isOk = false,
	-----UI------
	uiIcon,
	uiName,
	uiNeedNum,


}

function BadgeMaterial:Awake( ... )
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiIcon = find("Icon")
	self.uiName = find("Name")
	self.uiNeedNum = find("Num")
end

function BadgeMaterial:Start( ... )
	-- body
end

function BadgeMaterial:FixedUpdate( ... )
	-- body
end

function BadgeMaterial:OnDestroy( ... )
	-- body
end


function BadgeMaterial:SetMaterialId(id)
	self.materialId = id
	if self.materialId then
		local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.materialId)
		if goods then
			self.uiName:GetComponent("UILabel").text = goods.name
			local icon = getLuaComponent(createUI("GoodsIcon", self.uiIcon))
			icon.goodsID = id
			icon.hideNeed = true
		end
	end
end

function BadgeMaterial:SetNeedNum(num)
	local exnum = MainPlayer.Instance:GetGoodsCount(self.materialId)
	local exnumStr
	if num>exnum then 
		exnumStr = "[FF0000]"..exnum.."[-]"
	else
		exnumStr = "[FFFFFF]"..exnum.."[-]"
	end
	self.uiNeedNum:GetComponent("UILabel").text =exnumStr.."/"..num
	if exnum>=tonumber(num) then 
		self.isOk = true
	end
	print("物品现有的数量是："..exnum)
end

return BadgeMaterial









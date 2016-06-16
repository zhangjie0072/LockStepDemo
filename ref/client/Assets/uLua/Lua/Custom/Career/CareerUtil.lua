CareerUtil = {}

function CareerUtil.ListAwards(awards, transform, includeProperty, showvalue, sort)
	local addedID = {}
	for award_id, award_value in pairs(awards) do
		if (includeProperty or award_id > 100) and not addedID[award_id] then
			addedID[award_id] = true
			local goodsIcon = getLuaComponent(createUI("GoodsIcon", transform))
			goodsIcon.goodsID = award_id
			if showvalue == true then
				goodsIcon.hideNum = false
				goodsIcon.num = award_value	
				--warning('ListAwards=>'..tostring(award_value))
				--goodsIcon.hideNeed = true		
			end
			if sort == true then
				goodsIcon.gameObject.name = award_value
			end
			goodsIcon.hideLevel = true
			goodsIcon.hideNeed = true
		end
	end
end

function CareerUtil.ListAwardsNumBottom(awards, transform)
	local awardsNum = {}
	local enum = awards:GetEnumerator()
	while enum:MoveNext() do
		local award = enum.Current
		if not awardsNum[award.award_id] then
			awardsNum[award.award_id] = award.award_value
		else
			awardsNum[award.award_id] = awardsNum[award.award_id] + award.award_value
		end
	end

	local addedID = {}
	enum = awards:GetEnumerator()
	while enum:MoveNext() do
		local id = enum.Current.award_id
		if not addedID[id] then
			local goodsIcon = getLuaComponent(createUI("GoodsIcon", transform))
			goodsIcon.goodsID = id
			goodsIcon.num = awardsNum[id]
			goodsIcon.hideLevel = true
			goodsIcon.hideNum = false
			addedID[id] = true
			warning('ListAwardsNumBottom=>'..tostring(goodsIcon.hideNum))
		end
	end
end

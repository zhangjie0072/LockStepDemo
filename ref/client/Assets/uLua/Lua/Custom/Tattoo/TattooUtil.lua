TattooUtil = {}

function TattooUtil.GetTattooTypeName(tattooType)
	if tattooType == TattooType.TT_NECK then
		return getCommonStr("TATTOO_POSITION_NECK")
	elseif tattooType == TattooType.TT_CHEST then
		return getCommonStr("TATTOO_POSITION_CHEST")
	elseif tattooType == TattooType.TT_ARM then
		return getCommonStr("TATTOO_POSITION_ARM")
	elseif tattooType == TattooType.TT_BACK then
		return getCommonStr("TATTOO_POSITION_BACK")
	end
end

function TattooUtil.GetTattooTypeFromSubCategory(subCategory)
	return TattooType.IntToEnum(tonumber(subCategory))
end

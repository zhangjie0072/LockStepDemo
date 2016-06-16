--encoding=utf-8
require "common/stringUtil"


HonorCompetitionRules =
{
	uiName = 'HonorCompetitionRules',
	
	--------------------------
	uiPopupFrame,
	uiRulesList,
	uiGrid,
};


-----------------------------------------------------------------
--Awake
function HonorCompetitionRules:Awake()
	local transform = self.transform


	self.uiRulesList = transform:FindChild('RulesDetail/RulesList'):GetComponent('UIScrollView')
end

--Start
function HonorCompetitionRules:Start()

end

--Update
function HonorCompetitionRules:Update( ... )
	-- body
end


-----------------------------------------------------------------
---
function HonorCompetitionRules:Init()
	local str = getCommonStr('STR_GAME_RULES_INTRO')
	local rules = Split(str, "\n")
	local topPosition = 0
	for k, v in ipairs(rules or {}) do
		local go = CommonFunction.InstantiateObject('Prefab/GUI/RulesItem', self.uiRulesList.transform)
		if go == nil then
			Debugger.Log('-- InstantiateObject falied ')
			return
		end
		local item = getLuaComponent(go)
		item:SetNum(k)
		item:SetText(v)
		go.transform.localPosition = Vector3.New(0, topPosition, 0);
		local widget = go:GetComponent('UIWidget')
		widget.height = item.uiText.height + 10
		topPosition = topPosition - widget.height
	end
	self.uiRulesList:ResetPosition()
end

function HonorCompetitionRules:OnCloseClick( ... )
	NGUITools.Destroy(self.gameObject)
end

return HonorCompetitionRules
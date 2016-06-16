--encoding=utf-8

SkillAttrInfo = {

	uiName = 'SkillAttrInfo',

	----------------------------------

	isImproveSkill = false,
	hideAfterValue = true,
	skillConfig,
	AttrNameConfig,

	----------------------------------UI
	uiSkillName,
	uiCurrentValue,
	uiAfterValue,
	uiArrow,
	uiAnimator,
}


-----------------------------------------------------------------
function SkillAttrInfo:Awake()
	self.uiSkillName = self.transform:FindChild('Name'):GetComponent('UILabel')
	self.uiCurrentValue = self.transform:FindChild('Value1'):GetComponent('UILabel')
	self.uiAfterValue = self.transform:FindChild('Value2'):GetComponent('UILabel')
	self.uiArrow = self.transform:FindChild('Arrow'):GetComponent('UISprite')
	self.uiAnimator = self.transform:GetComponent('Animator')

	self.skillConfig = GameSystem.Instance.SkillConfig
	self.AttrNameConfig = GameSystem.Instance.AttrNameConfigData
end

function SkillAttrInfo:Start()
end

function SkillAttrInfo:FixedUpdate( ... )
	-- body
end

function SkillAttrInfo:OnClose( ... )
end

function SkillAttrInfo:OnDestroy()
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function SkillAttrInfo:Refresh()

end


-----------------------------------------------------------------

function SkillAttrInfo:SetData(name, value, nextLevelSkill)
	if self.isImproveSkill and self.uiAnimator then
		self.uiAnimator:SetTrigger("New Trigger")
		self.isImproveSkill = false
	end
	self.uiSkillName.text = name
	self.uiCurrentValue.text = '+' .. value

	if value == 0 then
		NGUITools.SetActive(self.gameObject, false)
	end
	
	if nextLevelSkill and not self.hideAfterValue then
		local skillAddAttrs = nextLevelSkill.additional_attrs
		local enum = skillAddAttrs:GetEnumerator()
		while enum:MoveNext() do
			local nextName = self.AttrNameConfig:GetAttrName(enum.Current.Key)
			if nextName == name then
				self.uiAfterValue.text = '+' .. enum.Current.Value
				break
			end
		end
	end
	NGUITools.SetActive(self.uiArrow.gameObject, not self.hideAfterValue)
	NGUITools.SetActive(self.uiAfterValue.gameObject, not self.hideAfterValue)
end

return SkillAttrInfo
 
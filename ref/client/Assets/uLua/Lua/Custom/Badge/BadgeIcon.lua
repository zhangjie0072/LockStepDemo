BadgeIcon = 
{
	uiName = "BadgeIcon",
	badgeId = nil,
	----UI----
	uiIcon,
	uiNumLabel,
	uiMask,
	uiLevelIcon,
	uiEffect,
	uiAnimator,

}

function BadgeIcon:Awake( ... )
	-- body
	local transform = self.transform
	local find = function(name)
		return transform:FindChild(name)
	end
	self.uiIcon = find("Icon"):GetComponent("UISprite")
	self.uiNumLabel = find("Num"):GetComponent("UILabel")
	self.uiLevelIcon = find("LevelIcon"):GetComponent("UISprite")
	self.uiMask = find("Mask")
	self.uiEffect = find("E_BadgeGet")
	self.uiAnimator = find("E_BadgeGet"):GetComponent("Animator")
end

function BadgeIcon:Start( ... )
	
end

function BadgeIcon:SetId(id)
	self.badgeId = id
	local goods = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(self.badgeId)
	if goods then
		self:SetIcon(goods.icon)
		self:SetLevelIcon("tencent_biaoqian"..tostring(goods.quality))
	else
		error("The id of Badge:"..tostring(id).."hasn't defined within goods.xlms ,check please!")
	end
end

function BadgeIcon:SetIcon(iconName)
	-- body
	-- NGUITools.SetActive(self.uiIcon.gameObject,true)
	self.uiIcon.spriteName = iconName
end

function BadgeIcon:SetNum(num)
	NGUITools.SetActive(self.uiNumLabel.gameObject,true)
	self.uiNumLabel.text = num
end

function BadgeIcon:SetMaskVisble(isShow)
	NGUITools.SetActive(self.uiLevelIcon.gameObject,isShow)
end

function BadgeIcon:SetLevelIcon(iconName)
	NGUITools.SetActive(self.uiLevelIcon.gameObject,true)
	self.uiLevelIcon.spriteName = iconName
end

--播放动画---
function BadgeIcon:PlayAnimator( ... )
	if self.uiAnimator then
		NGUITools.SetActive(self.uiEffect.gameObject,true)
		self.uiAnimator:SetBool("TriggerGet", true)
	end
end

return BadgeIcon
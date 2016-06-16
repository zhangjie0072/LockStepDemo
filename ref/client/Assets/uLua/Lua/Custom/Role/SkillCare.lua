------------------------------------------------------------------------
-- class name	 : SkillCare
-- create time   : 20150821_100738
------------------------------------------------------------------------

SkillCare = {
	uiName = "SkillCare",
	-------------------------------
	progress,
	-----------------------------UI
	uiSkillName,
	uiIcon,
	uiIconSkillAttr,
	uiIconFrame,
	uiLock,
	uiProgress,
	uiLockNode,
	uiUnLockNode,
}

function SkillCare:Awake()
	self:UiParse()
end

function SkillCare:Start()
	--body
end

function SkillCare:Refresh()
	--body
end

function SkillCare:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


-- parse the prefeb
function SkillCare:UiParse()
	-- Please Do NOT MIND the comment Lines.
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiIcon = find("Icon"):GetComponent("Transform")

	------------------------------------
	-------  Lock
	self.uiLockNode = find("Lock"):GetComponent("Transform")
	self.uiLockSkillName = find("Lock/SkillName"):GetComponent("UILabel")
	self.uiLockSkillAttr = find("Lock/SkillAttr"):GetComponent("UILabel")
	self.uiLockProgress = find("Lock/ProBack"):GetComponent("UISlider")

	------------------------------------
	-------  UnLock
	self.uiUnLockNode = find("UnLock"):GetComponent("Transform")
	self.uiUnLockSkillName = find("UnLock/SkillName"):GetComponent("UILabel")
	self.uiUnLockSkillLevel = find("UnLock/SkillLevel"):GetComponent("UILabel")
	self.uiUnLockSkillTip1 = find("UnLock/SkillTip"):GetComponent("UILabel")
	self.uiUnLockSkillTip2 = find("UnLock/SkillTip1"):GetComponent("UILabel")
	self.uiUnLockSkillAttr = find("UnLock/SkillAttr"):GetComponent("UILabel")


	------------------------------------
	-------  Old.
	-- self.uiSkillName = self.transform:FindChild("SkillName"):GetComponent("UILabel")
	-- self.uiGrid = self.transform:FindChild("Grid")
	-- self.uiIconSkillAttr = self.transform:FindChild("SkillAttr"):GetComponent("UILabel")
	-- self.uiLock = self.transform:FindChild("Lock"):GetComponent("UISprite")
	-- self.uiProgress = self.transform:FindChild("ProBack"):GetComponent("UIProgressBar")
end


function SkillCare:SetData(roleId,id, progress)
	local quality = 1
	local roleInfo = MainPlayer.Instance:GetRole2(roleId)
	if roleInfo then
		quality = roleInfo.quality
	end

	-- check lock.
	--local skillQuality = GameSystem.Instance.qualityAttrConfig:GetQaulityAttrBySkill(roleId,id).quality
	--local isLock = skillQuality > quality

	local isLock = false
	NGUITools.SetActive(self.uiLockNode.gameObject, isLock )
	NGUITools.SetActive(self.uiUnLockNode.gameObject, not isLock )

	local config = GameSystem.Instance.GoodsConfigData:GetgoodsAttrConfig(id)
	local skillAttr = GameSystem.Instance.SkillConfig:GetSkill(id)

	self.uiUnLockSkillTip1.text = config.intro
	self.uiUnLockSkillTip2.text = config.purpose


	local icon = getLuaComponent(createUI("GoodsIcon",self.uiIcon))
	print("icon.transform.parent.parent.name=",icon.transform.parent.parent.name)
	icon.goodsID = id
	icon.hideLevel = true
	icon.hideNeed = true
	icon.showTips = false
	local scale = icon.transform.localScale
	scale.x = 1.05
	scale.y = 1.05
	icon.transform.localScale = scale

	self.uiLockSkillName.text = config.name
	self.uiUnLockSkillName.text = config.name

	self.progress = progress
	self.uiLockProgress.value = progress
	-- NGUITools.SetActive(self.uiLock.gameObject,isLock)

	if isLock then
		local strs = {
			"STR_UNLOCK_BY_QUALITY_1",
			"STR_UNLOCK_BY_QUALITY_2",
			"STR_UNLOCK_BY_QUALITY_3",
			"STR_UNLOCK_BY_QUALITY_4",
		}

		local limits = {
			2,
			4,
			7,
			11
		}
		local limit = 1
		for i =1,#limits do
			if limits[ i] == skillQuality then
				limit = i
				break
			end
		end
		self.uiLockSkillAttr.text = getCommonStr(strs[limit])
	else

	local skillLevel = skillAttr:GetSkillLevel(1)
	local skillAttr =  skillLevel.additional_attrs
		local enum = skillAttr:GetEnumerator()
		local attrs ={self.uiUnLockSkillAttr}--,self.icon_skill_attr2}

		local index = 1
		while enum:MoveNext() do
			local id = enum.Current.Key
			print("attr id: ".. id )
			local value = enum.Current.Value
			print("attr value: ".. value )

			local name  = GameSystem.Instance.AttrNameConfigData:GetAttrName(id)
			local attr = attrs[index]
			attr.text = name.."  +"..tostring(value)
			index = index + 1
			break
		end
	end
end

return SkillCare

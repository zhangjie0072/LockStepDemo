require "common/StringUtil"
ShootItem = {
	uiName = "ShootItem",
	--ui
	uiIcon,
	uiName,
	uiTip,
	uiTip2,
	--parameters
	onClick,
	matchType,
	isToday = false,
	isTomorrow = false,
}

function ShootItem:Awake()
	self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	self.uiIcon2 = self.transform:FindChild("Icon/Icon2"):GetComponent("UISprite")
	self.uiNameLabel = self.transform:FindChild("Name"):GetComponent("UILabel")
	self.uiTip = self.transform:FindChild("Tip"):GetComponent("UILabel")
	addOnClick(self.gameObject, self:OnClick())
end

function ShootItem:Start()
	local modify = Split(self["Modify"], '/')
	printTable(modify)
	self.uiIcon.spriteName = self[tostring(self.matchType)]
	self.uiIcon2.spriteName = self[tostring(self.matchType)..2]
	self.uiNameLabel.text = getCommonStr(self[tostring(self.matchType) .. "_Name"])
	if self.isToday == true then
		self.uiTip.text = getCommonStr("STR_TODAY_MATCH")
	elseif self.isTomorrow == true then
		self.uiTip.text = getCommonStr("STR_TOMORROW_MATCH")
		self.uiIcon.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiIcon2.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiNameLabel.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiTip.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
	else
		NGUITools.SetActive(self.uiTip.gameObject, false)
		self.uiIcon.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiIcon2.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiNameLabel.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
		self.uiTip.color = Color.New(modify[1]/255,modify[2]/255,modify[3]/255,modify[4]/255)
	end
end

function ShootItem:OnClick()
	return function()
		if self.onClick then self:onClick() return end
		if self.isToday == true then
			return
		else
			CommonFunction.ShowPopupMsg(getCommonStr('GAME_SHOOT_NOT_OPEN'), self.transform,nil,nil,nil,nil)
		end
	end
end

return ShootItem
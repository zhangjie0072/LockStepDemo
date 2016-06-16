------------------------------------------------------------------------
-- class name	: PracticeItem
-- create time   : 20150703_132254
------------------------------------------------------------------------


PracticeItem =  {
	uiName	 = "PracticeItem",
	------------------UI
	uiIcon,
	uiTitle,
	uiIntro,
	uiIntro,
	uiTipTitle,
	uiTips,
	uiGet,
	uiBtnText,
	--------------------parameters
	isGray = true,
	exp	 = 0,
	diamond = 0,
	gold	= 0,
}




function PracticeItem:Awake()
	--local bg = self.transform:FindChild("bg")
	local btnStart = self.transform:FindChild("Btn")
	self.uiBtnText = btnStart:FindChild("TrainLabel"):GetComponent("MultiLabel")
	-- btnText:SetText( getCommonStr("BUTTON_CONFIRM"))
	self.uiIcon = self.transform:FindChild("Icon"):GetComponent("UISprite")
	self.uiTitle = self.transform:FindChild("Title"):GetComponent("UILabel")
	self.uiIntro = self.transform:FindChild("Tip/Intro"):GetComponent("UILabel")
	self.uiIntro.text = getCommonStr("STR_CHALLENGE_DIFFICULT")
	self.uiTipTitle = self.transform:FindChild("Tip/TipTitle"):GetComponent("UILabel")
	self.uiTips = self.transform:FindChild("Tip/Tips"):GetComponent("UILabel")
	self.uiGet = self.transform:FindChild("Get").gameObject
	addOnClick(btnStart.gameObject,self:OnClick())

end

function PracticeItem:OnClick()
	return function()
	if self.onClick then
		self:onClick()
	end
	end
end

function PracticeItem:OnDestroy()

	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end


function PracticeItem:Start()

end

function PracticeItem:SetId(id)
	local icon ={}
	icon[10001] = "practice_c_half match"
	icon[10004] = "practice_c_Shoot"
	icon[10008] = "practice_c_Dunk"
	icon[10009] = "practice_c_Block a Shot"
	icon[10010] = "practice_c_Board"
	----------
	self.id = id
	self.isGray = MainPlayer.Instance:IsPractiseCompleted(self.id)
	self:RefreshGray()

	local config = GameSystem.Instance.PractiseConfig:GetConfig(id)
	self.uiTitle.text = tostring(config.title)
	if self.id == 10001 then
		self.uiTipTitle.text =  getCommonStr("MATCH_PRACTISE_COMPLETION_ONE_MATCH")
		self.uiTips.text = getCommonStr("MATCH_PRACTISE_COMPLETION")
	else
		self.uiTipTitle.text =  tostring(config.num_total)..getCommonStr("SINGLETIMES")..tostring(config.title)
		self.uiTips.text = getCommonStr("STR_COMPLETE_CONDITION"):format(config.num_complete)
	end
	self.uiIcon.spriteName = icon[ self.id]
end

function PracticeItem:RefreshGray()
	local getSprite = self.uiGet
	if not self.isGray then
		NGUITools.SetActive(getSprite,false)
	else
		NGUITools.SetActive(getSprite,true)
		-- self.transform:FindChild("Btn"):GetComponent("UIButton").isEnabled = false
		self.uiBtnText:SetText("MATCH_PRACTISE_FREE")
	end

end

return PracticeItem

-- 20150619_102102


PL_item =  {
	uiName = 'PL_item',

	member_item,

	icon_name ='',
	chapter_id=0,
	section_id=0,
}

function PL_item:Awake()
	self.go={}
	self.go.icon= self.transform:FindChild('Icon'):GetComponent('UISprite')
	self.go.chapter= self.transform:FindChild('Chapter'):GetComponent('UILabel')
	self.go.name= self.transform:FindChild('Name'):GetComponent('UILabel')

	addOnClick(self.gameObject, self:OnLinkClick())
end


function PL_item:set_member_item(item)
	self.member_item = item
end


function PL_item:Start()
	local chapter_index = getCommonStr("STR_CH_"..tostring(self.chapter_id%10000))

	local chapter_str = string.format(getCommonStr("CAREER_CHAPTER"),chapter_index)
	self.go.chapter.text = chapter_str

	local config = GameSystem.Instance.CareerConfigData:GetSectionData(self.section_id)
	self.go.name.text = config.name

	local section_config = GameSystem.Instance.CareerConfigData:GetSectionData(self.section_id)
	local game_mode_id = section_config.game_mode_id
	local match_type = GameSystem.Instance.GameModeConfig:GetGameMode(game_mode_id).matchType
	
	local str_match_type = tostring(match_type)
	self.go.icon.spriteName = "career_Ozd_"..self["Icon_"..str_match_type]
end


--
function PL_item:OnLinkClick()
	return function (go)
		TopPanelManager:ShowPanel("UICareer", self.chapter_id)
	end
end

return PL_item

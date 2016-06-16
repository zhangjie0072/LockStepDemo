require "common/StringUtil"

NoticePopup = {
	uiName = "NoticePopup",
}

function NoticePopup:Awake()
	self.uiTile = self.transform:FindChild("Window/Title1"):GetComponent("UILabel")
	self.uiTipGrid = self.transform:FindChild("Window/Rule")
	self.uiTitleTip = self.transform:FindChild("Window/TitleTip"):GetComponent("UILabel")
	self.uiButton = self.transform:FindChild("Window/Button")

	addOnClick(self.uiButton.gameObject, self:ClickConfirm())
end

function NoticePopup:Start()
	local config = GameSystem.Instance.AnnouncementConfigData:GetOpenItem()
	
	if not config then self.notNotice = true return end
	self.title = config.title
	self.info = config.info
	self.version = config.version
	self.label = config.label

	local text = DynamicStringManager.Instance.NoticePopupString
	local array = Split(text, '@')
	local length = #array

	--标题
	self.uiTile.text = array[1]
	--正文内容
	self.uiTitleTip.text = array[2]

	-- 修改为使用HTTP服务器上的文字描述信息
	-- local tips = Split(self.info, '\n')
	-- local last
	-- local tipNum = #tips
	-- for i = 1, tipNum do
	-- 	local tip = CommonFunction.InstantiateObject("Prefab/GUI/NoticeItem", self.uiTipGrid)
	-- 	tip.transform:FindChild("Label"):GetComponent("UILabel").text = tips[i]
	-- end
end

function NoticePopup:OnDestroy( ... )
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function NoticePopup:ClickConfirm()
	return function()
		if not self.notNotice then
			LoginIDManager.SetAnnouceVersion(self.version)
		end
		NGUITools.Destroy(self.gameObject)
	end
end

return NoticePopup

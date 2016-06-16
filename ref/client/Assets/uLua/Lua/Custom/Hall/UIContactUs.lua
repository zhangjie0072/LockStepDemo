--encoding=utf-8
--联系我们窗口

UIContactUs = 
{
	uiName = 'UIContactUs',
	--------------------
	btnClose,

	goClose,

	animator,

	uiContent,
}

------------------------
function UIContactUs:Awake()

	self.btnClose = self.transform:FindChild('Window/ButtonClose')
	self.goClose = createUI('ButtonClose', self.btnClose)
	self.animator = self.transform:GetComponent('Animator')

	self.uiContent = self.transform:FindChild("Window/Middle/Email"):GetComponent("UILabel")
end

function UIContactUs:Start()

	addOnClick(self.goClose.gameObject, self:ClickClose())

	self.uiContent.text = DynamicStringManager.Instance.ContactUsString

end

function UIContactUs:Refresh()
	-- code here
end

function UIContactUs:OnDestroy()
	Object.Destroy(self.gameObject)
end

--关闭窗口按钮
function UIContactUs:OnClose()
	self:OnDestroy()
end

function UIContactUs:DoClose( ... )
	if self.animator then
		self:AnimClose()
	else
		self:OnClose()
	end
end

--------------------------------------------------
--点击关闭按钮
function UIContactUs:ClickClose( ... )
	return function (go)
		self:DoClose()
	end
end

return UIContactUs
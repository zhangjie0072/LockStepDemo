--encoding=utf-8
--开关设置

SwitchButton = 
{
	uiName = 'SwitchButton',
	--------------------
	--parameters
	onValueChanged,
	--variables
	tfswitch,
	tfOpen,
	tfClose,
	tfUnlock,
	tfText,

	lblText,
	tweener,
	btnSwitch,

	onClickButton,
	CallbackTweenFinish,

	isOpen,
}

----------------------------------
function SwitchButton:Awake()
	self.tfswitch = self.transform:FindChild('ButtonLock')
	self.tfOpen = self.transform:FindChild('ButtonLock/OpenText')
	self.tfClose = self.transform:FindChild('ButtonLock/CloseText')
	self.tfUnlock = self.transform:FindChild('ButtonLock/ButtonUnlock')
	self.tfText = self.transform:FindChild('Text')

	self.tweener = self.tfswitch:GetComponent('UITweener')
	self.btnSwitch = self.tfswitch:GetComponent('UIButton')
	self.lblText = self.tfText:GetComponent('UILabel')

	self.onClickButton = self:ClickButton()

	self.CallbackTweenFinish = LuaHelper.Callback(self:TweenFinish())
end

function SwitchButton:Start()
	addOnClick(self.tfswitch.gameObject, self.onClickButton)

	self.tweener:AddOnFinished(self.CallbackTweenFinish)
end

--根据开关状态刷新按钮
function SwitchButton:Refresh()
	if self.isOpen then
		self.tweener:PlayReverse()
		NGUITools.SetActive(self.tfOpen.gameObject, true)
		NGUITools.SetActive(self.tfClose.gameObject, false)
		NGUITools.SetActive(self.tfUnlock.gameObject, false)
	else
		self.tweener:PlayForward()
		NGUITools.SetActive(self.tfOpen.gameObject, false)
		NGUITools.SetActive(self.tfClose.gameObject, true)
		NGUITools.SetActive(self.tfUnlock.gameObject, true)
	end
end

--[[
function SwitchButton:OnDestroy()
	-- code here
end

function SwitchButton:OnClose()
	-- code here
end

function SwitchButton:DoClose()
	-- code here
end
--]]

----------------------------------------------------
--设定开关状态接口
--参数: state (true:开, false:关)
function SwitchButton:SetState(state)
	self.isOpen = state
	self:Refresh()
end

--设定开关标题接口
--参数类型string
--参数:text
function SwitchButton:SetLabel(text)
	self.lblText.text = text
end

--Tween动画播放结束
function SwitchButton:TweenFinish()
	return function ()
		self.btnSwitch.enabled = true
	end
end

--点击按钮
function SwitchButton:ClickButton()
	return function ()
		self.btnSwitch.enabled = false
		self.isOpen = not self.isOpen
		self:Refresh()

		if self.onValueChanged then
			self.onValueChanged(self.isOpen)
		end
	end
end

return SwitchButton
--class UIwait
--created ...
 UIWait = {
	 uiName = "UIWait",
	 uiSpBg = nil,
	 uiSpAnim = nil,
	 uiSpPoint = nil,
	 bgAlpha = 0.5,
	 AnimSpeed = 1.0,
	 enabled = true,

}
function  UIWait:Awake( )
	-- body
	if self.enabled then
		self.uiSpBg = getComponentInChild(self.transform, "Mask", "UISprite")
		self.uiSpAnim = getComponentInChild(self.transform,"Icon","UISprite")
		self.uiSpPoint = getComponentInChild(self.transform,"Point","UISprite")
		self.uiSpBg.alpha = self.bgAlpha
		self.gameObject:SetActive(false)
	else
		GameObject.Destroy(self.gameObject)
	end
end
return UIWait
TipWithTween = {
	uiName = 'TipWithTween',
	message = "",

}


function TipWithTween:Awake()
	self.uiMessage = self.transform:FindChild("Info"):GetComponent("UILabel")
end

function TipWithTween:Start()
	-- GameObject:Destroy(self.transform.gameObject, 2)
end

function TipWithTween:SetMessage(message)
	self.message = message
	self.uiMessage.text = self.message
	GameObject.Destroy(self.transform.gameObject, 1)
end

return TipWithTween

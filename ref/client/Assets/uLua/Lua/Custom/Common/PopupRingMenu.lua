PopupRingMenu = {
	uiName = 'PopupRingMenu',
	
	itemImages = {},
	startAngle = 30,
	deltaAngle = 40,
	-- function OnClick(index)
	onClick = nil,
	onClickFullScreen = nil,

}

function PopupRingMenu:Awake()
	self.tmRing = self.transform:FindChild("Ring")
	self.items = {}
	self.items[1] = self.tmRing:FindChild("Item1")
	NGUITools.SetActive(self.items[1].gameObject, false)
	self.radius = self.tmRing:FindChild("Fan"):GetComponent("UIWidget").width

	addOnClick(self.gameObject,self:clickFullScreen())
end

function PopupRingMenu:Start()
	local itemCount = table.getn(self.itemImages)

	for i = 2, itemCount do
		self.items[i] = CommonFunction.InstantiateObject(self.items[1].gameObject, self.tmRing).transform
		addOnClick(self.items[i].gameObject, self:MakeOnClick(i))
	end
	addOnClick(self.items[1].gameObject, self:MakeOnClick(1))

	for i = 1, itemCount do
		local imageInfo = self.itemImages[i]
		local tmItem = self.items[i]
		if imageInfo.sprite then
			local tmBG = tmItem:FindChild("BG")
			local sprite = tmItem:FindChild("Sprite"):GetComponent("UISprite")
			local text = tmItem:FindChild("Sprite/Text"):GetComponent("UISprite")

			--set position and rotation
			local angle = self.startAngle + self.deltaAngle * (i - 1)
			local rot = Quaternion.AngleAxis(angle, Vector3.forward)
			local pos = rot * Vector3.up * self.radius
			tmItem.localPosition = pos
			tmBG.localRotation = rot

			sprite.atlas = ResourceLoadManager.Instance:GetAtlas("Atlas/" .. imageInfo.atlas)
			sprite.spriteName = imageInfo.sprite
			sprite:MakePixelPerfect()

			if imageInfo.offset then
				local position = sprite.transform.localPosition
				position.x = imageInfo.offset.x
				position.y = imageInfo.offset.y
				sprite.transform.localPosition = position
			end
			if imageInfo.textSprite then
				text.spriteName = imageInfo.textSprite
				text.atlas = sprite.atlas
				text:MakePixelPerfect()
				if imageInfo.textOffset then
					local position = text.transform.localPosition
					position.x = imageInfo.textOffset.x
					position.y = imageInfo.textOffset.y
					text.transform.localPosition = position
				end
				NGUITools.SetActive(text.gameObject, true)
			else
				NGUITools.SetActive(text.gameObject, false)
			end
			NGUITools.SetActive(tmItem.gameObject, true)
		else
			NGUITools.SetActive(tmItem.gameObject, false)
		end
	end
end

function PopupRingMenu:MakeOnClick(index)
	return function ()
		if self.onClick then self.onClick(index) end
	end
end

function PopupRingMenu:clickFullScreen()
	return function ()
		if self.onClickFullScreen then self.onClickFullScreen() end
	end
end

return PopupRingMenu

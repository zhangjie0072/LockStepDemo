Scene = {
	uiName = 'Scene',

	targetUI,
	subID,
	params = {},
	purpose,					-- UI or MATCH.
}

function Scene:Start()
	if self.purpose ~= "UI" then
		return
	end

	if Scene.targetUI then
		TopPanelManager:ShowPanel(Scene.targetUI, Scene.subID, Scene.params)
		Scene.targetUI = nil
		Scene.subID = nil
		Scene.params = nil
	else
		if Application.loadedLevelName ~= 'Startup' then
			TopPanelManager:ShowPanel("UIHall", Scene.subID, Scene.params)
		end
	end
end

function Scene:OnDestroy()
	local ui = UIManager.Instance
	if ui then
		ui:DestroyBasePanelObjects()
	end
end


return Scene

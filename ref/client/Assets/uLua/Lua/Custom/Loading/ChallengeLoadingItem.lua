--encoding=utf-8

ChallengeLoadingItem = {
  uiName = 'ChallengeLoadingItem',
  ----------------------UI
  uiIcon,
  uiPlayerName,
  uiPosition,
  uiBg,
  uiCombat,
  uiLoadingState,
  ---------------------parameters
  id,
  loadingState = nil,
  loadDone = nil,
  power = 0,
  name = nil,
}


-----------------------------------------------------------------
--Awake
function ChallengeLoadingItem:Awake( ... )
  self.uiIcon = self.transform:FindChild("Icon/CareerRoleIcon")
  self.uiPlayerName = self.transform:FindChild("Name"):GetComponent("UILabel")
  self.uiPosition = self.transform:FindChild("Position"):GetComponent("UILabel")
  self.uiBg = self.transform:FindChild('Background'):GetComponent('UISprite')
  self.uiLoadingState = self.transform:FindChild('Loading'):GetComponent('UILabel')
  --self.uiCombat = self.transform:FindChild('Combat/Power'):GetComponent('UILabel')
  self.uiCombat = self.transform:FindChild('Power'):GetComponent('UILabel')
end

function ChallengeLoadingItem:Start()
  local positions ={'PF','SF','C','PG','SG'}
  local player = getLuaComponent(self.uiIcon)
  player.id = self.id
  player.showPosition = false
  player.isShowName = false

  self.uiCombat.text = self.power
  --self.uiLoadingState.text = self.loadingState
  --local roleConfig = GameSystem.Instance.RoleBaseConfigData2:GetConfigData(self.id)
  --self.uiPostion.spriteName = 'PT_'..positions[roleConfig.position]
  --self.uiPlayerName.text = roleConfig.name

end

function ChallengeLoadingItem:FixedUpdate( ... )
  self.uiLoadingState.text = self.loadingState
  if self.loadDone then
    self.uiLoadingState.color = Color.green
  end
    
end

return ChallengeLoadingItem

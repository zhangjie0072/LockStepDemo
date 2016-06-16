------------------------------------------------------------------------
-- class name    : HallMatch
-- create time   : 20150711_110010
------------------------------------------------------------------------

HallMatch =  {
	uiName = "HallMatch", 

}

function HallMatch:Awake()
	self.go = {} -- create this for contained gameObject
	self.go.pve_box = self.transform:FindChild("BoxPVE")
	self.go.pve = self.transform:FindChild("BoxPVE/PVE"):GetComponent("UISprite")

	self.go.ladder_box = self.transform:FindChild("BoxLadder")
	self.go.ladder = self.transform:FindChild("BoxLadder/Ladder"):GetComponent("UISprite")

	self.go.train_box = self.transform:FindChild("BoxTrain")
	self.go.train = self.transform:FindChild("BoxTrain/Train"):GetComponent("UISprite")

	self.go.match_box = self.transform:FindChild("BoxMatch")
	self.go.match = self.transform:FindChild("BoxMatch/Match"):GetComponent("UISprite")

	addOnClick(self.go.pve_box.gameObject,self:click_pve())
	addOnClick(self.go.ladder_box.gameObject,self:click_ladder())
	addOnClick(self.go.train_box.gameObject,self:click_train())
	addOnClick(self.go.match_box.gameObject,self:click_match())
end


function HallMatch:click_pve()
	return function()
		self:HideMenu()
		self:set_all_gray(true)
		self:set_pve_gray(false)
		
		self.uiMenu = getLuaComponent(createUI("PopupRingMenu",self.go.pve.transform)) 
		self.uiMenu.itemImages = { 
			{ atlas ="Hall/Hall",
			  sprite = "hall_career",
			  textSprite = "hall_career_text" },
			{ atlas = "Hall/Hall", 
			  sprite = "hall_tour", 
			  offset = {x = 20, y = 4,}, 
			  textSprite ="hall_tour_text",
			  textOffset = {x = -24, y = -79,},
			}, 
		}

		self.uiMenu.startAngle = 45 self.uiMenu.deltaAngle = 90
		self.uiMenu.onClick = self:MakeOnPVEMenuItemClick()
		self.uiMenu.onClickFullScreen = self:click_full_screen()
		UIManager.Instance:BringPanelForward(self.uiMenu.gameObject)
		-- self.btnPVE.enabled = false
		self.go.pve_box:GetComponent("UIButtonScale").enabled = false
		NGUITools.BringForward(self.go.pve_box.gameObject)
	end
end


function HallMatch:set_pve_gray(is_gray)
	local color = Color.white
	if is_gray then 
		color = Color.gray
	end
	self.go.pve.color = color
end

function HallMatch:set_ladder_gray(is_gray)
	local color = Color.white
	if is_gray then 
		color = Color.gray
	end
	self.go.ladder.color = color
end

function HallMatch:set_match_gray(is_gray)
	local color = Color.white
	if is_gray then 
		color = Color.gray
	end
	self.go.match.color = color
end

function HallMatch:set_train_gray(is_gray)
	local color = Color.white
	if is_gray then 
		color = Color.gray
	end
	self.go.train.color = color
end

function HallMatch:set_all_gray( is_gray)
	self:set_match_gray(is_gray)
	self:set_pve_gray(is_gray)
	self:set_ladder_gray(is_gray)
	self:set_train_gray(is_gray)
end

function HallMatch:MakeOnPVEMenuItemClick()
	return function (index)
		self:HideMenu()
		if index == 1 then
			TopPanelManager:ShowPanel("UICareer")
		elseif index == 2 then
			TopPanelManager:ShowPanel("UITour")
		end
	end
end

function HallMatch:click_ladder()
	return function()
		--入口条件（未添加）
		--[[	
		CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("IN_CONSTRUCTING"), nil, nil,nil);
		NGUITools.BringForward(self.go.train_box.gameObject)
		--]]
		self:HideMenu()
		TopPanelManager:ShowPanel("UIQualifying")
	end
end

function HallMatch:click_full_screen()
	return function()
		self:HideMenu()
	end
end

function HallMatch:refresh()
	self:HideMenu()
end

function HallMatch:click_match()
	return function()
		self:HideMenu()
		self:set_all_gray(true)
		self:set_match_gray(false)
		self.uiMenu = getLuaComponent(createUI("PopupRingMenu", self.go.match.transform))
		self.uiMenu.itemImages = {
			{
				atlas = "Hall/Hall", 
				sprite = "hall_honorCompetition",
				textSprite = "hall_honorCompetition_text",
				textOffset = {x = 0, y = -79,},
			},
			
			{
				atlas = "Hall/Hall", 
				sprite = "hall_honorCompetition",
				textSprite = "hall_pvp_text",
				textOffset = {x = 0, y = -79,},
			}
		}
		self.uiMenu.startAngle = -45
		self.uiMenu.deltaAngle = -60
		
		self.uiMenu.onClick = self:MakeOnMatchMenuItemClick()
		self.uiMenu.onClickFullScreen = self:click_full_screen()
		UIManager.Instance:BringPanelForward(self.uiMenu.gameObject)
		-- self.btnMatch.enabled = false
		self.go.match_box:GetComponent("UIButtonScale").enabled = false
		NGUITools.BringForward(self.go.match_box.gameObject)
	end
end

function HallMatch:MakeOnMatchMenuItemClick()
	return function (index)
		self:HideMenu()
		if index == 1 then
			TopPanelManager:ShowPanel('UIHonorCompetition')
		end
		if index == 2 then
			TopPanelManager:ShowPanel('UIPVPCompetition')
		end
	end
end



function HallMatch:click_train()
	return function()
		self:HideMenu()
		TopPanelManager:ShowPanel("UIPractice",nil)
		NGUITools.BringForward(self.go.train_box.gameObject)
	end
end


function HallMatch:HideMenu()
	if self.uiMenu then 
		local parent = self.uiMenu.transform.parent
		parent.localScale = Vector3.one
		NGUITools.Destroy(self.uiMenu.gameObject)
		self.uiMenu = nil
		-- self.btnPVE.enabled = true
		self.go.pve_box:GetComponent("UIButtonScale").enabled = true
		-- self.btnMatch.enabled = true
		self.go.match_box:GetComponent("UIButtonScale").enabled = true
		-- self.btnRank.enabled = true
		self.go.ladder_box:GetComponent("UIButtonScale").enabled = true
		-- self.btnPractise.enabled = true
		self.go.train_box:GetComponent("UIButtonScale").enabled = true
		self:set_all_gray(false)
		
		NGUITools.BringForward(self.go.match_box.gameObject)
		local depth = self.go.match_box:GetComponent("UIPanel").depth
		self.go.pve_box:GetComponent("UIPanel").depth = depth + 1
		self.go.ladder_box:GetComponent("UIPanel").depth =  depth + 2
		self.go.train_box:GetComponent("UIPanel").depth = depth + 3
	end
end

function HallMatch:OnDestroy()
	--TODO: add process here.
	Object.Destroy(self.uiAnimator)
	Object.Destroy(self.transform)
	Object.Destroy(self.gameObject)
end

function HallMatch:Start()
	self:set_all_gray(false)
end

return HallMatch

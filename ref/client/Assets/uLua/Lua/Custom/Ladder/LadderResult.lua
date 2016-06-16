------------------------------------------------------------------------
-- class name    : LadderResult
-- create time   : Wed Mar  9 15:59:37 2016
------------------------------------------------------------------------

LadderResult =  {
    uiName     = "LadderResult",
    --------------------------------------------------------------------
    -- UI Module: Name Start with 'ui',  such as uiButton, uiClick    --
    --------------------------------------------------------------------
    uiWinNode      = nil,
    uiLoseNode     = nil,
    uiShowOffBtn   = nil,
    uiConfirmBtn   = nil,
    uiLoseConfirmBtn = nil,
    uiAcquireGoods = nil,
    uiAcquireLabel = nil,


    -----------------------
    -- Parameters Module --
    -----------------------
    isWin = true,
    ladderRewardWin,
    league_extra_score,

}




---------------------------------------------------------------
-- Below functions are system function callback from system. --
---------------------------------------------------------------
function LadderResult:Awake()
    self:UiParse()				-- Foucs on UI Parse.
end


function LadderResult:Start()
    addOnClick(self.uiShowOffBtn.gameObject, self:ClickShowOff())
    addOnClick(self.uiConfirmBtn.gameObject, self:ClickConfirm())
    addOnClick(self.uiLoseConfirmBtn.gameObject, self:ClickConfirm())

    local ladderLevel = GameSystem.Instance.ladderConfig:GetLevelByScore(MainPlayer.Instance.pvpLadderScore)
    self.uiAcquireGoods.spriteName = ladderLevel.icon
    self.uiAcquireLabel.text = ladderLevel.name
    self:Refresh()
end

function LadderResult:Refresh()
    self.uiWinNode.gameObject:SetActive(self.isWin)
    self.uiLoseNode.gameObject:SetActive(not self.isWin)
end

-- uncommoent if needed
-- function LadderResult:FixedUpdate()

-- end


function LadderResult:OnDestroy()

end


--------------------------------------------------------------
-- Above function are system function callback from system. --
--------------------------------------------------------------

function LadderResult:ClickShowOff()
    return function()

    end
end



function LadderResult:ClickConfirm()
    return function()
        print("1927 - <LadderResult>  self.ladderRewardWin.Count, self.league_extra_score=",self.ladderRewardWin.Count, self.league_extra_score)
        -- if self.ladderRewardWin.Count == 5 and self.league_extra_score == 1 then
        --     self.nextShowUI = "LadderAcquire"
        --     self.nextShowUIParams = { ladderRewardWin = self.ladderRewardWin
        --     }
        -- else
        -- self.nextShowUI = "UIHall"
        -- end

        -- if self.ladderRewardWin.Count ~= 5 then
        --	self.nextShowUI = "UIHall"
        -- else
        --	self.nextShowUI = "LadderAcquire"
        --	self.nextShowUIParams = { ladderRewardWin = self.ladderRewardWin
        --	}
        -- end
        if self.uiAnimator then
            self:AnimClose()
        else
            self:OnClose()
        end
    end
end

function LadderResult:OnClose()
    Ladder.PrepareBackToLadder()
    -- if self.nextShowUI then
    --     if self.nextShowUI=="UIHall" then
    --         jumpToUI("UIHall", nil, nil)
    --         return
    --     end
    --     TopPanelManager:ShowPanel(self.nextShowUI, self.nextShowUISubID, self.nextShowUIParams)
    --     self.nextShowUI = nil
    -- else
    --     TopPanelManager:HideTopPanel()
    -- end
end





---------------------------------------------------------------------------------------------------
-- Parse the prefab and extract the GameObject from it.                                          --
-- Such as UIButton, UIScrollView, UIGrid are all GameObject.                                    --
-- NOTE:                                                                                         --
--	1. This function only used to parse the UI(GameObject).                                      --
--	2. The name start with self.ui which means is ONLY used for naming Prefeb.                   --
--	3. The name is according to the structure of prefab.                                         --
--	4. Please Do NOT MINDE the Comment Lines.                                                    --
--	5. The value Name in front each Line will be CHANGED for other SHORT appropriate name.       --
---------------------------------------------------------------------------------------------------
function LadderResult:UiParse()
    self.uiWinNode      = self.transform:FindChild("Window/Result/WinNode"):GetComponent("Transform")
    self.uiLoseNode     = self.transform:FindChild("Window/Result/LoseNode"):GetComponent("Transform")
    self.uiShowOffBtn   = self.transform:FindChild("Window/Result/WinNode/ShowOffBtn"):GetComponent("UIButton")
    self.uiConfirmBtn   = self.transform:FindChild("Window/Result/WinNode/ConfirmBtn"):GetComponent("UIButton")
    self.uiLoseConfirmBtn = self.transform:FindChild("Window/Result/LoseNode/ConfirmBtn"):GetComponent("UIButton")
    self.uiAcquireGoods = self.transform:FindChild("Window/Top/Acquire/AcquireGoods"):GetComponent("UISprite")
    self.uiAcquireLabel = self.transform:FindChild("Window/Top/Acquire/AcquireLabel"):GetComponent("UILabel")
end

return LadderResult

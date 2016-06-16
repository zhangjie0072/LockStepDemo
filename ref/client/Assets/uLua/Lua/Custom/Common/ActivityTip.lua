ActivityTip =  {
    uiName     = "ActivityTip",

}

function ActivityTip:Awake()

end


function ActivityTip:Start()
	addOnClick(self.gameObject,self:OnClick())
end

function ActivityTip:Refresh()
	
end

-- uncommoent if needed
-- function ActivityTip:FixedUpdate()

-- end


function ActivityTip:OnDestroy()

end

function ActivityTip:UiParse()

end

function ActivityTip:OnClick()
    return function ()
        NGUITools.Destroy(self.gameObject)
    end
end

return ActivityTip

require "Custom/Activity/ActivityAnnounceData"
local UIActivityAnnounce =  {
	uiName	= "UIActivityAnnounce",
	m_bDirty = false,
	parent,
	uiLblTitle ,	--顶部title
	uiSpBackground,	--背景图
	uiClickArea,	--点击区域
	announceIndex = 0 ,--显示的第几个活动公告

}
local logLevel = function () end--warning
function UIActivityAnnounce:Start( )
	-- body
	-- if self.uiClickArea then 
 --    	addOnClick(self.uiClickArea, self:DoClose())
	-- end
	-- logLevel("UIActivityAnnounce:Start( )")
	self:Reset()
end
function UIActivityAnnounce:Refresh( ... )
	-- body

end
function UIActivityAnnounce:Dirty( ... )
	-- body
end
function UIActivityAnnounce:LateRefresh( ... )
	-- body
	return function ( ... )
		-- body
		--
	end
end
function UIActivityAnnounce:SetParent( parent )
	-- body
	self.parent = parent
end

function UIActivityAnnounce:Reset( ... )
	-- body
	if self.announceIndex <= #ActivityAnnounceData.ActivityAnnoucementList then
		self.announceIndex = self.announceIndex + 1
		local data = ActivityAnnounceData.ActivityAnnoucementList[self.announceIndex]
		if data then
			local m_uiRootBasePanel = UIManager.Instance.m_uiRootBasePanel
			local go = GameObject.Instantiate(data.mainAsset)
			if go then 
				go.transform.parent = m_uiRootBasePanel.transform
				go.transform.localPosition = Vector3.New(0,0,0)
				go.transform.localScale = Vector3.New(1,1,1)
				local uicom = go.transform:FindChild('GameObject').gameObject:AddComponent("UICloseOnClick")
				if	uicom then
					uicom.closeCallBack = self:DoClose()
					return true
				end
    			UIManager.Instance:BringPanelForward(go.gameObject)
			else
				warning('assetbundle ',data,' cant be Instantiated!')
			end
		end
		self:Reset()
		return false
	else
		self:OnClose()
	end

end
function UIActivityAnnounce:DoClose( ... )
	-- body
	return function ( ... )
		-- body
		if self.announceIndex <= #ActivityAnnounceData.ActivityAnnoucementList then
			self:Reset()
		else
			--活动已经显示完，退出活动公告界面
			self:OnClose()
		end
	end
	
end
--界面关闭时回调函数
function UIActivityAnnounce:OnClose()
	if self.onClose then
		--print("uiBack",self.uiName,"--:",self.onClose)
		self.onClose()
		self.onClose = nil
	else

	end
	ActivityAnnounceData.AnnounceAlreadyRead = true
	self = nil
	-- Resources:UnloadUnusedAssets()
end
function UIActivityAnnounce:OnDestroy( ... )
	-- body
end
return UIActivityAnnounce
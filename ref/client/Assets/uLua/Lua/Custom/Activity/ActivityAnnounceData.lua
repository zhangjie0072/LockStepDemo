local modname = "ActivityAnnounceData"

module(modname, package.seeall)

ActivityAnnoucementList = {}	--活动公告列表
AnnounceAlreadyRead = false		--活动是否显示

-- local logLevel = warning
function initialize( ... )
	-- body
	local activities = DynamicStringManager.Instance:getActivityLuaTable()
	for i=1,3 do
		ActivityAnnoucementList[i] = activities[i]
	end
end
initialize()
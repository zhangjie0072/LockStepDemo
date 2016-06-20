local modname = "RankList"

module(modname, package.seeall)

--parameters
onListRefreshed = nil
onUITaskRefreshed = nil

--variables
rankLists = {}		-- {rankType -> rankInfoList}
myRankInfos = {}	-- {rankType -> rankInfo}

firstRefreshCallback = true

function Initialize()
	LuaHelper.RegisterPlatMsgHandler(MsgID.RankListRespID, HandleRankListResp, modname)

	warning('LuaHelper.RegisterPlatMsgHandler(MsgID.RankListRespID, HandleRankListResp, modname)')
end

function GetList(rankType)
	return rankLists[rankType]
end

function GetMyRankInfo(rankType)
	return myRankInfos[rankType]
end

function ReqList(rankType)
	-- print(modname, "ReqRankList", rankType)
	-- local req = {
	-- 	type = tostring(rankType)
	-- }

	-- if firstRefreshCallback then
	-- 	firstRefreshCallback = false
	-- end
	-- local buf = protobuf.encode("fogs.proto.msg.RankListReq", req)
	-- CommonFunction.ShowWait()
	-- LuaHelper.SendPlatMsgFromLua(MsgID.RankListReqID, buf)
end

function HandleRankListResp(buf)
	print(modname, "HandleRankListResp")
	local resp, err = protobuf.decode("fogs.proto.msg.RankListResp", buf)
	CommonFunction.StopWait()
	if resp then
		if resp.result == 0 then
			local rankType = RankType[resp.type]
			print(modname, "RankType:", rankType, "count:", table.getn(resp.rank_list))
			rankLists[rankType] = resp.rank_list

			local myAccountID = MainPlayer.Instance.AccountID
			local myRankInfo
			for _, v in ipairs(resp.rank_list) do
				if v.acc_id == myAccountID then
					myRankInfo = v
					break
				end
			end
			if not myRankInfo then
				myRankInfo = MakeMyRankInfo(rankType)
			end
			myRankInfos[rankType] = myRankInfo

			print(modname, "onListRefreshed", onListRefreshed)
			if onListRefreshed then
				onListRefreshed(rankType, resp.rank_list, myRankInfo)
			end
			if onUITaskRefreshed then
				onUITaskRefreshed(myRankInfo)
			end
		else
			CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result),nil)
		end
	else
		error(modname, "decode error," , err)
	end
end

function MakeMyRankInfo(rankType)
	if MainPlayer.Instance.SquadInfo.Count > 0 then 
		local rankInfo = {
			acc_id = MainPlayer.Instance.AccountID,
			ranking = 0,
			name = MainPlayer.Instance.Name,
			level = MainPlayer.Instance.Level,
			show_id = tostring(MainPlayer.Instance.SquadInfo:get_Item(0).role_id),
		}
		if rankType == RankType.RT_QUALIFYING_NEW then
			rankInfo.points = MainPlayer.Instance.qualifying_new.score
		elseif rankType == RankType.RT_LADDER then
			rankInfo.points = MainPlayer.Instance.pvpLadderScore
		elseif rankType == RankType.RT_ACHIEVEMENT then
			rankInfo.points = MainPlayer.Instance.Honor2
		end
		return rankInfo
	end
end

-- 服务器每小时推送一次，不需要请求
-- function RegisterRefreshCallback()
-- 	local mTime = os.date('%M', GameSystem.mTime)
-- 	local sTime = os.date('%S', GameSystem.mTime)
-- 	local waitTime = 3600 - (mTime*60+sTime)
-- 	Scheduler.Instance:AddTimer(waitTime, false, LuaHelper.Action(RefreshRankList))
-- end

function RefreshRankList()
	for k, v in pairs(rankLists) do
		ReqList(k)
	end
	print(modname, "RefreshRankList")
	Scheduler.Instance:RemoveTimer(LuaHelper.Action(RefreshRankList))
	-- RegisterRefreshCallback()
end

Initialize()

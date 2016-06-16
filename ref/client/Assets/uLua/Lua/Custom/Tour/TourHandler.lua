TourHandler = {}

local function CompleteHandler(buf)
	--[[
	local resp, err = protobuf.decode("fogs.proto.msg.TourCompleteResp", buf)
	if resp then
		if resp.result == 0 then
			if resp.direct_clear == 1 then	--正常结束由结算界面处理
				MainPlayer.Instance.MaxTourID = resp.max_tour_id
				MainPlayer.Instance.CurTourID = MainPlayer.Instance.CurTourID + 1
			end
		end
	else
		error("UITour:", err)
	end
	--]]
	local resp, err = protobuf.decode("fogs.proto.msg.ExitGameResp", buf)
	if resp then
		if resp.type == 'MT_TOUR' and resp.tour.result == 0 then
			if resp.tour.direct_clear == 1 then	--正常结束由结算界面处理
				MainPlayer.Instance.MaxTourID = resp.tour.max_tour_id
				MainPlayer.Instance.CurTourID = MainPlayer.Instance.CurTourID + 1
			end
		end
	else
		error("UITour:", err)
	end
end

function TourHandler.Register()
	LuaHelper.RegisterPlatMsgHandler(MsgID.ExitGameRespID, CompleteHandler, "TourHandler")
end

TourHandler.Register()

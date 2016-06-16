local modname = "TourNewData"
module(modname, package.seeall)
TourNewGradesAwardsCallBack = nil
function HandleGetTourNewGradesAwardsResp()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.GetTourNewGradesAwardsResp", buf)
		CommonFunction.StopWait()
		if not resp then
			error(err)
			return
		end
		if TourNewGradesAwardsCallBack ~= nil then
			TourNewGradesAwardsCallBack(resp)
		else
			print('TourNewGradesAwardsCallBack is nil ')
		end
		



	end
end
LuaHelper.RegisterPlatMsgHandler(MsgID.GetTourNewGradesAwardsRespID, HandleGetTourNewGradesAwardsResp(), modname)

local modname = "LuaPlayerData"
module(modname, package.seeall)

--上一次胜利的时间（0特殊值，表示首胜奖励不可用，且在大厅不显示）
last_win_time = 0

awards = nil

function FirstWinInfoHandler()
	return function(buf)
		local resp, err = protobuf.decode("fogs.proto.msg.FirstWinInfo", buf)
		CommonFunction.StopWait()
		if not resp then
			error(err)
			return
		end

		last_win_time = resp.last_time
		awards = resp.awards

		--添加首胜推送消息
		local firstwinCD = GameSystem.Instance.CommonConfig:GetUInt("gFirstWinLastTime")
		local diff = firstwinCD*60-(GameSystem.mTime - last_win_time)
		--如果首胜CD低于30秒不进行推送
		print('FirstWinInfoHandler '..last_win_time..',diff'..diff..',cd '..firstwinCD)
		-- if diff <0 then diff =0 end -- 测试
		if diff > 30 then
			-- local date = 0
			-- local  time = 0
			-- if diff>60*60*24 then
			-- 	date = math.floor(diff/(60*60*24))
			-- 	diff = diff - 60*60*24*date
			-- end
			-- local  timeNow = math.floor(GameSystem.mTime%(24*60*60))
			-- if timeNow + diff > 3600*24 then
			-- 	time = timeNow+diff-3600*24
			-- 	date = date + 1
			-- else
			-- 	time = diff
			-- end

			local  content = CommonFunction.GetConstString("STR_LOADING_TIPS_12")
			CommonFunction.SetPushInfo(10012,5,0,0,diff ,content)
		end



		warning(last_win_time)
	end
end

LuaHelper.RegisterPlatMsgHandler(MsgID.FirstWinInfoID, FirstWinInfoHandler(), modname)
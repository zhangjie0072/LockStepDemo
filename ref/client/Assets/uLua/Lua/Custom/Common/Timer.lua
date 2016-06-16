Timer = {}

Timer.__index = Timer

function Timer.New(time, func, loop)
	local t = {
		time = time,
		remainTime = time,
		func = func,
		loop = loop,
		playing = true,
	}
	setmetatable(t, Timer)
	return t
end

function Timer:Update(deltaTime)
	if not self.playing then return end
	self.remainTime = self.remainTime - deltaTime
	if self.remainTime <= 0 then
		self:func()
		if not self.loop then
			self:Stop()
		else
			self.remainTime = self.time
		end
	end
end

function Timer:Stop()
	self.playing = false
	self.remainTime = 0
end

function Timer:IsStopped()
	return not self.playing and self.remainTime == 0
end


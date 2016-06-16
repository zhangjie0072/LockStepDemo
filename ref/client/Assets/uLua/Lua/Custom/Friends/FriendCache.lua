
--[[
-- class.lua
-- Compatible with Lua 5.1 (not 5.0).
function Class(base, _ctor)
    local c = { }
    return xClass(c, base, _ctor);
end

function xClass(c, base, _ctor)
    -- a new class instance
    if not _ctor and type(base) == 'function' then
        _ctor = base
        base = nil
    elseif type(base) == 'table' then
        -- our new class is a shallow copy of the base class!
        for i, v in pairs(base) do
            c[i] = v
        end
        c._base = base
    end

    -- the class will be the metatable for all its objects,
    -- and they will look up their methods in it.
    c.__index = c

    -- expose a constructor which can be called by <classname>(<args>)
    local mt = { }

    mt.__call = function(class_tbl, ...)
        local obj = { }
        setmetatable(obj, c)

        if _ctor then
            _ctor(obj, ...)
        end
        return obj
    end

    c._ctor = _ctor
    c.is_a = function(self, klass)
        local m = getmetatable(self)
        while m do
            if m == klass then return true end
            m = m._base
        end
        return false
    end
    setmetatable(c, mt)
    return c
end
]]
--好友缓存信息类
local autoReleaseTotal = 300
local autoReleaseDelt = 60
local friendCache = 
{
}
local function autoRelease( o )
		-- body
		return function ()
			-- body
			-- if not o then return end
			o.autoReleaseDuration = o.autoReleaseDuration - autoReleaseDelt
			if o.autoReleaseDuration > 0 then
				Scheduler.Instance:AddTimer(autoReleaseDelt, false, autoRelease(o))
				return
			end

			if o.ref then
				for k,v in pairs(o.ref) do
					if v == o then
						print('remove friend cache '..v.resp.acc_id..',name '..v.resp.name)
						table.remove(o.ref,k)						
						return
					end
				end
				-- error('friendCache id '..o.resp.acc_id..' was trying to release self but not contained in ref object!')
				o = nil	

			else
				-- error('friendCache id '..o.resp.acc_id..' was trying to release self but ref object does not exist!')
				o  = nil
			end

		end
	end
function friendCache:New( resp,ref)
	-- body
	local o = {
		resp = resp,
		ref = ref,
		autoReleaseDuration = autoReleaseTotal,--自动释放时间300秒
	}
	setmetatable(o,self)
	self.__index = self
	-- setmetatable(o, {__mode = "kv"})--弱引用
	
	Scheduler.Instance:AddTimer(autoReleaseDelt, false, autoRelease(o))
	return o
end

return friendCache
local modname = "ConditionallyActive"
module(modname, package.seeall)

--variables
uiName = modname
conditions = nil
instances = {}

function Awake(self)
	print(self.uiName, "Awake", self.UINAME, self.gameObject.name)
	self.conditions = {}
	for i = 1, 10000 do
		local cond = self["CONDITION" .. i]
		if not cond then
			break
		end

		local inverse = false
		local prefix = string.sub(cond, 1, 1)
		if prefix == "~" then
			inverse = true
			cond = string.sub(cond, 2)
		end

		local tokens = string.split(cond, ":")
		local conditionType = ConditionType.IntToEnum(tonumber(tokens[1]))
		local arg = tokens[2]
		if arg == nil then arg = "" end
		print(self.uiName, "Inverse:", inverse, "Condition:", conditionType, "Arg:", arg)
		table.insert(self.conditions, {inverse = inverse, conditionType = conditionType, arg = arg})
	end

	if not self.UINAME then
		error(self.uiName, "UINAME not set.")
	end

	local map = self.instances[self.UINAME]
	if not map then
		map = {}
		self.instances[self.UINAME] = map
	end
	map[self.gameObject] = self
end

function OnDestroy(self)
	local map = self.instances[self.UINAME]
	map[self.gameObject] = nil
end

function Validate(self)
	print(self.uiName, "Validate", self.UINAME, self.gameObject.name)
	local failed = false
	for _, v in ipairs(self.conditions) do
		print(self.uiName, v.conditionType, v.arg, "inverse:", v.inverse)
		local result = ConditionValidator.Instance:Validate(v.conditionType, v.arg)
		print(self.uiName, "result:", result)
		if v.inverse == result then
			failed = true
			break
		end
	end
	print(self.uiName, "Failed:", failed)
	NGUITools.SetActive(self.gameObject, not failed)
end

function ValidateAll(UINAME)
	print(modname, "Validate all for ", UINAME)
	local map = instances[UINAME]
	if not map then return end

	for k, v in pairs(map) do
		if v then
			v:Validate()
		end
	end
end

return _G[modname]

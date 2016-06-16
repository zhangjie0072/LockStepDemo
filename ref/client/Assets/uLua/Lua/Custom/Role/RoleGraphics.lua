RoleGraphics =	{
	uiName	= "RoleGraphics",
	pointArray,
	scale = {},
	mesh = nil,

}

function RoleGraphics:Awake()
	local mesh_filter = self.transform:GetComponent('MeshFilter')
	self.mesh = mesh_filter.mesh
	local render = self.transform:GetComponent('MeshRenderer')
	render.material = Material.New(UnityEngine.Shader.Find("Unlit/PureColor"))

	self:CreatePointArray()
	self.scale = {}
end


function RoleGraphics:Start()

end

function RoleGraphics:Show()
	print('RoleGraphics:Show() is called')

	self:CreatePointArray()
	local vertices = {}
	vertices[1] = self.pointArray[1]

	local dir = self.pointArray[2] - self.pointArray[1]
	local distance = dir.magnitude
	local d = dir.normalized
	vertices[2] = self.pointArray[1] + d * distance * self.scale[1]


	dir = self.pointArray[3] - self.pointArray[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[3] = self.pointArray[1] + d * distance * self.scale[2]


	dir = self.pointArray[4] - self.pointArray[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[4] = self.pointArray[1] + d * distance * self.scale[3]


	dir = self.pointArray[5] - self.pointArray[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[5] = self.pointArray[1] + d * distance * self.scale[4]

	dir = self.pointArray[6] - self.pointArray[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[6] = self.pointArray[1] + d * distance * self.scale[5]


	dir = self.pointArray[7] - self.pointArray[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[7] = self.pointArray[1] + d * distance * self.scale[6]



	local triangles = {}
	for i =1,18 do
		-- for i =0,14 do		  --
		triangles[i] = i-1
	end

	local all_vertices = {}
	local vmap = {1,2,3,1,3,4,1,4,5,1,5,6,1,6,7,1,7,2}

	for i =1,18 do
		all_vertices[i] = vertices[vmap[i]]
		local v=all_vertices[i]
		print('all_v:i='..tostring(i)..',x='..tostring(v.x)..',y='..tostring(v.y)..',z='..tostring(v.z))
	end

	self.mesh.vertices = all_vertices
	self.mesh.triangles = triangles

	local colors32 = {}
	for i=1,18 do
		-- colors32[i] = Color.New(41,53,165,255)
		colors32[i] = Color.New(41/255,53/255,165/255,255/255)
	end
	self.mesh.colors = colors32
end

function RoleGraphics:SetData(roleId)
	local attrConfig = GameSystem.Instance.RoleBaseConfigData2
	local attrNameConfig = GameSystem.Instance.AttrNameConfigData
	local talent = attrConfig:GetTalent(roleId)
	local function getAttr(name)
		return attrConfig:GetAttrValue(roleId, attrNameConfig:GetAttrData(name).id)
	end

	local d1 = 0
	local data = getAttr("shoot_middle")
	d1 = data
	data = getAttr("layup_middle")
	d1 = d1 + data
	data = getAttr("dunk_middle")
	d1 = d1 + data
	data = getAttr("shoot_near")
	d1 = d1 + data
	data = getAttr("layup_near")
	d1 = d1 + data
	data = getAttr("dunk_near")
	d1 = d1 + data
	d1 = d1 * talent /1200
	self.scale[4] = d1



	data = getAttr("shoot_far")
	data = data * talent / 200
	self.scale[3] = data



	local d3 = 0
	data = getAttr("disturb")
	d3 = d3 + data
	data = getAttr("steal")
	d3 = d3 + data
	data = getAttr("block")
	d3 = d3 + data
	data = getAttr("interception")
	d3 = d3 + data
	d3 = d3 * talent / 800
	self.scale[6] = d3


	local d4 = 0
	data = getAttr("anti_disturb")
	d4 = d4 + data
	data = getAttr("control")
	d4 = d4 + data
	data = getAttr("anti_block")
	d4 = d4 + data
	data = getAttr("pass")
	d4 = d4 + data
	d4 = d4 * talent / 800
	self.scale[2] = d4

	data = getAttr("rebound")
	data = data * talent / 200
	self.scale[5] = data

	data = getAttr("speed")
	data = data * talent / 200
	self.scale[1] = data

	for i = 1, 6 do
		print("tuxing self.scale[i]=",self.scale[i], "i=", i)
		if self.scale[i] > 1 then
			self.scale[i] = 1
		end
	end
	self:Show()
end

function RoleGraphics:CreatePointArray()
	if self.pointArray ~= nil then
		return
	end
	self.pointArray ={}
	self.pointArray[1] = Vector3.New(0,0,0)
	self.pointArray[2] = Vector3.New(0,-100,0)
	self.pointArray[3] = Vector3.New(88,-50,0)
	self.pointArray[4] = Vector3.New(88,53,0)
	self.pointArray[5] = Vector3.New(0,103,0)
	self.pointArray[6] = Vector3.New(-89,53,0)
	self.pointArray[7] = Vector3.New(-89,-52,0)
end


return RoleGraphics,  "RoleGraphics"

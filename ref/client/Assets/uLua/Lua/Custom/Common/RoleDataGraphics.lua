-- 20150624_202122



RoleDataGraphics =  {
	uiName = 'RoleDataGraphics',
	
	point_array ={},
	scale = {},
	mesh = nil,

}



function RoleDataGraphics:refresh_captain_data(id,bias)
 local attr_data = GameSystem.Instance.AttrDataConfigData:GetCaptainAttrData(id, bias)
	print('attr_data'..tostring(attr_data))
	
	local num ={0,0,0,0,0}
	local igv ={0,0,0,0,0}
	
	local enum = attr_data.attrs:GetEnumerator()

	while enum:MoveNext() do
	  local k = enum.Current.Key
	  local v = enum.Current.Value
	  local a_data = GameSystem.Instance.AttrNameConfigData:GetAttrData(k)
	  
	  local attr_id = a_data.id
	  local value = v
	  
	  print('attr_id='..tostring(attr_id))
	  print('v='..tostring(v))


	  if attr_id == 20 or attr_id == 17 or attr_id == 18 then
	 igv[1] = igv[1] + value
	 num[1] = num[1] + 1
	  end

	  if attr_id == 3 or attr_id == 2 then
	 igv[2] = igv[2] + value
	 num[2] = num[2] + 1
	  end

	  if attr_id == 23 then
	 igv[3] = igv[3] + value
	 num[3] = num[3] + 1
	  end

	  if attr_id == 21 then
	 igv[4] = igv[4] + value
	 num[4] = num[4] + 1
	  end

	  if attr_id == 1 or attr_id == 4 or attr_id == 5 or attr_id == 6 or attr_id == 7 then
	 igv[5] = igv[5] + value
	 num[5] = num[5] + 1
	  end
	end
	

	for i=1,5 do
	  igv[i] = igv[i]/500/num[i]
	  print('igv['..tostring(i)..']='..tostring(igv[i]))
	  if igv[i] > 1 then
	 igv[i]=1
	  end
	end
	self:set_scale(igv[1],igv[2],igv[3],igv[4],igv[5])
end


function RoleDataGraphics:Awake()
	local mesh_filter = self.transform:GetComponent('MeshFilter')
	self.mesh = mesh_filter.mesh
	local mesh_render = self.transform:GetComponent("MeshRenderer")
	mesh_render.material = Material.New(Shader.Find("Toon/Advanced Outline"))
	mesh_render.material.color = Color.New(20/255,0,1,1)

	self.point_array ={}
	
	self.point_array[1] = Vector3.New(0,-1,0)
	self.point_array[2] = Vector3.New(0,96,0)
	self.point_array[3] = Vector3.New(100,23,0)
	self.point_array[4] = Vector3.New(61,-96,0)
	self.point_array[5] = Vector3.New(-62,-96,0)
	self.point_array[6] = Vector3.New(-100,23,0)
	
	self.scale = {}
	for i=1,5 do 
	  self.scale[i]=1
	end

end


function RoleDataGraphics:set_scale(f1,f2,f3,f4,f5)
	self.scale[1] = f1
	self.scale[2] = f2
	self.scale[3] = f3
	self.scale[4] = f4
	self.scale[5] = f5

	self:show()
end


function RoleDataGraphics:show()
	local vertices = {}
	vertices[1] = self.point_array[1]

	local dir = self.point_array[2] - self.point_array[1]
	local distance = dir.magnitude
	local d = dir.normalized
	vertices[2] = self.point_array[1] + d * distance * self.scale[1]
	

	dir = self.point_array[3] - self.point_array[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[3] = self.point_array[1] + d * distance * self.scale[2]


	dir = self.point_array[4] - self.point_array[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[4] = self.point_array[1] + d * distance * self.scale[3]
	

	dir = self.point_array[5] - self.point_array[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[5] = self.point_array[1] + d * distance * self.scale[4]

	dir = self.point_array[6] - self.point_array[1]
	distance = dir.magnitude
	d = dir.normalized
	vertices[6] = self.point_array[1] + d * distance * self.scale[5]


	local triangles = {}
	for i =1,15 do
	  triangles[i] = i-1
	end

	local all_vertices = {}
	local vmap = {1,2,3,1,3,4,1,4,5,1,5,6,1,6,2}
	
	for i =1,15 do
	  all_vertices[i] = vertices[vmap[i]]
	  local v=all_vertices[i]
	end

	self.mesh.vertices = all_vertices
	self.mesh.triangles = triangles

	local colors32 = {}
	for i=1,15 do
	  colors32[i] = Color.New(20/255,0,1,1)
	  -- colors32[i] = Color.New(41/255,53/255,165/255,255/255)
	end

	self.mesh.colors = colors32
end


function RoleDataGraphics:Start()
	self:show()
end


return RoleDataGraphics

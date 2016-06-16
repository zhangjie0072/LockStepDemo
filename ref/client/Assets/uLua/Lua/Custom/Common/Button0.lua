

Button0 = {}
Button0._transform = nil

local this = Button0


function Button0.Awake(transform)
	print("Button0.Init called")
	this._transform = transform
end




function Button0.OnClick(func,go)
	 not use this way, now.
	print("Button0 called Button0.OnClick func=",func)
	print("go=",go)
	
end

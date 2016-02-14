
--打印--
--print("Hello lua！")
--print('Hello lua！')

--for循环--
--[[
	length = 10
	i = 1   
	while i <= length do	
		print(i) 
		i = i + 1
	end	
--]]

	length = 10
	i = 1
	while i <= length do
		print(i)
		i = i + 1
	end

--表(类)及函数--
--[[
	a = { x = 99 } 
	function a:fun()   
		print(self.x)  
	end   
	a:fun()
--]]
	
--粒子效果--
--[[
    luanet.load_assembly('UnityEngine')							--导入UnityEngine命名空间。
    GameObject = luanet.import_type('UnityEngine.GameObject')   --定义GameObject类。
	
    local newGameObj = GameObject('NewObj')                     --new一个GameObject对象。
    newGameObj:AddComponent('ParticleSystem')                   --给GameObject对象添加粒子组件。
--]]

--粒子效果--表--	
--[[
	luanet.load_assembly('UnityEngine')							--导入UnityEngine命名空间。
	GameObject = luanet.import_type('UnityEngine.GameObject')	--定义GameObject类。

	particles = {}
	Objs2Spawn = 5
	for i = 1, Objs2Spawn, 1  do                                --Objs2Spawn变量，需要通过对LuaState对象通过索引器方式赋值。
		local newGameObj = GameObject('NewObj' .. i)  			--new一个GameObject对象。
		local ps = newGameObj:AddComponent('ParticleSystem')
		ps:Stop()
		--ps:Play()
		table.insert(particles, ps)								--表中添加数据
	end
--]]

--函数--
--[[
	function luaFunc(info)
		print(info)
		return "返回值"
	end
--]]

-- ?
--[[
function fib(n)
	local a, b = 0, 1
	while n > 0 do
		--a, b = b, a + b
		a = b 
		b = a + b
		n = n - 1
	end

	return a
end
--]]

--线程--
--[[luanet.load_assembly('UnityEngine')
local Time = luanet.import_type('UnityEngine.Time')

-- This yields every frame that the global game time is still less than the desired timestamp
function waitSeconds(t)
	local timeStamp = Time.time + t
	while Time.time < timeStamp do
		coroutine.yield()
	end
end

function myFunc()
	print('Coroutine started')
	local i = 0
	for i = 0, 10, 1 do
		--print(fib(i))
		print(i);
		waitSeconds(1)
	end
	print('Coroutine ended')
end
--]]

--[[--
好了，是时候来设计AI脚本的API了。AI脚本用于定义单独的一个子状态
Lua 命名规范
函数 小驼峰
变量 蛇形命名
常量 全大写，下划线分割
--]]--

local actions = require 'actions'
local instrs = require 'instrs'

local aim_lock_instr = instrs.create(instrs.CHARA_AIM_LOCK)

function enter()
    aim_lock_instr.Target = self.Sensor.Enemy
    DebugLog(aim_lock_instr)
    self.Chara:Execute(aim_lock_instr)
end

function process()
    math.randomseed(os.time())
    local randomval = math.random(100)

    if not self.Sensor.EnemyFound then
        return
    end

    if not TransformUtil.IsInSectorArea(self.transform, self.Sensor.Enemy, RelativeDir.Forward, 100, 3) then
        self.Actions:TryAdd(actions.create(actions.COMMON_APPROCH, self, self.Sensor.Enemy))
    elseif randomval < 10 then
        self.Actions:Clear()
        local dir = -1
        if math.random(100) < 50 then
            dir = -1
        else
            dir = 1
        end
        self.Actions:TryAdd(actions.create(actions.COMMON_DODGE, self, Vector2(dir, 0), 1 ,3))
    elseif randomval < 70 then
        self.Actions:TryAdd(actions.create(actions.SK_LIGHT_ATTACK, self, self.Sensor.Enemy))
    elseif randomval > 90 then
        self.Actions:Clear()
        self.Actions:TryAdd(actions.create(actions.COMMON_RECEDE, self, self.Sensor.Enemy))
    end

end
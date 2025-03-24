local M = {
    COMMON_APPROCH = 1,
    COMMON_RECEDE = 2,
    COMMON_ALIGN = 3,
    COMMON_WANDER = 4,
    COMMON_PATH_MOVE = 5,
    COMMON_DODGE = 6,
    SK_LIGHT_ATTACK = 20
}


-- AI 行动表
local actions = {
    [M.COMMON_APPROCH] = CS.QS.Agent.ApprochAction,
    [M.COMMON_RECEDE] = CS.QS.Agent.RecedeAction,
    [M.COMMON_ALIGN] = CS.QS.Agent.AlignAction,
    [M.COMMON_WANDER] = CS.QS.Agent.WanderAction,
    [M.COMMON_PATH_MOVE] = CS.QS.Agent.PathMoveAction,
    [M.COMMON_DODGE] = CS.QS.Agent.DodgeAction,
    [M.SK_LIGHT_ATTACK] = CS.QS.Agent.SKLightAttackAction
}

function M.create(id, ...)
    return actions[id](...)
end


return M
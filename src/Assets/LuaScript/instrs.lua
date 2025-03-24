local M ={
    CHARA_AIM_LOCK = 1
}

local instrs = {
    [M.CHARA_AIM_LOCK] = CS.QS.Chara.AimLockInstr
}

function M.create(id)
    return instrs[id]()
end

return M
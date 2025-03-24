using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara.Instrs
{
    /// <summary>
    /// 现在我们假设所有角色垫步速度一样的
    /// 
    /// 角色应该时刻受到控制，即便是静止 也需要有一个空控制
    /// </summary>
    public interface IShuffleStepInstr : IInstruction
    {

        Vector2 Direction { get; }
        Vector3 BaseRight { get; }
        Vector3 Baseforword { get; }

    }
}
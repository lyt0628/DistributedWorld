using QS.Api.Executor.Domain;
using UnityEngine;


namespace QS.Chara
{
    /// <summary>
    /// 虽然是值类型，但是创建十分频繁，并且用处有限，直接提供设置接口更适合复用
    /// </summary>
    public interface IMoveInstr : IInstruction
    {
        public Vector2 value { get; set; }

        public bool run { get; set; }
    }
    /// <summary>
    /// 这些指令是通用的指令，也就是说，可以有多个可以处理它们的处理器
    /// 用 struct 可以直接传递副本，更安全
    /// </summary>
    public struct MoveInstr : IMoveInstr
    {

        public Vector2 value { get; set; }

        public bool run { get; set; }
    }
}
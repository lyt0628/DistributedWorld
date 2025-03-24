using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// 指示移動的角色，封裝了移動AI的算法 
    /// 像是A*之类的
    /// 负责寻路的组件
    /// </summary>
    public interface ISteering
    {
        /// <summary>
        /// Agent 的目标移动位置
        /// </summary>
        Vector3 Destination { get; set; }
        /// <summary>
        /// 最基本的指定速度的方式
        /// </summary>
        bool Run { get; set; }
        /// <summary>
        /// 为了到达目标位置在本帧需要指定的指令
        /// </summary>
        /// <returns></returns>
        IInstruction GetTranslateInstr();
    }
}
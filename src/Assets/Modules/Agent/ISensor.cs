


using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// 负责感知的模块，不管是基于图的感知还是基于碰撞体的感知
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Sensor 检测到的敌人
        /// </summary>
        /// <returns></returns>
        Transform[] Enemies { get; }
        Transform Enemy { get; }
        bool EnemyFound { get; }


    }
}
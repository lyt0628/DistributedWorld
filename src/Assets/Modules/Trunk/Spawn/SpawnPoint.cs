



using UnityEngine;

namespace QS.Spawn
{
    /// <summary>
    /// 场景管理中的出生模块
    /// </summary>
    class SpawnPoint
    {
        /// <summary>
        /// 初始化设置的位置
        /// </summary>
        public Vector3 position { get; }
        /// <summary>
        /// 初始化设置的旋转值
        /// </summary>
        public Quaternion rotation { get; }
    }
}



using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 物理检测,主要用于状态的迁移
    /// </summary>
    public interface ICharaControlPhysicalProbe
    {
        bool IsGrounded { get; }
        bool IsGroundedStrict { get; }


        /// <summary>
        /// 在某个方向是否被挡住
        /// </summary>
        bool IsBlockedInDir(Vector3 dir);
        /// <summary>
        /// 在某个方向是否存在墙壁
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        bool IsWallInDir(Vector3 dir, out float height);
        /// <summary>
        /// 坡度（deg）
        /// </summary>
        float GroundSlope { get; }
        /// <summary>
        /// 是否在阶梯上
        /// </summary>
        bool InStep { get; }

    }
}


using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 负责计算速度和位移的模块
    /// </summary>
    public interface ICharaControlMotor
    {
        /// <summary>
        /// 速度
        /// </summary>
        Vector3 Velocity { get; }
        public Vector2 HorVelocity { get; }

        /// <summary>
        /// 旋转值
        /// </summary>
        Quaternion Rotation { get; }
        /// <summary>
        /// 模型坐标速度
        /// </summary>
        Vector3 LocalVelocity { get; }
        /// <summary>
        /// 没有经过 限制处理 的位移
        /// </summary>
        Vector3 UnclampedDisplacement { get; set; }
        /// <summary>
        /// 经过限制处理的位移，每次获取都会重新计算
        /// </summary>
        Vector3 ClampedDisplacement { get; }
        /// <summary>
        /// 水平的速度大小
        /// </summary>
        float HorizontalSpeed { get; }
        /// <summary>
        /// 竖直方向的速度大小
        /// </summary>
        float VerticalSpeed { get; }
        /// <summary>
        /// 模型坐标的向前速度大小
        /// </summary>
        float ForwardSpeed { get; }
        /// <summary>
        /// 模型坐标侧向的速度大小
        /// </summary>
        float RightSpeed { get; }
        /// <summary>
        /// 转动量的大小
        /// </summary>
        float TurnSpeed { get; }
        /// <summary>
        /// 对象是否在上升
        /// </summary>
        bool IsRaising { get; }
        /// <summary>
        /// 对象是否在下落
        /// </summary>
        bool IsFalling { get; }
        bool IsJumping { get; }
        Vector2 LocalHorVelocity { get; }

        /// <summary>
        /// 更新内部状态
        /// </summary>
        void Update();

        /// <summary>
        /// 更新数据，但是不会当作额外帧来计算，在每次可能导致内部状态改变的操作后计算
        /// 为了获取实时数据
        /// </summary>
        void Refresh();
    }
}
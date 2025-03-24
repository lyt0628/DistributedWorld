



using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using System.Linq;
using UnityEngine;

namespace QS.Chara
{

    /// <summary>
    /// Motor必须允许外部设置速度或Disp，要不就没办法在 根骨骼动画和Inplace动画
    /// 中得到复用
    /// </summary>
    public class DefaultCharaControlMotor : ICharaControlMotor
    {
        readonly CharaControlTemplate controlFSM;
        readonly Transform transform;
        Vector3 m_Velocity = Vector3.zero;
        Vector2 m_HorVelocity = Vector2.zero;
        Vector3 m_LocalVelocity = Vector3.zero;
        Vector3 m_LocalHorVelocity = Vector3.zero;
        Quaternion m_Rotation = Quaternion.identity;
        float m_TurnSpeed = 0f;
        float m_HorizontalSpeed = 0f;
        float m_VertSpeed = 0f;

        public DefaultCharaControlMotor(CharaControlTemplate ctrlTpl)
        {
            this.controlFSM = ctrlTpl;
            transform = ctrlTpl.transform;
        }

        public void Update()
        {
            // 获取用户输入方向
            var moveDir = controlFSM.MoveDir.x * controlFSM.meta.right() + controlFSM.MoveDir.y * controlFSM.meta.forward();
            // 计算速度
            m_Velocity = ComputeSpeed(moveDir);
            m_HorVelocity = new Vector2(m_Velocity.x, m_Velocity.z);
            m_HorizontalSpeed = m_HorVelocity.magnitude;
            // 计算没有限制处理的位移
            UnclampedDisplacement = ComputeUncalmpedDisplacement(m_Velocity);
            // 计算旋转值
            m_Rotation = ComputeRotaion(m_Velocity);
            // 计算本地速度
            m_LocalVelocity = transform.InverseTransformDirection(m_Velocity);
            m_LocalHorVelocity = new Vector2(m_LocalVelocity.x, m_LocalVelocity.z);
            m_TurnSpeed = ComputeTurnSpeed(m_LocalVelocity);

            //Debug.Log(HorizontalSpeed);
        }

        public void Refresh()
        {
            /// TODO
        }
        protected virtual float ComputeTurnSpeed(Vector3 localVelocity)
        {
            return Mathf.Atan2(localVelocity.x, localVelocity.y);
        }

        protected virtual Quaternion ComputeRotaion(Vector3 velocity)
        {
            velocity.y = 0f;
            if (controlFSM.AimLock)
            {
                var aimDir = controlFSM.AimTarget.position - transform.position;
                aimDir.y = 0f;
                return Quaternion.Lerp(m_Rotation, Quaternion.LookRotation(aimDir), 5 * Time.deltaTime);
            }

            if (velocity.magnitude != 0)
            {
                return Quaternion.Lerp(m_Rotation, Quaternion.LookRotation(velocity), 3 * Time.deltaTime);
            }
            return m_Rotation;
        }

        protected virtual Vector3 ComputeUncalmpedDisplacement(Vector3 velocity)
        {
            return m_Velocity * Time.deltaTime;
        }

        protected virtual Vector3 ComputeSpeed(Vector3 moveDir)
        {
            var expectedSpeed = controlFSM.Run ? controlFSM.meta.runSpeed : controlFSM.meta.walkSpeed;
            var speed = Mathf.Lerp(m_HorizontalSpeed, expectedSpeed, 10 * Time.deltaTime);

            if (IsFalling)
            {
                if (controlFSM.IsGrounded && controlFSM.Jump)
                {
                    m_VertSpeed = controlFSM.meta.jumpSpeed;
                }
                else if (controlFSM.IsGroundedStrict)
                {
                    m_VertSpeed = Mathf.Lerp(m_VertSpeed, 0, 0.5f);
                }
            }
            if (!controlFSM.IsGrounded)
            {
                m_VertSpeed -= controlFSM.meta.fallGrivity * Time.deltaTime;
            }

            var velocity = moveDir * speed;
            velocity.y = m_VertSpeed;

            return velocity;
        }
        protected virtual Vector3 ClampDisplacement(Vector3 position, Vector3 disp)
        {
            var height = controlFSM.meta.height;
            var radius = controlFSM.meta.radius;

            //disp.y = 0;
            if (disp.y < 0)
            {
                var downRay = RaycastHelperAll.Of(CastedObject
                .Ray(position, -controlFSM.meta.up())
                .IgnoreTrigger()
                // 检测距离增加有效防止检测失效
                .MaxDistance(Mathf.Abs(disp.y) + 0.01f));
                var obstacleHitinfos = downRay
                    .HitInfos
                    .Where(o => o.collider.gameObject != controlFSM.gameObject
                                        && !o.transform.IsChildOf(controlFSM.transform));

                if (obstacleHitinfos.Any())
                {
                    var hitInfo = obstacleHitinfos.First();
                    var clampDisp = Mathf.Max((hitInfo.distance - 0.01f), 0f);
                    disp.y = -Mathf.Min(clampDisp, Mathf.Abs(disp.y));
                }
                //if (flag && disp.y < -0.01f)
                //{
                //    Debug.LogError("Error");
                //}
            }


            var p1 = position + new Vector3(disp.x, 0, disp.z) + radius * Vector3.up;
            var distance = height - 2 * radius;
            if (RaycastHelperAll.Of(CastedObject
                .Sphere(p1, radius, Vector3.up)
                .IgnoreTrigger()
                .MaxDistance(distance)
                ).AllColliders
                .Where(o => o.gameObject != controlFSM.gameObject
                                    && !o.transform.IsChildOf(controlFSM.transform))
                                    .Any())
            {
                disp.x = disp.z = 0f;
            }

            //Debug.Log($"{controlFSM.name} Displacement {disp}");
            return disp;
        }


        public Vector3 Velocity => m_Velocity;

        public Quaternion Rotation => m_Rotation;
        public Vector3 UnclampedDisplacement { get; set; }

        public float HorizontalSpeed => m_HorizontalSpeed;


        public float VerticalSpeed => m_VertSpeed;
        public Vector3 LocalVelocity => m_LocalVelocity;

        public float ForwardSpeed => m_LocalVelocity.z;

        public float RightSpeed => m_LocalVelocity.x;

        public float TurnSpeed => m_TurnSpeed;

        public bool IsRaising => m_VertSpeed > 0;

        public bool IsFalling => m_VertSpeed <= 0;

        public Vector3 ClampedDisplacement => ClampDisplacement(controlFSM.transform.position, UnclampedDisplacement);

        public Vector2 HorVelocity => m_HorVelocity;
        public Vector2 LocalHorVelocity => m_LocalHorVelocity;

        public bool IsJumping { get; protected set; }
    }
}
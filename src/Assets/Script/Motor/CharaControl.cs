


using GameLib.DI;
using QS.Api.Setting;
using QS.Common.ComputingService;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Motor
{
    /// <summary>
    /// 如何讓這些控制結構清晰與可重用是最核心的問題。
    /// 作爲控制模塊，至少包括這些指令：
    /// 玩家的走，跑，跳，如果可行的話，其實希望每一個指令對應一個服務。
    /// 但是這些指令都是面對位移的，如果單獨寫又會造成重複，複雜在分開，現在作爲簡單一個服務
    /// 
    /// 還應當提供特殊控制，像是瞬移，飛索控制，以及路徑控制等。
    /// 這些控制是可參數化的，這些參數由指令提供
    /// 使用计算服务最大的缺点是必须每帧都调用指令保持状态
    /// </summary>
    public class CharaControl : AbstractComputingService<CharaControl.Input, CharaControl.Result, CharaControl.State>
    {

        readonly IGlobalPhysicSetting globalPhysic;

        public class Input
        {
            public Vector3 position;
            public float horizontal;
            public float vertical;
            public bool jump;
            public bool dash;
            public Vector3 baseRight = Vector3.right;
            public Vector3 baseForword = Vector3.forward;
            public Vector3 baseUp = Vector3.up;

            public Quaternion Rotation { get; set; }
        }
        public class State
        {
            public float verticalSpeed = 0f;
            public bool inAir = false;
        }
        public class Result
        {
            public float speed;
            public Vector3 disp;
            public bool inAir;
        }

        [Injected]
        public CharaControl(IGlobalPhysicSetting globalPhysic, DataSource<Input, State> dataSource) : base(dataSource)
        {
            this.globalPhysic = globalPhysic;
        }


        protected override CharaControl.Result Compute(CharaControl.Input input, CharaControl.State state)
        {
            

            var vRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(input.position, Vector3.down)
                                    .IgnoreTrigger());


            // 計算水平方向速度和位移
            var hVelocity = input.vertical * input.baseForword + input.horizontal * input.baseRight;
            if (input.dash)
            {
                hVelocity *= 4;
            }

            var hDisp = Time.deltaTime * hVelocity;

            // 在可容忍的誤差內，是否可以判斷物體着地
            var isGrounded = state.verticalSpeed < 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);

            if (isGrounded)
            {
                // 玩家發送跳躍指令
                if (input.jump)
                {
                    state.inAir = true;

                    state.verticalSpeed = 10f;
                    input.position += input.baseUp * globalPhysic.ErrorTolerance;
                    isGrounded = false; // 跳躍的這一時刻，我們就認爲物體已經離地

                }

                //在跳躍的時候接觸到地板，就是停止跳躍了
                if (!input.jump && state.inAir)
                {
                    state.inAir = false;
                    state.verticalSpeed = 0;
                }

            }
            else //跳躍的時候，按重力計算垂直方向速度
            {
                state.verticalSpeed += globalPhysic.Gravity * Time.deltaTime;
            }

            // 垂直方向速度
            var vDisp = state.verticalSpeed * Time.deltaTime;

            // 水平方向速度
            var disp = hDisp;
            // 水平方向速度投影到地面，以應對地面非水平的情況
            disp = Vector3.ProjectOnPlane(disp, vRay.Normal);

            // 合併後的位移
            disp.y += vDisp;

            // 檢測縱向的位移是否會穿透
            var a = RaycastHelper
                            .Of(CastedObject
                                    .Ray(input.position, Mathf.Sign(disp.y) * Vector3.up)
                                    .IgnoreTrigger());
            // 如果會穿透的話就移動到可以移動的最大位置
            if (a.IsCloserThanOrJust(-disp.y))
            {
                disp.y = -a.Distance + globalPhysic.HalfErrorTolerance;
            }

            // 檢測最終位移是否會穿透， 如果會穿透的話就停止運動
            var b = RaycastHelper
                            .Of(CastedObject
                                    .Ray(input.position, disp.normalized)
                                    .IgnoreTrigger());
            if (b.IsCloserThanOrJust(disp.magnitude))
            {
                disp.z = 0f;
                disp.x = 0f;
            }


            var translation = new CharaControl.Result
            {
                disp = disp,
                speed = hVelocity.magnitude,
                inAir = state.inAir
            };
            return translation;
        }

        protected override void DoReset(State state)
        {
        }
    }
}
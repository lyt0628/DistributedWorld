


using GameLib.DI;
using QS.Api.Control.Service;
using QS.Api.Control.Service.DTO;
using QS.Api.Setting;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using QS.Impl.Service.DTO;
using UnityEngine;

namespace QS.Control.Service
{
    /// <summary>
    /// 如何讓這些控制結構清晰與可重用是最核心的問題。
    /// 作爲控制模塊，至少包括這些指令：
    /// 玩家的走，跑，跳，如果可行的話，其實希望每一個指令對應一個服務。
    /// 但是這些指令都是面對位移的，如果單獨寫又會造成重複，複雜在分開，現在作爲簡單一個服務
    /// 
    /// 還應當提供特殊控制，像是瞬移，飛索控制，以及路徑控制等。
    /// 這些控制是可參數化的，這些參數由指令提供
    /// </summary>
    class CharaTranslationControl : ICharaTranslationControl
    {
        [Injected]
        readonly IGlobalPhysicSetting globalPhysic;

        [Injected]
        readonly ICharaTranslationProxyDataSource_tag proxys;

        public Relay<ICharaTranslation> GetTranslation(string uuid)
        {
            return  proxys.Get(uuid).Map(p => ComputeTranslation(p));
        }


        ICharaTranslation ComputeTranslation(ICharaTranslationProxy proxy)
        {

            var vRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, Vector3.down)
                                    .IgnoreTrigger());


            // 計算水平方向速度和位移
            var hVelocity = proxy.Vertical * proxy.BaseForword + proxy.Horizontal * proxy.BaseRight;
            if (proxy.Dash) // 不應該放這裏，這也是指令的一部分
            {
                hVelocity *= 4;
            }

            var hDisp = Time.deltaTime * hVelocity;

            // 在可容忍的誤差內，是否可以判斷物體着地
            var isGrounded = proxy.VerticalSpeed <= 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);
           
            if (isGrounded)
            {
                // 玩家發送跳躍指令
                if (proxy.Jump)
                {
                    proxy.Jumping = true;

                    proxy.VerticalSpeed = 10f;
                    proxy.Position += proxy.BaseUp * globalPhysic.ErrorTolerance;
                    isGrounded = false; // 跳躍的這一時刻，我們就認爲物體已經離地
                    
                }

                //在跳躍的時候接觸到地板，就是停止跳躍了
                if (!proxy.Jump && proxy.Jumping)
                {
                    proxy.Jumping = false;
                    proxy.VerticalSpeed = 0;
                }

            }
            else //跳躍的時候，按重力計算垂直方向速度
            {
                proxy.VerticalSpeed += globalPhysic.Gravity * Time.deltaTime;
            }

            // 垂直方向速度
            var vDisp = proxy.VerticalSpeed * Time.deltaTime;

            // 水平方向速度
            var disp = hDisp;
            var dispRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, disp)
                                    .IgnoreTrigger());
            // 水平方向速度投影到地面，以應對地面非水平的情況
            disp = Vector3.ProjectOnPlane(disp, dispRay.Normal);

            // 合併後的位移
            disp.y += vDisp;

            // 檢測縱向的位移是否會穿透
            var a = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, Mathf.Sign(disp.y) * Vector3.up)
                                    .IgnoreTrigger());
           // 如果會穿透的話就移動到可以移動的最大位置
            if (a.IsCloserThanOrJust(-disp.y))
            {
                disp.y = - a.Distance + globalPhysic.HalfErrorTolerance;
            }

            // 檢測最終位移是否會穿透， 如果會穿透的話就停止運動
            var b = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, disp.normalized)
                                    .IgnoreTrigger());
            if (b.IsCloserThanOrJust(disp.magnitude))
            {
                disp.z = 0f;
                disp.x = 0f;
            }

          
            //if (b.IsCloserThanOrJust(disp.magnitude))
            //{
            //    Debug.LogError("Collidsion Limit Failed!");
            //}


            var translation = ControlGlobal.Instance.DI.GetInstance<CharaTranslation>();
            translation.Displacement = disp;
            translation.Speed = hVelocity.magnitude;
         
            translation.Jumping = proxy.Jumping;
            return translation;
        }

    }
}
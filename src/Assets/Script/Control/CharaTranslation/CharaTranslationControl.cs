


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
    /// ���׌�@Щ���ƽY�������c������������ĵĆ��}��
    /// ��������ģ�K�����ٰ����@Щָ�
    /// ��ҵ��ߣ��ܣ�����������е�Ԓ���䌍ϣ��ÿһ��ָ���һ�����ա�
    /// �����@Щָ����挦λ�Ƶģ�����Ϊ����֕�������}���}�s�ڷ��_���F����������һ������
    /// 
    /// ߀�����ṩ������ƣ�����˲�ƣ��w�����ƣ��Լ�·�����Ƶȡ�
    /// �@Щ�����ǿɅ������ģ��@Щ������ָ���ṩ
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


            // Ӌ��ˮƽ�����ٶȺ�λ��
            var hVelocity = proxy.Vertical * proxy.BaseForword + proxy.Horizontal * proxy.BaseRight;
            if (proxy.Dash) // ����ԓ���@�Y���@Ҳ��ָ���һ����
            {
                hVelocity *= 4;
            }

            var hDisp = Time.deltaTime * hVelocity;

            // �ڿ����̵��`��ȣ��Ƿ�����Д����w�ŵ�
            var isGrounded = proxy.VerticalSpeed <= 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);
           
            if (isGrounded)
            {
                // ��Ұl�����Sָ��
                if (proxy.Jump)
                {
                    proxy.Jumping = true;

                    proxy.VerticalSpeed = 10f;
                    proxy.Position += proxy.BaseUp * globalPhysic.ErrorTolerance;
                    isGrounded = false; // ���S���@һ�r�̣��҂����J�����w�ѽ��x��
                    
                }

                //�����S�ĕr����|���ذ壬����ֹͣ���S��
                if (!proxy.Jump && proxy.Jumping)
                {
                    proxy.Jumping = false;
                    proxy.VerticalSpeed = 0;
                }

            }
            else //���S�ĕr�򣬰�����Ӌ�㴹ֱ�����ٶ�
            {
                proxy.VerticalSpeed += globalPhysic.Gravity * Time.deltaTime;
            }

            // ��ֱ�����ٶ�
            var vDisp = proxy.VerticalSpeed * Time.deltaTime;

            // ˮƽ�����ٶ�
            var disp = hDisp;
            var dispRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, disp)
                                    .IgnoreTrigger());
            // ˮƽ�����ٶ�ͶӰ�����棬�ԑ��������ˮƽ����r
            disp = Vector3.ProjectOnPlane(disp, dispRay.Normal);

            // �ρ����λ��
            disp.y += vDisp;

            // �z�y�v���λ���Ƿ����͸
            var a = RaycastHelper
                            .Of(CastedObject
                                    .Ray(proxy.Position, Mathf.Sign(disp.y) * Vector3.up)
                                    .IgnoreTrigger());
           // �������͸��Ԓ���Ƅӵ������Ƅӵ����λ��
            if (a.IsCloserThanOrJust(-disp.y))
            {
                disp.y = - a.Distance + globalPhysic.HalfErrorTolerance;
            }

            // �z�y��Kλ���Ƿ����͸�� �������͸��Ԓ��ֹͣ�\��
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
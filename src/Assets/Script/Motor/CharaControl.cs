


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
    /// ���׌�@Щ���ƽY�������c������������ĵĆ��}��
    /// ��������ģ�K�����ٰ����@Щָ�
    /// ��ҵ��ߣ��ܣ�����������е�Ԓ���䌍ϣ��ÿһ��ָ���һ�����ա�
    /// �����@Щָ����挦λ�Ƶģ�����Ϊ����֕�������}���}�s�ڷ��_���F����������һ������
    /// 
    /// ߀�����ṩ������ƣ�����˲�ƣ��w�����ƣ��Լ�·�����Ƶȡ�
    /// �@Щ�����ǿɅ������ģ��@Щ������ָ���ṩ
    /// ʹ�ü����������ȱ���Ǳ���ÿ֡������ָ���״̬
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


            // Ӌ��ˮƽ�����ٶȺ�λ��
            var hVelocity = input.vertical * input.baseForword + input.horizontal * input.baseRight;
            if (input.dash)
            {
                hVelocity *= 4;
            }

            var hDisp = Time.deltaTime * hVelocity;

            // �ڿ����̵��`��ȣ��Ƿ�����Д����w�ŵ�
            var isGrounded = state.verticalSpeed < 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);

            if (isGrounded)
            {
                // ��Ұl�����Sָ��
                if (input.jump)
                {
                    state.inAir = true;

                    state.verticalSpeed = 10f;
                    input.position += input.baseUp * globalPhysic.ErrorTolerance;
                    isGrounded = false; // ���S���@һ�r�̣��҂����J�����w�ѽ��x��

                }

                //�����S�ĕr����|���ذ壬����ֹͣ���S��
                if (!input.jump && state.inAir)
                {
                    state.inAir = false;
                    state.verticalSpeed = 0;
                }

            }
            else //���S�ĕr�򣬰�����Ӌ�㴹ֱ�����ٶ�
            {
                state.verticalSpeed += globalPhysic.Gravity * Time.deltaTime;
            }

            // ��ֱ�����ٶ�
            var vDisp = state.verticalSpeed * Time.deltaTime;

            // ˮƽ�����ٶ�
            var disp = hDisp;
            // ˮƽ�����ٶ�ͶӰ�����棬�ԑ��������ˮƽ����r
            disp = Vector3.ProjectOnPlane(disp, vRay.Normal);

            // �ρ����λ��
            disp.y += vDisp;

            // �z�y�v���λ���Ƿ����͸
            var a = RaycastHelper
                            .Of(CastedObject
                                    .Ray(input.position, Mathf.Sign(disp.y) * Vector3.up)
                                    .IgnoreTrigger());
            // �������͸��Ԓ���Ƅӵ������Ƅӵ����λ��
            if (a.IsCloserThanOrJust(-disp.y))
            {
                disp.y = -a.Distance + globalPhysic.HalfErrorTolerance;
            }

            // �z�y��Kλ���Ƿ����͸�� �������͸��Ԓ��ֹͣ�\��
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
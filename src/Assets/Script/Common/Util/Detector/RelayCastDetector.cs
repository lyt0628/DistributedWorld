using QS.Api.Common.Util.Detector;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    /// <summary>
    /// ��ϣ����߿��Լ�һ����չ, ������, �������ݿ��Զ�̬�仯, ���ʹ������������
    /// ����Ͷ����, ������ײ����, ��ײ������Լ�, ֻ��Ҫ�򵥵�һ��Collide����,
    /// �����������һ��ColliderDetector�������������, ����Ҫʱ�Ͱ����Detector���
    /// ͬ���� ����ʹ�� Hierarchy ģʽ�����ε���������������
    /// </summary>
    class RelayCastDetector : IDetector
    {
        readonly ICastDetector detector;
        public RelayCastDetector(ICastDetector detector, Relay<ICastedObject> objRalay)
        {
            this.detector = detector;
            objRalay.Subscrib((o) => detector.SetCastedObject(o));
        }

        public IEnumerable<GameObject> Detect()
        {
            return detector.Detect();
        }

    }
}
using QS.Api.Common.Util.Detector;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    /// <summary>
    /// 我希望这边可以加一点拓展, 基本的, 输入数据可以动态变化, 因此使用流来更新它
    /// 除了投射检测, 还有碰撞体检测, 碰撞体检测相对简单, 只需要简单的一个Collide即可,
    /// 在它身上添加一个ColliderDetector组件来返回数据, 不需要时就把这个Detector拆除
    /// 同样的 可以使用 Hierarchy 模式来屏蔽掉单个与多个的区别
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
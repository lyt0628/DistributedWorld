using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Api.Common.Util.Detector
{

    /// <summary>
    /// 没法直接放到 GameLib, 这会使GameLib内部不同模块耦合, 放到Common, 比较合适
    /// 或者另外立一个Util模块???另外立一个吧Common还是放一些领域相关的东西好
    /// 不行, 放到另一个Assembly就得依赖于Common来获得ModuleGlobal, 至于把ModuleGlobal放到Util中???
    /// 不要, Util不是干这个的, 还不如把领域模型拉到别的模块
    /// </summary>
    public interface IDetector
    {
        IEnumerable<GameObject> Detect();
    }
}

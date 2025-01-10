


using UnityEngine;

namespace QS.Common.Util.Mounter
{
    /// <summary>
    /// 對與某一個模型，找到指定的 GameObject, 創造一個新的GameObject
    /// 作爲中間級別，這個中間級別的GameObject 會被裝配到指定的模型上
    /// 把gameObject 裝配到中間級別的GameObject 上。
    /// 把中間級別的GameObject 返回。
    /// </summary>
    public interface IMounter
    {
        void Mount(GameObject gameObject);
    }
}
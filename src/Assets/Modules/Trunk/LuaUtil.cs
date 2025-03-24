


using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace QS.Trunk
{
    /// <summary>
    /// 专门用于Lua环境下的工具类，C#内部不要使用
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名样式", Justification = "<挂起>")]
    public static class LuaUtil
    {
        
        public static string substr(string str, int start, int length)
        {
            Assert.IsTrue(start >= 1);
            return str.Substring(start - 1, length);
        }

        public static int strlen(string str)
        {
            //Debug.Log(str);
            return str.Length;
        }

        public static Task<VisualTreeAsset> load_uidoc(string address)
        {
           return Addressables.LoadAssetAsync<VisualTreeAsset>(address).Task;
        }

        public static Task<TextAsset> load_text(string address)
        {
            return Addressables.LoadAssetAsync<TextAsset>(address).Task;
        }
    }
}
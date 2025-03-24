


using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace QS.Trunk
{
    /// <summary>
    /// ר������Lua�����µĹ����࣬C#�ڲ���Ҫʹ��
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:������ʽ", Justification = "<����>")]
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
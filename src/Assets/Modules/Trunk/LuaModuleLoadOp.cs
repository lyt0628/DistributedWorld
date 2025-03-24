

using QS.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using XLua;

namespace QS.Trunk
{
    class LuaModuleLoadOp : AsyncOpBase<Unit>
    {

        readonly LuaEnv luaEnv;
        /// <summary>
        /// ±£´æ³ÌÐòLuaÄ£¿é
        /// </summary>
        readonly Dictionary<string, byte[]> m_LuaModules = new();
        public LuaModuleLoadOp(LuaEnv luaEnv)
        {
            this.luaEnv = luaEnv;
        }

        protected override async void Execute()
        {
            await LoadLuaModules();
            luaEnv.AddLoader(XLuaAddressablesLoader);
            await LoadLuaMain();

            Complete(new Unit());

            async System.Threading.Tasks.Task LoadLuaModules()
            {
                var h_LuaModules = Addressables.LoadAssetsAsync<TextAsset>(ConfigConstants.LUA_MODULE_ADDRESSABLES_TAG, null);
                await h_LuaModules.Task;
                var luaModules = h_LuaModules.Result;
                foreach (var luaModule in luaModules)
                {
                    m_LuaModules.Add(luaModule.name, luaModule.bytes);
                }
                //foreach (var item in m_LuaModules)
                //{
                //    Debug.Log(item.Key);
                //}
            }

            async System.Threading.Tasks.Task LoadLuaMain()
            {
                var h_luaMain = Addressables.LoadAssetAsync<TextAsset>("Lua_Main");
                await h_luaMain.Task;
                luaEnv.DoString(h_luaMain.Result.text);
            }
        }


        public byte[] XLuaAddressablesLoader(ref string filepath)
        {
            if (m_LuaModules.ContainsKey(filepath))
            {
                return m_LuaModules[filepath];
            }
            else
            {
                return null;
            }
        }

    }
}
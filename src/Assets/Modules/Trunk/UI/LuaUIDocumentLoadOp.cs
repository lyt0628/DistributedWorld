

using Cysharp.Threading.Tasks;
using QS.Api.Common;
using QS.Common;
using QS.GameLib.View;
using QS.Trunk;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace QS.UI
{
    public class LuaUIDocumentLoadOp : AsyncOpBase<LuaUIDocument>
    {
        readonly string address;
        readonly IViewNode parent;
        readonly VisualElement container;

        public LuaUIDocumentLoadOp(string address, IViewNode parent = null)
        {
            this.address = address;
            this.parent = parent;
        }

        public LuaUIDocumentLoadOp(VisualElement container, string address ,IViewNode parent = null)
        {
            this.container = container;
            this.address = address;
            this.parent = parent;
        }

        protected override async void Execute()
        {
            var h_LoadLuaModule = TrunkGlobal.Instance.Context.GetInstance<IAsyncOpHandle>(Trunk.DINames.ASYNCOP_HANDLE_LOAD_LUA_MODULES);
            var h_Script = Addressables.LoadAssetAsync<TextAsset>(address);
            // 等到 Deps 模块加载完毕再返回结果
            await UniTask.WhenAll(h_LoadLuaModule.Task.AsUniTask(),
                                  h_Script.Task.AsUniTask());

            if(container != null)
            {
                Complete(new LuaUIDocument(container, h_Script.Result.text, parent));
            }
            else
            {
                Complete(new LuaUIDocument(h_Script.Result.text, parent));
            }
        }
    }
}
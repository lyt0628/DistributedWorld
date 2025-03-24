using QS.Common;
using QS.GameLib.View;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using XLua;

namespace QS.UI
{
    public sealed class LuaUIDocument : BaseDocument<TypelessDict, TypelessDict>
    {

         LuaTable scopedEnv;

         TypelessDict luaDefaultProps;
         string luaAddress;
         bool luaIsResident = false;
         bool luaNeedPreload = false;
         Action luaOnActive;
         Action luaOnDeactive;
         Func<Task> luaOnDocumentLoaded;
         Func<TypelessDict, TypelessDict> luaLoadStatesFromProps;
         Action luaOnShutdown;
         Action luaRender;

        public LuaUIDocument(VisualTreeAsset template, string script, IViewNode parent = null) :base(template, parent)
        {
            InitLuaEnv(script);

            if (luaNeedPreload)
            {
                // 要替换成DOPreload
                LoadAsync();
            }
        }

        public LuaUIDocument(string script, IViewNode parent = null) : base(parent)
        {
            InitLuaEnv(script);

            if (luaNeedPreload)
            {
                // 要替换成DOPreload
                LoadAsync();
            }
        }

        public LuaUIDocument(VisualElement container, string script, IViewNode parent = null) : base(container, parent)
        {
            InitLuaEnv(script);

            if (luaNeedPreload)
            {
                // 要替换成DOPreload
                LoadAsync();
            }
        }

        private void InitLuaEnv(string script)
        {
            var luaEnv = UIGlobal.Instance.GetInstance<LuaEnv>();

            scopedEnv = luaEnv.NewTable();
            using (LuaTable meta = luaEnv.NewTable())
            {
                meta.Set("__index", luaEnv.Global);
                scopedEnv.SetMetaTable(meta);
            }

            scopedEnv.Set("self", this);
            luaEnv.DoString(script, nameof(LuaUIDocument), scopedEnv);

            scopedEnv.Get("address", out luaAddress);
            scopedEnv.Get("is_resident", out luaIsResident);
            scopedEnv.Get("default_props", out luaDefaultProps);
            scopedEnv.Get("need_preload", out luaNeedPreload);
            scopedEnv.Get("onActive", out luaOnActive);
            scopedEnv.Get("onDeactive", out luaOnDeactive);
            scopedEnv.Get("onDocumentLoaded", out luaOnDocumentLoaded);
            scopedEnv.Get("loadStatesFromProps", out luaLoadStatesFromProps);
            scopedEnv.Get("onShutdown", out luaOnShutdown);
            scopedEnv.Get("render", out luaRender);

            luaDefaultProps ??= new();
        }

        public override string Address => luaAddress ?? string.Empty;

        public override void OnActive() => luaOnActive?.Invoke();

        public override void OnDeactive() => luaOnDeactive?.Invoke();

        protected override Task OnDocumentLoaded()
        {
            if (luaOnDocumentLoaded != null)
            {
                return luaOnDocumentLoaded?.Invoke();
            }
            else
            {
                return base.OnDocumentLoaded();
            }

        }

        public override void Render()
        {
            luaRender?.Invoke();
        }

        protected override void LoadStatesFromProps(TypelessDict props, out TypelessDict states)
        {
            if (luaLoadStatesFromProps != null)
            {
                var s = luaLoadStatesFromProps(props);
                //Debug.Log(s);

                states = s;
            }
            else
            {
                base.LoadStatesFromProps(props, out states);
            }
        }
        protected override void DoShutdown() => luaOnShutdown?.Invoke();

        public override bool IsResident => luaIsResident;

        public override TypelessDict DefaultProps => luaDefaultProps;

    }
}
using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Common;
using QS.Player.Instrs;
using QS.PlayerControl;
using QS.Trunk;
using QS.UI;
using System.Linq;
using UnityEngine;


namespace QS.Dialog
{
    /// <summary>
    /// 这个组件只是一个容器，真的的事情由Lua脚本来做
    /// </summary>
    class DialogNPC : MonoBehaviour, IExecutor
    {
        [Injected]
        readonly IUIStack uiStack;


        LuaUIDocument uiDialogPannel;

        public IDialog[] dialogs;

        public void Execute(IInstruction instruction)
        {
            if (uiDialogPannel == null)
            {
                if (TrunkGlobal.Instance.Context.TryGetInstance<LuaUIDocument>(Trunk.DINames.UI_DIALOG_PANEL, out var doc))
                {
                    uiDialogPannel = doc;
                }
                else
                {
                    Debug.Log("Not to load");
                    return;
                }
            }

            TrunkGlobal.Instance.Context.GetInstance(Trunk.DINames.UI_DIALOG_PANEL);
            if (dialogs == null) return;
            if (uiDialogPannel.IsVisible) return;
            if (instruction is not IInteractInstr) return;

            var dialog = dialogs.First();
           
            var p = new TypelessDict();
            p.Set("speaker", "牛逼");
            p.Set("lines", dialog.Select(l => l.Line.Text).ToArray());
            var a = dialog.Select(l => l.Options);
            p.Set("options", dialog.Select(l => l.Options.Select(o=>o.Text).ToArray()).ToArray());
            uiDialogPannel.props = p;
            uiStack.Push(uiDialogPannel);

        }

        private void Start()
        {
            TrunkGlobal.Instance.Context.Inject(this);
            var op = new DialogsLoadOp();
            op.Invoke();
            op.Completed += (h) =>
            {
                dialogs = h.Result;
            };
        }
    }

}

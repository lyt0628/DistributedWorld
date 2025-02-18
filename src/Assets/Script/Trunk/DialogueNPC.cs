


using QS.Api.Chara.Instruction;
using QS.Api.Executor.Domain;
using QS.Chara.Domain;
using QS.Executor;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using QS.PlayerControl.Instrs;
using QS.UI;
using UnityEngine;

class DialogueNPC : ExecutorBehaviour
{
    private void Start()
    {
        AddLast(MathUtil.UUID(), new DestroySelfHandler(this));
    }
}

class DestroySelfHandler : BehaviourHandler
{
    public DestroySelfHandler(ExecutorBehaviour character) : base(character)
    {
    }

    public override void Read(IPipelineHandlerContext context, object msg)
    {
        if(msg is IInteractInstr)
        {
            var dialogue = UIGlobal.Instance.GetInstance<DialoguePannel>();
            dialogue.Content = "I am dangerous, help me!";
            dialogue.Show();

        }
    }
}
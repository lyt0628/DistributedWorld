


using GameLib.DI;
using QS.Api.Chara.Instruction;
using QS.Api.Executor.Domain;
using QS.Chara.Domain;
using QS.Executor;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using QS.Player.Instrs;
using QS.PlayerControl;
using QS.UI;
using UnityEngine;
using UnityEngine.InputSystem;

class DialogueNPC : ExecutorBehaviour
{

    private void Start()
    {
        AddLast(MathUtil.UUID(), new DestroySelfHandler(this));
    }
}

class DestroySelfHandler : BehaviourHandler
{
    [Injected]
    readonly PlayerControls playerControls;
    [Injected]
    readonly DialoguePannel dialoguePannel;
    public DestroySelfHandler(ExecutorBehaviour character) : base(character)
    {
        playerControls = TrunkGlobal.Instance.DI.GetInstance<PlayerControls>();
        dialoguePannel = TrunkGlobal.Instance.DI.GetInstance<DialoguePannel>();
    }

    public override void Read(IPipelineHandlerContext context, object msg)
    {
        if(msg is IInteractInstr)
        {

            dialoguePannel.Content = "I am in danger, help me!";
            dialoguePannel.Speaker = "LuXi";
            dialoguePannel.Show();


            playerControls.DialoguePanel.Continue.started += HideDialoguePannel;
        }

        context.Write(msg);
    }


    public void HideDialoguePannel(InputAction.CallbackContext context)
    {
        dialoguePannel.Hide();
        playerControls.DialoguePanel.Continue.started -= HideDialoguePannel;
    }
}
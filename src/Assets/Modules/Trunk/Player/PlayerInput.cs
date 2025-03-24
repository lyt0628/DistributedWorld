


using GameLib.DI;
using QS.Chara;
using QS.Chara.Domain;
using QS.Common;
using QS.Common.FSM;
using QS.GameLib.Util;
using QS.Player.Instrs;
using QS.PlayerControl;
using QS.Skill;
using QS.Trunk.Chara.Samurai;
using QS.Trunk.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QS.Player
{

    /// <summary>
    /// 玩家控制的接入
    /// </summary>
    public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
    {
        [Injected]
        readonly IPlayer player;
        [Injected]
        readonly PlayerControls playerActions;
        /// <summary>
        /// 通过DI 来注入指令
        /// </summary>
        [Injected]
        readonly KatanaLightAttackInstr katanaLTInstr;
        [Injected]
        readonly BowLightAttackInstr bowLTInstr;
        [Injected]
        readonly LightAttackInstr lightAttackInstr;
        [Injected]
        readonly SwitchWeaponInstr switchWeaponInstr;
        [Injected]
        readonly IJumpInstr jumpInstr;
        [Injected]
        readonly IMoveInstr moveInstr;
        [Injected]
        readonly IInteractInstr interactInstr;
        [Injected]
        readonly IAimLockInstr aimLockInstr;
        [Injected]
        readonly ICarmeraFocusInstr carmeraFocusInstr;
        [Injected]
        readonly IDodgeInstr rollInstr;
        LongPress runAction = new(.5f);
        Click rollAction = new(.5f);
        public Transform LockTarget;
        IFSM<CameraState> cameraFSM;
        private void Start()
        {
            PlayerGlobal.Instance.Inject(this);
            playerActions.Player.AddCallbacks(this);
            aimLockInstr.Target = LockTarget;
            cameraFSM = Camera.main.GetComponent<IFSM<CameraState>>();
            GetComponent<CharaControlTemplate>().meta = new CharaControlMeta
            {
                right = () => MiscUtil.Right3P,
                forward = () => MiscUtil.Forward3p,
                up = () => Vector3.up,
                runSpeed = 3.9f,
                walkSpeed = 1.9f,
                height = 1.7f,
                radius = 0.5f,
                jumpSpeed = 6f,
                fallGrivity = 9.8f,
            };
            var chara = GetComponent<Character>();
            chara.Messager.AddListener(CharaConstants.CHARA_DIE, (_) =>
            {
                chara.Frozen = true;
            });
            /// 持有PlayerInput的Chara就是Player的ActiveChara
            chara.InstructionConverter.AddConversion((i) => i is LightAttackInstr, 
                                                    (_) => {
                SamuraiFSM fsm = (SamuraiFSM)chara.ControlFSM;
                if(fsm.currentWeapon == 1)
                {
                    return katanaLTInstr;
                }
                else
                {
                    return bowLTInstr;
                }
            });

        }
        private void Update()
        {
            moveInstr.run = playerActions.Player.Dash.IsPressed();

            if (!playerActions.Player.Move.IsPressed())
            {
                moveInstr.value = Vector2.zero;
            }
            else
            {
                moveInstr.value = playerActions.Player.Move.ReadValue<Vector2>().normalized;
            }
            if (player.ActiveChara != null)
            {
                player.ActiveChara.Execute(moveInstr);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                player.ActiveChara.Execute(switchWeaponInstr);
            }
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                player.ActiveChara.Execute(lightAttackInstr);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            //moveInstr.value = context.ReadValue<Vector2>().normalized;
            //player.ActiveChara?.Invoke(moveInstr);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (player.ActiveChara == null) return;

            if (context.started)
            {
                var hitColliders = Physics.OverlapSphere(
                player.ActiveChara.transform.position, 3f);
                foreach (var hitCollider in hitColliders)
                {
                    var direction = hitCollider.transform.position - player.ActiveChara.transform.position;
                    if (Vector3.Dot(player.ActiveChara.transform.forward, direction) > .5f)
                    {
                        if (hitCollider.TryGetComponent<IExecutor>(out var executor))
                        {
                            executor.Execute(interactInstr);
                        }
                    }

                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && player.ActiveChara != null)
            {
                player.ActiveChara.Execute(jumpInstr);
            }

        }

        public void OnDash(InputAction.CallbackContext context)
        {
            moveInstr.run = runAction.Input(context.ReadValueAsButton());
            if (rollAction.Input(context.ReadValueAsButton()))
            {
                if (player.ActiveChara != null)
                {
                    player.ActiveChara.Execute(rollInstr);
                }
            }

        }

        public void OnView(InputAction.CallbackContext context)
        {
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
        }

        public void OnAimLock(InputAction.CallbackContext context)
        {
            // 前一次已经执行了锁定
            if (aimLockInstr.Target != null)
            {
                aimLockInstr.Target = null;
                player.ActiveChara.Execute(aimLockInstr);

                carmeraFocusInstr.FocusTarget = null;
                cameraFSM.TryHande(carmeraFocusInstr);

            }
            else
            {
                aimLockInstr.Target = LockTarget;
                player.ActiveChara.Execute(aimLockInstr);

                carmeraFocusInstr.FocusTarget = LockTarget;

                cameraFSM.TryHande(carmeraFocusInstr);
            }
        }
    }
}
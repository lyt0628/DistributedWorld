



using QS.Api.Executor.Domain;
using QS.Chara.Abilities;
using QS.Chara.Domain;
using QS.Common.FSM;
using QS.GameLib.Pattern.Message;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Chara
{

    public class BeforeHitEvent : UnityEvent
    {

    }

    /// <summary>
    /// ״̬������û��Ҫ��ΪMonobehaviourʵ��
    /// </summary>
    public abstract class CharaControlTemplate : FSMBehaviour<CharaState>
    {

        public CharaControlMeta meta;
        /// <summary>
        /// �������������Ϣ����ģ��
        /// </summary>
        public ICharaControlPhysicalProbe PhysicalProbe { get; set; }
        public ICharaControlMotor Motor { get; set; }

        public Character Chara { get; protected set; }

        #region [[״̬]]
        /// ��Щ�����ֶ��ɲ���ģ�����ã����ǲ���ģ������״̬����
        public Vector3 Velocity => Motor.Velocity;
        public Vector2 HorVelocity => Motor.HorVelocity;
        public Vector3 LocalVelocity => Motor.LocalVelocity;
        public Vector2 LocalHorVelocity => Motor.LocalHorVelocity;
        public bool IsRasing => Motor.IsRaising;
        public bool IsFalling => Motor.IsFalling;
        public bool IsBlocking { get; private set; } = false;
        public float HorizontalSpeed => Motor.HorizontalSpeed;
        public float ForwardSpeed => Motor.ForwardSpeed;
        public float RightSpeed => Motor.RightSpeed;
        public float TurnSpeed => Motor.TurnSpeed;
        public bool IsGrounded => PhysicalProbe.IsGrounded;
        public bool IsGroundedStrict => PhysicalProbe.IsGroundedStrict;

        public bool AimLock => AimTarget != null;
        public Transform AimTarget { get; private set; } = null;
        #endregion

        #region [[Reusable Input]]

        public Vector2 MoveDir { get; private set; } = Vector2.zero;
        public bool Run { get; private set; } = false;
        public bool Jump { get; private set; } = false;
        public bool Hit { get; private set; } = false;
        /// <summary>
        /// �������Եķ�λ������ڼ��������¶ε����ֻ����
        /// </summary>
        public Vector3 AttackDir { get; private set; } = Vector3.zero;
        public float AttackForce { get; private set; } = 0f;
        public float hitStopTime = 0f;
        #endregion

        public BeforeHitEvent BeforeHit { get; } = new();
        public void ApplyDefaultRotation()
        {
            //Debug.Log(Motor.Rotation);
            transform.rotation = Motor.Rotation;
        }
        public void ApplyDefaultDosplacement()
        {
            transform.position += Motor.ClampedDisplacement;
        }


        public override CharaState DefaultState => CharaState.Idle;
        public override ProcessTime ProcessTime { 
            get
            {
                if (Transiting)
                {
                    return ((ICharaStateTransition)ActiveTransition).ProcessTime;
                }
                else
                {
                   return ((CharaStateBase)GetState(CurrentState)).ProcessTime;
                }
            }
        }


        protected override void DoProcess()
        {


            //Debug.Log($"{gameObject.name} Current State : {CurrentState}");
            //Debug.Log($"{name}>>>Control State: {CurrentState}");
            // ���£�����Ĭ�ϵ��ƶ���Ϣ
            if (CurrentProcessTime is ProcessTime.Update)
            {
                Motor.Update();
            }

            // �����ƶ���Ϣ�������ã�����Ǹ������Ļ������λ��
            // Ϊ�ˣ������� OnAnimMove�׶ν��и���
            // ��Ȼ����ʱ����Template�����ˣ���״̬��û�б�Ҫ��ΪMonoBehaviourʵ��
            base.DoProcess();

            if (CurrentProcessTime is ProcessTime.Update)
            {
                if (CurrentState is CharaState.Jumping && IsGroundedStrict && IsFalling)
                {
                    Debug.Log($"{gameObject.name} From Jump to Grounded");
                    SwitchTo(CharaState.Idle);
                }

                ClearStates();
            }
        }

        void ClearStates()
        {
            Jump = false;
            Run = false;
            MoveDir = Vector3.zero;
            Hit = false;
        }

        /// <summary>
        /// �һ��æ�����ת����ϸ�ڣ����ǵ���֧��ʲô�Լ�����
        /// </summary>
        /// <param name="instruction"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Handle(IInstruction instruction)
        {
            GatherInput(instruction);

            switch (instruction)
            {
                case JumpInstr:
                    HandleJumpInstr();
                    break;
                case BlockInstr:
                    HandleBlockInstr();
                    break;
                case HitInstr:
                    HandleHitInstr();
                    break;
                case DodgeInstr:
                    HandleRollInstr();
                    break;
            }
        }

        private void HandleRollInstr()
        {
            if (CurrentState is CharaState.Walking or CharaState.Runing or CharaState.Idle)
            {
                SwitchTo(CharaState.Dodge);
            }
        }

        private void HandleHitInstr()
        {
            //Debug.Log($"{gameObject.name} got hit instr");
            // �ȷ�˵���ܣ����ܵ�������ʱ���ܲ��ܴ��
            BeforeHit.Invoke();
            if (CurrentState is CharaState.Idle or CharaState.Walking or CharaState.Runing or CharaState.Hit)
            {
                //Debug.Log($"{gameObject.name} Switch to Hit State");
                SwitchTo(CharaState.Hit);
            }
        }

        private void HandleBlockInstr()
        {
            switch (CurrentState)
            {
                case CharaState.Idle or CharaState.Walking or CharaState.Runing:
                    SwitchTo(CharaState.Blocking);
                    break;
                default:
                    SwitchTo(CharaState.Idle);
                    break;
            }
        }

        private void HandleJumpInstr()
        {
            if (CurrentState is CharaState.Idle or CharaState.Walking or CharaState.Runing
                && IsGrounded && IsFalling)
            {
                Debug.Log($"{gameObject.name} To Jump State");
                Jump = true;
                SwitchTo(CharaState.Jumping);
            }
        }

        private void GatherInput(IInstruction instruction)
        {
            if (instruction is MoveInstr moveInstr)
            {
                MoveDir = moveInstr.value;
                Run = moveInstr.run;
            }

            if (instruction is HitInstr hitInstr)
            {
                Hit = true;
                AttackDir = hitInstr.AttackDir;
                AttackForce = hitInstr.AttackForce;
                hitStopTime = hitInstr.HitStopTime;
            }

            if (instruction is AimLockInstr aimlockInstr)
            {
                AimTarget = aimlockInstr.Target;
            }
        }

        public abstract CharaStateBase GetCharaState(CharaState state);

        public override IState<CharaState> GetState(CharaState state)
        {
            return GetCharaState(state);
        }

        protected virtual void Start()
        {
            Chara = GetComponent<Character>();

            Chara.Messager.AddListener(CharaConstants.CHARA_DIE, OnCharaDie);
        }

        void OnCharaDie(IMessage _)
        {
            Chara.Animator.SetBool("Dead", true);
        }
    }
}
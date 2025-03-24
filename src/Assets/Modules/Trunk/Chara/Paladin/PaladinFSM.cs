

//using QS.Api.Executor.Domain;
//using QS.Chara.Abilities;
//using QS.Common.FSM;
//using UnityEngine;

//namespace QS.Chara
//{
//    public class PaladinFSM : CharaControlTemplate
//    {
//        SamuraiGrounded_Katana katanaMove;
//        SamuraiHit_Katana katanaHit;
//        CharaStateBase rootMotionControl;


//        private void Start()
//        {
//            meta = new CharaControlMeta
//            {
//                right = () => Vector3.right,
//                forward = () => Vector3.forward,
//                up = () => Vector3.up,
//                runSpeed = 3.0f,
//                walkSpeed = 0.5f,
//                height = 1.6f,
//                radius = 0.5f,
//            };

//            Motor = new DefaultCharaControlMotor(this);
//            PhysicalProbe = new DefaultCharaControlPhysicalProbe(this);

//            katanaMove = new SamuraiGrounded_Katana(this);
//            rootMotionControl = new DefaultRootMotionState(this);
//            katanaHit = new SamuraiHit_Katana(this);
//        }

//        public override bool CanHandle(IInstruction instruction)
//            => instruction is IMoveInstr or IHitInstr or IAimLockInstr;

//        public override IState<CharaState> GetState(CharaState state)
//        {
//            return state switch
//            {
//                CharaState.Idle => katanaMove,
//                CharaState.Walking => katanaMove,
//                CharaState.Runing => katanaMove,
//                CharaState.Hit => katanaHit,
//                CharaState.RootMotion => rootMotionControl,
//                _ => throw new System.NotImplementedException(),
//            };
//        }

//        public override void Handle(IInstruction instruction)
//        {
//            if (instruction is IHitInstr hitInstr)
//            {
//                katanaHit.hitStopTime = hitInstr.HitStopTime;
//            }
//            base.Handle(instruction);
//        }
//    }
//}

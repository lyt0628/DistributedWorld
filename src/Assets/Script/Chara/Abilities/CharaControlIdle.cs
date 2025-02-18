

using GameLib.DI;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using QS.Common.ComputingService;
using QS.GameLib.Rx.Relay;
using QS.Motor;
using UnityEngine;

namespace QS.Chara.Abilities
{
    class CharaControlIdle : BaseCharaControlState
    {
        [Injected]
        readonly DataSource<FreeFallControl.Input, FreeFallControl.State> dataSource;
        [Injected]
        readonly FreeFallControl control;

        readonly FreeFallControl.Input input;
        FreeFallControl.Result result;
        readonly IMotion motion;
        readonly string uuid;
        
        public CharaControlIdle(Character chara, BaseCharaControlAbilityWrapper wrapper) : base(chara, wrapper)
        {
            CharaGlobal.Instance.DI.Inject(this);
            //input = dataSource.Create();
            input = new FreeFallControl.Input();
            var state = new FreeFallControl.State();
            
            var dataRelay = Relay<FreeFallControl.Input>.Tick(() =>
            {
                input.grivityDir = Vector3.down;
                input.grivityValue = 1f;
                input.position = Character.transform.position;
                return input;
            }, out motion);
            uuid = dataSource.Add(dataRelay);
            control.Get(uuid).Subscrib((t) => result = t);
        }

        public override CharaControlState State => CharaControlState.Idle;

        public override void OnControl(ICharaControlInstr instr)
        {
            motion.Set();
            Character.transform.position += result.disp;
        }

        public override void OnFrozenControl(ICharaControlInstr instr)
        {
            motion.Set();
            Character.transform.position += result.disp;
        }

        public override void OnEnter()
        {
            var anim = Character.GetComponent<Animator>();
            anim.SetFloat("Speed", 0);
        }

        public override void OnExit()
        {
            control.Reset(uuid);
        }
    }
}
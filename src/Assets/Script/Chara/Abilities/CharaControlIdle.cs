

using GameLib.DI;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using QS.Common;
using QS.Common.ComputingService;
using QS.GameLib.Rx.Relay;
using QS.Motor;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Chara.Abilities
{
    class CharaControlIdle : BaseCharaControlState
    {
       
        readonly IMotion motion;
        public CharaControlIdle(Character chara, BaseCharaControlAbilityWrapper wrapper) : base(chara, wrapper)
        {
            
            MotorFlow
                .Combine(new List<IObservable<MotorResult>>()
                {
                    MotorFlow.Grivity(Vector3.down, 1.0f, Relay<GrivityInput>.Tick(()=>new GrivityInput(Character.transform.position), out motion))
                })
                .Clamp()
                .Subscrib((r) => Character.transform.position += r.displacement);
        }

        public override CharaControlState State => CharaControlState.Idle;

        public override void OnControl(ICharaControlInstr instr)
        {
            motion.Set();
        }

        public override void OnFrozenControl(ICharaControlInstr instr)
        {
            motion.Set();
        }

        public override void OnEnter()
        {
            var anim = Character.GetComponent<Animator>();
            anim.SetFloat("Speed", 0);
        }
    }
}

using GameLib.DI;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using QS.Common;
using QS.Common.ComputingService;
using QS.Executor.Domain.Handler;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using QS.Motor;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Chara.Abilities
{
    class CharaControlMove : BaseCharaControlState
    {
        readonly MotionGroup motion;
        readonly MoveInput mi = new();
        float speed = 0f;
        public CharaControlMove(Character character, BaseCharaControlAbilityWrapper wrapper) : base(character, wrapper)
        {
            

            MotorFlow
                .Combine(new List<GameLib.Rx.Relay.IObservable<MotorResult>>{
                    MotorFlow.Grivity(Vector3.down, 1.0f, Relay<GrivityInput>.Tick(()=>new GrivityInput(Character.transform.position), out var m1)),
                    MotorFlow.Move( Relay<MoveInput>.Tick(()=>mi, out var m2))
                })
                .Clamp()
                .Subscrib((r) => {
                    Character.transform.position += r.displacement;
                    var anim = Character.GetComponent<Animator>();
                    speed = r.velocity.magnitude;
                    if (speed != 0)
                    {
                        anim.SetFloat("Speed", speed);
                        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Quaternion.LookRotation(r.velocity.normalized), 5f * Time.deltaTime);
                    }
                } );
            motion = new MotionGroup();
            motion.Add(m1);
            motion.Add(m2);
        }

        public override CharaControlState State  
        {
            get
            {
              
                if (speed > 2.0f) return CharaControlState.Runing;
                else return CharaControlState.Walking;
            }
        }

        float speedFactor = 1.0f;
        float smoothDampVelocity;
        public override void OnControl(ICharaControlInstr instr)
        {
            BuildInput(instr);
            motion.Set();

        }

     
        public override void OnFrozenControl(ICharaControlInstr instr)
        {
            BuildInput(instr);
            mi.horizontal = 0f;
            mi.vertical = 0f;
            motion.Set();
        }

        private void BuildInput(ICharaControlInstr instr)
        {
            if (instr.Dash)
            {
                speedFactor = Mathf.SmoothDamp(speedFactor, 4.0f, ref smoothDampVelocity, 1.0f);

            }
            else
            {
                speedFactor = 1.0f;
            }

            mi.position = Character.transform.position;
            mi.horizontal = instr.Horizontal;
            mi.vertical = instr.Vertical;
            mi.speedFactor = speedFactor;
            mi.forward = instr.Baseforword;
            mi.right = instr.BaseRight;
            mi.up = instr.BaseUp;
        }

    }
}
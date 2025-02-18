
using GameLib.DI;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using QS.Common.ComputingService;
using QS.Executor.Domain.Handler;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using QS.Motor;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Chara.Abilities
{
    class CharaControlMove : BaseCharaControlState
    {
        [Injected]
        readonly MoveControl control;
        [Injected]
        readonly DataSource<MoveControl.Input, MoveControl.State> dataSource;
        readonly IMotion motion;
        readonly MoveControl.Input input;
        MoveControl.Result result;

        public CharaControlMove(Character character, BaseCharaControlAbilityWrapper wrapper) : base(character, wrapper)
        {
            CharaGlobal.Instance.DI.Inject(this);

            //input = dataSource.Create();
            input = new MoveControl.Input();
            var dataRelay = Relay<MoveControl.Input>.Tick(() => input, out motion);
            var uuid = dataSource.Add(dataRelay);
            control.Get(uuid).Subscrib((t) => result = t);
        }

        public override CharaControlState State  {get
            {
                if (result is null) return CharaControlState.Walking;
                else if (result.speed > 3.0f) return CharaControlState.Runing;
                else return CharaControlState.Walking;
            }
        }
        float speedFactor = 1.0f;
        float smoothDampVelocity;
        public override void OnControl(ICharaControlInstr instr)
        {
            input.up = instr.BaseUp;
            input.right = instr.BaseRight;
            input.forward = instr.Baseforword;
            input.crossInput = new Vector2(instr.Horizontal, instr.Vertical);
            input.grivity = 1.0f;
            input.speedFactor = 1.0f;
            input.position = Character.transform.position;

            //Debug.Log(instr.Dash);
            if (instr.Dash)
            {
                speedFactor = Mathf.SmoothDamp(speedFactor, 4.0f,ref smoothDampVelocity, 5.0f);
                
            }
            else
            {
                speedFactor = 1.0f;
            }
            input.speedFactor = speedFactor;


            motion.Set();
            Character.transform.position += result.disp;
            if (result.face != Quaternion.identity)
            {
                Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, result.face, 5f * Time.deltaTime);
            }

            var anim = Character.GetComponent<Animator>();
            if(result.speed != 0)
            {
                anim.SetFloat("Speed", result.speed);
            }
          
        }

        public override void OnFrozenControl(ICharaControlInstr instr)
        {
            input.up = instr.BaseUp;
            input.right = instr.BaseRight;
            input.forward = instr.Baseforword;
            input.crossInput = Vector2.zero;
            input.grivity = 1.0f;
            input.position = Character.transform.position;

            motion.Set();
            Character.transform.position += result.disp;
        }

    }
}
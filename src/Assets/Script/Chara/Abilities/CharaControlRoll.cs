
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using System.Collections;
using UnityEngine;

namespace QS.Chara.Abilities
{
    class CharaControlRoll : BaseCharaControlState
    {
        readonly Animator animator; 

        public CharaControlRoll(Character character, BaseCharaControlAbilityWrapper wrapper) : base(character, wrapper)
        {
            animator = character.GetComponent<Animator>();
        }

        public override CharaControlState State => CharaControlState.Roll;

        float speed = 4f;
        public override void OnEnter()
        {
            animator.SetTrigger("Roll");
            Character.StartCoroutine(Next());
        }

        public override void OnControl(ICharaControlInstr instr)
        {
            Character.transform.position += speed * Time.deltaTime * Character.transform.forward;
        }
 
        public override void OnFrozenControl(ICharaControlInstr instr)
        {
            //throw new System.NotImplementedException();
        }

        IEnumerator Next()
        {
            yield return new WaitForSeconds(.5f);
            wrapper.SwitchTo(CharaControlState.Idle, this);
        }
    }
}


using GameLib.DI;
using QS.Api;
using QS.Api.Chara.Instruction;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
using QS.Chara.Domain;
using QS.Chara.Instrs;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using System;
using System.Collections;
using UnityEngine;


namespace QS.Chara.Abilities
{
    /// <summary>
    /// �󲿷�ʱ�����Ǳ��뿿ά��״̬�������쳣
    /// </summary>
    class ShuffleStepAbility : Ability
    {
        public static Type[] blockedInstrs = new[]
        {
            typeof(ICharaControlInstr),
            typeof(IShuffleStepInstr),
            typeof(IInjuredInstr) // �沽ʱ���޵�
        };

        bool casting = false;
        bool colldown = false;
        Vector3 movement;

        [Injected]
        readonly IInstructionFactory instrFactory;
        [Injected]
        readonly ILifecycleProivder life;
        public ShuffleStepAbility(Character character) : base(character)
        {
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if (msg is not IShuffleStepInstr shuffleStepInstr)
            {
                context.Write(msg);
                return;
            }
            if (casting || colldown) {
                return;
            }

            casting = true;
            Character.Execute(instrFactory.AddBlockedInstrs(blockedInstrs));
            Character.gameObject.GetComponent<Animator>().SetTrigger("ShuffleStep");

            //����ֻ���ұߵ沽
            movement = shuffleStepInstr.BaseRight;

            life.UpdateAction.AddListener(Move);

            Character.StartCoroutine(EndShuffle());

        }


        void Move()
        {
            //Character.transform.position += 8 * Time.deltaTime * movement.normalized;
        }
        IEnumerator EndShuffle()
        {
            yield return new WaitForSeconds(0.3f);
            life.UpdateAction.RemoveListener(Move);
            Character.Execute(instrFactory.RemoveBlockedInstrs(blockedInstrs));
            casting = false;
            colldown = true;
            
            Character.StartCoroutine(Refresh() );
        }

        IEnumerator Refresh()
        {
            yield return new WaitForSeconds(3);
            colldown = false;
        }
    }
}
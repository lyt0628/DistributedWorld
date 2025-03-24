


using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Abilities;
using QS.Chara.Domain;
using QS.Chara.Domain.Handler;
using QS.Executor.Domain.Handler;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Chara.Service
{
    /// <summary>
    /// ����Ҫ�������ע��
    /// </summary>
    class CharaAblityFactory : ICharaAblityFactory
    {

        public IInstructionHandler ShuffleStep(Character executor)
        {
            var a = new ShuffleStepAbility(executor);
            CharaGlobal.Instance.DI.Inject(a);
             return a;
        }

        // ��Ҫ��̬���ڴ��������õ���, Ϊʲô�������???? �ڴ��޷��ͷ� TLS
        //readonly IDIContext context = CharaGlobal.Instance.DI;



        public ICharaConrolAbility CharaControl(Character chara)
        {
            var ability = new DefaultCharaControlAbility(chara);
            CharaGlobal.Instance.DI.Inject(ability);
            return ability;
        }
    }
}
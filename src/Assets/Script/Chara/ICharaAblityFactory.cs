using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Abilities;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Api.Chara.Service
{
    public interface ICharaAblityFactory
    {
        ICharaConrolAbility CharaControl(Character executor);
        IInstructionHandler ShuffleStep(Character executor);
    }
}
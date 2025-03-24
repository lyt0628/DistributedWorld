

using QS.Chara.Abilities;
using QS.Common.FSM;

namespace QS.Chara
{
    public interface ICharaStateTransition : ITransition<CharaState>
    {
        ProcessTime ProcessTime { get; }
    }
}
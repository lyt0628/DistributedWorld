


using GameLib.DI;
using QS.Skill.Domain.Handler;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// Lua�Č��F����ţ����OӋ��Lua�h��ǰ����Ҫ���@��
    /// </summary>
    [Scope(Value =ScopeFlag.Sington, Lazy = false)]
    class ScriptableSubHandler : SimpleSkillSubHandlerAdapter
    {
        [Injected]
        public ScriptableSubHandler(ISubHandlerRegistry registry)
        {
            registry.Register("Scriptable", this);
        }
    }
}
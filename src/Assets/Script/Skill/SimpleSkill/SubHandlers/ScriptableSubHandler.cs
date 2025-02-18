


using GameLib.DI;
using QS.Skill.Domain.Handler;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// Lua的實現往後放，在設計好Lua環境前，不要動這個
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



using GameLib.DI;
using QS.Api.Common;
using QS.Chara;
using QS.Common;
using QS.Executor;
using QS.PlayerControl;
using QS.Skill;

namespace QS.Agent
{
    public class AgentGlobal : ModuleGlobal<AgentGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        protected override IDIContext DIContext { get; } = IDIContext.New();

        public AgentGlobal() {
            CommonGlobal.Instance.ProvideBinding(DI);
            ExecutorGlobal.Instance.ProvideBinding(DI);
            CharaGlobal.Instance.ProvideBinding(DI);
            PlayerControlGlobal.Instance.ProvideBinding(DI);
            SkillGlobal.Instance.ProvideBinding(DI);

            DI.Bind<Steering>();
        }


        public override void ProvideBinding(IDIContext context)
        {
          
        }
    }

}
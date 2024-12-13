


using GameLib.DI;
using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.Control;
using QS.Executor.Domain.Handler;
using QS.Executor.Service;

namespace QS.Executor
{
    public class ExecutorGlobal : ModuleGlobal<ExecutorGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public ExecutorGlobal()
        {
            ControlGlobal.Instance.ProvideBinding(DI);
        }

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(new InstructionFactory())
                .BindExternalInstance(new InstructionHandlerFactory())
                .Bind<BaseExecutor>(Api.Executor.DINames.BaseExecutor, ScopeFlag.Prototype);
        }

    }
}
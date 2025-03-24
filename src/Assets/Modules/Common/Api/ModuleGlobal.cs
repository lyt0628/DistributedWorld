
using GameLib.DI;
using QS.Common;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;

namespace QS.Api.Common
{
    public abstract class ModuleGlobal<T>
        : Sington<T>, IModuleGlobal where T : IModuleGlobal, new()
    {

        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Initializing;

        protected IDIContext DIContext { get; } = IDIContext.New();

        public IMessager Messager { get; } = new Messager();

        public static string MSG_READY = "ready";


        public R GetInstance<R>()
        {
            return DIContext.GetInstance<R>();
        }

        public R GetInstance<R>(string name)
        {
            return DIContext.GetInstance<R>(name);
        }
        public bool TryGetInstance<R>(string name, out R obj)
        {
            try
            {
                obj = DIContext.GetInstance<R>(name);
                return true;
            }
            catch (BindingNotFoundException)
            {
                obj = default;
                return false;
            }
        }

        public void ReviceBinding(object instance)
        {
            DIContext.BindExternalInstance(instance);
        }

        public void Inject(object instance)
        {
            DIContext.Inject(instance);
        }


        public void SetupBinding(ITrunkGlobal global)
        {

            /// 所有的子模块都以全局为父上下文
            DIContext.SetParent(global.Context);
            DoSetupBinding(global.Context, DIContext);
        }

        protected virtual void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext) { }

        public IAsyncOpHandle<IModuleGlobal> LoadAsync()
        {
            LoadOp.Invoke();
            return LoadOp.Handle;
        }
        public IAsyncOpHandle<IModuleGlobal> LoadHandle => LoadOp.Handle;

        protected abstract AsyncOpBase<IModuleGlobal> LoadOp { get; }
    }
}
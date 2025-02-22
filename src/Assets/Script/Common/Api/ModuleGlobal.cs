



using GameLib.DI;
using QS.Common;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace QS.Api.Common
{
    public abstract class ModuleGlobal<T>
        : Sington<T>, IMessagerProvider, IModuleGlobal where T : IModuleGlobal, new()
    {
        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Initializing;

        protected abstract IDIContext DIContext {get;}

        public IMessager Messager { get; } = new Messager();
        
        public static string MSG_READY = "ready";
        public UnityEvent OnReady { get; } = new();

        public R GetInstance<R>()
        {
            return DIContext.GetInstance<R>();
        }

        public R GetInstance<R>(string name)
        {
            return DIContext.GetInstance<R>(name);
        }

        public void ReviceBinding(object instance)
        {
            DIContext.BindExternalInstance(instance);
        }
        public virtual void Initialize()
        {
            ResourceStatus = ResourceInitStatus.Started;
            OnReady?.Invoke();
            Messager.Boardcast(MSG_READY, Msg0.Instance);
        }

        public virtual void ProvideBinding(IDIContext context) { }

    }
}
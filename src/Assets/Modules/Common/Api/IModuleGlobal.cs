using QS.Api.Common;

namespace QS.Common
{
    public interface IModuleGlobal : IInstanceProvider, IBindingReciver
    {
        /// <summary>
        /// 为全局环境进行绑定
        /// </summary>
        /// <param name="global"></param>
        void SetupBinding(ITrunkGlobal global);
        IAsyncOpHandle<IModuleGlobal> LoadAsync();
        IAsyncOpHandle<IModuleGlobal> LoadHandle { get; }
    }
}
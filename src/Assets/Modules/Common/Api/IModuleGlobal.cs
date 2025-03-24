using QS.Api.Common;

namespace QS.Common
{
    public interface IModuleGlobal : IInstanceProvider, IBindingReciver
    {
        /// <summary>
        /// Ϊȫ�ֻ������а�
        /// </summary>
        /// <param name="global"></param>
        void SetupBinding(ITrunkGlobal global);
        IAsyncOpHandle<IModuleGlobal> LoadAsync();
        IAsyncOpHandle<IModuleGlobal> LoadHandle { get; }
    }
}


namespace QS.Api.Common
{
    public interface IInstanceProvider
    {
        T GetInstance<T>();
        T GetInstance<T>(string name);

    }
}

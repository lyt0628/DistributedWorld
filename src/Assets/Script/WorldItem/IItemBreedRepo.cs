

using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;

namespace QS.Api.WorldItem
{
    public interface IItemBreedRepo
    {
        string[] GetItemNames();
        T GetItemBreed<T>(string name) where T : IItemBreed;
        IItemBreed GetItemBreed(string name);
    }

}
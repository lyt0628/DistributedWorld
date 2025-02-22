

using QS.WorldItem.Domain;

namespace QS.WorldItem
{
    class DefaultProp : BaseItem<IPropBreed>, IProp
    {
        public DefaultProp(IPropBreed breed, string uuid) : base(breed, uuid)
        {

        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }

    }
}
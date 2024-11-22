


using QS.Impl.Data.Model;

namespace QS.API.Data.Model
{
    class SimpleItem : IItem
    {
        private readonly SimpleItemBreed breed;
        public SimpleItem(SimpleItemBreed itemBreed)
        {
            breed = itemBreed;
        }

        public string UUID { get => ((IItem)breed).UUID; set => ((IItem)breed).UUID = value; }
        public string Name { get => ((IItem)breed).Name; set => ((IItem)breed).Name = value; }
        public ItemType Type { get => ((IItem)breed).Type; set => ((IItem)breed).Type = value; }
    }
}
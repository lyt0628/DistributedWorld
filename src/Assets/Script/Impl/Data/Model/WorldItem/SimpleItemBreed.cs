



using QS.API.Data.Model;

namespace QS.Impl.Data.Model
{
    class SimpleItemBreed : IItem
    {
        public string UUID { get; set; }
        public string Name {  get; set; }
        public ItemType Type { get; set; }
    }
}

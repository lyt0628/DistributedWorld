using System;

namespace QS.Domain.Item
{
    public abstract class AbstractItem : IItem
    {
        private string uuid;
        private readonly IItem delegat;
        public AbstractItem(IItem bread)
        {
            this.delegat = bread;
        }

        public AbstractItem(string uuid, IItem bread)
        {
            this.uuid = uuid;
            this.delegat = bread;
        }

        public string UUID
        {
            get => uuid;
            set
            {
                if (uuid == null)
                    new InvalidOperationException("UUID is assigned aleady");
                uuid = value;
            }
        }
        public string Name => delegat.Name;
        public ItemType Type => delegat.Type;
        public string Img => delegat.Img;
        public string Prefab => delegat.Prefab;
        public string Description => delegat.Description;
        public abstract IItem Clone();
    }
}
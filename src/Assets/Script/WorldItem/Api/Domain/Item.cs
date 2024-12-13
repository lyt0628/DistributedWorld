using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    public abstract class Item
        : IItem, IItemAttribute_tag
    {
        protected ItemBreed breed;
        public ItemBreed Breed
        {
            get => breed;
            set => breed = value;
        }


        private string uuid;
        public string UUID
        {
            get => uuid;
            set
            {
                if (uuid != null)
                {
                    throw new System.InvalidOperationException();
                }
                uuid = value;
            }
        }

        public string Name
        {
            get => breed.Name;
            set => breed.Name = value;
        }

        public ItemType Type
        {
            get => breed.Type;
            set => breed.Type = value;
        }

        public string Img
        {
            get => breed.Img; set => breed.Img = value;
        }

        public string Prefab
        {
            get => breed.Prefab; set => breed.Prefab = value;
        }

        public string Description
        {
            get => breed.Description; set => breed.Description = value;
        }

        /// <summary>
        /// 在原物体上产生新物体
        /// </summary>
        /// <returns></returns>
        public abstract Item Clone();
    }
}
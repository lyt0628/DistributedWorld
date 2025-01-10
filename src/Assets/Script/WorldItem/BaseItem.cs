using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{

    public abstract class BaseItem : IItem 
    {
        public BaseItem(IItemBreed breed, string uuid)
        {
            // 類對象這邊使用即時複製的方式，單純覺得這樣可以而以
            UUID = uuid;
            Name = breed.Name;
            Type = breed.Type;
            Sprite = breed.Sprite;
            Prefab = breed.Prefab;
            Description = breed.Description;
        }


        public string UUID { get; }

        public string Name { get; }

        public ItemType Type { get; }

        public string Sprite {  get; }

        public string Prefab {  get; }

        public string Description { get; }


    }
}
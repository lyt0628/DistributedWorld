using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{

    public abstract class BaseItem : IItem 
    {
        public BaseItem(IItemBreed breed, string uuid)
        {
            // ����@߅ʹ�ü��r�}�u�ķ�ʽ���μ��X���@�ӿ��Զ���
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
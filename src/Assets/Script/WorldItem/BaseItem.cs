using QS.Api.Common;
using QS.Api.WorldItem.Domain;
using Tomlet.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace QS.WorldItem.Domain
{

    public abstract class BaseItem<T> : IItem where T : IItemBreed
    {
        public BaseItem(T breed ,string uuid)
        {
            UUID = uuid;
            this.Breed = breed;
        }
        protected T Breed { get; } 

        public string UUID { get; }

        public string Name => Breed.Name;

        public ItemType Type => Breed.Type;

        /// <summary>
        /// 武器预制体的地址
        /// </summary>
        public string Prefab => Breed.Prefab;
        /// <summary>
        /// 物品的描述，运行富文本
        /// </summary>
        public string Description => Breed.Description;

        public Sprite Image => Breed.Image;

        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Shutdown;

        public UnityEvent OnReady { get; } = new();

        public void Initialize()
        {
            if(Breed.ResourceStatus == ResourceInitStatus.Started)
            {
                ResourceStatus = ResourceInitStatus.Started;
            }
            else if(Breed.ResourceStatus == ResourceInitStatus.Initializing)
            {
                Breed.OnReady.AddListener(()=>
                {
                    ResourceStatus = ResourceInitStatus.Started;
                    OnReady.Invoke();
                });
            }

            Breed.OnReady.AddListener(() =>
            {
                ResourceStatus = ResourceInitStatus.Started;
                OnReady.Invoke();
            });
            Breed.Initialize();
        }
    }
}
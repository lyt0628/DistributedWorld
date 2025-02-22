using Cysharp.Threading.Tasks;
using QS.Api.Common;
using QS.Api.WorldItem.Domain;
using Tomlet.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The FlyWight of BaseItem. 
    /// and can be called as type object pattern.
    /// </summary>
    abstract class BaseItemBreed : IItemBreed
    {
        
        public string Name { get; set; }
        public ItemType Type { get; set; }

        [TomlNonSerialized]
        public Sprite Image { get; protected set; }
        [TomlProperty(mapFrom: "Image")]
        protected string Image_ { get; set; }

        public string Prefab { get; set; }

        public string Description { get; set; }

        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Shutdown;

        public UnityEvent OnReady { get; } = new();

        public virtual async void Initialize()
        {
            ResourceStatus = ResourceInitStatus.Initializing;

            var h1 = Addressables.LoadAssetAsync<Sprite>(Image_);
            h1.Completed += (h) => Image = h.Result;

            await h1.ToUniTask();
            ResourceStatus = ResourceInitStatus.Started;
            OnReady.Invoke();
         }
    }
}

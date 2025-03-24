
using QS.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.WorldItem
{
    class PropFlyweightLoadOp : AsyncOpBase<IProp>
    {
        readonly PropFlyWeight prop;

        public PropFlyweightLoadOp(PropFlyWeight prop)
        {
            this.prop = prop;
        }

        protected override async void Execute()
        {
            var loadImg = Addressables.LoadAssetAsync<Sprite>(prop.imageAddress);
            await loadImg.Task;
            prop.image = loadImg.Result;
            Complete(prop);
        }
    }
}
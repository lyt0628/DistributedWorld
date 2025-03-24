

using QS.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.WorldItem
{
    class MaterialLoadOp : AsyncOpBase<IMaterial>
    {
        readonly DefaultMaterial mat;

        public MaterialLoadOp(DefaultMaterial mat)
        {
            this.mat = mat;
        }

        protected override async void Execute()
        {
            var loadImg = Addressables.LoadAssetAsync<Sprite>(mat.imageAddress);
            await loadImg.Task;
            mat.image = loadImg.Result;
            Complete(mat);
        }
    }
}
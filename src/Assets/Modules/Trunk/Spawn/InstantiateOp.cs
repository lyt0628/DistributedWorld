

using Cysharp.Threading.Tasks;
using QS.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.Spawn
{
    class InstantiateOp : AsyncOpBase<GameObject>
    {
        readonly SpawnPoint point;
        readonly string prefabAddress;

        protected override async void Execute()
        {
            var loadPrefab = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
            await loadPrefab.Task.AsUniTask();
            var obj = GameObject.Instantiate(loadPrefab.Result, point.position, point.rotation);
            Complete(obj);
        }
    }

}
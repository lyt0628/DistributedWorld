

using QS.Api.WorldItem.Domain;
using QS.Common;
using QS.GameLib.Util;
using QS.WorldItem.Domain;

namespace QS.WorldItem
{
    class WeaponLoadOp : AsyncOpBase<IWeapon>
    {
        readonly IWorldItems worldItems;
        readonly string name;

        public WeaponLoadOp(IWorldItems worldItems, string name)
        {
            this.name = name;
            this.worldItems = worldItems;
        }

        protected override async void Execute()
        {
            var loadFw = worldItems.LoadWeaponFlyweight(name);
            await loadFw.Task;
            var wf = new DefaultWeapon(MathUtil.UUID(), loadFw.Result);
            Complete(wf);
        }
    }
}
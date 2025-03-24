using QS.Common;
using QS.GameLib.Util;

namespace QS.WorldItem
{
    class PropLoadOp : AsyncOpBase<IProp>
    {
        readonly IWorldItems worldItems;
        readonly string name;

        public PropLoadOp(IWorldItems worldItems, string name)
        {
            this.name = name;
            this.worldItems = worldItems;
        }

        protected override async void Execute()
        {
            var loadFw = worldItems.LoadPropFlyweight(name);
            await loadFw.Task;
            var p = new DefaultProp(MathUtil.UUID(), loadFw.Result);
            Complete(p);
        }
    }
}
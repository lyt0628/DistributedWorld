using QS.Api.Common;
using QS.Common;

namespace QS.Quest
{
    public class QuestGlobal : ModuleGlobal<QuestGlobal>
    {
        public QuestGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }

        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }
    }
}
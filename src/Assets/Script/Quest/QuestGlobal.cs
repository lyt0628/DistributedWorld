


using GameLib.DI;
using QS.Api.Common;

namespace QS.Quest
{
    public class QuestGlobal : ModuleGlobal<QuestGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        protected override IDIContext DIContext => DI;

        public QuestGlobal()
        {

        }

        public override void ProvideBinding(IDIContext context)
        {
          
        }
    }
}
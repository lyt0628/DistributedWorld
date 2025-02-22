


using GameLib.DI;
using QS.Api.Common;

using QS.Common;
using QS.Common.ComputingService;


namespace QS.Motor
{
    /// <summary>
    /// MotorFlow 模块负责提供控制的服务, 主要是一些完成控制服务的领域服务类
    /// /// </summary>
    public class MotorGlobal : ModuleGlobal<MotorGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public MotorGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);

            DI
                .BindExternalInstance(new DataSource<CharaControl.Input, CharaControl.State>())
                .Bind<CharaControl>();
        }

        public override void ProvideBinding(IDIContext context)
        {

            // 注意声明和获取的接口要一致
            context
                 .BindExternalInstance(DI.GetInstance<DataSource<CharaControl.Input, CharaControl.State>>())
                 .BindExternalInstance(DI.GetInstance<CharaControl>());
        }

    }
}
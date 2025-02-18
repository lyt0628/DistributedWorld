


using GameLib.DI;
using QS.Api.Common;

using QS.Common;
using QS.Common.ComputingService;


namespace QS.Motor
{
    /// <summary>
    /// Motor ģ�鸺���ṩ���Ƶķ���, ��Ҫ��һЩ��ɿ��Ʒ�������������
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
                .Bind<CharaControl>()
                .BindExternalInstance(new DataSource<FreeFallControl.Input,FreeFallControl.State>())
                .Bind<FreeFallControl>()
                .BindExternalInstance(new DataSource<MoveControl.Input, MoveControl.State>())
                .Bind<MoveControl>();
        }

        public override void ProvideBinding(IDIContext context)
        {

            // ע�������ͻ�ȡ�Ľӿ�Ҫһ��
            context
                 .BindExternalInstance(DI.GetInstance<DataSource<CharaControl.Input, CharaControl.State>>())
                 .BindExternalInstance(DI.GetInstance<CharaControl>())
                 .BindExternalInstance(DI.GetInstance<DataSource<FreeFallControl.Input, FreeFallControl.State>>())
                 .BindExternalInstance(DI.GetInstance<FreeFallControl>())
                 .BindExternalInstance(DI.GetInstance<DataSource<MoveControl.Input, MoveControl.State>>())
                 .BindExternalInstance(DI.GetInstance<MoveControl>());
        }

    }
}
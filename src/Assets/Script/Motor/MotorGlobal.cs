


using GameLib.DI;
using QS.Api.Common;

using QS.Common;
using QS.Common.ComputingService;


namespace QS.Motor
{
    /// <summary>
    /// MotorFlow ģ�鸺���ṩ���Ƶķ���, ��Ҫ��һЩ��ɿ��Ʒ�������������
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

            // ע�������ͻ�ȡ�Ľӿ�Ҫһ��
            context
                 .BindExternalInstance(DI.GetInstance<DataSource<CharaControl.Input, CharaControl.State>>())
                 .BindExternalInstance(DI.GetInstance<CharaControl>());
        }

    }
}
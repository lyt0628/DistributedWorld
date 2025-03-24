using GameLib.DI;
using QS.Api.Common;
using QS.Common.Util.Detector;
using QS.GameLib.Pattern.Message;
using QS.Impl;
using QS.Impl.Setting;
using QS.PlayerControl;

namespace QS.Common
{
    /// <summary>
    /// ���������һЩ�����ˣ�
    /// 
    /// Item��Weapon�� Chara��Terrian�����B����Ч��
    /// �Ӯ��� UI
    /// </summary>
    public class CommonGlobal : ModuleGlobal<CommonGlobal>
    {
        public CommonGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }
        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }


        protected override void DoSetupBinding(IDIContext glboalContext, IDIContext muduleContext)
        {

            var playerControls = new PlayerControls();
            playerControls.Enable();
            playerControls.DialoguePanel.Disable();
            glboalContext
                // Provide Bindings from GameLib ��������ȡȫ����, ��ͨ��������GetInstance����������
                //.Bind<Messager>(DINames.GameLib_Message_Messager, ScopeFlag.Prototype)
                // Util
                .Bind<DetectorFactory>()
                .Bind<QS.Common.Util.Timer>()
                .Bind<FilterHandler>()
                .Bind<HandlerGroup>()
                .Bind<InstructionConverter>()
                //.Bind<AddressCache>()
                // Global Message Bus
                .BindExternalInstance(DINames.MsgBus, new Messager())
                .BindExternalInstance(playerControls)
                .BindExternalInstance(LifecycleProvider.Instance)
                .Bind<GlobalPhysicSetting>();
        }

    }
}

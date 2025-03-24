



using GameLib.DI;
using QS.Api.Common;
using QS.Chara.Service;
using QS.Common;

namespace QS.Chara
{
    //�ں����߼�������� Unity ����ϵġ�ֱ��ʹ��Unity �����ϵͳ����Ҫʹ���Լ���
    // ����ģʽ����Ҫʹ�ã��������������ֻ��Ϊ�˷������ݺ�ϵͳ�ӿ�
    // Ӧ��ʹ�� ϵͳ.����(����) �����ַ�ʽ�������߼�����
    // �������ݼ������⣬�������ֶ��Ǻ� Unity ��ϵ�
    public class CharaGlobal : ModuleGlobal<CharaGlobal>
    {
        public CharaGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }

        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {
            globalContext
                // Deprecated
                .Bind<CharaInstrFacotry>()
                .Bind<JumpInstr>()
                .Bind<DodgeInstr>()
                .Bind<HitInstr>()
                .Bind<AimLockInstr>()
                .Bind<MoveInstr>(ScopeFlag.Prototype);
        }
    }
}
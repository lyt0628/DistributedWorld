

using Cysharp.Threading.Tasks;
using GameLib.DI;
using QS.Api.Common;
using QS.Combat;
using QS.Common;
using QS.Executor;
using QS.GameLib.Pattern.Message;


namespace QS.UI
{
    /// <summary>
    /// UI ģ�K��һ���ό�ģ�K���ṩ Handler ���� API 
    /// ���w���f�ǣ��ӽM����ҪĳЩ���ܣ������@Щ�������όӌ��F
    /// ���N�ӽM������ṩĳЩ�ӿڣ��@���ӿڵČ��F���ό�
    /// �όӽM��ͨ�^��هע�룬���ӷ����@Щ���F
    /// Defaul
    /// �]�k�����ܽ��|���нM����ֻ��Trunk�����ԽK��UI����߅����
    /// �@߅���FһЩUI�M�����m
    /// </summary>
    public class UIGlobal : ModuleGlobal<UIGlobal>
    {
        internal  IDIContext DI = IDIContext.New();
        public UIGlobal() 
        {
            /// ֻ�е��������Ĳ��ܱ�֤����
            DI.Bind<DialoguePannel>(nameof(DialoguePannel));
            DI.Bind<DefaultUIStack>();
            CommonGlobal.Instance.ProvideBinding(DI);
            CombatGlobal.Instance.ProvideBinding(DI);
            ExecutorGlobal.Instance.ProvideBinding(DI);

            DI.BindExternalInstance(DINames.UI_GLOBAL_MESSAGER, new Messager());
        }
        
        protected override IDIContext DIContext => DI;


        public override void ProvideBinding(IDIContext context)
        {
            // FIX: ģ��䴫�ݰ󶨱����Ȼ�ȡʵ��
            context.BindExternalInstance(DI.GetInstance<DialoguePannel>());
            context.BindExternalInstance(DI.GetInstance<IUIStack>());
        }
    }
}
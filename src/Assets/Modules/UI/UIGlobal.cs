using GameLib.DI;
using QS.Api.Common;
using QS.Common;
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
        public UIGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }
        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {

            globalContext
                .Bind<DefaultUIStack>()

                .BindExternalInstance(DINames.UI_GLOBAL_MESSAGER, new Messager());
        }
    }
}
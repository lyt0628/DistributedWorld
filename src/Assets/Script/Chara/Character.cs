using QS.Api.Chara;
using QS.Executor;
using QS.GameLib.Pattern.Message;



namespace QS.Chara.Domain
{
    /// <summary>
    /// �[����}�s���ҿ����X���Լ������á����ǣ���ȥ�����lҲ��֪������Ч�����N��
    /// </summary>
    public class Character : ExecutorBehaviour, ICharacter    
    {

        public IMessager Messager { get; } = new Messager();

        public void AnimAware(string param)
        {
            Messager.Boardcast(param, Msg0.Instance);
        }
        public virtual bool Frozen { get; set; }
    }
}

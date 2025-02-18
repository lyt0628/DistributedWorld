using QS.Api.Chara;
using QS.Executor;
using QS.GameLib.Pattern.Message;



namespace QS.Chara.Domain
{
    /// <summary>
    /// 遊戲很複雜，我總不覺得自己能做好。但是，不去做，誰也不知道最後效果怎麼樣
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

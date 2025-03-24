namespace QS.GameLib.Pattern.Message
{
    public interface IMessage
    {
        public static readonly IMessage Unit = new UnitMsg();
    }
}

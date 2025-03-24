namespace QS.GameLib.Pattern.Message
{
    public class UnitMsg : IMessage
    {
        public static readonly UnitMsg Instance = new();
        internal UnitMsg() { }
    }
}
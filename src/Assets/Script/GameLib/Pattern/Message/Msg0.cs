namespace QS.GameLib.Pattern.Message
{
    public class Msg0 : IMessage
    {
        public static readonly Msg0 Instance = new();
        private Msg0() { }
    }
}
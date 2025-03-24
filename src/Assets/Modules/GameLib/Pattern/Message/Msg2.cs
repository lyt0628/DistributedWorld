namespace QS.GameLib.Pattern.Message
{
    public class Msg2<T, U> : IMessage
    {
        public Msg2(T first, U second)
        {
            First = first;
            Second = second;
        }
        public T First { get; set; }
        public U Second { get; set; }
        public string Type
        {
            get
            {
                return null;
            }
        }
    }
}
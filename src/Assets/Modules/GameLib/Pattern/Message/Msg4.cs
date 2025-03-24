namespace QS.GameLib.Pattern.Message
{
    public class Msg4<T, U, V, X> : IMessage
    {
        public Msg4(T first, U second, V three, X four)
        {
            First = first;
            Second = second;
            Three = three;
            Four = four;
        }
        public T First { get; set; }
        public U Second { get; set; }
        public V Three { get; set; }
        public X Four { get; set; }
        public string Type
        {
            get
            {
                return null;
            }
        }
    }
}
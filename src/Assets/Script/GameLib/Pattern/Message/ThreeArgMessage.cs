

namespace GameLib.Pattern.Message
{
    class ThreeArgMessage<T, U, V> : IMessage
    {

        public ThreeArgMessage(T first, U second, V three)
        {
            First = first;
            Second = second;
            Three = three;
        }

        public T First { get; set; }
        public U Second { get; set; }
        public V Three { get; set; }
        public string Type
        {
            get
            {
                return null;
            }
        }
    }
}
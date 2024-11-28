namespace QS.GameLib.Pattern.Message
{
    public class Msg1<T> : IMessage
    {

        public Msg1(T arg)
        {
            Value = arg;
        }


        public T Value { get; set; }
        public string Type
        {
            get { return null; }
        }
    }
}
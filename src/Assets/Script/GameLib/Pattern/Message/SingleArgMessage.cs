
namespace GameLib.Pattern.Message
{
    public class SingleArgMessage<T> : IMessage
    {

        public SingleArgMessage(T arg)
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
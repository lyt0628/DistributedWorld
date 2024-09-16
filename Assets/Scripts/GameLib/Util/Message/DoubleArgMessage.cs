
namespace GameLib
{
    public class DoubleArgMessage<T, U>  : IMessage 
    {
        public DoubleArgMessage (T first, U second)
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
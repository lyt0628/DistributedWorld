namespace QS.Common
{
    public class UnitAsyncOp<T> : AsyncOpBase<T>
    {
        readonly T value;
        public UnitAsyncOp(T value)
        {
            this.value = value;
        }

        protected override void Execute()
        {
            Complete(value);
        }
    }
}
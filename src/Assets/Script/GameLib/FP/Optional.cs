
using System;

namespace GameLib.FP
{
    public class Optional<T>
    {
        private readonly T Value;
        private Optional(T Value) {  this.Value = Value; }

        public static Optional<T> Of(T value)
        {
            if(value == null)
            {
                throw new Exception("Value should not be null");
            }
            return new Optional<T>(value);
        }

        public bool IsPresent() { return Value != null; }
        public T Get() {  return Value; }
    }
}
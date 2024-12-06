


using System;

namespace QS.GameLib.Rx.Relay
{
    class MapOperator<T, U> : AbstractOperator<T, U>
    {
        readonly Func<T, U> mapper;
        public MapOperator(Func<T,U> mapper) 
        {
            this.mapper = mapper;
        }
        protected override U Operate(T t)
        {
            return mapper(t);
        }
    }
}
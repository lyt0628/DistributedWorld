


using QS.GameLib.Rx.Relay;
using System.Collections.Generic;

namespace QS.Common.ComputingService
{
    public abstract class AbstractComputingService<T,R,S> where S : new()
    {
        protected readonly DataSource<T, S> dataSource;


        public AbstractComputingService(DataSource<T, S> dataSource)
        {
            this.dataSource = dataSource;
        }

        public Relay<R> Get(string uuid)
        {
            
            var relay = dataSource.Get(uuid, out var state);
            return relay.Map(i => Compute(i, state));
        }

        protected abstract R Compute(T input, S state);
        public void Reset(string uuid)
        {
            dataSource.Get(uuid, out var state);
        }
        protected abstract void DoReset(S state);

    }
}
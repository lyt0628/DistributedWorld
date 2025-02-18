


using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using System.Collections.Generic;
using System;

namespace QS.Common.ComputingService
{
    public class DataSource<T,S> : IDataSource<T>
        where T : new() where S : new()
    {

        readonly Dictionary<string, S> states = new();
        readonly Dictionary<string, Relay<T>> inputs = new();

        //public T Create()=>new();

        public string Add(Relay<T> data)
        {

            var uuid = MathUtil.UUID();
            states[uuid] = new();
            inputs[uuid] = data;

            return uuid;
        }

        public string Add(Relay<T> data, S state)
        {

            var uuid = MathUtil.UUID();
            states[uuid] = state;
            inputs[uuid] = data;

            return uuid;
        }
        public void Remove(string uuid)
        {
            states.Remove(uuid);
            inputs.Remove(uuid);
        }

        public Relay<T> Get(string uuid, out S state)
        {
            try
            {
                state = states[uuid]; 
                return inputs[uuid];
            }
            catch (Exception)
            {
                throw new ProxyNotFoundException(uuid, typeof(T).FullName);
            }
        }
    }
}
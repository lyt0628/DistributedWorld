



using QS.API;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace QS
{
    class SimpleItemPool
    {


        
        private Dictionary<string, IItemModel> itemModelMap = new();

        public IItem Get(string name)
        {
            if (itemModelMap.ContainsKey(name))
            {
                var ret = new SimpleItem ()
                {
                    Model = itemModelMap[name]
                };
                return ret;
               
            }
            return null;
        }

        public void Add(IItemModel model)
        {
            if (itemModelMap.ContainsKey(model.Name)) return;
            itemModelMap.Add(model.Name, model);
        }
    }
}

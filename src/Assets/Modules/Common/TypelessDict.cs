

using System.Collections.Generic;

namespace QS.Common
{
    public class TypelessDict
    {
        readonly Dictionary<string, object> m_dict = new();
       public object Get(string key)
        {
            return m_dict[key];
        }

        public void Set(string key, object value)
        {
            m_dict[key] = value;
        }
    }
}
using System;
using System.Collections.Generic;


namespace QS.GameLib.Pattern.Message
{

    public class Messager : IMessager
    {

        private readonly Dictionary<string, Action<IMessage>> m_broker = new();


        public void AddListener(string type, Action<IMessage> handler)
        {

            if (!m_broker.ContainsKey(type))
            {
                m_broker.Add(type, handler);
            }
            else
            {
                m_broker[type] += handler;
            }
        }

        public void RemoveListener(string type, Action<IMessage> handler)
        {
            if (!m_broker.ContainsKey(type)) return;
            m_broker[type] -= handler;
        }
        public void Boardcast(string type, IMessage msg)
        {
            //Debug.Log($"{type} >>> {msg}");
            if (type == null || !m_broker.ContainsKey(type)) return;
            m_broker[type]?.Invoke(msg);
        }


    }
}


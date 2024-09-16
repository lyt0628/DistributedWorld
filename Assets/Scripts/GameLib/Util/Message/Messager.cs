namespace GameLib {
    using System;
    using System.Collections;
    using System.Collections.Generic;   
    using UnityEngine;

    public class Messager : ISington<Messager> 
    {
        #region Sington
        private static Messager _instance;
        private static readonly object _lock = new();
        public static Messager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new();
                    }
                }
                return _instance;
            }
        }
        #endregion
       
        private Dictionary<string, Action<IMessage>> m_broker = new();

       
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
        public void Boardcast(string type ,IMessage msg)
        {
            
            if (type == null || !m_broker.ContainsKey(type)) return;
            m_broker[type]?.Invoke(msg);
        }


    }
}


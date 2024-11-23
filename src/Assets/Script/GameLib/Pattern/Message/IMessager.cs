


using System;

namespace GameLib.Pattern.Message
{
    public interface IMessager
    {

        public void AddListener(string type, Action<IMessage> handler);
        public void RemoveListener(string type, Action<IMessage> handler);
        public void Boardcast(string type, IMessage msg);
    }
}
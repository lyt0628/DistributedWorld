using System;

namespace QS.GameLib.Pattern.Message
{
    /// <summary>
    /// TODO�����Ӻ��εķ��ͽӿ�
    /// </summary>
    public interface IMessager
    {

        public void AddListener(string type, Action<IMessage> handler);
        public void RemoveListener(string type, Action<IMessage> handler);
        /// <summary>
        /// Depreced
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void Boardcast(string type, IMessage msg);

    }
}
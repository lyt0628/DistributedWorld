using System.Collections.Generic;

namespace QS.GameLib.Pattern
{
    public interface IListenable<T>
    {
        void AddListener(T listener);
        void RemoveListener(T listener);
    }


    public class Listenable<T> : IListenable<T>
    {
        protected List<T> listenrs = new();

        public void AddListener(T listener)
        {
            listenrs.Add(listener);
        }

        public void RemoveListener(T listener)
        {
            listenrs.Remove(listener);
        }
    }

    public class ListenableWrapper<T> : Listenable<T>
    {
        public List<T> Listeners
        {
            get { return listenrs; }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameLib
{

    public interface IPublisher<T> 
    {
        public void Subscribe(ISubcscriber<T> subcscriber);
        public void Submit(T item);
    }
}

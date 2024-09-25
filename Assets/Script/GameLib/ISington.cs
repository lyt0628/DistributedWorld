using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLib
{
    public interface ISington<T>
    {
        public static T Instance{ get;}
    }

    public class Sington<T> : ISington<T> where T: new(){

        private static T _instance;
        private static readonly object _lock = new();
        public static T Instance
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

    }
}

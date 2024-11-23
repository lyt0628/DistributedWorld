

using UnityEngine;


namespace GameLib
{
    public class SingtonBehaviour<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        GameObject obj = new()
                        {
                            hideFlags = HideFlags.HideAndDontSave
                        };
                        _instance = (T)obj.AddComponent(typeof(T));
                    }
                }
                return _instance;
            }
        }

        public virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            // I cannot directly destroy it, 
            // Sometimes, The Static Instance is called before Awake
            {
                Destroy(gameObject);
            }
        }
    }
}
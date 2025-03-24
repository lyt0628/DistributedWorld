using System.Collections.Generic;
using UnityEngine;

public class MyDictionary<K, V> : ISerializationCallbackReceiver
{
    [SerializeField] private List<K> _keys;
    [SerializeField] private List<V> _values;
    private Dictionary<K, V> _dictionary = new Dictionary<K, V>();

    public V this[K key]
    {
        get
        {
            if (!_dictionary.ContainsKey(key))
            {
                return default;
            }
            return _dictionary[key];
        }
        set
        {
            _dictionary[key] = value;
        }
    }

    public void OnAfterDeserialize()
    {
        var len = _keys.Count;
        Debug.Log("XXXXXXXXXXXXXX" + len);
        _dictionary = new Dictionary<K, V>();
        for (int i = 0; i < len; i++)
        {
            _dictionary[_keys[i]] = _values[i];
        }
        _keys = null;
        _values = null;
    }
    public void OnBeforeSerialize()
    {
        _keys = new List<K>();
        _values = new List<V>();
        foreach (var item in _dictionary)
        {
            _keys.Add(item.Key);
            _values.Add(item.Value);
        }
    }
}


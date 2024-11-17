using System;
using System.Collections.Generic;


namespace GameLib.DI
{

    public class Key
    {
        private Key(string name, Type type)
        {
            _name = name; 
            _type = type;
        }

        private readonly Type _type;
        private readonly object _name;

        public Type Type { get { return _type; }  }
        public object Name { get { return _name; } }

        public override bool Equals(object obj)
        {
            if (obj == null && GetType()!=obj.GetType()) return false;
            Key other = obj as Key;
            return Name == other.Name && Type == other.Type; 
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + _name.GetHashCode();
            hash = hash * 23 + _type.GetHashCode();
            return hash;
        }

        private static readonly IDictionary<Type, Key> keyCache = new Dictionary<Type, Key>();
        public static Key Get(Type type)
        {
            if (keyCache.ContainsKey(type)) {  return keyCache[type]; }
            var k = new Key(ReflectionUtil.GenerateNameOf(type), type);
            keyCache.Add(type, k);
            return k;
        }
    }
}

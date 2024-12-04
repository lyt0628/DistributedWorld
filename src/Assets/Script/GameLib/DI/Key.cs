using System;
using System.Collections.Generic;
using System.Linq;


namespace GameLib.DI
{

    public class Key
    {
        private Key(string name, Type type)
        {
            _name = name;
            _type = type;
            _typeKey = new TypeKey(type);
        }

        private readonly Type _type;
        private readonly string _name;
        private readonly TypeKey _typeKey;
        public TypeKey KeyType { get { return _typeKey; } }
        public Type Type { get { return _type; } }
        public string Name { get { return _name; } }

        public override bool Equals(object obj)
        {
            if (obj == null && GetType() != obj.GetType()) return false;
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

        private static readonly ISet<Key> keyCache = new HashSet<Key>();
        public static Key Get(Type type)
        {
            var k = new Key(DIUtil.GenerateNameOf(type), type);
            return Resolve(k);
        }

        private static Key Resolve(Key k)
        {
            if (keyCache.Contains(k))
            {
                return keyCache.Where(key => key.Equals(k)).First();
            }
            keyCache.Add(k);
            return k;
        }

        public static Key Get(string name, Type type)
        {
            var k = new Key(name, type);
            return Resolve(k);
        }
    }
}

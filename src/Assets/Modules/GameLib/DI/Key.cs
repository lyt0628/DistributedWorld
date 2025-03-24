using System;
using System.Collections.Generic;
using System.Linq;


namespace GameLib.DI
{

    public class Key
    {
        internal Key(string name, Type type)
        {
            Name = name;
            Type = type;
        }
        public Type Type { get; }
        public string Name { get; }

        public override bool Equals(object obj)
        {
            if (obj == null && GetType() != obj.GetType()) return false;
            Key other = obj as Key;
            return Name == other.Name && Type == other.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type);
        }

        static readonly ISet<Key> keyCache = new HashSet<Key>();
        public static Key Get(Type type)
        {
            var k = new Key(DIUtil.GenerateNameOf(type), type);
            return Resolve(k);
        }

        static Key Resolve(Key k)
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

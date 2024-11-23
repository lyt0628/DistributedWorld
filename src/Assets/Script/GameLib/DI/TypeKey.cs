using System;


namespace GameLib.DI
{
    public class TypeKey
    {
        private Type type;
        public Type Type { get { return type; } }

        public TypeKey(Type type)
        {
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null && GetType() != obj.GetType()) return false;
            TypeKey other = obj as TypeKey;
            return Type == other.Type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}

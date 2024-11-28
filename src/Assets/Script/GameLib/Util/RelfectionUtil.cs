

namespace QS.GameLib.Util
{
    public static class ReflectionUtil
    {
        public static bool IsParentOf<TChild,TParent>()
        {
            return typeof(TParent).IsAssignableFrom(typeof(TChild));
        }

        public static bool IsChildOf<TParent>(object child)
        {
            return typeof(TParent).IsAssignableFrom(child.GetType());
        }

    }
}


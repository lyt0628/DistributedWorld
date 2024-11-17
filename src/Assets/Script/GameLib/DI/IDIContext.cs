
using System;

namespace GameLib.DI
{
    interface IDIContext
    {
        static IDIContext Get()
        {
            return new DefaultDIContext();
        }
        IDIContext Bind(Type type);

        T GetInstance<T>(Type type);
    }
}
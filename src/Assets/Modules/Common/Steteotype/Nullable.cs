using System;

namespace QS.Stereotype
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    public class Nullable : Attribute
    {
    }

}
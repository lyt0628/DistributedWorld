
using System;

namespace GameLib.DI
{
    public class Scope : Attribute
    {
        public ScopeFlag Value { get; set; }
    }

    public enum ScopeFlag
    {
        Sington,
        Prototype,
        Default
    }
}
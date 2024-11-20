
using System;
using System.Reflection;

namespace GameLib.DI
{
    public class Scope : Attribute
    {
        public ScopeFlag Value { get; set; }
    }

    public enum ScopeFlag
    {
        Sington,
        Prototype
    }
}

using System;

namespace GameLib.DI
{
    public class Priority : Attribute
    {
        public int Value { get; set; }
    }
}
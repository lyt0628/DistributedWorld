
using System;

namespace GameLib.DI
{
    public class DIException : Exception
    {
        public DIException(string message) : base(message) { }
    }
}
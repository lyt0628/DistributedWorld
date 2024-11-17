
using System;

namespace GameLib.DI
{
    class DIException : Exception
    {
        public DIException(string message) : base(message) { }
    }
}
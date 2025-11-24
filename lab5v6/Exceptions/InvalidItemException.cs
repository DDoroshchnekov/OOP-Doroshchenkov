using System;

namespace Lab5.Exceptions
{
    public class InvalidItemException : Exception
    {
        public InvalidItemException(string message) : base(message) { }
    }
}

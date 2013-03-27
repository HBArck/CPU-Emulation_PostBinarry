using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class ErrorTypeException: Exception
    {
         public ErrorTypeException()
        {
        }
        public ErrorTypeException(string message)
            : base(message)
        {
        }
        public ErrorTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class NoSuchErrorTypeException : Exception
    {
        public NoSuchErrorTypeException()
        {
        }
        public NoSuchErrorTypeException(string message)
            : base(message)
        {
        }
        public NoSuchErrorTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

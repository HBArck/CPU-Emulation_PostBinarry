using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    /* VALIDATOR EXCEPTIONS BEGIN*/
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

    class EmptyErrorTypeException : Exception
    {
        public EmptyErrorTypeException()
        {
        }
        public EmptyErrorTypeException(string message)
            : base(message)
        {
        }
        public EmptyErrorTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ErrorTypeNumberMissingException : Exception
    {
        public ErrorTypeNumberMissingException()
        {
        }
        public ErrorTypeNumberMissingException(string message)
            : base(message)
        {
        }
        public ErrorTypeNumberMissingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    /* VALIDATOR EXCEPTIONS END*/


    /* PALU EXCEPTIONS BEGIN*/

    /* PALU EXCEPTIONS END*/
}

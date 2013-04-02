using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    /* Program Core Exceptions BEGIN */
    public class FCCoreGeneralException : System.Exception
    {
        public FCCoreGeneralException()
        {
        }
        public FCCoreGeneralException(string message)
            : base(message)
        {
        }
        public FCCoreGeneralException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    public class FCCoreArithmeticException : System.Exception
    {
        public FCCoreArithmeticException()
        {
        }
        public FCCoreArithmeticException(string message)
            : base(message)
        {
        }
        public FCCoreArithmeticException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    public class FCCoreFunctionException : System.Exception
    {
        public FCCoreFunctionException()
        {
        }
        public FCCoreFunctionException(string message)
            : base(message)
        {
        }
        public FCCoreFunctionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    /* Program Core Exceptions END */


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
    class IncorrectSignException : FCCoreArithmeticException
    {
        public IncorrectSignException()
        {
        }
        public IncorrectSignException(string message)
            : base(message)
        {
        }
        public IncorrectSignException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    /* PALU EXCEPTIONS END*/


}

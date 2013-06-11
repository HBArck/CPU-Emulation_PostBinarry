using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes.PostBinary
{
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


    public class PBGeneralException : System.Exception
    {
        public PBGeneralException()
        {
        }
        public PBGeneralException(string message)
            : base(message)
        {
        }
        public PBGeneralException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class PBArithmeticException : System.Exception
    {
        public PBArithmeticException()
        {
        }
        public PBArithmeticException(string message)
            : base(message)
        {
        }
        public PBArithmeticException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class PBFunctionException : System.Exception
    {
        public PBFunctionException()
        {
        }
        public PBFunctionException(string message)
            : base(message)
        {
        }
        public PBFunctionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

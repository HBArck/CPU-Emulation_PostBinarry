using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class Error
    {
        #region LastMessage
        private String lastMessage;
        public String Message
        {
            get { return lastMessage; }
            set {
                if (!String.IsNullOrEmpty(value))
                {
                    lastMessage = value;
                }
            }
        }
        #endregion 

        #region LastMessage
        private String type;
        public String Type
        {
            get { return type; }
            set {
                try
                {
                    if (Properties.Resources.ResourceManager.GetObject(value) != null)
                    {
                        type = value;
                    }
                    else
                        throw new NoSuchErrorTypeException("There is no such message in resource file");
                }
               // catch (NoSuchErrorTypeException ex1)
               // {
               //throw new ArgumentException
               // }
                catch (Exception ex2)
                {
                    throw new ErrorTypeException("Type ");
                }
                
            }
        }
        #endregion

        private int[] argsPos;  // Position to start insert argument

        // Constructor
        public Error()
        { 
        }

        /// <summary>
        /// Input error string with arguments to display. ex "Missing {0} in expression."
        /// </summary>
        /// <param name="msg">Message to display.</param>
        /// <param name="args">Arguments for string.</param>
        public void SendError(String msg,String [] args)
        {
            Message = msg;
            
            // Rise Event
            // 
        }

        /// <summary>
        /// Finds arguments positions in string.
        /// </summary>
        /// <param name="message">Input error string with arguments. ex "Missing {0}'}' in expression."</param>
        public void foundArgs(String message)
        { 
        }

    }

    class ValidatorErrorType : Error
    {
        int errorType;
        String[] Messages;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ValidatorErrorType()
        { 
        }

        /// <summary>
        /// Sends error to Program Core, that should notify user about.
        /// </summary>
        /// <param name="msg">Error for User Interface.</param>
        public void SendError(String msg)
        { 
        }
    }
}

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
                    if (value != "")
                    {
                        int NumPos = -1; // There is no number for this message
                        String currErrorTypeName = value;
                        for (int i = 1; i < 10; i++)
                        {
                            if (value.Contains(i.ToString()))
                            {
                                NumPos = i;
                            }
                        }

                        if (NumPos == -1)
                        {
                            throw new ErrorTypeNumberMissingException("Missing error type number in name.");
                        }

                        currErrorTypeName.Insert( NumPos - 1, getLocalization());
                        if (Properties.Resources.ResourceManager.GetObject(currErrorTypeName) != null) 
                        {
                            type = currErrorTypeName;
                        }
                        else
                            throw new NoSuchErrorTypeException("There is no such message in resource file.");
                    }
                    else
                    {
                        throw new EmptyErrorTypeException("Message name is empty.");
                    }

                }
                catch (Exception ex2)
                {
                    throw new ErrorTypeException("Error Type Exception ["+ex2.Message+"]");
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

        public String getLocalization()
        {
            return "En";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PostBinary.Classes
{
    /// <summary>
    /// This class stores all information of programm state for
    /// current moment
    /// </summary>
    class ProgramCore
    {
        // Errors
        // Exceptions
        // Steps 
        private Thread mainThread;
        private int stepNumber;
        public int StepNumber
        {
            get { return stepNumber; }
            set 
            {
                if (value > 0)
                    stepNumber = value;
            }
        }
        ProgramCore()
        {
            mainThread = new Thread(new ParameterizedThreadStart(Steps));
        }


        /// <summary>
        /// Making each step formed by Stack
        /// </summary>
        /// <param name="ContextObject">Some data to transmit</param>
        public void Steps(object ContextObject)
        {
            for (int i = 0; i < stepNumber; i++)
            { 

            }
        }
    }

}

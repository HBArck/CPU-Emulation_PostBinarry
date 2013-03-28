using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    /// <summary>
    /// Main purpose is to save every step made by program
    /// </summary>
    class Logger
    {
        private List<String> logs;
        public List<String> Logs
        {
            get { return logs; }
            set {
                if (value.Count > 0)
                    logs.AddRange(value);
            }
        }
        /// <summary>
        /// Returns items list if exists, else empty string ""
        /// </summary>
        /// <param name="index">Element index.</param>
        /// <returns>String that represent one of log item.</returns>
        public String getItem(int index)
        {
            if (logs.Count > index)
            {
                return logs[index];
            }else
                return "";
        }

        public void debug(String msg)
        { 

        }

        public void error(String msg)
        { 

        }

        public void info(String msg)
        { 
        }
    }
}

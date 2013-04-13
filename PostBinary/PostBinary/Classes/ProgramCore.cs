using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PostBinary.Classes
{
    struct variable
    {
        public String VariableName;
        public String VariableValue;
            
        public variable(String varName, String varVal)
        {
            this.VariableName = varName;
            this.VariableValue = varVal;
        }
    }
    class VariableList
    {
        // Variables
        private List<variable> items = new List<variable>();
        public List<variable> Items
        {
            get { return items; }
        }
        public VariableList(){}
        public void Add(String varName)
        {
            variable tempVar = new variable();
            tempVar.VariableName = varName;
            tempVar.VariableValue = "1";
            items.Add(tempVar);
        }
           
        public void Add(String varName, String varVal)
        {
            variable tempVar = new variable();
            tempVar.VariableName = varName;
            tempVar.VariableValue = varVal;
            items.Add(tempVar);
        }
    }
    

    /// <summary>
    /// This class stores all information of programm state for
    /// current moment
    /// </summary>
    class ProgramCore
    {
        
        List<String> Memory;
        // Errors
        // Exceptions
        // Steps 
        private Thread mainThread;
        private int stepNumber;
        public Validator ValidatorTool;
        public int StepNumber
        {
            get { return stepNumber; }
            set 
            {
                if (value > 0)
                    stepNumber = value;
            }
        }
        public  ProgramCore()
        {
            ValidatorTool = new Validator();
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

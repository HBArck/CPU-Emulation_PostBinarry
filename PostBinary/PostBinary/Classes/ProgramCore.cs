using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PostBinary.Classes
{
    /// <summary>
    /// This enum indicates type of number: Integer, Float, Interval
    /// </summary>
    public enum NumberFormat
    { 
        Integer,
        Float,
        Interval
    };

    /// <summary>
    /// This enum indicates rounding type for number: to Zero, to Number, to Positive Infinity, to Negative Infinity
    ///  0 - to zero 1 - to number 2 - to Pos Inf 3 - to Neg Inf 4 - to Pos Neg Inf
    /// </summary>
    public enum RoundingType
    {
        Zero=0,
        Integer=1,
        PositiveInfinity=2,
        NegativeInfinity=3
    };

    /// <summary>
    /// Indicates left or right part of Number.Number must be in Float or Interval Format
    /// </summary>
    public enum PartOfNumber
    {
        Left = 1,
        Right = 2,
    };
        
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
     
        private NumberFormat leftOperandNumberFormat;
        public NumberFormat LeftOperandNumberFormat
        {
            get { return leftOperandNumberFormat; }
            set 
            {
                // if value in same type and less or equal to bounds of enum
                if ((value.GetType() == typeof(NumberFormat)) && (int)value <= 2)
                {
                    leftOperandNumberFormat = value;
                }
            }
        }
        private NumberFormat rightOperandNumberFormat;
        public NumberFormat RightOperandNumberFormat
        {
            get { return rightOperandNumberFormat; }
            set
            {
                // if value in same type and less or equal to bounds of enum
                if ((value.GetType() == typeof(NumberFormat)) && (int)value <= 2)
                {
                    rightOperandNumberFormat = value;
                }
            }
        }
        private byte roundnig;
        public byte Rounding // 0 - to zero 1 - to number 2 - to Pos Inf 3 - to Neg Inf 4 - to Pos Neg Inf
        {
            get { return roundnig; }
            set
            {
                if (value > 4 || value < 0)
                    roundnig = 0;
                else
                    roundnig = value;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PostBinary.Classes.PostBinary;
namespace PostBinary.Classes
{
    public class CommandBase
    {
        public enum commVals { Load, Add, Sub, Mul, Div, Exp, Mem };
        public readonly String[] commNames = { "LOAD", "ADD", "SUB", "MUL", "DIV", "EXP", "M{0}" };
    }
    public class Command : CommandBase
    {
        /*public System.Collections.Hashtable commands = new System.Collections.Hashtable();*/
        private int Code;
        private PBNumber leftOperand;
        private PBNumber rightOperand;
        private PBNumber result;
        public PBNumber Result
        {
            get
            {
                return result;
            }
            set{}
        }
        public int MemoryCellUsed;
        public int MemoryCellNeeded;
        public Command() {}

        public Command(String inOperation, String inLeftOperand, String inRightOperand)
        {
            switch (inOperation)
            {
                case "+": this.Code = 1; break;
                case "-": this.Code = 2; break;
                case "*": this.Code = 3; break;
                case "/": this.Code = 4; break;
                // Exponent need to be checked in Parser.cs
                case "^": this.Code = 5; break;
                default: break;
            }
            this.leftOperand = new PBNumber(inLeftOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
            this.rightOperand = new PBNumber(inRightOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
        }

        public Command(String inOperation, String inLeftOperand, int inRightOperandFromMemory)
        {
            switch (inOperation)
            {
                case "+": this.Code = 1; break;
                case "-": this.Code = 2; break;
                case "*": this.Code = 3; break;
                case "/": this.Code = 4; break;
                // Exponent need to be checked in Parser.cs
                case "^": this.Code = 5; break;
                default: break;
            }
            this.leftOperand = new PBNumber(inLeftOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);

            this.MemoryCellNeeded = inRightOperandFromMemory;
            //this.rightOperand = new PBNumber(inRightOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
        }
        public Command(String inOperation, int inLeftOperandFromMemory, String inRightOperand)
        {
            switch (inOperation)
            {
                case "+": this.Code = 1; break;
                case "-": this.Code = 2; break;
                case "*": this.Code = 3; break;
                case "/": this.Code = 4; break;
                // Exponent need to be checked in Parser.cs
                case "^": this.Code = 5; break;
                default: break;
            }
            
            this.rightOperand = new PBNumber(inRightOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
            this.MemoryCellNeeded = inLeftOperandFromMemory;
        }

        public Command(String inOperation, String inLeftOperand, String inRightOperand, int inCellUsed) 
            : this(inOperation, inLeftOperand, inRightOperand)
        {
            this.MemoryCellUsed = inCellUsed;
        }
        public Command(String inOperation, String inLeftOperand, int inRightOperand, int inCellUsed) 
            : this(inOperation,inLeftOperand, inRightOperand)
        {
            this.MemoryCellUsed = inCellUsed;
        }
        public Command(String inOperation, int inLeftOperand, String inRightOperand, int inCellUsed)
            : this(inOperation, inLeftOperand, inRightOperand)
        {
            this.MemoryCellUsed = inCellUsed;
        }
    }

    /// <summary>
    /// Class used for temporary saving operation result
    /// </summary>
    public class MemoryCell
    {
        // Operation result
        public PBNumber MemoryCellValue;
        // Flag,which determine that this memory cell still needed or not,and can be reused.
        public bool CanBeErased;
        // Flag,which determine that this memory cell has already new value.
        public bool valueSet;

        public MemoryCell()
        {
            MemoryCellValue = null;
            CanBeErased = false;
            valueSet = false;
        }
    }
    
    /// <summary>
    /// This class represent polynom analizer
    /// </summary>
    public class Stack 
    {
        private Stack<Command> commandStack;
        public Stack<Command> CommandStack
        {
            get { return commandStack; }
            set 
            {
                commandStack = value;
                /*if (value.GetType() == Type.GetType("Command"))
                {
                    OnCommandStackChanged.Invoke();
                }*/
            }
        }
        
        public List<MemoryCell> StackMemory;
        public delegate void EventHandler();

        public event EventHandler OnCommandStackChanged ;
          
        public Stack()
        {
            CommandStack = new Stack<Command>();
            StackMemory = new List<MemoryCell>();
            
        }
    
        public void PushCommand(Command comm)
        {
            CommandStack.Push(comm);
        }

        /*public void pushCommand(String inOperation, int inLeftOperand, int inRightOperand)
        {
            String leftOperand = inLeftOperand.ToString();
            String rightOperand = inRightOperand.ToString();
            pushCommand(inOperation, leftOperand, rightOperand);
        }*/

        private int GetUnusedMemoryCell()
        {
            int returnValue = -1;
            for (int index = 0; index < StackMemory.Count; index++)
            {
                if (StackMemory[index].CanBeErased == true) 
                {
                    returnValue = index;
                    break;
                }
            }
            if (returnValue == -1)
            {
                StackMemory.Add(new MemoryCell());
                returnValue = StackMemory.Count -1;
            }
            return returnValue;
        }
        /// <summary>
        /// Push new Command for evaluation in stack.
        /// </summary>
        /// <param name="inOperation">Operation to evaluate.</param>
        /// <param name="inLeftOperand">Left operand.</param>
        /// <param name="requiredCellMemory">Memory Cell in StackMemory, that stores needed value.</param>
        public void PushCommand(String inOperation, String inLeftOperand, int requiredCellMemory)
        {
            freeMemory(requiredCellMemory);

            int currEmptyCellMemory = GetUnusedMemoryCell();

            Command currCommand = new Command(inOperation, inLeftOperand, requiredCellMemory, currEmptyCellMemory);
            commandStack.Push(currCommand);
        }
        /// <summary>
        /// Push new Command for evaluation in stack.
        /// </summary>
        /// <param name="inOperation">Operation to evaluate.</param>
        /// <param name="inLeftOperand">Left operand.</param>
        /// <param name="requiredCellMemory">Memory Cell in StackMemory, that stores needed value.</param>
        public void PushCommand(String inOperation,  int requiredCellMemory, String inLeftOperand)
        {
            freeMemory(requiredCellMemory);

            int currEmptyCellMemory = GetUnusedMemoryCell();

            Command currCommand = new Command(inOperation, requiredCellMemory, inLeftOperand, currEmptyCellMemory);
            commandStack.Push(currCommand);
        }

        /// <summary>
        /// Push new Command for evaluation in stack
        /// </summary>
        /// <param name="inOperation">Operation to evaluate.</param>
        /// <param name="inLeftOperand">Left operand.</param>
        /// <param name="inRightOperand">Right operand.</param>
        public void PushCommand(String inOperation, String inLeftOperand, String inRightOperand)
        {
            int currEmptyCellMemory = GetUnusedMemoryCell();

            Command currCommand = new Command(inOperation, inLeftOperand, inRightOperand, currEmptyCellMemory);   
            commandStack.Push(currCommand);
        }

        /// <summary>
        /// Peeks last added Command in Stack.
        /// </summary>
        /// <returns>Command that was last added to stack.</returns>
        public Command PeekCommand()
        {
            Command retValue = CommandStack.Peek();
            return retValue;
        }

        /// <summary>
        /// Pops from stack Command, and removes it from stack.
        /// </summary>
        /// <returns>Command that was last added to stack, and was removed.</returns>
        public Command PopCommand()
        {
            Command retValue = CommandStack.Pop();
            this.freeMemory(retValue.MemoryCellUsed);

            return retValue;
        }

        /// <summary>
        /// Function dispose memory for re usage.
        /// </summary>
        /// <param name="inMemoryCellIndex">Memory Index in StackMemory for erasament.</param>
        private void freeMemory(int inMemoryCellIndex)
        {
            StackMemory[inMemoryCellIndex].CanBeErased = true;
        }

    }
   

}

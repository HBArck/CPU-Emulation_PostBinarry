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
    }
    public class Command : CommandBase
    {
        public static String[] commNames = new String[]{ "LOAD", "ADD", "SUB", "MUL", "DIV", "EXP", "SAVE M{0}" };

        public  int Code;
        public  PBNumber leftOperand;
        public  PBNumber rightOperand;
        private PBNumber result;
        public  PBNumber Result
        {
            get { return result; }
            set{}
        }
        public int MemoryCellUsed;
        public int MemoryCellNeeded;
        public int MemoryCellNeededExtra;

        #region Constructors
            public Command() {}

            public Command(CommandBase.commVals inCommandInstruction, Object inValue) 
            {
                switch (inCommandInstruction)
                {
                    case commVals.Load:
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            if (inValue.GetType() == typeof(int))
                            {
                                this.leftOperand = null;
                                this.MemoryCellNeeded = (int)inValue;
                            }
                            else
                                this.leftOperand = (PBNumber)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectOperandType("Command(" + inCommandInstruction + ", " + inValue + ")=[ " + ex.Message + " ]");
                        }

                        break;
                    case commVals.Mem:
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            this.MemoryCellUsed = (int)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectMemoryCellAddress("Command(" + inCommandInstruction + ", " + (String)inValue + ")=[ " + ex.Message + " ]");
                        }

                        break;
                    default :
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            if (inValue.GetType() == typeof(int))
                            {
                                this.leftOperand = null;
                                this.MemoryCellNeeded = (int)inValue;
                            }
                            else
                                this.leftOperand = (PBNumber)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectOperandType("Command(" + inCommandInstruction + ", " + inValue + ")=[ " + ex.Message + " ]");
                        }
                        break;
                }
            }

            public Command(CommandBase.commVals inCommandInstruction, Object inValue, bool usedExtraMemory)
            {
                switch (inCommandInstruction)
                {
                    case commVals.Load:
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            if (inValue.GetType() == typeof(int))
                            {
                                this.leftOperand = null;
                                this.MemoryCellNeededExtra = (int)inValue;
                            }
                            else
                                this.leftOperand = (PBNumber)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectOperandType("Command(" + inCommandInstruction + ", " + inValue + ")=[ " + ex.Message + " ]");
                        }

                        break;
                    case commVals.Mem:
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            this.MemoryCellNeededExtra = (int)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectMemoryCellAddress("Command(" + inCommandInstruction + ", " + (String)inValue + ")=[ " + ex.Message + " ]");
                        }

                        break;
                    default:
                        this.Code = (int)inCommandInstruction;
                        try
                        {
                            if (inValue.GetType() == typeof(int))
                            {
                                this.leftOperand = null;
                                this.MemoryCellNeededExtra = (int)inValue;
                            }
                            else
                                this.leftOperand = (PBNumber)inValue;
                        }
                        catch (Exception ex)
                        {
                            throw new IncorrectOperandType("Command(" + inCommandInstruction + ", " + inValue + ")=[ " + ex.Message + " ]");
                        }
                        break;
                }
            }

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

            private int getOperationCode(String inOperation)
            {
                int code = 0;
                switch (inOperation)
                {
                    case "+": code = 1; break;
                    case "-": code = 2; break;
                    case "*": code = 3; break;
                    case "/": code = 4; break;
                    // Exponent need to be checked in Parser.cs
                    case "^": code = 5; break;
                    default: code = -1;  break;
                }
                return code;
            }
            public Command(String inOperation, String inLeftOperand, int inRightOperandFromMemory)
            {
                this.Code = getOperationCode(inOperation);
                this.leftOperand = new PBNumber(inLeftOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
                this.MemoryCellNeeded = inRightOperandFromMemory;
            }
            public Command(String inOperation, int inLeftOperandFromMemory, String inRightOperand)
            {
                this.Code = getOperationCode(inOperation);
                this.rightOperand = new PBNumber(inRightOperand, IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);
                this.MemoryCellNeeded = inLeftOperandFromMemory;
            }

            public Command(String inOperation, int inLeftOperandFromMemory, int inRightOperandFromMemory)
            {
                this.Code = getOperationCode(inOperation);

                this.leftOperand = null;
                this.rightOperand = null;

                this.MemoryCellNeededExtra = inRightOperandFromMemory;
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
            public Command(String inOperation, int inLeftOperand, int inRightOperand, int inCellUsed)
                : this(inOperation, inLeftOperand, inRightOperand)
            {
                this.MemoryCellUsed = inCellUsed;
            }
            public Command(String inOperation, int inLeftOperand, String inRightOperand, int inCellUsed)
                : this(inOperation, inLeftOperand, inRightOperand)
            {
                this.MemoryCellUsed = inCellUsed;
            }
        #endregion
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
    
        private void PushCommand(Command comm)
        {
            CommandStack.Push(comm);
        }

        private int GetUnusedMemoryCell()
        {
            int returnValue = -1;
            for (int index = 0; index < StackMemory.Count; index++)
            {
                if (StackMemory[index].CanBeErased == true) 
                {
                    StackMemory[index].CanBeErased = false;
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

            Command currCommand = new Command(inOperation, inLeftOperand, requiredCellMemory, currEmptyCellMemory);
            commandStack.Push(currCommand);
        }

        /// <summary>
        /// Push new Command for evaluation in stack.
        /// </summary>
        /// <param name="inOperation">Operation to evaluate.</param>
        /// <param name="inLeftOperand">Left operand.</param>
        /// <param name="requiredCellMemory">Memory Cell in StackMemory, that stores needed value.</param>
        public void PushCommand(String inOperation, int requiredCellMemory, int inLeftOperand)
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
            Command retValue;
            try
            {
                retValue = CommandStack.Pop();
                this.freeMemory(retValue.MemoryCellUsed);
            }
            catch (Exception ex)
            {
                return null;
            }
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

        /// <summary>
        /// Function converts stack with two-address instruction commands into pbStack with one-address instruction commands.
        /// </summary>
        /// <returns>Stack with one-address instruction commands.</returns>
        public Stack<Command> populateStack()
        {
            Stack<Command> returnVal = new Stack<Command>();
            Stack<Command> tempStack ;
            Command loadCommand;
            Command OperationCommand;
            Command SaveCommand;
            foreach (Command currCommand in CommandStack)
            {
                if (currCommand.leftOperand == null)
                {
                    if (currCommand.rightOperand == null)
                    {
                        // if Right points direct to value and Left operand stores in MemoryCell

                        // Save result to Memory Cell
                        // KOP  |MEM  |M{#}|
                        SaveCommand = new Command(CommandBase.commVals.Mem, currCommand.MemoryCellUsed);
                        returnVal.Push(SaveCommand);

                        // Load Right operand and Evaluate Operation
                        // KOP  |OP   |RO|
                        OperationCommand = new Command((CommandBase.commVals)currCommand.Code, currCommand.MemoryCellNeededExtra, true);
                        returnVal.Push(OperationCommand);

                        // Load left operand
                        // KOP  |LOAD |M{#}|
                        loadCommand = new Command(CommandBase.commVals.Load, currCommand.MemoryCellNeeded);
                        returnVal.Push(loadCommand);
                    }
                    else 
                    {
                        // if Right points direct to value and Left operand stores in MemoryCell

                        // Save result to Memory Cell
                        // KOP  |MEM  |M{#}|
                        SaveCommand = new Command(CommandBase.commVals.Mem, currCommand.MemoryCellUsed);
                        returnVal.Push(SaveCommand);

                        // Load Right operand and Evaluate Operation
                        // KOP  |OP   |RO|
                        OperationCommand = new Command((CommandBase.commVals)currCommand.Code, currCommand.MemoryCellNeeded);
                        returnVal.Push(OperationCommand);

                        // Load left operand
                        // KOP  |LOAD |M{#}|
                        loadCommand = new Command(CommandBase.commVals.Load, currCommand.MemoryCellNeeded);
                        returnVal.Push(loadCommand);
                    }
                }
                else
                    if (currCommand.rightOperand == null)
                    {
                        // if Left points direct to value and Right operand stores in MemoryCell

                        // Save result to Memory Cell
                        // KOP  |MEM  |M{#}|
                        SaveCommand = new Command(CommandBase.commVals.Mem, currCommand.MemoryCellUsed);
                        returnVal.Push(SaveCommand); 

                        // Load Right operand and Evaluate Operation
                        // KOP  |OP   |M{#}|
                        OperationCommand = new Command((CommandBase.commVals)currCommand.Code, currCommand.MemoryCellNeeded);
                        returnVal.Push(OperationCommand);

                        // Load left operand
                        // KOP  |LOAD |LO|
                        loadCommand = new Command(CommandBase.commVals.Load, currCommand.leftOperand);
                        returnVal.Push(loadCommand);
                    }
                    else
                    { 
                        // if Left and Right Operands are points direct to their values.

                        // Save result to Memory Cell
                        // KOP  |MEM  |M{#}|
                        SaveCommand = new Command(CommandBase.commVals.Mem, currCommand.MemoryCellUsed);
                        returnVal.Push(SaveCommand); 

                        // Load Right operand and Evaluate Operation
                        // KOP  |OP   |RO|
                        OperationCommand = new Command((CommandBase.commVals)currCommand.Code, currCommand.rightOperand);
                        returnVal.Push(OperationCommand);

                        // Load left operand
                        // KOP  |LOAD |LO|
                        loadCommand = new Command(CommandBase.commVals.Load, currCommand.leftOperand);
                        returnVal.Push(loadCommand);
                    }
                
            }// foreach
            //tempStack = new Stack<Command>(returnVal);
            //return tempStack;
            return returnVal;
        }
        public void clearStack()
        {
            CommandStack.Clear();   
            StackMemory.Clear(); 
        }        
    }
   

}

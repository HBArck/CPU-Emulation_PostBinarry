using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    
        

    public class Command
    {
        public enum commVals { Load, Add, Sub, Mul, Div, Exp, Mem };
        public String[] commNames = { "LOAD", "ADD", "SUB", "MUL", "DIV", "EXP", "M{0}" };
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

        public Command()
        {
            /*
            commands.Add("Load","LOAD");
            commands.Add("Add", "ADD");
            commands.Add("Sub", "SUB");
            commands.Add("Mul", "MUL");
            commands.Add("Div", "DIV");
            commands.Add("Exp", "EXP");
            commands.Add("Mem", "M{0}");*/
        }
        
    }

    /// <summary>
    /// This class represent polynom analizer
    /// </summary>
    class Stack 
    {
        public ProgramCore ProgramCoreInstance;

        private Stack<Command> commandStack;
        public Stack<Command> CommandStack
        {
            get { return commandStack; }
            set 
            {
                if (value.GetType() == Type.GetType("Command"))
                {
                    OnCommandStackChanged.Invoke();

                }
            }
        }

        
        public delegate void EventHandler();

        public event EventHandler OnCommandStackChanged ;
        
        
        Stack()
        {
            CommandStack = new Stack<Command>();
        }

        Stack(ProgramCore PCinst)
        {
            ProgramCoreInstance = PCinst;
            CommandStack = new Stack<Command>();
            
        }

        public void pushCommand(Command comm)
        {
            CommandStack.Push(comm);
            OnCommandStackChanged.Invoke();
        }

        public Command popCommand()
        {
            Command retValue = CommandStack.Pop();
            OnCommandStackChanged.Invoke();
            return retValue;
        }
    }
   

}

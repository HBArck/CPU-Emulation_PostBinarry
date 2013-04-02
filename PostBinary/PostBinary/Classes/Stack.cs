using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{

   
    class Command
    {
        private int Code;
        private Number leftOperand;
        private Number rightOperand;
        private Number result;
        public Number Result
        {
            get
            {
                return result;
            }
            set{}
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

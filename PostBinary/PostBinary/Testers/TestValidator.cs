using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class TestValidator
    {
        int numberOfCalls = 0;
        Validator validator;
        String[] arrayForValidator = {"abcde", "123", "e123", "[123]]", "(12)a(442)", "1+a[s]","a[2]", "33e-4" };
        public TestValidator() 
        {
            for (int i = 0; i < arrayForValidator.Length; i++)
            {
                runValidator(arrayForValidator[i]);
            }
        }

        private void runValidator(String str)
        {
            validator = new Validator();
            ValidationResponce response = validator.validate(str);
            if (!response.Error) 
            {
                Console.WriteLine("test#" + numberOfCalls +
                                   "\n     done\n");
            }
            else
            {
                Console.WriteLine("test#" + numberOfCalls +
                                    "\n     error: " + response.Error +
                                    "(" + response.ErrorType + ")" +
                                    " from: " + response.PositionBegin + ", to: " + response.PositionEnd +
                                    "\n     in the string:\n     " + str + "\n");
            }
            ++numberOfCalls;
        }
    }
}

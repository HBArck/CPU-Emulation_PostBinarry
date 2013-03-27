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
        public TestValidator() { 
            runValidator("abcde");
        }
        private void runValidator(String str){
            validator = new Validator();
            Responce.validationResponce response = validator.validate(str);
            if (!response.error) 
            {
                Console.WriteLine("test#" + numberOfCalls +
                                   "\n     done");
            }
            else
            {
                Console.WriteLine("test#" + numberOfCalls +
                                    "\n     error: " + response.error +
                                    "(" + response.errorType + ")" +
                                    " from: " + response.positionBegin + ", to: " + response.positionEnd +
                                    "\n     in the string:\n     " + str + "\n");
            }
            ++numberOfCalls;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class TestVariableValidator
    {
        Validator validator;
        String[] arrayForValidator = { "-00,1", "000.1", "-1", "+23", "-,55", ",6556", "000,333", "+3,400" };
        public TestVariableValidator()
        {
            for (int i = 0; i < arrayForValidator.Length; i++)
            {
                runVariableValidator(arrayForValidator[i]);
            }
        }
        public void runVariableValidator(String str)
        {
            Responce resp = new Responce();
            validator = new Validator();
            resp = validator.validateNumber(str);
            if (resp.Error)
                Console.WriteLine("error: " + resp.Error + " in string: \"" + resp.Result + "\"");
            else
                Console.WriteLine("       " + resp.Error + " in string: \"" + resp.Result + "\" OK");
        }
    }
}

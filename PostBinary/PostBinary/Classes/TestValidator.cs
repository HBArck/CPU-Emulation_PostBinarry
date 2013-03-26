using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class TestValidator
    {
        Validator validator;
        public TestValidator() { 
            String str = "Вася";
            validator = new Validator();
            Responce.validationResponce response = validator.validate(str);
            Console.WriteLine("error: " + response.error + "(" + response.errorType + ")" + "   from: " + response.positionBegin + ", to: " + response.positionEnd); 
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class Base
    {

    }
    public interface IValidationResponce
    {
        
            
    }

    class Responce 
    {
        String input;
        String validation;
        String stack;
        public struct validationResponce
        {
            public bool error ;// true - if validator found syntax error in inputStr, else - false
            public ValidatorErrorType errorType;
            public int positionBegin ;// begin position of error in inputStr; "-1" - means no error
            public int positionEnd ;// end position of error in inputStr; "-1" - means no error

            public void setValidationResponce(bool err, int posBegin, int posEnd, ValidatorErrorType errType)
            {
                if (posBegin > posEnd)
                {
                    int temp = posEnd;
                    posEnd = posBegin;
                    posBegin = temp;
                }
                error = err;
                errorType = errType;
                positionBegin = posBegin;
                positionEnd = posEnd;
            }
        } 
        
        validationResponce validResponce;      
    }

    /* 
     * Exceptions
     */
}

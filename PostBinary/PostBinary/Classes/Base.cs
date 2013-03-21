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
            public bool vError ;// true - if validator found syntax error in inputStr, else - false
            public ValidatorErrorType vErrorType;
            public int vPositionBegin ;// begin position of error in inputStr; "-1" - means no error
            public int vPositionEnd ;// end position of error in inputStr; "-1" - means no error
        } 
        
        validationResponce validResponce;
        public void setValidationResponce(bool error, int posBegin, int posEnd, ValidatorErrorType errType)
        {
            validResponce = new validationResponce();
            validResponce.vError = error;
            validResponce.vErrorType = errType;
            validResponce.vPositionBegin = posBegin;
            validResponce.vPositionEnd = posEnd;
        }
       
    }

    /* 
     * Errors 
     */
    class ValidatorErrorType 
    { 

    }
    /* 
     * Exceptions
     */
}

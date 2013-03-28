using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    /// <summary>
    /// Basic class for responces in program
    /// </summary>
    class Responce 
    {
        String input;
        String validation;
        String stack;
        struct resp {
            
        }
        
    }

    /// <summary>
    /// Validation Responce 
    /// </summary>
    public class ValidationResponce 
    {
       public static struct resp{
            public bool error;// true - if validator found syntax error in inputStr, else - false
            public String errorType;
            public int positionBegin;// begin position of error in inputStr; "-1" - means no error
            public int positionEnd;// end position of error in inputStr; "-1" - means no error
        }
        private resp responce = new resp();
        public resp Responce
        {
            get { return responce; }
        }

        public void setResponce(bool err, int posBegin, int posEnd, String errType)
        {
            if (posBegin > posEnd)
            {
                int temp = posEnd;
                posEnd = posBegin;
                posBegin = temp;
            }
            responce.error = err;
            responce.errorType = errType;
            responce.positionBegin = posBegin;
            responce.positionEnd = posEnd;
        }
        public bool Error
        {
            get { return responce.error; }
            set { responce.error = value; }
        }

    }

}


/* UNUSED */

/*public struct validationResponce
        {
            public bool error ;// true - if validator found syntax error in inputStr, else - false
            public String errorType;
            public int positionBegin ;// begin position of error in inputStr; "-1" - means no error
            public int positionEnd ;// end position of error in inputStr; "-1" - means no error

            public void setValidationResponce(bool err, int posBegin, int posEnd, String errType)
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
        } */

//validationResponce validResponce;      
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    /// <summary>
    /// This class makes input validation. 
    /// It generates response for current input sting.
    /// </summary>
    class Validator
    {
        /** DELETE THIS ON IMPLEMINTATION
         * validErEn0	Syntax Error	Базовая ошибка, говорящая о ошибке в выражении
         * validErEn1	Missing {0} bracket in expression	"()) ; [][" Ошибка, которая говорит, что в строке отсутствует скобка 
         * validErEn2	No such operation	"= ; ! ; ~" Если пользователь ввел символ неподдерживаемой операции
         * validErEn3	Variable or number missing	"++"  Если пользователь ввел подряд две операции
         * validErEn4	Incorrect scientific notation	"3,e ; 3,e-3,9"   Ошибка в научной нотации
         * validErEn5	Missing '-' or '+' in expression	"3,0e3"   Отсутствует знак степени для научной нотации
         * validErEn6	Only digits allowed to represent array index	"a[a]"   Если пользователь ввел
         * validErEn7	Only integers allowed to represent array index	"a[12,3] ; a[1+2]"  Если пользователь ввел вещественное число и\или оператор в качестве индекса массива
         * validErEn8	Missing array identifier	"+[x]+" Если пользователь не ввел имя переменной для массива
         * validErEn9	Only positive integers should represent array index	"a[-1]"  Если пользователь ввел отрицательный индекс массива
         */
        public String inputStr = "";
        //private ValidatorErrorType errorMessenger;
        private String currErrorType; 

        public Validator()
        {
            //errorMessenger = new ValidatorErrorType();
        }

        public Responce.validationResponce validate(String inStr)
        {
            Responce.validationResponce response = new Responce.validationResponce();

            response = vCharacters(inStr);
            if (!response.error)
            {
                response = vSequenceOfNumVar(inStr);
                if (!response.error)
                {
                    response = vBreckets(inStr);
                }
            }
            return response;

        }
        /*
         * validate number of brackets
         */
        private Responce.validationResponce vBreckets(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            Responce.validationResponce response = new Responce.validationResponce();

            int openedSquareBrackets = 0;
            int closedSquareBrackets = 0;
            int openedRoundBrackets = 0;
            int closedRoundBrackets = 0;

            CharEnumerator ce = inStr.GetEnumerator();
            while (ce.MoveNext())
            {
                switch (ce.Current)
                {
                    case '(':
                        {
                            currErrorType = "validErEn1";
                            openedSquareBrackets++;
                            break;
                        }
                    case ')':
                        {
                            currErrorType = "validErEn1";
                            closedSquareBrackets++;
                            break;
                        }
                    case '[':
                        {
                            currErrorType = "validErEn1";
                            openedRoundBrackets++;
                            break;
                        }
                    case ']':
                        {
                            currErrorType = "validErEn1";
                            closedRoundBrackets++;
                            break;
                        }
                }

            }
            if (openedSquareBrackets != closedSquareBrackets)
            {
                error = true;
                errorBegin = inStr.LastIndexOf("(");
                errorEnd = inStr.LastIndexOf(")");
            }
            else if (openedRoundBrackets != closedRoundBrackets)
            {
                error = true;
                errorBegin = inStr.LastIndexOf("[");
                errorEnd = inStr.LastIndexOf("]");
            }
            response.setValidationResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }

        private Responce.validationResponce vCharacters(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            Responce.validationResponce response = new Responce.validationResponce();
            Regex rgx = new Regex(@"[*+^\-/a-df-zA-DF-Z\d\(\)\[\]]+");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count != 1)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validErEn0";
            }
            response.setValidationResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
         * validate sequence of numbers and variables 
         */
        private Responce.validationResponce vSequenceOfNumVar(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            Responce.validationResponce response = new Responce.validationResponce();

            Regex rgx = new Regex(@"[*+^\-/]{2}");

            MatchCollection mc = rgx.Matches(inStr);

            if (mc.Count > 0)
            {
                error = true;
                errorBegin = inStr.IndexOf(mc[0].ToString());
                String lastError = mc[mc.Count - 1].ToString();
                errorEnd = inStr.LastIndexOf(lastError) + lastError.Length - 1;
                currErrorType = "";
            }

            response.setValidationResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
    }
}

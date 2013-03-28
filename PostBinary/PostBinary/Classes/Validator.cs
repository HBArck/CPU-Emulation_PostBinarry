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
         * [errorName]+[localization]+[# - number]
         * example of Syntax error for English user will be "validErEn0" , and for Russian - "validErRu0"
         */
        public String inputStr = "";
        //private ValidatorErrorType errorMessenger;
        private String currErrorType; 

        public Validator()
        {
            //errorMessenger = new ValidatorErrorType();
        }

        public ValidationResponce validate(String inStr)
        {
            ValidationResponce response = new ValidationResponce();

            response = vCharacters(inStr);
            if (!response.Error)
            {
                response = vSequenceOfNumVar(inStr);
                if (!response.Error)
                {
                    response = vBreckets(inStr);
                }
            }
            return response;

        }
        /*
         * validate number of brackets
         */
        private ValidationResponce vBreckets(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            ValidationResponce response = new ValidationResponce();

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
                            currErrorType = "validEr1";
                            openedSquareBrackets++;
                            break;
                        }
                    case ')':
                        {
                            currErrorType = "validEr1";
                            closedSquareBrackets++;
                            break;
                        }
                    case '[':
                        {
                            currErrorType = "validEr1";
                            openedRoundBrackets++;
                            break;
                        }
                    case ']':
                        {
                            currErrorType = "validEr1";
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
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }

        private ValidationResponce vCharacters(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"[*+^\-/a-df-zA-DF-Z\d\(\)\[\]]+");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count != 1)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr0";
            }
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
         * validate sequence of numbers and variables 
         */
        private ValidationResponce vSequenceOfNumVar(String inStr)
        {
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            ValidationResponce response = new ValidationResponce();

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

            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
    }
}

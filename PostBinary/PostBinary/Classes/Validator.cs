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
         * validEr0	Syntax Error	Базовая ошибка, говорящая о ошибке в выражении
         * validEr1	Missing {0} bracket in expression	"()) ; [][" Ошибка, которая говорит, что в строке отсутствует скобка 
         * validEr3	Variable or number missing	"++"  Если пользователь ввел подряд две операции
         * validEr4	Incorrect scientific notation	"3,e ; 3,e-3,9"   Ошибка в научной нотации
         * validEr5	Missing '-' or '+' in expression	"3,0e3"   Отсутствует знак степени для научной нотации
         * validEr6	Only digits allowed to represent array index	"a[a]" ; "a[12,3] ; a[1+2]"  Если пользователь ввел
         * validEr7	Missing array identifier	"+[x]+" Если пользователь не ввел имя переменной для массива
         * 
         * [errorName]+[localization]+[# - number]  - localization adds in Error.cs
         * example of Syntax error for English user will be "validErEn0" , and for Russian - "validErRu0"
         */
        public String inputStr = "";
        //private ValidatorErrorType errorMessenger;
        private String currErrorType;

        public Validator()
        {
            //errorMessenger = new ValidatorErrorType();
        }

        /// <summary>
        /// Validates float number 
        /// </summary>
        /// <param name="inNumber">Float number to verify.</param>
        /// <returns></returns>
        public Responce validateNumber(String inNumber)
        {
            Responce resp = new Responce();
            resp.Error = false;
            resp.Result = inNumber;

            return resp;
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
                    if (!response.Error)
                    {
                        response = vInsideBreckets(inStr);
                        if (!response.Error)
                        {
                            response = vNameOfArray(inStr);
                            if (!response.Error)
                            {
                                response = vScientificNotation(inStr);
                            }
                        }
                    }
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
            String[] args;
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
            //  args = new String[] { ce.Current.ToString() };
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
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;
            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"[*+^\-/a-zA-Z\d\(\)\[\]\.,]+");
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
                currErrorType = "validEr3";
            }

            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
        * validate number of brackets
        */
        private ValidationResponce vInsideBreckets(String inStr)
        {
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;

            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"(\[[*+^\-/a-zA-Z\(\)\[\]]+\])");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count > 0)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr6";
            }
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
        * validate number of brackets
        */
        private ValidationResponce vNameOfArray(String inStr)
        {
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;

            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"([*+^\-/eE\d\(\)\[\]]\[)");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count > 0)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr7";
            }
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
        * validate number of brackets
        */
        private ValidationResponce vScientificNotation(String inStr)
        {
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */


          //  "3,e ; 3,e-3,9"  
        //"3,0e3"

            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;

            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"(e[*^/a-zA-Z\d\(\)\[\]\.,])");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count > 0)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr4";
            }
            if (!error) {
                char check = 'e';
                int count = 0;
                CharEnumerator ce = inStr.GetEnumerator();
                while (ce.MoveNext())
                {
                    if (check == ce.Current)
                    {
                        count++;
                    }
                }

                Regex regexScientificNotation = new Regex(@"(\d+[+\-\d+]\d+[,.]e[+\-]\d+)");
                              
                MatchCollection mcScientificNotation = rgx.Matches(inStr);
                Console.WriteLine(count +"|"+ mcScientificNotation.Count + "  "+ inStr);
                if (count != mcScientificNotation.Count) {
                    error = true;
                    errorBegin = 0;
                    errorEnd = 0;
                    currErrorType = "validEr4.1";
                }
            }
            
            
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
    }
}

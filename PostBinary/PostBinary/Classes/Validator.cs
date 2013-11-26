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
        public String inputStr = "";
        //private ValidatorErrorType errorMessenger;
        private String currErrorType;
        public delegate ValidationResponce validates(String inStr);
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
          /*
           * TODO: find and fill errorBegin / errorEnd 
           */
            Responce resp = new Responce();

            /*if ((inNumber[0] != '-') && (inNumber[0] != '+'))
                inNumber = "+" + inNumber;*/

            inNumber = inNumber.Replace(',', '.');

            if (inNumber.IndexOf('.') == -1)
                inNumber += ".0";

            if (inNumber[1] == '.')
                inNumber = inNumber[0] + "0" + inNumber.Substring(1);

            if (inNumber[inNumber.Length - 1] == '.')
                inNumber += "0";

            inNumber = deleteZero(inNumber);

            Regex rgx = new Regex(@"\d+.\d+"); //(@"[\+\-]\d+,\d+")
            MatchCollection mc = rgx.Matches(inNumber);
            resp.Error = (mc.Count != 1);
            resp.Result = inNumber;
            return resp;
        }
        private String deleteZero(String inNumber)
        {
            char sign = inNumber[0];
            inNumber = inNumber.Substring(1);

            int firstZeroPosition = inNumber.IndexOf('0');

            if (firstZeroPosition != 0)
                return sign + inNumber;

            int commaPosition = inNumber.IndexOf(',');
            int lastZeroPosition = inNumber.Remove(commaPosition).LastIndexOf('0');
            //int firstNonZeroPosition = inNumber.Substring(commaPosition).LastIndexOf('0');



            if (lastZeroPosition > firstZeroPosition - 1)
                for (var i = 0; i < lastZeroPosition; i++)
                {
                    if (inNumber[i] == '0')
                    {
                        inNumber = inNumber.Substring(1);
                        if (inNumber[i + 1] == ',')
                            break;
                        --i;

                    }
                    else
                        break;
                }

            return sign + inNumber;
        }
        public ValidationResponce validate(String inStr)
        {
            ValidationResponce response = new ValidationResponce();
            Delegate[] delGreet = new Delegate[8] {
                new validates(vCharacters),
                new validates(vSequenceOfNumVar),
                new validates(vBreckets),
                new validates(vInsideBreckets),
                new validates(vNameOfArray),
                new validates(vScientificNotation),
                new validates(vMultiplication),
                new validates(vConstant)
            };

            foreach (validates del in delGreet)
            {
                response = del(inStr);
                if (response.Error)
                    return response;
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
            String currErrorType = "validEr1";
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
                            //   currErrorType = "validEr1";
                            openedSquareBrackets++;
                            break;
                        }
                    case ')':
                        {
                            //       currErrorType = "validEr1";
                            closedSquareBrackets++;
                            break;
                        }
                    case '[':
                        {
                            //    currErrorType = "validEr1";
                            openedRoundBrackets++;
                            break;
                        }
                    case ']':
                        {
                            //   currErrorType = "validEr1";
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
            Regex rgx = new Regex(@"[*+^\-/a-zA-Z\d\(\)\[\]\.,#]+");
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
            Regex rgx = new Regex(@"([*+^\-/E#\d\(\)\[\]]\[)");
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
            if (!error)
            {
                /*char check = 'e';
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
                Console.WriteLine(count + "|" + mcScientificNotation.Count + "  " + inStr);
                if (count != mcScientificNotation.Count)
                {
                    error = true;
                    errorBegin = 0;
                    errorEnd = 0;
                    currErrorType = "validEr4.1";
                }*/
            }


            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
        /*
        * validate sign of multiplication
        */
        private ValidationResponce vMultiplication(String inStr)
        {
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;

            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"([\d\)\]][\(\[])");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count > 0)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr9";
            }
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
       /*
        * validate constant
        */
        private ValidationResponce vConstant(String inStr)
        {
            /*
             * TODO: find and fill errorBegin / errorEnd 
             */
            bool error = false;
            int errorBegin = -1;
            int errorEnd = -1;

            ValidationResponce response = new ValidationResponce();
            Regex rgx = new Regex(@"(#[*+^\-/\d\(\)\[\]\.,])");
            MatchCollection mc = rgx.Matches(inStr);
            if (mc.Count > 0)
            {
                error = true;
                errorBegin = 0;
                errorEnd = 0;
                currErrorType = "validEr10";
            }
            response.setResponce(error, errorBegin, errorEnd, currErrorType);
            return response;
        }
    }
}
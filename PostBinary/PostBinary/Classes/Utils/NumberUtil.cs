using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PostBinary.Classes
{
    /// <summary>
    /// Stores number states
    /// </summary>
    public enum stateOfNumber
    {
        normalized,
        denormalized,
        zero,
        infinite,
        NaN,
        error
    };

    class NumberUtil
    {
        private ProgramCore PCoreInst = null;

        /// <summary>
        /// Struct stores Float and Integer part of Number
        /// </summary>
        public struct IFPartsOfNumber
        {
            public String Sign;
            public String IntegerPart;
            public String FloatPart;
        }

        public NumberUtil(ProgramCore newPCoreInst)
        {
            if (PCoreInst == null)
            {
                PCoreInst = newPCoreInst;
            }
        }
       
        /// <summary>
        /// Function converts from scientific notation to normal notation.
        /// Number must be in scientific notation.
        /// </summary>
        /// <param name="str">Number in scientific notation. (1,23e+4)</param>
        /// <returns>Number in normal notation. (12300)</returns>
        public String ScientificToNormal(String str)
        {
            return str.Replace("e", "*10^(") + ")";
        }

        /// <summary>
        /// Function counts significant characters in current number 
        /// Number must be in scientific notation.
        /// </summary>
        /// <param name="str">Number in which significant characters should be found. (1,123e-56)</param>
        /// <returns>Counted number of significant characters in number. (4)</returns>
        public int CountSignificantCharacters(String str)
        {
            return str.Split('e')[0].Remove(str.Split('e')[0].IndexOf(','), 1).Trim('-').Length;
        }

        /// <summary>
        /// Function finds exponent of the number.
        /// Number must be in scientific notation.
        /// </summary>
        /// <param name="str">Number in which exponent should be found. (1,123e-56)</param>
        /// <returns>Number exponent. (56)</returns>
        public String NumberExponent(String str)
        {
            return str.Split('e')[str.Split('e').Count() - 1].Trim('-');
        }

        /// <summary>
        /// Function converts from normal notation to scientific notation.
        /// </summary>
        /// <param name="str">Number in normal notation. (12300)</param>
        /// <returns>Number in scientific notation. (1,23e+4)</returns>
        public String NormalToScientific(String str)
        {
            return "";
        }


        ////////////////////
         public struct scientificNotationNumber
        {
            public String str;
            public int numCh;
            public string exponent;
        };
        public scientificNotationNumber retStruct;
        public void Run()
        {
            String[] testStr = { "123,456e-789", "123,456e+789", "3,0e-1" };
            for (int i = 0; i < testStr.Length; i++)
            {
                retStruct = SplitString(testStr[i]);
                Console.WriteLine(" {0} , {1}, {2}", retStruct.str, retStruct.numCh, retStruct.exponent);
                Console.ReadLine();
            }

        }
        public scientificNotationNumber SplitString(string str)
        {
            retStruct = new scientificNotationNumber();
            retStruct.numCh = str.Split('e')[0].Remove(str.Split('e')[0].IndexOf(','), 1).Trim('-').Length;
            retStruct.exponent = str.Split('e')[str.Split('e').Count() - 1].Trim('-');
            retStruct.str = str.Replace("e", "*10^(") + ")";
            return retStruct;
        }

        public PBNumber CreateNumber(String inNumber)
        {
            String currentNumber = "";
            String Exponent = "";
            String Mantissa = "";
            
            IFPartsOfNumber currentPartialNumber;
            IFPartsOfNumber currentPartialNumber2cc;
            
            currentNumber = NormalizeNumber(inNumber,ACCURANCY, NumberFormat.Integer);
            currentPartialNumber = DenormalizeNumber(currentNumber, NumberFormat.Integer);

            currentPartialNumber2cc.Sign = currentPartialNumber.Sign;
            currentPartialNumber2cc.IntegerPart = convert10to2IPart(currentPartialNumber.IntegerPart);
            currentPartialNumber2cc.FloatPart = convert10to2FPart(currentPartialNumber.FloatPart);

            /*Create he Pb Number and fill it*/
            
            PBNumber number = new PBNumber(128);
            //Define Exponent before Mantissa for correct running algorithm 
            Exponent = selectExp(currentPartialNumber2cc, NumberFormat.Integer, number);
            number.SetFields(currentPartialNumber.Sign, Exponent, Mantissa);
            Mantissa = selectMantissa(currentPartialNumber2cc, NumberFormat.Integer, number);
            number.SetFields(currentPartialNumber.Sign, Exponent, Mantissa);
            return number;
            
        }

        public String NormalizeNumber(String dataString, int inAccuracy, NumberFormat inNumberFormat)
        {
            /// Current Number Sign 0 = '+'; 1 = '-'
            String Sign;
            try
            {
                if (dataString.Length > inAccuracy)
                    dataString = dataString.Substring(0, inAccuracy);

                if (dataString.Contains("E"))
                    dataString = dataString.Replace('E', 'e');
                //else
                // return null || Rise FCCoreException
                if (dataString.Contains("."))
                    dataString = dataString.Replace('.', ',');
                //else
                // return null || Rise FCCoreException

                if (dataString.IndexOf(',') == 0)
                    dataString = "0" + dataString;


                if ((dataString[0] != '-') && (dataString[0] != '+'))
                {
                    dataString = "+" + dataString;
                    if (inNumberFormat == 0)
                    {
                        Sign = "0";
                        //SignLeft = "0";
                    }
                    else
                    {
                        /** Reserved for float and interval formats
                        if (Left_Right == PartOfNumber.Left)
                            SignLeft = "0";
                        else
                            SignRight = "0";
                         */
                    }
                }
                else
                {
                    if (inNumberFormat == 0)
                    {
                        Sign = "0";
                        
                        //SignLeft = "1";
                    }
                    else
                    {
                        /** Reserved for float and interval formats
                        if (Left_Right == PartOfNumber.Left)
                            SignLeft = "1";
                        else
                            SignRight = "1";
                         */
                    }
                }                
                if (dataString.IndexOf(',') == -1)
                    if (dataString.IndexOf('e') != -1)
                        dataString = dataString.Substring(0, dataString.IndexOf('e')) + ",0" + dataString.Substring(dataString.IndexOf('e'));
                    else
                        dataString = dataString + ",0";

                if ((dataString[dataString.IndexOf('e') + 1] != '+') &&
                    (dataString[dataString.IndexOf('e') + 1] != '-'))
                    dataString = dataString.Replace("e", "e+");

                if (dataString.IndexOf('e') == -1)
                    dataString = dataString + "e+0";
            }
            catch (Exception ex)
            {
                throw new Exception("NormalizeNumber:" + ex.Message);
            }
            
            return dataString;
        }

        /// <summary>
        /// Denormolizes number
        /// </summary>
        /// <param name="dataString">Input String to dernomolize</param>
        /// <param name="NumberFormat">Indicates format of input number</param>
        /// <returns>Denormolized number as String</returns>
        public IFPartsOfNumber DenormalizeNumber(String dataString, NumberFormat inNumberFormat)
        {
            String denormNumber = "";
            String denormIntPart = "", denormFloatPart = "";
            String ExpSign, Sign="0", SignCharacter="+";
            String E;
            String[] tempArray;
            IFPartsOfNumber returnValue = new IFPartsOfNumber();
            try
            {
                ExpSign = dataString.Substring(dataString.IndexOf('e') + 1, 1);
                if (dataString[0] == '+')
                {
                    if (inNumberFormat == 0)
                    {
                        Sign = "0";
                        SignCharacter = "+";
                    }
                    else
                    {
                        /*
                        if (Left_Right == PartOfNumber.Left)
                        {
                            SignLeft = "0";
                            SignCharacterLeft = "+";
                        }
                        else
                        {
                            SignRight = "0";
                            SignCharacterRight = "+";
                        }*/
                    }
                }
                else
                    if (dataString[0] == '-')
                    {
                        if (inNumberFormat == 0)
                        {
                            Sign = "1";
                            SignCharacter = "-";
                        }
                        else
                        {
                            /*
                            if (Left_Right == PartOfNumber.Left)
                            {
                                SignLeft = "1";
                                SignCharacterLeft = "-";
                            }
                            else
                            {
                                SignRight = "1";
                                SignCharacterRight = "-";
                            }*/
                        }
                    }
                    else
                    {
                        if (inNumberFormat == 0)
                        {
                            Sign = "0";
                            SignCharacter = "+";
                        }
                        else
                        {
                            /*
                            if (Left_Right == PartOfNumber.Left)
                            {
                                SignLeft = "0";
                                SignCharacterLeft = "+";
                            }
                            else
                            {
                                SignRight = "0";
                                SignCharacterRight = "+";
                            }*/
                        }
                    }

                returnValue.Sign = Sign;

                //throw new Exception("Func [selectSEM]:= NoSignException.");

                int index = dataString.IndexOf('e') + 1;
                if (index < dataString.Length)
                    E = dataString.Substring(index, dataString.Length - index);
                else
                    throw new Exception("Func [selectSEM]:= NoExponentaException.");

                int iExp = Math.Abs(int.Parse(E));
                if ((dataString[0] == '-') || (dataString[0] == '+'))
                    dataString = dataString.Substring(1);
                /*iPart */
                denormIntPart = dataString.Substring(0, dataString.IndexOf(','));
                index = dataString.IndexOf(',') + 1;

                /*fPart*/
                denormFloatPart = dataString.Substring(index, dataString.IndexOf('e') - index);////+1
                if (ExpSign == "+")
                {
                    String fPartTemp = denormFloatPart;
                    if (iExp > 0)
                    {
                        tempArray = new String[Math.Abs(iExp - denormFloatPart.Length)];
                        for (int i = 0; i < (Math.Abs(iExp - denormFloatPart.Length)); i++)
                            tempArray[i] = "0";
                        fPartTemp = fPartTemp + String.Join("", tempArray);
                        denormFloatPart = "0";
                    }
                    denormIntPart = denormIntPart + fPartTemp.Substring(0, iExp);
                    denormFloatPart = fPartTemp.Substring(iExp);
                    if (denormFloatPart.Length == 0)
                        denormFloatPart = "0";
                }
                else
                {
                    String iPartTemp = denormIntPart;
                    tempArray = new String[Math.Abs(iExp - denormIntPart.Length)];
                    for (int i = 0; i < Math.Abs((iExp - denormIntPart.Length)); i++)
                        tempArray[i] = "0";
                    iPartTemp = String.Join("", tempArray) + iPartTemp;
                    if (iExp > denormIntPart.Length)
                    {
                        denormFloatPart = iPartTemp + denormFloatPart;
                        denormIntPart = "0";
                    }
                    else
                    {
                        denormFloatPart = iPartTemp.Substring(iPartTemp.Length - iExp) + denormFloatPart;
                        if (iPartTemp.Length != iExp)
                            denormIntPart = iPartTemp.Substring(0, iPartTemp.Length - iExp);
                        else
                            denormIntPart = "0";
                    }
                }
                // iPart = myUtil.deleteZeroFromNumber(iPart);
                // if (iPart[0] == '0')
                //    iPart = iPart.Substring(1);
                while ((denormIntPart[0] == '0') && (denormIntPart.Length > 1))
                {
                    denormIntPart = denormIntPart.Substring(1);
                }
                // Compact to one statement num32 = num64 = num128 = num256 = denorm
                
                denormNumber = SignCharacter + denormIntPart + "," + denormFloatPart;
                
                //Num32.Denormalized = Num64.Denormalized = Num128.Denormalized = Num256.Denormalized = 
                if (inNumberFormat == 0)
                {
                    /*Num32.IntPartDenormalized = Num64.IntPartDenormalized = Num128.IntPartDenormalized = Num256.IntPartDenormalized = IntPartDenormalized = denormIntPart;
                    Num32.FloatPartDenormalized = Num64.FloatPartDenormalized = Num128.FloatPartDenormalized = Num256.FloatPartDenormalized = FloatPartDenormalized = denormFloatPart;*/
                    //DenormIntPart = denormIntPart;
                    returnValue.IntegerPart = denormIntPart;
                    //DenormFloatPart = denormFloatPart;
                    returnValue.FloatPart = denormFloatPart;
                }
                else
                {
                    /*
                    if (Left_Right == PartOfNumber.Left)
                    {
                        IntPartDenormalizedFILeft = denormIntPart;
                        FloatPartDenormalizedFILeft = denormFloatPart;
                    }
                    else
                    {
                        IntPartDenormalizedFIRight = denormIntPart;
                        FloatPartDenormalizedFIRight = denormFloatPart;
                    }
                    //Num32.IntPartDenormalizedFI = Num64.IntPartDenormalizedFI = Num128.IntPartDenormalizedFI = Num256.IntPartDenormalizedFI = IntPartDenormalizedFI = denormIntPart;
                    //Num32.FloatPartDenormalizedFI = Num64.FloatPartDenormalizedFI = Num128.FloatPartDenormalizedFI = Num256.FloatPartDenormalizedFI = FloatPartDenormalizedFI = denormFloatPart;
                    DenormIntPartFI = denormIntPart;
                    DenormFloatPartFI = denormFloatPart;
                    */
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                //throw new FCCoreFunctionException("Exception in Func ['selectSEM'] Mess=[" + ex.Message + "]");
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public const int ACCURANCY = 2000;
        public static int AdditionalAccurancy = 0;
        /// <summary>
        /// Converts float part of number form 10cc to 2cc.Lenght depends from constant 'ACCURANCY'
        /// Funcs using: NONE
        /// Vars using : ACCURANCY
        /// </summary>
        /// <param name="inString">Input Number to convert</param>
        /// <returns>Returns number float part in 2cc</returns>
        public String convert10to2FPart(String inString)
        {// accurancy   -> ACCURANCY
            String result = "";
            String outString = "";
            int plusOne = 0;
            int countAccuracy, i, currNumber;

            try
            {
                if (inString == "0")
                {
                    result = "0";
                    return result;
                }

                for (countAccuracy = 0; countAccuracy < ACCURANCY + AdditionalAccurancy; countAccuracy++)
                {

                    outString = "";
                    plusOne = 0;
                    for (i = inString.Length; i > 0; i--)
                    {
                        currNumber = int.Parse(inString[i - 1].ToString());
                        if (currNumber < 5)
                        {
                            outString = (currNumber * 2 + plusOne).ToString() + outString;
                            plusOne = 0;
                        }
                        else
                        {
                            outString = (currNumber * 2 + plusOne - 10).ToString() + outString;
                            plusOne = 1;
                            if (i == 1)
                                outString = "1" + outString;
                        }
                    }

                    if (countAccuracy != ACCURANCY + AdditionalAccurancy - 1)
                    {
                        if (outString.Length > inString.Length)
                        {
                            result = result + "1";
                            outString = outString.Substring(1);
                        }
                        else
                        {
                            result = result + "0";
                        }

                    }
                    else
                        if (isStringZero(result))
                            AdditionalAccurancy += 200;

                    inString = outString;
                }
            }
            catch (Exception ex)
            {
                //throw new FCCoreException();
                throw new Exception("Func [convert10to2FPart]:=" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Converts integer part of number form 10cc to 2cc.Lenght depends only from number
        /// Funcs using: NONE
        /// Vars using : NONE
        /// </summary>
        /// <param name="inString">Input Number to convert</param>
        /// <returns>Returns number integer part in 2cc</returns>
        public static String convert10to2IPart(String inString)
        {
            String result = ""; 	// Результат каждого деления
            String balanse = ""; 	// Массив остатков от каждого деления (исходное число в 2 с/с)
            String balanseTemp = "";// Временная переменная для подсчета остатка
            int activeDividend;		// Текущее делимое
            bool saveMinus = false;
            int i = 0;

            try
            {
                if (inString.IndexOf(',') != -1)
                    inString = inString.Substring(0, inString.IndexOf(','));
                if (inString[0] == '-')
                {
                    saveMinus = true;
                    i++;
                }
                else
                    if (inString[0] == '+')
                        inString = inString.Substring(1);

                while ((inString[i] == '0') && (inString.Length > 1))
                {
                    if (!saveMinus)
                        inString = inString.Substring(1);
                    else
                        inString = "-" + inString.Substring(2);
                }

                result = inString;
                int iRes;
                while (true)
                {	          // цикл по всем делениям (14,7,3,1)
                    /*
                     *        14 |_2_
                     *        14 |7	 |_2_
                     *		  --  6  |3  |_2_
                     *		   0  --  2  |1  
                     *			  1   --
                     *				  1	
                     *
                     *									balanse=1110
                     *					result=14
                     *					result=7
                     *					   ...
                     *					result=1
                     */
                    if (result == "")
                        break;

                    inString = result;
                    result = "";
                    inString = inString + ("0");
                    activeDividend = int.Parse(inString[0].ToString());

                    for (i = 0; i < (inString.Length - 1); i++)
                    { // деление предыдущего результата  
                        switch (activeDividend)
                        {
                            case 0:
                                {
                                    result = result + ("0");
                                    break;
                                }
                            case 1:
                                {
                                    if (i != 0)
                                        result = result + ("0");
                                    if (i == (inString.Length - 2))
                                        balanseTemp = "1";
                                    break;
                                }
                            default:
                                {
                                    iRes = activeDividend / 2;
                                    result = result + ((iRes));
                                    activeDividend %= 2;
                                    balanseTemp = activeDividend.ToString();
                                    break;
                                }
                        }
                        if ((activeDividend != 0) || (inString[i + 1] != '0'))
                            activeDividend = int.Parse((activeDividend).ToString() + inString[i + 1].ToString());
                    }
                    balanse = balanseTemp + (balanse);

                    if (result.Length == 1)//  Выход из цикла 
                    {
                        int iTemp = int.Parse(result);	//  когда результат=1, или =0
                        if ((iTemp == 0) || (iTemp == 1))				//
                            break;								//
                    }
                }
                balanse = result + (balanse);
            }
            catch (Exception ex)
            {
                //throw new FCCoreException();
                throw new Exception("Func [convert10to2IPart]:=" + ex.Message);
            }
            return balanse;
        }
       
        /// <summary>
        /// Converts number integer part from 2cc to 10cc
        /// </summary>
        /// <param name="inString">Input number integer part </param>
        /// <returns>Number integer part in 10cc</returns>
        public String convert2to10IPart(String inString)
        {
            /*
             * Перевод целой части числа из 2 с/с в 10 с/с
             */
            String result = "0";
            String tempRes = "0";
            String factor = "1"; // множитель=степени 2
            try
            {
                if (isStringZero(inString))
                {
                    return "0";
                }
                if ((inString == "0") || (inString == ""))
                    return "0";
                for (int i = inString.Length; i > 0; i--)
                {
                    if (inString[i - 1] == '1')
                        tempRes = Addition(result, factor);
                    result = tempRes;
                    factor = Multiplication(factor, "2");
                }

                if ((result != "0") && (result != ""))
                {
                    result = result.Substring(1);
                    if (result.IndexOf(',') != -1)
                        return result.Substring(0, result.IndexOf(','));
                    else
                        return result;
                }
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw new FCCoreArithmeticException("Func 'convert2to10IPart' = [ " + ex.Message + " ]");
            }
        }

        #region Addition Functions
        /// <summary>
        /// Adds Operand1 to Operand2
        /// Permissions: Input Numbers can be with sign's or without
        /// </summary>
        /// <param name="Operand1">First operand of addition</param>
        /// <param name="Operand2">Second operand of addition</param>
        /// <returns>Number wich is representation an addition of two numbers</returns>
        public String Addition(String Operand1, String Operand2)
        {
            String SignResult = "", Result = "";
            String iPartOperand1 = "", fPartOperand1 = "";
            String iPartOperand2 = "", fPartOperand2 = "";

            //Temporary Vars
            String signOperand1 = "", signOperand2 = "";
            int LenFloatPartOperand1 = 0, LenFloatPartOperand2 = 0;
            int i, z;
            int Operand1Dot = 0;
            int Operand2Dot = 0;
            int Operand1Exp = 0, Operand2Exp = 0;
            char[] chArr = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            BigInteger BigOperand1 = 0;
            BigInteger BigOperand2 = 0;
            try
            {
                /* Sign Result */
                if ((Operand1[0] == '-') || (Operand1[0] == '+'))
                {
                    signOperand1 = Operand1.Substring(0, 1);
                    Operand1 = Operand1.Substring(1);  // Devident without sign = 123123,123
                }
                else
                    signOperand1 = "+";

                if ((Operand2[0] == '-') || (Operand2[0] == '+'))
                {
                    signOperand2 = Operand2.Substring(0, 1);
                    Operand2 = Operand2.Substring(1);  // Devider without sign = 789789,789
                }
                else
                    signOperand2 = "+";

                if (signOperand1 == "+")
                {
                    if (signOperand2 == "+")
                        SignResult = "+";
                    else
                        SignResult = "-";
                }
                else // if signDevident == "-"
                {
                    if (signOperand2 == "+")
                        SignResult = "-";
                    else
                        SignResult = "+";
                }

                if (isStringZero(Operand1) && !isStringZero(Operand2))
                {
                    return signOperand2 + Operand2;
                }
                else
                {
                    if (!isStringZero(Operand1) && isStringZero(Operand2))
                    {
                        return signOperand1 + Operand1;
                    }
                    else
                        if (isStringZero(Operand1) && isStringZero(Operand2))
                            return "+0,0";
                }


                /*Float Part*/
                Operand1Dot = Operand1.IndexOf(",");
                Operand2Dot = Operand2.IndexOf(",");

                if (Operand1Dot != -1)
                {
                    LenFloatPartOperand1 = Operand1.Length - Operand1Dot - 1;    // -1 - ','
                    fPartOperand1 = Operand1.Substring(Operand1Dot + 1);  // copy float part Devident //  123
                    iPartOperand1 = Operand1.Substring(0, Operand1Dot);
                }
                else
                { LenFloatPartOperand1 = 1; fPartOperand1 = "0"; iPartOperand1 = Operand1; }

                if (Operand2Dot != -1)
                {
                    LenFloatPartOperand2 = Operand2.Length - Operand2Dot - 1;      // -1 - ','
                    fPartOperand2 = Operand2.Substring(Operand2Dot + 1);  // copy float part Devider // 789
                    iPartOperand2 = Operand2.Substring(0, Operand2Dot);
                }
                else
                { LenFloatPartOperand2 = 1; fPartOperand2 = "0"; iPartOperand2 = Operand2; }

                if (LenFloatPartOperand1 >= LenFloatPartOperand2)
                {
                    for (i = 0; i < LenFloatPartOperand1 - LenFloatPartOperand2; i++) // After Research Modification NEEDED !
                    {
                        fPartOperand2 += "0";
                    }
                }
                else
                {
                    for (i = 0; i < LenFloatPartOperand2 - LenFloatPartOperand1; i++) // After Research Modification NEEDED !
                    {
                        fPartOperand1 += "0";
                    }
                }

                Operand1Exp = -LenFloatPartOperand1;
                Operand2Exp = -LenFloatPartOperand2;

                iPartOperand1 += fPartOperand1; // 123123
                iPartOperand2 += fPartOperand2;   // 678678

                BigOperand1 = BigInteger.Parse(iPartOperand1);
                BigOperand2 = BigInteger.Parse(iPartOperand2);
                Result = addV2(BigOperand1, BigOperand2);
                if ((LenFloatPartOperand1 >= Result.Length) || (LenFloatPartOperand2 >= Result.Length))
                {
                    z = Result.Length;
                    for (i = 0; i < LenFloatPartOperand2 + LenFloatPartOperand1 - z + 1; i++) // After Research Modification NEEDED !
                    {
                        Result = "0" + Result;
                    }
                }
                if (LenFloatPartOperand1 > LenFloatPartOperand2)
                    Result = SignResult + Result.Insert(Result.Length - Math.Abs(LenFloatPartOperand1), ",");
                else
                    Result = SignResult + Result.Insert(Result.Length - Math.Abs(LenFloatPartOperand2), ",");
                return deleteZeroFromNumberV2(Result);
            }
            catch (Exception ex)
            { throw new FCCoreArithmeticException("Func 'Addition' = [" + ex.Message + "]"); }
        }

        private String addV2(BigInteger Operand1, BigInteger Operand2)
        {
            BigInteger Result;
            try
            {
                Result = Operand1 + Operand2;
                return Result.ToString();
            }
            catch (Exception ex)
            { throw new FCCoreArithmeticException("Func 'addV2' = [" + ex.Message + "]"); }
        }

        #endregion

        #region Multiplication Functions
        /// <summary>
        /// Multiplies two float digits.
        /// Permissions: Input Numbers can be with sign's or without
        /// Warnings: Can't be in exponential form.
        /// </summary>
        /// <param name="Multiplicand">Number to multiply</param>
        /// <param name="Factor">Number wich is multiplied</param>
        /// <returns></returns>
        public String Multiplication(String Multiplicator, String Factor)
        {
            String SignResult = "", Result = "";
            String iPartMultiplicator = "", fPartMultiplicator = "";
            String iPartFactor = "", fPartFactor = "";

            //Temporary Vars
            String signMultiplicator = "", signFactor = "";
            int LenFloatPartMultiplicator = 0, LenFloatPartFactor = 0;
            int i, z;
            int MultiplicatorDot = 0;
            int FactorDot = 0;
            int MultiplicatorExp = 0, FactorExp = 0;
            char[] chArr = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            BigInteger BigMultiplicator = 0;
            BigInteger BigFactor = 0;

            try
            {
                if (isStringZero(Multiplicator) || isStringZero(Factor))
                {
                    return "+0,0";
                }

                /* Sign Result */
                if ((Multiplicator[0] == '-') || (Multiplicator[0] == '+'))
                {
                    signMultiplicator = Multiplicator.Substring(0, 1);
                    Multiplicator = Multiplicator.Substring(1);  // Devident without sign = 123123,123
                }
                else
                    signMultiplicator = "+";

                if ((Factor[0] == '-') || (Factor[0] == '+'))
                {
                    signFactor = Factor.Substring(0, 1);
                    Factor = Factor.Substring(1);  // Devider without sign = 789789,789
                }
                else
                    signFactor = "+";

                if (signMultiplicator == "+")
                {
                    if (signFactor == "+")
                        SignResult = "+";
                    else
                        SignResult = "-";
                }
                else // if signDevident == "-"
                {
                    if (signFactor == "+")
                        SignResult = "-";
                    else
                        SignResult = "+";
                }

                /*Float Part*/
                MultiplicatorDot = Multiplicator.IndexOf(",");
                FactorDot = Factor.IndexOf(",");

                if (MultiplicatorDot != -1)
                {
                    LenFloatPartMultiplicator = Multiplicator.Length - MultiplicatorDot - 1;    // -1 - ','
                    fPartMultiplicator = Multiplicator.Substring(MultiplicatorDot + 1);  // copy float part Devident //  123
                    iPartMultiplicator = Multiplicator.Substring(0, MultiplicatorDot);
                }
                else
                { LenFloatPartMultiplicator = 1; fPartMultiplicator = "0"; iPartMultiplicator = Multiplicator; }

                if (FactorDot != -1)
                {
                    LenFloatPartFactor = Factor.Length - FactorDot - 1;      // -1 - ','
                    fPartFactor = Factor.Substring(FactorDot + 1);  // copy float part Devider // 789
                    iPartFactor = Factor.Substring(0, FactorDot);
                }
                else
                { LenFloatPartFactor = 1; fPartFactor = "0"; iPartFactor = Factor; }

                MultiplicatorExp = -LenFloatPartMultiplicator;
                FactorExp = -LenFloatPartFactor;

                iPartMultiplicator += fPartMultiplicator; // 123123
                iPartFactor += fPartFactor;   // 678678

                BigMultiplicator = BigInteger.Parse(iPartMultiplicator);
                BigFactor = BigInteger.Parse(iPartFactor);

                Result = mulV2(BigMultiplicator, BigFactor);
                if (LenFloatPartFactor + LenFloatPartMultiplicator >= Result.Length)
                {
                    z = Result.Length;
                    for (i = 0; i < LenFloatPartFactor + LenFloatPartMultiplicator - z + 1; i++)
                    {
                        Result = "0" + Result;
                    }
                }
                Result = SignResult + Result.Insert(Result.Length - Math.Abs(LenFloatPartMultiplicator + LenFloatPartFactor), ",");
                return deleteZeroFromNumberV2(Result);
            }
            catch (Exception ex)
            {
                throw new FCCoreArithmeticException("Func 'Multiplication' = [ " + ex.Message + " ]");
            }
        }

        /// <summary>
        /// Multiplicates 2 unsigned integers.
        /// </summary>
        /// <param name="Multiplicator">Multiplicator</param>
        /// <param name="Factor">Multiplication factor</param>
        /// <returns>Number in string form that repsents result of multiplication 2 unsigned integers.</returns>
        private String mulV2(BigInteger Multiplicator, BigInteger Factor)
        {
            BigInteger Result = Multiplicator * Factor;
            return Result.ToString();
        }

        /// <summary>
        /// Trims input number all useless zero characters.
        /// </summary>
        /// <param name="inStr">Input number.</param>
        /// <returns>Trimed stringlified number.</returns>
        public String deleteZeroFromNumberV2(String inStr)
        {
            String result = "";
            String sign = "";
            String LeftPart;
            String RightPart;
            if ((inStr[0] == '-') || (inStr[0] == '+'))
            {
                sign = inStr.Substring(0, 1);
                inStr = inStr.Substring(1);
            }

            if (inStr.IndexOf(",") != -1)
            {
                LeftPart = inStr.Substring(0, inStr.IndexOf(","));
                RightPart = inStr.Substring(inStr.IndexOf(",") + 1);
                if (LeftPart.Length > 1)
                    LeftPart = LeftPart.TrimStart('0');
                if (RightPart.Length > 1)
                    RightPart = RightPart.TrimEnd('0');
                if (LeftPart == "") LeftPart = "0";
                if (RightPart == "") RightPart = "0";
                result = sign + LeftPart + "," + RightPart;
            }
            else
            {
                LeftPart = inStr.Substring(0);
                result = sign + LeftPart + ",0";
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Check if the input string consist only from '1' value bits. Use this function only for 2cc numbers.
        /// </summary>
        /// <param name="inStr">Input string for check</param>
        /// <returns>True - if there none '0' symbols in input string ; False -  one or more symbols are '0'</returns>
        public bool checkStringFull(String inStr)
        {
            int i;
            for (i = 0; i < inStr.Length; i++)
            {
                if (inStr[i] == '0')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Finds if string consist only of '0'value . Use this function only for 10cc numbers.
        /// </summary>
        /// <param name="inStr">String to check</param>
        /// <returns>True - if consist; False - if there is one or more symbols alike '0'</returns>
        public bool isStringZero(String inStr)
        {
            int i;

            if (inStr != null)
            {
                for (i = 1; i < 10; i++)
                {
                    if (inStr.Contains(i.ToString()))
                        return false;
                }
                return true;
            }
            else
            {
                throw new FCCoreGeneralException("Func 'isStringZero' = [ Input String is Null. ]");
            }
        }

        /// <summary>
        /// Calculates Exponent for specified number
        /// Uses : Number.BinaryIntPart,Number.BinaryFloatPart
        /// </summary>
        /// <param name="inNumber">Number - var from which exponenta need to be taken</param>
        /// <param name="Left_Right">False - Left part og number, else - Right </param>
        /// <returns>Returns Exponent in 2cc</returns>
        public String selectExp(IFPartsOfNumber inStingNumber, NumberFormat inNumberFormat, PBNumber inObjectNumber)
        {

            int z = 0;
            int Offset = 0;
            String temp, result = "";
            String bynaryStringInt = "", bynaryStringFloat = "";
            if (inNumberFormat == 0)
            {
                bynaryStringInt = inStingNumber.IntegerPart;
                bynaryStringFloat = inStingNumber.FloatPart;
            }
            else
            {
                /*
                if (Left_Right == PartOfNumber.Left)
                {// Left part of number
                    bynaryStringInt = inNumber.BinaryIntPartFILeft;
                    bynaryStringFloat = inNumber.BinaryFloatPartFILeft;
                }
                else
                {// Right part of number
                    bynaryStringInt = inNumber.BinaryIntPartFIRight;
                    bynaryStringFloat = inNumber.BinaryFloatPartFIRight;
                }*/
            }

            if (bynaryStringInt != null)
            {
                if (bynaryStringInt != "")
                {
                    temp = bynaryStringInt + bynaryStringFloat;
                }
                else
                {
                    //inNumber.CalcStatus = Flexible_computing.CalculationStatus.Exception;
                    if (inNumberFormat == 0)
                    { inObjectNumber.NumberState = stateOfNumber.error; }
                    /*else
                        if (Left_Right == PartOfNumber.Left)
                        { inNumber.NumberState = stateOfNumber.error; }
                        else
                        { inNumber.NumberStateRight = stateOfNumber.error; }*/
                    throw new FCCoreArithmeticException("Exception in Func ['selectExp'] Mess=[ Empty String - BynaryIntPart ] (" + inObjectNumber.Name + ")");
                }
            }
            else
            {
                //inNumber.CalcStatus = Flexible_computing.CalculationStatus.Exception;

                if (inNumberFormat == 0)
                { inObjectNumber.NumberState = stateOfNumber.error; }
                /*else
                    if (Left_Right == PartOfNumber.Left)
                    { inNumber.NumberState = stateOfNumber.error; }
                    else
                    { inNumber.NumberStateRight = stateOfNumber.error; }*/
                throw new FCCoreArithmeticException("Exception in Func ['selectExp'] Mess=[ Null - BynaryIntPart ] (" + inObjectNumber.Name + ")");
            }
            try
            {
                switch (inNumberFormat)
                {
                    case 0: Offset = inObjectNumber.Offset; break;
                  /*case 1:
                    case 2: Offset = inNumber.OffsetFI; break;
                    case 3: Offset = inNumber.OffsetTetra; break;
                    case 4: Offset = inNumber.OffsetFITetra; break;*/
                }
                if (bynaryStringInt.IndexOf('1') != -1)
                {
                    temp = bynaryStringInt;
                    temp = temp.TrimStart('0');
                    z = temp.Length - (temp.IndexOf('1') + 1);

                    if (z > Offset)
                        z = Offset;
                }
                else
                    if (bynaryStringFloat.IndexOf('1') != -1)
                    {
                        temp = bynaryStringFloat;
                        temp = temp.TrimEnd('0');
                        z = (temp.IndexOf('1') + 1);
                        z *= -1;
                        if (z < -Offset)
                            z = -Offset;
                    }
                    else
                    {
                        z = -Offset;
                    }

                result = convert10to2IPart((z + Offset).ToString());
                //inNumber.Exponenta = result;
                return result;
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Exception in Func ['selectExp'] Mess=[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// Calculates Mantissa for specified number. Define Exponent before Mantissa for correct running algorithm.
        /// Uses funcs: isStringZero,convert10to2IPart, checkStringFull, sumExp
        /// Uses Vars: Number.BinaryIntPart,Number.BinaryFloatPart
        /// </summary>
        /// <param name="inNumber">Number - var from which mantissa need to be taken</param>
        /// <param name="Left_Right">False - Left part og number, else - Right </param>
        /// <returns>Returns Mantissa in 2cc</returns>
        public String selectMantissa(IFPartsOfNumber inStringNumber, NumberFormat inNumberFormat, PBNumber inObjectNumber)
        {
            int i, l, z = 0;
            int currMBits;
            String result = "";
            String M = "";
            String[] tempArray;
            int offsetDot = 1;
            String Sign;
            String bynaryStringInt = "", bynaryStringFloat = "";

            /* Sign */
            //Sign = SignCharacter;
            
            if (inNumberFormat == 0)
            {
                bynaryStringInt = inStringNumber.IntegerPart;
                bynaryStringFloat = inStringNumber.FloatPart;
            }
            else
            {
                /*
                if (Left_Right == PartOfNumber.Left)
                {// Left part of number
                    bynaryStringInt = inNumber.BinaryIntPartFILeft;
                    bynaryStringFloat = inNumber.BinaryFloatPartFILeft;
                }
                else
                {// Right part of number
                    bynaryStringInt = inNumber.BinaryIntPartFIRight;
                    bynaryStringFloat = inNumber.BinaryFloatPartFIRight;
                }*/
            }

            try
            {
                if ((bynaryStringInt != null) && (bynaryStringFloat != null))
                {
                    if ((bynaryStringInt != "") && (bynaryStringFloat != ""))
                    {
                        if (bynaryStringInt.IndexOf('1') != -1)
                        {
                            offsetDot = bynaryStringInt.IndexOf('1');
                            result = bynaryStringInt.Substring(offsetDot + 1) + bynaryStringFloat;
                        }
                        else
                            if (bynaryStringFloat.IndexOf('1') != -1)
                                if (isStringZero(inObjectNumber.Exponent))
                                    result = "" + bynaryStringFloat.Substring(inObjectNumber.Offset - 1, inObjectNumber.MantissaLenght + 1);
                                else
                                {
                                    offsetDot = bynaryStringFloat.IndexOf('1') + 1;
                                    result = "" + bynaryStringFloat.Substring(offsetDot);
                                }
                            else
                            {
                                switch (inNumberFormat)
                                {
                                    case 0: currMBits = inObjectNumber.MantissaLenght; break;
                                    /*case 1:
                                      case 2: currMBits = inNumber.MBitsFI; break;*/
                                    default: currMBits = inObjectNumber.MantissaLenght; break;
                                }
                                tempArray = new String[currMBits];
                                for (i = 0; i < currMBits; i++)
                                    tempArray[i] = "0";
                                result = result + String.Join("", tempArray);
                            }
                    }
                    else
                    {
                        throw new FCCoreArithmeticException("Exception in Func ['selectMantissa'] Mess=[ Empty String - BynaryIntPart or BynaryFloatPart  ] (" + inObjectNumber.Name + ")");
                    }
                }
                else
                {
                    throw new FCCoreArithmeticException("Exception in Func ['selectMantissa'] Mess=[ Null - BynaryIntPart or BynaryFloatPart ] (" + inObjectNumber.Name + ")");
                }

                switch (inNumberFormat)
                {
                    case 0: currMBits = inObjectNumber.MantissaLenght; break;
                    /*case 1:
                      case 2: currMBits = inNumber.MBitsFI; break;*/
                    default: currMBits = inObjectNumber.MantissaLenght; break;
                }
                if (result.Length <= inObjectNumber.MantissaLenght)
                {
                    // After Research Modification HERE NEEDED !
                    l = currMBits + 1 - result.Length;
                    tempArray = new String[l];
                    for (i = 0; i < l; i++)
                    {
                        tempArray[i] = "0";
                    }
                    result = result + String.Join("", tempArray);
                }
                switch (PCoreInst.Rounding)
                {
                    case 0:
                        M = result.Substring(0, currMBits);
                        break;
                    case 1:
                        if (isStringZero(inObjectNumber.Exponent))
                        {
                            tempArray = new String[offsetDot];
                            for (i = 0; i < offsetDot; i++)
                                tempArray[i] = "0";
                            M = M + String.Join("", tempArray);

                            M += result.Substring(0, currMBits + 1 - offsetDot);
                        }
                        else
                            M = result.Substring(0, currMBits + 0);
                        if ((result[currMBits] == '1') && (inStringNumber.Sign[0] == '+'))
                        {
                            if (!checkStringFull(M))
                            {
                                M = convert2to10IPart(M);
                                //M = sumIPart(M, "1");
                                M = Addition(M, "1");
                            }
                            else
                            {
                                M = "0";
                                if (checkStringFull(inObjectNumber.Exponent))
                                {
                                    //if (NumberFormat == 0)
                                        inObjectNumber.NumberState = stateOfNumber.NaN;
                                    //else
                                      //  inNumber.NumberStateRight = stateOfNumber.NaN;
                                }
                                else
                                {
                                    sumExp(inObjectNumber, "1", inNumberFormat);
                                }
                            }
                            M = convert10to2IPart(M);
                            if (M.Length + 1 == currMBits)
                            {
                                M = "0" + M;
                            }
                            else
                                if (M.Length < currMBits)
                                {
                                    l = currMBits - M.Length;
                                    tempArray = new String[l];
                                    for (i = 0; i < l; i++)
                                        tempArray[i] = "0";
                                    M = String.Join("", tempArray) + M;
                                }
                        }
                        // inNumber.Mantisa = M;
                        break;

                    case 2:// +Inf 
                        M = result.Substring(1, currMBits);
                        if (inStringNumber.Sign[0] == '+')
                        {
                            if (!checkStringFull(M))
                            {
                                M = convert2to10IPart(M);
                                //M = sumIPart(M, "1");
                                M = Addition(M, "1");
                            }
                            else
                            {
                                M = "0";
                                if (checkStringFull(inObjectNumber.Exponent))
                                {
                                    //if (NumberFormat == 0)
                                    inObjectNumber.NumberState = stateOfNumber.NaN;
                                    //else
                                     //   inNumber.NumberStateRight = stateOfNumber.NaN;
                                }
                                else
                                {
                                    sumExp(inObjectNumber, "1", inNumberFormat);
                                }
                            }
                            M = convert10to2IPart(M);
                        }
                        break;

                    case 3:
                        // -Inf
                        M = result.Substring(1, currMBits);
                        if (inStringNumber.Sign[0] == '-')
                        {
                            if (!checkStringFull(M))
                            {
                                M = convert2to10IPart(M);
                                M = Addition(M, "1");
                                //M = sumIPart(M, "1");
                            }
                            else
                            {
                                M = "0";
                                if (checkStringFull(inObjectNumber.Exponent))
                                {
                                   // if (NumberFormat == 0)
                                    inObjectNumber.NumberState = stateOfNumber.NaN;
                                    //else
                                      //  inNumber.NumberStateRight = stateOfNumber.NaN;
                                }
                                else
                                {
                                    sumExp(inObjectNumber, "1", inNumberFormat);
                                }
                            }
                            M = convert10to2IPart(M);
                        }
                        break;
                }

                return M;
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Exception in Func ['selectMantissa'] Mess=[" + ex.Message + "]");
            }
        }

        public void sumExp(PBNumber inNumber, String inStr,NumberFormat inNumberFormat)
        {
            String E;
            int iE, Offset = 0;
            E = inNumber.Exponent; // It can be Fraction or Interval
            switch (inNumberFormat)
            {
                case 0: Offset = inNumber.Offset; break;
              /*case 1:
                case 2: Offset = inNumber.OffsetFI; break;
                case 3: Offset = inNumber.OffsetTetra; break;
                case 4: Offset = inNumber.OffsetFITetra; break;*/
            }
            E = convert2to10IPart(E);
            iE = int.Parse(E) - Offset;
            // Check if inStr > E.Max


            iE += int.Parse(inStr); // This Addition is UNSECURE !
            // Check if iE > E.Max
            E = convert10to2IPart((iE + Offset).ToString());
            inNumber.Exponent = E;
        }
    }
}

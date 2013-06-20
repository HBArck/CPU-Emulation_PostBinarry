using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using PostBinary.Classes;
namespace PostBinary.Classes.PostBinary
{
    class PBConvertion
    {


        #region Convertion Functoins


        public String NormalizeNumber(String dataString, int inAccuracy, IPBNumber.NumberFormat inNumberFormat)
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
        public IPBNumber.IFPartsOfNumber DenormalizeNumber(String dataString, IPBNumber.NumberFormat inNumberFormat)
        {
            String denormNumber = "";
            String denormIntPart = "", denormFloatPart = "";
            String ExpSign, Sign = "0", SignCharacter = "+";
            String E;
            String[] tempArray;
            IPBNumber.IFPartsOfNumber returnValue = new IPBNumber.IFPartsOfNumber();
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
        public String convert10to2IPart(String inString)
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
                throw new PBArithmeticException("Func 'convert2to10IPart' = [ " + ex.Message + " ]");
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
            { throw new PBArithmeticException("Func 'Addition' = [" + ex.Message + "]"); }
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
            { throw new PBArithmeticException("Func 'addV2' = [" + ex.Message + "]"); }
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
        private String Multiplication(String Multiplicator, String Factor)
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
                throw new PBArithmeticException("Func 'Multiplication' = [ " + ex.Message + " ]");
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
        private String deleteZeroFromNumberV2(String inStr)
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
                throw new PBGeneralException("Func 'isStringZero' = [ Input String is Null. ]");
            }
        }

        /// <summary>
        /// Calculates number offset depending on number capacity.
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="inCapacity"></param>
        /// <returns></returns>
        public IPBNumber.NumberOffset getNumberOffset(IPBNumber.NumberCapacity inCapacity)
        {
            switch (inCapacity)
            {
                case IPBNumber.NumberCapacity.PB32:
                    {
                        return IPBNumber.NumberOffset.PB32;
                    }
                case IPBNumber.NumberCapacity.PB64:
                    {
                        return IPBNumber.NumberOffset.PB64;
                    }
                case IPBNumber.NumberCapacity.PB128:
                    {
                        return IPBNumber.NumberOffset.PB128;
                    }
                case IPBNumber.NumberCapacity.PB256:
                    {
                        return IPBNumber.NumberOffset.PB256;
                    }
            }
            return IPBNumber.NumberOffset.PB128;
        }

        /// <summary>
        /// Calculates number mantissa length depending on number capacity.
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="inCapacity"></param>
        /// <returns></returns>
        public IPBNumber.NumberMantissaLength getNumberMantissaLength(IPBNumber.NumberCapacity inCapacity)
        {
            switch (inCapacity)
            {
                case IPBNumber.NumberCapacity.PB32:
                    {
                        return IPBNumber.NumberMantissaLength.PB32;
                    }
                case IPBNumber.NumberCapacity.PB64:
                    {
                        return IPBNumber.NumberMantissaLength.PB64;
                    }
                case IPBNumber.NumberCapacity.PB128:
                    {
                        return IPBNumber.NumberMantissaLength.PB128;
                    }
                case IPBNumber.NumberCapacity.PB256:
                    {
                        return IPBNumber.NumberMantissaLength.PB256;
                    }
            }
            return IPBNumber.NumberMantissaLength.PB128;
        }
        /// <summary>
        /// Gets empty mantissa depending on number capacity.
        /// </summary>
        /// <param name="inCapacity"></param>
        /// <returns></returns>
        public String getEmptyMantissa(IPBNumber.NumberCapacity inCapacity)
        {
            switch (inCapacity)
            {
                case IPBNumber.NumberCapacity.PB32:
                    {
                        return IPBNumber.EmptyMantissa[(int)IPBNumber.NumberFormatCode.PB32];
                    }
                case IPBNumber.NumberCapacity.PB64:
                    {
                        return IPBNumber.EmptyMantissa[(int)IPBNumber.NumberFormatCode.PB64];
                    }
                case IPBNumber.NumberCapacity.PB128:
                    {
                        return IPBNumber.EmptyMantissa[(int)IPBNumber.NumberFormatCode.PB128];
                    }
                case IPBNumber.NumberCapacity.PB256:
                    {
                        return IPBNumber.EmptyMantissa[(int)IPBNumber.NumberFormatCode.PB256];
                    }
            }
            return IPBNumber.EmptyMantissa[(int)IPBNumber.NumberFormatCode.PB128];
        }

        /// <summary>
        /// Adds number to exponent.
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="inNumber"></param>
        /// <param name="inStr"></param>
        /// <param name="inCapacity"></param>
        public void sumExp(String Exponent, String inStr, IPBNumber.NumberCapacity inCapacity)
        {
            String E;
            int iE, Offset = 0;
            E = Exponent; // It can be Fraction or Interval

            Offset = (int)getNumberOffset(inCapacity);
            /*switch (inNumberFormat)
            {
                case 0: Offset = (int)inNumber.Offset; break;
                case 1:
                case 2: Offset = inNumber.OffsetFI; break;
                case 3: Offset = inNumber.OffsetTetra; break;
                case 4: Offset = inNumber.OffsetFITetra; break;
            }*/
            E = convert2to10IPart(E);
            iE = int.Parse(E) - Offset;
            // Check if inStr > E.Max


            iE += int.Parse(inStr); // This Addition is UNSECURE !
            // Check if iE > E.Max
            E = convert10to2IPart((iE + Offset).ToString());
            Exponent = E;
        }
        #endregion


        #region From PostBinary to 10cc

        /// <summary>
        /// Function converts postbinary normilized number to number in 10cc
        /// </summary>
        /// <param name="inNumber">PostBinary number.</param>
        /// <param name="precision"></param>
        public String calcResForNorm(PBNumber inNumber, int precision)
        {
            String binIPartOut, binFPartOut, Sign;
            String[] tempArray;
            String M, E;
            String CorrectResult = "", CorrectResult2cc;
            try
            {
                E = inNumber.Exponent;
                M = inNumber.Mantissa;

                M = "1" + M;
                E = convert2to10IPart(E);
                int iE = int.Parse(E) - (int)inNumber.Offset;
                if (iE > M.Length)
                {
                    tempArray = new String[Math.Abs(iE) + 1];
                    for (int i = 0; i <= iE; i++)
                        tempArray[i] = "0";
                    M = M + String.Join("", tempArray);

                }
                if (iE >= 0)
                {
                    if (iE + 1 <= M.Length)
                    {
                        binIPartOut = M.Substring(0, iE + 1);
                        //binFPartOut = "0" + M.Substring(iE + 1);
                        binFPartOut = M.Substring(iE + 1);
                    }
                    else
                    {
                        int temp = M.Length;
                        binIPartOut = M.Substring(0, temp);
                        binFPartOut = M.Substring(temp);
                    }
                }
                else
                {
                    // After Research
                    int max = 0;
                    tempArray = new String[Math.Abs(iE) + 1];
                    for (int i = 1; i < Math.Abs(iE); i++) //for (int i = -1; i > iE; i--)
                    {
                        tempArray[max] = "0";
                        max++;
                    }
                    if (max > 0)
                        M = String.Join("", tempArray) + M;
                    //else
                    //    M = "0" + M;

                    binIPartOut = "0";
                    binFPartOut = M;
                }

                /* Sign */
                Sign = inNumber.Sign;
                /*if ((z == 0) || (NumberFormat == 0))
                    Sign = SignCharacterLeft;
                else
                    Sign = SignCharacterRight;
                */
                IPBNumber.NumberFormat NumberFormat = IPBNumber.NumberFormat.INTEGER;
                switch (NumberFormat)
                {
                    case 0:
                        CorrectResult = Sign + convert2to10IPart(binIPartOut) + "," + convert2to10FPart(binFPartOut, precision);
                        CorrectResult2cc = Sign + binIPartOut + "," + binFPartOut;
                        break;
                    /*
                case 1:
                    if (z == 0)
                    {
                        inNumber.CorrectResultFractionL = Sign + convert2to10IPart(binIPartOut) + "," + convert2to10FPart(binFPartOut, precision);
                        inNumber.CorrectResultFraction2ccL = Sign + binIPartOut + "," + binFPartOut;
                    }
                    else
                    {
                        inNumber.CorrectResultFractionR = Sign + convert2to10IPart(binIPartOut) + "," + convert2to10FPart(binFPartOut, precision);
                        inNumber.CorrectResultFraction2ccR = Sign + binIPartOut + "," + binFPartOut;
                    }
                    break;
                case 2:
                    if (z == 0)
                    {
                        inNumber.CorrectResultIntervalL = Sign + convert2to10IPart(binIPartOut) + "," + convert2to10FPart(binFPartOut, precision);
                        inNumber.CorrectResultInterval2ccL = Sign + binIPartOut + "," + binFPartOut;
                    }
                    else
                    {
                        inNumber.CorrectResultIntervalR = Sign + convert2to10IPart(binIPartOut) + "," + convert2to10FPart(binFPartOut, precision);
                        inNumber.CorrectResultInterval2ccR = Sign + binIPartOut + "," + binFPartOut;
                    }
                    break;*/
                }
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Func 'calcResForNorm' = [ " + ex.Message + " ]");
            }

            CorrectResult = deleteNonSignificantBits(CorrectResult);
            return CorrectResult;
            //break;
        }
        public void calcResForDenorm(PBNumber inNumber, int precision)
        {
            String[] tempArray;
            String Sign;
            String E, M;
            String CorrectResult, CorrectResult2cc;
            int Offset = (int)inNumber.Offset;
            try
            {
                E = inNumber.Exponent;
                M = inNumber.Mantissa;
                tempArray = new String[Math.Abs(Offset) + 1];
                for (int i = 1; i <= Offset; i++)
                    tempArray[i] = "0";
                M = String.Join("", tempArray) + M;

                /* Sign */
                Sign = inNumber.Sign;
                /*if ((z == 0) || (NumberFormat == 0))
                    Sign = SignCharacterLeft;
                else
                    Sign = SignCharacterRight;
                */
                IPBNumber.NumberFormat NumberFormat = IPBNumber.NumberFormat.INTEGER;
                switch (NumberFormat)
                {
                    case IPBNumber.NumberFormat.INTEGER:
                        CorrectResult = Sign + "0," + convert2to10FPart(M, precision);
                        CorrectResult2cc = Sign + "0," + M;
                        break;
                    /*
                     * case 1:
                        if (z == 0)
                        {
                            inNumber.CorrectResultFractionL = Sign + "0," + convert2to10FPart(M, precision);
                            inNumber.CorrectResultFraction2ccL = Sign + "0," + M;
                        }
                        else
                        {
                            inNumber.CorrectResultFractionR = Sign + "0," + convert2to10FPart(M, precision);
                            inNumber.CorrectResultFraction2ccR = Sign + "0," + M;
                        }
                        break;
                    case 2:
                        if (z == 0)
                        {
                            inNumber.CorrectResultIntervalL = Sign + "0," + convert2to10FPart(M, precision);
                            inNumber.CorrectResultInterval2ccL = Sign + "0," + M;
                        }
                        else
                        {
                            inNumber.CorrectResultIntervalR = Sign + "0," + convert2to10FPart(M, precision);
                            inNumber.CorrectResultInterval2ccR = Sign + "0," + M;
                        }
                        break;
                     */
                }
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Func 'calcResForDenorm' = [ " + ex.Message + " ]");
            }
        }
        public void calcResForZero(PBNumber inNumber)
        {
            try
            {
                /*
                String Sign;
                switch (inNumber.Name)
                {
                    case "Num32":
                        Num32.Exponenta = "00000000";
                        Num32.Mantisa = "000000000000000000000"; // 21
                        break;
                    case "Num64":
                        if (z == 0)
                        {
                            Num64.Exponenta = Format == 0 ? "00000000000" : "00000000";
                            Num64.Mantisa = Format == 0 ? "000000000000000000000000000000000000000000000000" : "000000000000000000000"; // 48 - 21
                        }
                        else
                        {
                            Num64.ExponentaRight = Format == 0 ? "00000000000" : "00000000";
                            Num64.MantisaRight = Format == 0 ? "000000000000000000000000000000000000000000000000" : "000000000000000000000"; // 48 - 21
                        }
                        break;
                    case "Num128":
                        if (z == 0)
                        {
                            Num128.Exponenta = Format == 0 ? "000000000000000" : "00000000000";
                            Num128.Mantisa = Format == 0 ? "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" : "000000000000000000000000000000000000000000000000";  // 104 - 48
                        }
                        else
                        {
                            Num128.ExponentaRight = Format == 0 ? "000000000000000" : "00000000000";
                            Num128.MantisaRight = Format == 0 ? "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" : "000000000000000000000000000000000000000000000000";  // 104 - 48
                        }
                        break;
                    case "Num256":
                        if (z == 0)
                        {
                            Num256.Exponenta = Format == 0 ? "00000000000000000000" : "000000000000000";
                            Num256.Mantisa = Format == 0 ? "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" : "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"; // 219 - 104
                        }
                        else
                        {
                            Num256.ExponentaRight = Format == 0 ? "00000000000000000000" : "000000000000000";
                            Num256.MantisaRight = Format == 0 ? "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" : "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"; // 219 - 104
                        }
                        break;
                }

                /* Sign */
                /* if ((z == 0) || (NumberFormat == 0))
                     Sign = SignCharacterLeft;
                 else
                     Sign = SignCharacterRight;

                 if (NumberFormat == 0)
                 {
                     inNumber.CorrectResult = Sign + "0,0";
                     inNumber.CorrectResultExp = Sign + "0,0";
                     inNumber.CorrectResult2cc = Sign + "0,0";
                     inNumber.CorrectResult2ccExp = Sign + "0,0";
                     inNumber.Error = Sign + "0,0";
                 }

                 if (cycle == 2 && NumberFormat == 1)
                 {
                     if (z == 0)
                     {
                         inNumber.CorrectResultFractionL = Sign + "0,0";
                         inNumber.CorrectResultFractionExpL = Sign + "0,0";
                         inNumber.CorrectResultFraction2ccL = Sign + "0,0";
                         inNumber.CorrectResultFraction2ccExpL = Sign + "0,0";
                         inNumber.ErrorFractionLeft = Sign + "0,0";
                     }
                     if (z == 1)
                     {
                         inNumber.CorrectResultFractionR = Sign + "0,0";
                         inNumber.CorrectResultFractionExpR = Sign + "0,0";
                         inNumber.CorrectResultFraction2ccR = Sign + "0,0";
                         inNumber.CorrectResultFraction2ccExpR = Sign + "0,0";
                         inNumber.ErrorIntervalRight = Sign + "0,0";
                     }
                 }

                 if (cycle == 2 && NumberFormat == 2)
                 {
                     if (z == 0)
                     {
                         inNumber.CorrectResultIntervalL = Sign + "0,0";
                         inNumber.CorrectResultIntervalExpL = Sign + "0,0";
                         inNumber.CorrectResultInterval2ccL = Sign + "0,0";
                         inNumber.CorrectResultInterval2ccExpL = Sign + "0,0";
                         inNumber.ErrorIntervalLeft = Sign + "0,0";
                     }
                     if (z == 1)
                     {
                         inNumber.CorrectResultIntervalR = Sign + "0,0";
                         inNumber.CorrectResultIntervalExpR = Sign + "0,0";
                         inNumber.CorrectResultInterval2ccR = Sign + "0,0";
                         inNumber.CorrectResultInterval2ccExpR = Sign + "0,0";
                         inNumber.ErrorIntervalRight = Sign + "0,0";
                     }
                 }*/
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Func 'calcResForZero' = [ " + ex.Message + " ]");
            }
        }
        public void calcResForInf(PBNumber inNumber, int precision)
        {
            String Sign;
            try
            {
                /* Sign */
                /*
                if ((z == 0) || (NumberFormat == 0))
                    Sign = SignCharacterLeft;
                else
                    Sign = SignCharacterRight;
                inNumber.CorrectResult = DenormalizedNumber;
                inNumber.CorrectResultExp = NormalizedNumber;

                inNumber.CorrectResult2cc = Sign + convert10to2IPart(DenormIntPart) + "," + convert10to2FPart(DenormFloatPart);
                inNumber.CorrectResult2ccExp = Sign + convertToExp(convert10to2IPart(DenormIntPart) + "," + convert10to2FPart(DenormFloatPart));

                inNumber.Error = Sign + "0,0";
                */
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Func 'calcResForInf' = [ " + ex.Message + " ]");
            }

        }
        public void calcResForNan(PBNumber inNumber)
        {
            try
            {
                /* 
                if (NumberFormat == 0)
                {
                    inNumber.CorrectResult = "Невозможно представить в данном формате";
                    inNumber.CorrectResultExp = "Невозможно представить в данном формате";
                    inNumber.CorrectResult2cc = "Невозможно представить в данном формате";
                    inNumber.CorrectResult2ccExp = "Невозможно представить в данном формате";
                    inNumber.Error = "Невозможно представить в данном формате";
                }

                if (cycle == 2 && NumberFormat == 1)
                {
                    if (z == 0)
                    {
                        inNumber.CorrectResultFractionL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFractionExpL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFraction2ccL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFraction2ccExpL = "Невозможно представить в данном формате";
                        inNumber.ErrorFractionLeft = "Невозможно представить в данном формате";
                    }
                    if (z == 1)
                    {
                        inNumber.CorrectResultFractionR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFractionExpR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFraction2ccR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultFraction2ccExpR = "Невозможно представить в данном формате";
                        inNumber.ErrorFractionRight = "Невозможно представить в данном формате";
                    }
                }


                if (cycle == 2 && NumberFormat == 2)
                {
                    if (z == 0)
                    {
                        inNumber.CorrectResultIntervalL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultIntervalExpL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultInterval2ccL = "Невозможно представить в данном формате";
                        inNumber.CorrectResultInterval2ccExpL = "Невозможно представить в данном формате";
                        inNumber.ErrorIntervalLeft = "Невозможно представить в данном формате";
                    }
                    if (z == 1)
                    {
                        inNumber.CorrectResultIntervalR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultIntervalExpR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultInterval2ccR = "Невозможно представить в данном формате";
                        inNumber.CorrectResultInterval2ccExpR = "Невозможно представить в данном формате";
                        inNumber.ErrorIntervalRight = "Невозможно представить в данном формате";
                    }
                }*/
            }
            catch (Exception ex)
            {
                throw new FCCoreFunctionException("Func 'calcResForNan' = [ " + ex.Message + " ]");
            }
        }

        public String convert2to10FPart(String inString, int Precision)
        {
            /*
             * Перевод дробной части числа из 2 с/с в 10 с/с
             */
            String result = "0";
            String divider = "0,5"; // делитель=степени 2

            if (isStringZero(inString))
            {
                return "0";
            }
            try
            {
                for (int i = 0; i < inString.Length; i++)
                {
                    if (inString[i] == '1')
                    {
                        divider = DevideBy2(i);
                        result = Addition(result, divider);
                    }
                    // divider = Devision(divider, "2", Precision);
                }
                return result.Substring(result.IndexOf(',') + 1, result.Length - result.IndexOf(',') - 1);
            }
            catch (Exception ex)
            {
                throw new FCCoreArithmeticException("Func 'convert2to10FPart' = [ " + ex.Message + " ]");
            }
        }

        public static String DevideBy2(int Degree)
        {
            BigInteger Devident = 1;
            String Result;
            int Exp = 0;// Onle negative value from -1 to -Inf
            String[] temp;
            int i;
            for (i = 0; i <= Degree; i++)
            {
                Devident *= 5;
                if ((Devident.ToString()[0] == '1') && (i + 1 <= Degree))
                {
                    Exp++;
                }
            }

            temp = new String[Exp];
            for (i = 0; i < Exp; i++)
                temp[i] = "0";
            Result = String.Join("", temp) + Devident.ToString();
            //if (Degree != 0)
            return "+0," + Result;
            //else
            //    return "+" + Result;
        }
        public String convertToExp(String inputStr)
        {
            String currentValue;
            String Exp = "";
            String signExp;
            try
            {

                if (inputStr.IndexOf('e') != -1)
                {
                    currentValue = inputStr;
                    Exp = inputStr.Substring(inputStr.IndexOf('e') + 1);
                    inputStr = inputStr.Substring(0, inputStr.IndexOf('e'));
                }
                if ((inputStr[0] != '-') && (inputStr[0] != '+'))
                    inputStr = "+" + inputStr;

                if (isStringZero(inputStr) == true)
                    return inputStr.Substring(1, 3);

                String outString = "";
                String signTemp = "+";

                if ((inputStr[1] == '0') && (inputStr[2] == ','))
                {

                    int offset = 0;
                    for (int i = 3; i < inputStr.Length; i++)
                        if (inputStr[i] != '0')
                        {
                            offset = i;
                            break;
                        }
                    if (inputStr.Length == offset + 1)
                        inputStr += "0";

                    String temp1, temp2, temp3;
                    temp1 = inputStr.Substring(offset, 1);
                    temp2 = inputStr.Substring(offset + 1);
                    temp3 = (offset - 2).ToString();
                    //outString = signTemp + temp1 + ","+ temp2 +"e-"+ temp3;
                    if (Exp == "")
                    {
                        if (int.Parse(temp3) > 0)
                            signExp = "-";
                        else
                            signExp = "+";
                        outString = temp1 + "," + temp2 + "e" + signExp + temp3;
                    }
                    else
                    {
                        int res = int.Parse("-" + temp3) + int.Parse(Exp);
                        if (res >= 0)
                            outString = temp1 + "," + temp2 + "e+" + res.ToString();
                        else
                            outString = temp1 + "," + temp2 + "e" + res.ToString();
                    }
                }
                else
                {
                    int offset = inputStr.IndexOf(',') - 2;
                    if (Exp != "")
                        offset += int.Parse(Exp);
                    if (offset >= 0)
                        signExp = "+";
                    else
                        signExp = "";
                    inputStr = inputStr.Replace(",", "");
                    outString = inputStr.Substring(0, 2) + "," + inputStr.Substring(2) + "e" + signExp + offset.ToString();
                    outString = outString.Substring(1, outString.Length - 1);
                }
                return deleteNonSignificantBits(outString);
            }
            catch (Exception ex)
            {
                throw new FCCoreArithmeticException("Func 'convertToExp' = [ " + ex.Message + " ]");
            }
        }
        #endregion

        //TODO: move to correct file for deleteNonSignificantBits
        /// <summary>
        /// Gets string without non-significant bits
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        private String deleteNonSignificantBits(String inString)
        {
            int indexDot = inString.IndexOf(',');

            int firstZero = inString.IndexOf('0');
            while ((firstZero == 0) && (firstZero < indexDot))
            {
                inString = inString.Substring(1);
                firstZero = inString.IndexOf('0');
            }
            
            int lastZero = inString.LastIndexOf('0');
            while ((lastZero == inString.Length - 1) && (lastZero > indexDot))
            {
                inString = inString.Substring(0, inString.Length - 1);
                lastZero = inString.LastIndexOf('0');
            }
            return inString;
        }
    }
}

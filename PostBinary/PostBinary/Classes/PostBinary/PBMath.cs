using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes.PostBinary
{
    public class PBMath
    {

        #region Fileds
        #endregion
        #region Constructors
        #endregion
        #region Functions
        /// <summary>
        /// Postbinary numeric addition
        /// </summary>
        /// <param name="leftOperand">Left operand of operation.</param>
        /// <param name="rightOperand">Right operand of operation.</param>
        /// <returns>Result of operation</returns>
        public PBNumber ADD(PBNumber leftOperand, PBNumber rightOperand)
        {
            PBNumber opA = (PBNumber)leftOperand.Clone();
            PBNumber opB = (PBNumber)rightOperand.Clone();
            PBNumber opC = new PBNumber("0", IPBNumber.NumberCapacity.PB128, IPBNumber.RoundingType.POST_BINARY);

            String iuA = "";
            String iuB = "";
            switch (CMP(opA.Exponent, opB.Exponent)) // Exponent align
            {
                case 1:
                    opB = ExponentAlign(opA, opB);
                    // log here A, B
                    iuA = "1";
                    iuB = "";
                    break;
                case -1:
                    opA = ExponentAlign(opB, opA);
                    // log here A, B
                    iuA = "";
                    iuB = "1";
                    break;
                case 0:
                default:
                    // log here A, B
                    break;
            }

            PBConvertion pbconvertion = new PBConvertion();
            int a = Int32.Parse(pbconvertion.convert2to10IPart(opA.Exponent));

            String str = ADDTetra(iuA + opA.Mantissa, iuB + opB.Mantissa, false);
            int iuPosition = str.IndexOf('1');
            a -= iuPosition;
            str = str.Substring(iuPosition);
           
            opC.Mantissa = str.Substring(1);
            
            opC.Exponent = pbconvertion.convert10to2IPart(a.ToString());
            // log here C
            return opC;
        }
        
        /// <summary>
        /// Returns aligned rightOperand relative to leftOperand.
        /// IMPORTANT: leftOperand must be greater or equal (>=) to rightOperand
        /// </summary>
        /// <param name="leftOperand">Operand, relative to which rightOperand will be aligned.</param>
        /// <param name="rightOperand">Operand, that will be aligned</param>
        /// <returns>Aligned operand.</returns>
        public PBNumber ExponentAlign(PBNumber leftOperand, PBNumber rightOperand)
        {
            PBConvertion pbconvertion = new PBConvertion();
            int leftExponentValue = Int32.Parse(pbconvertion.convert2to10IPart(leftOperand.Exponent));
            int rightExponentValue = Int32.Parse(pbconvertion.convert2to10IPart(rightOperand.Exponent));

            return Shift(rightOperand, leftExponentValue - rightExponentValue);
        }
        /// <summary>
        /// Shifts PBNumber number with regard to shiftValue sign(shift direction) and modulus(counts of shifts).
        /// </summary>
        /// <param name="operand">PBNumber to shift.</param>
        /// <param name="shiftValue">Sign shows direction of shift; Modulus shows counts of shifts.</param>
        /// <returns>Shifted PBNumber</returns>
        public static PBNumber Shift(PBNumber operand, int shiftValue)
        {
            PBConvertion pbconvertion = new PBConvertion();
            int expA = Int32.Parse(pbconvertion.convert2to10IPart(operand.Exponent));
            expA += shiftValue;
            IPBNumber.IFPartsOfNumber ipbn = new IPBNumber.IFPartsOfNumber();
            if (shiftValue != 0)
            {
                if (shiftValue > 0)
                {
                    String newMantissa = AddSymbols("0", "1" + operand.Mantissa, shiftValue - 1, true);
                    operand.Mantissa = newMantissa;
                }
                else
                {
                    String precipitated = operand.Mantissa.Substring(operand.Mantissa.Length + shiftValue);
                    operand.Mantissa = AddSymbols("0", operand.Mantissa, Math.Abs(shiftValue), true);
                    if (precipitated.Length >= 1)
                    {
                        operand.Round(IPBNumber.RoundingType.POST_BINARY, operand.Mantissa, 104, ipbn, 0, IPBNumber.NumberCapacity.PB128);
                    }
                }
            }
            operand.Exponent = pbconvertion.convert10to2IPart(expA.ToString());
            return operand;
        }
        /// <summary>
        /// Adds number of symbols to input string. 
        /// </summary>
        /// <param name="symbol">Symbol to add.</param>
        /// <param name="str">Input string.</param>
        /// <param name="number">Quantity of symbols to add.</param>
        /// <param name="insertPosition">Insert posotion for symbols. True - insert at the beginning; False - insert at the end.</param>
        /// <returns>Result string with inserted symbols.</returns>
        public static String AddSymbols(String symbol, String str, int number, bool insertPosition)
        {
            if (insertPosition)
            {
                for (int i = number; i > 0; i--)
                    str = symbol + str;
            }
            else
            {
                for (int i = number; i > 0; i--)
                    str = str + symbol;
            }
            return str;
        }

        /// <summary>
        /// Compares input operands.
        /// </summary>
        /// <param name="leftOperand">Operand to compare.</param>
        /// <param name="rightOperand">Operand to compare.</param>
        /// <returns> -1 - leftOperand less (<) then rightOperand; 0 - leftOperand equal (=) to rightOperand; 1 - leftOperand greater (>) then rightOperand. </returns>
        public static int CMP(String leftOperand, String rightOperand)
        {
            PBConvertion pbconvertion = new PBConvertion();
            if ((leftOperand.Length > 31) || (rightOperand.Length > 31))
                throw new ArithmeticException("Length of operands should not be longer, than 31 bits");
            int a = Int32.Parse(pbconvertion.convert2to10IPart(leftOperand));
            int b = Int32.Parse(pbconvertion.convert2to10IPart(rightOperand));

            int result = 0;
            if (a > b)
                result = 1;
            else if (a < b)
                result = -1;

            return result;
        }

        /// <summary>
        /// Adds tetralogic numbers.
        /// IMPORTANT: Input operands must be tetralogic number.
        /// </summary>
        /// <param name="leftOperand">Input operand. Tetralogic number.</param>
        /// <param name="rightOperand">Input operand. Tetralogic number.</param>
        /// <param name="carryFlag">Flag that define carring. True - conside carry; False - don't conside carry.</param>
        /// <returns>Sum of tetra addition.</returns>
        public static String ADDTetra(String opA, String opB, bool carryFlag)
        {
            String opC = "";	// result string

            //if one of operand is empty - return empty string
            if (opA == "" || opB == "") return opC;

            // Rounding strings A and B
            int Al = opA.Length, Bl = opB.Length; // fill strings lenght
            if (opA.Length > opB.Length) for (int i = 0; i < Al - Bl; i++) opB = opB.Insert(0, "0");
            else if (opA.Length != opB.Length) for (int i = 0; i < Bl - Al; i++) opA = opA.Insert(0, "0");

            Char buf = '0';  // carry for sum

            // Add start
            for (int i = opA.Length - 1; i >= 0; i--)
            {
                switch (opA[i])
                {
                    case '0':
                        switch (buf)
                        {
                            case '0':
                                opC = opC.Insert(0, opB[i].ToString());
                                break;
                            case '1':
                                buf = opB[i];
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "1");
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "0");
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "M");
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "A");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'A':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "A");
                                        buf = '0';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "M");
                                        buf = 'A';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "0");
                                        buf = 'A';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "1");
                                        buf = '0';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'M':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "M");
                                        buf = '0';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "A");
                                        buf = 'M';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "1");
                                        buf = '0';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "0");
                                        buf = 'M';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case '1':
                        switch (buf)
                        {
                            case '0':
                                buf = opB[i];
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "1");
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "0");
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "M");
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "A");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case '1':
                                opC = opC.Insert(0, opB[i].ToString());
                                break;
                            case 'A':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "M");
                                        buf = 'A';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "A");
                                        buf = '1';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "1");
                                        buf = 'A';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "0");
                                        buf = '1';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'M':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "A");
                                        buf = 'M';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "M");
                                        buf = '1';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "0");
                                        buf = '1';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "1");
                                        buf = 'M';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                        break;

                    case 'A':
                        switch (buf)
                        {
                            case '0':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "A");
                                        buf = '0';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "M");
                                        buf = 'A';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "0");
                                        buf = 'A';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "1");
                                        buf = '0';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case '1':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "M");
                                        buf = 'A';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "A");
                                        buf = '1';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "1");
                                        buf = 'A';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "0");
                                        buf = '1';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'A':
                                opC = opC.Insert(0, opB[i].ToString());
                                break;
                            case 'M':
                                buf = opB[i];
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "1");
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "0");
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "M");
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "A");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        switch (buf)
                        {
                            case '0':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "M");
                                        buf = '0';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "A");
                                        buf = 'M';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "1");
                                        buf = '0';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "0");
                                        buf = 'M';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case '1':
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "A");
                                        buf = 'M';
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "M");
                                        buf = '1';
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "0");
                                        buf = '1';
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "1");
                                        buf = 'M';
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 'A':
                                buf = opB[i];
                                switch (opB[i])
                                {
                                    case '0':
                                        opC = opC.Insert(0, "1");
                                        break;
                                    case '1':
                                        opC = opC.Insert(0, "0");
                                        break;
                                    case 'A':
                                        opC = opC.Insert(0, "M");
                                        break;
                                    case 'M':
                                        opC = opC.Insert(0, "A");
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case 'M':
                                opC = opC.Insert(0, opB[i].ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }

            if (buf != '0' && carryFlag) opC = opC.Insert(0, buf.ToString()); // conside carry

            // IMPORTANT: never delete insignificant zeros in mantissa's addition

            return opC;
        }

        /// <summary>
        /// Postbinary numeric substraction
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <param name="rightOperand"></param>
        /// <returns></returns>
        public PBNumber SUB(PBNumber leftOperand, PBNumber rightOperand)
        {
            return null;
        }

        /// <summary>
        /// Postbinary numeric multiplication
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <param name="rightOperand"></param>
        /// <returns></returns>
        public PBNumber MUL(PBNumber leftOperand, PBNumber rightOperand)
        {
            return null;
        }

        /// <summary>
        /// Postbinary numeric division
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <param name="rightOperand"></param>
        /// <returns></returns>
        public PBNumber DIV(PBNumber leftOperand, PBNumber rightOperand)
        {
            return null;
        }

        /// <summary>
        /// Postbinary numeric negative
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <returns></returns>
        public PBNumber NEG(PBNumber leftOperand)
        {
            leftOperand.Sign = leftOperand.Sign == "0" ? "1" : "0";
            return leftOperand;
        }

        #endregion
    }
}

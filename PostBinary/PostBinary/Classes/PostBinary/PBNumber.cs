using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PostBinary.Classes.Utils;

namespace PostBinary.Classes.PostBinary
{
    public struct PBPrototype
    {
        public String Sign;
        public String Exponent;
        public String Mantissa;
    }
    public class PBNumber : IPBNumber, ICloneable
    {

        #region Class Properties
        public String InitValue = "";
        private String name;
        public String Name
        {
            get { return name; }
            /*Read only*/
            set { }
        }

        private stateOfNumber numberState;
        public stateOfNumber NumberState
        {
            get { return numberState; }
            set { numberState = value; }
        }

        private NumberCapacity width;
        public NumberCapacity Width
        {
            get { return width; }
            set { width = value; }
        }

        private NumberExponentLength exponentLenght;
        public NumberExponentLength ExponentLenght
        {
            get { return exponentLenght; }
            /*Read only*/
            set { }
        }

        private NumberMantissaLength mantissaLenght;
        public NumberMantissaLength MantissaLenght
        {
            get { return mantissaLenght; }
            /*Read only*/
            set { }
        }

        private NumberOffset offset;
        public NumberOffset Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private RoundingType roundingType = RoundingType.NEAR_INTEGER;

        private PBConvertion pbConvertion;
        #endregion

        #region Class Fields

        private String sign;
        public String Sign
        {
            get { return sign; }
            set
            {
                if ((value == "1") || (value == "0"))
                    sign = value;
                else
                    throw new IncorrectSignException("Base Number: Sign should be only '0' and '1' symbols");
            }
        }
        public String signCharacter
        {
            get { return sign == "0" ? "+" : "-"; }
            set
            {
                sign = value == "+" ? "0" : "1";
            }
        }
        private String exponent;
        public String Exponent
        {
            get { return exponent; }
            set
            {
                if (value.Length == (int)exponentLenght)
                {
                    exponent = value;
                }
                else
                {
                    if (value.Length < (int)exponentLenght)
                    {
                        exponent = value;
                        while (exponent.Length < (int)exponentLenght)
                            exponent = "0" + exponent;
                    }
                    else
                    {
                        exponent = value.Substring(0, (int)exponentLenght);
                        NumberState = stateOfNumber.NAN;
                    }
                };
            }
        }
        
        private String mantissa;
        public String Mantissa
        {
            get { return mantissa; }
            set
            {
                if (value.Length == (int)mantissaLenght)
                {
                    mantissa = value;
                }
                else
                {
                    if (value.Length < (int)mantissaLenght)
                    {
                        mantissa = value;
                        while (mantissa.Length < (int)mantissaLenght)
                            mantissa = "0" + mantissa;
                    }
                    else
                    {
                        mantissa = value.Substring(0, (int)mantissaLenght);
                    }
                }
            }
        }
        
        private String mf;
        public String MF
        {
            get { return mf; }
            set { mf = value; }
        }
        private String cf;
        public String CF
        {
            get { return cf; }
            set { cf = value; }
        }
        #endregion

        #region Class Constructor
        public PBNumber() { }

        public PBNumber(String inDigit, NumberCapacity inCapacity, RoundingType inRounding)
            : this(inCapacity)
        {
            String currentNumber = "";
            this.InitValue = inDigit;

            IPBNumber.IFPartsOfNumber currentPartialNumber;
            IPBNumber.IFPartsOfNumber currentPartialNumber2cc;
            if (pbConvertion == null)
                pbConvertion = new PBConvertion();
            currentNumber = pbConvertion.NormalizeNumber(inDigit, PBConvertion.ACCURANCY, IPBNumber.NumberFormat.INTEGER);
            currentPartialNumber = pbConvertion.DenormalizeNumber(currentNumber, IPBNumber.NumberFormat.INTEGER);

            currentPartialNumber2cc.Sign = currentPartialNumber.Sign;
            currentPartialNumber2cc.IntegerPart = pbConvertion.convert10to2IPart(currentPartialNumber.IntegerPart);
            currentPartialNumber2cc.FloatPart = pbConvertion.convert10to2FPart(currentPartialNumber.FloatPart);

            //Define Exponent before Mantissa for correct running algorithm 
            selectExp(currentPartialNumber2cc, inCapacity, IPBNumber.NumberFormat.INTEGER);
            selectMantissa(currentPartialNumber2cc, inCapacity, IPBNumber.NumberFormat.INTEGER, inRounding);
            this.Sign = currentPartialNumber.Sign;
        }
        public PBNumber(PBPrototype inNumberPrototype, NumberCapacity inCapacity, RoundingType inRounding)
            : this(inCapacity, inNumberPrototype.Sign, inNumberPrototype.Exponent, inNumberPrototype.Mantissa, inRounding)
        {
            if (pbConvertion == null)
                pbConvertion = new PBConvertion();
        }
        public PBNumber(NumberCapacity inWidth, String inSign, String inExponent, String inMantissa, RoundingType inRounding)
        {
            this.width = inWidth;
            switch (this.width)
            {
                case NumberCapacity.PB32:
                    {
                        this.offset = NumberOffset.PB32;
                        this.exponentLenght = NumberExponentLength.PB32;
                        this.mantissaLenght = NumberMantissaLength.PB32;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB32];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB32];
                        break;
                    }
                case NumberCapacity.PB64:
                    {
                        this.offset = NumberOffset.PB64;
                        this.exponentLenght = NumberExponentLength.PB64;
                        this.mantissaLenght = NumberMantissaLength.PB64;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB64];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB64];
                        break;
                    }
                case NumberCapacity.PB128:
                    {
                        this.offset = NumberOffset.PB128;
                        this.exponentLenght = NumberExponentLength.PB128;
                        this.mantissaLenght = NumberMantissaLength.PB128;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB128];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB128];
                        break;
                    }
                case NumberCapacity.PB256:
                    {
                        this.offset = NumberOffset.PB256;
                        this.exponentLenght = NumberExponentLength.PB256;
                        this.mantissaLenght = NumberMantissaLength.PB256;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB256];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB256];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.name = "Name-" + this.width.ToString() + "[" + inSign + inExponent + "|" + inMantissa + "]";
            this.sign = inSign;
            this.Exponent = inExponent;
            this.Mantissa = inMantissa;
            this.roundingType = inRounding;
            if (pbConvertion == null)
                pbConvertion = new PBConvertion();
        }
        public PBNumber(NumberCapacity inWidth, String inSign, String inExponent, String inMantissa)
        {
            this.width = inWidth;
            switch (this.width)
            {
                case NumberCapacity.PB32:
                    {
                        this.offset = NumberOffset.PB32;
                        this.exponentLenght = NumberExponentLength.PB32;
                        this.mantissaLenght = NumberMantissaLength.PB32;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB32];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB32];
                        break;
                    }
                case NumberCapacity.PB64:
                    {
                        this.offset = NumberOffset.PB64;
                        this.exponentLenght = NumberExponentLength.PB64;
                        this.mantissaLenght = NumberMantissaLength.PB64;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB64];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB64];
                        break;
                    }
                case NumberCapacity.PB128:
                    {
                        this.offset = NumberOffset.PB128;
                        this.exponentLenght = NumberExponentLength.PB128;
                        this.mantissaLenght = NumberMantissaLength.PB128;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB128];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB128];
                        break;
                    }
                case NumberCapacity.PB256:
                    {
                        this.offset = NumberOffset.PB256;
                        this.exponentLenght = NumberExponentLength.PB256;
                        this.mantissaLenght = NumberMantissaLength.PB256;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB256];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB256];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.name = "Name-" + this.width.ToString() + "[" + inSign + inExponent + "|" + inMantissa + "]";
            this.sign = inSign;
            this.Exponent = inExponent;
            this.Mantissa = inMantissa;
            if (pbConvertion == null)
                pbConvertion = new PBConvertion();
        }
        public PBNumber(NumberCapacity inWidth)
        {
            this.width = inWidth;
            switch (this.width)
            {
                case NumberCapacity.PB32:
                    {
                        this.offset = NumberOffset.PB32;
                        this.exponentLenght = NumberExponentLength.PB32;
                        this.mantissaLenght = NumberMantissaLength.PB32;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB32];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB32];
                        break;
                    }
                case NumberCapacity.PB64:
                    {
                        this.offset = NumberOffset.PB64;
                        this.exponentLenght = NumberExponentLength.PB64;
                        this.mantissaLenght = NumberMantissaLength.PB64;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB64];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB64];
                        break;
                    }
                case NumberCapacity.PB128:
                    {
                        this.offset = NumberOffset.PB128;
                        this.exponentLenght = NumberExponentLength.PB128;
                        this.mantissaLenght = NumberMantissaLength.PB128;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB128];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB128];
                        break;
                    }
                case NumberCapacity.PB256:
                    {
                        this.offset = NumberOffset.PB256;
                        this.exponentLenght = NumberExponentLength.PB256;
                        this.mantissaLenght = NumberMantissaLength.PB256;
                        this.mf = IPBNumber.NumberMFs[(int)NumberMF.PB256];
                        this.cf = IPBNumber.NumberCFs[(int)NumberCF.PB256];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.name = "Name-" + this.width.ToString() + "[]";
            if (pbConvertion == null)
                pbConvertion = new PBConvertion();
        }
        #endregion

        #region Class Functions
        public object Clone()
        {
            return new PBNumber(this.width, this.sign, this.exponent, this.mantissa, this.roundingType);
        }
        public void SetFields(String inSign, String inExponent, String inMantissa)
        {
            if (inSign != "")
                this.sign = inSign;
            if (inExponent != "")
            {
                if (inExponent.Length > (int)this.exponentLenght)
                    this.Exponent = inExponent.Substring(inExponent.Length - ((int)this.exponentLenght - 1));
                else
                    this.Exponent = inExponent;
            }
            if (inMantissa != "")
            {
                if (inMantissa.Length > (int)this.mantissaLenght)
                    this.Mantissa = inMantissa.Substring(inMantissa.Length - ((int)this.mantissaLenght - 1));
                else
                    this.Mantissa = inMantissa;
            }
            this.name = "Name-" + this.width.ToString() + "[S={" + inSign + "} | E={" + this.Exponent + "} | M={" + this.Mantissa + "} ]";
        }


        /// <summary>
        /// Based on Number Exp and Mantisa function calculates Correct Value 
        /// Uses: Number.E, Number.M
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="scientificNotation">true - returns digit in scientific notation, else - in simple format</param>
        /// <returns>Returns digit accordingly parameters</returns>
        public string toDigit(bool scientificNotation)
        {
            String CorrectResult = "";
            String CorrectResultExp = "";

            try
            {
                int Offset = 0;
                int precision = 1900;
                String Sign = "";
                // temp Vars
                IPBNumber.stateOfNumber currentState;
                IPBNumber.NumberFormat NumberFormat = IPBNumber.NumberFormat.INTEGER;
                PBConvertion pbconvertion = new PBConvertion();
                switch (this.Name)
                {
                    case "Num32": precision = 1000; break;
                    case "Num64": precision = 1200; break;
                    case "Num128": precision = 1800; break;
                    case "Num256": precision = 1900; break;
                    default: precision = 1800; break;
                }
                switch (NumberFormat)
                {
                    case 0: Offset = (int)this.Offset; break;
                }
                currentState = this.NumberState;

                switch (currentState)
                {
                    case IPBNumber.stateOfNumber.NORMALIZED:
                        CorrectResult = pbconvertion.calcResForNorm(this, precision);
                        break;
                    default:
                        pbconvertion.calcResForNan(this);
                        break;
                }
                IPBNumber.stateOfNumber tempState = this.NumberState;
                Sign = this.Sign;

                if ((tempState == IPBNumber.stateOfNumber.NORMALIZED) || (tempState == IPBNumber.stateOfNumber.DENORMALIZED))
                {
                    if (NumberFormat == 0)
                    {
                        if (CorrectResult.IndexOf(':') == -1)
                            CorrectResultExp = pbconvertion.convertToExp(CorrectResult);
                        else
                        {
                            CorrectResultExp = pbconvertion.convertToExp(CorrectResult.Split(':')[0]);
                            CorrectResultExp += ":" + pbconvertion.convertToExp(CorrectResult.Split(':')[1]);
                        }
                    }
                }

                String result = (scientificNotation) ? CorrectResultExp : CorrectResult;
                return signCharacter + result;
            }
            catch (Exception ex)
            {
                throw new FCCoreArithmeticException("Func 'calcRes' = [ " + ex.Message + " ]");
            }
        }

        /// <summary>
        /// Based on Number Exp and Mantisa function calculates Correct Value 
        /// Uses: Number.E, Number.M
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="number">Number of significant digits</param>
        /// <param name="scientificNotation">true - returns digit in scientific notation, else - in simple format</param>
        /// <returns>Returns digit accordingly parameters</returns>
        public string toDigit(int number, bool scientificNotation)
        {
            String digit = "";
            int signConsider = 1; // toDigit() always returns digit with sign
            int dotConsider;
            String resultDigit;
            String exp = "";

            digit = toDigit(scientificNotation);
            dotConsider = (digit.IndexOf(',') < number + 1) ? 1 : 0; // is dot inside of significant bits

            if (scientificNotation)
            {
                int expIndex = digit.IndexOf('e');
                exp = digit.Substring(expIndex);
                digit = digit.Substring(0, expIndex);
            }

            int digitLengthWithOffset = number + signConsider + dotConsider;
            resultDigit = (digit.Length > digitLengthWithOffset) ? PBConvertion.deleteNonSignificantBits(digit.Substring(0, digitLengthWithOffset)) : digit;
            return resultDigit + exp;
        }

        /// <summary>
        /// Based on Number Exp and Mantisa function calculates Correct Value 
        /// Uses: Number.E, Number.M
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <param name="number">Number of significant digits</param>
        /// <returns>Returns digit accordingly parameters in simple format</returns>
        public string toDigit(int number)
        {
            return toDigit(number, false);
        }

        /// <summary>
        /// Based on Number Exp and Mantisa function calculates Correct Value 
        /// Uses: Number.E, Number.M
        /// IMPORTANT- Doesn't count variety of formats (only Integer)
        /// </summary>
        /// <returns>Returns full digit in simple format</returns>
        public string toDigit()
        {
            return toDigit(-1, false);
        }

        /// <summary>
        /// Calculates Exponent for specified number
        /// Uses : Number.BinaryIntPart,Number.BinaryFloatPart
        /// </summary>
        /// <param name="inNumber">Number - var from which exponenta need to be taken</param>
        /// <param name="Left_Right">False - Left part og number, else - Right </param>
        /// <returns>Returns Exponent in 2cc</returns>
        private void selectExp(IFPartsOfNumber inStingNumber, IPBNumber.NumberCapacity inCapacity, IPBNumber.NumberFormat inNumberFormat)//, PBNumber inObjectNumber
        {

            int z = 0;
            int Offset = 0;
            String temp, result = "";
            String bynaryStringInt = "", bynaryStringFloat = "";
            bynaryStringInt = inStingNumber.IntegerPart;
            bynaryStringFloat = inStingNumber.FloatPart;

            try
            {
                Offset = (int)pbConvertion.getNumberOffset(inCapacity);
                
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

                result = pbConvertion.convert10to2IPart((z + Offset).ToString());
                this.Exponent = result;
            }
            catch (Exception ex)
            {
                throw new PBFunctionException("Exception in Func ['selectExp'] Mess=[" + ex.Message + "]");
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
        private void selectMantissa(IFPartsOfNumber inStringNumber, IPBNumber.NumberCapacity inCapacity, IPBNumber.NumberFormat inNumberFormat, IPBNumber.RoundingType inRoundingType)// PBNumber inObjectNumber, 
        {
            int i, l = 0;
            int currMBits;
            String result = "";
            String[] tempArray;
            int offsetDot = 1;
            String bynaryStringInt = "", bynaryStringFloat = "";

            if (inNumberFormat == 0)
            {
                bynaryStringInt = inStringNumber.IntegerPart;
                bynaryStringFloat = inStringNumber.FloatPart;
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
                                if (pbConvertion.isStringZero(Exponent))
                                    result = "" + bynaryStringFloat.Substring((int)pbConvertion.getNumberOffset(inCapacity) - 1, (int)pbConvertion.getNumberMantissaLength(inCapacity) + 1);
                                else
                                {
                                    offsetDot = bynaryStringFloat.IndexOf('1') + 1;
                                    result = "" + bynaryStringFloat.Substring(offsetDot);
                                }
                            else
                            {
                                currMBits = (int)pbConvertion.getNumberMantissaLength(inCapacity);
                                tempArray = new String[currMBits];
                                for (i = 0; i < currMBits; i++)
                                    tempArray[i] = "0";
                                result = result + String.Join("", tempArray);
                            }
                    }
                    else
                    {
                        throw new PBArithmeticException("Exception in Func ['selectMantissa'] Mess=[ Empty String - BynaryIntPart or BynaryFloatPart  ] ( PB" + inCapacity + "=" + inStringNumber.ToString() + ")");
                    }
                }
                else
                {
                    throw new PBArithmeticException("Exception in Func ['selectMantissa'] Mess=[ Null - BynaryIntPart or BynaryFloatPart ] ( PB" + inCapacity + "=" + inStringNumber.ToString() + ")");
                }

                currMBits = (int)pbConvertion.getNumberMantissaLength(inCapacity);

                if (result.Length <= (int)currMBits)
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
                Round(inRoundingType, result, currMBits, inStringNumber, offsetDot, inCapacity);


            }
            catch (Exception ex)
            {
                throw new PBFunctionException("Exception in Func ['selectMantissa'] Mess=[" + ex.Message + "]");
            }
        }

        public void Round(IPBNumber.RoundingType inRoundingType, String result, int currMBits, IFPartsOfNumber inStringNumber, int offsetDot, IPBNumber.NumberCapacity inCapacity)
        {
            String M = "";

            switch (inRoundingType)
            {
                case IPBNumber.RoundingType.ZERO: // to ZERO
                    M = result.Substring(0, currMBits);
                    break;
                case IPBNumber.RoundingType.NEAR_INTEGER:// to INTEGER
                    if (pbConvertion.isStringZero(Exponent))
                    {
                        String[] tempArray = new String[offsetDot];
                        for (int i = 0; i < offsetDot; i++)
                            tempArray[i] = "0";
                        M = M + String.Join("", tempArray);

                        M += result.Substring(0, currMBits + 1 - offsetDot);
                    }
                    else
                        M = result.Substring(0, currMBits + 0);
                    if ((result[currMBits] == '1') && (inStringNumber.Sign[0] == '+'))
                    {
                        if (!pbConvertion.checkStringFull(M))
                        {
                            M = pbConvertion.convert2to10IPart(M);
                            M = pbConvertion.Addition(M, "1");
                        }
                        else
                        {
                            M = "0";
                            if (!pbConvertion.checkStringFull(Exponent))
                            {
                                pbConvertion.sumExp(Exponent, "1", inCapacity);
                            }
                        }
                        M = pbConvertion.convert10to2IPart(M);
                        if (M.Length + 1 == currMBits)
                        {
                            M = "0" + M;
                        }
                        else
                            if (M.Length < currMBits)
                            {
                                int l = currMBits - M.Length;
                                String[] tempArray = new String[l];
                                for (int i = 0; i < l; i++)
                                    tempArray[i] = "0";
                                M = String.Join("", tempArray) + M;
                            }
                    }
                    break;

                case IPBNumber.RoundingType.POSITIVE_INFINITY:// +Inf 
                    M = result.Substring(0, currMBits);
                    if (inStringNumber.Sign[0] == '0')
                    {
                        if (!pbConvertion.checkStringFull(M))
                        {
                            M = pbConvertion.convert2to10IPart(M);
                            M = pbConvertion.Addition(M, "1");
                        }
                        else
                        {
                            M = "0";
                            if (!pbConvertion.checkStringFull(Exponent))
                            {
                                pbConvertion.sumExp(Exponent, "1", inCapacity);
                            }
                        }
                        M = pbConvertion.convert10to2IPart(M);
                    }
                    break;

                case IPBNumber.RoundingType.NEGATIVE_INFINITY:
                    // -Inf
                    M = result.Substring(0, currMBits);
                    if (inStringNumber.Sign[0] == '1')
                    {
                        if (!pbConvertion.checkStringFull(M))
                        {
                            M = pbConvertion.convert2to10IPart(M);
                            M = pbConvertion.Addition(M, "1");
                        }
                        else
                        {
                            M = "0";
                            if (!pbConvertion.checkStringFull(Exponent))
                            {
                                pbConvertion.sumExp(Exponent, "1", inCapacity);
                            }
                        }
                        M = pbConvertion.convert10to2IPart(M);
                    }
                    break;
                case IPBNumber.RoundingType.POST_BINARY:
                    if (result.Length >= currMBits)
                    {
                        M = result.Substring(0, currMBits);
                    }
                    else
                    {
                        M = result;
                        while (M.Length < currMBits)
                        {
                            M = "0" + M;
                        }
                    }

                    if (result.Length >= currMBits + 2)
                    {
                        String nonSignificantBits = result.Substring(currMBits, 2);
                        if ((nonSignificantBits == "01") || (nonSignificantBits == "10"))
                        {
                            int lastZero = M.LastIndexOf('0');
                            if (lastZero == -1)
                            {
                                if (nonSignificantBits == "10")
                                {
                                    M = pbConvertion.getEmptyMantissa(inCapacity).ToString();

                                    //TODO: Create function for addition in binary, and delete next 3th lines of code
                                    String tempExponent = pbConvertion.convert2to10IPart(this.Exponent);// 
                                    tempExponent = pbConvertion.Addition(tempExponent, "1");            // ADD 1 to Exponent
                                    this.Exponent = pbConvertion.convert10to2IPart(tempExponent);       //
                                }
                            }
                            else
                            {
                                //TODO: Control here 
                                M = M.Substring(0, lastZero) + "M";
                                while (M.Length < currMBits)
                                {
                                    M += "A";
                                }
                            }
                        }else
                            if (nonSignificantBits == "11")
                            {
                                if (M.LastIndexOf('0')!=-1)
                                {
                                   M = pbConvertion.convert10to2IPart( pbConvertion.Addition(pbConvertion.convert2to10IPart(M), "1") );
                                }else
                                {
                                    Round(IPBNumber.RoundingType.NEAR_INTEGER, result, currMBits, inStringNumber, offsetDot, inCapacity);
                                }
                            }
                    }
                    break;
            }
            this.Mantissa = M;
        }
        #endregion

    }

}

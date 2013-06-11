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
    public class PBNumber : IPBNumber
    {
      
        #region Class Properties
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

        #endregion

        #region Class Fields
     
        private String sign;
        public String Sign
        {
            get { return sign; }
            set
            {
                if ((value == "-") || (value == "+"))
                    sign = value;
                else
                    throw new IncorrectSignException("Base Number: Sign should be only '-' and '+' symbols");
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

        public PBNumber(String inDigit, NumberCapacity inCapacity, RoundingType inRounding) : this(new PBConvertion().CreateNumber(inDigit, inCapacity, inRounding), inCapacity, inRounding) { }
        public PBNumber(PBPrototype inNumberPrototype, NumberCapacity inCapacity, RoundingType inRounding)
            : this(inCapacity, inNumberPrototype.Sign, inNumberPrototype.Exponent, inNumberPrototype.Mantissa, inRounding)
        { 

        }
        public PBNumber(NumberCapacity inWidth, String inSign, String inExponent, String inMantissa,RoundingType inRounding)
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
            this.name = "Name-"+this.width.ToString() + "[" + inSign + inExponent + "|" + inMantissa + "]";
            this.sign = inSign;
            this.Exponent = inExponent;
            this.Mantissa = inMantissa;
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
                        this.mf = IPBNumber.NumberMFs[ (int)NumberMF.PB32 ];
                        this.cf = IPBNumber.NumberCFs[ (int)NumberCF.PB32 ];
                        break;
                    }
                case NumberCapacity.PB64:
                    {
                        this.offset = NumberOffset.PB64;
                        this.exponentLenght = NumberExponentLength.PB64;
                        this.mantissaLenght = NumberMantissaLength.PB64;
                        this.mf = IPBNumber.NumberMFs[ (int)NumberMF.PB64 ];
                        this.cf = IPBNumber.NumberCFs[ (int)NumberCF.PB64 ];
                        break;
                    }
                case NumberCapacity.PB128:
                    {
                        this.offset = NumberOffset.PB128;
                        this.exponentLenght = NumberExponentLength.PB128;
                        this.mantissaLenght = NumberMantissaLength.PB128;
                        this.mf = IPBNumber.NumberMFs[ (int)NumberMF.PB128 ];
                        this.cf = IPBNumber.NumberCFs[ (int)NumberCF.PB128 ];
                        break;
                    }
                case NumberCapacity.PB256:
                    {
                        this.offset = NumberOffset.PB256;
                        this.exponentLenght = NumberExponentLength.PB256;
                        this.mantissaLenght = NumberMantissaLength.PB256;
                        this.mf = IPBNumber.NumberMFs[ (int)NumberMF.PB256 ];
                        this.cf = IPBNumber.NumberCFs[ (int)NumberCF.PB256 ];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.name = "Name-"+this.width.ToString() + "[]";
        }
        #endregion

        #region Class Functions
        public void SetFields(String inSign,String inExponent, String inMantissa)
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
            this.name = "Name-" + this.width.ToString() + "[S={" + inSign+"} | E={" + this.Exponent + "} | M={" + this.Mantissa + "} ]";
        }

        #endregion

    }

}

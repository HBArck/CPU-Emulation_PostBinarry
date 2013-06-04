using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    public class PBNumber
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
       
        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int mantissaLenght;
        public int MantissaLenght
        {
            get { return mantissaLenght; }
            /*Read only*/
            set { }
        }
        private int exponentLenght;
        public int ExponentLenght
        {
            get { return exponentLenght; }
            /*Read only*/
            set { }
        }

        private int offset;
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
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
                if (value.Length == exponentLenght)
                {
                    exponent = value;
                }
                else
                {
                    if (value.Length < exponentLenght)
                    {
                        exponent = value;
                        while (exponent.Length < exponentLenght)
                            exponent = "0" + exponent;
                    }
                    else
                    {
                        exponent = value.Substring(0, exponentLenght);
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
                if (value.Length == mantissaLenght)
                {
                    mantissa = value;
                }
                else
                {
                    if (value.Length < mantissaLenght)
                    {
                        mantissa = value;
                        while (mantissa.Length < mantissaLenght)
                            mantissa = "0" + mantissa;
                    }
                    else
                    {
                        mantissa = value.Substring(0, mantissaLenght);
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
        
        public PBNumber(int inWidth, String inSign, String inExponent, String inMantissa)
        {
            this.width = inWidth;
            switch (this.width)
            {
                case 32:
                    {
                        this.offset = 127;
                        this.exponentLenght = 8;
                        this.mantissaLenght = 21;
                        this.mf = "00";
                        this.cf = "0";
                        break;
                    }
                case 64:
                    {
                        this.offset = 1023;
                        this.exponentLenght = 11;
                        this.mantissaLenght = 48;
                        this.mf = "000";
                        this.cf = "01";
                        break;
                    }
                case 128:
                    {
                        this.offset = 16383;
                        this.exponentLenght = 15;
                        this.mantissaLenght = 104;
                        this.mf = "00000";
                        this.cf = "011";
                        break;
                    }
                case 256:
                    {
                        this.offset = 524287;
                        this.exponentLenght = 20;
                        this.mantissaLenght = 219;
                        this.mf = "000000000000";
                        this.cf = "0111";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            this.name = "Name-"+this.width.ToString() + "[" + inSign + inExponent + "|" + inMantissa + "]";
            this.sign = inSign;
            this.exponent = inExponent;
            this.mantissa = inMantissa;
        }
        public PBNumber(int inWidth)
        {
            this.width = inWidth;
            switch (this.width)
            {
                case 32:
                    {
                        this.offset = 127;
                        this.exponentLenght = 8;
                        this.mantissaLenght = 21;
                        this.mf = "00";
                        this.cf = "0";
                        break;
                    }
                case 64:
                    {
                        this.offset = 1023;
                        this.exponentLenght = 11;
                        this.mantissaLenght = 48;
                        this.mf = "000";
                        this.cf = "01";
                        break;
                    }
                case 128:
                    {
                        this.offset = 16383;
                        this.exponentLenght = 15;
                        this.mantissaLenght = 104;
                        this.mf = "00000";
                        this.cf = "011";
                        break;
                    }
                case 256:
                    {
                        this.offset = 524287;
                        this.exponentLenght = 20;
                        this.mantissaLenght = 219;
                        this.mf = "000000000000";
                        this.cf = "0111";
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
                if (inExponent.Length > this.exponentLenght)
                    this.exponent = inExponent.Substring(inExponent.Length - (this.exponentLenght - 1));
                else
                    this.exponent = inExponent;
            }
            if (inMantissa != "")
            {
                if (inMantissa.Length > this.mantissaLenght)
                    this.mantissa = inMantissa.Substring(inMantissa.Length - (this.mantissaLenght - 1));
                else
                    this.mantissa = inMantissa;
            }
            this.name = "Name-" + this.width.ToString() + "[S={" + inSign+"} | E={" + inExponent + "} | M={" + inMantissa + "} ]";
        }
        #endregion
    }
}

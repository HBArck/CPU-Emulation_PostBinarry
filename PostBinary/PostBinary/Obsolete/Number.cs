using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    [Obsolete("Not used anymore", true)]
    public struct exponent
    {
        /*
        private static bool isempty;
        public static bool isEmpty
        {
            get { return isempty; }
        }
        */
        private String leftPart;
        public String LeftPart
        {
            get { return leftPart; }
            set
            {
                if (value != "")
                    leftPart = value;
            }
        }

        private String rightPart;
        public String RightPart
        {
            get { return rightPart; }
            set
            {
                if (value != "")
                    rightPart = value;
            }
        }
        /*
        public abstract exponent() 
        {
            this.leftPart = "";
            this.rightPart = "";
        }*/
        public exponent(String leftPart, String rightPart)
        {
            this.leftPart = leftPart;
            this.rightPart = rightPart;
        }

    }
    [Obsolete("Not used anymore", true)]
    public struct mantissa
    {
        private String leftPart;
        public String LeftPart
        {
            get { return leftPart; }
            set
            {
                if (value != "")
                    leftPart = value;
            }
        }

        private String rightPart;
        public String RightPart
        {
            get { return rightPart; }
            set
            {
                if (value != "")
                    rightPart = value;
            }
        }
        /*
        public abstract mantissa() 
        {
            this.leftPart = "";
            this.rightPart = "";
        }
        */
        public mantissa(String leftPart, String rightPart)
        {
            this.leftPart = leftPart;
            this.rightPart = rightPart;
        }
    }

    /// <summary>
    /// Base class for number in PostBinary Calculator
    /// </summary>
    [Obsolete("Not used anymore",true)]
    public class BaseNumber
    {
        private String sign;
        public String Sign
        {
            get { return sign; }
            set {
                if ((value == "-") || (value == "+"))
                    sign = value;
                else
                    throw new IncorrectSignException("Base Number: Sign should be only '-' and '+' symbols");
            }
        }
        private mantissa mantissa;
        public mantissa Mantissa
        {
            get { return mantissa; }
            set { mantissa = value; }
        }
    
        private exponent exponenta;
        public exponent Exponenta
        {
            get { return exponenta; }
            set { exponenta = value; }
        }

        public BaseNumber()
        {
            //Mantissa = new mantissa();
            //Exponenta = new exponent();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }

    /// <summary>
    /// Class that represents number in PostBinary32 format
    /// </summary>
    [Obsolete("Not used anymore", true)]
    public class Number32 : BaseNumber
    { 

    }

    /// <summary>
    /// Class that represents number in PostBinary64 format
    /// </summary>
    [Obsolete("Not used anymore", true)]
    public class Number64 : BaseNumber
    {

    }

    /// <summary>
    /// Class that represents number in PostBinary128 format
    /// </summary>
    [Obsolete("Not used anymore", true)]
    public class Number128 : BaseNumber
    {

    }

    /// <summary>
    /// Class that represents number in PostBinary256 format
    /// </summary>
    [Obsolete("Not used anymore", true)]
    public class Number256 : BaseNumber
    {

    }
    [Obsolete("Not used anymore", true)]
    public class Number : BaseNumber
    {
        private Number32 num32;
        public Number32 Num32
        {
            get { return num32; }
            set { num32 = value; }
        }

        public Number pb32()
        {
            return null;
        }
        public Number pb64()
        {
            return null;
        }
        public Number pb128()
        {
            return null;
        }
        public Number pb256()
        {
            return null;
        }
    }
}

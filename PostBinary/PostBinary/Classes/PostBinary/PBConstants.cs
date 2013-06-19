using System.Text;
using System;

namespace PostBinary.Classes.PostBinary
{
    public class IPBNumber
    {
        /// <summary>
        /// Rounding type for postbinary operations
        /// </summary>
        public enum RoundingType
        {
            ZERO = 0,
            NEAR_INTEGER = 1,
            POSITIVE_INFINITY = 2,
            NEGATIVE_INFINITY = 3,
            POST_BINARY = 4
        };

        /// <summary>
        /// This enum indicates type of number: Integer, Float, Interval
        /// </summary>
        public enum NumberFormat
        {
            INTEGER,
            FLOAT,
            INTERVAL
        };

        /// <summary>
        /// Stores number states
        /// </summary>
        public enum stateOfNumber
        {
            NORMALIZED,
            DENORMALIZED,
            ZERO,
            INFINITE,
            NAN,
            ERROR
        };

        /// <summary>
        /// Variety of number capacity. Defines quantity of tetrits in digit.
        /// </summary>
        public enum NumberCapacity
        {
            PB32 = 32,
            PB64 = 64,
            PB128 = 128,
            PB256 = 256
        };

        /// <summary>
        /// Defines offset for numbers in diferent formats.
        /// </summary>
        public enum NumberOffset
        {
            PB32 = 127,
            PB64 = 1023,
            PB128 = 16383,
            PB256 = 524287
        }

        /// <summary>
        /// Defines Exponent Length for numbers in diferent formats.
        /// </summary>
        public enum NumberExponentLength
        {
            PB32 = 8,
            PB64 = 11,
            PB128 = 15,
            PB256 = 20
        }

        /// <summary>
        /// Defines Mantissa Length for numbers in diferent formats.
        /// </summary>
        public enum NumberMantissaLength
        {
            PB32 = 21,
            PB64 = 48,
            PB128 = 104,
            PB256 = 219
        }
        /// <summary>
        /// Defines amount accurate decimal digits
        /// </summary>
        public static String[] DecimalDigitAccuracy = 
        {
            "6",
            "14", //14-15
            "31", //31-32
            "66"
        };
        /// <summary>
        /// Defines tetrits enum 
        /// </summary>
        public enum Tetrits
        {
            ZERO_TETRIT = '0',
            ONE_TETRIT = '1',
            M_TETRIT = 'M',
            A_TETRIT = 'A'
        }
        /// <summary>
        /// 
        /// </summary>
        public static String[] NumberMFs = {   "00",
                                        "000",
                                        "00000",
                                        "000000000000"
        };
        /// <summary>
        /// 
        /// </summary>
        public static String[] NumberCFs = {   "0",
                                        "01",
                                        "011",
                                        "0111"
        };

        /// <summary>
        /// Defines MF constant field.
        /// </summary>
        public enum NumberMF
        {
            PB32 = 0,
            PB64 = 1,
            PB128 = 2,
            PB256 = 3
        }

        /// <summary>
        /// Defines MF constant field.
        /// </summary>
        public enum NumberCF
        {
            PB32 = 0,
            PB64 = 1,
            PB128 = 2,
            PB256 = 3
        }

        /// <summary>
        /// Struct stores Float and Integer part of Number
        /// </summary>
        public struct IFPartsOfNumber
        {
            public String Sign;
            public String IntegerPart;
            public String FloatPart;
        }

        /// <summary>
        /// Defines empty Exponent values
        /// </summary>
        public static String[] EmptyExponent =
        {
            "00000000",
            "00000000000",
            "000000000000000",
            "00000000000000000000"
        };
        /// <summary>
        /// Defines empty Mantissa values
        /// </summary>
        public static String[] EmptyMantissa =
        {
            "000000000000000000000",
            "000000000000000000000000000000000000000000000000",
            "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
        };
        public enum NumberFormatCode
        {
            PB32 = 0,
            PB64 = 1,
            PB128 = 2,
            PB256 = 3
        };
    }
}
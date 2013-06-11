using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PostBinary.Classes.Utils
{
  
    class NumberUtil
    {
        private ProgramCore PCoreInst = null;

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


        public struct scientificNotationNumber
        {
            public String str;
            public int numCh;
            public string exponent;
        };
        public static scientificNotationNumber retStruct;
        public static scientificNotationNumber SplitNumber(string str)
        {
            retStruct = new scientificNotationNumber();
            retStruct.numCh = str.Split('e')[0].Remove(str.Split('e')[0].IndexOf(','), 1).Trim('-').Length;
            retStruct.exponent = str.Split('e')[str.Split('e').Count() - 1].Trim('-');
            retStruct.str = str.Replace("e", "*10^(") + ")";
            return retStruct;
        }
    }
}

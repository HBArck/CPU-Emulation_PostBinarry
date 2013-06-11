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
        /// <param name="leftOperand"></param>
        /// <param name="rightOperand"></param>
        /// <returns></returns>
        public PBNumber ADD(PBNumber leftOperand, PBNumber rightOperand)
        {
            return null;
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

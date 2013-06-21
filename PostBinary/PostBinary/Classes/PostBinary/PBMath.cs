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
            return null;// ЗАГЛУШКА
        }

        public static int CMP(String opA, String opB) {
            PBConvertion pbconvertion = new PBConvertion();
            if (opA.Length > 31)
                throw new ArithmeticException("");
            int a = Int32.Parse(pbconvertion.convert2to10IPart(opA));
            int b = Int32.Parse(pbconvertion.convert2to10IPart(opB));

            int result = 0;
            if (a > b)
                result = 1;
            else if (a < b)
                result = -1;

            return result;
        }
        public static String ADDTetra(String opA, String opB, bool und)
        {
            // сложение тетракодов  
            // (если und = true - сумма с учетом выходного переноса)
            String opC = "";	//строка-результат

            //если одна из строк-операндов пустая - возвращаем пустую
            if (opA == "" || opB == "") return opC;

            // выравниваем строки А и В
            int Al = opA.Length, Bl = opB.Length; // запоминаем длины строк
            if (opA.Length > opB.Length) for (int i = 0; i < Al - Bl; i++) opB = opB.Insert(0, "0");
            else if (opA.Length != opB.Length) for (int i = 0; i < Bl - Al; i++) opA = opA.Insert(0, "0");

            Char buf = '0';  // перенос для суммы

            // начинаем складывать
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

            if (buf != '0' && und) opC = opC.Insert(0, buf.ToString()); // учитываем выходной перенос

            /* ни в коем случае при сложении для мантиссы нельзя удалять незначащие (те которые спереди) нули!!! */

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

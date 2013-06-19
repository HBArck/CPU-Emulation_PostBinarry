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
         /*   // сложение тетракодов  
        // (если und = true - сумма с учетом выходного переноса)
            String opA;
            String opB;
            bool und;
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
                        if (buf == '0') opC = opC.Insert(0, opB[i].ToString());
                        else if (buf == '1')
                        {
                            buf = opB[i];
                            if (opB[i] == '0') opC = opC.Insert(0, "1");
                            if (opB[i] == '1') opC = opC.Insert(0, "0");
                            if (opB[i] == 'A') opC = opC.Insert(0, "M");
                            if (opB[i] == 'M') opC = opC.Insert(0, "A");
                        }
                        else if (buf == 'A')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "A"); buf = '0'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "M"); buf = 'A'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "0"); buf = 'A'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "1"); buf = '0'; }
                        }
                        else if (buf == 'M')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "M"); buf = '0'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "A"); buf = 'M'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "1"); buf = '0'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "0"); buf = 'M'; }
                        }
                        break;

                    case '1':
                        if (buf == '0')
                        {
                            buf = opB[i];
                            if (opB[i] == '0') opC = opC.Insert(0, "1");
                            if (opB[i] == '1') opC = opC.Insert(0, "0");
                            if (opB[i] == 'A') opC = opC.Insert(0, "M");
                            if (opB[i] == 'M') opC = opC.Insert(0, "A");
                        }
                        else if (buf == '1') opC = opC.Insert(0, opB[i].ToString());
                        else if (buf == 'A')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "M"); buf = 'A'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "A"); buf = '1'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "1"); buf = 'A'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "0"); buf = '1'; }
                        }
                        else if (buf == 'M')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "A"); buf = 'M'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "M"); buf = '1'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "0"); buf = '1'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "1"); buf = 'M'; }
                        }
                        break;

                    case 'A':
                        if (buf == '0')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "A"); buf = '0'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "M"); buf = 'A'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "0"); buf = 'A'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "1"); buf = '0'; }
                        }
                        else if (buf == '1')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "M"); buf = 'A'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "A"); buf = '1'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "1"); buf = 'A'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "0"); buf = '1'; }
                        }
                        else if (buf == 'A') opC = opC.Insert(0, opB[i].ToString());
                        else if (buf == 'M')
                        {
                            buf = opB[i];
                            if (opB[i] == '0') opC = opC.Insert(0, "1");
                            if (opB[i] == '1') opC = opC.Insert(0, "0");
                            if (opB[i] == 'A') opC = opC.Insert(0, "M");
                            if (opB[i] == 'M') opC = opC.Insert(0, "A");
                        }
                        break;
                    default:
                        if (buf == '0')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "M"); buf = '0'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "A"); buf = 'M'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "1"); buf = '0'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "0"); buf = 'M'; }
                        }
                        else if (buf == '1')
                        {
                            if (opB[i] == '0') { opC = opC.Insert(0, "A"); buf = 'M'; }
                            if (opB[i] == '1') { opC = opC.Insert(0, "M"); buf = '1'; }
                            if (opB[i] == 'A') { opC = opC.Insert(0, "0"); buf = '1'; }
                            if (opB[i] == 'M') { opC = opC.Insert(0, "1"); buf = 'M'; }
                        }
                        else if (buf == 'M') opC = opC.Insert(0, opB[i].ToString());
                        else if (buf == 'A')
                        {
                            buf = opB[i];
                            if (opB[i] == '0') opC = opC.Insert(0, "1");
                            if (opB[i] == '1') opC = opC.Insert(0, "0");
                            if (opB[i] == 'A') opC = opC.Insert(0, "M");
                            if (opB[i] == 'M') opC = opC.Insert(0, "A");
                        }
                        break;
                }

            }

            if (buf != '0' && und) opC = opC.Insert(0, buf.ToString()); // учитываем выходной перенос

            /* ни в коем случае при сложении для мантиссы нельзя удалять незначащие (те которые спереди) нули!!! */
            /*
            return opC;*/
            return null;// ЗАГЛУШКА
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

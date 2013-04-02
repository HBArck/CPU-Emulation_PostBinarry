using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{
    class StringUtil
    {
        /// <summary>
        /// Trims the number from symbol '0'
        /// </summary>
        /// <param name="inputStr">Входная строка для обработки</param>
        /// <returns>Возвращает строку без избыточных нулей вначале и конце числа</returns>
        public static String deleteZeroFromNumber(String inputStr)
        {
            // удаление 0 в начале числа
            String outStr = "";
            char[] trimparams = { '0' };
            int i = 0, z = 0, k = 0;
            //inputStr.TrimStart('0');
            //inputStr.TrimEnd('0');
            try
            {
                if (inputStr.Length >= 3)
                {
                    if ((inputStr[0] == '-') || (inputStr[0] == '+'))
                        z++;
                    for (i = z; i < inputStr.IndexOf(',') - 1; i++)
                    {
                        if (inputStr[i] == '0')
                            continue;
                        else
                            break;
                    }

                    if (z == 1)
                        outStr = inputStr[0].ToString();

                    if (inputStr[inputStr.Length - 1] == '0')
                    {
                        for (k = inputStr.Length - 1; k > inputStr.IndexOf(',') + 1; k--)
                            if (inputStr[k] != '0')
                                break;
                    }
                    else
                        k = inputStr.Length - 1;
                    outStr += inputStr.Substring(i, k + 1 - i);
                    return outStr;
                }
                else
                    return "0,0";
            }
            catch (Exception ex)
            {
                throw new FCCoreGeneralException("Func 'deleteZeroFromNumber' = [ " + ex.Message + " ]");
            }
        }

        /// <summary>
        /// Function trims input polynom string from symbol '0'.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String Trim(String str)
        {
            return "";
        }
    }
}

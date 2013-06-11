using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PostBinary.Classes.Utils;
using PostBinary.Classes.PostBinary;

namespace PostBinary.Testers
{
    class CommonTester
    {

        public void TEST_SplitNumber()
        {
            String[] testStr = { "123,456e-789", "123,456e+789", "3,0e-1" };
            for (int i = 0; i < testStr.Length; i++)
            {
                NumberUtil.retStruct = NumberUtil.SplitNumber(testStr[i]);
                Console.WriteLine(" {0} , {1}, {2}", NumberUtil.retStruct.str, NumberUtil.retStruct.numCh, NumberUtil.retStruct.exponent);
                Console.ReadLine();
            }

        }
        public static void CreatePBNumber()
        {
            String testNumber = "123.123";

            PBNumber pbNUmber = new PBNumber(testNumber, NumberCapacity.PB128, RoundingType.NEAR_INTEGER);
        }
    }
}

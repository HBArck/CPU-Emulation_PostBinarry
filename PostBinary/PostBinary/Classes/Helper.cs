using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinary.Classes
{

   
    /// <summary>
    /// Helper should show examples for different expressions
    /// </summary>
    class Helper
    {
       // Constants
       public String[] constNames = {"none", "exp", "Pi"};
       public String[] constants = 
       { 
        "",
        "2.718281828459045235360287471352662497757247093699959574966967627724076", // 70 decimal digits
        "3.141592653589793238462643383279502884197169399375105820974944592307816" // 70 decimal digits
        };


       // Polynoms
       public String[] polynomNames = { "none", "Rump", "Rump 2" };
       public String[] polynoms = 
       { 
        "",
        "333.75b^6+a2(11a^2b^2-b^6-121b^4-2)+5.5b^8+a/(2b)", // 
        "21b^2-2a^2+55b^4–10a^2b^2+a/(2b)" // 
        };

       // Scientific notation -> moved to NumberUtils         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
   public class Verification
    {
        public int OneTimePin { get; set; }

        // Generates a random number within a range.      
        public static bool GenerateOTP()
        {
            // Instantiate random number generator.  
            Random _random = new Random();

            Global.OneTimePin = _random.Next(99999, 999999);

            return true;
        }
    }
}

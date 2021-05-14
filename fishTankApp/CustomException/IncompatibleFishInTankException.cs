using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.CustomException
{
    public class IncompatibleFishInTankException: Exception
    {
        public IncompatibleFishInTankException(): base("Cannot add fish to the tank as an incompatible fish already exists")
        {

        }
    }
}

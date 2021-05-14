using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.CustomException
{
    public class NotEnoughCapacityException: Exception
    {
        public NotEnoughCapacityException(): base("Not enought capacity in fish tank")
        {

        }
    }
}

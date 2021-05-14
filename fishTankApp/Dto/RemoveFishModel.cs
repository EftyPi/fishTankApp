using fishTankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.Dto
{
    public class RemoveFishModel
    {
        public int Id { get; set; }
        public Fish Fish { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.Dto
{
    public class UpdateFishTankCapacityModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(5, 38)]
        public int Capacity { get; set; }
    }
}

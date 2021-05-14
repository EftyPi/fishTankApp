using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Dto
{
    public class CreateFishTankModel
    {
        [Required]
        [Range(5, 38)]
        public int Capacity { get; set; }
    }
}

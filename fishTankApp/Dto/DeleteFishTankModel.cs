using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Dto
{
    public class DeleteFishTankModel
    {
        [Required]
        public int Id { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Models
{
    public class Breed : Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(0, 4)]
        public int Size { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Models
{
    public class Plant : Entity
    {
        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        [Required]
        [Range(0, 12)]
        public int Size { get; set; }

        // Cannot be Color type because it si not a primitive type
        [Required]
        public string Colour { get; set; }
    }
}

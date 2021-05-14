using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Models
{
    public class Plant : Decoration
    {
        [Required]
        [Range(0, 12)]
        public new int Size { get; set; }

    }
}

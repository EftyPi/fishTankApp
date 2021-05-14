using System;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Models
{
    public class Fish : Entity
    {
        [Required]
        public Breed Breed { get; private set;  }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Size { get; set; }
        
        [StringLength(255)]
        public string? Name { get; set; }


        public void SetBreed(Breed breed)
        {
            Breed = breed;
            Size = breed.Size;
        }
    }
}

using fishTankApp.Models;
using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Dto
{
    public class AddItemToFishTank
    {
        [Required]
        public int Id { get; set; }

        public Fish Fish { get; set; }

        // item can be either decoration or plant
        public Decoration Item { get; set; }
    }
}

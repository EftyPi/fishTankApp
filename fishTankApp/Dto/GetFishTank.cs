using System.ComponentModel.DataAnnotations;

namespace fishTankApp.Dto
{
    public class GetFishTank
    {
        [Required]
        public int Id { get; set; }
    }
}


using fishTankApp.Database.Context;
using fishTankApp.Dto;
using fishTankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishTankController : ControllerBase
    {
        private readonly FishTankContext _context;
        public FishTankController(FishTankContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddFishTank([FromBody] CreateFishTankModel model)
        {
            FishTank tank = new FishTank();
            tank.Capacity = model.Capacity;
            tank.AvailableCapacity = model.Capacity;

            await _context.FishTank.AddAsync(tank);
            await _context.SaveChangesAsync();
            return Ok(tank);
        }

        [HttpPatch]
        public async Task<IActionResult> ReplaceFishTank([FromBody] UpdateFishTankCapacityModel model)
        {
            // Find fish tank
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id);

            if (tank == null)
            {
                return BadRequest($"Fish tank with Id {model.Id} not found");
            }

            // Check if the old fish can fit in the new capacity
            if (tank.Fishes != null && tank.Fishes.Count() > 0)
            {
                // Find the size of all the fishes
                int fishSize = tank.Fishes.ToList().Select(f => f.Size).Aggregate((sum, val) => sum + val);
                if (fishSize > model.Capacity)
                {
                    return BadRequest($"Fish tank with Id {model.Id} not big enough to hold the fishes of the old tank");
                }
            }
            

            // The new tank to replace the old
            FishTank newTank = new FishTank();
            // deocaration and plants are discarded when replacing the fish tank
            newTank.Capacity = model.Capacity;
            newTank.AddFish(tank.Fishes);

            // remove the old fishes from the old tank
            tank.RemoveFish(tank.Fishes.Select(f => f.Id));
            // add and save the new fish tank
            await _context.FishTank.AddAsync(newTank);
            await _context.SaveChangesAsync();

            return Ok(newTank);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmptyFishTank([FromBody] DeleteFishTankModel model)
        {
            // Find fish tank
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id);

            if (tank == null)
            {
                return BadRequest($"Fish tank with Id {model.Id} not found");
            }

            // check how many items are in the tank (all fishes, decoration and plant)
            int itemsCount = tank.Fishes.ToList().Count + tank.Items.ToList().Count;

            if (itemsCount != 0)
            {
                return BadRequest($"Fish tank with Id {model.Id} is not empty");
            }

            // remove and save the changes
            _context.FishTank.Remove(tank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/capacity")]
        public async Task<IActionResult> GetFishTankByCapacityAndAvailableCapacity([FromBody] TankByCapacityAndAvailableCapacityModel model)
        {
            List<FishTank> fishTanks = await _context.FishTank
                .Where(t => t.Capacity == model.Capacity && t.AvailableCapacity == model.AvailableCapacity).ToListAsync();

            if (!fishTanks.Any())
            {
                return BadRequest("Fish tank with given capacities not found");
            }

            return Ok(fishTanks.First());
        }

        [HttpGet]
        public async Task<IActionResult> GetFishTankById([FromBody] GetFishTank model)
        {
            // Find fish tank
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id);

            if (tank == null)
            {
                return BadRequest($"Fish tank with Id {model.Id} not found");
            }

            return Ok(tank);

        }


        [HttpPost("/content")]
        public async Task<IActionResult> AddItemToFishTank(AddItemToFishTank model)
        {
            // Find fish tank
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id);

            if (tank == null)
            {
                return BadRequest($"Fish tank with Id {model.Id} not found");
            }


            // check if it is a fish or an item and get its size
            int newItemSize = model.Fish != null ? model.Fish.Size : model.Item.Size;

            // check if the fish tank has the capacity to hold it
            if (tank.AvailableCapacity < newItemSize)
            {
                return BadRequest($"Fish tank with Id {model.Id} cannot fit the new item");
            }

            // add the new item
            if (model.Fish != null)
            {
                tank.AddFish(model.Fish);
            }
            else
            {
                tank.AddItem(model.Item);
            }

            await _context.SaveChangesAsync();

            return Ok(tank);
        }


        [HttpPost("/nextAvailable/fish")]
        public async Task<IActionResult> AddToNextAvailable(AddFishModel model) 
        {
            FishTank tank = await NextAvailableFishTankWithCapacity(model.Fish.Size);

            if (tank == null)
            {
                return BadRequest("No Tank found that can hold this fish, please add a new tank");
            }

            tank.AddFish(model.Fish);

            await _context.SaveChangesAsync();

            return Ok(tank);
        }

        [HttpPost("/nextAvailable/item")]
        public async Task<IActionResult> AddToNextAvailable(AddItemModel model)
        {
            FishTank tank = await NextAvailableFishTankWithCapacity(model.Decoration.Size);

            if (tank == null)
            {
                return BadRequest("No Tank found that can hold this fish, please add a new tank");
            }

            tank.AddItem(model.Decoration);

            await _context.SaveChangesAsync();

            return Ok(tank);
        }

        [HttpDelete("item")]
        public async Task<IActionResult> DeleteItem(RemoveItemModel model)
        {
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id) ;

            if (tank == null)
            {
                return BadRequest("No Tank found with Id");
            }

            tank.RemoveItem(model.Decoration.Id);

            await _context.SaveChangesAsync();

            return Ok(tank);
        }

        [HttpDelete("fish")]
        public async Task<IActionResult> DeleteFish(RemoveFishModel model)
        {
            FishTank tank = await _context.FishTank.FirstOrDefaultAsync(t => t.Id == model.Id);

            if (tank == null)
            {
                return BadRequest("No Tank found with Id");
            }

            tank.RemoveFish(model.Fish.Id);

            await _context.SaveChangesAsync();

            return Ok(tank);
        }

        private async Task<FishTank> NextAvailableFishTankWithCapacity(int capacity)
        {
            return await _context.FishTank.FirstOrDefaultAsync(t => t.AvailableCapacity >= capacity);
        }
    }
}

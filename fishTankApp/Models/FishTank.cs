using fishTankApp.CustomException;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fishTankApp.Models
{
    public class FishTank : Entity
    {
        [Required]
        [Range(5,38)]
        public int Capacity { get; set; }


        [Required]
        // decoratio nand plant 
        public IEnumerable<Decoration> Items { get; set; }

        [Required]
        public IEnumerable<Fish> Fishes { get; set; }

        public int AvailableCapacity { get; set; }

        public void RemoveItem(int decorationId)
        {
            Decoration decoration = Items.FirstOrDefault(i => i.Id == decorationId);

            if (decoration != null)
            {
                Items.ToList().Remove(decoration);
                AvailableCapacity += decoration.Size;
            }
        }

        public void RemoveItem(IEnumerable<int> decorationIds)
        {
            foreach (int decId in decorationIds)
            {
                RemoveItem(decId);
            }
        }

        public void RemoveFish(int fishId)
        {
            Fish fish = Fishes.FirstOrDefault(i => i.Id == fishId);

            if (fish != null)
            {
                Fishes.ToList().Remove(fish);
                AvailableCapacity += fish.Size;
            }
        }

        public void RemoveFish(IEnumerable<int> fishIds)
        {
            foreach (int fId in fishIds)
            {
                RemoveItem(fId);
            }
        }


        public void AddItem(Decoration decoration)
        {
            if (AvailableCapacity < decoration.Size) 
            {
                throw new NotEnoughCapacityException();
            }
            AvailableCapacity -= decoration.Size;
            Items.Append(decoration);
        }

        public void AddItem(IEnumerable<Decoration> decorations)
        {
            foreach (Decoration d in decorations)
            {
                AddItem(d);
            }
        }

        public void AddFish(IEnumerable<Fish> fishes)
        {
            foreach(Fish f in fishes)
            {
                AddFish(f);
            }
        }

        public void AddFish(Fish fish)
        {
            if (AvailableCapacity < fish.Size)
            {
                throw new NotEnoughCapacityException();
            }

            // Check to see if incompatible fish exists in our tank
            // note: Should avoid magic strings 
            switch (fish.Breed.Name)
            {
                case "Yellow lo":
                    if (Fishes.Any(f => f.Breed.Name == "Red Ganymede"))
                    {
                        throw new IncompatibleFishInTankException();
                    }
                    break;
                case "Red Ganymede":
                    if (Fishes.Any(f => f.Breed.Name == "Yellow lo" || f.Breed.Name == "Purple Callisto"))
                    {
                        throw new IncompatibleFishInTankException();
                    }
                    break;
                case "Purple Callisto":
                    if (Fishes.Any(f => f.Breed.Name == "Red Ganymede"))
                    {
                        throw new IncompatibleFishInTankException();
                    }
                    break;
                default:
                    break;
            }

            AvailableCapacity -= fish.Size;
            Fishes.Append(fish);
        }

    }
}

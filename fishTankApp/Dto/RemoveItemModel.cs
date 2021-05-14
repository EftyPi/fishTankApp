using fishTankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.Dto
{
    public class RemoveItemModel
    {
        public int Id { get; set; }
        public Decoration Decoration { get; set; }
    }
}

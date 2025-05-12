using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.Branch.Models
{
    public class BranchToUpdateDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Location { get; set; }
        public DateTime BranchDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public int? RegionId { get; set; }
    }
}

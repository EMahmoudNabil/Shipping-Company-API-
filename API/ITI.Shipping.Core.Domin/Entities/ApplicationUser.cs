using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ITI.Shipping.Core.Domin.Entities_Helper;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            Id = Guid.CreateVersion7().ToString();
            SecurityStamp = Guid.CreateVersion7().ToString();
        }
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //----------- StoreName For Merchant -------------------------------  
        public string? StoreName { get; set; }
        public decimal? PickupPrice { get; set; }
        public decimal? CanceledOrder { get; set; }
        //----------- Enum For Deduction-type For Employee -------------------------------  
        public DeductionTypes? DeductionTypes { get; set; }
        public decimal? DeductionCompanyFromOrder { get; set; }
        //----------- Obj From Branch and ForeignKey BranchId ---------------------------------
        [ ForeignKey(nameof(Branch))]
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        //----------- Obj From Region and ForeignKey RegionId ---------------------------------
        [ ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }
        //----------- Obj From City and ForeignKey CityId ---------------------------------
        [ForeignKey(nameof(City))]
        public int? CityId { get; set; }
        public virtual CitySetting? City { get; set; }
        //------------- ICollection From Order ------------------------------
        public virtual ICollection<Order> Orders { get; set; } = [];
        //------------- ICollection From CourierReport ------------------------------
        public virtual ICollection<CourierReport> CourierReports { get; set; } = [];
        //------------- ICollection From SpecialPickup ------------------------------
        public virtual ICollection<SpecialCityCost> SpecialPickups { get; set; } = [];
        //------------- ICollection From SpecialCourierRegion ------------------------------
        public virtual ICollection<SpecialCourierRegion> SpecialCourierRegion { get; set; } = [];
    }
}


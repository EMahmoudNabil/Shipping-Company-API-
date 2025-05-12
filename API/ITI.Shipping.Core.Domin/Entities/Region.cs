using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class Region
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Governorate { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //------------- ICollection From User ------------------------------
        public virtual ICollection<ApplicationUser> Users { get; set; } = [];
        //------------- ICollection From CitySetting ------------------------------
        public virtual ICollection<CitySetting> CitySettings { get; set; } = [];
        //------------- ICollection From Branch ------------------------------
        public virtual ICollection<Branch> Branches { get; set; } = [];
        //------------- ICollection From Order ------------------------------
        public virtual ICollection<Order> Orders { get; set; } = [];
        //------------- ICollection From SpecialCourierRegion ------------------------------
        public virtual ICollection<SpecialCourierRegion> SpecialCourierRegion { get; set; } = [];
    }
}
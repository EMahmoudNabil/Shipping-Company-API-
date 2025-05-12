using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime BranchDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }=false;
        //----------- Obj From Region and ForeignKey RegionId ---------------------------------
        [ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }
        //------------- ICollection From User ------------------------------
        public virtual ICollection<ApplicationUser> Users { get; set; } = [];
    }
}

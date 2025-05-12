using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class SpecialCourierRegion
    {
        public int Id { get; set; }

        //----------- Obj From Region and ForeignKey RegionId ---------------------------------
        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }
        public virtual Region? Region { get; set; }
        //----------- Obj From User and ForeignKey CourierId ---------------------------------
        [ForeignKey(nameof(Courier))]
        public string CourierId { get; set; } = string.Empty;
        public virtual ApplicationUser? Courier { get; set; }
    }
}
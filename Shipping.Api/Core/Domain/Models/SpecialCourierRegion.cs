

using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Api.Core.Domain.Models;

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

